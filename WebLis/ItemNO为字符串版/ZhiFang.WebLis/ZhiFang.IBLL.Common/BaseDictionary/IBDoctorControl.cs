using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDoctorControl	
	/// </summary>
	public interface IBDoctorControl :IBBase<ZhiFang.Model.DoctorControl>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string DoctorControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string DoctorControlNo);
				
		int DeleteList(string Idlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.DoctorControl GetModel(string DoctorControlNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.DoctorControl model);

        List<ZhiFang.Model.Doctor> ControlDataTableToList(DataTable dt, int ControlLabNo);
		#endregion  成员方法

        #region 字典对照
        DataSet GetListByPage(ZhiFang.Model.DoctorControl model, int nowPageNum, int nowPageSize);

        DataSet B_lab_GetListByPage(ZhiFang.Model.DoctorControl model, int nowPageNum, int nowPageSize);

        List<ZhiFang.Model.DoctorControl> DataTableToList(DataTable dt);
        #endregion
	} 
}