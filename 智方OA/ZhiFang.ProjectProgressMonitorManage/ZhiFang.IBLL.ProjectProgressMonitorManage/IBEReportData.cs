using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBEReportData : IBGenericManager<EReportData>
    {
        EntityList<PReportData> QueryCheckReportData(int templetType, string templetID, string equipID, string employeeID, string beginDate, string endDate, int checkType, string otherPara);

        BaseResultDataValue QueryCheckPdfPath(long reportDataID);

        BaseResultDataValue AddEReportData(long templetID, DateTime reportDate, string reportFilePath, int isCheck, string checker, string checkView);

        BaseResultDataValue QueryReportIsChecked(long templetID, DateTime reportDate);

        EntityList<EReportData> QueryEReportDataByHQL(string where, string sort, int page, int limit);

        BaseResultDataValue EditEReportDataCheckState(long reportID, string cancelCheckerID, string cancelChecker, string cancelCheckView);
    }
}