

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodClass : IBGenericManager<BloodClass>
	{
		void AddSCOperation(BloodClass serverEntity, string[] arrFields, long empID, string empName);
	}
}