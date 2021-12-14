using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBBUSINESSMANControl	
	/// </summary>
	public interface IBBUSINESSMANControl :IBBase<ZhiFang.Model.BUSINESSMANControl>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string BMANControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string BMANControlNo);
				
		int DeleteList(string Idlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.BUSINESSMANControl GetModel(string BMANControlNo);

		#endregion  成员方法
	} 
}