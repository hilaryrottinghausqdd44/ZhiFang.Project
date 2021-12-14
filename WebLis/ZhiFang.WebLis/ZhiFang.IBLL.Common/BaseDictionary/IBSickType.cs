using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
using ZhiFang.Model;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBSickType	
	/// </summary>
	public interface IBSickType :IBBase<ZhiFang.Model.SickType>,IBDataPage<ZhiFang.Model.SickType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int SickTypeNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int SickTypeNo);
				
		int DeleteList(string SickTypeIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.SickType GetModel(int SickTypeNo);
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.SickType> DataTableToList(DataTable dt);
				
		#endregion  成员方法

        int Add(List<ZhiFang.Model.SickType> modelList);
        int Update(List<Model.SickType> modelList);
        BaseResult CopyToLabByItemNoList(List<string> itemNos, List<string> labCodeNo);
    } 
}