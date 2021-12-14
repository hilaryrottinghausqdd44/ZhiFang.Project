using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.IBLL.LIIP.RBACClone
{
    public interface IBBDeptEmpClone
    {
        ZhiFang.Entity.Base.BaseResultDataValue DeptEmpClone(string DBType);
    }
}
