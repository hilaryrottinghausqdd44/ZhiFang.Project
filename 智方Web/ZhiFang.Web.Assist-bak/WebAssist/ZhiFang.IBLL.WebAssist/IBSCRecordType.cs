

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSCRecordType : IBGenericManager<SCRecordType>
	{
		void AddSCOperation(SCRecordType serverEntity, string[] arrFields, long empID, string empName);
	}
}