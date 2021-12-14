using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.IDAL.Other;
using System.Data.SqlClient;

namespace ZhiFang.DAL.Oracle.weblis.Other
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
           
            switch (ViewName)
            {
                case "_ReportFormFullDataSource":
                    strSql.Append(" ReportFormID,FORMNO,BARCODE,PersonID,RECEIVEDATE,SECTIONNO,SECTIONNAME,TESTTYPENO,SAMPLENO,STATUSNO, StatusType,SAMPLETYPENO,CNAME,GENDERNO,AGE,AGEUNITNO, FOLKNO,TelNo,DOCTOR, COLLECTER,COLLECTDATE, COLLECTTIME,");
                    strSql.Append(" TECHNICIAN, TESTDATE, TESTTIME, OPERATOR, OPERDATE, OPERTIME,CHECKER, CHECKDATE, CHECKTIME, SERIALNO, SICKTYPENO, WebLisSourceOrgID,");
                    strSql.Append(" WebLisSourceOrgName,WebLisOrgID,WebLisOrgName,resultstatus,AGEUNITNAME,GENDERNAME, DEPTNAME, DOCTORNAME, DISTRICTNAME, ");
                    strSql.Append(" WARDNAME, FOLKNAME, SICKTYPENAME, SAMPLETYPENAME, TESTTYPENAME ");
                    strSql.Append(" from ReportFormFull ");
                    break;
                case "_ReportItemFullDataSource":
                    strSql.Append(" ReportFormID, FORMNO, Barcode, RECEIVEDATE, SECTIONNO, SectionName, TESTTYPENO, SAMPLENO, PARITEMNO, ITEMNO, ");
                    strSql.Append(" TESTITEMNAME, TESTITEMSNAME, ORIGINALVALUE, REPORTVALUE, UNIT, ORIGINALDESC, REPORTDESC, STATUSNO, EQUIPNO,  EquipName, ");
                    strSql.Append("  CheckType, CheckTypeName, MODIFIED, REFRANGE, ITEMDATE, ITEMTIME, ISMATCH, RESULTSTATUS, PARITEMNAME, ");
                    strSql.Append(" PARITEMSNAME");
                    strSql.Append(" from ReportItemFull ");
                    break;
                case "_ReportMicroFullDataSource":
                    strSql.Append(" ReportFormID, FormNo, Barcode,  ReceiveDate, SectionNo, TestTypeNo, SampleNo, ItemNo,  ");
                    strSql.Append(" ItemName, DescNo, DescName, MicroNo, MicroDesc, MicroName, AntiNo, AntiName, Suscept, SusQuan, RefRange, SusDesc, AntiUnit, ItemDesc, EquipNo,  ");
                    strSql.Append(" EquipName, ItemDate, ItemTime, Modified, CheckType ");
                    strSql.Append(" from ReportMicroFull ");
                    break;
                case "_ReportMarrowFullDataSource":
                    strSql.Append(" ReportFormID, FormNo,  Barcode, ReceiveDate, SectionNo,  TestTypeNo, SampleNo, ParItemNo, ");
                    strSql.Append(" ItemNo, ItemCName,  ItemEName,  ParItemCName, ParItemEName, BloodNum, ");
                    strSql.Append(" BloodPercent, MarrowNum, MarrowPercent, BloodDesc, MarrowDesc, RefRange, EquipNo, EquipName, ItemDate, ItemTime, ResultStatus");
                    strSql.Append(" from ReportMarrowFull ");
                    break;
                case "_NRequestFormFullDataSource"://申请单查询
                    strSql.Append(" nf.PersonID,nf.GenderNo,nf.GenderName,nf.SampleType,nf.SampleTypeNo,nf.WARDNAME,nf.WARDNO, ");
                    strSql.Append(" nf.CNAME,nf.PatNo,nf.Doctor,nf.Age,nf.AgeUnit,nf.FOLKNAME,nf.CollectDate,nf.OperDate,nf.ClientNo, ");
                    strSql.Append(" nf.ClientName,nf.WebLisSourceOrgID,nf.WebLisSourceOrgName,nf.SerialNo,nf.TESTTYPENO,nf.TESTTYPEName,nf.STATUSNO,nf.DISTRICTNO,nf.DISTRICTNAME,nf.DeptNo,nf.DeptName,nf.CollecterName,nf.jztype,nf.TELNO,nf.AGEUNITNO,nf.COLLECTTIME,nf.WEBLISSOURCEORGID,nf.WEBLISORGID,nf.WEBLISORGNAME,nf.STATUSNAME,nf.JZTYPENAME,nf.CHECKNO,nf.CHECKNAME");
                    strSql.Append(" from NRequestForm as nf left join NRequestItem as ni on ni.NRequestFormNo=nf.NRequestFormNo ");
                    break;
                case "_SynergyReportFormFull"://不走weblis平台，不签收申请。手动录入
                    strSql.Append(" ReportFormID,FORMNO,BARCODE,PersonID,RECEIVEDATE,SECTIONNO,SECTIONNAME,TESTTYPENO,SAMPLENO,STATUSNO, StatusType,SAMPLETYPENO,CNAME,GENDERNO,AGE,AGEUNITNO, FOLKNO,TelNo,DOCTOR, COLLECTER,COLLECTDATE, COLLECTTIME,");
                    strSql.Append(" TECHNICIAN, TESTDATE, TESTTIME, OPERATOR, OPERDATE, OPERTIME,CHECKER, CHECKDATE, CHECKTIME, SERIALNO, SICKTYPENO, WebLisSourceOrgID,");
                    strSql.Append(" WebLisSourceOrgName,WebLisOrgID,WebLisOrgName,resultstatus,AGEUNITNAME,GENDERNAME, DEPTNAME, DOCTORNAME, DISTRICTNAME, ");
                    strSql.Append(" WARDNAME, FOLKNAME, SICKTYPENAME, SAMPLETYPENAME, TESTTYPENAME,CLIENTNO, ");
                    strSql.Append(" Isdown as WebLisFlag  from ReportFormFull r ");
                    break;
                default:
                    strSql.Append(" * from  ReportFormFull r inner join BarCodeForm b on r.SERIALNO =b.BarCode ");
                    break;
            }

            strSql.Append(" where  1=1 ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            if (Top > -1)
            {
                strSql.Append(" and ROWNUM <= '" + Top + "'");
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
                sp0.Value = Top;
                sp1.Value = strWhere;
                sp2.Value = strOrder;
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
        public DataSet GetViewData2(int Top, string ViewName, string strWhere, string strOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            if (Top > -1)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" from " + ViewName + " ");
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
        public DataSet GetReportValue(string[] p)
        {
            return new DataSet();
        }

    }
}
