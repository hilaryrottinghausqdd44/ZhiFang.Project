using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.IBLL.LIIP.RBACClone
{
    public interface IBBDeptClone
    {
        ZhiFang.Entity.Base.BaseResultDataValue DeptClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HRDept> entity);
        ZhiFang.Entity.Base.BaseResultDataValue CatchDeptDataList(string DBType);
        ZhiFang.Entity.Base.BaseResultDataValue HRDeptClone(string DBType, string EmpId, string EmpName, List<ZhiFang.Entity.RBAC.HRDept> entity);
    }
}
