using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_District	
	/// </summary>
	public interface IBLab_District :IBBase<ZhiFang.Model.Lab_District>,IBDataPage<ZhiFang.Model.Lab_District>
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

        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.Lab_District> DataTableToList(DataTable dt);	
		int DeleteList(string DistrictIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_District GetModel(string LabCode,int LabDistrictNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_District model);
        DataSet GetListByLike(ZhiFang.Model.Lab_District model);
		#endregion  成员方法
	} 
}