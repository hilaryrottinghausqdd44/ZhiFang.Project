using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    /// <summary>
    /// 业务逻辑类BEmpDeptLinks 的摘要说明。
    /// </summary>
    public class BEmpDeptLinks
    {
        private readonly IDEmpDeptLinks dal = DalFactory<IDEmpDeptLinks>.GetDal("EmpDeptLinks");

        public DataSet GetEmpDeptLinks(string Where) {
            return dal.GetList(Where);
        }
        public int Add(Model.EmpDeptLinks entity)
        {
            return dal.Add(entity);
        }
        public int Delete(long EDLID)
        {
            return dal.Delete(EDLID);
        }
    }
}

