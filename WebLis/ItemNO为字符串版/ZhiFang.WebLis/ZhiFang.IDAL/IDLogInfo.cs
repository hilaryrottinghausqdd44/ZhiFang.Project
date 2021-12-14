using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLogInfo	
	/// </summary>
	public interface IDLogInfo:IDataBase<ZhiFang.Model.LogInfo>,IDataPage<ZhiFang.Model.LogInfo>
	{
		#region  成员方法
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(int Id);
        int DeleteList(string Idlist );				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        ZhiFang.Model.LogInfo GetModel(int Id);
		
		/// <summary>
        /// 根据客户端提交的时间获取DataSet
        /// </summary>
        DataSet GetListByTimeStampe(ZhiFang.Model.LogInfo model);
		#endregion  成员方法
    } 
}