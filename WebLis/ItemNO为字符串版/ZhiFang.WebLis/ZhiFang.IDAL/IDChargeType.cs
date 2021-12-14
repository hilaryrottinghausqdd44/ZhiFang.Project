using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDChargeType	
	/// </summary>
	public interface IDChargeType:IDataBase<ZhiFang.Model.ChargeType>,IDataPage<ZhiFang.Model.ChargeType>
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

        int DeleteList(string ChargeIDlist);
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.ChargeType GetModel(int ChargeNo);
		
		#endregion  成员方法
	} 
}