using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.WebLisService.WL.IBLL
{
    public interface IBUpLoadReportForm
    {
        int UpLoadReportForm(string reportform_xmlstr, out string err);
    }
}
