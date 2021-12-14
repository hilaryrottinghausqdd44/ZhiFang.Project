using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_Doctor	
	/// </summary>
	public interface IDLab_Doctor:IDataBase<ZhiFang.Model.Lab_Doctor>,IDataPage<ZhiFang.Model.Lab_Doctor>
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
		
		
		DataSet GetListByLike(ZhiFang.Model.Lab_Doctor model);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        #endregion  成员方法
    } 
}