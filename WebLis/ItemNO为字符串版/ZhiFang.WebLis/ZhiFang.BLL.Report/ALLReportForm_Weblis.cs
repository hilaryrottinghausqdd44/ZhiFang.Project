using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using System.Data;

namespace ZhiFang.BLL.Report
{
    class ALLReportForm_Weblis : ZhiFang.IBLL.Report.IBALLReportForm
    {        
        #region IBALLReportForm 成员

        public System.Data.DataTable GetFromInfo(string FormNo)
        {
            IDReportFormFull dal = DalFactory<IDReportFormFull>.GetDalByClassName("ReportFormFull");
            Model.ReportFormFull rff_m=new Model.ReportFormFull();
            rff_m.ReportFormID=FormNo;
            return dal.GetList(rff_m).Tables[0];
        }

        public System.Data.DataTable GetFromItemList(string FormNo)
        {
            IDReportItemFull dal = DalFactory<IDReportItemFull>.GetDalByClassName("ReportItemFull");
            Model.ReportItemFull rfi_m = new Model.ReportItemFull();
            rfi_m.ReportFormID = FormNo;
            return dal.GetList(rfi_m).Tables[0];
        }

        public System.Data.DataTable GetFromPGroupInfo(int SectionNo)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetFromGraphList(string FormNo)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 微生物表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroInfo(string FormNo)
        {
            IDReportFormFull dal = DalFactory<IDReportFormFull>.GetDalByClassName("ReportFormFull");
            Model.ReportFormFull rff_m = new Model.ReportFormFull();
            rff_m.ReportFormID = FormNo;
            return dal.GetList(rff_m).Tables[0];
        }
       
        #endregion
    }
}
