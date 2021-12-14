using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDDoctorControl	
	/// </summary>
	public interface IDDoctorControl:IDataBase<ZhiFang.Model.DoctorControl>
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
	
		
		#endregion  成员方法

        #region 字典对照
        DataSet GetListByPage(ZhiFang.Model.DoctorControl model, int nowPageNum, int nowPageSize);

        DataSet B_lab_GetListByPage(ZhiFang.Model.DoctorControl model, int nowPageNum, int nowPageSize);
        #endregion
	} 
}