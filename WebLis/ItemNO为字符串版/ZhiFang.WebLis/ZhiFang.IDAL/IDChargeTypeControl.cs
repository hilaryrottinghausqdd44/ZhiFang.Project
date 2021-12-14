using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDChargeTypeControl	
	/// </summary>
	public interface IDChargeTypeControl:IDataBase<ZhiFang.Model.ChargeTypeControl>
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
	
		#endregion  成员方法
	} 
}