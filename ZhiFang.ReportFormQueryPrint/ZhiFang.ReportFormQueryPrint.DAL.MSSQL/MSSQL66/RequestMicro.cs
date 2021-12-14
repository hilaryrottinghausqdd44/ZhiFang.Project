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
    /// 数据访问类RequestMicro。
    /// </summary>
    public class RequestMicro : IDRequestMicro
    {
        DbHelperSQLObj DbHelperSQL = new DbHelperSQLObj(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));
        public RequestMicro()
		{}

        public int Add(Model.RequestMicro t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.RequestMicro t)
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

        public int Update(Model.RequestMicro t)
        {
            throw new NotImplementedException();
        }
        #region IDRequestMicro 成员

        public DataTable GetRequestMicroList(string FormNo)
        {
            if (FormNo == null || FormNo == "") return new DataTable();
            try
            {
                string[] p = FormNo.Split(';');
                string receivedate = p[0];
                string sectionno = p[1];
                string testtypeno = p[2];
                string sampleno = p[3];
                var strSql = "select * from RequestMicroQueryDataSource where ReceiveDate='" + receivedate + "' and SectionNo=" + sectionno + " and TestTypeNo=" + testtypeno + " and SampleNo='" + sampleno + "' ";
                //if (p.Length >= 4)
                //{
                //    strSql.Append("select MI.*,TI.cname itemcname,TI.ename itemename from (select DISTINCT b.ItemNo,Convert(varchar(10),b.ReceiveDate,21) as ReceiveDate,b.SectionNo,b.TestTypeNo,b.SampleNo,b.itemdesc  from  reportmicro b where b.ReceiveDate='" + p[0] + "' and b.SectionNo=" + p[1] + " and b.TestTypeNo=" + p[2] + " and b.SampleNo='" + p[3] + "' ) MI  left join TestItem TI on(TI.itemNo=MI.itemNo)  WHERE MI.ReceiveDate='" + p[0] + "' and MI.SectionNo=" + p[1] + " and MI.TestTypeNo=" + p[2] + " and MI.SampleNo='" + p[3] + "' ");
                //}
                //else
                //{
                //    strSql.Append("select MI.*,TI.cname itemcname,TI.ename itemename from (select DISTINCT b.ItemNo,Convert(varchar(10),b.ReceiveDate,21) as ReceiveDate,b.SectionNo,b.TestTypeNo,b.SampleNo,b.itemdesc  from  reportmicro b where b.ReceiveDate='" + p[0] + "' and b.SectionNo=" + p[1] + " and b.TestTypeNo=" + p[2] + " and b.SampleNo='" + p[3] + "' ) MI  left join TestItem TI on(TI.itemNo=MI.itemNo)  WHERE 1=2 ");
                //}
                ZhiFang.Common.Log.Log.Debug("MSSQL6.6.RequestMicro.GetRequestMicroList:sql=" + strSql.ToString());
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return new DataTable();
            }
        }

        public DataTable GetRequestMicroGroupListForSTestType(string FormNo)
        {
            throw new NotImplementedException();
        }
        public DataTable GetRequestMicroGroupList(string FormNo)
        {
            try
            {
                #region 执行存储过程
                string[] p = FormNo.Split(';');
                if (p.Length >= 4)
                {

                    SqlParameter sp = new SqlParameter("@ReceiveDate", SqlDbType.VarChar, 10);
                    SqlParameter sp1 = new SqlParameter("@SectionNo", SqlDbType.VarChar, 50);
                    SqlParameter sp2 = new SqlParameter("@TestTypeNo", SqlDbType.VarChar, 50);
                    SqlParameter sp3 = new SqlParameter("@SampleNo", SqlDbType.VarChar, 50);
                    sp.Value = p[0];
                    sp1.Value = p[1];
                    sp2.Value = p[2];
                    sp3.Value = p[3];
                    DataSet ds = DbHelperSQL.RunProcedure("GetRequestMicroGroupFullList", new SqlParameter[] { sp, sp1, sp2, sp3 }, "ReportMicFull");
                    ZhiFang.Common.Log.Log.Debug(ds.GetXml() + "@" + DbHelperSQL.connectionString);
                    if (ds.Tables.Count > 0)
                    {
                        ds.Tables[0].Columns.Add("DISPLAYID", typeof(string));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
                        }
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
                #endregion
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return new DataTable();
            }
        }
        #endregion
    }
}

