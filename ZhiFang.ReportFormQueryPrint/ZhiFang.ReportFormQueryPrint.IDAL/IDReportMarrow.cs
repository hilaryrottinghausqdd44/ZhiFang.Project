using System;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层ReportMarrow
	/// </summary>
    public interface IDReportMarrow : IDataBase<Model.ReportMarrow>
	{
		#region  成员方法		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(string ItemNo, string FormNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(string ItemNo, string FormNo);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Model.ReportMarrow GetModel(string ItemNo, string FormNo);
		#endregion  成员方法

        DataTable GetReportMarrowItemList(string FormNo);

        DataSet GetReportMarrowFullList(string p);

        DataSet GetReportMarrowFullByReportFormID(string reportformid);

        int UpdateReportMarrowFull(ReportMarrowFull model);
    } 
}
