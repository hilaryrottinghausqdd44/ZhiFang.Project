using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBWardType	
    /// </summary>
    public interface IBWardType : IBBase<ZhiFang.Model.WardType>, IBDataPage<ZhiFang.Model.WardType>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DistrictNo, int WardNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int DistrictNo, int WardNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.WardType GetModel(int DistrictNo, int WardNo);
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.WardType> DataTableToList(DataTable dt);					
        #endregion  成员方法

        DataSet GetList(int p, Model.WardType wt);
    }
}