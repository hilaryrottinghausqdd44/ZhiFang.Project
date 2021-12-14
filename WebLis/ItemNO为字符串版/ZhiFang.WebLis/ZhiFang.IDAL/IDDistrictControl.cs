using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDDistrictControl	
	/// </summary>
	public interface IDDistrictControl:IDataBase<ZhiFang.Model.DistrictControl>
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
	
		
		#endregion  成员方法
        #region 字典对照
        DataSet GetListByPage(ZhiFang.Model.DistrictControl model, int nowPageNum, int nowPageSize);

        DataSet B_lab_GetListByPage(ZhiFang.Model.DistrictControl model, int nowPageNum, int nowPageSize);
        #endregion
	} 
}