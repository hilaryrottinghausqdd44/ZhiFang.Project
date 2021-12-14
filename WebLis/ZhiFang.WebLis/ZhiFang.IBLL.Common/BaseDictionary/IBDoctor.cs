using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDoctor	
	/// </summary>
	public interface IBDoctor :IBBase<ZhiFang.Model.Doctor>,IBDataPage<ZhiFang.Model.Doctor>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DoctorNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int DoctorNo);

        int DeleteList(string DoctorIDlist);
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Doctor GetModel(int DoctorNo);
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.Doctor> DataTableToList(DataTable dt);					
		#endregion  成员方法

        DataSet GetList(int p, Model.Doctor doctor_m);

        int Add(List<ZhiFang.Model.Doctor> modelList);
        int Update(List<Model.Doctor> modelList);
    } 
}