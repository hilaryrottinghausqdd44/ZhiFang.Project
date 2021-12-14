using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BBColumnsUnit
    {
        private readonly IDBColumnsUnit dal = DalFactory<IDBColumnsUnit>.GetDal("BColumnsUnit");
        public int Add(Model.BColumnsUnit t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.BColumnsUnit t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.BColumnsUnit t)
        {
            throw new NotImplementedException();
        }
    }
}

