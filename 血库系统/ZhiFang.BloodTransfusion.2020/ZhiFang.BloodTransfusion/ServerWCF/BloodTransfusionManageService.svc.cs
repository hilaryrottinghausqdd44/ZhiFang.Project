using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json.Linq;
using ZhiFang.BloodTransfusion.Common;
using ZhiFang.BloodTransfusion.hisinterface;
using ZhiFang.BloodTransfusion.ServerContract;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IBLL.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BloodTransfusion.ServerWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“BloodTransfusionManageService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 BloodTransfusionManageService.svc 或 BloodTransfusionManageService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BloodTransfusionManageService : IBloodTransfusionManageService
    {
        #region IBLL
        IBBloodUseDesc IBBloodUseDesc { get; set; }
        IBBUserUIConfig IBBUserUIConfig { get; set; }
        IBDepartment IBDepartment { get; set; }
        IBPUser IBPUser { get; set; }
        IBBloodBReqForm IBBloodBReqForm { get; set; }
        IBBloodBReqItem IBBloodBReqItem { get; set; }
        IBBloodBTestItem IBBloodBTestItem { get; set; }
        IBBloodClass IBBloodClass { get; set; }
        IBBloodLargeUseItem IBBloodLargeUseItem { get; set; }
        IBBloodStyle IBBloodStyle { get; set; }
        IBBloodUnit IBBloodUnit { get; set; }
        IBLL.RBAC.IBBloodDocGrade IBBloodDocGrade { get; set; }
        IBBloodBReqFormResult IBBloodBReqFormResult { get; set; }
        IBLL.BloodTransfusion.IBBloodABO IBBloodABO { get; set; }
        IBLL.BloodTransfusion.IBBloodBOutForm IBBloodBOutForm { get; set; }
        IBLL.BloodTransfusion.IBBloodBOutItem IBBloodBOutItem { get; set; }
        IBLL.BloodTransfusion.IBBloodBPreForm IBBloodBPreForm { get; set; }
        IBLL.BloodTransfusion.IBBloodBPreItem IBBloodBPreItem { get; set; }
        IBLL.BloodTransfusion.IBBloodTransForm IBBloodTransForm { get; set; }
        IBLL.BloodTransfusion.IBBloodTransItem IBBloodTransItem { get; set; }
        IBLL.BloodTransfusion.IBBloodBagProcess IBBloodBagProcess { get; set; }
        IBLL.BloodTransfusion.IBBloodBagProcessType IBBloodBagProcessType { get; set; }
        IBLL.BloodTransfusion.IBBloodBInForm IBBloodBInForm { get; set; }
        IBLL.BloodTransfusion.IBBloodBInItem IBBloodBInItem { get; set; }
        IBLL.BloodTransfusion.IBBloodRecei IBBloodRecei { get; set; }
        IBLL.BloodTransfusion.IBBloodReceiItem IBBloodReceiItem { get; set; }
        IBDepartmentUser IBDepartmentUser { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IBBTemplate IBBTemplate { get; set; }
        IBBloodBagProcessTypeQry IBBloodBagProcessTypeQry { get; set; }

        IBBloodAggluItem IBBloodAggluItem { get; set; }
        IBBloodBagHandoverOper IBBloodBagHandoverOper { get; set; }
        IBBloodBagRecordDtl IBBloodBagRecordDtl { get; set; }
        IBBloodBagRecordItem IBBloodBagRecordItem { get; set; }
        IBBloodBagRecordType IBBloodBagRecordType { get; set; }
        IBBloodBUnit IBBloodBUnit { get; set; }
        IBBloodChargeGroupItem IBBloodChargeGroupItem { get; set; }
        IBBloodChargeItem IBBloodChargeItem { get; set; }
        IBBloodChargeItemLink IBBloodChargeItemLink { get; set; }
        IBBloodChargeItemType IBBloodChargeItemType { get; set; }
        IBBloodChargeMoney IBBloodChargeMoney { get; set; }
        IBBloodInChecked IBBloodInChecked { get; set; }
        IBBloodInCheckedCurrent IBBloodInCheckedCurrent { get; set; }
        IBBloodInCheckedItem IBBloodInCheckedItem { get; set; }
        IBBloodInterfaceTransport IBBloodInterfaceTransport { get; set; }

        IBBloodPatinfo IBBloodPatinfo { get; set; }
        IBBloodQtyDtl IBBloodQtyDtl { get; set; }
        IBBloodSelfBlood IBBloodSelfBlood { get; set; }
        IBBloodSetQtyAlertColor IBBloodSetQtyAlertColor { get; set; }
        IBBloodSetQtyAlertInfo IBBloodSetQtyAlertInfo { get; set; }
        IBSCBagOperation IBSCBagOperation { get; set; }
        IBBloodClassUnitLink IBBloodClassUnitLink { get; set; }
        IBBloodBagChargeLink IBBloodBagChargeLink { get; set; }
        IBBloodLisResult IBBloodLisResult { get; set; }
        IBBloodInterfaceMaping IBBloodInterfaceMaping { get; set; }

        #endregion

        #region 6.6登录处理
        /// <summary>
        /// 用户登陆服务
        /// </summary>
        /// <param name="strUserAccount"></param>
        /// <param name="strPassWord"></param>
        /// <returns></returns>
        public BaseResultDataValue BT_SYS_LoginOfPUser(string strUserAccount, string strPassWord)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //ZhiFang.Common.Log.Log.Debug("BT_SYS_LoginOfPUser.strUserAccount:" + strUserAccount + ",strPassWord:" + strPassWord);
            //ZhiFang.Common.Log.Log.Debug(strPassWord + ",sliNdez2解密后为:" + IBPUser.UnCovertPassWord("sliNdez2"));
            if (string.IsNullOrEmpty(strUserAccount) && string.IsNullOrEmpty(strPassWord))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：帐号及密码不能为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                tempBaseResultDataValue = IBPUser.GetUserLogin(strUserAccount, strPassWord);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_SYS_LoginOfPUser.Error.account:" + strUserAccount);
                ZhiFang.Common.Log.Log.Error("BT_SYS_LoginOfPUser.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_SYS_LoginOfPUser.Error.StackTrace:" + ex.StackTrace);
            }

            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_SYS_LoginOfPUserByHisCode(string hisWardCode, string hisDeptCode, string hisDoctorCode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Debug("BT_SYS_LoginOfPUserByHisCode:" + "hisWardCode:" + hisWardCode + ",hisDeptCode:" + hisDeptCode + ",hisDoctorCode:" + hisDoctorCode);
            // ZhiFang.Common.Log.Log.Debug(strPassWord + ",=uqC解密后为:" + IBPUser.UnCovertPassWord("=uqC"));
            if (string.IsNullOrEmpty(hisDoctorCode))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：hisDoctorCode不能为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                bool isAutoAddDepartmentUser = false;
                string paraValue = BParameterCache.GetParaValue(SYSParaNo.HIS调用时按传入信息自动建立科室人员关系.Key);
                if (!string.IsNullOrEmpty(paraValue) && (paraValue == "1" || paraValue == "true"))
                {
                    isAutoAddDepartmentUser = true;
                }
                tempBaseResultDataValue = IBPUser.GetUserLoginByHisCode(hisWardCode, hisDeptCode, hisDoctorCode, isAutoAddDepartmentUser);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_SYS_LoginOfPUserByHisCode.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_SYS_LoginOfPUserByHisCode.Error.StackTrace:" + ex.StackTrace);
            }

            return tempBaseResultDataValue;
        }
        public bool BT_SYS_LogoutOfPUser(string strUserAccount)
        {
            try
            {
                SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, null);//实验室ID
                SessionHelper.SetSessionValue(DicCookieSession.UserAccount, null);//员工账户名
                SessionHelper.SetSessionValue(DicCookieSession.UseCode, null);//员工代码
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, null); //员工ID
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, null);//员工姓名
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, null);//员工代码
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, null);//部门ID
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, null);//部门名称
                SessionHelper.SetSessionValue(DicCookieSession.OldModuleID, null);
                SessionHelper.SetSessionValue(DicCookieSession.CurModuleOperID, null);

                Common.Cookie.CookieHelper.Remove(SysPublicSet.SysDicCookieSession.LabID);//实验室ID
                Common.Cookie.CookieHelper.Remove(DicCookieSession.UserID);//账户ID
                Common.Cookie.CookieHelper.Remove(DicCookieSession.UserAccount);//账户名
                Common.Cookie.CookieHelper.Remove(DicCookieSession.UseCode);//账户代码
                Common.Cookie.CookieHelper.Remove(DicCookieSession.EmployeeID);// 员工ID
                Common.Cookie.CookieHelper.Remove(DicCookieSession.EmployeeName);// 员工姓名
                Common.Cookie.CookieHelper.Remove(DicCookieSession.EmployeeUseCode);// 员工代码
                return IBPUser.RBAC_BA_Logout(strUserAccount);
            }
            catch
            {
                return false;
            }
        }
        public bool BT_SYS_DBUpdate()
        {
            bool tempBool = true;
            ZhiFang.DBUpdate.PM.DBUpdate.DataBaseUpdate("");
            return tempBool;
        }
        #endregion

        #region 公共封装处理

        public BaseResultDataValue BT_UDTO_SearchRBACUserOfPUserByHQL(bool isliip, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACUser> tempEntityList = new EntityList<RBACUser>();
            try
            {
                if (isliip == true)
                {
                    tempBaseResultDataValue = RS_UDTO_SearchRBACUserOFLIMPByHQL("", page, limit, fields, where, sort, isPlanish);
                }
                else
                {
                    if ((sort != null) && (sort.Length > 0))
                    {
                        sort = CommonServiceMethod.GetSortHQL(sort);
                    }
                    tempEntityList = IBPUser.SearchRBACUserOfPUserByHQL(where, sort, page, limit);

                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACUser>(tempEntityList);
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
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue BT_UDTO_SearchPUserByFieldKey(string fieldKey, string fieldVal, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(fieldKey))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "传入参数(fieldKey)值为空!";
                    return tempBaseResultDataValue;
                }
                if (string.IsNullOrEmpty(fieldVal))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "传入参数(fieldVal)值为空!";
                    return tempBaseResultDataValue;
                }
                var tempEntity = IBPUser.SearchPUserByFieldKey(fieldKey, fieldVal);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PUser>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        public BaseResultDataValue BT_UDTO_SearchRBACUserByFieldKey(string fieldKey, string fieldVal, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(fieldKey))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "传入参数(fieldKey)值为空!";
                    return tempBaseResultDataValue;
                }
                if (string.IsNullOrEmpty(fieldVal))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "传入参数(fieldVal)值为空!";
                    return tempBaseResultDataValue;
                }
                var tempEntity = IBPUser.SearchRBACUserByFieldKey(fieldKey, fieldVal);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACUser>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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

        #endregion

        #region 封装集成平台服务
        public BaseResultDataValue RS_UDTO_SearchRBACUserOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(platformUrl))
            {
                //从系统运行参数中获取
                platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                    return baseresultdata;
                }
            }
            try
            {
                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL";
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的帐户列表信息开始!");
                if (!string.IsNullOrEmpty(sort))
                {
                    string sort1 = JsonHelper.UrlEncode(sort);
                    sort = sort1;
                }
                if (!string.IsNullOrEmpty(where))
                    where = JsonHelper.UrlEncode(where);
                if (!string.IsNullOrEmpty(sort))
                    sort = JsonHelper.UrlEncode(sort);
                string paramStr = string.Format("?page={0}&limit={1}&fields={2}&where={3}&sort={4}&isPlanish={5}", page, limit, fields, where, sort, isPlanish);
                // paramStr = JsonHelper.UrlEncode(paramStr);
                url = url + paramStr;
                string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
                //ZhiFang.Common.Log.Log.Debug(string.Format("resultStr:{0}", resultStr));
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的帐户列表信息结束!");
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
                        baseresultdata.ErrorInfo = "获取智方集成平台的帐户列表信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取智方集成平台的帐户列表信息返回结果为空!";
                    return baseresultdata;
                }

            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            return baseresultdata;
        }
        public BaseResultDataValue RBAC_UDTO_SearchHREmpIdentityByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(platformUrl))
            {
                //从系统运行参数中获取
                platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                    return baseresultdata;
                }
            }
            try
            {
                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL";
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的员工身份列表信息开始!");
                if (!string.IsNullOrEmpty(sort))
                {
                    string sort1 = JsonHelper.UrlEncode(sort);
                    sort = sort1;
                }
                if (!string.IsNullOrEmpty(where))
                    where = JsonHelper.UrlEncode(where);
                if (!string.IsNullOrEmpty(sort))
                    sort = JsonHelper.UrlEncode(sort);
                string paramStr = string.Format("?page={0}&limit={1}&fields={2}&where={3}&sort={4}&isPlanish={5}", page, limit, fields, where, sort, isPlanish);
                // paramStr = JsonHelper.UrlEncode(paramStr);
                url = url + paramStr;
                string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
                //ZhiFang.Common.Log.Log.Debug(string.Format("resultStr:{0}", resultStr));
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的员工身份列表信息结束!");
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
                        baseresultdata.ErrorInfo = "获取智方集成平台的员工身份列表信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取智方集成平台的员工身份列表信息返回结果为空!";
                    return baseresultdata;
                }

            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            return baseresultdata;
        }
        public BaseResultDataValue RBAC_UDTO_SearchHRDeptEmpByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(platformUrl))
            {
                //从系统运行参数中获取
                platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                    return baseresultdata;
                }
            }
            try
            {
                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL";
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的部门员工列表信息开始!");
                if (!string.IsNullOrEmpty(sort))
                {
                    string sort1 = JsonHelper.UrlEncode(sort);
                    sort = sort1;
                }
                if (!string.IsNullOrEmpty(where))
                    where = JsonHelper.UrlEncode(where);
                if (!string.IsNullOrEmpty(sort))
                    sort = JsonHelper.UrlEncode(sort);
                string paramStr = string.Format("?page={0}&limit={1}&fields={2}&where={3}&sort={4}&isPlanish={5}", page, limit, fields, where, sort, isPlanish);
                // paramStr = JsonHelper.UrlEncode(paramStr);
                url = url + paramStr;
                string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
                //ZhiFang.Common.Log.Log.Debug(string.Format("resultStr:{0}", resultStr));
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的部门员工列表信息结束!");
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
                        baseresultdata.ErrorInfo = "获取智方集成平台的部门员工列表信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取智方集成平台的部门员工列表信息返回结果为空!";
                    return baseresultdata;
                }

            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            return baseresultdata;
        }

        #region 员工信息
        public BaseResultDataValue RS_UDTO_SearchHREmployeeOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(platformUrl))
            {
                //从系统运行参数中获取
                platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                    return baseresultdata;
                }
            }
            try
            {
                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL";
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的员工列表信息开始!");
                if (!string.IsNullOrEmpty(sort))
                {
                    string sort1 = JsonHelper.UrlEncode(sort);
                    sort = sort1;
                }
                if (!string.IsNullOrEmpty(where))
                    where = JsonHelper.UrlEncode(where);
                if (!string.IsNullOrEmpty(sort))
                    sort = JsonHelper.UrlEncode(sort);
                string paramStr = string.Format("?page={0}&limit={1}&fields={2}&where={3}&sort={4}&isPlanish={5}", page, limit, fields, where, sort, isPlanish);
                // paramStr = JsonHelper.UrlEncode(paramStr);
                url = url + paramStr;
                string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
                //ZhiFang.Common.Log.Log.Debug(string.Format("resultStr:{0}", resultStr));
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的员工列表信息结束!");
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
                        baseresultdata.ErrorInfo = "获取智方集成平台的员工列表信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取智方集成平台的员工列表信息返回结果为空!";
                    return baseresultdata;
                }

            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            return baseresultdata;
        }
        /// <summary>
        /// 查询部门直属员工列表(包含子部门)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_GetHREmployeeByHRDeptID(string platformUrl, string where, int limit, int page, bool isPlanish, string fields, string sort)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(platformUrl))
                {
                    //从系统运行参数中获取
                    platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                    if (string.IsNullOrEmpty(platformUrl))
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                        return baseresultdata;
                    }
                }
                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID";
                //ZhiFang.Common.Log.Log.Debug("查询部门直属员工列表(包含子部门)信息开始!");
                if (!string.IsNullOrEmpty(sort))
                {
                    string sort1 = JsonHelper.UrlEncode(sort);
                    sort = sort1;
                }
                if (!string.IsNullOrEmpty(where))
                    where = JsonHelper.UrlEncode(where);
                if (!string.IsNullOrEmpty(sort))
                    sort = JsonHelper.UrlEncode(sort);
                string paramStr = string.Format("?page={0}&limit={1}&fields={2}&where={3}&sort={4}&isPlanish={5}", page, limit, fields, where, sort, isPlanish);
                url = url + paramStr;
                string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
                //ZhiFang.Common.Log.Log.Debug(string.Format("resultStr:{0}", resultStr));
                //ZhiFang.Common.Log.Log.Debug("查询部门直属员工列表(包含子部门)结束!");
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
                        baseresultdata.ErrorInfo = "查询部门直属员工列表(包含子部门)信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "查询部门直属员工列表(包含子部门)信息返回结果为空!";
                    return baseresultdata;
                }
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseresultdata;
        }
        public BaseResultDataValue RBAC_UDTO_SearchHREmployeeByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "传入的调用集成平台URL(platformUrl)参数为空!";
                    //从系统运行参数中获取
                    //return baseresultdata;
                }
                if (platformUrl.LastIndexOf("/") <= -1)
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL";
                ZhiFang.Common.Log.Log.Debug("查询员工信息开始!");

                if (!string.IsNullOrEmpty(sort))
                {
                    string sort1 = JsonHelper.UrlEncode(sort);
                    ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_SearchHREmployeeByHQL.sort.UrlEncode:" + sort1);
                    sort = sort1;
                }
                if (!string.IsNullOrEmpty(where))
                    where = JsonHelper.UrlEncode(where);
                if (!string.IsNullOrEmpty(sort))
                    sort = JsonHelper.UrlEncode(sort);
                string paramStr = string.Format("?page={0}&limit={1}&fields={2}&where={3}&sort={4}&isPlanish={5}", page, limit, fields, where, sort, isPlanish);
                // paramStr = JsonHelper.UrlEncode(paramStr);
                ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_SearchHREmployeeByHQL.paramStr.UrlEncode:" + paramStr);
                url = url + paramStr;
                ZhiFang.Common.Log.Log.Debug(string.Format("url:{0}", url));
                string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
                //ZhiFang.Common.Log.Log.Debug(string.Format("resultStr:{0}", resultStr));
                ZhiFang.Common.Log.Log.Debug("查询员工信息结束!");
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
                        baseresultdata.ErrorInfo = "查询员工信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "查询员工信息返回结果为空!";
                    return baseresultdata;
                }
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseresultdata;
        }
        public BaseResultDataValue BT_UDTO_SyncPuserListOfHREmployeeToLIMP(string platformUrl, string syncType)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (syncType != "emptolimp")
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "syncType传入错误!";
                return baseresultdata;
            }

            ZhiFang.Common.Log.Log.Debug("将6.6数据库的人员同步到集成平台开始!");
            if (string.IsNullOrEmpty(platformUrl))
            {
                //从系统运行参数中获取
                platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                    return baseresultdata;
                }
            }
            if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                platformUrl = platformUrl + "/";
            string url1 = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_AddHREmployee";

            Dictionary<JObject, JObject> objList = new Dictionary<JObject, JObject>();
            objList = IBPUser.GetSyncPUserList();
            foreach (var item in objList)
            {
                JObject jemp = item.Key;
                string postData = jemp.ToString().Replace(Environment.NewLine, "");
                ZhiFang.Common.Log.Log.Debug("HREmployee:" + postData);
                try
                {
                    string resultStr = WebRequestHelp.Post(postData, "JSON", url1, WebRequestHelp.TIME_OUT_MILLISECOND);
                    if (!string.IsNullOrEmpty(resultStr))
                    {
                        JObject jresult = JObject.Parse(resultStr);
                        if (jresult["success"] != null)
                        {
                            ZhiFang.Common.Log.Log.Debug("resultStr:" + resultStr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("将6.6数据库的人员同步到集成平台错误:" + ex.StackTrace);
                    break;
                }
                ZhiFang.Common.Log.Log.Debug("将6.6数据库的人员同步到集成平台结束!");
            }

            return baseresultdata;
        }
        public BaseResultDataValue BT_UDTO_SyncPuserListOfRBACUseToLIMP(string platformUrl, string syncType)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (syncType != "usertolimp")
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "syncType传入错误!";
                return baseresultdata;
            }

            ZhiFang.Common.Log.Log.Debug("将6.6数据库的人员的帐号同步到集成平台开始!");
            if (string.IsNullOrEmpty(platformUrl))
            {
                //从系统运行参数中获取
                platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                    return baseresultdata;
                }
            }
            if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                platformUrl = platformUrl + "/";
            string url1 = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACUser";

            Dictionary<JObject, JObject> objList = new Dictionary<JObject, JObject>();
            objList = IBPUser.GetSyncPUserList();
            foreach (var item in objList)
            {
                JObject juser = item.Value;
                string postData2 = juser.ToString().Replace(Environment.NewLine, "");
                ZhiFang.Common.Log.Log.Debug("RBACUser:" + postData2);
                try
                {
                    string resultStr = WebRequestHelp.Post(postData2, "JSON", url1, WebRequestHelp.TIME_OUT_MILLISECOND);
                    if (!string.IsNullOrEmpty(resultStr))
                    {
                        JObject jresult = JObject.Parse(resultStr);
                        if (jresult["success"] != null)
                        {
                            ZhiFang.Common.Log.Log.Debug("resultStr:" + resultStr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("将6.6数据库的人员的帐号同步到集成平台错误:" + ex.StackTrace);
                    break;
                }
            }
            ZhiFang.Common.Log.Log.Debug("将6.6数据库的人员的帐号同步到集成平台结束!");
            return baseresultdata;
        }

        #endregion

        #region 部门信息
        public BaseResultDataValue RBAC_UDTO_SearchHRDeptByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (string.IsNullOrEmpty(platformUrl))
            {
                //从系统运行参数中获取
                platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                    return baseresultdata;
                }
            }
            try
            {
                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL";
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的部门列表信息开始!");

                if (!string.IsNullOrEmpty(sort))
                {
                    string sort1 = JsonHelper.UrlEncode(sort);
                    ZhiFang.Common.Log.Log.Debug("RS_UDTO_SearchHRDeptOFLIMPByHQL.sort.UrlEncode:" + sort1);
                    sort = sort1;
                }
                if (!string.IsNullOrEmpty(where))
                    where = JsonHelper.UrlEncode(where);
                if (!string.IsNullOrEmpty(sort))
                    sort = JsonHelper.UrlEncode(sort);
                string paramStr = string.Format("?page={0}&limit={1}&fields={2}&where={3}&sort={4}&isPlanish={5}", page, limit, fields, where, sort, isPlanish);
                // paramStr = JsonHelper.UrlEncode(paramStr);
                url = url + paramStr;
                string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
                //ZhiFang.Common.Log.Log.Debug(string.Format("resultStr:{0}", resultStr));
                //ZhiFang.Common.Log.Log.Debug("获取智方集成平台的部门列表信息结束!");
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
                        baseresultdata.ErrorInfo = "获取智方集成平台的部门列表信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取智方集成平台的部门列表信息返回结果为空!";
                    return baseresultdata;
                }

            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误：" + ex.Message + ex.StackTrace);
            }
            return baseresultdata;
        }

        public BaseResultDataValue RBAC_RJ_GetHRDeptFrameListTree(string platformUrl, string id, string fields)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(platformUrl))
                {
                    //从系统运行参数中获取
                    platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                    if (string.IsNullOrEmpty(platformUrl))
                    {
                        baseresultdata.success = false;
                        baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                        return baseresultdata;
                    }
                }
                if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                    platformUrl = platformUrl + "/";
                string url = platformUrl + "ServerWCF/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree";
                //ZhiFang.Common.Log.Log.Debug("根据部门ID获取部门列表树信息开始!");

                long tempHRDeptId = 0;
                if (!((string.IsNullOrEmpty(id.Trim())) || (id.ToLower().Trim() == "root")))
                    tempHRDeptId = Int64.Parse(id);
                string paramStr = string.Format("?id={0}&fields={1}", tempHRDeptId, fields);
                url = url + paramStr;
                string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
                //ZhiFang.Common.Log.Log.Debug(string.Format("resultStr:{0}", resultStr));
                //ZhiFang.Common.Log.Log.Debug("根据部门ID获取部门列表树信息结束!");
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
                        baseresultdata.ErrorInfo = "根据部门ID获取部门列表树信息异常!";
                        return baseresultdata;
                    }
                }
                else
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "查询员工信息返回结果为空!";
                    return baseresultdata;
                }
            }
            catch (Exception ex)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseresultdata;
        }
        public BaseResultDataValue BT_UDTO_SyncDeptListToLIMP(string platformUrl, string syncType)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            if (syncType != "tolimp")
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "syncType传入错误!";
                return baseresultdata;
            }

            ZhiFang.Common.Log.Log.Debug("将6.6数据库的科室同步到集成平台开始!");
            if (string.IsNullOrEmpty(platformUrl))
            {
                //从系统运行参数中获取
                platformUrl = BParameterCache.GetParaValue(SYSParaNo.集成平台服务访问URL.Key);
                if (string.IsNullOrEmpty(platformUrl))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "获取集成平台服务访问URL的运行参数值为空!";
                    return baseresultdata;
                }
            }
            if (platformUrl.Substring(0, platformUrl.Length - 1) != "/")
                platformUrl = platformUrl + "/";
            string url = platformUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_AddHRDept";
            ZhiFang.Common.Log.Log.Debug("WebRequestHelp.Post.url:" + url);
            IList<JObject> objList = new List<JObject>();
            objList = IBDepartment.GetSyncDeptList();
            foreach (var jPostData in objList)
            {
                // 字符串提交方式              
                string postData = jPostData.ToString().Replace(Environment.NewLine, "");
                try
                {
                    //ZhiFang.Common.Log.Log.Debug("postData:" + postData);
                    string resultStr = WebRequestHelp.Post(postData, "JSON", url, WebRequestHelp.TIME_OUT_MILLISECOND);
                    if (!string.IsNullOrEmpty(resultStr))
                    {
                        //ZhiFang.Common.Log.Log.Debug("同步结果:"+ resultStr);
                    }
                }
                catch (Exception ex)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "调用平台服务错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("将6.6数据库的科室同步到集成平台错误:" + ex.StackTrace);
                    break;
                }
                ZhiFang.Common.Log.Log.Debug("将6.6数据库的科室同步到集成平台结束!");
            }

            return baseresultdata;
        }
        #endregion

        #endregion

        #region Department
        //Add  Department
        public BaseResultDataValue BT_UDTO_AddDepartment(Department entity)
        {
            IBDepartment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBDepartment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBDepartment.Get(IBDepartment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBDepartment.Entity);
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
        //Update  Department
        public BaseResultBool BT_UDTO_UpdateDepartment(Department entity)
        {
            IBDepartment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDepartment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  Department
        public BaseResultBool BT_UDTO_UpdateDepartmentByField(Department entity, string fields, long empID, string empName)
        {
            IBDepartment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBDepartment.Entity, fields);
                    if (tempArray != null)
                    {
                        Department serverDept = IBDepartment.Get(IBDepartment.Entity.Id);
                        tempBaseResultBool.success = IBDepartment.Update(tempArray);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBDepartment.AddSCOperation(serverDept, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBDepartment.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateDepartmentByField.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  Department
        public BaseResultBool BT_UDTO_DelDepartment(int longDepartmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDepartment.Remove(longDepartmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchDepartment(Department entity)
        {
            IBDepartment.Entity = entity;
            EntityList<Department> tempEntityList = new EntityList<Department>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBDepartment.Search();
                tempEntityList.count = IBDepartment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Department>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchDepartmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<Department> tempEntityList = new EntityList<Department>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBDepartment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBDepartment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Department>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchDepartmentById(int id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBDepartment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<Department>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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

        public BaseResultBool BT_UDTO_AddWarpAndDept()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                //tempBaseResultBool = IBBloodBReqForm.AddWarpAndDept();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_AddWarpAndDept.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }

        #endregion

        #region PUser
        //Add  PUser
        public BaseResultDataValue BT_UDTO_AddPUser(PUser entity)
        {

            IBPUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPUser.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPUser.Get(IBPUser.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPUser.Entity);
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
        //Update  PUser
        public BaseResultBool BT_UDTO_UpdatePUser(PUser entity)
        {
            IBPUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPUser.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PUser
        public BaseResultBool BT_UDTO_UpdatePUserByField(PUser entity, string fields, long empID, string empName)
        {
            if (fields.Contains("Password"))
                entity.Password = IBPUser.CovertPassWord(entity.Password);
            IBPUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPUser.Entity, fields);
                    if (tempArray != null)
                    {
                        PUser serverPUser = IBPUser.Get(IBPUser.Entity.Id);
                        tempBaseResultBool.success = IBPUser.Update(tempArray);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBPUser.AddSCOperation(serverPUser, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPUser.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  PUser
        public BaseResultBool BT_UDTO_DelPUser(long longPUserID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPUser.Remove(int.Parse(longPUserID.ToString()));
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchPUser(PUser entity)
        {
            IBPUser.Entity = entity;
            EntityList<PUser> tempEntityList = new EntityList<PUser>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPUser.Search();
                tempEntityList.count = IBPUser.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PUser>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PUser> tempEntityList = new EntityList<PUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPUser.SearchListByHQL(where, page, limit);
                }
                for (int i = 0; i < tempEntityList.list.Count; i++)
                {
                    tempEntityList.list[i].Password = IBPUser.UnCovertPassWord(tempEntityList.list[i].Password);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PUser>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchPUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPUser.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PUser>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchPUserById.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region DepartmentUser
        //Add  DepartmentUser
        public BaseResultDataValue BT_UDTO_AddDepartmentUser(DepartmentUser entity)
        {
            IBDepartmentUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBDepartmentUser.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBDepartmentUser.Get(IBDepartmentUser.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBDepartmentUser.Entity);
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
        //Update  DepartmentUser
        public BaseResultBool BT_UDTO_UpdateDepartmentUser(DepartmentUser entity)
        {
            IBDepartmentUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDepartmentUser.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  DepartmentUser
        public BaseResultBool BT_UDTO_UpdateDepartmentUserByField(DepartmentUser entity, string fields)
        {
            IBDepartmentUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBDepartmentUser.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBDepartmentUser.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBDepartmentUser.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  DepartmentUser
        public BaseResultBool BT_UDTO_DelDepartmentUser(long longDepartmentUserID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDepartmentUser.Remove(longDepartmentUserID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchDepartmentUser(DepartmentUser entity)
        {
            IBDepartmentUser.Entity = entity;
            EntityList<DepartmentUser> tempEntityList = new EntityList<DepartmentUser>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBDepartmentUser.Search();
                tempEntityList.count = IBDepartmentUser.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<DepartmentUser>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchDepartmentUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<DepartmentUser> tempEntityList = new EntityList<DepartmentUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBDepartmentUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBDepartmentUser.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<DepartmentUser>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchDepartmentUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBDepartmentUser.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<DepartmentUser>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodDocGrade
        //Add  BloodDocGrade
        public BaseResultDataValue BT_UDTO_AddBloodDocGrade(BloodDocGrade entity)
        {
            IBBloodDocGrade.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodDocGrade.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodDocGrade.Get(IBBloodDocGrade.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodDocGrade.Entity);
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
        //Update  BloodDocGrade
        public BaseResultBool BT_UDTO_UpdateBloodDocGrade(BloodDocGrade entity)
        {
            IBBloodDocGrade.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodDocGrade.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodDocGrade
        public BaseResultBool BT_UDTO_UpdateBloodDocGradeByField(BloodDocGrade entity, string fields)
        {
            IBBloodDocGrade.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodDocGrade.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodDocGrade.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodDocGrade.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodDocGrade
        public BaseResultBool BT_UDTO_DelBloodDocGrade(long longBloodDocGradeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodDocGrade.Remove(longBloodDocGradeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodDocGrade(BloodDocGrade entity)
        {
            IBBloodDocGrade.Entity = entity;
            EntityList<BloodDocGrade> tempEntityList = new EntityList<BloodDocGrade>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodDocGrade.Search();
                tempEntityList.count = IBBloodDocGrade.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodDocGrade>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodDocGradeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodDocGrade> tempEntityList = new EntityList<BloodDocGrade>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodDocGrade.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodDocGrade.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodDocGrade>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodDocGradeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodDocGrade.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodDocGrade>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BUserUIConfig
        //Add  BUserUIConfig
        public BaseResultDataValue BT_UDTO_AddBUserUIConfig(BUserUIConfig entity)
        {
            ZhiFang.Common.Log.Log.Info("ST_UDTO_AddBUserUIConfig:" + entity.Id);
            IBBUserUIConfig.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBUserUIConfig.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBUserUIConfig.Get(IBBUserUIConfig.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBUserUIConfig.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AddBUserUIConfig:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        //Update  BUserUIConfig
        public BaseResultBool BT_UDTO_UpdateBUserUIConfig(BUserUIConfig entity)
        {
            IBBUserUIConfig.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBUserUIConfig.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BUserUIConfig
        public BaseResultBool BT_UDTO_UpdateBUserUIConfigByField(BUserUIConfig entity, string fields)
        {
            ZhiFang.Common.Log.Log.Info("ST_UDTO_AddBUserUIConfig:" + entity.Id);
            IBBUserUIConfig.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBUserUIConfig.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBUserUIConfig.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBUserUIConfig.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_UpdateBUserUIConfigByField:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BUserUIConfig
        public BaseResultBool BT_UDTO_DelBUserUIConfig(long longBUserUIConfigID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBUserUIConfig.Remove(longBUserUIConfigID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBUserUIConfig(BUserUIConfig entity)
        {
            IBBUserUIConfig.Entity = entity;
            EntityList<BUserUIConfig> tempEntityList = new EntityList<BUserUIConfig>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBUserUIConfig.Search();
                tempEntityList.count = IBBUserUIConfig.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BUserUIConfig>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBUserUIConfigByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BUserUIConfig> tempEntityList = new EntityList<BUserUIConfig>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBUserUIConfig.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBUserUIConfig.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BUserUIConfig>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBUserUIConfigById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBUserUIConfig.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BUserUIConfig>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region SCOperation
        //Add  SCOperation
        public BaseResultDataValue BT_UDTO_AddSCOperation(SCOperation entity)
        {
            IBSCOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCOperation.Get(IBSCOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCOperation.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  SCOperation
        public BaseResultBool BT_UDTO_UpdateSCOperation(SCOperation entity)
        {
            IBSCOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCOperation
        public BaseResultBool BT_UDTO_UpdateSCOperationByField(SCOperation entity, string fields)
        {
            IBSCOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCOperation.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  SCOperation
        public BaseResultBool BT_UDTO_DelSCOperation(long longSCOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCOperation.Remove(longSCOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchSCOperation(SCOperation entity)
        {
            IBSCOperation.Entity = entity;
            EntityList<SCOperation> tempEntityList = new EntityList<SCOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCOperation.Search();
                tempEntityList.count = IBSCOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCOperation>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //查询公共操作记录表ByHQL
        public BaseResultDataValue BT_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCOperation> tempEntityList = new EntityList<SCOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCOperation>(tempEntityList);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue BT_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCOperation>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region BloodABO
        //Add  BloodABO
        public BaseResultDataValue BT_UDTO_AddBloodABO(BloodABO entity)
        {
            IBBloodABO.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodABO.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodABO.Get(IBBloodABO.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodABO.Entity);
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
        //Update  BloodABO
        public BaseResultBool BT_UDTO_UpdateBloodABO(BloodABO entity)
        {
            IBBloodABO.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodABO.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodABO
        public BaseResultBool BT_UDTO_UpdateBloodABOByField(BloodABO entity, string fields, long empID, string empName)
        {
            IBBloodABO.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodABO.Entity, fields);
                    if (tempArray != null)
                    {
                        BloodABO serverEntity = IBBloodABO.Get(IBPUser.Entity.Id);
                        tempBaseResultBool.success = IBBloodABO.Update(tempArray);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodABO.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodABO.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodABO
        public BaseResultBool BT_UDTO_DelBloodABO(long longBloodABOID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodABO.Remove(longBloodABOID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodABO(BloodABO entity)
        {
            IBBloodABO.Entity = entity;
            EntityList<BloodABO> tempEntityList = new EntityList<BloodABO>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodABO.Search();
                tempEntityList.count = IBBloodABO.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodABO>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodABOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodABO> tempEntityList = new EntityList<BloodABO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodABO.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodABO.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodABO>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodABOById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodABO.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodABO>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodAggluItem
        //Add  BloodAggluItem
        public BaseResultDataValue BT_UDTO_AddBloodAggluItem(BloodAggluItem entity)
        {
            IBBloodAggluItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodAggluItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodAggluItem.Get(IBBloodAggluItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodAggluItem.Entity);
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
        //Update  BloodAggluItem
        public BaseResultBool BT_UDTO_UpdateBloodAggluItem(BloodAggluItem entity)
        {
            IBBloodAggluItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodAggluItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodAggluItem
        public BaseResultBool BT_UDTO_UpdateBloodAggluItemByField(BloodAggluItem entity, string fields, long empID, string empName)
        {
            IBBloodAggluItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodAggluItem serverEntity = IBBloodAggluItem.Get(IBBloodAggluItem.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodAggluItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodAggluItem.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodAggluItem.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodAggluItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodAggluItem
        public BaseResultBool BT_UDTO_DelBloodAggluItem(long longBloodAggluItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodAggluItem.Remove(longBloodAggluItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodAggluItem(BloodAggluItem entity)
        {
            IBBloodAggluItem.Entity = entity;
            EntityList<BloodAggluItem> tempEntityList = new EntityList<BloodAggluItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodAggluItem.Search();
                tempEntityList.count = IBBloodAggluItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodAggluItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodAggluItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodAggluItem> tempEntityList = new EntityList<BloodAggluItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodAggluItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodAggluItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodAggluItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodAggluItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodAggluItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodAggluItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBagHandoverOper
        //Add  BloodBagHandoverOper
        public BaseResultDataValue BT_UDTO_AddBloodBagHandoverOper(BloodBagHandoverOper entity)
        {
            IBBloodBagHandoverOper.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagHandoverOper.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagHandoverOper.Get(IBBloodBagHandoverOper.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagHandoverOper.Entity);
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
        //Update  BloodBagHandoverOper
        public BaseResultBool BT_UDTO_UpdateBloodBagHandoverOper(BloodBagHandoverOper entity)
        {
            IBBloodBagHandoverOper.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagHandoverOper.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagHandoverOper
        public BaseResultBool BT_UDTO_UpdateBloodBagHandoverOperByField(BloodBagHandoverOper entity, string fields)
        {
            IBBloodBagHandoverOper.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagHandoverOper.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagHandoverOper.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagHandoverOper.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBagHandoverOper
        public BaseResultBool BT_UDTO_DelBloodBagHandoverOper(long longBloodBagHandoverOperID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagHandoverOper.Remove(longBloodBagHandoverOperID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagHandoverOper(BloodBagHandoverOper entity)
        {
            IBBloodBagHandoverOper.Entity = entity;
            EntityList<BloodBagHandoverOper> tempEntityList = new EntityList<BloodBagHandoverOper>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagHandoverOper.Search();
                tempEntityList.count = IBBloodBagHandoverOper.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagHandoverOper>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagHandoverOperByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagHandoverOper> tempEntityList = new EntityList<BloodBagHandoverOper>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagHandoverOper.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagHandoverOper.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagHandoverOper>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagHandoverOperById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagHandoverOper.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagHandoverOper>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBagProcess
        //Add  BloodBagProcess
        public BaseResultDataValue BT_UDTO_AddBloodBagProcess(BloodBagProcess entity)
        {
            IBBloodBagProcess.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagProcess.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagProcess.Get(IBBloodBagProcess.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagProcess.Entity);
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
        //Update  BloodBagProcess
        public BaseResultBool BT_UDTO_UpdateBloodBagProcess(BloodBagProcess entity)
        {
            IBBloodBagProcess.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagProcess.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagProcess
        public BaseResultBool BT_UDTO_UpdateBloodBagProcessByField(BloodBagProcess entity, string fields)
        {
            IBBloodBagProcess.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagProcess.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagProcess.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagProcess.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBagProcess
        public BaseResultBool BT_UDTO_DelBloodBagProcess(long longBloodBagProcessID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagProcess.Remove(longBloodBagProcessID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcess(BloodBagProcess entity)
        {
            IBBloodBagProcess.Entity = entity;
            EntityList<BloodBagProcess> tempEntityList = new EntityList<BloodBagProcess>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagProcess.Search();
                tempEntityList.count = IBBloodBagProcess.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagProcess>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcessByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagProcess> tempEntityList = new EntityList<BloodBagProcess>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagProcess.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagProcess.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagProcess>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcessById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagProcess.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagProcess>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBagProcessType
        //Add  BloodBagProcessType
        public BaseResultDataValue BT_UDTO_AddBloodBagProcessType(BloodBagProcessType entity)
        {
            IBBloodBagProcessType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagProcessType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagProcessType.Get(IBBloodBagProcessType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagProcessType.Entity);
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
        //Update  BloodBagProcessType
        public BaseResultBool BT_UDTO_UpdateBloodBagProcessType(BloodBagProcessType entity)
        {
            IBBloodBagProcessType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagProcessType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagProcessType
        public BaseResultBool BT_UDTO_UpdateBloodBagProcessTypeByField(BloodBagProcessType entity, string fields, long empID, string empName)
        {
            IBBloodBagProcessType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodBagProcessType serverEntity = IBBloodBagProcessType.Get(IBBloodBagProcessType.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagProcessType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagProcessType.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodBagProcessType.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagProcessType.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBagProcessType
        public BaseResultBool BT_UDTO_DelBloodBagProcessType(long longBloodBagProcessTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagProcessType.Remove(longBloodBagProcessTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcessType(BloodBagProcessType entity)
        {
            IBBloodBagProcessType.Entity = entity;
            EntityList<BloodBagProcessType> tempEntityList = new EntityList<BloodBagProcessType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagProcessType.Search();
                tempEntityList.count = IBBloodBagProcessType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagProcessType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagProcessType> tempEntityList = new EntityList<BloodBagProcessType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagProcessType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagProcessType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagProcessType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagProcessType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagProcessType>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBagProcessTypeQry
        //Add  BloodBagProcessTypeQry
        public BaseResultDataValue BT_UDTO_AddBloodBagProcessTypeQry(BloodBagProcessTypeQry entity)
        {
            IBBloodBagProcessTypeQry.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagProcessTypeQry.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagProcessTypeQry.Get(IBBloodBagProcessTypeQry.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagProcessTypeQry.Entity);
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
        //Update  BloodBagProcessTypeQry
        public BaseResultBool BT_UDTO_UpdateBloodBagProcessTypeQry(BloodBagProcessTypeQry entity)
        {
            IBBloodBagProcessTypeQry.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagProcessTypeQry.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagProcessTypeQry
        public BaseResultBool BT_UDTO_UpdateBloodBagProcessTypeQryByField(BloodBagProcessTypeQry entity, string fields)
        {
            IBBloodBagProcessTypeQry.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagProcessTypeQry.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagProcessTypeQry.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagProcessTypeQry.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBagProcessTypeQry
        public BaseResultBool BT_UDTO_DelBloodBagProcessTypeQry(long longBloodBagProcessTypeQryID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagProcessTypeQry.Remove(longBloodBagProcessTypeQryID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeQry(BloodBagProcessTypeQry entity)
        {
            IBBloodBagProcessTypeQry.Entity = entity;
            EntityList<BloodBagProcessTypeQry> tempEntityList = new EntityList<BloodBagProcessTypeQry>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagProcessTypeQry.Search();
                tempEntityList.count = IBBloodBagProcessTypeQry.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagProcessTypeQry>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeQryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagProcessTypeQry> tempEntityList = new EntityList<BloodBagProcessTypeQry>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagProcessTypeQry.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagProcessTypeQry.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagProcessTypeQry>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeQryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagProcessTypeQry.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagProcessTypeQry>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBagRecordDtl
        //Add  BloodBagRecordDtl
        public BaseResultDataValue BT_UDTO_AddBloodBagRecordDtl(BloodBagRecordDtl entity)
        {
            IBBloodBagRecordDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagRecordDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagRecordDtl.Get(IBBloodBagRecordDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagRecordDtl.Entity);
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
        //Update  BloodBagRecordDtl
        public BaseResultBool BT_UDTO_UpdateBloodBagRecordDtl(BloodBagRecordDtl entity)
        {
            IBBloodBagRecordDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagRecordDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagRecordDtl
        public BaseResultBool BT_UDTO_UpdateBloodBagRecordDtlByField(BloodBagRecordDtl entity, string fields)
        {
            IBBloodBagRecordDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagRecordDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagRecordDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagRecordDtl.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBagRecordDtl
        public BaseResultBool BT_UDTO_DelBloodBagRecordDtl(long longBloodBagRecordDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagRecordDtl.Remove(longBloodBagRecordDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagRecordDtl(BloodBagRecordDtl entity)
        {
            IBBloodBagRecordDtl.Entity = entity;
            EntityList<BloodBagRecordDtl> tempEntityList = new EntityList<BloodBagRecordDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagRecordDtl.Search();
                tempEntityList.count = IBBloodBagRecordDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagRecordDtl>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagRecordDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagRecordDtl> tempEntityList = new EntityList<BloodBagRecordDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagRecordDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagRecordDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagRecordDtl>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagRecordDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagRecordDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagRecordDtl>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBagRecordItem
        //Add  BloodBagRecordItem
        public BaseResultDataValue BT_UDTO_AddBloodBagRecordItem(BloodBagRecordItem entity)
        {
            IBBloodBagRecordItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagRecordItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagRecordItem.Get(IBBloodBagRecordItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagRecordItem.Entity);
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
        //Update  BloodBagRecordItem
        public BaseResultBool BT_UDTO_UpdateBloodBagRecordItem(BloodBagRecordItem entity)
        {
            IBBloodBagRecordItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagRecordItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagRecordItem
        public BaseResultBool BT_UDTO_UpdateBloodBagRecordItemByField(BloodBagRecordItem entity, string fields, long empID, string empName)
        {
            IBBloodBagRecordItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodBagRecordItem serverEntity = IBBloodBagRecordItem.Get(IBBloodBagRecordItem.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagRecordItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagRecordItem.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodBagRecordItem.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagRecordItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBagRecordItem
        public BaseResultBool BT_UDTO_DelBloodBagRecordItem(long longBloodBagRecordItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagRecordItem.Remove(longBloodBagRecordItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagRecordItem(BloodBagRecordItem entity)
        {
            IBBloodBagRecordItem.Entity = entity;
            EntityList<BloodBagRecordItem> tempEntityList = new EntityList<BloodBagRecordItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagRecordItem.Search();
                tempEntityList.count = IBBloodBagRecordItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagRecordItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagRecordItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagRecordItem> tempEntityList = new EntityList<BloodBagRecordItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagRecordItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagRecordItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagRecordItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagRecordItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagRecordItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagRecordItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBagRecordType
        //Add  BloodBagRecordType
        public BaseResultDataValue BT_UDTO_AddBloodBagRecordType(BloodBagRecordType entity)
        {
            IBBloodBagRecordType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagRecordType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagRecordType.Get(IBBloodBagRecordType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagRecordType.Entity);
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
        //Update  BloodBagRecordType
        public BaseResultBool BT_UDTO_UpdateBloodBagRecordType(BloodBagRecordType entity)
        {
            IBBloodBagRecordType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagRecordType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagRecordType
        public BaseResultBool BT_UDTO_UpdateBloodBagRecordTypeByField(BloodBagRecordType entity, string fields, long empID, string empName)
        {
            IBBloodBagRecordType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodBagRecordType serverEntity = IBBloodBagRecordType.Get(IBBloodBagRecordType.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagRecordType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagRecordType.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodBagRecordType.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagRecordType.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:"+ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBagRecordType
        public BaseResultBool BT_UDTO_DelBloodBagRecordType(long longBloodBagRecordTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagRecordType.Remove(longBloodBagRecordTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagRecordType(BloodBagRecordType entity)
        {
            IBBloodBagRecordType.Entity = entity;
            EntityList<BloodBagRecordType> tempEntityList = new EntityList<BloodBagRecordType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagRecordType.Search();
                tempEntityList.count = IBBloodBagRecordType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagRecordType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagRecordTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagRecordType> tempEntityList = new EntityList<BloodBagRecordType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagRecordType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagRecordType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagRecordType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagRecordTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagRecordType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagRecordType>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBInForm
        //Add  BloodBInForm
        public BaseResultDataValue BT_UDTO_AddBloodBInForm(BloodBInForm entity)
        {
            IBBloodBInForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBInForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBInForm.Get(IBBloodBInForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBInForm.Entity);
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
        //Update  BloodBInForm
        public BaseResultBool BT_UDTO_UpdateBloodBInForm(BloodBInForm entity)
        {
            IBBloodBInForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBInForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBInForm
        public BaseResultBool BT_UDTO_UpdateBloodBInFormByField(BloodBInForm entity, string fields)
        {
            IBBloodBInForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBInForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBInForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBInForm.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBInForm
        public BaseResultBool BT_UDTO_DelBloodBInForm(long longBloodBInFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBInForm.Remove(longBloodBInFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBInForm(BloodBInForm entity)
        {
            IBBloodBInForm.Entity = entity;
            EntityList<BloodBInForm> tempEntityList = new EntityList<BloodBInForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBInForm.Search();
                tempEntityList.count = IBBloodBInForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBInForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBInFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBInForm> tempEntityList = new EntityList<BloodBInForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBInForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBInForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBInForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBInFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBInForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBInForm>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBInItem
        //Add  BloodBInItem
        public BaseResultDataValue BT_UDTO_AddBloodBInItem(BloodBInItem entity)
        {
            IBBloodBInItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBInItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBInItem.Get(IBBloodBInItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBInItem.Entity);
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
        //Update  BloodBInItem
        public BaseResultBool BT_UDTO_UpdateBloodBInItem(BloodBInItem entity)
        {
            IBBloodBInItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBInItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBInItem
        public BaseResultBool BT_UDTO_UpdateBloodBInItemByField(BloodBInItem entity, string fields)
        {
            IBBloodBInItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBInItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBInItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBInItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBInItem
        public BaseResultBool BT_UDTO_DelBloodBInItem(long longBloodBInItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBInItem.Remove(longBloodBInItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBInItem(BloodBInItem entity)
        {
            IBBloodBInItem.Entity = entity;
            EntityList<BloodBInItem> tempEntityList = new EntityList<BloodBInItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBInItem.Search();
                tempEntityList.count = IBBloodBInItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBInItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBInItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBInItem> tempEntityList = new EntityList<BloodBInItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBInItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBInItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBInItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBInItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBInItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBInItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBOutForm
        //Add  BloodBOutForm
        public BaseResultDataValue BT_UDTO_AddBloodBOutForm(BloodBOutForm entity)
        {
            IBBloodBOutForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBOutForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBOutForm.Get(IBBloodBOutForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBOutForm.Entity);
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
        //Update  BloodBOutForm
        public BaseResultBool BT_UDTO_UpdateBloodBOutForm(BloodBOutForm entity)
        {
            IBBloodBOutForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBOutForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBOutForm
        public BaseResultBool BT_UDTO_UpdateBloodBOutFormByField(BloodBOutForm entity, string fields)
        {
            IBBloodBOutForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBOutForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBOutForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBOutForm.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBOutForm
        public BaseResultBool BT_UDTO_DelBloodBOutForm(long longBloodBOutFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBOutForm.Remove(longBloodBOutFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBOutForm(BloodBOutForm entity)
        {
            IBBloodBOutForm.Entity = entity;
            EntityList<BloodBOutForm> tempEntityList = new EntityList<BloodBOutForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBOutForm.Search();
                tempEntityList.count = IBBloodBOutForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBOutForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBOutFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBOutForm> tempEntityList = new EntityList<BloodBOutForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBOutForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBOutForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBOutForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBOutFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBOutForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBOutForm>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBOutItem
        //Add  BloodBOutItem
        public BaseResultDataValue BT_UDTO_AddBloodBOutItem(BloodBOutItem entity)
        {
            IBBloodBOutItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBOutItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBOutItem.Get(IBBloodBOutItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBOutItem.Entity);
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
        //Update  BloodBOutItem
        public BaseResultBool BT_UDTO_UpdateBloodBOutItem(BloodBOutItem entity)
        {
            IBBloodBOutItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBOutItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBOutItem
        public BaseResultBool BT_UDTO_UpdateBloodBOutItemByField(BloodBOutItem entity, string fields)
        {
            IBBloodBOutItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBOutItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBOutItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBOutItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBOutItem
        public BaseResultBool BT_UDTO_DelBloodBOutItem(long longBloodBOutItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBOutItem.Remove(longBloodBOutItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBOutItem(BloodBOutItem entity)
        {
            IBBloodBOutItem.Entity = entity;
            EntityList<BloodBOutItem> tempEntityList = new EntityList<BloodBOutItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBOutItem.Search();
                tempEntityList.count = IBBloodBOutItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBOutItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBOutItem> tempEntityList = new EntityList<BloodBOutItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBOutItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBOutItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBOutItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBOutItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBOutItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBPreForm
        //Add  BloodBPreForm
        public BaseResultDataValue BT_UDTO_AddBloodBPreForm(BloodBPreForm entity)
        {
            IBBloodBPreForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBPreForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBPreForm.Get(IBBloodBPreForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBPreForm.Entity);
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
        //Update  BloodBPreForm
        public BaseResultBool BT_UDTO_UpdateBloodBPreForm(BloodBPreForm entity)
        {
            IBBloodBPreForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBPreForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBPreForm
        public BaseResultBool BT_UDTO_UpdateBloodBPreFormByField(BloodBPreForm entity, string fields)
        {
            IBBloodBPreForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBPreForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBPreForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBPreForm.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBPreForm
        public BaseResultBool BT_UDTO_DelBloodBPreForm(long longBloodBPreFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBPreForm.Remove(longBloodBPreFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBPreForm(BloodBPreForm entity)
        {
            IBBloodBPreForm.Entity = entity;
            EntityList<BloodBPreForm> tempEntityList = new EntityList<BloodBPreForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBPreForm.Search();
                tempEntityList.count = IBBloodBPreForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBPreForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBPreFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBPreForm> tempEntityList = new EntityList<BloodBPreForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBPreForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBPreForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBPreForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBPreFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBPreForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBPreForm>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBPreItem
        //Add  BloodBPreItem
        public BaseResultDataValue BT_UDTO_AddBloodBPreItem(BloodBPreItem entity)
        {
            IBBloodBPreItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBPreItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBPreItem.Get(IBBloodBPreItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBPreItem.Entity);
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
        //Update  BloodBPreItem
        public BaseResultBool BT_UDTO_UpdateBloodBPreItem(BloodBPreItem entity)
        {
            IBBloodBPreItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBPreItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBPreItem
        public BaseResultBool BT_UDTO_UpdateBloodBPreItemByField(BloodBPreItem entity, string fields)
        {
            IBBloodBPreItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBPreItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBPreItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBPreItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBPreItem
        public BaseResultBool BT_UDTO_DelBloodBPreItem(long longBloodBPreItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBPreItem.Remove(longBloodBPreItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBPreItem(BloodBPreItem entity)
        {
            IBBloodBPreItem.Entity = entity;
            EntityList<BloodBPreItem> tempEntityList = new EntityList<BloodBPreItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBPreItem.Search();
                tempEntityList.count = IBBloodBPreItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBPreItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBPreItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBPreItem> tempEntityList = new EntityList<BloodBPreItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBPreItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBPreItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBPreItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBPreItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBPreItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBPreItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBReqForm
        //Add  BloodBReqForm
        public BaseResultDataValue BT_UDTO_AddBloodBReqForm(BloodBReqForm entity)
        {
            IBBloodBReqForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBReqForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBReqForm.Get(IBBloodBReqForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBReqForm.Entity);
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
        //Update  BloodBReqForm
        public BaseResultBool BT_UDTO_UpdateBloodBReqForm(BloodBReqForm entity)
        {
            IBBloodBReqForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBReqForm
        public BaseResultBool BT_UDTO_UpdateBloodBReqFormByField(BloodBReqForm entity, string fields)
        {
            IBBloodBReqForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBReqForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBReqForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBReqForm.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBReqForm
        public BaseResultBool BT_UDTO_DelBloodBReqForm(long longBloodBReqFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqForm.Remove(longBloodBReqFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBReqForm(BloodBReqForm entity)
        {
            IBBloodBReqForm.Entity = entity;
            EntityList<BloodBReqForm> tempEntityList = new EntityList<BloodBReqForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBReqForm.Search();
                tempEntityList.count = IBBloodBReqForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqForm> tempEntityList = new EntityList<BloodBReqForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBReqForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBReqForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBReqForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBReqForm>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBReqFormResult
        //Add  BloodBReqFormResult
        public BaseResultDataValue BT_UDTO_AddBloodBReqFormResult(BloodBReqFormResult entity)
        {
            IBBloodBReqFormResult.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBReqFormResult.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBReqFormResult.Get(IBBloodBReqFormResult.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBReqFormResult.Entity);
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
        //Update  BloodBReqFormResult
        public BaseResultBool BT_UDTO_UpdateBloodBReqFormResult(BloodBReqFormResult entity)
        {
            IBBloodBReqFormResult.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqFormResult.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBReqFormResult
        public BaseResultBool BT_UDTO_UpdateBloodBReqFormResultByField(BloodBReqFormResult entity, string fields)
        {
            IBBloodBReqFormResult.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBReqFormResult.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBReqFormResult.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBReqFormResult.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBReqFormResult
        public BaseResultBool BT_UDTO_DelBloodBReqFormResult(long longBloodBReqFormResultID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqFormResult.Remove(longBloodBReqFormResultID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormResult(BloodBReqFormResult entity)
        {
            IBBloodBReqFormResult.Entity = entity;
            EntityList<BloodBReqFormResult> tempEntityList = new EntityList<BloodBReqFormResult>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBReqFormResult.Search();
                tempEntityList.count = IBBloodBReqFormResult.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqFormResult>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormResultByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqFormResult> tempEntityList = new EntityList<BloodBReqFormResult>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBReqFormResult.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBReqFormResult.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqFormResult>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormResultById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBReqFormResult.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBReqFormResult>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBReqItem
        //Add  BloodBReqItem
        public BaseResultDataValue BT_UDTO_AddBloodBReqItem(BloodBReqItem entity)
        {
            IBBloodBReqItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBReqItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBReqItem.Get(IBBloodBReqItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBReqItem.Entity);
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
        //Update  BloodBReqItem
        public BaseResultBool BT_UDTO_UpdateBloodBReqItem(BloodBReqItem entity)
        {
            IBBloodBReqItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBReqItem
        public BaseResultBool BT_UDTO_UpdateBloodBReqItemByField(BloodBReqItem entity, string fields)
        {
            IBBloodBReqItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBReqItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBReqItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBReqItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBReqItem
        public BaseResultBool BT_UDTO_DelBloodBReqItem(long longBloodBReqItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqItem.Remove(longBloodBReqItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBReqItem(BloodBReqItem entity)
        {
            IBBloodBReqItem.Entity = entity;
            EntityList<BloodBReqItem> tempEntityList = new EntityList<BloodBReqItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBReqItem.Search();
                tempEntityList.count = IBBloodBReqItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqItem> tempEntityList = new EntityList<BloodBReqItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBReqItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBReqItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBReqItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBReqItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBTestItem
        //Add  BloodBTestItem
        public BaseResultDataValue BT_UDTO_AddBloodBTestItem(BloodBTestItem entity)
        {
            IBBloodBTestItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBTestItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBTestItem.Get(IBBloodBTestItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBTestItem.Entity);
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
        //Update  BloodBTestItem
        public BaseResultBool BT_UDTO_UpdateBloodBTestItem(BloodBTestItem entity)
        {
            IBBloodBTestItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBTestItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBTestItem
        public BaseResultBool BT_UDTO_UpdateBloodBTestItemByField(BloodBTestItem entity, string fields, long empID, string empName)
        {
            IBBloodBTestItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodBTestItem serverEntity = IBBloodBTestItem.Get(IBBloodBTestItem.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBTestItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBTestItem.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodBTestItem.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBTestItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBTestItem
        public BaseResultBool BT_UDTO_DelBloodBTestItem(long longBloodBTestItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBTestItem.Remove(longBloodBTestItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBTestItem(BloodBTestItem entity)
        {
            IBBloodBTestItem.Entity = entity;
            EntityList<BloodBTestItem> tempEntityList = new EntityList<BloodBTestItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBTestItem.Search();
                tempEntityList.count = IBBloodBTestItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBTestItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBTestItem> tempEntityList = new EntityList<BloodBTestItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBTestItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBTestItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBTestItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBTestItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBTestItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBTestItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBUnit
        //Add  BloodBUnit
        public BaseResultDataValue BT_UDTO_AddBloodBUnit(BloodBUnit entity)
        {
            IBBloodBUnit.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBUnit.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBUnit.Get(IBBloodBUnit.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBUnit.Entity);
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
        //Update  BloodBUnit
        public BaseResultBool BT_UDTO_UpdateBloodBUnit(BloodBUnit entity)
        {
            IBBloodBUnit.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBUnit.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBUnit
        public BaseResultBool BT_UDTO_UpdateBloodBUnitByField(BloodBUnit entity, string fields)
        {
            IBBloodBUnit.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBUnit.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBUnit.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBUnit.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBUnit
        public BaseResultBool BT_UDTO_DelBloodBUnit(long longBloodBUnitID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBUnit.Remove(longBloodBUnitID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBUnit(BloodBUnit entity)
        {
            IBBloodBUnit.Entity = entity;
            EntityList<BloodBUnit> tempEntityList = new EntityList<BloodBUnit>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBUnit.Search();
                tempEntityList.count = IBBloodBUnit.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBUnit>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBUnit> tempEntityList = new EntityList<BloodBUnit>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBUnit.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBUnit.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBUnit>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBUnitById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBUnit.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBUnit>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodChargeGroupItem
        //Add  BloodChargeGroupItem
        public BaseResultDataValue BT_UDTO_AddBloodChargeGroupItem(BloodChargeGroupItem entity)
        {
            IBBloodChargeGroupItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodChargeGroupItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodChargeGroupItem.Get(IBBloodChargeGroupItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodChargeGroupItem.Entity);
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
        //Update  BloodChargeGroupItem
        public BaseResultBool BT_UDTO_UpdateBloodChargeGroupItem(BloodChargeGroupItem entity)
        {
            IBBloodChargeGroupItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeGroupItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodChargeGroupItem
        public BaseResultBool BT_UDTO_UpdateBloodChargeGroupItemByField(BloodChargeGroupItem entity, string fields)
        {
            IBBloodChargeGroupItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodChargeGroupItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodChargeGroupItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodChargeGroupItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodChargeGroupItem
        public BaseResultBool BT_UDTO_DelBloodChargeGroupItem(long longBloodChargeGroupItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeGroupItem.Remove(longBloodChargeGroupItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodChargeGroupItem(BloodChargeGroupItem entity)
        {
            IBBloodChargeGroupItem.Entity = entity;
            EntityList<BloodChargeGroupItem> tempEntityList = new EntityList<BloodChargeGroupItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodChargeGroupItem.Search();
                tempEntityList.count = IBBloodChargeGroupItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeGroupItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeGroupItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodChargeGroupItem> tempEntityList = new EntityList<BloodChargeGroupItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodChargeGroupItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodChargeGroupItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeGroupItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeGroupItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodChargeGroupItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodChargeGroupItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodChargeItem
        //Add  BloodChargeItem
        public BaseResultDataValue BT_UDTO_AddBloodChargeItem(BloodChargeItem entity)
        {
            IBBloodChargeItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodChargeItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodChargeItem.Get(IBBloodChargeItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodChargeItem.Entity);
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
        //Update  BloodChargeItem
        public BaseResultBool BT_UDTO_UpdateBloodChargeItem(BloodChargeItem entity)
        {
            IBBloodChargeItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodChargeItem
        public BaseResultBool BT_UDTO_UpdateBloodChargeItemByField(BloodChargeItem entity, string fields, long empID, string empName)
        {
            IBBloodChargeItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodChargeItem serverEntity = IBBloodChargeItem.Get(IBBloodChargeItem.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodChargeItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodChargeItem.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodChargeItem.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodChargeItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodChargeItem
        public BaseResultBool BT_UDTO_DelBloodChargeItem(long longBloodChargeItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeItem.Remove(longBloodChargeItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItem(BloodChargeItem entity)
        {
            IBBloodChargeItem.Entity = entity;
            EntityList<BloodChargeItem> tempEntityList = new EntityList<BloodChargeItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodChargeItem.Search();
                tempEntityList.count = IBBloodChargeItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodChargeItem> tempEntityList = new EntityList<BloodChargeItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodChargeItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodChargeItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodChargeItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodChargeItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodChargeItemLink
        //Add  BloodChargeItemLink
        public BaseResultDataValue BT_UDTO_AddBloodChargeItemLink(BloodChargeItemLink entity)
        {
            IBBloodChargeItemLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodChargeItemLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodChargeItemLink.Get(IBBloodChargeItemLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodChargeItemLink.Entity);
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
        //Update  BloodChargeItemLink
        public BaseResultBool BT_UDTO_UpdateBloodChargeItemLink(BloodChargeItemLink entity)
        {
            IBBloodChargeItemLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeItemLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodChargeItemLink
        public BaseResultBool BT_UDTO_UpdateBloodChargeItemLinkByField(BloodChargeItemLink entity, string fields, long empID, string empName)
        {
            IBBloodChargeItemLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodChargeItemLink serverEntity = IBBloodChargeItemLink.Get(IBBloodChargeItemLink.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodChargeItemLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodChargeItemLink.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodChargeItemLink.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodChargeItemLink.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodChargeItemLink
        public BaseResultBool BT_UDTO_DelBloodChargeItemLink(long longBloodChargeItemLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeItemLink.Remove(longBloodChargeItemLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemLink(BloodChargeItemLink entity)
        {
            IBBloodChargeItemLink.Entity = entity;
            EntityList<BloodChargeItemLink> tempEntityList = new EntityList<BloodChargeItemLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodChargeItemLink.Search();
                tempEntityList.count = IBBloodChargeItemLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeItemLink>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodChargeItemLink> tempEntityList = new EntityList<BloodChargeItemLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodChargeItemLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodChargeItemLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeItemLink>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodChargeItemLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodChargeItemLink>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodChargeItemType
        //Add  BloodChargeItemType
        public BaseResultDataValue BT_UDTO_AddBloodChargeItemType(BloodChargeItemType entity)
        {
            IBBloodChargeItemType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodChargeItemType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodChargeItemType.Get(IBBloodChargeItemType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodChargeItemType.Entity);
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
        //Update  BloodChargeItemType
        public BaseResultBool BT_UDTO_UpdateBloodChargeItemType(BloodChargeItemType entity)
        {
            IBBloodChargeItemType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeItemType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodChargeItemType
        public BaseResultBool BT_UDTO_UpdateBloodChargeItemTypeByField(BloodChargeItemType entity, string fields, long empID, string empName)
        {
            IBBloodChargeItemType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodChargeItemType serverEntity = IBBloodChargeItemType.Get(IBBloodChargeItemType.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodChargeItemType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodChargeItemType.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodChargeItemType.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodChargeItemType.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodChargeItemType
        public BaseResultBool BT_UDTO_DelBloodChargeItemType(long longBloodChargeItemTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeItemType.Remove(longBloodChargeItemTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemType(BloodChargeItemType entity)
        {
            IBBloodChargeItemType.Entity = entity;
            EntityList<BloodChargeItemType> tempEntityList = new EntityList<BloodChargeItemType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodChargeItemType.Search();
                tempEntityList.count = IBBloodChargeItemType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeItemType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodChargeItemType> tempEntityList = new EntityList<BloodChargeItemType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodChargeItemType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodChargeItemType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeItemType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodChargeItemType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodChargeItemType>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodChargeMoney
        //Add  BloodChargeMoney
        public BaseResultDataValue BT_UDTO_AddBloodChargeMoney(BloodChargeMoney entity)
        {
            IBBloodChargeMoney.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodChargeMoney.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodChargeMoney.Get(IBBloodChargeMoney.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodChargeMoney.Entity);
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
        //Update  BloodChargeMoney
        public BaseResultBool BT_UDTO_UpdateBloodChargeMoney(BloodChargeMoney entity)
        {
            IBBloodChargeMoney.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeMoney.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodChargeMoney
        public BaseResultBool BT_UDTO_UpdateBloodChargeMoneyByField(BloodChargeMoney entity, string fields)
        {
            IBBloodChargeMoney.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodChargeMoney.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodChargeMoney.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodChargeMoney.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodChargeMoney
        public BaseResultBool BT_UDTO_DelBloodChargeMoney(long longBloodChargeMoneyID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodChargeMoney.Remove(longBloodChargeMoneyID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodChargeMoney(BloodChargeMoney entity)
        {
            IBBloodChargeMoney.Entity = entity;
            EntityList<BloodChargeMoney> tempEntityList = new EntityList<BloodChargeMoney>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodChargeMoney.Search();
                tempEntityList.count = IBBloodChargeMoney.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeMoney>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeMoneyByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodChargeMoney> tempEntityList = new EntityList<BloodChargeMoney>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodChargeMoney.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodChargeMoney.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodChargeMoney>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodChargeMoneyById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodChargeMoney.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodChargeMoney>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodClass
        //Add  BloodClass
        public BaseResultDataValue BT_UDTO_AddBloodClass(BloodClass entity)
        {
            IBBloodClass.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodClass.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodClass.Get(IBBloodClass.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodClass.Entity);
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
        //Update  BloodClass
        public BaseResultBool BT_UDTO_UpdateBloodClass(BloodClass entity)
        {
            IBBloodClass.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodClass.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodClass
        public BaseResultBool BT_UDTO_UpdateBloodClassByField(BloodClass entity, string fields, long empID, string empName)
        {
            IBBloodClass.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodClass serverEntity = IBBloodClass.Get(IBBloodClass.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodClass.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodClass.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodClass.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodClass.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodClass
        public BaseResultBool BT_UDTO_DelBloodClass(long longBloodClassID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodClass.Remove(longBloodClassID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodClass(BloodClass entity)
        {
            IBBloodClass.Entity = entity;
            EntityList<BloodClass> tempEntityList = new EntityList<BloodClass>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodClass.Search();
                tempEntityList.count = IBBloodClass.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodClass>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodClassByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodClass> tempEntityList = new EntityList<BloodClass>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodClass.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodClass.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodClass>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodClassById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodClass.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodClass>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodInChecked
        //Add  BloodInChecked
        public BaseResultDataValue BT_UDTO_AddBloodInChecked(BloodInChecked entity)
        {
            IBBloodInChecked.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodInChecked.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodInChecked.Get(IBBloodInChecked.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodInChecked.Entity);
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
        //Update  BloodInChecked
        public BaseResultBool BT_UDTO_UpdateBloodInChecked(BloodInChecked entity)
        {
            IBBloodInChecked.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInChecked.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodInChecked
        public BaseResultBool BT_UDTO_UpdateBloodInCheckedByField(BloodInChecked entity, string fields)
        {
            IBBloodInChecked.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodInChecked.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodInChecked.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodInChecked.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodInChecked
        public BaseResultBool BT_UDTO_DelBloodInChecked(long longBloodInCheckedID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInChecked.Remove(longBloodInCheckedID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodInChecked(BloodInChecked entity)
        {
            IBBloodInChecked.Entity = entity;
            EntityList<BloodInChecked> tempEntityList = new EntityList<BloodInChecked>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodInChecked.Search();
                tempEntityList.count = IBBloodInChecked.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInChecked>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInCheckedByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodInChecked> tempEntityList = new EntityList<BloodInChecked>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodInChecked.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodInChecked.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInChecked>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInCheckedById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodInChecked.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodInChecked>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodInCheckedCurrent
        //Add  BloodInCheckedCurrent
        public BaseResultDataValue BT_UDTO_AddBloodInCheckedCurrent(BloodInCheckedCurrent entity)
        {
            IBBloodInCheckedCurrent.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodInCheckedCurrent.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodInCheckedCurrent.Get(IBBloodInCheckedCurrent.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodInCheckedCurrent.Entity);
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
        //Update  BloodInCheckedCurrent
        public BaseResultBool BT_UDTO_UpdateBloodInCheckedCurrent(BloodInCheckedCurrent entity)
        {
            IBBloodInCheckedCurrent.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInCheckedCurrent.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodInCheckedCurrent
        public BaseResultBool BT_UDTO_UpdateBloodInCheckedCurrentByField(BloodInCheckedCurrent entity, string fields)
        {
            IBBloodInCheckedCurrent.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodInCheckedCurrent.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodInCheckedCurrent.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodInCheckedCurrent.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodInCheckedCurrent
        public BaseResultBool BT_UDTO_DelBloodInCheckedCurrent(long longBloodInCheckedCurrentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInCheckedCurrent.Remove(longBloodInCheckedCurrentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodInCheckedCurrent(BloodInCheckedCurrent entity)
        {
            IBBloodInCheckedCurrent.Entity = entity;
            EntityList<BloodInCheckedCurrent> tempEntityList = new EntityList<BloodInCheckedCurrent>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodInCheckedCurrent.Search();
                tempEntityList.count = IBBloodInCheckedCurrent.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInCheckedCurrent>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInCheckedCurrentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodInCheckedCurrent> tempEntityList = new EntityList<BloodInCheckedCurrent>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodInCheckedCurrent.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodInCheckedCurrent.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInCheckedCurrent>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInCheckedCurrentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodInCheckedCurrent.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodInCheckedCurrent>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodInCheckedItem
        //Add  BloodInCheckedItem
        public BaseResultDataValue BT_UDTO_AddBloodInCheckedItem(BloodInCheckedItem entity)
        {
            IBBloodInCheckedItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodInCheckedItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodInCheckedItem.Get(IBBloodInCheckedItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodInCheckedItem.Entity);
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
        //Update  BloodInCheckedItem
        public BaseResultBool BT_UDTO_UpdateBloodInCheckedItem(BloodInCheckedItem entity)
        {
            IBBloodInCheckedItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInCheckedItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodInCheckedItem
        public BaseResultBool BT_UDTO_UpdateBloodInCheckedItemByField(BloodInCheckedItem entity, string fields)
        {
            IBBloodInCheckedItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodInCheckedItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodInCheckedItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodInCheckedItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodInCheckedItem
        public BaseResultBool BT_UDTO_DelBloodInCheckedItem(long longBloodInCheckedItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInCheckedItem.Remove(longBloodInCheckedItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodInCheckedItem(BloodInCheckedItem entity)
        {
            IBBloodInCheckedItem.Entity = entity;
            EntityList<BloodInCheckedItem> tempEntityList = new EntityList<BloodInCheckedItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodInCheckedItem.Search();
                tempEntityList.count = IBBloodInCheckedItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInCheckedItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInCheckedItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodInCheckedItem> tempEntityList = new EntityList<BloodInCheckedItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodInCheckedItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodInCheckedItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInCheckedItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInCheckedItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodInCheckedItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodInCheckedItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodInterfaceTransport
        //Add  BloodInterfaceTransport
        public BaseResultDataValue BT_UDTO_AddBloodInterfaceTransport(BloodInterfaceTransport entity)
        {
            IBBloodInterfaceTransport.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodInterfaceTransport.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodInterfaceTransport.Get(IBBloodInterfaceTransport.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodInterfaceTransport.Entity);
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
        //Update  BloodInterfaceTransport
        public BaseResultBool BT_UDTO_UpdateBloodInterfaceTransport(BloodInterfaceTransport entity)
        {
            IBBloodInterfaceTransport.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInterfaceTransport.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodInterfaceTransport
        public BaseResultBool BT_UDTO_UpdateBloodInterfaceTransportByField(BloodInterfaceTransport entity, string fields)
        {
            IBBloodInterfaceTransport.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodInterfaceTransport.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodInterfaceTransport.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodInterfaceTransport.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodInterfaceTransport
        public BaseResultBool BT_UDTO_DelBloodInterfaceTransport(long longBloodInterfaceTransportID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInterfaceTransport.Remove(longBloodInterfaceTransportID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodInterfaceTransport(BloodInterfaceTransport entity)
        {
            IBBloodInterfaceTransport.Entity = entity;
            EntityList<BloodInterfaceTransport> tempEntityList = new EntityList<BloodInterfaceTransport>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodInterfaceTransport.Search();
                tempEntityList.count = IBBloodInterfaceTransport.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInterfaceTransport>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInterfaceTransportByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodInterfaceTransport> tempEntityList = new EntityList<BloodInterfaceTransport>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodInterfaceTransport.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodInterfaceTransport.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInterfaceTransport>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInterfaceTransportById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodInterfaceTransport.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodInterfaceTransport>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodLargeUseItem
        //Add  BloodLargeUseItem
        public BaseResultDataValue BT_UDTO_AddBloodLargeUseItem(BloodLargeUseItem entity)
        {
            IBBloodLargeUseItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodLargeUseItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodLargeUseItem.Get(IBBloodLargeUseItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodLargeUseItem.Entity);
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
        //Update  BloodLargeUseItem
        public BaseResultBool BT_UDTO_UpdateBloodLargeUseItem(BloodLargeUseItem entity)
        {
            IBBloodLargeUseItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodLargeUseItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodLargeUseItem
        public BaseResultBool BT_UDTO_UpdateBloodLargeUseItemByField(BloodLargeUseItem entity, string fields)
        {
            IBBloodLargeUseItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodLargeUseItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodLargeUseItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodLargeUseItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodLargeUseItem
        public BaseResultBool BT_UDTO_DelBloodLargeUseItem(long longBloodLargeUseItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodLargeUseItem.Remove(longBloodLargeUseItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodLargeUseItem(BloodLargeUseItem entity)
        {
            IBBloodLargeUseItem.Entity = entity;
            EntityList<BloodLargeUseItem> tempEntityList = new EntityList<BloodLargeUseItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodLargeUseItem.Search();
                tempEntityList.count = IBBloodLargeUseItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodLargeUseItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodLargeUseItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodLargeUseItem> tempEntityList = new EntityList<BloodLargeUseItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodLargeUseItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodLargeUseItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodLargeUseItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodLargeUseItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodLargeUseItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodLargeUseItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodPatinfo
        //Add  BloodPatinfo
        public BaseResultDataValue BT_UDTO_AddBloodPatinfo(BloodPatinfo entity)
        {
            IBBloodPatinfo.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodPatinfo.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodPatinfo.Get(IBBloodPatinfo.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodPatinfo.Entity);
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
        //Update  BloodPatinfo
        public BaseResultBool BT_UDTO_UpdateBloodPatinfo(BloodPatinfo entity)
        {
            IBBloodPatinfo.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodPatinfo.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodPatinfo
        public BaseResultBool BT_UDTO_UpdateBloodPatinfoByField(BloodPatinfo entity, string fields)
        {
            IBBloodPatinfo.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodPatinfo.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodPatinfo.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodPatinfo.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodPatinfo
        public BaseResultBool BT_UDTO_DelBloodPatinfo(long longBloodPatinfoID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodPatinfo.Remove(longBloodPatinfoID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodPatinfo(BloodPatinfo entity)
        {
            IBBloodPatinfo.Entity = entity;
            EntityList<BloodPatinfo> tempEntityList = new EntityList<BloodPatinfo>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodPatinfo.Search();
                tempEntityList.count = IBBloodPatinfo.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodPatinfo>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodPatinfoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodPatinfo> tempEntityList = new EntityList<BloodPatinfo>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodPatinfo.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodPatinfo.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodPatinfo>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodPatinfoById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodPatinfo.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodPatinfo>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodQtyDtl
        //Add  BloodQtyDtl
        public BaseResultDataValue BT_UDTO_AddBloodQtyDtl(BloodQtyDtl entity)
        {
            IBBloodQtyDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodQtyDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodQtyDtl.Get(IBBloodQtyDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodQtyDtl.Entity);
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
        //Update  BloodQtyDtl
        public BaseResultBool BT_UDTO_UpdateBloodQtyDtl(BloodQtyDtl entity)
        {
            IBBloodQtyDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodQtyDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodQtyDtl
        public BaseResultBool BT_UDTO_UpdateBloodQtyDtlByField(BloodQtyDtl entity, string fields)
        {
            IBBloodQtyDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodQtyDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodQtyDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodQtyDtl.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodQtyDtl
        public BaseResultBool BT_UDTO_DelBloodQtyDtl(long longBloodQtyDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodQtyDtl.Remove(longBloodQtyDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodQtyDtl(BloodQtyDtl entity)
        {
            IBBloodQtyDtl.Entity = entity;
            EntityList<BloodQtyDtl> tempEntityList = new EntityList<BloodQtyDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodQtyDtl.Search();
                tempEntityList.count = IBBloodQtyDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodQtyDtl>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodQtyDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodQtyDtl> tempEntityList = new EntityList<BloodQtyDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodQtyDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodQtyDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodQtyDtl>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodQtyDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodQtyDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodQtyDtl>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodRecei
        //Add  BloodRecei
        public BaseResultDataValue BT_UDTO_AddBloodRecei(BloodRecei entity)
        {
            IBBloodRecei.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodRecei.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodRecei.Get(IBBloodRecei.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodRecei.Entity);
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
        //Update  BloodRecei
        public BaseResultBool BT_UDTO_UpdateBloodRecei(BloodRecei entity)
        {
            IBBloodRecei.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodRecei.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodRecei
        public BaseResultBool BT_UDTO_UpdateBloodReceiByField(BloodRecei entity, string fields)
        {
            IBBloodRecei.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodRecei.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodRecei.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodRecei.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodRecei
        public BaseResultBool BT_UDTO_DelBloodRecei(long longBloodReceiID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodRecei.Remove(longBloodReceiID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodRecei(BloodRecei entity)
        {
            IBBloodRecei.Entity = entity;
            EntityList<BloodRecei> tempEntityList = new EntityList<BloodRecei>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodRecei.Search();
                tempEntityList.count = IBBloodRecei.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodRecei>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodReceiByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodRecei> tempEntityList = new EntityList<BloodRecei>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodRecei.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodRecei.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodRecei>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodReceiById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodRecei.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodRecei>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodReceiItem
        //Add  BloodReceiItem
        public BaseResultDataValue BT_UDTO_AddBloodReceiItem(BloodReceiItem entity)
        {
            IBBloodReceiItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodReceiItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodReceiItem.Get(IBBloodReceiItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodReceiItem.Entity);
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
        //Update  BloodReceiItem
        public BaseResultBool BT_UDTO_UpdateBloodReceiItem(BloodReceiItem entity)
        {
            IBBloodReceiItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodReceiItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodReceiItem
        public BaseResultBool BT_UDTO_UpdateBloodReceiItemByField(BloodReceiItem entity, string fields)
        {
            IBBloodReceiItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodReceiItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodReceiItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodReceiItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodReceiItem
        public BaseResultBool BT_UDTO_DelBloodReceiItem(long longBloodReceiItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodReceiItem.Remove(longBloodReceiItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodReceiItem(BloodReceiItem entity)
        {
            IBBloodReceiItem.Entity = entity;
            EntityList<BloodReceiItem> tempEntityList = new EntityList<BloodReceiItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodReceiItem.Search();
                tempEntityList.count = IBBloodReceiItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodReceiItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodReceiItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodReceiItem> tempEntityList = new EntityList<BloodReceiItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodReceiItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodReceiItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodReceiItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodReceiItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodReceiItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodReceiItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodSelfBlood
        //Add  BloodSelfBlood
        public BaseResultDataValue BT_UDTO_AddBloodSelfBlood(BloodSelfBlood entity)
        {
            IBBloodSelfBlood.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodSelfBlood.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodSelfBlood.Get(IBBloodSelfBlood.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodSelfBlood.Entity);
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
        //Update  BloodSelfBlood
        public BaseResultBool BT_UDTO_UpdateBloodSelfBlood(BloodSelfBlood entity)
        {
            IBBloodSelfBlood.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodSelfBlood.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodSelfBlood
        public BaseResultBool BT_UDTO_UpdateBloodSelfBloodByField(BloodSelfBlood entity, string fields)
        {
            IBBloodSelfBlood.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodSelfBlood.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodSelfBlood.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodSelfBlood.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodSelfBlood
        public BaseResultBool BT_UDTO_DelBloodSelfBlood(long longBloodSelfBloodID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodSelfBlood.Remove(longBloodSelfBloodID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodSelfBlood(BloodSelfBlood entity)
        {
            IBBloodSelfBlood.Entity = entity;
            EntityList<BloodSelfBlood> tempEntityList = new EntityList<BloodSelfBlood>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodSelfBlood.Search();
                tempEntityList.count = IBBloodSelfBlood.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodSelfBlood>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodSelfBloodByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodSelfBlood> tempEntityList = new EntityList<BloodSelfBlood>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodSelfBlood.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodSelfBlood.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodSelfBlood>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodSelfBloodById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodSelfBlood.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodSelfBlood>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodSetQtyAlertColor
        //Add  BloodSetQtyAlertColor
        public BaseResultDataValue BT_UDTO_AddBloodSetQtyAlertColor(BloodSetQtyAlertColor entity)
        {
            IBBloodSetQtyAlertColor.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodSetQtyAlertColor.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodSetQtyAlertColor.Get(IBBloodSetQtyAlertColor.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodSetQtyAlertColor.Entity);
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
        //Update  BloodSetQtyAlertColor
        public BaseResultBool BT_UDTO_UpdateBloodSetQtyAlertColor(BloodSetQtyAlertColor entity)
        {
            IBBloodSetQtyAlertColor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodSetQtyAlertColor.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodSetQtyAlertColor
        public BaseResultBool BT_UDTO_UpdateBloodSetQtyAlertColorByField(BloodSetQtyAlertColor entity, string fields)
        {
            IBBloodSetQtyAlertColor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodSetQtyAlertColor.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodSetQtyAlertColor.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodSetQtyAlertColor.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodSetQtyAlertColor
        public BaseResultBool BT_UDTO_DelBloodSetQtyAlertColor(long longBloodSetQtyAlertColorID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodSetQtyAlertColor.Remove(longBloodSetQtyAlertColorID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertColor(BloodSetQtyAlertColor entity)
        {
            IBBloodSetQtyAlertColor.Entity = entity;
            EntityList<BloodSetQtyAlertColor> tempEntityList = new EntityList<BloodSetQtyAlertColor>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodSetQtyAlertColor.Search();
                tempEntityList.count = IBBloodSetQtyAlertColor.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodSetQtyAlertColor>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertColorByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodSetQtyAlertColor> tempEntityList = new EntityList<BloodSetQtyAlertColor>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodSetQtyAlertColor.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodSetQtyAlertColor.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodSetQtyAlertColor>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertColorById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodSetQtyAlertColor.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodSetQtyAlertColor>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodSetQtyAlertInfo
        //Add  BloodSetQtyAlertInfo
        public BaseResultDataValue BT_UDTO_AddBloodSetQtyAlertInfo(BloodSetQtyAlertInfo entity)
        {
            IBBloodSetQtyAlertInfo.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodSetQtyAlertInfo.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodSetQtyAlertInfo.Get(IBBloodSetQtyAlertInfo.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodSetQtyAlertInfo.Entity);
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
        //Update  BloodSetQtyAlertInfo
        public BaseResultBool BT_UDTO_UpdateBloodSetQtyAlertInfo(BloodSetQtyAlertInfo entity)
        {
            IBBloodSetQtyAlertInfo.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodSetQtyAlertInfo.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodSetQtyAlertInfo
        public BaseResultBool BT_UDTO_UpdateBloodSetQtyAlertInfoByField(BloodSetQtyAlertInfo entity, string fields)
        {
            IBBloodSetQtyAlertInfo.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodSetQtyAlertInfo.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodSetQtyAlertInfo.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodSetQtyAlertInfo.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodSetQtyAlertInfo
        public BaseResultBool BT_UDTO_DelBloodSetQtyAlertInfo(long longBloodSetQtyAlertInfoID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodSetQtyAlertInfo.Remove(longBloodSetQtyAlertInfoID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertInfo(BloodSetQtyAlertInfo entity)
        {
            IBBloodSetQtyAlertInfo.Entity = entity;
            EntityList<BloodSetQtyAlertInfo> tempEntityList = new EntityList<BloodSetQtyAlertInfo>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodSetQtyAlertInfo.Search();
                tempEntityList.count = IBBloodSetQtyAlertInfo.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodSetQtyAlertInfo>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertInfoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodSetQtyAlertInfo> tempEntityList = new EntityList<BloodSetQtyAlertInfo>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodSetQtyAlertInfo.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodSetQtyAlertInfo.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodSetQtyAlertInfo>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertInfoById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodSetQtyAlertInfo.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodSetQtyAlertInfo>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodStyle
        //Add  BloodStyle
        public BaseResultDataValue BT_UDTO_AddBloodStyle(BloodStyle entity)
        {
            IBBloodStyle.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodStyle.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodStyle.Get(IBBloodStyle.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodStyle.Entity);
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
        //Update  BloodStyle
        public BaseResultBool BT_UDTO_UpdateBloodStyle(BloodStyle entity)
        {
            IBBloodStyle.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodStyle.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodStyle
        public BaseResultBool BT_UDTO_UpdateBloodStyleByField(BloodStyle entity, string fields, long empID, string empName)
        {
            IBBloodStyle.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodStyle serverEntity = IBBloodStyle.Get(IBBloodStyle.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodStyle.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodStyle.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodStyle.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodStyle.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodStyle
        public BaseResultBool BT_UDTO_DelBloodStyle(long longBloodStyleID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodStyle.Remove(longBloodStyleID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodStyle(BloodStyle entity)
        {
            IBBloodStyle.Entity = entity;
            EntityList<BloodStyle> tempEntityList = new EntityList<BloodStyle>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodStyle.Search();
                tempEntityList.count = IBBloodStyle.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodStyle>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodStyleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodStyle> tempEntityList = new EntityList<BloodStyle>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodStyle.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodStyle.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodStyle>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodStyleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodStyle.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodStyle>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodTransForm
        //Add  BloodTransForm
        public BaseResultDataValue BT_UDTO_AddBloodTransForm(BloodTransForm entity)
        {
            IBBloodTransForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodTransForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodTransForm.Get(IBBloodTransForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodTransForm.Entity);
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
        //Update  BloodTransForm
        public BaseResultBool BT_UDTO_UpdateBloodTransForm(BloodTransForm entity)
        {
            IBBloodTransForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodTransForm
        public BaseResultBool BT_UDTO_UpdateBloodTransFormByField(BloodTransForm entity, string fields)
        {
            IBBloodTransForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodTransForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodTransForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodTransForm.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodTransForm
        public BaseResultBool BT_UDTO_DelBloodTransForm(long longBloodTransFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransForm.Remove(longBloodTransFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodTransForm(BloodTransForm entity)
        {
            IBBloodTransForm.Entity = entity;
            EntityList<BloodTransForm> tempEntityList = new EntityList<BloodTransForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodTransForm.Search();
                tempEntityList.count = IBBloodTransForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodTransForm> tempEntityList = new EntityList<BloodTransForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodTransForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodTransForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodTransForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodTransForm>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodTransItem
        //Add  BloodTransItem
        public BaseResultDataValue BT_UDTO_AddBloodTransItem(BloodTransItem entity)
        {
            IBBloodTransItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodTransItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodTransItem.Get(IBBloodTransItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodTransItem.Entity);
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
        //Update  BloodTransItem
        public BaseResultBool BT_UDTO_UpdateBloodTransItem(BloodTransItem entity)
        {
            IBBloodTransItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodTransItem
        public BaseResultBool BT_UDTO_UpdateBloodTransItemByField(BloodTransItem entity, string fields)
        {
            IBBloodTransItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodTransItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodTransItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodTransItem.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodTransItem
        public BaseResultBool BT_UDTO_DelBloodTransItem(long longBloodTransItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransItem.Remove(longBloodTransItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodTransItem(BloodTransItem entity)
        {
            IBBloodTransItem.Entity = entity;
            EntityList<BloodTransItem> tempEntityList = new EntityList<BloodTransItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodTransItem.Search();
                tempEntityList.count = IBBloodTransItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodTransItem> tempEntityList = new EntityList<BloodTransItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodTransItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodTransItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodTransItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodTransItem>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodUnit
        //Add  BloodUnit
        public BaseResultDataValue BT_UDTO_AddBloodUnit(BloodUnit entity)
        {
            IBBloodUnit.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodUnit.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodUnit.Get(IBBloodUnit.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodUnit.Entity);
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
        //Update  BloodUnit
        public BaseResultBool BT_UDTO_UpdateBloodUnit(BloodUnit entity)
        {
            IBBloodUnit.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodUnit.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodUnit
        public BaseResultBool BT_UDTO_UpdateBloodUnitByField(BloodUnit entity, string fields, long empID, string empName)
        {
            IBBloodUnit.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodUnit serverEntity = IBBloodUnit.Get(IBBloodUnit.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodUnit.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodUnit.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodUnit.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodUnit.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodUnit
        public BaseResultBool BT_UDTO_DelBloodUnit(long longBloodUnitID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodUnit.Remove(longBloodUnitID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodUnit(BloodUnit entity)
        {
            IBBloodUnit.Entity = entity;
            EntityList<BloodUnit> tempEntityList = new EntityList<BloodUnit>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodUnit.Search();
                tempEntityList.count = IBBloodUnit.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodUnit>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodUnit> tempEntityList = new EntityList<BloodUnit>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodUnit.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodUnit.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodUnit>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodUnitById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodUnit.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodUnit>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodUseDesc
        //Add  BloodUseDesc
        public BaseResultDataValue BT_UDTO_AddBloodUseDesc(BloodUseDesc entity)
        {
            IBBloodUseDesc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodUseDesc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodUseDesc.Get(IBBloodUseDesc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodUseDesc.Entity);
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
        //Update  BloodUseDesc
        public BaseResultBool BT_UDTO_UpdateBloodUseDesc(BloodUseDesc entity)
        {
            IBBloodUseDesc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodUseDesc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodUseDesc
        public BaseResultBool BT_UDTO_UpdateBloodUseDescByField(BloodUseDesc entity, string fields)
        {
            IBBloodUseDesc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodUseDesc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodUseDesc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodUseDesc.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodUseDesc
        public BaseResultBool BT_UDTO_DelBloodUseDesc(long longBloodUseDescID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodUseDesc.Remove(longBloodUseDescID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodUseDesc(BloodUseDesc entity)
        {
            IBBloodUseDesc.Entity = entity;
            EntityList<BloodUseDesc> tempEntityList = new EntityList<BloodUseDesc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodUseDesc.Search();
                tempEntityList.count = IBBloodUseDesc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodUseDesc>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodUseDescByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodUseDesc> tempEntityList = new EntityList<BloodUseDesc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodUseDesc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodUseDesc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodUseDesc>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodUseDescById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodUseDesc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodUseDesc>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region SCBagOperation
        //Add  SCBagOperation
        public BaseResultDataValue BT_UDTO_AddSCBagOperation(SCBagOperation entity)
        {
            IBSCBagOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCBagOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCBagOperation.Get(IBSCBagOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCBagOperation.Entity);
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
        //Update  SCBagOperation
        public BaseResultBool BT_UDTO_UpdateSCBagOperation(SCBagOperation entity)
        {
            IBSCBagOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCBagOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCBagOperation
        public BaseResultBool BT_UDTO_UpdateSCBagOperationByField(SCBagOperation entity, string fields)
        {
            IBSCBagOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCBagOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCBagOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCBagOperation.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  SCBagOperation
        public BaseResultBool BT_UDTO_DelSCBagOperation(long longSCBagOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCBagOperation.Remove(longSCBagOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchSCBagOperation(SCBagOperation entity)
        {
            IBSCBagOperation.Entity = entity;
            EntityList<SCBagOperation> tempEntityList = new EntityList<SCBagOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCBagOperation.Search();
                tempEntityList.count = IBSCBagOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCBagOperation>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchSCBagOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCBagOperation> tempEntityList = new EntityList<SCBagOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCBagOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCBagOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCBagOperation>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchSCBagOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCBagOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCBagOperation>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodBagChargeLink
        //Add  BloodBagChargeLink
        public BaseResultDataValue BT_UDTO_AddBloodBagChargeLink(BloodBagChargeLink entity)
        {
            IBBloodBagChargeLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagChargeLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagChargeLink.Get(IBBloodBagChargeLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagChargeLink.Entity);
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
        //Update  BloodBagChargeLink
        public BaseResultBool BT_UDTO_UpdateBloodBagChargeLink(BloodBagChargeLink entity)
        {
            IBBloodBagChargeLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagChargeLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagChargeLink
        public BaseResultBool BT_UDTO_UpdateBloodBagChargeLinkByField(BloodBagChargeLink entity, string fields, long empID, string empName)
        {
            IBBloodBagChargeLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodBagChargeLink serverEntity = IBBloodBagChargeLink.Get(IBBloodBagChargeLink.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagChargeLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagChargeLink.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodBagChargeLink.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagChargeLink.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBagChargeLink
        public BaseResultBool BT_UDTO_DelBloodBagChargeLink(long longBloodBagChargeLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagChargeLink.Remove(longBloodBagChargeLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagChargeLink(BloodBagChargeLink entity)
        {
            IBBloodBagChargeLink.Entity = entity;
            EntityList<BloodBagChargeLink> tempEntityList = new EntityList<BloodBagChargeLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagChargeLink.Search();
                tempEntityList.count = IBBloodBagChargeLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagChargeLink>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagChargeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagChargeLink> tempEntityList = new EntityList<BloodBagChargeLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagChargeLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagChargeLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagChargeLink>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagChargeLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagChargeLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagChargeLink>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion


        #region BloodClassUnitLink
        //Add  BloodClassUnitLink
        public BaseResultDataValue BT_UDTO_AddBloodClassUnitLink(BloodClassUnitLink entity)
        {
            IBBloodClassUnitLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodClassUnitLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodClassUnitLink.Get(IBBloodClassUnitLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodClassUnitLink.Entity);
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
        //Update  BloodClassUnitLink
        public BaseResultBool BT_UDTO_UpdateBloodClassUnitLink(BloodClassUnitLink entity)
        {
            IBBloodClassUnitLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodClassUnitLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodClassUnitLink
        public BaseResultBool BT_UDTO_UpdateBloodClassUnitLinkByField(BloodClassUnitLink entity, string fields, long empID, string empName)
        {
            IBBloodClassUnitLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodClassUnitLink serverEntity = IBBloodClassUnitLink.Get(IBBloodClassUnitLink.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodClassUnitLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodClassUnitLink.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodClassUnitLink.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodClassUnitLink.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodClassUnitLink
        public BaseResultBool BT_UDTO_DelBloodClassUnitLink(long longBloodClassUnitLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodClassUnitLink.Remove(longBloodClassUnitLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodClassUnitLink(BloodClassUnitLink entity)
        {
            IBBloodClassUnitLink.Entity = entity;
            EntityList<BloodClassUnitLink> tempEntityList = new EntityList<BloodClassUnitLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodClassUnitLink.Search();
                tempEntityList.count = IBBloodClassUnitLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodClassUnitLink>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodClassUnitLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodClassUnitLink> tempEntityList = new EntityList<BloodClassUnitLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodClassUnitLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodClassUnitLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodClassUnitLink>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodClassUnitLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodClassUnitLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodClassUnitLink>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodLisResult
        //Add  BloodLisResult
        public BaseResultDataValue BT_UDTO_AddBloodLisResult(BloodLisResult entity)
        {
            IBBloodLisResult.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodLisResult.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodLisResult.Get(IBBloodLisResult.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodLisResult.Entity);
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
        //Update  BloodLisResult
        public BaseResultBool BT_UDTO_UpdateBloodLisResult(BloodLisResult entity)
        {
            IBBloodLisResult.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodLisResult.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodLisResult
        public BaseResultBool BT_UDTO_UpdateBloodLisResultByField(BloodLisResult entity, string fields)
        {
            IBBloodLisResult.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodLisResult.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodLisResult.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodLisResult.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodLisResult
        public BaseResultBool BT_UDTO_DelBloodLisResult(long longBloodLisResultID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodLisResult.Remove(longBloodLisResultID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodLisResult(BloodLisResult entity)
        {
            IBBloodLisResult.Entity = entity;
            EntityList<BloodLisResult> tempEntityList = new EntityList<BloodLisResult>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodLisResult.Search();
                tempEntityList.count = IBBloodLisResult.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodLisResult>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodLisResultByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodLisResult> tempEntityList = new EntityList<BloodLisResult>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodLisResult.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodLisResult.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodLisResult>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodLisResultById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodLisResult.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodLisResult>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region BloodInterfaceMaping
        //Add  BloodInterfaceMaping
        public BaseResultDataValue BT_UDTO_AddBloodInterfaceMaping(BloodInterfaceMaping entity)
        {
            IBBloodInterfaceMaping.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodInterfaceMaping.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodInterfaceMaping.Entity);
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
        //Update  BloodInterfaceMaping
        public BaseResultBool BT_UDTO_UpdateBloodInterfaceMaping(BloodInterfaceMaping entity)
        {
            IBBloodInterfaceMaping.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInterfaceMaping.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodInterfaceMaping
        public BaseResultBool BT_UDTO_UpdateBloodInterfaceMapingByField(BloodInterfaceMaping entity, string fields, long empID, string empName)
        {
            IBBloodInterfaceMaping.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodInterfaceMaping.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodInterfaceMaping.Update(tempArray);
                        //BloodInterfaceMaping serverEntity = IBBloodInterfaceMaping.Get(IBBloodInterfaceMaping.Entity.Id);
                        if (tempBaseResultBool.success)
                        {
                            if (string.IsNullOrEmpty(empName))
                            {
                                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                                if (!string.IsNullOrEmpty(employeeID))
                                    empID = long.Parse(employeeID);
                                empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            }
                            string[] arrFields = fields.Split(',');
                            IBBloodInterfaceMaping.AddSCOperation(serverEntity, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodInterfaceMaping.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodInterfaceMaping
        public BaseResultBool BT_UDTO_DelBloodInterfaceMaping(long longBloodInterfaceMapingID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodInterfaceMaping.Remove(longBloodInterfaceMapingID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodInterfaceMaping(BloodInterfaceMaping entity)
        {
            IBBloodInterfaceMaping.Entity = entity;
            EntityList<BloodInterfaceMaping> tempEntityList = new EntityList<BloodInterfaceMaping>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodInterfaceMaping.Search();
                tempEntityList.count = IBBloodInterfaceMaping.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInterfaceMaping>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInterfaceMapingByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodInterfaceMaping> tempEntityList = new EntityList<BloodInterfaceMaping>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodInterfaceMaping.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodInterfaceMaping.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodInterfaceMaping>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodInterfaceMapingById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodInterfaceMaping.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodInterfaceMaping>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
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
        #endregion

        #region 业务接口对照
        public BaseResultDataValue BT_UDTO_SearchBDictMapingVOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string deveCode, string useCode, string mapingWhere, long objectTypeId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BDictMapingVO> tempEntityList = new EntityList<BDictMapingVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodInterfaceMaping.GetBDictMapingVOListByHQL(where, sort, page, limit, deveCode, useCode, mapingWhere, objectTypeId);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDictMapingVO>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBDictMapingVOByHQL.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 双列表选择左查询定制服务
        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemByChargeGItemHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BloodChargeItem> entityList = new EntityList<BloodChargeItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                entityList = IBBloodChargeGroupItem.SearchBloodChargeItemByChargeGItemHQL(page, limit, where, linkWhere, sort);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BloodChargeItem>(entityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodChargeItemByChargeGItemHQL.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodChargeItemByChargeItemLinkHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BloodChargeItem> entityList = new EntityList<BloodChargeItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                entityList = IBBloodChargeItemLink.SearchBloodChargeItemByChargeItemLinkHQL(page, limit, where, linkWhere, sort);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BloodChargeItem>(entityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodChargeItemByChargeGItemHQL.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodStyleBySetQtyAlertInfoHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BloodStyle> entityList = new EntityList<BloodStyle>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                entityList = IBBloodSetQtyAlertInfo.SearchBloodStyleBySetQtyAlertInfoHQL(page, limit, where, linkWhere, sort);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BloodStyle>(entityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodStyleBySetQtyAlertInfoHQL.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodUnitByClassUnitLinkHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BloodUnit> entityList = new EntityList<BloodUnit>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                entityList = IBBloodClassUnitLink.SearchBloodUnitByClassUnitLinkHQL(page, limit, where, linkWhere, sort);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BloodUnit>(entityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodUnitByClassUnitLinkHQL.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }

        #endregion
    }
}
