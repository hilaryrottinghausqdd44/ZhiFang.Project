using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    public class SiteOperationRecords : IDSiteOperationRecords
    {
        public int Add(Model.SiteOperationRecords t)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (t.LabID != null)
            {
                strSql1.Append("LabID ,");
                strSql2.Append("'" + t.LabID + "',");
            }
            if (t.SiteIP != null)
            {
                strSql1.Append("SiteIP ,");
                strSql2.Append("'" + t.SiteIP + "',");
            }
            if (t.SiteHostName != null)
            {
                strSql1.Append("SiteHostName ,");
                strSql2.Append("'" + t.SiteHostName + "',");
            }
            if (t.ServiceName != null)
            {
                strSql1.Append("ServiceName,");
                strSql2.Append("'" + t.ServiceName + "',");
            }
            if (t.EmpID != null)
            {
                strSql1.Append("EmpID,");
                strSql2.Append("'" + t.EmpID + "',");
            }
            if (t.EmpName != null)
            {
                strSql1.Append("EmpName,");
                strSql2.Append("'" + t.EmpName + "',");
            }

            strSql1.Append("SiteOperationID,");
            strSql2.Append(GUIDHelp.GetGUIDInt() + ",");
            strSql1.Append("DataAddTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into Site_Operation_Records(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("MSSQL66.SiteOperationRecords.Add sql = " + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public bool Exists(string SiteHostName, string SiteIP)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Site_Operation_Records");

            if (!string.IsNullOrWhiteSpace(SiteHostName) && !string.IsNullOrWhiteSpace(SiteIP))
            {
                strSql.Append(" where SiteHostName='" + SiteHostName + "' and SiteIP='" + SiteIP + "'");
            }
            else
            {
                strSql.Append(" where 1=2 ");
            }
            ZhiFang.Common.Log.Log.Debug("MSSQL66.SiteOperationRecords.Exists sql = " + strSql.ToString());
            return DbHelperSQL.Exists(strSql.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.SiteOperationRecords t)
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

        public int Update(Model.SiteOperationRecords t)
        {
            throw new NotImplementedException();
        }
    }
}
