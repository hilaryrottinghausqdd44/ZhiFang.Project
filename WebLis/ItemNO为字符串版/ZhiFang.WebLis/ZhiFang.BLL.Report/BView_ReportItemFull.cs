using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Report;
using System.Data;

namespace ZhiFang.BLL.Report
{
    public class BView_ReportItemFull : IBView_ReportItemFull
    {
        IDAL.IDView_ReportItemFull dal = DALFactory.DalFactory<IDAL.IDView_ReportItemFull>.GetDalByClassName("View_ReportItemFull");

        public DataSet GetViewItemFull(Model.VIEW_ReportItemFull model)
        {
            return dal.GetViewItemFull(model);
        }
        public DataSet GetViewItemFull(string reportformid)
        {
            return dal.GetViewItemFull(reportformid);
        }

    }
}
