

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSCRecordDtl : IBGenericManager<SCRecordDtl>
	{
		/// <summary>
		/// 新增院感登记信息
		/// </summary>
		/// <param name="docEntity"></param>
		/// <param name="dtlEntityList"></param>
		/// <param name="empID"></param>
		/// <param name="empName"></param>
		/// <returns></returns>
		BaseResultDataValue AddSCRecordDtlListOfGK(GKSampleRequestForm docEntity,ref IList<SCRecordDtl> dtlEntityList, long empID, string empName);

		/// <summary>
		/// 更新院感记录项信息
		/// </summary>
		/// <param name="docEntity"></param>
		/// <param name="dtlEntityList"></param>
		/// <param name="empID"></param>
		/// <param name="empName"></param>
		/// <returns></returns>
		BaseResultBool EditSCRecordDtlOfGKList(GKSampleRequestForm docEntity,ref IList<SCRecordDtl> dtlEntityList);
	}
}