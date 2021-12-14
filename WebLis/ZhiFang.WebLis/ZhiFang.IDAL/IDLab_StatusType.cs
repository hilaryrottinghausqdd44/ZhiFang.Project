using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_StatusType	
	/// </summary>
	public interface IDLab_StatusType:IDataBase<ZhiFang.Model.Lab_StatusType>,IDataPage<ZhiFang.Model.Lab_StatusType>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabStatusNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabStatusNo);
		
				int DeleteList(string StatusIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_StatusType GetModel(string LabCode,int LabStatusNo);
		
		
		DataSet GetListByLike(ZhiFang.Model.Lab_StatusType model);
		#endregion  成员方法
	} 
}