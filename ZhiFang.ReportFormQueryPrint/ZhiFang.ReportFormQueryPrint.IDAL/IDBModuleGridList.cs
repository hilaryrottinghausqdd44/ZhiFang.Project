using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDBModuleGridList : IDAL.IDataBase<Model.BModuleGridList>
    {
        int deleteById(long id);
    }
}
