using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBPGroupControl	
	/// </summary>
	public interface IBPGroupControl :IBBase<ZhiFang.Model.PGroupControl>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string SectionControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string SectionControlNo);
				
		int DeleteList(string Idlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.PGroupControl GetModel(string SectionControlNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.PGroupControl model);


        List<ZhiFang.Model.PGroup> ControlDataTableToList(DataTable dt, int ControlLabNo);
		#endregion  成员方法

        #region 字典对照
        DataSet GetListByPage(ZhiFang.Model.PGroupControl model, int nowPageNum, int nowPageSize);

        DataSet B_lab_GetListByPage(ZhiFang.Model.PGroupControl model, int nowPageNum, int nowPageSize);

        List<ZhiFang.Model.PGroupControl> DataTableToList(DataTable dt);
        #endregion
	} 
}