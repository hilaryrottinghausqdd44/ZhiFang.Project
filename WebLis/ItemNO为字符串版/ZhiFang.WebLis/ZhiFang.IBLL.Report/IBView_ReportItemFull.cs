using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBView_ReportItemFull
    {
        DataSet GetViewItemFull(ZhiFang.Model.VIEW_ReportItemFull model);
        DataSet GetViewItemFull(string reportformid);
    }
}
