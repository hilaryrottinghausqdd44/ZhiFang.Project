using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDSiteOperationRecords : IDataBase<Model.SiteOperationRecords>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string SiteHostName, string SiteIP);
    }
}
