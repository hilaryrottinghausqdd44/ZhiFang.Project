using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.History;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BHistoryReportItem
    {
        private readonly HistoryReportItem dal = new HistoryReportItem();

        public DataTable GetReportItemList_DataTable(string FormNo)
        {
            return dal.GetReportItemFullList(FormNo);
        }

        public DataTable GetReportItemList_DataTableByReportTemp(string FormNo)
        {
            return dal.GetReportItemFullListByReportTemp(FormNo);
        }

    }
}
