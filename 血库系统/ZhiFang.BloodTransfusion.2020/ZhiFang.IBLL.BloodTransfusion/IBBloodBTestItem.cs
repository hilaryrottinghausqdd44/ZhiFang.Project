

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodBTestItem : IBGenericManager<BloodBTestItem>
	{
		void AddSCOperation(BloodBTestItem serverEntity, string[] arrFields, long empID, string empName);
	}
}