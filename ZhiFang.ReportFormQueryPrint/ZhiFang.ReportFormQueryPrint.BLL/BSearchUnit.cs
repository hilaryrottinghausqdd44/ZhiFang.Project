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
    public class BBSearchUnit
    {
        private readonly IDBSearchUnit dal = DalFactory<IDBSearchUnit>.GetDal("BSearchUnit");
        public int Add(Model.BSearchUnit t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.BSearchUnit t)
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

        public int Update(Model.BSearchUnit t)
        {
            throw new NotImplementedException();
        }
    }
}

