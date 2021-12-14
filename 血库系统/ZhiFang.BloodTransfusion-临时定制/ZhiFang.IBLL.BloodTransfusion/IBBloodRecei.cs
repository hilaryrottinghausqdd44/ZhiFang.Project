

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodRecei : IBGenericManager<BloodRecei, string>
	{
		/// <summary>
		/// (输血申请综合查询)按申请信息获取到相应的样本信息
		/// 先按申请单号获取,如果获取不到,再按申请信息获取
		/// </summary>
		/// <param name="reqFormId"></param>
		/// <param name="bReqVO"></param>
		/// <param name="sort"></param>
		/// <param name="page"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		EntityList<BloodRecei> SearchBloodReceiListByBReqVO(string reqFormId, BloodBReqFormVO bReqVO, string sort, int page, int limit);
	}
}