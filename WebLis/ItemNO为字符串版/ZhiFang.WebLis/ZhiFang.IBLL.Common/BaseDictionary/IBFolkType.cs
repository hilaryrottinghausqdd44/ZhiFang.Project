using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBFolkType	
	/// </summary>
	public interface IBFolkType :IBBase<ZhiFang.Model.FolkType>,IBDataPage<ZhiFang.Model.FolkType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int FolkNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int FolkNo);

        int DeleteList(string FolkIDlist);
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.FolkType GetModel(int FolkNo);
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.FolkType> DataTableToList(DataTable dt);			
		#endregion  成员方法
	} 
}