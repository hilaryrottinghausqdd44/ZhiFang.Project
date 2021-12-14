using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层IRFGraphData 的摘要说明。
	/// </summary>
	public interface IDRFGraphData:IDAL.IDataBase<Model.RFGraphData>
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

        DataSet GetListByReportFormId(string ReportFormId);

		/// <summary>
		/// 参数isNeedPDF是否过滤pointType为8的数据，true为不过滤，false为过滤
		/// </summary>
		DataSet GetListByReportPublicationID(string ReportPublicationID);

		/// <summary>
		/// 参数PointType为要过滤指定类型的数据，格式1,2,3
		/// </summary>
		DataSet GetListByReportPublicationIDAndPointType(string ReportPublicationID, string PointType);
		
	}
}
