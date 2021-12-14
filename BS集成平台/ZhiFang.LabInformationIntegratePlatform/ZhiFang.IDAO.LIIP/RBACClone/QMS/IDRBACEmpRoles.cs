using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.IDAO.Base;


namespace ZhiFang.IDAO.LIIP.QMS
{
	public interface IDRBACEmpRoles
	{
		List<ZhiFang.Entity.RBAC.RBACEmpRoles> GetAllObjectList();
	} 
}