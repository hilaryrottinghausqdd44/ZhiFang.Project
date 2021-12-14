using System;
using System.Data;
using ZhiFang.IBLL.Common;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBRFGraphData	
	/// </summary>
	public interface IBRFGraphData :IBBase<ZhiFang.Model.RFGraphData>,IBDataPage<ZhiFang.Model.RFGraphData>
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string GraphName,int GraphNo,string FormNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(string GraphName, int GraphNo, string FormNo);
				
		int DeleteList(string GraphIDlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
        ZhiFang.Model.RFGraphData GetModel(string GraphName, int GraphNo, string FormNo);
				
		#endregion  成员方法
	} 
}