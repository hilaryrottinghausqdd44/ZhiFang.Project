using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.NHB.RBAC
{	
	public class BCountyDao : Base.BaseDaoNHBService<BCounty, long>, IDBCountyDao
	{
	} 
}