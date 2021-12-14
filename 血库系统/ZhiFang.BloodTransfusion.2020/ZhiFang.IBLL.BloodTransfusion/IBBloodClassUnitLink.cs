

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodClassUnitLink : IBGenericManager<BloodClassUnitLink>
	{
		void AddSCOperation(BloodClassUnitLink serverEntity, string[] arrFields, long empID, string empName);
		/// <summary>
		/// 血制品分类与单位换算关系选择血制品单位时,获取待选择的血制品单位信息(HQL)
		/// </summary>
		/// <param name="page"></param>
		/// <param name="limit"></param>
		/// <param name="where"></param>
		/// <param name="linkWhere"></param>
		/// <param name="sort"></param>
		/// <returns></returns>
		EntityList<BloodUnit> SearchBloodUnitByClassUnitLinkHQL(int page, int limit, string where, string linkWhere, string sort);
	}
}