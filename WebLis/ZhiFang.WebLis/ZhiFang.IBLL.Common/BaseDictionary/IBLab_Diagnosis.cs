using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_Diagnosis	
	/// </summary>
	public interface IBLab_Diagnosis :IBBase<ZhiFang.Model.Lab_Diagnosis>,IBDataPage<ZhiFang.Model.Lab_Diagnosis>
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
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_Diagnosis model);
        DataSet GetListByLike(ZhiFang.Model.Lab_Diagnosis model);
		#endregion  成员方法
	} 
}