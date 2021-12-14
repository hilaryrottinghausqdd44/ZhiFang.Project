using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDBModuleFormList : IDAL.IDataBase<Model.BModuleFormList>
    {
        int deleteById(long id);
    }
}
