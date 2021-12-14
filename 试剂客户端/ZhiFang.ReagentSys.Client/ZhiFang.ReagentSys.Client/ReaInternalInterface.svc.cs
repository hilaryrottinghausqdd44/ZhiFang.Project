using System;
using System.Web;
using System.Data;
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
using ZhiFang.InterfaceFactory;
using System.Collections;

namespace ZhiFang.ReagentSys.Client
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReaInternalInterface : IReaInternalInterface
    {
        IBReaGoods IBReaGoods { get; set; }
        IBHRDept IBHRDept { get; set; }
        IBRBACUser IBRBACUser { get; set; }
        IBReaStorage IBReaStorage { get; set; }
        IBHREmployee IBHREmployee { get; set; }
        IBCenOrg IBCenOrg { get; set; }
        IBReaCenOrg IBReaCenOrg { get; set; }
        IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }


        public BaseResultData RS_GetReaStorageInfo(string storeCode)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                brd = UserLogin();
                if (!brd.success)
                    return brd;
                CenOrg cenOrg = null;
                HRDept dept = null;
                HREmployee emp = null;
                brd = GetCurUserCenOrg(ref cenOrg, ref dept, ref emp);
                if (!brd.success)
                    return brd;
                string tempType = InterfaceCommon.GetThirdSaleDocType().data;
                if (!string.IsNullOrWhiteSpace(tempType))
                {
                    if (tempType.ToLower() == "bjsjtyy_wanghai")
                    {
                        InterfaceWangHai interfaceWangHai = new InterfaceWangHai();
                        brd = interfaceWangHai.GetStoreInfo(storeCode, IBReaStorage);
                    }
                    else if (tempType.ToLower() == "jxgnfyyy_his" || tempType.ToLower() == "sshqyy_his" || tempType.ToLower() == "nnsdyrmyy_his")
                    {
                        InterfaceView interfaceView = new InterfaceView();
                        brd = interfaceView.GetStoreInfo(storeCode, IBReaStorage);
                    }
                    else if (tempType.ToLower() == "bjellyy_his")
                    {
                        InterfaceErLongLu interfaceErLongLu = new InterfaceErLongLu();
                        brd = interfaceErLongLu.GetBaseDataInfo("REASTORAGE");
                    }
                }
            }
            catch (Exception ex)
            {
                brd.success = false;
                brd.message = "获取第三方接口库房信息错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(brd.message);
            }
            return brd;
        }

        public BaseResultData RS_GetHRDeptInfo(string deptCode)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                brd = UserLogin();
                if (!brd.success)
                    return brd;
                CenOrg cenOrg = null;
                HRDept dept = null;
                HREmployee emp = null;
                brd = GetCurUserCenOrg(ref cenOrg, ref dept, ref emp);
                if (!brd.success)
                    return brd;
                string tempType = InterfaceCommon.GetThirdSaleDocType().data;
                if (!string.IsNullOrWhiteSpace(tempType))
                {
                    if (tempType.ToLower() == "bjsjtyy_wanghai")
                    {
                        InterfaceWangHai interfaceWangHai = new InterfaceWangHai();
                        brd = interfaceWangHai.GetDeptInfo(deptCode, dept, IBHRDept);
                    }
                    else if (tempType.ToLower() == "jxgnfyyy_his" || tempType.ToLower() == "sshqyy_his" || tempType.ToLower() == "nnsdyrmyy_his")
                    {
                        InterfaceView interfaceView = new InterfaceView();
                        brd = interfaceView.GetDeptInfo(deptCode, dept, IBHRDept);
                    }
                    else if (tempType.ToLower() == "bjellyy_his")
                    {
                        InterfaceErLongLu interfaceErLongLu = new InterfaceErLongLu();
                        brd = interfaceErLongLu.GetBaseDataInfo("DEPT");
                    }
                }
            }
            catch (Exception ex)
            {
                brd.success = false;
                brd.message = "获取第三方接口科室信息错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(brd.message);
            }
            return brd;
        }

        public BaseResultData RS_GetHREmployeeInfo(string empCode)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                brd = UserLogin();
                if (!brd.success)
                    return brd;
                CenOrg cenOrg = null;
                HRDept dept = null;
                HREmployee emp = null;
                brd = GetCurUserCenOrg(ref cenOrg, ref dept, ref emp);
                if (!brd.success)
                    return brd;
                string tempType = InterfaceCommon.GetThirdSaleDocType().data;
                if (!string.IsNullOrWhiteSpace(tempType))
                {
                    if (tempType.ToLower() == "bjsjtyy_wanghai")
                    {
                        InterfaceWangHai interfaceWangHai = new InterfaceWangHai();
                        brd = interfaceWangHai.GetUserInfo(empCode, dept, IBHREmployee);
                    }
                    else if (tempType.ToLower() == "jxgnfyyy_his" || tempType.ToLower() == "sshqyy_his" || tempType.ToLower() == "nnsdyrmyy_his")
                    {
                        InterfaceView interfaceView = new InterfaceView();
                        brd = interfaceView.GetUserInfo(empCode, dept, IBHREmployee);
                    }
                    else if (tempType.ToLower() == "bjellyy_his")
                    {
                        InterfaceErLongLu interfaceErLongLu = new InterfaceErLongLu();
                        brd = interfaceErLongLu.GetBaseDataInfo("EMPLOYEE");
                    }
                }
            }
            catch (Exception ex)
            {
                brd.success = false;
                brd.message = "获取第三方接口人员信息错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(brd.message);
            }
            return brd;
        }

        public BaseResultData RS_GetReaCenOrgInfo(string compCode)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                brd = UserLogin();
                if (!brd.success)
                    return brd;
                CenOrg cenOrg = null;
                HRDept dept = null;
                HREmployee emp = null;
                brd = GetCurUserCenOrg(ref cenOrg, ref dept, ref emp);
                if (!brd.success)
                    return brd;
                string tempType = InterfaceCommon.GetThirdSaleDocType().data;
                if (!string.IsNullOrWhiteSpace(tempType))
                {
                    if (tempType.ToLower() == "jxgnfyyy_his" || tempType.ToLower() == "sshqyy_his" || tempType.ToLower() == "nnsdyrmyy_his")
                    {
                        InterfaceView interfaceView = new InterfaceView();
                        brd = interfaceView.GetReaCenOrgInfo(compCode, IBReaCenOrg);
                    }
                    else if (tempType.ToLower() == "bjellyy_his")
                    {
                        InterfaceErLongLu interfaceErLongLu = new InterfaceErLongLu();
                        brd = interfaceErLongLu.GetBaseDataInfo("REACENORG");
                    }
                }
            }
            catch (Exception ex)
            {
                brd.success = false;
                brd.message = "获取第三方接口供货商信息错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(brd.message);
            }
            return brd;
        }

        public BaseResultData RS_GetReaGoodsInfo(string goodsCode, string lastModifyDate)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                brd = UserLogin();
                if (!brd.success)
                    return brd;
                CenOrg cenOrg = null;
                HRDept dept = null;
                HREmployee emp = null;
                brd = GetCurUserCenOrg(ref cenOrg, ref dept, ref emp);
                if (!brd.success)
                    return brd;
                IList<ReaCenOrg> listOrg = new List<ReaCenOrg>();
                IList<ReaGoods> listReaGoods = new List<ReaGoods>();
                string tempType = InterfaceCommon.GetThirdSaleDocType().data;
                if (!string.IsNullOrWhiteSpace(tempType))
                {
                    if (tempType.ToLower() == "bjsjtyy_wanghai")
                    {
                        InterfaceWangHai interfaceWangHai = new InterfaceWangHai();
                        brd = interfaceWangHai.GetReaGoodsInfo(goodsCode, "1", "20000", lastModifyDate, ref listOrg, ref listReaGoods);
                    }
                    else if (tempType.ToLower() == "jxgnfyyy_his" || tempType.ToLower() == "sshqyy_his" || tempType.ToLower() == "nnsdyrmyy_his")
                    {
                        InterfaceView interfaceView = new InterfaceView();
                        brd = interfaceView.GetReaGoodsInfo(goodsCode, IBReaGoods, ref listOrg, ref listReaGoods);
                    }
                    else if (tempType.ToLower() == "bjellyy_his")
                    {
                        InterfaceErLongLu interfaceErLongLu = new InterfaceErLongLu();
                        //brd = interfaceErLongLu.GetBaseDataInfo("REAGOODS");
                        brd = interfaceErLongLu.GetReaGoodInfo(ref listOrg, ref listReaGoods);
                    }
                }
                try
                {
                    brd = IBReaCenOrg.SaveReaCenOrgByMatchInterface(listOrg);
                    if (!brd.success)
                    {
                        ZhiFang.Common.Log.Log.Error("保存物资接口供应商信息:" + brd.message);
                    }
                    brd = IBReaGoods.SaveReaGoodsByMatchInterface(listReaGoods, emp.Id, emp.CName);
                    if (!brd.success)
                    {
                        ZhiFang.Common.Log.Log.Error("保存物资接口机构货品信息:" + brd.message);
                    }
                    brd = IBReaGoodsOrgLink.SaveReaGoodsOrgLinkByMatchInterface(listReaGoods, emp.Id, emp.CName);
                    if (!brd.success)
                    {
                        ZhiFang.Common.Log.Log.Error("保存物资接口供货货品关系信息:" + brd.message);
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("保存物资接口错误信息:" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                brd.success = false;
                brd.message = "获取第三方接口物资信息错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(brd.message);
            }
            return brd;
        }

        public BaseResultData RS_GetReaGoodsTypeInfo(string goodsTypeCode)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                brd = UserLogin();
                if (!brd.success)
                    return brd;
                InterfaceWangHai interfaceWangHai = new InterfaceWangHai();
                brd = interfaceWangHai.GetMateType(goodsTypeCode);
            }
            catch (Exception ex)
            {
                brd.success = false;
                brd.message = "获取第三方接口物资分类信息错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(brd.message);
            }
            return brd;
        }

        public BaseResultData UserLogin()
        {
            BaseResultData baseResultData = new BaseResultData();
            string userAccount = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.UserAccount);
            string userPassword = "";
            string isValidate = "";
            if (string.IsNullOrEmpty(userAccount))
            {
                string serverPath = HttpContext.Current.Server.MapPath("~/");
                string xmlInterfaceUser = serverPath + "\\BaseTableXML\\Interface\\InterfaceUser.xml";
                if (System.IO.File.Exists(xmlInterfaceUser))
                {
                    DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(xmlInterfaceUser);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        userAccount = ds.Tables[0].Rows[0]["UserCode"].ToString();
                        if (ds.Tables[0].Columns.Contains("UserPassword"))
                            userPassword = ds.Tables[0].Rows[0]["UserPassword"].ToString();
                        if (ds.Tables[0].Columns.Contains("isValidate"))
                            isValidate = ds.Tables[0].Rows[0]["isValidate"].ToString();
                        if (string.IsNullOrEmpty(isValidate))
                            isValidate = "1";
                    }
                    else
                    {
                        baseResultData.success = false;
                        baseResultData.message = "获取不到接口登录用户信息";
                        return baseResultData;
                    }
                    baseResultData.success = RBAC_BA_Login(userAccount, userPassword, (isValidate == "1"));
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "获取不到接口登录用户信息";
                }
            }
            return baseResultData;
        }

        public bool RBAC_BA_Login(string strUserAccount, string strPassWord, bool isValidate)
        {
            bool tempBool = false;
            if (!isValidate)
                return true;
            IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(strUserAccount);
            if (tempRBACUser.Count == 1)
            {
                if (!string.IsNullOrEmpty(strPassWord))
                    strPassWord = SecurityHelp.MD5Encrypt(strPassWord, SecurityHelp.PWDMD5Key);
                if (tempRBACUser[0].IsUse.HasValue && tempRBACUser[0].IsUse.Value)
                {
                    if (tempRBACUser[0].HREmployee.IsUse.HasValue && tempRBACUser[0].HREmployee.IsUse.Value && tempRBACUser[0].HREmployee.IsEnabled == 1)
                    {
                        if (string.IsNullOrWhiteSpace(strPassWord))
                            tempBool = (tempRBACUser[0].Account == strUserAccount) && (!tempRBACUser[0].AccLock);
                        else
                            tempBool = (tempRBACUser[0].Account == strUserAccount) && (tempRBACUser[0].PWD == strPassWord) && (!tempRBACUser[0].AccLock);
                        if (tempBool)
                        {
                            SetUserSession(tempRBACUser[0]);
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("员工被禁用或者逻辑删除！");
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("员工帐号被逻辑删除！");
                }
            }
            return tempBool;
        }

        public void SetUserSession(RBACUser rbacUser)
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

        private BaseResultData GetCurUserCenOrg(ref CenOrg cenOrg, ref HRDept dept, ref HREmployee emp)
        {
            BaseResultData baseResultDataValue = new BaseResultData();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.message = "无法获取当前用户的ID信息";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.message);
                return baseResultDataValue;
            }
            HREmployee hrEMP = IBHREmployee.Get(long.Parse(employeeID));
            if (hrEMP == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.message = "无法获取当前用户信息";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.message);
                return baseResultDataValue;

            }
            emp = hrEMP;
            string orgNo = hrEMP.HRDept.UseCode;
            dept = hrEMP.HRDept;
            if (string.IsNullOrEmpty(orgNo))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.message = "当前用户所属机构的编码为空！请联系管理员维护！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.message);
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

        
    }
}
