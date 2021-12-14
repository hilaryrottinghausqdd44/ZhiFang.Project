namespace ZhiFang.IDAL
{
    using System.Data.Common;
    using ZhiFang.Model;
    using System;
    using System.Data;

    public interface IDReportFormMerge
    {
        DataTable GetModelDataFrFormAll(string[] FormNo);

        DataTable GetModelDataFrFormAll(string[] FormNo, string sortsql);
    }
}

