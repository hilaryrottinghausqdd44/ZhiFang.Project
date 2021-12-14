

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  interface IBMEGroupSampleForm : IBGenericManager<MEGroupSampleForm>
	{
        /// <summary>
        /// 按院感申请提交信息进行自动核收
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtlEntityList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue SaveByDeptAutoCheck(ref GKSampleRequestForm entity, IList<SCRecordDtl> dtlEntityList, long empID, string empName);

    }
}