using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json.Linq;
using ZhiFang.WebAssist.Common;
using ZhiFang.WebAssist.ServerContract;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.IBLL.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.WebAssist.ServerWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WebAssistManageService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 WebAssistManageService.svc 或 WebAssistManageService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WebAssistManageService : IWebAssistManageService
    {
        #region IBLL

        IBDepartment IBDepartment { get; set; }
        IBDoctor IBDoctor { get; set; }
        IBPUser IBPUser { get; set; }
        IBLL.WebAssist.IBNPUser IBNPUser { get; set; }
        IBWardType IBWardType { get; set; }
        IBDepartmentUser IBDepartmentUser { get; set; }

        IBSCOperation IBSCOperation { get; set; }
        IBBTemplate IBBTemplate { get; set; }
        IBBUserUIConfig IBBUserUIConfig { get; set; }
        IBSCRecordType IBSCRecordType { get; set; }
        IBSCRecordTypeItem IBSCRecordTypeItem { get; set; }
        IBSCRecordPhrase IBSCRecordPhrase { get; set; }
        IBSCRecordDtl IBSCRecordDtl { get; set; }
        IBSCRecordItemLink IBSCRecordItemLink { get; set; }
        IBSCBarCodeRules IBSCBarCodeRules { get; set; }
        IBBLodopTemplet IBBLodopTemplet { get; set; }
        IBSCInterfaceMaping IBSCInterfaceMaping { get; set; }

        IBGKSampleRequestForm IBGKSampleRequestForm { get; set; }
        IBGKDeptAutoCheckLink IBGKDeptAutoCheckLink { get; set; }
        IBGKBarcode IBGKBarcode { get; set; }
        IBPGroup IBPGroup { get; set; }
        IBTestItem IBTestItem { get; set; }
        IBSampleType IBSampleType { get; set; }

        IBLisSyncHisData IBLisSyncHisData { get; set; }

        #endregion

        #region LIS同步HIS
        public BaseResultBool WA_UDTO_SaveLisSyncDpetHisData()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (SysRunHelp.LisSyncHisData == true)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "系统正在同步中，请稍等！";
                return tempBaseResultBool;
            }
            try
            {
                SysRunHelp.LisSyncHisData = true;
                tempBaseResultBool = IBLisSyncHisData.SaveLisSyncDpetHisData();
            }
            catch (Exception ex)
            {
                SysRunHelp.LisSyncHisData = false;
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SaveLisSyncDpetHisDataInfo.Error" + ex.StackTrace);
            }
            SysRunHelp.LisSyncHisData = false;
            return tempBaseResultBool;
        }

        public BaseResultBool WA_UDTO_SaveLisSyncHisDataInfo()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (SysRunHelp.LisSyncHisData == true)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "系统正在同步中，请稍等！";
                return tempBaseResultBool;
            }
            try
            {
                SysRunHelp.LisSyncHisData = true;
                tempBaseResultBool = IBLisSyncHisData.SaveLisSyncHisDataInfo();
            }
            catch (Exception ex)
            {
                SysRunHelp.LisSyncHisData = false;
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SaveLisSyncHisDataInfo.Error" + ex.StackTrace);
            }
            SysRunHelp.LisSyncHisData = false;
            return tempBaseResultBool;
        }

        #endregion

        #region 6.6登录处理
        /// <summary>
        /// 重置用户名密码
        /// </summary>
        /// <param name="longRBACUserID">用户ID</param>
        /// <returns>六位的新密码</returns>
        public BaseResultDataValue WA_RJ_ResetAccountPassword(long longRBACUserID, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                string tempStr = IBPUser.RJ_ResetAccountPassword(longRBACUserID, empID, empName);
                tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr("AccountPassword:" + "\"" + tempStr + "\"");
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue WA_UDTO_AddPUserOfReg(PUser entity)
        {
            int curPuserId = IBPUser.GetMaxId();
            if (curPuserId != -1)
            {
                entity.Id = curPuserId + 1;
            }
            IBPUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {

                tempBaseResultDataValue = IBPUser.AddPUserOfReg();
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
        public BaseResultBool WA_UDTO_UpdatePUserByBindDept(PUser entity, string fields, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            IBPUser.Entity = entity;
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
                            //更新Session
                            if (entity.Department != null)
                            {
                                UpdateSession(IBPUser.Entity, entity.Department.Id);
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
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        private void UpdateSession(PUser puser, int deptId)
        {
            Department dept = null;
            dept = IBDepartment.Get(deptId);
            if (dept != null)
            {
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, dept.Id);//部门ID
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, dept.CName);//部门名称
                ZhiFang.WebAssist.Common.Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, dept.Id.ToString());//部门ID
                ZhiFang.WebAssist.Common.Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, dept.CName);//部门名称

                IBPUser.SaveDepartmentUser(puser, dept);
            }
        }
        public bool WA_BA_LoginValidate(string strUserAccount, string strPassWord, bool isValidate)
        {
            bool tempBool = false;
            if (strUserAccount.Trim() == DicCookieSession.SuperUser && strPassWord == DicCookieSession.SuperUserPwd)
            {
                tempBool = true;
                //if (!isValidate)
                //SetUserSession(null);
            }
            else
            {
                IList<PUser> tempRBACUser = IBPUser.SearchListByHQL(string.Format("puser.ShortCode = '{0}' and puser.Password = '{1}'", strUserAccount, IBPUser.CovertPassWord(strPassWord)));
                if (tempRBACUser.Count == 1)
                {
                    tempBool = true;
                }
                else
                {
                    tempBool = false;
                }
            }
            return tempBool;
        }
        public BaseResultBool WA_UDTO_SaveSyncGKBarcodeInfo()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (SysRunHelp.LisSyncGKData == true)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "系统正在同步中，请稍等！";
                return tempBaseResultBool;
            }
            try
            {
                SysRunHelp.LisSyncGKData = true;
                tempBaseResultBool = IBGKBarcode.SaveSyncGKBarcodeInfo();
            }
            catch (Exception ex)
            {
                SysRunHelp.LisSyncGKData = false;
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SaveSyncGKBarcodeInfo.Error" + ex.StackTrace);
            }
            SysRunHelp.LisSyncGKData = false;
            return tempBaseResultBool;
        }
        public BaseResultBool WA_UDTO_SaveSyncTestTypeInfo()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (SysRunHelp.LisSyncGKData == true)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "系统正在同步中，请稍等！";
                return tempBaseResultBool;
            }
            try
            {
                SysRunHelp.LisSyncGKData = true;
                tempBaseResultBool = IBGKBarcode.SaveSyncTestTypeInfo();
            }
            catch (Exception ex)
            {
                SysRunHelp.LisSyncGKData = false;
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SaveSyncTestTypeInfo.Error" + ex.StackTrace);
            }
            SysRunHelp.LisSyncGKData = false;
            return tempBaseResultBool;
        }
        public BaseResultBool WA_UDTO_SaveSyncDeptPhraseInfo()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (SysRunHelp.LisSyncGKData == true)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "系统正在同步中，请稍等！";
                return tempBaseResultBool;
            }
            try
            {
                SysRunHelp.LisSyncGKData = true;
                tempBaseResultBool = IBGKBarcode.SaveSyncDeptPhraseInfo();
            }
            catch (Exception ex)
            {
                SysRunHelp.LisSyncGKData = false;
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SaveSyncDeptPhraseInfo.Error" + ex.StackTrace);
            }
            SysRunHelp.LisSyncGKData = false;
            return tempBaseResultBool;
        }
        public BaseResultBool WA_UDTO_SaveSyncGKBarRedInfo()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (SysRunHelp.LisSyncGKData == true)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "系统正在同步中，请稍等！";
                return tempBaseResultBool;
            }
            try
            {
                SysRunHelp.LisSyncGKData = true;
                tempBaseResultBool = IBGKBarcode.SaveSyncGKBarRedInfo();
            }
            catch (Exception ex)
            {
                SysRunHelp.LisSyncGKData = false;
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SaveSyncTestTypeInfo.Error" + ex.StackTrace);
            }
            SysRunHelp.LisSyncGKData = false;
            return tempBaseResultBool;
        }
        public BaseResultBool WA_UDTO_SaveSyncDeptUserOfGKForm()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (SysRunHelp.LisSyncGKData == true)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "系统正在同步中，请稍等！";
                return tempBaseResultBool;
            }
            try
            {
                SysRunHelp.LisSyncGKData = true;
                tempBaseResultBool = IBDepartmentUser.SaveSyncDeptUserOfGKForm();
            }
            catch (Exception ex)
            {
                SysRunHelp.LisSyncGKData = false;
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SaveSyncDeptUserOfGKForm.Error" + ex.StackTrace);
            }
            SysRunHelp.LisSyncGKData = false;
            return tempBaseResultBool;
        }

        public BaseResultBool WA_UDTO_AddInitysOfLabId()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long labId = 0;
                if (labId <= 0)
                    labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                tempBaseResultBool = IBSCBarCodeRules.AddInitysOfLabId(labId);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("WA_UDTO_AddInitysOfLabId.Error" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
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
                SessionHelper.SetSessionValue(DicCookieSession.MonitorType, null);//感控监测类型
                SessionHelper.SetSessionValue(DicCookieSession.OldModuleID, null);
                SessionHelper.SetSessionValue(DicCookieSession.CurModuleOperID, null);

                Common.Cookie.CookieHelper.Remove(SysPublicSet.SysDicCookieSession.LabID);//实验室ID
                Common.Cookie.CookieHelper.Remove(DicCookieSession.UserID);//账户ID
                Common.Cookie.CookieHelper.Remove(DicCookieSession.UserAccount);//账户名
                Common.Cookie.CookieHelper.Remove(DicCookieSession.UseCode);//账户代码
                Common.Cookie.CookieHelper.Remove(DicCookieSession.EmployeeID);// 员工ID
                Common.Cookie.CookieHelper.Remove(DicCookieSession.EmployeeName);// 员工姓名
                Common.Cookie.CookieHelper.Remove(DicCookieSession.EmployeeUseCode);// 员工代码
                Common.Cookie.CookieHelper.Remove(DicCookieSession.MonitorType);//部门感控监测类型
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
        public BaseResultDataValue WA_UDTO_SearchRBACUserOfPUserByHQL(bool isliip, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACUser> tempEntityList = new EntityList<RBACUser>();
            try
            {
                if (isliip == true)
                {
                    tempBaseResultDataValue = WA_UDTO_SearchRBACUserOFLIMPByHQL("", page, limit, fields, where, sort, isPlanish);
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
        public BaseResultDataValue WA_UDTO_SearchRBACUserOfNPUserByHQL(bool isliip, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACUser> tempEntityList = new EntityList<RBACUser>();
            try
            {
                if (isliip == true)
                {
                    tempBaseResultDataValue = WA_UDTO_SearchRBACUserOFLIMPByHQL("", page, limit, fields, where, sort, isPlanish);
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
        public BaseResultDataValue WA_UDTO_SearchPUserByFieldKey(string fieldKey, string fieldVal, string fields, bool isPlanish)
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
        public BaseResultDataValue WA_UDTO_SearchRBACUserByFieldKey(string fieldKey, string fieldVal, string fields, bool isPlanish)
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
        public BaseResultDataValue WA_UDTO_SearchRBACUserOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish)
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
        public BaseResultDataValue WA_UDTO_SearchHREmployeeOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish)
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
        public BaseResultDataValue WA_UDTO_SyncPuserListOfHREmployeeToLIMP(string platformUrl, string syncType)
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
        public BaseResultDataValue WA_UDTO_SyncPuserListOfRBACUseToLIMP(string platformUrl, string syncType)
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
                    ZhiFang.Common.Log.Log.Debug("WA_UDTO_SearchHRDeptOFLIMPByHQL.sort.UrlEncode:" + sort1);
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
        public BaseResultDataValue WA_UDTO_SyncDeptListToLIMP(string platformUrl, string syncType)
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

        #region PUser
        //Add  PUser
        public BaseResultDataValue WA_UDTO_AddPUser(PUser entity)
        {
            IBPUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPUser.AddPUserOfReg();
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
        public BaseResultBool WA_UDTO_UpdatePUser(PUser entity)
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
        public BaseResultBool WA_UDTO_UpdatePUserByField(PUser entity, string fields, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (fields.Contains("ShortCode"))
            {
                //验证账号是否重复
                BaseResultDataValue brdv = IBPUser.GetPUserByShortCode(entity, "edit");
                if (brdv.success == false)
                {
                    tempBaseResultBool.success = brdv.success;
                    tempBaseResultBool.ErrorInfo = brdv.ErrorInfo;
                    return tempBaseResultBool;
                }
            }


            if (fields.Contains("Password"))
                entity.Password = IBPUser.CovertPassWord(entity.Password);
            IBPUser.Entity = entity;

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
        public BaseResultBool WA_UDTO_DelPUser(long longPUserID)
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

        public BaseResultDataValue WA_UDTO_SearchPUser(PUser entity)
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

        public BaseResultDataValue WA_UDTO_SearchPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
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

        public BaseResultDataValue WA_UDTO_SearchPUserById(long id, string fields, bool isPlanish)
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

        #region BUserUIConfig
        //Add  BUserUIConfig
        public BaseResultDataValue WA_UDTO_AddBUserUIConfig(BUserUIConfig entity)
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
        public BaseResultBool WA_UDTO_UpdateBUserUIConfig(BUserUIConfig entity)
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
        public BaseResultBool WA_UDTO_UpdateBUserUIConfigByField(BUserUIConfig entity, string fields)
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
        public BaseResultBool WA_UDTO_DelBUserUIConfig(long longBUserUIConfigID)
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

        public BaseResultDataValue WA_UDTO_SearchBUserUIConfig(BUserUIConfig entity)
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

        public BaseResultDataValue WA_UDTO_SearchBUserUIConfigByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
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

        public BaseResultDataValue WA_UDTO_SearchBUserUIConfigById(long id, string fields, bool isPlanish)
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
        public BaseResultDataValue WA_UDTO_AddSCOperation(SCOperation entity)
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
        public BaseResultBool WA_UDTO_UpdateSCOperation(SCOperation entity)
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
        public BaseResultBool WA_UDTO_UpdateSCOperationByField(SCOperation entity, string fields)
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
        public BaseResultBool WA_UDTO_DelSCOperation(long longSCOperationID)
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

        public BaseResultDataValue WA_UDTO_SearchSCOperation(SCOperation entity)
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
        public BaseResultDataValue WA_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
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

        public BaseResultDataValue WA_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish)
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

        #region SCRecordType
        //Add  SCRecordType
        public BaseResultDataValue WA_UDTO_AddSCRecordType(SCRecordType entity)
        {
            IBSCRecordType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCRecordType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCRecordType.Get(IBSCRecordType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCRecordType.Entity);
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
        //Update  SCRecordType
        public BaseResultBool WA_UDTO_UpdateSCRecordType(SCRecordType entity)
        {
            IBSCRecordType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCRecordType
        public BaseResultBool WA_UDTO_UpdateSCRecordTypeByField(SCRecordType entity, string fields, long empID, string empName)
        {
            IBSCRecordType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCRecordType.Entity, fields);
                    if (tempArray != null)
                    {
                        SCRecordType serverDept = IBSCRecordType.Get(IBSCRecordType.Entity.Id);
                        tempBaseResultBool.success = IBSCRecordType.Update(tempArray);
                        if (tempBaseResultBool.success)
                        {
                            string[] arrFields = fields.Split(',');
                            IBSCRecordType.AddSCOperation(serverDept, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCRecordType.Edit();
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
        //Delele  SCRecordType
        public BaseResultBool WA_UDTO_DelSCRecordType(long longSCRecordTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordType.Remove(longSCRecordTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchSCRecordType(SCRecordType entity)
        {
            IBSCRecordType.Entity = entity;
            EntityList<SCRecordType> tempEntityList = new EntityList<SCRecordType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCRecordType.Search();
                tempEntityList.count = IBSCRecordType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordType>(tempEntityList);
                }
                catch (Exception ex)
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

        public BaseResultDataValue WA_UDTO_SearchSCRecordTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isTestItem)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCRecordType> tempEntityList = new EntityList<SCRecordType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCRecordType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCRecordType.SearchListByHQL(where, page, limit);
                }
                if (isTestItem == true)
                {
                    for (int i = 0; i < tempEntityList.list.Count; i++)
                    {
                        string code1 = tempEntityList.list[i].TestItemCode;
                        if (!string.IsNullOrEmpty(code1))
                        {
                            tempEntityList.list[i].TestItemCName = IBTestItem.GetTestItemCName(code1);
                        }

                        string code2 = tempEntityList.list[i].SampleTypeCode;
                        if (!string.IsNullOrEmpty(code2))
                        {
                            tempEntityList.list[i].SampleTypeCName = IBSampleType.GetSampleTypeCName(code2);
                        }


                    }
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordType>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSCRecordTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCRecordType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCRecordType>(tempEntity);
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

        #region SCRecordTypeItem
        //Add  SCRecordTypeItem
        public BaseResultDataValue WA_UDTO_AddSCRecordTypeItem(SCRecordTypeItem entity)
        {
            IBSCRecordTypeItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCRecordTypeItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCRecordTypeItem.Get(IBSCRecordTypeItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCRecordTypeItem.Entity);
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
        //Update  SCRecordTypeItem
        public BaseResultBool WA_UDTO_UpdateSCRecordTypeItem(SCRecordTypeItem entity)
        {
            IBSCRecordTypeItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordTypeItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCRecordTypeItem
        public BaseResultBool WA_UDTO_UpdateSCRecordTypeItemByField(SCRecordTypeItem entity, string fields, long empID, string empName)
        {
            IBSCRecordTypeItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCRecordTypeItem.Entity, fields);
                    if (tempArray != null)
                    {
                        SCRecordTypeItem serverDept = IBSCRecordTypeItem.Get(entity.Id);
                        tempBaseResultBool.success = IBSCRecordTypeItem.Update(tempArray);
                        if (tempBaseResultBool.success)
                        {
                            string[] arrFields = fields.Split(',');
                            IBSCRecordTypeItem.AddSCOperation(serverDept, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCRecordTypeItem.Edit();
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
        //Delele  SCRecordTypeItem
        public BaseResultBool WA_UDTO_DelSCRecordTypeItem(long longSCRecordTypeItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordTypeItem.Remove(longSCRecordTypeItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchSCRecordTypeItem(SCRecordTypeItem entity)
        {
            IBSCRecordTypeItem.Entity = entity;
            EntityList<SCRecordTypeItem> tempEntityList = new EntityList<SCRecordTypeItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCRecordTypeItem.Search();
                tempEntityList.count = IBSCRecordTypeItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordTypeItem>(tempEntityList);
                }
                catch (Exception ex)
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

        public BaseResultDataValue WA_UDTO_SearchSCRecordTypeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isTestItem)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCRecordTypeItem> tempEntityList = new EntityList<SCRecordTypeItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCRecordTypeItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCRecordTypeItem.SearchListByHQL(where, page, limit);
                }
                if (isTestItem == true)
                {
                    for (int i = 0; i < tempEntityList.list.Count; i++)
                    {
                        string code1 = tempEntityList.list[i].ItemCode;
                        if (string.IsNullOrEmpty(code1)) continue;

                        tempEntityList.list[i].TestItemCName = IBTestItem.GetTestItemCName(code1);

                    }
                }

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordTypeItem>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSCRecordTypeItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCRecordTypeItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCRecordTypeItem>(tempEntity);
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

        #region SCRecordPhrase
        //Add  SCRecordPhrase
        public BaseResultDataValue WA_UDTO_AddSCRecordPhrase(SCRecordPhrase entity)
        {
            IBSCRecordPhrase.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCRecordPhrase.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCRecordPhrase.Get(IBSCRecordPhrase.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCRecordPhrase.Entity);
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
        //Update  SCRecordPhrase
        public BaseResultBool WA_UDTO_UpdateSCRecordPhrase(SCRecordPhrase entity)
        {
            IBSCRecordPhrase.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordPhrase.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCRecordPhrase
        public BaseResultBool WA_UDTO_UpdateSCRecordPhraseByField(SCRecordPhrase entity, string fields, long empID, string empName)
        {
            IBSCRecordPhrase.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    SCRecordPhrase serverDept = IBSCRecordPhrase.Get(entity.Id);
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCRecordPhrase.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCRecordPhrase.Update(tempArray);
                        if (tempBaseResultBool.success)
                        {
                            string[] arrFields = fields.Split(',');
                            IBSCRecordPhrase.AddSCOperation(serverDept, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCRecordPhrase.Edit();
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
        //Delele  SCRecordPhrase
        public BaseResultBool WA_UDTO_DelSCRecordPhrase(long longSCRecordPhraseID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordPhrase.Remove(longSCRecordPhraseID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchSCRecordPhrase(SCRecordPhrase entity)
        {
            IBSCRecordPhrase.Entity = entity;
            EntityList<SCRecordPhrase> tempEntityList = new EntityList<SCRecordPhrase>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCRecordPhrase.Search();
                tempEntityList.count = IBSCRecordPhrase.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordPhrase>(tempEntityList);
                }
                catch (Exception ex)
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

        public BaseResultDataValue WA_UDTO_SearchSCRecordPhraseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCRecordPhrase> tempEntityList = new EntityList<SCRecordPhrase>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCRecordPhrase.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCRecordPhrase.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordPhrase>(tempEntityList);
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
        public BaseResultDataValue WA_UDTO_SearchSCRecordPhraseOfGKByHQL(int page, int limit, string where, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //EntityList<SCRecordPhrase> tempEntityList = new EntityList<SCRecordPhrase>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempBaseResultDataValue = IBSCRecordPhrase.SearchSCRecordPhraseOfGKByHQL(page, limit, where, sort);

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue WA_UDTO_SearchSCRecordPhraseById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCRecordPhrase.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCRecordPhrase>(tempEntity);
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

        #region SCRecordDtl
        //Add  SCRecordDtl
        public BaseResultDataValue WA_UDTO_AddSCRecordDtl(SCRecordDtl entity)
        {
            IBSCRecordDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCRecordDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCRecordDtl.Get(IBSCRecordDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCRecordDtl.Entity);
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
        //Update  SCRecordDtl
        public BaseResultBool WA_UDTO_UpdateSCRecordDtl(SCRecordDtl entity)
        {
            IBSCRecordDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCRecordDtl
        public BaseResultBool WA_UDTO_UpdateSCRecordDtlByField(SCRecordDtl entity, string fields)
        {
            IBSCRecordDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCRecordDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCRecordDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCRecordDtl.Edit();
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
        //Delele  SCRecordDtl
        public BaseResultBool WA_UDTO_DelSCRecordDtl(long longSCRecordDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordDtl.Remove(longSCRecordDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchSCRecordDtl(SCRecordDtl entity)
        {
            IBSCRecordDtl.Entity = entity;
            EntityList<SCRecordDtl> tempEntityList = new EntityList<SCRecordDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCRecordDtl.Search();
                tempEntityList.count = IBSCRecordDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordDtl>(tempEntityList);
                }
                catch (Exception ex)
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

        public BaseResultDataValue WA_UDTO_SearchSCRecordDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCRecordDtl> tempEntityList = new EntityList<SCRecordDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCRecordDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCRecordDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordDtl>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSCRecordDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCRecordDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCRecordDtl>(tempEntity);
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

        #region SCRecordItemLink
        //Add  SCRecordItemLink
        public BaseResultDataValue WA_UDTO_AddSCRecordItemLink(SCRecordItemLink entity)
        {
            IBSCRecordItemLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCRecordItemLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCRecordItemLink.Get(IBSCRecordItemLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCRecordItemLink.Entity);
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
        //Update  SCRecordItemLink
        public BaseResultBool WA_UDTO_UpdateSCRecordItemLink(SCRecordItemLink entity)
        {
            IBSCRecordItemLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordItemLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCRecordItemLink
        public BaseResultBool WA_UDTO_UpdateSCRecordItemLinkByField(SCRecordItemLink entity, string fields)
        {
            IBSCRecordItemLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCRecordItemLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCRecordItemLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCRecordItemLink.Edit();
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
        //Delele  SCRecordItemLink
        public BaseResultBool WA_UDTO_DelSCRecordItemLink(long longSCRecordItemLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCRecordItemLink.Remove(longSCRecordItemLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchSCRecordItemLink(SCRecordItemLink entity)
        {
            IBSCRecordItemLink.Entity = entity;
            EntityList<SCRecordItemLink> tempEntityList = new EntityList<SCRecordItemLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCRecordItemLink.Search();
                tempEntityList.count = IBSCRecordItemLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordItemLink>(tempEntityList);
                }
                catch (Exception ex)
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

        public BaseResultDataValue WA_UDTO_SearchSCRecordItemLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isTestItem)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCRecordItemLink> tempEntityList = new EntityList<SCRecordItemLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCRecordItemLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCRecordItemLink.SearchListByHQL(where, page, limit);
                }
                if (isTestItem == true)
                {
                    for (int i = 0; i < tempEntityList.list.Count; i++)
                    {
                        string code1 = tempEntityList.list[i].TestItemCode;
                        if (string.IsNullOrEmpty(code1)) continue;

                        tempEntityList.list[i].TestItemCName = IBTestItem.GetTestItemCName(code1);

                    }
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCRecordItemLink>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSCRecordItemLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCRecordItemLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCRecordItemLink>(tempEntity);
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

        #region BLodopTemplet
        //Add  BLodopTemplet
        public BaseResultDataValue WA_UDTO_AddBLodopTemplet(BLodopTemplet entity)
        {
            IBBLodopTemplet.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBLodopTemplet.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBLodopTemplet.Get(IBBLodopTemplet.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBLodopTemplet.Entity);
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
        //Update  BLodopTemplet
        public BaseResultBool WA_UDTO_UpdateBLodopTemplet(BLodopTemplet entity)
        {
            IBBLodopTemplet.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBLodopTemplet.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BLodopTemplet
        public BaseResultBool WA_UDTO_UpdateBLodopTempletByField(BLodopTemplet entity, string fields)
        {
            IBBLodopTemplet.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBLodopTemplet.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBLodopTemplet.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBLodopTemplet.Edit();
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
        //Delele  BLodopTemplet
        public BaseResultBool WA_UDTO_DelBLodopTemplet(long longBLodopTempletID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBLodopTemplet.Remove(longBLodopTempletID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchBLodopTemplet(BLodopTemplet entity)
        {
            IBBLodopTemplet.Entity = entity;
            EntityList<BLodopTemplet> tempEntityList = new EntityList<BLodopTemplet>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBLodopTemplet.Search();
                tempEntityList.count = IBBLodopTemplet.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BLodopTemplet>(tempEntityList);
                }
                catch (Exception ex)
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

        public BaseResultDataValue WA_UDTO_SearchBLodopTempletByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BLodopTemplet> tempEntityList = new EntityList<BLodopTemplet>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBLodopTemplet.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBLodopTemplet.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BLodopTemplet>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchBLodopTempletById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBLodopTemplet.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BLodopTemplet>(tempEntity);
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

        #region SCBarCodeRules
        //Add  SCBarCodeRules
        public BaseResultDataValue WA_UDTO_AddSCBarCodeRules(SCBarCodeRules entity)
        {
            IBSCBarCodeRules.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCBarCodeRules.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCBarCodeRules.Get(IBSCBarCodeRules.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCBarCodeRules.Entity);
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
        //Update  SCBarCodeRules
        public BaseResultBool WA_UDTO_UpdateSCBarCodeRules(SCBarCodeRules entity)
        {
            IBSCBarCodeRules.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCBarCodeRules.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCBarCodeRules
        public BaseResultBool WA_UDTO_UpdateSCBarCodeRulesByField(SCBarCodeRules entity, string fields)
        {
            IBSCBarCodeRules.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCBarCodeRules.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCBarCodeRules.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCBarCodeRules.Edit();
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
        //Delele  SCBarCodeRules
        public BaseResultBool WA_UDTO_DelSCBarCodeRules(long longSCBarCodeRulesID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCBarCodeRules.Remove(longSCBarCodeRulesID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchSCBarCodeRules(SCBarCodeRules entity)
        {
            IBSCBarCodeRules.Entity = entity;
            EntityList<SCBarCodeRules> tempEntityList = new EntityList<SCBarCodeRules>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCBarCodeRules.Search();
                tempEntityList.count = IBSCBarCodeRules.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCBarCodeRules>(tempEntityList);
                }
                catch (Exception ex)
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

        public BaseResultDataValue WA_UDTO_SearchSCBarCodeRulesByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCBarCodeRules> tempEntityList = new EntityList<SCBarCodeRules>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCBarCodeRules.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCBarCodeRules.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCBarCodeRules>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSCBarCodeRulesById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCBarCodeRules.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCBarCodeRules>(tempEntity);
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

        #region GKSampleRequestForm
        //Add  GKSampleRequestFormAndDtl
        public BaseResultDataValue WA_UDTO_AddGKSampleRequestFormAndDtl(GKSampleRequestForm entity, IList<SCRecordDtl> dtlEntityList, long empID, string empName)
        {
            IBGKSampleRequestForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBGKSampleRequestForm.AddGKSampleRequestFormAndDtl(entity, dtlEntityList, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBGKSampleRequestForm.Get(IBGKSampleRequestForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBGKSampleRequestForm.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("WA_UDTO_AddGKSampleRequestFormAndDtl.Error.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("WA_UDTO_AddGKSampleRequestFormAndDtl.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        //Update  GKSampleRequestFormAndDtl
        public BaseResultBool WA_UDTO_UpdateGKSampleRequestFormAndDtlByField(GKSampleRequestForm entity, string fields, IList<SCRecordDtl> dtlEntityList, long empID, string empName)
        {
            IBGKSampleRequestForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGKSampleRequestForm.Entity, fields);
                    //if (tempArray != null)
                    //{
                    //    tempBaseResultBool.success = IBGKSampleRequestForm.Update(tempArray);
                    //}

                    tempBaseResultBool = IBGKSampleRequestForm.EditGKSampleRequestFormAndDtl(entity, tempArray, dtlEntityList, empID, empName);

                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBGKSampleRequestForm.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("WA_UDTO_UpdateGKSampleRequestFormAndDtlByField.Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Add  GKSampleRequestForm
        public BaseResultDataValue WA_UDTO_AddGKSampleRequestForm(GKSampleRequestForm entity)
        {
            IBGKSampleRequestForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBGKSampleRequestForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBGKSampleRequestForm.Get(IBGKSampleRequestForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBGKSampleRequestForm.Entity);
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
        //Update  GKSampleRequestForm
        public BaseResultBool WA_UDTO_UpdateGKSampleRequestForm(GKSampleRequestForm entity)
        {
            IBGKSampleRequestForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBGKSampleRequestForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  GKSampleRequestForm
        public BaseResultBool WA_UDTO_UpdateGKSampleRequestFormByField(GKSampleRequestForm entity, string fields, long empID, string empName)
        {
            IBGKSampleRequestForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGKSampleRequestForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool = IBGKSampleRequestForm.EditGKSampleRequestForm(entity, tempArray, empID, empName);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBGKSampleRequestForm.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("WA_UDTO_UpdateGKSampleRequestFormByField.Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  GKSampleRequestForm
        public BaseResultBool WA_UDTO_DelGKSampleRequestForm(long longGKSampleRequestFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBGKSampleRequestForm.Remove(longGKSampleRequestFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchGKSampleRequestForm(GKSampleRequestForm entity)
        {
            IBGKSampleRequestForm.Entity = entity;
            EntityList<GKSampleRequestForm> tempEntityList = new EntityList<GKSampleRequestForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBGKSampleRequestForm.Search();
                tempEntityList.count = IBGKSampleRequestForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GKSampleRequestForm>(tempEntityList);
                }
                catch (Exception ex)
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

        public BaseResultDataValue WA_UDTO_SearchGKSampleRequestFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<GKSampleRequestForm> tempEntityList = new EntityList<GKSampleRequestForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBGKSampleRequestForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBGKSampleRequestForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GKSampleRequestForm>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SearchGKSampleRequestFormAndDtlByHQL.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue WA_UDTO_SearchGKSampleRequestFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBGKSampleRequestForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<GKSampleRequestForm>(tempEntity);
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

        public BaseResultDataValue WA_UDTO_SearchGKSampleRequestFormAndDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<GKSampleRequestForm> tempEntityList = new EntityList<GKSampleRequestForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    //tempEntityList = IBGKSampleRequestForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    //tempEntityList = IBGKSampleRequestForm.SearchListByHQL(where, page, limit);
                }
                tempEntityList = IBGKSampleRequestForm.SearchGKSampleRequestFormAndDtlByHQL(where, sort, page, limit, false);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GKSampleRequestForm>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SearchGKSampleRequestFormAndDtlByHQL.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue WA_UDTO_SearchGKSampleRequestFormAndDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBGKSampleRequestForm.GetGKSampleRequestFormAndDtlById(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<GKSampleRequestForm>(tempEntity);
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
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SearchGKSampleRequestFormAndDtlById.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region GKDeptAutoCheckLink
        //Add  GKDeptAutoCheckLink
        public BaseResultDataValue WA_UDTO_AddGKDeptAutoCheckLink(GKDeptAutoCheckLink entity)
        {
            IBGKDeptAutoCheckLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBGKDeptAutoCheckLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBGKDeptAutoCheckLink.Get(IBGKDeptAutoCheckLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBGKDeptAutoCheckLink.Entity);
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
        //Update  GKDeptAutoCheckLink
        public BaseResultBool WA_UDTO_UpdateGKDeptAutoCheckLink(GKDeptAutoCheckLink entity)
        {
            IBGKDeptAutoCheckLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBGKDeptAutoCheckLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  GKDeptAutoCheckLink
        public BaseResultBool WA_UDTO_UpdateGKDeptAutoCheckLinkByField(GKDeptAutoCheckLink entity, string fields)
        {
            IBGKDeptAutoCheckLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGKDeptAutoCheckLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBGKDeptAutoCheckLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBGKDeptAutoCheckLink.Edit();
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
        //Delele  GKDeptAutoCheckLink
        public BaseResultBool WA_UDTO_DelGKDeptAutoCheckLink(long longGKDeptAutoCheckLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBGKDeptAutoCheckLink.Remove(longGKDeptAutoCheckLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchGKDeptAutoCheckLink(GKDeptAutoCheckLink entity)
        {
            IBGKDeptAutoCheckLink.Entity = entity;
            EntityList<GKDeptAutoCheckLink> tempEntityList = new EntityList<GKDeptAutoCheckLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBGKDeptAutoCheckLink.Search();
                tempEntityList.count = IBGKDeptAutoCheckLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GKDeptAutoCheckLink>(tempEntityList);
                }
                catch (Exception ex)
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

        public BaseResultDataValue WA_UDTO_SearchGKDeptAutoCheckLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<GKDeptAutoCheckLink> tempEntityList = new EntityList<GKDeptAutoCheckLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBGKDeptAutoCheckLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBGKDeptAutoCheckLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GKDeptAutoCheckLink>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchGKDeptAutoCheckLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBGKDeptAutoCheckLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<GKDeptAutoCheckLink>(tempEntity);
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
        public BaseResultDataValue WA_UDTO_SearchBDictMapingVOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string deveCode, string useCode, string mapingWhere, long objectTypeId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BDictMapingVO> tempEntityList = new EntityList<BDictMapingVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBSCInterfaceMaping.GetBDictMapingVOListByHQL(where, sort, page, limit, deveCode, useCode, mapingWhere, objectTypeId);
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
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SearchBDictMapingVOByHQL.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region PDF清单报表
        public BaseResultDataValue WA_UDTO_SearchPublicTemplateFileInfoByType(string publicTemplateDir)
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
        public BaseResultBool WA_UDTO_AddBTemplateOfPublicTemplate(string entityList, long labId, string labCName)
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
        public BaseResultDataValue WA_UDTO_SearchBTemplateByLabIdAndType(long labId, long breportType, string publicTemplateDir)
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

        public Stream WA_UDTO_SearchBusinessReportOfPdfById(string reaReportClass, string breportType, string id, long operateType, string frx)
        {
            Stream fileStream = null;
            string fileName = "", info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID)) employeeID = "-1";
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string labIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID);
                long labId = 0;
                if (!string.IsNullOrEmpty(labIdStr)) labId = long.Parse(labIdStr);
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (breportType == BTemplateType.入库清单.Key)
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

                else if (breportType == BTemplateType.领用清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.领用清单.Key];
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

        public Stream WA_UDTO_SearchGKSampleRequestFormOfPdfByHQL(long operateType, string breportType, string groupType, string docVO, string where, string sort, string frx)
        {
            Stream fileStream = null;
            string fileName = "", info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID)) employeeID = "-1";
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string labIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID);
                long labId = 0;
                if (!string.IsNullOrEmpty(labIdStr)) labId = long.Parse(labIdStr);
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);

                BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.院感登记.Key];

                info = dicEntity.Name;
                breportType = dicEntity.Name;
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                fileStream = IBGKSampleRequestForm.SearchGKSampleRequestFormOfPdfByHQL(labId, labCName, breportType, groupType, docVO, where, sort, frx, ref fileName);

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
        public Stream WA_UDTO_SearchBusinessReportOfExcelById(long operateType, string breportType, long id, string frx)
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
                else if (breportType == BTemplateType.样本接收.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.样本接收.Key];
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
        public Stream WA_UDTO_SearchGKSampleRequestFormOfExcelByHql(long operateType, string breportType, string groupType, string docVO, string where, string sort, string frx)
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

                BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.院感登记.Key];

                info = dicEntity.Name;
                breportType = dicEntity.Name;
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                fileStream = IBGKSampleRequestForm.SearchGKSampleRequestFormOfExcelByHql(labId, labCName, breportType, groupType, docVO, where, sort, frx, ref fileName);

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

        #region 统计报表
        public BaseResultDataValue WA_UDTO_SearchGKListByHQLInfectionOfDept(int page, int limit, string fields, string docVO, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<GKOfDeptEvaluation> tempEntityList = new EntityList<GKOfDeptEvaluation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBGKSampleRequestForm.SearchGKListByHQLInfectionOfDept(docVO, where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GKOfDeptEvaluation>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SearchGKListByHQLInfectionOfDept.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue WA_UDTO_SearchGKListByHQLInfectionOfQuarterly(int page, int limit, string fields, string docVO, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<GKOfDeptEvaluation> tempEntityList = new EntityList<GKOfDeptEvaluation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBGKSampleRequestForm.SearchGKListByHQLInfectionOfQuarterly(docVO, where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GKOfDeptEvaluation>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SearchGKListByHQLInfectionOfQuarterly.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue WA_UDTO_SearchGKListByHQLInfectionOfEvaluation(int page, int limit, string fields, string docVO, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<GKOfDeptEvaluation> tempEntityList = new EntityList<GKOfDeptEvaluation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBGKSampleRequestForm.SearchGKListByHQLInfectionOfEvaluation(docVO, where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GKOfDeptEvaluation>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SearchGKListByHQLInfectionOfEvaluation.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region 双列表选择左查询定制服务
        public BaseResultDataValue WA_UDTO_SearchDepartmentByAutoCheckLinkHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<Department> entityList = new EntityList<Department>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                entityList = IBGKDeptAutoCheckLink.SearchDepartmentByLinkHQL(page, limit, where, linkWhere, sort);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<Department>(entityList);
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
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SearchDepartmentByAutoCheckLinkHQL.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue WA_UDTO_SearchSCRecordTypeItemByLinkHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SCRecordTypeItem> entityList = new EntityList<SCRecordTypeItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                entityList = IBSCRecordItemLink.SearchSCRecordTypeItemByLinkHQL(page, limit, where, linkWhere, sort);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCRecordTypeItem>(entityList);
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
                ZhiFang.Common.Log.Log.Error("WA_UDTO_SearchBloodChargeItemByChargeGItemHQL.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 定制服务
        public BaseResultDataValue WA_UDTO_GetGKWarningAlertInfo()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                baseResultDataValue = IBGKSampleRequestForm.GetGKWarningAlertInfo(empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取预警信息错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

    }
}
