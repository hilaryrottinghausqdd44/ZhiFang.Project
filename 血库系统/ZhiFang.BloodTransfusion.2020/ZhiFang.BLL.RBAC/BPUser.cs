using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IDAO.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.RBAC
{
    /// <summary>
    ///
    /// </summary>
    public class BPUser : BaseBLL<PUser>, IBPUser
    {
        private readonly string Strcoust = "=qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890+";
        IDDepartmentDao IDDepartmentDao { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IDDepartmentUserDao IDDepartmentUserDao { get; set; }
        IDBloodDocGradeDao IDBloodDocGradeDao { get; set; }

        public override bool Add()
        {
            if (string.IsNullOrEmpty(this.Entity.Password))
            {
                this.Entity.Password = this.Entity.ShortCode;
            }
            this.Entity.Password = CovertPassWord(this.Entity.Password);
            bool a = DBDao.Save(this.Entity);
            return a;
        }

        /// <summary>
        /// 用户注销服务
        /// </summary>
        /// <param name="strUserAccount"></param>
        /// <returns></returns>
        public bool RBAC_BA_Logout(string strUserAccount)
        {
            return false;
        }
        #region PUser登录处理
        public BaseResultDataValue GetUserLogin(string account, string pwd)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (account.Trim() == DicCookieSession.SuperUser && pwd == DicCookieSession.SuperUserPwd)
            {
                SetUserSession(null);
                brdv.success = true;
                return brdv;
            }

            brdv.success = false;
            if (account == null || pwd == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "用户名密码不能为空";
                ZhiFang.Common.Log.Log.Debug("UserLogin:用户名密码不能为空");
                return brdv;
            }

            pwd = this.CovertPassWord(pwd);
            StringBuilder hqlWhere = new StringBuilder();
            hqlWhere.Append(" puser.ShortCode = '" + account + "' ");
            if (pwd.Length > 0)
                hqlWhere.Append(" and puser.Password = '" + pwd + "' ");
            ZhiFang.Common.Log.Log.Debug("UserLogin:" + hqlWhere.ToString());
            IList<PUser> userList = ((IDPUserDao)base.DBDao).GetListByHQL(hqlWhere.ToString());

            if (userList != null && userList.Count > 0)
            {
                PUser puser = userList[0];
                if (puser.Visible != true)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "该帐号已被停用(Visible!=1)!";
                    return brdv;
                }

                brdv.success = true;
                GetDepartment(ref puser, "", "", false);
                SetUserSession(puser);
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取帐号为:" + account + "的用户信息为空!";
                ZhiFang.Common.Log.Log.Warn(brdv.ErrorInfo);
            }

            return brdv;
        }
        public BaseResultDataValue GetUserLoginByHisCode(string hisWardCode, string hisDeptCode, string hisDoctorCode, bool isAutoAddDepartmentUser)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            if (hisDoctorCode == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "HIS工号对照码不能为空";
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
                return brdv;
            }

            StringBuilder hqlWhere = new StringBuilder();
            hqlWhere.Append(string.Format("(puser.Code1 = '{0}' or  puser.Code2 = '{1}' or puser.Code3= '{2}' or puser.Code4= '{3}' or puser.Code5= '{4}')", hisDoctorCode, hisDoctorCode, hisDoctorCode, hisDoctorCode, hisDoctorCode));
            ZhiFang.Common.Log.Log.Debug("按传入的HIS编码为:" + hisDoctorCode + ",获取PUser的hql" + hqlWhere.ToString());
            IList<PUser> userList = ((IDPUserDao)base.DBDao).GetListByHQL(hqlWhere.ToString());

            if (userList != null && userList.Count > 0)
            {
                PUser puser = userList[0];
                if (puser.Visible != true)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "该帐号已被停用(Visible!=1)!";
                    ZhiFang.Common.Log.Log.Warn(brdv.ErrorInfo);
                    return brdv;
                }

                brdv.success = true;
                ZhiFang.Common.Log.Log.Error("传入的HIS医生编码为:" + hisDoctorCode + ",在LIS获取PUser的编码为:" + puser.Id + "!");
                //判断是否人员帐号是否绑定了医生信息
                GetCurDoctorInfo(hisWardCode, hisDeptCode, hisDoctorCode, ref puser, ref brdv);
                //获取病区信息
                GetWard(ref puser, hisWardCode, hisDoctorCode, isAutoAddDepartmentUser);
                //获取科室信息
                GetDepartment(ref puser, hisDeptCode, hisDoctorCode, isAutoAddDepartmentUser);
                //SetUserSession
                SetUserSession(puser);
                //返回对应的病区,科室,人员及医生相关的信息
                GetCurUserInfo(hisWardCode, hisDeptCode, hisDoctorCode, puser, ref brdv);
                //病区科室关系绑定
                EditDepartment(puser);
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取HIS工号对照码为:" + hisDoctorCode + "的用户信息为空!";
                ZhiFang.Common.Log.Log.Warn(brdv.ErrorInfo);
            }

            return brdv;
        }
        private void GetCurDoctorInfo(string hisWardId, string hisDeptCode, string hisDoctorCode, ref PUser puser, ref BaseResultDataValue brdv)
        {
            //if (puser.Doctor != null)
            //{
            //    puser.Doctor = IDDoctorDao.Get(puser.Doctor.Id);
            //    ZhiFang.Common.Log.Log.Debug("HIS传入的医生编码为:" + hisDoctorCode + ",获取到LIS医生编码为:" + puser.Doctor.Id);
            //}
            //else
            //{
            //    string hql1 = string.Format(" doctor.Visible=1 and (doctor.Code1 ='{0}' or  doctor.Code2 ='{1}' or doctor.Code3='{2}' or doctor.Code4='{3}' or doctor.Code5='{4}')", hisDoctorCode, hisDoctorCode, hisDoctorCode, hisDoctorCode, hisDoctorCode);
            //    ZhiFang.Common.Log.Log.Debug("按hisDoctorCode获取查询doctor信息hql:" + hql1);
            //    IList<Doctor> doctorList = IDDoctorDao.GetListByHQL(hql1);// this.SearchListByHQL(hql1);
            //    if (doctorList == null || doctorList.Count <= 0)
            //    {
            //        //brdv.success = false;
            //        //brdv.ErrorInfo = "提示信息：获取医生信息为空!";
            //        ZhiFang.Common.Log.Log.Error("HIS传入的医生编码为:" + hisDoctorCode + ",在LIS未获取到对照信息!");
            //    }
            //    else
            //    {
            //        puser.Doctor = doctorList[0];
            //        //人员医生关系绑定
            //        EditUserAndDoctor(puser, hisDoctorCode);
            //        ZhiFang.Common.Log.Log.Debug("HIS传入的医生编码为:" + hisDoctorCode + ",获取到LIS医生编码为:" + puser.Doctor.Id);
            //    }
            //}
        }
        private void GetCurUserInfo(string hisWardId, string hisDeptCode, string hisDoctorCode, PUser puser, ref BaseResultDataValue brdv)
        {
            SysCurUserInfo userInfo = new SysCurUserInfo();

            userInfo.Account = puser.ShortCode;
            userInfo.Password = this.UnCovertPassWord(puser.Password);
            userInfo.UserId = puser.Id.ToString();
            userInfo.UserCName = puser.CName;

            if (puser.Ward != null)
            {
                userInfo.WardId = puser.Ward.Id.ToString();
                userInfo.WardCName = puser.Ward.CName;
                userInfo.HisWardId = hisWardId;
            }
            if (puser.Department != null)
            {
                userInfo.DeptId = puser.Department.Id.ToString();
                userInfo.DeptCName = puser.Department.CName;
                userInfo.HisDeptId = hisDeptCode;
            }
            //if (puser.Doctor != null)
            //{
            //    userInfo.DoctorId = puser.Doctor.Id.ToString();
            //    userInfo.DoctorCName = puser.Doctor.CName;
            //    userInfo.HisDoctorId = hisDoctorCode;
            //    if (puser.Doctor.GradeNo.HasValue)
            //    {
            //        BloodDocGrade docGrade = IDBloodDocGradeDao.Get(puser.Doctor.GradeNo.Value.ToString());
            //        if (docGrade != null)
            //        {
            //            userInfo.GradeId = docGrade.Id.ToString();
            //            userInfo.GradeName = docGrade.CName;
            //            userInfo.LowLimit = docGrade.LowLimit;
            //            userInfo.UpperLimit = docGrade.UpperLimit;
            //        }
            //        else
            //        {
            //            ZhiFang.Common.Log.Log.Debug("HIS传入的医生编码为:" + hisDoctorCode + ",在LIS未设置医生的所属等级信息");
            //        }
            //    }
            //    else
            //    {
            //        ZhiFang.Common.Log.Log.Debug("HIS传入的医生编码为:" + hisDoctorCode + ",在LIS未设置医生的所属等级信息");
            //    }
            //}
            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
            brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(userInfo);
        }
        private void GetWard(ref PUser puser, string hisWardId, string hisDoctorCode, bool isAutoAddDepartmentUser)
        {
            Department ward = null;
            if (!string.IsNullOrEmpty(hisWardId))
            {
                string hql1 = string.Format(" department.Visible=1 and (department.Code1='{0}' or department.Code2='{1}' or department.Code3='{2}' or department.Code4='{3}' or department.Code5='{4}')", hisWardId, hisWardId, hisWardId, hisWardId, hisWardId);
                ZhiFang.Common.Log.Log.Debug("按传入HIS病区编码为:" + hisWardId + ",获取Department的hql:" + hql1);
                IList<Department> deptList = IDDepartmentDao.GetListByHQL(hql1);
                if (deptList != null && deptList.Count > 0)
                {
                    ward = deptList[0];
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("传入的HIS病区编码为:" + hisWardId + ",在LIS未获取到对照信息!");
                }
            }
            else
            {
                //先从病区人员关系表获取科室信息
                IList<DepartmentUser> deptUserList = IDDepartmentUserDao.GetListByHQL("departmentuser.PUser.Id=" + puser.Id);
                if (deptUserList != null && deptUserList.Count >= 1)
                {
                    //department = deptUserList[0].Department;
                    ward = IDDepartmentDao.Get(deptUserList[0].Department.Id);
                }
                else
                {
                    if (puser.DeptNo.HasValue)
                    {
                        ward = IDDepartmentDao.Get(puser.DeptNo.Value);
                    }
                }
            }
            if (ward != null && ward.CName != null)
            {
                ZhiFang.Common.Log.Log.Error("传入的HIS病区编码为:" + hisWardId + ",在LIS获取到病区编码为:" + ward.Id + "!");
                puser.Ward = ward;
            }
            //判断是否存在病区人员关系信息
            if (isAutoAddDepartmentUser == true)
                AddDepartmentUser(puser, ward, hisDoctorCode, hisWardId);
        }
        private void GetDepartment(ref PUser puser, string hisDeptCode, string hisDoctorCode, bool isAutoAddDepartmentUser)
        {
            Department department = null;
            if (!string.IsNullOrEmpty(hisDeptCode))
            {
                string hql1 = string.Format(" department.Visible=1 and (department.Code1='{0}' or department.Code2='{1}' or department.Code3='{2}' or department.Code4='{3}' or department.Code5='{4}')", hisDeptCode, hisDeptCode, hisDeptCode, hisDeptCode, hisDeptCode);
                ZhiFang.Common.Log.Log.Debug("按传入HIS科室编码为:" + hisDeptCode + ",获取Department的hql:" + hql1);
                IList<Department> deptList = IDDepartmentDao.GetListByHQL(hql1);
                if (deptList != null && deptList.Count > 0)
                {
                    department = deptList[0];
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("传入的HIS科室编码为:" + hisDeptCode + ",在LIS未获取到对照信息!");
                }
            }
            else
            {
                //先从科室人员关系表获取科室信息
                IList<DepartmentUser> deptUserList = IDDepartmentUserDao.GetListByHQL("departmentuser.PUser.Id=" + puser.Id);
                if (deptUserList != null && deptUserList.Count >= 1)
                {
                    //department = deptUserList[0].Department;
                    department = IDDepartmentDao.Get(deptUserList[0].Department.Id);
                }
                else
                {
                    if (puser.DeptNo.HasValue)
                    {
                        department = IDDepartmentDao.Get(puser.DeptNo.Value);
                    }
                }
            }
            if (department != null && department.CName != null)
            {
                ZhiFang.Common.Log.Log.Error("传入的HIS科室编码为:" + hisDeptCode + ",在LIS获取到科室编码为:" + department.Id + "!");
                puser.Department = department;
                ZhiFang.BloodTransfusion.Common.Cookie.CookieHelper.Write("ULdept", department.CName);
            }
            else
            {
                ZhiFang.BloodTransfusion.Common.Cookie.CookieHelper.Write("ULdept", null);
            }
            //判断是否存在科室人员关系信息
            if (isAutoAddDepartmentUser == true)
                AddDepartmentUser(puser, department, hisDoctorCode, hisDeptCode);
        }
        private void AddDepartmentUser(PUser puser, Department department, string hisDoctorCode, string hisDeptCode)
        {
            if (puser != null && department != null)
            {
                IList<DepartmentUser> deptUserList = IDDepartmentUserDao.GetListByHQL("departmentuser.PUser.Id=" + puser.Id + " and departmentuser.Department.Id=" + department.Id);
                if (deptUserList == null || deptUserList.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("HIS(病区)科室编码为:" + hisDeptCode + ",HIS医生编码为:" + hisDoctorCode + ",在LIS不存在(病区)科室人员关系信息,LIS自动建立(病区)科室人员关系!");
                    DepartmentUser deptUser = new DepartmentUser();
                    deptUser.PUser = puser;
                    deptUser.Department = department;
                    bool result = IDDepartmentUserDao.Save(deptUser);
                    ZhiFang.Common.Log.Log.Debug("(病区)科室人员关系自动建立保存结果:" + result);
                }
            }
        }
        private void EditDepartment(PUser puser)
        {
            if (puser.Ward != null && puser.Department != null)
            {
                if (puser.Department.ParentID <= 0 && puser.Ward.Id != puser.Department.Id)
                {
                    //建立病区科室上下级关系
                    ZhiFang.Common.Log.Log.Info("建立病区科室上下级关系,病区编码为:" + puser.Ward.Id + ",科室编码为:" + puser.Department.Id);
                    puser.Department.ParentID = puser.Ward.Id;
                    IDDepartmentDao.Save(puser.Department);
                }
            }
        }
        private void EditUserAndDoctor(PUser puser, string hisDoctorCode)
        {
            //if (puser != null && puser.Doctor != null)
            //{
            //    IList<PUser> userList = ((IDPUserDao)base.DBDao).GetListByHQL("puser.Id=" + puser.Id + " and puser.Doctor.Id=" + puser.Doctor.Id);
            //    if (userList == null || userList.Count <= 0)
            //    {
            //        ZhiFang.Common.Log.Log.Debug("HIS医生编码为:" + hisDoctorCode + ",在LIS不存在人员医生绑定关系信息,LIS自动建立人员绑定医生关系!");
            //        DepartmentUser deptUser = new DepartmentUser();
            //        if (string.IsNullOrEmpty(puser.Usertype))
            //            puser.Usertype = BloodIdentityType.医生.Key;
            //        this.Entity = puser;
            //        bool result = this.Edit();
            //        ZhiFang.Common.Log.Log.Debug("人员绑定医生关系自动建立保存结果:" + result);
            //    }
            //}
        }
        public void SetUserSession(PUser rbacUser)
        {
            if (rbacUser != null)
            {
                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, "");
                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "");
                SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, rbacUser.LabID.ToString());//实验室ID
                SessionHelper.SetSessionValue(DicCookieSession.UserAccount, rbacUser.ShortCode);//员工账户名
                SessionHelper.SetSessionValue(DicCookieSession.UseCode, rbacUser.Id);//员工代码
                Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, rbacUser.LabID.ToString());//实验室ID
                if (rbacUser.LabID > 0)
                    Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "1");
                Cookie.CookieHelper.Write(DicCookieSession.UserID, rbacUser.Id.ToString());//账户ID
                Cookie.CookieHelper.Write(DicCookieSession.UserAccount, rbacUser.ShortCode);//账户名
                Cookie.CookieHelper.Write(DicCookieSession.UseCode, rbacUser.ShortCode);//账户代码
                //Cookie.CookieHelper.Write("000500", "4794031815009582380"); // 模块ID
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, rbacUser.Id); //员工ID
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, rbacUser.CName);//员工姓名 
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeUseCode, rbacUser.Id);//员工代码   
                Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, rbacUser.Id.ToString());// 员工ID
                Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, rbacUser.CName);// 员工姓名
                Cookie.CookieHelper.Write(DicCookieSession.EmployeeUseCode, rbacUser.Id.ToString());// 员工代码
                ZhiFang.Common.Log.Log.Debug("登录员工信息:PUser.Id:" + rbacUser.Id + ",PUser.CName:" + rbacUser.CName);
                if (rbacUser.Ward != null)
                {
                    ZhiFang.Common.Log.Log.Debug("登录所属病区信息:Ward.Id:" + rbacUser.Ward.Id + ",Ward.CName:" + rbacUser.Ward.CName);
                }
                if (rbacUser.Department != null)
                {
                    ZhiFang.Common.Log.Log.Debug("登录所属科室信息:Department.Id:" + rbacUser.Department.Id + ",Department.CName:" + rbacUser.Department.CName);
                    SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, rbacUser.Department.Id);//部门ID
                    SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, rbacUser.Department.CName);//部门名称
                    Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, rbacUser.Department.Id.ToString());//部门ID
                    Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, rbacUser.Department.CName);//部门名称
                    Cookie.CookieHelper.Write(DicCookieSession.HRDeptCode, rbacUser.Department.ShortCode);//部门名称
                }

                //if (rbacUser.Doctor != null)
                //{
                //    Cookie.CookieHelper.Write(DicCookieSession.DoctorId, rbacUser.Doctor.Id.ToString());//6.6人员帐号绑定的所属医生Id
                //    Cookie.CookieHelper.Write(DicCookieSession.DoctorCName, rbacUser.Doctor.CName);//6.6人员帐号绑定的所属医生姓名
                //    string gradeNo = "";
                //    if (rbacUser.Doctor.GradeNo.HasValue)
                //        gradeNo = rbacUser.Doctor.GradeNo.ToString();
                //    Cookie.CookieHelper.Write(DicCookieSession.GradeId, gradeNo);//6.6人员帐号绑定的所属医生的等级
                //    ZhiFang.Common.Log.Log.Debug("登录员工绑定医生信息:Doctor.Id:" + rbacUser.Doctor.Id + ",Doctor.CName:" + rbacUser.Doctor.CName);
                //}
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

                SessionHelper.SetSessionValue(DicCookieSession.DoctorId, "");//6.6人员帐号绑定的所属医生Id
                Cookie.CookieHelper.Write(DicCookieSession.DoctorCName, "");//6.6人员帐号绑定的所属医生姓名
                Cookie.CookieHelper.Write(DicCookieSession.GradeId, "");//6.6人员帐号绑定的所属医生的等级    
            }
        }

        #endregion

        #region 加密/解密
        //加密
        public string CovertPassWord(string astr)
        {
            int iLength, imod3, idv3;
            iLength = astr.Length;
            string result = "";
            if (iLength == 0)
            {
                return "=";
            }
            idv3 = iLength / 3;
            imod3 = iLength % 3;
            while (idv3 > 0)
            {
                Get64Str(3, astr, ref iLength, ref result);
                iLength = iLength - 3;
                idv3--;
            }
            switch (imod3)
            {
                case 1:
                    Get64Str(1, astr, ref iLength, ref result);
                    break;
                case 2:
                    Get64Str(2, astr, ref iLength, ref result);
                    break;
            }
            //ZhiFang.Common.Log.Log.Debug(astr + "加密后为:" + result);
            return result;
        }
        //解密
        public string UnCovertPassWord(string pwd)
        {
            int iLength, imod4, idv4;
            iLength = pwd.Length;
            string result = "";
            if (iLength == 0)
            {
                return "";
            }

            idv4 = iLength / 4;
            imod4 = iLength % 4;
            while (idv4 > 0)
            {
                Get256Str(4, ref pwd, ref iLength, ref result);
                iLength = iLength - 4;
                idv4--;
            }
            switch (imod4)
            {
                case 1:
                    Get256Str(1, ref pwd, ref iLength, ref result);
                    break;
                case 2:
                    Get256Str(2, ref pwd, ref iLength, ref result);
                    break;
                case 3:
                    Get256Str(3, ref pwd, ref iLength, ref result);
                    break;
            }

            for (var i = 0; i < result.Length; i++)
            {
                if ((int)result[i] < 32 || (int)result[i] > 127)
                {
                    result = pwd;
                }
            }
            //ZhiFang.Common.Log.Log.Debug(pwd + "解密后为:" + result);
            return result;
        }
        public void Get64Str(int il, string pwd, ref int iLength, ref string Result)
        {
            char achar;
            int iCount64, iChar, imod64, i;
            iCount64 = 0;
            for (i = 0; i < il; i++)
            {
                achar = pwd[iLength - il + i];
                iChar = (int)achar;
                iCount64 = iCount64 * 256 + iChar;
            }
            for (i = 0; i < 4; i++)
            {
                if (iCount64 == 0)
                {
                    Result = '=' + Result;
                }
                else
                {
                    imod64 = (iCount64 % 64) + 1;
                    iCount64 = iCount64 / 64;
                    achar = Strcoust[imod64 - 1];
                    Result = achar + Result;
                }
            }
        }

        public void Get256Str(int il, ref string pwd, ref int iLength, ref string Result)
        {
            int i, ichar, iCount64, imod256;
            iCount64 = 0;
            for (i = 0; i < il; i++)
            {
                ichar = Strcoust.IndexOf(pwd[iLength + i - il]);
                iCount64 = iCount64 * 64 + ichar;
            }
            for (i = 0; i < 3; i++)
            {
                if (iCount64 != 0)
                {
                    imod256 = iCount64 % 256;
                    Result = ((char)imod256) + Result;
                    iCount64 = iCount64 / 256;
                }
            }
        }
        #endregion

        #region 将PUser封装为RBACUser
        public EntityList<RBACUser> SearchRBACUserOfPUserByHQL(string where, string sort, int page, int limit)
        {
            EntityList<RBACUser> tempEntityList = new EntityList<RBACUser>();
            tempEntityList.list = new List<RBACUser>();
            EntityList<PUser> userList = this.SearchListByHQL(where, sort, page, limit);
            tempEntityList.count = userList.count;

            foreach (var entity in userList.list)
            {
                tempEntityList.list.Add(_getRBACUser(entity));
            }

            return tempEntityList;
        }
        private RBACUser _getRBACUser(PUser entity)
        {
            RBACUser user = new RBACUser();

            user.Id = entity.Id;
            user.Account = entity.ShortCode;
            user.Shortcode = entity.ShortCode;
            user.CName = entity.CName;
            user.DispOrder = entity.DispOrder;
            user.DataAddTime = entity.DataAddTime;
            user.IsUse = entity.Visible;

            user.Code1 = entity.Code1;
            user.Code2 = entity.Code2;
            user.Code3 = entity.Code3;
            user.Code4 = entity.Code4;
            user.Code5 = entity.Code5;

            HREmployee employee = new HREmployee();
            employee.Id = entity.Id;
            employee.CName = entity.CName;
            employee.DispOrder = entity.DispOrder;
            employee.DataAddTime = entity.DataAddTime;
            employee.IsUse = entity.Visible;
            employee.Code1 = entity.Code1;
            employee.Code2 = entity.Code2;
            employee.Code3 = entity.Code3;
            employee.Code4 = entity.Code4;
            employee.Code5 = entity.Code5;

            HRDept dept = null;
            if (entity.DeptNo.HasValue)
            {
                Department department = IDDepartmentDao.Get(entity.DeptNo.Value);
                if (department != null) dept = _getHRDept(department);
            }
            if (dept != null) employee.HRDept = dept;
            user.HREmployee = employee;

            return user;
        }
        private HRDept _getHRDept(Department department)
        {
            HRDept dept = new HRDept();
            dept.Id = department.Id;
            dept.CName = department.CName;
            dept.Shortcode = department.ShortCode;
            dept.IsUse = department.Visible;
            dept.DispOrder = department.DispOrder;
            dept.Code1 = department.Code1;
            dept.Code2 = department.Code2;
            dept.Code3 = department.Code3;
            dept.Code4 = department.Code4;
            dept.Code5 = department.Code5;

            return dept;
        }
        #endregion

        #region 按指定字段获取人员信息
        public PUser SearchPUserByFieldKey(string fieldKey, string fieldVal)
        {
            if (string.IsNullOrEmpty(fieldKey) || string.IsNullOrEmpty(fieldVal)) return null;

            PUser entity = new PUser();
            IList<PUser> userList = this.SearchListByHQL("puser.Visible=1 and puser." + fieldKey + "='" + fieldVal + "'");
            if (userList.Count > 0)
            {
                entity = userList[0];
            }
            else
            {
                entity = null;
            }
            return entity;
        }
        public RBACUser SearchRBACUserByFieldKey(string fieldKey, string fieldVal)
        {
            if (string.IsNullOrEmpty(fieldKey) || string.IsNullOrEmpty(fieldVal)) return null;
            RBACUser entity = new RBACUser();

            PUser puser = new PUser();
            IList<PUser> userList = this.SearchListByHQL("puser.Visible=1 and puser." + fieldKey + "='" + fieldVal + "'");
            if (userList.Count > 0)
            {
                puser = userList[0];
            }
            else
            {
                puser = null;
                entity = null;
            }
            if (puser != null)
                entity = _getRBACUser(puser);
            return entity;
        }

        #endregion

        #region 修改信息记录
        public void AddSCOperation(PUser serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemoHelp.EditGetUpdateMemo<PUser>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "PUser";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.人员修改记录.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
       #endregion

        #region 同步处理
        public Dictionary<JObject, JObject> GetSyncPUserList()
        {
            Dictionary<JObject, JObject> objList = new Dictionary<JObject, JObject>();
            //IList<JObject> objList = new List<JObject>();
            IList<PUser> allList = this.LoadAll();
            //byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (var entity in allList)
            {
                if (entity.Id < 0 || entity.CName.Length <= 0) continue;

                //人员信息
                JObject jemp = new JObject();
                jemp.Add("LabID", entity.LabID.ToString());
                jemp.Add("Id", entity.Id.ToString());
                if (entity.CName.Length > 0)
                {
                    jemp.Add("NameL", entity.CName.Substring(0, 1));
                    jemp.Add("NameF", entity.CName.Substring(1, entity.CName.Length - 1));
                    jemp.Add("CName", entity.CName);
                }
                jemp.Add("ShortCode", entity.ShortCode);
                jemp.Add("DispOrder", entity.DispOrder);
                if (!string.IsNullOrEmpty(entity.Code1))
                    jemp.Add("StandCode", entity.Code1);
                if (!string.IsNullOrEmpty(entity.Code2))
                    jemp.Add("SName", entity.Code2);
                jemp.Add("IsUse", entity.Visible);
                jemp.Add("IsEnabled", 1);//在职

                //人员对应帐号信息
                JObject juser = new JObject();
                juser.Add("LabID", entity.LabID.ToString());
                juser.Add("Id", entity.Id.ToString());
                juser.Add("CName", entity.CName);
                juser.Add("Account", entity.ShortCode);
                juser.Add("ShortCode", entity.ShortCode);
                juser.Add("PWD", UnCovertPassWord(entity.Password));
                juser.Add("EnMPwd", 1);//允许修改密码
                juser.Add("PwdExprd", 1);//密码永不过期
                juser.Add("AccLock", 0);//帐号被锁定
                //juser.Add("AccBeginTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//帐号启用时间 

                juser.Add("DispOrder", entity.DispOrder);
                if (!string.IsNullOrEmpty(entity.Code1))
                    juser.Add("StandCode", entity.Code1);
                if (!string.IsNullOrEmpty(entity.Code2))
                    juser.Add("SName", entity.Code2);
                juser.Add("IsUse", entity.Visible);

                JObject jemp2 = new JObject();
                jemp2.Add("Id", entity.Id.ToString());
                //jemp2.Add("DataTimeStamp", "0x0000000000FBA909");
                juser.Add("HREmployee", jemp2.ToString().Replace(Environment.NewLine, ""));//.ToString().Replace(Environment.NewLine, "")

                //人员实体
                JObject empentity = new JObject();
                empentity.Add("entity", jemp);
                //人员帐号实体
                JObject userentity = new JObject();
                userentity.Add("entity", juser);

                objList.Add(empentity, userentity);
            }
            return objList;
        }
        #endregion
    }
}