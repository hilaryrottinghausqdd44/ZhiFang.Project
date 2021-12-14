using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_BUSINESSMAN	
	/// </summary>
	public interface IBLab_BUSINESSMAN :IBBase<ZhiFang.Model.Lab_BUSINESSMAN>,IBDataPage<ZhiFang.Model.Lab_BUSINESSMAN>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabBMANNO);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabBMANNO);
				
		int DeleteList(string BNANIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_BUSINESSMAN GetModel(string LabCode,int LabBMANNO);
		
        DataSet GetListByLike(ZhiFang.Model.Lab_BUSINESSMAN model);
		#endregion  成员方法
	} 
}