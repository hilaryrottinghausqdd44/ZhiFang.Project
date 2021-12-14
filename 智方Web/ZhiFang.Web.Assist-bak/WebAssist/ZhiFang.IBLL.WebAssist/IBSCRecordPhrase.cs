

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSCRecordPhrase : IBGenericManager<SCRecordPhrase>
	{
		void AddSCOperation(SCRecordPhrase serverEntity, string[] arrFields, long empID, string empName);
		BaseResultDataValue SearchSCRecordPhraseOfGKByHQL(int page, int limit, string where, string sort);

	}
}