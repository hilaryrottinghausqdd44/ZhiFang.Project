using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_SuperGroup	
	/// </summary>
	public interface IBLab_SuperGroup :IBBase<ZhiFang.Model.Lab_SuperGroup>,IBDataPage<ZhiFang.Model.Lab_SuperGroup>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabSuperGroupNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabSuperGroupNo);
				
		int DeleteList(string SuperGroupIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_SuperGroup GetModel(string LabCode,int LabSuperGroupNo);


        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.Lab_SuperGroup> DataTableToList(DataTable dt);	
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_SuperGroup model);
        DataSet GetListByLike(ZhiFang.Model.Lab_SuperGroup model);
		#endregion  成员方法

        DataSet GetList(int ListColCount, int p, Model.Lab_SuperGroup lab_SuperGroup);
        DataSet GetParentSuperGroupNolist();
    } 
}