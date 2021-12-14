
using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.Backups;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    /// <summary>
    /// ҵ���߼���BackupsReportMicro ��ժҪ˵����
    /// </summary>
    public class BBackupsReportMicro
    {
        private readonly BackupsReportMicro dal = DalFactory<BackupsReportMicro>.GetDal("BackupsReportMicro", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.Backups");
        
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

