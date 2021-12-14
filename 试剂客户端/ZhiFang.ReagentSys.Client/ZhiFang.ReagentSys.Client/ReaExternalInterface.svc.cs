using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using ZhiFang.ReagentSys.Client.ServerContract;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;
using System.Globalization;

namespace ZhiFang.ReagentSys.Client
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReaExternalInterface : IReaExternalInterface
    {
        IBReaGoods IBReaGoods { get; set; }
        IBHRDept IBHRDept { get; set; }
        IBRBACUser IBRBACUser { get; set; }
        IBReaStorage IBReaStorage { get; set; }
        IBHREmployee IBHREmployee { get; set; }
        IBReaCenOrg IBReaCenOrg { get; set; }
        IBCenOrg IBCenOrg { get; set; }
        IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }
        IBRBACModule IBRBACModule { get; set; }

        public BaseResultDataValue Test()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            return baseResultDataValue;
        }


        /// <summary>
        /// 安阳市第六医院一体化平台统一登录定制接口
        /// signKey（签名密钥，采用 32 位 UUID 格式），由平台分配给各业务系统
        /// 
        /// 1.参数验证；
        /// 2.将orgCode=001&staffCode=1002&deptCode=003&timestamp=20210130152050&signKey=abcdefghijklmnoprrstuvwxyzabcdef，进行md5加密，生成加密串；
        /// 3.加密串和sign值比对一致则成功，不一致返回错误；
        /// 4.校验成功后，根据工号查询用户表，存在用户，返回成功，不存在返回失败；
        /// </summary>
        /// <param name="orgCode">医院代码</param>
        /// <param name="staffCode">员工工号</param>
        /// <param name="deptCode">科室/病区代码</param>
        /// <param name="timestamp">时间戳，如：20210130152050，采用24小时制，和服务器时间比对，时间差异在5分钟内可视为时间一致；不一致，可视为参数无效，做登录失败处理。</param>
        /// <param name="sign">签名值</param>
        /// <returns>BaseResultBool</returns>
        public BaseResultBool RBAC_BA_AnYangLiuYuanUnifiedLogin(string orgCode, string staffCode, string deptCode, string timestamp, string sign)
        {
            Log.Info(string.Format("安阳市第六医院一体化平台统一登录定制接口执行开始，参数：orgCode={0}，staffCode={1}，deptCode={2}，timestamp={3}，sign={4}", orgCode, staffCode, deptCode, timestamp, sign));
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                #region 参数校验
                if (string.IsNullOrWhiteSpace(orgCode))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "医院代码参数错误！";
                    return tempBaseResultBool;
                }
                if (string.IsNullOrWhiteSpace(staffCode))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "员工工号参数错误！";
                    return tempBaseResultBool;
                }
                if (string.IsNullOrWhiteSpace(deptCode))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "科室/病区代码参数错误！";
                    return tempBaseResultBool;
                }
                if (string.IsNullOrWhiteSpace(timestamp))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "时间戳参数错误！";
                    return tempBaseResultBool;
                }
                if (string.IsNullOrWhiteSpace(sign))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "sign签名值参数错误！";
                    return tempBaseResultBool;
                }
                #endregion

                #region 时间戳和服务器时间比对，时间差异>5分钟，返回失败
                try
                {
                    DateTime dtTimeStamp = DateTime.ParseExact(timestamp, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                    DateTime dtNowTime = DateTime.Now;
                    TimeSpan ts = dtNowTime.Subtract(dtTimeStamp);//时间差
                    if (Math.Abs(ts.Minutes) > 5)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "时间戳参数无效！";
                        return tempBaseResultBool;
                    }
                    Log.Info("时间戳和当前时间比对，时间差<5分钟，验证通过！");
                }
                catch (Exception ex1)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "时间戳参数格式错误！";
                    Log.Error(tempBaseResultBool.ErrorInfo, ex1);
                    return tempBaseResultBool;
                }
                #endregion

                #region sign签名校验
                string signKey = ZhiFang.Common.Public.ConfigHelper.GetConfigString("SignKey");
                Log.Info("签名密钥SignKey：" + signKey);
                string tempSign = string.Format("orgCode={0}&staffCode={1}&deptCode={2}&timestamp={3}&signKey={4}", orgCode, staffCode, deptCode, timestamp, signKey);
                Log.Info("MD5加密前字符串：" + tempSign);
                tempSign = SecurityHelp.NetMd5EncryptStr32(tempSign);
                Log.Info("MD5加密后字符串：" + tempSign);
                if (tempSign.Trim().ToLower() != sign.Trim().ToLower())
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "sign签名不一致，校验失败！";
                    Log.Error(tempBaseResultBool.ErrorInfo);
                    return tempBaseResultBool;
                }
                Log.Info("sign签名校验成功!");
                #endregion

                #region 根据员工工号查询账户表
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(staffCode);
                if (tempRBACUser.Count == 1)
                {
                    if (tempRBACUser[0].IsUse.HasValue && tempRBACUser[0].IsUse.Value)
                    {
                        if (tempRBACUser[0].HREmployee.IsUse.HasValue && tempRBACUser[0].HREmployee.IsUse.Value && tempRBACUser[0].HREmployee.IsEnabled == 1)
                        {
                            bool b = (tempRBACUser[0].Account == staffCode) && (!tempRBACUser[0].AccLock);
                            if (b)
                            {
                                Log.Info("账号[" + staffCode + "]验证成功！");
                                SetUserSession(tempRBACUser[0]);
                                Log.Info("账号[" + staffCode + "]Session和Cookie信息写入成功！");
                            }
                        }
                        else
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = "账号[" + staffCode + "]的所属员工被禁用或者逻辑删除！";
                            Log.Error(tempBaseResultBool.ErrorInfo);
                        }
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "员工账号[" + staffCode + "]被禁用或逻辑删除！";
                        Log.Error(tempBaseResultBool.ErrorInfo);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "员工工号[" + staffCode + "]在试剂系统中不存在，请检查！";
                    Log.Error(tempBaseResultBool.ErrorInfo);
                }
                #endregion
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "接口异常:" + ex.Message;
                Log.Error("RBAC_BA_AnYangLiuYuanUnifiedLogin接口异常->", ex);
            }
            Log.Info("安阳市第六医院一体化平台统一登录定制接口执行结束");
            return tempBaseResultBool;
        }

        /// <summary>
        /// 用户信息写入session和cookie
        /// </summary>
        /// <param name="rbacUser"></param>
        private void SetUserSession(RBACUser rbacUser)
        {
            if (rbacUser != null)
            {
                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, "");
                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "");

                SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, rbacUser.LabID.ToString());//实验室ID
                SessionHelper.SetSessionValue(DicCookieSession.UserAccount, rbacUser.Account);//员工账户名
                SessionHelper.SetSessionValue(DicCookieSession.UseCode, rbacUser.UseCode);//员工代码

                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, rbacUser.LabID.ToString());//实验室ID
                if (rbacUser.LabID > 0)
                    Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "1");
                Cookie.CookieHelper.Write(DicCookieSession.UserID, rbacUser.Id.ToString());//账户ID
                Cookie.CookieHelper.Write(DicCookieSession.UserAccount, rbacUser.Account);//账户名
                Cookie.CookieHelper.Write(DicCookieSession.UseCode, rbacUser.UseCode);//账户代码

                //Cookie.CookieHelper.Write("000500", "4794031815009582380"); // 模块ID

                if (rbacUser.HREmployee != null)
                {
                    SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, rbacUser.HREmployee.Id); //员工ID
                    SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, rbacUser.HREmployee.CName);//员工姓名 

                    SessionHelper.SetSessionValue(DicCookieSession.EmployeeUseCode, rbacUser.HREmployee.UseCode);//员工代码 



                    //员工时间戳
                    //SessionHelper.SetSessionValue(rbacUser.HREmployee.Id.ToString(), rbacUser.HREmployee.DataTimeStamp);

                    Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, rbacUser.HREmployee.Id.ToString());// 员工ID
                    Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, rbacUser.HREmployee.CName);// 员工姓名
                    Cookie.CookieHelper.Write(DicCookieSession.EmployeeUseCode, rbacUser.HREmployee.UseCode);// 员工代码
                    if (rbacUser.HREmployee.HRDept != null)
                    {
                        SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, rbacUser.HREmployee.HRDept.Id);//部门ID
                        SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, rbacUser.HREmployee.HRDept.CName);//部门名称
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, rbacUser.HREmployee.HRDept.Id.ToString());//部门ID
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, rbacUser.HREmployee.HRDept.CName);//部门名称
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptCode, rbacUser.HREmployee.HRDept.UseCode);//部门名称
                    }

                    //获取员工具有权限的模块列表
                    IList<RBACModule> tempRBACModuleList = IBRBACModule.SearchModuleByHREmpID(rbacUser.HREmployee.Id);
                    if (tempRBACModuleList != null && tempRBACModuleList.Count > 0)
                    {
                        Dictionary<string, string> tempRBACModuleDic = new Dictionary<string, string>();
                        foreach (RBACModule tempRBACModule in tempRBACModuleList)
                        {
                            if (!tempRBACModuleDic.ContainsKey(tempRBACModule.Id.ToString()))
                                tempRBACModuleDic.Add(tempRBACModule.Id.ToString(), tempRBACModule.Url);
                        }
                        SessionHelper.SetSessionValue(DicCookieSession.OldModuleID, tempRBACModuleDic);
                    }
                    //获取员工具有权限的模块操作列表
                    //IList<RBACModuleOper> tempRBACModuleOperList = IBRBACModuleOper.SearchModuleOperByHREmpID(rbacUser.HREmployee.Id);
                    //if (tempRBACModuleOperList != null && tempRBACModuleOperList.Count > 0)
                    //{
                    //    Dictionary<string, string> tempRBACModuleOperDic = new Dictionary<string, string>();
                    //    foreach (RBACModuleOper tempRBACModuleOper in tempRBACModuleOperList)
                    //    {
                    //        if (!tempRBACModuleOperDic.ContainsKey(tempRBACModuleOper.Id.ToString()))
                    //            tempRBACModuleOperDic.Add(tempRBACModuleOper.Id.ToString(), tempRBACModuleOper.OperateURL);
                    //    }
                    //    SessionHelper.SetSessionValue(DicCookieSession.CurModuleOperID, tempRBACModuleOperDic);
                    //}
                }
            }
            else
            {
                SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, "");//实验室ID
                SessionHelper.SetSessionValue(DicCookieSession.UserAccount, DicCookieSession.SuperUser);//账户名
                SessionHelper.SetSessionValue(DicCookieSession.UseCode, "");//员工代码
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, ""); //员工ID
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, DicCookieSession.SuperUserName);//员工姓名 
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, "");//部门ID
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, "");//部门名称
                SessionHelper.SetSessionValue(DicCookieSession.OldModuleID, "");
                SessionHelper.SetSessionValue(DicCookieSession.CurModuleID, "");

                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, "");//实验室ID
                Cookie.CookieHelper.Write(DicCookieSession.UserID, "");//账户ID
                Cookie.CookieHelper.Write(DicCookieSession.UserAccount, DicCookieSession.SuperUser);//账户名
                Cookie.CookieHelper.Write(DicCookieSession.UseCode, "");//账户代码

                Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, "");// 员工ID
                Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, DicCookieSession.SuperUserName);// 员工姓名
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeUseCode, "");//员工代码 
                Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, "");//部门ID
                Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, "");//部门名称            
            }
        }

        /// <summary>
        /// 一体化平台统一登录定制接口
        /// signKey（签名密钥，采用 32 位 UUID 格式），由平台分配给各业务系统
        /// 通过配置IsCheckToken和IsCheckIntervalTime控制是否需要进行安全校验
        /// 
        /// 1.参数验证；
        /// 2.将orgCode=001&staffCode=1002&deptCode=003&timestamp=20210130152050&signKey=abcdefghijklmnoprrstuvwxyzabcdef，进行md5加密，生成加密串；
        /// 3.加密串和sign值比对一致则成功，不一致返回错误；
        /// 4.校验成功后，根据工号查询用户表，存在用户，返回成功，不存在返回失败；
        /// </summary>
        /// <param name="orgCode">医院代码</param>
        /// <param name="staffCode">员工工号</param>
        /// <param name="deptCode">科室/病区代码</param>
        /// <param name="timestamp">时间戳，如：20210130152050，采用24小时制，和服务器时间比对，时间差异在5分钟内可视为时间一致；不一致，可视为参数无效，做登录失败处理。</param>
        /// <param name="sign">签名值</param>
        /// <returns>BaseResultBool</returns>
        public BaseResultBool RBAC_BA_UnifiedLogin(string orgCode, string staffCode, string deptCode, string timestamp, string sign)
        {
            Log.Info(string.Format("统一登录定制接口执行开始，参数：orgCode={0}，staffCode={1}，deptCode={2}，timestamp={3}，sign={4}", orgCode, staffCode, deptCode, timestamp, sign));
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string isCheckToken = ConfigHelper.GetConfigString("IsCheckToken");
                ZhiFang.Common.Log.Log.Info("读取Web.Config参数IsCheckToken(0为不验证，1为验证)，是否验证token=" + isCheckToken);

                string isCheckIntervalTime = ConfigHelper.GetConfigString("IsCheckIntervalTime");
                ZhiFang.Common.Log.Log.Info("读取Web.Config参数IsCheckIntervalTime(0为不验证，1为验证)，是否验证调用者和服务器的间隔时间=" + isCheckIntervalTime);

                #region 时间戳参数非空校验，时间戳和服务器时间比对，时间差异>n分钟，返回失败
                if (isCheckIntervalTime.Trim() == "1")
                {
                    if (string.IsNullOrWhiteSpace(timestamp))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "时间戳参数错误！";
                        return tempBaseResultBool;
                    }
                    try
                    {
                        int intervalTimeValue = 600;
                        string tempIntervalTimeValue = ConfigHelper.GetConfigString("IntervalTimeValue");
                        ZhiFang.Common.Log.Log.Info("读取Web.Config参数IntervalTimeValue(单位：秒)，间隔时间=" + tempIntervalTimeValue);
                        if (tempIntervalTimeValue != null && tempIntervalTimeValue.Trim().Length > 0)
                        {
                            if (!int.TryParse(tempIntervalTimeValue, out intervalTimeValue))
                            {
                                intervalTimeValue = 600;
                            }
                        }
                        //秒转换为分钟
                        intervalTimeValue = intervalTimeValue / 60;

                        DateTime dtTimeStamp = DateTime.ParseExact(timestamp, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                        DateTime dtNowTime = DateTime.Now;
                        TimeSpan ts = dtNowTime.Subtract(dtTimeStamp);//时间差
                        if (Math.Abs(ts.Minutes) > intervalTimeValue)
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = "时间戳参数无效！时间间隔差异过大！";
                            return tempBaseResultBool;
                        }
                        Log.Info("时间戳和当前时间比对，时间差未超过设置容差，验证通过！");
                    }
                    catch (Exception ex1)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "时间戳参数格式错误！";
                        Log.Error(tempBaseResultBool.ErrorInfo, ex1);
                        return tempBaseResultBool;
                    }
                }
                #endregion

                #region sign签名校验
                if (isCheckToken.Trim() == "1")
                {
                    #region 非空校验
                    if (string.IsNullOrWhiteSpace(orgCode))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "医院代码参数错误！";
                        return tempBaseResultBool;
                    }
                    if (string.IsNullOrWhiteSpace(staffCode))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "员工工号参数错误！";
                        return tempBaseResultBool;
                    }
                    if (string.IsNullOrWhiteSpace(deptCode))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "科室/病区代码参数错误！";
                        return tempBaseResultBool;
                    }
                    if (string.IsNullOrWhiteSpace(sign))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "sign签名值参数错误！";
                        return tempBaseResultBool;
                    }
                    #endregion

                    string signKey = ZhiFang.Common.Public.ConfigHelper.GetConfigString("SignKey");
                    Log.Info("签名密钥SignKey：" + signKey);
                    string tempSign = string.Format("orgCode={0}&staffCode={1}&deptCode={2}&timestamp={3}&signKey={4}", orgCode, staffCode, deptCode, timestamp, signKey);
                    Log.Info("MD5加密前字符串：" + tempSign);
                    tempSign = SecurityHelp.NetMd5EncryptStr32(tempSign);
                    Log.Info("MD5加密后字符串：" + tempSign);
                    if (tempSign.Trim().ToLower() != sign.Trim().ToLower())
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "sign签名不一致，校验失败！";
                        Log.Error(tempBaseResultBool.ErrorInfo);
                        return tempBaseResultBool;
                    }
                    Log.Info("sign签名校验成功!");
                }
                #endregion

                #region 根据员工工号查询账户表
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(staffCode);
                if (tempRBACUser.Count == 1)
                {
                    if (tempRBACUser[0].IsUse.HasValue && tempRBACUser[0].IsUse.Value)
                    {
                        if (tempRBACUser[0].HREmployee.IsUse.HasValue && tempRBACUser[0].HREmployee.IsUse.Value && tempRBACUser[0].HREmployee.IsEnabled == 1)
                        {
                            bool b = (tempRBACUser[0].Account == staffCode) && (!tempRBACUser[0].AccLock);
                            if (b)
                            {
                                Log.Info("账号[" + staffCode + "]验证成功！");
                                SetUserSession(tempRBACUser[0]);
                                Log.Info("账号[" + staffCode + "]Session和Cookie信息写入成功！");
                            }
                        }
                        else
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = "账号[" + staffCode + "]的所属员工被禁用或者逻辑删除！";
                            Log.Error(tempBaseResultBool.ErrorInfo);
                        }
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "员工账号[" + staffCode + "]被禁用或逻辑删除！";
                        Log.Error(tempBaseResultBool.ErrorInfo);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "员工工号[" + staffCode + "]在试剂系统中不存在，请检查！";
                    Log.Error(tempBaseResultBool.ErrorInfo);
                }
                #endregion
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "接口异常:" + ex.Message;
                Log.Error("RBAC_BA_UnifiedLogin接口异常->", ex);
            }
            Log.Info("统一登录定制接口执行结束");
            return tempBaseResultBool;
        }

    }
}
