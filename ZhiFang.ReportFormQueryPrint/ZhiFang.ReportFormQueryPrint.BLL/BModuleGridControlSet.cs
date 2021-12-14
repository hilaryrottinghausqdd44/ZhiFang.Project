using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BModuleGridControlSet
    {
        private readonly IDBModuleGridControlSet dal = DalFactory<IDBModuleGridControlSet>.GetDal("BModuleGridControlSet");
        public int Add(Model.BModuleGridControlSet t)
        {
            return dal.Add(t);
        }

        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public DataSet GetList(Model.BModuleGridControlSet t)
        {
            return dal.GetList(t);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public int Update(Model.BModuleGridControlSet t)
        {
            return dal.Update(t);
        }
        public int deleteById(long id)
        {
            return dal.deleteById(id);
        }

        public DataSet GetListSort(string strWhere,string sortFields)
        {
            return dal.GetListSort(strWhere, sortFields);
        }
    }
}
