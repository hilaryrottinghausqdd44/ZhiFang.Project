using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDChargeType : IDAL.IDataBase<Model.ChargeType>
    {
         DataSet GetChargeType(string Where);
    }
}
