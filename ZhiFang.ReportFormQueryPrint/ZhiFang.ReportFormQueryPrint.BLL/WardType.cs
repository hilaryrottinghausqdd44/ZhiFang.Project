using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BWardType
    {
        private readonly IDWardType dal = DalFactory<IDWardType>.GetDal("WardType");
        public BWardType()
        { }
        #region  成员方法

        public DataSet GetWardType(string strWhere) {
           DataSet aa =  dal.GetList(strWhere);
            return aa;
        }
        
        #endregion  成员方法
    }
}
