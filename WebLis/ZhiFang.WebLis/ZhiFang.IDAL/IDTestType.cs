using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDDic_TestType	
	/// </summary>
	public interface IDTestType:IDataBase<ZhiFang.Model.TestType>
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
		
		
		#endregion  成员方法
	} 
}