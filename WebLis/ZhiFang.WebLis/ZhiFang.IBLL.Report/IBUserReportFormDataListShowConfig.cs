using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBUserReportFormDataListShowConfig
    {
        SortedList<string, string[]> ShowReportFormListHeadName(string PageName, int Sort);
        SortedList<string, string[]> ShowReportFormListHeadName(string PageName);
        DataSet ShowClassList(string ConfigSetName);
        int ShowReportFormListPageSize(string PageName, int Sort);
        string ShowReportFormListOrderColumn(string PageName, int Sort);
        DataSet ShowFormTypeList(string ConfigSetName);
    }
}
