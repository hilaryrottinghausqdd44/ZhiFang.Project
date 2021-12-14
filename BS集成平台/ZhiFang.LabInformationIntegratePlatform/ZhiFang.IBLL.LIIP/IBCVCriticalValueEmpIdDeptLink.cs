using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
	/// <summary>
	///
	/// </summary>
	public  interface IBCVCriticalValueEmpIdDeptLink : IBGenericManager<CVCriticalValueEmpIdDeptLink>
	{
        BaseResultDataValue CV_SearchAndAddDoctorOrNurse(CV_AddDoctorOrNurseVO entity,out ZhiFang.Entity.RBAC.RBACUser rbacuser);
        BaseResultDataValue CV_SearchAndAddTech(CV_AddTechVO entity, out RBACUser rbacuser);
		BaseResultDataValue CV_AddDoctorOrNurseToEmp(CV_AddDoctorOrNurseVO entity);

	}
}