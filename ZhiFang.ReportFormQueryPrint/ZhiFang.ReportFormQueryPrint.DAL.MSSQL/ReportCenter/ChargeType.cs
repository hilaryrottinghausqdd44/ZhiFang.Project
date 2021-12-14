using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    public class ChargeType : IDChargeType
    {
        public int Add(Model.ChargeType t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetChargeType(string Where)
        {
            StringBuilder strSql = new StringBuilder();

            if (Where != null && Where.Trim() != "")
            {
                Where = " where 1=1 and " + Where;
            }
            strSql.Append("SELECT * FROM ChargeType " + Where);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(Model.ChargeType t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.ChargeType t)
        {
            throw new NotImplementedException();
        }
    }
}
