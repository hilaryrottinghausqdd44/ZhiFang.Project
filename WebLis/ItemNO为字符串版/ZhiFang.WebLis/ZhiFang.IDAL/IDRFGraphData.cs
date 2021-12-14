using System;
using System.Data;
namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层IRFGraphData 的摘要说明。
	/// </summary>
    public interface IDRFGraphData : IDAL.IDataBase<ZhiFang.Model.RFGraphData>, IDataPage<ZhiFang.Model.RFGraphData>
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(string GraphName, int GraphNo, string FormNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(string GraphName, int GraphNo, string FormNo);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Model.RFGraphData GetModel(string GraphName, int GraphNo, string FormNo);
		#endregion  成员方法

        int DeleteList(string GraphIDlist);
    }
}
