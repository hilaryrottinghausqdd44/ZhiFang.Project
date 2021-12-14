using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDBModuleModuleFormGridLink : IDataBase<Model.BModuleModuleFormGridLink>
    {
        int deleteById(long id);
    }
}
