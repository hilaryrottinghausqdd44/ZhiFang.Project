using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
namespace ZhiFang.ReportFormQueryPrint.BLL
{
	/// <summary>
	/// 业务逻辑类ReportMicro 的摘要说明。
	/// </summary>
    public class BRequestMicro 
	{
        private readonly IDRequestMicro dal = DalFactory<IDRequestMicro>.GetDal("RequestMicro");
        public DataTable GetRequestMicroList(string FormNo)
        {
            return dal.GetRequestMicroList(FormNo);
        }
        internal DataTable GetRequestMicroGroupListForSTestType(string FormNo)
        {
            return dal.GetRequestMicroGroupListForSTestType(FormNo);
        }
        public DataTable GetRequestMicroGroupList(string FormNo)
        {
            return dal.GetRequestMicroGroupList(FormNo);
        }
    }
}

