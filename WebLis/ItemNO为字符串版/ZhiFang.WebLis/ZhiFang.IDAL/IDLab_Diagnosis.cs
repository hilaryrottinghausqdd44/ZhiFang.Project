using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_Diagnosis	
	/// </summary>
	public interface IDLab_Diagnosis:IDataBase<ZhiFang.Model.Lab_Diagnosis>,IDataPage<ZhiFang.Model.Lab_Diagnosis>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabDiagNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabDiagNo);
		
				int DeleteList(string DiagIDlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_Diagnosis GetModel(string LabCode,int LabDiagNo);
		
		
		DataSet GetListByLike(ZhiFang.Model.Lab_Diagnosis model);
		#endregion  成员方法
	} 
}