using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_StatusType	
	/// </summary>
	public interface IBLab_StatusType :IBBase<ZhiFang.Model.Lab_StatusType>,IBDataPage<ZhiFang.Model.Lab_StatusType>
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
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_StatusType model);
        DataSet GetListByLike(ZhiFang.Model.Lab_StatusType model);
		#endregion  成员方法
	} 
}