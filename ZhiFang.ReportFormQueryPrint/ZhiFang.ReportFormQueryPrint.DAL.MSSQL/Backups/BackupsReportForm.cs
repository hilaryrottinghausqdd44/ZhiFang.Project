using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.Backups
{
    /// <summary>
    /// 数据访问类ReportForm。
    /// </summary>
    public class BackupsReportForm
    {
        DbHelperSQLObj DbHelperSQL = new DbHelperSQLObj(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("BackupsConnectionString"));
     
        public int Add(Model.ReportForm t)
        {
            throw new NotImplementedException();
        }
        public DataSet GetList(Model.ReportForm t)
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

        public int Update(Model.ReportForm t)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 查询数据量
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetCountFormFull(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as CountNo ");
            strSql.Append(" FROM ReportFormQueryDataSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            ZhiFang.Common.Log.Log.Debug("GetCountFormFull:"+strSql.ToString());
            var counto = DbHelperSQL.GetSingle(strSql.ToString());
            int count = 0;
            if (counto != null)
            {
                count = int.Parse(counto.ToString());
            }
            return count;

        }
        /// <summary>
        /// 查询报告单
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList_FormFull(string fields, string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            fields = fields.Replace("ReportFormID", " FormNo as ReportFormID");
            if (!string.IsNullOrEmpty(fields))
            {
                strSql.Append("select " + fields + " ");
            }
            else
            {
                strSql.Append("select * ");
            }
            strSql.Append(" FROM ReportFormQueryDataSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            ZhiFang.Common.Log.Log.Debug("GetList_FormFull.strSql:" + strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataTable GetReportFormFullList(string FormNo)
        {
            if (FormNo == null || FormNo == "") return new DataTable();

            try
            {
                #region 执行拼接脚本
                /*
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT     a.FormNo, a.ReceiveDate, a.SectionNo, a.TestTypeNo, a.SampleNo, a.StatusNo, a.SampleTypeNo, a.PatNo, a.CName, a.GenderNo, a.Birthday, a.Age, ");
                strSql.Append("a.AgeUnitNo, a.FolkNo, a.DistrictNo, a.WardNo, a.Bed, a.DeptNo, a.Doctor, a.ChargeNo, a.Charge, a.Collecter, a.CollectDate, a.CollectTime, ");
                strSql.Append("a.FormMemo, a.Technician, a.TestDate, a.TestTime, a.Operator, a.OperDate, a.OperTime, a.Checker, a.CheckDate, a.CheckTime, a.SerialNo, ");
                strSql.Append("a.RequestSource, a.DiagNo, a.PrintTimes, a.SickTypeNo, a.FormComment, a.zdy1, a.zdy2, a.zdy3, a.zdy4, a.zdy5, a.inceptdate, a.incepttime, ");
                strSql.Append("a.incepter, a.onlinedate, a.onlinetime, a.bmanno, a.clientno, a.chargeflag, a.resultprinttimes, a.paritemname, a.clientprint, a.resultsend, a.reportsend, ");
                strSql.Append("a.CountNodesFormSource, a.commsendflag, a.PrintDateTime, a.PrintOper, a.BarCode,");
                strSql.Append("(SELECT     CName");
                strSql.Append("  FROM          dbo.AgeUnit");
                strSql.Append("  WHERE      (AgeUnitNo = a.AgeUnitNo)) AS AgeUnitName,");
                strSql.Append("(SELECT     CName");
                strSql.Append("  FROM          dbo.GenderType");
                strSql.Append("   WHERE      (GenderNo = a.GenderNo)) AS GenderName,");
                strSql.Append("(SELECT     CName");
                strSql.Append("    FROM          dbo.Department");
                strSql.Append("    WHERE      (DeptNo = a.DeptNo)) AS DeptName, a.Doctor AS DoctorName,");
                strSql.Append("  (SELECT     CName");
                strSql.Append("     FROM          dbo.District");
                strSql.Append("     WHERE      (DistrictNo = a.DistrictNo)) AS DistrictName,");
                strSql.Append("   (SELECT     CName");
                strSql.Append("      FROM          dbo.WardType");
                strSql.Append("       WHERE      (WardNo = a.WardNo)) AS WardName,");
                strSql.Append("     (SELECT     CName");
                strSql.Append("      FROM          dbo.FolkType");
                strSql.Append("        WHERE      (FolkNo = a.FolkNo)) AS FolkName,");
                strSql.Append("    (SELECT     CName");
                strSql.Append("       FROM          dbo.SickType");
                strSql.Append("       WHERE      (SickTypeNo = a.SickTypeNo)) AS SickTypeName,");
                strSql.Append("     (SELECT     CName");
                strSql.Append("       FROM          dbo.SampleType");
                strSql.Append("       WHERE      (SampleTypeNo = a.SampleTypeNo)) AS SampleTypeName,");
                strSql.Append("     (SELECT     ParaValue");
                strSql.Append("      FROM          dbo.SysPara");
                strSql.Append("       WHERE      (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) + '_' + LTRIM(RTRIM(CONVERT(char, a.FormNo))) ");
                strSql.Append(" + '_' + LTRIM(RTRIM(CONVERT(char, a.SectionNo))) + '_' + LTRIM(RTRIM(CONVERT(char, a.TestTypeNo))) + '_' + LTRIM(RTRIM(CONVERT(char, ");
                strSql.Append("  a.SampleNo))) + '_' + LTRIM(RTRIM(CONVERT(varchar(20), a.ReceiveDate, 20))) AS ReportFormID,");
                strSql.Append("       (SELECT     ParaValue");
                strSql.Append("          FROM          dbo.SysPara AS SysPara_2");
                strSql.Append("          WHERE      (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) AS CheckNo,");
                strSql.Append("        (SELECT     ParaName");
                strSql.Append("          FROM          dbo.SysPara AS SysPara_1");
                strSql.Append("          WHERE      (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) AS CheckName, dbo.CLIENTELE.CNAME AS ClientName");
                strSql.Append(" FROM         dbo.ReportForm AS a INNER JOIN");
                strSql.Append("  dbo.CLIENTELE ON a.clientno = dbo.CLIENTELE.ClIENTNO");
                strSql.Append("  where a.FormNo="+FormNo);
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
                //    DataSet ds = DbHelperSQL.RunProcedure("GetReportFormFullList", new SqlParameter[] { sp, sp1, sp2, sp3 }, "ReportFormFull");

                //    if (ds.Tables.Count > 0)
                //    {
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

                #region 执行视图
                ZhiFang.Common.Log.Log.Debug("MSSQL.Backups.GetReportFormFullList:" + FormNo);
                string[] p = FormNo.Split(';');
                string receivedate = p[0];
                string sectionno = p[1];
                string testtypeno = p[2];
                string sampleno = p[3];
                var sql = "select * from ReportFormQueryDataSource where ReceiveDate='" + receivedate + "' and SectionNo=" + sectionno + " and TestTypeNo=" + testtypeno + " and SampleNo='" + sampleno + "' ";
                DataSet ds = DbHelperSQL.Query(sql);
                ZhiFang.Common.Log.Log.Debug("MSSQL.Backups.GetReportFormFullList:sql=" + sql);
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
                ZhiFang.Common.Log.Log.Debug("MSSQL.Backups.GetReportFormFullList:" + e);
                return new DataTable();
            }
        }
        public bool UpdatePageInfo(string reportformlist, string pageCount, string pageName)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update ReportForm set PageCount='" + pageCount + "' , pageName='" + pageName + "'   where  ");
                string[] aaa = reportformlist.Split(';');
                strSql.Append(" ( Receivedate='" + aaa[0] + "' and SectionNo='" + aaa[1] + "' and TestTypeNo='" + aaa[2] + "' and SampleNo='" + aaa[3] + "') or ");
                strSql.Append(" 1=2 ");
                //Hashtable ht=new Hashtable();
                //ht.Add(strSql.ToString(),new );
                ZhiFang.Common.Log.Log.Debug(strSql.ToString());
                if (DbHelperSQL.ExecuteSql(strSql.ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return false;
            }
        }

        public bool UpdatePrintTimes(string[] reportformlist)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update ReportForm set printtimes=printtimes+1 , PRINTDATETIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'         where  ");
                foreach (string a in reportformlist)
                {
                    string[] aaa = a.Split(';');
                    strSql.Append(" ( Receivedate='" + aaa[0] + "' and SectionNo='" + aaa[1] + "' and TestTypeNo='" + aaa[2] + "' and SampleNo='" + aaa[3] + "') or ");
                }
                strSql.Append(" 1=2 ");
                //Hashtable ht=new Hashtable();
                //ht.Add(strSql.ToString(),new );
                ZhiFang.Common.Log.Log.Debug("增加打印次数: sql = " + strSql.ToString());
                if (DbHelperSQL.ExecuteSql(strSql.ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("UpdatePrintTimes.异常：" + e.ToString());
                return false;
            }
        }
        public bool UpdateClientPrintTimes(string[] reportformlist)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update ReportForm set ClientPrint=isnull(ClientPrint, 0) +1 , PRINTDATETIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'         where  ");
                foreach (string a in reportformlist)
                {
                    string[] tmp = a.Split(';');
                    strSql.Append(" ( Receivedate='" + tmp[0] + "' and SectionNo='" + tmp[1] + "' and TestTypeNo='" + tmp[2] + "' and SampleNo='" + tmp[3] + "') or ");
                }
                strSql.Append(" 1=2 ");
                //Hashtable ht=new Hashtable();
                //ht.Add(strSql.ToString(),new );
                ZhiFang.Common.Log.Log.Debug(strSql.ToString());
                if (DbHelperSQL.ExecuteSql(strSql.ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("UpdateClientPrintTimes.异常：" + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="PatNo"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public DataSet ResultMhistory(string ReportFormID, string PatNo, string Where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CheckDate,CheckTime,CName,SectionName,PatNo,FormNo as ReportFormID,ReceiveDate ");
            string[] p = ReportFormID.Split(';');
            string receivedate = p[0];
            string sectionno = p[1];
            string testtypeno = p[2];
            string sampleno = p[3];
            strSql.Append(" from ReportFormQueryDataSource where ReceiveDate='" + receivedate + "' and SectionNo=" + sectionno + " and TestTypeNo=" + testtypeno + " and SampleNo='" + sampleno + "' ");
            //strSql.Append(" FROM ReportFormQueryDataSource Where 1=1 ");
            //if (!ReportFormID.Equals("") && ReportFormID != null)
            //{
            //    strSql.Append("and ReportFormID='" + ReportFormID + "'");
            //}
            if (!"".Equals(PatNo) && null != PatNo)
            {
                strSql.Append("and PatNo='" + PatNo + "'");
            }
            if (Where.Trim() != "")
            {
                strSql.Append(" and " + Where);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据病历号和日期查询报告单
        /// </summary>
        /// <param name="PatNo"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public DataSet ResultDataTimeMhistory(string PatNo, string Where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportFormQueryDataSource Where 1=1 ");
            if (PatNo.Trim() != "")
            {
                strSql.Append("and PatNo='" + PatNo + "'");
            }
            if (Where.Trim() != "")
            {
                strSql.Append(" and " + Where);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetReportFromByReportFormID(List<string> idList, string fields, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();

            if (!string.IsNullOrEmpty(fields))
            {
                strSql.Append("select " + fields + " ");
            }
            else
            {
                strSql.Append("select * ");
            }
            strSql.Append(" FROM ReportFormQueryDataSource ");
            List<string> idsql = new List<string>();
            foreach (var id in idList)
            {
                string[] tmp = id.Split(';');
                idsql.Add("( SectionNo='" + tmp[0] + "' and TestTypeNo='" + tmp[1] + "' and SampleNo='" + tmp[2] + "' and Receivedate='" + tmp[3] + "') ");
            }


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where   (" + string.Join("or", idsql) + ") and " + strWhere + "   ");
            }
            else
            {
                strSql.Append(" where   (" + string.Join("or", idsql) + ")  ");
            }
            ZhiFang.Common.Log.Log.Debug("GetReportFromByReportFormID:"+strSql.ToString());
            ZhiFang.Common.Log.Log.Debug("GetReportFromByReportFormID.idList:" + idList + ";fields:" + fields + ";strWhere:" + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

