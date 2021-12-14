using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.LIIP.RBACClone;

namespace ZhiFang.BLL.LIIP.RBACClone
{
    public class BBEmpClone : IBBEmpClone
    {
        public ZhiFang.IDAO.LIIP.IDEmpClone DoctorCloneDao { get; set; }
        public ZhiFang.IDAO.LIIP.IDEmpClone NPUserCloneDao { get; set; }
        public ZhiFang.IDAO.LIIP.IDEmpClone PUserCloneDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACRoleDao IDRBACRoleDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public ZhiFang.IDAO.LIIP.IDSLIIPSystemRBACCloneLog IDSLIIPSystemRBACCloneLog { get; set; }
        public ZhiFang.IDAO.RBAC.IDBSexDao IDBSexDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACUserDao IDRBACUserDao { get; set; }
        public BaseResultDataValue EmpClone(string DBType, string EmpId, string EmpName)
        {
            throw new NotImplementedException();
        }

        public BaseResultDataValue EmpClone_Doctor(string DBType,string EmpId,string EmpName, List<ZhiFang.Entity.RBAC.HREmployee> doctorentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var doctorlist_tmp = new List<ZhiFang.Entity.RBAC.HREmployee>();
            if (doctorentity == null || doctorentity.Count <= 0)
            {
                doctorlist_tmp = DoctorCloneDao.GetAllEmpList();
            }
            else
            {
                doctorlist_tmp = doctorentity;
            }
            if (doctorlist_tmp == null || doctorlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_Doctor.未能获取数据源中的医生列表。");
                brdv.ErrorInfo = "未能获取数据源中的医生列表。";
                brdv.success = false;
                return brdv;
            }
            var Emplist = IDHREmployeeDao.GetListByHQL(" 1=1 ");
            if (Emplist == null || Emplist.Count <= 0)
            {
                Emplist = new List<ZhiFang.Entity.RBAC.HREmployee>();
            }
            var sex = IDBSexDao.GetListByHQL(" Name='男' ");
            foreach (var doctor in doctorlist_tmp)
            {
                if (sex != null && sex.Count > 0)
                {
                    doctor.BSex = sex.ElementAt(0);
                }
                doctor.IsEnabled = 1;
                doctor.IsUse = true;
                if (Emplist.Count(a => a.StandCode == doctor.StandCode) <= 0)
                {
                    if (IDHREmployeeDao.Save(doctor))
                    {
                        count++;
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_Doctor.同步成功 ！医生名称：{doctor.CName},医生编码：{doctor.StandCode},医生简称：{doctor.SName}");
                        #region 角色
                        ZhiFang.Entity.Common.BaseClassDicEntity roleValue = ZFSystemRole.智方_系统角色_医生.Value;
                        var rolelist = IDRBACRoleDao.GetListByHQL(" DeveCode='" + roleValue.Code + "' ");
                        RBACEmpRoles emprole = new RBACEmpRoles();
                        emprole.HREmployee = doctor;
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
                        bool roleflag = IDRBACEmpRolesDao.Save(emprole);
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_Doctor.同步医生角色关系{(roleflag ? "成功" : "失败")}！医生名称：{doctor.CName},医生编码：{doctor.StandCode},医生简称：{doctor.SName}");
                        #endregion
                    }
                    else
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_Doctor.同步失败！医生名称：{doctor.CName},医生编码：{doctor.StandCode},医生简称：{doctor.SName}");

                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_Doctor.已存在！医生名称：{doctor.CName},医生编码：{doctor.StandCode},医生简称：{doctor.SName}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId,out long result)?result:0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(doctorlist_tmp),
                OperName = EmpName,
                DataName = "Doctor",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(doctorlist_tmp);
            return brdv;
        }

        public BaseResultDataValue EmpClone_PUser(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HREmployee> puserentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var puserlist_tmp = new List<ZhiFang.Entity.RBAC.HREmployee>();
            if (puserentity == null || puserentity.Count <= 0)
            {
                puserlist_tmp = PUserCloneDao.GetAllEmpList();
            }
            else {
                puserlist_tmp = puserentity;
            }
            if (puserlist_tmp == null || puserlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_PUser.未能获取数据源中的PUser列表。");
                brdv.ErrorInfo = "未能获取数据源中的PUser列表。";
                brdv.success = false;
                return brdv;
            }
            var Emplist = IDHREmployeeDao.GetListByHQL(" 1=1 ");
            if (Emplist == null || Emplist.Count <= 0)
            {
                Emplist = new List<ZhiFang.Entity.RBAC.HREmployee>();
            }
            var sex = IDBSexDao.GetListByHQL(" Name='男' ");
            foreach (var puser in puserlist_tmp)
            {
                if(puser.BSex!=null)
                    sex = IDBSexDao.GetListByHQL(" Id="+ puser.BSex.Id + " ");
                if (sex != null && sex.Count > 0)
                {
                    puser.BSex = sex.ElementAt(0);
                }
                puser.Birthday = puser.Birthday.Value;
                puser.IsEnabled = 1;
                puser.IsUse = true;
                if (Emplist.Count(a => a.StandCode == puser.StandCode) <= 0||puser.StandCode==null ||puser.StandCode.Trim()=="")
                {
                    if (IDHREmployeeDao.Save(puser))
                    {
                        count++;
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_PUser.同步成功 ！PUser名称：{puser.CName},PUse编码：{puser.StandCode},PUse简称：{puser.SName}");

                        #region 角色
                        ZhiFang.Entity.Common.BaseClassDicEntity roleValue = ZFSystemRole.智方_系统角色_检验技师.Value;
                        if (puser.Comment != null && puser.Comment.Trim() != "")
                        {
                            switch (puser.Comment.Trim())
                            {
                                case "医生": roleValue = ZFSystemRole.智方_系统角色_医生.Value; break;
                                case "护士": roleValue = ZFSystemRole.智方_系统角色_护士.Value; break;
                                default: roleValue = ZFSystemRole.智方_系统角色_检验技师.Value; break;
                            }
                        }                        
                        var rolelist = IDRBACRoleDao.GetListByHQL(" DeveCode='" + roleValue.Code + "' ");
                        RBACEmpRoles emprole = new RBACEmpRoles();
                        emprole.HREmployee = puser;
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
                        bool roleflag = IDRBACEmpRolesDao.Save(emprole);
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_PUser.同步PUser角色关系{(roleflag ? "成功" : "失败")}！PUser名称：{puser.CName},PUser编码：{puser.StandCode},PUser简称：{puser.SName}");
                        #endregion

                        //部门暂时不同步 因为变动较多所以很多检验之星都没有维护deptNo都是空或者0；

                    }
                    else
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_PUser.同步失败！PUser名称：{puser.CName},PUser编码：{puser.StandCode},PUser简称：{puser.SName}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_PUser.已存在！PUser名称：{puser.CName},PUser编码：{puser.StandCode},PUser简称：{puser.SName}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(puserlist_tmp),
                OperName = EmpName,
                DataName = "Puser",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(puserlist_tmp);
            return brdv;
        }

        public BaseResultDataValue EmpClone_NPUser(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HREmployee> npuserentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var npuserlist_tmp = new List<ZhiFang.Entity.RBAC.HREmployee>();
            if (npuserentity == null || npuserentity.Count <= 0)
            {
                npuserlist_tmp = NPUserCloneDao.GetAllEmpList();
            }
            else {
                npuserlist_tmp = npuserentity;
            }
            if (npuserlist_tmp == null || npuserlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_NPUser.未能获取数据源中的NPUser列表。");
                brdv.ErrorInfo = "未能获取数据源中的PUse列表。";
                brdv.success = false;
                return brdv;
            }
            var Emplist = IDHREmployeeDao.GetListByHQL(" 1=1 ");
            if (Emplist == null || Emplist.Count <= 0)
            {
                Emplist = new List<ZhiFang.Entity.RBAC.HREmployee>();
            }
            var sex = IDBSexDao.GetListByHQL(" Name='男' ");
            foreach (var npuser in npuserlist_tmp)
            {
                if (npuser.BSex != null)
                    sex = IDBSexDao.GetListByHQL(" Id=" + npuser.BSex.Id + " ");
                if (sex != null && sex.Count > 0)
                {
                    npuser.BSex = sex.ElementAt(0);
                }

                npuser.IsEnabled = 1;
                npuser.IsUse = true;
                if (Emplist.Count(a => a.StandCode == npuser.StandCode) <= 0 || npuser.StandCode == null || npuser.StandCode.Trim() == "")
                {
                    if (IDHREmployeeDao.Save(npuser))
                    {
                        count++;
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_NPUser.同步成功 ！NPUser名称：{npuser.CName},NPUser编码：{npuser.StandCode},NPUser简称：{npuser.SName}");

                        #region 角色
                        ZhiFang.Entity.Common.BaseClassDicEntity roleValue = ZFSystemRole.智方_系统角色_物流护工.Value;
                        if (npuser.Comment != null && npuser.Comment.Trim() != "")
                        {
                            switch (npuser.Comment.Trim())
                            {
                                case "医生": roleValue = ZFSystemRole.智方_系统角色_医生.Value; break;
                                case "护士": roleValue = ZFSystemRole.智方_系统角色_护士.Value; break;
                                default: roleValue = ZFSystemRole.智方_系统角色_物流护工.Value; break;
                            }
                        }
                        var rolelist = IDRBACRoleDao.GetListByHQL(" DeveCode='" + roleValue.Code + "' ");
                        RBACEmpRoles emprole = new RBACEmpRoles();
                        emprole.HREmployee = npuser;
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
                        bool roleflag = IDRBACEmpRolesDao.Save(emprole);
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_NPUser.同步NPUser角色关系{(roleflag ? "成功" : "失败")}！NPUser名称：{npuser.CName},NPUser编码：{npuser.StandCode},NPUser简称：{npuser.SName}");
                        #endregion

                        //部门暂时不同步 因为变动较多所以很多检验之星都没有维护deptNo都是空或者0；

                    }
                    else
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_NPUser.同步失败！NPUser名称：{npuser.CName},NPUser编码：{npuser.StandCode},NPUser简称：{npuser.SName}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_NPUser.已存在！NPUser名称：{npuser.CName},NPUser编码：{npuser.StandCode},NPUser简称：{npuser.SName}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(npuserlist_tmp),
                OperName = EmpName,
                DataName = "Puser",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(npuserlist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchDoctorDataList()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var doctorlist_tmp = DoctorCloneDao.GetAllEmpList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(doctorlist_tmp);
            return brdv;
        }
        public BaseResultDataValue CatchPuserDataList()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var puserlist_tmp = PUserCloneDao.GetAllEmpList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(puserlist_tmp);
            return brdv;
        }
        public BaseResultDataValue CatchNPuserDataList()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var npuserlist_tmp = NPUserCloneDao.GetAllEmpList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(npuserlist_tmp);
            return brdv;
        }

        public BaseResultDataValue EmpClone_HREmployeeGoToLabStar6Table(string DBType, List<string> TableTypeList, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HREmployee> hremployeeentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var  hremployeelist_tmp = new List<ZhiFang.Entity.RBAC.HREmployee>();
            if (hremployeeentity == null || hremployeeentity.Count <= 0)
            {
                hremployeelist_tmp = IDHREmployeeDao.GetListByHQL(" 1=1 ").ToList();
            }
            else
            {
                hremployeelist_tmp = hremployeeentity;
            }

            if (hremployeelist_tmp == null || hremployeelist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table.未能获取数据源中的HREmployee列表。");
                brdv.ErrorInfo = "未能获取数据源中的HREmployee列表。";
                brdv.success = false;
                return brdv;
            }
            foreach (string ttype in TableTypeList) {
                if (ttype.Equals("DOCTOR"))
                {
                    var doctorlist = DoctorCloneDao.GetAllEmpList();
                    if (doctorlist == null || doctorlist.Count <= 0)
                    {
                        doctorlist = new List<ZhiFang.Entity.RBAC.HREmployee>();
                    }
                    
                    foreach (var doctor in hremployeelist_tmp)
                    {
                        var  rBACEmpRoles =  IDRBACEmpRolesDao.GetListByHQL(" HREmployee.Id = " + doctor.Id);
                        if (rBACEmpRoles != null && rBACEmpRoles.Count > 0)
                        {
                            var rBACRoleslist = IDRBACRoleDao.GetListByHQL(" Id=" + rBACEmpRoles[0].RBACRole.Id);
                            ZhiFang.Entity.Common.BaseClassDicEntity roleValue = ZFSystemRole.智方_系统角色_医生.Value;
                            if (rBACRoleslist != null && rBACRoleslist.Count > 0 && rBACRoleslist[0].CName == roleValue.Name)
                            {
                                if (doctorlist.Count(a => a.Shortcode == doctor.Shortcode) <= 0 || doctor.Shortcode == null || doctor.Shortcode.Trim() == "")
                                {
                                    if (DoctorCloneDao.Add(doctor))
                                    {
                                        count++;
                                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Doctor.同步成功 ！HREmployee名称：{doctor.CName},HREmployee编码：{doctor.StandCode},HREmployee简称：{doctor.SName}");
                                    }
                                    else
                                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Doctor.同步失败！HREmployee名称：{doctor.CName},HREmployee编码：{doctor.StandCode},HREmployee简称：{doctor.SName}");
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Doctor.已存在！HREmployee名称：{doctor.CName},HREmployee编码：{doctor.StandCode},HREmployee简称：{doctor.SName}");
                                }

                            }
                            else 
                            {
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Doctor.非医生人员不进行同步！HREmployee名称：{doctor.CName},HREmployee编码：{doctor.StandCode},HREmployee简称：{doctor.SName}");
                            }

                        }
                        else 
                        {
                            ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Doctor.非医生人员不进行同步！HREmployee名称：{doctor.CName},HREmployee编码：{doctor.StandCode},HREmployee简称：{doctor.SName}");
                        }
                        
                        doctor.HRDept = null;
                        doctor.HRPosition = null;
                        doctor.BCity = null;
                        doctor.BCountry = null;
                        doctor.BProvince = null;
                        doctor.HRDeptEmpList = null;
                        doctor.HREmpIdentityList = null;
                        doctor.RBACEmpOptionsList = null;
                        doctor.RBACEmpRoleList = null;
                        doctor.RBACUserList = null;
                    }

                    IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
                    {
                        OperId = long.TryParse(EmpId, out long result) ? result : 0,
                        DataCount = count,
                        DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp),
                        OperName = EmpName,
                        DataName = "Doctor",
                        ForwardFlag = true,
                        SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Key),
                        SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Name,
                        SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Code
                    });
                    count = 0;
                }
                else if(ttype.Equals("PUSER")) {
                    var puserlist = PUserCloneDao.GetAllEmpList();
                    if (puserlist == null || puserlist.Count <= 0)
                    {
                        puserlist = new List<ZhiFang.Entity.RBAC.HREmployee>();
                    }

                    foreach (var puser in hremployeelist_tmp)
                    {
                        puser.RBACUserList = IDRBACUserDao.GetListByHQL(" HREmployee.Id = " + puser.Id);
                        if (puser.BSex == null) {
                            var  bsex = IDHREmployeeDao.GetListByHQL(" Id=" + puser.Id);
                            if (bsex.Count > 0)
                            {
                                puser.BSex = bsex[0].BSex;
                            }
                            else {
                                puser.BSex = new BSex() { Id=0 };
                            }
                        }                        
                        if (puserlist.Count(a => a.StandCode == puser.StandCode && a.Shortcode == puser.Shortcode) <= 0 && puser.RBACUserList != null && puser.RBACUserList.Count > 0 && puser.RBACUserList[0].Shortcode !="" && puser.RBACUserList[0].Shortcode != null)
                        {
                            if (PUserCloneDao.Add(puser))
                            {
                                count++;
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Puser.同步成功 ！HREmployee名称：{puser.CName},HREmployee编码：{puser.StandCode},HREmployee简称：{puser.SName}");
                            }
                            else
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Puser.同步失败！HREmployee名称：{puser.CName},HREmployee编码：{puser.StandCode},HREmployee简称：{puser.SName}");
                        }
                        else 
                        { 
                            ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Puser.已存在！HREmployee名称：{puser.CName},HREmployee编码：{puser.StandCode},HREmployee简称：{puser.SName}");
                        }
                        puser.HRDept = null;
                        puser.HRPosition = null;
                        puser.BCity = null;
                        puser.BCountry = null;
                        puser.BProvince = null;
                        puser.HRDeptEmpList = null;
                        puser.HREmpIdentityList = null;
                        puser.RBACEmpOptionsList = null;
                        puser.RBACEmpRoleList = null;
                        puser.RBACUserList = null;
                    }

                    IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
                    {
                        OperId = long.TryParse(EmpId, out long result) ? result : 0,
                        DataCount = count,
                        DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp),
                        OperName = EmpName,
                        DataName = "Puser",
                        ForwardFlag = true,
                        SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Key),
                        SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Name,
                        SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Code
                    });
                    count = 0;
                }
                else if (ttype.Equals("NPUSER"))
                {
                    var npuserlist = NPUserCloneDao.GetAllEmpList();
                    if (npuserlist == null || npuserlist.Count <= 0)
                    {
                        npuserlist = new List<ZhiFang.Entity.RBAC.HREmployee>();
                    }

                    foreach (var npuser in hremployeelist_tmp)
                    {
                        var rBACEmpRoles = IDRBACEmpRolesDao.GetListByHQL(" HREmployee.Id = " + npuser.Id);
                        if (rBACEmpRoles != null && rBACEmpRoles.Count > 0)
                        {
                            var rBACRoleslist = IDRBACRoleDao.GetListByHQL(" Id=" + rBACEmpRoles[0].RBACRole.Id);
                            ZhiFang.Entity.Common.BaseClassDicEntity roleValue1 = ZFSystemRole.智方_系统角色_护士.Value;
                            ZhiFang.Entity.Common.BaseClassDicEntity roleValue2 = ZFSystemRole.智方_系统角色_物流护工.Value;
                            if (rBACRoleslist != null && rBACRoleslist.Count > 0 && (rBACRoleslist[0].CName == roleValue1.Name || rBACRoleslist[0].CName == roleValue2.Name))
                            {
                                npuser.RBACUserList = IDRBACUserDao.GetListByHQL(" EmpID = " + npuser.Id);
                                if (npuserlist.Count(a => a.StandCode == npuser.StandCode && a.Shortcode == npuser.Shortcode) <= 0 && npuser.RBACUserList != null && npuser.RBACUserList.Count > 0 && npuser.RBACUserList[0].Shortcode != "" && npuser.RBACUserList[0].Shortcode != null)
                                {
                                    if (NPUserCloneDao.Add(npuser))
                                    {
                                        count++;
                                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_NPuser.同步成功 ！HREmployee名称：{npuser.CName},HREmployee编码：{npuser.StandCode},HREmployee简称：{npuser.SName}");
                                    }
                                    else
                                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_NPuser.同步失败！HREmployee名称：{npuser.CName},HREmployee编码：{npuser.StandCode},HREmployee简称：{npuser.SName}");
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_NPuser.已存在！HREmployee名称：{npuser.CName},HREmployee编码：{npuser.StandCode},HREmployee简称：{npuser.SName}");
                                }
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_NPuser.非护士人员不进行同步！HREmployee名称：{npuser.CName},HREmployee编码：{npuser.StandCode},HREmployee简称：{npuser.SName}");
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_NPuser.非护士人员不进行同步！HREmployee名称：{npuser.CName},HREmployee编码：{npuser.StandCode},HREmployee简称：{npuser.SName}");
                        }
                        npuser.HRDept = null;
                        npuser.HRPosition = null;
                        npuser.BCity = null;
                        npuser.BCountry = null;
                        npuser.BProvince = null;
                        npuser.HRDeptEmpList = null;
                        npuser.HREmpIdentityList = null;
                        npuser.RBACEmpOptionsList = null;
                        npuser.RBACEmpRoleList = null;
                        npuser.RBACUserList = null;
                    }

                    IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
                    {
                        OperId = long.TryParse(EmpId, out long result) ? result : 0,
                        DataCount = count,
                        DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp),
                        OperName = EmpName,
                        DataName = "Puser",
                        ForwardFlag = true,
                        SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Key),
                        SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Name,
                        SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Code
                    });
                    count = 0;
                }
            }            
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp);
            return brdv;
        }

        public BaseResultDataValue EmpClone_HREmployeeGoToLabStar6TableByEntity(string DBType, List<string> TableTypeList, string EmpId, string EmpName, List<HREmployee> hremployeeentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var hremployeelist_tmp = new List<ZhiFang.Entity.RBAC.HREmployee>();
            hremployeelist_tmp = hremployeeentity;

            if (hremployeelist_tmp == null || hremployeelist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table.未能获取数据源中的HREmployee列表。");
                brdv.ErrorInfo = "未能获取数据源中的HREmployee列表。";
                brdv.success = false;
                return brdv;
            }
            foreach (string ttype in TableTypeList)
            {
                if (ttype.Equals("DOCTOR"))
                {
                    var doctorlist = DoctorCloneDao.GetAllEmpList();
                    if (doctorlist == null || doctorlist.Count <= 0)
                    {
                        doctorlist = new List<ZhiFang.Entity.RBAC.HREmployee>();
                    }

                    foreach (var doctor in hremployeelist_tmp)
                    {
                        if (doctorlist.Count(a => a.Shortcode == doctor.Shortcode) <= 0 || doctor.Shortcode == null || doctor.Shortcode.Trim() == "")
                        {
                            if (DoctorCloneDao.Add(doctor))
                            {
                                count++;
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Doctor.同步成功 ！HREmployee名称：{doctor.CName},HREmployee编码：{doctor.StandCode},HREmployee简称：{doctor.SName}");
                            }
                            else
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Doctor.同步失败！HREmployee名称：{doctor.CName},HREmployee编码：{doctor.StandCode},HREmployee简称：{doctor.SName}");
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Doctor.已存在！HREmployee名称：{doctor.CName},HREmployee编码：{doctor.StandCode},HREmployee简称：{doctor.SName}");
                        }

                        doctor.HRDept = null;
                        doctor.HRPosition = null;
                        doctor.BCity = null;
                        doctor.BCountry = null;
                        doctor.BProvince = null;
                        doctor.HRDeptEmpList = null;
                        doctor.HREmpIdentityList = null;
                        doctor.RBACEmpOptionsList = null;
                        doctor.RBACEmpRoleList = null;
                        doctor.RBACUserList = null;
                    }

                    IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
                    {
                        OperId = long.TryParse(EmpId, out long result) ? result : 0,
                        DataCount = count,
                        DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp),
                        OperName = EmpName,
                        DataName = "Doctor",
                        ForwardFlag = true,
                        SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Key),
                        SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Name,
                        SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Code
                    });
                    count = 0;
                }
                else if (ttype.Equals("PUSER"))
                {
                    var puserlist = PUserCloneDao.GetAllEmpList();
                    if (puserlist == null || puserlist.Count <= 0)
                    {
                        puserlist = new List<ZhiFang.Entity.RBAC.HREmployee>();
                    }

                    foreach (var puser in hremployeelist_tmp)
                    {
                        puser.RBACUserList = IDRBACUserDao.GetListByHQL(" HREmployee.Id = " + puser.Id);
                        if (puser.BSex == null)
                        {
                            var bsex = IDHREmployeeDao.GetListByHQL(" Id=" + puser.Id);
                            if (bsex.Count > 0)
                            {
                                puser.BSex = bsex[0].BSex;
                            }
                            else
                            {
                                puser.BSex = new BSex() { Id = 0 };
                            }
                        }
                        if (puserlist.Count(a => a.StandCode == puser.StandCode && a.Shortcode == puser.Shortcode) <= 0 && puser.RBACUserList != null && puser.RBACUserList.Count > 0 && puser.RBACUserList[0].Shortcode != "" && puser.RBACUserList[0].Shortcode != null)
                        {
                            if (PUserCloneDao.Add(puser))
                            {
                                count++;
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Puser.同步成功 ！HREmployee名称：{puser.CName},HREmployee编码：{puser.StandCode},HREmployee简称：{puser.SName}");
                            }
                            else
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Puser.同步失败！HREmployee名称：{puser.CName},HREmployee编码：{puser.StandCode},HREmployee简称：{puser.SName}");
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_Puser.已存在！HREmployee名称：{puser.CName},HREmployee编码：{puser.StandCode},HREmployee简称：{puser.SName}");
                        }
                        puser.HRDept = null;
                        puser.HRPosition = null;
                        puser.BCity = null;
                        puser.BCountry = null;
                        puser.BProvince = null;
                        puser.HRDeptEmpList = null;
                        puser.HREmpIdentityList = null;
                        puser.RBACEmpOptionsList = null;
                        puser.RBACEmpRoleList = null;
                        puser.RBACUserList = null;
                    }

                    IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
                    {
                        OperId = long.TryParse(EmpId, out long result) ? result : 0,
                        DataCount = count,
                        DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp),
                        OperName = EmpName,
                        DataName = "Puser",
                        ForwardFlag = true,
                        SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Key),
                        SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Name,
                        SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Code
                    });
                    count = 0;
                }
                else if (ttype.Equals("NPUSER"))
                {
                    var npuserlist = NPUserCloneDao.GetAllEmpList();
                    if (npuserlist == null || npuserlist.Count <= 0)
                    {
                        npuserlist = new List<ZhiFang.Entity.RBAC.HREmployee>();
                    }

                    foreach (var npuser in hremployeelist_tmp)
                    {
                        
                        npuser.RBACUserList = IDRBACUserDao.GetListByHQL(" EmpID = " + npuser.Id);
                        if (npuserlist.Count(a => a.StandCode == npuser.StandCode && a.Shortcode == npuser.Shortcode) <= 0 && npuser.RBACUserList != null && npuser.RBACUserList.Count > 0 && npuser.RBACUserList[0].Shortcode != "" && npuser.RBACUserList[0].Shortcode != null)
                        {
                            if (NPUserCloneDao.Add(npuser))
                            {
                                count++;
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_NPuser.同步成功 ！HREmployee名称：{npuser.CName},HREmployee编码：{npuser.StandCode},HREmployee简称：{npuser.SName}");
                            }
                            else
                                ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_NPuser.同步失败！HREmployee名称：{npuser.CName},HREmployee编码：{npuser.StandCode},HREmployee简称：{npuser.SName}");
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBEmp.EmpClone_HREmployeeGoToLabStar6Table_NPuser.已存在！HREmployee名称：{npuser.CName},HREmployee编码：{npuser.StandCode},HREmployee简称：{npuser.SName}");
                        }
                        npuser.HRDept = null;
                        npuser.HRPosition = null;
                        npuser.BCity = null;
                        npuser.BCountry = null;
                        npuser.BProvince = null;
                        npuser.HRDeptEmpList = null;
                        npuser.HREmpIdentityList = null;
                        npuser.RBACEmpOptionsList = null;
                        npuser.RBACEmpRoleList = null;
                        npuser.RBACUserList = null;
                    }

                    IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
                    {
                        OperId = long.TryParse(EmpId, out long result) ? result : 0,
                        DataCount = count,
                        DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp),
                        OperName = EmpName,
                        DataName = "Puser",
                        ForwardFlag = true,
                        SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Key),
                        SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Name,
                        SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Code
                    });
                    count = 0;
                }
            }
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp);
            return brdv;
        }
    }
}
