using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBLab_ChargeType	
	/// </summary>
	public interface IBLab_ChargeType :IBBase<ZhiFang.Model.Lab_ChargeType>,IBDataPage<ZhiFang.Model.Lab_ChargeType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode,int LabChargeNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string LabCode,int LabChargeNo);
				
		int DeleteList(string ChargeIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.Lab_ChargeType GetModel(string LabCode,int LabChargeNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.Lab_ChargeType model);
        DataSet GetListByLike(ZhiFang.Model.Lab_ChargeType model);
		#endregion  成员方法
	} 
}