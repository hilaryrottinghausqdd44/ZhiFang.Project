using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.RBAC
{
    public interface IDRBACModuleDao_SQL : IDBaseDao<RBACModule, long>
    {
       IList<RBACModule> ObtainLoadAll();
    }
}
