using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IHisDisp : IDataBase<Model.HisDisp>
    {
        DataSet GetList(Model.HisDisp hisDisp);
    }
}
