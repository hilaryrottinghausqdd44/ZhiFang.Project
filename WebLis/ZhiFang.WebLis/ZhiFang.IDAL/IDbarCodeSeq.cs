using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDbarCodeSeq : IDataBase<Model.barCodeSeq>, IDataPage<Model.barCodeSeq>
    {
        //是否存在记录
        bool Exists(string LabCode, DateTime date);

        //得到一个对象实体
        DataSet GetList(ZhiFang.Model.barCodeSeq model);

        int UpdateByDataRow(DataRow dr);

        int AddByDataRow(DataRow dr);
        string ExecStoredProcedure(string LabCode, string Operdate);
    }
}
