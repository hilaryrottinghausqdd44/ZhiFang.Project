using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDEquipmentControl	
	/// </summary>
	public interface IDEquipmentControl:IDataBase<ZhiFang.Model.EquipmentControl>
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
	
		
		#endregion  成员方法
	} 
}