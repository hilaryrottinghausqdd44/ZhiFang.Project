using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Model.UiModel.WeiXin;

namespace ZhiFang.IBLL.Report
{
    /// <summary>
    /// 接口层ReportFormFull
    /// </summary>
    public interface IBReportFormFull : IBLLBase<Model.ReportFormFull>
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

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.ReportFormFull GetModelByCache(string ReportFormID);
        #endregion  成员方法

        DataSet GetList(string where);
        DataSet GetList(Model.ReportFormFull model, string city);
        DataSet GetColumn(string flag);
        DataSet GetMatchList(Model.ReportFormFull model);
        DataSet GetAllList(ZhiFang.Model.ReportFormFull model);
        bool CheckReportFormCenter(DataSet dsReportFormFull, string DestiOrgID, out string ReturnDescription);
        bool CheckReportFormLab(DataSet dsReportFormFull, string DestiOrgID, out string ReturnDescription);
        DataSet GetColumns();
        DataSet GetBarCode(string DestiOrgID, string BarCodeNo);
        DataSet GetReportFormInfo(List<string> reportFormNo);
        DataSet GetList(int Top, Model.ReportFormFull model, string filedOrder);
        int GetTotalCount(Model.ReportFormFull model);
        int Count(string wherestr);
        List<Model.ReportFormFull> DataTableToList(DataTable dt, string city);
        // DataSet GetList(string where);
        bool UpdatePrintTimesByReportFormID(string ReportFormID);
        int BackUpReportFormFullByWhere(string Strwhere);
        bool UpdateDownLoadState(string ReportFormID);
        ReportForm SearchReportFormResultByReportFormID(string reportFormID);

        DataSet SearchReportFormFull_ReportItem_WeiJiZhi(string strWhere, string orderby, int startIndex, int endIndex);
        int SearchReportFormFull_ReportItem_WeiJiZhi_Count(string wherestr);
    }
}
