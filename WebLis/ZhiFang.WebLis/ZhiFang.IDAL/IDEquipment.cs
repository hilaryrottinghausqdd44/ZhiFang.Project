using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDEquipment	
	/// </summary>
	public interface IDEquipment:IDataBase<ZhiFang.Model.Equipment>,IDataPage<ZhiFang.Model.Equipment>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int EquipNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(int EquipNo);

        int DeleteList(string EquipIDlist);
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Equipment GetModel(int EquipNo);
		
		
		#endregion  成员方法
	} 
}