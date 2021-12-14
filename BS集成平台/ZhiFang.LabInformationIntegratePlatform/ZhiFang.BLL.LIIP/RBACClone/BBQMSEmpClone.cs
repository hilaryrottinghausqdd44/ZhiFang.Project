using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.LIIP.RBACClone;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.BLL.LIIP.RBACClone
{
    public class BBQMSEmpClone : IBBQMSEmpClone
    {
        public ZhiFang.IDAO.LIIP.QMS.IDHRDept IDQMSHRDeptDao { get;set;}
        public ZhiFang.IDAO.LIIP.QMS.IDHRDeptEmp IDQMSHRDeptEmpDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDHRDeptIdentity IDQMSHRDeptIdentityDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDHREmpIdentity IDQMSHREmpIdentityDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDHREmployee IDQMSHREmployeeDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDHRPosition IDQMSHRPositionDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDRBACEmpOptions IDQMSRBACEmpOptionsDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDRBACEmpRoles IDQMSRBACEmpRolesDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDRBACModule IDQMSRBACModuleDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDRBACModuleOper IDQMSRBACModuleOperDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDRBACRole IDQMSRBACRoleDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDRBACRoleModule IDQMSRBACRoleModuleDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDRBACRoleRight IDQMSRBACRoleRightDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDRBACRowFilter IDQMSRBACRowFilterDao { get; set; }
        public ZhiFang.IDAO.LIIP.QMS.IDRBACUser IDQMSRBACUserDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDHRDeptEmpDao IDHRDeptEmpDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDHRDeptIdentityDao IDHRDeptIdentityDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDHREmpIdentityDao IDHREmpIdentityDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDHRPositionDao IDHRPositionDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACEmpOptionsDao IDRBACEmpOptionsDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACModuleDao IDRBACModuleDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACModuleOperDao IDRBACModuleOperDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACRoleDao IDRBACRoleDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACRoleModuleDao IDRBACRoleModuleDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACRoleRightDao IDRBACRoleRightDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACRowFilterDao IDRBACRowFilterDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDRBACUserDao IDRBACUserDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDBSexDao IDBSexDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDBTDAppComponentsDao IDBTDAppComponentsDao { get; set; }
        public ZhiFang.IDAO.RBAC.IDBTDAppComponentsOperateDao IDBTDAppComponentsOperateDao { get; set; }
        public ZhiFang.IDAO.LIIP.IDSLIIPSystemRBACCloneLog IDSLIIPSystemRBACCloneLog { get; set; }       
        
        public BaseResultDataValue HRDeptClone(string DBType, string EmpId, string EmpName, List<HRDept> deptlistentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.HRDept>();
            if (deptlistentity == null || deptlistentity.Count <= 0)
            {
                deptlist_tmp = IDQMSHRDeptDao.GetAllObjectList();
            }
            else
            {
                deptlist_tmp = deptlistentity;
            }
            if (deptlist_tmp == null || deptlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptClone.未能获取数据源中的部门列表。");
                brdv.ErrorInfo = "未能获取数据源中的部门列表。";
                brdv.success = false;
                return brdv;
            }
            //List<string> deptcodelist = new List<string>();
            //deptlist_tmp.ForEach(a => deptcodelist.Add(a.StandCode));
            var deptlist = IDHRDeptDao.GetListByHQL(" 1=1 ");
            if (deptlist == null || deptlist.Count <= 0)
            {
                deptlist = new List<ZhiFang.Entity.RBAC.HRDept>();
            }
            foreach (var dept in deptlist_tmp)
            {
                dept.DataAddTime = dept.DataAddTime.Value;
                if (deptlist.Count(a => a.Id == dept.Id) <= 0 && deptlist.Count(a => a.StandCode == dept.StandCode) <= 0)
                {
                    bool flag = false;
                    if (IDHRDeptDao.Save(dept))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptClone.同步{(flag ? "成功" : "失败")}！部门名称：{dept.CName},部门编码：{dept.StandCode},部门简称：{dept.SName}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptClone.已存在！部门名称：{dept.CName},部门编码：{dept.StandCode},部门简称：{dept.SName}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp),
                OperName = EmpName,
                DataName = "HRDept",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchHRDeptDataList(string DBType) 
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.HRDept>();
            deptlist_tmp = IDQMSHRDeptDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue HRDeptEmpClone(string DBType, string EmpId, string EmpName, List<HRDeptEmp> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var deptemplist_tmp = new List<ZhiFang.Entity.RBAC.HRDeptEmp>();
            if (entity == null || entity.Count <= 0)
            {
                deptemplist_tmp = IDQMSHRDeptEmpDao.GetAllObjectList();
            }
            else
            {
                deptemplist_tmp = entity;
            }
            if (deptemplist_tmp == null || deptemplist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptEmpClone.未能获取数据源中的部门员工关系列表。");
                brdv.ErrorInfo = "未能获取数据源中的部门员工关系列表。";
                brdv.success = false;
                return brdv;
            }
            var hrdeptemplist = IDHRDeptEmpDao.GetListByHQL(" 1=1 ");
            if (hrdeptemplist == null || hrdeptemplist.Count <= 0)
            {
                hrdeptemplist = new List<ZhiFang.Entity.RBAC.HRDeptEmp>();
            }
            foreach (var deptemp in deptemplist_tmp)
            {
                if (deptemp.HRDept != null)
                {
                    var entitylist = IDHRDeptDao.GetListByHQL(" Id=" + deptemp.HRDept.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        deptemp.HRDept = IDHRDeptDao.GetListByHQL(" Id=" + deptemp.HRDept.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        deptemp.HRDept = null;
                    }
                }
                if (deptemp.HREmployee != null)
                {
                    var entitylist = IDHREmployeeDao.GetListByHQL(" Id=" + deptemp.HREmployee.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        deptemp.HREmployee = IDHREmployeeDao.GetListByHQL(" Id=" + deptemp.HREmployee.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        
                        deptemp.HREmployee = null;
                    }
                }
                deptemp.DataAddTime = deptemp.DataAddTime.Value;
                if (null != deptemp.HREmployee && null != deptemp.HRDept)
                {
                    if (hrdeptemplist.Count(a => a.HRDept.Id == deptemp.HRDept.Id && a.HREmployee.Id == deptemp.HREmployee.Id) <= 0)
                    {
                        bool flag = false;
                        if (IDHRDeptEmpDao.Save(deptemp))
                        {
                            count++;
                            flag = true;
                        }
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptEmpClone.同步{(flag ? "成功" : "失败")}！Id：{deptemp.Id}");
                    }
                    else
                        ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptEmpClone.已存在！Id：{deptemp.Id}");
                }
                else {
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptEmpClone.数据错误不于同步！Id：{deptemp.Id}");
                }
                

            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptemplist_tmp),
                OperName = EmpName,
                DataName = "HRDeptEmp",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptemplist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchHRDeptEmpDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.HRDeptEmp>();
            deptlist_tmp = IDQMSHRDeptEmpDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue HRDeptIdentityClone(string DBType, string EmpId, string EmpName, List<HRDeptIdentity> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var hrdeptIdentitylist_tmp = new List<ZhiFang.Entity.RBAC.HRDeptIdentity>();
            if (entity == null || entity.Count <= 0)
            {
                hrdeptIdentitylist_tmp = IDQMSHRDeptIdentityDao.GetAllObjectList();
            }
            else
            {
                hrdeptIdentitylist_tmp = entity;
            }
            if (hrdeptIdentitylist_tmp == null || hrdeptIdentitylist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptIdentityClone.未能获取数据源中的部门身份列表。");
                brdv.ErrorInfo = "未能获取数据源中的部门身份列表。";
                brdv.success = false;
                return brdv;
            }
            var hrdeptIdentitylist = IDHRDeptIdentityDao.GetListByHQL(" 1=1 ");
            if (hrdeptIdentitylist == null || hrdeptIdentitylist.Count <= 0)
            {
                hrdeptIdentitylist = new List<ZhiFang.Entity.RBAC.HRDeptIdentity>();
            }
            foreach (var hrdeptidentity in hrdeptIdentitylist_tmp)
            {
                if (hrdeptidentity.HRDept != null)
                {
                    var entitylist = IDHRDeptDao.GetListByHQL(" Id=" + hrdeptidentity.HRDept.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        hrdeptidentity.HRDept = IDHRDeptDao.GetListByHQL(" Id=" + hrdeptidentity.HRDept.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        hrdeptidentity.HRDept = null;
                    }
                }
                hrdeptidentity.DataAddTime = hrdeptidentity.DataAddTime.Value;
                if (hrdeptIdentitylist.Count(a => a.HRDept.Id == hrdeptidentity.HRDept.Id && a.IdenTypeID == hrdeptidentity.IdenTypeID) <= 0 )
                {
                    bool flag = false;
                    if (IDHRDeptIdentityDao.Save(hrdeptidentity))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptIdentityClone.同步{(flag ? "成功" : "失败")}！Id：{hrdeptidentity.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRDeptIdentityClone.已存在！Id：{hrdeptidentity.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hrdeptIdentitylist_tmp),
                OperName = EmpName,
                DataName = "HRDeptIdentity",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hrdeptIdentitylist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchHRDeptIdentityDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.HRDeptIdentity>();
            deptlist_tmp = IDQMSHRDeptIdentityDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue HREmpIdentityClone(string DBType, string EmpId, string EmpName, List<HREmpIdentity> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var hrempIdentitylist_tmp = new List<ZhiFang.Entity.RBAC.HREmpIdentity>();
            if (entity == null || entity.Count <= 0)
            {
                hrempIdentitylist_tmp = IDQMSHREmpIdentityDao.GetAllObjectList();
            }
            else
            {
                hrempIdentitylist_tmp = entity;
            }
            if (hrempIdentitylist_tmp == null || hrempIdentitylist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HREmpIdentityClone.未能获取数据源中的员工身份列表。");
                brdv.ErrorInfo = "未能获取数据源中的员工身份列表。";
                brdv.success = false;
                return brdv;
            }
            var hrempIdentitylist = IDHREmpIdentityDao.GetListByHQL(" 1=1 ");
            if (hrempIdentitylist == null || hrempIdentitylist.Count <= 0)
            {
                hrempIdentitylist = new List<ZhiFang.Entity.RBAC.HREmpIdentity>();
            }
            foreach (var hrempIdentity in hrempIdentitylist_tmp)
            {
                if (hrempIdentity.HREmployee != null)
                {
                    var entitylist = IDHREmployeeDao.GetListByHQL(" Id=" + hrempIdentity.HREmployee.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        hrempIdentity.HREmployee = IDHREmployeeDao.GetListByHQL(" Id=" + hrempIdentity.HREmployee.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        hrempIdentity.HREmployee = null;
                    }
                }
                hrempIdentity.DataAddTime = hrempIdentity.DataAddTime.Value;
                if (hrempIdentitylist.Count(a => a.HREmployee.Id == hrempIdentity.HREmployee.Id && a.TSysID == hrempIdentity.TSysID) <= 0)
                {
                    bool flag = false;
                    if (IDHREmpIdentityDao.Save(hrempIdentity))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.HREmpIdentityClone.同步{(flag ? "成功" : "失败")}！Id：{hrempIdentity.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HREmpIdentityClone.已存在！Id：{hrempIdentity.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hrempIdentitylist_tmp),
                OperName = EmpName,
                DataName = "HREmpIdentity",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hrempIdentitylist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchHREmpIdentityDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.HREmpIdentity>();
            deptlist_tmp = IDQMSHREmpIdentityDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue HREmployeeClone(string DBType, string EmpId, string EmpName, List<HREmployee> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var hremployeelist_tmp = new List<ZhiFang.Entity.RBAC.HREmployee>();
            if (entity == null || entity.Count <= 0)
            {
                hremployeelist_tmp = IDQMSHREmployeeDao.GetAllObjectList();
            }
            else
            {
                hremployeelist_tmp = entity;
            }
            if (hremployeelist_tmp == null || hremployeelist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HREmployeeClone.未能获取数据源中的员工列表。");
                brdv.ErrorInfo = "未能获取数据源中的员工列表。";
                brdv.success = false;
                return brdv;
            }
            var hremployeelist = IDHREmployeeDao.GetListByHQL(" 1=1 ");
            if (hremployeelist == null || hremployeelist.Count <= 0)
            {
                hremployeelist = new List<ZhiFang.Entity.RBAC.HREmployee>();
            }
            var sex = IDBSexDao.GetListByHQL(" Name='男' ");
            foreach (var hremployee in hremployeelist_tmp)
            {
                if (hremployee.BSex != null)
                    sex = IDBSexDao.GetListByHQL(" Id=" + hremployee.BSex.Id + " ");
                if (sex != null && sex.Count > 0)
                {
                    hremployee.BSex = sex.ElementAt(0);
                }
                if (hremployee.HRDept != null) {
                    var hrdeptlist = IDHRDeptDao.GetListByHQL(" Id=" + hremployee.HRDept.Id + " ");
                    if (hrdeptlist != null && hrdeptlist.Count > 0)
                    {
                        hremployee.HRDept = IDHRDeptDao.GetListByHQL(" Id=" + hremployee.HRDept.Id + " ").ElementAt(0);
                    }
                    else {
                        hremployee.HRDept = null;
                    }                    
                }
                if (hremployee.HRPosition != null)
                {
                    var entitylist = IDHRPositionDao.GetListByHQL(" Id=" + hremployee.HRPosition.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        hremployee.HRPosition = IDHRPositionDao.GetListByHQL(" Id=" + hremployee.HRPosition.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        hremployee.HRPosition = null;
                    }
                }
                hremployee.DataAddTime = hremployee.DataAddTime.Value;
                if (hremployeelist.Count(a => a.Id == hremployee.Id) <= 0)
                {
                    bool flag = false;
                    if (IDHREmployeeDao.Save(hremployee))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.HREmployeeClone.同步{(flag ? "成功" : "失败")}！Id：{hremployee.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HREmployeeClone.已存在！Id：{hremployee.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp),
                OperName = EmpName,
                DataName = "HREmployee",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hremployeelist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchHREmployeeDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.HREmployee>();
            deptlist_tmp = IDQMSHREmployeeDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue HRPositionClone(string DBType, string EmpId, string EmpName, List<HRPosition> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var hrpositionlist_tmp = new List<ZhiFang.Entity.RBAC.HRPosition>();
            if (entity == null || entity.Count <= 0)
            {
                hrpositionlist_tmp = IDQMSHRPositionDao.GetAllObjectList();
            }
            else
            {
                hrpositionlist_tmp = entity;
            }
            if (hrpositionlist_tmp == null || hrpositionlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRPositionClone.未能获取数据源中的职位列表。");
                brdv.ErrorInfo = "未能获取数据源中的职位列表。";
                brdv.success = false;
                return brdv;
            }
            var hrpositionlist = IDHRPositionDao.GetListByHQL(" 1=1 ");
            if (hrpositionlist == null || hrpositionlist.Count <= 0)
            {
                hrpositionlist = new List<ZhiFang.Entity.RBAC.HRPosition>();
            }
            foreach (var hrposition in hrpositionlist_tmp)
            {
                hrposition.DataAddTime = hrposition.DataAddTime.Value;
                if (hrpositionlist.Count(a => a.Id == hrposition.Id) <= 0)
                {
                    bool flag = false;
                    if (IDHRPositionDao.Save(hrposition))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.HRPositionClone.同步{(flag ? "成功" : "失败")}！Id：{hrposition.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.HRPositionClone.已存在！Id：{hrposition.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hrpositionlist_tmp),
                OperName = EmpName,
                DataName = "HRPosition",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hrpositionlist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchHRPositionDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.HRPosition>();
            deptlist_tmp = IDQMSHRPositionDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue RBACEmpOptionsClone(string DBType, string EmpId, string EmpName, List<RBACEmpOptions> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var rbacempOptionslist_tmp = new List<ZhiFang.Entity.RBAC.RBACEmpOptions>();
            if (entity == null || entity.Count <= 0)
            {
                rbacempOptionslist_tmp = IDQMSRBACEmpOptionsDao.GetAllObjectList();
            }
            else
            {
                rbacempOptionslist_tmp = entity;
            }
            if (rbacempOptionslist_tmp == null || rbacempOptionslist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACEmpOptionsClone.未能获取数据源中的员工设置列表。");
                brdv.ErrorInfo = "未能获取数据源中的员工设置列表。";
                brdv.success = false;
                return brdv;
            }
            var rbacempOptionslist = IDRBACEmpOptionsDao.GetListByHQL(" 1=1 ");
            if (rbacempOptionslist == null || rbacempOptionslist.Count <= 0)
            {
                rbacempOptionslist = new List<ZhiFang.Entity.RBAC.RBACEmpOptions>();
            }
            foreach (var rbacempOptions in rbacempOptionslist_tmp)
            {
                if (rbacempOptions.HREmployee != null)
                {
                    var entitylist = IDHREmployeeDao.GetListByHQL(" Id=" + rbacempOptions.HREmployee.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacempOptions.HREmployee = IDHREmployeeDao.GetListByHQL(" Id=" + rbacempOptions.HREmployee.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacempOptions.HREmployee = null;
                    }
                }
                if (rbacempOptions.Default != null)
                {
                    var entitylist = IDRBACModuleDao.GetListByHQL(" Id=" + rbacempOptions.Default.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacempOptions.Default = IDRBACModuleDao.GetListByHQL(" Id=" + rbacempOptions.Default.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacempOptions.Default = null;
                    }
                }
                rbacempOptions.DataAddTime = rbacempOptions.DataAddTime.Value;
                if (rbacempOptionslist.Count(a => a.HREmployee.Id == rbacempOptions.HREmployee.Id && a.Default.Id == rbacempOptions.Default.Id) <= 0)
                {
                    bool flag = false;
                    if (IDRBACEmpOptionsDao.Save(rbacempOptions))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.RBACEmpOptionsClone.同步{(flag ? "成功" : "失败")}！Id：{rbacempOptions.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACEmpOptionsClone.已存在！Id：{rbacempOptions.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacempOptionslist_tmp),
                OperName = EmpName,
                DataName = "RBACEmpOptions",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacempOptionslist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchRBACEmpOptionsDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.RBACEmpOptions>();
            deptlist_tmp = IDQMSRBACEmpOptionsDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue RBACEmpRolesClone(string DBType, string EmpId, string EmpName, List<RBACEmpRoles> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var rbacempRoleslist_tmp = new List<ZhiFang.Entity.RBAC.RBACEmpRoles>();
            if (entity == null || entity.Count <= 0)
            {
                rbacempRoleslist_tmp = IDQMSRBACEmpRolesDao.GetAllObjectList();
            }
            else
            {
                rbacempRoleslist_tmp = entity;
            }
            if (rbacempRoleslist_tmp == null || rbacempRoleslist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACEmpRolesClone.未能获取数据源中的员工角色列表。");
                brdv.ErrorInfo = "未能获取数据源中的员工角色列表。";
                brdv.success = false;
                return brdv;
            }
            var rbacempRoleslist = IDRBACEmpRolesDao.GetListByHQL(" 1=1 ");
            if (rbacempRoleslist == null || rbacempRoleslist.Count <= 0)
            {
                rbacempRoleslist = new List<ZhiFang.Entity.RBAC.RBACEmpRoles>();
            }
            foreach (var rbacempRoles in rbacempRoleslist_tmp)
            {
                if (rbacempRoles.HREmployee != null)
                {
                    var entitylist = IDHREmployeeDao.GetListByHQL(" Id=" + rbacempRoles.HREmployee.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacempRoles.HREmployee = IDHREmployeeDao.GetListByHQL(" Id=" + rbacempRoles.HREmployee.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacempRoles.HREmployee = null;
                    }
                }
                if (rbacempRoles.RBACRole != null)
                {
                    var entitylist = IDRBACRoleDao.GetListByHQL(" Id=" + rbacempRoles.RBACRole.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacempRoles.RBACRole = IDRBACRoleDao.GetListByHQL(" Id=" + rbacempRoles.RBACRole.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacempRoles.RBACRole = null;
                    }
                }
                rbacempRoles.DataAddTime = rbacempRoles.DataAddTime.Value;
                if (rbacempRoleslist.Count(a => a.HREmployee.Id == rbacempRoles.HREmployee.Id && a.RBACRole.Id == rbacempRoles.RBACRole.Id) <= 0)
                {
                    bool flag = false;
                    if (IDRBACEmpRolesDao.Save(rbacempRoles))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.RBACEmpRolesClone.同步{(flag ? "成功" : "失败")}！Id：{rbacempRoles.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACEmpRolesClone.已存在！Id：{rbacempRoles.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacempRoleslist_tmp),
                OperName = EmpName,
                DataName = "RBACEmpRoles",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacempRoleslist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchRBACEmpRolesDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.RBACEmpRoles>();
            deptlist_tmp = IDQMSRBACEmpRolesDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue RBACModuleClone(string DBType, string EmpId, string EmpName, List<RBACModule> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var rbacmodulelist_tmp = new List<ZhiFang.Entity.RBAC.RBACModule>();
            if (entity == null || entity.Count <= 0)
            {
                rbacmodulelist_tmp = IDQMSRBACModuleDao.GetAllObjectList();
            }
            else
            {
                rbacmodulelist_tmp = entity;
            }
            if (rbacmodulelist_tmp == null || rbacmodulelist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACModuleClone.未能获取数据源中的模块列表。");
                brdv.ErrorInfo = "未能获取数据源中的模块列表。";
                brdv.success = false;
                return brdv;
            }
            var rbacmodulelist = IDRBACModuleDao.GetListByHQL(" 1=1 ");
            if (rbacmodulelist == null || rbacmodulelist.Count <= 0)
            {
                rbacmodulelist = new List<ZhiFang.Entity.RBAC.RBACModule>();
            }
            foreach (var rbacmodule in rbacmodulelist_tmp)
            {
                if (rbacmodule.BTDAppComponents != null)
                {
                    var entitylist = IDBTDAppComponentsDao.GetListByHQL(" Id=" + rbacmodule.BTDAppComponents.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacmodule.BTDAppComponents = IDBTDAppComponentsDao.GetListByHQL(" Id=" + rbacmodule.BTDAppComponents.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacmodule.BTDAppComponents = null;
                    }
                }
                rbacmodule.DataAddTime = rbacmodule.DataAddTime.Value;
                if (rbacmodulelist.Count(a => a.Id == rbacmodule.Id) <= 0)
                {
                    bool flag = false;
                    if (IDRBACModuleDao.Save(rbacmodule))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.RBACModuleClone.同步{(flag ? "成功" : "失败")}！Id：{rbacmodule.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACModuleClone.已存在！Id：{rbacmodule.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacmodulelist_tmp),
                OperName = EmpName,
                DataName = "RBACModule",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacmodulelist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchRBACModuleDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.RBACModule>();
            deptlist_tmp = IDQMSRBACModuleDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue RBACModuleOperClone(string DBType, string EmpId, string EmpName, List<RBACModuleOper> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var rbacmoduleOperlist_tmp = new List<ZhiFang.Entity.RBAC.RBACModuleOper>();
            if (entity == null || entity.Count <= 0)
            {
                rbacmoduleOperlist_tmp = IDQMSRBACModuleOperDao.GetAllObjectList();
            }
            else
            {
                rbacmoduleOperlist_tmp = entity;
            }
            if (rbacmoduleOperlist_tmp == null || rbacmoduleOperlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACModuleOperClone.未能获取数据源中的模块操作列表。");
                brdv.ErrorInfo = "未能获取数据源中的模块操作列表。";
                brdv.success = false;
                return brdv;
            }
            var rbacmoduleOperlist = IDRBACModuleOperDao.GetListByHQL(" 1=1 ");
            if (rbacmoduleOperlist == null || rbacmoduleOperlist.Count <= 0)
            {
                rbacmoduleOperlist = new List<ZhiFang.Entity.RBAC.RBACModuleOper>();
            }
            foreach (var rbacmoduleOper in rbacmoduleOperlist_tmp)
            {
                if (rbacmoduleOper.BTDAppComponentsOperate != null)
                {
                    var entitylist = IDBTDAppComponentsOperateDao.GetListByHQL(" Id=" + rbacmoduleOper.BTDAppComponentsOperate.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacmoduleOper.BTDAppComponentsOperate = IDBTDAppComponentsOperateDao.GetListByHQL(" Id=" + rbacmoduleOper.BTDAppComponentsOperate.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacmoduleOper.BTDAppComponentsOperate = null;
                    }
                }
                if (rbacmoduleOper.RBACModule != null)
                {
                    var entitylist = IDRBACModuleDao.GetListByHQL(" Id=" + rbacmoduleOper.RBACModule.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacmoduleOper.RBACModule = IDRBACModuleDao.GetListByHQL(" Id=" + rbacmoduleOper.RBACModule.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacmoduleOper.RBACModule = null;
                    }
                }
                if (rbacmoduleOper.RBACRowFilter != null)
                {
                    var entitylist = IDRBACRowFilterDao.GetListByHQL(" Id=" + rbacmoduleOper.RBACRowFilter.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacmoduleOper.RBACRowFilter = IDRBACRowFilterDao.GetListByHQL(" Id=" + rbacmoduleOper.RBACRowFilter.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacmoduleOper.RBACRowFilter = null;
                    }
                }
                rbacmoduleOper.DataAddTime = rbacmoduleOper.DataAddTime.Value;
                if (rbacmoduleOperlist.Count(a => a.Id == rbacmoduleOper.Id) <= 0)
                {
                    bool flag = false;
                    if (IDRBACModuleOperDao.Save(rbacmoduleOper))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.RBACModuleOperClone.同步{(flag ? "成功" : "失败")}！Id：{rbacmoduleOper.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACModuleOperClone.已存在！Id：{rbacmoduleOper.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacmoduleOperlist_tmp),
                OperName = EmpName,
                DataName = "RBACModuleOper",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacmoduleOperlist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchRBACModuleOperDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.RBACModuleOper>();
            deptlist_tmp = IDQMSRBACModuleOperDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue RBACRoleClone(string DBType, string EmpId, string EmpName, List<RBACRole> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var rbacrolelist_tmp = new List<ZhiFang.Entity.RBAC.RBACRole>();
            if (entity == null || entity.Count <= 0)
            {
                rbacrolelist_tmp = IDQMSRBACRoleDao.GetAllObjectList();
            }
            else
            {
                rbacrolelist_tmp = entity;
            }
            if (rbacrolelist_tmp == null || rbacrolelist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACRoleClone.未能获取数据源中的角色列表。");
                brdv.ErrorInfo = "未能获取数据源中的角色列表。";
                brdv.success = false;
                return brdv;
            }
            var rbacmoduleOperlist = IDRBACRoleDao.GetListByHQL(" 1=1 ");
            if (rbacmoduleOperlist == null || rbacmoduleOperlist.Count <= 0)
            {
                rbacmoduleOperlist = new List<ZhiFang.Entity.RBAC.RBACRole>();
            }
            foreach (var rbacrole in rbacrolelist_tmp)
            {
                rbacrole.DataAddTime = rbacrole.DataAddTime.Value;
                if (rbacmoduleOperlist.Count(a => a.Id == rbacrole.Id) <= 0)
                {
                    bool flag = false;
                    if (IDRBACRoleDao.Save(rbacrole))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.RBACRoleClone.同步{(flag ? "成功" : "失败")}！Id：{rbacrole.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACRoleClone.已存在！Id：{rbacrole.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacrolelist_tmp),
                OperName = EmpName,
                DataName = "RBACRole",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacrolelist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchRBACRoleDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.RBACRole>();
            deptlist_tmp = IDQMSRBACRoleDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue RBACRoleModuleClone(string DBType, string EmpId, string EmpName, List<RBACRoleModule> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var rbacroleModulelist_tmp = new List<ZhiFang.Entity.RBAC.RBACRoleModule>();
            if (entity == null || entity.Count <= 0)
            {
                rbacroleModulelist_tmp = IDQMSRBACRoleModuleDao.GetAllObjectList();
            }
            else
            {
                rbacroleModulelist_tmp = entity;
            }
            if (rbacroleModulelist_tmp == null || rbacroleModulelist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACRoleModuleClone.未能获取数据源中的角色模块访问权限列表。");
                brdv.ErrorInfo = "未能获取数据源中的角色模块访问权限列表。";
                brdv.success = false;
                return brdv;
            }
            var rbacmoduleOperlist = IDRBACRoleModuleDao.GetListByHQL(" 1=1 ");
            if (rbacmoduleOperlist == null || rbacmoduleOperlist.Count <= 0)
            {
                rbacmoduleOperlist = new List<ZhiFang.Entity.RBAC.RBACRoleModule>();
            }
            foreach (var rbacroleModule in rbacroleModulelist_tmp)
            {
                if (rbacroleModule.RBACModule != null)
                {
                    var entitylist = IDRBACModuleDao.GetListByHQL(" Id=" + rbacroleModule.RBACModule.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacroleModule.RBACModule = IDRBACModuleDao.GetListByHQL(" Id=" + rbacroleModule.RBACModule.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacroleModule.RBACModule = null;
                    }
                }
                if (rbacroleModule.RBACRole != null)
                {
                    var entitylist = IDRBACRoleDao.GetListByHQL(" Id=" + rbacroleModule.RBACRole.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacroleModule.RBACRole = IDRBACRoleDao.GetListByHQL(" Id=" + rbacroleModule.RBACRole.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacroleModule.RBACRole = null;
                    }
                }
                rbacroleModule.DataAddTime = rbacroleModule.DataAddTime.Value;
                if (rbacmoduleOperlist.Count(a => a.RBACModule.Id == rbacroleModule.RBACModule.Id && a.RBACRole.Id == rbacroleModule.RBACRole.Id) <= 0)
                {
                    bool flag = false;
                    if (IDRBACRoleModuleDao.Save(rbacroleModule))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.RBACRoleModuleClone.同步{(flag ? "成功" : "失败")}！Id：{rbacroleModule.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACRoleModuleClone.已存在！Id：{rbacroleModule.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacroleModulelist_tmp),
                OperName = EmpName,
                DataName = "RBACRoleModule",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacroleModulelist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchRBACRoleModuleDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.RBACRoleModule>();
            deptlist_tmp = IDQMSRBACRoleModuleDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue RBACRoleRightClone(string DBType, string EmpId, string EmpName, List<RBACRoleRight> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var rbacroleRightlist_tmp = new List<ZhiFang.Entity.RBAC.RBACRoleRight>();
            if (entity == null || entity.Count <= 0)
            {
                rbacroleRightlist_tmp = IDQMSRBACRoleRightDao.GetAllObjectList();
            }
            else
            {
                rbacroleRightlist_tmp = entity;
            }
            if (rbacroleRightlist_tmp == null || rbacroleRightlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACRoleRightClone.未能获取数据源中的角色权限列表。");
                brdv.ErrorInfo = "未能获取数据源中的角色权限列表。";
                brdv.success = false;
                return brdv;
            }
            var rbacroleRightlist = IDRBACRoleRightDao.GetListByHQL(" 1=1 ");
            if (rbacroleRightlist == null || rbacroleRightlist.Count <= 0)
            {
                rbacroleRightlist = new List<ZhiFang.Entity.RBAC.RBACRoleRight>();
            }
            foreach (var rbacroleRight in rbacroleRightlist_tmp)
            {
                if (rbacroleRight.RBACModuleOper != null)
                {
                    var entitylist = IDRBACModuleOperDao.GetListByHQL(" Id=" + rbacroleRight.RBACModuleOper.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacroleRight.RBACModuleOper = IDRBACModuleOperDao.GetListByHQL(" Id=" + rbacroleRight.RBACModuleOper.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacroleRight.RBACModuleOper = null;
                    }
                }
                if (rbacroleRight.RBACRole != null)
                {
                    var entitylist = IDRBACRoleDao.GetListByHQL(" Id=" + rbacroleRight.RBACRole.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacroleRight.RBACRole = IDRBACRoleDao.GetListByHQL(" Id=" + rbacroleRight.RBACRole.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacroleRight.RBACRole = null;
                    }
                }
                if (rbacroleRight.RBACRowFilter != null)
                {
                    var entitylist = IDRBACRowFilterDao.GetListByHQL(" Id=" + rbacroleRight.RBACRowFilter.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacroleRight.RBACRowFilter = IDRBACRowFilterDao.GetListByHQL(" Id=" + rbacroleRight.RBACRowFilter.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacroleRight.RBACRowFilter = null;
                    }
                }
                rbacroleRight.DataAddTime = rbacroleRight.DataAddTime.Value;
                if (rbacroleRightlist.Count(a => a.RBACRole.Id == rbacroleRight.RBACRole.Id && a.RBACModuleOper.Id == rbacroleRight.RBACModuleOper.Id && a.RBACRowFilter.Id == rbacroleRight.RBACRowFilter.Id) <= 0)
                {
                    bool flag = false;
                    if (IDRBACRoleRightDao.Save(rbacroleRight))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.RBACRoleRightClone.同步{(flag ? "成功" : "失败")}！Id：{rbacroleRight.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACRoleRightClone.已存在！Id：{rbacroleRight.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacroleRightlist_tmp),
                OperName = EmpName,
                DataName = "RBACRoleRight",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacroleRightlist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchRBACRoleRightDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.RBACRoleRight>();
            deptlist_tmp = IDQMSRBACRoleRightDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue RBACRowFilterClone(string DBType, string EmpId, string EmpName, List<RBACRowFilter> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var rbacrowFilterlist_tmp = new List<ZhiFang.Entity.RBAC.RBACRowFilter>();
            if (entity == null || entity.Count <= 0)
            {
                rbacrowFilterlist_tmp = IDQMSRBACRowFilterDao.GetAllObjectList();
            }
            else
            {
                rbacrowFilterlist_tmp = entity;
            }
            if (rbacrowFilterlist_tmp == null || rbacrowFilterlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACRowFilterClone.未能获取数据源中的行过滤列表。");
                brdv.ErrorInfo = "未能获取数据源中的行过滤列表。";
                brdv.success = false;
                return brdv;
            }
            var rbacrowFilterlist = IDRBACRowFilterDao.GetListByHQL(" 1=1 ");
            if (rbacrowFilterlist == null || rbacrowFilterlist.Count <= 0)
            {
                rbacrowFilterlist = new List<ZhiFang.Entity.RBAC.RBACRowFilter>();
            }
            foreach (var rbacrowFilter in rbacrowFilterlist_tmp)
            {
                rbacrowFilter.DataAddTime = rbacrowFilter.DataAddTime.Value;
                if (rbacrowFilterlist.Count(a => a.Id == rbacrowFilter.Id) <= 0)
                {
                    bool flag = false;
                    if (IDRBACRowFilterDao.Save(rbacrowFilter))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.RBACRowFilterClone.同步{(flag ? "成功" : "失败")}！Id：{rbacrowFilter.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACRowFilterClone.已存在！Id：{rbacrowFilter.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacrowFilterlist_tmp),
                OperName = EmpName,
                DataName = "RBACRowFilter",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacrowFilterlist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchRBACRowFilterDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.RBACRowFilter>();
            deptlist_tmp = IDQMSRBACRowFilterDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue RBACUserClone(string DBType, string EmpId, string EmpName, List<RBACUser> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var rbacuserlist_tmp = new List<ZhiFang.Entity.RBAC.RBACUser>();
            if (entity == null || entity.Count <= 0)
            {
                rbacuserlist_tmp = IDQMSRBACUserDao.GetAllObjectList();
            }
            else
            {
                rbacuserlist_tmp = entity;
            }
            if (rbacuserlist_tmp == null || rbacuserlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACUserClone.未能获取数据源中的帐户列表。");
                brdv.ErrorInfo = "未能获取数据源中的帐户列表。";
                brdv.success = false;
                return brdv;
            }
            var rbacuserlist = IDRBACUserDao.GetListByHQL(" 1=1 ");
            if (rbacuserlist == null || rbacuserlist.Count <= 0)
            {
                rbacuserlist = new List<ZhiFang.Entity.RBAC.RBACUser>();
            }
            foreach (var rbacuser in rbacuserlist_tmp)
            {
                if (rbacuser.HREmployee != null)
                {
                    var entitylist = IDHREmployeeDao.GetListByHQL(" Id=" + rbacuser.HREmployee.Id + " ");
                    if (entitylist != null && entitylist.Count > 0)
                    {
                        rbacuser.HREmployee = IDHREmployeeDao.GetListByHQL(" Id=" + rbacuser.HREmployee.Id + " ").ElementAt(0);
                    }
                    else
                    {
                        rbacuser.HREmployee = null;
                    }
                }
                rbacuser.DataAddTime = rbacuser.DataAddTime.Value;
                if (rbacuserlist.Count(a =>  a.Account == rbacuser.Account) <= 0)
                {
                    bool flag = false;
                    if (IDRBACUserDao.Save(rbacuser))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSHEmpClone.RBACUserClone.同步{(flag ? "成功" : "失败")}！Id：{rbacuser.Id}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBQMSEmpClone.RBACUserClone.已存在！Id：{rbacuser.Id}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacuserlist_tmp),
                OperName = EmpName,
                DataName = "RBACUser",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_QMS系统.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacuserlist_tmp);
            return brdv;
        }

        public BaseResultDataValue CatchRBACUserDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.RBACUser>();
            deptlist_tmp = IDQMSRBACUserDao.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }
    }
}
