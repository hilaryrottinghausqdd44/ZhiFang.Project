

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodUnit : IBGenericManager<BloodUnit>
	{
		void AddSCOperation(BloodUnit serverEntity, string[] arrFields, long empID, string empName);
	}
}