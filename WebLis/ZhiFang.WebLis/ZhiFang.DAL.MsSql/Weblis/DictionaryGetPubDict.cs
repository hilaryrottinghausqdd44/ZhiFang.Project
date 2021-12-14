using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public partial class DictionaryGetPubDict : IDDictionaryGetPubDict
    {
        public DBUtility.IDBConnection DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        public DictionaryGetPubDict(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DictionaryGetPubDict()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        public DataSet DictionaryGet(string tablename, string fields, string labcode, string filervalue, string precisequery,int page,int rows)
        {
            StringBuilder strSql = new StringBuilder();
            if (page > 0 && rows > 0)
            {
                strSql.Append("select top "+rows);
                if (!string.IsNullOrEmpty(fields))
                    strSql.Append(" " + fields + " ");
                else
                    strSql.Append(" " + precisequery + " ");
                strSql.Append(" FROM " + tablename);
                strSql.Append(" where 1=1  ");
                if (precisequery != "" && precisequery != null)
                    strSql.Append(" and " + precisequery + " = '" + filervalue + "' ");
                else
                {
                    if (filervalue != "" && filervalue != null)
                    {
                        if (tablename != "clientelearea")
                            strSql.Append(" and CNAME like '%" + filervalue + "%' or  SHORTCODE like '%" + filervalue + "%' ");
                        else
                            strSql.Append(" and AreaCName like '%" + filervalue + "%' or  SHORTCODE like '%" + filervalue + "%' ");
                    }

                }
                if (labcode != "" && labcode != null)
                    strSql.Append(" and labcode ='" + labcode + "' ");
                strSql.Append(" and ClIENTNO not in(select top " + (page-1)*rows + " ClIENTNO from CLIENTELE order by ClIENTNO) ");
                strSql.Append(" order by ");
                strSql.Append(fields != null && fields != "" ? fields : precisequery);
            }
            else {
                strSql.Append("select ");
                if (!string.IsNullOrEmpty(fields))
                    strSql.Append(" " + fields + " ");
                else
                    strSql.Append(" " + precisequery + " ");
                strSql.Append(" FROM " + tablename);
                strSql.Append(" where 1=1  ");
                if (precisequery != "" && precisequery != null)
                    strSql.Append(" and " + precisequery + " = '" + filervalue + "' ");
                else
                {
                    if (filervalue != "" && filervalue != null)
                    {
                        if (tablename != "clientelearea")
                            strSql.Append(" and CNAME like '%" + filervalue + "%' or  SHORTCODE like '%" + filervalue + "%' ");
                        else
                            strSql.Append(" and AreaCName like '%" + filervalue + "%' or  SHORTCODE like '%" + filervalue + "%' ");
                    }

                }
                if (labcode != "" && labcode != null)
                    strSql.Append(" and labcode ='" + labcode + "' ");
                strSql.Append(" order by ");
                strSql.Append(fields != null && fields != "" ? fields : precisequery);
            }
            ZhiFang.Common.Log.Log.Info("SQL:" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetTotalCount(string tablename) {
            int total = 0;
            DataSet ds = new DataSet();
            string strSql = "select count(*) from " + tablename;
            ZhiFang.Common.Log.Log.Info("统计表" + tablename + "总数" + strSql);
            ds=DbHelperSQL.ExecuteDataSet(strSql);
            if (ds != null && ds.Tables[0].Rows.Count > 0) {
                total = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            return total;
        }
    }
}
