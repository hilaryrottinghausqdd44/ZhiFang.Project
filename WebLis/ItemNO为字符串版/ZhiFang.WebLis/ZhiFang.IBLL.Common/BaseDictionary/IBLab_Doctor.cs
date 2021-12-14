using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_Doctor	
	/// </summary>
	public interface IBLab_Doctor :IBBase<ZhiFang.Model.Lab_Doctor>,IBDataPage<ZhiFang.Model.Lab_Doctor>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabDoctorNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabDoctorNo);
				
		int DeleteList(string DoctorIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_Doctor GetModel(string LabCode,int LabDoctorNo);

        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.Lab_Doctor> DataTableToList(DataTable dt);	


		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_Doctor model);
        DataSet GetListByLike(ZhiFang.Model.Lab_Doctor model);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        #endregion  成员方法
    } 
}