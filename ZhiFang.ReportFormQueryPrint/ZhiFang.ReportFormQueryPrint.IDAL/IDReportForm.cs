using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDReportForm : IDataBase<Model.ReportForm>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string FormNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string FormNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.ReportForm GetModel(string FormNo);
        /// <summary>
        /// 根据FormNo数组返回ReportForm列表
        /// </summary>
        /// <param name="FormNo">FormNo数组</param>
        /// <returns></returns>
        //DataTable GetReportFormList(string[] FormNo);
        /// <summary>
        /// 根据FormNo数组返回ReportForm列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportFormFullList(string FormNo);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(Model.ReportForm model, DateTime? StartDay, DateTime? EndDay);

        bool UpdatePrintTimes(string[] reportformlist,string uluserCName);

        DataSet GetListByFormNo(string FormNo);
        bool UpdatePageInfo(string reportformlist,string pageCount, string pageName);
        bool UpdateClientPrintTimes(string[] reportformlist);
        DataTable GetReportFormList(string[] formNo);
        DataSet GetReporFormAllByReportFormIdList(List<string> idList, string fields, string strWhere);

        DataSet GetReportFromByReportFormID(List<string> idList, string fields, string strWhere);

        int UpdateClientPrint(string fromno);

        int UpdatePrintTimes(string fromno);

        DataTable GetReportFormListByFormId(string formNo);

        DataSet GetSampleReportFromByReportFormID(List<string> idList, string fields, string strWhere);

        DataSet GetReportFormFullByReportFormID(string ReportFormID);

        int UpdateReportFormFull(ReportFormFull model);

        DataSet GetRepotFormByReportFormIDGroupByZdy15(string PatNo,string zdy15);

        DataSet GetRepotFormByReportFormIDAndZdy15AndReceiveDate(string PatNo, string zdy15,string ReceiveDate);

        DataSet GetListTopByWhereAndSort(int Top, string strWhere, string filedOrder);
    }
}
