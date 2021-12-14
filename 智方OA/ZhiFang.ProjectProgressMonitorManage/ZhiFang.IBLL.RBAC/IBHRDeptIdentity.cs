using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IBLL.RBAC
{	
	public interface IBHRDeptIdentity : ZhiFang.IBLL.Base.IBGenericManager<HRDeptIdentity>
	{
		bool Add();
        bool Edit();
        bool Remove();
	} 
}