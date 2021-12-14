using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBPLoanBill : IBGenericManager<PLoanBill>
    {
        BaseResultDataValue PLoanBillAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction);
        BaseResultBool PLoanBillStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long empID, string empName);
        EntityList<PLoanBill> SearchPLoanBillByExportType(long ExportType, long empid, string where, string sort, int page, int limit);
        EntityList<PLoanBill> SearchPLoanBillByExportType(long ExportType, long empid, string where, int page, int limit);
        BaseResultDataValue ExcelToPdfFile(long id, bool isPreview,string templetName,ref string fileName);
    }
}