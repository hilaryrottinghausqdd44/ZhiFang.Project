using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BStoredProcedure : ZhiFang.IBLL.ProjectProgressMonitorManage.IBStoredProcedure
    {
        public IDStoredProcedureDao IDStoredProcedureDao { get; set; }
        public EntityList<PReportData> QueryReportData(int templetType, string templetID, string equipID, string empID, string beginDate, string endDate, int checkType, string isCheck)
        {
            EntityList<PReportData> listReportData = new EntityList<PReportData>();
            listReportData.list = IDStoredProcedureDao.QueryReportDataDao(templetType, templetID, equipID, empID, beginDate, endDate, checkType, isCheck);
            if (listReportData.list != null)
                listReportData.count = listReportData.list.Count;
            return listReportData;
        }
    }
}