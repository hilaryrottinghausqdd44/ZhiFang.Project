

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  interface IBMEGroupSampleItem : IBGenericManager<MEGroupSampleItem>
	{
        /// <summary>
		/// 新增院感登记信息
		/// </summary>
		/// <param name="docEntity"></param>
		/// <param name="dtlEntityList"></param>
		/// <param name="empID"></param>
		/// <param name="empName"></param>
		/// <returns></returns>
		BaseResultDataValue SaveMEGroupSampleItemOfGK(MEGroupSampleForm docEntity, ref IList<MEGroupSampleItem> dtlEntityList, long empID, string empName);
    }
}