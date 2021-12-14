using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_ChargeType	
	/// </summary>
	public interface IDLab_ChargeType:IDataBase<ZhiFang.Model.Lab_ChargeType>,IDataPage<ZhiFang.Model.Lab_ChargeType>
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
		
		
		DataSet GetListByLike(ZhiFang.Model.Lab_ChargeType model);
		#endregion  成员方法
	} 
}