using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
    /// <summary>
    /// 数据访问类Doctor。
    /// </summary>
    public class BSearchSetting : IDBSearchSetting
    {
        public int deleteById(int id ) {
            int i = 0;
            string sql = "delete from B_SearchSetting where stid=" + id;
            ZhiFang.Common.Log.Log.Debug("B_SearchSetting.deleteById sql = " + sql);
            i = DbHelperSQL.ExecuteSql(sql);
            return i;
        }

        public int Add(Model.BSearchSetting t)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (t.JsCode != null)
            {
                strSql1.Append("JsCode,");
                strSql2.Append("'" + t.JsCode.Replace("'", "''") + "',");
            }
            if (t.Type != null)
            {
                strSql1.Append("Type,");
                strSql2.Append("'" + t.Type + "',");
            }
            if (t.SelectName != null)
            {
                strSql1.Append("SelectName ,");
                strSql2.Append("'" + t.SelectName + "',");
            }

            if (t.Xtype != null)
            {
                strSql1.Append("Xtype ,");
                strSql2.Append("'" + t.Xtype + "',");
            }
            if (t.Listeners != null)
            {
                strSql1.Append("Listeners ,");
                strSql2.Append("'" + t.Listeners.Replace("'", "''") + "',");
            }
            if (t.Mark != null)
            {
                strSql1.Append("Mark ,");
                strSql2.Append("'" + t.Mark + "',");
            }
            if (t.SID != null)
            {
                strSql1.Append("SID ,");
                strSql2.Append("'" + t.SID + "',");
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
            if (t.TextWidth != null)
            {
                strSql1.Append("TextWidth,");
                strSql2.Append(t.TextWidth + ",");
            }
            if (t.Width != null)
            {
                strSql1.Append("Width,");
                strSql2.Append(t.Width + ",");
            }
            if (t.ShowOrderNo != null)
            {
                strSql1.Append("ShowOrderNo,");
                strSql2.Append(t.ShowOrderNo + ",");
            }

            if (t.AppType != null)
            {
                strSql1.Append("AppType,");
                strSql2.Append("'" + t.AppType + "',");
            }           
            strSql1.Append("STID,");
            strSql2.Append(Common.GUIDHelp.GetGUIDInt() + ",");
            strSql1.Append("DataAddTime");
            strSql2.Append("'" + DateTime.Now + "'");
            strSql.Append("insert into B_SearchSetting(");
            strSql.Append(strSql1.ToString());
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString());
            strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug("B_SearchSetting.Add sql = " + strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public DataSet GetList(Model.BSearchSetting t)
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

            string sql = "select B_SearchSetting.*,B_SearchUnit.JsCode from B_SearchSetting  inner join B_SearchUnit  on  B_SearchSetting.SID = B_SearchUnit.SID where " + strWhere;
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

            string sql = "select B_SearchSetting.*,B_SearchUnit.JsCode from B_SearchSetting  inner join B_SearchUnit  on  B_SearchSetting.SID = B_SearchUnit.SID where " + strWhere+" order by "+order;
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

        public int Update(Model.BSearchSetting t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE B_SearchSetting SET ");

            builder.Append("DataUpdateTime=" + "'" + DateTime.Now + "'");

            if (t.JsCode != null && !t.JsCode.Equals(""))
            {
                builder.Append(",JsCode=" + "'" + t.JsCode + "'");
            }
            if (t.Type != null && !t.Type.Equals(""))
            {
                builder.Append(",Type=" + "'" + t.Type + "'");
            }
            if (t.TextWidth != null)
            {
                builder.Append(",TextWidth=" + t.TextWidth);
            }
            if (t.Width != null)
            {
                builder.Append(",Width=" + t.Width);
            }
            if (t.ShowOrderNo != null)
            {
                builder.Append(",ShowOrderNo=" + t.ShowOrderNo);
            }
            if (t.SID != null)
            {
                builder.Append(",SID=" + t.SID);
            }
            if (t.SelectName != null && !t.SelectName.Equals(""))
            {
                builder.Append(",SelectName=" + "'" + t.SelectName + "'");
            }
            if (t.Xtype != null && !t.Xtype.Equals(""))
            {
                builder.Append(",Xtype=" + "'" + t.Xtype + "'");
            }
            if (t.Mark != null && !t.Mark.Equals(""))
            {
                builder.Append(",Mark=" + "'" + t.Mark + "'");
            }
            if (t.Listeners != null && !t.Listeners.Equals(""))
            {
                builder.Append(",Listeners=" + "'" + t.Listeners + "'");
            }
            if (t.CName != null && !t.CName.Equals(""))
            {
                builder.Append(",CName=" + "'" + t.CName + "'");
            }
            if (t.ShowName != null && !t.ShowName.Equals(""))
            {
                builder.Append(",ShowName=" + "'" + t.ShowName + "'");
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
            builder.Append(" WHERE STID=" + t.STID);
            ZhiFang.Common.Log.Log.Debug("BSearchSetting.update.sql:"+builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public int deleteByAppType(string appType)
        {
            string sql = "delete from B_SearchSetting where apptype='" + appType + "'";
            ZhiFang.Common.Log.Log.Debug("B_SearchSetting.deleteByAppType sql = " + sql);
            return DbHelperSQL.ExecuteSql(sql);
        }
        public int GetIsSenior(long STID)
        {
            string sql = "select count(*) from B_SearchSetting where STID=" + STID;
            ZhiFang.Common.Log.Log.Debug("GetIsSenior:sql=" + sql);
            return int.Parse(DbHelperSQL.Query(sql).Tables[0].Rows[0][0].ToString());
        }
    }
}

