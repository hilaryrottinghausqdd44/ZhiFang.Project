using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_FolkType	
	/// </summary>
	public interface IBLab_FolkType :IBBase<ZhiFang.Model.Lab_FolkType>,IBDataPage<ZhiFang.Model.Lab_FolkType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabFolkNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabFolkNo);
				
		int DeleteList(string FolkIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_FolkType GetModel(string LabCode,int LabFolkNo);

        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.Lab_FolkType> DataTableToList(DataTable

dt);

		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_FolkType model);
        DataSet GetListByLike(ZhiFang.Model.Lab_FolkType model);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        #endregion  成员方法
    } 
}