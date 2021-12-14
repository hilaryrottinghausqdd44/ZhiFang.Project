using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBStatusTypeControl	
	/// </summary>
	public interface IBStatusTypeControl :IBBase<ZhiFang.Model.StatusTypeControl>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string StatusControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string StatusControlNo);
				
		int DeleteList(string Idlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.StatusTypeControl GetModel(string StatusControlNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.StatusTypeControl model);
		#endregion  成员方法
	} 
}