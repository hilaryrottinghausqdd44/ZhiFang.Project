using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.IDAO.Base;


namespace ZhiFang.IDAO.LIIP
{
	public interface IDEmpClone
	{
		//List<ZhiFang.Entity.RBAC.HREmployee> GetAllDoctorList();
		//List<ZhiFang.Entity.RBAC.HREmployee> GetAllNPuserList();
		//List<ZhiFang.Entity.RBAC.HREmployee> GetAllPuserList();
		List<ZhiFang.Entity.RBAC.HREmployee> GetAllEmpList();
		bool Add(ZhiFang.Entity.RBAC.HREmployee entity);
	} 
}