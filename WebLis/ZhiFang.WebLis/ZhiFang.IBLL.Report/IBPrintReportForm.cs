using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.Report
{
    public interface IBPrintReportForm
    {
        List<string> PrintReportFormHtml(List<string> reportformidlist, ZhiFang.Common.Dictionary.ReportFormTitle rft, ZhiFang.Common.Dictionary.ReportFormFileType rfct);
    }
}
