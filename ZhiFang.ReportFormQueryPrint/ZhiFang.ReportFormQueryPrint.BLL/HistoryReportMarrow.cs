using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.History;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    /// <summary>
    /// HistoryReportMicro
    /// </summary>
    public partial class BHistoryReportMarrow
    {
        private readonly HistoryReportMarrow dal = DalFactory<HistoryReportMarrow>.GetDal("HistoryReportMarrow", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.History");
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

