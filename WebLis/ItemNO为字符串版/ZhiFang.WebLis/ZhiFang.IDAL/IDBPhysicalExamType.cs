using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDBPhysicalExamType : IDataBase<Model.BPhysicalExamType>, IDataPage<Model.BPhysicalExamType>
    {
        bool Exists(long id);
        int Delete(long id);
        Model.BPhysicalExamType GetModel(long id);
        DataSet GetList(int Top, string strWhere, string filedOrder);
    }
}
