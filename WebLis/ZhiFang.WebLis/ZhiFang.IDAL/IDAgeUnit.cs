using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDDic_AgeUnit	
	/// </summary>
	public interface IDAgeUnit:IDataBase<ZhiFang.Model.AgeUnit>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int AgeUnitNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int AgeUnitNo);
		
		int DeleteList(string AgeUnitIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.AgeUnit GetModel(int AgeUnitNo);
		
		#endregion  成员方法
	} 
}