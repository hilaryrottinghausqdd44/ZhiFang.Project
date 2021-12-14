using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.SA;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.RBAC
{
    public interface IDCenOrgDao_SQL : IDBaseDao<CenOrg, long>
    {
        CenOrg ObtainById(long labId);
    }
}
