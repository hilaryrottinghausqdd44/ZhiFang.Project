using System;
using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using System.Linq;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class SServiceClientDao :BaseDaoNHBService<SServiceClient, long>, IDSServiceClientDao
    {
    }
}