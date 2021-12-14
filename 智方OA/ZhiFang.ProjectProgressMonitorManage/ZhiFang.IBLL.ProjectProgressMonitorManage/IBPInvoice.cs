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
    public  interface IBPInvoice : IBGenericManager<PInvoice>
	{
        BaseResultDataValue PInvoiceAdd(Entity.Base.SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction);

        BaseResultBool PInvoiceStatusUpdate(Entity.Base.SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long EmpID, string EmpName);
        EntityList<PInvoice> SearchPInvoiceByExportType(long ExportType, long empid, string where, string sort, int page, int limit);
        EntityList<PInvoice> SearchPInvoiceByExportType(long ExportType, long empid,string where, int page, int limit);
        BaseResultDataValue ExcelToPdfFile(long id, bool isPreview, string templetName, ref string fileName);
    }
}