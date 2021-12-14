

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodBagOperationDtl : IBGenericManager<BloodBagOperationDtl>
	{
        BaseResultDataValue AddDtlListOfHandover(BloodBagOperation entity, IList<BloodBagOperationDtl> bloodBagOperationDtlList, long empID, string empName);

    }
}