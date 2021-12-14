using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.IDAL;
using System.Data.SqlClient;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public class View_ReportItemFull : BaseDALLisDB, IDView_ReportItemFull
    {
        public DataSet GetViewItemFull(Model.VIEW_ReportItemFull model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM VIEW_ReportItemFull where 1=1");
                if (model.TESTITEMNAME != null)
                {
                    strSql.Append(" and TESTITEMNAME='" + model.TESTITEMNAME + "' ");
                }
                if (model.CLIENTNAME != null)
                {
                    strSql.Append(" and CLIENTNAME like '%" + model.CLIENTNAME + "%' ");
                }
                if (model.CLIENTNO != null)
                {
                    strSql.Append(" and CLIENTNO='" + model.CLIENTNO + "' ");
                }
                if (model.ClientList != null && model.ClientList.Trim().Length > 0)
                {
                    strSql.Append(" and ClientNo in (" + model.ClientList + ") ");
                }
                if (model.CheckName != null)
                {
                    strSql.Append(" and CheckName like '%" + model.CheckName + "%' ");
                }
                if (model.CheckNo != null)
                {
                    strSql.Append(" and CheckNo='" + model.CheckNo + "' ");
                }
                if (model.CHECKER != null)
                {
                    strSql.Append(" and CHECKER='" + model.CHECKER + "' ");
                }
                if (model.CHARGEFLAG != null)
                {
                    strSql.Append(" and CHARGEFLAG='" + model.CHARGEFLAG + "' ");
                }
                if (model.FORMMEMO != null)
                {
                    strSql.Append(" and FORMMEMO='" + model.FORMMEMO + "' ");
                }
                if (model.TESTDATE != null)
                {
                    strSql.Append(" and TESTDATE='" + model.TESTDATE + "' ");
                }
                if (model.TESTTIME != null)
                {
                    strSql.Append(" and TESTTIME='" + model.TESTTIME + "' ");
                }
                if (model.OPERATOR != null)
                {
                    strSql.Append(" and OPERATOR='" + model.OPERATOR + "' ");
                }
                if (model.PRINTTIMES != null)
                {
                    strSql.Append(" and PRINTTIMES='" + model.PRINTTIMES + "' ");
                }
                if (model.resultfile != null)
                {
                    strSql.Append(" and resultfile='" + model.resultfile + "' ");
                }
                if (model.CHECKTIME != null)
                {
                    strSql.Append(" and CHECKTIME='" + model.CHECKTIME + "' ");
                }
                if (model.REQUESTSOURCE != null)
                {
                    strSql.Append(" and REQUESTSOURCE='" + model.REQUESTSOURCE + "' ");
                }
                if (model.DIAGNO != null)
                {
                    strSql.Append(" and DIAGNO='" + model.DIAGNO + "' ");
                }
                if (model.SICKTYPENO != null)
                {
                    strSql.Append(" and SICKTYPENO='" + model.SICKTYPENO + "' ");
                }
                if (model.FORMCOMMENT != null)
                {
                    strSql.Append(" and FORMCOMMENT='" + model.FORMCOMMENT + "' ");
                }
                if (model.ARTIFICERORDER != null)
                {
                    strSql.Append(" and ARTIFICERORDER='" + model.ARTIFICERORDER + "' ");
                }
                if (model.ONLINETIME != null)
                {
                    strSql.Append(" and ONLINETIME='" + model.ONLINETIME + "' ");
                }
                if (model.INCEPTTIME != null)
                {
                    strSql.Append(" and INCEPTTIME='" + model.INCEPTTIME + "' ");
                }
                if (model.INCEPTDATE != null)
                {
                    strSql.Append(" and INCEPTDATE='" + model.INCEPTDATE + "' ");
                }
                if (model.BED != null)
                {
                    strSql.Append(" and BED='" + model.BED + "' ");
                }
                if (model.PATNO != null)
                {
                    strSql.Append(" and PATNO='" + model.PATNO + "' ");
                }
                if (model.GENDERNAME != null)
                {
                    strSql.Append(" and GENDERNAME='" + model.GENDERNAME + "' ");
                }
                if (model.DEPTNAME != null)
                {
                    strSql.Append(" and DEPTNAME='" + model.DEPTNAME + "' ");
                }
                if (model.SAMPLETYPENAME != null)
                {
                    strSql.Append(" and SAMPLETYPENAME='" + model.SAMPLETYPENAME + "' ");
                }
                if (model.AGE != null)
                {
                    strSql.Append(" and AGE='" + model.AGE + "' ");
                }
                if (model.OPERDATE != null)
                {
                    strSql.Append(" and OPERDATE='" + model.OPERDATE + "' ");
                }
                if (model.OPERTIME != null)
                {
                    strSql.Append(" and OPERTIME='" + model.OPERTIME + "' ");
                }
                if (model.DOCTORNAME != null)
                {
                    strSql.Append(" and DOCTORNAME='" + model.DOCTORNAME + "' ");
                }
                if (model.COLLECTER != null)
                {
                    strSql.Append(" and COLLECTER='" + model.COLLECTER + "' ");
                }
                if (model.ReportValueMSG != null)
                {
                    strSql.Append(" and ReportValueMSG='" + model.ReportValueMSG + "' ");
                }
                if (model.COLLECTDATE != null)
                {
                    strSql.Append(" and COLLECTDATE='" + model.COLLECTDATE + "' ");
                }
                if (model.Checkstartdate != null)
                {
                    strSql.Append(" and CHECKDATE>='" + model.Checkstartdate.Value.ToShortDateString() + "' ");
                }

                if (model.Checkenddate != null)
                {
                    strSql.Append(" and CHECKDATE<='" + model.Checkenddate.Value.ToShortDateString() + "' ");
                }
                if (model.PREC != null)
                {
                    strSql.Append(" and PREC='" + model.PREC + "' ");
                }
                if (model.itemunit != null)
                {
                    strSql.Append(" and itemunit='" + model.itemunit + "' ");
                }
                if (model.secretgrade != null)
                {
                    strSql.Append(" and secretgrade='" + model.secretgrade + "' ");
                }
                if (model.itemename != null)
                {
                    strSql.Append(" and itemename='" + model.itemename + "' ");
                }
                if (model.shortname != null)
                {
                    strSql.Append(" and shortname='" + model.shortname + "' ");
                }
                if (model.shortcode != null)
                {
                    strSql.Append(" and shortcode='" + model.shortcode + "' ");
                }
                if (model.cuegrade != null)
                {
                    strSql.Append(" and cuegrade='" + model.cuegrade + "' ");
                }
                if (model.AGEUNITNAME != null)
                {
                    strSql.Append(" and AGEUNITNAME='" + model.AGEUNITNAME + "' ");
                }
                if (model.DISTRICTNAME != null)
                {
                    strSql.Append(" and DISTRICTNAME='" + model.DISTRICTNAME + "' ");
                }
                if (model.WARDNAME != null)
                {
                    strSql.Append(" and WARDNAME='" + model.WARDNAME + "' ");
                }
                if (model.FOLKNAME != null)
                {
                    strSql.Append(" and FOLKNAME='" + model.FOLKNAME + "' ");
                }
                if (model.SICKTYPENAME != null)
                {
                    strSql.Append(" and SICKTYPENAME='" + model.SICKTYPENAME + "' ");
                }
                if (model.SECTIONNAME != null)
                {
                    strSql.Append(" and SECTIONNAME='" + model.SECTIONNAME + "' ");
                }
                if (model.TESTTYPENAME != null)
                {
                    strSql.Append(" and TESTTYPENAME='" + model.TESTTYPENAME + "' ");
                }
                if (model.SAMPLETYPENO != null)
                {
                    strSql.Append(" and SAMPLETYPENO='" + model.SAMPLETYPENO + "' ");
                }
                if (model.GENDERNO != null)
                {
                    strSql.Append(" and GENDERNO='" + model.GENDERNO + "' ");
                }
                if (model.BIRTHDAY != null)
                {
                    strSql.Append(" and BIRTHDAY='" + model.BIRTHDAY + "' ");
                }
                if (model.AGEUNITNO != null)
                {
                    strSql.Append(" and AGEUNITNO='" + model.AGEUNITNO + "' ");
                }
                if (model.FOLKNO != null)
                {
                    strSql.Append(" and FOLKNO='" + model.FOLKNO + "' ");
                }
                if (model.DISTRICTNO != null)
                {
                    strSql.Append(" and DISTRICTNO='" + model.DISTRICTNO + "' ");
                }
                if (model.WARDNO != null)
                {
                    strSql.Append(" and WARDNO='" + model.WARDNO + "' ");
                }
                if (model.DEPTNO != null)
                {
                    strSql.Append(" and DEPTNO='" + model.DEPTNO + "' ");
                }
                if (model.DOCTOR != null)
                {
                    strSql.Append(" and DOCTOR='" + model.DOCTOR + "' ");
                }
                if (model.CHARGE != null)
                {
                    strSql.Append(" and CHARGE='" + model.CHARGE + "' ");
                }
                if (model.CHARGENO != null)
                {
                    strSql.Append(" and CHARGENO='" + model.CHARGENO + "' ");
                }
                if (model.ItemRanNum != null)
                {
                    strSql.Append(" and ItemRanNum='" + model.ItemRanNum + "' ");
                }
                if (model.TESTITEMSNAME != null)
                {
                    strSql.Append(" and TESTITEMSNAME='" + model.TESTITEMSNAME + "' ");
                }
                if (model.RECEIVEDATE != null)
                {
                    strSql.Append(" and RECEIVEDATE='" + model.RECEIVEDATE + "' ");
                }
                if (model.SECTIONNO != null)
                {
                    strSql.Append(" and SECTIONNO='" + model.SECTIONNO + "' ");
                }
                if (model.TESTTYPENO != null)
                {
                    strSql.Append(" and TESTTYPENO='" + model.TESTTYPENO + "' ");
                }
                if (model.SAMPLENO != null)
                {
                    strSql.Append(" and SAMPLENO='" + model.SAMPLENO + "' ");
                }
                if (model.PARITEMNO != null)
                {
                    strSql.Append(" and PARITEMNO='" + model.PARITEMNO + "' ");
                }
                if (model.ITEMNO != null)
                {
                    strSql.Append(" and ITEMNO='" + model.ITEMNO + "' ");
                }
                if (model.ORIGINALVALUE != null)
                {
                    strSql.Append(" and ORIGINALVALUE='" + model.ORIGINALVALUE + "' ");
                }
                if (model.REPORTVALUE != null)
                {
                    strSql.Append(" and REPORTVALUE='" + model.REPORTVALUE + "' ");
                }
                if (model.ORIGINALDESC != null)
                {
                    strSql.Append(" and ORIGINALDESC='" + model.ORIGINALDESC + "' ");
                }
                if (model.REPORTDESC != null)
                {
                    strSql.Append(" and REPORTDESC='" + model.REPORTDESC + "' ");
                }
                if (model.STATUSNO != null)
                {
                    strSql.Append(" and STATUSNO='" + model.STATUSNO + "' ");
                }
                if (model.EQUIPNO != null)
                {
                    strSql.Append(" and EQUIPNO='" + model.EQUIPNO + "' ");
                }
                if (model.MODIFIED != null)
                {
                    strSql.Append(" and MODIFIED='" + model.MODIFIED + "' ");
                }
                if (model.REFRANGE != null)
                {
                    strSql.Append(" and REFRANGE='" + model.REFRANGE + "' ");
                }
                if (model.ITEMDATE != null)
                {
                    strSql.Append(" and ITEMDATE='" + model.ITEMDATE + "' ");
                }
                if (model.ITEMTIME != null)
                {
                    strSql.Append(" and ITEMTIME='" + model.ITEMTIME + "' ");
                }
                if (model.ISMATCH != null)
                {
                    strSql.Append(" and ISMATCH='" + model.ISMATCH + "' ");
                }
                if (model.RESULTSTATUS != null)
                {
                    strSql.Append(" and RESULTSTATUS='" + model.RESULTSTATUS + "' ");
                }
                if (model.TESTITEMDATETIME != null)
                {
                    strSql.Append(" and TESTITEMDATETIME='" + model.TESTITEMDATETIME + "' ");
                }
                if (model.REPORTVALUEALL != null)
                {
                    strSql.Append(" and REPORTVALUEALL='" + model.REPORTVALUEALL + "' ");
                }
                if (model.PARITEMNAME != null)
                {
                    strSql.Append(" and PARITEMNAME='" + model.PARITEMNAME + "' ");
                }
                if (model.PARITEMSNAME != null)
                {
                    strSql.Append(" and PARITEMSNAME='" + model.PARITEMSNAME + "' ");
                }
                if (model.DISPORDER != null)
                {
                    strSql.Append(" and DISPORDER='" + model.DISPORDER + "' ");
                }
                if (model.ITEMORDER != null)
                {
                    strSql.Append(" and ITEMORDER='" + model.ITEMORDER + "' ");
                }
                if (model.UNIT != null)
                {
                    strSql.Append(" and UNIT='" + model.UNIT + "' ");
                }
                if (model.SERIALNO != null)
                {
                    strSql.Append(" and SERIALNO like'%" + model.SERIALNO + "%' ");
                }
                if (model.ZDY1 != null)
                {
                    strSql.Append(" and ZDY1='" + model.ZDY1 + "' ");
                }
                if (model.ZDY2 != null)
                {
                    strSql.Append(" and ZDY2='" + model.ZDY2 + "' ");
                }
                if (model.ZDY3 != null)
                {
                    strSql.Append(" and ZDY3='" + model.ZDY3 + "' ");
                }
                if (model.ZDY4 != null)
                {
                    strSql.Append(" and ZDY4='" + model.ZDY4 + "' ");
                }
                if (model.ZDY5 != null)
                {
                    strSql.Append(" and ZDY5='" + model.ZDY5 + "' ");
                }
                if (model.HISORDERNO != null)
                {
                    strSql.Append(" and HISORDERNO='" + model.HISORDERNO + "' ");
                }
                if (model.FORMNO != null)
                {
                    strSql.Append(" and FORMNO=" + model.FORMNO + " ");
                }
                if (model.TECHNICIAN != null)
                {
                    strSql.Append(" and TECHNICIAN=" + model.TECHNICIAN + " ");
                }
                if (model.OLDSERIALNO != null)
                {
                    strSql.Append(" and OLDSERIALNO=" + model.OLDSERIALNO + " ");
                }
                if (model.ReportFormID != null)
                {
                    strSql.Append(" and ReportFormID='" + model.ReportFormID + "' ");
                }
                if (model.CNAME != null)
                {
                    strSql.Append(" and CNAME like '%" + model.CNAME + "%' ");
                }
                if (model.clientzdy3 != null)
                {
                    strSql.Append(" and clientzdy3='" + model.clientzdy3 + "' ");
                }
                if (model.NOperDate != null)
                {
                    strSql.Append(" and NOperDate='" + model.NOperDate + "' ");
                }
                if (model.NOPERTIME != null)
                {
                    strSql.Append(" and NOPERTIME='" + model.NOPERTIME + "' ");
                }
                if (model.ReportItemID != null)
                {
                    strSql.Append(" and ReportItemID=" + model.ReportItemID + " ");
                }
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception ex) { ZhiFang.Common.Log.Log.Info("查询VIEW_ReportItemFull出错：" + ex.Message.ToString()); }
            return null;
        }

        public DataSet GetViewItemFull(string reportformid)
        {
            //string[] strarray = reportformid.Split('@');

            SqlParameter sp0 = new SqlParameter("@where", SqlDbType.VarChar, 500);
            //SqlParameter sp1 = new SqlParameter("@reportformid", SqlDbType.VarChar, 200);
            //SqlParameter sp2 = new SqlParameter("@reportformid", SqlDbType.VarChar, 200);
            //SqlParameter sp3 = new SqlParameter("@reportformid", SqlDbType.VarChar, 200);
            //SqlParameter sp4 = new SqlParameter("@reportformid", SqlDbType.VarChar, 200);
            //SqlParameter sp5 = new SqlParameter("@reportformid", SqlDbType.VarChar, 200);
            //SqlParameter sp6 = new SqlParameter("@reportformid", SqlDbType.VarChar, 200);
            //SqlParameter sp7 = new SqlParameter("@reportformid", SqlDbType.VarChar, 200);
            sp0.Value += reportformid;
            //sp0.Value += "'" + strarray[1] + "'";
            //sp0.Value += "'" + strarray[2] + "'";
            //sp0.Value += "'" + strarray[3] + "'";
            //sp0.Value += "'" + strarray[4] + "'";
            //sp0.Value += "'" + strarray[5] + "'";
            //sp0.Value += "'" + strarray[6] + "'";
            //sp0.Value += "'" + strarray[7] + "'";


            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Excel_ReportItemFull", new SqlParameter[] { sp0 });
            return ds;
        }
    }
}
