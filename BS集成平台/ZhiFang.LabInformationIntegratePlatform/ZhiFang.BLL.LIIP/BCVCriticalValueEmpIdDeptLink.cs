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

namespace ZhiFang.BLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public class BCVCriticalValueEmpIdDeptLink : BaseBLL<CVCriticalValueEmpIdDeptLink>, IBCVCriticalValueEmpIdDeptLink
    {
        IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }

        IDAO.RBAC.IDRBACUserDao IDRBACUserDao { get; set; }

        IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }

        IDAO.RBAC.IDRBACRoleDao IDRBACRoleDao { get; set; }

        IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }

        IDAO.RBAC.IDBSexDao IDBSexDao { get; set; }

        public BaseResultDataValue CV_SearchAndAddDoctorOrNurse(CV_AddDoctorOrNurseVO entity, out RBACUser rbacuser)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<RBACUser> tempRBACUser = IDRBACUserDao.GetListByHQL(" Account='" + entity.Account + "' ");
            rbacuser = null;
            if (tempRBACUser.Count == 1)
            {
                rbacuser = tempRBACUser[0];
                return AccountVerification(entity, rbacuser);
            }
            else
            {
                bool empflag = false;
                bool userflag = false;
                bool roleflag = false;
                bool deptflag = false;
                bool cvdeptlinkflag = false;

                #region 注册部门
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门开始！");
                if (entity.DeptHISCode == null || entity.DeptHISCode.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门.部门编码为空！");
                    brdv.ErrorInfo = "传入参数部门编码为空！";
                    return brdv;
                }

                if (entity.DeptName == null || entity.DeptName.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门.部门名称为空！");
                    brdv.ErrorInfo = "传入参数部门名称为空！";
                    return brdv;
                }

                if (entity.DeptHISCode.Trim().Split(',').Count() != entity.DeptName.Trim().Split(',').Count())
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门.部门名称和部门编码数组长度不一致！");
                    brdv.ErrorInfo = "部门名称和部门编码数组长度不一致！";
                    return brdv;
                }

                if (entity.DeptLISCode != null && entity.DeptLISCode.Trim() != "" && entity.DeptLISCode.Trim().Split(',').Count() != entity.DeptName.Trim().Split(',').Count())
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门.部门Lis编码和部门编码、部门名称数组长度不一致！");
                    brdv.ErrorInfo = "部门名称和部门编码数组长度不一致！";
                    return brdv;
                }

                string[] depthiscode = entity.DeptHISCode.Split(',');
                string[] deptliscode = (entity.DeptLISCode != null && entity.DeptLISCode.Trim() != "") ? entity.DeptLISCode.Split(',') : new string[0];
                string[] depthisname = entity.DeptName.Split(',');
                var tmpDeptList = IDHRDeptDao.GetListByHQL(" DeveCode in ('" + string.Join("','", depthiscode) + "') ");
                List<HRDept> NewHRDept = new List<HRDept>();
                if (tmpDeptList.Count != depthiscode.Length)
                {
                    for (int i = 0; i < depthiscode.Length; i++)
                    {
                        if (tmpDeptList.Count(a => a.DeveCode == depthiscode[i]) <= 0)
                        {
                            HRDept dept = new HRDept();
                            dept.CName = depthisname[i];
                            dept.DeveCode = depthiscode[i];
                            if (deptliscode.Length > 0)
                            {
                                dept.StandCode = deptliscode[i];
                            }
                            NewHRDept.Add(dept);
                        }
                    }
                }
                foreach (var dept in NewHRDept)
                {
                    IDHRDeptDao.Save(dept);
                }
                deptflag = true;
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门结束！deptflag:" + deptflag);
                if (!deptflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册部门失败！";
                    return brdv;
                }
                #endregion

                #region 注册员工
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工开始！");
                HREmployee emp = new HREmployee();
                emp.Id = GUIDHelp.GetGUIDLong();
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工.emp.Id:" + emp.Id);
                emp.CName = entity.Name;
                emp.NameL = entity.Name;
                emp.NameF = "";
                emp.MobileTel = entity.Phone;
                var sex = IDBSexDao.GetListByHQL(" Name='" + entity.Sex + "' ");
                if (sex != null && sex.Count > 0)
                {
                    emp.BSex = sex.ElementAt(0);
                }

                if (tmpDeptList != null && tmpDeptList.Count > 0)
                {
                    emp.HRDept = tmpDeptList.Where(a => a.DeveCode.Trim().ToLower() == depthiscode[0].Trim().ToLower()).ElementAt(0);
                }
                else
                {
                    emp.HRDept = NewHRDept.Where(a => a.DeveCode.Trim().ToLower() == depthiscode[0].Trim().ToLower()).ElementAt(0);
                }
                emp.IsEnabled = 1;
                emp.IsUse = true;
                if (entity.HISCode != null && entity.HISCode.Trim() != "")
                {
                    emp.StandCode = entity.HISCode;//医生工号，HIS编码
                    emp.DeveCode= entity.HISCode;
                }
                else
                {
                    emp.StandCode = entity.Account;//医生工号，HIS编码
                    emp.DeveCode = entity.Account;
                }
                if (IDHREmployeeDao.GetListCountByHQL(" StandCode='" + emp.StandCode + "' or DeveCode='" + emp.DeveCode + "' ") <= 0)
                {
                    empflag = IDHREmployeeDao.Save(emp);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工失败！人员的LIS编码"+ emp.StandCode + "或HIS编码" + emp.DeveCode + "重复！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册员工失败！人员的LIS编码或HIS编码重复！";
                    return brdv;
                }
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工结束！empflag:" + empflag);
                if (!empflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册员工失败！";
                    return brdv;
                }
                #endregion

                #region 注册账号
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册账号开始！");
                RBACUser user = new RBACUser();
                user.CName = emp.CName;
                user.HREmployee = emp;
                user.Account = entity.Account;
                user.IsUse = true;
                user.PWD = SecurityHelp.MD5Encrypt(entity.PWD, SecurityHelp.PWDMD5Key);
                user.StandCode = entity.Account;
                user.AccLock = false;
                user.AccExprd = true;
                user.PwdExprd = true;
                user.EnMPwd = true;
                userflag = IDRBACUserDao.Save(user);
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册账号结束！userflag:" + userflag);
                if (!userflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册账号失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册账号失败！";
                    return brdv;
                }
                #endregion

                #region 账号角色
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.账号角色开始！");
                ZhiFang.Entity.Common.BaseClassDicEntity roleValue = entity.Type == "医生" ? ZFSystemRole.智方_系统角色_医生.Value : ZFSystemRole.智方_系统角色_护士.Value;
                var rolelist = IDRBACRoleDao.GetListByHQL(" DeveCode='" + roleValue.Code + "' ");
                RBACEmpRoles emprole = new RBACEmpRoles();
                emprole.HREmployee = emp;
                emprole.IsUse = true;
                if (rolelist.Count > 0)
                {
                    emprole.RBACRole = rolelist.ElementAt(0);
                }
                else
                {
                    RBACRole role = new RBACRole();
                    role.CName = roleValue.Name;
                    role.Id = long.Parse(roleValue.Id);
                    role.StandCode = roleValue.Code;
                    role.DeveCode = roleValue.Code;
                    role.DispOrder = int.Parse(roleValue.Id);
                    role.IsUse = true;
                    IDRBACRoleDao.Save(role);
                    emprole.RBACRole = role;
                }
                roleflag = IDRBACEmpRolesDao.Save(emprole);
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.账号角色结束！roleflag:" + roleflag);
                if (!roleflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册账号角色失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册账号角色失败！";
                    return brdv;
                }
                #endregion

                #region 危急值部门权限
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.危急值部门权限开始！");
                cvdeptlinkflag = true;
                foreach (var dept in tmpDeptList)
                {
                    CVCriticalValueEmpIdDeptLink cvempdeptlink = new CVCriticalValueEmpIdDeptLink();
                    cvempdeptlink.DeptID = dept.Id;
                    cvempdeptlink.DeptName = dept.CName;
                    cvempdeptlink.EmpID = emp.Id;
                    cvempdeptlink.EmpName = emp.CName;
                    cvempdeptlink.IsUse = true;
                    cvempdeptlink.DataAddTime = DateTime.Now;
                    if (!DBDao.Save(cvempdeptlink))
                    {
                        cvdeptlinkflag = false;
                        break;
                    }
                }

                foreach (var dept in NewHRDept)
                {
                    CVCriticalValueEmpIdDeptLink cvempdeptlink = new CVCriticalValueEmpIdDeptLink();
                    cvempdeptlink.DeptID = dept.Id;
                    cvempdeptlink.DeptName = dept.CName;
                    cvempdeptlink.EmpID = emp.Id;
                    cvempdeptlink.EmpName = emp.CName;
                    cvempdeptlink.IsUse = true;
                    cvempdeptlink.DataAddTime = DateTime.Now;
                    if (!DBDao.Save(cvempdeptlink))
                    {
                        cvdeptlinkflag = false;
                        break;
                    }
                }

                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.危急值部门权限结束！cvdeptlinkflag:" + cvdeptlinkflag);
                if (!cvdeptlinkflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册危急值部门权限失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册危急值部门权限失败！";
                    return brdv;
                }
                #endregion
                rbacuser = IDRBACUserDao.Get(user.Id);
                return brdv;
            }
        }
        public BaseResultDataValue CV_SearchAndAddTech(CV_AddTechVO entity, out RBACUser rbacuser)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<RBACUser> tempRBACUser = IDRBACUserDao.GetListByHQL(" Account='" + entity.Account + "' ");
            rbacuser = null;
            if (tempRBACUser.Count == 1)
            {
                rbacuser = tempRBACUser[0];
                return AccountVerification(entity, rbacuser);
            }
            else
            {
                bool empflag = false;
                bool userflag = false;
                bool roleflag = false;
                bool deptflag = false;
                bool cvdeptlinkflag = false;

                #region 注册部门
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册部门开始！");
                if (entity.DeptLISCode == null || entity.DeptLISCode.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册部门.部门编码为空！");
                    brdv.ErrorInfo = "传入参数部门编码为空！";
                    return brdv;
                }

                if (entity.DeptName == null || entity.DeptName.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册部门.部门名称为空！");
                    brdv.ErrorInfo = "传入参数部门名称为空！";
                    return brdv;
                }
                if (entity.DeptLISCode.Trim().Split(',').Count() != entity.DeptName.Trim().Split(',').Count())
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册部门.部门名称和部门编码数组长度不一致！");
                    brdv.ErrorInfo = "部门名称和部门编码数组长度不一致！";
                    return brdv;
                }
                if (entity.DeptHISCode != null && entity.DeptHISCode.Trim() != "" && entity.DeptHISCode.Trim().Split(',').Count() != entity.DeptName.Trim().Split(',').Count())
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册部门.部门Lis编码和部门编码、部门名称数组长度不一致！");
                    brdv.ErrorInfo = "部门名称和部门编码数组长度不一致！";
                    return brdv;
                }

                string[] depthiscode = (entity.DeptHISCode != null && entity.DeptHISCode.Trim() != "") ? entity.DeptHISCode.Split(',') : new string[0];
                string[] deptliscode = entity.DeptLISCode.Split(',');
                string[] deptname = entity.DeptName.Split(',');
                var tmpDeptList = IDHRDeptDao.GetListByHQL(" StandCode in ('" + string.Join("','", deptliscode) + "') ");
                List<HRDept> NewHRDept = new List<HRDept>();
                if (tmpDeptList.Count != deptliscode.Length)
                {
                    for (int i = 0; i < deptliscode.Length; i++)
                    {
                        if (tmpDeptList.Count(a => a.StandCode == deptliscode[i]) <= 0)
                        {
                            HRDept dept = new HRDept();
                            dept.CName = deptname[i];
                            dept.StandCode = deptliscode[i];
                            if (depthiscode.Length > 0)
                            {
                                dept.DeveCode = depthiscode[i];
                            }
                            NewHRDept.Add(dept);
                        }
                    }
                }
                foreach (var dept in NewHRDept)
                {
                    IDHRDeptDao.Save(dept);
                }
                deptflag = true;
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册部门结束！deptflag:" + deptflag);
                if (!deptflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册部门失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册部门失败！";
                    return brdv;
                }
                #endregion

                #region 注册员工
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册员工开始！");
                HREmployee emp = new HREmployee();
                emp.Id = GUIDHelp.GetGUIDLong();
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册员工.emp.Id:" + emp.Id);
                emp.CName = entity.Name;
                emp.NameL = entity.Name;
                emp.NameF = "";
                emp.MobileTel = entity.Phone;
                var sex = IDBSexDao.GetListByHQL(" Name='" + entity.Sex + "' ");
                if (sex != null && sex.Count > 0)
                {
                    emp.BSex = sex.ElementAt(0);
                }

                if (tmpDeptList != null && tmpDeptList.Count > 0)
                {
                    emp.HRDept = tmpDeptList.Where(a => a.StandCode.Trim().ToLower() == deptliscode[0].Trim().ToLower()).ElementAt(0);
                }
                else
                {
                    emp.HRDept = NewHRDept.Where(a => a.StandCode.Trim().ToLower() == deptliscode[0].Trim().ToLower()).ElementAt(0);
                }
                emp.IsEnabled = 1;
                emp.IsUse = true;
                emp.StandCode = entity.Account;//医生工号，HIS编码
                empflag = IDHREmployeeDao.Save(emp);
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册员工结束！empflag:" + empflag);
                if (!empflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册员工失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册员工失败！";
                    return brdv;
                }
                #endregion

                #region 注册账号
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册账号开始！");
                RBACUser user = new RBACUser();
                user.CName = emp.CName;
                user.HREmployee = emp;
                user.Account = entity.Account;
                user.IsUse = true;
                user.PWD = SecurityHelp.MD5Encrypt(entity.PWD, SecurityHelp.PWDMD5Key);
                user.StandCode = entity.Account;
                user.AccLock = false;
                user.AccExprd = true;
                user.PwdExprd = true;
                user.EnMPwd = true;
                userflag = IDRBACUserDao.Save(user);
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册账号结束！userflag:" + userflag);
                if (!userflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册账号失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册账号失败！";
                    return brdv;
                }
                #endregion

                #region 账号角色
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.账号角色开始！");
                var rolelist = IDRBACRoleDao.GetListByHQL(" DeveCode='" + ZFSystemRole.智方_系统角色_检验技师.Value.Code + "' ");
                RBACEmpRoles emprole = new RBACEmpRoles();
                emprole.HREmployee = emp;
                emprole.IsUse = true;
                if (rolelist.Count > 0)
                {
                    emprole.RBACRole = rolelist.ElementAt(0);
                }
                else
                {
                    RBACRole role = new RBACRole();
                    role.CName = ZFSystemRole.智方_系统角色_检验技师.Value.Name;
                    role.Id = long.Parse(ZFSystemRole.智方_系统角色_检验技师.Value.Id);
                    role.StandCode = ZFSystemRole.智方_系统角色_检验技师.Value.Code;
                    role.DeveCode = ZFSystemRole.智方_系统角色_检验技师.Value.Code;
                    role.DispOrder = int.Parse(ZFSystemRole.智方_系统角色_检验技师.Value.Id);
                    role.IsUse = true;
                    IDRBACRoleDao.Save(role);
                    emprole.RBACRole = role;
                }
                roleflag = IDRBACEmpRolesDao.Save(emprole);
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.账号角色结束！roleflag:" + roleflag);
                if (!roleflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册账号角色失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册账号角色失败！";
                    return brdv;
                }
                #endregion

                #region 危急值部门权限
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.危急值部门权限开始！");
                cvdeptlinkflag = true;
                foreach (var dept in tmpDeptList)
                {
                    CVCriticalValueEmpIdDeptLink cvempdeptlink = new CVCriticalValueEmpIdDeptLink();
                    cvempdeptlink.DeptID = dept.Id;
                    cvempdeptlink.DeptName = dept.CName;
                    cvempdeptlink.EmpID = emp.Id;
                    cvempdeptlink.EmpName = emp.CName;
                    cvempdeptlink.IsUse = true;
                    cvempdeptlink.DataAddTime = DateTime.Now;
                    if (!DBDao.Save(cvempdeptlink))
                    {
                        cvdeptlinkflag = false;
                        break;
                    }
                }

                foreach (var dept in NewHRDept)
                {
                    CVCriticalValueEmpIdDeptLink cvempdeptlink = new CVCriticalValueEmpIdDeptLink();
                    cvempdeptlink.DeptID = dept.Id;
                    cvempdeptlink.DeptName = dept.CName;
                    cvempdeptlink.EmpID = emp.Id;
                    cvempdeptlink.EmpName = emp.CName;
                    cvempdeptlink.IsUse = true;
                    cvempdeptlink.DataAddTime = DateTime.Now;
                    if (!DBDao.Save(cvempdeptlink))
                    {
                        cvdeptlinkflag = false;
                        break;
                    }
                }

                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.危急值部门权限结束！cvdeptlinkflag:" + cvdeptlinkflag);
                if (!cvdeptlinkflag)
                {
                    ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddTech.注册危急值部门权限失败！");
                    brdv.success = false;
                    brdv.ErrorInfo = "注册危急值部门权限失败！";
                    return brdv;
                }
                #endregion
                rbacuser = IDRBACUserDao.Get(user.Id);
                return brdv;
            }
        }

        public BaseResultDataValue CV_AddDoctorOrNurseToEmp(CV_AddDoctorOrNurseVO entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<RBACUser> tempRBACUser = IDRBACUserDao.GetListByHQL(" Account='" + entity.Account + "' ");

            bool empflag = false;
            bool userflag = false;
            bool roleflag = false;
            bool deptflag = false;
            bool cvdeptlinkflag = false;

            #region 注册部门
            ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门开始！");
            if (entity.DeptHISCode == null || entity.DeptHISCode.Trim() == "")
            {
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门.部门编码为空！");
                brdv.ErrorInfo = "传入参数部门编码为空！";
                return brdv;
            }

            if (entity.DeptName == null || entity.DeptName.Trim() == "")
            {
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门.部门名称为空！");
                brdv.ErrorInfo = "传入参数部门名称为空！";
                return brdv;
            }

            if (entity.DeptHISCode.Trim().Split(',').Count() != entity.DeptName.Trim().Split(',').Count())
            {
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门.部门名称和部门编码数组长度不一致！");
                brdv.ErrorInfo = "部门名称和部门编码数组长度不一致！";
                return brdv;
            }

            if (entity.DeptLISCode != null && entity.DeptLISCode.Trim() != "" && entity.DeptLISCode.Trim().Split(',').Count() != entity.DeptName.Trim().Split(',').Count())
            {
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门.部门Lis编码和部门编码、部门名称数组长度不一致！");
                brdv.ErrorInfo = "部门名称和部门编码数组长度不一致！";
                return brdv;
            }

            string[] depthiscode = entity.DeptHISCode.Split(',');
            string[] deptliscode = (entity.DeptLISCode != null && entity.DeptLISCode.Trim() != "") ? entity.DeptLISCode.Split(',') : new string[0];
            string[] depthisname = entity.DeptName.Split(',');
            var tmpDeptList = IDHRDeptDao.GetListByHQL(" DeveCode in ('" + string.Join("','", depthiscode) + "') ");
            List<HRDept> NewHRDept = new List<HRDept>();
            if (tmpDeptList.Count != depthiscode.Length)
            {
                for (int i = 0; i < depthiscode.Length; i++)
                {
                    if (tmpDeptList.Count(a => a.DeveCode == depthiscode[i]) <= 0)
                    {
                        HRDept dept = new HRDept();
                        dept.CName = depthisname[i];
                        dept.DeveCode = depthiscode[i];
                        if (deptliscode.Length > 0)
                        {
                            dept.StandCode = deptliscode[i];
                        }
                        NewHRDept.Add(dept);
                    }
                }
            }
            foreach (var dept in NewHRDept)
            {
                IDHRDeptDao.Save(dept);
            }
            deptflag = true;
            ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门结束！deptflag:" + deptflag);
            if (!deptflag)
            {
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册部门失败！");
                brdv.success = false;
                brdv.ErrorInfo = "注册部门失败！";
                return brdv;
            }
            #endregion

            #region 注册员工
            ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工开始！");
            HREmployee emp = new HREmployee();
            emp.Id = GUIDHelp.GetGUIDLong();
            ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工.emp.Id:" + emp.Id);
            emp.CName = entity.Name;
            emp.NameL = entity.Name;
            emp.NameF = "";
            emp.MobileTel = entity.Phone;
            var sex = IDBSexDao.GetListByHQL(" Name='" + entity.Sex + "' ");
            if (sex != null && sex.Count > 0)
            {
                emp.BSex = sex.ElementAt(0);
            }

            if (tmpDeptList != null && tmpDeptList.Count > 0)
            {
                emp.HRDept = tmpDeptList.Where(a => a.DeveCode.Trim().ToLower() == depthiscode[0].Trim().ToLower()).ElementAt(0);
            }
            else
            {
                emp.HRDept = NewHRDept.Where(a => a.DeveCode.Trim().ToLower() == depthiscode[0].Trim().ToLower()).ElementAt(0);
            }
            emp.IsEnabled = 1;
            emp.IsUse = true;
            emp.StandCode = entity.Account;//医生工号，HIS编码
            empflag = IDHREmployeeDao.Save(emp);
            if (IDHREmployeeDao.GetListCountByHQL(" StandCode='" + emp.StandCode + "' or DeveCode='" + emp.DeveCode + "' ") <= 0)
            {
                empflag = IDHREmployeeDao.Save(emp);
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工失败！人员的LIS编码" + emp.StandCode + "或HIS编码" + emp.DeveCode + "重复！");
                brdv.success = false;
                brdv.ErrorInfo = "注册员工失败！人员的LIS编码或HIS编码重复！";
                return brdv;
            }
            ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工结束！empflag:" + empflag);
            if (!empflag)
            {
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册员工失败！");
                brdv.success = false;
                brdv.ErrorInfo = "注册员工失败！";
                return brdv;
            }
            brdv.ResultDataValue = emp.Id.ToString();
            #endregion            

            #region 账号角色
            ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.账号角色开始！");
            ZhiFang.Entity.Common.BaseClassDicEntity roleValue = entity.Type == "医生" ? ZFSystemRole.智方_系统角色_医生.Value : ZFSystemRole.智方_系统角色_护士.Value;
            var rolelist = IDRBACRoleDao.GetListByHQL(" DeveCode='" + roleValue.Code + "' ");
            RBACEmpRoles emprole = new RBACEmpRoles();
            emprole.HREmployee = emp;
            emprole.IsUse = true;
            if (rolelist.Count > 0)
            {
                emprole.RBACRole = rolelist.ElementAt(0);
            }
            else
            {
                RBACRole role = new RBACRole();
                role.CName = roleValue.Name;
                role.Id = long.Parse(roleValue.Id);
                role.StandCode = roleValue.Code;
                role.DeveCode = roleValue.Code;
                role.DispOrder = int.Parse(roleValue.Id);
                role.IsUse = true;
                IDRBACRoleDao.Save(role);
                emprole.RBACRole = role;
            }
            roleflag = IDRBACEmpRolesDao.Save(emprole);
            ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.账号角色结束！roleflag:" + roleflag);
            if (!roleflag)
            {
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册账号角色失败！");
                brdv.success = false;
                brdv.ErrorInfo = "注册账号角色失败！";
                return brdv;
            }
            #endregion

            #region 危急值部门权限
            ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.危急值部门权限开始！");
            cvdeptlinkflag = true;
            foreach (var dept in tmpDeptList)
            {
                CVCriticalValueEmpIdDeptLink cvempdeptlink = new CVCriticalValueEmpIdDeptLink();
                cvempdeptlink.DeptID = dept.Id;
                cvempdeptlink.DeptName = dept.CName;
                cvempdeptlink.EmpID = emp.Id;
                cvempdeptlink.EmpName = emp.CName;
                cvempdeptlink.IsUse = true;
                cvempdeptlink.DataAddTime = DateTime.Now;
                if (!DBDao.Save(cvempdeptlink))
                {
                    cvdeptlinkflag = false;
                    break;
                }
            }

            foreach (var dept in NewHRDept)
            {
                CVCriticalValueEmpIdDeptLink cvempdeptlink = new CVCriticalValueEmpIdDeptLink();
                cvempdeptlink.DeptID = dept.Id;
                cvempdeptlink.DeptName = dept.CName;
                cvempdeptlink.EmpID = emp.Id;
                cvempdeptlink.EmpName = emp.CName;
                cvempdeptlink.IsUse = true;
                cvempdeptlink.DataAddTime = DateTime.Now;
                if (!DBDao.Save(cvempdeptlink))
                {
                    cvdeptlinkflag = false;
                    break;
                }
            }

            ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.危急值部门权限结束！cvdeptlinkflag:" + cvdeptlinkflag);
            if (!cvdeptlinkflag)
            {
                ZhiFang.Common.Log.Log.Debug("CV_SearchAndAddDoctorOrNurse.注册危急值部门权限失败！");
                brdv.success = false;
                brdv.ErrorInfo = "注册危急值部门权限失败！";
                return brdv;
            }
            #endregion

            
            return brdv;
        }

        public BaseResultDataValue AccountVerification(CV_AddDoctorOrNurseVO entity, RBACUser rbacuser)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string strPassWord = SecurityHelp.MD5Encrypt(entity.PWD, SecurityHelp.PWDMD5Key);
            ZhiFang.Common.Log.Log.Debug("AccountVerification.entity.PWD:" + entity.PWD);
            if (rbacuser.IsUse.HasValue && rbacuser.IsUse.Value)
            {
                ZhiFang.Common.Log.Log.Debug("AccountVerification.strPassWord:" + strPassWord + ";tempRBACUser[0].PWD:" + rbacuser.PWD);
                if (rbacuser.HREmployee.IsUse.HasValue && rbacuser.HREmployee.IsUse.Value && rbacuser.HREmployee.IsEnabled == 1)
                {
                    brdv.success = (rbacuser.Account == entity.Account) && (rbacuser.PWD == strPassWord) && (!rbacuser.AccLock);
                    return brdv;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("AccountVerification.员工账号被禁用或者逻辑删除！");
                    brdv.success = false;
                    brdv.ErrorInfo = "员工账号被禁用或者逻辑删除！";
                    return brdv;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("AccountVerification.员工帐号被逻辑删除！");
                brdv.success = false;
                brdv.ErrorInfo = "员工帐号被逻辑删除！";
                return brdv;
            }
        }
    }
}