using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ZhiFang.Digitlab.IBLL.RBAC
{	
	public interface IBHRPosition : IBGenericManager<ZhiFang.Digitlab.Entity.HRPosition>
	{
		bool Add();
        bool Edit();
        bool Remove();
	} 
}