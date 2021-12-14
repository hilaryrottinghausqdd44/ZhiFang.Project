using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDepartment	
	/// </summary>
	public interface IBDepartment :IBBase<ZhiFang.Model.Department>,IBDataPage<ZhiFang.Model.Department>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DeptNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int DeptNo);

        int DeleteList(string DeptIDlist);
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Department GetModel(int DeptNo);
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.Department> DataTableToList(DataTable dt);			
        DataSet GetListLike(ZhiFang.Model.Department model);  
				
		#endregion  成员方法

        DataSet GetList(int p, Model.Department dept);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int Add(List<ZhiFang.Model.Department> modelList);
        int Update(List<Model.Department> modelList);
    } 
}
