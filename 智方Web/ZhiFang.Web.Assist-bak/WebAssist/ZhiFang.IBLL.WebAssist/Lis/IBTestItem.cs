

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  interface IBTestItem : IBGenericManager<TestItem, int>
	{
		/// <summary>
		/// 按对照码获取项目名称
		/// </summary>
		/// <param name="code1"></param>
		/// <returns></returns>
		string GetTestItemCName(string code1);

	}
}