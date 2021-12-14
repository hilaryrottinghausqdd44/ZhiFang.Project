using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBShowReport
    {
        DataTable GetReportFormAndItemData(string fromNo, int sectiontype, out DataTable dsrf_Out);
        string GetTemplatePath(DataTable dtrf, DataTable dtri, string pageName, int showType, int sectiontype);
        string GetResult(DataTable dtrf, DataTable dtri, string templatePath);
    }
}
