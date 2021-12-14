using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL.Other;
using ZhiFang.DALFactory;
using System.Data;

namespace ZhiFang.BLL.Report.Other
{

    public class BView : ZhiFang.IBLL.Report.Other.IBView
    {
        private readonly IDView dal = DalFactory<IDView>.GetDalByClassName("Other.DView");

        public DataSet GetViewData(int Top, string ViewName, string strWhere, string strOrder)
        {
            return dal.GetViewData(Top, ViewName, strWhere, strOrder);
        }
        public DataSet GetViewData_Revise(int Top, string ViewName, string strWhere, string strOrder)
        {
            return dal.GetViewData_Revise(Top, ViewName, strWhere, strOrder);
        }
        public DataSet GetReportValue(string[] p)
        {
            return dal.GetReportValue(p);
        }
    }
}
