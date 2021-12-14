using System;
using System.Data;
using ZhiFang.IBLL.Common;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBDic_TestType	
	/// </summary>
	public interface IBTestType :IBBase<ZhiFang.Model.TestType>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int TestTypeNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int TestTypeNo);
				
		int DeleteList(string TestTypeIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.TestType GetModel(int TestTypeNo);
		
		/// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.TestType model);
		#endregion  成员方法
	} 
}