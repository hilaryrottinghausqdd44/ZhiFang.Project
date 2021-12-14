using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDic_GenderType	
	/// </summary>
    public interface IBGenderType : IBBase<ZhiFang.Model.GenderType>, IBDataPage<ZhiFang.Model.GenderType>
	{
		#region  成员方法	
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int GenderNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int GenderNo);
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.GenderType> DataTableToList(DataTable dt);	
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.GenderType GetModel(int GenderNo);
        DataSet GetListLike(ZhiFang.Model.GenderType model);
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