using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using Newtonsoft.Json.Linq;
using ZhiFang.Digitlab.ServiceCommon;

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ZFReaRestfulService : ZhiFang.Digitlab.ReagentSys.ServerContract.IZFReaRestfulService
    {
        //订货单必填信息
        private string OrderDocRequiredField = "OrderDocNo,LabNo,CompNo";
        private string OrderDtlRequiredField = "GoodsNo,GoodsQty";
        //供货单必填信息
        private string SaleDocRequiredField = "SaleDocNo,LabNo,CompNo";
        private string SaleDtlRequiredField = "GoodsNo,GoodsQty";

        IBLL.ReagentSys.IBBmsCenOrderDoc IBBmsCenOrderDoc { get; set; }

        IBLL.ReagentSys.IBBmsCenOrderDtl IBBmsCenOrderDtl { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDoc IBBmsCenSaleDoc { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDtl IBBmsCenSaleDtl { get; set; }

        IBLL.ReagentSys.IBGoods IBGoods { get; set; }

        IBLL.ReagentSys.IBCenOrg IBCenOrg { get; set; }

        IBLL.ReagentSys.IBCenOrgCondition IBCenOrgCondition { get; set; }
        IBLL.ReagentSys.IBBmsCenSaleDocConfirm IBBmsCenSaleDocConfirm { get; set; }
        IBLL.ReagentSys.IBBmsCenSaleDtlConfirm IBBmsCenSaleDtlConfirm { get; set; }
        /// <summary>
        /// AppKey验证，验证用户的合法性
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="timestamp"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private BaseResultData UserAuthentication(string appkey, string timestamp, string token)
        {
            BaseResultData baseresultdata = new BaseResultData();
            ZhiFang.Common.Log.Log.Info("UserAuthentication 开始");
            string appSecret = "950df74b-ad5b-430f-8c20-bff65de3f04a";
            //判断AppKey是否存在
            //如果存在，取出appSecret，接口调用方ID
            //计算Token
            string token_server = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(timestamp + appSecret);
            if (token_server != token)
            {
                baseresultdata.success = false;
                baseresultdata.code = ((int)InterfaceCodeValue.token无效访问被拒绝).ToString();
                baseresultdata.message = "token无效，访问被拒绝！";
                ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                return baseresultdata;
            }
            DateTime clientTime = DateTime.ParseExact(timestamp, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime servertTime = DateTime.Now;
            TimeSpan span = servertTime - clientTime;
            int interval = System.Math.Abs((int)span.TotalSeconds);
            if (interval > 600)//600秒，客户端和服务器时间间隔大于600秒，访问被拒绝
            {
                baseresultdata.success = false;
                baseresultdata.code = ((int)InterfaceCodeValue.客户端时间和服务器时间间隔相差过大).ToString();
                baseresultdata.message = "客户端时间和服务器时间间隔相差过大，访问被拒绝！";
                ZhiFang.Common.Log.Log.Info(baseresultdata.code + "：" + baseresultdata.message);
                return baseresultdata;
            }
            ZhiFang.Common.Log.Log.Info("UserAuthentication 结束");
            return baseresultdata;
        }

        /// <summary>
        /// 数据验证，验证业务数据合法性
        /// </summary>
        /// <param name="dataType">数据种类</param>
        /// <param name="dataInfo">数据信息</param>
        /// <returns></returns>
        private BaseResultData DataAuthentication(int dataType, string dataInfo)
        {
            BaseResultData brdv = new BaseResultData();
            return brdv;
        }

        private BaseResultData ParaIsNullValidate(string appkey, string timestamp, string data, string token, string version)
        {
            ZhiFang.Common.Log.Log.Info("ParaIsNullValidate 开始");
            BaseResultData baseresultdata = new BaseResultData();
            if (string.IsNullOrEmpty(appkey))
            {
                baseresultdata.success = false;
                baseresultdata.message = "参数appkey不能为空！";
            }
            else
            if (string.IsNullOrEmpty(timestamp))
            {
                baseresultdata.success = false;
                baseresultdata.message = "参数timestamp不能为空！";
            }
            else
            if (string.IsNullOrEmpty(data))
            {
                baseresultdata.success = false;
                baseresultdata.message = "参数data不能为空！";
            }
            else
            if (string.IsNullOrEmpty(timestamp))
            {
                baseresultdata.success = false;
                baseresultdata.message = "参数token不能为空！";
            }
            if (!baseresultdata.success)
            {
                baseresultdata.code = ((int)InterfaceCodeValue.必填参数信息为空).ToString();
                ZhiFang.Common.Log.Log.Info(baseresultdata.code + "：" + baseresultdata.message);
            }
            ZhiFang.Common.Log.Log.Info("ParaIsNullValidate 结束");
            return baseresultdata;
        }

        private BaseResultData JudageRequiredFieldInIsNull(JToken jsonToken, string requiredField, string docName, int docType, bool isArray)
        {
            BaseResultData baseresultdata = new BaseResultData();
            ZhiFang.Common.Log.Log.Info("JudageRequiredFieldInIsNull 开始");
            IList<string> listField = requiredField.Split(',').ToList();
            bool isNull = false;
            string nullFieldName = "";//有空值的字段列表
            bool isVlueNull = false;//数组里的必填字段是否有空值的标志
            foreach (string field in listField)
            {
                JToken tokenField = null;
                if (isArray)
                {
                    var listValue = jsonToken.Select(p => p[field]).ToList();
                    foreach (var value in listValue)
                    {
                        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                        {
                            isVlueNull = true;
                            break;
                        }
                    }
                }
                else
                {
                    tokenField = jsonToken.SelectToken(field);
                    isVlueNull = (tokenField == null || string.IsNullOrWhiteSpace(tokenField.ToString()));
                }
                if (isVlueNull)
                {
                    isNull = true;
                    if (nullFieldName == "")
                        nullFieldName = field;
                    else
                        nullFieldName += "," + field;
                }
            }
            if (isNull)
            {
                baseresultdata.success = false;
                if (docType == 1)
                    baseresultdata.code = ((int)InterfaceCodeValue.订货单必填字段信息为空).ToString();
                else if (docType == 2)
                    baseresultdata.code = ((int)InterfaceCodeValue.供货单必填字段信息为空).ToString();
                baseresultdata.message = docName + "必填字段信息为空！字段名称：" + nullFieldName;
                ZhiFang.Common.Log.Log.Info(baseresultdata.code + "：" + baseresultdata.message);
            }
            ZhiFang.Common.Log.Log.Info("JudageRequiredFieldInIsNull 结束");
            return baseresultdata;
        }

        private BaseResultData OrderDocDataValidate(string data, ref string orderDocNo, ref string labNo, ref string compNo)
        {
            BaseResultData baseresultdata = new BaseResultData();
            JObject jsonObject = JObject.Parse(data);
            JToken tokenOrderDoc = jsonObject.SelectToken("OrderDocInfo");
            JToken tokenOrderDtl = jsonObject.SelectToken("OrderDtlList");
            if (tokenOrderDoc == null || string.IsNullOrWhiteSpace(tokenOrderDoc.ToString()))
            {
                baseresultdata.success = false;
                baseresultdata.code = ((int)InterfaceCodeValue.订货单主单信息为空).ToString();
                baseresultdata.message = "订货单主单信息为空！";
            }
            else
            {
                baseresultdata = JudageRequiredFieldInIsNull(tokenOrderDoc, OrderDocRequiredField, "订货单主单", 1, false);
                if (!baseresultdata.success)
                    return baseresultdata;
                labNo = tokenOrderDoc.SelectToken("LabNo").ToString();
                compNo = tokenOrderDoc.SelectToken("CompNo").ToString();
                orderDocNo = tokenOrderDoc.SelectToken("OrderDocNo").ToString();
            }

            if (tokenOrderDtl == null || string.IsNullOrWhiteSpace(tokenOrderDtl.ToString()))
            {
                baseresultdata.success = false;
                baseresultdata.code = ((int)InterfaceCodeValue.订货单产品信息为空).ToString();
                baseresultdata.message = "订货单产品信息为空！";
            }
            else
            {
                baseresultdata = JudageRequiredFieldInIsNull(tokenOrderDtl, OrderDtlRequiredField, "订货单产品", 1, true);
                if (!baseresultdata.success)
                    return baseresultdata;
            }
            if (baseresultdata.success)
            {
                JObject jsonOrderDoc = JObject.Parse(tokenOrderDoc.ToString());
                jsonOrderDoc.Add("BmsCenOrderDtlList", tokenOrderDtl);
                baseresultdata.data = jsonOrderDoc.ToString();
            }
            return baseresultdata;
        }

        public BaseResultData RS_OrderDocCreate(string appkey, string timestamp, string data, string token, string version)
        {
            BaseResultData baseresultdata = new BaseResultData();
            string orderDocNo = "";
            string labNo = "";
            string compNo = "";
            try
            {
                ZhiFang.Common.Log.Log.Info("RS_OrderDocCreate：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
                baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
                if (!baseresultdata.success)
                    return baseresultdata;
                baseresultdata = UserAuthentication(appkey, timestamp, token);
                if (!baseresultdata.success)
                    return baseresultdata;
                string orderDoc = ZhiFang.Common.Public.Base64Help.DecodingString(data, Encoding.UTF8);
                baseresultdata = OrderDocDataValidate(orderDoc, ref orderDocNo, ref labNo, ref compNo);
                if (baseresultdata.success)
                    orderDoc = baseresultdata.data;
                else
                    return baseresultdata;

                IBBmsCenOrderDoc.AddBmsCenOrderDoc(orderDoc);
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.code = ((int)InterfaceCodeValue.订货单创建错误).ToString();
                baseresultdata.message = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseresultdata;
        }

        /// <summary>
        /// 智方订单同步到第三方系统后
        /// 第三方系统调用此接口确认订单并传递确认相关信息到智方平台。
        /// </summary>
        public BaseResultData RS_OrderDocConfirm(string appkey, string timestamp, string data, string token, string version)
        {
            BaseResultData baseresultdata = new BaseResultData();
            ZhiFang.Common.Log.Log.Info("RS_OrderDocConfirm：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
            baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
            if (!baseresultdata.success)
                return baseresultdata;
            baseresultdata = UserAuthentication(appkey, timestamp, token);
            if (!baseresultdata.success)
                return baseresultdata;
            string dataValue = ZhiFang.Common.Public.Base64Help.DecodingString(data, Encoding.UTF8);
            //此处增加打标志处理逻辑
            return baseresultdata;
        }

        /// <summary>
        /// 智方订单同步到第三方系统后，如果此订单已发货
        /// 第三方系统调用此接口传递订单发货相关信息到智方平台。
        /// </summary>
        public BaseResultData RS_OrderDocSendOut(string appkey, string timestamp, string data, string token, string version)
        {
            BaseResultData baseresultdata = new BaseResultData();
            ZhiFang.Common.Log.Log.Info("RS_OrderDocSendOut：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
            baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
            if (!baseresultdata.success)
                return baseresultdata;
            baseresultdata = UserAuthentication(appkey, timestamp, token);
            if (!baseresultdata.success)
                return baseresultdata;
            string dataValue = ZhiFang.Common.Public.Base64Help.DecodingString(data, Encoding.UTF8);
            //此处增加打标志处理逻辑
            return baseresultdata;
        }

        private BaseResultData SaleDocDataValidate(string data, ref string saleDocNo, ref string labNo, ref string compNo)
        {
            BaseResultData baseresultdata = new BaseResultData();
            JObject jsonObject = JObject.Parse(data);
            JToken tokenSaleDoc = jsonObject.SelectToken("SaleDocInfo");
            JToken tokenSaleDtl = jsonObject.SelectToken("SaleDtlList");
            if (tokenSaleDoc == null || string.IsNullOrWhiteSpace(tokenSaleDoc.ToString()))
            {
                baseresultdata.success = false;
                baseresultdata.code = "2101";
                baseresultdata.message = ((int)InterfaceCodeValue.供货单主单信息为空).ToString(); ;
            }
            else
            {
                baseresultdata = JudageRequiredFieldInIsNull(tokenSaleDoc, SaleDocRequiredField, "供货单主单", 2, false);
                if (!baseresultdata.success)
                    return baseresultdata;
                labNo = tokenSaleDoc.SelectToken("LabNo").ToString();
                compNo = tokenSaleDoc.SelectToken("CompNo").ToString();
                saleDocNo = tokenSaleDoc.SelectToken("SaleDocNo").ToString();
            }

            if (tokenSaleDtl == null || string.IsNullOrWhiteSpace(tokenSaleDtl.ToString()))
            {
                baseresultdata.success = false;
                baseresultdata.code = ((int)InterfaceCodeValue.供货单产品信息为空).ToString(); ;
                baseresultdata.message = "供货单产品信息为空！";
            }
            else
            {
                baseresultdata = JudageRequiredFieldInIsNull(tokenSaleDtl, SaleDtlRequiredField, "供货单产品", 2, true);
                if (!baseresultdata.success)
                    return baseresultdata;
            }
            if (baseresultdata.success)
            {
                JObject jsonSaleDoc = JObject.Parse(tokenSaleDoc.ToString());
                jsonSaleDoc.Add("BmsCenSaleDtlList", tokenSaleDtl);
                baseresultdata.data = jsonSaleDoc.ToString();
            }
            return baseresultdata;
        }

        public BaseResultData RS_SaleDocCreate(string appkey, string timestamp, string data, string token, string version)
        {
            BaseResultData baseresultdata = new BaseResultData();
            string orderDocNo = "";
            string labNo = "";
            string compNo = "";
            try
            {
                ZhiFang.Common.Log.Log.Info("RS_SaleDocCreate：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
                baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
                if (!baseresultdata.success)
                    return baseresultdata;
                baseresultdata = UserAuthentication(appkey, timestamp, token);
                if (!baseresultdata.success)
                    return baseresultdata;
                string saleDoc = ZhiFang.Common.Public.Base64Help.DecodingString(data, Encoding.UTF8);
                baseresultdata = SaleDocDataValidate(saleDoc, ref orderDocNo, ref labNo, ref compNo);
                if (baseresultdata.success)
                    saleDoc = baseresultdata.data;
                else
                    return baseresultdata;

                //baseresultdata = IBBmsCenSaleDoc.AddBmsCenSaleDoc(saleDoc);
                baseresultdata = IBBmsCenSaleDoc.AddBmsCenSaleDoc(saleDoc, orderDocNo, labNo, compNo);
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.code = ((int)InterfaceCodeValue.供货单创建错误).ToString();
                baseresultdata.message = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseresultdata;
        }

        /// <summary>
        /// 智方供货单同步到第三方系统后
        /// 第三方系统调用此接口确认供货单并传递确认相关信息到智方平台。
        /// </summary>
        public BaseResultData RS_SaleDocConfirm(string appkey, string timestamp, string data, string token, string version)
        {
            BaseResultData baseresultdata = new BaseResultData();
            ZhiFang.Common.Log.Log.Info("RS_SaleDocConfirm：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
            baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
            if (!baseresultdata.success)
                return baseresultdata;
            baseresultdata = UserAuthentication(appkey, timestamp, token);
            if (!baseresultdata.success)
                return baseresultdata;
            string dataValue = ZhiFang.Common.Public.Base64Help.DecodingString(data, Encoding.UTF8);
            //此处增加打标志处理逻辑
            return baseresultdata;
        }

        #region 试剂平台与试剂客户端的服务接口
        /// <summary>
        /// 客户端物理删除平台订单信息(订单状态都为暂存或已提交才能删除)
        /// </summary>
        /// <param name="labOrgNo">实验室编号</param>
        /// <param name="orderDocNo">订单编号</param>
        /// <returns></returns>
        public BaseResultBool RS_UDTO_DelBmsCenOrderDocByLabOrgNoAndOrderDocNo(string labOrgNo, string orderDocNo)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(labOrgNo))
            {
                baseResultBool.BoolFlag = false;
                baseResultBool.ErrorInfo = "实验室编号不能为空!";
                return baseResultBool;
            }
            if (string.IsNullOrEmpty(orderDocNo))
            {
                baseResultBool.BoolFlag = false;
                baseResultBool.ErrorInfo = "订单编号不能为空!";
                return baseResultBool;
            }
            ZhiFang.Common.Log.Log.Debug(string.Format("实验室编号为:{0},订单编号为:{1},删除平台订单信息开始!", labOrgNo, orderDocNo));
            try
            {
                baseResultBool = IBBmsCenOrderDoc.DelBmsCenOrderDocByLabOrgNoAndOrderDocNo(labOrgNo, orderDocNo);
                baseResultBool.success = baseResultBool.BoolFlag;
                ZhiFang.Common.Log.Log.Debug(string.Format("实验室编号为:{0},订单编号为:{1},删除平台订单信息结束!执行结果为:{2}", labOrgNo, orderDocNo, baseResultBool.success));
            }
            catch (Exception ex)
            {
                string errorInfo = string.Format("实验室编号为:{0},订单编号为:{1},删除平台订单信息失败!{2}", labOrgNo, orderDocNo, ex.Message);
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + errorInfo;
                ZhiFang.Common.Log.Log.Error(errorInfo);
            }
            return baseResultBool;
        }

        /// <summary>
        /// 获取供货验收主单列表信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_SearchBmsCenSaleDocConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDocConfirm> entityList = new EntityList<BmsCenSaleDocConfirm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenSaleDocConfirm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenSaleDocConfirm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDocConfirm>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 依某一验收主单ID获取供货验收明细信息
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_SearchBmsCenSaleDtlConfirmByDocId(long docId, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtlConfirm> entityList = new EntityList<BmsCenSaleDtlConfirm>();
            try
            {
                string where = "bmscensaledtlconfirm.BmsCenSaleDocConfirm.Id=" + docId;
                entityList = IBBmsCenSaleDtlConfirm.SearchListByHQL(where, 0, 0);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtlConfirm>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 试剂客户端下载供货单相关服务接口
        public BaseResultData RS_Client_GetBmsCenSaleDoc(string appkey, string timestamp, string data, string token, string version)
        {
            BaseResultData baseresultdata = new BaseResultData();
            try
            {
                ZhiFang.Common.Log.Log.Info("RS_Client_GetBmsCenSaleDoc：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
                //baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                //baseresultdata = UserAuthentication(appkey, timestamp, token);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                //string dataValue = ZhiFang.Common.Public.Base64Help.DecodingString(data, Encoding.UTF8);
                JObject jsonObject = JObject.Parse(data);
                string labOrgNo = jsonObject.SelectToken("LabOrgNo").ToString();
                string compOrgNo = jsonObject.SelectToken("CompOrgNo").ToString();
                string saleDocNo = jsonObject.SelectToken("SaleDocNo").ToString();
                if (string.IsNullOrWhiteSpace(compOrgNo) || string.IsNullOrWhiteSpace(labOrgNo) || string.IsNullOrWhiteSpace(saleDocNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "参数平台供应商编码、平台实验室编码或供货单编号不能为空！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                IList<CenOrg> listCompCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=" + compOrgNo);
                if (listCompCenOrg == null || listCompCenOrg.Count == 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台供应商编码获取不到供应商信息，请确认编码是否正确！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                if (listCompCenOrg.Count > 1)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台供应商编码获取到多个供应商信息，请联系平台管理员确认编码！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                IList<CenOrg> listLabCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=" + labOrgNo);
                if (listLabCenOrg == null || listLabCenOrg.Count == 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台实验室编码获取不到实验室信息，请确认编码是否正确！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                if (listLabCenOrg.Count > 1)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台实验室编码获取到多个实验室信息，请联系平台管理员确认编码！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                IList<BmsCenSaleDoc> listBmsCenSaleDoc = IBBmsCenSaleDoc.SearchListByHQL(" bmscensaledoc.SaleDocNo=\'" + saleDocNo + "\'"+
                    " and bmscensaledoc.Comp.Id=" + listCompCenOrg[0].Id.ToString() +
                    " and bmscensaledoc.Lab.Id=" + listLabCenOrg[0].Id.ToString());
                if (listBmsCenSaleDoc == null || listBmsCenSaleDoc.Count == 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台供应商编码、平台实验室编码和供货单编号，找不到对应的供货单信息！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                if (listBmsCenSaleDoc.Count > 1)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台供应商编码、平台实验室编码和供货单编号获取到多条供货单信息，请联系平台管理员确认信息！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                string saleDocFields = "BmsCenSaleDoc_Id,BmsCenSaleDoc_SaleDocNo,BmsCenSaleDoc_OrderDocNo,BmsCenSaleDoc_OtherOrderDocNo," +
                    "BmsCenSaleDoc_Lab_OrgNo,BmsCenSaleDoc_Lab_CName,BmsCenSaleDoc_DeptName,BmsCenSaleDoc_Comp_OrgNo,BmsCenSaleDoc_Comp_CName," +
                    "BmsCenSaleDoc_UrgentFlag,BmsCenSaleDoc_UrgentFlagName,BmsCenSaleDoc_InvoiceNo,BmsCenSaleDoc_UserName,BmsCenSaleDoc_OperDate," +
                    "BmsCenSaleDoc_TotalPrice,BmsCenSaleDoc_Memo,BmsCenSaleDoc_Sender,BmsCenSaleDoc_Source";
                string saleDtlFields = "BmsCenSaleDoc_BmsCenSaleDtlList_Id,BmsCenSaleDoc_BmsCenSaleDtlList_SaleDtlNo,BmsCenSaleDoc_BmsCenSaleDtlList_PSaleDtlID," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_ProdGoodsNo,BmsCenSaleDoc_BmsCenSaleDtlList_ProdOrgName,BmsCenSaleDoc_BmsCenSaleDtlList_Goods_BarCodeMgr," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_GoodsName,BmsCenSaleDoc_BmsCenSaleDtlList_ShortCode,BmsCenSaleDoc_BmsCenSaleDtlList_Goods_GoodsNo," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_LotNo,BmsCenSaleDoc_BmsCenSaleDtlList_InvalidDate,BmsCenSaleDoc_BmsCenSaleDtlList_GoodsUnit,BmsCenSaleDoc_BmsCenSaleDtlList_UnitMemo," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_GoodsQty,BmsCenSaleDoc_BmsCenSaleDtlList_Price,BmsCenSaleDoc_BmsCenSaleDtlList_SumTotal," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_TaxRate,BmsCenSaleDoc_BmsCenSaleDtlList_ProdDate,BmsCenSaleDoc_BmsCenSaleDtlList_BiddingNo," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_GoodsSerial,BmsCenSaleDoc_BmsCenSaleDtlList_LotSerial,BmsCenSaleDoc_BmsCenSaleDtlList_PackSerial,BmsCenSaleDoc_BmsCenSaleDtlList_MixSerial," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_Goods_GoodsClass,BmsCenSaleDoc_BmsCenSaleDtlList_Memo";
                ParseObjectProperty pop = new ParseObjectProperty("");
                string josn = pop.GetObjectPropertyNoPlanish(listBmsCenSaleDoc[0], saleDocFields + "," + saleDtlFields);
                baseresultdata.data = josn;
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.message = "错误信息：" + ex.Message;
            }
            return baseresultdata;
        }

        public BaseResultData RS_Client_UpdateBmsCenSaleDocExtractFlag(string appkey, string timestamp, string data, string token, string version)
        {
            BaseResultData baseresultdata = new BaseResultData();
            try
            {
                ZhiFang.Common.Log.Log.Info("RS_Client_GetBmsCenSaleDoc：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
                //baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                //baseresultdata = UserAuthentication(appkey, timestamp, token);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                //string dataValue = ZhiFang.Common.Public.Base64Help.DecodingString(data, Encoding.UTF8);
                JObject jsonObject = JObject.Parse(data);
                string saleDocId = jsonObject.SelectToken("SaleDocId").ToString();
                string saleDtlIdList = jsonObject.SelectToken("SaleDtlIdList").ToString();
                if (string.IsNullOrWhiteSpace(saleDocId) || string.IsNullOrWhiteSpace(saleDtlIdList))
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "参数供货单ID或供货单明细ID不能为空！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                baseresultdata = IBBmsCenSaleDoc.EditExtractFlagSaleDocByIDAndDtlIDList(saleDocId, saleDtlIdList);
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.message = "错误信息：" + ex.Message;
            }
            return baseresultdata;
        }

        public BaseResultData RS_Client_OrderDocCreate(string appkey, string timestamp, string data, string token, string version)
        {
            BaseResultData baseresultdata = new BaseResultData();
            string orderDocNo = "";
            string labNo = "";
            string compNo = "";
            try
            {
                //ZhiFang.Common.Log.Log.Info("RS_OrderDocCreate：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
                //baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                //baseresultdata = UserAuthentication(appkey, timestamp, token);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                string orderDoc = ZhiFang.Common.Public.Base64Help.DecodingString(data, Encoding.UTF8);
                baseresultdata = OrderDocDataValidate(orderDoc, ref orderDocNo, ref labNo, ref compNo);
                if (baseresultdata.success)
                    orderDoc = baseresultdata.data;
                else
                    return baseresultdata;

                IBBmsCenOrderDoc.AddBmsCenOrderDoc(orderDoc);
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.code = ((int)InterfaceCodeValue.订货单创建错误).ToString();
                baseresultdata.message = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseresultdata;
        }

        public BaseResultData RS_Client_UpdateDownloadFlagByLab(string validatePara, string labID, string startDate, string endDate)
        {
            BaseResultData baseresultdata = new BaseResultData();
            try
            {
                //ZhiFang.Common.Log.Log.Info("RS_OrderDocCreate：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
                //baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                //baseresultdata = UserAuthentication(appkey, timestamp, token);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                if (string.IsNullOrEmpty(labID) || string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                {
                    baseresultdata.success = false;
                    baseresultdata.code = ((int)InterfaceCodeValue.必填参数信息为空).ToString();
                    baseresultdata.message = "错误信息：labID, startDate, endDate参数值中任意一个都不能为空！";
                    return baseresultdata;
                }
                baseresultdata = IBGoods.EditGoodsDownloadFlagByLabID(labID, startDate, endDate);
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.message = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseresultdata;
        }

        #endregion

        public BaseResultData RS_Test(string appkey, string timestamp, string data, string token, string version)
        {
            BaseResultData baseresultdata = new BaseResultData();
            try
            {
                ZhiFang.Common.Log.Log.Info("RS_Client_GetBmsCenSaleDoc：" + appkey + "|" + timestamp + "|" + data + "|" + token + "|" + version);
                //baseresultdata = ParaIsNullValidate(appkey, timestamp, data, token, version);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                //baseresultdata = UserAuthentication(appkey, timestamp, token);
                //if (!baseresultdata.success)
                //    return baseresultdata;
                //string dataValue = ZhiFang.Common.Public.Base64Help.DecodingString(data, Encoding.UTF8);
                JObject jsonObject = JObject.Parse(data);
                string compOrgNo = jsonObject.SelectToken("CompOrgNo").ToString();
                string saleDocNo = jsonObject.SelectToken("SaleDocNo").ToString();
                if (string.IsNullOrWhiteSpace(compOrgNo) || string.IsNullOrWhiteSpace(saleDocNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "参数平台供应商编码或供货单编号不能为空！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                IList<CenOrg> listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=" + compOrgNo);
                if (listCenOrg == null || listCenOrg.Count == 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台供应商编码获取不到供应商信息，请确认编码是否正确！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                if (listCenOrg.Count > 1)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台供应商编码获取到多个供应商信息，请联系平台管理员确认编码！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                IList<BmsCenSaleDoc> listBmsCenSaleDoc = IBBmsCenSaleDoc.SearchListByHQL(" bmscensaledoc.Comp.Id=" + listCenOrg[0].Id.ToString() +
                    " and bmscensaledoc.SaleDocNo=\'" + saleDocNo + "\'");
                if (listBmsCenSaleDoc == null || listBmsCenSaleDoc.Count == 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台供应商编码和供货单编号，找不到对应的供货单信息！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                if (listBmsCenSaleDoc.Count > 1)
                {
                    baseresultdata.success = false;
                    baseresultdata.message = "根据平台供应商编码和供货单编号获取到多条供货单信息，请联系平台管理员确认信息！";
                    ZhiFang.Common.Log.Log.Info(baseresultdata.message);
                    return baseresultdata;
                }
                string saleDocFields = "BmsCenSaleDoc_Id,BmsCenSaleDoc_SaleDocNo,BmsCenSaleDoc_OrderDocNo,BmsCenSaleDoc_OtherOrderDocNo," +
                    "BmsCenSaleDoc_Lab_OrgNo,BmsCenSaleDoc_Lab_CName,BmsCenSaleDoc_DeptName,BmsCenSaleDoc_Comp_OrgNo,BmsCenSaleDoc_Comp_CName," +
                    "BmsCenSaleDoc_UrgentFlag,BmsCenSaleDoc_UrgentFlagName,BmsCenSaleDoc_InvoiceNo,BmsCenSaleDoc_UserName,BmsCenSaleDoc_OperDate," +
                    "BmsCenSaleDoc_TotalPrice,BmsCenSaleDoc_Memo,BmsCenSaleDoc_Sender,BmsCenSaleDoc_Source";
                string saleDtlFields = "BmsCenSaleDoc_BmsCenSaleDtlList_Id,BmsCenSaleDoc_BmsCenSaleDtlList_SaleDtlNo,BmsCenSaleDoc_BmsCenSaleDtlList_PSaleDtlID" +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_ProdGoodsNo,BmsCenSaleDoc_BmsCenSaleDtlList_ProdOrgName,BmsCenSaleDoc_BmsCenSaleDtlList_Goods_BarCodeMgr," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_GoodsName,BmsCenSaleDoc_BmsCenSaleDtlList_ShortCode,BmsCenSaleDoc_BmsCenSaleDtlList_Goods_GoodsNo," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_LotNo,BmsCenSaleDoc_BmsCenSaleDtlList_InvalidDate,BmsCenSaleDoc_BmsCenSaleDtlList_GoodsUnit,BmsCenSaleDoc_BmsCenSaleDtlList_UnitMemo," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_GoodsQty,BmsCenSaleDoc_BmsCenSaleDtlList_Price,BmsCenSaleDoc_BmsCenSaleDtlList_SumTotal," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_TaxRate,BmsCenSaleDoc_BmsCenSaleDtlList_ProdDate,BmsCenSaleDoc_BmsCenSaleDtlList_BiddingNo," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_GoodsSerial,BmsCenSaleDoc_BmsCenSaleDtlList_LotSerial,BmsCenSaleDoc_BmsCenSaleDtlList_PackSerial,BmsCenSaleDoc_BmsCenSaleDtlList_MixSerial," +
                    "BmsCenSaleDoc_BmsCenSaleDtlList_Goods_GoodsClass,BmsCenSaleDoc_BmsCenSaleDtlList_Memo";
                ParseObjectProperty pop = new ParseObjectProperty("");
                string josn = pop.GetObjectPropertyNoPlanish(listBmsCenSaleDoc[0], saleDocFields + "," + saleDtlFields);
                baseresultdata.data = josn;
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.message = "错误信息：" + ex.Message;
            }
            return baseresultdata;
        }
    }
}
