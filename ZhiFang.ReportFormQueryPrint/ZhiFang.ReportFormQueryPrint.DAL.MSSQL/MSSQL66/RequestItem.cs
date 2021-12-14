using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// 数据访问类ReportItem。
	/// </summary>
    public class RequestItem : IDRequestItem
	{
        DbHelperSQLObj DbHelperSQL = new DbHelperSQLObj(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));
        public RequestItem()
		{}
        #region  成员方法
        public int Add(Model.RequestItem t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.RequestItem t)
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

        public int Update(Model.RequestItem t)
        {
            throw new NotImplementedException();
        }
        #endregion
        public DataTable GetRequestItemFullList(string FormNo)
        {

            try
            {
                #region 执行拼接脚本
                /*
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT     dbo.ReportForm.FormNo, TestItem_2.CName AS TestItemName, TestItem_2.StandardCode AS TestItemSName, dbo.ReportForm.ReceiveDate, ");
                strSql.Append("dbo.ReportForm.SectionNo, dbo.ReportForm.TestTypeNo, dbo.ReportForm.SampleNo, dbo.ReportItem.ParItemNo, dbo.ReportItem.ItemNo,  ");
                strSql.Append("dbo.ReportItem.OriginalValue, dbo.ReportItem.ReportValue, dbo.ReportItem.OriginalDesc, dbo.ReportItem.ReportDesc, dbo.ReportItem.StatusNo,  ");
                strSql.Append("dbo.ReportItem.EquipNo, dbo.ReportItem.Modified, dbo.ReportItem.RefRange, dbo.ReportItem.ItemDate, dbo.ReportItem.ItemTime,  ");
                strSql.Append("dbo.ReportItem.IsMatch, dbo.ReportItem.ResultStatus, CONVERT(varchar(10), dbo.ReportItem.ItemDate, 120) + ' ' + CONVERT(varchar(8),  ");
                strSql.Append("dbo.ReportItem.ItemTime, 114) AS TestItemDateTime, ISNULL(dbo.ReportItem.ReportDesc, '') + ISNULL(CONVERT(VARCHAR(50),  ");
                strSql.Append("dbo.ReportItem.ReportValue), '') AS ReportValueAll, TestItem_1.CName AS ParItemName, TestItem_1.ShortName AS ParItemSName,  ");
                strSql.Append("TestItem_2.DispOrder, TestItem_2.DispOrder AS ItemOrder, TestItem_2.Unit, dbo.ReportForm.SerialNo, dbo.ReportForm.zdy1,  ");
                strSql.Append("dbo.ReportForm.zdy2 AS OldSerialNlo, dbo.ReportForm.zdy3, dbo.ReportForm.zdy5, dbo.ReportForm.zdy4, TestItem_2.OrderNo AS HisOrderNo,  ");
                strSql.Append("dbo.ReportForm.Technician, dbo.ReportForm.Checker, CONVERT(varchar(10), dbo.ReportForm.CheckDate, 120) + ' ' + CONVERT(varchar(8),  ");
                strSql.Append("dbo.ReportForm.CheckTime, 114) AS checkdatetime, dbo.ReportForm.OldSerialNo AS zdy2 ");
                strSql.Append("FROM         dbo.ReportItem INNER JOIN ");
                strSql.Append("dbo.ReportForm ON dbo.ReportItem.FormNo = dbo.ReportForm.FormNo LEFT OUTER JOIN ");
                strSql.Append("dbo.TestItem AS TestItem_1 ON dbo.ReportItem.ParItemNo = TestItem_1.ItemNo LEFT OUTER JOIN ");
                strSql.Append("dbo.TestItem AS TestItem_2 ON dbo.ReportItem.ItemNo = TestItem_2.ItemNo ");
                strSql.Append("  where ReportItem.FormNo=" + FormNo + "  ");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportItem.xml");
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
                */
                #endregion
                #region 执行存储过程
                //string[] p = FormNo.Split(';');
                //if (p.Length >= 4)
                //{

                //    SqlParameter sp = new SqlParameter("@ReceiveDate", SqlDbType.VarChar, 10);
                //    SqlParameter sp1 = new SqlParameter("@SectionNo", SqlDbType.VarChar, 50);
                //    SqlParameter sp2 = new SqlParameter("@TestTypeNo", SqlDbType.VarChar, 50);
                //    SqlParameter sp3 = new SqlParameter("@SampleNo", SqlDbType.VarChar, 50);
                //    sp.Value = p[0];
                //    sp1.Value = p[1];
                //    sp2.Value = p[2];
                //    sp3.Value = p[3];
                //    DataSet ds = DbHelperSQL.RunProcedure("GetReportItemFullList", new SqlParameter[] { sp,sp1,sp2,sp3 }, "ReportItemFull");
                //    if (ds.Tables.Count > 0)
                //    {
                //        ds.Tables[0].Columns.Add("DISPLAYID", typeof(string));
                //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //        {
                //            ds.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
                //        }
                //        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                //        {
                //            ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                //        }
                //        //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
                //        return ds.Tables[0];
                //    }
                //    else
                //    {
                //        return new DataTable();
                //    }
                //}
                //else
                //{
                //    return new DataTable();
                //}
                #endregion

                string[] p = FormNo.Split(';');

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM RequestItemQueryDataSource ");
                if (p.Length >= 4)
                {
                    strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "'");
                }
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
                    //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportItem.xml");
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

        public DataTable GetRequestItemFullListByReportTemp(string FormNo)
        {
            try
            {
                #region 执行拼接脚本
                /*
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT     dbo.ReportForm.FormNo, TestItem_2.CName AS TestItemName, TestItem_2.StandardCode AS TestItemSName, dbo.ReportForm.ReceiveDate, ");
                strSql.Append("dbo.ReportForm.SectionNo, dbo.ReportForm.TestTypeNo, dbo.ReportForm.SampleNo, dbo.ReportItem.ParItemNo, dbo.ReportItem.ItemNo,  ");
                strSql.Append("dbo.ReportItem.OriginalValue, dbo.ReportItem.ReportValue, dbo.ReportItem.OriginalDesc, dbo.ReportItem.ReportDesc, dbo.ReportItem.StatusNo,  ");
                strSql.Append("dbo.ReportItem.EquipNo, dbo.ReportItem.Modified, dbo.ReportItem.RefRange, dbo.ReportItem.ItemDate, dbo.ReportItem.ItemTime,  ");
                strSql.Append("dbo.ReportItem.IsMatch, dbo.ReportItem.ResultStatus, CONVERT(varchar(10), dbo.ReportItem.ItemDate, 120) + ' ' + CONVERT(varchar(8),  ");
                strSql.Append("dbo.ReportItem.ItemTime, 114) AS TestItemDateTime, ISNULL(dbo.ReportItem.ReportDesc, '') + ISNULL(CONVERT(VARCHAR(50),  ");
                strSql.Append("dbo.ReportItem.ReportValue), '') AS ReportValueAll, TestItem_1.CName AS ParItemName, TestItem_1.ShortName AS ParItemSName,  ");
                strSql.Append("TestItem_2.DispOrder, TestItem_2.DispOrder AS ItemOrder, TestItem_2.Unit, dbo.ReportForm.SerialNo, dbo.ReportForm.zdy1,  ");
                strSql.Append("dbo.ReportForm.zdy2 AS OldSerialNlo, dbo.ReportForm.zdy3, dbo.ReportForm.zdy5, dbo.ReportForm.zdy4, TestItem_2.OrderNo AS HisOrderNo,  ");
                strSql.Append("dbo.ReportForm.Technician, dbo.ReportForm.Checker, CONVERT(varchar(10), dbo.ReportForm.CheckDate, 120) + ' ' + CONVERT(varchar(8),  ");
                strSql.Append("dbo.ReportForm.CheckTime, 114) AS checkdatetime, dbo.ReportForm.OldSerialNo AS zdy2 ");
                strSql.Append("FROM         dbo.ReportItem INNER JOIN ");
                strSql.Append("dbo.ReportForm ON dbo.ReportItem.FormNo = dbo.ReportForm.FormNo LEFT OUTER JOIN ");
                strSql.Append("dbo.TestItem AS TestItem_1 ON dbo.ReportItem.ParItemNo = TestItem_1.ItemNo LEFT OUTER JOIN ");
                strSql.Append("dbo.TestItem AS TestItem_2 ON dbo.ReportItem.ItemNo = TestItem_2.ItemNo ");
                strSql.Append("  where ReportItem.FormNo=" + FormNo + "  ");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportItem.xml");
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
                */
                #endregion
                #region 执行存储过程
                //string[] p = FormNo.Split(';');
                //if (p.Length >= 4)
                //{

                //    SqlParameter sp = new SqlParameter("@ReceiveDate", SqlDbType.VarChar, 10);
                //    SqlParameter sp1 = new SqlParameter("@SectionNo", SqlDbType.VarChar, 50);
                //    SqlParameter sp2 = new SqlParameter("@TestTypeNo", SqlDbType.VarChar, 50);
                //    SqlParameter sp3 = new SqlParameter("@SampleNo", SqlDbType.VarChar, 50);
                //    sp.Value = p[0];
                //    sp1.Value = p[1];
                //    sp2.Value = p[2];
                //    sp3.Value = p[3];
                //    DataSet ds = DbHelperSQL.RunProcedure("GetReportItemFullList", new SqlParameter[] { sp,sp1,sp2,sp3 }, "ReportItemFull");
                //    if (ds.Tables.Count > 0)
                //    {
                //        ds.Tables[0].Columns.Add("DISPLAYID", typeof(string));
                //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //        {
                //            ds.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
                //        }
                //        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                //        {
                //            ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                //        }
                //        //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
                //        return ds.Tables[0];
                //    }
                //    else
                //    {
                //        return new DataTable();
                //    }
                //}
                //else
                //{
                //    return new DataTable();
                //}
                #endregion

                string[] p = FormNo.Split(';');

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM RequestItemQueryDataSource ");
                if (p.Length >= 4)
                {
                    strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' and ReportTemp = 1 ");
                }
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
                    //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportItem.xml");
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
    }
}

