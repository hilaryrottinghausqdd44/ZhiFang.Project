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

namespace ZhiFang.BloodTransfusion
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“BloodTransfusionManageService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 BloodTransfusionManageService.svc 或 BloodTransfusionManageService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BloodTransfusionManageService : IBloodTransfusionManageService
    {
        #region IBLL
        IBBUserUIConfig IBBUserUIConfig { get; set; }
        IBDepartment IBDepartment { get; set; }
        IBDoctor IBDoctor { get; set; }
        IBPUser IBPUser { get; set; }
        IBBloodBReqEditItem IBBloodBReqEditItem { get; set; }
        IBBloodBReqForm IBBloodBReqForm { get; set; }
        IBBloodBReqItem IBBloodBReqItem { get; set; }
        IBBloodBReqItemResult IBBloodBReqItemResult { get; set; }
        IBBloodBReqType IBBloodBReqType { get; set; }
        IBBloodBReqTypeItem IBBloodBReqTypeItem { get; set; }
        IBBloodBTestItem IBBloodBTestItem { get; set; }
        IBBloodClass IBBloodClass { get; set; }
        IBBloodLargeUseForm IBBloodLargeUseForm { get; set; }
        IBBloodLargeUseItem IBBloodLargeUseItem { get; set; }
        IBBloodstyle IBBloodstyle { get; set; }
        IBBloodUnit IBBloodUnit { get; set; }
        IBBloodUseType IBBloodUseType { get; set; }
        IBLL.BloodTransfusion.IBNPUser IBNPUser { get; set; }
        IBBlooddocGrade IBBlooddocGrade { get; set; }
        IBBloodUseDesc IBBloodUseDesc { get; set; }
        IBVBloodLisResult IBVBloodLisResult { get; set; }
        IBBloodBReqFormResult IBBloodBReqFormResult { get; set; }
        IBWardType IBWardType { get; set; }
        IBLL.BloodTransfusion.IBBloodABO IBBloodABO { get; set; }
        IBLL.BloodTransfusion.IBBloodBagOperation IBBloodBagOperation { get; set; }
        IBLL.BloodTransfusion.IBBloodBagOperationDtl IBBloodBagOperationDtl { get; set; }
        IBLL.BloodTransfusion.IBBloodBOutForm IBBloodBOutForm { get; set; }
        IBLL.BloodTransfusion.IBBloodBOutItem IBBloodBOutItem { get; set; }
        IBLL.BloodTransfusion.IBBloodBPreForm IBBloodBPreForm { get; set; }
        IBLL.BloodTransfusion.IBBloodBPreItem IBBloodBPreItem { get; set; }
        IBLL.BloodTransfusion.IBBloodTransForm IBBloodTransForm { get; set; }
        IBLL.BloodTransfusion.IBBloodTransItem IBBloodTransItem { get; set; }
        IBLL.BloodTransfusion.IBBloodTransRecordType IBBloodTransRecordType { get; set; }
        IBLL.BloodTransfusion.IBBloodTransRecordTypeItem IBBloodTransRecordTypeItem { get; set; }
        IBLL.BloodTransfusion.IBBloodUsePlace IBBloodUsePlace { get; set; }
        IBLL.BloodTransfusion.IBBloodBagABOCheck IBBloodBagABOCheck { get; set; }
        IBLL.BloodTransfusion.IBBloodBagABOCheckLisItem IBBloodBagABOCheckLisItem { get; set; }
        IBLL.BloodTransfusion.IBBloodBagProcess IBBloodBagProcess { get; set; }
        IBLL.BloodTransfusion.IBBloodBagProcessType IBBloodBagProcessType { get; set; }
        IBLL.BloodTransfusion.IBBloodbagProcessTypeQry IBBloodbagProcessTypeQry { get; set; }
        IBLL.BloodTransfusion.IBBloodBInForm IBBloodBInForm { get; set; }
        IBLL.BloodTransfusion.IBBloodBInItem IBBloodBInItem { get; set; }
        IBLL.BloodTransfusion.IBBloodBInItemState IBBloodBInItemState { get; set; }
        IBLL.BloodTransfusion.IBBloodReason IBBloodReason { get; set; }
        IBLL.BloodTransfusion.IBBloodRecei IBBloodRecei { get; set; }
        IBLL.BloodTransfusion.IBBloodReceiItem IBBloodReceiItem { get; set; }
        IBLL.BloodTransfusion.IBBloodrefuse IBBloodrefuse { get; set; }
        IBLL.BloodTransfusion.IBBloodrefuseDispose IBBloodrefuseDispose { get; set; }
        IBBloodTransOperation IBBloodTransOperation { get; set; }
        IBDepartmentUser IBDepartmentUser { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IBBloodBagTracking IBBloodBagTracking { get; set; }
        IBBTemplate IBBTemplate { get; set; }

        #endregion

        #region 6.6登录处理
        /// <summary>
        /// HIS调用时,依传入HIS医生ID,获取到的医生信息
        /// </summary>
        /// <param name="strUserAccount"></param>
        /// <param name="strPassWord"></param>
        /// <returns></returns>
        public BaseResultDataValue BT_SYS_GetSysCurDoctorInfoByHisCode(string hisDeptCode, string hisDoctorCode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Debug("BT_SYS_GetSysCurDoctorInfoByHisCode.hisDoctorCode:" + hisDoctorCode + ",hisDeptCode:" + hisDeptCode);
            if (string.IsNullOrEmpty(hisDoctorCode))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：HIS医生编码不能为空!";
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
                tempBaseResultDataValue = IBDoctor.GetSysCurDoctorInfoByHisCode(hisDoctorCode, hisDeptCode, isAutoAddDepartmentUser);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_SYS_GetSysCurDoctorInfoByHisCode.Error.hisDoctorCode:" + hisDoctorCode + ",hisDeptCode:" + hisDeptCode);
                ZhiFang.Common.Log.Log.Error("BT_SYS_GetSysCurDoctorInfoByHisCode.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_SYS_GetSysCurDoctorInfoByHisCode.Error.StackTrace:" + ex.StackTrace);
            }
            // ZhiFang.Common.Log.Log.Debug("tempBaseResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_SYS_GetSysCurDoctorInfoByAccount(string account, string pwd)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //ZhiFang.Common.Log.Log.Debug("BT_SYS_GetSysCurDoctorInfoByAccount.account:" + account + ",pwd:" + pwd);
            if (string.IsNullOrEmpty(account))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：用户帐号信息不能为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                tempBaseResultDataValue = IBDoctor.GetSysCurDoctorInfoByAccount(account, pwd);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_SYS_GetSysCurDoctorInfoByAccount.Error.account:" + account);
                ZhiFang.Common.Log.Log.Error("BT_SYS_GetSysCurDoctorInfoByAccount.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_SYS_GetSysCurDoctorInfoByAccount.Error.StackTrace:" + ex.StackTrace);
            }
            // ZhiFang.Common.Log.Log.Debug("tempBaseResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
            return tempBaseResultDataValue;
        }
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
        public BaseResultDataValue BT_UDTO_SearchRBACUserOfNPUserByHQL(bool isliip, int page, int limit, string fields, string where, string sort, bool isPlanish)
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
                    tempEntityList = IBNPUser.SearchRBACUserOfNPUserByHQL(where, sort, page, limit);

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

        #region WardType

        public BaseResultDataValue BT_UDTO_SearchWardType(WardType entity)
        {
            IBWardType.Entity = entity;
            EntityList<WardType> tempEntityList = new EntityList<WardType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBWardType.Search();
                tempEntityList.count = IBWardType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<WardType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchWardTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<WardType> tempEntityList = new EntityList<WardType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBWardType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBWardType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<WardType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchWardTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBWardType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<WardType>(tempEntity);
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

        #region BloodBReqEditItem
        //Add  BloodBReqEditItem
        public BaseResultDataValue BT_UDTO_AddBloodBReqEditItem(BloodBReqEditItem entity)
        {
            IBBloodBReqEditItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBReqEditItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBReqEditItem.Get(IBBloodBReqEditItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBReqEditItem.Entity);
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
        //Update  BloodBReqEditItem
        public BaseResultBool BT_UDTO_UpdateBloodBReqEditItem(BloodBReqEditItem entity)
        {
            IBBloodBReqEditItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqEditItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBReqEditItem
        public BaseResultBool BT_UDTO_UpdateBloodBReqEditItemByField(BloodBReqEditItem entity, string fields)
        {
            IBBloodBReqEditItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBReqEditItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBReqEditItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBReqEditItem.Edit();
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
        //Delele  BloodBReqEditItem
        public BaseResultBool BT_UDTO_DelBloodBReqEditItem(string longBloodBReqEditItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqEditItem.Remove(longBloodBReqEditItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBReqEditItem(BloodBReqEditItem entity)
        {
            IBBloodBReqEditItem.Entity = entity;
            EntityList<BloodBReqEditItem> tempEntityList = new EntityList<BloodBReqEditItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBReqEditItem.Search();
                tempEntityList.count = IBBloodBReqEditItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqEditItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqEditItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqEditItem> tempEntityList = new EntityList<BloodBReqEditItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBReqEditItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBReqEditItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqEditItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqEditItemById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBReqEditItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBReqEditItem>(tempEntity);
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
                ZhiFang.Common.Log.Log.Error(ex.StackTrace.ToString());
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
                ZhiFang.Common.Log.Log.Error(ex.StackTrace.ToString());
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
        public BaseResultBool BT_UDTO_DelBloodBReqForm(string id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBBloodBReqForm.DeleteBloodBReqForm(id); //IBBloodBReqForm.Remove(longBloodBReqFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("删除医嘱申请单ID为:" + id + ",删除失败;错误信息为:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool BT_UDTO_UpdateBloodBReqFormPrintTotalById(string id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBBloodBReqForm.UpdateBloodBReqFormPrintTotalById(id);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("用血申请单ID：" + id + ",更新打印总数失败!错误信息为:" + ex.StackTrace);
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
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormByHQL.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                BloodBReqForm tempEntity = IBBloodBReqForm.Get(id);
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
            ZhiFang.Common.Log.Log.Debug("fields:" + fields);
            ZhiFang.Common.Log.Log.Debug("ResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
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
            if (string.IsNullOrEmpty(where))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询条件(where)为空";
                return tempBaseResultDataValue;
            }
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

        #region BloodBReqItemResult
        //Add  BloodBReqItemResult
        public BaseResultDataValue BT_UDTO_AddBloodBReqItemResult(BloodBReqItemResult entity)
        {
            IBBloodBReqItemResult.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBReqItemResult.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBReqItemResult.Get(IBBloodBReqItemResult.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBReqItemResult.Entity);
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
        //Update  BloodBReqItemResult
        public BaseResultBool BT_UDTO_UpdateBloodBReqItemResult(BloodBReqItemResult entity)
        {
            IBBloodBReqItemResult.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqItemResult.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBReqItemResult
        public BaseResultBool BT_UDTO_UpdateBloodBReqItemResultByField(BloodBReqItemResult entity, string fields)
        {
            IBBloodBReqItemResult.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBReqItemResult.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBReqItemResult.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBReqItemResult.Edit();
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
        //Delele  BloodBReqItemResult
        public BaseResultBool BT_UDTO_DelBloodBReqItemResult(long longBloodBReqItemResultID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqItemResult.Remove(longBloodBReqItemResultID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBReqItemResult(BloodBReqItemResult entity)
        {
            IBBloodBReqItemResult.Entity = entity;
            EntityList<BloodBReqItemResult> tempEntityList = new EntityList<BloodBReqItemResult>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBReqItemResult.Search();
                tempEntityList.count = IBBloodBReqItemResult.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqItemResult>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqItemResultByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqItemResult> tempEntityList = new EntityList<BloodBReqItemResult>();
            if (string.IsNullOrEmpty(where))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询条件(where)为空";
                return tempBaseResultDataValue;
            }
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBReqItemResult.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBReqItemResult.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqItemResult>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqItemResultById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBReqItemResult.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBReqItemResult>(tempEntity);
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
            if (string.IsNullOrEmpty(where))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询条件(where)为空";
                return tempBaseResultDataValue;
            }
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

        #region BloodBReqType
        //Add  BloodBReqType
        public BaseResultDataValue BT_UDTO_AddBloodBReqType(BloodBReqType entity)
        {
            IBBloodBReqType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBReqType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBReqType.Get(IBBloodBReqType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBReqType.Entity);
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
        //Update  BloodBReqType
        public BaseResultBool BT_UDTO_UpdateBloodBReqType(BloodBReqType entity)
        {
            IBBloodBReqType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBReqType
        public BaseResultBool BT_UDTO_UpdateBloodBReqTypeByField(BloodBReqType entity, string fields)
        {
            IBBloodBReqType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBReqType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBReqType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBReqType.Edit();
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
        //Delele  BloodBReqType
        public BaseResultBool BT_UDTO_DelBloodBReqType(int longBloodBReqTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqType.Remove(longBloodBReqTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBReqType(BloodBReqType entity)
        {
            IBBloodBReqType.Entity = entity;
            EntityList<BloodBReqType> tempEntityList = new EntityList<BloodBReqType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBReqType.Search();
                tempEntityList.count = IBBloodBReqType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqType> tempEntityList = new EntityList<BloodBReqType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBReqType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBReqType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqTypeById(int id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBReqType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBReqType>(tempEntity);
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

        #region BloodBReqTypeItem
        //Add  BloodBReqTypeItem
        public BaseResultDataValue BT_UDTO_AddBloodBReqTypeItem(BloodBReqTypeItem entity)
        {
            IBBloodBReqTypeItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBReqTypeItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBReqTypeItem.Get(IBBloodBReqTypeItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBReqTypeItem.Entity);
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
        //Update  BloodBReqTypeItem
        public BaseResultBool BT_UDTO_UpdateBloodBReqTypeItem(BloodBReqTypeItem entity)
        {
            IBBloodBReqTypeItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqTypeItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBReqTypeItem
        public BaseResultBool BT_UDTO_UpdateBloodBReqTypeItemByField(BloodBReqTypeItem entity, string fields)
        {
            IBBloodBReqTypeItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBReqTypeItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBReqTypeItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBReqTypeItem.Edit();
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
        //Delele  BloodBReqTypeItem
        public BaseResultBool BT_UDTO_DelBloodBReqTypeItem(string longBloodBReqTypeItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBReqTypeItem.Remove(longBloodBReqTypeItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBReqTypeItem(BloodBReqTypeItem entity)
        {
            IBBloodBReqTypeItem.Entity = entity;
            EntityList<BloodBReqTypeItem> tempEntityList = new EntityList<BloodBReqTypeItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBReqTypeItem.Search();
                tempEntityList.count = IBBloodBReqTypeItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqTypeItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqTypeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqTypeItem> tempEntityList = new EntityList<BloodBReqTypeItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBReqTypeItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBReqTypeItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqTypeItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBReqTypeItemById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBReqTypeItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBReqTypeItem>(tempEntity);
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
        public BaseResultBool BT_UDTO_UpdateBloodBTestItemByField(BloodBTestItem entity, string fields)
        {
            IBBloodBTestItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBTestItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBTestItem.Update(tempArray);
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
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBTestItem
        public BaseResultBool BT_UDTO_DelBloodBTestItem(int longBloodBTestItemID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodBTestItemById(int id, string fields, bool isPlanish)
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
        public BaseResultBool BT_UDTO_UpdateBloodClassByField(BloodClass entity, string fields)
        {
            IBBloodClass.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodClass.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodClass.Update(tempArray);
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
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodClass
        public BaseResultBool BT_UDTO_DelBloodClass(string longBloodClassID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodClassById(string id, string fields, bool isPlanish)
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

        #region BloodLargeUseForm
        //Add  BloodLargeUseForm
        public BaseResultDataValue BT_UDTO_AddBloodLargeUseForm(BloodLargeUseForm entity)
        {
            IBBloodLargeUseForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodLargeUseForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodLargeUseForm.Get(IBBloodLargeUseForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodLargeUseForm.Entity);
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
        //Update  BloodLargeUseForm
        public BaseResultBool BT_UDTO_UpdateBloodLargeUseForm(BloodLargeUseForm entity)
        {
            IBBloodLargeUseForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodLargeUseForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodLargeUseForm
        public BaseResultBool BT_UDTO_UpdateBloodLargeUseFormByField(BloodLargeUseForm entity, string fields)
        {
            IBBloodLargeUseForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodLargeUseForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodLargeUseForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodLargeUseForm.Edit();
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
        //Delele  BloodLargeUseForm
        public BaseResultBool BT_UDTO_DelBloodLargeUseForm(string longBloodLargeUseFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodLargeUseForm.Remove(longBloodLargeUseFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodLargeUseForm(BloodLargeUseForm entity)
        {
            IBBloodLargeUseForm.Entity = entity;
            EntityList<BloodLargeUseForm> tempEntityList = new EntityList<BloodLargeUseForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodLargeUseForm.Search();
                tempEntityList.count = IBBloodLargeUseForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodLargeUseForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodLargeUseFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodLargeUseForm> tempEntityList = new EntityList<BloodLargeUseForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodLargeUseForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodLargeUseForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodLargeUseForm>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodLargeUseFormById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodLargeUseForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodLargeUseForm>(tempEntity);
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

        #region Bloodstyle
        //Add  Bloodstyle
        public BaseResultDataValue BT_UDTO_AddBloodstyle(Bloodstyle entity)
        {
            IBBloodstyle.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodstyle.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodstyle.Get(IBBloodstyle.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodstyle.Entity);
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
        //Update  Bloodstyle
        public BaseResultBool BT_UDTO_UpdateBloodstyle(Bloodstyle entity)
        {
            IBBloodstyle.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodstyle.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  Bloodstyle
        public BaseResultBool BT_UDTO_UpdateBloodstyleByField(Bloodstyle entity, string fields)
        {
            IBBloodstyle.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodstyle.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodstyle.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodstyle.Edit();
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
        //Delele  Bloodstyle
        public BaseResultBool BT_UDTO_DelBloodstyle(int longBloodstyleID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodstyle.Remove(longBloodstyleID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodstyle(Bloodstyle entity)
        {
            IBBloodstyle.Entity = entity;
            EntityList<Bloodstyle> tempEntityList = new EntityList<Bloodstyle>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodstyle.Search();
                tempEntityList.count = IBBloodstyle.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Bloodstyle>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodstyleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<Bloodstyle> tempEntityList = new EntityList<Bloodstyle>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodstyle.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodstyle.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Bloodstyle>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodstyleById(int id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodstyle.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<Bloodstyle>(tempEntity);
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
        public BaseResultBool BT_UDTO_UpdateBloodUnitByField(BloodUnit entity, string fields)
        {
            IBBloodUnit.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodUnit.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodUnit.Update(tempArray);
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
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodUnit
        public BaseResultBool BT_UDTO_DelBloodUnit(int longBloodUnitID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodUnitById(int id, string fields, bool isPlanish)
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

        #region BloodUseType
        //Add  BloodUseType
        public BaseResultDataValue BT_UDTO_AddBloodUseType(BloodUseType entity)
        {
            IBBloodUseType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodUseType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodUseType.Get(IBBloodUseType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodUseType.Entity);
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
        //Update  BloodUseType
        public BaseResultBool BT_UDTO_UpdateBloodUseType(BloodUseType entity)
        {
            IBBloodUseType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodUseType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodUseType
        public BaseResultBool BT_UDTO_UpdateBloodUseTypeByField(BloodUseType entity, string fields)
        {
            IBBloodUseType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodUseType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodUseType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodUseType.Edit();
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
        //Delele  BloodUseType
        public BaseResultBool BT_UDTO_DelBloodUseType(string longBloodUseTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodUseType.Remove(longBloodUseTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodUseType(BloodUseType entity)
        {
            IBBloodUseType.Entity = entity;
            EntityList<BloodUseType> tempEntityList = new EntityList<BloodUseType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodUseType.Search();
                tempEntityList.count = IBBloodUseType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodUseType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodUseTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodUseType> tempEntityList = new EntityList<BloodUseType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodUseType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodUseType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodUseType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodUseTypeById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodUseType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodUseType>(tempEntity);
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
                tempBaseResultBool = IBBloodBReqForm.AddWarpAndDept();
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

        #region Doctor
        //Add  Doctor
        public BaseResultDataValue BT_UDTO_AddDoctor(Doctor entity)
        {
            IBDoctor.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBDoctor.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBDoctor.Get(IBDoctor.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBDoctor.Entity);
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
        //Update  Doctor
        public BaseResultBool BT_UDTO_UpdateDoctor(Doctor entity)
        {
            IBDoctor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDoctor.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  Doctor
        public BaseResultBool BT_UDTO_UpdateDoctorByField(Doctor entity, string fields, long empID, string empName)
        {
            IBDoctor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBDoctor.Entity, fields);
                    if (tempArray != null)
                    {
                        Doctor serverDoctor = IBDoctor.Get(IBDoctor.Entity.Id);
                        tempBaseResultBool.success = IBDoctor.Update(tempArray);

                        if (tempBaseResultBool.success)
                        {
                            string[] arrFields = fields.Split(',');
                            IBDoctor.AddSCOperation(serverDoctor, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBDoctor.Edit();
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
        //Delele  Doctor
        public BaseResultBool BT_UDTO_DelDoctor(int longDoctorID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDoctor.Remove(longDoctorID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchDoctor(Doctor entity)
        {
            IBDoctor.Entity = entity;
            EntityList<Doctor> tempEntityList = new EntityList<Doctor>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBDoctor.Search();
                tempEntityList.count = IBDoctor.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Doctor>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchDoctorByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<Doctor> tempEntityList = new EntityList<Doctor>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBDoctor.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBDoctor.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Doctor>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchDoctorById(int id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBDoctor.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<Doctor>(tempEntity);
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
                //throw new Exception(ex.Message);
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
                var tempEntity = IBPUser.Get(int.Parse(id.ToString()));
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

        #endregion

        #region NPUser
        //Add  NPUser
        public BaseResultDataValue BT_UDTO_AddNPUser(NPUser entity)
        {
            IBNPUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBNPUser.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBNPUser.Get(IBNPUser.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBNPUser.Entity);
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
        //Update  NPUser
        public BaseResultBool BT_UDTO_UpdateNPUser(NPUser entity)
        {
            IBNPUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBNPUser.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  NPUser
        public BaseResultBool BT_UDTO_UpdateNPUserByField(NPUser entity, string fields)
        {
            IBNPUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBNPUser.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBNPUser.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBNPUser.Edit();
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
        //Delele  NPUser
        public BaseResultBool BT_UDTO_DelNPUser(int longNPUserID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBNPUser.Remove(longNPUserID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchNPUser(NPUser entity)
        {
            IBNPUser.Entity = entity;
            EntityList<NPUser> tempEntityList = new EntityList<NPUser>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBNPUser.Search();
                tempEntityList.count = IBNPUser.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<NPUser>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchNPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<NPUser> tempEntityList = new EntityList<NPUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBNPUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBNPUser.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<NPUser>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchNPUserById(int id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBNPUser.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<NPUser>(tempEntity);
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

        #region BlooddocGrade
        //Add  BlooddocGrade
        public BaseResultDataValue BT_UDTO_AddBlooddocGrade(BlooddocGrade entity)
        {
            IBBlooddocGrade.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBlooddocGrade.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBlooddocGrade.Get(IBBlooddocGrade.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBlooddocGrade.Entity);
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
        //Update  BlooddocGrade
        public BaseResultBool BT_UDTO_UpdateBlooddocGrade(BlooddocGrade entity)
        {
            IBBlooddocGrade.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBlooddocGrade.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BlooddocGrade
        public BaseResultBool BT_UDTO_UpdateBlooddocGradeByField(BlooddocGrade entity, string fields)
        {
            IBBlooddocGrade.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBlooddocGrade.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBlooddocGrade.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBlooddocGrade.Edit();
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
        //Delele  BlooddocGrade
        public BaseResultBool BT_UDTO_DelBlooddocGrade(string longBlooddocGradeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBlooddocGrade.Remove(longBlooddocGradeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBlooddocGrade(BlooddocGrade entity)
        {
            IBBlooddocGrade.Entity = entity;
            EntityList<BlooddocGrade> tempEntityList = new EntityList<BlooddocGrade>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBlooddocGrade.Search();
                tempEntityList.count = IBBlooddocGrade.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BlooddocGrade>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBlooddocGradeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BlooddocGrade> tempEntityList = new EntityList<BlooddocGrade>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBlooddocGrade.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBlooddocGrade.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BlooddocGrade>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBlooddocGradeById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBlooddocGrade.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BlooddocGrade>(tempEntity);
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
        public BaseResultBool BT_UDTO_DelBloodUseDesc(int longBloodUseDescID)
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodUseDescById.查询错误：" + ex.StackTrace);
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
        public BaseResultBool BT_UDTO_UpdateBloodABOByField(BloodABO entity, string fields)
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
                        tempBaseResultBool.success = IBBloodABO.Update(tempArray);
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
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodABO
        public BaseResultBool BT_UDTO_DelBloodABO(string longBloodABOID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodABOById(string id, string fields, bool isPlanish)
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

        #region BloodBagOperation
        //Add  BloodBagOperation
        public BaseResultDataValue BT_UDTO_AddBloodBagOperation(BloodBagOperation entity)
        {
            IBBloodBagOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagOperation.Get(IBBloodBagOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagOperation.Entity);
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
        //Update  BloodBagOperation
        public BaseResultBool BT_UDTO_UpdateBloodBagOperation(BloodBagOperation entity)
        {
            IBBloodBagOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagOperation
        public BaseResultBool BT_UDTO_UpdateBloodBagOperationByField(BloodBagOperation entity, string fields)
        {
            IBBloodBagOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagOperation.Edit();
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
        //Delele  BloodBagOperation
        public BaseResultBool BT_UDTO_DelBloodBagOperation(long longBloodBagOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagOperation.Remove(longBloodBagOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagOperation(BloodBagOperation entity)
        {
            IBBloodBagOperation.Entity = entity;
            EntityList<BloodBagOperation> tempEntityList = new EntityList<BloodBagOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagOperation.Search();
                tempEntityList.count = IBBloodBagOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagOperation>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagOperation> tempEntityList = new EntityList<BloodBagOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagOperation>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagOperation>(tempEntity);
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

        #region BloodBagOperationDtl
        //Add  BloodBagOperationDtl
        public BaseResultDataValue BT_UDTO_AddBloodBagOperationDtl(BloodBagOperationDtl entity)
        {
            IBBloodBagOperationDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagOperationDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagOperationDtl.Get(IBBloodBagOperationDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagOperationDtl.Entity);
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
        //Update  BloodBagOperationDtl
        public BaseResultBool BT_UDTO_UpdateBloodBagOperationDtl(BloodBagOperationDtl entity)
        {
            IBBloodBagOperationDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagOperationDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagOperationDtl
        public BaseResultBool BT_UDTO_UpdateBloodBagOperationDtlByField(BloodBagOperationDtl entity, string fields)
        {
            IBBloodBagOperationDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagOperationDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagOperationDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagOperationDtl.Edit();
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
        //Delele  BloodBagOperationDtl
        public BaseResultBool BT_UDTO_DelBloodBagOperationDtl(long longBloodBagOperationDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagOperationDtl.Remove(longBloodBagOperationDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagOperationDtl(BloodBagOperationDtl entity)
        {
            IBBloodBagOperationDtl.Entity = entity;
            EntityList<BloodBagOperationDtl> tempEntityList = new EntityList<BloodBagOperationDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagOperationDtl.Search();
                tempEntityList.count = IBBloodBagOperationDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagOperationDtl>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagOperationDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagOperationDtl> tempEntityList = new EntityList<BloodBagOperationDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagOperationDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagOperationDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagOperationDtl>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagOperationDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagOperationDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagOperationDtl>(tempEntity);
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
        public BaseResultBool BT_UDTO_DelBloodBOutForm(string longBloodBOutFormID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodBOutFormById(string id, string fields, bool isPlanish)
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
        public BaseResultBool BT_UDTO_DelBloodBOutItem(string longBloodBOutItemID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemById(string id, string fields, bool isPlanish)
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
        public BaseResultBool BT_UDTO_DelBloodBPreForm(string longBloodBPreFormID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodBPreFormById(string id, string fields, bool isPlanish)
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
        public BaseResultBool BT_UDTO_DelBloodBPreItem(string longBloodBPreItemID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodBPreItemById(string id, string fields, bool isPlanish)
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

        #region BloodTransOperation
        //Add  BloodTransOperation
        public BaseResultDataValue BT_UDTO_AddBloodTransOperation(BloodTransOperation entity)
        {
            IBBloodTransOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodTransOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodTransOperation.Get(IBBloodTransOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodTransOperation.Entity);
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
        //Update  BloodTransOperation
        public BaseResultBool BT_UDTO_UpdateBloodTransOperation(BloodTransOperation entity)
        {
            IBBloodTransOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodTransOperation
        public BaseResultBool BT_UDTO_UpdateBloodTransOperationByField(BloodTransOperation entity, string fields)
        {
            IBBloodTransOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodTransOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodTransOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodTransOperation.Edit();
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
        //Delele  BloodTransOperation
        public BaseResultBool BT_UDTO_DelBloodTransOperation(long longBloodTransOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransOperation.Remove(longBloodTransOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodTransOperation(BloodTransOperation entity)
        {
            IBBloodTransOperation.Entity = entity;
            EntityList<BloodTransOperation> tempEntityList = new EntityList<BloodTransOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodTransOperation.Search();
                tempEntityList.count = IBBloodTransOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransOperation>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodTransOperation> tempEntityList = new EntityList<BloodTransOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodTransOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodTransOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransOperation>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodTransOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodTransOperation>(tempEntity);
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

        #region BloodTransRecordType
        //Add  BloodTransRecordType
        public BaseResultDataValue BT_UDTO_AddBloodTransRecordType(BloodTransRecordType entity)
        {
            IBBloodTransRecordType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodTransRecordType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodTransRecordType.Get(IBBloodTransRecordType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodTransRecordType.Entity);
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
        //Update  BloodTransRecordType
        public BaseResultBool BT_UDTO_UpdateBloodTransRecordType(BloodTransRecordType entity)
        {
            IBBloodTransRecordType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransRecordType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodTransRecordType
        public BaseResultBool BT_UDTO_UpdateBloodTransRecordTypeByField(BloodTransRecordType entity, string fields)
        {
            IBBloodTransRecordType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodTransRecordType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodTransRecordType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodTransRecordType.Edit();
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
        //Delele  BloodTransRecordType
        public BaseResultBool BT_UDTO_DelBloodTransRecordType(long longBloodTransRecordTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransRecordType.Remove(longBloodTransRecordTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodTransRecordType(BloodTransRecordType entity)
        {
            IBBloodTransRecordType.Entity = entity;
            EntityList<BloodTransRecordType> tempEntityList = new EntityList<BloodTransRecordType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodTransRecordType.Search();
                tempEntityList.count = IBBloodTransRecordType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransRecordType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodTransRecordType> tempEntityList = new EntityList<BloodTransRecordType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodTransRecordType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodTransRecordType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransRecordType>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodTransRecordType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodTransRecordType>(tempEntity);
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

        #region BloodTransRecordTypeItem
        //Add  BloodTransRecordTypeItem
        public BaseResultDataValue BT_UDTO_AddBloodTransRecordTypeItem(BloodTransRecordTypeItem entity)
        {
            IBBloodTransRecordTypeItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodTransRecordTypeItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodTransRecordTypeItem.Get(IBBloodTransRecordTypeItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodTransRecordTypeItem.Entity);
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
        //Update  BloodTransRecordTypeItem
        public BaseResultBool BT_UDTO_UpdateBloodTransRecordTypeItem(BloodTransRecordTypeItem entity)
        {
            IBBloodTransRecordTypeItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransRecordTypeItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodTransRecordTypeItem
        public BaseResultBool BT_UDTO_UpdateBloodTransRecordTypeItemByField(BloodTransRecordTypeItem entity, string fields)
        {
            IBBloodTransRecordTypeItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodTransRecordTypeItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodTransRecordTypeItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodTransRecordTypeItem.Edit();
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
        //Delele  BloodTransRecordTypeItem
        public BaseResultBool BT_UDTO_DelBloodTransRecordTypeItem(long longBloodTransRecordTypeItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodTransRecordTypeItem.Remove(longBloodTransRecordTypeItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeItem(BloodTransRecordTypeItem entity)
        {
            IBBloodTransRecordTypeItem.Entity = entity;
            EntityList<BloodTransRecordTypeItem> tempEntityList = new EntityList<BloodTransRecordTypeItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodTransRecordTypeItem.Search();
                tempEntityList.count = IBBloodTransRecordTypeItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransRecordTypeItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodTransRecordTypeItem> tempEntityList = new EntityList<BloodTransRecordTypeItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodTransRecordTypeItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                    //ZhiFang.Common.Log.Log.Debug("sort"+ CommonServiceMethod.GetSortHQL(sort));
                }
                else
                {
                    tempEntityList = IBBloodTransRecordTypeItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodTransRecordTypeItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodTransRecordTypeItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodTransRecordTypeItem>(tempEntity);
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

        #region BloodUsePlace
        //Add  BloodUsePlace
        public BaseResultDataValue BT_UDTO_AddBloodUsePlace(BloodUsePlace entity)
        {
            IBBloodUsePlace.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodUsePlace.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodUsePlace.Get(IBBloodUsePlace.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodUsePlace.Entity);
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
        //Update  BloodUsePlace
        public BaseResultBool BT_UDTO_UpdateBloodUsePlace(BloodUsePlace entity)
        {
            IBBloodUsePlace.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodUsePlace.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodUsePlace
        public BaseResultBool BT_UDTO_UpdateBloodUsePlaceByField(BloodUsePlace entity, string fields)
        {
            IBBloodUsePlace.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodUsePlace.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodUsePlace.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodUsePlace.Edit();
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
        //Delele  BloodUsePlace
        public BaseResultBool BT_UDTO_DelBloodUsePlace(string longBloodUsePlaceID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodUsePlace.Remove(longBloodUsePlaceID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodUsePlace(BloodUsePlace entity)
        {
            IBBloodUsePlace.Entity = entity;
            EntityList<BloodUsePlace> tempEntityList = new EntityList<BloodUsePlace>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodUsePlace.Search();
                tempEntityList.count = IBBloodUsePlace.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodUsePlace>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodUsePlaceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodUsePlace> tempEntityList = new EntityList<BloodUsePlace>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodUsePlace.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodUsePlace.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodUsePlace>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodUsePlaceById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodUsePlace.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodUsePlace>(tempEntity);
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

        #region BloodBagABOCheck
        //Add  BloodBagABOCheck
        public BaseResultDataValue BT_UDTO_AddBloodBagABOCheck(BloodBagABOCheck entity)
        {
            IBBloodBagABOCheck.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagABOCheck.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagABOCheck.Get(IBBloodBagABOCheck.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagABOCheck.Entity);
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
        //Update  BloodBagABOCheck
        public BaseResultBool BT_UDTO_UpdateBloodBagABOCheck(BloodBagABOCheck entity)
        {
            IBBloodBagABOCheck.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagABOCheck.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagABOCheck
        public BaseResultBool BT_UDTO_UpdateBloodBagABOCheckByField(BloodBagABOCheck entity, string fields)
        {
            IBBloodBagABOCheck.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagABOCheck.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagABOCheck.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagABOCheck.Edit();
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
        //Delele  BloodBagABOCheck
        public BaseResultBool BT_UDTO_DelBloodBagABOCheck(string longBloodBagABOCheckID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagABOCheck.Remove(longBloodBagABOCheckID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagABOCheck(BloodBagABOCheck entity)
        {
            IBBloodBagABOCheck.Entity = entity;
            EntityList<BloodBagABOCheck> tempEntityList = new EntityList<BloodBagABOCheck>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagABOCheck.Search();
                tempEntityList.count = IBBloodBagABOCheck.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagABOCheck>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagABOCheck> tempEntityList = new EntityList<BloodBagABOCheck>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagABOCheck.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagABOCheck.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagABOCheck>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagABOCheck.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagABOCheck>(tempEntity);
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

        #region BloodBagABOCheckLisItem
        //Add  BloodBagABOCheckLisItem
        public BaseResultDataValue BT_UDTO_AddBloodBagABOCheckLisItem(BloodBagABOCheckLisItem entity)
        {
            IBBloodBagABOCheckLisItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBagABOCheckLisItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBagABOCheckLisItem.Get(IBBloodBagABOCheckLisItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBagABOCheckLisItem.Entity);
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
        //Update  BloodBagABOCheckLisItem
        public BaseResultBool BT_UDTO_UpdateBloodBagABOCheckLisItem(BloodBagABOCheckLisItem entity)
        {
            IBBloodBagABOCheckLisItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagABOCheckLisItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBagABOCheckLisItem
        public BaseResultBool BT_UDTO_UpdateBloodBagABOCheckLisItemByField(BloodBagABOCheckLisItem entity, string fields)
        {
            IBBloodBagABOCheckLisItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagABOCheckLisItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagABOCheckLisItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBagABOCheckLisItem.Edit();
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
        //Delele  BloodBagABOCheckLisItem
        public BaseResultBool BT_UDTO_DelBloodBagABOCheckLisItem(long longBloodBagABOCheckLisItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBagABOCheckLisItem.Remove(longBloodBagABOCheckLisItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckLisItem(BloodBagABOCheckLisItem entity)
        {
            IBBloodBagABOCheckLisItem.Entity = entity;
            EntityList<BloodBagABOCheckLisItem> tempEntityList = new EntityList<BloodBagABOCheckLisItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBagABOCheckLisItem.Search();
                tempEntityList.count = IBBloodBagABOCheckLisItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagABOCheckLisItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckLisItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagABOCheckLisItem> tempEntityList = new EntityList<BloodBagABOCheckLisItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBagABOCheckLisItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBagABOCheckLisItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagABOCheckLisItem>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckLisItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBagABOCheckLisItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagABOCheckLisItem>(tempEntity);
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
        public BaseResultBool BT_UDTO_UpdateBloodBagProcessTypeByField(BloodBagProcessType entity, string fields)
        {
            IBBloodBagProcessType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBagProcessType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBagProcessType.Update(tempArray);
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
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BloodBagProcessType
        public BaseResultBool BT_UDTO_DelBloodBagProcessType(string longBloodBagProcessTypeID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeById(string id, string fields, bool isPlanish)
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

        #region BloodbagProcessTypeQry
        //Add  BloodbagProcessTypeQry
        public BaseResultDataValue BT_UDTO_AddBloodbagProcessTypeQry(BloodbagProcessTypeQry entity)
        {
            IBBloodbagProcessTypeQry.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodbagProcessTypeQry.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodbagProcessTypeQry.Get(IBBloodbagProcessTypeQry.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodbagProcessTypeQry.Entity);
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
        //Update  BloodbagProcessTypeQry
        public BaseResultBool BT_UDTO_UpdateBloodbagProcessTypeQry(BloodbagProcessTypeQry entity)
        {
            IBBloodbagProcessTypeQry.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodbagProcessTypeQry.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodbagProcessTypeQry
        public BaseResultBool BT_UDTO_UpdateBloodbagProcessTypeQryByField(BloodbagProcessTypeQry entity, string fields)
        {
            IBBloodbagProcessTypeQry.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodbagProcessTypeQry.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodbagProcessTypeQry.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodbagProcessTypeQry.Edit();
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
        //Delele  BloodbagProcessTypeQry
        public BaseResultBool BT_UDTO_DelBloodbagProcessTypeQry(long longBloodbagProcessTypeQryID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodbagProcessTypeQry.Remove(longBloodbagProcessTypeQryID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodbagProcessTypeQry(BloodbagProcessTypeQry entity)
        {
            IBBloodbagProcessTypeQry.Entity = entity;
            EntityList<BloodbagProcessTypeQry> tempEntityList = new EntityList<BloodbagProcessTypeQry>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodbagProcessTypeQry.Search();
                tempEntityList.count = IBBloodbagProcessTypeQry.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodbagProcessTypeQry>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodbagProcessTypeQryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodbagProcessTypeQry> tempEntityList = new EntityList<BloodbagProcessTypeQry>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodbagProcessTypeQry.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodbagProcessTypeQry.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodbagProcessTypeQry>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodbagProcessTypeQryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodbagProcessTypeQry.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodbagProcessTypeQry>(tempEntity);
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
        public BaseResultBool BT_UDTO_DelBloodBInForm(string longBloodBInFormID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodBInFormById(string id, string fields, bool isPlanish)
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
        public BaseResultBool BT_UDTO_DelBloodBInItem(string longBloodBInItemID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodBInItemById(string id, string fields, bool isPlanish)
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

        #region BloodBInItemState
        //Add  BloodBInItemState
        public BaseResultDataValue BT_UDTO_AddBloodBInItemState(BloodBInItemState entity)
        {
            IBBloodBInItemState.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodBInItemState.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodBInItemState.Get(IBBloodBInItemState.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodBInItemState.Entity);
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
        //Update  BloodBInItemState
        public BaseResultBool BT_UDTO_UpdateBloodBInItemState(BloodBInItemState entity)
        {
            IBBloodBInItemState.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBInItemState.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodBInItemState
        public BaseResultBool BT_UDTO_UpdateBloodBInItemStateByField(BloodBInItemState entity, string fields)
        {
            IBBloodBInItemState.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodBInItemState.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodBInItemState.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodBInItemState.Edit();
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
        //Delele  BloodBInItemState
        public BaseResultBool BT_UDTO_DelBloodBInItemState(string longBloodBInItemStateID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodBInItemState.Remove(longBloodBInItemStateID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBInItemState(BloodBInItemState entity)
        {
            IBBloodBInItemState.Entity = entity;
            EntityList<BloodBInItemState> tempEntityList = new EntityList<BloodBInItemState>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodBInItemState.Search();
                tempEntityList.count = IBBloodBInItemState.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBInItemState>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBInItemStateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBInItemState> tempEntityList = new EntityList<BloodBInItemState>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodBInItemState.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodBInItemState.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBInItemState>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodBInItemStateById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodBInItemState.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBInItemState>(tempEntity);
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

        #region BloodReason
        //Add  BloodReason
        public BaseResultDataValue BT_UDTO_AddBloodReason(BloodReason entity)
        {
            IBBloodReason.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodReason.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodReason.Get(IBBloodReason.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodReason.Entity);
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
        //Update  BloodReason
        public BaseResultBool BT_UDTO_UpdateBloodReason(BloodReason entity)
        {
            IBBloodReason.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodReason.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodReason
        public BaseResultBool BT_UDTO_UpdateBloodReasonByField(BloodReason entity, string fields)
        {
            IBBloodReason.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodReason.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodReason.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodReason.Edit();
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
        //Delele  BloodReason
        public BaseResultBool BT_UDTO_DelBloodReason(string longBloodReasonID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodReason.Remove(longBloodReasonID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodReason(BloodReason entity)
        {
            IBBloodReason.Entity = entity;
            EntityList<BloodReason> tempEntityList = new EntityList<BloodReason>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodReason.Search();
                tempEntityList.count = IBBloodReason.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodReason>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodReasonByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodReason> tempEntityList = new EntityList<BloodReason>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodReason.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodReason.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodReason>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodReasonById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodReason.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodReason>(tempEntity);
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
        public BaseResultBool BT_UDTO_DelBloodRecei(string longBloodReceiID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodReceiById(string id, string fields, bool isPlanish)
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
        public BaseResultBool BT_UDTO_DelBloodReceiItem(string longBloodReceiItemID)
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

        public BaseResultDataValue BT_UDTO_SearchBloodReceiItemById(string id, string fields, bool isPlanish)
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

        #region Bloodrefuse
        //Add  Bloodrefuse
        public BaseResultDataValue BT_UDTO_AddBloodrefuse(Bloodrefuse entity)
        {
            IBBloodrefuse.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodrefuse.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodrefuse.Get(IBBloodrefuse.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodrefuse.Entity);
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
        //Update  Bloodrefuse
        public BaseResultBool BT_UDTO_UpdateBloodrefuse(Bloodrefuse entity)
        {
            IBBloodrefuse.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodrefuse.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  Bloodrefuse
        public BaseResultBool BT_UDTO_UpdateBloodrefuseByField(Bloodrefuse entity, string fields)
        {
            IBBloodrefuse.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodrefuse.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodrefuse.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodrefuse.Edit();
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
        //Delele  Bloodrefuse
        public BaseResultBool BT_UDTO_DelBloodrefuse(string longBloodrefuseID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodrefuse.Remove(longBloodrefuseID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodrefuse(Bloodrefuse entity)
        {
            IBBloodrefuse.Entity = entity;
            EntityList<Bloodrefuse> tempEntityList = new EntityList<Bloodrefuse>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodrefuse.Search();
                tempEntityList.count = IBBloodrefuse.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Bloodrefuse>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodrefuseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<Bloodrefuse> tempEntityList = new EntityList<Bloodrefuse>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodrefuse.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodrefuse.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Bloodrefuse>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodrefuseById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodrefuse.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<Bloodrefuse>(tempEntity);
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

        #region BloodrefuseDispose
        //Add  BloodrefuseDispose
        public BaseResultDataValue BT_UDTO_AddBloodrefuseDispose(BloodrefuseDispose entity)
        {
            IBBloodrefuseDispose.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBloodrefuseDispose.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBloodrefuseDispose.Get(IBBloodrefuseDispose.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBloodrefuseDispose.Entity);
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
        //Update  BloodrefuseDispose
        public BaseResultBool BT_UDTO_UpdateBloodrefuseDispose(BloodrefuseDispose entity)
        {
            IBBloodrefuseDispose.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodrefuseDispose.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BloodrefuseDispose
        public BaseResultBool BT_UDTO_UpdateBloodrefuseDisposeByField(BloodrefuseDispose entity, string fields)
        {
            IBBloodrefuseDispose.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBloodrefuseDispose.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBloodrefuseDispose.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBloodrefuseDispose.Edit();
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
        //Delele  BloodrefuseDispose
        public BaseResultBool BT_UDTO_DelBloodrefuseDispose(string longBloodrefuseDisposeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBloodrefuseDispose.Remove(longBloodrefuseDisposeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodrefuseDispose(BloodrefuseDispose entity)
        {
            IBBloodrefuseDispose.Entity = entity;
            EntityList<BloodrefuseDispose> tempEntityList = new EntityList<BloodrefuseDispose>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBloodrefuseDispose.Search();
                tempEntityList.count = IBBloodrefuseDispose.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodrefuseDispose>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodrefuseDisposeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodrefuseDispose> tempEntityList = new EntityList<BloodrefuseDispose>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBloodrefuseDispose.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBloodrefuseDispose.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodrefuseDispose>(tempEntityList);
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

        public BaseResultDataValue BT_UDTO_SearchBloodrefuseDisposeById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBloodrefuseDispose.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodrefuseDispose>(tempEntity);
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

        #region PDF清单报表
        public BaseResultDataValue BT_UDTO_SearchPublicTemplateFileInfoByType(string publicTemplateDir)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<JObject> entityList = new EntityList<JObject>();
            try
            {
                entityList = IBBTemplate.GetPublicTemplateDirFile(publicTemplateDir);

                ParseObjectProperty pop = new ParseObjectProperty("");
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
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
        public BaseResultBool BT_UDTO_AddBTemplateOfPublicTemplate(string entityList, long labId, string labCName)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (!string.IsNullOrEmpty(entityList))
            {
                try
                {
                    JArray jarray = JArray.Parse(entityList);
                    long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                    string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    if (labId <= 0)
                        labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                    if (string.IsNullOrEmpty(labCName))
                        labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                    baseResultBool = IBBTemplate.AddBTemplateOfPublicTemplate(jarray, labId, labCName, empID, empName);
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                    ZhiFang.Common.Log.Log.Error("新增报表模板保存失败:" + ex.StackTrace);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        public BaseResultDataValue BT_UDTO_SearchBTemplateByLabIdAndType(long labId, long breportType, string publicTemplateDir)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<JObject> entityList = new EntityList<JObject>();
            try
            {
                entityList = IBBTemplate.SearchBTemplateByLabIdAndType(labId, breportType, publicTemplateDir);

                ParseObjectProperty pop = new ParseObjectProperty("");
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
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

        public Stream BT_UDTO_SearchBusinessReportOfPdfById(string reaReportClass, string breportType, string id, long operateType, string frx)
        {
            Stream fileStream = null;
            string fileName = "", info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID)) employeeID = "-1";
                //if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                //{
                //    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                //    return memoryStream;
                //}
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string labIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID);
                long labId = 0;
                if (!string.IsNullOrEmpty(labIdStr)) labId = long.Parse(labIdStr);
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (breportType == BTemplateType.医嘱申请.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.医嘱申请.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.入库清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.入库清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.样本接收.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.样本接收.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.交叉配血.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.交叉配血.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.发血清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.发血清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.领用清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.领用清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.血袋接收.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.血袋接收.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.输血过程.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.输血过程.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.血袋回收.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.血袋回收.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.输血综合查询.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.输血综合查询.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
                Encoding code = Encoding.GetEncoding("gb2312");
                System.Web.HttpContext.Current.Response.ContentEncoding = code;
                System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                fileName = EncodeFileName.ToEncodeFileName(fileName);
                if (operateType == 0) //下载文件
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                }
                else if (operateType == 1)//直接打开文件
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                }
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "获取" + info + "PDF文件失败!";
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }

        #endregion

        #region Excel导出
        public Stream RS_UDTO_SearchBusinessReportOfExcelById(long operateType, string breportType, long id, string frx)
        {
            Stream fileStream = null;
            string fileName = "";
            string info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);

                if (breportType == BTemplateType.医嘱申请.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.医嘱申请.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBBloodBReqForm.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.入库清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.入库清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsInDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.血型复核.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.血型复核.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsTransferDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.样本接收.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.样本接收.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsOutDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.交叉配血.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.交叉配血.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsOutDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.发血清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.发血清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsOutDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.领用清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.领用清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsOutDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.血袋接收.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.血袋接收.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsOutDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.输血过程.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.输血过程.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsOutDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.血袋回收.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.血袋回收.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsOutDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.输血综合查询.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.输血综合查询.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    //fileStream = IBReaBmsOutDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                if (fileStream != null)
                {
                    Encoding code = Encoding.GetEncoding("GB2312");
                    System.Web.HttpContext.Current.Response.Charset = "GB2312";
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    fileName = EncodeFileName.ToEncodeFileName(fileName);
                    if (operateType == 0) //下载文件application/octet-stream
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                    }
                }
                else
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + "数据为空!");
                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                if (fileStream != null)
                    fileStream.Close();
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + ex.Message + "错误!");
                return memoryStream;
            }
            return fileStream;
        }
        #endregion

        #region 医生站定制服务
        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormEntityListByHql(string where, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqForm> tempEntityList = new EntityList<BloodBReqForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBReqForm.SearchBloodBReqFormEntityListByHql(where, sort, page, limit);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormEntityListByJoinHql.查询错误：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormEntityListByJoinHql(string where, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //if (string.IsNullOrEmpty(where) && string.IsNullOrEmpty(docHql))
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "HQL查询错误：查询条件不能为空!";
            //    return tempBaseResultDataValue;
            //}
            EntityList<BloodBReqForm> tempEntityList = new EntityList<BloodBReqForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBReqForm.SearchBloodBReqFormEntityListByJoinHql(where, sort, page, limit);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormEntityListByJoinHql.查询错误：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodstyleEntityListByJoinHql(string where, string bloodclassHql, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //if (string.IsNullOrEmpty(where) && string.IsNullOrEmpty(bloodclassHql))
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "HQL查询错误：查询条件不能为空!";
            //    return tempBaseResultDataValue;
            //}

            EntityList<Bloodstyle> tempEntityList = new EntityList<Bloodstyle>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodstyle.SearchBloodstyleEntityListByJoinHql(where, bloodclassHql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Bloodstyle>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodstyleEntityListByJoinHql.查询错误：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBReqItemEntityListByJoinHql(string where, string docHql, string bloodstyleHql, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(where) && string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(bloodstyleHql))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：查询条件不能为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BloodBReqItem> tempEntityList = new EntityList<BloodBReqItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBReqItem.SearchBloodBReqItemEntityListByJoinHql(where, docHql, bloodstyleHql, sort, page, limit);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqItemEntityListByJoinHql.查询错误：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormResultEntityListByJoinHql(string where, string docHql, string bloodstyleHql, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(where) && string.IsNullOrEmpty(bloodstyleHql))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：查询条件不能为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BloodBReqFormResult> tempEntityList = new EntityList<BloodBReqFormResult>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBReqFormResult.SearchBloodBReqFormResultEntityListByJoinHql(where, bloodstyleHql, sort, page, limit);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormResultEntityListByJoinHql.查询错误：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBReqItemResultEntityListByJoinHql(string where, string docHql, string bloodbtestitemHql, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(where) && string.IsNullOrEmpty(bloodbtestitemHql))//&& string.IsNullOrEmpty(docHql)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：查询条件不能为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BloodBReqItemResult> tempEntityList = new EntityList<BloodBReqItemResult>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBReqItemResult.SearchBloodBReqItemResultEntityListByJoinHql(where, bloodbtestitemHql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqItemResult>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqItemResultEntityListByJoinHql.查询错误：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_GetBloodBReqItemResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(vlisresultHql) && string.IsNullOrEmpty(reqresulthql))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：查询条件不能为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BloodBReqItemResult> tempEntityList = new EntityList<BloodBReqItemResult>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBReqItemResult.GetBloodBReqItemResultListByVLisResultHql(reqFormId, vlisresultHql, reqresulthql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqItemResult>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_GetBloodBReqItemResultListByVLisResultHql.查询错误：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_GetBloodBReqFormResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(vlisresultHql) && string.IsNullOrEmpty(reqresulthql))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：查询条件不能为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BloodBReqFormResult> tempEntityList = new EntityList<BloodBReqFormResult>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBReqFormResult.GetBloodBReqFormResultListByVLisResultHql(reqFormId, vlisresultHql, reqresulthql, sort, page, limit);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_GetBloodBReqFormResultListByVLisResultHql.查询错误：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SelectBloodBReqFormResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(vlisresultHql) && string.IsNullOrEmpty(reqresulthql))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：查询条件不能为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BloodBReqFormResult> tempEntityList = new EntityList<BloodBReqFormResult>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBReqFormResult.SelectBloodBReqFormResultListByVLisResultHql(reqFormId, vlisresultHql, reqresulthql, sort, page, limit);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SelectBloodBReqFormResultListByVLisResultHql.查询错误：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_AddBloodBReqFormAndDtl(BloodBReqForm entity, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, IList<BloodBReqItem> addBreqItemList, IList<BloodBReqFormResult> addResultList, string empID, string empName)
        {
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",新增保存(总)处理开始!");
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (addBreqItemList == null || addBreqItemList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数addBreqItemList为空!";
                return brdv;
            }
            try
            {
                ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",新增保存到血库数据库开始!");
                brdv = IBBloodBReqForm.AddBloodBReqFormAndDtl(entity, curDoctor, addBreqItemList, addResultList, long.Parse(empID), empName);
                ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",新增保存到血库数据库结束!");
                if (brdv.success)
                {
                    entity = IBBloodBReqForm.Get(entity.Id);
                    #region 用血申请保存后处理
                    //用血申请是否审批完成及是否需要上传HIS
                    ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",是否审批完成:" + entity.CheckCompleteFlag + ",审批完成后是否自动上传HIS:" + bloodsConfigVO.HisInterface.ISTOHISDATA + ",HIS数据标志:" + entity.ToHisFlag);
                    if (entity.CheckCompleteFlag == true && bloodsConfigVO.HisInterface.ISTOHISDATA == true)
                    {
                        if (BReqToHisHelp.ToHisCurIdList.Contains(entity.Id) == false)
                        {
                            brdv.HasInterface = true;
                            BaseResult baseresultdata = BReqToHisHelp.UploadReqDataToHis(entity, bloodsConfigVO);
                            brdv.InterfaceSuccess = baseresultdata.InterfaceSuccess;
                            brdv.InterfaceMsg = baseresultdata.InterfaceMsg;
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",正在上传中!");
                        }
                    }
                    #endregion
                    brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_AddBloodBReqFormAndDtl.错误：" + ex.StackTrace);
                //throw new Exception(ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",新增保存(总)处理结束!");
            return brdv;
        }
        //Update  BloodBReqForm
        public BaseResultBool BT_UDTO_UpdateBloodBReqFormAndDtlByField(BloodBReqForm entity, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, IList<BloodBReqItem> addBreqItemList, IList<BloodBReqItem> editBreqItemList, IList<BloodBReqFormResult> addResultList, IList<BloodBReqFormResult> editResultList, string fields, long empID, string empName)
        {
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存(总)处理开始!");
            BaseResultBool brdv = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                    if (tempArray != null)
                    {
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存到血库数据库开始!");
                        brdv = IBBloodBReqForm.EditBloodBReqFormAndDtlByField(ref entity, curDoctor, tempArray, addBreqItemList, editBreqItemList, addResultList, editResultList, empID, empName);
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存到血库数据库结束!");
                        #region 用血申请保存后处理
                        BloodBReqForm curEntity = IBBloodBReqForm.Get(entity.Id);
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + curEntity.Id + ",是否审批完成:" + curEntity.CheckCompleteFlag + ",审批完成后是否自动上传HIS:" + bloodsConfigVO.HisInterface.ISTOHISDATA + ",HIS数据标志:" + curEntity.ToHisFlag);
                        if (curEntity.CheckCompleteFlag == true && bloodsConfigVO.HisInterface.ISTOHISDATA == true && curEntity.ToHisFlag != 1)
                        {
                            if (BReqToHisHelp.ToHisCurIdList.Contains(entity.Id) == false)
                            {
                                brdv.HasInterface = true;
                                BaseResult baseresultdata = BReqToHisHelp.UploadReqDataToHis(entity, bloodsConfigVO);
                                brdv.InterfaceSuccess = baseresultdata.InterfaceSuccess;
                                brdv.InterfaceMsg = baseresultdata.InterfaceMsg;
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",正在上传中!");
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：fields参数不能为空！";
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateBloodBReqFormAndDtlByField.错误：" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存(总)处理结束!");
            return brdv;
        }
        public BaseResultBool BT_UDTO_UpdateBloodBReqFormOfConfirmApplyByReqFormId(BloodBReqForm entity, BloodsConfig bloodsConfigVO, string fields, long empID, string empName)
        {
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存(总)处理开始!");
            BaseResultBool brdv = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    //判断是否审批完成
                    if (entity.CheckCompleteFlag == true)
                    {
                        entity.CheckCompleteTime = DateTime.Now;
                    }
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                    if (tempArray != null)
                    {
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存到血库数据库开始!");
                        brdv = IBBloodBReqForm.EditBloodBReqFormOfConfirmApplyByReqFormId(entity, tempArray, empID, empName);
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存到血库数据库结束!");
                        #region 用血申请保存后处理
                        BloodBReqForm curEntity = IBBloodBReqForm.Get(entity.Id);
                        //用血申请是否审批完成及是否需要上传HIS
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + curEntity.Id + ",是否审批完成:" + curEntity.CheckCompleteFlag + ",审批完成后是否自动上传HIS:" + bloodsConfigVO.HisInterface.ISTOHISDATA + ",HIS数据标志:" + curEntity.ToHisFlag);
                        if (curEntity.CheckCompleteFlag == true && bloodsConfigVO.HisInterface.ISTOHISDATA == true && curEntity.ToHisFlag != 1)
                        {
                            if (BReqToHisHelp.ToHisCurIdList.Contains(entity.Id) == false)
                            {
                                brdv.HasInterface = true;
                                BaseResult baseresultdata = BReqToHisHelp.UploadReqDataToHis(entity, bloodsConfigVO);
                                brdv.InterfaceSuccess = baseresultdata.InterfaceSuccess;
                                brdv.InterfaceMsg = baseresultdata.InterfaceMsg;
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",正在上传中!");
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateBloodBReqFormOfConfirmApplyByReqFormId.错误：" + ex.StackTrace);
                //throw new Exception(ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存(总)处理结束!");
            return brdv;
        }
        public BaseResultBool BT_UDTO_UpdateBloodBReqFormOfReviewByReqForm(BloodBReqForm entity, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, string fields, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存(总)处理开始!");
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                    if (tempArray != null)
                    {
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存到血库数据库开始!");
                        brdv = IBBloodBReqForm.EditBloodBReqFormAndDtlByField(ref entity, curDoctor, tempArray, null, null, null, null, empID, empName);
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存到血库数据库结束!");
                        #region 用血申请保存后处理
                        BloodBReqForm curEntity = IBBloodBReqForm.Get(entity.Id);
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + curEntity.Id + ",是否审批完成:" + curEntity.CheckCompleteFlag + ",审批完成后是否自动上传HIS:" + bloodsConfigVO.HisInterface.ISTOHISDATA + ",HIS数据标志:" + entity.ToHisFlag);
                        if (curEntity.CheckCompleteFlag == true && bloodsConfigVO.HisInterface.ISTOHISDATA == true && curEntity.ToHisFlag != 1)
                        {
                            if (BReqToHisHelp.ToHisCurIdList.Contains(entity.Id) == false)
                            {
                                brdv.HasInterface = true;
                                BaseResult baseresultdata = BReqToHisHelp.UploadReqDataToHis(entity, bloodsConfigVO);
                                brdv.InterfaceSuccess = baseresultdata.InterfaceSuccess;
                                brdv.InterfaceMsg = baseresultdata.InterfaceMsg;
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",正在上传中!");
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateBloodBReqFormOfReviewByReqForm.错误：" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",编辑保存(总)处理结束!");
            return brdv;
        }
        public BaseResultBool BT_UDTO_UpdateReqDataUploadToHis(string id, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, string fields, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + id + ",上传HIS(总)处理开始!");
            try
            {
                BloodBReqForm entity = IBBloodBReqForm.Get(id);

                ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",是否审批完成:" + entity.CheckCompleteFlag + ",审批完成后是否自动上传HIS:" + bloodsConfigVO.HisInterface.ISTOHISDATA + ",HIS数据标志:" + entity.ToHisFlag);
                if (entity.CheckCompleteFlag == true && bloodsConfigVO.HisInterface.ISTOHISDATA == true && entity.ToHisFlag != 1)
                {
                    if (BReqToHisHelp.ToHisCurIdList.Contains(entity.Id) == false)
                    {
                        brdv.HasInterface = true;
                        BaseResult baseresultdata = BReqToHisHelp.UploadReqDataToHis(entity, bloodsConfigVO);
                        brdv.InterfaceSuccess = baseresultdata.InterfaceSuccess;
                        brdv.InterfaceMsg = baseresultdata.InterfaceMsg;
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",正在上传中!");
                    }
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UploadReqDataToHis.错误：" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + id + ",上传HIS(总)处理结束!");
            return brdv;
        }
        public BaseResultBool BT_UDTO_UpdateReqDataObsoleteToHis(string id, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, string fields, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + id + ",作废HIS(总)处理开始!");
            try
            {
                BloodBReqForm entity = IBBloodBReqForm.Get(id);

                ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",是否审批完成:" + entity.CheckCompleteFlag + ",当前用血申请状态:" + BreqFormStatus.GetStatusDic()[entity.BreqStatusID.ToString()].Name + ",HIS数据标志:" + entity.ToHisFlag);
                if (entity.CheckCompleteFlag == true && bloodsConfigVO.HisInterface.ISTOHISDATA == true && entity.ToHisFlag != 1)
                {
                    if (BReqToHisHelp.ToHisCurIdList.Contains(entity.Id) == false)
                    {
                        brdv.HasInterface = true;
                        BaseResult baseresultdata = BReqToHisHelp.ObsoleteReqDataToHis(entity, bloodsConfigVO, curDoctor);
                        brdv.InterfaceSuccess = baseresultdata.InterfaceSuccess;
                        brdv.InterfaceMsg = baseresultdata.InterfaceMsg;
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",正在作废中!");
                    }
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateReqDataObsoleteToHis.错误：" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + id + ",作废HIS(总)处理结束!");
            return brdv;
        }

        #endregion

        #region 医生站PDF预览及打印
        public BaseResultBool BT_UDTO_SearchBusinessReportOfPDFJSById(string id, string reaReportClass, string breportType, long operateType, string frx)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string fileName = "", info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID)) employeeID = "-1";
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string labIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID);
                long labId = 0;
                if (!string.IsNullOrEmpty(labIdStr)) labId = long.Parse(labIdStr);
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (breportType == BTemplateType.医嘱申请.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.医嘱申请.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    tempBaseResultBool = IBBloodBReqForm.SearchBusinessReportOfPDFJSById(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }

                return tempBaseResultBool;
            }
            catch (Exception ex)
            {
                string errorInfo = "获取" + info + "PDF文件失败!";
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.StackTrace);
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                return tempBaseResultBool;
            }
        }

        public BaseResultDataValue BT_UDTO_SearchImageReportToBase64String(string id, string reaReportClass, string breportType, long operateType, string frx)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string fileName = "", info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID)) employeeID = "-1";
                //if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                //{
                //    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                //    return memoryStream;
                //}
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string labIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID);
                long labId = 0;
                if (!string.IsNullOrEmpty(labIdStr)) labId = long.Parse(labIdStr);
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (breportType == BTemplateType.医嘱申请.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.医嘱申请.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    brdv = IBBloodBReqForm.SearchImageReportToBase64String(reaReportClass, id, labId, labCName, empID, empName, breportType, frx, ref fileName);
                }
            }
            catch (Exception ex)
            {
                string errorInfo = "获取" + info + "PDF文件失败!";
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.StackTrace);
                brdv.success = false;
                brdv.ErrorInfo = errorInfo;

            }
            return brdv;
        }
        #endregion

        #region 护士站定制

        #region 血制品交接登记
        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormVOOfHandoverByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqFormVO> tempEntityList = new EntityList<BloodBReqFormVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBOutForm.SearchBloodBReqFormVOOfHandoverByHQL(where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBReqFormVO>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormVOOfHandoverByHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormVOOfHandoverByHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormVOOfHandoverByHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfHandoverByBReqVOHQL(int page, int limit, string fields, string bReqVO, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //bReqVO封装成BloodBReqFormVO
            if (string.IsNullOrEmpty(bReqVO))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入查询条件(bReqVO)信息为空!";
                return tempBaseResultDataValue;
            }

            EntityList<BloodBOutItem> tempEntityList = new EntityList<BloodBOutItem>();
            try
            {
                JObject jresult = JObject.Parse(bReqVO);
                BloodBReqFormVO bReqVO1 = JsonHelper.JsonToObject<BloodBReqFormVO>(jresult, "");
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBOutItem.SearchBloodBOutItemOfHandoverByBReqVOHQL(bReqVO1, where, sort, page, limit);
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
                    ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfHandoverByBReqVOHQL.Error.where:" + where);
                    ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfHandoverByBReqVOHQL.Error.Message:" + ex.Message);
                    ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfHandoverByBReqVOHQL.Error.StackTrace:" + ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfHandoverByBReqVOHQL:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBOutFormOfHandoverByBReqVOHQL(int page, int limit, string fields, string bReqVO, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //bReqVO封装成BloodBReqFormVO
            if (string.IsNullOrEmpty(bReqVO))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入查询条件(bReqVO)信息为空!";
                return tempBaseResultDataValue;
            }

            EntityList<BloodBOutForm> tempEntityList = new EntityList<BloodBOutForm>();
            try
            {
                JObject jresult = JObject.Parse(bReqVO);
                BloodBReqFormVO bReqVO1 = JsonHelper.JsonToObject<BloodBReqFormVO>(jresult, "");
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBOutForm.SearchBloodBOutFormOfHandoverByBReqVOHQL(bReqVO1, where, sort, page, limit);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutFormOfHandoverByBReqVOHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutFormOfHandoverByBReqVOHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutFormOfHandoverByBReqVOHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfHandoverByBBagCodeHQL(int page, int limit, string fields, string where, string bagCode, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(bagCode))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入的血袋号参数(bagCode)不能为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BloodBOutItem> tempEntityList = new EntityList<BloodBOutItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                //从系统运行参数中获取血袋扫码识别字段
                string scanCodeField = BParameterCache.GetParaValue(SYSParaNo.血袋扫码识别字段.Key);
                ZhiFang.Common.Log.Log.Info("血制品交接登记.血袋扫码识别字段：" + scanCodeField + ",传入值为:" + bagCode);
                scanCodeField = scanCodeField.Trim();

                tempEntityList = IBBloodBOutItem.SearchBloodBOutItemOfHandoverByBBagCodeHQL(where, scanCodeField, bagCode, sort, page, limit, ref tempBaseResultDataValue);
                if (tempBaseResultDataValue.success == false)
                    return tempBaseResultDataValue;

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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfHandoverByBBagCodeHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfHandoverByBBagCodeHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfHandoverByBBagCodeHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_AddBloodBagOperationAndDtlOfHandover(BloodBagOperation entity, IList<BloodBagOperationDtl> bloodBagOperationDtlList, string empID, string empName)
        {
            ZhiFang.Common.Log.Log.Debug("血袋号为:" + entity.Id + ",新增血制品交接登记开始!");
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数(entity)为空!";
                return brdv;
            }
            if (bloodBagOperationDtlList == null || bloodBagOperationDtlList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数(bloodBagOperationDtlList)为空!";
                return brdv;
            }
            try
            {
                brdv = IBBloodBagOperation.AddBloodBagOperationAndDtlOfHandover(entity, bloodBagOperationDtlList, long.Parse(empID), empName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_AddBloodBagOperationAndDtlOfHandover.错误：" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("血袋号为:" + entity.Id + ",新增血制品交接登记处理结束!");
            return brdv;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBagOperationAndDtlOfHandoverByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBagOperation> tempEntityList = new EntityList<BloodBagOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                tempEntityList = IBBloodBagOperation.SearchBloodBagOperationAndDtlOfHandoverByHQL(where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagOperation>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBagOperationAndDtlOfHandoverByHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBagOperationAndDtlOfHandoverByHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBagOperationAndDtlOfHandoverByHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBOutItem> tempEntityList = new EntityList<BloodBOutItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBOutItem.SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL(where, sort, page, limit, ref tempBaseResultDataValue);
                if (tempBaseResultDataValue.success == false)
                    return tempBaseResultDataValue;

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
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBagHandoverVOById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                BloodBagHandoverVO tempEntity = IBBloodBagOperation.GetBloodBagHandoverVO(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BloodBagHandoverVO>(tempEntity);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBagHandoverVOById.Error.id:" + id);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBagHandoverVOById.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBagHandoverVOById.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool BT_UDTO_UpdateBloodBagHandoverVO(BloodBagHandoverVO entity)
        {
            //IBBloodBagOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(entity)为空!";
                return tempBaseResultBool;
            }
            if (entity.BloodBagHandover == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(BloodBagHandover)为空!";
                return tempBaseResultBool;
            }
            if (entity.BloodAppearance == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(BloodAppearance)为空!";
                return tempBaseResultBool;
            }
            if (entity.BloodIntegrity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的参数(BloodIntegrity)为空!";
                return tempBaseResultBool;
            }
            try
            {
                tempBaseResultBool = IBBloodBagOperation.EditBloodBagHandoverVO(entity);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateBloodBagHandoverVO.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        #endregion

        #region 血袋回收登记
        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfRecycleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            ZhiFang.Common.Log.Log.Debug("BT_UDTO_SearchBloodBOutItemOfRecycleByHQL.where:" + where);
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBOutItem> tempEntityList = new EntityList<BloodBOutItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBOutItem.SearchBloodBOutItemOfRecycleByHQL(where, sort, page, limit);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfRecycleByHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfRecycleByHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfRecycleByHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfRecycleByBBagCodeHQL(int page, int limit, string fields, string where, string bagCode, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(bagCode))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入的血袋号参数(bagCode)不能为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BloodBOutItem> tempEntityList = new EntityList<BloodBOutItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                //从系统运行参数中获取血袋扫码识别字段
                string scanCodeField = BParameterCache.GetParaValue(SYSParaNo.血袋扫码识别字段.Key);
                ZhiFang.Common.Log.Log.Info("血袋回收登记.血袋扫码识别字段：" + scanCodeField + ",传入值为:" + bagCode);
                scanCodeField = scanCodeField.Trim();
                tempEntityList = IBBloodBOutItem.SearchBloodBOutItemOfRecycleByBBagCodeHQL(where, scanCodeField, bagCode, sort, page, limit, ref tempBaseResultDataValue);
                if (tempBaseResultDataValue.success == false)
                    return tempBaseResultDataValue;
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfRecycleByBBagCodeHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfRecycleByBBagCodeHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfRecycleByBBagCodeHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_AddBloodBagOperationListOfRecycle(IList<BloodBagOperation> bloodBagOperationList, bool isHasTrans, string empID, string empName)
        {
            ZhiFang.Common.Log.Log.Debug("新增血袋回收登记开始!");
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (bloodBagOperationList == null || bloodBagOperationList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数(bloodBagOperationList)为空!";
                return brdv;
            }
            try
            {
                //从系统运行参数中获取
                string isHasTransStr = BParameterCache.GetParaValue(SYSParaNo.是否输血过程记录登记后才能血袋回收登记.Key);
                ZhiFang.Common.Log.Log.Error("是否输血过程记录登记后才能血袋回收登记：" + isHasTransStr);
                if (isHasTransStr == "1" || isHasTransStr == "true")
                {
                    isHasTrans = true;
                }
                else
                {
                    isHasTrans = true;
                }
                brdv = IBBloodBagOperation.AddBloodBagOperationListOfRecycle(bloodBagOperationList, isHasTrans, long.Parse(empID), empName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_AddBloodBagOperationListOfRecycle.错误：" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("新增血袋回收登记处理结束!");
            return brdv;

        }
        #endregion

        #region 输血过程记录
        public BaseResultDataValue BT_UDTO_SearchBloodBOutFormOfBloodTransByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBOutForm> tempEntityList = new EntityList<BloodBOutForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBOutForm.SearchBloodBOutFormOfBloodTransByHQL(where, sort, page, limit);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutFormOfBloodTransByHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutFormOfBloodTransByHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutFormOfBloodTransByHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfBloodTransByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBOutItem> tempEntityList = new EntityList<BloodBOutItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBOutItem.SearchBloodBOutItemOfBloodTransByHQL(where, sort, page, limit);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfBloodTransByHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfBloodTransByHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemOfBloodTransByHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchTransfusionAntriesOfBloodTransByHQL(string fields, string where, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempBaseResultDataValue = IBBloodTransRecordTypeItem.SearchTransfusionAntriesOfBloodTransByHQL(where, sort);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchTransfusionAntriesOfBloodTransByHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchTransfusionAntriesOfBloodTransByHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchTransfusionAntriesOfBloodTransByHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodTransItemListByTransFormId(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodTransItem> tempEntityList = new EntityList<BloodTransItem>();
            try
            {
                tempEntityList = IBBloodTransItem.SearchBloodTransItemListByTransFormId(id);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodTransItemListByTransFormId.Error.id:" + id);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodTransItemListByTransFormId.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodTransItemListByTransFormId.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodTransItemByContentTypeID(long contentTypeId, string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                BloodTransItem tempEntity = IBBloodTransItem.GetBloodTransItemByContentTypeID(contentTypeId, id);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodTransItemByContentTypeID.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_AddBloodTransFormAndDtlList(IList<BloodBOutItem> outDtlList, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, string empID, string empName)
        {
            //ZhiFang.Common.Log.Log.Debug("新增血袋输血过程记录开始!");
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (outDtlList == null || outDtlList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数(outDtlList)为空!";
                return brdv;
            }
            if (transForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数(transForm)为空!";
                return brdv;
            }
            try
            {
                long tempEmpId = -1;
                if (!string.IsNullOrEmpty(empID)) tempEmpId = long.Parse(empID);
                brdv = IBBloodTransForm.AddBloodTransFormAndDtlList(outDtlList, transForm, transfusionAntriesList, adverseReactionList, clinicalMeasuresList, clinicalResults, clinicalResultsDesc, tempEmpId, empName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_AddBloodTransFormAndDtlList.Error.StackTrace:" + ex.StackTrace);
            }
            //ZhiFang.Common.Log.Log.Debug("新增血袋输血过程记录处理结束!");
            return brdv;
        }
        public BaseResultDataValue BT_UDTO_UpdateBloodTransFormAndDtlList(BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, string empID, string empName)
        {
            ZhiFang.Common.Log.Log.Debug("更新血袋输血过程记录开始!");
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (transForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数(transForm)为空!";
                return brdv;
            }
            try
            {
                long tempEmpId = -1;
                if (!string.IsNullOrEmpty(empID)) tempEmpId = long.Parse(empID);
                brdv = IBBloodTransForm.EditBloodTransFormAndDtlList(transForm, transfusionAntriesList, adverseReactionList, clinicalMeasuresList, clinicalResults, clinicalResultsDesc, tempEmpId, empName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateBloodTransFormAndDtlList.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("更新血袋输血过程记录处理结束!");
            return brdv;

        }

        #region 批量修改录入
        public BaseResultDataValue BT_UDTO_SearchBloodTransFormByOutDtlIdStr(string outDtlIdStr, string fields, bool isPlanish)
        {
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取输血过程记录项基本信息开始:outDtlIdStr:" + outDtlIdStr);
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                int batchSign = 0;
                var tempEntity = IBBloodTransForm.SearchBloodTransFormByOutDtlIdStr(outDtlIdStr, ref batchSign);
                tempBaseResultDataValue.BatchSignValue = batchSign.ToString();
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodTransFormByOutDtlIdStr.Error.outDtlIdStr:" + outDtlIdStr);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodTransFormByOutDtlIdStr.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodTransFormByOutDtlIdStr.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取输血过程记录项基本信息结束!");
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchPatientSignsByOutDtlIdStr(string outDtlIdStr, string fields, bool isPlanish)
        {
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取病人体征记录项信息开始:outDtlIdStr:" + outDtlIdStr);
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodTransItem> tempEntityList = new EntityList<BloodTransItem>();
            try
            {
                //int batchSign = 0;
                tempEntityList = IBBloodTransItem.SearchPatientSignsByOutDtlIdStr(outDtlIdStr);
                //tempBaseResultDataValue.BatchSignValue = batchSign.ToString();
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchPatientSignsByOutDtlIdStr.Error.outDtlIdStr:" + outDtlIdStr);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchPatientSignsByOutDtlIdStr.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchPatientSignsByOutDtlIdStr.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取病人体征记录项信息结束!");
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr(string outDtlIdStr, long recordTypeId, string where, string fields, bool isPlanish)
        {
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取不良反应症状记录项信息开始:outDtlIdStr:" + outDtlIdStr);
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodTransItem> tempEntityList = new EntityList<BloodTransItem>();
            try
            {
                int batchSign = 0;
                tempEntityList = IBBloodTransItem.SearchAdverseReactionOptionsByOutDtlIdStr(outDtlIdStr, recordTypeId, where, ref batchSign);
                tempBaseResultDataValue.BatchSignValue = batchSign.ToString();
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr.Error.outDtlIdStr:" + outDtlIdStr);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr.Error.recordTypeId:" + recordTypeId);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取不良反应症状记录项信息结束！");
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchClinicalMeasuresByOutDtlIdStr(string outDtlIdStr, string where, string fields, bool isPlanish)
        {
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取临床处理措施记录项信息开始:outDtlIdStr:" + outDtlIdStr);
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodTransItem> tempEntityList = new EntityList<BloodTransItem>();
            try
            {
                int batchSign = 0;
                tempEntityList = IBBloodTransItem.SearchClinicalMeasuresByOutDtlIdStr(outDtlIdStr, where, ref batchSign);
                tempBaseResultDataValue.BatchSignValue = batchSign.ToString();
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalMeasuresByOutDtlIdStr.Error.outDtlIdStr:" + outDtlIdStr);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalMeasuresByOutDtlIdStr.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalMeasuresByOutDtlIdStr.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalMeasuresByOutDtlIdStr.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取临床处理措施记录项信息结束!");
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchClinicalResultsByOutDtlIdStr(string outDtlIdStr, string where, string fields, bool isPlanish)
        {
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取临床处理结果信息开始:outDtlIdStr:" + outDtlIdStr);
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                int batchSign = 0;
                var tempEntity = IBBloodTransItem.SearchClinicalResultsByOutDtlIdStr(outDtlIdStr, where, ref batchSign);
                tempBaseResultDataValue.BatchSignValue = batchSign.ToString();
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
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalResultsByOutDtlIdStr.Error.outDtlIdStr:" + outDtlIdStr);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalResultsByOutDtlIdStr.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalResultsByOutDtlIdStr.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalResultsByOutDtlIdStr.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取临床处理结果信息结束!");
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchClinicalResultsDescByOutDtlIdStr(string outDtlIdStr, string where, string fields, bool isPlanish)
        {
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取临床处理结果描述信息开始:outDtlIdStr:" + outDtlIdStr);
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                int batchSign = 0;
                var tempEntity = IBBloodTransItem.SearchClinicalResultsDescByOutDtlIdStr(outDtlIdStr, where, ref batchSign);
                tempBaseResultDataValue.BatchSignValue = batchSign.ToString();
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
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalResultsDescByOutDtlIdStr.Error.outDtlIdStr:" + outDtlIdStr);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalResultsDescByOutDtlIdStr.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalResultsDescByOutDtlIdStr.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchClinicalResultsDescByOutDtlIdStr.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("(批量修改录入)获取临床处理结果描述信息结束!");
            return tempBaseResultDataValue;
        }
        public BaseResultBool BT_UDTO_DelBatchTransItemByAdverseReactions(string outDtlIdStr, long recordTypeId, string empID, string empName)
        {
            ZhiFang.Common.Log.Log.Debug("输血过程记录批量修改录入--删除不良反应症状开始:outDtlIdStr:" + outDtlIdStr);
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long tempEmpId = -1;
                if (!string.IsNullOrEmpty(empID)) tempEmpId = long.Parse(empID);
                tempBaseResultBool = IBBloodTransItem.DelBatchTransItemByAdverseReactions(outDtlIdStr, recordTypeId, tempEmpId, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_DelBatchTransItemByAdverseReactions.Error.outDtlIdStr:" + outDtlIdStr);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_DelBatchTransItemByAdverseReactions.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_DelBatchTransItemByAdverseReactions.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("输血过程记录批量修改录入--删除不良反应症状结束!");
            return tempBaseResultBool;
        }
        public BaseResultBool BT_UDTO_DelBatchTransItemByClinicalMeasures(string outDtlIdStr, string empID, string empName)
        {
            ZhiFang.Common.Log.Log.Debug("输血过程记录批量修改录入--删除临床处理措施开始:outDtlIdStr:" + outDtlIdStr);
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long tempEmpId = -1;
                if (!string.IsNullOrEmpty(empID)) tempEmpId = long.Parse(empID);
                tempBaseResultBool = IBBloodTransItem.DelBatchTransItemByClinicalMeasures(outDtlIdStr, tempEmpId, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_DelBatchTransItemByClinicalMeasures.Error.outDtlIdStr:" + outDtlIdStr);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_DelBatchTransItemByClinicalMeasures.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_DelBatchTransItemByClinicalMeasures.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("输血过程记录批量修改录入--删除临床处理措施结束!");
            return tempBaseResultBool;
        }
        public BaseResultDataValue BT_UDTO_UpdateBatchTransFormAndDtlList(IList<BloodBOutItem> outDtlList, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, string empID, string empName)
        {
            ZhiFang.Common.Log.Log.Debug("输血过程记录批量修改录入保存开始!");
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (outDtlList == null || outDtlList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数(outDtlList)为空!";
                return brdv;
            }
            if (transForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数(transForm)为空!";
                return brdv;
            }
            try
            {
                long tempEmpId = -1;
                if (!string.IsNullOrEmpty(empID)) tempEmpId = long.Parse(empID);
                brdv = IBBloodTransForm.EditBatchTransFormAndDtlListByOutDtlList(outDtlList, transForm, transfusionAntriesList, adverseReactionList, clinicalMeasuresList, clinicalResults, clinicalResultsDesc, tempEmpId, empName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateBatchBloodTransFormAndDtlList.Error.StackTrace:" + ex.StackTrace);
            }
            ZhiFang.Common.Log.Log.Debug("输血过程记录批量修改录入保存结束!");
            return brdv;
        }

        public BaseResultBool BT_UDTO_UpdateBOutCourseCompletionByOutId(string id, string updateValue, string empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(id))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的发血单号（id）为空！";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(updateValue))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的输血登记完成度（updateValue）为空！";
                return tempBaseResultBool;
            }
            try
            {
                long tempEmpId = -1;
                if (!string.IsNullOrEmpty(empID)) tempEmpId = long.Parse(empID);
                tempBaseResultBool = IBBloodBOutForm.EditBOutCourseCompletionByOutId(id, updateValue, tempEmpId, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateBOutCourseCompletionByOutId.Error:"+ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool BT_UDTO_UpdateBOutCourseCompletionByEndBloodOper(BloodBOutForm entity, string updateValue, string empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的发血单号（id）为空！";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(updateValue))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的输血登记完成度（updateValue）为空！";
                return tempBaseResultBool;
            }
            try
            {
                long tempEmpId = -1;
                if (!string.IsNullOrEmpty(empID)) tempEmpId = long.Parse(empID);
                tempBaseResultBool = IBBloodBOutForm.EditBOutCourseCompletionByEndBloodOper(entity, updateValue, tempEmpId, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_UpdateBOutCourseCompletionByEndBloodOper.Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }

        #endregion
        #endregion

        #region 输血申请综合查询
        public BaseResultDataValue BT_UDTO_SearchBloodBReqFormListByBBagCodeAndHql(string wardId, string where, string bbagCode, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBReqForm> tempEntityList = new EntityList<BloodBReqForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                //从系统运行参数中获取血袋扫码识别字段
                string scanCodeField = BParameterCache.GetParaValue(SYSParaNo.血袋扫码识别字段.Key);
                ZhiFang.Common.Log.Log.Info("输血申请综合查询.血袋扫码识别字段：" + scanCodeField + ",传入值为:" + bbagCode);
                scanCodeField = scanCodeField.Trim();
                tempEntityList = IBBloodBReqForm.SearchBloodBReqFormListByBBagCodeAndHql(wardId, where, scanCodeField, bbagCode, sort, page, limit);

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
                    ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormListByBBagCodeAndHql.Error.StackTrace:" + ex.StackTrace);
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormListByBBagCodeAndHql.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormListByBBagCodeAndHql.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBReqFormListByBBagCodeAndHql.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodReceiListByBReqVO(string reqFormId, int page, int limit, string fields, string bReqVO, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //bReqVO封装成BloodBReqFormVO
            if (string.IsNullOrEmpty(bReqVO))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入查询条件(bReqVO)信息为空!";
                return tempBaseResultDataValue;
            }

            EntityList<BloodRecei> tempEntityList = new EntityList<BloodRecei>();
            try
            {
                JObject jresult = JObject.Parse(bReqVO);
                BloodBReqFormVO bReqVO1 = JsonHelper.JsonToObject<BloodBReqFormVO>(jresult, "");
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodRecei.SearchBloodReceiListByBReqVO(reqFormId, bReqVO1, sort, page, limit);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodReceiListByBReqVO.Error.where:" + bReqVO);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodReceiListByBReqVO.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodReceiListByBReqVO.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBOutItemByBReqFormIDAndHQL(int page, int limit, string fields, string where, string reqFormId, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BloodBOutItem> tempEntityList = new EntityList<BloodBOutItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBOutItem.SearchBloodBOutItemByBReqFormIDAndHQL(where, reqFormId, sort, page, limit);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemByBReqFormIDAndHQL.Error.where:" + where);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemByBReqFormIDAndHQL.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBOutItemByBReqFormIDAndHQL.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBReqComplexOfInInfoVOByBReqFormID(int page, int limit, string fields, string reqFormId, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(reqFormId))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入申请单号(reqFormId)参数为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BReqComplexOfInInfoVO> tempEntityList = new EntityList<BReqComplexOfInInfoVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBPreForm.SearchBReqComplexOfInInfoVOByBReqFormID(reqFormId, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BReqComplexOfInInfoVO>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBReqComplexOfInInfoVOByBReqFormID.reqFormId:" + reqFormId);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBReqComplexOfInInfoVOByBReqFormID.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue BT_UDTO_SearchBloodBagOperationVOOfByBReqFormID(int page, int limit, string fields, string reqFormId, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(reqFormId))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入申请单号(reqFormId)参数为空!";
                return tempBaseResultDataValue;
            }
            EntityList<BloodBagOperationVO> tempEntityList = new EntityList<BloodBagOperationVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBBloodBagOperation.SearchBloodBagOperationVOOfByBReqFormID(reqFormId, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BloodBagOperationVO>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBagOperationByBReqFormID.reqFormId:" + reqFormId);
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBloodBagOperationByBReqFormID.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #endregion

    }
}
