

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBloodBagRecordType : IBGenericManager<BloodBagRecordType>
	{
		void AddSCOperation(BloodBagRecordType serverEntity, string[] arrFields, long empID, string empName);
	}
}