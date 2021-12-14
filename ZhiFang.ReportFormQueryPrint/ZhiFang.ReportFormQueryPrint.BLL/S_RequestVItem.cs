using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BS_RequestVItem
    {
        private readonly IDS_RequestVItem dal = DalFactory<IDS_RequestVItem>.GetDal("S_RequestVItem");
        public DataSet GetListByReportPublicationID(string ReportPublicationID)
        {
            return dal.GetListByReportPublicationID(ReportPublicationID);
        }

    }
}

