

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodBagChargeLink : IBGenericManager<BloodBagChargeLink>
	{
		void AddSCOperation(BloodBagChargeLink serverEntity, string[] arrFields, long empID, string empName);
	}
}