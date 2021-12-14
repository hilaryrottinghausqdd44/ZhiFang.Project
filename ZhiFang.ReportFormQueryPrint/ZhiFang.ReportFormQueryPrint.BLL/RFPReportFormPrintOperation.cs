using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BRFPReportFormPrintOperation
    {
        private readonly IDRFPReportFormPrintOperation dal = DalFactory<IDRFPReportFormPrintOperation>.GetDal("RFPReportFormPrintOperation");
        public BRFPReportFormPrintOperation()
        { }
        #region  成员方法
        public int Add(Model.RFPReportFormPrintOperation t) {
            return dal.Add(t);
        }
        
        
        
        #endregion  成员方法
    }
}
