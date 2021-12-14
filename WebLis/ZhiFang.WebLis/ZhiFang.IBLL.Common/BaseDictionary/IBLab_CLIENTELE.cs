using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_CLIENTELE	
	/// </summary>
	public interface IBLab_CLIENTELE :IBBase<ZhiFang.Model.Lab_CLIENTELE>,IBDataPage<ZhiFang.Model.Lab_CLIENTELE>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabClIENTNO);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabClIENTNO);
				
		int DeleteList(string ClIENTIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_CLIENTELE GetModel(string LabCode,int LabClIENTNO);
		
	
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_CLIENTELE model);
        DataSet GetListByLike(ZhiFang.Model.Lab_CLIENTELE model);
		#endregion  成员方法
	} 
}