using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BRequestItem 
    {
        private readonly IDRequestItem dal = DalFactory<IDRequestItem>.GetDal("RequestItem");

        public DataTable GetRequestItemList_DataTable(string FormNo)
        {
            return dal.GetRequestItemFullList(FormNo);
        }

        public DataTable GetRequestItemList_DataTableByReportTemp(string FormNo)
        {
            return dal.GetRequestItemFullListByReportTemp(FormNo);
        }

    }
}
