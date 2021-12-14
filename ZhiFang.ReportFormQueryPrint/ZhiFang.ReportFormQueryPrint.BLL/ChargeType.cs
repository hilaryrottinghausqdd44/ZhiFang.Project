using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class ChargeType
    {
        private readonly IDChargeType dal = DalFactory<IDChargeType>.GetDal("ChargeType");

        public DataSet GetChargeType(string Where) {
            return dal.GetChargeType(Where);
        }
    }
}
