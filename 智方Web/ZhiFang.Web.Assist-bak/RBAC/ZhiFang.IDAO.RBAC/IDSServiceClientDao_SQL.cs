using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.RBAC
{
    public interface IDSServiceClientDao_SQL : IDBaseDao<SServiceClient, long>
    {
        SServiceClient ObtainById(long labId);
    }
}
