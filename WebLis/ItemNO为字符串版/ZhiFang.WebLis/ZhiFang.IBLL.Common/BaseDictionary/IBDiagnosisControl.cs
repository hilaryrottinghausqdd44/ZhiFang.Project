using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDiagnosisControl	
	/// </summary>
	public interface IBDiagnosisControl :IBBase<ZhiFang.Model.DiagnosisControl>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string DiagControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string DiagControlNo);
				
		int DeleteList(string Idlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.DiagnosisControl GetModel(string DiagControlNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.DiagnosisControl model);
		#endregion  成员方法
	} 
}