

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodTransRecordTypeItem : IBGenericManager<BloodTransRecordTypeItem>
	{
		/// <summary>
		/// (输血过程记录登记)获取不良反应表单记录项(病人体征信息)", Desc = "(输血过程记录登记)获取不良反应表单记录项(病人体征信息)
		/// </summary>
		/// <param name="where"></param>
		/// <param name="sort"></param>
		/// <returns></returns>
		BaseResultDataValue SearchTransfusionAntriesOfBloodTransByHQL(string where, string sort);
	}
}