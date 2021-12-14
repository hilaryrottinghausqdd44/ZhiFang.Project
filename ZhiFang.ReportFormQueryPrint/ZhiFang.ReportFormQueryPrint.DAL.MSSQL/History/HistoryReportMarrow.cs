using System;
using System.Data;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.History
{
	/// <summary>
	/// 数据访问类ReportMarrow。
	/// </summary>
    public class HistoryReportMarrow
    {
        DbHelperSQLObj DbHelperSQL = new DbHelperSQLObj(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("HistoryConnectionString"));
      

        public int Add(Model.ReportMarrow t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.ReportMarrow t)
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

        public int Update(Model.ReportMarrow t)
        {
            throw new NotImplementedException();
        }
        public DataTable GetReportMarrowItemList(string FormNo)
        {
            #region 执行存储过程
            //SqlParameter sp = new SqlParameter("@FormNo", SqlDbType.VarChar, 50);
            //sp.Value = FormNo;
            //DataSet ds = DbHelperSQL.RunProcedure("GetReportMarrowItemFullList", new SqlParameter[] { sp }, "ReportMarrowItemFull");
            //if (ds.Tables.Count > 0)
            //{
            //    ds.Tables[0].Columns.Add("DISPLAYID", typeof(string));
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        ds.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
            //    }
            //    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            //    {
            //        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
            //    }
            //    //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
            //    return ds.Tables[0];
            //}
            //else
            //{
            //    return new DataTable();
            //}
            #endregion

            if (FormNo == null || FormNo == "") return new DataTable();

            try
            {
                #region 执行视图
                ZhiFang.Common.Log.Log.Debug("MSSQL.History.GetReportMarrowItemList:" + FormNo);
                string[] p = FormNo.Split(';');
                string receivedate = p[0];
                string sectionno = p[1];
                string testtypeno = p[2];
                string sampleno = p[3];
                var sql = "select * from ReportMarrowQueryDataSource where ReceiveDate='" + receivedate + "' and SectionNo=" + sectionno + " and TestTypeNo=" + testtypeno + " and SampleNo='" + sampleno + "' ";
                DataSet ds = DbHelperSQL.Query(sql);
                ZhiFang.Common.Log.Log.Debug("MSSQL.History.GetReportMarrowItemList:sql=" + sql);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
                #endregion
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("MSSQL.History.GetReportMarrowItemList:" + e);
                return new DataTable();
            }
        }
        public DataSet GetReportMarrowFullList(string FormNo)
        {

            if (FormNo == null || FormNo == "") return new DataSet();

            try
            {
                #region 执行视图
                ZhiFang.Common.Log.Log.Debug("MSSQL.History.GetReportMarrowFullList:" + FormNo);
                string[] p = FormNo.Split(';');
                string receivedate = p[0];
                string sectionno = p[1];
                string testtypeno = p[2];
                string sampleno = p[3];
                var sql = "select * from ReportMarrowQueryDataSource where ReceiveDate='" + receivedate + "' and SectionNo=" + sectionno + " and TestTypeNo=" + testtypeno + " and SampleNo='" + sampleno + "' ";
                DataSet ds = DbHelperSQL.Query(sql);
                ZhiFang.Common.Log.Log.Debug("MSSQL.History.GetReportMarrowFullList:sql=" + sql);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    return ds;
                }
                else
                {
                    return new DataSet();
                }
                #endregion
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("MSSQL.History.GetReportMarrowFullList:" + e);
                return new DataSet();
            }
        }
    }
}

