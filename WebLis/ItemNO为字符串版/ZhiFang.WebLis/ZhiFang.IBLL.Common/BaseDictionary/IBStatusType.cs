using System;
using System.Data;
using ZhiFang.IBLL.Common;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBStatusType	
	/// </summary>
	public interface IBStatusType :IBBase<ZhiFang.Model.StatusType>,IBDataPage<ZhiFang.Model.StatusType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int StatusNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int StatusNo);
				
		int DeleteList(string StatusIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.StatusType GetModel(int StatusNo);
				
		#endregion  成员方法
	} 
}