using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL2009
{
    public class ReportForm : IDReportForm
    {
        public ReportForm()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("FormNo", "ReportForm");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string FormNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ReportForm");
            strSql.Append(" where FormNo=" + FormNo + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ReportForm model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ReceiveDate != null)
            {
                strSql1.Append("ReceiveDate,");
                strSql2.Append("'" + model.ReceiveDate + "',");
            }
            if (model.SectionNo != null)
            {
                strSql1.Append("SectionNo,");
                strSql2.Append("" + model.SectionNo + ",");
            }
            if (model.TestTypeNo != null)
            {
                strSql1.Append("TestTypeNo,");
                strSql2.Append("" + model.TestTypeNo + ",");
            }
            if (model.SampleNo != null)
            {
                strSql1.Append("SampleNo,");
                strSql2.Append("'" + model.SampleNo + "',");
            }
            if (model.StatusNo != null)
            {
                strSql1.Append("StatusNo,");
                strSql2.Append("" + model.StatusNo + ",");
            }
            if (model.SampleTypeNo != null)
            {
                strSql1.Append("SampleTypeNo,");
                strSql2.Append("" + model.SampleTypeNo + ",");
            }
            if (model.PatNo != null)
            {
                strSql1.Append("PatNo,");
                strSql2.Append("'" + model.PatNo + "',");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.GenderNo != null)
            {
                strSql1.Append("GenderNo,");
                strSql2.Append("" + model.GenderNo + ",");
            }
            if (model.Birthday != null)
            {
                strSql1.Append("Birthday,");
                strSql2.Append("'" + model.Birthday + "',");
            }
            if (model.Age != null)
            {
                strSql1.Append("Age,");
                strSql2.Append("" + model.Age + ",");
            }
            if (model.AgeUnitNo != null)
            {
                strSql1.Append("AgeUnitNo,");
                strSql2.Append("" + model.AgeUnitNo + ",");
            }
            if (model.FolkNo != null)
            {
                strSql1.Append("FolkNo,");
                strSql2.Append("" + model.FolkNo + ",");
            }
            if (model.DistrictNo != null)
            {
                strSql1.Append("DistrictNo,");
                strSql2.Append("" + model.DistrictNo + ",");
            }
            if (model.WardNo != null)
            {
                strSql1.Append("WardNo,");
                strSql2.Append("" + model.WardNo + ",");
            }
            if (model.Bed != null)
            {
                strSql1.Append("Bed,");
                strSql2.Append("'" + model.Bed + "',");
            }
            if (model.DeptNo != null)
            {
                strSql1.Append("DeptNo,");
                strSql2.Append("" + model.DeptNo + ",");
            }
            if (model.Doctor != null)
            {
                strSql1.Append("Doctor,");
                strSql2.Append("'" + model.Doctor + "',");
            }
            if (model.ChargeNo != null)
            {
                strSql1.Append("ChargeNo,");
                strSql2.Append("" + model.ChargeNo + ",");
            }
            if (model.Charge != null)
            {
                strSql1.Append("Charge,");
                strSql2.Append("" + model.Charge + ",");
            }
            if (model.Collecter != null)
            {
                strSql1.Append("Collecter,");
                strSql2.Append("'" + model.Collecter + "',");
            }
            if (model.CollectDate != null)
            {
                strSql1.Append("CollectDate,");
                strSql2.Append("'" + model.CollectDate + "',");
            }
            if (model.CollectTime != null)
            {
                strSql1.Append("CollectTime,");
                strSql2.Append("'" + model.CollectTime + "',");
            }
            if (model.FormMemo != null)
            {
                strSql1.Append("FormMemo,");
                strSql2.Append("'" + model.FormMemo + "',");
            }
            if (model.Technician != null)
            {
                strSql1.Append("Technician,");
                strSql2.Append("'" + model.Technician + "',");
            }
            if (model.TestDate != null)
            {
                strSql1.Append("TestDate,");
                strSql2.Append("'" + model.TestDate + "',");
            }
            if (model.TestTime != null)
            {
                strSql1.Append("TestTime,");
                strSql2.Append("'" + model.TestTime + "',");
            }
            if (model.Operator != null)
            {
                strSql1.Append("Operator,");
                strSql2.Append("'" + model.Operator + "',");
            }
            if (model.OperDate != null)
            {
                strSql1.Append("OperDate,");
                strSql2.Append("'" + model.OperDate + "',");
            }
            if (model.OperTime != null)
            {
                strSql1.Append("OperTime,");
                strSql2.Append("'" + model.OperTime + "',");
            }
            if (model.Checker != null)
            {
                strSql1.Append("Checker,");
                strSql2.Append("'" + model.Checker + "',");
            }
            if (model.CheckDate != null)
            {
                strSql1.Append("CheckDate,");
                strSql2.Append("'" + model.CheckDate + "',");
            }
            if (model.CheckTime != null)
            {
                strSql1.Append("CheckTime,");
                strSql2.Append("'" + model.CheckTime + "',");
            }
            if (model.SerialNo != null)
            {
                strSql1.Append("SerialNo,");
                strSql2.Append("'" + model.SerialNo + "',");
            }
            if (model.BarCode != null)
            {
                strSql1.Append("BarCode,");
                strSql2.Append("'" + model.BarCode + "',");
            }
            if (model.RequestSource != null)
            {
                strSql1.Append("RequestSource,");
                strSql2.Append("'" + model.RequestSource + "',");
            }
            if (model.DiagNo != null)
            {
                strSql1.Append("DiagNo,");
                strSql2.Append("" + model.DiagNo + ",");
            }
            if (model.PrintTimes != null)
            {
                strSql1.Append("PrintTimes,");
                strSql2.Append("" + model.PrintTimes + ",");
            }
            if (model.SickTypeNo != null)
            {
                strSql1.Append("SickTypeNo,");
                strSql2.Append("" + model.SickTypeNo + ",");
            }
            if (model.FormComment != null)
            {
                strSql1.Append("FormComment,");
                strSql2.Append("'" + model.FormComment + "',");
            }
            if (model.zdy1 != null)
            {
                strSql1.Append("zdy1,");
                strSql2.Append("'" + model.zdy1 + "',");
            }
            if (model.zdy2 != null)
            {
                strSql1.Append("zdy2,");
                strSql2.Append("'" + model.zdy2 + "',");
            }
            if (model.zdy3 != null)
            {
                strSql1.Append("zdy3,");
                strSql2.Append("'" + model.zdy3 + "',");
            }
            if (model.zdy4 != null)
            {
                strSql1.Append("zdy4,");
                strSql2.Append("'" + model.zdy4 + "',");
            }
            if (model.zdy5 != null)
            {
                strSql1.Append("zdy5,");
                strSql2.Append("'" + model.zdy5 + "',");
            }
            if (model.inceptdate != null)
            {
                strSql1.Append("inceptdate,");
                strSql2.Append("'" + model.inceptdate + "',");
            }
            if (model.incepttime != null)
            {
                strSql1.Append("incepttime,");
                strSql2.Append("'" + model.incepttime + "',");
            }
            if (model.incepter != null)
            {
                strSql1.Append("incepter,");
                strSql2.Append("'" + model.incepter + "',");
            }
            if (model.onlinedate != null)
            {
                strSql1.Append("onlinedate,");
                strSql2.Append("'" + model.onlinedate + "',");
            }
            if (model.onlinetime != null)
            {
                strSql1.Append("onlinetime,");
                strSql2.Append("'" + model.onlinetime + "',");
            }
            if (model.bmanno != null)
            {
                strSql1.Append("bmanno,");
                strSql2.Append("" + model.bmanno + ",");
            }
            if (model.clientno != null)
            {
                strSql1.Append("clientno,");
                strSql2.Append("" + model.clientno + ",");
            }
            if (model.chargeflag != null)
            {
                strSql1.Append("chargeflag,");
                strSql2.Append("'" + model.chargeflag + "',");
            }
            if (model.isReceive != null)
            {
                strSql1.Append("isReceive,");
                strSql2.Append("" + model.isReceive + ",");
            }
            if (model.ReceiveMan != null)
            {
                strSql1.Append("ReceiveMan,");
                strSql2.Append("'" + model.ReceiveMan + "',");
            }
            if (model.ReceiveTime != null)
            {
                strSql1.Append("ReceiveTime,");
                strSql2.Append("'" + model.ReceiveTime + "',");
            }
            if (model.concessionNum != null)
            {
                strSql1.Append("concessionNum,");
                strSql2.Append("'" + model.concessionNum + "',");
            }
            if (model.Sender2 != null)
            {
                strSql1.Append("Sender2,");
                strSql2.Append("'" + model.Sender2 + "',");
            }
            if (model.SenderTime2 != null)
            {
                strSql1.Append("SenderTime2,");
                strSql2.Append("'" + model.SenderTime2 + "',");
            }
            if (model.resultstatus != null)
            {
                strSql1.Append("resultstatus,");
                strSql2.Append("" + model.resultstatus + ",");
            }
            if (model.testaim != null)
            {
                strSql1.Append("testaim,");
                strSql2.Append("'" + model.testaim + "',");
            }
            if (model.resultprinttimes != null)
            {
                strSql1.Append("resultprinttimes,");
                strSql2.Append("" + model.resultprinttimes + ",");
            }
            if (model.paritemname != null)
            {
                strSql1.Append("paritemname,");
                strSql2.Append("'" + model.paritemname + "',");
            }
            if (model.clientprint != null)
            {
                strSql1.Append("clientprint,");
                strSql2.Append("'" + model.clientprint + "',");
            }
            if (model.resultsend != null)
            {
                strSql1.Append("resultsend,");
                strSql2.Append("'" + model.resultsend + "',");
            }
            if (model.reportsend != null)
            {
                strSql1.Append("reportsend,");
                strSql2.Append("'" + model.reportsend + "',");
            }
            if (model.CountNodesFormSource != null)
            {
                strSql1.Append("CountNodesFormSource,");
                strSql2.Append("'" + model.CountNodesFormSource + "',");
            }
            if (model.commsendflag != null)
            {
                strSql1.Append("commsendflag,");
                strSql2.Append("" + model.commsendflag + ",");
            }
            if (model.ZDY6 != null)
            {
                strSql1.Append("ZDY6,");
                strSql2.Append("'" + model.ZDY6 + "',");
            }
            if (model.ZDY7 != null)
            {
                strSql1.Append("ZDY7,");
                strSql2.Append("'" + model.ZDY7 + "',");
            }
            if (model.ZDY8 != null)
            {
                strSql1.Append("ZDY8,");
                strSql2.Append("'" + model.ZDY8 + "',");
            }
            if (model.ZDY9 != null)
            {
                strSql1.Append("ZDY9,");
                strSql2.Append("'" + model.ZDY9 + "',");
            }
            if (model.ZDY10 != null)
            {
                strSql1.Append("ZDY10,");
                strSql2.Append("'" + model.ZDY10 + "',");
            }
            if (model.PrintDateTime != null)
            {
                strSql1.Append("PrintDateTime,");
                strSql2.Append("'" + model.PrintDateTime + "',");
            }
            if (model.PrintOper != null)
            {
                strSql1.Append("PrintOper,");
                strSql2.Append("'" + model.PrintOper + "',");
            }
            if (model.FormNo != null)
            {
                strSql1.Append("FormNo,");
                strSql2.Append("" + model.FormNo + ",");
            }
            if (model.FormStateNo != null)
            {
                strSql1.Append("FormStateNo,");
                strSql2.Append("" + model.FormStateNo + ",");
            }
            if (model.OldSerialNo != null)
            {
                strSql1.Append("OldSerialNo,");
                strSql2.Append("'" + model.OldSerialNo + "',");
            }
            if (model.mresulttype != null)
            {
                strSql1.Append("mresulttype,");
                strSql2.Append("" + model.mresulttype + ",");
            }
            if (model.Diagnose != null)
            {
                strSql1.Append("Diagnose,");
                strSql2.Append("'" + model.Diagnose + "',");
            }
            if (model.TestPurpose != null)
            {
                strSql1.Append("TestPurpose,");
                strSql2.Append("'" + model.TestPurpose + "',");
            }
            if (model.IsFree != null)
            {
                strSql1.Append("IsFree,");
                strSql2.Append("" + model.IsFree + ",");
            }
            if (model.NOperator != null)
            {
                strSql1.Append("NOperator,");
                strSql2.Append("'" + model.NOperator + "',");
            }
            if (model.NOperDate != null)
            {
                strSql1.Append("NOperDate,");
                strSql2.Append("'" + model.NOperDate + "',");
            }
            if (model.NOperTime != null)
            {
                strSql1.Append("NOperTime,");
                strSql2.Append("'" + model.NOperTime + "',");
            }
            if (model.PathologyNo != null)
            {
                strSql1.Append("PathologyNo,");
                strSql2.Append("'" + model.PathologyNo + "',");
            }
            strSql.Append("insert into ReportForm(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.ReportForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ReportForm set ");
            if (model.ReceiveDate != null)
            {
                strSql.Append("ReceiveDate='" + model.ReceiveDate + "',");
            }
            if (model.SectionNo != null)
            {
                strSql.Append("SectionNo=" + model.SectionNo + ",");
            }
            if (model.TestTypeNo != null)
            {
                strSql.Append("TestTypeNo=" + model.TestTypeNo + ",");
            }
            if (model.SampleNo != null)
            {
                strSql.Append("SampleNo='" + model.SampleNo + "',");
            }
            if (model.StatusNo != null)
            {
                strSql.Append("StatusNo=" + model.StatusNo + ",");
            }
            if (model.SampleTypeNo != null)
            {
                strSql.Append("SampleTypeNo=" + model.SampleTypeNo + ",");
            }
            if (model.PatNo != null)
            {
                strSql.Append("PatNo='" + model.PatNo + "',");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.GenderNo != null)
            {
                strSql.Append("GenderNo=" + model.GenderNo + ",");
            }
            if (model.Birthday != null)
            {
                strSql.Append("Birthday='" + model.Birthday + "',");
            }
            if (model.Age != null)
            {
                strSql.Append("Age=" + model.Age + ",");
            }
            if (model.AgeUnitNo != null)
            {
                strSql.Append("AgeUnitNo=" + model.AgeUnitNo + ",");
            }
            if (model.FolkNo != null)
            {
                strSql.Append("FolkNo=" + model.FolkNo + ",");
            }
            if (model.DistrictNo != null)
            {
                strSql.Append("DistrictNo=" + model.DistrictNo + ",");
            }
            if (model.WardNo != null)
            {
                strSql.Append("WardNo=" + model.WardNo + ",");
            }
            if (model.Bed != null)
            {
                strSql.Append("Bed='" + model.Bed + "',");
            }
            if (model.DeptNo != null)
            {
                strSql.Append("DeptNo=" + model.DeptNo + ",");
            }
            if (model.Doctor != null)
            {
                strSql.Append("Doctor='" + model.Doctor + "',");
            }
            if (model.ChargeNo != null)
            {
                strSql.Append("ChargeNo=" + model.ChargeNo + ",");
            }
            if (model.Charge != null)
            {
                strSql.Append("Charge=" + model.Charge + ",");
            }
            if (model.Collecter != null)
            {
                strSql.Append("Collecter='" + model.Collecter + "',");
            }
            if (model.CollectDate != null)
            {
                strSql.Append("CollectDate='" + model.CollectDate + "',");
            }
            if (model.CollectTime != null)
            {
                strSql.Append("CollectTime='" + model.CollectTime + "',");
            }
            if (model.FormMemo != null)
            {
                strSql.Append("FormMemo='" + model.FormMemo + "',");
            }
            if (model.Technician != null)
            {
                strSql.Append("Technician='" + model.Technician + "',");
            }
            if (model.TestDate != null)
            {
                strSql.Append("TestDate='" + model.TestDate + "',");
            }
            if (model.TestTime != null)
            {
                strSql.Append("TestTime='" + model.TestTime + "',");
            }
            if (model.Operator != null)
            {
                strSql.Append("Operator='" + model.Operator + "',");
            }
            if (model.OperDate != null)
            {
                strSql.Append("OperDate='" + model.OperDate + "',");
            }
            if (model.OperTime != null)
            {
                strSql.Append("OperTime='" + model.OperTime + "',");
            }
            if (model.Checker != null)
            {
                strSql.Append("Checker='" + model.Checker + "',");
            }
            if (model.CheckDate != null)
            {
                strSql.Append("CheckDate='" + model.CheckDate + "',");
            }
            if (model.CheckTime != null)
            {
                strSql.Append("CheckTime='" + model.CheckTime + "',");
            }
            if (model.SerialNo != null)
            {
                strSql.Append("SerialNo='" + model.SerialNo + "',");
            }
            if (model.BarCode != null)
            {
                strSql.Append("BarCode='" + model.BarCode + "',");
            }
            if (model.RequestSource != null)
            {
                strSql.Append("RequestSource='" + model.RequestSource + "',");
            }
            if (model.DiagNo != null)
            {
                strSql.Append("DiagNo=" + model.DiagNo + ",");
            }
            if (model.PrintTimes != null)
            {
                strSql.Append("PrintTimes=" + model.PrintTimes + ",");
            }
            if (model.SickTypeNo != null)
            {
                strSql.Append("SickTypeNo=" + model.SickTypeNo + ",");
            }
            if (model.FormComment != null)
            {
                strSql.Append("FormComment='" + model.FormComment + "',");
            }
            if (model.zdy1 != null)
            {
                strSql.Append("zdy1='" + model.zdy1 + "',");
            }
            if (model.zdy2 != null)
            {
                strSql.Append("zdy2='" + model.zdy2 + "',");
            }
            if (model.zdy3 != null)
            {
                strSql.Append("zdy3='" + model.zdy3 + "',");
            }
            if (model.zdy4 != null)
            {
                strSql.Append("zdy4='" + model.zdy4 + "',");
            }
            if (model.zdy5 != null)
            {
                strSql.Append("zdy5='" + model.zdy5 + "',");
            }
            if (model.inceptdate != null)
            {
                strSql.Append("inceptdate='" + model.inceptdate + "',");
            }
            if (model.incepttime != null)
            {
                strSql.Append("incepttime='" + model.incepttime + "',");
            }
            if (model.incepter != null)
            {
                strSql.Append("incepter='" + model.incepter + "',");
            }
            if (model.onlinedate != null)
            {
                strSql.Append("onlinedate='" + model.onlinedate + "',");
            }
            if (model.onlinetime != null)
            {
                strSql.Append("onlinetime='" + model.onlinetime + "',");
            }
            if (model.bmanno != null)
            {
                strSql.Append("bmanno=" + model.bmanno + ",");
            }
            if (model.clientno != null)
            {
                strSql.Append("clientno=" + model.clientno + ",");
            }
            if (model.chargeflag != null)
            {
                strSql.Append("chargeflag='" + model.chargeflag + "',");
            }
            if (model.isReceive != null)
            {
                strSql.Append("isReceive=" + model.isReceive + ",");
            }
            if (model.ReceiveMan != null)
            {
                strSql.Append("ReceiveMan='" + model.ReceiveMan + "',");
            }
            if (model.ReceiveTime != null)
            {
                strSql.Append("ReceiveTime='" + model.ReceiveTime + "',");
            }
            if (model.concessionNum != null)
            {
                strSql.Append("concessionNum='" + model.concessionNum + "',");
            }
            if (model.Sender2 != null)
            {
                strSql.Append("Sender2='" + model.Sender2 + "',");
            }
            if (model.SenderTime2 != null)
            {
                strSql.Append("SenderTime2='" + model.SenderTime2 + "',");
            }
            if (model.resultstatus != null)
            {
                strSql.Append("resultstatus=" + model.resultstatus + ",");
            }
            if (model.testaim != null)
            {
                strSql.Append("testaim='" + model.testaim + "',");
            }
            if (model.resultprinttimes != null)
            {
                strSql.Append("resultprinttimes=" + model.resultprinttimes + ",");
            }
            if (model.paritemname != null)
            {
                strSql.Append("paritemname='" + model.paritemname + "',");
            }
            if (model.clientprint != null)
            {
                strSql.Append("clientprint='" + model.clientprint + "',");
            }
            if (model.resultsend != null)
            {
                strSql.Append("resultsend='" + model.resultsend + "',");
            }
            if (model.reportsend != null)
            {
                strSql.Append("reportsend='" + model.reportsend + "',");
            }
            if (model.CountNodesFormSource != null)
            {
                strSql.Append("CountNodesFormSource='" + model.CountNodesFormSource + "',");
            }
            if (model.commsendflag != null)
            {
                strSql.Append("commsendflag=" + model.commsendflag + ",");
            }
            if (model.ZDY6 != null)
            {
                strSql.Append("ZDY6='" + model.ZDY6 + "',");
            }
            if (model.ZDY7 != null)
            {
                strSql.Append("ZDY7='" + model.ZDY7 + "',");
            }
            if (model.ZDY8 != null)
            {
                strSql.Append("ZDY8='" + model.ZDY8 + "',");
            }
            if (model.ZDY9 != null)
            {
                strSql.Append("ZDY9='" + model.ZDY9 + "',");
            }
            if (model.ZDY10 != null)
            {
                strSql.Append("ZDY10='" + model.ZDY10 + "',");
            }
            if (model.PrintDateTime != null)
            {
                strSql.Append("PrintDateTime='" + model.PrintDateTime + "',");
            }
            if (model.PrintOper != null)
            {
                strSql.Append("PrintOper='" + model.PrintOper + "',");
            }
            if (model.FormStateNo != null)
            {
                strSql.Append("FormStateNo=" + model.FormStateNo + ",");
            }
            if (model.OldSerialNo != null)
            {
                strSql.Append("OldSerialNo='" + model.OldSerialNo + "',");
            }
            if (model.mresulttype != null)
            {
                strSql.Append("mresulttype=" + model.mresulttype + ",");
            }
            if (model.Diagnose != null)
            {
                strSql.Append("Diagnose='" + model.Diagnose + "',");
            }
            if (model.TestPurpose != null)
            {
                strSql.Append("TestPurpose='" + model.TestPurpose + "',");
            }
            if (model.IsFree != null)
            {
                strSql.Append("IsFree=" + model.IsFree + ",");
            }
            if (model.NOperator != null)
            {
                strSql.Append("NOperator='" + model.NOperator + "',");
            }
            if (model.NOperDate != null)
            {
                strSql.Append("NOperDate='" + model.NOperDate + "',");
            }
            if (model.NOperTime != null)
            {
                strSql.Append("NOperTime='" + model.NOperTime + "',");
            }
            if (model.PathologyNo != null)
            {
                strSql.Append("PathologyNo='" + model.PathologyNo + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where FormNo=" + model.FormNo + " ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string FormNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ReportForm ");
            strSql.Append(" where FormNo=" + FormNo + " ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ReportForm GetModel(string FormNo)
        {           
            Model.ReportForm model = new Model.ReportForm();
            DataSet ds = this.GetListByFormNo(FormNo);
            if (ds!=null&&ds.Tables[0].Rows.Count > 0)
            {
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
                if (ds.Tables[0].Rows[0]["StatusNo"].ToString() != "")
                {
                    model.StatusNo = int.Parse(ds.Tables[0].Rows[0]["StatusNo"].ToString());
                }
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
                    model.Age = Convert.ToInt32(ds.Tables[0].Rows[0]["Age"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AgeUnitNo"].ToString() != "")
                {
                    model.AgeUnitNo = int.Parse(ds.Tables[0].Rows[0]["AgeUnitNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FolkNo"].ToString() != "")
                {
                    model.FolkNo = int.Parse(ds.Tables[0].Rows[0]["FolkNo"].ToString());
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
                if (ds.Tables[0].Rows[0]["ChargeNo"].ToString() != "")
                {
                    model.ChargeNo = int.Parse(ds.Tables[0].Rows[0]["ChargeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Charge"].ToString() != "")
                {
                    model.Charge = decimal.Parse(ds.Tables[0].Rows[0]["Charge"].ToString());
                }
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
                model.BarCode = ds.Tables[0].Rows[0]["BarCode"].ToString();
                model.RequestSource = ds.Tables[0].Rows[0]["RequestSource"].ToString();
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
                if (ds.Tables[0].Rows[0]["onlinedate"].ToString() != "")
                {
                    model.onlinedate = DateTime.Parse(ds.Tables[0].Rows[0]["onlinedate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["onlinetime"].ToString() != "")
                {
                    model.onlinetime = DateTime.Parse(ds.Tables[0].Rows[0]["onlinetime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["bmanno"].ToString() != "")
                {
                    model.bmanno = int.Parse(ds.Tables[0].Rows[0]["bmanno"].ToString());
                }
                if (ds.Tables[0].Rows[0]["clientno"].ToString() != "")
                {
                    model.clientno = int.Parse(ds.Tables[0].Rows[0]["clientno"].ToString());
                }
                model.chargeflag = ds.Tables[0].Rows[0]["chargeflag"].ToString();
                if (ds.Tables[0].Rows[0]["isReceive"].ToString() != "")
                {
                    model.isReceive = int.Parse(ds.Tables[0].Rows[0]["isReceive"].ToString());
                }
                model.ReceiveMan = ds.Tables[0].Rows[0]["ReceiveMan"].ToString();
                if (ds.Tables[0].Rows[0]["ReceiveTime"].ToString() != "")
                {
                    model.ReceiveTime = DateTime.Parse(ds.Tables[0].Rows[0]["ReceiveTime"].ToString());
                }
                model.concessionNum = ds.Tables[0].Rows[0]["concessionNum"].ToString();
                model.Sender2 = ds.Tables[0].Rows[0]["Sender2"].ToString();
                if (ds.Tables[0].Rows[0]["SenderTime2"].ToString() != "")
                {
                    model.SenderTime2 = DateTime.Parse(ds.Tables[0].Rows[0]["SenderTime2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["resultstatus"].ToString() != "")
                {
                    model.resultstatus = int.Parse(ds.Tables[0].Rows[0]["resultstatus"].ToString());
                }
                model.testaim = ds.Tables[0].Rows[0]["testaim"].ToString();
                if (ds.Tables[0].Rows[0]["resultprinttimes"].ToString() != "")
                {
                    model.resultprinttimes = int.Parse(ds.Tables[0].Rows[0]["resultprinttimes"].ToString());
                }
                model.paritemname = ds.Tables[0].Rows[0]["paritemname"].ToString();
                model.clientprint = ds.Tables[0].Rows[0]["clientprint"].ToString();
                model.resultsend = ds.Tables[0].Rows[0]["resultsend"].ToString();
                model.reportsend = ds.Tables[0].Rows[0]["reportsend"].ToString();
                model.CountNodesFormSource = ds.Tables[0].Rows[0]["CountNodesFormSource"].ToString();
                if (ds.Tables[0].Rows[0]["commsendflag"].ToString() != "")
                {
                    model.commsendflag = int.Parse(ds.Tables[0].Rows[0]["commsendflag"].ToString());
                }
                model.ZDY6 = ds.Tables[0].Rows[0]["ZDY6"].ToString();
                model.ZDY7 = ds.Tables[0].Rows[0]["ZDY7"].ToString();
                model.ZDY8 = ds.Tables[0].Rows[0]["ZDY8"].ToString();
                model.ZDY9 = ds.Tables[0].Rows[0]["ZDY9"].ToString();
                model.ZDY10 = ds.Tables[0].Rows[0]["ZDY10"].ToString();
                if (ds.Tables[0].Rows[0]["PrintDateTime"].ToString() != "")
                {
                    model.PrintDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["PrintDateTime"].ToString());
                }
                model.PrintOper = ds.Tables[0].Rows[0]["PrintOper"].ToString();
                if (ds.Tables[0].Rows[0]["FormNo"].ToString() != "")
                {
                    model.FormNo = ds.Tables[0].Rows[0]["FormNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FormStateNo"].ToString() != "")
                {
                    model.FormStateNo = int.Parse(ds.Tables[0].Rows[0]["FormStateNo"].ToString());
                }
                model.OldSerialNo = ds.Tables[0].Rows[0]["OldSerialNo"].ToString();
                if (ds.Tables[0].Rows[0]["mresulttype"].ToString() != "")
                {
                    model.mresulttype = int.Parse(ds.Tables[0].Rows[0]["mresulttype"].ToString());
                }
                model.Diagnose = ds.Tables[0].Rows[0]["Diagnose"].ToString();
                model.TestPurpose = ds.Tables[0].Rows[0]["TestPurpose"].ToString();
                if (ds.Tables[0].Rows[0]["IsFree"].ToString() != "")
                {
                    model.IsFree = int.Parse(ds.Tables[0].Rows[0]["IsFree"].ToString());
                }
                model.NOperator = ds.Tables[0].Rows[0]["NOperator"].ToString();
                if (ds.Tables[0].Rows[0]["NOperDate"].ToString() != "")
                {
                    model.NOperDate = DateTime.Parse(ds.Tables[0].Rows[0]["NOperDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NOperTime"].ToString() != "")
                {
                    model.NOperTime = DateTime.Parse(ds.Tables[0].Rows[0]["NOperTime"].ToString());
                }
                model.PathologyNo = ds.Tables[0].Rows[0]["PathologyNo"].ToString();
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
            strSql.Append("select ReceiveDate,SectionNo,TestTypeNo,SampleNo,StatusNo,SampleTypeNo,PatNo,CName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,ChargeNo,Charge,Collecter,CollectDate,CollectTime,FormMemo,Technician,TestDate,TestTime,Operator,OperDate,OperTime,Checker,CheckDate,CheckTime,SerialNo,BarCode,RequestSource,DiagNo,PrintTimes,SickTypeNo,FormComment,zdy1,zdy2,zdy3,zdy4,zdy5,inceptdate,incepttime,incepter,onlinedate,onlinetime,bmanno,clientno,chargeflag,isReceive,ReceiveMan,ReceiveTime,concessionNum,Sender2,SenderTime2,resultstatus,testaim,resultprinttimes,paritemname,clientprint,resultsend,reportsend,CountNodesFormSource,commsendflag,zdy6,PrintDateTime,PrintOper,FormNo,FormStateNo,OldSerialNo,mresulttype,Diagnose,TestPurpose,IsFree,NOperator,NOperDate,NOperTime,PathologyNo ");
            strSql.Append(" FROM ReportForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.ReportForm model)
        {
           return this.GetList(model, null, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.ReportForm model,DateTime? StartDay,DateTime? EndDay)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,dbo.Addb(SampleNo) as SampleNoSort,SampleNo,StatusNo,SampleTypeNo,dbo.Addb(PatNo) as PatNoSort,PatNo,CName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,ChargeNo,Charge,Collecter,CollectDate,CollectTime,FormMemo,Technician,TestDate,TestTime,Operator,OperDate,OperTime,Checker,CheckDate,CheckTime,SerialNo,BarCode,RequestSource,DiagNo,PrintTimes,SickTypeNo,FormComment,zdy1,zdy2,zdy3,zdy4,zdy5,inceptdate,incepttime,incepter,onlinedate,onlinetime,bmanno,clientno,chargeflag,isReceive,ReceiveMan,ReceiveTime,concessionNum,Sender2,SenderTime2,resultstatus,testaim,resultprinttimes,paritemname,clientprint,resultsend,reportsend,CountNodesFormSource,commsendflag,zdy6,PrintDateTime,PrintOper,FormNo,FormStateNo,OldSerialNo,mresulttype,Diagnose,TestPurpose,IsFree,NOperator,NOperDate,NOperTime,PathologyNo ");
                strSql.Append(" FROM ReportForm where 1=1 ");
                if (StartDay != null)
                {
                    strSql.Append(" and ReceiveDate>='" + StartDay.Value + "' ");
                }
                if (EndDay != null)
                {
                    strSql.Append(" and ReceiveDate<='" + EndDay.Value + "' ");
                }
                if (model.ReceiveDate != null)
                {
                    strSql.Append(" and ReceiveDate='" + model.ReceiveDate + "'");
                }
                if (model.SectionNo != null)
                {
                    strSql.Append(" and SectionNo=" + model.SectionNo + "");
                }
                if (model.TestTypeNo != null)
                {
                    strSql.Append(" and TestTypeNo=" + model.TestTypeNo + "");
                }
                if (model.SampleNo != null)
                {
                    strSql.Append(" and SampleNo='" + model.SampleNo + "'");
                }
                if (model.StatusNo != null)
                {
                    strSql.Append(" and StatusNo=" + model.StatusNo + "");
                }
                if (model.SampleTypeNo != null)
                {
                    strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + "");
                }
                if (model.PatNo != null)
                {
                    strSql.Append(" and PatNo='" + model.PatNo + "'");
                }
                if (model.CName != null)
                {
                    strSql.Append(" and CName='" + model.CName + "'");
                }
                if (model.GenderNo != null)
                {
                    strSql.Append(" and GenderNo=" + model.GenderNo + "");
                }
                if (model.Birthday != null)
                {
                    strSql.Append(" and Birthday='" + model.Birthday + "'");
                }
                if (model.Age != null)
                {
                    strSql.Append(" and Age=" + model.Age + "");
                }
                if (model.AgeUnitNo != null)
                {
                    strSql.Append(" and AgeUnitNo=" + model.AgeUnitNo + "");
                }
                if (model.FolkNo != null)
                {
                    strSql.Append(" and FolkNo=" + model.FolkNo + "");
                }
                if (model.DistrictNo != null)
                {
                    strSql.Append(" and DistrictNo=" + model.DistrictNo + "");
                }
                if (model.WardNo != null)
                {
                    strSql.Append(" and WardNo=" + model.WardNo + "");
                }
                if (model.Bed != null)
                {
                    strSql.Append(" and Bed='" + model.Bed + "'");
                }
                if (model.DeptNo != null)
                {
                    strSql.Append(" and DeptNo=" + model.DeptNo + "");
                }
                if (model.Doctor != null)
                {
                    strSql.Append(" and Doctor='" + model.Doctor + "'");
                }
                if (model.ChargeNo != null)
                {
                    strSql.Append(" and ChargeNo=" + model.ChargeNo + "");
                }
                if (model.Charge != null)
                {
                    strSql.Append(" and Charge=" + model.Charge + "");
                }
                if (model.Collecter != null)
                {
                    strSql.Append(" and Collecter='" + model.Collecter + "'");
                }
                if (model.CollectDate != null)
                {
                    strSql.Append(" and CollectDate='" + model.CollectDate + "'");
                }
                if (model.CollectTime != null)
                {
                    strSql.Append(" and CollectTime='" + model.CollectTime + "'");
                }
                if (model.FormMemo != null)
                {
                    strSql.Append(" and FormMemo='" + model.FormMemo + "'");
                }
                if (model.Technician != null)
                {
                    strSql.Append(" and Technician='" + model.Technician + "'");
                }
                if (model.TestDate != null)
                {
                    strSql.Append(" and TestDate='" + model.TestDate + "'");
                }
                if (model.TestTime != null)
                {
                    strSql.Append(" and TestTime='" + model.TestTime + "'");
                }
                if (model.Operator != null)
                {
                    strSql.Append(" and Operator='" + model.Operator + "'");
                }
                if (model.OperDate != null)
                {
                    strSql.Append(" and OperDate='" + model.OperDate + "'");
                }
                if (model.OperTime != null)
                {
                    strSql.Append(" and OperTime='" + model.OperTime + "'");
                }
                if (model.Checker != null)
                {
                    strSql.Append(" and Checker='" + model.Checker + "'");
                }
                if (model.CheckDate != null)
                {
                    strSql.Append(" and CheckDate='" + model.CheckDate + "'");
                }
                if (model.CheckTime != null)
                {
                    strSql.Append(" and CheckTime='" + model.CheckTime + "'");
                }
                if (model.SerialNo != null)
                {
                    strSql.Append(" and SerialNo='" + model.SerialNo + "'");
                }
                if (model.BarCode != null)
                {
                    strSql.Append(" and BarCode='" + model.BarCode + "'");
                }
                if (model.RequestSource != null)
                {
                    strSql.Append(" and RequestSource='" + model.RequestSource + "'");
                }
                if (model.DiagNo != null)
                {
                    strSql.Append(" and DiagNo=" + model.DiagNo + "");
                }
                if (model.PrintTimes != null)
                {
                    strSql.Append(" and PrintTimes=" + model.PrintTimes + "");
                }
                if (model.SickTypeNo != null)
                {
                    strSql.Append(" and SickTypeNo=" + model.SickTypeNo + "");
                }
                if (model.FormComment != null)
                {
                    strSql.Append(" and FormComment='" + model.FormComment + "'");
                }
                if (model.zdy1 != null)
                {
                    strSql.Append(" and zdy1='" + model.zdy1 + "'");
                }
                if (model.zdy2 != null)
                {
                    strSql.Append(" and zdy2='" + model.zdy2 + "'");
                }
                if (model.zdy3 != null)
                {
                    strSql.Append(" and zdy3='" + model.zdy3 + "'");
                }
                if (model.zdy4 != null)
                {
                    strSql.Append(" and zdy4='" + model.zdy4 + "'");
                }
                if (model.zdy5 != null)
                {
                    strSql.Append(" and zdy5='" + model.zdy5 + "'");
                }
                if (model.inceptdate != null)
                {
                    strSql.Append(" and inceptdate='" + model.inceptdate + "'");
                }
                if (model.incepttime != null)
                {
                    strSql.Append(" and incepttime='" + model.incepttime + "'");
                }
                if (model.incepter != null)
                {
                    strSql.Append(" and incepter='" + model.incepter + "'");
                }
                if (model.onlinedate != null)
                {
                    strSql.Append(" and onlinedate='" + model.onlinedate + "'");
                }
                if (model.onlinetime != null)
                {
                    strSql.Append(" and onlinetime='" + model.onlinetime + "'");
                }
                if (model.bmanno != null)
                {
                    strSql.Append(" and bmanno=" + model.bmanno + "");
                }
                if (model.clientno != null)
                {
                    strSql.Append(" and clientno=" + model.clientno + "");
                }
                if (model.chargeflag != null)
                {
                    strSql.Append(" and chargeflag='" + model.chargeflag + "'");
                }
                if (model.isReceive != null)
                {
                    strSql.Append(" and isReceive=" + model.isReceive + "");
                }
                if (model.ReceiveMan != null)
                {
                    strSql.Append(" and ReceiveMan='" + model.ReceiveMan + "'");
                }
                if (model.ReceiveTime != null)
                {
                    strSql.Append(" and ReceiveTime='" + model.ReceiveTime + "'");
                }
                if (model.concessionNum != null)
                {
                    strSql.Append(" and concessionNum='" + model.concessionNum + "'");
                }
                if (model.Sender2 != null)
                {
                    strSql.Append(" and Sender2='" + model.Sender2 + "'");
                }
                if (model.SenderTime2 != null)
                {
                    strSql.Append(" and SenderTime2='" + model.SenderTime2 + "'");
                }
                if (model.resultstatus != null)
                {
                    strSql.Append(" and resultstatus=" + model.resultstatus + "");
                }
                if (model.testaim != null)
                {
                    strSql.Append(" and testaim='" + model.testaim + "'");
                }
                if (model.resultprinttimes != null)
                {
                    strSql.Append(" and resultprinttimes=" + model.resultprinttimes + "");
                }
                if (model.paritemname != null)
                {
                    strSql.Append(" and paritemname='" + model.paritemname + "'");
                }
                if (model.clientprint != null)
                {
                    strSql.Append(" and clientprint='" + model.clientprint + "'");
                }
                if (model.resultsend != null)
                {
                    strSql.Append(" and resultsend='" + model.resultsend + "'");
                }
                if (model.reportsend != null)
                {
                    strSql.Append(" and reportsend='" + model.reportsend + "'");
                }
                if (model.CountNodesFormSource != null)
                {
                    strSql.Append(" and CountNodesFormSource='" + model.CountNodesFormSource + "'");
                }
                if (model.commsendflag != null)
                {
                    strSql.Append(" and commsendflag=" + model.commsendflag + "");
                }
                if (model.ZDY6 != null)
                {
                    strSql.Append(" and ZDY6='" + model.ZDY6 + "'");
                }
                if (model.ZDY7 != null)
                {
                    strSql.Append(" and ZDY7='" + model.ZDY7 + "'");
                }
                if (model.ZDY8 != null)
                {
                    strSql.Append(" and ZDY8='" + model.ZDY8 + "'");
                }
                if (model.ZDY9 != null)
                {
                    strSql.Append(" and ZDY9='" + model.ZDY9 + "'");
                }
                if (model.ZDY10 != null)
                {
                    strSql.Append(" and ZDY10='" + model.ZDY10 + "'");
                }
                if (model.PrintDateTime != null)
                {
                    strSql.Append(" and PrintDateTime='" + model.PrintDateTime + "'");
                }
                if (model.PrintOper != null)
                {
                    strSql.Append(" and PrintOper='" + model.PrintOper + "'");
                }
                if (model.FormStateNo != null)
                {
                    strSql.Append(" and FormStateNo=" + model.FormStateNo + "");
                }
                if (model.OldSerialNo != null)
                {
                    strSql.Append(" and OldSerialNo='" + model.OldSerialNo + "'");
                }
                if (model.mresulttype != null)
                {
                    strSql.Append(" and mresulttype=" + model.mresulttype + "");
                }
                if (model.Diagnose != null)
                {
                    strSql.Append(" and Diagnose='" + model.Diagnose + "'");
                }
                if (model.TestPurpose != null)
                {
                    strSql.Append(" and TestPurpose='" + model.TestPurpose + "'");
                }
                if (model.IsFree != null)
                {
                    strSql.Append(" and IsFree=" + model.IsFree + "");
                }
                if (model.NOperator != null)
                {
                    strSql.Append(" and NOperator='" + model.NOperator + "'");
                }
                if (model.NOperDate != null)
                {
                    strSql.Append(" and NOperDate='" + model.NOperDate + "'");
                }
                if (model.NOperTime != null)
                {
                    strSql.Append(" and NOperTime='" + model.NOperTime + "'");
                }
                if (model.PathologyNo != null)
                {
                    strSql.Append(" and PathologyNo='" + model.PathologyNo + "'");
                }
                if (model.FormNo != null)
                {
                    strSql.Append(" and FormNo='" + model.FormNo + "'");
                }
                ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
                return DbHelperSQL.Query(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName+".异常："+ex.ToString());
                return null;
            }
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
            strSql.Append(" ReceiveDate,SectionNo,TestTypeNo,SampleNo,StatusNo,SampleTypeNo,PatNo,CName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,ChargeNo,Charge,Collecter,CollectDate,CollectTime,FormMemo,Technician,TestDate,TestTime,Operator,OperDate,OperTime,Checker,CheckDate,CheckTime,SerialNo,BarCode,RequestSource,DiagNo,PrintTimes,SickTypeNo,FormComment,zdy1,zdy2,zdy3,zdy4,zdy5,inceptdate,incepttime,incepter,onlinedate,onlinetime,bmanno,clientno,chargeflag,isReceive,ReceiveMan,ReceiveTime,concessionNum,Sender2,SenderTime2,resultstatus,testaim,resultprinttimes,paritemname,clientprint,resultsend,reportsend,CountNodesFormSource,commsendflag,zdy6,PrintDateTime,PrintOper,FormNo,FormStateNo,OldSerialNo,mresulttype,Diagnose,TestPurpose,IsFree,NOperator,NOperDate,NOperTime,PathologyNo ");
            strSql.Append(" FROM ReportForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 根据FormNo数组返回ReportForm列表
        /// </summary>
        /// <param name="FormNo">FormNo数组</param>
        /// <returns></returns>
        public DataTable GetReportFormList(string[] FormNo)
        {
            try
            {
                string tmpformno = "";
                foreach (string a in FormNo)
                {
                    tmpformno += a + ",";
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Convert(varchar(10),RF.ReceiveDate,21) AS ReceiveDate ,RF.SectionNo,RF.TestTypeNo,RF.SampleNo,RF.StatusNo,RF.SampleTypeNo,RF.PatNo,RF.CName,RF.GenderNo,RF.Birthday,RF.Age,RF.AgeUnitNo,RF.FolkNo,RF.DistrictNo,RF.WardNo,RF.Bed,RF.DeptNo,RF.Doctor,RF.ChargeNo,RF.Charge,RF.Collecter,RF.CollectDate,RF.CollectTime,RF.FormMemo,RF.Technician,RF.TestDate,RF.TestTime,RF.Operator,RF.OperDate,RF.OperTime,RF.Checker,RF.CheckDate,RF.CheckTime,RF.SerialNo,RF.BarCode,RF.RequestSource,RF.DiagNo,RF.PrintTimes,RF.SickTypeNo,RF.FormComment,RF.zdy1,RF.zdy2,RF.zdy3,RF.zdy4,RF.zdy5,RF.inceptdate,RF.incepttime,RF.incepter,RF.onlinedate,RF.onlinetime,RF.bmanno,RF.clientno,RF.chargeflag,RF.isReceive,RF.ReceiveMan,RF.ReceiveTime,RF.concessionNum,RF.Sender2,RF.SenderTime2,RF.resultstatus,RF.testaim,RF.resultprinttimes,RF.paritemname,RF.clientprint,RF.resultsend,RF.reportsend,RF.CountNodesFormSource,RF.commsendflag,RF.zdy6,RF.PrintDateTime,RF.PrintOper,RF.FormNo,RF.FormStateNo,RF.OldSerialNo,RF.mresulttype,RF.Diagnose,RF.TestPurpose,RF.IsFree,RF.NOperator,RF.NOperDate,RF.NOperTime,RF.PathologyNo ");
                strSql.Append(",CHARINDEX(','+ CONVERT(nvarchar, formno) + ',','," + tmpformno + "') as orderid ,GT.cname Gender,ST.cname sick,SPT.cname sampletype,FT.cname folk,DM.cname Dept,DT.cname district,WT.cname ward,AU.cname AgeUnit,TT.cname TestType,ds.cname diag,cn.cname client,cn.clientstyle,cn.ename clientename,cn.shortcode clientcode,clientarea,ClientReportTitle ,pg.cname sectionname,pg.sectiondesc sectiondesc,pg.shortcode sectioncode,PU1.userimage Ptechnician ,PU2.userimage PChecker ,PU3.userimage POperator ,PU4.userimage Pincepter   ");
                strSql.Append(",RF.Doctor as DOCTORNAME,GT.cname as GENDERNAME,DM.cname as DEPTNAME,AU.cname as AGEUNITNAME,DT.cname as DISTRICTNAME,WT.cname as WARDNAME,ST.cname as SICKTYPENAME,FT.cname as FOLKNAME,SPT.cname as SAMPLETYPENAME ");
                strSql.Append(",(SELECT ParaValue FROM dbo.SysPara WHERE (ParaNo = '1000000') AND (NodeName = 'ALLNODE'))+'_'+Convert(varchar(20),RF.FormNo)+'_'+Convert(varchar(20),RF.SectionNo)+'_'+Convert(varchar(20),RF.StatusNo)+'_'+Convert(varchar(20),RF.SampleNo)+'_'+Convert(varchar(10),RF.ReceiveDate,21) as REPORTFORMID   ");
                strSql.Append(",(SELECT ParaValue FROM dbo.SysPara AS SysPara_2 WHERE (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) as CHECKNO");
                strSql.Append(",(SELECT ParaName FROM dbo.SysPara AS SysPara_1 WHERE (ParaNo = '1000000') AND (NodeName = 'ALLNODE')) as CHECKNAME ");
                strSql.Append(",cn.CNAME as CLIENTNAME");
                strSql.Append(" from (select * from reportform where formno in(" + tmpformno.Substring(0, tmpformno.Length - 1) + ")) RF  left join  GenderType GT on(GT.GenderNo=RF.GenderNo) left join  SickType ST on(ST.sickTypeNo=RF.sickTypeNo) left join  sampletype SPT on(SPT.sampletypeNo=RF.sampletypeNo) left join  folktype FT on(FT.folkNo=RF.folkNo) left join  DepartMent DM on(DM.DeptNo=RF.DeptNo) left join  district DT on(DT.districtNo=RF.districtNo) left join  wardtype WT on(WT.wardNo=RF.wardNo) left join  AgeUnit AU on(AU.AgeUnitNo=RF.AgeUnitNo) left join  TestType TT on(TT.TestTypeNo=RF.TestTypeNo) left join  diagnosis ds on(ds.diagno=RF.diagno) left join  clientele cn on(cn.clientno=RF.clientno) left join  pgroup pg on(pg.sectionno=RF.sectionno) left join  puser PU1 on(PU1.cname=RF.technician) left join  puser PU2 on(PU2.cname=RF.Checker) left join  puser PU3 on(PU3.cname=RF.Operator) left join  puser PU4 on(PU4.cname=RF.incepter) ORDER BY orderid ");
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
            }
            catch
            {
                return new DataTable();
            }
        }
        /// <summary>
        /// 根据FormNo数组返回ReportForm列表
        /// </summary>
        /// <param name="FormNo">FormNo数组</param>
        /// <returns></returns>
        public DataTable GetReportFormFullList(string FormNo)
        {
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
                SqlParameter sp = new SqlParameter("@FormNo", SqlDbType.VarChar, 50);
                sp.Value = FormNo;
                DataSet ds = DbHelperSQL.RunProcedure("GetReportFormFullList", new SqlParameter[] { sp }, "ReportFormFull");
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
                #endregion
            }
            catch
            {
                return new DataTable();
            }
        }
        public bool UpdatePrintTimes(string[] reportformlist)
        {
            throw new NotImplementedException();
        }
        public DataSet GetListByFormNo(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1  ");
                strSql.Append(" ReceiveDate,SectionNo,TestTypeNo,SampleNo,StatusNo,SampleTypeNo,PatNo,CName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,ChargeNo,Charge,Collecter,CollectDate,CollectTime,FormMemo,Technician,TestDate,TestTime,Operator,OperDate,OperTime,Checker,CheckDate,CheckTime,SerialNo,BarCode,RequestSource,DiagNo,PrintTimes,SickTypeNo,FormComment,zdy1,zdy2,zdy3,zdy4,zdy5,inceptdate,incepttime,incepter,onlinedate,onlinetime,bmanno,clientno,chargeflag,isReceive,ReceiveMan,ReceiveTime,concessionNum,Sender2,SenderTime2,resultstatus,testaim,resultprinttimes,paritemname,clientprint,resultsend,reportsend,CountNodesFormSource,commsendflag,zdy6,PrintDateTime,PrintOper,FormNo,FormStateNo,OldSerialNo,mresulttype,Diagnose,TestPurpose,IsFree,NOperator,NOperDate,NOperTime,PathologyNo ");
                strSql.Append(" from ReportForm ");
                strSql.Append(" where FormNo=" + FormNo + " ");

                return DbHelperSQL.Query(strSql.ToString());
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL2009.GetListByFormNo:" + e.ToString());
                return null;
            }
        }

        public bool UpdatePageInfo(string reportformlist,string pageCount, string pageName)
        {
            throw new NotImplementedException();
        }

        public bool UpdateClientPrintTimes(string[] reportformlist)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
