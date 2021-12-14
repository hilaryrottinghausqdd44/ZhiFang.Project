using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.Backups;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    /// <summary>
    /// HistoryReportMicro
    /// </summary>
    public partial class BBackupsReportMarrow
    {
        private readonly BackupsReportMarrow dal = DalFactory<BackupsReportMarrow>.GetDal("BackupsReportMarrow", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.Backups");
        internal DataTable GetReportMarrowItemList(string FormNo)
        {
            return dal.GetReportMarrowItemList(FormNo);
        }

        public DataTable GetReportItemList_DataTable(string FormNo)
        {
            return dal.GetReportMarrowFullList(FormNo).Tables[0];
        }
    }
}

