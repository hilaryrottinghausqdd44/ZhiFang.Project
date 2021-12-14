using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using System.Linq;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.Common;
using ZhiFang.ReagentSys.Client.ServerContract;

using Newtonsoft.Json.Linq;
using ZhiFang.ServiceCommon.RBAC;
using Newtonsoft.Json;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using ZhiFang.IBLL.RBAC;

namespace ZhiFang.ReagentSys.Client
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ZFReaRestfulService : IZFReaRestfulService
    {
        IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBReaBmsCenSaleDoc IBReaBmsCenSaleDoc { get; set; }
        IBReaBmsCenSaleDtl IBReaBmsCenSaleDtl { get; set; }
        IBReaBmsCenOrderDocOfService IBReaBmsCenOrderDocOfService { get; set; }
        IBCenOrg IBCenOrg { get; set; }

        IBReaBmsOutDoc IBReaBmsOutDoc { get; set; }
        IBHRDept IBHRDept { get; set; }

        #region 客户端与平台在同一数据库
        public BaseResultBool RS_UDTO_UpdateReaBmsCenOrderDocOfUploadByIdStr(string idStr, bool isVerifyProdGoodsNo)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(idStr))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "idStr为空！";
                return baseResultBool;
            }
            try
            {
                idStr = idStr.TrimEnd(',');
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultBool = IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocOfUploadOfIdStr(idStr, isVerifyProdGoodsNo, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsCenOrderDocOfCancelUpload(ReaBmsCenOrderDoc entity, bool isVerifyProdGoodsNo, string fields)
        {
            if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
            {
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
                if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
            }
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                //long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                tempBaseResultBool = IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocOfCancelUpload(entity, tempArray, isVerifyProdGoodsNo, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateReaBmsCenOrderDocOfCancelUpload:" + ex.Source);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsCenOrderDocOfComp(ReaBmsCenOrderDoc entity, string fields)
        {
            if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
            {
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
                if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
            }
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
            {
                tempBaseResultBool.ErrorInfo = "未能获取身份信息，请重新登录！";
                tempBaseResultBool.success = false;
                return tempBaseResultBool;
            }
            try
            {
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                tempBaseResultBool = IBReaBmsCenOrderDocOfService.EditReaBmsCenOrderDocOfComp(entity, tempArray, labID, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateReaBmsCenOrderDocOfComp:" + ex.Source);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue RS_UDTO_AddReaBmsCenSaleDocOfOrderToSupply(long orderId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultDataValue = IBReaBmsCenSaleDoc.AddReaBmsCenSaleDocOfOrderToSupply(orderId, labID, empID, empName);
                if (baseResultDataValue.success)
                {
                    IBReaBmsCenSaleDoc.Get(IBReaBmsCenSaleDoc.Entity.Id);
                    baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCenSaleDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("RS_UDTO_AddReaBmsCenSaleDocOfOrderToSupply:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsCenSaleDocOfExtract(long saleDocId, string reaServerCompCode, string saleDocNo, string reaServerLabcCode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultBool = IBReaBmsCenSaleDoc.EditReaBmsCenSaleDocOfExtract(saleDocId, reaServerCompCode, saleDocNo, reaServerLabcCode, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            return tempBaseResultBool;
        }

        #endregion

        #region 客户端与平台不在同一数据库--客户端部分
        public BaseResultDataValue RS_Client_EditUploadPlatformReaOrderDocAndDtl(long orderId, string appkey, string timestamp, string token, string version, string platformUrl, bool isVerifyProdGoodsNo, long empID, string empName)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Debug("订单ID为:" + orderId + ",上传平台内部处理开始!");
            try
            {
                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ZFReaRestfulService.svc/RS_Client_AddReaOrderDocAndDtlOfUploadPlatform";
                ReaBmsCenOrderDoc orderDoc = null;
                JObject jPostData = new JObject();
                baseresultdata = IBReaBmsCenOrderDoc.GetUploadPlatformReaOrderDocAndDtl(orderId, ref jPostData, ref orderDoc);
                if (orderDoc == null)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取订单信息为空!";
                    return baseresultdata;
                }
                string orderDocNo = orderDoc.OrderDocNo;
                //订货方所属机构平台编码
                string labNo = orderDoc.ReaServerLabcCode;
                //供货方所属机构平台编码
                string compNo = orderDoc.ReaServerCompCode;
                if (string.IsNullOrEmpty(orderDocNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订单号为空!";
                    return baseresultdata;
                }
                if (string.IsNullOrEmpty(labNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订货方所属机构平台编码为空!";
                    return baseresultdata;
                }
                if (string.IsNullOrEmpty(compNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "供货方所属机构平台编码为空!";
                    return baseresultdata;
                }
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "平台入口地址参数为空(platformUrl)!";
                    return baseresultdata;
                }
                if (baseresultdata.success == false) return baseresultdata;

                jPostData.Add("Appkey", "");
                jPostData.Add("Timestamp", "");
                jPostData.Add("Token", "");
                jPostData.Add("Version", "");
                jPostData.Add("EmpID", empID);
                jPostData.Add("EmpName", empName);
                jPostData.Add("IsVerifyProdGoodsNo", isVerifyProdGoodsNo);
                jPostData.Add("OrderDocId", orderDoc.Id);
                jPostData.Add("OrderDocNo", orderDoc.OrderDocNo);
                // 字符串提交方式              
                string postDataStr = jPostData.ToString().Replace(Environment.NewLine, "");
                postDataStr = "{\"postData\":\"" + postDataStr.Replace("\"", "\\\"") + "\"}";

                //实体提交方式测试
                //string postDataStr = jPostData.ToString().Replace(Environment.NewLine, "");
                //postDataStr = postDataStr.Replace("\"", "\\\"");

                ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + "提交数据信息为:" + postDataStr);
                ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + ",上传平台内部处理结束!");

                ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + ",上传平台调用服务开始!");
                ZhiFang.Common.Log.Log.Debug(string.Format("订单号为:{0},订货方所属机构平台编码为:{1},供货方所属机构平台编码为:{2}", orderDoc.OrderDocNo, orderDoc.ReaServerLabcCode, orderDoc.ReaServerCompCode));
                string resultStr = WebRequestHelp.Post(postDataStr, "JSON", url, ZFPlatformHelp.TIME_OUT_MILLISECOND);
                ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + ",上传平台调用服务返回结果信息为:" + resultStr);
                if (!string.IsNullOrEmpty(resultStr))
                {
                    JObject jresult = JObject.Parse(resultStr);
                    if (jresult["success"] != null)
                    {
                        baseresultdata.success = bool.Parse(jresult["success"].ToString());
                        baseresultdata.ErrorInfo = jresult["ErrorInfo"].ToString();
                    }
                    else
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "订单号为:" + orderDoc.OrderDocNo + ",上传平台的返回结果信息异常!";
                        return baseresultdata;
                    }
                    //更新上传标志等
                    if (baseresultdata.success == true)
                    {
                        ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + ",上传平台后更新操作开始!");
                        orderDoc.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.已上传.Key);//标志更新为已上传
                        orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.订单上传.Key);//订单上传
                        orderDoc.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[orderDoc.Status.ToString()].Name;
                        bool result = true;
                        IBReaBmsCenOrderDoc.Entity = orderDoc;
                        result = IBReaBmsCenOrderDoc.Edit();
                        if (result == false)
                        {
                            baseresultdata.DataCode = "Error001";
                            baseresultdata.ErrorInfo = "错误信息:订单号为:" + orderDoc.OrderDocNo + ",更新上传标志失败!";
                            ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                        }
                        ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + ",上传平台后更新操作结束!");
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订单号为:" + orderDoc.OrderDocNo + ",上传平台的返回结果信息为空!";
                    return baseresultdata;
                }
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_Client_AddReaOrderDocAndDtlOfUpload:" + ex.Message + ex.StackTrace);
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }

            return baseresultdata;
        }

        public BaseResultDataValue RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL(string platformUrl, string labcCode, string compCode, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(platformUrl))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的调用平台供货商的URL(platformUrl)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(labcCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的订货方所属机构平台编码(labcCode)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(compCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的供货方所属机构平台编码(compCode)参数为空!";
                return baseresultdata;
            }
            EntityList<ReaBmsCenSaleDoc> entityList = new EntityList<ReaBmsCenSaleDoc>();
            try
            {
                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ZFReaRestfulService.svc/RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocToClientByHQL";
                ZhiFang.Common.Log.Log.Debug("(客户端部分)智方客户端获取智方平台供货商的供货总单信息开始!");

                if (!string.IsNullOrEmpty(sort))
                {
                    string sort1 = JsonHelper.UrlEncode(sort);
                    ZhiFang.Common.Log.Log.Debug("RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL.sort.UrlEncode:" + sort1);
                    sort = sort1;
                }
                if (!string.IsNullOrEmpty(where))
                    where = JsonHelper.UrlEncode(where);
                if (!string.IsNullOrEmpty(sort))
                    sort = JsonHelper.UrlEncode(sort);
                string paramStr = string.Format("?labcCode={0}&compCode={1}&page={2}&limit={3}&fields={4}&where={5}&sort={6}&isPlanish={7}", labcCode, compCode, page, limit, fields, where, sort, isPlanish);
                // paramStr = JsonHelper.UrlEncode(paramStr);
                ZhiFang.Common.Log.Log.Debug("RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL.paramStr.UrlEncode:" + paramStr);
                url = url + paramStr;
                ZhiFang.Common.Log.Log.Debug(string.Format("url:{0}", url));
                string resultStr = WebRequestHelp.Get(url, ZFPlatformHelp.TIME_OUT_MILLISECOND);
                ZhiFang.Common.Log.Log.Debug(string.Format("resultStr:{0}", resultStr));
                ZhiFang.Common.Log.Log.Debug("(客户端部分)智方客户端获取智方平台供货商的供货总单信息结束!");
                if (!string.IsNullOrEmpty(resultStr))
                {
                    JObject jresult = JObject.Parse(resultStr);
                    if (jresult["success"] != null)
                    {
                        baseresultdata.success = bool.Parse(jresult["success"].ToString());
                        baseresultdata.ErrorInfo = jresult["ErrorInfo"].ToString();
                        baseresultdata.ResultDataValue = jresult["ResultDataValue"].ToString();
                    }
                    else
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "提取平台供货商供货信息返回结果信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "提取平台供货商供货信息返回结果为空!";
                    return baseresultdata;
                }

            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            return baseresultdata;
        }

        public BaseResultDataValue RS_UDTO_AddSaleDocAndDtlOfPlatformExtract(string platformUrl, string labcCode, string compCode, long saleDocId, string saleDocNo)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(saleDocNo))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的供货单号(saleDocNo)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(platformUrl))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的调用平台供货商的URL(platformUrl)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(labcCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的订货方所属机构平台编码(labcCode)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(compCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的供货方所属机构平台编码(compCode)参数为空!";
                return baseresultdata;
            }

            try
            {
                //判断供货信息是否存在,是否已经提取过等
                ZhiFang.Common.Log.Log.Debug(string.Format("labcCode:{0},compCode:{1}", labcCode, compCode));
                string where = string.Format("reabmscensaledoc.Id={0} and reabmscensaledoc.SaleDocNo='{1}'", saleDocId, saleDocNo);
                IList<ReaBmsCenSaleDoc> docList = IBReaBmsCenSaleDoc.SearchListByHQL(where);
                if (docList != null && docList.Count > 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = string.Format("供货单号为:{0},供货单Id为:{1},已经已经被提取!", saleDocNo, saleDocId);
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }

                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ZFReaRestfulService.svc/RS_UDTO_SearchPlatformSaleDocAndDtlToClient";
                ZhiFang.Common.Log.Log.Debug("智方平台供货商向智方客户端提供获取供货商供货信息(总单+明细)开始!");
                string paramStr = string.Format("?labcCode={0}&compCode={1}&saleDocId={2}&saleDocNo={3}", labcCode, compCode, saleDocId, saleDocNo);
                //paramStr = JsonHelper.UrlEncode(paramStr);
                ZhiFang.Common.Log.Log.Debug("RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL.paramStr.UrlEncode:" + paramStr);

                url = url + paramStr;
                ZhiFang.Common.Log.Log.Debug(string.Format("客户端提取平台供货商供货信息的URL:{0}", url));
                string resultStr = WebRequestHelp.Get(url, ZFPlatformHelp.TIME_OUT_MILLISECOND);
                ZhiFang.Common.Log.Log.Debug(string.Format("客户端提取平台供货商供货信息的返回结果:{0}", resultStr));
                ZhiFang.Common.Log.Log.Debug("智方平台供货商向智方客户端提供获取供货商供货信息(总单+明细)结束!");
                if (!string.IsNullOrEmpty(resultStr))
                {
                    JObject jresult = JObject.Parse(resultStr);
                    if (jresult["success"] != null)
                    {
                        bool success = bool.Parse(jresult["success"].ToString());
                        string errorInfo = jresult["ErrorInfo"].ToString();
                        string resultDataValue = jresult["ResultDataValue"].ToString();
                        if (!success)
                        {
                            baseresultdata.success = false;
                            baseresultdata.ErrorInfo = errorInfo;
                            return baseresultdata;
                        }
                        if (string.IsNullOrEmpty(resultDataValue))
                        {
                            baseresultdata.success = false;
                            baseresultdata.ErrorInfo = "提取供货信息返回结果为空!";
                            return baseresultdata;
                        }
                        JObject resultData = JObject.Parse(resultDataValue);
                        long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                        string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                        baseresultdata = IBReaBmsCenSaleDoc.AddSaleDocAndDtlOfPlatformExtract(labcCode, compCode, saleDocId, saleDocNo, empID, empName, resultData);

                        #region 提取平台供货数据成功后,更新平台供货单的提取标志及状态
                        if (baseresultdata.success == true)
                        {
                            if (platformUrl.LastIndexOf("/") < -1)
                                platformUrl = platformUrl + "/";
                            string updateUrl = platformUrl + "ZFReaRestfulService.svc/RS_UDTO_UpdatePlatformSaleDocAndDtlToClient";
                            ZhiFang.Common.Log.Log.Debug("客户端提取平台供货商供货数据成功后,更新平台供货单的提取标志及状态开始!");
                            updateUrl = updateUrl + string.Format("?labcCode={0}&compCode={1}&saleDocId={2}&saleDocNo={3}", labcCode, compCode, saleDocId, saleDocNo);
                            string resultStr2 = WebRequestHelp.Get(updateUrl, ZFPlatformHelp.TIME_OUT_MILLISECOND);
                            if (!string.IsNullOrEmpty(resultStr2))
                            {
                                JObject jresult2 = JObject.Parse(resultStr2);
                                bool success2 = bool.Parse(jresult2["success"].ToString());
                                string errorInfo2 = jresult2["ErrorInfo"].ToString();
                                string resultDataValue2 = jresult2["ResultDataValue"].ToString();
                                if (!success2)
                                {
                                    //baseresultdata.success = false;
                                    baseresultdata.ErrorInfo = errorInfo2;
                                    ZhiFang.Common.Log.Log.Error("客户端提取平台供货商供货数据成功后,更新平台供货单的提取标志及状态处理失败:" + errorInfo2);
                                    return baseresultdata;
                                }
                                ZhiFang.Common.Log.Log.Debug("客户端提取平台供货商供货数据成功后,更新平台供货单的提取标志及状态结束!");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "提取平台供货商的供货返回信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "提取平台供货商的供货返回信息为空!";
                    return baseresultdata;
                }
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            return baseresultdata;
        }

        /// <summary>
        /// 客户端出库单上传至试剂平台
        /// </summary>
        public BaseResultDataValue RS_Client_UploadOutDocAndDtlToPlatform(long outDocId, string appkey, string timestamp, string token, string version, string platformUrl, long empID, string empName, long deptID)
        {
            ZhiFang.Common.Log.Log.Info("进入到接口上传出库单，方法名称=RS_Client_UploadOutDocAndDtlToPlatform，参数outDocId=" + outDocId + "，appkey=" + appkey + "，timestamp=" + timestamp + "，token=" + token + "，version=" + version + "，platformUrl=" + platformUrl + "，empID=" + empID + "，empName=" + empName+ "，deptID="+ deptID);
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Debug("出库单ID为:" + outDocId + ",上传平台内部处理开始!");
            try
            {
                if (platformUrl.Substring(platformUrl.Length - 1) != "/")
                {
                    platformUrl = platformUrl + "/";
                }
                string url = platformUrl + "ZFReaRestfulService.svc/RS_Platform_ReceiveOutDocAndDtlFormClient";

                HRDept hRDept = GetRootHRDept(deptID);
                if (hRDept == null || string.IsNullOrWhiteSpace(hRDept.UseCode))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "出库单使用部门的平台编码为空，请维护好重试!";
                    return baseresultdata;
                }

                ReaBmsOutDoc outDoc = null;
                JObject jPostData = new JObject();
                baseresultdata = IBReaBmsOutDoc.GetUploadPlatformOutDocAndDtl(outDocId, hRDept.UseCode, ref jPostData, ref outDoc);
                if (outDoc == null)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取出库信息为空!";
                    return baseresultdata;
                }

                string outDocNo = outDoc.OutDocNo;
                //实验室所属机构平台编码
                string labNo = outDoc.ReaServerLabcCode;
                if (string.IsNullOrEmpty(labNo))
                {
                    labNo = hRDept.UseCode;
                    outDoc.ReaServerLabcCode= hRDept.UseCode;
                }
                if (string.IsNullOrEmpty(outDocNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "出库单号为空!";
                    return baseresultdata;
                }
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "平台入口地址参数为空(platformUrl)!";
                    return baseresultdata;
                }
                if (baseresultdata.success == false) return baseresultdata;

                jPostData.Add("Appkey", "");
                jPostData.Add("Timestamp", "");
                jPostData.Add("Token", "");
                jPostData.Add("Version", "");
                jPostData.Add("EmpID", empID);
                jPostData.Add("EmpName", empName);
                jPostData.Add("OutDocId", outDoc.Id);
                jPostData.Add("OutDocNo", outDoc.OutDocNo);
                // 字符串提交方式              
                string postDataStr = jPostData.ToString().Replace(Environment.NewLine, "");
                postDataStr = "{\"postData\":\"" + postDataStr.Replace("\"", "\\\"") + "\"}";

                //实体提交方式测试
                //string postDataStr = jPostData.ToString().Replace(Environment.NewLine, "");
                //postDataStr = postDataStr.Replace("\"", "\\\"");
                
                ZhiFang.Common.Log.Log.Debug("出库单号为:" + outDoc.OutDocNo + "提交数据信息为:" + postDataStr);
                ZhiFang.Common.Log.Log.Debug("出库单号为:" + outDoc.OutDocNo + ",上传平台内部处理结束!");

                ZhiFang.Common.Log.Log.Debug(string.Format("出库单号为:{0},客户端平台编码为:{1},上传平台调用服务开始!", outDoc.OutDocNo, outDoc.ReaServerLabcCode));
                string resultStr = WebRequestHelp.Post(postDataStr, "JSON", url, ZFPlatformHelp.TIME_OUT_MILLISECOND);
                ZhiFang.Common.Log.Log.Debug("出库单号为:" + outDoc.OutDocNo + ",上传平台调用服务结束！平台返回结果信息为:" + resultStr);
                if (!string.IsNullOrEmpty(resultStr))
                {
                    JObject jresult = JObject.Parse(resultStr);
                    if (jresult["success"] != null)
                    {
                        baseresultdata.success = bool.Parse(jresult["success"].ToString());
                        baseresultdata.ErrorInfo = jresult["ErrorInfo"].ToString();
                    }
                    else
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "出库单号为:" + outDoc.OutDocNo + ",上传平台的返回结果信息异常!";
                        return baseresultdata;
                    }
                    //更新上传标志等
                    if (baseresultdata.success == true)
                    {
                        ZhiFang.Common.Log.Log.Debug("出库单号为:" + outDoc.OutDocNo + ",上传平台成功，更新操作开始!");
                        
                        outDoc.IOFlag = int.Parse(ReaBmsOutDocIOFlag.已上传.Key);//标志更新为已上传
                        outDoc.Status = int.Parse(ReaBmsOutDocStatus.出库单上传平台.Key);//状态
                        outDoc.StatusName = ReaBmsOutDocStatus.GetStatusDic()[outDoc.Status.ToString()].Name;
                        outDoc.DataUpdateTime = DateTime.Now;

                        string fields = "Id,IOFlag,Status,StatusName,DataUpdateTime";
                        string[] tempUpdateArray = CommonServiceMethod.GetUpdateFieldValueStr(outDoc, fields);

                        bool result = true;
                        result = IBReaBmsOutDoc.Update(tempUpdateArray);
                        if (result == false)
                        {
                            baseresultdata.DataCode = "Error001";
                            baseresultdata.ErrorInfo = "错误信息:出库单号为:" + outDoc.OutDocNo + ",更新上传标志失败!";
                            ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                        }
                        ZhiFang.Common.Log.Log.Debug("出库单号为:" + outDoc.OutDocNo + ",更新操作结束!");
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "出库单号为:" + outDoc.OutDocNo + ",上传平台的返回结果信息为空!";
                    return baseresultdata;
                }
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_Client_UploadOutDocAndDtlToPlatform:" + ex.Message + ex.StackTrace);
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Info("接口上传出库单结束");
            return baseresultdata;
        }

        /// <summary>
        /// 根据当前登录人员的部门，获取其根级部门
        /// </summary>
        /// <returns></returns>
        private HRDept GetRootHRDept(long deptID)
        {
            List<long> allParent = IBHRDept.GetParentDeptIdListByDeptId(deptID);
            deptID = allParent[allParent.Count - 1];
            HRDept hRDept = IBHRDept.Get(deptID);
            return hRDept;
        }

        /// <summary>
        /// 客户端订单：取消上传
        /// </summary>
        public BaseResultDataValue RS_Client_EditCancelUploadPlatformReaOrderDocAndDtl(long orderId, string appkey, string timestamp, string token, string version, string platformUrl, bool isVerifyProdGoodsNo, long empID, string empName)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Info("进入到接口订单取消上传开始，方法名称=RS_Client_EditCancelUploadPlatformReaOrderDocAndDtl，参数orderId=" + orderId + "，appkey=" + appkey + "，timestamp=" + timestamp + "，token=" + token + "，version=" + version + "，platformUrl=" + platformUrl + "，empID=" + empID + "，empName=" + empName + "，isVerifyProdGoodsNo=" + isVerifyProdGoodsNo);
            try
            {
                ZhiFang.Common.Log.Log.Debug("订单ID为:" + orderId + ",上传平台内部处理开始!");

                if (platformUrl.Substring(platformUrl.Length - 1) != "/")
                {
                    platformUrl = platformUrl + "/";
                }
                string url = platformUrl + "ZFReaRestfulService.svc/RS_Platform_ReceiveCancelUploadOrder";

                ReaBmsCenOrderDoc orderDoc = IBReaBmsCenOrderDoc.Get(orderId);                
                if (orderDoc == null)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "试剂客户端获取订单信息为空!";
                    return baseresultdata;
                }
                string orderDocNo = orderDoc.OrderDocNo;
                if (string.IsNullOrEmpty(orderDocNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订单号为空!";
                    return baseresultdata;
                }
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "平台入口地址参数为空(platformUrl)!";
                    return baseresultdata;
                }

                JObject jPostData = new JObject();
                jPostData.Add("Appkey", "");
                jPostData.Add("Timestamp", "");
                jPostData.Add("Token", "");
                jPostData.Add("Version", "");
                jPostData.Add("EmpID", empID);
                jPostData.Add("EmpName", empName);
                jPostData.Add("OrderDocId", orderDoc.Id);
                jPostData.Add("OrderDocNo", orderDoc.OrderDocNo);
                // 字符串提交方式              
                string postDataStr = jPostData.ToString().Replace(Environment.NewLine, "");
                postDataStr = "{\"postData\":\"" + postDataStr.Replace("\"", "\\\"") + "\"}";

                //实体提交方式测试
                //string postDataStr = jPostData.ToString().Replace(Environment.NewLine, "");
                //postDataStr = postDataStr.Replace("\"", "\\\"");

                ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + "提交数据信息为:" + postDataStr);
                ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + ",上传平台内部处理结束!");

                ZhiFang.Common.Log.Log.Debug(string.Format("订单号为:{0},客户端平台编码为:{1},上传平台调用服务开始!", orderDoc.OrderDocNo, orderDoc.ReaServerLabcCode));
                string resultStr = WebRequestHelp.Post(postDataStr, "JSON", url, ZFPlatformHelp.TIME_OUT_MILLISECOND);
                ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + ",上传平台调用服务结束！平台返回结果信息为:" + resultStr);
                if (!string.IsNullOrEmpty(resultStr))
                {
                    JObject jresult = JObject.Parse(resultStr);
                    if (jresult["success"] != null)
                    {
                        baseresultdata.success = bool.Parse(jresult["success"].ToString());
                        baseresultdata.ErrorInfo = jresult["ErrorInfo"].ToString();
                    }
                    else
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "订单号为:" + orderDoc.OrderDocNo + ",上传平台的返回结果信息异常!";
                        return baseresultdata;
                    }
                    //修改客户端订单状态=取消上传
                    if (baseresultdata.success)
                    {
                        ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + ",上传平台成功，更新操作开始!");

                        orderDoc.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.取消上传.Key);
                        orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.取消上传.Key);
                        orderDoc.StatusName = ReaBmsOutDocStatus.GetStatusDic()[orderDoc.Status.ToString()].Name;
                        orderDoc.DataUpdateTime = DateTime.Now;

                        string fields = "Id,IOFlag,Status,StatusName,DataUpdateTime";
                        string[] tempUpdateArray = CommonServiceMethod.GetUpdateFieldValueStr(orderDoc, fields);

                        bool result = true;
                        result = IBReaBmsCenOrderDoc.Update(tempUpdateArray);
                        if (result == false)
                        {
                            baseresultdata.DataCode = "Error001";
                            baseresultdata.ErrorInfo = "错误信息:订单号为:" + orderDoc.OrderDocNo + ",更新订单状态及标志失败!";
                            ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                        }
                        ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDoc.OrderDocNo + ",更新成功，更新操作结束!");
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订单号为:" + orderDoc.OrderDocNo + ",上传平台的返回结果信息为空!";
                    return baseresultdata;
                }

            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_Client_EditCancelUploadPlatformReaOrderDocAndDtl:", ex);
            }
            ZhiFang.Common.Log.Log.Info("订单取消上传结束");
            return baseresultdata;
        }

        #endregion

        #region 客户端与平台不在同一数据库--平台部分
        public BaseResultDataValue RS_Client_AddReaOrderDocAndDtlOfUploadPlatform(String postData)
        {
            string orderDocNo = "";
            ZhiFang.Common.Log.Log.Debug("(平台部分)订单上传平台保存处理开始.postData:" + postData);
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(postData))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入参数(postData)订单信息为空!";
                return baseresultdata;
            }

            try
            {
                ReaBmsCenOrderDoc orderDoc = null;
                IList<ReaBmsCenOrderDtl> orderDtlList = new List<ReaBmsCenOrderDtl>();
                JObject jresult = JObject.Parse(postData);
                ZhiFang.Common.Log.Log.Debug("解析封装订单信息完成");
                if (jresult["OrderDocNo"] == null || string.IsNullOrEmpty(jresult["OrderDocNo"].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(OrderDocNo)订单信息为空!";
                    return baseresultdata;
                }
                if (jresult["OrderDocId"] == null || string.IsNullOrEmpty(jresult["OrderDocId"].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(OrderDocId)订单信息为空!";
                    return baseresultdata;
                }
                if (jresult[ZFPlatformHelp.订货总单.Key] == null || string.IsNullOrEmpty(jresult[ZFPlatformHelp.订货总单.Key].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(orderDoc)订单信息为空!";
                    return baseresultdata;
                }
                if (jresult[ZFPlatformHelp.订货明细单.Key] == null || string.IsNullOrEmpty(jresult[ZFPlatformHelp.订货明细单.Key].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(orderDtlList)订单明细为空!";
                    return baseresultdata;
                }
                orderDocNo = jresult["OrderDocNo"].ToString();
                long orderDocId = long.Parse(jresult["OrderDocId"].ToString());

                ZhiFang.Common.Log.Log.Debug("上传订货单的订单号为:" + orderDocNo);
                long empID = long.Parse(jresult["EmpID"].ToString());
                string empName = jresult["EmpName"].ToString();
                ZhiFang.Common.Log.Log.Debug("empID:" + empID);
                ZhiFang.Common.Log.Log.Debug("empName:" + empName);
                bool isVerifyProdGoodsNo = false;
                if (jresult["IsVerifyProdGoodsNo"] == null || string.IsNullOrEmpty(jresult["IsVerifyProdGoodsNo"].ToString()))
                    isVerifyProdGoodsNo = bool.Parse(jresult["IsVerifyProdGoodsNo"].ToString());

                //判断订单信息是否存在平台上
                string where = string.Format("reabmscenorderdoc.Id={0} and reabmscenorderdoc.OrderDocNo='{1}'", orderDocId, orderDocNo);
                IList<ReaBmsCenOrderDoc> orderDocList = IBReaBmsCenOrderDoc.SearchListByHQL(where);
                if (orderDocList != null && orderDocList.Count > 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = string.Format("订货单号为:{0},订货单Id为:{1},已经已经上传!", orderDocNo, orderDocId);
                    ZhiFang.Common.Log.Log.Debug(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }

                ZhiFang.Common.Log.Log.Debug("解析封装的订货主单信息开始");
                orderDoc = JsonHelper.JsonToObject<ReaBmsCenOrderDoc>(jresult, ZFPlatformHelp.订货总单.Key);

                ZhiFang.Common.Log.Log.Debug(string.Format("TotalPrice:{0},DataUpdateTime:{1},UrgentFlag:{2},IOFlag:{3},Id:{4}", orderDoc.TotalPrice, orderDoc.DataUpdateTime, orderDoc.UrgentFlag, orderDoc.IOFlag, orderDoc.Id));
                ZhiFang.Common.Log.Log.Debug("解析封装的订货主单信息完成");

                ZhiFang.Common.Log.Log.Debug("解析封装的订货明细信息开始");
                //var jorderDtlList = jresult.SelectToken("orderDtlList").ToList();
                //orderDtlList = jresult["orderDtlList"].ToObject<IList<ReaBmsCenOrderDtl>>();//Newtonsoft.Json.JsonSerializer.Create(setting)
                orderDtlList = JsonHelper.JsonToObjectList<ReaBmsCenOrderDtl>(jresult, ZFPlatformHelp.订货明细单.Key);
                if (orderDtlList != null)
                {
                    ZhiFang.Common.Log.Log.Debug("解析封装的订货明细信息.Count:" + orderDtlList.Count);
                    foreach (var item in orderDtlList)
                    {
                        ZhiFang.Common.Log.Log.Debug(string.Format("ReaGoodsName:{0},ReqGoodsQty:{1},GoodsQty:{2},OrderDocID:{3}", item.ReaGoodsName, item.ReqGoodsQty, item.GoodsQty, item.OrderDocID));
                    }
                }
                ZhiFang.Common.Log.Log.Debug("解析封装的订货明细信息完成");
                if (orderDoc == null)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(orderDoc)订单信息为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                if (orderDtlList == null || orderDtlList.Count < 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(orderDtlList)订单明细为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }

                //订货方所属机构平台编码
                string labNo = orderDoc.ReaServerLabcCode;
                //供货方所属机构平台编码
                string compNo = orderDoc.ReaServerCompCode;
                if (string.IsNullOrEmpty(orderDocNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订单号为空!";
                    return baseresultdata;
                }
                if (string.IsNullOrEmpty(labNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订单的所属订货方机构平台编码为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                if (string.IsNullOrEmpty(compNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订单的所属供货方机构平台编码为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDocNo + ",所属订货方机构平台编码为:" + labNo + ",所属供货方机构平台编码为:" + compNo);
                baseresultdata = IBReaBmsCenOrderDoc.AddReaOrderDocAndDtlOfUpload(orderDoc, isVerifyProdGoodsNo, orderDtlList, empID, empName);
                ZhiFang.Common.Log.Log.Debug("订单号为:" + orderDocNo + ",上传处理结束,上传处理结果为:" + baseresultdata.success);
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("订单上传处理错误信息:订单号为:" + orderDocNo + ":" + ex.Message + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            ZhiFang.Common.Log.Log.Debug("(平台部分)订单上传平台保存处理结束,处理结果:" + baseresultdata.success);
            return baseresultdata;
        }
        public BaseResultDataValue RS_Client_AddReaOrderDocAndDtlOfUploadPlatform2(ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> orderDtlList)
        {
            ZhiFang.Common.Log.Log.Debug("RS_Client_AddReaOrderDocAndDtlOfUploadPlatform2.postData:");
            BaseResultDataValue baseresultdata = new BaseResultDataValue();

            try
            {
                if (orderDoc != null)
                {
                    ZhiFang.Common.Log.Log.Debug("orderDoc.:" + orderDoc.OrderDocNo);
                }
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            return baseresultdata;
        }
        public BaseResultDataValue RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocToClientByHQL(string labcCode, string compCode, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(labcCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的订货方所属机构平台编码(labcCode)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(compCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的供货方所属机构平台编码(compCode)参数为空!";
                return baseresultdata;
            }
            ZhiFang.Common.Log.Log.Debug("sort:" + sort);
            if (!string.IsNullOrEmpty(sort))
            {
                string sort2 = JsonHelper.UrlDecode(sort);
                ZhiFang.Common.Log.Log.Debug("sort.UrlDecode:" + sort2);
                sort = sort2;
            }
            ZhiFang.Common.Log.Log.Debug("(平台部分)智方客户端获取智方平台供货商的供货总单信息开始!");
            ZhiFang.Common.Log.Log.Debug(string.Format("labcCode:{0},compCode:{1}", labcCode, compCode));
            string paramStr = string.Format("fields={0}&where={1}&sort={2}", fields, where, sort);
            ZhiFang.Common.Log.Log.Debug("查询条件信息:" + paramStr);

            EntityList<ReaBmsCenSaleDoc> entityList = new EntityList<ReaBmsCenSaleDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                    ZhiFang.Common.Log.Log.Debug("sort.GetSortHQL:" + sort);
                    entityList = IBReaBmsCenSaleDoc.SearchListByHQL(where, sort, page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenSaleDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseresultdata.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenSaleDoc>(entityList);
                    }
                    else
                    {
                        baseresultdata.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("(平台部分)智方客户端获取智方平台供货商的供货总单信息结束!");
            return baseresultdata;
        }
        public BaseResultDataValue RS_UDTO_SearchPlatformSaleDocAndDtlToClient(string labcCode, string compCode, long saleDocId, string saleDocNo)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Debug("(平台部分)智方平台供货商向智方客户端提供获取供货商供货信息(总单+明细)开始!");
            ZhiFang.Common.Log.Log.Debug(string.Format("labcCode:{0},compCode:{1}", labcCode, compCode));
            string paramStr = string.Format("labcCode={0}&compCode={1}&saleDocId={2}&saleDocNo={3}", labcCode, compCode, saleDocId, saleDocNo);
            ZhiFang.Common.Log.Log.Debug("传入参数信息:" + paramStr);
            if (string.IsNullOrEmpty(saleDocNo))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的供货单号(saleDocNo)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(labcCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的订货方所属机构平台编码(labcCode)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(compCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的供货方所属机构平台编码(compCode)参数为空!";
                return baseresultdata;
            }
            //判断供货信息是否存在,是否已经提取过等

            JObject jPostData = new JObject();
            baseresultdata = IBReaBmsCenSaleDoc.GetPlatformSaleDocAndDtlToClient(labcCode, compCode, saleDocId, saleDocNo, ref jPostData);

            try
            {
                jPostData.Add("saleDocId", saleDocId);
                jPostData.Add("saleDocNo", saleDocNo);
                // 字符串提交方式              
                string postDataStr = jPostData.ToString().Replace(Environment.NewLine, "");
                ZhiFang.Common.Log.Log.Debug("供货单号为:" + saleDocNo + ",供货信息为:" + postDataStr);
                baseresultdata.success = true;
                baseresultdata.ResultDataValue = postDataStr;
                //postDataStr = "{\"postData\":\"" + postDataStr.Replace("\"", "\\\"") + "\"}";
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("(平台部分)智方平台供货商向智方客户端提供获取供货商供货信息(总单+明细)结束!");
            return baseresultdata;
        }
        public BaseResultDataValue RS_UDTO_UpdatePlatformSaleDocAndDtlToClient(string labcCode, string compCode, long saleDocId, string saleDocNo)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Debug("(平台部分)客户端提取平台供货数据成功后,更新平台供货单的提取标志及状态(总单+明细)开始!");
            ZhiFang.Common.Log.Log.Debug(string.Format("labcCode:{0},compCode:{1}", labcCode, compCode));
            string paramStr = string.Format("?labcCode={0}&compCode={1}&saleDocId={2}&saleDocNo={3}", labcCode, compCode, saleDocId, saleDocNo);
            ZhiFang.Common.Log.Log.Debug("传入参数信息:" + paramStr);
            if (string.IsNullOrEmpty(saleDocNo))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的供货单号(saleDocNo)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(labcCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的订货方所属机构平台编码(labcCode)参数为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(compCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入的供货方所属机构平台编码(compCode)参数为空!";
                return baseresultdata;
            }
            //判断供货信息是否存在,是否已经提取过等

            JObject jPostData = new JObject();
            baseresultdata = IBReaBmsCenSaleDoc.UpdatePlatformSaleDocAndDtlToClient(labcCode, compCode, saleDocId, saleDocNo);

            try
            {
                // 字符串提交方式              
                string postDataStr = jPostData.ToString().Replace(Environment.NewLine, "");
                ZhiFang.Common.Log.Log.Debug("供货单号为:" + saleDocNo + ",供货信息为:" + postDataStr);
                baseresultdata.success = true;
                baseresultdata.ResultDataValue = postDataStr;
                //postDataStr = "{\"postData\":\"" + postDataStr.Replace("\"", "\\\"") + "\"}";
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("(平台部分)客户端提取平台供货数据成功后,更新平台供货单的提取标志及状态(总单+明细)结束!");
            return baseresultdata;
        }

        /// <summary>
        /// 智方试剂平台端，接收出库单
        /// </summary>
        public BaseResultDataValue RS_Platform_ReceiveOutDocAndDtlFormClient(String postData)
        {            
            ZhiFang.Common.Log.Log.Debug("(平台端接口，方法名称=RS_Platform_ReceiveOutDocAndDtlFormClient)平台接收出库单保存开始，参数postData:" + postData);
            string outDocNo = "";
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(postData))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入参数(postData)出库单信息为空!";
                return baseresultdata;
            }

            try
            {
                ReaBmsOutDoc outDoc = null;
                IList<ReaBmsOutDtl> outDtlList = new List<ReaBmsOutDtl>();
                JObject jresult = JObject.Parse(postData);
                ZhiFang.Common.Log.Log.Debug("解析封装出库单信息完成");
                if (jresult["OutDocNo"] == null || string.IsNullOrEmpty(jresult["OutDocNo"].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(OutDocNo)为空!";
                    return baseresultdata;
                }
                if (jresult["OutDocId"] == null || string.IsNullOrEmpty(jresult["OutDocId"].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(OutDocId)为空!";
                    return baseresultdata;
                }
                if (jresult[ZFPlatformHelp.出库总单.Key] == null || string.IsNullOrEmpty(jresult[ZFPlatformHelp.出库总单.Key].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(outDoc)出库单信息为空!";
                    return baseresultdata;
                }
                if (jresult[ZFPlatformHelp.出库明细单.Key] == null || string.IsNullOrEmpty(jresult[ZFPlatformHelp.出库明细单.Key].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(outDtlList)出库单明细为空!";
                    return baseresultdata;
                }
                outDocNo = jresult["OutDocNo"].ToString();
                long outDocId = long.Parse(jresult["OutDocId"].ToString());
                ZhiFang.Common.Log.Log.Debug("接收到出库单号为:" + outDocNo);

                long empID = long.Parse(jresult["EmpID"].ToString());
                string empName = jresult["EmpName"].ToString();
                ZhiFang.Common.Log.Log.Debug("empID:" + empID);
                ZhiFang.Common.Log.Log.Debug("empName:" + empName);

                //bool isVerifyProdGoodsNo = false;
                //if (jresult["IsVerifyProdGoodsNo"] == null || string.IsNullOrEmpty(jresult["IsVerifyProdGoodsNo"].ToString()))
                //    isVerifyProdGoodsNo = bool.Parse(jresult["IsVerifyProdGoodsNo"].ToString());

                //判断出库单信息是否存在平台上
                string where = string.Format("reabmsoutdoc.Id={0} and reabmsoutdoc.OutDocNo='{1}'", outDocId, outDocNo);
                IList<ReaBmsOutDoc> outDocList = IBReaBmsOutDoc.SearchListByHQL(where);
                if (outDocList != null && outDocList.Count > 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = string.Format("出库单号为:{0},出库单Id为:{1},已经已经上传!", outDocNo, outDocId);
                    ZhiFang.Common.Log.Log.Debug(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }

                ZhiFang.Common.Log.Log.Debug("解析封装的出库主单信息开始");
                outDoc = JsonHelper.JsonToObject<ReaBmsOutDoc>(jresult, ZFPlatformHelp.出库总单.Key);
                ZhiFang.Common.Log.Log.Debug("解析封装的出库主单信息完成");

                ZhiFang.Common.Log.Log.Debug("解析封装的出库单明细信息开始");
                //var jorderDtlList = jresult.SelectToken("orderDtlList").ToList();
                //orderDtlList = jresult["orderDtlList"].ToObject<IList<ReaBmsCenOrderDtl>>();//Newtonsoft.Json.JsonSerializer.Create(setting)
                outDtlList = JsonHelper.JsonToObjectList<ReaBmsOutDtl>(jresult, ZFPlatformHelp.出库明细单.Key);
                if (outDtlList != null)
                {
                    ZhiFang.Common.Log.Log.Debug("解析封装的出库单明细信息.Count:" + outDtlList.Count);
                    foreach (var item in outDtlList)
                    {
                        ZhiFang.Common.Log.Log.Debug(string.Format("GoodsCName:{0},ReqGoodsQty:{1},GoodsQty:{2},OutDocID:{3}", item.GoodsCName, item.ReqGoodsQty, item.GoodsQty, item.OutDocID));
                    }
                }
                ZhiFang.Common.Log.Log.Debug("解析封装的出库单明细信息完成");

                if (outDoc == null)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(outDoc)出库单信息为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                if (outDtlList == null || outDtlList.Count <= 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(outDtlList)出库单明细为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }

                //客户端平台编码
                string labNo = outDoc.ReaServerLabcCode;                
                if (string.IsNullOrEmpty(labNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "客户端平台编码为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }

                ZhiFang.Common.Log.Log.Debug("出库单号为:" + outDocNo + ",客户端平台编码为:" + labNo + ",数据写入开始");
                baseresultdata = IBReaBmsOutDoc.SaveClientOutDocAndDtl(outDoc, outDtlList, empID, empName);
                ZhiFang.Common.Log.Log.Debug("出库单号为:" + outDocNo + ",数据写入结束,结果为:" + baseresultdata.success);
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("平台RS_Platform_ReceiveOutDocAndDtlFormClient接收出库单信息异常:订单号为:" + outDocNo + ":" + ex.Message + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            ZhiFang.Common.Log.Log.Debug("(平台端接口，方法名称=RS_Platform_ReceiveOutDocAndDtlFormClient)平台接收出库单保存结束，返回结果:" + baseresultdata.success);
            return baseresultdata;
        }

        /// <summary>
        /// 智方试剂平台端，接收订单取消
        /// 判断平台库里的该订单状态=取消确认，直接删除；其他状态不可删除。
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        public BaseResultDataValue RS_Platform_ReceiveCancelUploadOrder(String postData)
        {
            ZhiFang.Common.Log.Log.Debug("(平台端接口，方法名称=RS_Platform_ReceiveCancelUploadOrder)平台接收取消订单开始，参数postData:" + postData);
            string orderDocNo = "";
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(postData))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入参数(postData)出库单信息为空!";
                return baseresultdata;
            }

            try
            {
                JObject jresult = JObject.Parse(postData);
                ZhiFang.Common.Log.Log.Debug("解析封装出库单信息完成");
                if (jresult["OrderDocNo"] == null || string.IsNullOrEmpty(jresult["OrderDocNo"].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(OrderDocNo)为空!";
                    return baseresultdata;
                }
                if (jresult["OrderDocId"] == null || string.IsNullOrEmpty(jresult["OrderDocId"].ToString()))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入参数(OrderDocId)为空!";
                    return baseresultdata;
                }
                orderDocNo = jresult["OrderDocNo"].ToString();
                long orderDocId = long.Parse(jresult["OrderDocId"].ToString());
                ZhiFang.Common.Log.Log.Debug("接收到订单号为:" + orderDocNo);

                ReaBmsCenOrderDoc reaBmsCenOrderDoc = IBReaBmsCenOrderDoc.Get(orderDocId);
                if (reaBmsCenOrderDoc == null)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "试剂平台获取订单信息为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }

                if (reaBmsCenOrderDoc.Status == int.Parse(ReaBmsOrderDocStatus.取消确认.Key))
                {
                    //删除平台库里的订单信息
                    IBReaBmsCenOrderDtl.DeleteByHql("from ReaBmsCenOrderDtl where OrderDocID=" + orderDocId);
                    IBReaBmsCenOrderDoc.DeleteByHql("from ReaBmsCenOrderDoc where Id=" + orderDocId);
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "试剂平台里的订单状态为[" + reaBmsCenOrderDoc.StatusName + "]不能取消上传!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("平台RS_Platform_ReceiveCancelUploadOrder接收信息异常:订单号为:" + orderDocNo, ex);
            }
            ZhiFang.Common.Log.Log.Debug("(平台端接口，方法名称=RS_Platform_ReceiveCancelUploadOrder)平台接收取消订单结束，返回结果:" + baseresultdata.success);
            return baseresultdata;
        }
        #endregion

    }
}
