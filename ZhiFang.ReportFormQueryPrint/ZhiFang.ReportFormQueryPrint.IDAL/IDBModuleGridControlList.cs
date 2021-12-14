using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDBModuleGridControlList : IDAL.IDataBase<Model.BModuleGridControlList>
    {
        int deleteById(long id);

        DataSet GetListSort(string strWhere, string sortFields);
    }
}
