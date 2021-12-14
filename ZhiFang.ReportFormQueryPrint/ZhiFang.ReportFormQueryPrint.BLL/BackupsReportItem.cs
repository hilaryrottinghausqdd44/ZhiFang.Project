using System.Data;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.Backups;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BBackupsReportItem
    {
        private readonly BackupsReportItem dal = new BackupsReportItem();

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
