using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ZhiFang.IBLL.RBAC
{	
	public interface IBHREmpIdentity : ZhiFang.IBLL.Base.IBGenericManager<ZhiFang.Entity.RBAC.HREmpIdentity>
	{
		bool Add();
        bool Edit();
        bool Remove();
	} 
}