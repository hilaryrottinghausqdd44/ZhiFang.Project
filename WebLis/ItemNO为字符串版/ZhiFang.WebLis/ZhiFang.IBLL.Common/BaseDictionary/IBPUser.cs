using System;
using System.Data;
using ZhiFang.IBLL.Common;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBPUser	
	/// </summary>
	public interface IBPUser :IBBase<ZhiFang.Model.PUser>,IBDataPage<ZhiFang.Model.PUser>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int UserNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int UserNo);
				
		int DeleteList(string UserIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.PUser GetModel(int UserNo);
				
		#endregion  成员方法
	} 
}