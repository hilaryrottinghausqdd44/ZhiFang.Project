
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.IDAO.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.RBAC
{
    /// <summary>
    ///
    /// </summary>
    public class BDoctor : BaseBLL<Doctor, int>, IBDoctor
    {
        IDBlooddocGradeDao IDBlooddocGradeDao { get; set; }
        IDDepartmentDao IDDepartmentDao { get; set; }
        IDPUserDao IDPUserDao { get; set; }
        IBPUser IBPUser { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IDDepartmentUserDao IDDepartmentUserDao { get; set; }

        public BaseResultDataValue GetSysCurDoctorInfoByAccount(string account, string pwd)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(account))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "提示信息：用户帐号信息不能为空!";
                return tempBaseResultDataValue;
            }
            //先按用户帐号从PUser获取到HisOrderCode
            string hql = " puser.Visible=1 and  puser.ShortCode='" + account + "'";
            if (!string.IsNullOrEmpty(pwd))
            {
                //加密密码后再查询
                pwd = IBPUser.CovertPassWord(pwd);
                hql = hql + " and puser.Password = '" + pwd + "' ";
            }
            IList<PUser> userList = IDPUserDao.GetListByHQL(hql);
            if (userList == null || userList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "提示信息：登录帐号为:" + account + ",获取用户帐号信息为空!";
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                return tempBaseResultDataValue;
            }
            if (userList.Count > 1)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "提示信息：登录帐号为:" + account + ",获取用户帐号信息存在多个!";
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                return tempBaseResultDataValue;
            }

            PUser puser = userList[0];
            string hisDoctorCode = "";
            Doctor doctor = null;
            if (puser.Doctor == null)
            {
                //按puser的code1到code5匹配Doctor的code1到code5
                string hql1 = string.Format(" doctor.Visible=1 and (doctor.Code1 ='{0}' or  doctor.Code2 ='{1}' or doctor.Code3='{2}' or doctor.Code4='{3}' or doctor.Code5='{4}')", puser.Code1, puser.Code2, puser.Code3, puser.Code3, puser.Code5);
                ZhiFang.Common.Log.Log.Debug("按puser的Code1--Code5获取查询doctor信息hql:" + hql1);
                IList<Doctor> doctorList = this.SearchListByHQL(hql1);
                if (doctorList == null || doctorList.Count <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "提示信息：登录帐号为:" + account + ",获取医生信息为空!";
                    ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                    return tempBaseResultDataValue;
                }
                var tempList = doctorList.Where(p => p.Code1 == puser.Code1);
                if (tempList.Count() > 0 && !string.IsNullOrEmpty(puser.Code1))
                {
                    doctor = tempList.ElementAt(0);
                    hisDoctorCode = puser.Code1;
                }
                if (doctor == null && !string.IsNullOrEmpty(puser.Code2))
                {
                    tempList = doctorList.Where(p => p.Code2 == puser.Code2);
                    if (tempList.Count() > 0)
                    {
                        doctor = tempList.ElementAt(0);
                        hisDoctorCode = puser.Code2;
                    }
                }
                if (doctor == null && !string.IsNullOrEmpty(puser.Code3))
                {
                    tempList = doctorList.Where(p => p.Code3 == puser.Code3);
                    if (tempList.Count() > 0)
                    {
                        doctor = tempList.ElementAt(0);
                        hisDoctorCode = puser.Code3;
                    }
                }
                if (doctor == null && !string.IsNullOrEmpty(puser.Code4))
                {
                    tempList = doctorList.Where(p => p.Code4 == puser.Code4);
                    if (tempList.Count() > 0)
                    {
                        doctor = tempList.ElementAt(0);
                        hisDoctorCode = puser.Code4;
                    }
                }
                if (doctor == null && !string.IsNullOrEmpty(puser.Code5))
                {
                    tempList = doctorList.Where(p => p.Code5 == puser.Code5);
                    if (tempList.Count() > 0)
                    {
                        doctor = tempList.ElementAt(0);
                        hisDoctorCode = puser.Code5;
                    }
                }
            }
            else
            {
                doctor = this.Get(puser.Doctor.Id);
            }

            if (doctor == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "提示信息：登录帐号为:" + account + ",获取医生信息为空!";
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                return tempBaseResultDataValue;
            }
            tempBaseResultDataValue = GetSysCurDoctorInfo(puser, doctor, hisDoctorCode, "", false);

            return tempBaseResultDataValue;
        }
        public BaseResultDataValue GetSysCurDoctorInfoByHisCode(string hisDoctorCode, string hisDeptCode, bool isAutoAddDepartmentUser)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //ZhiFang.Common.Log.Log.Error("HIS传入的医生编码为:" + hisDoctorCode + ",HIS传入的科室编码为:" + hisDeptCode);
            if (string.IsNullOrEmpty(hisDoctorCode))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "提示信息：HIS医生编码不能为空!";
                return tempBaseResultDataValue;
            }
            string hql1 = string.Format(" doctor.Visible=1 and (doctor.Code1 ='{0}' or  doctor.Code2 ='{1}' or doctor.Code3='{2}' or doctor.Code4='{3}' or doctor.Code5='{4}')", hisDoctorCode, hisDoctorCode, hisDoctorCode, hisDoctorCode, hisDoctorCode);
            ZhiFang.Common.Log.Log.Debug("按hisDoctorCode获取查询doctor信息hql:" + hql1);
            IList<Doctor> doctorList = ((IDDoctorDao)base.DBDao).GetListByHQL(hql1);// this.SearchListByHQL(hql1);
            if (doctorList == null || doctorList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "提示信息：获取医生信息为空!";
                ZhiFang.Common.Log.Log.Error("HIS传入的医生编码为:" + hisDoctorCode + ",在LIS未获取到对照信息!");
                return tempBaseResultDataValue;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("HIS传入的医生编码为:" + hisDoctorCode + ",获取到LIS医生编码为:" + doctorList[0].Id);
            }
            tempBaseResultDataValue = GetSysCurDoctorInfo(null, doctorList[0], hisDoctorCode, hisDeptCode, isAutoAddDepartmentUser);
            return tempBaseResultDataValue;
        }
        private BaseResultDataValue GetSysCurDoctorInfo(PUser puser, Doctor doctor, string hisDoctorCode, string hisDeptCode, bool isAutoAddDepartmentUser)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            SysCurUserInfo userInfo = new SysCurUserInfo();

            if (doctor.GradeNo > 0)
            {
                BlooddocGrade docGrade = IDBlooddocGradeDao.Get(doctor.GradeNo);
                if (docGrade != null)
                {
                    userInfo.GradeId = docGrade.Id.ToString();
                    userInfo.GradeName = docGrade.CName;
                    userInfo.LowLimit = docGrade.LowLimit;
                    userInfo.UpperLimit = docGrade.UpperLimit;
                    if (!userInfo.UpperLimit.HasValue && docGrade.BCount.HasValue)
                        userInfo.UpperLimit = docGrade.BCount;
                    ZhiFang.Common.Log.Log.Debug("HIS传入的医生编码为:" + hisDoctorCode + ",医生所属等级信息:"+ userInfo.GradeName+",用血量权限范围:"+ userInfo.LowLimit+"--"+ userInfo.UpperLimit);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("HIS传入的医生编码为:" + hisDoctorCode + ",在LIS未设置医生的所属等级信息.GetSysCurDoctorInfo.docGrade");
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("HIS传入的医生编码为:" + hisDoctorCode + ",在LIS未设置医生的所属等级信息.GetSysCurDoctorInfo.doctor2");
            }
            //科室信息处理
            if (!string.IsNullOrEmpty(hisDeptCode))
            {
                string hql1 = string.Format(" department.Visible=1 and (department.Code1='{0}' or department.Code2='{1}' or department.Code3='{2}' or department.Code4='{3}' or department.Code5='{4}' )", hisDeptCode, hisDeptCode, hisDeptCode, hisDeptCode, hisDeptCode);
                ZhiFang.Common.Log.Log.Debug("按hisDeptCode获取查询department信息hql:" + hql1);
                IList<Department> deptList = IDDepartmentDao.GetListByHQL(hql1);
                if (deptList != null && deptList.Count > 0)
                {
                    Department department = deptList[0];
                    userInfo.DeptId = department.Id.ToString();
                    userInfo.DeptCName = department.CName;
                    userInfo.HisDeptId = hisDeptCode;
                    ZhiFang.Common.Log.Log.Debug("HIS传入的科室编码为:" + hisDeptCode + ",获取到LIS科室编码为:" + userInfo.DeptId);
                    //判断是否存在科室人员关系信息
                    if (isAutoAddDepartmentUser)
                        AddDepartmentUser(puser, doctor, department, hisDoctorCode, hisDeptCode);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("HIS传入的科室编码为:" + hisDeptCode + ",在LIS未获取到对照信息!");
                }
            }
            if (puser != null)
            {
                userInfo.Account = puser.ShortCode;
                userInfo.Password = IBPUser.UnCovertPassWord(puser.Password);
            }
            else
            {
                userInfo.Account = "";
                userInfo.Password = "";
            }
            userInfo.DoctorId = doctor.Id.ToString();
            userInfo.DoctorCName = doctor.CName;
            userInfo.HisDoctorId = hisDoctorCode;

            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(userInfo);

            return tempBaseResultDataValue;
        }
        private void AddDepartmentUser(PUser puser, Doctor doctor, Department department, string hisDoctorCode, string hisDeptCode)
        {
            if (puser == null)
            {
                string hql = " puser.Doctor.Id=" + doctor.Id + "";// puser.Visible=1 and  
                IList<PUser> userList = IDPUserDao.GetListByHQL(hql);
                if (userList.Count > 0)
                {
                    puser = userList[0];
                    ZhiFang.Common.Log.Log.Info("(按人员帐号绑定医生信息)LIS医生编码为:" + doctor.Id + ",获取医生的人员帐号编码为:" + puser.Id);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("(按人员帐号绑定医生信息)LIS医生编码为:" + doctor.Id + ",获取医生的人员帐号信息为空!");
                    hql = string.Format("(puser.Code1 ='{0}' or  puser.Code2 ='{1}' or puser.Code3='{2}' or puser.Code4='{3}' or puser.Code5='{4}')", hisDoctorCode, hisDoctorCode, hisDoctorCode, hisDoctorCode, hisDoctorCode);
                    userList = IDPUserDao.GetListByHQL(hql);
                    if (userList.Count > 0)
                    {
                        puser = userList[0];
                        ZhiFang.Common.Log.Log.Info("(按HIS对照码)LIS医生编码为:" + doctor.Id + ",获取医生的人员帐号编码为:" + puser.Id);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("(按HIS对照码)LIS医生编码为:" + doctor.Id + ",HIS医生编码为:" + hisDoctorCode + ",获取医生的人员帐号信息为空!");
                    }
                }
            }
            if (puser != null && department != null)
            {
                IList<DepartmentUser> deptUserList = IDDepartmentUserDao.GetListByHQL("departmentuser.PUser.Id=" + puser.Id + " and departmentuser.Department.Id=" + department.Id);
                if (deptUserList == null || deptUserList.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("HIS科室编码为:" + hisDeptCode + ",HIS医生编码为:" + hisDoctorCode + ",在LIS不存在科室人员关系信息,LIS自动建立科室人员关系!");
                    DepartmentUser deptUser = new DepartmentUser();
                    deptUser.PUser = puser;
                    deptUser.Department = department;
                    bool result = IDDepartmentUserDao.Save(deptUser);
                    ZhiFang.Common.Log.Log.Debug("科室人员关系自动建立保存结果:" + result);
                }
            }
        }
        private void EditUserAndDoctor(PUser puser, string hisDoctorCode)
        {
            if (puser != null && puser.Doctor != null)
            {
                IList<PUser> userList = ((IDPUserDao)base.DBDao).GetListByHQL("puser.Id=" + puser.Id + " and puser.Doctor.Id=" + puser.Doctor.Id);
                if (userList == null || userList.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("HIS医生编码为:" + hisDoctorCode + ",在LIS不存在人员医生绑定关系信息,LIS自动建立人员医生绑定关系!");
                    DepartmentUser deptUser = new DepartmentUser();
                    if (string.IsNullOrEmpty(puser.Usertype))
                        puser.Usertype = BloodIdentityType.医生.Key;
                    IBPUser.Entity = puser;
                    bool result = IBPUser.Edit();
                    ZhiFang.Common.Log.Log.Debug("人员医生绑定关系自动建立保存结果:" + result);
                }
            }
        }
        #region 修改信息记录
        public void AddSCOperation(Doctor serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemo<Doctor>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "Doctor";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.医生修改记录.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        private void EditGetUpdateMemo<T>(T serverEntity, T updateEntity, Type type, string[] arrFields, ref StringBuilder strbMemo)
        {
            //为空判断
            if (serverEntity == null && updateEntity == null)
                return;
            else if (serverEntity == null || updateEntity == null)
                return;

            Type t = type;
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            foreach (var po in props)
            {
                if (po.Name == "Id" || po.Name == "LabID" || po.Name == "DataAddTime" || po.Name == "DataUpdateTime" || po.Name == "DataTimeStamp")
                    continue;

                if (arrFields.Contains(po.Name) == true && IsCanCompare(po.PropertyType))
                {
                    object serverValue = po.GetValue(serverEntity, null);
                    object updateValue = po.GetValue(updateEntity, null);
                    if (serverValue == null)
                        serverValue = "";
                    if (updateValue == null)
                        updateValue = "";
                    if (!serverValue.ToString().Equals(updateValue.ToString()))
                    {
                        string cname = po.Name;
                        foreach (var pattribute in po.GetCustomAttributes(false))
                        {
                            if (pattribute.ToString() == "ZhiFang.Entity.Base.DataDescAttribute")
                            {
                                cname = ((DataDescAttribute)pattribute).CName;
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(serverValue.ToString()))
                            serverValue = "空";
                        if (string.IsNullOrEmpty(updateValue.ToString()))
                            updateValue = "空";
                        strbMemo.Append("【" + cname + "】由原来:" + serverValue.ToString() + ",修改为:" + updateValue.ToString() + ";" + System.Environment.NewLine);
                    }
                }
            }
        }
        /// <summary>
        /// 该类型是否可直接进行值的比较
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsCanCompare(Type t)
        {
            if (t.IsValueType)
            {
                return true;
            }
            else
            {
                //String是特殊的引用类型，它可以直接进行值的比较
                if (t.FullName == typeof(String).FullName)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

    }
}