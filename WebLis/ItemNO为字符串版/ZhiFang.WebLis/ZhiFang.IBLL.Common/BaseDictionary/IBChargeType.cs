using System;
using System.Data;
using ZhiFang.IBLL.Common;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBChargeType	
	/// </summary>
	public interface IBChargeType :IBBase<ZhiFang.Model.ChargeType>,IBDataPage<ZhiFang.Model.ChargeType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ChargeNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int ChargeNo);
				
		int DeleteList(string ChargeIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.ChargeType GetModel(int ChargeNo);
		
		#endregion  成员方法
	} 
}