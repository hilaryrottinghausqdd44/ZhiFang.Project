namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    using System.Data.Common;
    using ZhiFang.ReportFormQueryPrint.Model;
    using System;
    using System.Data;

    public interface IDReportFormMerge
    {
        DataTable GetModelDataFrFormAll(string[] FormNo);
    }
}

