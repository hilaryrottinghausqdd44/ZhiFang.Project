using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBCLIENTELEControl	
	/// </summary>
	public interface IBCLIENTELEControl :IBBase<ZhiFang.Model.CLIENTELEControl>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ClIENTControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string ClIENTControlNo);
				
		int DeleteList(string Idlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.CLIENTELEControl GetModel(string ClIENTControlNo);

		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.CLIENTELEControl model);
		#endregion  成员方法
	} 
}