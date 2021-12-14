using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDRequestItem : IDataBase<Model.RequestItem>
    {
        /// <summary>
        /// 根据FormNo返回ReportForm包含的RequestItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetRequestItemFullList(string FormNo); 

       DataTable GetRequestItemFullListByReportTemp(string FormNo);
    }
}
