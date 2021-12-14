using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDistrict	
	/// </summary>
	public interface IBDistrict :IBBase<ZhiFang.Model.District>,IBDataPage<ZhiFang.Model.District>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DistrictNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int DistrictNo);

        int DeleteList(string DistrictIDlist);
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.District GetModel(int DistrictNo);
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.District> DataTableToList(DataTable dt);
		#endregion  成员方法

        DataSet GetList(int p, Model.District d);
    } 
}