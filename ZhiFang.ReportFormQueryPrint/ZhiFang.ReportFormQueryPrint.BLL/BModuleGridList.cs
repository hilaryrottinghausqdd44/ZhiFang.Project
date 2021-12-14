using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BModuleGridList
    {
        private readonly IDBModuleGridList dal = DalFactory<IDBModuleGridList>.GetDal("BModuleGridList");
        public int Add(Model.BModuleGridList t)
        {
            return dal.Add(t);
        }
        public int deleteById(long id)
        {
            return dal.deleteById(id);
        }
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public DataSet GetList(Model.BModuleGridList t)
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

        public int Update(Model.BModuleGridList t)
        {
            return dal.Update(t);
        }
    }
}
