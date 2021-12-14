using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_GenderType	
	/// </summary>
	public interface IBLab_GenderType :IBBase<ZhiFang.Model.Lab_GenderType>,IBDataPage<ZhiFang.Model.Lab_GenderType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabGenderNo);
        bool Exists(System.Collections.Hashtable ht);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabGenderNo);
				
		int DeleteList(string GenderIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_GenderType GetModel(string LabCode,int LabGenderNo);


        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.Lab_GenderType> DataTableToList(DataTable dt);
        DataSet GetListByLike(ZhiFang.Model.Lab_GenderType model);
        
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
		#endregion  成员方法
	} 
}