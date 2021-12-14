using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBChargeTypeControl	
	/// </summary>
	public interface IBChargeTypeControl :IBBase<ZhiFang.Model.ChargeTypeControl>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ChargeControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string ChargeControlNo);
				
		int DeleteList(string Idlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.ChargeTypeControl GetModel(string ChargeControlNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.ChargeTypeControl model);
		#endregion  成员方法
	} 
}