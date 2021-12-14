using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ZhiFang.Digitlab.IBLL.RBAC
{	
	public interface IBHRDeptEmp : IBGenericManager<ZhiFang.Digitlab.Entity.HRDeptEmp>
	{
		bool Add();
        bool Edit();
        bool Remove();
	} 
}