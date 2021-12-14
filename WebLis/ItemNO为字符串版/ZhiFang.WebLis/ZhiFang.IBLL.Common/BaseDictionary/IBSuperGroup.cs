using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBSuperGroup	
	/// </summary>
	public interface IBSuperGroup :IBBase<ZhiFang.Model.SuperGroup>,IBDataPage<ZhiFang.Model.SuperGroup>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int SuperGroupNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int SuperGroupNo);
				
		int DeleteList(string SuperGroupIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.SuperGroup GetModel(int SuperGroupNo);
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.SuperGroup> DataTableToList(DataTable dt);			
				
		#endregion  成员方法

        DataSet GetList(int ListColCount, int PageIndex, Model.SuperGroup superGroup);

        DataSet GetParentSuperGroupNolist();
    } 
}