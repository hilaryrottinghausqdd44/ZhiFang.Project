using System;
using System.Data;
using ZhiFang.IBLL.Common;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDiagnosis	
	/// </summary>
	public interface IBDiagnosis :IBBase<ZhiFang.Model.Diagnosis>,IBDataPage<ZhiFang.Model.Diagnosis>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DiagNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int DiagNo);

        int DeleteList(string DiagIDlist);
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Diagnosis GetModel(int DiagNo);
				
		#endregion  成员方法
	} 
}