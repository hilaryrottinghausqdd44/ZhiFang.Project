

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSampleType : IBGenericManager<SampleType, int>
	{
		/// <summary>
		/// 按对照码获取样本名称
		/// </summary>
		/// <param name="code1"></param>
		/// <returns></returns>
		string GetSampleTypeCName(string code1);

	}
}