

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSCBarCodeRules : IBGenericManager<SCBarCodeRules>
	{
		/// <summary>
		/// 按机构Id初始化条码信息
		/// </summary>
		/// <param name="labID"></param>
		/// <returns></returns>
		BaseResultBool AddInitysOfLabId(long labID);

		/// <summary>
		/// 获取某一条码类型的下一条码值
		/// </summary>
		/// <param name="labId"></param>
		/// <param name="bmsType"></param>
		/// <param name="maxBarCode"></param>
		/// <returns></returns>
		string GetNextBarCode(long labId, string bmsType,ref long maxBarCode);

		/// <summary>
		/// 默认不添加按LabID的过滤条件获取数据
		/// </summary>
		IList<SCBarCodeRules> GetListOfNoLabIDByHql(string hqlWhere);

	}
}