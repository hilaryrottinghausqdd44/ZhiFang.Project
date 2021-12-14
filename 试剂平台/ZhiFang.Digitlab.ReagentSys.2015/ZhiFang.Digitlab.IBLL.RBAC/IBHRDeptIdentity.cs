using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ZhiFang.Digitlab.IBLL.RBAC
{	
	public interface IBHRDeptIdentity : IBGenericManager<ZhiFang.Digitlab.Entity.HRDeptIdentity>
	{
		bool Add();
        bool Edit();
        bool Remove();
	} 
}