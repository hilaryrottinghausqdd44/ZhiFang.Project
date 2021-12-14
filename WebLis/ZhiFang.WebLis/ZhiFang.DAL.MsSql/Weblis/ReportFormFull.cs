using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ZhiFang.DAL.MsSql.Weblis
{
    /// <summary>
    /// 数据访问类:ReportFormFull
    /// </summary>
    public partial class ReportFormFull : BaseDALLisDB, IDReportFormFull
    {
        public ReportFormFull(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public ReportFormFull()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
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
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.ReportFormFull model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ReportFormID != null)
            {
                strSql1.Append("ReportFormID,");
                strSql2.Append("'" + model.ReportFormID + "',");
            }
            if (model.CLIENTNO != null)
            {
                strSql1.Append("CLIENTNO,");
                strSql2.Append("'" + model.CLIENTNO + "',");
            }
            if (model.CNAME != null)
            {
                strSql1.Append("CNAME,");
                strSql2.Append("'" + model.CNAME + "',");
            }
            if (model.AGEUNITNAME != null)
            {
                strSql1.Append("AGEUNITNAME,");
                strSql2.Append("'" + model.AGEUNITNAME + "',");
            }
            if (model.GENDERNAME != null)
            {
                strSql1.Append("GENDERNAME,");
                strSql2.Append("'" + model.GENDERNAME + "',");
            }
            if (model.DEPTNAME != null)
            {
                strSql1.Append("DEPTNAME,");
                strSql2.Append("'" + model.DEPTNAME + "',");
            }
            if (model.DOCTORNAME != null)
            {
                strSql1.Append("DOCTORNAME,");
                strSql2.Append("'" + model.DOCTORNAME + "',");
            }
            if (model.DISTRICTNAME != null)
            {
                strSql1.Append("DISTRICTNAME,");
                strSql2.Append("'" + model.DISTRICTNAME + "',");
            }
            if (model.WARDNAME != null)
            {
                strSql1.Append("WARDNAME,");
                strSql2.Append("'" + model.WARDNAME + "',");
            }
            if (model.FOLKNAME != null)
            {
                strSql1.Append("FOLKNAME,");
                strSql2.Append("'" + model.FOLKNAME + "',");
            }
            if (model.SICKTYPENAME != null)
            {
                strSql1.Append("SICKTYPENAME,");
                strSql2.Append("'" + model.SICKTYPENAME + "',");
            }
            if (model.SAMPLETYPENAME != null)
            {
                strSql1.Append("SAMPLETYPENAME,");
                strSql2.Append("'" + model.SAMPLETYPENAME + "',");
            }
            if (model.SECTIONNAME != null)
            {
                strSql1.Append("SECTIONNAME,");
                strSql2.Append("'" + model.SECTIONNAME + "',");
            }
            if (model.TESTTYPENAME != null)
            {
                strSql1.Append("TESTTYPENAME,");
                strSql2.Append("'" + model.TESTTYPENAME + "',");
            }
            if (model.RECEIVEDATE != null)
            {
                strSql1.Append("RECEIVEDATE,");
                strSql2.Append("'" + model.RECEIVEDATE + "',");
            }
            if (model.SECTIONNO != null)
            {
                strSql1.Append("SECTIONNO,");
                strSql2.Append("'" + model.SECTIONNO + "',");
            }
            if (model.TESTTYPENO != null)
            {
                strSql1.Append("TESTTYPENO,");
                strSql2.Append("'" + model.TESTTYPENO + "',");
            }
            if (model.SAMPLENO != null)
            {
                strSql1.Append("SAMPLENO,");
                strSql2.Append("'" + model.SAMPLENO + "',");
            }
            if (model.STATUSNO != null)
            {
                strSql1.Append("STATUSNO,");
                strSql2.Append("" + model.STATUSNO + ",");
            }
            if (model.SAMPLETYPENO != null)
            {
                strSql1.Append("SAMPLETYPENO,");
                strSql2.Append("" + model.SAMPLETYPENO + ",");
            }
            if (model.PATNO != null)
            {
                strSql1.Append("PATNO,");
                strSql2.Append("'" + model.PATNO + "',");
            }

            if (model.GENDERNO != null)
            {
                strSql1.Append("GENDERNO,");
                strSql2.Append("" + model.GENDERNO + ",");
            }
            if (model.BIRTHDAY != null)
            {
                strSql1.Append("BIRTHDAY,");
                strSql2.Append("'" + model.BIRTHDAY + "',");
            }
            if (model.AGE != null)
            {
                strSql1.Append("AGE,");
                strSql2.Append("'" + model.AGE + "',");
            }
            if (model.AGEUNITNO != null)
            {
                strSql1.Append("AGEUNITNO,");
                strSql2.Append("" + model.AGEUNITNO + ",");
            }
            if (model.FOLKNO != null)
            {
                strSql1.Append("FOLKNO,");
                strSql2.Append("'" + model.FOLKNO + "',");
            }
            if (model.DISTRICTNO != null)
            {
                strSql1.Append("DISTRICTNO,");
                strSql2.Append("'" + model.DISTRICTNO + "',");
            }
            if (model.WARDNO != null)
            {
                strSql1.Append("WARDNO,");
                strSql2.Append("'" + model.WARDNO + "',");
            }
            if (model.BED != null)
            {
                strSql1.Append("BED,");
                strSql2.Append("'" + model.BED + "',");
            }
            if (model.DEPTNO != null)
            {
                strSql1.Append("DEPTNO,");
                strSql2.Append("" + model.DEPTNO + ",");
            }
            if (model.DOCTOR != null)
            {
                strSql1.Append("DOCTOR,");
                strSql2.Append("'" + model.DOCTOR + "',");
            }
            if (model.CHARGENO != null)
            {
                strSql1.Append("CHARGENO,");
                strSql2.Append("'" + model.CHARGENO + "',");
            }
            if (model.CHARGE != null)
            {
                strSql1.Append("CHARGE,");
                strSql2.Append("'" + model.CHARGE + "',");
            }
            if (model.COLLECTER != null)
            {
                strSql1.Append("COLLECTER,");
                strSql2.Append("'" + model.COLLECTER + "',");
            }
            if (model.COLLECTDATE != null)
            {
                strSql1.Append("COLLECTDATE,");
                strSql2.Append("'" + model.COLLECTDATE + "',");
            }
            if (model.COLLECTTIME != null)
            {
                strSql1.Append("COLLECTTIME,");
                strSql2.Append("'" + model.COLLECTTIME + "',");
            }
            if (model.FORMMEMO != null)
            {
                strSql1.Append("FORMMEMO,");
                strSql2.Append("'" + model.FORMMEMO + "',");
            }
            if (model.TECHNICIAN != null)
            {
                strSql1.Append("TECHNICIAN,");
                strSql2.Append("'" + model.TECHNICIAN + "',");
            }
            if (model.TESTDATE != null)
            {
                strSql1.Append("TESTDATE,");
                strSql2.Append("'" + model.TESTDATE + "',");
            }
            if (model.TESTTIME != null)
            {
                strSql1.Append("TESTTIME,");
                strSql2.Append("'" + model.TESTTIME + "',");
            }
            if (model.OPERATOR != null)
            {
                strSql1.Append("OPERATOR,");
                strSql2.Append("'" + model.OPERATOR + "',");
            }
            if (model.OPERDATE != null)
            {
                strSql1.Append("OPERDATE,");
                strSql2.Append("'" + model.OPERDATE + "',");
            }
            if (model.OPERTIME != null)
            {
                strSql1.Append("OPERTIME,");
                strSql2.Append("'" + model.OPERTIME + "',");
            }
            if (model.CHECKER != null)
            {
                strSql1.Append("CHECKER,");
                strSql2.Append("'" + model.CHECKER + "',");
            }
            if (model.PRINTTIMES != null)
            {
                strSql1.Append("PRINTTIMES,");
                strSql2.Append("" + model.PRINTTIMES + ",");
            }
            if (model.resultfile != null)
            {
                strSql1.Append("resultfile,");
                strSql2.Append("'" + model.resultfile + "',");
            }
            if (model.CHECKDATE != null)
            {
                strSql1.Append("CHECKDATE,");
                strSql2.Append("'" + model.CHECKDATE + "',");
            }
            if (model.CHECKTIME != null)
            {
                strSql1.Append("CHECKTIME,");
                strSql2.Append("'" + model.CHECKTIME + "',");
            }
            if (model.SERIALNO != null)
            {
                strSql1.Append("SERIALNO,");
                strSql2.Append("'" + model.SERIALNO + "',");
            }
            if (model.REQUESTSOURCE != null)
            {
                strSql1.Append("REQUESTSOURCE,");
                strSql2.Append("'" + model.REQUESTSOURCE + "',");
            }
            if (model.DIAGNO != null)
            {
                strSql1.Append("DIAGNO,");
                strSql2.Append("'" + model.DIAGNO + "',");
            }
            if (model.SICKTYPENO != null)
            {
                strSql1.Append("SICKTYPENO,");
                strSql2.Append("'" + model.SICKTYPENO + "',");
            }
            if (model.FORMCOMMENT != null)
            {
                strSql1.Append("FORMCOMMENT,");
                strSql2.Append("'" + model.FORMCOMMENT + "',");
            }
            if (model.ARTIFICERORDER != null)
            {
                strSql1.Append("ARTIFICERORDER,");
                strSql2.Append("'" + model.ARTIFICERORDER + "',");
            }
            if (model.SICKORDER != null)
            {
                strSql1.Append("SICKORDER,");
                strSql2.Append("'" + model.SICKORDER + "',");
            }
            if (model.SICKTYPE != null)
            {
                strSql1.Append("SICKTYPE,");
                strSql2.Append("'" + model.SICKTYPE + "',");
            }
            if (model.CHARGEFLAG != null)
            {
                strSql1.Append("CHARGEFLAG,");
                strSql2.Append("'" + model.CHARGEFLAG + "',");
            }
            if (model.TESTDEST != null)
            {
                strSql1.Append("TESTDEST,");
                strSql2.Append("'" + model.TESTDEST + "',");
            }
            if (model.SLABLE != null)
            {
                strSql1.Append("SLABLE,");
                strSql2.Append("'" + model.SLABLE + "',");
            }
            if (model.ZDY1 != null)
            {
                strSql1.Append("ZDY1,");
                strSql2.Append("'" + model.ZDY1 + "',");
            }
            if (model.ZDY2 != null)
            {
                strSql1.Append("ZDY2,");
                strSql2.Append("'" + model.ZDY2 + "',");
            }
            if (model.ZDY3 != null)
            {
                strSql1.Append("ZDY3,");
                strSql2.Append("'" + model.ZDY3 + "',");
            }
            if (model.ZDY4 != null)
            {
                strSql1.Append("ZDY4,");
                strSql2.Append("'" + model.ZDY4 + "',");
            }
            if (model.ZDY5 != null)
            {
                strSql1.Append("ZDY5,");
                strSql2.Append("'" + model.ZDY5 + "',");
            }
            if (model.INCEPTDATE != null)
            {
                strSql1.Append("INCEPTDATE,");
                strSql2.Append("'" + model.INCEPTDATE + "',");
            }
            if (model.INCEPTTIME != null)
            {
                strSql1.Append("INCEPTTIME,");
                strSql2.Append("'" + model.INCEPTTIME + "',");
            }
            if (model.INCEPTER != null)
            {
                strSql1.Append("INCEPTER,");
                strSql2.Append("'" + model.INCEPTER + "',");
            }
            if (model.ONLINEDATE != null)
            {
                strSql1.Append("ONLINEDATE,");
                strSql2.Append("'" + model.ONLINEDATE + "',");
            }
            if (model.ONLINETIME != null)
            {
                strSql1.Append("ONLINETIME,");
                strSql2.Append("'" + model.ONLINETIME + "',");
            }
            if (model.BMANNO != null)
            {
                strSql1.Append("BMANNO,");
                strSql2.Append("'" + model.BMANNO + "',");
            }
            if (model.FILETYPE != null)
            {
                strSql1.Append("FILETYPE,");
                strSql2.Append("'" + model.FILETYPE + "',");
            }
            if (model.JPGFILE != null)
            {
                strSql1.Append("JPGFILE,");
                strSql2.Append("'" + model.JPGFILE + "',");
            }
            if (model.PDFFILE != null)
            {
                strSql1.Append("PDFFILE,");
                strSql2.Append("'" + model.PDFFILE + "',");
            }
            if (model.FORMNO != null)
            {
                strSql1.Append("FORMNO,");
                strSql2.Append("" + model.FORMNO + ",");
            }
            if (model.CHILDTABLENAME != null)
            {
                strSql1.Append("CHILDTABLENAME,");
                strSql2.Append("'" + model.CHILDTABLENAME + "',");
            }
            if (model.PRINTEXEC != null)
            {
                strSql1.Append("PRINTEXEC,");
                strSql2.Append("'" + model.PRINTEXEC + "',");
            }
            if (model.LABCENTER != null)
            {
                strSql1.Append("LABCENTER,");
                strSql2.Append("'" + model.LABCENTER + "',");
            }
            if (model.PRINTTEXEC != null)
            {
                strSql1.Append("PRINTTEXEC,");
                strSql2.Append("'" + model.PRINTTEXEC + "',");
            }
            strSql.Append("insert into ReportFormFull(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.ReportFormFull model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ReportFormFull set ");
            if (model.ReportFormID != null && model.ReportFormID != "")
            {
                strSql.Append("ReportFormID='" + model.ReportFormID + "',");
            }
            if (model.FOLKNAME != null)
            {
                strSql.Append("FOLKNAME='" + model.FOLKNAME + "',");
            }
            if (model.SICKTYPENAME != null)
            {
                strSql.Append("SICKTYPENAME='" + model.SICKTYPENAME + "',");
            }
            if (model.SAMPLETYPENAME != null)
            {
                strSql.Append("SAMPLETYPENAME='" + model.SAMPLETYPENAME + "',");
            }
            if (model.SECTIONNAME != null)
            {
                strSql.Append("SECTIONNAME='" + model.SECTIONNAME + "',");
            }
            if (model.TESTTYPENAME != null)
            {
                strSql.Append("TESTTYPENAME='" + model.TESTTYPENAME + "',");
            }
            if (model.RECEIVEDATE != null)
            {
                strSql.Append("RECEIVEDATE='" + model.RECEIVEDATE + "',");
            }
            if (model.SECTIONNO != null)
            {
                strSql.Append("SECTIONNO='" + model.SECTIONNO + "',");
            }
            if (model.TESTTYPENO != null)
            {
                strSql.Append("TESTTYPENO='" + model.TESTTYPENO + "',");
            }
            if (model.SAMPLENO != null)
            {
                strSql.Append("SAMPLENO='" + model.SAMPLENO + "',");
            }
            if (model.STATUSNO != null)
            {
                strSql.Append("STATUSNO=" + model.STATUSNO + ",");
            }
            if (model.CLIENTNO != null)
            {
                strSql.Append("CLIENTNO='" + model.CLIENTNO + "',");
            }
            if (model.SAMPLETYPENO != null)
            {
                strSql.Append("SAMPLETYPENO=" + model.SAMPLETYPENO + ",");
            }
            if (model.PATNO != null)
            {
                strSql.Append("PATNO='" + model.PATNO + "',");
            }
            if (model.GENDERNO != null)
            {
                strSql.Append("GENDERNO=" + model.GENDERNO + ",");
            }
            if (model.BIRTHDAY != null)
            {
                strSql.Append("BIRTHDAY='" + model.BIRTHDAY + "',");
            }
            if (model.AGE != null)
            {
                strSql.Append("AGE='" + model.AGE + "',");
            }
            if (model.AGEUNITNO != null)
            {
                strSql.Append("AGEUNITNO=" + model.AGEUNITNO + ",");
            }
            if (model.FOLKNO != null)
            {
                strSql.Append("FOLKNO='" + model.FOLKNO + "',");
            }
            if (model.DISTRICTNO != null)
            {
                strSql.Append("DISTRICTNO='" + model.DISTRICTNO + "',");
            }
            if (model.WARDNO != null)
            {
                strSql.Append("WARDNO='" + model.WARDNO + "',");
            }
            if (model.BED != null)
            {
                strSql.Append("BED='" + model.BED + "',");
            }
            if (model.CNAME != null)
            {
                strSql.Append("CNAME='" + model.CNAME + "',");
            }
            if (model.DEPTNO != null)
            {
                strSql.Append("DEPTNO=" + model.DEPTNO + ",");
            }
            if (model.DOCTOR != null)
            {
                strSql.Append("DOCTOR='" + model.DOCTOR + "',");
            }
            if (model.CHARGENO != null)
            {
                strSql.Append("CHARGENO='" + model.CHARGENO + "',");
            }
            if (model.CHARGE != null)
            {
                strSql.Append("CHARGE='" + model.CHARGE + "',");
            }
            if (model.COLLECTER != null)
            {
                strSql.Append("COLLECTER='" + model.COLLECTER + "',");
            }
            if (model.COLLECTDATE != null)
            {
                strSql.Append("COLLECTDATE='" + model.COLLECTDATE + "',");
            }
            if (model.COLLECTTIME != null)
            {
                strSql.Append("COLLECTTIME='" + model.COLLECTTIME + "',");
            }
            if (model.FORMMEMO != null)
            {
                strSql.Append("FORMMEMO='" + model.FORMMEMO + "',");
            }
            if (model.TECHNICIAN != null)
            {
                strSql.Append("TECHNICIAN='" + model.TECHNICIAN + "',");
            }
            if (model.TESTDATE != null)
            {
                strSql.Append("TESTDATE='" + model.TESTDATE + "',");
            }
            if (model.AGEUNITNAME != null)
            {
                strSql.Append("AGEUNITNAME='" + model.AGEUNITNAME + "',");
            }
            if (model.TESTTIME != null)
            {
                strSql.Append("TESTTIME='" + model.TESTTIME + "',");
            }
            if (model.OPERATOR != null)
            {
                strSql.Append("OPERATOR='" + model.OPERATOR + "',");
            }
            if (model.OPERDATE != null)
            {
                strSql.Append("OPERDATE='" + model.OPERDATE + "',");
            }
            if (model.OPERTIME != null)
            {
                strSql.Append("OPERTIME='" + model.OPERTIME + "',");
            }
            if (model.CHECKER != null)
            {
                strSql.Append("CHECKER='" + model.CHECKER + "',");
            }
            if (model.PRINTTIMES != null)
            {
                strSql.Append("PRINTTIMES=" + model.PRINTTIMES + ",");
            }
            if (model.resultfile != null)
            {
                strSql.Append("resultfile='" + model.resultfile + "',");
            }
            if (model.CHECKDATE != null)
            {
                strSql.Append("CHECKDATE='" + model.CHECKDATE + "',");
            }
            if (model.CHECKTIME != null)
            {
                strSql.Append("CHECKTIME='" + model.CHECKTIME + "',");
            }
            if (model.SERIALNO != null)
            {
                strSql.Append("SERIALNO='" + model.SERIALNO + "',");
            }
            if (model.GENDERNAME != null)
            {
                strSql.Append("GENDERNAME='" + model.GENDERNAME + "',");
            }
            if (model.REQUESTSOURCE != null)
            {
                strSql.Append("REQUESTSOURCE='" + model.REQUESTSOURCE + "',");
            }
            if (model.DIAGNO != null)
            {
                strSql.Append("DIAGNO='" + model.DIAGNO + "',");
            }
            if (model.SICKTYPENO != null)
            {
                strSql.Append("SICKTYPENO='" + model.SICKTYPENO + "',");
            }
            if (model.FORMCOMMENT != null)
            {
                strSql.Append("FORMCOMMENT='" + model.FORMCOMMENT + "',");
            }
            if (model.ARTIFICERORDER != null)
            {
                strSql.Append("ARTIFICERORDER='" + model.ARTIFICERORDER + "',");
            }
            if (model.SICKORDER != null)
            {
                strSql.Append("SICKORDER='" + model.SICKORDER + "',");
            }
            if (model.SICKTYPE != null)
            {
                strSql.Append("SICKTYPE='" + model.SICKTYPE + "',");
            }
            if (model.CHARGEFLAG != null)
            {
                strSql.Append("CHARGEFLAG='" + model.CHARGEFLAG + "',");
            }
            if (model.TESTDEST != null)
            {
                strSql.Append("TESTDEST='" + model.TESTDEST + "',");
            }
            if (model.SLABLE != null)
            {
                strSql.Append("SLABLE='" + model.SLABLE + "',");
            }
            if (model.DEPTNAME != null)
            {
                strSql.Append("DEPTNAME='" + model.DEPTNAME + "',");
            }
            if (model.ZDY1 != null)
            {
                strSql.Append("ZDY1='" + model.ZDY1 + "',");
            }
            if (model.ZDY2 != null)
            {
                strSql.Append("ZDY2='" + model.ZDY2 + "',");
            }
            if (model.ZDY3 != null)
            {
                strSql.Append("ZDY3='" + model.ZDY3 + "',");
            }
            if (model.ZDY4 != null)
            {
                strSql.Append("ZDY4='" + model.ZDY4 + "',");
            }
            if (model.ZDY5 != null)
            {
                strSql.Append("ZDY5='" + model.ZDY5 + "',");
            }
            if (model.INCEPTDATE != null)
            {
                strSql.Append("INCEPTDATE='" + model.INCEPTDATE + "',");
            }
            if (model.INCEPTTIME != null)
            {
                strSql.Append("INCEPTTIME='" + model.INCEPTTIME + "',");
            }
            if (model.INCEPTER != null)
            {
                strSql.Append("INCEPTER='" + model.INCEPTER + "',");
            }
            if (model.ONLINEDATE != null)
            {
                strSql.Append("ONLINEDATE='" + model.ONLINEDATE + "',");
            }
            if (model.ONLINETIME != null)
            {
                strSql.Append("ONLINETIME='" + model.ONLINETIME + "',");
            }
            if (model.DOCTORNAME != null)
            {
                strSql.Append("DOCTORNAME='" + model.DOCTORNAME + "',");
            }
            if (model.BMANNO != null)
            {
                strSql.Append("BMANNO='" + model.BMANNO + "',");
            }
            if (model.FILETYPE != null)
            {
                strSql.Append("FILETYPE='" + model.FILETYPE + "',");
            }
            if (model.JPGFILE != null)
            {
                strSql.Append("JPGFILE='" + model.JPGFILE + "',");
            }
            if (model.PDFFILE != null)
            {
                strSql.Append("PDFFILE='" + model.PDFFILE + "',");
            }
            if (model.FORMNO != null)
            {
                strSql.Append("FORMNO=" + model.FORMNO + ",");
            }
            if (model.CHILDTABLENAME != null)
            {
                strSql.Append("CHILDTABLENAME='" + model.CHILDTABLENAME + "',");
            }
            if (model.PRINTEXEC != null)
            {
                strSql.Append("PRINTEXEC='" + model.PRINTEXEC + "',");
            }
            if (model.LABCENTER != null)
            {
                strSql.Append("LABCENTER='" + model.LABCENTER + "',");
            }
            if (model.PRINTTEXEC != null)
            {
                strSql.Append("PRINTTEXEC='" + model.PRINTTEXEC + "',");
            }
            if (model.SectionType != null)
            {
                strSql.Append("SECTIONTYPE='" + model.SectionType + "',");
            }
            if (model.DISTRICTNAME != null)
            {
                strSql.Append("DISTRICTNAME='" + model.DISTRICTNAME + "',");
            }
            if (model.BarCode != null)
            {
                strSql.Append("BARCODE='" + model.BarCode + "',");
            }
            if (model.WARDNAME != null)
            {
                strSql.Append("WARDNAME='" + model.WARDNAME + "',");
            }
            if (model.Isdown != null)
            {
                strSql.Append("Isdown='" + model.Isdown + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where 1=1 ");
            if (model.ReportFormID != null && model.ReportFormID != "")
            {
                strSql.Append(" and ReportFormID='" + model.ReportFormID + "'");
            }
            if (model.SERIALNO != null)
            {
                strSql.Append(" and SERIALNO='" + model.SERIALNO + "'");
            }
            if (model.CLIENTNO != null)
            {
                strSql.Append(" and CLIENTNO='" + model.CLIENTNO + "'");
            }
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string ReportFormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ReportFormFull ");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }       /// <summary>
                /// 删除一条数据
                /// </summary>
        public bool DeleteList(string ReportFormIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ReportFormFull ");
            strSql.Append(" where ReportFormID in (" + ReportFormIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
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
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.ReportFormFull GetModel(string ReportFormID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" * ");
            strSql.Append(" from ReportFormFull ");
            strSql.Append(" where ReportFormID='" + ReportFormID + "' ");
            Model.ReportFormFull model = new Model.ReportFormFull();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    if (ds.Tables[0].Rows[0]["ReportFormID"] != null && ds.Tables[0].Rows[0]["ReportFormID"].ToString() != "")
                    {
                        model.ReportFormID = ds.Tables[0].Rows[0]["ReportFormID"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CLIENTNO"] != null && ds.Tables[0].Rows[0]["CLIENTNO"].ToString() != "")
                    {
                        model.CLIENTNO = ds.Tables[0].Rows[0]["CLIENTNO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CLIENTNAME"] != null && ds.Tables[0].Rows[0]["CLIENTNAME"].ToString() != "")
                    {
                        model.CLIENTNAME = ds.Tables[0].Rows[0]["CLIENTNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CNAME"] != null && ds.Tables[0].Rows[0]["CNAME"].ToString() != "")
                    {
                        model.CNAME = ds.Tables[0].Rows[0]["CNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["AGEUNITNAME"] != null && ds.Tables[0].Rows[0]["AGEUNITNAME"].ToString() != "")
                    {
                        model.AGEUNITNAME = ds.Tables[0].Rows[0]["AGEUNITNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["GENDERNAME"] != null && ds.Tables[0].Rows[0]["GENDERNAME"].ToString() != "")
                    {
                        model.GENDERNAME = ds.Tables[0].Rows[0]["GENDERNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DEPTNAME"] != null && ds.Tables[0].Rows[0]["DEPTNAME"].ToString() != "")
                    {
                        model.DEPTNAME = ds.Tables[0].Rows[0]["DEPTNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DOCTORNAME"] != null && ds.Tables[0].Rows[0]["DOCTORNAME"].ToString() != "")
                    {
                        model.DOCTORNAME = ds.Tables[0].Rows[0]["DOCTORNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DISTRICTNAME"] != null && ds.Tables[0].Rows[0]["DISTRICTNAME"].ToString() != "")
                    {
                        model.DISTRICTNAME = ds.Tables[0].Rows[0]["DISTRICTNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["WARDNAME"] != null && ds.Tables[0].Rows[0]["WARDNAME"].ToString() != "")
                    {
                        model.WARDNAME = ds.Tables[0].Rows[0]["WARDNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["FOLKNAME"] != null && ds.Tables[0].Rows[0]["FOLKNAME"].ToString() != "")
                    {
                        model.FOLKNAME = ds.Tables[0].Rows[0]["FOLKNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SICKTYPENAME"] != null && ds.Tables[0].Rows[0]["SICKTYPENAME"].ToString() != "")
                    {
                        model.SICKTYPENAME = ds.Tables[0].Rows[0]["SICKTYPENAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SAMPLETYPENAME"] != null && ds.Tables[0].Rows[0]["SAMPLETYPENAME"].ToString() != "")
                    {
                        model.SAMPLETYPENAME = ds.Tables[0].Rows[0]["SAMPLETYPENAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SECTIONNAME"] != null && ds.Tables[0].Rows[0]["SECTIONNAME"].ToString() != "")
                    {
                        model.SECTIONNAME = ds.Tables[0].Rows[0]["SECTIONNAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["TESTTYPENAME"] != null && ds.Tables[0].Rows[0]["TESTTYPENAME"].ToString() != "")
                    {
                        model.TESTTYPENAME = ds.Tables[0].Rows[0]["TESTTYPENAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["RECEIVEDATE"] != null && ds.Tables[0].Rows[0]["RECEIVEDATE"].ToString() != "")
                    {
                        model.RECEIVEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["RECEIVEDATE"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["SECTIONNO"] != null && ds.Tables[0].Rows[0]["SECTIONNO"].ToString() != "")
                    {
                        model.SECTIONNO = ds.Tables[0].Rows[0]["SECTIONNO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["TESTTYPENO"] != null && ds.Tables[0].Rows[0]["TESTTYPENO"].ToString() != "")
                    {
                        model.TESTTYPENO = ds.Tables[0].Rows[0]["TESTTYPENO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SAMPLENO"] != null && ds.Tables[0].Rows[0]["SAMPLENO"].ToString() != "")
                    {
                        model.SAMPLENO = ds.Tables[0].Rows[0]["SAMPLENO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["STATUSNO"] != null && ds.Tables[0].Rows[0]["STATUSNO"].ToString() != "")
                    {
                        model.STATUSNO = int.Parse(ds.Tables[0].Rows[0]["STATUSNO"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["SAMPLETYPENO"] != null && ds.Tables[0].Rows[0]["SAMPLETYPENO"].ToString() != "")
                    {
                        model.SAMPLETYPENO = int.Parse(ds.Tables[0].Rows[0]["SAMPLETYPENO"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["PATNO"] != null && ds.Tables[0].Rows[0]["PATNO"].ToString() != "")
                    {
                        model.PATNO = ds.Tables[0].Rows[0]["PATNO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["GENDERNO"] != null && ds.Tables[0].Rows[0]["GENDERNO"].ToString() != "")
                    {
                        model.GENDERNO = int.Parse(ds.Tables[0].Rows[0]["GENDERNO"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["BIRTHDAY"] != null && ds.Tables[0].Rows[0]["BIRTHDAY"].ToString() != "")
                    {
                        model.BIRTHDAY = DateTime.Parse(ds.Tables[0].Rows[0]["BIRTHDAY"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["AGE"] != null && ds.Tables[0].Rows[0]["AGE"].ToString() != "")
                    {
                        model.AGE = ds.Tables[0].Rows[0]["AGE"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["AGEUNITNO"] != null && ds.Tables[0].Rows[0]["AGEUNITNO"].ToString() != "")
                    {
                        model.AGEUNITNO = int.Parse(ds.Tables[0].Rows[0]["AGEUNITNO"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["FOLKNO"] != null && ds.Tables[0].Rows[0]["FOLKNO"].ToString() != "")
                    {
                        model.FOLKNO = ds.Tables[0].Rows[0]["FOLKNO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DISTRICTNO"] != null && ds.Tables[0].Rows[0]["DISTRICTNO"].ToString() != "")
                    {
                        model.DISTRICTNO = ds.Tables[0].Rows[0]["DISTRICTNO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["WARDNO"] != null && ds.Tables[0].Rows[0]["WARDNO"].ToString() != "")
                    {
                        model.WARDNO = ds.Tables[0].Rows[0]["WARDNO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["BED"] != null && ds.Tables[0].Rows[0]["BED"].ToString() != "")
                    {
                        model.BED = ds.Tables[0].Rows[0]["BED"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DEPTNO"] != null && ds.Tables[0].Rows[0]["DEPTNO"].ToString() != "")
                    {
                        model.DEPTNO = int.Parse(ds.Tables[0].Rows[0]["DEPTNO"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["DOCTOR"] != null && ds.Tables[0].Rows[0]["DOCTOR"].ToString() != "")
                    {
                        model.DOCTOR = ds.Tables[0].Rows[0]["DOCTOR"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CHARGENO"] != null && ds.Tables[0].Rows[0]["CHARGENO"].ToString() != "")
                    {
                        model.CHARGENO = ds.Tables[0].Rows[0]["CHARGENO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CHARGE"] != null && ds.Tables[0].Rows[0]["CHARGE"].ToString() != "")
                    {
                        model.CHARGE = ds.Tables[0].Rows[0]["CHARGE"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["COLLECTER"] != null && ds.Tables[0].Rows[0]["COLLECTER"].ToString() != "")
                    {
                        model.COLLECTER = ds.Tables[0].Rows[0]["COLLECTER"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["COLLECTDATE"] != null && ds.Tables[0].Rows[0]["COLLECTDATE"].ToString() != "")
                    {
                        model.COLLECTDATE = DateTime.Parse(ds.Tables[0].Rows[0]["COLLECTDATE"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["COLLECTTIME"] != null && ds.Tables[0].Rows[0]["COLLECTTIME"].ToString() != "")
                    {
                        model.COLLECTTIME = DateTime.Parse(ds.Tables[0].Rows[0]["COLLECTTIME"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["FORMMEMO"] != null && ds.Tables[0].Rows[0]["FORMMEMO"].ToString() != "")
                    {
                        model.FORMMEMO = ds.Tables[0].Rows[0]["FORMMEMO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["TECHNICIAN"] != null && ds.Tables[0].Rows[0]["TECHNICIAN"].ToString() != "")
                    {
                        model.TECHNICIAN = ds.Tables[0].Rows[0]["TECHNICIAN"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["TESTDATE"] != null && ds.Tables[0].Rows[0]["TESTDATE"].ToString() != "")
                    {
                        model.TESTDATE = DateTime.Parse(ds.Tables[0].Rows[0]["TESTDATE"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["TESTTIME"] != null && ds.Tables[0].Rows[0]["TESTTIME"].ToString() != "")
                    {
                        model.TESTTIME = DateTime.Parse(ds.Tables[0].Rows[0]["TESTTIME"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["OPERATOR"] != null && ds.Tables[0].Rows[0]["OPERATOR"].ToString() != "")
                    {
                        model.OPERATOR = ds.Tables[0].Rows[0]["OPERATOR"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["OPERDATE"] != null && ds.Tables[0].Rows[0]["OPERDATE"].ToString() != "")
                    {
                        model.OPERDATE = DateTime.Parse(ds.Tables[0].Rows[0]["OPERDATE"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["OPERTIME"] != null && ds.Tables[0].Rows[0]["OPERTIME"].ToString() != "")
                    {
                        model.OPERTIME = DateTime.Parse(ds.Tables[0].Rows[0]["OPERTIME"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["CHECKER"] != null && ds.Tables[0].Rows[0]["CHECKER"].ToString() != "")
                    {
                        model.CHECKER = ds.Tables[0].Rows[0]["CHECKER"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["PRINTTIMES"] != null && ds.Tables[0].Rows[0]["PRINTTIMES"].ToString() != "")
                    {
                        model.PRINTTIMES = int.Parse(ds.Tables[0].Rows[0]["PRINTTIMES"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["resultfile"] != null && ds.Tables[0].Rows[0]["resultfile"].ToString() != "")
                    {
                        model.resultfile = ds.Tables[0].Rows[0]["resultfile"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CHECKDATE"] != null && ds.Tables[0].Rows[0]["CHECKDATE"].ToString() != "")
                    {
                        model.CHECKDATE = DateTime.Parse(ds.Tables[0].Rows[0]["CHECKDATE"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["CHECKTIME"] != null && ds.Tables[0].Rows[0]["CHECKTIME"].ToString() != "")
                    {
                        model.CHECKTIME = DateTime.Parse(ds.Tables[0].Rows[0]["CHECKTIME"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["SERIALNO"] != null && ds.Tables[0].Rows[0]["SERIALNO"].ToString() != "")
                    {
                        model.SERIALNO = ds.Tables[0].Rows[0]["SERIALNO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["REQUESTSOURCE"] != null && ds.Tables[0].Rows[0]["REQUESTSOURCE"].ToString() != "")
                    {
                        model.REQUESTSOURCE = ds.Tables[0].Rows[0]["REQUESTSOURCE"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DIAGNO"] != null && ds.Tables[0].Rows[0]["DIAGNO"].ToString() != "")
                    {
                        model.DIAGNO = ds.Tables[0].Rows[0]["DIAGNO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SICKTYPENO"] != null && ds.Tables[0].Rows[0]["SICKTYPENO"].ToString() != "")
                    {
                        model.SICKTYPENO = ds.Tables[0].Rows[0]["SICKTYPENO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["FORMCOMMENT"] != null && ds.Tables[0].Rows[0]["FORMCOMMENT"].ToString() != "")
                    {
                        model.FORMCOMMENT = ds.Tables[0].Rows[0]["FORMCOMMENT"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ARTIFICERORDER"] != null && ds.Tables[0].Rows[0]["ARTIFICERORDER"].ToString() != "")
                    {
                        model.ARTIFICERORDER = ds.Tables[0].Rows[0]["ARTIFICERORDER"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SICKORDER"] != null && ds.Tables[0].Rows[0]["SICKORDER"].ToString() != "")
                    {
                        model.SICKORDER = ds.Tables[0].Rows[0]["SICKORDER"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SICKTYPE"] != null && ds.Tables[0].Rows[0]["SICKTYPE"].ToString() != "")
                    {
                        model.SICKTYPE = ds.Tables[0].Rows[0]["SICKTYPE"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CHARGEFLAG"] != null && ds.Tables[0].Rows[0]["CHARGEFLAG"].ToString() != "")
                    {
                        model.CHARGEFLAG = ds.Tables[0].Rows[0]["CHARGEFLAG"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["TESTDEST"] != null && ds.Tables[0].Rows[0]["TESTDEST"].ToString() != "")
                    {
                        model.TESTDEST = ds.Tables[0].Rows[0]["TESTDEST"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SLABLE"] != null && ds.Tables[0].Rows[0]["SLABLE"].ToString() != "")
                    {
                        model.SLABLE = ds.Tables[0].Rows[0]["SLABLE"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ZDY1"] != null && ds.Tables[0].Rows[0]["ZDY1"].ToString() != "")
                    {
                        model.ZDY1 = ds.Tables[0].Rows[0]["ZDY1"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ZDY2"] != null && ds.Tables[0].Rows[0]["ZDY2"].ToString() != "")
                    {
                        model.ZDY2 = ds.Tables[0].Rows[0]["ZDY2"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ZDY3"] != null && ds.Tables[0].Rows[0]["ZDY3"].ToString() != "")
                    {
                        model.ZDY3 = ds.Tables[0].Rows[0]["ZDY3"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ZDY4"] != null && ds.Tables[0].Rows[0]["ZDY4"].ToString() != "")
                    {
                        model.ZDY4 = ds.Tables[0].Rows[0]["ZDY4"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ZDY5"] != null && ds.Tables[0].Rows[0]["ZDY5"].ToString() != "")
                    {
                        model.ZDY5 = ds.Tables[0].Rows[0]["ZDY5"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["INCEPTDATE"] != null && ds.Tables[0].Rows[0]["INCEPTDATE"].ToString() != "")
                    {
                        model.INCEPTDATE = DateTime.Parse(ds.Tables[0].Rows[0]["INCEPTDATE"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["INCEPTTIME"] != null && ds.Tables[0].Rows[0]["INCEPTTIME"].ToString() != "")
                    {
                        model.INCEPTTIME = DateTime.Parse(ds.Tables[0].Rows[0]["INCEPTTIME"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["INCEPTER"] != null && ds.Tables[0].Rows[0]["INCEPTER"].ToString() != "")
                    {
                        model.INCEPTER = ds.Tables[0].Rows[0]["INCEPTER"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ONLINEDATE"] != null && ds.Tables[0].Rows[0]["ONLINEDATE"].ToString() != "")
                    {
                        model.ONLINEDATE = DateTime.Parse(ds.Tables[0].Rows[0]["ONLINEDATE"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["ONLINETIME"] != null && ds.Tables[0].Rows[0]["ONLINETIME"].ToString() != "")
                    {
                        model.ONLINETIME = DateTime.Parse(ds.Tables[0].Rows[0]["ONLINETIME"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["BMANNO"] != null && ds.Tables[0].Rows[0]["BMANNO"].ToString() != "")
                    {
                        model.BMANNO = ds.Tables[0].Rows[0]["BMANNO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["FILETYPE"] != null && ds.Tables[0].Rows[0]["FILETYPE"].ToString() != "")
                    {
                        model.FILETYPE = ds.Tables[0].Rows[0]["FILETYPE"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["JPGFILE"] != null && ds.Tables[0].Rows[0]["JPGFILE"].ToString() != "")
                    {
                        model.JPGFILE = ds.Tables[0].Rows[0]["JPGFILE"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["PDFFILE"] != null && ds.Tables[0].Rows[0]["PDFFILE"].ToString() != "")
                    {
                        model.PDFFILE = ds.Tables[0].Rows[0]["PDFFILE"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["FORMNO"] != null && ds.Tables[0].Rows[0]["FORMNO"].ToString() != "")
                    {
                        int a;
                        if (int.TryParse(ds.Tables[0].Rows[0]["FORMNO"].ToString(), out a))
                            model.FORMNO = a;
                        model.FormNo2 = ds.Tables[0].Rows[0]["FORMNO"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["CHILDTABLENAME"] != null && ds.Tables[0].Rows[0]["CHILDTABLENAME"].ToString() != "")
                    {
                        model.CHILDTABLENAME = ds.Tables[0].Rows[0]["CHILDTABLENAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["PRINTEXEC"] != null && ds.Tables[0].Rows[0]["PRINTEXEC"].ToString() != "")
                    {
                        model.PRINTEXEC = ds.Tables[0].Rows[0]["PRINTEXEC"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["LABCENTER"] != null && ds.Tables[0].Rows[0]["LABCENTER"].ToString() != "")
                    {
                        model.LABCENTER = ds.Tables[0].Rows[0]["LABCENTER"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["PRINTTEXEC"] != null && ds.Tables[0].Rows[0]["PRINTTEXEC"].ToString() != "")
                    {
                        model.PRINTTEXEC = ds.Tables[0].Rows[0]["PRINTTEXEC"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SECTIONTYPE"] != null && ds.Tables[0].Rows[0]["SECTIONTYPE"].ToString() != "")
                    {
                        model.SectionType = ds.Tables[0].Rows[0]["SECTIONTYPE"].ToString();
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error(e.ToString() + "@@@@@@@@" + e.Source + "@@@@" + e.Message + "%%%%" + e.StackTrace);
                    // throw e.ToString() + e.StackTrace;
                }
                return model;
            }
            else
            {
                return null;
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
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                //strSql.Append(" order by RECEIVEDATE desc ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > -1)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ReportFormFull ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder == null)
            { }
            else
            {
                strSql.Append(" order by " + filedOrder);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /*
        */

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(ZhiFang.Model.ReportFormFull model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM ReportFormFull where 1=1");
                if (model.CLIENTNO != null)
                {
                    strSql.Append(" and CLIENTNO='" + model.CLIENTNO + "' ");
                }
                if (model.ClientList != null && model.ClientList.Trim().Length > 0)
                {
                    strSql.Append(" and ClientNo in (" + model.ClientList + ") ");
                }
                if (model.serialno != null)
                {
                    strSql.Append(" and serialno='" + model.serialno + "' ");
                }
                if (model.clientcode != null)
                {
                    strSql.Append(" and clientcode like '%" + model.clientcode.Trim() + "%' ");
                }
                if (model.CNAME != null)
                {
                    if (model.LIKESEARCH == "1")
                    {
                        strSql.Append(" and CNAME like '%" + model.CNAME.Trim() + "%' ");
                    }
                    else
                    {
                        strSql.Append(" and CNAME='" + model.CNAME + "' ");
                    }
                }
                if (model.AGEUNITNAME != null)
                {
                    strSql.Append(" and AGEUNITNAME='" + model.AGEUNITNAME + "' ");
                }
                if (model.GENDERNAME != null)
                {
                    strSql.Append(" and GENDERNAME='" + model.GENDERNAME + "' ");
                }
                if (model.DEPTNAME != null)
                {
                    strSql.Append(" and DEPTNAME='" + model.DEPTNAME + "' ");
                }
                if (model.DOCTORNAME != null)
                {
                    strSql.Append(" and DOCTORNAME='" + model.DOCTORNAME + "' ");
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
                if (model.SAMPLETYPENAME != null)
                {
                    strSql.Append(" and SAMPLETYPENAME='" + model.SAMPLETYPENAME + "' ");
                }

                if (model.SECTIONNAME != null)
                {
                    strSql.Append(" and SECTIONNAME='" + model.SECTIONNAME + "' ");
                }

                if (model.PRINTTIMES != null)
                {
                    if (model.PRINTTIMES == 0)
                    {//未打印
                        strSql.Append(" and PRINTTIMES=0 ");
                    }
                    else if (model.PRINTTIMES == 1)
                    {//已打印
                        strSql.Append(" and PRINTTIMES>0 ");
                    }
                    else if (model.PRINTTIMES == 2)
                    {
                        //全部
                    }
                }
                if (model.ZDY6 != null)
                {
                    strSql.Append(" and ZDY6='" + model.ZDY6 + "' ");
                }
                if (model.ZDY7 != null)
                {
                    strSql.Append(" and ZDY7='" + model.ZDY7 + "' ");
                }
                if (model.ZDY8 != null)
                {
                    strSql.Append(" and ZDY8='" + model.ZDY8 + "' ");
                }
                if (model.ZDY9 != null)
                {
                    strSql.Append(" and ZDY9='" + model.ZDY9 + "' ");
                }
                if (model.ZDY10 != null)
                {
                    strSql.Append(" and ZDY10='" + model.ZDY10 + "' ");
                }
                if (model.TESTTYPENAME != null)
                {
                    strSql.Append(" and TESTTYPENAME='" + model.TESTTYPENAME + "' ");
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
                if (model.STATUSNO != null)
                {
                    strSql.Append(" and STATUSNO=" + model.STATUSNO + " ");
                }

                if (model.SAMPLETYPENO != null)
                {
                    strSql.Append(" and SAMPLETYPENO=" + model.SAMPLETYPENO + " ");
                }

                if (model.PATNO != null && model.PATNO != "")
                {
                    strSql.Append(" and PATNO='" + model.PATNO.Trim() + "' ");
                }
                if (model.PERSONID != null && model.PERSONID != "")
                {
                    strSql.Append(" and PERSONID='" + model.PERSONID.Trim() + "' ");
                }
                if (model.GENDERNO != null)
                {
                    strSql.Append(" and GENDERNO=" + model.GENDERNO + " ");
                }

                if (model.BIRTHDAY != null)
                {
                    strSql.Append(" and BIRTHDAY='" + model.BIRTHDAY + "' ");
                }

                if (model.AGE != null)
                {
                    strSql.Append(" and AGE='" + model.AGE + "' ");
                }

                if (model.AGEUNITNO != null)
                {
                    strSql.Append(" and AGEUNITNO=" + model.AGEUNITNO + " ");
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

                if (model.BED != null)
                {
                    strSql.Append(" and BED='" + model.BED + "' ");
                }

                if (model.DEPTNO != null)
                {
                    strSql.Append(" and DEPTNO=" + model.DEPTNO + " ");
                }

                if (model.DOCTOR != null)
                {
                    strSql.Append(" and DOCTOR='" + model.DOCTOR + "' ");
                }

                if (model.CHARGENO != null)
                {
                    strSql.Append(" and CHARGENO='" + model.CHARGENO + "' ");
                }

                if (model.CHARGE != null)
                {
                    strSql.Append(" and CHARGE='" + model.CHARGE + "' ");
                }

                if (model.COLLECTER != null)
                {
                    strSql.Append(" and COLLECTER='" + model.COLLECTER + "' ");
                }

                if (model.COLLECTDATE != null)
                {
                    strSql.Append(" and COLLECTDATE='" + model.COLLECTDATE + "' ");
                }

                if (model.COLLECTTIME != null)
                {
                    strSql.Append(" and COLLECTTIME='" + model.COLLECTTIME + "' ");
                }

                if (model.FORMMEMO != null)
                {
                    strSql.Append(" and FORMMEMO='" + model.FORMMEMO + "' ");
                }

                if (model.TECHNICIAN != null)
                {
                    strSql.Append(" and TECHNICIAN='" + model.TECHNICIAN + "' ");
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

                if (model.OPERDATE != null)
                {
                    strSql.Append(" and OPERDATE='" + model.OPERDATE + "' ");
                }

                if (model.OPERTIME != null)
                {
                    strSql.Append(" and OPERTIME='" + model.OPERTIME + "' ");
                }

                if (model.CHECKER != null)
                {
                    strSql.Append(" and CHECKER='" + model.CHECKER + "' ");
                }


                if (model.resultfile != null)
                {
                    strSql.Append(" and resultfile='" + model.resultfile + "' ");
                }

                if (model.CHECKDATE != null)
                {
                    strSql.Append(" and CHECKDATE='" + model.CHECKDATE + "' ");
                }

                if (model.CHECKTIME != null)
                {
                    strSql.Append(" and CHECKTIME='" + model.CHECKTIME + "' ");
                }

                if (model.SERIALNO != null)
                {
                    strSql.Append(" and SERIALNO='" + model.SERIALNO + "' ");
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

                if (model.SICKORDER != null)
                {
                    strSql.Append(" and SICKORDER='" + model.SICKORDER + "' ");
                }

                if (model.SICKTYPE != null)
                {
                    strSql.Append(" and SICKTYPE='" + model.SICKTYPE + "' ");
                }

                if (model.CHARGEFLAG != null)
                {
                    strSql.Append(" and CHARGEFLAG='" + model.CHARGEFLAG + "' ");
                }

                if (model.TESTDEST != null)
                {
                    strSql.Append(" and TESTDEST='" + model.TESTDEST + "' ");
                }

                if (model.SLABLE != null)
                {
                    strSql.Append(" and SLABLE='" + model.SLABLE + "' ");
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

                if (model.INCEPTDATE != null)
                {
                    strSql.Append(" and INCEPTDATE='" + model.INCEPTDATE + "' ");
                }

                if (model.INCEPTTIME != null)
                {
                    strSql.Append(" and INCEPTTIME='" + model.INCEPTTIME + "' ");
                }

                if (model.INCEPTER != null)
                {
                    strSql.Append(" and INCEPTER='" + model.INCEPTER + "' ");
                }

                if (model.ONLINEDATE != null)
                {
                    strSql.Append(" and ONLINEDATE='" + model.ONLINEDATE + "' ");
                }

                if (model.ONLINETIME != null)
                {
                    strSql.Append(" and ONLINETIME='" + model.ONLINETIME + "' ");
                }

                if (model.BMANNO != null)
                {
                    strSql.Append(" and BMANNO='" + model.BMANNO + "' ");
                }

                if (model.FILETYPE != null)
                {
                    strSql.Append(" and FILETYPE='" + model.FILETYPE + "' ");
                }

                if (model.JPGFILE != null)
                {
                    strSql.Append(" and JPGFILE='" + model.JPGFILE + "' ");
                }

                if (model.PDFFILE != null)
                {
                    strSql.Append(" and PDFFILE='" + model.PDFFILE + "' ");
                }

                if (model.FORMNO != null)
                {
                    strSql.Append(" and FORMNO=" + model.FORMNO + " ");
                }

                if (model.CHILDTABLENAME != null)
                {
                    strSql.Append(" and CHILDTABLENAME='" + model.CHILDTABLENAME + "' ");
                }

                if (model.PRINTEXEC != null)
                {
                    strSql.Append(" and PRINTEXEC='" + model.PRINTEXEC + "' ");
                }
                if (model.LABCENTER != null)
                {
                    strSql.Append(" and LABCENTER='" + model.LABCENTER + "' ");
                }

                if (model.Startdate != null)
                {
                    strSql.Append(" and ReceiveDate>='" + model.Startdate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.Enddate != null)
                {
                    if (model.Enddate.Value.TimeOfDay.Ticks > 0)
                    {
                        strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                    }
                    else
                    {
                        strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToShortDateString() + " 23:59:59" + "' ");
                    }
                }
                if (model.collectStartdate != null)
                {
                    strSql.Append(" and COLLECTDATE>='" + model.collectStartdate.Value.ToShortDateString() + "' ");
                }

                if (model.collectEnddate != null)
                {
                    strSql.Append(" and COLLECTDATE<='" + model.collectEnddate.Value.ToShortDateString() + "' ");
                }
                if (model.CheckStartDate != null)
                {
                    strSql.Append(" and checkdate>='" + model.CheckStartDate.Value.ToShortDateString() + "' ");
                }

                if (model.CheckEndDate != null)
                {
                    strSql.Append(" and checkdate<='" + model.CheckEndDate.Value.ToShortDateString() + "' ");
                }
                if (model.noperdateStart != null)
                {
                    strSql.Append(" and noperdate>='" + model.noperdateStart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.noperdateEnd != null)
                {
                    strSql.Append(" and noperdate<='" + model.noperdateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.operdateStart != null)
                {
                    strSql.Append(" and operdate>='" + model.operdateStart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.operdateEnd != null)
                {
                    strSql.Append(" and operdate<='" + model.operdateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.BarCode != null)
                {
                    strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
                }

                if (model.SectionType != null)
                {
                    strSql.Append(" and SectionType='" + model.SectionType.Trim() + "' ");
                }
                if (model.DIAGNOSE != null)
                {
                    strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
                }

                if (model.DIAGNOSE != null)
                {
                    strSql.Append(" and DIAGNOSE='" + model.DIAGNOSE.Trim() + "' ");
                }
                if (model.CLIENTNAME != null)
                {
                    strSql.Append(" and CLIENTNAME='" + model.CLIENTNAME.Trim() + "' ");
                }
                if (model.ReportFormID != null)
                {
                    strSql.Append(" and ReportFormID='" + model.ReportFormID.Trim() + "' ");
                }


                if (model.RBACSQL != null && model.RBACSQL.Trim() != "")
                {
                    strSql.Append(" and " + model.RBACSQL.Trim() + " ");
                }
                if (model.WeblisSourceOrgList != null && model.WeblisSourceOrgList.Trim().Length > 0)
                {
                    strSql.Append(" and WeblisSourceOrgId in (" + model.WeblisSourceOrgList + ") ");
                }
                if (model.WeblisSourceOrgId != null)
                {
                    strSql.Append(" and WeblisSourceOrgId=" + model.WeblisSourceOrgId + " ");
                }
                ZhiFang.Common.Log.Log.Info("报告列表信息:" + strSql.ToString() + "@" + DbHelperSQL.ConnectionString);
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception ex)
            {
                Common.Log.Log.Debug("异常信息:" + ex.ToString());
                return null;
            }
        }


        public DataSet GetList(ZhiFang.Model.ReportFormFull model, string city)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select CNAME,PersonID");
                strSql.Append(" FROM ReportFormFull where 1=1");
                if (model.CLIENTNO != null)
                {
                    strSql.Append(" and CLIENTNO='" + model.CLIENTNO + "' ");
                }
                if (model.CNAME != null)
                {
                    if (model.LIKESEARCH == "1")
                    {
                        strSql.Append(" and CNAME like '%" + model.CNAME.Trim() + "%' ");
                    }
                    else
                    {
                        strSql.Append(" and CNAME='" + model.CNAME + "' ");
                    }
                }

                if (model.SECTIONNO != null)
                {
                    strSql.Append(" and SECTIONNO='" + model.SECTIONNO + "' ");
                }


                if (model.PATNO != null && model.PATNO != "")
                {
                    strSql.Append(" and PATNO='" + model.PATNO.Trim() + "' ");
                }
                if (model.PERSONID != null && model.PERSONID != "")
                {
                    strSql.Append(" and PERSONID='" + model.PERSONID.Trim() + "' ");
                }


                if (model.Startdate != null)
                {
                    strSql.Append(" and ReceiveDate>='" + model.Startdate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.Enddate != null)
                {
                    strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToShortDateString() + " 23:59:59" + "' ");
                }


                if (model.ReportFormID != null)
                {
                    strSql.Append(" and ReportFormID='" + model.ReportFormID.Trim() + "' ");
                }

                strSql.Append("group by CNAME,PersonID;");
                ZhiFang.Common.Log.Log.Info("报告列表信息:" + strSql.ToString() + "@" + DbHelperSQL.ConnectionString);
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception ex)
            {
                Common.Log.Log.Debug("异常信息:" + ex.ToString());
                return null;
            }
        }

        public DataSet GetList(int Top, Model.ReportFormFull model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString() + " ");
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ReportFormFull where 1=1 ");
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFullSearchSql") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFullSearchSql") != "")
            {
                strSql.Append(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFullSearchSql"));
            }
            if (model.CLIENTNO != null)
            {
                strSql.Append(" and CLIENTNO='" + model.CLIENTNO + "' ");
            }
            if (model.ClientList != null && model.ClientList.Trim().Length > 0)
            {
                strSql.Append(" and ClientNo in (" + model.ClientList + ") ");
            }
            if (model.serialno != null)
            {
                strSql.Append(" and serialno='" + model.serialno + "' ");
            }
            if (model.clientcode != null)
            {
                strSql.Append(" and clientcode like '%" + model.clientcode.Trim() + "%' ");
            }
            if (model.CNAME != null)
            {
                if (model.LIKESEARCH == "1")
                {
                    strSql.Append(" and CNAME like '%" + model.CNAME.Trim() + "%' ");
                }
                else
                {
                    strSql.Append(" and CNAME='" + model.CNAME + "' ");
                }
            }
            if (model.AGEUNITNAME != null)
            {
                strSql.Append(" and AGEUNITNAME='" + model.AGEUNITNAME + "' ");
            }
            if (model.GENDERNAME != null)
            {
                strSql.Append(" and GENDERNAME='" + model.GENDERNAME + "' ");
            }
            if (model.DEPTNAME != null)
            {
                strSql.Append(" and DEPTNAME='" + model.DEPTNAME + "' ");
            }
            if (model.DOCTORNAME != null)
            {
                strSql.Append(" and DOCTORNAME='" + model.DOCTORNAME + "' ");
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
            if (model.SAMPLETYPENAME != null)
            {
                strSql.Append(" and SAMPLETYPENAME='" + model.SAMPLETYPENAME + "' ");
            }

            if (model.SECTIONNAME != null)
            {
                strSql.Append(" and SECTIONNAME='" + model.SECTIONNAME + "' ");
            }

            if (model.PRINTTIMES != null)
            {
                if (model.PRINTTIMES == 0)
                {//未打印
                    strSql.Append(" and PRINTTIMES=0 ");
                }
                else if (model.PRINTTIMES == 1)
                {//已打印
                    strSql.Append(" and PRINTTIMES>0 ");
                }
                else if (model.PRINTTIMES == 2)
                {
                    //全部
                }
            }
            if (model.ZDY10 != null)
            {
                strSql.Append(" and ZDY10='" + model.ZDY10 + "' ");
            }
            if (model.TESTTYPENAME != null)
            {
                strSql.Append(" and TESTTYPENAME='" + model.TESTTYPENAME + "' ");
            }

            if (model.RECEIVEDATE != null)
            {
                strSql.Append(" and RECEIVEDATE='" + model.RECEIVEDATE + "' ");
            }
            if (model.SECTIONNO != null)
            {
                strSql.Append(" and SECTIONNO in ('" + String.Join("','", model.SECTIONNO.Split(',')) + "') ");
            }
            if (model.TESTTYPENO != null)
            {
                strSql.Append(" and TESTTYPENO='" + model.TESTTYPENO + "' ");
            }
            if (model.SAMPLENO != null)
            {
                strSql.Append(" and SAMPLENO='" + model.SAMPLENO + "' ");
            }
            if (model.STATUSNO != null)
            {
                strSql.Append(" and STATUSNO=" + model.STATUSNO + " ");
            }

            if (model.SAMPLETYPENO != null)
            {
                strSql.Append(" and SAMPLETYPENO=" + model.SAMPLETYPENO + " ");
            }

            if (model.PATNO != null && model.PATNO != "")
            {
                strSql.Append(" and PATNO='" + model.PATNO.Trim() + "' ");
            }
            if (model.PERSONID != null && model.PERSONID != "")
            {
                strSql.Append(" and PERSONID='" + model.PERSONID.Trim() + "' ");
            }
            if (model.GENDERNO != null)
            {
                strSql.Append(" and GENDERNO=" + model.GENDERNO + " ");
            }

            if (model.BIRTHDAY != null)
            {
                strSql.Append(" and BIRTHDAY='" + model.BIRTHDAY + "' ");
            }

            if (model.AGE != null)
            {
                strSql.Append(" and AGE='" + model.AGE + "' ");
            }

            if (model.AGEUNITNO != null)
            {
                strSql.Append(" and AGEUNITNO=" + model.AGEUNITNO + " ");
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

            if (model.BED != null)
            {
                strSql.Append(" and BED='" + model.BED + "' ");
            }

            if (model.DEPTNO != null)
            {
                strSql.Append(" and DEPTNO=" + model.DEPTNO + " ");
            }

            if (model.DOCTOR != null)
            {
                strSql.Append(" and DOCTOR='" + model.DOCTOR + "' ");
            }

            if (model.CHARGENO != null)
            {
                strSql.Append(" and CHARGENO='" + model.CHARGENO + "' ");
            }

            if (model.CHARGE != null)
            {
                strSql.Append(" and CHARGE='" + model.CHARGE + "' ");
            }

            if (model.COLLECTER != null)
            {
                strSql.Append(" and COLLECTER='" + model.COLLECTER + "' ");
            }

            if (model.COLLECTDATE != null)
            {
                strSql.Append(" and COLLECTDATE='" + model.COLLECTDATE + "' ");
            }

            if (model.COLLECTTIME != null)
            {
                strSql.Append(" and COLLECTTIME='" + model.COLLECTTIME + "' ");
            }

            if (model.FORMMEMO != null)
            {
                strSql.Append(" and FORMMEMO='" + model.FORMMEMO + "' ");
            }

            if (model.TECHNICIAN != null)
            {
                strSql.Append(" and TECHNICIAN='" + model.TECHNICIAN + "' ");
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

            if (model.OPERDATE != null)
            {
                strSql.Append(" and OPERDATE='" + model.OPERDATE + "' ");
            }

            if (model.OPERTIME != null)
            {
                strSql.Append(" and OPERTIME='" + model.OPERTIME + "' ");
            }

            if (model.CHECKER != null)
            {
                strSql.Append(" and CHECKER='" + model.CHECKER + "' ");
            }


            if (model.resultfile != null)
            {
                strSql.Append(" and resultfile='" + model.resultfile + "' ");
            }

            if (model.CHECKDATE != null)
            {
                strSql.Append(" and CHECKDATE='" + model.CHECKDATE + "' ");
            }

            if (model.CHECKTIME != null)
            {
                strSql.Append(" and CHECKTIME='" + model.CHECKTIME + "' ");
            }

            if (model.SERIALNO != null)
            {
                strSql.Append(" and SERIALNO='" + model.SERIALNO + "' ");
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

            if (model.SICKORDER != null)
            {
                strSql.Append(" and SICKORDER='" + model.SICKORDER + "' ");
            }

            if (model.SICKTYPE != null)
            {
                strSql.Append(" and SICKTYPE='" + model.SICKTYPE + "' ");
            }

            if (model.CHARGEFLAG != null)
            {
                strSql.Append(" and CHARGEFLAG='" + model.CHARGEFLAG + "' ");
            }

            if (model.TESTDEST != null)
            {
                strSql.Append(" and TESTDEST='" + model.TESTDEST + "' ");
            }

            if (model.SLABLE != null)
            {
                strSql.Append(" and SLABLE='" + model.SLABLE + "' ");
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

            if (model.INCEPTDATE != null)
            {
                strSql.Append(" and INCEPTDATE='" + model.INCEPTDATE + "' ");
            }

            if (model.INCEPTTIME != null)
            {
                strSql.Append(" and INCEPTTIME='" + model.INCEPTTIME + "' ");
            }

            if (model.INCEPTER != null)
            {
                strSql.Append(" and INCEPTER='" + model.INCEPTER + "' ");
            }

            if (model.ONLINEDATE != null)
            {
                strSql.Append(" and ONLINEDATE='" + model.ONLINEDATE + "' ");
            }

            if (model.ONLINETIME != null)
            {
                strSql.Append(" and ONLINETIME='" + model.ONLINETIME + "' ");
            }

            if (model.BMANNO != null)
            {
                strSql.Append(" and BMANNO='" + model.BMANNO + "' ");
            }

            if (model.FILETYPE != null)
            {
                strSql.Append(" and FILETYPE='" + model.FILETYPE + "' ");
            }

            if (model.JPGFILE != null)
            {
                strSql.Append(" and JPGFILE='" + model.JPGFILE + "' ");
            }

            if (model.PDFFILE != null)
            {
                strSql.Append(" and PDFFILE='" + model.PDFFILE + "' ");
            }

            if (model.FORMNO != null)
            {
                strSql.Append(" and FORMNO=" + model.FORMNO + " ");
            }

            if (model.CHILDTABLENAME != null)
            {
                strSql.Append(" and CHILDTABLENAME='" + model.CHILDTABLENAME + "' ");
            }

            if (model.PRINTEXEC != null)
            {
                strSql.Append(" and PRINTEXEC='" + model.PRINTEXEC + "' ");
            }
            if (model.LABCENTER != null)
            {
                strSql.Append(" and LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.Startdate != null)
            {
                strSql.Append(" and ReceiveDate>='" + model.Startdate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.Enddate != null)
            {
                if (model.Enddate.Value.TimeOfDay.Ticks > 0)
                {
                    strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }
                else
                {
                    strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToShortDateString() + " 23:59:59" + "' ");
                }
            }
            if (model.collectStartdate != null)
            {
                strSql.Append(" and COLLECTDATE>='" + model.collectStartdate.Value.ToShortDateString() + "' ");
            }

            if (model.collectEnddate != null)
            {
                strSql.Append(" and COLLECTDATE<='" + model.collectEnddate.Value.ToShortDateString() + "' ");
            }
            if (model.CheckStartDate != null)
            {
                strSql.Append(" and checkdate>='" + model.CheckStartDate.Value.ToShortDateString() + "' ");
            }

            if (model.CheckEndDate != null)
            {
                strSql.Append(" and checkdate<='" + model.CheckEndDate.Value.ToShortDateString() + "' ");
            }
            if (model.noperdateStart != null)
            {
                strSql.Append(" and noperdate>='" + model.noperdateStart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.noperdateEnd != null)
            {
                strSql.Append(" and noperdate<='" + model.noperdateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }
            if (model.operdateStart != null)
            {
                strSql.Append(" and operdate>='" + model.operdateStart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.operdateEnd != null)
            {
                strSql.Append(" and operdate<='" + model.operdateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.BarCode != null)
            {
                strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
            }

            if (model.SectionType != null)
            {
                strSql.Append(" and SectionType='" + model.SectionType.Trim() + "' ");
            }
            if (model.DIAGNOSE != null)
            {
                strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
            }

            if (model.DIAGNOSE != null)
            {
                strSql.Append(" and DIAGNOSE='" + model.DIAGNOSE.Trim() + "' ");
            }
            if (model.CLIENTNAME != null)
            {
                strSql.Append(" and CLIENTNAME='" + model.CLIENTNAME.Trim() + "' ");
            }
            if (model.ReportFormID != null)
            {
                strSql.Append(" and ReportFormID='" + model.ReportFormID.Trim() + "' ");
            }


            if (model.RBACSQL != null && model.RBACSQL.Trim() != "")
            {
                strSql.Append(" and " + model.RBACSQL.Trim() + " ");
            }
            if (model.WeblisSourceOrgList != null && model.WeblisSourceOrgList.Trim().Length > 0)
            {
                strSql.Append(" and WeblisSourceOrgId in (" + model.WeblisSourceOrgList + ") ");
            }
            if (model.WeblisSourceOrgId != null)
            {
                strSql.Append(" and WeblisSourceOrgId=" + model.WeblisSourceOrgId + " ");
            }
            if (model.ResultStatus.HasValue)
            {
                strSql.Append(" and ResultStatus=" + model.ResultStatus + " ");
            }
            if (filedOrder != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            //ZhiFang.Common.Log.Log.Info("报告列表信息:" + strSql.ToString() + "@" + DbHelperSQL.ConnectionString);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportFormFull ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetColumns()
        {
            //select name from syscolumns where id=(select max(id) from sysobjects where xtype='u' and name='ReportFormFull
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top 1 * from ReportFormFull ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion

        #region IDataBase<ReportFormFull> 成员

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ReportFormFull");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.ReportFormFull model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ReportFormFull where 1=1  ");
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFullSearchSql") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFullSearchSql") != "")
            {
                strSql.Append(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFullSearchSql"));
            }
            if (model.CLIENTNO != null)
            {
                strSql.Append(" and CLIENTNO='" + model.CLIENTNO + "' ");
            }
            if (model.ClientList != null && model.ClientList.Trim().Length > 0)
            {
                strSql.Append(" and ClientNo in (" + model.ClientList + ") ");
            }
            if (model.serialno != null)
            {
                strSql.Append(" and serialno='" + model.serialno + "' ");
            }
            if (model.clientcode != null)
            {
                strSql.Append(" and clientcode like '%" + model.clientcode.Trim() + "%' ");
            }
            if (model.CNAME != null)
            {
                if (model.LIKESEARCH == "1")
                {
                    strSql.Append(" and CNAME like '%" + model.CNAME.Trim() + "%' ");
                }
                else
                {
                    strSql.Append(" and CNAME='" + model.CNAME + "' ");
                }
            }
            if (model.AGEUNITNAME != null)
            {
                strSql.Append(" and AGEUNITNAME='" + model.AGEUNITNAME + "' ");
            }
            if (model.GENDERNAME != null)
            {
                strSql.Append(" and GENDERNAME='" + model.GENDERNAME + "' ");
            }
            if (model.DEPTNAME != null)
            {
                strSql.Append(" and DEPTNAME='" + model.DEPTNAME + "' ");
            }
            if (model.DOCTORNAME != null)
            {
                strSql.Append(" and DOCTORNAME='" + model.DOCTORNAME + "' ");
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
            if (model.SAMPLETYPENAME != null)
            {
                strSql.Append(" and SAMPLETYPENAME='" + model.SAMPLETYPENAME + "' ");
            }

            if (model.SECTIONNAME != null)
            {
                strSql.Append(" and SECTIONNAME='" + model.SECTIONNAME + "' ");
            }

            if (model.PRINTTIMES != null)
            {
                if (model.PRINTTIMES == 0)
                {//未打印
                    strSql.Append(" and PRINTTIMES=0 ");
                }
                else if (model.PRINTTIMES == 1)
                {//已打印
                    strSql.Append(" and PRINTTIMES>0 ");
                }
                else if (model.PRINTTIMES == 2)
                {
                    //全部
                }
            }
            if (model.ZDY10 != null)
            {
                strSql.Append(" and ZDY10='" + model.ZDY10 + "' ");
            }
            if (model.TESTTYPENAME != null)
            {
                strSql.Append(" and TESTTYPENAME='" + model.TESTTYPENAME + "' ");
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
            if (model.STATUSNO != null)
            {
                strSql.Append(" and STATUSNO=" + model.STATUSNO + " ");
            }

            if (model.SAMPLETYPENO != null)
            {
                strSql.Append(" and SAMPLETYPENO=" + model.SAMPLETYPENO + " ");
            }

            if (model.PATNO != null && model.PATNO != "")
            {
                strSql.Append(" and PATNO='" + model.PATNO.Trim() + "' ");
            }
            if (model.PERSONID != null && model.PERSONID != "")
            {
                strSql.Append(" and PERSONID='" + model.PERSONID.Trim() + "' ");
            }
            if (model.GENDERNO != null)
            {
                strSql.Append(" and GENDERNO=" + model.GENDERNO + " ");
            }

            if (model.BIRTHDAY != null)
            {
                strSql.Append(" and BIRTHDAY='" + model.BIRTHDAY + "' ");
            }

            if (model.AGE != null)
            {
                strSql.Append(" and AGE='" + model.AGE + "' ");
            }

            if (model.AGEUNITNO != null)
            {
                strSql.Append(" and AGEUNITNO=" + model.AGEUNITNO + " ");
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

            if (model.BED != null)
            {
                strSql.Append(" and BED='" + model.BED + "' ");
            }

            if (model.DEPTNO != null)
            {
                strSql.Append(" and DEPTNO=" + model.DEPTNO + " ");
            }

            if (model.DOCTOR != null)
            {
                strSql.Append(" and DOCTOR='" + model.DOCTOR + "' ");
            }

            if (model.CHARGENO != null)
            {
                strSql.Append(" and CHARGENO='" + model.CHARGENO + "' ");
            }

            if (model.CHARGE != null)
            {
                strSql.Append(" and CHARGE='" + model.CHARGE + "' ");
            }

            if (model.COLLECTER != null)
            {
                strSql.Append(" and COLLECTER='" + model.COLLECTER + "' ");
            }

            if (model.COLLECTDATE != null)
            {
                strSql.Append(" and COLLECTDATE='" + model.COLLECTDATE + "' ");
            }

            if (model.COLLECTTIME != null)
            {
                strSql.Append(" and COLLECTTIME='" + model.COLLECTTIME + "' ");
            }

            if (model.FORMMEMO != null)
            {
                strSql.Append(" and FORMMEMO='" + model.FORMMEMO + "' ");
            }

            if (model.TECHNICIAN != null)
            {
                strSql.Append(" and TECHNICIAN='" + model.TECHNICIAN + "' ");
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

            if (model.OPERDATE != null)
            {
                strSql.Append(" and OPERDATE='" + model.OPERDATE + "' ");
            }

            if (model.OPERTIME != null)
            {
                strSql.Append(" and OPERTIME='" + model.OPERTIME + "' ");
            }

            if (model.CHECKER != null)
            {
                strSql.Append(" and CHECKER='" + model.CHECKER + "' ");
            }


            if (model.resultfile != null)
            {
                strSql.Append(" and resultfile='" + model.resultfile + "' ");
            }

            if (model.CHECKDATE != null)
            {
                strSql.Append(" and CHECKDATE='" + model.CHECKDATE + "' ");
            }

            if (model.CHECKTIME != null)
            {
                strSql.Append(" and CHECKTIME='" + model.CHECKTIME + "' ");
            }

            if (model.SERIALNO != null)
            {
                strSql.Append(" and SERIALNO='" + model.SERIALNO + "' ");
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

            if (model.SICKORDER != null)
            {
                strSql.Append(" and SICKORDER='" + model.SICKORDER + "' ");
            }

            if (model.SICKTYPE != null)
            {
                strSql.Append(" and SICKTYPE='" + model.SICKTYPE + "' ");
            }

            if (model.CHARGEFLAG != null)
            {
                strSql.Append(" and CHARGEFLAG='" + model.CHARGEFLAG + "' ");
            }

            if (model.TESTDEST != null)
            {
                strSql.Append(" and TESTDEST='" + model.TESTDEST + "' ");
            }

            if (model.SLABLE != null)
            {
                strSql.Append(" and SLABLE='" + model.SLABLE + "' ");
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

            if (model.INCEPTDATE != null)
            {
                strSql.Append(" and INCEPTDATE='" + model.INCEPTDATE + "' ");
            }

            if (model.INCEPTTIME != null)
            {
                strSql.Append(" and INCEPTTIME='" + model.INCEPTTIME + "' ");
            }

            if (model.INCEPTER != null)
            {
                strSql.Append(" and INCEPTER='" + model.INCEPTER + "' ");
            }

            if (model.ONLINEDATE != null)
            {
                strSql.Append(" and ONLINEDATE='" + model.ONLINEDATE + "' ");
            }

            if (model.ONLINETIME != null)
            {
                strSql.Append(" and ONLINETIME='" + model.ONLINETIME + "' ");
            }

            if (model.BMANNO != null)
            {
                strSql.Append(" and BMANNO='" + model.BMANNO + "' ");
            }

            if (model.FILETYPE != null)
            {
                strSql.Append(" and FILETYPE='" + model.FILETYPE + "' ");
            }

            if (model.JPGFILE != null)
            {
                strSql.Append(" and JPGFILE='" + model.JPGFILE + "' ");
            }

            if (model.PDFFILE != null)
            {
                strSql.Append(" and PDFFILE='" + model.PDFFILE + "' ");
            }

            if (model.FORMNO != null)
            {
                strSql.Append(" and FORMNO=" + model.FORMNO + " ");
            }

            if (model.CHILDTABLENAME != null)
            {
                strSql.Append(" and CHILDTABLENAME='" + model.CHILDTABLENAME + "' ");
            }

            if (model.PRINTEXEC != null)
            {
                strSql.Append(" and PRINTEXEC='" + model.PRINTEXEC + "' ");
            }
            if (model.LABCENTER != null)
            {
                strSql.Append(" and LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.Startdate != null)
            {
                strSql.Append(" and ReceiveDate>='" + model.Startdate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.Enddate != null)
            {
                if (model.Enddate.Value.TimeOfDay.Ticks > 0)
                {
                    strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }
                else
                {
                    strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToShortDateString() + " 23:59:59" + "' ");
                }
            }
            if (model.collectStartdate != null)
            {
                strSql.Append(" and COLLECTDATE>='" + model.collectStartdate.Value.ToShortDateString() + "' ");
            }

            if (model.collectEnddate != null)
            {
                strSql.Append(" and COLLECTDATE<='" + model.collectEnddate.Value.ToShortDateString() + "' ");
            }
            if (model.CheckStartDate != null)
            {
                strSql.Append(" and checkdate>='" + model.CheckStartDate.Value.ToShortDateString() + "' ");
            }

            if (model.CheckEndDate != null)
            {
                strSql.Append(" and checkdate<='" + model.CheckEndDate.Value.ToShortDateString() + "' ");
            }
            if (model.noperdateStart != null)
            {
                strSql.Append(" and noperdate>='" + model.noperdateStart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.noperdateEnd != null)
            {
                strSql.Append(" and noperdate<='" + model.noperdateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.operdateStart != null)
            {
                strSql.Append(" and operdate>='" + model.operdateStart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.operdateEnd != null)
            {
                strSql.Append(" and operdate<='" + model.operdateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.BarCode != null)
            {
                strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
            }

            if (model.SectionType != null)
            {
                strSql.Append(" and SectionType='" + model.SectionType.Trim() + "' ");
            }
            if (model.DIAGNOSE != null)
            {
                strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
            }

            if (model.DIAGNOSE != null)
            {
                strSql.Append(" and DIAGNOSE='" + model.DIAGNOSE.Trim() + "' ");
            }
            if (model.CLIENTNAME != null)
            {
                strSql.Append(" and CLIENTNAME='" + model.CLIENTNAME.Trim() + "' ");
            }
            if (model.ReportFormID != null)
            {
                strSql.Append(" and ReportFormID='" + model.ReportFormID.Trim() + "' ");
            }


            if (model.RBACSQL != null && model.RBACSQL.Trim() != "")
            {
                strSql.Append(" and " + model.RBACSQL.Trim() + " ");
            }
            if (model.WeblisSourceOrgList != null && model.WeblisSourceOrgList.Trim().Length > 0)
            {
                strSql.Append(" and WeblisSourceOrgId in (" + model.WeblisSourceOrgList + ") ");
            }
            if (model.WeblisSourceOrgId != null)
            {
                strSql.Append(" and WeblisSourceOrgId=" + model.WeblisSourceOrgId + " ");
            }
            if (model.ResultStatus.HasValue)
            {
                strSql.Append(" and ResultStatus=" + model.ResultStatus + " ");
            }
            ZhiFang.Common.Log.Log.Debug("GetTotalCountSQL:" + strSql.ToString());
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        if (this.Exists(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim()))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                            count += this.AddByDataRow(dr);
                    }
                    if (count == ds.Tables[0].Rows.Count)
                        return 1;
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
            else
                return 1;
        }
        public int AddByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ReportFormFull (");
                strSql.Append("ReportFormID,CLIENTNO,CNAME,AGEUNITNAME,GENDERNAME,DEPTNAME,DOCTORNAME,DISTRICTNAME,WARDNAME,FOLKNAME,SICKTYPENAME,SAMPLETYPENAME,SECTIONNAME,TESTTYPENAME,RECEIVEDATE,SECTIONNO,TESTTYPENO,SAMPLENO,STATUSNO,SAMPLETYPENO,PATNO,GENDERNO,BIRTHDAY,AGE,AGEUNITNO,FOLKNO,DISTRICTNO,WARDNO,BED,DEPTNO,DOCTOR,CHARGENO,CHARGE,COLLECTER,COLLECTDATE,COLLECTTIME,FORMMEMO,TECHNICIAN,TESTDATE,TESTTIME,OPERATOR,OPERDATE,OPERTIME,CHECKER,PRINTTIMES,resultfile,CHECKDATE,CHECKTIME,SERIALNO,REQUESTSOURCE,DIAGNO,SICKTYPENO,FORMCOMMENT,ARTIFICERORDER,SICKORDER,SICKTYPE,CHARGEFLAG,TESTDEST,SLABLE,ZDY1,ZDY2,ZDY3,ZDY4,ZDY5,INCEPTDATE,INCEPTTIME,INCEPTER,ONLINEDATE,ONLINETIME,BMANNO,FILETYPE,JPGFILE,PDFFILE,FORMNO,CHILDTABLENAME,PRINTEXEC,LABCENTER,PRINTTEXEC,SECTIONTYPE,BARCODE,SECTIONSHORTNAME,SECTIONSHORTCODE,DIAGNOSE,CLIENTNAME,OldSerialNo");
                strSql.Append(") values (");
                strSql.Append("'" + dr["ReportFormID"].ToString().Trim() + "','" + dr["CLIENTNO"].ToString().Trim() + "','" + dr["CNAME"].ToString().Trim() + "','" + dr["AGEUNITNAME"].ToString().Trim() + "','" + dr["GENDERNAME"].ToString().Trim() + "','" + dr["DEPTNAME"].ToString().Trim() + "','" + dr["DOCTORNAME"].ToString().Trim() + "','" + dr["DISTRICTNAME"].ToString().Trim() + "','" + dr["WARDNAME"].ToString().Trim() + "','" + dr["FOLKNAME"].ToString().Trim() + "','" + dr["SICKTYPENAME"].ToString().Trim() + "','" + dr["SAMPLETYPENAME"].ToString().Trim() + "','" + dr["SECTIONNAME"].ToString().Trim() + "','" + dr["TESTTYPENAME"].ToString().Trim() + "','" + dr["RECEIVEDATE"].ToString().Trim() + "','" + dr["SECTIONNO"].ToString().Trim() + "','" + dr["TESTTYPENO"].ToString().Trim() + "','" + dr["SAMPLENO"].ToString().Trim() + "','" + dr["STATUSNO"].ToString().Trim() + "','" + dr["SAMPLETYPENO"].ToString().Trim() + "','" + dr["PATNO"].ToString().Trim() + "','" + dr["GENDERNO"].ToString().Trim() + "','" + dr["BIRTHDAY"].ToString().Trim() + "','" + dr["AGE"].ToString().Trim() + "','" + dr["AGEUNITNO"].ToString().Trim() + "','" + dr["FOLKNO"].ToString().Trim() + "','" + dr["DISTRICTNO"].ToString().Trim() + "','" + dr["WARDNO"].ToString().Trim() + "','" + dr["BED"].ToString().Trim() + "','" + dr["DEPTNO"].ToString().Trim() + "','" + dr["DOCTOR"].ToString().Trim() + "','" + dr["CHARGENO"].ToString().Trim() + "','" + dr["CHARGE"].ToString().Trim() + "','" + dr["COLLECTER"].ToString().Trim() + "','" + dr["COLLECTDATE"].ToString().Trim() + "','" + dr["COLLECTTIME"].ToString().Trim() + "','" + dr["FORMMEMO"].ToString().Trim() + "','" + dr["TECHNICIAN"].ToString().Trim() + "','" + dr["TESTDATE"].ToString().Trim() + "','" + dr["TESTTIME"].ToString().Trim() + "','" + dr["OPERATOR"].ToString().Trim() + "','" + dr["OPERDATE"].ToString().Trim() + "','" + dr["OPERTIME"].ToString().Trim() + "','" + dr["CHECKER"].ToString().Trim() + "','" + dr["PRINTTIMES"].ToString().Trim() + "','" + dr["resultfile"].ToString().Trim() + "','" + dr["CHECKDATE"].ToString().Trim() + "','" + dr["CHECKTIME"].ToString().Trim() + "','" + dr["SERIALNO"].ToString().Trim() + "','" + dr["REQUESTSOURCE"].ToString().Trim() + "','" + dr["DIAGNO"].ToString().Trim() + "','" + dr["SICKTYPENO"].ToString().Trim() + "','" + dr["FORMCOMMENT"].ToString().Trim() + "','" + dr["ARTIFICERORDER"].ToString().Trim() + "','" + dr["SICKORDER"].ToString().Trim() + "','" + dr["SICKTYPE"].ToString().Trim() + "','" + dr["CHARGEFLAG"].ToString().Trim() + "','" + dr["TESTDEST"].ToString().Trim() + "','" + dr["SLABLE"].ToString().Trim() + "','" + dr["ZDY1"].ToString().Trim() + "','" + dr["ZDY2"].ToString().Trim() + "','" + dr["ZDY3"].ToString().Trim() + "','" + dr["ZDY4"].ToString().Trim() + "','" + dr["ZDY5"].ToString().Trim() + "','" + dr["INCEPTDATE"].ToString().Trim() + "','" + dr["INCEPTTIME"].ToString().Trim() + "','" + dr["INCEPTER"].ToString().Trim() + "','" + dr["ONLINEDATE"].ToString().Trim() + "','" + dr["ONLINETIME"].ToString().Trim() + "','" + dr["BMANNO"].ToString().Trim() + "','" + dr["FILETYPE"].ToString().Trim() + "','" + dr["JPGFILE"].ToString().Trim() + "','" + dr["PDFFILE"].ToString().Trim() + "','" + dr["FORMNO"].ToString().Trim() + "','" + dr["CHILDTABLENAME"].ToString().Trim() + "','" + dr["PRINTEXEC"].ToString().Trim() + "','" + dr["LABCENTER"].ToString().Trim() + "','" + dr["PRINTTEXEC"].ToString().Trim() + "','" + dr["SECTIONTYPE"].ToString().Trim() + "','" + dr["BARCODE"].ToString().Trim() + "','" + dr["SECTIONSHORTNAME"].ToString().Trim() + "','" + dr["SECTIONSHORTCODE"].ToString().Trim() + "','" + dr["DIAGNOSE"].ToString().Trim() + "','" + dr["CLIENTNAME"].ToString().Trim() + "','" + dr["OldSerialNo"].ToString().Trim() + "'");
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ReportFormFull set ");

                strSql.Append(" CLIENTNO = '" + dr["CLIENTNO"].ToString().Trim() + "' , ");
                strSql.Append(" CNAME = '" + dr["CNAME"].ToString().Trim() + "' , ");
                strSql.Append(" AGEUNITNAME = '" + dr["AGEUNITNAME"].ToString().Trim() + "' , ");
                strSql.Append(" GENDERNAME = '" + dr["GENDERNAME"].ToString().Trim() + "' , ");
                strSql.Append(" DEPTNAME = '" + dr["DEPTNAME"].ToString().Trim() + "' , ");
                strSql.Append(" DOCTORNAME = '" + dr["DOCTORNAME"].ToString().Trim() + "' , ");
                strSql.Append(" DISTRICTNAME = '" + dr["DISTRICTNAME"].ToString().Trim() + "' , ");
                strSql.Append(" WARDNAME = '" + dr["WARDNAME"].ToString().Trim() + "' , ");
                strSql.Append(" FOLKNAME = '" + dr["FOLKNAME"].ToString().Trim() + "' , ");
                strSql.Append(" SICKTYPENAME = '" + dr["SICKTYPENAME"].ToString().Trim() + "' , ");
                strSql.Append(" SAMPLETYPENAME = '" + dr["SAMPLETYPENAME"].ToString().Trim() + "' , ");
                strSql.Append(" SECTIONNAME = '" + dr["SECTIONNAME"].ToString().Trim() + "' , ");
                strSql.Append(" TESTTYPENAME = '" + dr["TESTTYPENAME"].ToString().Trim() + "' , ");
                strSql.Append(" RECEIVEDATE = '" + dr["RECEIVEDATE"].ToString().Trim() + "' , ");
                strSql.Append(" SECTIONNO = '" + dr["SECTIONNO"].ToString().Trim() + "' , ");
                strSql.Append(" TESTTYPENO = '" + dr["TESTTYPENO"].ToString().Trim() + "' , ");
                strSql.Append(" SAMPLENO = '" + dr["SAMPLENO"].ToString().Trim() + "' , ");
                strSql.Append(" STATUSNO = '" + dr["STATUSNO"].ToString().Trim() + "' , ");
                strSql.Append(" SAMPLETYPENO = '" + dr["SAMPLETYPENO"].ToString().Trim() + "' , ");
                strSql.Append(" PATNO = '" + dr["PATNO"].ToString().Trim() + "' , ");
                strSql.Append(" GENDERNO = '" + dr["GENDERNO"].ToString().Trim() + "' , ");
                strSql.Append(" BIRTHDAY = '" + dr["BIRTHDAY"].ToString().Trim() + "' , ");
                strSql.Append(" AGE = '" + dr["AGE"].ToString().Trim() + "' , ");
                strSql.Append(" AGEUNITNO = '" + dr["AGEUNITNO"].ToString().Trim() + "' , ");
                strSql.Append(" FOLKNO = '" + dr["FOLKNO"].ToString().Trim() + "' , ");
                strSql.Append(" DISTRICTNO = '" + dr["DISTRICTNO"].ToString().Trim() + "' , ");
                strSql.Append(" WARDNO = '" + dr["WARDNO"].ToString().Trim() + "' , ");
                strSql.Append(" BED = '" + dr["BED"].ToString().Trim() + "' , ");
                strSql.Append(" DEPTNO = '" + dr["DEPTNO"].ToString().Trim() + "' , ");
                strSql.Append(" DOCTOR = '" + dr["DOCTOR"].ToString().Trim() + "' , ");
                strSql.Append(" CHARGENO = '" + dr["CHARGENO"].ToString().Trim() + "' , ");
                strSql.Append(" CHARGE = '" + dr["CHARGE"].ToString().Trim() + "' , ");
                strSql.Append(" COLLECTER = '" + dr["COLLECTER"].ToString().Trim() + "' , ");
                strSql.Append(" COLLECTDATE = '" + dr["COLLECTDATE"].ToString().Trim() + "' , ");
                strSql.Append(" COLLECTTIME = '" + dr["COLLECTTIME"].ToString().Trim() + "' , ");
                strSql.Append(" FORMMEMO = '" + dr["FORMMEMO"].ToString().Trim() + "' , ");
                strSql.Append(" TECHNICIAN = '" + dr["TECHNICIAN"].ToString().Trim() + "' , ");
                strSql.Append(" TESTDATE = '" + dr["TESTDATE"].ToString().Trim() + "' , ");
                strSql.Append(" TESTTIME = '" + dr["TESTTIME"].ToString().Trim() + "' , ");
                strSql.Append(" OPERATOR = '" + dr["OPERATOR"].ToString().Trim() + "' , ");
                strSql.Append(" OPERDATE = '" + dr["OPERDATE"].ToString().Trim() + "' , ");
                strSql.Append(" OPERTIME = '" + dr["OPERTIME"].ToString().Trim() + "' , ");
                strSql.Append(" CHECKER = '" + dr["CHECKER"].ToString().Trim() + "' , ");
                strSql.Append(" PRINTTIMES = '" + dr["PRINTTIMES"].ToString().Trim() + "' , ");
                strSql.Append(" resultfile = '" + dr["resultfile"].ToString().Trim() + "' , ");
                strSql.Append(" CHECKDATE = '" + dr["CHECKDATE"].ToString().Trim() + "' , ");
                strSql.Append(" CHECKTIME = '" + dr["CHECKTIME"].ToString().Trim() + "' , ");
                strSql.Append(" SERIALNO = '" + dr["SERIALNO"].ToString().Trim() + "' , ");
                strSql.Append(" REQUESTSOURCE = '" + dr["REQUESTSOURCE"].ToString().Trim() + "' , ");
                strSql.Append(" DIAGNO = '" + dr["DIAGNO"].ToString().Trim() + "' , ");
                strSql.Append(" SICKTYPENO = '" + dr["SICKTYPENO"].ToString().Trim() + "' , ");
                strSql.Append(" FORMCOMMENT = '" + dr["FORMCOMMENT"].ToString().Trim() + "' , ");
                strSql.Append(" ARTIFICERORDER = '" + dr["ARTIFICERORDER"].ToString().Trim() + "' , ");
                strSql.Append(" SICKORDER = '" + dr["SICKORDER"].ToString().Trim() + "' , ");
                strSql.Append(" SICKTYPE = '" + dr["SICKTYPE"].ToString().Trim() + "' , ");
                strSql.Append(" CHARGEFLAG = '" + dr["CHARGEFLAG"].ToString().Trim() + "' , ");
                strSql.Append(" TESTDEST = '" + dr["TESTDEST"].ToString().Trim() + "' , ");
                strSql.Append(" SLABLE = '" + dr["SLABLE"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY1 = '" + dr["ZDY1"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY2 = '" + dr["ZDY2"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY3 = '" + dr["ZDY3"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY4 = '" + dr["ZDY4"].ToString().Trim() + "' , ");
                strSql.Append(" ZDY5 = '" + dr["ZDY5"].ToString().Trim() + "' , ");
                strSql.Append(" INCEPTDATE = '" + dr["INCEPTDATE"].ToString().Trim() + "' , ");
                strSql.Append(" INCEPTTIME = '" + dr["INCEPTTIME"].ToString().Trim() + "' , ");
                strSql.Append(" INCEPTER = '" + dr["INCEPTER"].ToString().Trim() + "' , ");
                strSql.Append(" ONLINEDATE = '" + dr["ONLINEDATE"].ToString().Trim() + "' , ");
                strSql.Append(" ONLINETIME = '" + dr["ONLINETIME"].ToString().Trim() + "' , ");
                strSql.Append(" BMANNO = '" + dr["BMANNO"].ToString().Trim() + "' , ");
                strSql.Append(" FILETYPE = '" + dr["FILETYPE"].ToString().Trim() + "' , ");
                strSql.Append(" JPGFILE = '" + dr["JPGFILE"].ToString().Trim() + "' , ");
                strSql.Append(" PDFFILE = '" + dr["PDFFILE"].ToString().Trim() + "' , ");
                strSql.Append(" FORMNO = '" + dr["FORMNO"].ToString().Trim() + "' , ");
                strSql.Append(" CHILDTABLENAME = '" + dr["CHILDTABLENAME"].ToString().Trim() + "' , ");
                strSql.Append(" PRINTEXEC = '" + dr["PRINTEXEC"].ToString().Trim() + "' , ");
                strSql.Append(" LABCENTER = '" + dr["LABCENTER"].ToString().Trim() + "' , ");
                strSql.Append(" PRINTTEXEC = '" + dr["PRINTTEXEC"].ToString().Trim() + "' , ");
                strSql.Append(" SECTIONTYPE = '" + dr["SECTIONTYPE"].ToString().Trim() + "' , ");
                strSql.Append(" BARCODE = '" + dr["BARCODE"].ToString().Trim() + "' , ");
                strSql.Append(" SECTIONSHORTNAME = '" + dr["SECTIONSHORTNAME"].ToString().Trim() + "' , ");
                strSql.Append(" SECTIONSHORTCODE = '" + dr["SECTIONSHORTCODE"].ToString().Trim() + "' , ");
                strSql.Append(" DIAGNOSE = '" + dr["DIAGNOSE"].ToString().Trim() + "' , ");
                strSql.Append(" CLIENTNAME = '" + dr["CLIENTNAME"].ToString().Trim() + "' , ");
                strSql.Append(" OldSerialNo = '" + dr["OldSerialNo"].ToString().Trim() + "'  ");
                strSql.Append(" where ReportFormID='" + dr["ReportFormID"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region IDReportFormFull 成员


        public DataSet GetColumnByView()
        {
            return DbHelperSQL.ExecuteDataSet(" SELECT * FROM SysColumns WHERE id=Object_Id('view_reportformfull') ");
        }
        public DataSet GetColumnByTable()
        {
            return DbHelperSQL.ExecuteDataSet(" SELECT * FROM SysColumns WHERE id=Object_Id('reportformfull')  union  SELECT * FROM SysColumns WHERE id=Object_Id('reportitemfull')");
        }
        public DataSet GetMatchList(ZhiFang.Model.ReportFormFull model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 ReportFormFull.SerialNo as BarCode,ReportFormFull.*,ReportItemFull.*");
                strSql.Append(" FROM ReportFormFull join ReportItemFull on ReportFormFull.ReportFormID=ReportItemFull.ReportFormID");
                strSql.Append(" where 1=1 ");
                if (model.CLIENTNO != null)
                {
                    strSql.Append(" and CLIENTNO='" + model.CLIENTNO + "' ");
                }
                if (model.CNAME != null)
                {
                    strSql.Append(" and CNAME='" + model.CNAME + "' ");
                }
                if (model.AGEUNITNAME != null)
                {
                    strSql.Append(" and AGEUNITNAME='" + model.AGEUNITNAME + "' ");
                }
                if (model.GENDERNAME != null)
                {
                    strSql.Append(" and GENDERNAME='" + model.GENDERNAME + "' ");
                }
                if (model.DEPTNAME != null)
                {
                    strSql.Append(" and DEPTNAME='" + model.DEPTNAME + "' ");
                }
                if (model.DOCTORNAME != null)
                {
                    strSql.Append(" and DOCTORNAME='" + model.DOCTORNAME + "' ");
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
                if (model.SAMPLETYPENAME != null)
                {
                    strSql.Append(" and SAMPLETYPENAME='" + model.SAMPLETYPENAME + "' ");
                }

                if (model.SECTIONNAME != null)
                {
                    strSql.Append(" and SECTIONNAME='" + model.SECTIONNAME + "' ");
                }

                if (model.TESTTYPENAME != null)
                {
                    strSql.Append(" and TESTTYPENAME='" + model.TESTTYPENAME + "' ");
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
                if (model.STATUSNO != null)
                {
                    strSql.Append(" and STATUSNO=" + model.STATUSNO + " ");
                }

                if (model.SAMPLETYPENO != null)
                {
                    strSql.Append(" and SAMPLETYPENO=" + model.SAMPLETYPENO + " ");
                }

                if (model.PATNO != null)
                {
                    strSql.Append(" and PATNO='" + model.PATNO + "' ");
                }

                if (model.GENDERNO != null)
                {
                    strSql.Append(" and GENDERNO=" + model.GENDERNO + " ");
                }

                if (model.BIRTHDAY != null)
                {
                    strSql.Append(" and BIRTHDAY='" + model.BIRTHDAY + "' ");
                }

                if (model.AGE != null)
                {
                    strSql.Append(" and AGE='" + model.AGE + "' ");
                }

                if (model.AGEUNITNO != null)
                {
                    strSql.Append(" and AGEUNITNO=" + model.AGEUNITNO + " ");
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

                if (model.BED != null)
                {
                    strSql.Append(" and BED='" + model.BED + "' ");
                }

                if (model.DEPTNO != null)
                {
                    strSql.Append(" and DEPTNO=" + model.DEPTNO + " ");
                }

                if (model.DOCTOR != null)
                {
                    strSql.Append(" and DOCTOR='" + model.DOCTOR + "' ");
                }

                if (model.CHARGENO != null)
                {
                    strSql.Append(" and CHARGENO='" + model.CHARGENO + "' ");
                }

                if (model.CHARGE != null)
                {
                    strSql.Append(" and CHARGE='" + model.CHARGE + "' ");
                }

                if (model.COLLECTER != null)
                {
                    strSql.Append(" and COLLECTER='" + model.COLLECTER + "' ");
                }

                if (model.COLLECTDATE != null)
                {
                    strSql.Append(" and COLLECTDATE='" + model.COLLECTDATE + "' ");
                }

                if (model.COLLECTTIME != null)
                {
                    strSql.Append(" and COLLECTTIME='" + model.COLLECTTIME + "' ");
                }

                if (model.FORMMEMO != null)
                {
                    strSql.Append(" and FORMMEMO='" + model.FORMMEMO + "' ");
                }

                if (model.TECHNICIAN != null)
                {
                    strSql.Append(" and TECHNICIAN='" + model.TECHNICIAN + "' ");
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

                if (model.OPERDATE != null)
                {
                    strSql.Append(" and OPERDATE='" + model.OPERDATE + "' ");
                }

                if (model.OPERTIME != null)
                {
                    strSql.Append(" and OPERTIME='" + model.OPERTIME + "' ");
                }

                if (model.CHECKER != null)
                {
                    strSql.Append(" and CHECKER='" + model.CHECKER + "' ");
                }

                if (model.PRINTTIMES != null)
                {
                    strSql.Append(" and PRINTTIMES=" + model.PRINTTIMES + " ");
                }

                if (model.resultfile != null)
                {
                    strSql.Append(" and resultfile='" + model.resultfile + "' ");
                }

                if (model.CHECKDATE != null)
                {
                    strSql.Append(" and CHECKDATE='" + model.CHECKDATE + "' ");
                }

                if (model.CHECKTIME != null)
                {
                    strSql.Append(" and CHECKTIME='" + model.CHECKTIME + "' ");
                }

                if (model.SERIALNO != null)
                {
                    strSql.Append(" and SERIALNO='" + model.SERIALNO + "' ");
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

                if (model.SICKORDER != null)
                {
                    strSql.Append(" and SICKORDER='" + model.SICKORDER + "' ");
                }

                if (model.SICKTYPE != null)
                {
                    strSql.Append(" and SICKTYPE='" + model.SICKTYPE + "' ");
                }

                if (model.CHARGEFLAG != null)
                {
                    strSql.Append(" and CHARGEFLAG='" + model.CHARGEFLAG + "' ");
                }

                if (model.TESTDEST != null)
                {
                    strSql.Append(" and TESTDEST='" + model.TESTDEST + "' ");
                }

                if (model.SLABLE != null)
                {
                    strSql.Append(" and SLABLE='" + model.SLABLE + "' ");
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

                if (model.INCEPTDATE != null)
                {
                    strSql.Append(" and INCEPTDATE='" + model.INCEPTDATE + "' ");
                }

                if (model.INCEPTTIME != null)
                {
                    strSql.Append(" and INCEPTTIME='" + model.INCEPTTIME + "' ");
                }

                if (model.INCEPTER != null)
                {
                    strSql.Append(" and INCEPTER='" + model.INCEPTER + "' ");
                }

                if (model.ONLINEDATE != null)
                {
                    strSql.Append(" and ONLINEDATE='" + model.ONLINEDATE + "' ");
                }

                if (model.ONLINETIME != null)
                {
                    strSql.Append(" and ONLINETIME='" + model.ONLINETIME + "' ");
                }

                if (model.BMANNO != null)
                {
                    strSql.Append(" and BMANNO='" + model.BMANNO + "' ");
                }

                if (model.FILETYPE != null)
                {
                    strSql.Append(" and FILETYPE='" + model.FILETYPE + "' ");
                }

                if (model.JPGFILE != null)
                {
                    strSql.Append(" and JPGFILE='" + model.JPGFILE + "' ");
                }

                if (model.PDFFILE != null)
                {
                    strSql.Append(" and PDFFILE='" + model.PDFFILE + "' ");
                }

                if (model.FORMNO != null)
                {
                    strSql.Append(" and FORMNO=" + model.FORMNO + " ");
                }

                if (model.CHILDTABLENAME != null)
                {
                    strSql.Append(" and CHILDTABLENAME='" + model.CHILDTABLENAME + "' ");
                }

                if (model.PRINTEXEC != null)
                {
                    strSql.Append(" and PRINTEXEC='" + model.PRINTEXEC + "' ");
                }
                if (model.LABCENTER != null)
                {
                    strSql.Append(" and LABCENTER='" + model.LABCENTER + "' ");
                }

                //if (model.PRINTTEXEC != null)
                //{
                //    if (model.PRINTTEXEC == "0")
                //    {
                //        strSql.Append(" and PRINTTIMES<=0 ");
                //    }
                //    if (model.PRINTTEXEC == "1")
                //    {
                //        strSql.Append(" and PRINTTIMES>0 ");
                //    }
                //}

                if (model.Startdate != null)
                {
                    strSql.Append(" and ReportFormFull.ReceiveDate>='" + model.Startdate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.Enddate != null)
                {
                    if (model.Enddate.Value.TimeOfDay.Ticks > 0)
                    {
                        strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                    }
                    else
                    {
                        strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToShortDateString() + " 23:59:59" + "' ");
                    }
                }

                if (model.CheckStartDate != null)
                {
                    strSql.Append(" and checkdate>='" + model.CheckStartDate.Value.ToShortDateString() + "' ");
                }

                if (model.CheckEndDate != null)
                {
                    strSql.Append(" and checkdate<='" + model.CheckEndDate.Value.ToShortDateString() + "' ");
                }

                if (model.BarCode != null)
                {
                    strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
                }

                if (model.SectionType != null)
                {
                    strSql.Append(" and SectionType='" + model.SectionType.Trim() + "' ");
                }
                if (model.DIAGNOSE != null)
                {
                    strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
                }

                if (model.DIAGNOSE != null)
                {
                    strSql.Append(" and DIAGNOSE='" + model.DIAGNOSE.Trim() + "' ");
                }
                if (model.CLIENTNAME != null)
                {
                    strSql.Append(" and CLIENTNAME='" + model.CLIENTNAME.Trim() + "' ");
                }
                if (model.ReportFormID != null)
                {
                    strSql.Append(" and ReportFormID='" + model.ReportFormID.Trim() + "' ");
                }

                if (model.ClientList != null && model.ClientList.Trim().Length > 0)
                {
                    strSql.Append(" and ClientNo in (" + model.ClientList + ") ");
                }
                if (model.RBACSQL != null && model.RBACSQL.Trim() != "")
                {
                    strSql.Append(" and " + model.RBACSQL.Trim() + " ");
                }
                //WriteLog.WriteData.WriteLine(strSql.ToString() + "@" + DbHelperSQL.connectionString );
                Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception e)
            {
                Common.Log.Log.Info(e.ToString());
                return null;
            }
        }
        public DataSet GetAllList(ZhiFang.Model.ReportFormFull model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ReportFormFulll.OldSerialNo as BarCode,ReportFormFull.*,ReportItemFull.*,ReportMarrowFull.*,ReportMicroFull.*");
            strSql.Append(" FROM ReportFormFull join ReportItemFull on ReportFormFull.ReportFormID=ReportItemFull.ReportFormID");
            strSql.Append(" join ReportMarrowFull on ReportMarrowFull.ReportFormID=ReportFormFull.ReportFormID");
            strSql.Append(" join ReportMicroFull on ReportMicroFull.ReportFormID=ReportFormFull.ReportFormID ");
            strSql.Append(" where 1=1 ");
            if (model.CLIENTNO != null)
            {
                strSql.Append(" and CLIENTNO='" + model.CLIENTNO + "' ");
            }
            if (model.CNAME != null)
            {
                strSql.Append(" and CNAME='" + model.CNAME + "' ");
            }
            if (model.AGEUNITNAME != null)
            {
                strSql.Append(" and AGEUNITNAME='" + model.AGEUNITNAME + "' ");
            }
            if (model.GENDERNAME != null)
            {
                strSql.Append(" and GENDERNAME='" + model.GENDERNAME + "' ");
            }
            if (model.DEPTNAME != null)
            {
                strSql.Append(" and DEPTNAME='" + model.DEPTNAME + "' ");
            }
            if (model.DOCTORNAME != null)
            {
                strSql.Append(" and DOCTORNAME='" + model.DOCTORNAME + "' ");
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
            if (model.SAMPLETYPENAME != null)
            {
                strSql.Append(" and SAMPLETYPENAME='" + model.SAMPLETYPENAME + "' ");
            }

            if (model.SECTIONNAME != null)
            {
                strSql.Append(" and SECTIONNAME='" + model.SECTIONNAME + "' ");
            }

            if (model.TESTTYPENAME != null)
            {
                strSql.Append(" and TESTTYPENAME='" + model.TESTTYPENAME + "' ");
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
            if (model.STATUSNO != null)
            {
                strSql.Append(" and STATUSNO=" + model.STATUSNO + " ");
            }

            if (model.SAMPLETYPENO != null)
            {
                strSql.Append(" and SAMPLETYPENO=" + model.SAMPLETYPENO + " ");
            }

            if (model.PATNO != null)
            {
                strSql.Append(" and PATNO='" + model.PATNO + "' ");
            }

            if (model.GENDERNO != null)
            {
                strSql.Append(" and GENDERNO=" + model.GENDERNO + " ");
            }

            if (model.BIRTHDAY != null)
            {
                strSql.Append(" and BIRTHDAY='" + model.BIRTHDAY + "' ");
            }

            if (model.AGE != null)
            {
                strSql.Append(" and AGE='" + model.AGE + "' ");
            }

            if (model.AGEUNITNO != null)
            {
                strSql.Append(" and AGEUNITNO=" + model.AGEUNITNO + " ");
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

            if (model.BED != null)
            {
                strSql.Append(" and BED='" + model.BED + "' ");
            }

            if (model.DEPTNO != null)
            {
                strSql.Append(" and DEPTNO=" + model.DEPTNO + " ");
            }

            if (model.DOCTOR != null)
            {
                strSql.Append(" and DOCTOR='" + model.DOCTOR + "' ");
            }

            if (model.CHARGENO != null)
            {
                strSql.Append(" and CHARGENO='" + model.CHARGENO + "' ");
            }

            if (model.CHARGE != null)
            {
                strSql.Append(" and CHARGE='" + model.CHARGE + "' ");
            }

            if (model.COLLECTER != null)
            {
                strSql.Append(" and COLLECTER='" + model.COLLECTER + "' ");
            }

            if (model.COLLECTDATE != null)
            {
                strSql.Append(" and COLLECTDATE='" + model.COLLECTDATE + "' ");
            }

            if (model.COLLECTTIME != null)
            {
                strSql.Append(" and COLLECTTIME='" + model.COLLECTTIME + "' ");
            }

            if (model.FORMMEMO != null)
            {
                strSql.Append(" and FORMMEMO='" + model.FORMMEMO + "' ");
            }

            if (model.TECHNICIAN != null)
            {
                strSql.Append(" and TECHNICIAN='" + model.TECHNICIAN + "' ");
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

            if (model.OPERDATE != null)
            {
                strSql.Append(" and OPERDATE='" + model.OPERDATE + "' ");
            }

            if (model.OPERTIME != null)
            {
                strSql.Append(" and OPERTIME='" + model.OPERTIME + "' ");
            }

            if (model.CHECKER != null)
            {
                strSql.Append(" and CHECKER='" + model.CHECKER + "' ");
            }

            if (model.PRINTTIMES != null)
            {
                strSql.Append(" and PRINTTIMES=" + model.PRINTTIMES + " ");
            }

            if (model.resultfile != null)
            {
                strSql.Append(" and resultfile='" + model.resultfile + "' ");
            }

            if (model.CHECKDATE != null)
            {
                strSql.Append(" and CHECKDATE='" + model.CHECKDATE + "' ");
            }

            if (model.CHECKTIME != null)
            {
                strSql.Append(" and CHECKTIME='" + model.CHECKTIME + "' ");
            }

            if (model.SERIALNO != null)
            {
                strSql.Append(" and SERIALNO='" + model.SERIALNO + "' ");
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

            if (model.SICKORDER != null)
            {
                strSql.Append(" and SICKORDER='" + model.SICKORDER + "' ");
            }

            if (model.SICKTYPE != null)
            {
                strSql.Append(" and SICKTYPE='" + model.SICKTYPE + "' ");
            }

            if (model.CHARGEFLAG != null)
            {
                strSql.Append(" and CHARGEFLAG='" + model.CHARGEFLAG + "' ");
            }

            if (model.TESTDEST != null)
            {
                strSql.Append(" and TESTDEST='" + model.TESTDEST + "' ");
            }

            if (model.SLABLE != null)
            {
                strSql.Append(" and SLABLE='" + model.SLABLE + "' ");
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

            if (model.INCEPTDATE != null)
            {
                strSql.Append(" and INCEPTDATE='" + model.INCEPTDATE + "' ");
            }

            if (model.INCEPTTIME != null)
            {
                strSql.Append(" and INCEPTTIME='" + model.INCEPTTIME + "' ");
            }

            if (model.INCEPTER != null)
            {
                strSql.Append(" and INCEPTER='" + model.INCEPTER + "' ");
            }

            if (model.ONLINEDATE != null)
            {
                strSql.Append(" and ONLINEDATE='" + model.ONLINEDATE + "' ");
            }

            if (model.ONLINETIME != null)
            {
                strSql.Append(" and ONLINETIME='" + model.ONLINETIME + "' ");
            }

            if (model.BMANNO != null)
            {
                strSql.Append(" and BMANNO='" + model.BMANNO + "' ");
            }

            if (model.FILETYPE != null)
            {
                strSql.Append(" and FILETYPE='" + model.FILETYPE + "' ");
            }

            if (model.JPGFILE != null)
            {
                strSql.Append(" and JPGFILE='" + model.JPGFILE + "' ");
            }

            if (model.PDFFILE != null)
            {
                strSql.Append(" and PDFFILE='" + model.PDFFILE + "' ");
            }

            if (model.FORMNO != null)
            {
                strSql.Append(" and FORMNO=" + model.FORMNO + " ");
            }

            if (model.CHILDTABLENAME != null)
            {
                strSql.Append(" and CHILDTABLENAME='" + model.CHILDTABLENAME + "' ");
            }

            if (model.PRINTEXEC != null)
            {
                strSql.Append(" and PRINTEXEC='" + model.PRINTEXEC + "' ");
            }
            if (model.LABCENTER != null)
            {
                strSql.Append(" and LABCENTER='" + model.LABCENTER + "' ");
            }

            //if (model.PRINTTEXEC != null)
            //{
            //    if (model.PRINTTEXEC == "0")
            //    {
            //        strSql.Append(" and PRINTTIMES<=0 ");
            //    }
            //    if (model.PRINTTEXEC == "1")
            //    {
            //        strSql.Append(" and PRINTTIMES>0 ");
            //    }
            //}

            if (model.Startdate != null)
            {
                strSql.Append(" and ReportFormFull.ReceiveDate>='" + model.Startdate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            }

            if (model.Enddate != null)
            {
                if (model.Enddate.Value.TimeOfDay.Ticks > 0)
                {
                    strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }
                else
                {
                    strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToShortDateString() + " 23:59:59" + "' ");
                }
            }

            if (model.CheckStartDate != null)
            {
                strSql.Append(" and checkdate>='" + model.CheckStartDate.Value.ToShortDateString() + "' ");
            }

            if (model.CheckEndDate != null)
            {
                strSql.Append(" and checkdate<='" + model.CheckEndDate.Value.ToShortDateString() + "' ");
            }

            if (model.BarCode != null)
            {
                strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
            }

            if (model.SectionType != null)
            {
                strSql.Append(" and SectionType='" + model.SectionType.Trim() + "' ");
            }
            if (model.DIAGNOSE != null)
            {
                strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
            }

            if (model.DIAGNOSE != null)
            {
                strSql.Append(" and DIAGNOSE='" + model.DIAGNOSE.Trim() + "' ");
            }
            if (model.CLIENTNAME != null)
            {
                strSql.Append(" and CLIENTNAME='" + model.CLIENTNAME.Trim() + "' ");
            }
            if (model.ReportFormID != null)
            {
                strSql.Append(" and ReportFormID='" + model.ReportFormID.Trim() + "' ");
            }

            if (model.ClientList != null && model.ClientList.Trim().Length > 0)
            {
                strSql.Append(" and ClientNo in (" + model.ClientList + ") ");
            }
            if (model.RBACSQL != null && model.RBACSQL.Trim() != "")
            {
                strSql.Append(" and " + model.RBACSQL.Trim() + " ");
            }
            //WriteLog.WriteData.WriteLine(strSql.ToString() + "@" + DbHelperSQL.connectionString );
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            //return null;
        }
        #endregion


        public int InsertSql(string sql)
        {
            return DbHelperSQL.ExecuteNonQuery(sql);
        }

        public int DeleteByWhere(string Strwhere)
        {
            string str = "delete from ReportFormFull where " + Strwhere;
            return DbHelperSQL.ExecuteNonQuery(str);
        }

        public int BackUpReportFormFullByWhere(string Strwhere)
        {
            //string str = " insert into ReportFormFull_BackUp  select * from ReportFormFull where " + Strwhere;
            StringBuilder strb = new StringBuilder();
            strb.Append(" insert into ReportFormFull_BackUp");
            strb.Append(" ([ReportFormID] ");
            strb.Append("  ,[CLIENTNO]");
            strb.Append("   ,[CNAME]");
            strb.Append(" ,[AGEUNITNAME]");
            strb.Append("  ,[GENDERNAME]");
            strb.Append("   ,[DEPTNAME]");
            strb.Append("   ,[DOCTORNAME]");
            strb.Append("   ,[DISTRICTNAME]");
            strb.Append("  ,[WARDNAME]");
            strb.Append("   ,[FOLKNAME]");
            strb.Append("  ,[SICKTYPENAME]");
            strb.Append("  ,[SAMPLETYPENAME]");
            strb.Append("  ,[SECTIONNAME]");
            strb.Append("  ,[TESTTYPENAME]");
            strb.Append("  ,[RECEIVEDATE]");
            strb.Append("  ,[SECTIONNO]");
            strb.Append("   ,[TESTTYPENO]");
            strb.Append("   ,[SAMPLENO]");
            strb.Append("   ,[STATUSNO]");
            strb.Append("   ,[SAMPLETYPENO]");
            strb.Append("   ,[PATNO]");
            strb.Append("   ,[GENDERNO]");
            strb.Append("  ,[BIRTHDAY]");
            strb.Append("  ,[AGE]");
            strb.Append("   ,[AGEUNITNO]");
            strb.Append("     ,[FOLKNO]");
            strb.Append("   ,[DISTRICTNO]");
            strb.Append("    ,[WARDNO]");
            strb.Append("   ,[BED]");
            strb.Append("   ,[DEPTNO]");
            strb.Append("   ,[DOCTOR]");
            strb.Append("   ,[CHARGENO]");
            strb.Append("  ,[CHARGE]");
            strb.Append("  ,[COLLECTER]");
            strb.Append("   ,[COLLECTDATE]");
            strb.Append("   ,[COLLECTTIME]");
            strb.Append("  ,[FORMMEMO]");
            strb.Append("   ,[TECHNICIAN]");
            strb.Append("  ,[TESTDATE]");
            strb.Append("  ,[TESTTIME]");
            strb.Append("    ,[OPERATOR]");
            strb.Append("   ,[OPERDATE]");
            strb.Append("   ,[OPERTIME]");
            strb.Append("   ,[CHECKER]");
            strb.Append("   ,[PRINTTIMES]");
            strb.Append("  ,[resultfile]");
            strb.Append("   ,[CHECKDATE]");
            strb.Append("   ,[CHECKTIME]");
            strb.Append("   ,[SERIALNO]");
            strb.Append("    ,[REQUESTSOURCE]");
            strb.Append("   ,[DIAGNO]");
            strb.Append("    ,[SICKTYPENO]");
            strb.Append("    ,[FORMCOMMENT]");
            strb.Append("  ,[ARTIFICERORDER]");
            strb.Append("  ,[SICKORDER]");
            strb.Append("   ,[SICKTYPE]");
            strb.Append("   ,[CHARGEFLAG]");
            strb.Append("   ,[TESTDEST]");
            strb.Append("   ,[SLABLE]");
            strb.Append("   ,[ZDY1]");
            strb.Append("   ,[ZDY2]");
            strb.Append("   ,[ZDY3]");
            strb.Append("   ,[ZDY4]");
            strb.Append("   ,[ZDY5]");
            strb.Append("  ,[INCEPTDATE]");
            strb.Append("   ,[INCEPTTIME]");
            strb.Append("   ,[INCEPTER]");
            strb.Append("    ,[ONLINEDATE]");
            strb.Append("    ,[ONLINETIME]");
            strb.Append("     ,[BMANNO]");
            strb.Append("    ,[FILETYPE]");
            strb.Append("     ,[JPGFILE]");
            strb.Append("    ,[PDFFILE]");
            strb.Append("     ,[FORMNO]");
            strb.Append("    ,[CHILDTABLENAME]");
            strb.Append("   ,[PRINTEXEC]");
            strb.Append("  ,[LABCENTER]");
            strb.Append("    ,[CheckName]");
            strb.Append("   ,[CheckNo]");
            strb.Append("    ,[CLIENTNAME]");
            strb.Append("    ,[BARCODE]");
            strb.Append("   ,[PRINTDATETIME]");
            strb.Append("   ,[PRINTTEXEC]");
            strb.Append("  ,[UploadDate]");
            strb.Append("   ,[isdown]");
            strb.Append("   ,[SECTIONTYPE]");
            strb.Append("    ,[SECTIONSHORTNAME]");
            strb.Append("    ,[SECTIONSHORTCODE]");
            strb.Append("     ,[DIAGNOSE]");
            strb.Append("     ,[OldSerialno]");
            strb.Append("     ,[AreaSendFlag]");
            strb.Append("      ,[AreaSendTime]");
            strb.Append("    ,[GenderEname]");
            strb.Append("     ,[SickEname]");
            strb.Append("   ,[sampletypeename]");
            strb.Append("     ,[folkename]");
            strb.Append("      ,[Deptename]");
            strb.Append("     ,[districtename]");
            strb.Append("     ,[AgeUnitename]");
            strb.Append("      ,[TestType]");
            strb.Append("      ,[TestTypeename]");
            strb.Append("     ,[diag]");
            strb.Append("      ,[clientstyle]");
            strb.Append("     ,[ClientReportTitle]");
            strb.Append("      ,[ADDRESS]");
            strb.Append("     ,[czdy1]");
            strb.Append("     ,[czdy2]");
            strb.Append("   ,[czdy3]");
            strb.Append("     ,[czdy4]");
            strb.Append("    ,[czdy5]");
            strb.Append("   ,[czdy6]");
            strb.Append("   ,[Poperator]");
            strb.Append("   ,[PNOperator]");
            strb.Append("   ,[PSender2]");
            strb.Append("   ,[NOperDate]");
            strb.Append("    ,[NOPERTIME]");
            strb.Append("   ,[clientzdy3]");
            strb.Append("     ,[ZDY6]");
            strb.Append("     ,[ZDY7]");
            strb.Append("     ,[ZDY8]");
            strb.Append("     ,[ZDY9]");
            strb.Append("     ,[ZDY10]");
            strb.Append("      ,[clientename]");
            strb.Append("     ,[clientcode]");
            strb.Append("      ,[sectiondesc]");
            strb.Append("     ,[Ptechnician]");
            strb.Append("      ,[Pincepter]");
            strb.Append("       ,[receivetime]");
            strb.Append("       ,[CollectPart]");
            strb.Append("      ,[WebLisOrgID]");
            strb.Append("      ,[WebLisSourceOrgID]");
            strb.Append("      ,[TelNo]");
            strb.Append("      ,[resultstatus]");
            strb.Append("      ,[WebLisOrgName]");
            strb.Append("     ,[WebLisSourceOrgName]");
            strb.Append("     ,[StatusType]");
            strb.Append("     ,[PersonID]");
            strb.Append("     ,[ReportFormIndexID]) ");

            strb.Append(" select ");
            strb.Append(" [ReportFormID] ");
            strb.Append("  ,[CLIENTNO]");
            strb.Append("   ,[CNAME]");
            strb.Append(" ,[AGEUNITNAME]");
            strb.Append("  ,[GENDERNAME]");
            strb.Append("   ,[DEPTNAME]");
            strb.Append("   ,[DOCTORNAME]");
            strb.Append("   ,[DISTRICTNAME]");
            strb.Append("  ,[WARDNAME]");
            strb.Append("   ,[FOLKNAME]");
            strb.Append("  ,[SICKTYPENAME]");
            strb.Append("  ,[SAMPLETYPENAME]");
            strb.Append("  ,[SECTIONNAME]");
            strb.Append("  ,[TESTTYPENAME]");
            strb.Append("  ,[RECEIVEDATE]");
            strb.Append("  ,[SECTIONNO]");
            strb.Append("   ,[TESTTYPENO]");
            strb.Append("   ,[SAMPLENO]");
            strb.Append("   ,[STATUSNO]");
            strb.Append("   ,[SAMPLETYPENO]");
            strb.Append("   ,[PATNO]");
            strb.Append("   ,[GENDERNO]");
            strb.Append("  ,[BIRTHDAY]");
            strb.Append("  ,[AGE]");
            strb.Append("   ,[AGEUNITNO]");
            strb.Append("     ,[FOLKNO]");
            strb.Append("   ,[DISTRICTNO]");
            strb.Append("    ,[WARDNO]");
            strb.Append("   ,[BED]");
            strb.Append("   ,[DEPTNO]");
            strb.Append("   ,[DOCTOR]");
            strb.Append("   ,[CHARGENO]");
            strb.Append("  ,[CHARGE]");
            strb.Append("  ,[COLLECTER]");
            strb.Append("   ,[COLLECTDATE]");
            strb.Append("   ,[COLLECTTIME]");
            strb.Append("  ,[FORMMEMO]");
            strb.Append("   ,[TECHNICIAN]");
            strb.Append("  ,[TESTDATE]");
            strb.Append("  ,[TESTTIME]");
            strb.Append("    ,[OPERATOR]");
            strb.Append("   ,[OPERDATE]");
            strb.Append("   ,[OPERTIME]");
            strb.Append("   ,[CHECKER]");
            strb.Append("   ,[PRINTTIMES]");
            strb.Append("  ,[resultfile]");
            strb.Append("   ,[CHECKDATE]");
            strb.Append("   ,[CHECKTIME]");
            strb.Append("   ,[SERIALNO]");
            strb.Append("    ,[REQUESTSOURCE]");
            strb.Append("   ,[DIAGNO]");
            strb.Append("    ,[SICKTYPENO]");
            strb.Append("    ,[FORMCOMMENT]");
            strb.Append("  ,[ARTIFICERORDER]");
            strb.Append("  ,[SICKORDER]");
            strb.Append("   ,[SICKTYPE]");
            strb.Append("   ,[CHARGEFLAG]");
            strb.Append("   ,[TESTDEST]");
            strb.Append("   ,[SLABLE]");
            strb.Append("   ,[ZDY1]");
            strb.Append("   ,[ZDY2]");
            strb.Append("   ,[ZDY3]");
            strb.Append("   ,[ZDY4]");
            strb.Append("   ,[ZDY5]");
            strb.Append("  ,[INCEPTDATE]");
            strb.Append("   ,[INCEPTTIME]");
            strb.Append("   ,[INCEPTER]");
            strb.Append("    ,[ONLINEDATE]");
            strb.Append("    ,[ONLINETIME]");
            strb.Append("     ,[BMANNO]");
            strb.Append("    ,[FILETYPE]");
            strb.Append("     ,[JPGFILE]");
            strb.Append("    ,[PDFFILE]");
            strb.Append("     ,[FORMNO]");
            strb.Append("    ,[CHILDTABLENAME]");
            strb.Append("   ,[PRINTEXEC]");
            strb.Append("  ,[LABCENTER]");
            strb.Append("    ,[CheckName]");
            strb.Append("   ,[CheckNo]");
            strb.Append("    ,[CLIENTNAME]");
            strb.Append("    ,[BARCODE]");
            strb.Append("   ,[PRINTDATETIME]");
            strb.Append("   ,[PRINTTEXEC]");
            strb.Append("  ,[UploadDate]");
            strb.Append("   ,[isdown]");
            strb.Append("   ,[SECTIONTYPE]");
            strb.Append("    ,[SECTIONSHORTNAME]");
            strb.Append("    ,[SECTIONSHORTCODE]");
            strb.Append("     ,[DIAGNOSE]");
            strb.Append("     ,[OldSerialno]");
            strb.Append("     ,[AreaSendFlag]");
            strb.Append("      ,[AreaSendTime]");
            strb.Append("    ,[GenderEname]");
            strb.Append("     ,[SickEname]");
            strb.Append("   ,[sampletypeename]");
            strb.Append("     ,[folkename]");
            strb.Append("      ,[Deptename]");
            strb.Append("     ,[districtename]");
            strb.Append("     ,[AgeUnitename]");
            strb.Append("      ,[TestType]");
            strb.Append("      ,[TestTypeename]");
            strb.Append("     ,[diag]");
            strb.Append("      ,[clientstyle]");
            strb.Append("     ,[ClientReportTitle]");
            strb.Append("      ,[ADDRESS]");
            strb.Append("     ,[czdy1]");
            strb.Append("     ,[czdy2]");
            strb.Append("   ,[czdy3]");
            strb.Append("     ,[czdy4]");
            strb.Append("    ,[czdy5]");
            strb.Append("   ,[czdy6]");
            strb.Append("   ,[Poperator]");
            strb.Append("   ,[PNOperator]");
            strb.Append("   ,[PSender2]");
            strb.Append("   ,[NOperDate]");
            strb.Append("    ,[NOPERTIME]");
            strb.Append("   ,[clientzdy3]");
            strb.Append("     ,[ZDY6]");
            strb.Append("     ,[ZDY7]");
            strb.Append("     ,[ZDY8]");
            strb.Append("     ,[ZDY9]");
            strb.Append("     ,[ZDY10]");
            strb.Append("      ,[clientename]");
            strb.Append("     ,[clientcode]");
            strb.Append("      ,[sectiondesc]");
            strb.Append("     ,[Ptechnician]");
            strb.Append("      ,[Pincepter]");
            strb.Append("       ,[receivetime]");
            strb.Append("       ,[CollectPart]");
            strb.Append("      ,[WebLisOrgID]");
            strb.Append("      ,[WebLisSourceOrgID]");
            strb.Append("      ,[TelNo]");
            strb.Append("      ,[resultstatus]");
            strb.Append("      ,[WebLisOrgName]");
            strb.Append("     ,[WebLisSourceOrgName]");
            strb.Append("     ,[StatusType]");
            strb.Append("     ,[PersonID]");
            strb.Append("     ,[ReportFormIndexID] ");


            strb.Append(" from ReportFormFull where ");
            strb.Append(Strwhere);
            return DbHelperSQL.ExecuteNonQuery(strb.ToString());
        }

        public DataSet GetBarCode(string OrgID, string barCodeNo)
        {
            string str = "select distinct barcode from reportformfull where barcode in(select BarCode from reportformfull where clientno='" + OrgID + "' " + barCodeNo + " group by barcode having count(1)>1)";
            return DbHelperSQL.ExecuteDataSet(str);
        }

        public DataSet GetReportFormInfo(List<string> reportFormNoList)
        {
            DataSet ds = new DataSet();
            try
            {
                string reportFormNo = "";
                for (int i = 0; i < reportFormNoList.Count; i++)
                {
                    if (reportFormNo.Trim() == "")
                        reportFormNo = "'" + reportFormNoList[i].Trim() + "'";
                    else
                        reportFormNo += "," + "'" + reportFormNoList[i].Trim() + "'";
                }
                string strSql = "select *from ReportFormFull  where ReportFormID in (" + reportFormNo + ") order by CName";
                ds = DbHelperSQL.ExecuteDataSet(strSql);
                return ds;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("异常->", ex);
                return null;
            }
        }

        #region IDReportFormFull 成员


        public int Count(string wherestr)
        {


            ZhiFang.Common.Log.Log.Info("重复检验提醒SQL:" + "select count(*) from ReportFormFull where " + wherestr);
            string result = DbHelperSQL.ExecuteScalar("select count(*) from ReportFormFull where " + wherestr);
            if (result != null && result.Trim() != "")
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return -1;
            }
        }

        #endregion

        public bool UpdatePrintTimesByReportFormID(string ReportFormID)
        {
            bool b = false;
            if (ReportFormID.IndexOf(',') >= 0)
            {
                string tmpidlist = "";
                string[] tmpidlista = ReportFormID.Split(',');
                for (int i = 0; i < tmpidlista.Length; i++)
                {
                    tmpidlist += "'" + tmpidlista[i] + "',";
                }
                string strsql = "update  ReportFormfull set PRINTTIMES = PRINTTIMES + 1,PRINTDATETIME='" + DateTime.Now.ToString() + "'  where ReportFormID in (" + tmpidlist.Remove(tmpidlist.LastIndexOf(',')) + ")";
                if (DbHelperSQL.ExecuteNonQuery(strsql) > 0)
                {
                    b = true;
                }
                else
                    b = false;
                return b;
            }
            else
            {
                string strsql = "update  ReportFormfull set PRINTTIMES = PRINTTIMES + 1,PRINTDATETIME='" + DateTime.Now.ToString() + "'  where ReportFormID='" + ReportFormID + "'";
                if (DbHelperSQL.ExecuteNonQuery(strsql) > 0)
                {
                    b = true;
                }
                else
                    b = false;
                return b;
            }
        }

        public bool UpdateDownLoadState(string ReportFormID)
        {
            bool b = false;

            string strsql = "update  ReportFormfull set isdown = 1  where ReportFormID='" + ReportFormID + "'";
            if (DbHelperSQL.ExecuteNonQuery(strsql) > 0)
            {
                b = true;
            }
            else
                b = false;
            return b;
        }

        public DataTable ReportFormGroupByClientNoAndReportDate(string startDateTime, string endDateTime, string clientNoList, int dateType)
        {
            #region PROCEDURE
            //            CREATE PROCEDURE[dbo].[Statistics_ReportFormGroupByClientNoAndReportDate]
            //        @startDateTime varchar(50), --开始时间
            //    @endDateTime varchar(150), --结束时间
            //    @clientNoList varchar(5000), --单位ID数组
            //    @dateType int

            //    AS
            //BEGIN
            //declare @sqlstr varchar(5000);
            //if(@clientNoList<>'')
            //begin
            //set @sqlstr='select StatisticsDate,WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from 
            //(select SUBSTRING(Convert(varchar, UploadDate,120),0,11) as StatisticsDate ,WeblisSourceOrgId,WebLisSourceOrgName from ReportFormFull
            // where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null and WeblisSourceOrgId in ('+@clientNoList+') ) rf
            // group by StatisticsDate, WeblisSourceOrgId, WebLisSourceOrgName';
            //end
            //if(@clientNoList is null)
            //begin
            //set @sqlstr='select StatisticsDate,WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from 
            //(select SUBSTRING(Convert(varchar, UploadDate,120),0,11) as StatisticsDate ,WeblisSourceOrgId,WebLisSourceOrgName from ReportFormFull
            // where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null  ) rf
            // group by StatisticsDate, WeblisSourceOrgId, WebLisSourceOrgName';
            //end
            //if(@clientNoList='')
            //begin
            //set @sqlstr='select StatisticsDate,WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from 
            //(select SUBSTRING(Convert(varchar, UploadDate,120),0,11) as StatisticsDate ,WeblisSourceOrgId,WebLisSourceOrgName from ReportFormFull
            // where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null  ) rf
            // group by StatisticsDate, WeblisSourceOrgId, WebLisSourceOrgName';
            //end
            //    exec(@sqlstr);
            //    print(@sqlstr);
            //        END
            #endregion

            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@dateType", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = dateType;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Statistics_ReportFormGroupByClientNoAndReportDate", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable ReportFormGroupByClientNo(string startDateTime, string endDateTime, string clientNoList, int dateType)
        {
            #region PROCEDURE
            //            CREATE PROCEDURE[dbo].[Statistics_ReportFormGroupByClientNo]
            //        @startDateTime varchar(50), --开始时间
            //    @endDateTime varchar(150), --结束时间
            //    @clientNoList varchar(5000), --单位ID数组
            //    @dateType int

            //    AS
            //BEGIN
            //declare @sqlstr varchar(5000);
            //if(@clientNoList<>'')
            //begin
            //set @sqlstr='select WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from ReportFormFull where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null and WeblisSourceOrgId in ('+@clientNoList+') group by WeblisSourceOrgId,WebLisSourceOrgName';
            //end
            //if(@clientNoList is null)
            //begin
            //set @sqlstr='select WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from ReportFormFull where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null group by WeblisSourceOrgId,WebLisSourceOrgName';
            //end
            //if(@clientNoList='')
            //begin
            //set @sqlstr='select WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from ReportFormFull
            //where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null group by WeblisSourceOrgId,WebLisSourceOrgName';
            //end
            //    exec(@sqlstr);
            //    print(@sqlstr);
            //        END
            #endregion

            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@dateType", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = dateType;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Statistics_ReportFormGroupByClientNo", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataSet GetListByView(Model.ReportFormFull model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM ReportFormFullDataSource where 1=1");
                if (model.CLIENTNO != null)
                {
                    strSql.Append(" and CLIENTNO='" + model.CLIENTNO + "' ");
                }
                if (model.ClientList != null && model.ClientList.Trim().Length > 0)
                {
                    strSql.Append(" and ClientNo in (" + model.ClientList + ") ");
                }
                if (model.serialno != null)
                {
                    strSql.Append(" and serialno='" + model.serialno + "' ");
                }
                if (model.clientcode != null)
                {
                    strSql.Append(" and clientcode like '%" + model.clientcode.Trim() + "%' ");
                }
                if (model.CNAME != null)
                {
                    if (model.LIKESEARCH == "1")
                    {
                        strSql.Append(" and CNAME like '%" + model.CNAME.Trim() + "%' ");
                    }
                    else
                    {
                        strSql.Append(" and CNAME='" + model.CNAME + "' ");
                    }
                }
                if (model.AGEUNITNAME != null)
                {
                    strSql.Append(" and AGEUNITNAME='" + model.AGEUNITNAME + "' ");
                }
                if (model.GENDERNAME != null)
                {
                    strSql.Append(" and GENDERNAME='" + model.GENDERNAME + "' ");
                }
                if (model.DEPTNAME != null)
                {
                    strSql.Append(" and DEPTNAME='" + model.DEPTNAME + "' ");
                }
                if (model.DOCTORNAME != null)
                {
                    strSql.Append(" and DOCTORNAME='" + model.DOCTORNAME + "' ");
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
                if (model.SAMPLETYPENAME != null)
                {
                    strSql.Append(" and SAMPLETYPENAME='" + model.SAMPLETYPENAME + "' ");
                }

                if (model.SECTIONNAME != null)
                {
                    strSql.Append(" and SECTIONNAME='" + model.SECTIONNAME + "' ");
                }

                if (model.PRINTTIMES != null)
                {
                    if (model.PRINTTIMES == 0)
                    {//未打印
                        strSql.Append(" and PRINTTIMES=0 ");
                    }
                    else if (model.PRINTTIMES == 1)
                    {//已打印
                        strSql.Append(" and PRINTTIMES>0 ");
                    }
                    else if (model.PRINTTIMES == 2)
                    {
                        //全部
                    }
                }
                if (model.ZDY6 != null)
                {
                    strSql.Append(" and ZDY6='" + model.ZDY6 + "' ");
                }
                if (model.ZDY7 != null)
                {
                    strSql.Append(" and ZDY7='" + model.ZDY7 + "' ");
                }
                if (model.ZDY8 != null)
                {
                    strSql.Append(" and ZDY8='" + model.ZDY8 + "' ");
                }
                if (model.ZDY9 != null)
                {
                    strSql.Append(" and ZDY9='" + model.ZDY9 + "' ");
                }
                if (model.ZDY10 != null)
                {
                    strSql.Append(" and ZDY10='" + model.ZDY10 + "' ");
                }
                if (model.TESTTYPENAME != null)
                {
                    strSql.Append(" and TESTTYPENAME='" + model.TESTTYPENAME + "' ");
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
                if (model.STATUSNO != null)
                {
                    strSql.Append(" and STATUSNO=" + model.STATUSNO + " ");
                }

                if (model.SAMPLETYPENO != null)
                {
                    strSql.Append(" and SAMPLETYPENO=" + model.SAMPLETYPENO + " ");
                }

                if (model.PATNO != null && model.PATNO != "")
                {
                    strSql.Append(" and PATNO='" + model.PATNO.Trim() + "' ");
                }
                if (model.PERSONID != null && model.PERSONID != "")
                {
                    strSql.Append(" and PERSONID='" + model.PERSONID.Trim() + "' ");
                }
                if (model.GENDERNO != null)
                {
                    strSql.Append(" and GENDERNO=" + model.GENDERNO + " ");
                }

                if (model.BIRTHDAY != null)
                {
                    strSql.Append(" and BIRTHDAY='" + model.BIRTHDAY + "' ");
                }

                if (model.AGE != null)
                {
                    strSql.Append(" and AGE='" + model.AGE + "' ");
                }

                if (model.AGEUNITNO != null)
                {
                    strSql.Append(" and AGEUNITNO=" + model.AGEUNITNO + " ");
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

                if (model.BED != null)
                {
                    strSql.Append(" and BED='" + model.BED + "' ");
                }

                if (model.DEPTNO != null)
                {
                    strSql.Append(" and DEPTNO=" + model.DEPTNO + " ");
                }

                if (model.DOCTOR != null)
                {
                    strSql.Append(" and DOCTOR='" + model.DOCTOR + "' ");
                }

                if (model.CHARGENO != null)
                {
                    strSql.Append(" and CHARGENO='" + model.CHARGENO + "' ");
                }

                if (model.CHARGE != null)
                {
                    strSql.Append(" and CHARGE='" + model.CHARGE + "' ");
                }

                if (model.COLLECTER != null)
                {
                    strSql.Append(" and COLLECTER='" + model.COLLECTER + "' ");
                }

                if (model.COLLECTDATE != null)
                {
                    strSql.Append(" and COLLECTDATE='" + model.COLLECTDATE + "' ");
                }

                if (model.COLLECTTIME != null)
                {
                    strSql.Append(" and COLLECTTIME='" + model.COLLECTTIME + "' ");
                }

                if (model.FORMMEMO != null)
                {
                    strSql.Append(" and FORMMEMO='" + model.FORMMEMO + "' ");
                }

                if (model.TECHNICIAN != null)
                {
                    strSql.Append(" and TECHNICIAN='" + model.TECHNICIAN + "' ");
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

                if (model.OPERDATE != null)
                {
                    strSql.Append(" and OPERDATE='" + model.OPERDATE + "' ");
                }

                if (model.OPERTIME != null)
                {
                    strSql.Append(" and OPERTIME='" + model.OPERTIME + "' ");
                }

                if (model.CHECKER != null)
                {
                    strSql.Append(" and CHECKER='" + model.CHECKER + "' ");
                }


                if (model.resultfile != null)
                {
                    strSql.Append(" and resultfile='" + model.resultfile + "' ");
                }

                if (model.CHECKDATE != null)
                {
                    strSql.Append(" and CHECKDATE='" + model.CHECKDATE + "' ");
                }

                if (model.CHECKTIME != null)
                {
                    strSql.Append(" and CHECKTIME='" + model.CHECKTIME + "' ");
                }

                if (model.SERIALNO != null)
                {
                    strSql.Append(" and SERIALNO='" + model.SERIALNO + "' ");
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

                if (model.SICKORDER != null)
                {
                    strSql.Append(" and SICKORDER='" + model.SICKORDER + "' ");
                }

                if (model.SICKTYPE != null)
                {
                    strSql.Append(" and SICKTYPE='" + model.SICKTYPE + "' ");
                }

                if (model.CHARGEFLAG != null)
                {
                    strSql.Append(" and CHARGEFLAG='" + model.CHARGEFLAG + "' ");
                }

                if (model.TESTDEST != null)
                {
                    strSql.Append(" and TESTDEST='" + model.TESTDEST + "' ");
                }

                if (model.SLABLE != null)
                {
                    strSql.Append(" and SLABLE='" + model.SLABLE + "' ");
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

                if (model.INCEPTDATE != null)
                {
                    strSql.Append(" and INCEPTDATE='" + model.INCEPTDATE + "' ");
                }

                if (model.INCEPTTIME != null)
                {
                    strSql.Append(" and INCEPTTIME='" + model.INCEPTTIME + "' ");
                }

                if (model.INCEPTER != null)
                {
                    strSql.Append(" and INCEPTER='" + model.INCEPTER + "' ");
                }

                if (model.ONLINEDATE != null)
                {
                    strSql.Append(" and ONLINEDATE='" + model.ONLINEDATE + "' ");
                }

                if (model.ONLINETIME != null)
                {
                    strSql.Append(" and ONLINETIME='" + model.ONLINETIME + "' ");
                }

                if (model.BMANNO != null)
                {
                    strSql.Append(" and BMANNO='" + model.BMANNO + "' ");
                }

                if (model.FILETYPE != null)
                {
                    strSql.Append(" and FILETYPE='" + model.FILETYPE + "' ");
                }

                if (model.JPGFILE != null)
                {
                    strSql.Append(" and JPGFILE='" + model.JPGFILE + "' ");
                }

                if (model.PDFFILE != null)
                {
                    strSql.Append(" and PDFFILE='" + model.PDFFILE + "' ");
                }

                if (model.FORMNO != null)
                {
                    strSql.Append(" and FORMNO=" + model.FORMNO + " ");
                }

                if (model.CHILDTABLENAME != null)
                {
                    strSql.Append(" and CHILDTABLENAME='" + model.CHILDTABLENAME + "' ");
                }

                if (model.PRINTEXEC != null)
                {
                    strSql.Append(" and PRINTEXEC='" + model.PRINTEXEC + "' ");
                }
                if (model.LABCENTER != null)
                {
                    strSql.Append(" and LABCENTER='" + model.LABCENTER + "' ");
                }

                if (model.Startdate != null)
                {
                    strSql.Append(" and ReceiveDate>='" + model.Startdate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.Enddate != null)
                {
                    if (model.Enddate.Value.TimeOfDay.Ticks > 0)
                    {
                        strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                    }
                    else
                    {
                        strSql.Append(" and ReceiveDate<='" + model.Enddate.Value.ToShortDateString() + " 23:59:59" + "' ");
                    }
                }
                if (model.collectStartdate != null)
                {
                    strSql.Append(" and COLLECTDATE>='" + model.collectStartdate.Value.ToShortDateString() + "' ");
                }

                if (model.collectEnddate != null)
                {
                    strSql.Append(" and COLLECTDATE<='" + model.collectEnddate.Value.ToShortDateString() + "' ");
                }
                if (model.CheckStartDate != null)
                {
                    strSql.Append(" and checkdate>='" + model.CheckStartDate.Value.ToShortDateString() + "' ");
                }

                if (model.CheckEndDate != null)
                {
                    strSql.Append(" and checkdate<='" + model.CheckEndDate.Value.ToShortDateString() + "' ");
                }
                if (model.noperdateStart != null)
                {
                    strSql.Append(" and noperdate>='" + model.noperdateStart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.noperdateEnd != null)
                {
                    strSql.Append(" and noperdate<='" + model.noperdateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.operdateStart != null)
                {
                    strSql.Append(" and operdate>='" + model.operdateStart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.operdateEnd != null)
                {
                    strSql.Append(" and operdate<='" + model.operdateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                }

                if (model.BarCode != null)
                {
                    strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
                }

                if (model.SectionType != null)
                {
                    strSql.Append(" and SectionType='" + model.SectionType.Trim() + "' ");
                }
                if (model.DIAGNOSE != null)
                {
                    strSql.Append(" and BarCode='" + model.BarCode.Trim() + "' ");
                }

                if (model.DIAGNOSE != null)
                {
                    strSql.Append(" and DIAGNOSE='" + model.DIAGNOSE.Trim() + "' ");
                }
                if (model.CLIENTNAME != null)
                {
                    strSql.Append(" and CLIENTNAME='" + model.CLIENTNAME.Trim() + "' ");
                }
                if (model.ReportFormID != null)
                {
                    strSql.Append(" and ReportFormID='" + model.ReportFormID.Trim() + "' ");
                }


                if (model.RBACSQL != null && model.RBACSQL.Trim() != "")
                {
                    strSql.Append(" and " + model.RBACSQL.Trim() + " ");
                }
                if (model.WeblisSourceOrgList != null && model.WeblisSourceOrgList.Trim().Length > 0)
                {
                    strSql.Append(" and WeblisSourceOrgId in (" + model.WeblisSourceOrgList + ") ");
                }
                if (model.WeblisSourceOrgId != null)
                {
                    strSql.Append(" and WeblisSourceOrgId=" + model.WeblisSourceOrgId + " ");
                }
                ZhiFang.Common.Log.Log.Info("报告列表信息:" + strSql.ToString() + "@" + DbHelperSQL.ConnectionString);
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception ex)
            {
                Common.Log.Log.Debug("异常信息:" + ex.ToString());
                return null;
            }
        }

        public DataSet SearchReportFormFull_ReportItem_WeiJiZhi(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.checkdate desc,T.checktime desc");
            }
            strSql.Append(")AS Row, T.*  from ReportFormFullAll_ReportItem T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            ZhiFang.Common.Log.Log.Debug("SearchReportFormFull_ReportItem_WeiJiZhi.strSql:" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int SearchReportFormFull_ReportItem_WeiJiZhi_Count(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(*) FROM  ReportFormFullAll_ReportItem  ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            ZhiFang.Common.Log.Log.Debug("SearchReportFormFull_ReportItem_WeiJiZhi_Count.strSql:" + strSql.ToString());
            return int.Parse(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }
    }

}

