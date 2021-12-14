using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDBModuleFormControlSet : IDAL.IDataBase<Model.BModuleFormControlSet>
    {
        int deleteById(long id);
    }
}
