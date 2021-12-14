using System.Collections.Generic;
using System.Linq;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.LIIP.RBACClone;

namespace ZhiFang.BLL.LIIP.RBACClone
{
    public class BBDeptClone : IBBDeptClone
    {
        public ZhiFang.IDAO.LIIP.IDepartmentClone IDepartmentClone { get; set; }
        public ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        public ZhiFang.IDAO.LIIP.IDSLIIPSystemRBACCloneLog IDSLIIPSystemRBACCloneLog { get; set; }

        public BaseResultDataValue DeptClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HRDept> deptlistentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.HRDept>();
            if (deptlistentity == null || deptlistentity.Count <= 0)
            {
                deptlist_tmp = IDepartmentClone.GetAllObjectList();
            }
            else
            {
                deptlist_tmp = deptlistentity;
            }
            if (deptlist_tmp == null || deptlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBDept.DeptClone.未能获取数据源中的部门列表。");
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
                if (deptlist.Count(a => a.StandCode == dept.StandCode) <= 0)
                {
                    bool flag = false;
                    if (IDHRDeptDao.Save(dept))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBDept.DeptClone.同步{(flag ? "成功" : "失败")}！部门名称：{dept.CName},部门编码：{dept.StandCode},部门简称：{dept.SName}");
                }
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBDept.DeptClone.已存在！部门名称：{dept.CName},部门编码：{dept.StandCode},部门简称：{dept.SName}");
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp),
                OperName = EmpName,
                DataName = "Dept",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_检验之星6.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }
        public BaseResultDataValue CatchDeptDataList(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var deptlist_tmp = new List<ZhiFang.Entity.RBAC.HRDept>();
            deptlist_tmp = IDepartmentClone.GetAllObjectList();
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(deptlist_tmp);
            return brdv;
        }

        public BaseResultDataValue HRDeptClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HRDept> deptlistentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var hrdeptList_tmp = new List<ZhiFang.Entity.RBAC.HRDept>();

            if (deptlistentity == null || deptlistentity.Count <= 0)
            {
                hrdeptList_tmp = IDHRDeptDao.GetListByHQL(" 1=1 ").ToList();
            }
            else
            {
                hrdeptList_tmp = deptlistentity;
            }
            if (hrdeptList_tmp == null || hrdeptList_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBDept.HRDeptClone.未能获取数据源中的部门列表。");
                brdv.ErrorInfo = "未能获取数据源中的部门列表。";
                brdv.success = false;
                return brdv;
            }
            var deptlist = IDepartmentClone.GetAllObjectList();
            if (deptlist == null || deptlist.Count <= 0)
            {
                deptlist = new List<ZhiFang.Entity.RBAC.HRDept>();
            }
            for (int i = 0 ; i<hrdeptList_tmp.Count;i++)
            {
                if (deptlist.Count(a => a.StandCode == hrdeptList_tmp[i].StandCode) <= 0)
                {
                    bool flag = false;
                    if (IDepartmentClone.Add(hrdeptList_tmp[i]))
                    {
                        count++;
                        flag = true;
                    }
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBDept.HRDeptClone.同步{(flag ? "成功" : "失败")}！部门名称：{hrdeptList_tmp[i].CName},部门编码：{hrdeptList_tmp[i].StandCode},部门简称：{hrdeptList_tmp[i].SName}");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.RBACClone.BBDept.HRDeptClone.已存在！部门名称：{hrdeptList_tmp[i].CName},部门编码：{hrdeptList_tmp[i].StandCode},部门简称：{hrdeptList_tmp[i].SName}");
                }
                hrdeptList_tmp[i].HRDeptEmpList = null;
                hrdeptList_tmp[i].HRDeptIdentityList = null;
                hrdeptList_tmp[i].HREmployeeList = null;
            }

            IDSLIIPSystemRBACCloneLog.Save(new Entity.LIIP.SLIIPSystemRBACCloneLog()
            {
                OperId = long.TryParse(EmpId, out long result) ? result : 0,
                DataCount = count,
                DataJson = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hrdeptList_tmp),
                OperName = EmpName,
                DataName = "HRDept",
                ForwardFlag = true,
                SystemId = long.Parse(ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Key),
                SystemName = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Name,
                SystemCode = ZhiFang.Entity.LIIP.ZFSystem.智方_LIS平台.Value.Code
            });
            brdv.success = true;
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hrdeptList_tmp);
            return brdv;
        }
    }
}
