using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDBModuleFormControlList : IDAL.IDataBase<Model.BModuleFormControlList>
    {
        int deleteById(long id);

        DataSet GetListSort(string strWhere, string sortFields);
    }
}
