using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IBLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBStoredProcedure
    {
        EntityList<PReportData> QueryReportData(int templetType, string templetID, string equipID, string empID, string beginDate, string endDate, int checkType, string otherPara);
    }
}