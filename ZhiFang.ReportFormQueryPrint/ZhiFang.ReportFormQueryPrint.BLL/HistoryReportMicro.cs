
using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.History;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    /// <summary>
    /// ҵ���߼���HistoryReportMicro ��ժҪ˵����
    /// </summary>
    public class BHistoryReportMicro
    {
        private readonly HistoryReportMicro dal = DalFactory<HistoryReportMicro>.GetDal("HistoryReportMicro", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.History");
        public DataTable GetReportMicroList(string FormNo)
        {
            return dal.GetReportMicroList(FormNo);
        }
        internal DataTable GetReportMicroGroupListForSTestType(string FormNo)
        {
            return dal.GetReportMicroGroupListForSTestType(FormNo);
        }
        public DataTable GetReportMicroGroupList(string FormNo)
        {
            return dal.GetReportMicroGroupList(FormNo);
        }
    }
}

