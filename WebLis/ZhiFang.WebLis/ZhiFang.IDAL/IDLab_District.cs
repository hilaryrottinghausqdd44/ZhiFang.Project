using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_District	
	/// </summary>
	public interface IDLab_District:IDataBase<ZhiFang.Model.Lab_District>,IDataPage<ZhiFang.Model.Lab_District>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabDistrictNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabDistrictNo);
		
				int DeleteList(string DistrictIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_District GetModel(string LabCode,int LabDistrictNo);
		
		
		DataSet GetListByLike(ZhiFang.Model.Lab_District model);
		#endregion  成员方法
	} 
}