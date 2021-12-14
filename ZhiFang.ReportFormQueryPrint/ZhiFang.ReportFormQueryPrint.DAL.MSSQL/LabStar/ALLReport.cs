using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data.SqlClient;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.LabStar
{
    public class ALLReport
    {
        DbHelperSQLObj DbHelperSQL = new DbHelperSQLObj(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LabStarConnectionString"));
        
        /// <summary>
        /// 技师站历史对比
        /// </summary>
        /// <param name="Where">查询条件</param>
        /// <returns></returns>
        public DataSet ResultMhistory(string Where) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CheckDate,CheckTime,CName,SectionName,PatNo,ReportFormID,ReceiveDate,GTestDate,GSampleNo,GSampleType ");
            strSql.Append(" FROM TestFormQueryDataSource  ");
            if (Where.Trim() != "")
            {
                strSql.Append(" where " + Where);
            }
            else {
                return new DataSet();
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataTable GetReportItemList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM TestItemQueryDataSource ");
                strSql.Append(" where ReportFormID='" + FormNo + "' ");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportItemFullList:" + ex.ToString());
                return new DataTable();
            }
        }
        public DataTable GetReportItemListByWhere(string where)
        {
            try
            {
                if (string.IsNullOrEmpty(where))
                {
                    ZhiFang.Common.Log.Log.Debug("GetReportItemListByWhere:where条件不能为空");
                    return new DataTable();
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM TestItemQueryDataSource ");
                strSql.Append(" where " + where);
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportItemListByWhere:" + ex.ToString());
                return new DataTable();
            }
        }
        public DataTable GetReportValue(string[] p, string aa)
        {
            try
            {
                if (p.Length >= 4)
                {
                    SqlParameter sp0 = new SqlParameter("@PatNo", SqlDbType.VarChar, 50);
                    SqlParameter sp1 = new SqlParameter("@ItemNo", SqlDbType.VarChar, 50);
                    SqlParameter sp2 = new SqlParameter("@Check", SqlDbType.VarChar, 50);
                    SqlParameter sp3 = new SqlParameter("@Where", SqlDbType.VarChar, 500);
                    //SqlParameter sp4 = new SqlParameter("@EndRd", SqlDbType.VarChar, 50);
                    sp0.Value = p[0];
                    sp1.Value = p[1];
                    sp2.Value = p[2];
                    sp3.Value = p[3];
                    //sp4.Value = p[4];
                    DataSet ds = DbHelperSQL.RunProcedure("GetReportValue", new SqlParameter[] { sp0, sp1, sp2, sp3 }, "ReportForm");

                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                        }
                        //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
                        return ds.Tables[0];
                    }
                    else
                    {
                        return new DataTable();
                    }
                }
                else
                {
                    return new DataTable();
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        public int UpdateTestFormPrintCount(string TestFormID)
        {
            string sql="UPDATE Lis_TestForm SET PrintCount=isnull(printtimes, 0) +1 where TestFormID="+ TestFormID;
            ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.DAL.MSSQL.LabStar.ALLReport.Update.sql:" + sql);
            return DbHelperSQL.ExecuteSql(sql);
        }
    }
}
