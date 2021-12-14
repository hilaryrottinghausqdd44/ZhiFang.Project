using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDStatusTypeControl	
	/// </summary>
	public interface IDStatusTypeControl:IDataBase<ZhiFang.Model.StatusTypeControl>
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
	
		
		#endregion  成员方法
	} 
}