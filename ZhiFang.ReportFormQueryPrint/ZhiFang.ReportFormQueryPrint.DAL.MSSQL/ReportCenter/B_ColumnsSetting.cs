using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    /// <summary>
    /// 数据访问类Doctor。
    /// </summary>
    public class BColumnsSetting : IDBColumnsSetting
    {

        public int deleteById(long id)
        {
            int i = 0;
            string sql = "delete from B_ColumnsSetting where ctid=" + id;
            ZhiFang.Common.Log.Log.Debug("B_ColumnsSetting.deleteById sql = " + sql);
            i = DbHelperSQL.ExecuteSql(sql);
            return i;
        }

        public int deleteById(int id) {
            int i = 0;
            string sql = "delete from B_ColumnsSetting where ctid=" + id;
            ZhiFang.Common.Log.Log.Debug("B_ColumnsSetting.deleteById sql = " + sql);
            i = DbHelperSQL.ExecuteSql(sql);
            return i;
        }
        public int Add(Model.BColumnsSetting t)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if(t.ColumnName != null)
            {
                strSql1.Append("ColumnName ,");
                strSql2.Append("'" + t.ColumnName + "',");
            }
            if(t.ColID!= null)
            {
                strSql1.Append("ColID ,");
                strSql2.Append("'" + t.ColID + "',");
            }
            if (t.IsShow != null)
            {
                strSql1.Append("IsShow ,");
                strSql2.Append("'" + t.IsShow + "',");
            }
            if (t.CName != null)
            {
                strSql1.Append("CName ,");
                strSql2.Append("'" + t.CName + "',");
            }
            if (t.OrderFlag != null)
            {
                strSql1.Append("OrderFlag,");
                strSql2.Append("'"+t.OrderFlag + "',");
            }
            if (t.OrderMode != null && t.OrderMode.Length > 0)
            {
                strSql1.Append("OrderMode,");
                strSql2.Append("'" + t.OrderMode + "',");
            }
            if (t.OrderDesc != null)
            {
                strSql1.Append("OrderDesc,");
                strSql2.Append(t.OrderDesc + ",");
            }
            if (t.ShowName != null)
            {
                strSql1.Append("ShowName,");
                strSql2.Append("'" + t.ShowName + "',");
            }
            if (t.Site != null)
            {
                strSql1.Append("Site,");
                strSql2.Append("'" + t.Site + "',");
            }
            if (t.Width != null)
            {
                strSql1.Append("Width,");
                strSql2.Append(t.Width + ",");
            }
            if (t.OrderNo != null)
            {
                strSql1.Append("OrderNo,");
                strSql2.Append(t.OrderNo + ",");
            }
            if (t.AppType != null)
            {
                strSql1.Append("AppType,");
                strSql2.Append("'" + t.AppType + "',");
            }
            if (t.IsUse != null)
            {
                strSql1.Append("IsUse ,");
                strSql2.Append("'" + t.IsUse + "',");
            }
            strSql1.Append("CTID,");
            strSql2.Append(GUIDHelp.GetGUIDInt()+",");
            strSql1.Append("DataAddTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into B_ColumnsSetting(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("B_ColumnsSetting.Add sql = " + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public DataSet GetList(Model.BColumnsSetting t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            DataSet ds = new DataSet();
            if (strWhere == null || strWhere.Length < 1)
            {
                ZhiFang.Common.Log.Log.Debug("ReportPrint_GetList:条件不符合规范 strWhere=" + strWhere);
                return ds;
            }

            string sql = "select B_ColumnsSetting.*,B_ColumnsUnit.Render from B_ColumnsSetting inner join B_ColumnsUnit on B_ColumnsSetting.ColID = B_ColumnsUnit.ColID where " + strWhere;
            ZhiFang.Common.Log.Log.Debug("ReportPrint_GetList:sql=" + sql);
            return DbHelperSQL.Query(sql);
        }
        public DataSet GetList(string strWhere,string order)
        {
            DataSet ds = new DataSet();
            if (strWhere == null || strWhere.Length < 1)
            {
                ZhiFang.Common.Log.Log.Debug("ReportPrint_GetList:条件不符合规范 strWhere=" + strWhere);
                return ds;
            }

            string sql = "select B_ColumnsSetting.*,B_ColumnsUnit.Render from B_ColumnsSetting inner join B_ColumnsUnit on B_ColumnsSetting.ColID = B_ColumnsUnit.ColID where " + strWhere;
            sql += " order by " + order; 
            ZhiFang.Common.Log.Log.Debug("ReportPrint_GetList:sql=" + sql);
            return DbHelperSQL.Query(sql);
        }
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Update(Model.BColumnsSetting t)
        {

            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE B_ColumnsSetting SET ");

            builder.Append("DataUpdateTime=" + "'" + DateTime.Now + "'");

            if (t.CName != null && !t.CName.Equals(""))
            {
                builder.Append(",CName=" + "'" + t.CName + "'");
            }
            if (t.ShowName != null && !t.ShowName.Equals(""))
            {
                builder.Append(",ShowName=" + "'" + t.ShowName + "'");
            }
            if (t.Width != null)
            {
                builder.Append(",Width=" + t.Width);
            }
            if (t.OrderNo != null)
            {
                builder.Append(",OrderNo=" + t.OrderNo);
            }
            if (t.OrderFlag != null)
            {
                builder.Append(",OrderFlag=" + "'" + t.OrderFlag + "'");
            }
            if (t.OrderDesc != null)
            {
                builder.Append(",OrderDesc=" + t.OrderDesc);
            }
            if (t.OrderMode != null && !t.OrderMode.Equals(""))
            {
                builder.Append(",OrderMode=" + "'" + t.OrderMode + "'");
            }
            if (t.Site != null && !t.Site.Equals(""))
            {
                builder.Append(",Site=" + "'" + t.Site + "'");
            }
            if (t.AppType != null && !t.AppType.Equals(""))
            {
                builder.Append(",AppType=" + "'" + t.AppType + "'");
            }

            if (t.IsShow != null)
            {
                builder.Append(",IsShow=" + "'" + t.IsShow + "'");
            }
            if (t.ColID != null)
            {
                builder.Append(",ColID=" + t.ColID);
            }
            if (t.ColumnName != null && !t.ColumnName.Equals(""))
            {
                builder.Append(",ColumnName=" + "'" + t.ColumnName + "'");
            }
            builder.Append(" WHERE CTID=" + t.CTID);
            ZhiFang.Common.Log.Log.Debug(builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public int deleteByAppType(string appType)
        {

            string sql = "delete from B_ColumnsSetting where apptype='" + appType + "'";
            ZhiFang.Common.Log.Log.Debug("B_ColumnsSetting.deleteByAppType sql = " + sql);
            return DbHelperSQL.ExecuteSql(sql);
        }
    }
}

