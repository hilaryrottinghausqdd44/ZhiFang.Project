using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBEquipmentControl	
	/// </summary>
	public interface IBEquipmentControl :IBBase<ZhiFang.Model.EquipmentControl>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string EquipControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string EquipControlNo);
				
		int DeleteList(string Idlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.EquipmentControl GetModel(string EquipControlNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.EquipmentControl model);
		#endregion  成员方法
	} 
}