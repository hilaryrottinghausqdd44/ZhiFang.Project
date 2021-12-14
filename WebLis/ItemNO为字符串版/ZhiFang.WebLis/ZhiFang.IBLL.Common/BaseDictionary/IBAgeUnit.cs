using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDic_AgeUnit	
	/// </summary>
	public interface IBAgeUnit :IBBase<ZhiFang.Model.AgeUnit>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int AgeUnitNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int AgeUnitNo);
				
		int DeleteList(string AgeUnitIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.AgeUnit GetModel(int AgeUnitNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.AgeUnit model);
        List<ZhiFang.Model.AgeUnit> DataTableToList(DataTable dt);
		#endregion  成员方法
	} 
}