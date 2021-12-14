using System;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.LIIP;
using ZhiFang.Entity.LIIP;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.LIIP;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.Entity.RBAC;
using System.Collections.Generic;
using ZhiFang.Common.Public;
using ZhiFang.Entity.LIIP.ViewObject.DicReceive;
using ZhiFang.IDAO.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public class BSAccountRegister : BaseBLL<SAccountRegister>, IBSAccountRegister
    {
        IDHREmployeeDao IDHREmployeeDao { get; set; }
        IDHRDeptDao IDHRDeptDao { get; set; }

        IDRBACUserDao IDRBACUserDao { get; set; }
        IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        IDBHospitalEmpLinkDao IDBHospitalEmpLinkDao { get; set; }
        IDRBACRoleDao IDRBACRoleDao { get; set; }
        IDBHospitalDao IDBHospitalDao { get; set; }
        IDBSexDao IDBSexDao { get; set; }

        public BaseResultDataValue AddEntity(SAccountRegister entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (DBDao.GetListCountByHQL("Account='" + entity.Account + "' and StatusId <> "+ AccountRegisterApprovalType.审批打回.Key+ " ") > 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "账户名称已被申请！";
                return brdv;
            }

            if (!string.IsNullOrEmpty(entity.IdInfoNo))
            {
                if (DBDao.GetListCountByHQL("IdInfoNo='" + entity.IdInfoNo + "' and StatusId <> " + AccountRegisterApprovalType.审批打回.Key + " ") > 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "当前用户已申请过，请不要重复申请！";
                    return brdv;
                }
            }

            if (IDRBACUserDao.GetListCountByHQL("Account='" + entity.Account + "'") > 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "账户名称已存在！";
                return brdv;
            }
            brdv.success= DBDao.Save(entity);
            return brdv;
        }

        public BaseResultDataValue ST_UDTO_ApprovalSAccountRegister(SAccountRegister entity, bool IsPass)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                SAccountRegister sAccountRegister = DBDao.Get(entity.Id);

                if (sAccountRegister.StatusId == long.Parse(AccountRegisterApprovalType.审批通过.Key) || sAccountRegister.StatusId == long.Parse(AccountRegisterApprovalType.审批打回.Key))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "请勿重复审批！";
                    return brdv;
                }
                if (IsPass)//ture审批通过  false审批打回
                {
                    #region 验证
                    
                    if (IDRBACUserDao.GetListCountByHQL("Account='" + sAccountRegister.Account + "'") > 0) {
                        brdv.success = false;
                        brdv.ErrorInfo = "账户名称已存在！";
                        return brdv;
                    }
                    #endregion
                    #region 新增人员HREmployee
                    HREmployee hREmployee = new HREmployee();
                    IList<HRDept> hRDepts = IDHRDeptDao.GetListByHQL("StandCode = 'IEQAYH'");
                    hREmployee.HRDept = hRDepts.Count > 0 ? hRDepts[0] : null;
                    hREmployee.NameL = sAccountRegister.Name.Substring(0, 1);
                    hREmployee.NameF = sAccountRegister.Name.Substring(1);
                    hREmployee.CName = sAccountRegister.Name;
                    hREmployee.EName = sAccountRegister.EName;
                    hREmployee.IsEnabled = 1;
                    hREmployee.IsUse = true;
                    hREmployee.Birthday = sAccountRegister.Birthday != null ? sAccountRegister.Birthday : null;
                    //var sex= IDBSexDao.Get(sAccountRegister.SexID);
                    hREmployee.BSex = sAccountRegister.SexID != 0 ? IDBSexDao.Get(sAccountRegister.SexID) : null;
                    hREmployee.Email = string.IsNullOrEmpty(sAccountRegister.EMAIL) ? null : sAccountRegister.EMAIL;
                    hREmployee.MobileTel = string.IsNullOrEmpty(sAccountRegister.MobileTel) ? null : sAccountRegister.MobileTel;
                    hREmployee.Address = string.IsNullOrEmpty(sAccountRegister.Address) ? null : sAccountRegister.Address;
                    hREmployee.DataAddTime = DateTime.Now;
                    object empid = IDHREmployeeDao.SaveByEntity(hREmployee);
                    #endregion
                    if (!string.IsNullOrEmpty(empid.ToString()) && empid.ToString() != "0")
                    {
                        #region 新增帐户RBAC_User
                        RBACUser rBACUser = new RBACUser();
                        hREmployee.Id = long.Parse(empid.ToString());
                        rBACUser.HREmployee = hREmployee;
                        rBACUser.Account = sAccountRegister.Account;
                        rBACUser.PWD = SecurityHelp.MD5Encrypt(sAccountRegister.PassWord, SecurityHelp.PWDMD5Key);
                        rBACUser.EnMPwd = true;
                        rBACUser.PwdExprd = true;
                        rBACUser.AccLock = false;
                        rBACUser.CName = sAccountRegister.Name;
                        rBACUser.EName = sAccountRegister.EName;
                        rBACUser.IsUse = true;
                        rBACUser.DataAddTime = new DateTime();
                        bool user = IDRBACUserDao.Save(rBACUser);
                        #endregion
                        #region 新增员工角色RBAC_EmpRoles
                        foreach (var item in entity.RolesIDList)
                        {
                            RBACEmpRoles rBACEmpRoles = new RBACEmpRoles();
                            rBACEmpRoles.HREmployee = hREmployee;
                            IList<RBACRole> rBACRoles= IDRBACRoleDao.GetListByHQL("Id = " + long.Parse(item));
                            rBACEmpRoles.RBACRole = rBACRoles[0];
                            rBACEmpRoles.IsUse = true;
                            rBACEmpRoles.DataAddTime = new DateTime();
                            bool role =  IDRBACEmpRolesDao.Save(rBACEmpRoles);
                        }
                        #endregion
                        #region 新增B_HospitalEmpLink医院人员关系
                        BHospitalEmpLink bHospitalEmpLink = new BHospitalEmpLink();
                        bHospitalEmpLink.HospitalID = entity.HospitalID;
                        var hosp=IDBHospitalDao.Get(entity.HospitalID);
                        bHospitalEmpLink.EmpID = long.Parse(empid.ToString());
                        bHospitalEmpLink.HospitalCode = hosp.HospitalCode;
                        bHospitalEmpLink.HospitalName = hosp.Name;
                        bHospitalEmpLink.EmpName = sAccountRegister.Name;
                        bHospitalEmpLink.DataAddTime = new DateTime();
                        bHospitalEmpLink.IsUse = true;
                        bHospitalEmpLink.LinkTypeID = long.Parse(ZFHospitalEmpLinkType.所属.Key);
                        bHospitalEmpLink.LinkTypeName = ZFHospitalEmpLinkType.所属.Value.Name;
                        bool helink = IDBHospitalEmpLinkDao.Save(bHospitalEmpLink);
                        #endregion
                        entity.EmpID = long.Parse(empid.ToString());
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "新增人员失败！";
                        return brdv;
                    }
                    entity.StatusId = long.Parse(AccountRegisterApprovalType.审批通过.Key);
                    entity.StatusName = AccountRegisterApprovalType.审批通过.Value.Name;
                }
                else
                {                   
                    entity.StatusId = long.Parse(AccountRegisterApprovalType.审批打回.Key);
                    entity.StatusName = AccountRegisterApprovalType.审批打回.Value.Name;
                }
                entity.DataUpdateTime = DateTime.Now;
                entity.ApprovalDateTime = DateTime.Now;
                entity.ApprovalID = long.Parse(ZhiFang.LIIP.Common.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                entity.ApprovalName = ZhiFang.LIIP.Common.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                string fields = "Id,EmpID,HospitalID,HospitalCode,ApprovalID,ApprovalName,ApprovalDateTime,ApprovalInfo,StatusId,StatusName,DataUpdateTime";
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                bool isok = DBDao.Update(tempArray);
                if (isok)
                {
                    brdv.success = true;
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "审批失败！";
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
            }
            return brdv;
        }

        public bool SetOpenIdByEmpId(string weiXinMiniOpenID, string EmpId)
        {
           return DBDao.UpdateByHql(" update SAccountRegister set IdInfoNo='" + weiXinMiniOpenID + "' where  EmpId= " + EmpId)>0;
        }
    }
}