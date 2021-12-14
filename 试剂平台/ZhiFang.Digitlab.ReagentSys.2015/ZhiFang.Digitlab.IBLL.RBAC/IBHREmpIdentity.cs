using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ZhiFang.Digitlab.IBLL.RBAC
{	
	public interface IBHREmpIdentity : IBGenericManager<ZhiFang.Digitlab.Entity.HREmpIdentity>
	{
		bool Add();
        bool Edit();
        bool Remove();
	} 
}