using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BModuleFormList
    {
        private readonly IDBModuleFormList dal = DalFactory<IDBModuleFormList>.GetDal("BModuleFormList");
        public int Add(Model.BModuleFormList t)
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

        public DataSet GetList(Model.BModuleFormList t)
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

        public int Update(Model.BModuleFormList t)
        {
            return dal.Update(t);
        }
    }
}
