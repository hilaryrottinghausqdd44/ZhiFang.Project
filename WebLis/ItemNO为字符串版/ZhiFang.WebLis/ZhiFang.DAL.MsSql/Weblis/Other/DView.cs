using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.IDAL.Other;
using System.Data.SqlClient;
using System.Configuration;

namespace ZhiFang.DAL.MsSql.Weblis.Other
{
    public partial class DView : BaseDALLisDB, IDView
    {
        public DView(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DView()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        /// <summary>
        /// 查询指定视图中所有数据
        /// </summary>
        /// <param name="Top">条数</param>
        /// <param name="ViewName">视图名称</param>
        /// <param name="strWhere">条件</param>
        /// <param name="StrOrder">排序</param>
        /// <returns>DataSet</returns>
        public DataSet GetViewData(int Top, string ViewName, string strWhere, string strOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            if (Top > -1)
            {
                strSql.Append(" top " + Top.ToString());
            }
            switch (ViewName)
            {
                case "_ReportFormFullDataSource":
                    strSql.Append(" ReportFormID,FORMNO,BARCODE,PersonID,RECEIVEDATE,SECTIONNO,SECTIONNAME,TESTTYPENO,SAMPLENO,STATUSNO, StatusType,SAMPLETYPENO,CNAME,GENDERNO,AGE,AGEUNITNO, FOLKNO,TelNo,DOCTOR, COLLECTER,COLLECTDATE, COLLECTTIME,");
                    strSql.Append(" TECHNICIAN, TESTDATE, TESTTIME, OPERATOR, OPERDATE, OPERTIME,CHECKER, CHECKDATE, CHECKTIME, SERIALNO, SICKTYPENO, WebLisSourceOrgID,");
                    strSql.Append(" WebLisSourceOrgName,WebLisOrgID,WebLisOrgName,resultstatus,AGEUNITNAME,GENDERNAME, DEPTNAME, DOCTORNAME, DISTRICTNAME, ");
                    strSql.Append(" WARDNAME, FOLKNAME, SICKTYPENAME, SAMPLETYPENAME, TESTTYPENAME, ");
                    strSql.Append(" ZDY2, formmemo,clientno,SECTIONTYPE ");
                    strSql.Append(" from ReportFormFull ");
                    break;
                case "_ReportFormFullDataSource_BoErCheng":
                    string ReportFormFullField = System.Configuration.ConfigurationManager.AppSettings["ReportFormFullField"].ToString();

                    strSql.Append(" "+ReportFormFullField+" ");
                    strSql.Append(" from ReportFormFull ");
                    break;
                case "_ReportFormFullDataSource_Hehe"://OldSerialno   
                    strSql.Append(" rf.ReportFormID,rf.FORMNO,rf.BARCODE,PersonID,rf.RECEIVEDATE,rf.SECTIONNO,rf.SECTIONNAME,rf.TESTTYPENO,rf.SAMPLENO,r.STATUSNO, StatusType,SAMPLETYPENO,CNAME,GENDERNO,AGE,AGEUNITNO, FOLKNO,TelNo,DOCTOR, COLLECTER,COLLECTDATE, COLLECTTIME,");
                    strSql.Append(" rf.TECHNICIAN, TESTDATE, TESTTIME, OPERATOR, OPERDATE, OPERTIME,CHECKER, CHECKDATE, CHECKTIME, ri.OldSerialno as SERIALNO, SICKTYPENO, WebLisSourceOrgID,");
                    strSql.Append(" WebLisSourceOrgName,WebLisOrgID,WebLisOrgName,rf.resultstatus,AGEUNITNAME,GENDERNAME, DEPTNAME, DOCTORNAME, DISTRICTNAME, ");
                    strSql.Append(" WARDNAME, FOLKNAME, SICKTYPENAME, SAMPLETYPENAME, TESTTYPENAME ");
                    strSql.Append(" from ReportFormFull rf inner join ReportItemFull ri on rf.ReportFormID = ri.ReportFormID ");
                    break;
                case "_ReportFormFullDataSource_Hehe_Micro"://OldSerialno 未完成
                    strSql.Append(" r.ReportFormID,r.FORMNO,r.BARCODE,PersonID,r.RECEIVEDATE,r.SECTIONNO,r.SECTIONNAME,r.TESTTYPENO,r.SAMPLENO,r.STATUSNO, StatusType,SAMPLETYPENO,CNAME,GENDERNO,AGE,AGEUNITNO, FOLKNO,TelNo,DOCTOR, COLLECTER,COLLECTDATE, COLLECTTIME,");
                    strSql.Append(" r.TECHNICIAN, TESTDATE, TESTTIME, OPERATOR, OPERDATE, OPERTIME,CHECKER, CHECKDATE, CHECKTIME, i.OldSerialno as SERIALNO, SICKTYPENO, WebLisSourceOrgID,");
                    strSql.Append(" WebLisSourceOrgName,WebLisOrgID,WebLisOrgName,r.resultstatus,AGEUNITNAME,GENDERNAME, DEPTNAME, DOCTORNAME, DISTRICTNAME, ");
                    strSql.Append(" WARDNAME, FOLKNAME, SICKTYPENAME, SAMPLETYPENAME, TESTTYPENAME ");
                    strSql.Append(" from ReportFormFull r inner join ReportMicroFull i on r.ReportFormID = i.ReportFormID ");
                    break;
                case "_ReportFormFullDataSource_Hehe_Marrow"://OldSerialno  未完成
                    strSql.Append(" r.ReportFormID,r.FORMNO,r.BARCODE,PersonID,r.RECEIVEDATE,r.SECTIONNO,r.SECTIONNAME,r.TESTTYPENO,r.SAMPLENO,r.STATUSNO, StatusType,SAMPLETYPENO,CNAME,GENDERNO,AGE,AGEUNITNO, FOLKNO,TelNo,DOCTOR, COLLECTER,COLLECTDATE, COLLECTTIME,");
                    strSql.Append(" r.TECHNICIAN, TESTDATE, TESTTIME, OPERATOR, OPERDATE, OPERTIME,CHECKER, CHECKDATE, CHECKTIME, i.OldSerialno as SERIALNO, SICKTYPENO, WebLisSourceOrgID,");
                    strSql.Append(" WebLisSourceOrgName,WebLisOrgID,WebLisOrgName,r.resultstatus,AGEUNITNAME,GENDERNAME, DEPTNAME, DOCTORNAME, DISTRICTNAME, ");
                    strSql.Append(" WARDNAME, FOLKNAME, SICKTYPENAME, SAMPLETYPENAME, TESTTYPENAME ");
                    strSql.Append(" from ReportFormFull r inner join ReportMarrowFull i on r.ReportFormID = i.ReportFormID ");
                    break;
                case "_ReportItemFullDataSource_Exists":
                    strSql.Append(" ReportFormID as ExisCount from ReportItemFull ");
                    break;
                case "_ReportMicroFullDataSource_Exists":
                    strSql.Append(" ReportFormID as ExisCount from ReportMicroFull ");
                    break;
                case "_ReportMarrowFullDataSource_Exists":
                    strSql.Append(" ReportFormID as ExisCount from ReportMarrowFull ");
                    break;
                case "_ReportItemFullDataSource":
                    strSql.Append(" ReportFormID, FORMNO, Barcode, RECEIVEDATE, SECTIONNO, SectionName, TESTTYPENO, SAMPLENO, PARITEMNO, ITEMNO, ");
                    strSql.Append(" TESTITEMNAME, TESTITEMSNAME, ORIGINALVALUE, REPORTVALUE, UNIT, ORIGINALDESC, REPORTDESC, STATUSNO, EQUIPNO,  EquipName, ");
                    strSql.Append("  CheckType, CheckTypeName, MODIFIED, REFRANGE, ITEMDATE, ITEMTIME, ISMATCH, RESULTSTATUS, PARITEMNAME, ");
                    strSql.Append(" PARITEMSNAME");
                    strSql.Append(" from ReportItemFull ");
                    break;
                case "_ReportItemFullDataSource_BoErCheng":
                    string ReportItemFullField = System.Configuration.ConfigurationManager.AppSettings["ReportItemFullField"].ToString();
                    ReportItemFullField= ReportItemFullField.ToUpper();
                    //if (ReportItemFullField.IndexOf("TESTITEMNAME") > -1) 
                    //{
                    //    //TESTITEMNAME子项名称，关联TestItem表的CName
                    //    ReportItemFullField=ReportItemFullField.Replace("TESTITEMNAME", "TestItem.CName as TESTITEMNAME");
                    //}
                    //if (ReportItemFullField.IndexOf("TESTITEMSNAME") > -1)
                    //{
                    //    //TESTITEMSNAME子项简称，关联ReportItemFull表的shortcode
                    //   ReportItemFullField= ReportItemFullField.Replace("TESTITEMSNAME", "ReportItemFull.shortcode as TESTITEMSNAME");
                    //}
                    //if (ReportItemFullField.IndexOf("UNIT") > -1)
                    //{
                    //    //Unit单位，关联TestItem表的Unit
                    //    ReportItemFullField = ReportItemFullField.Replace("UNIT", "TestItem.Unit as UNIT");
                    //}
                    strSql.Append(" " + ReportItemFullField+" ");
                    strSql.Append(" from ReportItemFull ");
                    break;
                case "_ReportItemFullDataSource_ShiJiaZhuang":
                    strSql.Append(" ReportFormID, FORMNO, Barcode, RECEIVEDATE, SECTIONNO, SectionName, TESTTYPENO, SAMPLENO, PARITEMNO, ");
                    strSql.Append(" b.CName as TESTITEMNAME, a.shortcode as TESTITEMSNAME, b.Unit,ORIGINALVALUE, REPORTVALUE,  ORIGINALDESC, REPORTDESC, STATUSNO, EQUIPNO,  EquipName, ");
                    strSql.Append("  CheckType, CheckTypeName, MODIFIED, REFRANGE, ITEMDATE, ITEMTIME, ISMATCH, RESULTSTATUS, PARITEMNAME, ");
                    strSql.Append(" PARITEMSNAME");
                    strSql.Append("  from ReportItemFull a join TestItem b on a.ITEMNO=b.ItemNo ");
                    break;
                case "_ReportMicroFullDataSource":
                    strSql.Append(" ReportFormID, FormNo, Barcode,  ReceiveDate, SectionNo, TestTypeNo, SampleNo, ItemNo,  ");
                    strSql.Append(" ItemName, DescNo, DescName, MicroNo, MicroDesc, MicroName, AntiNo, AntiName, Suscept, SusQuan, RefRange, SusDesc, AntiUnit, ItemDesc, EquipNo,  ");
                    strSql.Append(" EquipName, ItemDate, ItemTime, Modified, CheckType,ResultNo ");
                    strSql.Append(" from ReportMicroFull ");
                    break;
                case "_ReportMicroFullDataSource_BoErCheng":
                    string ReportMicroFullField = System.Configuration.ConfigurationManager.AppSettings["ReportMicroFullField"].ToString();
                    ReportMicroFullField = ReportMicroFullField.ToUpper();
                    strSql.Append(" " + ReportMicroFullField + " ");
                   
                    strSql.Append(" from ReportMicroFull ");
                    break;
                case "_ReportMarrowFullDataSource":
                    strSql.Append(" ReportFormID, FormNo,  Barcode, ReceiveDate, SectionNo,  TestTypeNo, SampleNo, ParItemNo, ");
                    strSql.Append(" ItemNo, ItemCName,  ItemEName,  ParItemCName, ParItemEName, BloodNum, ");
                    strSql.Append(" BloodPercent, MarrowNum, MarrowPercent, BloodDesc, MarrowDesc, RefRange, EquipNo, EquipName, ItemDate, ItemTime, ResultStatus");
                    strSql.Append(" from ReportMarrowFull ");
                    break;
                case "_ReportMarrowFullDataSource_BoErCheng":
                    string ReportMarrowFullField = System.Configuration.ConfigurationManager.AppSettings["ReportMarrowFullField"].ToString();
                    ReportMarrowFullField = ReportMarrowFullField.ToUpper();
                    strSql.Append(" " + ReportMarrowFullField + " ");
                    strSql.Append(" from ReportMarrowFull ");
                    break;
                case "_NRequestFormFullDataSource"://申请单查询
                    strSql.Append(" nf.PersonID,nf.GenderNo,nf.GenderName,nf.SampleType,nf.SampleTypeNo,nf.WARDNAME,nf.WARDNO, ");
                    strSql.Append(" nf.CNAME,nf.PatNo,nf.Doctor,nf.Age,nf.AgeUnit,nf.FOLKNAME,nf.CollectDate,nf.OperDate,nf.ClientNo, ");
                    strSql.Append(" nf.ClientName,nf.WebLisSourceOrgID,nf.WebLisSourceOrgName,nf.SerialNo,nf.TESTTYPENO,nf.TESTTYPEName,nf.STATUSNO,nf.DISTRICTNO,nf.DISTRICTNAME,nf.DeptNo,nf.DeptName,nf.CollecterName,nf.jztype,nf.TELNO,nf.AGEUNITNO,nf.COLLECTTIME,nf.WEBLISSOURCEORGID,nf.WEBLISORGID,nf.WEBLISORGNAME,nf.STATUSNAME,nf.JZTYPENAME,nf.CHECKNO,nf.CHECKNAME,nf.WeblisFlag ");
                    strSql.Append(" from NRequestForm as nf left join NRequestItem as ni on ni.NRequestFormNo=nf.NRequestFormNo ");
                    break;
                case "_SynergyReportFormFull"://不走weblis平台，不签收申请。手动录入
                    strSql.Append(" Isdown as WebLisFlag,* from ReportFormFull r ");
                    break;
                case "ReportFormFull":
                    strSql.Append(" * from ReportFormFull ");
                    break;
                case "ReportItemFull":
                    strSql.Append(" * from ReportItemFull ");
                    break;
                case "ReportMicroFull":
                    strSql.Append(" * from ReportMicroFull ");
                    break;
                case "ReportMarrowFull":
                    strSql.Append(" * from ReportMarrowFull ");
                    break;
                default://走weblis平台的协同报告，签收申请
                    strSql.Append(" * from  ReportFormFull r inner join BarCodeForm b on r.SERIALNO =b.BarCode ");
                    break;
            }

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (strOrder.Trim() != "")
            {
                strSql.Append(" order by " + strOrder);
            }
            Common.Log.Log.Info("sql语句:" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetViewData_Revise(int Top, string ViewName, string strWhere, string strOrder)
        {
            try
            {
                #region 执行存储过程
                SqlParameter sp0 = new SqlParameter("@Top", SqlDbType.VarChar, 50);
                SqlParameter sp1 = new SqlParameter("@strWhere", SqlDbType.VarChar, 150);
                SqlParameter sp2 = new SqlParameter("@strOrder", SqlDbType.VarChar, 150);
                sp0.Value = Top == -1 ? "" : " top " + Top;
                sp1.Value = strWhere;
                sp2.Value = strOrder == null || strOrder == "" ? "" : " order by " + strOrder;
                DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure(ViewName, new SqlParameter[] { sp0, sp1, sp2 });
                //ds.Tables[0].TableName = "ReportFormFull";
                if (ds.Tables.Count > 0)
                {
                    //for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    //{
                    //    ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    //}
                    return ds;
                }
                else
                {
                    return new DataSet();
                }
                #endregion
            }
            catch
            {
                return new DataSet();
            }
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append(" select ");
            //if (Top > -1)
            //{
            //    strSql.Append(" top " + Top.ToString());
            //}
            //strSql.Append(" * ");
            //strSql.Append(" from " + ViewName + " ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //if (strOrder.Trim() != "")
            //{
            //    strSql.Append(" order by " + strOrder);
            //}
            //Common.Log.Log.Info("sql语句:" + strSql.ToString());
            //return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetReportValue(string[] p)
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
                    DataSet ds = RunProcedure("GetReportValue", new SqlParameter[] { sp0, sp1, sp2, sp3 }, "ReportForm");

                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                        }
                        //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
                        return ds;
                    }
                    else
                    {
                        return new DataSet();
                    }
                }
                else
                {
                    return new DataSet();
                }
            }
            catch
            {
                return new DataSet();
            }

        }

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            //if (ConStringEncrypt == "true")
            //{
            //    connectionString = DESEncrypt.Decrypt(connectionString);
            //}
            return connectionString;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(""))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        /// <summary>   
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)   
        /// </summary>   
        /// <param name="connection">数据库连接</param>   
        /// <param name="storedProcName">存储过程名</param>   
        /// <param name="parameters">存储过程参数</param>   
        /// <returns>SqlCommand</returns>   
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

    }
}
