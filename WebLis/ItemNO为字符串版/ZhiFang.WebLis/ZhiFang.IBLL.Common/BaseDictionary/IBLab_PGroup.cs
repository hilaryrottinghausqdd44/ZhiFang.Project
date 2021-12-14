using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_PGroup	
	/// </summary>
	public interface IBLab_PGroup :IBBase<ZhiFang.Model.Lab_PGroup>,IBDataPage<ZhiFang.Model.Lab_PGroup>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabSectionNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabSectionNo);
        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.Lab_PGroup> DataTableToList(DataTable dt);	
		int DeleteList(string SectionIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_PGroup GetModel(string LabCode,int LabSectionNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_PGroup model);
        DataSet GetListByLike(ZhiFang.Model.Lab_PGroup model);
		#endregion  成员方法
	} 
}