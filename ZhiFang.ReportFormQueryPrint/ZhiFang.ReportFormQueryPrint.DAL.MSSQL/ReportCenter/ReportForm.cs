using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.IDAL;
using System.Collections.Generic;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    /// <summary>
    /// 数据访问类:ReportFormFull
    /// </summary>
    public class ReportForm : IDReportForm
    {
        public ReportForm()
        { }
        #region  Method


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ReportFormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ReportFormFull");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string ReportFormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ReportFormFull ");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' ");
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }       /// <summary>
                /// 批量删除数据
                /// </summary>
        public bool DeleteList(string ReportFormIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ReportFormFull ");
            strSql.Append(" where ReportFormID in (" + ReportFormIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportFormFull ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ReportFormFull ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM ReportFormFull ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ReportFormID desc");
            }
            strSql.Append(")AS Row, T.*  from ReportFormFull T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method


        //int IDReportForm.Delete(string FormNo)
        //{
        //    throw new NotImplementedException();
        //}

        public Model.ReportForm GetModel(string FormNo)
        {
            DataSet ds = this.GetListByFormNo(FormNo);
            Model.ReportForm model = new Model.ReportForm();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["FormNo"].ToString() != "")
                {
                    model.FormNo = ds.Tables[0].Rows[0]["FormNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReceiveDate"].ToString() != "")
                {
                    model.ReceiveDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReceiveDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SectionNo"].ToString() != "")
                {
                    model.SectionNo = int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TestTypeNo"].ToString() != "")
                {
                    model.TestTypeNo = int.Parse(ds.Tables[0].Rows[0]["TestTypeNo"].ToString());
                }
                model.SampleNo = ds.Tables[0].Rows[0]["SampleNo"].ToString();

               
                if (ds.Tables[0].Rows[0]["SampleTypeNo"].ToString() != "")
                {
                    model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString());
                }
                model.PatNo = ds.Tables[0].Rows[0]["PatNo"].ToString();
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                if (ds.Tables[0].Rows[0]["GenderNo"].ToString() != "")
                {
                    model.GenderNo = int.Parse(ds.Tables[0].Rows[0]["GenderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Age"].ToString() != "")
                {
                    model.Age = Convert.ToDouble(ds.Tables[0].Rows[0]["Age"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AgeUnitNo"].ToString() != "")
                {
                    model.AgeUnitNo = int.Parse(ds.Tables[0].Rows[0]["AgeUnitNo"].ToString());
                }
                
                if (ds.Tables[0].Rows[0]["DistrictNo"].ToString() != "")
                {
                    model.DistrictNo = int.Parse(ds.Tables[0].Rows[0]["DistrictNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WardNo"].ToString() != "")
                {
                    model.WardNo = int.Parse(ds.Tables[0].Rows[0]["WardNo"].ToString());
                }
                model.Bed = ds.Tables[0].Rows[0]["Bed"].ToString();
                if (ds.Tables[0].Rows[0]["DeptNo"].ToString() != "")
                {
                    model.DeptNo = int.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
                }
                model.Doctor = ds.Tables[0].Rows[0]["Doctor"].ToString();
               
                model.Collecter = ds.Tables[0].Rows[0]["Collecter"].ToString();
                if (ds.Tables[0].Rows[0]["CollectDate"].ToString() != "")
                {
                    model.CollectDate = DateTime.Parse(ds.Tables[0].Rows[0]["CollectDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CollectTime"].ToString() != "")
                {
                    model.CollectTime = DateTime.Parse(ds.Tables[0].Rows[0]["CollectTime"].ToString());
                }
                model.FormMemo = ds.Tables[0].Rows[0]["FormMemo"].ToString();
                model.Technician = ds.Tables[0].Rows[0]["Technician"].ToString();
                if (ds.Tables[0].Rows[0]["TestDate"].ToString() != "")
                {
                    model.TestDate = DateTime.Parse(ds.Tables[0].Rows[0]["TestDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TestTime"].ToString() != "")
                {
                    model.TestTime = DateTime.Parse(ds.Tables[0].Rows[0]["TestTime"].ToString());
                }
                model.Operator = ds.Tables[0].Rows[0]["Operator"].ToString();
                if (ds.Tables[0].Rows[0]["OperDate"].ToString() != "")
                {
                    model.OperDate = DateTime.Parse(ds.Tables[0].Rows[0]["OperDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OperTime"].ToString() != "")
                {
                    model.OperTime = DateTime.Parse(ds.Tables[0].Rows[0]["OperTime"].ToString());
                }
                model.Checker = ds.Tables[0].Rows[0]["Checker"].ToString();
                if (ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    model.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckTime"].ToString() != "")
                {
                    model.CheckTime = DateTime.Parse(ds.Tables[0].Rows[0]["CheckTime"].ToString());
                }
                model.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                if (ds.Tables[0].Rows[0]["DiagNo"].ToString() != "")
                {
                    model.DiagNo = int.Parse(ds.Tables[0].Rows[0]["DiagNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PrintTimes"].ToString() != "")
                {
                    model.PrintTimes = int.Parse(ds.Tables[0].Rows[0]["PrintTimes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SickTypeNo"].ToString() != "")
                {
                    model.SickTypeNo = int.Parse(ds.Tables[0].Rows[0]["SickTypeNo"].ToString());
                }
                model.FormComment = ds.Tables[0].Rows[0]["FormComment"].ToString();
                model.zdy1 = ds.Tables[0].Rows[0]["zdy1"].ToString();
                model.zdy2 = ds.Tables[0].Rows[0]["zdy2"].ToString();
                model.zdy3 = ds.Tables[0].Rows[0]["zdy3"].ToString();
                model.zdy4 = ds.Tables[0].Rows[0]["zdy4"].ToString();
                model.zdy5 = ds.Tables[0].Rows[0]["zdy5"].ToString();
                if (ds.Tables[0].Rows[0]["inceptdate"].ToString() != "")
                {
                    model.inceptdate = DateTime.Parse(ds.Tables[0].Rows[0]["inceptdate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["incepttime"].ToString() != "")
                {
                    model.incepttime = DateTime.Parse(ds.Tables[0].Rows[0]["incepttime"].ToString());
                }
                model.incepter = ds.Tables[0].Rows[0]["incepter"].ToString();
              
                if (ds.Tables[0].Rows[0]["clientno"].ToString() != "")
                {
                    model.clientno = int.Parse(ds.Tables[0].Rows[0]["clientno"].ToString());
                }
                
                if (ds.Tables[0].Rows[0]["ReceiveTime"].ToString() != "")
                {
                    model.ReceiveTime = DateTime.Parse(ds.Tables[0].Rows[0]["ReceiveTime"].ToString());
                }
                
                model.testaim = ds.Tables[0].Rows[0]["testaim"].ToString();
               
                model.paritemname = ds.Tables[0].Rows[0]["paritemname"].ToString();
                model.clientprint = ds.Tables[0].Rows[0]["clientprint"].ToString();
                model.resultsend = ds.Tables[0].Rows[0]["resultsend"].ToString();
                model.reportsend = ds.Tables[0].Rows[0]["reportsend"].ToString();
               
                if (ds.Tables[0].Rows[0]["PrintDateTime"].ToString() != "")
                {
                    model.PrintDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["PrintDateTime"].ToString());
                }
                model.PrintOper = ds.Tables[0].Rows[0]["PrintOper"].ToString();
               
                model.ZDY6 = ds.Tables[0].Rows[0]["ZDY6"].ToString();
                model.ZDY7 = ds.Tables[0].Rows[0]["ZDY7"].ToString();
                model.ZDY8 = ds.Tables[0].Rows[0]["ZDY8"].ToString();
                model.ZDY9 = ds.Tables[0].Rows[0]["ZDY9"].ToString();
                model.ZDY10 = ds.Tables[0].Rows[0]["ZDY10"].ToString();
              
                return model;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetReportFormFullList(string ReportFormID)
        {
            if (ReportFormID == null || ReportFormID == "") return new DataTable();

            try
            {
                var sql = "select * from ReportFormQueryDataSource where ReportFormID='" + ReportFormID + "' ";
                DataSet ds = DbHelperSQL.Query(sql);

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
            } catch (Exception ex) {
                ZhiFang.Common.Log.Log.Error("GetReportFormFullList:" + ex.ToString());
                return new DataTable();
            }
        }


        int IDReportForm.Delete(string FormNo)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.ReportForm model, DateTime? StartDay, DateTime? EndDay)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePrintTimes(string[] reportformidlist, string uluserCName)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (null != uluserCName && !"".Equals(uluserCName))
                {
                    strSql.Append(" update ReportFormFull set PrintOper = '" + uluserCName + "',printtimes=isnull(printtimes, 0) +1 , PRINTDATETIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  where  ");
                }
                else
                {
                    strSql.Append(" update ReportFormFull set printtimes=isnull(printtimes, 0) +1 , PRINTDATETIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  where  ");
                }
                foreach (string a in reportformidlist)
                {
                    strSql.Append(" ( ReportPublicationID=" + a + ") or ");
                }
                strSql.Append(" 1=2 ");
                //Hashtable ht=new Hashtable();
                //ht.Add(strSql.ToString(),new );
                ZhiFang.Common.Log.Log.Debug("UpdatePrintTimes.增加打印次数: sql = " + strSql.ToString());
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

        public DataSet GetListByFormNo(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1  ");
                strSql.Append(" Convert(varchar(10),ReceiveDate,21)+';'+Convert(varchar(30),SectionNo)+';'+Convert(varchar(30),TestTypeNo)+';'+Convert(varchar(30),SampleNo) as FormNo ,Convert(varchar(10),ReceiveDate,21)+';'+Convert(varchar(30),SectionNo)+';'+Convert(varchar(30),TestTypeNo)+';'+Convert(varchar(30),SampleNo) as ReportFormID ,Convert(varchar(10),ReceiveDate,21) as ReceiveDate,* ");
                strSql.Append(" from ReportFormFull ");
                strSql.Append(" where ReportPublicationID=" + FormNo+" ");
                
                return DbHelperSQL.Query(strSql.ToString());
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter.GetListByFormNo:" + e.ToString());
                return null;
            }
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Add(Model.ReportForm t)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.ReportForm t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.ReportForm t)
        {
            throw new NotImplementedException();
        }

        internal DataTable GetReportValue(string[] p)
        {
            throw new NotImplementedException();
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

        public bool UpdatePageInfo(string reportformid, string pageCount, string pageName)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update ReportFormFull set PageCount='" + pageCount + "' , pageName='" + pageName + "'   where  ");
                strSql.Append("  ReportPublicationID=" + reportformid + " ");
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

        public bool UpdateClientPrintTimes(string[] reportformidlist)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update ReportFormFull set ClientPrint=isnull(ClientPrint, 0) +1 , PRINTDATETIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  where  ");
                foreach (string a in reportformidlist)
                {
                    strSql.Append(" ( ReportPublicationID=" + a + ") or ");
                }
                strSql.Append(" 1=2 ");
                //Hashtable ht=new Hashtable();
                //ht.Add(strSql.ToString(),new );
                ZhiFang.Common.Log.Log.Debug("UpdateClientPrintTime.增加ClientPrint打印次数.SQL："+strSql.ToString());
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

       public DataTable GetReportFormList(string[] reprotFormIdList)
        {
            if (reprotFormIdList == null || reprotFormIdList.Length <= 0) return new DataTable();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from ReportFormQueryDataSource where ");
                foreach (string a in reprotFormIdList)
                {
                    sql.Append(" ( ReportFormID='" + a + "') or ");
                }
                sql.Append(" 1=2 ");
                DataSet ds = DbHelperSQL.Query(sql.ToString());

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
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("GetReportFormList:" + ex.ToString());
                return new DataTable();
            }
        }

        public DataSet GetReporFormAllByReportFormIdList(List<string> idList, string fields, string strWhere)
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
            strSql.Append(" FROM ReportFormAllQueryDataSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where ActiveFlag=1 and reportformid in ('"+ string.Join("','",idList) + "') and " + strWhere + "   ");
            }
            else
            {
                strSql.Append(" where ActiveFlag=1 and reportformid in ('" + string.Join("','", idList) + "') ");
            }
            ZhiFang.Common.Log.Log.Debug("GetReporFormAllByReportFormIdList.idList:"+ idList + ";fields:" + fields + ";strWhere:" + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetReportFromByReportFormID(List<string> idList, string fields, string strWhere)
        {
            throw new NotImplementedException();
        }

        public int UpdateClientPrint(string formno)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update ReportFormFull set ClientPrint=0   ");
                string[] p = formno.Split(';');
                if (p.Length >= 4)
                {
                    strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                }
                ZhiFang.Common.Log.Log.Debug("清除自助打印次数: sql = " + strSql.ToString());
                return DbHelperSQL.ExecuteSql(strSql.ToString());

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("UpdateClientPrint.异常：" + e.ToString());
                return 0;
            }
        }

        public int UpdatePrintTimes(string formno) {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update ReportFormFull set PrintTimes=0   ");
                string[] p = formno.Split(';');
                if (p.Length >= 4)
                {
                    strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                }
                ZhiFang.Common.Log.Log.Debug("清除临床打印次数: sql = " + strSql.ToString());
                return DbHelperSQL.ExecuteSql(strSql.ToString());

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("UpdatePrintTimes.异常：" + e.ToString());
                return 0;
            }
        }

        public DataTable GetReportFormListByFormId(string FormNo)
        {
            if (FormNo == null || FormNo.Equals("")) return new DataTable();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from ReportFormQueryDataSource where " + FormNo);
                ZhiFang.Common.Log.Log.Debug("GetReportFormListByFormId.查询报告单.SQL:" + sql.ToString());
                DataSet ds = DbHelperSQL.Query(sql.ToString());

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
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("GetReportFormListByFormId:" + ex.ToString());
                return new DataTable();
            }
        }

        public DataSet GetSampleReportFromByReportFormID(List<string> idList, string fields, string strWhere)
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
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where ActiveFlag=1 and reportformid in ('" + string.Join("','", idList) + "') and " + strWhere + "   ");
            }
            else
            {
                strSql.Append(" where ActiveFlag=1 and reportformid in ('" + string.Join("','", idList) + "') ");
            }
            ZhiFang.Common.Log.Log.Debug("GetSampleReportFromByReportFormID.idList:" + idList + ";fields:" + fields + ";strWhere:" + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetReportFormFullByReportFormID(string ReportFormID)
        {
            if (ReportFormID == null || ReportFormID.Equals("")){
                ZhiFang.Common.Log.Log.Debug("GetReportFormFullByReportFormID.ReportFormID为空请检查传入参数！");
                return null;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ReportFormFull where ReportPublicationID = " + ReportFormID +" ");
            ZhiFang.Common.Log.Log.Debug("GetReportFormFullByReportFormID.ReportFormFull.SQL:" + sql.ToString());
            return DbHelperSQL.Query(sql.ToString());
            
        }

        public int UpdateReportFormFull(ReportFormFull t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE ReportFormFull SET ");

            builder.Append("DataUpdateTime=" + "'" + DateTime.Now + "'");

            if (t.SectionNo != null && !t.SectionNo.Equals(""))
            {
                builder.Append(",SectionNo=" + "'" + t.SectionNo + "'");
            }
            if (t.TestTypeNo != null && !t.TestTypeNo.Equals(""))
            {
                builder.Append(",TestTypeNo=" + "'" + t.TestTypeNo + "'");
            }
            if (t.SampleNo != null && !t.SampleNo.Equals(""))
            {
                builder.Append(",SampleNo=" + "'" + t.SampleNo + "'");
            }
            if (t.SectionName != null && !t.SectionName.Equals(""))
            {
                builder.Append(",SectionName=" + "'" + t.SectionName + "'");
            }
            if (t.TestTypeName != null && !t.TestTypeName.Equals(""))
            {
                builder.Append(",TestTypeName=" + "'" + t.TestTypeName + "'");
            }
            if (t.SampleTypeNo != null && !t.SampleTypeNo.Equals(""))
            {
                builder.Append(",SampleTypeNo=" + "'" + t.SampleTypeNo + "'");
            }
            if (t.SampletypeName != null && !t.SampletypeName.Equals(""))
            {
                builder.Append(",SampletypeName=" + "'" + t.SampletypeName + "'");
            }
            if (t.SecretType != null && !t.SecretType.Equals(""))
            {
                builder.Append(",SecretType=" + "'" + t.SecretType + "'");
            }
            if (t.PatNo != null && !t.PatNo.Equals(""))
            {
                builder.Append(",PatNo=" + "'" + t.PatNo + "'");
            }
            if (t.CName != null && !t.CName.Equals(""))
            {
                builder.Append(",CName=" + "'" + t.CName + "'");
            }
            if (t.GenderNo != null && !t.GenderNo.Equals(""))
            {
                builder.Append(",GenderNo=" + "'" + t.GenderNo + "'");
            }
            if (t.GenderName != null && !t.GenderName.Equals(""))
            {
                builder.Append(",GenderName=" + "'" + t.GenderName + "'");
            }
            if (t.Age != null && !t.Age.Equals(""))
            {
                builder.Append(",Age=" + "'" + t.Age + "'");
            }
            if (t.AgeUnitNo != null && !t.AgeUnitNo.Equals(""))
            {
                builder.Append(",AgeUnitNo=" + "'" + t.AgeUnitNo + "'");
            }
            if (t.AgeUnitName != null && !t.AgeUnitName.Equals(""))
            {
                builder.Append(",AgeUnitName=" + "'" + t.AgeUnitName + "'");
            }
            if (t.DistrictNo != null && !t.DistrictNo.Equals(""))
            {
                builder.Append(",DistrictNo=" + "'" + t.DistrictNo + "'");
            }
            if (t.DistrictName != null && !t.DistrictName.Equals(""))
            {
                builder.Append(",DistrictName=" + "'" + t.DistrictName + "'");
            }
            if (t.WardNo != null && !t.WardNo.Equals(""))
            {
                builder.Append(",WardNo=" + "'" + t.WardNo + "'");
            }
            if (t.WardName != null && !t.WardName.Equals(""))
            {
                builder.Append(",WardName=" + "'" + t.WardName + "'");
            }
            if (t.Bed != null && !t.Bed.Equals(""))
            {
                builder.Append(",Bed=" + "'" + t.Bed + "'");
            }
            if (t.DeptNo != null && !t.DeptNo.Equals(""))
            {
                builder.Append(",DeptNo=" + "'" + t.DeptNo + "'");
            }
            if (t.DeptName != null && !t.DeptName.Equals(""))
            {
                builder.Append(",DeptName=" + "'" + t.DeptName + "'");
            }
            if (t.SerialNo != null && !t.SerialNo.Equals(""))
            {
                builder.Append(",SerialNo=" + "'" + t.SerialNo + "'");
            }
            if (t.FormComment != null && !t.FormComment.Equals(""))
            {
                builder.Append(",FormComment=" + "'" + t.FormComment + "'");
            }

            if (t.FFormMemo != null && !t.FFormMemo.Equals(""))
            {
                builder.Append(",FFormMemo=" + "'" + t.FFormMemo + "'");
            }
            if (t.SickTypeNo != null && !t.SickTypeNo.Equals(""))
            {
                builder.Append(",SickTypeNo=" + "'" + t.SickTypeNo + "'");
            }
            if (t.SickTypeName != null && !t.SickTypeName.Equals(""))
            {
                builder.Append(",SickTypeName=" + "'" + t.SickTypeName + "'");
            }
            if (t.ZDY1 != null && !t.ZDY1.Equals(""))
            {
                builder.Append(",ZDY1=" + "'" + t.ZDY1 + "'");
            }
            if (t.ZDY2 != null && !t.ZDY2.Equals(""))
            {
                builder.Append(",ZDY2=" + "'" + t.ZDY2 + "'");
            }
            if (t.ZDY3 != null && !t.ZDY3.Equals(""))
            {
                builder.Append(",ZDY3=" + "'" + t.ZDY3 + "'");
            }
            if (t.ZDY4 != null && !t.ZDY4.Equals(""))
            {
                builder.Append(",ZDY4=" + "'" + t.ZDY4 + "'");
            }
            if (t.ZDY5 != null && !t.ZDY5.Equals(""))
            {
                builder.Append(",ZDY5=" + "'" + t.ZDY5 + "'");
            }
            if (t.ZDY6 != null && !t.ZDY6.Equals(""))
            {
                builder.Append(",ZDY6=" + "'" + t.ZDY6 + "'");
            }
            if (t.ZDY7 != null && !t.ZDY7.Equals(""))
            {
                builder.Append(",ZDY7=" + "'" + t.ZDY7 + "'");
            }
            if (t.ZDY8 != null && !t.ZDY8.Equals(""))
            {
                builder.Append(",ZDY8=" + "'" + t.ZDY8 + "'");
            }
            if (t.ZDY9 != null && !t.ZDY9.Equals(""))
            {
                builder.Append(",ZDY9=" + "'" + t.ZDY9 + "'");
            }
            if (t.ZDY10 != null && !t.ZDY10.Equals(""))
            {
                builder.Append(",ZDY10=" + "'" + t.ZDY10 + "'");
            }
            if (t.ZDY11 != null && !t.ZDY11.Equals(""))
            {
                builder.Append(",ZDY11=" + "'" + t.ZDY11 + "'");
            }
            if (t.PrintTimes>0)
            {
                builder.Append(",PrintTimes=" + "'" + t.PrintTimes + "'");
            }
            if (t.ClientPrint > 0)
            {
                builder.Append(",ClientPrint=" + "'" + t.ClientPrint + "'");
            }
            builder.Append(" WHERE ReportPublicationID =" + t.ReportPublicationID);
            ZhiFang.Common.Log.Log.Debug("UpdateReportFormFull:"+builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public DataSet GetRepotFormByReportFormIDGroupByZdy15(string PatNo, string zdy15)
        {
            if (null == PatNo || "" == PatNo)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQLReportCenter.GetRepotFormByReportFormIDGroupByZdy15.PatNo为空");
                return new DataSet();
            }
            if (null == zdy15 || "" == zdy15)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQLReportCenter.GetRepotFormByReportFormIDGroupByZdy15.zdy15");
                return new DataSet();
            }
            ZhiFang.Common.Log.Log.Debug("MSSSQLReportCenter.GetRepotFormByReportFormIDGroupByZdy15.PatNo:" + PatNo + ",zdy15:" + zdy15);
            var sql = "select * from ReportFormQueryDataSource where PatNo='" + PatNo + "' and zdy15=" + zdy15;
            ZhiFang.Common.Log.Log.Debug("MSSSQLReportCenter.GetRepotFormByReportFormIDGroupByZdy15.SQL:" + sql);
            DataSet ds = DbHelperSQL.Query(sql);
            return ds;
        }

        public DataSet GetRepotFormByReportFormIDAndZdy15AndReceiveDate(string PatNo, string zdy15, string ReceiveDate)
        {
            if (null == PatNo || "" == PatNo)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQLReportCenter.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.PatNo为空");
                return new DataSet();
            }
            if (null == zdy15 || "" == zdy15)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQLReportCenter.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.zdy15");
                return new DataSet();
            }
            if (null == ReceiveDate || "" == ReceiveDate)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQLReportCenter.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.ReceiveDate");
                return new DataSet();
            }
            ZhiFang.Common.Log.Log.Debug("MSSSQLReportCenter.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.PatNo:" + PatNo + ",zdy15:" + zdy15 + ",ReceiveDate:" + ReceiveDate);
            var sql = "select * from ReportFormQueryDataSource where ActiveFlag=1 and PatNo='" + PatNo + "' and zdy15='" + zdy15 + "' and ReceiveDate='" + ReceiveDate+"'";
            ZhiFang.Common.Log.Log.Debug("MSSSQLReportCenter.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.SQL:" + sql);
            DataSet ds = DbHelperSQL.Query(sql);
            return ds;
        }

        public DataSet GetListTopByWhereAndSort(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ReportFormQueryDataSource where ActiveFlag=1");
            if (strWhere.Trim() != "")
            {
                strSql.Append("  and  " + strWhere);
            }
            if (!string.IsNullOrEmpty(filedOrder))
            {
                strSql.Append(" order by " + filedOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

