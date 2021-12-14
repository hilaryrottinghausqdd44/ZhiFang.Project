using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDistrictControl	
	/// </summary>
	public interface IBDistrictControl :IBBase<ZhiFang.Model.DistrictControl>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string DistrictControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string DistrictControlNo);
				
		int DeleteList(string Idlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.DistrictControl GetModel(string DistrictControlNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.DistrictControl model);


        List<ZhiFang.Model.District> ControlDataTableToList(DataTable dt, int ControlLabNo);
		#endregion  成员方法

        #region 字典对照
        DataSet GetListByPage(ZhiFang.Model.DistrictControl model, int nowPageNum, int nowPageSize);

        DataSet B_lab_GetListByPage(ZhiFang.Model.DistrictControl model, int nowPageNum, int nowPageSize);

        List<ZhiFang.Model.DistrictControl> DataTableToList(DataTable dt);
        #endregion
	} 
}