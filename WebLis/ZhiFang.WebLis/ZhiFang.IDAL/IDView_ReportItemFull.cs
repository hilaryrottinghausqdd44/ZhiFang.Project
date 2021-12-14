using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDView_ReportItemFull
    {
        DataSet GetViewItemFull(ZhiFang.Model.VIEW_ReportItemFull model);
        DataSet GetViewItemFull(string reportformid);
    }
}
