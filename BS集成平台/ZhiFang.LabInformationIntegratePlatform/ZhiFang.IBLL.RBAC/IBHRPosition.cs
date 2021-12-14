using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ZhiFang.IBLL.RBAC
{	
	public interface IBHRPosition : ZhiFang.IBLL.Base.IBGenericManager<ZhiFang.Entity.RBAC.HRPosition>
	{
		bool Add();
        bool Edit();
        bool Remove();
	} 
}