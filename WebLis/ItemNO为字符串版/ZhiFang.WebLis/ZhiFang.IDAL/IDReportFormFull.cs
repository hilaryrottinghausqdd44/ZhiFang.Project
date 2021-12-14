using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层ReportFormFull
	/// </summary>
    public interface IDReportFormFull : IDataBase<ZhiFang.Model.ReportFormFull>
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string ReportFormID);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string ReportFormID);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.ReportFormFull GetModel(string ReportFormID);
		#endregion  成员方法

        DataSet GetColumnByView();

        DataSet GetColumnByTable();
        DataSet GetMatchList(ZhiFang.Model.ReportFormFull model);
        DataSet GetAllList(ZhiFang.Model.ReportFormFull model);
        int InsertSql(string sql);
        int DeleteByWhere(string where);
        DataSet GetColumns();
        DataSet GetBarCode(string DestiOrgID, string BarCodeNo);
        DataSet GetReportFormInfo(List<string> reportFormNo);
        DataSet GetList(string where);
        DataSet GetList(Model.ReportFormFull model,string city);
        DataSet GetList(int Top, Model.ReportFormFull model, string filedOrder);
        int GetTotalCount(Model.ReportFormFull model);
        int Count(string wherestr);
        bool UpdatePrintTimesByReportFormID(string ReportFormID);
        int BackUpReportFormFullByWhere(string Strwhere);
        bool UpdateDownLoadState(string ReportFormID);
    } 
}
