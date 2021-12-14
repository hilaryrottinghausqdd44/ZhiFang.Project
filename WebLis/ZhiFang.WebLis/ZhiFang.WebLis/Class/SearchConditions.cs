using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;

namespace ZhiFang.WebLis.Class
{
    public class SearchConditions
    {
        public string searchconditionsstr(ZhiFang.Model.ReportForm rfm, DateTime? StartDay, DateTime? EndDay)
        {
            StringBuilder scondition = new StringBuilder();
            if (StartDay != null)
            {
                scondition.Append("StartDay=" + StartDay.Value.ToShortDateString() + ";");
            }
            if (EndDay != null)
            {
                scondition.Append("EndDay=" + EndDay.Value.ToShortDateString() + ";");
            }
            scondition.Append(searchconditionsstr(rfm));
            return scondition.ToString();
        }
        public string searchconditionsstr(ZhiFang.Model.ReportForm model)
        {
            StringBuilder scondition = new StringBuilder();
            if (model.ReceiveDate != null)
            {
                scondition.Append("ReceiveDate="+ model.ReceiveDate + ";");
            }
            if (model.SectionNo != null)
            {
                scondition.Append("SectionNo="+ model.SectionNo + ";");
            }
            if (model.TestTypeNo != null)
            {
                scondition.Append("TestTypeNo="+ model.TestTypeNo + ";");
            }
            if (model.SampleNo != null)
            {
                scondition.Append("SampleNo="+ model.SampleNo + ";");
            }
            if (model.StatusNo != null)
            {
                scondition.Append("StatusNo="+ model.StatusNo + ";");
            }
            if (model.SampleTypeNo != null)
            {
                scondition.Append("SampleTypeNo="+ model.SampleTypeNo + ";");
            }
            if (model.PatNo != null)
            {
                scondition.Append("PatNo="+ model.PatNo + ";");
            }
            if (model.CName != null)
            {
                scondition.Append("CName="+ model.CName + ";");
            }
            if (model.GenderNo != null)
            {
                scondition.Append("GenderNo="+ model.GenderNo + ";");
            }
            if (model.Birthday != null)
            {
                scondition.Append("Birthday="+ model.Birthday + ";");
            }
            if (model.Age != null)
            {
                scondition.Append("Age="+ model.Age + ";");
            }
            if (model.AgeUnitNo != null)
            {
                scondition.Append("AgeUnitNo="+ model.AgeUnitNo + ";");
            }
            if (model.FolkNo != null)
            {
                scondition.Append("FolkNo="+ model.FolkNo + ";");
            }
            if (model.DistrictNo != null)
            {
                scondition.Append("DistrictNo="+ model.DistrictNo + ";");
            }
            if (model.WardNo != null)
            {
                scondition.Append("WardNo="+ model.WardNo + ";");
            }
            if (model.Bed != null)
            {
                scondition.Append("Bed="+ model.Bed + ";");
            }
            if (model.DeptNo != null)
            {
                scondition.Append("DeptNo="+ model.DeptNo + ";");
            }
            if (model.Doctor != null)
            {
                scondition.Append("Doctor="+ model.Doctor + ";");
            }
            if (model.ChargeNo != null)
            {
                scondition.Append("ChargeNo="+ model.ChargeNo + ";");
            }
            if (model.Charge != null)
            {
                scondition.Append("Charge="+ model.Charge + ";");
            }
            if (model.Collecter != null)
            {
                scondition.Append("Collecter="+ model.Collecter + ";");
            }
            if (model.CollectDate != null)
            {
                scondition.Append("CollectDate="+ model.CollectDate + ";");
            }
            if (model.CollectTime != null)
            {
                scondition.Append("CollectTime="+ model.CollectTime + ";");
            }
            if (model.FormMemo != null)
            {
                scondition.Append("FormMemo="+ model.FormMemo + ";");
            }
            if (model.Technician != null)
            {
                scondition.Append("Technician="+ model.Technician + ";");
            }
            if (model.TestDate != null)
            {
                scondition.Append("TestDate="+ model.TestDate + ";");
            }
            if (model.TestTime != null)
            {
                scondition.Append("TestTime="+ model.TestTime + ";");
            }
            if (model.Operator != null)
            {
                scondition.Append("Operator="+ model.Operator + ";");
            }
            if (model.OperDate != null)
            {
                scondition.Append("OperDate="+ model.OperDate + ";");
            }
            if (model.OperTime != null)
            {
                scondition.Append("OperTime="+ model.OperTime + ";");
            }
            if (model.Checker != null)
            {
                scondition.Append("Checker="+ model.Checker + ";");
            }
            if (model.CheckDate != null)
            {
                scondition.Append("CheckDate="+ model.CheckDate + ";");
            }
            if (model.CheckTime != null)
            {
                scondition.Append("CheckTime="+ model.CheckTime + ";");
            }
            if (model.SerialNo != null)
            {
                scondition.Append("SerialNo="+ model.SerialNo + ";");
            }
            if (model.BarCode != null)
            {
                scondition.Append("BarCode="+ model.BarCode + ";");
            }
            if (model.RequestSource != null)
            {
                scondition.Append("RequestSource="+ model.RequestSource + ";");
            }
            if (model.DiagNo != null)
            {
                scondition.Append("DiagNo="+ model.DiagNo + ";");
            }
            if (model.PrintTimes != null)
            {
                scondition.Append("PrintTimes="+ model.PrintTimes + ";");
            }
            if (model.SickTypeNo != null)
            {
                scondition.Append("SickTypeNo="+ model.SickTypeNo + ";");
            }
            if (model.FormComment != null)
            {
                scondition.Append("FormComment="+ model.FormComment + ";");
            }
            if (model.zdy1 != null)
            {
                scondition.Append("zdy1="+ model.zdy1 + ";");
            }
            if (model.zdy2 != null)
            {
                scondition.Append("zdy2="+ model.zdy2 + ";");
            }
            if (model.zdy3 != null)
            {
                scondition.Append("zdy3="+ model.zdy3 + ";");
            }
            if (model.zdy4 != null)
            {
                scondition.Append("zdy4="+ model.zdy4 + ";");
            }
            if (model.zdy5 != null)
            {
                scondition.Append("zdy5="+ model.zdy5 + ";");
            }
            if (model.inceptdate != null)
            {
                scondition.Append("inceptdate="+ model.inceptdate + ";");
            }
            if (model.incepttime != null)
            {
                scondition.Append("incepttime="+ model.incepttime + ";");
            }
            if (model.incepter != null)
            {
                scondition.Append("incepter="+ model.incepter + ";");
            }
            if (model.onlinedate != null)
            {
                scondition.Append("onlinedate="+ model.onlinedate + ";");
            }
            if (model.onlinetime != null)
            {
                scondition.Append("onlinetime="+ model.onlinetime + ";");
            }
            if (model.bmanno != null)
            {
                scondition.Append("bmanno="+ model.bmanno + ";");
            }
            if (model.clientno != null)
            {
                scondition.Append("clientno="+ model.clientno + ";");
            }
            if (model.chargeflag != null)
            {
                scondition.Append("chargeflag="+ model.chargeflag + ";");
            }
            if (model.isReceive != null)
            {
                scondition.Append("isReceive="+ model.isReceive + ";");
            }
            if (model.ReceiveMan != null)
            {
                scondition.Append("ReceiveMan="+ model.ReceiveMan + ";");
            }
            if (model.ReceiveTime != null)
            {
                scondition.Append("ReceiveTime="+ model.ReceiveTime + ";");
            }
            if (model.concessionNum != null)
            {
                scondition.Append("concessionNum="+ model.concessionNum + ";");
            }
            if (model.Sender2 != null)
            {
                scondition.Append("Sender2="+ model.Sender2 + ";");
            }
            if (model.SenderTime2 != null)
            {
                scondition.Append("SenderTime2="+ model.SenderTime2 + ";");
            }
            if (model.resultstatus != null)
            {
                scondition.Append("resultstatus="+ model.resultstatus + ";");
            }
            if (model.testaim != null)
            {
                scondition.Append("testaim="+ model.testaim + ";");
            }
            if (model.resultprinttimes != null)
            {
                scondition.Append("resultprinttimes="+ model.resultprinttimes + ";");
            }
            if (model.paritemname != null)
            {
                scondition.Append("paritemname="+ model.paritemname + ";");
            }
            if (model.clientprint != null)
            {
                scondition.Append("clientprint="+ model.clientprint + ";");
            }
            if (model.resultsend != null)
            {
                scondition.Append("resultsend="+ model.resultsend + ";");
            }
            if (model.reportsend != null)
            {
                scondition.Append("reportsend="+ model.reportsend + ";");
            }
            if (model.CountNodesFormSource != null)
            {
                scondition.Append("CountNodesFormSource="+ model.CountNodesFormSource + ";");
            }
            if (model.commsendflag != null)
            {
                scondition.Append("commsendflag="+ model.commsendflag + ";");
            }
            if (model.ZDY6 != null)
            {
                scondition.Append("ZDY6=" + model.ZDY6 + ";");
            }
            if (model.ZDY7 != null)
            {
                scondition.Append("ZDY7=" + model.ZDY7 + ";");
            }
            if (model.ZDY8 != null)
            {
                scondition.Append("ZDY8=" + model.ZDY8 + ";");
            }
            if (model.ZDY9 != null)
            {
                scondition.Append("ZDY9=" + model.ZDY9 + ";");
            }
            if (model.ZDY10 != null)
            {
                scondition.Append("ZDY10=" + model.ZDY10 + ";");
            }
            if (model.PrintDateTime != null)
            {
                scondition.Append("PrintDateTime="+ model.PrintDateTime + ";");
            }
            if (model.PrintOper != null)
            {
                scondition.Append("PrintOper="+ model.PrintOper + ";");
            }
            if (model.FormStateNo != null)
            {
                scondition.Append("FormStateNo="+ model.FormStateNo + ";");
            }
            if (model.OldSerialNo != null)
            {
                scondition.Append("OldSerialNo="+ model.OldSerialNo + ";");
            }
            if (model.mresulttype != null)
            {
                scondition.Append("mresulttype="+ model.mresulttype + ";");
            }
            if (model.Diagnose != null)
            {
                scondition.Append("Diagnose="+ model.Diagnose + ";");
            }
            if (model.TestPurpose != null)
            {
                scondition.Append("TestPurpose="+ model.TestPurpose + ";");
            }
            if (model.IsFree != null)
            {
                scondition.Append("IsFree="+ model.IsFree + ";");
            }
            if (model.NOperator != null)
            {
                scondition.Append("NOperator="+ model.NOperator + ";");
            }
            if (model.NOperDate != null)
            {
                scondition.Append("NOperDate="+ model.NOperDate + ";");
            }
            if (model.NOperTime != null)
            {
                scondition.Append("NOperTime="+ model.NOperTime + ";");
            }
            if (model.PathologyNo != null)
            {
                scondition.Append("PathologyNo="+ model.PathologyNo + ";");
            }
            if (model.FormNo != null)
            {
                scondition.Append("FormNo="+ model.FormNo + ";");
            }
            if (scondition.Length > 0)
            {
                return scondition.ToString().Substring(0,scondition.Length-1);
            }
            else
            {
                return scondition.ToString();
            }            
        }
        public DateTime?[] searchDateTimeconditions(string searchconditionsstr)
        {
            DateTime?[] a = new DateTime?[2];
            SortedList sl = ZhiFang.Common.Public.StringPlus.GetStrSortedList(searchconditionsstr, ';', '=');
            try
            {
                a[0] = Convert.ToDateTime(sl["StartDay"]);
            }
            catch
            {
                a[0] = null;
            }
            try
            {
                a[1] = Convert.ToDateTime(sl["EndDay"]);
            }
            catch
            {
                a[1] = null;
            }
            return a;
        }
        public ZhiFang.Model.ReportForm searchModelconditions(string searchconditionsstr)
        {
            ZhiFang.Model.ReportForm rfm = new ZhiFang.Model.ReportForm();
            SortedList sl = ZhiFang.Common.Public.StringPlus.GetStrSortedList(searchconditionsstr, ';', '=');
            if (sl["ReceiveDate"].ToString() != "")
            {
                rfm.ReceiveDate = DateTime.Parse(sl["ReceiveDate"].ToString());
            }
            if (sl["SectionNo"].ToString() != "")
            {
                rfm.SectionNo = int.Parse(sl["SectionNo"].ToString());
            }
            if (sl["TestTypeNo"].ToString() != "")
            {
                rfm.TestTypeNo = int.Parse(sl["TestTypeNo"].ToString());
            }
            rfm.SampleNo = sl["SampleNo"].ToString();
            if (sl["StatusNo"].ToString() != "")
            {
                rfm.StatusNo = int.Parse(sl["StatusNo"].ToString());
            }
            if (sl["SampleTypeNo"].ToString() != "")
            {
                rfm.SampleTypeNo = int.Parse(sl["SampleTypeNo"].ToString());
            }
            rfm.PatNo = sl["PatNo"].ToString();
            rfm.CName = sl["CName"].ToString();
            if (sl["GenderNo"].ToString() != "")
            {
                rfm.GenderNo = int.Parse(sl["GenderNo"].ToString());
            }
            if (sl["Birthday"].ToString() != "")
            {
                rfm.Birthday = DateTime.Parse(sl["Birthday"].ToString());
            }
            if (sl["Age"].ToString() != "")
            {
                rfm.Age = decimal.Parse(sl["Age"].ToString());
            }
            if (sl["AgeUnitNo"].ToString() != "")
            {
                rfm.AgeUnitNo = int.Parse(sl["AgeUnitNo"].ToString());
            }
            if (sl["FolkNo"].ToString() != "")
            {
                rfm.FolkNo = int.Parse(sl["FolkNo"].ToString());
            }
            if (sl["DistrictNo"].ToString() != "")
            {
                rfm.DistrictNo = int.Parse(sl["DistrictNo"].ToString());
            }
            if (sl["WardNo"].ToString() != "")
            {
                rfm.WardNo = int.Parse(sl["WardNo"].ToString());
            }
            rfm.Bed = sl["Bed"].ToString();
            if (sl["DeptNo"].ToString() != "")
            {
                rfm.DeptNo = int.Parse(sl["DeptNo"].ToString());
            }
            rfm.Doctor = sl["Doctor"].ToString();
            if (sl["ChargeNo"].ToString() != "")
            {
                rfm.ChargeNo = int.Parse(sl["ChargeNo"].ToString());
            }
            if (sl["Charge"].ToString() != "")
            {
                rfm.Charge = decimal.Parse(sl["Charge"].ToString());
            }
            rfm.Collecter = sl["Collecter"].ToString();
            if (sl["CollectDate"].ToString() != "")
            {
                rfm.CollectDate = DateTime.Parse(sl["CollectDate"].ToString());
            }
            if (sl["CollectTime"].ToString() != "")
            {
                rfm.CollectTime = DateTime.Parse(sl["CollectTime"].ToString());
            }
            rfm.FormMemo = sl["FormMemo"].ToString();
            rfm.Technician = sl["Technician"].ToString();
            if (sl["TestDate"].ToString() != "")
            {
                rfm.TestDate = DateTime.Parse(sl["TestDate"].ToString());
            }
            if (sl["TestTime"].ToString() != "")
            {
                rfm.TestTime = DateTime.Parse(sl["TestTime"].ToString());
            }
            rfm.Operator = sl["Operator"].ToString();
            if (sl["OperDate"].ToString() != "")
            {
                rfm.OperDate = DateTime.Parse(sl["OperDate"].ToString());
            }
            if (sl["OperTime"].ToString() != "")
            {
                rfm.OperTime = DateTime.Parse(sl["OperTime"].ToString());
            }
            rfm.Checker = sl["Checker"].ToString();
            if (sl["CheckDate"].ToString() != "")
            {
                rfm.CheckDate = DateTime.Parse(sl["CheckDate"].ToString());
            }
            if (sl["CheckTime"].ToString() != "")
            {
                rfm.CheckTime = DateTime.Parse(sl["CheckTime"].ToString());
            }
            rfm.SerialNo = sl["SerialNo"].ToString();
            rfm.BarCode = sl["BarCode"].ToString();
            rfm.RequestSource = sl["RequestSource"].ToString();
            if (sl["DiagNo"].ToString() != "")
            {
                rfm.DiagNo = int.Parse(sl["DiagNo"].ToString());
            }
            if (sl["PrintTimes"].ToString() != "")
            {
                rfm.PrintTimes = int.Parse(sl["PrintTimes"].ToString());
            }
            if (sl["SickTypeNo"].ToString() != "")
            {
                rfm.SickTypeNo = int.Parse(sl["SickTypeNo"].ToString());
            }
            rfm.FormComment = sl["FormComment"].ToString();
            rfm.zdy1 = sl["zdy1"].ToString();
            rfm.zdy2 = sl["zdy2"].ToString();
            rfm.zdy3 = sl["zdy3"].ToString();
            rfm.zdy4 = sl["zdy4"].ToString();
            rfm.zdy5 = sl["zdy5"].ToString();
            if (sl["inceptdate"].ToString() != "")
            {
                rfm.inceptdate = DateTime.Parse(sl["inceptdate"].ToString());
            }
            if (sl["incepttime"].ToString() != "")
            {
                rfm.incepttime = DateTime.Parse(sl["incepttime"].ToString());
            }
            rfm.incepter = sl["incepter"].ToString();
            if (sl["onlinedate"].ToString() != "")
            {
                rfm.onlinedate = DateTime.Parse(sl["onlinedate"].ToString());
            }
            if (sl["onlinetime"].ToString() != "")
            {
                rfm.onlinetime = DateTime.Parse(sl["onlinetime"].ToString());
            }
            if (sl["bmanno"].ToString() != "")
            {
                rfm.bmanno = int.Parse(sl["bmanno"].ToString());
            }
            if (sl["clientno"].ToString() != "")
            {
                rfm.clientno = int.Parse(sl["clientno"].ToString());
            }
            rfm.chargeflag = sl["chargeflag"].ToString();
            if (sl["isReceive"].ToString() != "")
            {
                rfm.isReceive = int.Parse(sl["isReceive"].ToString());
            }
            rfm.ReceiveMan = sl["ReceiveMan"].ToString();
            if (sl["ReceiveTime"].ToString() != "")
            {
                rfm.ReceiveTime = DateTime.Parse(sl["ReceiveTime"].ToString());
            }
            rfm.concessionNum = sl["concessionNum"].ToString();
            rfm.Sender2 = sl["Sender2"].ToString();
            if (sl["SenderTime2"].ToString() != "")
            {
                rfm.SenderTime2 = DateTime.Parse(sl["SenderTime2"].ToString());
            }
            if (sl["resultstatus"].ToString() != "")
            {
                rfm.resultstatus = int.Parse(sl["resultstatus"].ToString());
            }
            rfm.testaim = sl["testaim"].ToString();
            if (sl["resultprinttimes"].ToString() != "")
            {
                rfm.resultprinttimes = int.Parse(sl["resultprinttimes"].ToString());
            }
            rfm.paritemname = sl["paritemname"].ToString();
            rfm.clientprint = sl["clientprint"].ToString();
            rfm.resultsend = sl["resultsend"].ToString();
            rfm.reportsend = sl["reportsend"].ToString();
            rfm.CountNodesFormSource = sl["CountNodesFormSource"].ToString();
            if (sl["commsendflag"].ToString() != "")
            {
                rfm.commsendflag = int.Parse(sl["commsendflag"].ToString());
            }
            rfm.ZDY6 = sl["ZDY6"].ToString();
            rfm.ZDY7 = sl["ZDY7"].ToString();
            rfm.ZDY8 = sl["ZDY8"].ToString();
            rfm.ZDY9 = sl["ZDY9"].ToString();
            rfm.ZDY10 = sl["ZDY10"].ToString();
            if (sl["PrintDateTime"].ToString() != "")
            {
                rfm.PrintDateTime = DateTime.Parse(sl["PrintDateTime"].ToString());
            }
            rfm.PrintOper = sl["PrintOper"].ToString();
            if (sl["FormNo"].ToString() != "")
            {
                rfm.FormNo = sl["FormNo"].ToString();
            }
            if (sl["FormStateNo"].ToString() != "")
            {
                rfm.FormStateNo = int.Parse(sl["FormStateNo"].ToString());
            }
            rfm.OldSerialNo = sl["OldSerialNo"].ToString();
            if (sl["mresulttype"].ToString() != "")
            {
                rfm.mresulttype = int.Parse(sl["mresulttype"].ToString());
            }
            rfm.Diagnose = sl["Diagnose"].ToString();
            rfm.TestPurpose = sl["TestPurpose"].ToString();
            if (sl["IsFree"].ToString() != "")
            {
                rfm.IsFree = int.Parse(sl["IsFree"].ToString());
            }
            rfm.NOperator = sl["NOperator"].ToString();
            if (sl["NOperDate"].ToString() != "")
            {
                rfm.NOperDate = DateTime.Parse(sl["NOperDate"].ToString());
            }
            if (sl["NOperTime"].ToString() != "")
            {
                rfm.NOperTime = DateTime.Parse(sl["NOperTime"].ToString());
            }
            rfm.PathologyNo = sl["PathologyNo"].ToString();
            return rfm;
        }
        public string searchconditionsstr(ZhiFang.Model.ReportFormFull model)
        {
            StringBuilder scondition = new StringBuilder();
            if (model.CLIENTNO != null)
            {
                scondition.Append(" CLIENTNO=" + model.CLIENTNO + ";");
            }
            if (model.CNAME != null)
            {
                scondition.Append(" CNAME=" + model.CNAME + ";");
            }
            if (model.AGEUNITNAME != null)
            {
                scondition.Append(" AGEUNITNAME=" + model.AGEUNITNAME + ";");
            }
            if (model.GENDERNAME != null)
            {
                scondition.Append(" GENDERNAME=" + model.GENDERNAME + ";");
            }
            if (model.DEPTNAME != null)
            {
                scondition.Append(" DEPTNAME=" + model.DEPTNAME + ";");
            }
            if (model.DOCTORNAME != null)
            {
                scondition.Append(" DOCTORNAME=" + model.DOCTORNAME + ";");
            }
            if (model.DISTRICTNAME != null)
            {
                scondition.Append(" DISTRICTNAME=" + model.DISTRICTNAME + ";");
            }
            if (model.WARDNAME != null)
            {
                scondition.Append(" WARDNAME=" + model.WARDNAME + ";");
            }
            if (model.FOLKNAME != null)
            {
                scondition.Append(" FOLKNAME=" + model.FOLKNAME + ";");
            }
            if (model.SICKTYPENAME != null)
            {
                scondition.Append(" SICKTYPENAME=" + model.SICKTYPENAME + ";");
            }
            if (model.SAMPLETYPENAME != null)
            {
                scondition.Append(" SAMPLETYPENAME=" + model.SAMPLETYPENAME + ";");
            }

            if (model.SECTIONNAME != null)
            {
                scondition.Append(" SECTIONNAME=" + model.SECTIONNAME + ";");
            }

            if (model.TESTTYPENAME != null)
            {
                scondition.Append(" TESTTYPENAME=" + model.TESTTYPENAME + ";");
            }

            if (model.RECEIVEDATE != null)
            {
                scondition.Append(" RECEIVEDATE=" + model.RECEIVEDATE + ";");
            }
            if (model.SECTIONNO != null)
            {
                scondition.Append(" SECTIONNO=" + model.SECTIONNO + ";");
            }
            if (model.TESTTYPENO != null)
            {
                scondition.Append(" TESTTYPENO=" + model.TESTTYPENO + ";");
            }
            if (model.SAMPLENO != null)
            {
                scondition.Append(" SAMPLENO=" + model.SAMPLENO + ";");
            }
            if (model.STATUSNO != null)
            {
                scondition.Append(" STATUSNO=" + model.STATUSNO + ";");
            }

            if (model.SAMPLETYPENO != null)
            {
                scondition.Append(" SAMPLETYPENO=" + model.SAMPLETYPENO + ";");
            }

            if (model.PATNO != null)
            {
                scondition.Append(" PATNO=" + model.PATNO + ";");
            }

            if (model.GENDERNO != null)
            {
                scondition.Append(" GENDERNO=" + model.GENDERNO + ";");
            }

            if (model.BIRTHDAY != null)
            {
                scondition.Append(" BIRTHDAY=" + model.BIRTHDAY + ";");
            }

            if (model.AGE != null)
            {
                scondition.Append(" AGE=" + model.AGE + ";");
            }

            if (model.AGEUNITNO != null)
            {
                scondition.Append(" AGEUNITNO=" + model.AGEUNITNO + ";");
            }

            if (model.FOLKNO != null)
            {
                scondition.Append(" FOLKNO=" + model.FOLKNO + ";");
            }

            if (model.DISTRICTNO != null)
            {
                scondition.Append(" DISTRICTNO=" + model.DISTRICTNO + ";");
            }

            if (model.WARDNO != null)
            {
                scondition.Append(" WARDNO=" + model.WARDNO + ";");
            }

            if (model.BED != null)
            {
                scondition.Append(" BED=" + model.BED + ";");
            }

            if (model.DEPTNO != null)
            {
                scondition.Append(" DEPTNO=" + model.DEPTNO + ";");
            }

            if (model.DOCTOR != null)
            {
                scondition.Append(" DOCTOR=" + model.DOCTOR + ";");
            }

            if (model.CHARGENO != null)
            {
                scondition.Append(" CHARGENO=" + model.CHARGENO + ";");
            }

            if (model.CHARGE != null)
            {
                scondition.Append(" CHARGE=" + model.CHARGE + ";");
            }

            if (model.COLLECTER != null)
            {
                scondition.Append(" COLLECTER=" + model.COLLECTER + ";");
            }

            if (model.COLLECTDATE != null)
            {
                scondition.Append(" COLLECTDATE=" + model.COLLECTDATE + ";");
            }

            if (model.COLLECTTIME != null)
            {
                scondition.Append(" COLLECTTIME=" + model.COLLECTTIME + ";");
            }

            if (model.FORMMEMO != null)
            {
                scondition.Append(" FORMMEMO=" + model.FORMMEMO + ";");
            }

            if (model.TECHNICIAN != null)
            {
                scondition.Append(" TECHNICIAN=" + model.TECHNICIAN + ";");
            }

            if (model.TESTDATE != null)
            {
                scondition.Append(" TESTDATE=" + model.TESTDATE + ";");
            }

            if (model.TESTTIME != null)
            {
                scondition.Append(" TESTTIME=" + model.TESTTIME + ";");
            }

            if (model.OPERATOR != null)
            {
                scondition.Append(" OPERATOR=" + model.OPERATOR + ";");
            }

            if (model.OPERDATE != null)
            {
                scondition.Append(" OPERDATE=" + model.OPERDATE + ";");
            }

            if (model.OPERTIME != null)
            {
                scondition.Append(" OPERTIME=" + model.OPERTIME + ";");
            }

            if (model.CHECKER != null)
            {
                scondition.Append(" CHECKER=" + model.CHECKER + ";");
            }

            if (model.PRINTTIMES != null)
            {
                scondition.Append(" PRINTTIMES=" + model.PRINTTIMES + ";");
            }

            if (model.resultfile != null)
            {
                scondition.Append(" resultfile=" + model.resultfile + ";");
            }

            if (model.CHECKDATE != null)
            {
                scondition.Append(" CHECKDATE=" + model.CHECKDATE + ";");
            }

            if (model.CHECKTIME != null)
            {
                scondition.Append(" CHECKTIME=" + model.CHECKTIME + ";");
            }

            if (model.SERIALNO != null)
            {
                scondition.Append(" SERIALNO=" + model.SERIALNO + ";");
            }

            if (model.REQUESTSOURCE != null)
            {
                scondition.Append(" REQUESTSOURCE=" + model.REQUESTSOURCE + ";");
            }

            if (model.DIAGNO != null)
            {
                scondition.Append(" DIAGNO=" + model.DIAGNO + ";");
            }

            if (model.SICKTYPENO != null)
            {
                scondition.Append(" SICKTYPENO=" + model.SICKTYPENO + ";");
            }

            if (model.FORMCOMMENT != null)
            {
                scondition.Append(" FORMCOMMENT=" + model.FORMCOMMENT + ";");
            }

            if (model.ARTIFICERORDER != null)
            {
                scondition.Append(" ARTIFICERORDER=" + model.ARTIFICERORDER + ";");
            }

            if (model.SICKORDER != null)
            {
                scondition.Append(" SICKORDER=" + model.SICKORDER + ";");
            }

            if (model.SICKTYPE != null)
            {
                scondition.Append(" SICKTYPE=" + model.SICKTYPE + ";");
            }

            if (model.CHARGEFLAG != null)
            {
                scondition.Append(" CHARGEFLAG=" + model.CHARGEFLAG + ";");
            }

            if (model.TESTDEST != null)
            {
                scondition.Append(" TESTDEST=" + model.TESTDEST + ";");
            }

            if (model.SLABLE != null)
            {
                scondition.Append(" SLABLE=" + model.SLABLE + ";");
            }

            if (model.ZDY1 != null)
            {
                scondition.Append(" ZDY1=" + model.ZDY1 + ";");
            }

            if (model.ZDY2 != null)
            {
                scondition.Append(" ZDY2=" + model.ZDY2 + ";");
            }

            if (model.ZDY3 != null)
            {
                scondition.Append(" ZDY3=" + model.ZDY3 + ";");
            }

            if (model.ZDY4 != null)
            {
                scondition.Append(" ZDY4=" + model.ZDY4 + ";");
            }

            if (model.ZDY5 != null)
            {
                scondition.Append(" ZDY5=" + model.ZDY5 + ";");
            }

            if (model.INCEPTDATE != null)
            {
                scondition.Append(" INCEPTDATE=" + model.INCEPTDATE + ";");
            }

            if (model.INCEPTTIME != null)
            {
                scondition.Append(" INCEPTTIME=" + model.INCEPTTIME + ";");
            }

            if (model.INCEPTER != null)
            {
                scondition.Append(" INCEPTER=" + model.INCEPTER + ";");
            }

            if (model.ONLINEDATE != null)
            {
                scondition.Append(" ONLINEDATE=" + model.ONLINEDATE + ";");
            }

            if (model.ONLINETIME != null)
            {
                scondition.Append(" ONLINETIME=" + model.ONLINETIME + ";");
            }

            if (model.BMANNO != null)
            {
                scondition.Append(" BMANNO=" + model.BMANNO + ";");
            }

            if (model.FILETYPE != null)
            {
                scondition.Append(" FILETYPE=" + model.FILETYPE + ";");
            }

            if (model.JPGFILE != null)
            {
                scondition.Append(" JPGFILE=" + model.JPGFILE + ";");
            }

            if (model.PDFFILE != null)
            {
                scondition.Append(" PDFFILE=" + model.PDFFILE + ";");
            }

            if (model.FORMNO != null)
            {
                scondition.Append(" FORMNO=" + model.FORMNO + ";");
            }

            if (model.CHILDTABLENAME != null)
            {
                scondition.Append(" CHILDTABLENAME=" + model.CHILDTABLENAME + ";");
            }

            if (model.PRINTEXEC != null)
            {
                scondition.Append(" PRINTEXEC=" + model.PRINTEXEC + ";");
            }
            if (model.LABCENTER != null)
            {
                scondition.Append(" LABCENTER=" + model.LABCENTER + ";");
            }

            if (model.PRINTTEXEC != null)
            {
                scondition.Append(" PRINTTEXEC=" + model.PRINTTEXEC + ";");
            }

            if (model.Startdate != null)
            {
                scondition.Append(" StartDay=" + model.Startdate.Value.ToShortDateString() + ";");
            }

            if (model.Enddate != null)
            {
                scondition.Append(" EndDay=" + model.Enddate.Value.ToShortDateString() + ";");
            }
            if (scondition.Length > 0)
            {
                return scondition.ToString().Substring(0, scondition.Length - 1);
            }
            else
            {
                return scondition.ToString();
            }
        }
    }
}
