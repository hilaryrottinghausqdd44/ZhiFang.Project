using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using ZhiFang.Digitlab.ReagentSys.ServerContract;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.ServiceCommon;
using System.Web;
using ZhiFang.Common.Public;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using ZhiFang.Digitlab.ReagentSys.BusinessObject;
using Spring.Context;
using Spring.Context.Support;
using System.Collections.Specialized;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReagentService : IReagentService
    {
        private string ErrorDateInfo = "开始时间和结束时间不能为空";

        public IBLL.ReagentSys.IBCenOrg IBCenOrg { get; set; }

        public IBLL.ReagentSys.IBCenOrgCondition IBCenOrgCondition { get; set; }

        IBLL.ReagentSys.IBGoods IBGoods { get; set; }

        public IBLL.ReagentSys.IBBmsCenOrderDoc IBBmsCenOrderDoc { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDoc IBBmsCenSaleDoc { get; set; }
        
        IBLL.ReagentSys.IBDBStoredProcedure IBDBStoredProcedure { get; set; }

        IBLL.RBAC.IBHREmployee IBHREmployee { get; set; }

        IBLL.RBAC.IBRBACUser IBRBACUser { get; set; }


        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlBarCodeList(long SaleDtlID, string SaleDtlBarCodeIDList, IList<BmsCenSaleDtlBarCode> BmsCenSaleDtlBarCodeList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBBmsCenSaleDoc.AddBmsCenSaleDtlBarCodeList(SaleDtlID, SaleDtlBarCodeIDList, BmsCenSaleDtlBarCodeList);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;        
        }

        public BaseResultDataValue ST_UDTO_MigrationCenQtyDtlTemp(string QtyDtlIDList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (!string.IsNullOrEmpty(QtyDtlIDList))
                {
                    string[] tempList = QtyDtlIDList.Split(',');
                    foreach (string strQtyDtlID in tempList)
                        IBDBStoredProcedure.MigrationCenQtyDtlTemp(Int64.Parse(strQtyDtlID));
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;           
        }

        public BaseResultDataValue ST_UDTO_StatReagentConsume(string strPara, int groupByType, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntityList = IBDBStoredProcedure.StatReagentConsume(strPara, groupByType);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenQtyDtlTempHistory>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_GetMaxOrgNoByOrgType(long orgTypeID, int minOrgNo)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                int maxOrgNo = IBCenOrg.GetMaxOrgNo(orgTypeID, minOrgNo);
                tempBaseResultDataValue.ResultDataValue = maxOrgNo.ToString();
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_GetMaxOrgNo()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                int maxOrgNo = IBCenOrg.GetMaxOrgNo();
                tempBaseResultDataValue.ResultDataValue = maxOrgNo.ToString();
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compID">供应商ID</param>
        /// <returns></returns>
        private BaseResultDataValue getBaronCusCode(long compID, ref long cenOrgID, ref string customerAccount)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取当前用户的ID信息";
                return baseResultDataValue;
            }
            HREmployee hrEmployee = IBHREmployee.Get(Int64.Parse(employeeID));
            IList<CenOrg> listCenOrg = null;
            if (hrEmployee.HRDept != null && (!string.IsNullOrEmpty(hrEmployee.HRDept.UseCode)))
            {
                listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=\'" + hrEmployee.HRDept.UseCode + "\'");
                if (listCenOrg != null && listCenOrg.Count > 0)
                {
                    if (listCenOrg.Count == 1)
                    {
                        cenOrgID = listCenOrg[0].Id;
                        string wherehql = " cenorgcondition.cenorg1.Id=" + compID.ToString() + " and cenorgcondition.cenorg2.Id=" + listCenOrg[0].Id.ToString();
                        IList<CenOrgCondition> listCenOrgCondition = IBCenOrgCondition.SearchListByHQL(wherehql);
                        if (listCenOrgCondition != null && listCenOrgCondition.Count > 0)
                        {
                            if (listCenOrgCondition.Count == 1)
                            {
                                baseResultDataValue.ResultDataValue = listCenOrgCondition[0].CustomerCode;
                                customerAccount = listCenOrgCondition[0].CustomerAccount;
                            }
                            else
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "获取到" + listCenOrgCondition.Count.ToString() + "条组织机构代码";
                            }
                        }
                        else
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "无法获取组织机构代码";
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "获取到" + listCenOrg.Count.ToString() + "条组织机构";
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取组织机构";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "根据用户获取到的组织机构代码为空";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compID">供应商ID</param>
        /// <returns></returns>
        private BaseResultDataValue BaronCustomerCode(long compID, long labID, ref string customerCode, ref string customerAccount)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string wherehql = " cenorgcondition.cenorg1.Id=" + compID.ToString() + " and cenorgcondition.cenorg2.Id=" + labID.ToString();
            IList<CenOrgCondition> listCenOrgCondition = IBCenOrgCondition.SearchListByHQL(wherehql);
            if (listCenOrgCondition != null && listCenOrgCondition.Count > 0)
            {
                if (listCenOrgCondition.Count == 1)
                {
                    customerCode = listCenOrgCondition[0].CustomerCode;
                    customerAccount = listCenOrgCondition[0].CustomerAccount;
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "在平台获取到" + listCenOrgCondition.Count.ToString() + "条巴瑞接口账户相关配置信息";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法在平台获取巴瑞接口账户相关配置信息";
            }


            return baseResultDataValue;
        }

        //private BaseResultDataValue Baron_UDTO_GetGoods(long compID, string searchCode, int pageSize, EntityList<Goods> listGoods)
        private BaseResultDataValue Baron_UDTO_GetGoods(long compID, string cenOrgNo, string searchCode, int pageSize)
        {
            string jsoncode = "";
            string token = "";
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //tempResult.ResultDataValue = "ZRYH"; //测试使用
            if (!string.IsNullOrEmpty(cenOrgNo))
            {
                jsoncode = "{\"CusCode\":\"" + cenOrgNo + 
                    "\",\"currentpage\":\"1\",\"pagesize\":\""+
                    pageSize.ToString() + "\",\"GoodsType\":\"1\",\"SearchCode\":\"" +
                    searchCode + "\",\"SearchType\":\"\"}";
                token = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(jsoncode + "0ecc6701d5f42298");
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "根据用户获取到的组织机构代码为空";
                return baseResultDataValue;
            }
            ServiceReference_Baron.Pro_GetListRequest request = new ServiceReference_Baron.Pro_GetListRequest();
            request.Body = new ServiceReference_Baron.Pro_GetListRequestBody();
            request.Body.jsoncode = jsoncode;
            request.Body.token = token;
            ServiceReference_Baron.UserApiSoap userApiSoap = new ServiceReference_Baron.UserApiSoapClient();
            ServiceReference_Baron.Pro_GetListResponse response = userApiSoap.Pro_GetList(request);
            string strResult = response.Body.Pro_GetListResult;
            if (!string.IsNullOrEmpty(strResult))
            {
                JObject jsonObject = JObject.Parse(strResult);
                strResult = jsonObject["Rows"].ToString();
                strResult = Regex.Replace(strResult, "cInvCode", "ProdGoodsNo");
                strResult = Regex.Replace(strResult, "cInvName", "CName");
                strResult = Regex.Replace(strResult, "cEnglishName", "EName");
                strResult = Regex.Replace(strResult, "cCurrencyName", "ShortCode");
                strResult = Regex.Replace(strResult, "cInvStd", "UnitMemo");
                strResult = Regex.Replace(strResult, "cComUnitName", "UnitName");
                strResult = Regex.Replace(strResult, "iNInvSaleCost", "Price");
                strResult = Regex.Replace(strResult, "ApplicableModels", "Equipment");
                strResult = Regex.Replace(strResult, "cProClassName", "GoodsClassType");
                //IList<Goods> goodslist = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<Goods>>(strResult);
                //if (goodslist != null && goodslist.Count>0)
                //{
                //    foreach (Goods goods in goodslist)
                //        goods.GoodsSource = 1;  
                //    listGoods.count = goodslist.Count; 
                //    listGoods.list = goodslist;
                //}
            }
            baseResultDataValue.ResultDataValue = strResult;
            return baseResultDataValue;
        }

        //cInvCode:试剂(商品)编号
        //cInvName试剂(商品)名称	CName中文名
        //cEnglishName试剂英文名称	EName英文名
        //cCurrencyName助记码	ShortCode代码
        //cInvStd规格型号	UnitMemo单位描述
        //cComUnitName计量单位	UnitName单位
        //iNInvSaleCost协议售价	Price单价
        //ApplicableModels适用机型	
        //cInvCName商品分类	GoodsClass一级分类

        private BaseResultDataValue Baron_UDTO_AddGoodsOrder(long compID, string searchCode, int pageSize, EntityList<Goods> listGoods)
        {
            string jsoncode = "";
            string token = "";
            long cenOrgID = 0;
            string customerAccount = "";
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BaseResultDataValue tempResult = getBaronCusCode(compID, ref cenOrgID, ref customerAccount);
            if (!tempResult.success)
            {
                return tempResult;
            }
            //tempResult.ResultDataValue = "ZRYH"; //测试使用
            if (!string.IsNullOrEmpty(tempResult.ResultDataValue))
            {
                jsoncode = "{\"CusCode\":\"" + tempResult.ResultDataValue +
                    "\",\"currentpage\":\"1\",\"pagesize\":\"" +
                    pageSize.ToString() + "\",\"GoodsType\":\"1\",\"SearchCode\":\"" +
                    searchCode + "\",\"SearchType\":\"\"}";
                token = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(jsoncode + "0ecc6701d5f42298");
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "根据用户获取到的组织机构代码为空";
                return baseResultDataValue;
            }
            ServiceReference_Baron.Order_SaveSubRequest request = new ServiceReference_Baron.Order_SaveSubRequest();
            request.Body = new ServiceReference_Baron.Order_SaveSubRequestBody();
            request.Body.jsoncode = jsoncode;
            request.Body.token = token;
            ServiceReference_Baron.UserApiSoap userApiSoap = new ServiceReference_Baron.UserApiSoapClient();
            ServiceReference_Baron.Order_SaveSubResponse response = userApiSoap.Order_SaveSub(request);
            string strResult = response.Body.Order_SaveSubResult;
            baseResultDataValue.ResultDataValue = "";
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_SearchGoodsByHQL(long compID, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<Goods> tempEntityList = new EntityList<Goods>();
            if (compID <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "供应商ID小于等于0";
            }
            CenOrg cenorg = IBCenOrg.Get(compID);
            if (cenorg == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法根据供应商ID获取供应商信息";
            }
            long cenOrgID = 0;
            string customerAccount = "";
            BaseResultDataValue tempResult = getBaronCusCode(compID, ref cenOrgID, ref customerAccount);
            if (!tempResult.success)
            {
                return tempResult;
            }
            try
            {               
                if (cenorg.OrgNo == 3059)
                {
                    BaseResultDataValue tempJosn = Baron_UDTO_GetGoods(compID, tempResult.ResultDataValue, "", 1000);
                    if (cenOrgID > 0 && (!string.IsNullOrEmpty(tempJosn.ResultDataValue)))
                        tempEntityList = IBGoods.EditBaronGetGoods(compID, cenOrgID, tempJosn.ResultDataValue);
                }
                else
                {
                    if ((sort != null) && (sort.Length > 0))
                    {
                        tempEntityList = IBGoods.SearchListByHQL(" goods.Comp.Id=" + compID.ToString() + " and goods.CenOrg.Id=" + cenOrgID.ToString(), CommonServiceMethod.GetSortHQL(sort), page, limit);
                    }
                    else
                    {
                        tempEntityList = IBGoods.SearchListByHQL(" goods.Comp.Id=" + compID.ToString() + " and goods.CenOrg.Id=" + cenOrgID.ToString(), page, limit);
                    }
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Goods>(tempEntityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
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

        public BaseResultDataValue RS_UDTO_UpdateBmsCenOrderDoc(BmsCenOrderDoc bmsCenOrderDoc, string mainFields, IList<BmsCenOrderDtl> listAddBmsCenOrderDtl, IList<BmsCenOrderDtl> listUpdateBmsCenOrderDtl, string childFields, string delBmsCenOrderDtlID, int IsWriteExternalSystem, int IsAutoCreateBmsCenSaleDoc)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBBmsCenOrderDoc.EditBmsCenOrderDoc(bmsCenOrderDoc, mainFields, listAddBmsCenOrderDtl, listUpdateBmsCenOrderDtl, childFields, delBmsCenOrderDtlID, IsAutoCreateBmsCenSaleDoc);
                bmsCenOrderDoc = IBBmsCenOrderDoc.Get(bmsCenOrderDoc.Id);
                if (baseResultDataValue.success && IsWriteExternalSystem != 0 && bmsCenOrderDoc.Status == IsWriteExternalSystem)
                {
                    if (bmsCenOrderDoc.Comp == null)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "新增订单成功，但提交失败，原因：供应商等于Null";
                    }
                    baseResultDataValue = RS_UDTO_OrderSaveToOtherSystem(bmsCenOrderDoc.Id);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        private BaseResultDataValue Baron_UDTO_OrderCreatSubAPI(long compID, string userNo, string customerAccount, string goodsJson, string orderMemo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string jsoncode = "";
            string token = "";
            string fileTime = DateTime.Now.ToFileTime().ToString();
            if (!string.IsNullOrEmpty(userNo))
            {
                jsoncode = IBBmsCenOrderDoc.GetBaronOrderJson(fileTime, userNo, customerAccount, goodsJson, orderMemo);
                token = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(fileTime + "0ecc6701d5f42298");
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "根据用户获取到的组织机构代码为空";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            ServiceReference_Baron.Order_CreatSubAPIRequest request = new ServiceReference_Baron.Order_CreatSubAPIRequest();
            request.Body = new ServiceReference_Baron.Order_CreatSubAPIRequestBody();
            request.Body.jsoncode = jsoncode;
            request.Body.token = token;
            ServiceReference_Baron.UserApiSoap userApiSoap = new ServiceReference_Baron.UserApiSoapClient();
            ZhiFang.Common.Log.Log.Info("OrderCreatSubAPI");
            ZhiFang.Common.Log.Log.Info(request.Body.jsoncode);
            ZhiFang.Common.Log.Log.Info(request.Body.token);
            ServiceReference_Baron.Order_CreatSubAPIResponse response = userApiSoap.Order_CreatSubAPI(request);
            ZhiFang.Common.Log.Log.Info(response.Body.Order_CreatSubAPIResult);
            string strResult = response.Body.Order_CreatSubAPIResult;
            JObject jsonObject = JObject.Parse(strResult);
            strResult = jsonObject["resultvalue"].ToString();
            if (strResult == "-1")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "-1";
                baseResultDataValue.ErrorInfo = "验证或生成订单失败";
            }
            else if (strResult == "-2")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "-2";
                baseResultDataValue.ErrorInfo = " 验证异常Errinfo:" + jsonObject["errinfo"].ToString();
            }
            else if (strResult == "1")
            {
                baseResultDataValue.ResultDataValue = "1";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 智方平台订单写入其他平台系统
        /// </summary>
        /// <param name="orderDocID">订单ID</param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_OrderSaveToOtherSystem(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenOrderDoc orderDoc = IBBmsCenOrderDoc.Get(id);
            if (orderDoc != null && orderDoc.BmsCenOrderDtlList != null && orderDoc.BmsCenOrderDtlList.Count > 0)
            {
                if (orderDoc.IsThirdFlag == 1)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "订单已同步其他系统平台";
                    return baseResultDataValue;
                }
                CenOrg compCenOrg = IBCenOrg.Get(orderDoc.Comp.Id);//获取订单的供应商信息
                CenOrg labCenOrg = IBCenOrg.Get(orderDoc.Lab.Id);//获取订单的实验室信息
                if (compCenOrg == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "订单同步其他系统平台失败：无法根据供应商ID" + orderDoc.Comp.Id.ToString() + "获取供应商信息";
                    return baseResultDataValue;
                }
                if (labCenOrg == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "订单同步其他系统平台失败：无法根据实验室ID" + orderDoc.Lab.Id.ToString() + "获取实验室信息";
                    return baseResultDataValue;
                }
                BaseResultDataValue brdv = GetInterfaceConfig((int)InterfaceType.订货单接口, compCenOrg.OrgNo.ToString());
                if (brdv.success)
                {
                    string customerCode = "";
                    string customerAccount = "";
                    BaseResultDataValue tempResult = BaronCustomerCode(orderDoc.Comp.Id, orderDoc.Lab.Id, ref customerCode, ref customerAccount);
                    if (!tempResult.success)
                    {
                        ZhiFang.Common.Log.Log.Info(tempResult.ErrorInfo);
                        return tempResult;
                    }
                    string strJoin = IBBmsCenOrderDoc.GetBaronGoodsJson(orderDoc.BmsCenOrderDtlList, customerCode);
                    ZhiFang.Common.Log.Log.Info(strJoin);
                    baseResultDataValue = Baron_UDTO_OrderCreatSubAPI(orderDoc.Comp.Id, customerCode, customerAccount, strJoin, orderDoc.Memo);
                    if (baseResultDataValue.success)
                    {
                        IBBmsCenOrderDoc.EditBmsCenOrderDocThirdFlag(id);
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "订单同步其他系统平台失败：无法根据订单ID" + id.ToString() + "获取订单信息";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_AddBmsCenOrderDoc(BmsCenOrderDoc entity, int IsWriteExternalSystem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {

                baseResultDataValue = IBBmsCenOrderDoc.AddBmsCenOrderDoc(entity);

                if (IsWriteExternalSystem != 0 && entity.Status == IsWriteExternalSystem)
                {
                    if (entity.Comp == null)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "新增订单成功，但提交失败，原因：供应商等于Null";
                    }
                    baseResultDataValue = RS_UDTO_OrderSaveToOtherSystem(entity.Id);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_BmsCenSaleDocByOrderDoc(BmsCenOrderDoc entity, string fields)
        {
            IBBmsCenOrderDoc.Entity = entity;
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        baseResultDataValue.success = IBBmsCenOrderDoc.Update(tempArray);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenOrderDoc.Edit();
                }
                baseResultDataValue = IBBmsCenSaleDoc.AddBmsCenSaleDocByOrderDoc(IBBmsCenOrderDoc.Entity.Id);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_CopyGoodsByID(string listID, long compId, long cenOrgId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if ((listID != null) && (listID.Length > 0))
                {
                    baseResultDataValue = IBGoods.CopyGoodsByID(listID, compId, cenOrgId); 
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：listID参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenOrderDoc.Edit();
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;        
        
        }

        //2017-02-21 Add
        public BaseResultDataValue RS_UDTO_ConfirmSaleByDocID(string docID, string invoiceNo, string accepterMemo, string secAccepterAccount, string secAccepterPwd, int secAccepterType, IList<BmsCenSaleDtl> listBmsCenSaleDtl, int isSplit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string secAccepterID = "";
            string secAccepterName = "";
            if (string.IsNullOrEmpty(docID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单ID不能为空！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(secAccepterAccount) || string.IsNullOrEmpty(secAccepterPwd))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：次验收人账号或密码不能为空！";
                return baseResultDataValue;
            }
            else
            {
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(secAccepterAccount);
                if (tempRBACUser.Count >= 1)
                {
                    baseResultDataValue = IBBmsCenSaleDoc.JudgeIsSameOrg(secAccepterType, docID, secAccepterAccount, secAccepterPwd, tempRBACUser[0]);
                    if (!baseResultDataValue.success)
                       return baseResultDataValue;
                    secAccepterID = tempRBACUser[0].HREmployee.Id.ToString();
                    secAccepterName = tempRBACUser[0].HREmployee.CName;
                }
                else if (tempRBACUser.Count <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：次验收人账号不存在！";
                    return baseResultDataValue;               
                }
            }
            try
            {
                baseResultDataValue = IBBmsCenSaleDoc.AddConfirmSaleDocByID(docID, invoiceNo, accepterMemo, SessionHelper.GetSessionValue(DicCookieSession.EmployeeID), SessionHelper.GetSessionValue(DicCookieSession.EmployeeName), secAccepterID, secAccepterName, listBmsCenSaleDtl, isSplit);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        //2017-05-11 Add
        /// <summary>
        /// 供货单验收服务
        /// 此服务只打验收标志和把异常的子单的混合条码清空，不错供货单拆分操作
        /// </summary>
        /// <param name="saleDocID">供货单ID</param>
        /// <param name="saleDtlIDList">异常子单的ID字符串</param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_ConfirmSaleDocByIDAndDtlIDList(string saleDocID, string saleDtlIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(saleDocID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单ID不能为空！";
                return baseResultDataValue;
            }
            try
            {
                baseResultDataValue = IBBmsCenSaleDoc.EditConfirmSaleDocByIDAndDtlIDList(saleDocID, saleDtlIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        //2017-07-20 Add
        public BaseResultDataValue RS_UDTO_ConfirmSaleDocByID(string docID, string invoiceNo, string accepterMemo, string secAccepterAccount, string secAccepterPwd, int secAccepterType, IList<BmsCenSaleDtl> listSaleDtlError, IList<BmsCenSaleDtl> listSaleDtlNoBarCode, int isSplit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string secAccepterID = "";
            string secAccepterName = "";
            if (string.IsNullOrEmpty(docID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单ID不能为空！";
                return baseResultDataValue;
            }
            try
            {
                baseResultDataValue = IBBmsCenSaleDoc.AddConfirmSaleDocByID(docID, invoiceNo, accepterMemo, SessionHelper.GetSessionValue(DicCookieSession.EmployeeID), SessionHelper.GetSessionValue(DicCookieSession.EmployeeName), secAccepterID, secAccepterName, listSaleDtlError, listSaleDtlNoBarCode, isSplit);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        //2017-03-24 Add
        public BaseResultDataValue RS_UDTO_ConfirmSaleCancel(string docID, string reason, string accepterAccount, string accepterPwd)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(docID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单ID不能为空！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(accepterAccount) || string.IsNullOrEmpty(accepterPwd))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供应商账号或密码不能为空！";
            }
            else
            {
                IList<RBACUser> rbacUser = IBRBACUser.SearchRBACUserByUserAccount(accepterAccount);
                if (rbacUser.Count >= 1)
                {
                    string strPassWord = SecurityHelp.MD5Encrypt(accepterPwd, SecurityHelp.PWDMD5Key);
                    bool tempBool = (rbacUser[0].Account == accepterAccount) && (rbacUser[0].PWD == strPassWord) && (!rbacUser[0].AccLock);
                    if (tempBool)
                    {
                        try
                        {
                            long secAccepterID = rbacUser[0].HREmployee.Id;
                            baseResultDataValue = IBBmsCenSaleDoc.EditConfirmSaleDocCancel(docID, reason, accepterAccount, secAccepterID);
                        }
                        catch (Exception ex)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                            //throw new Exception(ex.Message);
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "错误信息：供应商账号登录密码错误！";
                    }
                }
                else if (rbacUser.Count <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：供应商账号不存在！";
                }
            }
            if (!string.IsNullOrEmpty(baseResultDataValue.ErrorInfo))
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_SplitSaleDocCancel(string docID, string reason)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(docID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单ID不能为空！";
                return baseResultDataValue;
            }

            baseResultDataValue = IBBmsCenSaleDoc.EditSplitSaleDocCancelByID(long.Parse(docID), reason);
            return baseResultDataValue;
        }

        //2017-05-06 Add
        /// <summary>
        /// 供货单产品拆分条码服务
        /// </summary>
        /// <param name="docID">供货单ID</param>
        /// <param name="splitType">是否强制拆分(强制拆分指已经拆分过的供货单重新拆分)，默认为0，不强制拆分；1强制拆分</param>
        /// <param name="compatibleType">兼容类型，0为兼容旧流程，1为新流程（主要判断条码打印标志）</param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_SplitSaleDocByID(string docID, int splitType, int compatibleType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(docID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单ID不能为空！";
                return baseResultDataValue;
            }
            try
            {
                baseResultDataValue = IBBmsCenSaleDoc.AddSplitSaleDocByID(docID, splitType, compatibleType);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        //2017-05-06 Add
        /// <summary>
        /// 供货单审核服务
        /// </summary>
        /// <param name="docID">供货单ID</param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_CheckSaleDocByID(string docID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(docID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单ID不能为空！";
                return baseResultDataValue;
            }
            try
            {
                baseResultDataValue = IBBmsCenSaleDoc.EditCheckSaleDocByID(docID, 0);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        //2017-07-28 Add
        /// <summary>
        /// 供货单审核服务
        /// </summary>
        /// <param name="idList">供货单ID列表</param>
        /// <param name="validateType">验证类型，0为旧验证流程，1为先拆分后验证流程</param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_CheckSaleDocByIDList(string idList, int validateType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(idList))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：供货单ID不能为空！";
                return baseResultDataValue;
            }
            try
            {
                baseResultDataValue = IBBmsCenSaleDoc.EditCheckSaleDocByID(idList, 1);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量审核和拆分供货单服务
        /// 说明：此服务的功能是供货单验收和拆分，因此服务设计比较早，当时还没有验收的概念，审核相当于验收。
        /// </summary>
        /// <param name="listID">供货单ID列表</param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_CheckSaleByDocIDList(string listID, int isSplit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if ((listID != null) && (listID.Length > 0))
                {
                    baseResultDataValue = IBBmsCenSaleDoc.AddConfirmSaleDocByIDList(listID, isSplit);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：listID参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenOrderDoc.Edit();
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;

        }

        public BaseResultDataValue RS_UDTO_SetOrderDocDeleteFlagByID(string idList, int deleteFlag)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBBmsCenOrderDoc.EditOrderDocDeleteFlagByID(idList, deleteFlag);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_SetSaleDocDeleteFlagByID(string idList, int deleteFlag)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBBmsCenSaleDoc.EditSaleDocDeleteFlagByID(idList, deleteFlag);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }            
            return baseResultDataValue;
        }

        //2017-03-06 Add
        public BaseResultDataValue RS_UDTO_SearchGoodsByCompID(string labCenOrgID, string compCenOrgID, string where, string fields, int page, int limit, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<Goods> tempEntityList = new EntityList<Goods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBGoods.SearchGoodsByCompID(labCenOrgID, compCenOrgID, where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBGoods.SearchGoodsByCompID(labCenOrgID, compCenOrgID, where, "", page, limit);
                }
                
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Goods>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        //2017-03-07 Add
        public Message RS_UDTO_StatBmsCenSaleDocTotalPrice()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDoc> tempEntityList = new EntityList<BmsCenSaleDoc>();
            string listID = "";
            string fields = "";
            string[] allkey = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < allkey.Length; i++)
            {
                switch (allkey[i])
                {
                    case "listID":
                        listID = HttpContext.Current.Request.Form["listID"];
                        break;
                    case "fields":
                        fields = HttpContext.Current.Request.Form["fields"];
                        break;
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(listID))
                {
                    tempEntityList = IBBmsCenSaleDoc.StatBmsCenSaleDocTotalPrice(listID);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDoc>(tempEntityList);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "参数listID的值不能为空！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        //2017-05-09 Add
        /// <summary>
        /// 订单审核服务
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_CheckBmsCenOrderDocByID(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (id > 0)
                {
                    baseResultDataValue = IBBmsCenOrderDoc.EditCheckBmsCenOrderDocByID(id);
                    if (baseResultDataValue.success)
                    {
                        BaseResultDataValue brdv = IBBmsCenOrderDoc.BmsCenOrderDocCheckedAndPush((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, id);
                        if (!brdv.success)
                            ZhiFang.Common.Log.Log.Info(brdv.ErrorInfo);
                        baseResultDataValue = RS_UDTO_OrderSaveToOtherSystem(id);
                        if (!baseResultDataValue.success)
                            ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：参数id的值小于或等于0！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;

        }

        public BaseResultDataValue RS_UDTO_EditBmsCenSaleDocTotalPrice(long saleDocID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDocID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_EditBmsCenOrderDocTotalPrice(long orderDocID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBBmsCenOrderDoc.EditBmsCenOrderDocTotalPrice(orderDocID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_BmsCenSaleDtlStat(string beginDate, string endDate, string listStatus, long compID, long labID, long goodID, string goodLotNo, int groupbyType, string fields, bool isPlanish, int page, int limit, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(beginDate) || string.IsNullOrEmpty(endDate))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = ErrorDateInfo;
                    return tempBaseResultDataValue;
                }
                endDate = DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd");
                if ((sort != null) && (sort.Length > 0))
                    sort = CommonServiceMethod.GetSortHQL(sort);
                else
                    sort = "" ;
                var tempEntityList = IBBmsCenSaleDoc.StatBmsCenSaleDtlGoodsQty(beginDate, endDate, listStatus, compID, labID, goodID, goodLotNo, groupbyType, page, limit, sort);
               
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    ZhiFang.Common.Log.Log.Info("Stat 序列化开始!");
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDtl>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                    ZhiFang.Common.Log.Log.Info("Stat 序列化结束!");
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }


        #region 导入服务

        public Message ST_UDTO_UploadBmsCenSaleDocDataByExcel()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    baseResultDataValue.ErrorInfo = "未检测到文件！";
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.success = false;
                }
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string parentPath = HttpContext.Current.Server.MapPath("~/UploadBaseTableInfo/");
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    string LabID = HttpContext.Current.Request.Form["LabID"];
                    string CompID = HttpContext.Current.Request.Form["CompID"];
                    string filepath = Path.Combine(parentPath, Common.Public.GUIDHelp.GetGUIDString() + '_' + Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);
                    baseResultDataValue = IBBmsCenSaleDoc.ReadBmsCenSaleDocDataFormExcel(LabID, CompID, filepath, HttpContext.Current.Server.MapPath("~/"));
                }
                else
                {
                    baseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    baseResultDataValue.success = false;
                };
            }
            catch (Exception ex)
            {
                baseResultDataValue.ErrorInfo = ex.Message;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
            }
            string strResult =ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        } //

        public Message ST_UDTO_UploadGoodsDataByExcel()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    baseResultDataValue.ErrorInfo = "未检测到文件！";
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.success = false;
                }
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string parentPath = HttpContext.Current.Server.MapPath("~/UploadBaseTableInfo/");
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    string LabID = HttpContext.Current.Request.Form["LabID"];
                    string CompID = HttpContext.Current.Request.Form["CompID"];
                    string ProdID = HttpContext.Current.Request.Form["ProdID"];
                    string filepath = Path.Combine(parentPath, Common.Public.GUIDHelp.GetGUIDString() + '_' + Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);
                    baseResultDataValue = IBGoods.CheckGoodsExcelFormat(filepath, HttpContext.Current.Server.MapPath("~/"));
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue = IBGoods.AddGoodsDataFormExcel(LabID, CompID, ProdID, filepath, HttpContext.Current.Server.MapPath("~/"));
                    }
                }
                else
                {
                    baseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    baseResultDataValue.success = false;
                };
            }
            catch (Exception ex)
            {
                baseResultDataValue.ErrorInfo = ex.Message;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        } //

        #endregion

        #region 导出服务

        public Message RS_UDTO_GetReportDetailExcelPath()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string reportType = ""; 
            string idList=""; 
            string where=""; 
            string isHeader="";
            string tempFileName = "";
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            DataSet ds = null;

            string[] allkey = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < allkey.Length; i++)
            {
                switch (allkey[i])
                {
                    case "reportType":
                        reportType = HttpContext.Current.Request.Form["reportType"];
                        break;
                    case "idList":
                        idList = HttpContext.Current.Request.Form["idList"];
                        break;
                    case "where":
                        where = HttpContext.Current.Request.Form["where"];
                        break;
                    case "isHeader":
                        isHeader = HttpContext.Current.Request.Form["isHeader"];
                        break;
                }
            }
            if (reportType == "1")
            {
                tempFileName = "试剂信息列表";
                ds = IBGoods.GetGoodsInfoByID(idList, where, basePath + "\\BaseTableXML\\Report_Goods.xml");
            }
            try
            {
                string excelName = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "." + CommonRS.GetExcelExtName();
                string tempFilePath = basePath + "\\TempExcelFile\\" + excelName;
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    string headerText = "";
                    if (isHeader == "1")
                        headerText = tempFileName;
                    if (!ExcelHelper.CreateExcelByNPOI(ds.Tables[0], headerText, tempFilePath))
                    {
                        tempFilePath = "";
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "生成Excel文件失败！";
                    }
                }
                else
                {
                    tempFilePath = "";
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无任何试剂记录信息！";
                }
                if (!string.IsNullOrEmpty(tempFilePath) && File.Exists(tempFilePath))
                {
                    baseResultDataValue.ResultDataValue = "/TempExcelFile/" + excelName;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        public Stream RS_UDTO_ReportDetailToExcel(int reportType, string idList, int operateType, int isHeader)
        {
            FileStream tempFileStream = null;
            string tempFileName = "";
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            DataSet ds = null;
            if (reportType == 1)
            {
                tempFileName = "试剂信息列表";
                ds = IBGoods.GetGoodsInfoByID(idList, "", basePath + "\\BaseTableXML\\Report_Goods.xml");
            }
        
            try
            {
                string tempFilePath = basePath + "\\TempExcelFile\\" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "." + CommonRS.GetExcelExtName();
              
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    string headerText = "";
                    if (isHeader == 1)
                        headerText = tempFileName;
                    if (!ExcelHelper.CreateExcelByNPOI(ds.Tables[0], headerText, tempFilePath))
                        tempFilePath = "";
                }
                else
                    tempFilePath = "";
                if (!string.IsNullOrEmpty(tempFilePath) && File.Exists(tempFilePath))
                {
                    tempFileName += "." + CommonRS.GetExcelExtName();
                    tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + tempFileName);
                        //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                        //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                    }
                    else if (operateType == 1)//直接打开PDF文件
                    {
                        //WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/vnd.ms-excel";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + tempFileName);
                        //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                tempFileStream = null;
                throw new Exception(ex.Message);
            }
            return tempFileStream;
        }

        public Stream RS_UDTO_DownLoadExcel(string fileName, string downFileName, int isUpLoadFile, int operateType)
        {
            FileStream tempFileStream = null;
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                string extName = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(downFileName))
                    downFileName = "试剂信息错误文件" + extName;
                else
                    downFileName = downFileName + extName;
                string tempFilePath = basePath + "\\UploadBaseTableInfo\\" + fileName;
                if (isUpLoadFile == 1)
                    tempFilePath = basePath + "\\TempExcelFile\\" + fileName;

                if (!string.IsNullOrEmpty(tempFilePath) && File.Exists(tempFilePath))
                {
                    tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + downFileName);
                        //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                        //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                    }
                    else if (operateType == 1)//直接打开PDF文件
                    {
                        //WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/vnd.ms-excel";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + downFileName);
                        //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                tempFileStream = null;
                throw new Exception(ex.Message);
            }
            return tempFileStream;
        }


        #endregion

        private BaseResultDataValue ReadUserCenOrgInfo(ref CenOrg cenOrg, ref string orgNo, ref string orgName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取当前用户的ID信息,请重新登录！";
                return baseResultDataValue;
            }
            HREmployee hrEmployee = IBHREmployee.Get(Int64.Parse(employeeID));
            IList<CenOrg> listCenOrg = null;
            if (hrEmployee.HRDept != null && (!string.IsNullOrEmpty(hrEmployee.HRDept.UseCode)))
            {
                listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=\'" + hrEmployee.HRDept.UseCode + "\'");
                if (listCenOrg != null && listCenOrg.Count > 0)
                {
                    if (listCenOrg.Count == 1)
                    {
                        cenOrg = listCenOrg[0];
                        orgNo = cenOrg.ShortCode;
                        orgName = cenOrg.CName;
                        if (string.IsNullOrEmpty(orgNo))
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "本机构的代码信息没有维护，请联系管理员！";
                            return baseResultDataValue;
                        }
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "找不到当前登陆者的机构信息！";
                    return baseResultDataValue;
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "当前登陆者的机构编码信息不存在！";
                return baseResultDataValue;
            }
            return baseResultDataValue;
        }

        //public BaseResultDataValue RS_UDTO_InputSaleDocInterface(string saleDocNo, long compOrgId, long labOrgId)
        //{
        //    BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
        //    BaseResultDataValue brdvURL = RS_UDTO_IsUseSaleDocInterface();
        //    string url = "";
        //    if (brdvURL.success)
        //        url = brdvURL.ResultDataValue;
        //    if (!string.IsNullOrEmpty(url) || !string.IsNullOrEmpty(saleDocNo))
        //    {
        //        IList<BmsCenSaleDoc> listBmsCenSaleDoc = IBBmsCenSaleDoc.SearchListByHQL(" bmscensaledoc.SaleDocNo=\'" + saleDocNo + "\'");
        //        if (listBmsCenSaleDoc != null && listBmsCenSaleDoc.Count > 0)
        //        {
        //            baseResultDataValue.success = false;
        //            baseResultDataValue.ErrorInfo = "该供货单号的供货单已经存在！供货单号：" + saleDocNo;
        //            return baseResultDataValue;
        //        }
        //        string orgNo = "";
        //        string orgName = "";
        //        CenOrg cenOrg = null;
        //        BaseResultDataValue brdv = ReadUserCenOrgInfo(ref cenOrg, ref orgNo, ref orgName);
        //        if (!brdv.success) 
        //            return brdv;

        //        //string para = "&string=10030&string1=四川省迈克实业有限公司&string2=XC161133915";
        //        string para = "&string=" + orgNo + "&string1=" + cenOrg.CName + "&string2=" + saleDocNo;
        //        ZhiFang.Common.Log.Log.Info("接口参数：" + para);
        //        string contentType = "text/plain";
        //        try
        //        {
        //            string jsonData = ZhiFang.Common.Public.WebHttp.WebRequestHttpPost(url, para, contentType);
        //            ZhiFang.Common.Log.Log.Info("接口返回数据：" + jsonData);
        //            if (!string.IsNullOrEmpty(jsonData))
        //            {
        //                baseResultDataValue = IBBmsCenSaleDoc.AddBmsCenSaleDocDataByMaiKe(jsonData, HttpContext.Current.Server.MapPath("~/"), cenOrg);
        //            }
        //            else
        //            {
        //                baseResultDataValue.success = false;
        //                baseResultDataValue.ErrorInfo = "获取不到供货单信息！";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            baseResultDataValue.success = false;
        //            baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        baseResultDataValue.success = false;
        //        baseResultDataValue.ErrorInfo = "错误信息：接口地址或供货单号为空！";
        //    }
        //    return baseResultDataValue;       
        //}

        private BaseResultDataValue Maike_InputSaleDocInterface(string saleDocNo, string url)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(url) || !string.IsNullOrEmpty(saleDocNo))
            {
                string orgNo = "";
                string orgName = "";
                CenOrg cenOrg = null;
                BaseResultDataValue brdv = ReadUserCenOrgInfo(ref cenOrg, ref orgNo, ref orgName);
                if (!brdv.success)
                    return brdv;
                IList<BmsCenSaleDoc> listBmsCenSaleDoc = IBBmsCenSaleDoc.SearchListByHQL(" bmscensaledoc.SaleDocNo=\'" + saleDocNo + "\'" +
                                                    " and bmscensaledoc.Comp.Id=" + cenOrg.Id.ToString());
                if (listBmsCenSaleDoc != null && listBmsCenSaleDoc.Count > 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "单号为【" + saleDocNo + "】的供货单已经存在！";
                    return baseResultDataValue;
                }
                //string para = "&string=10030&string1=四川省迈克实业有限公司&string2=XC161133915";
                string para = "&string=" + orgNo + "&string1=" + cenOrg.CName + "&string2=" + saleDocNo;
                ZhiFang.Common.Log.Log.Info("接口参数：" + para);
                string contentType = "text/plain";
                try
                {
                    string jsonData = ZhiFang.Common.Public.WebHttp.WebRequestHttpPost(url, para, contentType);
                    ZhiFang.Common.Log.Log.Info("接口返回数据：" + jsonData);
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        baseResultDataValue = IBBmsCenSaleDoc.AddBmsCenSaleDocDataByMaiKe(jsonData, HttpContext.Current.Server.MapPath("~/"), cenOrg);
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "获取不到供货单信息！";
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：接口地址或供货单号为空！";
            }
            return baseResultDataValue;
        }

        private BaseResultDataValue Baron_InputSaleDocInterface(string saleDocNo, string url, CenOrg compOrg, CenOrg labOrg)
        {
            //747026a6-ac50-49cb-b75e-ad4b7acfc198
            //9e15210b0f6524f2中关村
            //||SA01|00090262
            ZhiFang.Common.Log.Log.Info("Baron SaleDocNo：" + saleDocNo);
            IList<string> listNo = saleDocNo.Split('|').ToList();
            if (listNo != null && listNo.Count > 0)
            {
                saleDocNo = listNo[listNo.Count - 1];
                ZhiFang.Common.Log.Log.Info("Dispose SaleDocNo：" + saleDocNo);
            }

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<BmsCenSaleDoc> listBmsCenSaleDoc = IBBmsCenSaleDoc.SearchListByHQL(" bmscensaledoc.SaleDocNo=\'" + saleDocNo + "\'" +
                                                    " and bmscensaledoc.Comp.Id=" + compOrg.Id.ToString());
                                                    //" and bmscensaledoc.Lab.Id=" + labOrg.Id.ToString());
            if (listBmsCenSaleDoc != null && listBmsCenSaleDoc.Count > 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "单号为【" + saleDocNo + "】的供货单已经存在！";
                return baseResultDataValue;
            }
            string responseContent = "";
            string appkey = "";
            string password = "";
            BaseResultDataValue brdv = GetInterfaceAppKey((int)InterfaceType.供货单接口, compOrg.OrgNo.ToString(), labOrg.OrgNo.ToString(), ref appkey, ref password);
            if (brdv.success)
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string strData = ZhiFang.Common.Public.Base64Help.EncodingString("{\"接口类型\": \"下载二维码\",\"接收系统标识\": \"JKWMS\",\"接口数据\": {\"出库单号\": \"" + saleDocNo + "\"}}", Encoding.UTF8);
                string sign = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(timestamp + strData + password);
                try
                {
                    ZhiFang.Common.Log.Log.Info("开始调用巴瑞供货单接口！");
                    ZhiFang.Common.Log.Log.Info("供应商名称：" + compOrg.CName + "---" + compOrg.OrgNo);
                    ZhiFang.Common.Log.Log.Info("实验室名称：" + labOrg.CName + "---" + labOrg.OrgNo);
                    NameValueCollection stringDict = new NameValueCollection();
                    stringDict.Add("appkey", appkey);
                    stringDict.Add("timestamp", timestamp);
                    stringDict.Add("data", strData);
                    stringDict.Add("sign", sign);
                    stringDict.Add("ver", "1");
                    string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                    byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                    byte[] endboundarybytes = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

                    var memStream = new MemoryStream();
                    var webRequest = (HttpWebRequest)WebRequest.Create(url);

                    // 设置属性  
                    webRequest.Method = "POST";
                    webRequest.Timeout = 10000;
                    webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                    // 写入字符串的Key  
                    string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                    foreach (string key in stringDict.Keys)
                    {
                        memStream.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, key, stringDict[key]);
                        byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                        memStream.Write(formitembytes, 0, formitembytes.Length);
                    }
                    memStream.Write(endboundarybytes, 0, endboundarybytes.Length);//结束

                    webRequest.ContentLength = memStream.Length;

                    var requestStream = webRequest.GetRequestStream();

                    memStream.Position = 0;
                    var tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();

                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    requestStream.Close();

                    var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

                    using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                    {
                        responseContent = httpStreamReader.ReadToEnd();
                    }

                    httpWebResponse.Close();
                    webRequest.Abort();
                    ZhiFang.Common.Log.Log.Info("巴瑞供货单接口返回值："+responseContent);
                    ZhiFang.Common.Log.Log.Info("结束调用巴瑞供货单接口！");
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "巴瑞供货单接口错误：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                    return baseResultDataValue;
                }
                string resultString = ZhiFang.Common.Public.Base64Help.DecodingString(responseContent, Encoding.UTF8);
                ZhiFang.Common.Log.Log.Info("巴瑞供货单接口返回值解密：" + resultString);
                baseResultDataValue = IBBmsCenSaleDoc.AddBmsCenSaleDocDataByBaron(saleDocNo, resultString, compOrg, labOrg);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "巴瑞供货单接口错误：获取AppKey配置失败！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_InputSaleDocInterface(string saleDocNo, long compOrgId, long labOrgId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            CenOrg cenOrg = null;
            baseResultDataValue = GetCurUserCenOrg(ref cenOrg);
            if (!baseResultDataValue.success)
                return baseResultDataValue; 
            if (cenOrg != null)
            {
                string orgIndex = "";
                if (cenOrg.CenOrgType.ShortCode == "2")//供应商
                {
                    BaseResultDataValue brdv = GetInterfaceConfig((int)InterfaceType.供货单接口, cenOrg.OrgNo.ToString(), ref orgIndex);
                    if (brdv.success)
                    {
                        if (orgIndex == ((int)InterfaceIndex.四川迈克).ToString())
                        {
                            baseResultDataValue = Maike_InputSaleDocInterface(saleDocNo, brdv.ResultDataValue);
                        }
                    }
                }
                else if (cenOrg.CenOrgType.ShortCode == "3")//实验室
                {
                    CenOrg compOrg = IBCenOrg.Get(compOrgId);
                    if (compOrg != null)
                    {
                        BaseResultDataValue brdv = GetInterfaceConfig((int)InterfaceType.供货单接口, compOrg.OrgNo.ToString(), ref orgIndex);
                        if (brdv.success)
                        {
                            if (orgIndex == ((int)InterfaceIndex.北京巴瑞).ToString())
                            {
                                baseResultDataValue = Baron_InputSaleDocInterface(saleDocNo, brdv.ResultDataValue, compOrg, cenOrg);
                            }
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "无法获取当前实验室的供应商信息";
                        ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                        return baseResultDataValue;
                    }
                }           
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取当前用户的机构信息";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_IsUseSaleDocInterface()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取当前用户的ID信息";
                return baseResultDataValue;
            }
            HREmployee hrEMP = IBHREmployee.Get(long.Parse(employeeID));
            if (hrEMP == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取当前用户信息";
                return baseResultDataValue;            
            
            }
            string orgNo = hrEMP.HRDept.UseCode;
            if (string.IsNullOrEmpty(orgNo))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "当前用户所属机构的编码为空！请联系管理员维护！";
                return baseResultDataValue;
            }
            baseResultDataValue = GetInterfaceConfig((int)InterfaceType.供货单接口, orgNo);
            return baseResultDataValue;
        }

        //获取实验室配置的供货单接口机构列表
        public BaseResultDataValue RS_UDTO_GetLabInterfaceOrgList()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenOrg> entityList = new EntityList<CenOrg>();
            entityList.list = new List<CenOrg>();
            try
            {
                CenOrg cenOrg = null;
                baseResultDataValue = GetCurUserCenOrg(ref cenOrg);
                if (!baseResultDataValue.success)
                    return baseResultDataValue; 
                if (cenOrg != null)
                {
                    if (cenOrg.CenOrgType.ShortCode == "2")//供应商
                    {
                        BaseResultDataValue brdv = GetInterfaceConfig((int)InterfaceType.供货单接口, cenOrg.OrgNo.ToString());
                        if (brdv.success)
                        {
                            entityList.list.Add(cenOrg);
                            entityList.count = 1;
                        }

                    }
                    else if (cenOrg.CenOrgType.ShortCode == "3")//实验室
                    {
                        string wherehql = " cenorgcondition.cenorg2.Id=" + cenOrg.Id.ToString();
                        IList<CenOrgCondition> listCenOrgCondition = IBCenOrgCondition.SearchListByHQL(wherehql);
                        if (listCenOrgCondition != null && listCenOrgCondition.Count > 0)
                        {
                            foreach (CenOrgCondition entityOrg in listCenOrgCondition)
                            {
                                BaseResultDataValue brdv = GetInterfaceConfig((int)InterfaceType.供货单接口, entityOrg.cenorg1.OrgNo.ToString());
                                if (brdv.success)
                                {
                                    entityList.list.Add(entityOrg.cenorg1);
                                }
                            }
                            entityList.count = entityList.list.Count;
                        }
                    }
                    string fields = "CenOrg_Id,CenOrg_CName,CenOrg_OrgNo";
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenOrg>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;        
        
        }

        private BaseResultDataValue GetInterfaceConfig(int typeIndex, string orgNo)
        {
            string orgIndex = "";
            BaseResultDataValue baseResultDataValue = GetInterfaceConfig(typeIndex, orgNo, ref orgIndex);
            return baseResultDataValue;
        }

        private BaseResultDataValue GetInterfaceConfig(int typeIndex, string orgNo, ref string orgIndex)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string fieldXmlPath = "";
            if (HttpContext.Current != null && HttpContext.Current.Server != null)
                fieldXmlPath = HttpContext.Current.Server.MapPath("~/");
            else
                fieldXmlPath = ZhiFang.Common.Public.GetFilePath.GetBinDir();
            if (typeIndex == 1)//供货单接口配置
                fieldXmlPath = fieldXmlPath + "BaseTableXML\\SaleDocInterface\\OrgURL.xml";
            else if (typeIndex == 2)//订货单接口配置
                fieldXmlPath = fieldXmlPath + "BaseTableXML\\OrderDocInterface\\OrgURL.xml";
            if (File.Exists(fieldXmlPath))
            {
                baseResultDataValue.success = false;
                DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string strOrgIndex = dataRow["OrgIndex"].ToString();
                    string strOrgNo = dataRow["OrgNo"].ToString();
                    IList<string> listOrgNo = strOrgNo.Split(',').ToList();

                    if (listOrgNo.IndexOf(orgNo) >= 0)
                    {
                        baseResultDataValue.success = true;
                        baseResultDataValue.ResultDataValue = dataRow["OrgURL"].ToString();
                        orgIndex = strOrgIndex;
                        break;
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取OrgURL.xml配置文件不存在！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        private BaseResultDataValue GetInterfaceAppKey(int typeIndex, string compOrgNo, string labOrgNo, ref string appkey, ref string password)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string fieldXmlPath = "";
            if (HttpContext.Current != null && HttpContext.Current.Server != null)
                fieldXmlPath = HttpContext.Current.Server.MapPath("~/");
            else
                fieldXmlPath = ZhiFang.Common.Public.GetFilePath.GetBinDir();
            if (typeIndex == 1)//供货单接口配置
                fieldXmlPath = fieldXmlPath + "BaseTableXML\\SaleDocInterface\\OrgAppKey.xml";
            else if (typeIndex == 2)//订货单接口配置
                fieldXmlPath = fieldXmlPath + "BaseTableXML\\OrderDocInterface\\OrgAppKey.xml";
            if (File.Exists(fieldXmlPath))
            {
                baseResultDataValue.success = false;
                DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(fieldXmlPath);
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string strOrgIndex = dataRow["OrgIndex"].ToString();
                    string strCompOrgNo = dataRow["CompOrgNo"].ToString();
                    string strLabOrgNo = dataRow["LabOrgNo"].ToString();
                    if (strCompOrgNo == compOrgNo && strLabOrgNo == labOrgNo)
                    {
                        baseResultDataValue.success = true;
                        appkey = dataRow["AppKey"].ToString();
                        password = dataRow["AppPassword"].ToString();
                        break;
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取OrgAppKey.xml配置文件不存在！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        private BaseResultDataValue GetCurUserCenOrg(ref CenOrg cenOrg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取当前用户的ID信息";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            HREmployee hrEMP = IBHREmployee.Get(long.Parse(employeeID));
            if (hrEMP == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取当前用户信息";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;

            }
            string orgNo = hrEMP.HRDept.UseCode;
            if (string.IsNullOrEmpty(orgNo))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "当前用户所属机构的编码为空！请联系管理员维护！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            else
            {
                IList<CenOrg> listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=\'" + orgNo + "\'");
                if (listCenOrg != null && listCenOrg.Count > 0)
                    cenOrg = listCenOrg[0];
            }
            return baseResultDataValue;
        }

        public string WebRequestHttpPost()
        {
            string resultString = "";
            string url = "https://esb.maccura.com:9088/api/Domain/univereven/mksaleck/saleck?appkey=86087a37977f42e8b236ff2009debd4e&string=10030&string1=四川省迈克实业有限公司&string2=XC161133915";
            string para = "";
            var data = Encoding.ASCII.GetBytes(para);
            var request = (HttpWebRequest)WebRequest.Create(url);
        
                
                request.Method = "POST";

                request.ContentType = "text/plain";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                resultString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JObject jsonObject = JObject.Parse(resultString);
            return resultString;
        }

        public BaseResultDataValue RS_UDTO_AutoCheckOrderDoc(long orderDocId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string timeSec = ConfigHelper.GetConfigString("AutoCheckOrderDocTime").Trim();
                if (string.IsNullOrEmpty(timeSec))
                    timeSec = "600";
                TimerCheckOrderDoc myTimer = new TimerCheckOrderDoc();
                myTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
                myTimer.Interval = int.Parse(timeSec) * 1000;
                myTimer.OrderDocID = orderDocId;
                myTimer.Enabled = true;
                ZhiFang.Common.Log.Log.Info("------- 开始启动订单自动审核ID:" + myTimer.OrderDocID + " ----------------");
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            ((TimerCheckOrderDoc)source).Enabled = false; 
            ZhiFang.Common.Log.Log.Info("------- 开始执行订单自动审核 ----------------");
            long id = ((TimerCheckOrderDoc)source).OrderDocID; 
            ZhiFang.Common.Log.Log.Info("------- 自动审核订单ID:" + id.ToString() + " ----------------");
            if (id > 0)
            {
                BaseResultDataValue baseResultDataValue = IBBmsCenOrderDoc.EditAutoCheckBmsCenOrderDocByID(id);
                if (baseResultDataValue.success)
                    IBBmsCenOrderDoc.BmsCenOrderDocCheckedAndPush((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, id);
            }
            ZhiFang.Common.Log.Log.Info("------- 结束执行订单自动审核 ----------------");
        }

        public string WebRequestHttpPostWMS(string saleDocNo)
        {
            //747026a6-ac50-49cb-b75e-ad4b7acfc198
            //9e15210b0f6524f2中关村
            string responseContent = "";
            string url = "http://36.110.53.20:801/barcodewebapi/api/barcode/downloaddata";
            string appkey = "747026a6-ac50-49cb-b75e-ad4b7acfc198";
            string password = "9e15210b0f6524f2";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string strData = "{\"接口类型\": \"下载二维码\",\"接收系统标识\": \"JKWMS\",\"接口数据\": {\"出库单号\": \"" + saleDocNo + "\"}}";
            //string sign = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(timestamp + strData + password);


            //string strData = StringToBase64(JsonSerialize(new { 接口类型 = "下载二维码", 接收系统标识 = "JKWMS", 接口数据 = new { 出库单号 = "test" } }));
            string strData = ZhiFang.Common.Public.Base64Help.EncodingString("{\"接口类型\": \"下载二维码\",\"接收系统标识\": \"JKWMS\",\"接口数据\": {\"出库单号\": \"" + saleDocNo + "\"}}", Encoding.UTF8);

            //string sign = GetMd5(timestamp + strData + password, "UTF-8");
            string sign = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(timestamp + strData + password);
            try
            {
                NameValueCollection stringDict = new NameValueCollection();
                stringDict.Add("appkey", appkey);
                stringDict.Add("timestamp", timestamp);
                stringDict.Add("data", strData);
                stringDict.Add("sign", sign);
                stringDict.Add("ver", "1");
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                byte[] endboundarybytes = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

                var memStream = new MemoryStream();
                var webRequest = (HttpWebRequest)WebRequest.Create(url);

                // 设置属性  
                webRequest.Method = "POST";
                webRequest.Timeout = 10000;
                webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                // 写入字符串的Key  
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                foreach (string key in stringDict.Keys)
                {
                    memStream.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, stringDict[key]);
                    byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }
                memStream.Write(endboundarybytes, 0, endboundarybytes.Length);//结束

                webRequest.ContentLength = memStream.Length;

                var requestStream = webRequest.GetRequestStream();

                memStream.Position = 0;
                var tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                requestStream.Close();

                var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

                using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                {
                    responseContent = httpStreamReader.ReadToEnd();
                }

                httpWebResponse.Close();
                webRequest.Abort();
            }
            catch (Exception exp)
            {
                responseContent = exp.ToString();
            }
            string resultString = ZhiFang.Common.Public.Base64Help.DecodingString(responseContent, Encoding.UTF8);
            return "";
        }

    }   
}
