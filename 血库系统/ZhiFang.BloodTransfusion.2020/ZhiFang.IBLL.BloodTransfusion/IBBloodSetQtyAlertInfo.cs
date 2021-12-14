

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodSetQtyAlertInfo : IBGenericManager<BloodSetQtyAlertInfo>
	{
		/// <summary>
		/// 库存预警设置选择血制品时,获取待选择的血制品信息(HQL)
		/// </summary>
		/// <param name="page"></param>
		/// <param name="limit"></param>
		/// <param name="where"></param>
		/// <param name="linkWhere"></param>
		/// <param name="sort"></param>
		/// <returns></returns>
		EntityList<BloodStyle> SearchBloodStyleBySetQtyAlertInfoHQL(int page, int limit, string where, string linkWhere, string sort);

	}
}