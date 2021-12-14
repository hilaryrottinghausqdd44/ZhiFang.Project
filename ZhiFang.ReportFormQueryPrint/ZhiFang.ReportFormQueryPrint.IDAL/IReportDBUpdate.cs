using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;


namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public interface IReportDBUpdate
    {
        int UpdatedReportDB(string reserved);
    }
}
