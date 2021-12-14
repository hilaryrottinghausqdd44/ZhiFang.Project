using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
    /// <summary>
    /// 数据访问类ReportForm。
    /// </summary>
    public class ReportForm : IDReportForm
    {
        DbHelperSQLObj DbHelperSQL;
        public ReportForm()
        {
            DbHelperSQL = new DbHelperSQLObj();
        }
        public ReportForm(string ConnStr)
        {
            DbHelperSQL = new DbHelperSQLObj(ConnStr);
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return -1;// DbHelperSQL.GetMaxID("SectionNo", "ReportForm"); 
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string FormNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ReportForm");
            string[] p = FormNo.Split(';');
            if (p.Length >= 4)
            {
                strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
            }
            else
            {
                strSql.Append(" where 1=2 ");
            }
            //strSql.Append(" where ReceiveDate='" + ReceiveDate + "' and SectionNo=" + SectionNo + " and TestTypeNo=" + TestTypeNo + " and SampleNo='" + SampleNo + "' ");
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
            if (model.caseno != null)
            {
                strSql1.Append("caseno,");
                strSql2.Append("'" + model.caseno + "',");
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
            if (model.abnormityflag != null)
            {
                strSql1.Append("abnormityflag,");
                strSql2.Append("" + model.abnormityflag + ",");
            }
            if (model.HISDateTime != null)
            {
                strSql1.Append("HISDateTime,");
                strSql2.Append("'" + model.HISDateTime + "',");
            }
            if (model.allowprint != null)
            {
                strSql1.Append("allowprint,");
                strSql2.Append("" + model.allowprint + ",");
            }
            if (model.RemoveFeesReason != null)
            {
                strSql1.Append("RemoveFeesReason,");
                strSql2.Append("'" + model.RemoveFeesReason + "',");
            }
            if (model.UrgentState != null)
            {
                strSql1.Append("UrgentState,");
                strSql2.Append("'" + model.UrgentState + "',");
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
            if (model.phoneCode != null)
            {
                strSql1.Append("phoneCode,");
                strSql2.Append("'" + model.phoneCode + "',");
            }
            if (model.IsNode != null)
            {
                strSql1.Append("IsNode,");
                strSql2.Append("" + model.IsNode + ",");
            }
            if (model.PhoneNodeCount != null)
            {
                strSql1.Append("PhoneNodeCount,");
                strSql2.Append("" + model.PhoneNodeCount + ",");
            }
            if (model.AutoNodeCount != null)
            {
                strSql1.Append("AutoNodeCount,");
                strSql2.Append("" + model.AutoNodeCount + ",");
            }
            if (model.FormDesc != null)
            {
                strSql1.Append("FormDesc,");
                strSql2.Append("'" + model.FormDesc + "',");
            }
            if (model.EquipCommMemo != null)
            {
                strSql1.Append("EquipCommMemo,");
                strSql2.Append("'" + model.EquipCommMemo + "',");
            }
            if (model.ESampleNo != null)
            {
                strSql1.Append("ESampleNo,");
                strSql2.Append("'" + model.ESampleNo + "',");
            }
            if (model.EPosition != null)
            {
                strSql1.Append("EPosition,");
                strSql2.Append("'" + model.EPosition + "',");
            }
            if (model.ISUsePG != null)
            {
                strSql1.Append("ISUsePG,");
                strSql2.Append("" + model.ISUsePG + ",");
            }
            if (model.OperMemo != null)
            {
                strSql1.Append("OperMemo,");
                strSql2.Append("'" + model.OperMemo + "',");
            }
            if (model.FromQCL != null)
            {
                strSql1.Append("FromQCL,");
                strSql2.Append("'" + model.FromQCL + "',");
            }
            if (model.ESend != null)
            {
                strSql1.Append("ESend,");
                strSql2.Append("'" + model.ESend + "',");
            }
            if (model.IsDel != null)
            {
                strSql1.Append("IsDel,");
                strSql2.Append("" + model.IsDel + ",");
            }
            if (model.EModule != null)
            {
                strSql1.Append("EModule,");
                strSql2.Append("'" + model.EModule + "',");
            }
            if (model.IsRedo != null)
            {
                strSql1.Append("IsRedo,");
                strSql2.Append("" + model.IsRedo + ",");
            }
            if (model.SampleSender != null)
            {
                strSql1.Append("SampleSender,");
                strSql2.Append("'" + model.SampleSender + "',");
            }
            if (model.SampleSendTime != null)
            {
                strSql1.Append("SampleSendTime,");
                strSql2.Append("'" + model.SampleSendTime + "',");
            }
            if (model.SendPlaceNo != null)
            {
                strSql1.Append("SendPlaceNo,");
                strSql2.Append("" + model.SendPlaceNo + ",");
            }
            if (model.SendFlag != null)
            {
                strSql1.Append("SendFlag,");
                strSql2.Append("" + model.SendFlag + ",");
            }
            if (model.Sickorder != null)
            {
                strSql1.Append("Sickorder,");
                strSql2.Append("'" + model.Sickorder + "',");
            }
            if (model.SickType != null)
            {
                strSql1.Append("SickType,");
                strSql2.Append("" + model.SickType + ",");
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
            if (model.StatusNo != null)
            {
                strSql.Append("StatusNo=" + model.StatusNo + ",");
            }
            if (model.SampleTypeNo != null)
            {
                strSql.Append("SampleTypeNo=" + model.SampleTypeNo + ",");
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
            if (model.caseno != null)
            {
                strSql.Append("caseno='" + model.caseno + "',");
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
            if (model.PrintDateTime != null)
            {
                strSql.Append("PrintDateTime='" + model.PrintDateTime + "',");
            }
            if (model.PrintOper != null)
            {
                strSql.Append("PrintOper='" + model.PrintOper + "',");
            }
            if (model.abnormityflag != null)
            {
                strSql.Append("abnormityflag=" + model.abnormityflag + ",");
            }
            if (model.HISDateTime != null)
            {
                strSql.Append("HISDateTime='" + model.HISDateTime + "',");
            }
            if (model.allowprint != null)
            {
                strSql.Append("allowprint=" + model.allowprint + ",");
            }
            if (model.RemoveFeesReason != null)
            {
                strSql.Append("RemoveFeesReason='" + model.RemoveFeesReason + "',");
            }
            if (model.UrgentState != null)
            {
                strSql.Append("UrgentState='" + model.UrgentState + "',");
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
            if (model.phoneCode != null)
            {
                strSql.Append("phoneCode='" + model.phoneCode + "',");
            }
            if (model.IsNode != null)
            {
                strSql.Append("IsNode=" + model.IsNode + ",");
            }
            if (model.PhoneNodeCount != null)
            {
                strSql.Append("PhoneNodeCount=" + model.PhoneNodeCount + ",");
            }
            if (model.AutoNodeCount != null)
            {
                strSql.Append("AutoNodeCount=" + model.AutoNodeCount + ",");
            }
            if (model.FormDesc != null)
            {
                strSql.Append("FormDesc='" + model.FormDesc + "',");
            }
            if (model.EquipCommMemo != null)
            {
                strSql.Append("EquipCommMemo='" + model.EquipCommMemo + "',");
            }
            if (model.ESampleNo != null)
            {
                strSql.Append("ESampleNo='" + model.ESampleNo + "',");
            }
            if (model.EPosition != null)
            {
                strSql.Append("EPosition='" + model.EPosition + "',");
            }
            if (model.ISUsePG != null)
            {
                strSql.Append("ISUsePG=" + model.ISUsePG + ",");
            }
            if (model.OperMemo != null)
            {
                strSql.Append("OperMemo='" + model.OperMemo + "',");
            }
            if (model.FromQCL != null)
            {
                strSql.Append("FromQCL='" + model.FromQCL + "',");
            }
            if (model.ESend != null)
            {
                strSql.Append("ESend='" + model.ESend + "',");
            }
            if (model.IsDel != null)
            {
                strSql.Append("IsDel=" + model.IsDel + ",");
            }
            if (model.EModule != null)
            {
                strSql.Append("EModule='" + model.EModule + "',");
            }
            if (model.IsRedo != null)
            {
                strSql.Append("IsRedo=" + model.IsRedo + ",");
            }
            if (model.SampleSender != null)
            {
                strSql.Append("SampleSender='" + model.SampleSender + "',");
            }
            if (model.SampleSendTime != null)
            {
                strSql.Append("SampleSendTime='" + model.SampleSendTime + "',");
            }
            if (model.SendPlaceNo != null)
            {
                strSql.Append("SendPlaceNo=" + model.SendPlaceNo + ",");
            }
            if (model.SendFlag != null)
            {
                strSql.Append("SendFlag=" + model.SendFlag + ",");
            }
            if (model.Sickorder != null)
            {
                strSql.Append("Sickorder='" + model.Sickorder + "',");
            }
            if (model.SickType != null)
            {
                strSql.Append("SickType=" + model.SickType + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            if (model.FormNo != null && model.FormNo.Split(';').Length >= 4)
            {
                string[] p = model.FormNo.Split(';');
                if (p.Length >= 4)
                {
                    strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                }
                else
                {
                    strSql.Append(" where 1=2 ");
                }
            }
            else
            {
                strSql.Append(" where ReceiveDate='" + model.ReceiveDate + "' and SectionNo=" + model.SectionNo + " and TestTypeNo=" + model.TestTypeNo + " and SampleNo='" + model.SampleNo + "' ");
            }

            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string FormNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ReportForm ");
            string[] p = FormNo.Split(';');
            if (p.Length >= 4)
            {
                strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
            }
            else
            {
                strSql.Append(" where 1=2 ");
            }
            //strSql.Append(" where ReceiveDate='" + ReceiveDate + "' and SectionNo=" + SectionNo + " and TestTypeNo=" + TestTypeNo + " and SampleNo='" + SampleNo + "' ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
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
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Convert(varchar(10),ReceiveDate,21)+';'+Convert(varchar(30),SectionNo)+';'+Convert(varchar(30),TestTypeNo)+';'+Convert(varchar(30),SampleNo) as FormNo ,Convert(varchar(10),ReceiveDate,21)+';'+Convert(varchar(30),SectionNo)+';'+Convert(varchar(30),TestTypeNo)+';'+Convert(varchar(30),SampleNo) as ReportFormID ,Convert(varchar(10),ReceiveDate,21) as ReceiveDate,* ");
            strSql.Append(" FROM ReportForm ");
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
            strSql.Append(" Convert(varchar(10),ReceiveDate,21)+';'+Convert(varchar(30),SectionNo)+';'+Convert(varchar(30),TestTypeNo)+';'+Convert(varchar(30),SampleNo) as FormNo,Convert(varchar(10),ReceiveDate,21)+';'+Convert(varchar(30),SectionNo)+';'+Convert(varchar(30),TestTypeNo)+';'+Convert(varchar(30),SampleNo) as ReportFormID , Convert(varchar(10),ReceiveDate,21) as ReceiveDate,* ");
            strSql.Append(" FROM ReportForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataTable GetReportValue(string[] p)
        {
            try
            {
                if (p.Length >= 4)
                {
                    SqlParameter sp0 = new SqlParameter("@PatNo", SqlDbType.VarChar, 40);
                    SqlParameter sp1 = new SqlParameter("@ItemNo", SqlDbType.VarChar, 40);
                    SqlParameter sp2 = new SqlParameter("@Check", SqlDbType.VarChar, 40);
                    SqlParameter sp3 = new SqlParameter("@StarRd", SqlDbType.VarChar, 40);
                    SqlParameter sp4 = new SqlParameter("@EndRd", SqlDbType.VarChar, 40);
                    sp0.Value = p[0];
                    sp1.Value = p[1];
                    sp2.Value = p[2];
                    sp3.Value = p[3];
                    sp4.Value = p[4];
                    DataSet ds = DbHelperSQL.RunProcedure("GetReportValue", new SqlParameter[] { sp0, sp1, sp2, sp3, sp4 }, "ReportFormFull");

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
        public DataTable GetReportValue(string[] p, string a)
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
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetReportFormFullList:" + FormNo);
                string[] p = FormNo.Split(';');
                string receivedate = p[0];
                string sectionno = p[1];
                string testtypeno = p[2];
                string sampleno = p[3];
                var sql = "select * from ReportFormQueryDataSource where ReceiveDate='" + receivedate + "' and SectionNo=" + sectionno + " and TestTypeNo=" + testtypeno + " and SampleNo='" + sampleno + "' ";
                DataSet ds = DbHelperSQL.Query(sql);
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetReportFormFullList:sql="+sql);
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
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetReportFormFullList:" + e);
                return new DataTable();
            }
        }

        public DataSet GetList(Model.ReportForm model, DateTime? StartDay, DateTime? EndDay)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Convert(varchar(10),ReceiveDate,21)+';'+Convert(varchar(30),SectionNo)+';'+Convert(varchar(30),TestTypeNo)+';'+Convert(varchar(30),SampleNo) as FormNo ,Convert(varchar(10),ReceiveDate,21)+';'+Convert(varchar(30),SectionNo)+';'+Convert(varchar(30),TestTypeNo)+';'+Convert(varchar(30),SampleNo) as ReportFormID ,case when ReceiveTime is null then Convert(varchar(10),ReceiveDate,21)+' 00:00:00' when ReceiveTime is not null then Convert(varchar(10),ReceiveDate,21)+' '+Convert(varchar(10),receivetime,108) end as ReceiveDateTime ,* ");
                strSql.Append(" FROM ReportForm where 1=1 ");

                if (model.ReceiveDate != null)
                {
                    strSql.Append(" and ReceiveDate='" + model.ReceiveDate + "' ");
                }
                if (StartDay != null)
                {
                    strSql.Append(" and ReceiveDate>='" + StartDay.Value + "' ");
                }
                if (EndDay != null)
                {
                    strSql.Append(" and ReceiveDate<='" + EndDay.Value + "' ");
                }

                if (model.SectionNo != null)
                {
                    strSql.Append(" and SectionNo=" + model.SectionNo + "");
                }
                if (model.TestTypeNo != null)
                {
                    strSql.Append(" and TestTypeNo='" + model.TestTypeNo + "'");
                }
                if (model.SampleNo != null)
                {
                    strSql.Append(" and SampleNo=" + model.SampleNo + "");
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
                    strSql.Append(" and CName = '" + model.CName + "'");
                }
                if (model.GenderNo != null)
                {
                    strSql.Append(" and GenderNo=" + model.GenderNo + "");
                }
                if (model.Birthday != null)
                {
                    strSql.Append(" and Birthday='" + model.Birthday + "'");
                }
                if (model.Age != null && model.Age != 0)
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
                if (model.zdy1 != null && model.zdy1 != "")
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
                if (model.caseno != null)
                {
                    strSql.Append(" and caseno='" + model.caseno + "'");
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
                if (model.PrintDateTime != null)
                {
                    strSql.Append(" and PrintDateTime='" + model.PrintDateTime + "'");
                }
                if (model.PrintOper != null)
                {
                    strSql.Append(" and PrintOper='" + model.PrintOper + "'");
                }
                if (model.abnormityflag != null)
                {
                    strSql.Append(" and abnormityflag=" + model.abnormityflag + "");
                }
                if (model.HISDateTime != null)
                {
                    strSql.Append(" and HISDateTime='" + model.HISDateTime + "'");
                }
                if (model.allowprint != null)
                {
                    strSql.Append(" and allowprint=" + model.allowprint + "");
                }
                if (model.RemoveFeesReason != null)
                {
                    strSql.Append(" and RemoveFeesReason='" + model.RemoveFeesReason + "'");
                }
                if (model.UrgentState != null)
                {
                    strSql.Append(" and UrgentState='" + model.UrgentState + "'");
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
                if (model.phoneCode != null)
                {
                    strSql.Append(" and phoneCode='" + model.phoneCode + "'");
                }
                if (model.IsNode != null)
                {
                    strSql.Append(" and IsNode=" + model.IsNode + "");
                }
                if (model.PhoneNodeCount != null)
                {
                    strSql.Append(" and PhoneNodeCount=" + model.PhoneNodeCount + "");
                }
                if (model.AutoNodeCount != null)
                {
                    strSql.Append(" and AutoNodeCount=" + model.AutoNodeCount + "");
                }
                if (model.FormDesc != null)
                {
                    strSql.Append(" and FormDesc='" + model.FormDesc + "'");
                }
                if (model.EquipCommMemo != null)
                {
                    strSql.Append(" and EquipCommMemo='" + model.EquipCommMemo + "'");
                }
                if (model.ESampleNo != null)
                {
                    strSql.Append(" and ESampleNo='" + model.ESampleNo + "'");
                }
                if (model.EPosition != null)
                {
                    strSql.Append(" and EPosition='" + model.EPosition + "'");
                }
                if (model.ISUsePG != null)
                {
                    strSql.Append(" and ISUsePG=" + model.ISUsePG + "");
                }
                if (model.OperMemo != null)
                {
                    strSql.Append(" and OperMemo='" + model.OperMemo + "'");
                }
                if (model.FromQCL != null)
                {
                    strSql.Append(" and FromQCL='" + model.FromQCL + "'");
                }
                if (model.ESend != null)
                {
                    strSql.Append(" and ESend='" + model.ESend + "'");
                }
                if (model.IsDel != null)
                {
                    strSql.Append(" and IsDel=" + model.IsDel + "");
                }
                if (model.EModule != null)
                {
                    strSql.Append(" and EModule='" + model.EModule + "'");
                }
                if (model.IsRedo != null)
                {
                    strSql.Append(" and IsRedo=" + model.IsRedo + "");
                }
                if (model.SampleSender != null)
                {
                    strSql.Append(" and SampleSender='" + model.SampleSender + "'");
                }
                if (model.SampleSendTime != null)
                {
                    strSql.Append(" and SampleSendTime='" + model.SampleSendTime + "'");
                }
                if (model.SendPlaceNo != null)
                {
                    strSql.Append(" and SendPlaceNo=" + model.SendPlaceNo + "");
                }
                if (model.SendFlag != null)
                {
                    strSql.Append(" and SendFlag=" + model.SendFlag + "");
                }
                if (model.Sickorder != null)
                {
                    strSql.Append(" and Sickorder='" + model.Sickorder + "'");
                }
                if (model.SickType != null)
                {
                    strSql.Append(" and SickType=" + model.SickType + "");
                }
                ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
                return DbHelperSQL.Query(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                return null;
            }
        }

        public DataSet GetList(Model.ReportForm model)
        {
            return this.GetList(model, null, null);
        }

        public bool UpdatePrintTimes(string[] reportformlist, string uluserCName)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (null != uluserCName && !"".Equals(uluserCName))
                {
                    strSql.Append(" update ReportForm set PrintOper = '" + uluserCName + "',printtimes=isnull(printtimes, 0) +1 , PRINTDATETIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'         where  ");
                }
                else 
                { 
                    strSql.Append(" update ReportForm set printtimes=isnull(printtimes, 0) +1 , PRINTDATETIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'         where  ");
                }
                foreach (string a in reportformlist)
                {
                    string[] aaa = a.Split(';');
                    strSql.Append(" ( Receivedate='" + aaa[0] + "' and SectionNo='" + aaa[1] + "' and TestTypeNo='" + aaa[2] + "' and SampleNo='" + aaa[3] + "') or ");
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
                ZhiFang.Common.Log.Log.Debug("UpdatePrintTimes.异常："+e.ToString());
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
                strSql.Append(" from ReportForm ");
                string[] p = FormNo.Split(';');
                if (p.Length >= 4)
                {
                    strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                }
                else
                {
                    strSql.Append(" where 1=2 ");
                }
                //strSql.Append(" where ReceiveDate='" + ReceiveDate + "' and SectionNo=" + SectionNo + " and TestTypeNo=" + TestTypeNo + " and SampleNo='" + SampleNo + "' ");
                return DbHelperSQL.Query(strSql.ToString());
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66.GetListByFormNo:" + e.ToString());
                return null;
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
                ZhiFang.Common.Log.Log.Debug("UpdateClientPrintTimes.异常：" + e.ToString());
                return false;
            }
        }

        public DataTable GetReportFormList(string[] formNo)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * ");
            strSql.Append(" FROM ReportFormQueryDataSource ");
            List<string> idsql = new List<string>();
            foreach (var id in formNo)
            {
                string[] tmp = id.Split(';');
                idsql.Add("( Receivedate='" + tmp[0] + "' and SectionNo='" + tmp[1] + "' and TestTypeNo='" + tmp[2] + "' and SampleNo='" + tmp[3] + "') ");
            }
            strSql.Append(" where   (" + string.Join("or", idsql) + ")  ");
            
            ZhiFang.Common.Log.Log.Debug("GetReportFormList.formNo:" + formNo );
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
            List<string> idsql = new List<string>();
            foreach (var id in idList)
            {
                string[] tmp = id.Split(';');
                idsql.Add("( Receivedate='" + tmp[0] + "' and SectionNo='" + tmp[1] + "' and TestTypeNo='" + tmp[2] + "' and SampleNo='" + tmp[3] + "') ");
            }


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where   (" + string.Join("or", idsql) + ") and " + strWhere + "   ");
            }
            else
            {
                strSql.Append(" where   (" + string.Join("or", idsql) + ")  ");
            }
            ZhiFang.Common.Log.Log.Debug("GetReporFormAllByReportFormIdList.idList:" + idList + ";fields:" + fields + ";strWhere:" + strWhere);
            ZhiFang.Common.Log.Log.Debug("GetReporFormAllByReportFormIdList.sql:"+ strSql.ToString());
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

        public int UpdateClientPrint(string formno)
        {

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update ReportForm set ClientPrint=0   ");
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

        public int UpdatePrintTimes(string formno)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update ReportForm set PrintTimes=0   ");
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

        public DataTable GetReportFormListByFormId(string formNo)
        {
            throw new NotImplementedException();
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
            List<string> idsql = new List<string>();
            foreach (var id in idList)
            {
                string[] tmp = id.Split(';');
                idsql.Add("( Receivedate='" + tmp[0] + "' and SectionNo='" + tmp[1] + "' and TestTypeNo='" + tmp[2] + "' and SampleNo='" + tmp[3] + "') ");
            }


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where   (" + string.Join("or", idsql) + ") and " + strWhere + "   ");
            }
            else
            {
                strSql.Append(" where   (" + string.Join("or", idsql) + ")  ");
            }
            ZhiFang.Common.Log.Log.Debug("GetSampleReportFromByReportFormID.idList:" + idList + ";fields:" + fields + ";strWhere:" + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetReportFormFullByReportFormID(string ReportFormID)
        {
            throw new NotImplementedException();
        }

        public int UpdateReportFormFull(ReportFormFull model)
        {
            throw new NotImplementedException();
        }

        public DataSet GetRepotFormByReportFormIDGroupByZdy15(string PatNo, string zdy15)
        {

            if (null == PatNo || "" == PatNo) {
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetRepotFormByReportFormIDGroupByZdy15.PatNo为空");
                return new DataSet();
            }
            if(null == zdy15 || "" == zdy15)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetRepotFormByReportFormIDGroupByZdy15.zdy15");
                return new DataSet();
            }
            ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetRepotFormByReportFormIDGroupByZdy15.PatNo:" + PatNo + ",zdy15:" + zdy15);
            var sql = "select * from ReportFormQueryDataSource where PatNo='" + PatNo + "' and zdy15='" + zdy15+"'" ;
            ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetRepotFormByReportFormIDGroupByZdy15.SQL:" + sql);
            DataSet ds = DbHelperSQL.Query(sql);
            return ds;
        }

        public DataSet GetRepotFormByReportFormIDAndZdy15AndReceiveDate(string PatNo, string zdy15, string ReceiveDate)
        {
            if (null == PatNo || "" == PatNo)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.PatNo为空");
                return new DataSet();
            }
            if (null == zdy15 || "" == zdy15)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.zdy15");
                return new DataSet();
            }
            if (null == ReceiveDate || "" == ReceiveDate)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.ReceiveDate");
                return new DataSet();
            }
            ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.PatNo:" + PatNo + ",zdy15:" + zdy15+ ",ReceiveDate:" + ReceiveDate);
            var sql = "select * from ReportFormQueryDataSource where PatNo='" + PatNo + "' and zdy15='" + zdy15 + "' and ReceiveDate='"+ReceiveDate+"'";
            ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetRepotFormByReportFormIDAndZdy15AndReceiveDate.SQL:" + sql);
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
            strSql.Append(" FROM ReportFormQueryDataSource ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrEmpty(filedOrder))
            {
                strSql.Append(" order by " + filedOrder);
            }
            
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

