using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.LIIP.RBACClone;

namespace ZhiFang.BLL.LIIP.RBACClone
{
    public class BBDeptEmp : IBBDeptEmpClone
    {
        public ZhiFang.IDAO.LIIP.IDepartmentClone IDepartmentClone { get;set;}
        public ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        public BaseResultDataValue DeptClone(string DBType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            int count = 0;
            var deptlist_tmp=IDepartmentClone.GetAllObjectList();
            if (deptlist_tmp == null || deptlist_tmp.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.LIIP.RBACClone.BBDept.DeptClone.未能获取数据源中的部门列表。");
                brdv.ErrorInfo = "未能获取数据源中的部门列表。";
                brdv.success = false;
                return brdv;
            }
            //List<string> deptcodelist = new List<string>();
            //deptlist_tmp.ForEach(a => deptcodelist.Add(a.StandCode));
            var deptlist=IDHRDeptDao.GetListByHQL(" 1=1 ");
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
            return brdv;
        }

        public BaseResultDataValue DeptEmpClone(string DBType)
        {
            throw new NotImplementedException();
        }
    }
}
