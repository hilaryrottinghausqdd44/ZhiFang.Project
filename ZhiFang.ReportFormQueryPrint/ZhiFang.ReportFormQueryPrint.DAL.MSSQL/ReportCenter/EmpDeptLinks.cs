using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    /// <summary>
    /// 数据访问类EmpDeptLinks。
    /// </summary>
    public class EmpDeptLinks : IDEmpDeptLinks
    {
        public EmpDeptLinks()
        { }

        public int Add(Model.EmpDeptLinks model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql1.Append("EDLID,");
            strSql2.Append("" + Common.GUIDHelp.GetGUIDInt() + ",");
            strSql1.Append("DataAddTime,");
            strSql2.Append("'" + DateTime.Now + "',");
            if (model.UserNo != null)
            {
                strSql1.Append("UserNo,");
                strSql2.Append("'" + model.UserNo + "',");
            }
            if (model.DeptNo != null)
            {
                strSql1.Append("DeptNo,");
                strSql2.Append("'" + model.DeptNo + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.UserCName != null)
            {
                strSql1.Append("UserCName,");
                strSql2.Append("'" + model.UserCName + "',");
            }
            if (model.DeptCName != null)
            {
                strSql1.Append("DeptCName,");
                strSql2.Append("'" + model.DeptCName + "',");
            }            
            strSql.Append("insert into EmpDeptLinks(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("EmpDeptLinksAdd:"+strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public int Delete(long EDLID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM EmpDeptLinks WHERE EDLID = " + EDLID);
            ZhiFang.Common.Log.Log.Debug("EmpDeptLinksDelect:" + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public DataSet GetList(Model.EmpDeptLinks t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM EmpDeptLinks ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.EmpDeptLinks t)
        {
            throw new NotImplementedException();
        }
    }
}

