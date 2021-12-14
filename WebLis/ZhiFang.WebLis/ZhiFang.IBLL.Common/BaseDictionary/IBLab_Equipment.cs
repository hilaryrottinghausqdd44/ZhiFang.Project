using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_Equipment	
	/// </summary>
	public interface IBLab_Equipment :IBBase<ZhiFang.Model.Lab_Equipment>,IBDataPage<ZhiFang.Model.Lab_Equipment>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabEquipNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabEquipNo);
				
		int DeleteList(string EquipIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_Equipment GetModel(string LabCode,int LabEquipNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_Equipment model);
        DataSet GetListByLike(ZhiFang.Model.Lab_Equipment model);
		#endregion  成员方法
	} 
}