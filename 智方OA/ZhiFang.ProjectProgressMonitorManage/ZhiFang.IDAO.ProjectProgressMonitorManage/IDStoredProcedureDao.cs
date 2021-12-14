using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.ProjectProgressMonitorManage
{
	public interface IDStoredProcedureDao
	{
        IList<PReportData> QueryReportDataDao(int templetType, string templetID, string equipID, string empID, string beginDate, string endDate, int checkType, string isCheck);

    } 
}