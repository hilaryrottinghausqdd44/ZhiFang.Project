using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.RBAC
{
	/// <summary>
	///
	/// </summary>
	public  interface IBDepartmentUser : IBGenericManager<DepartmentUser>
	{
		/// <summary>
		/// LIS同步原院感申请记录信息
		/// </summary>
		/// <returns></returns>
		BaseResultBool SaveSyncDeptUserOfGKForm();

	}
}