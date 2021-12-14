
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.Entity.WebAssist;
using ZhiFang.DAO.SQL.WebAssist.Lis;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.DAO.SQL.WebAssist
{
    /// <summary>
    /// 数据访问类:MEGroupSampleFormDao_SQL
    /// </summary>
    public partial class MEGroupSampleFormDao_SQL : IDMEGroupSampleFormDao_SQL
    {
        public MEGroupSampleFormDao_SQL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Insert(MEGroupSampleForm model)
        {
            return AddByParameter(model);
        }
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public bool AddBySql(MEGroupSampleForm model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.LabID>-1)
            {
                strSql1.Append("LabID,");
                strSql2.Append("" + model.LabID + ",");
            }
            if (model.Id > -1)
            {
                strSql1.Append("GroupSampleFormID,");
                strSql2.Append("" + model.Id + ",");
            }
            if (model.OrderFormID != null)
            {
                strSql1.Append("OrderFormID,");
                strSql2.Append("" + model.OrderFormID + ",");
            }
            if (model.SampleStatusID != null)
            {
                strSql1.Append("SampleStatusID,");
                strSql2.Append("" + model.SampleStatusID + ",");
            }
            if (model.SectionNo > -2)
            {
                strSql1.Append("SectionNo,");
                strSql2.Append("" + model.SectionNo + ",");
            }
            if (model.SerialNo != null)
            {
                strSql1.Append("SerialNo,");
                strSql2.Append("'" + model.SerialNo + "',");
            }
            if (model.GTestDate != null)
            {
                strSql1.Append("GTestDate,");
                strSql2.Append("'" + model.GTestDate + "',");
            }
            if (model.GSampleNo != null)
            {
                strSql1.Append("GSampleNo,");
                strSql2.Append("'" + model.GSampleNo + "',");
            }
            if (model.GTestNo > -1)
            {
                strSql1.Append("GTestNo,");
                strSql2.Append("" + model.GTestNo + ",");
            }
            if (model.SampleTypeNo > -1)
            {
                strSql1.Append("SampleTypeNo,");
                strSql2.Append("" + model.SampleTypeNo + ",");
            }
            if (model.GSampleInfo != null)
            {
                strSql1.Append("GSampleInfo,");
                strSql2.Append("'" + model.GSampleInfo + "',");
            }
            if (model.TestComment != null)
            {
                strSql1.Append("TestComment,");
                strSql2.Append("'" + model.TestComment + "',");
            }
            if (model.DistributeFlag > -1)
            {
                strSql1.Append("DistributeFlag,");
                strSql2.Append("" + model.DistributeFlag + ",");
            }
            if (model.IsPrint != null)
            {
                strSql1.Append("IsPrint,");
                strSql2.Append("" + model.IsPrint + ",");
            }
            if (model.PrintCount != null)
            {
                strSql1.Append("PrintCount,");
                strSql2.Append("" + model.PrintCount + ",");
            }
            if (model.IsUpload != null)
            {
                strSql1.Append("IsUpload,");
                strSql2.Append("" + model.IsUpload + ",");
            }
            if (model.FFormMemo != null)
            {
                strSql1.Append("F_FormMemo,");
                strSql2.Append("'" + model.FFormMemo + "',");
            }
            if (model.FZDY1 != null)
            {
                strSql1.Append("F_ZDY1,");
                strSql2.Append("'" + model.FZDY1 + "',");
            }
            if (model.FZDY2 != null)
            {
                strSql1.Append("F_ZDY2,");
                strSql2.Append("'" + model.FZDY2 + "',");
            }
            if (model.FZDY3 != null)
            {
                strSql1.Append("F_ZDY3,");
                strSql2.Append("'" + model.FZDY3 + "',");
            }
            if (model.FZDY4 != null)
            {
                strSql1.Append("F_ZDY4,");
                strSql2.Append("'" + model.FZDY4 + "',");
            }
            if (model.FZDY5 != null)
            {
                strSql1.Append("F_ZDY5,");
                strSql2.Append("'" + model.FZDY5 + "',");
            }
            if (model.MainState != null)
            {
                strSql1.Append("MainState,");
                strSql2.Append("" + model.MainState + ",");
            }
            if (model.IsHasNuclearAdmission != null)
            {
                strSql1.Append("IsHasNuclearAdmission,");
                strSql2.Append("" + model.IsHasNuclearAdmission + ",");
            }
            if (model.IsOnMachine != null)
            {
                strSql1.Append("IsOnMachine,");
                strSql2.Append("" + model.IsOnMachine + ",");
            }
            if (model.IsCancelConfirmedOrAudited != null)
            {
                strSql1.Append("IsCancelConfirmedOrAudited,");
                strSql2.Append("" + model.IsCancelConfirmedOrAudited + ",");
            }
            if (model.CommState != null)
            {
                strSql1.Append("CommState,");
                strSql2.Append("" + model.CommState + ",");
            }
            if (model.DataAddTime != null)
            {
                strSql1.Append("DataAddTime,");
                strSql2.Append("'" + model.DataAddTime + "',");
            }
            if (model.DataUpdateTime != null)
            {
                strSql1.Append("DataUpdateTime,");
                strSql2.Append("'" + model.DataUpdateTime + "',");
            }
            if (model.DeleteFlag != null)
            {
                strSql1.Append("DeleteFlag,");
                strSql2.Append("" + (model.DeleteFlag ? 1 : 0) + ",");
            }
            if (model.MigrationFlag != null)
            {
                strSql1.Append("MigrationFlag,");
                strSql2.Append("" + (model.MigrationFlag ? 1 : 0) + ",");
            }
            if (model.PositiveFlag != null)
            {
                strSql1.Append("PositiveFlag,");
                strSql2.Append("" + model.PositiveFlag + ",");
            }
            if (model.ESampleNo != null)
            {
                strSql1.Append("ESampleNo,");
                strSql2.Append("'" + model.ESampleNo + "',");
            }
            if (model.GBarCode != null)
            {
                strSql1.Append("GBarCode,");
                strSql2.Append("'" + model.GBarCode + "',");
            }
            if (model.MainTester != null)
            {
                strSql1.Append("MainTester,");
                strSql2.Append("'" + model.MainTester + "',");
            }
            if (model.MainTesterId != null)
            {
                strSql1.Append("MainTesterId,");
                strSql2.Append("" + model.MainTesterId + ",");
            }
            if (model.OtherTester != null)
            {
                strSql1.Append("OtherTester,");
                strSql2.Append("'" + model.OtherTester + "',");
            }
            if (model.Confirmer != null)
            {
                strSql1.Append("Confirmer,");
                strSql2.Append("'" + model.Confirmer + "',");
            }
            if (model.ConfirmerId != null)
            {
                strSql1.Append("ConfirmerId,");
                strSql2.Append("" + model.ConfirmerId + ",");
            }
            if (model.ConfirmeDate != null)
            {
                strSql1.Append("ConfirmeDate,");
                strSql2.Append("'" + model.ConfirmeDate + "',");
            }
            if (model.Examiner != null)
            {
                strSql1.Append("Examiner,");
                strSql2.Append("'" + model.Examiner + "',");
            }
            if (model.ExaminerId != null)
            {
                strSql1.Append("ExaminerId,");
                strSql2.Append("" + model.ExaminerId + ",");
            }
            if (model.ExamineDate != null)
            {
                strSql1.Append("ExamineDate,");
                strSql2.Append("'" + model.ExamineDate + "',");
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
                strSql2.Append("" + model.Doctor + ",");
            }
            if (model.Diag != null)
            {
                strSql1.Append("Diag,");
                strSql2.Append("'" + model.Diag + "',");
            }
            if (model.Diagno != null)
            {
                strSql1.Append("diagno,");
                strSql2.Append("" + model.Diagno + ",");
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
            if (model.Sickorder != null)
            {
                strSql1.Append("sickorder,");
                strSql2.Append("'" + model.Sickorder + "',");
            }
            if (model.Chargeflag != null)
            {
                strSql1.Append("chargeflag,");
                strSql2.Append("'" + model.Chargeflag + "',");
            }
            if (model.Jztype != null)
            {
                strSql1.Append("jztype,");
                strSql2.Append("" + model.Jztype + ",");
            }
            if (model.FormComment != null)
            {
                strSql1.Append("FormComment,");
                strSql2.Append("'" + model.FormComment + "',");
            }
            if (model.Incepter != null)
            {
                strSql1.Append("incepter,");
                strSql2.Append("'" + model.Incepter + "',");
            }
            if (model.InceptTime != null)
            {
                strSql1.Append("inceptTime,");
                strSql2.Append("'" + model.InceptTime + "',");
            }
            if (model.InceptDate != null)
            {
                strSql1.Append("inceptDate,");
                strSql2.Append("'" + model.InceptDate + "',");
            }
            if (model.TestTypeNo != null)
            {
                strSql1.Append("TestTypeNo,");
                strSql2.Append("" + model.TestTypeNo + ",");
            }
            if (model.CollecterID != null)
            {
                strSql1.Append("CollecterID,");
                strSql2.Append("'" + model.CollecterID + "',");
            }
            if (model.OldSerialNo != null)
            {
                strSql1.Append("OldSerialNo,");
                strSql2.Append("'" + model.OldSerialNo + "',");
            }
            if (model.CountNodesFormSource != null)
            {
                strSql1.Append("CountNodesFormSource,");
                strSql2.Append("'" + model.CountNodesFormSource + "',");
            }
            if (model.Clientno != null)
            {
                strSql1.Append("clientno,");
                strSql2.Append("" + model.Clientno + ",");
            }
            if (model.UrgentState != null)
            {
                strSql1.Append("UrgentState,");
                strSql2.Append("'" + model.UrgentState + "',");
            }
            if (model.DispenseFlag != null)
            {
                strSql1.Append("DispenseFlag,");
                strSql2.Append("" + model.DispenseFlag + ",");
            }
            if (model.Jytype != null)
            {
                strSql1.Append("jytype,");
                strSql2.Append("'" + model.Jytype + "',");
            }
            if (model.NurseSender != null)
            {
                strSql1.Append("NurseSender,");
                strSql2.Append("'" + model.NurseSender + "',");
            }
            if (model.NurseSendTime != null)
            {
                strSql1.Append("NurseSendTime,");
                strSql2.Append("'" + model.NurseSendTime + "',");
            }
            if (model.NurseSendCarrier != null)
            {
                strSql1.Append("NurseSendCarrier,");
                strSql2.Append("'" + model.NurseSendCarrier + "',");
            }
            if (model.NurseSendNo != null)
            {
                strSql1.Append("NurseSendNo,");
                strSql2.Append("'" + model.NurseSendNo + "',");
            }
            if (model.ForeignSendFlag != null)
            {
                strSql1.Append("ForeignSendFlag,");
                strSql2.Append("" + model.ForeignSendFlag + ",");
            }
            if (model.HisDoctorId != null)
            {
                strSql1.Append("HisDoctorId,");
                strSql2.Append("'" + model.HisDoctorId + "',");
            }
            if (model.HisDoctorPhoneCode != null)
            {
                strSql1.Append("HisDoctorPhoneCode,");
                strSql2.Append("'" + model.HisDoctorPhoneCode + "',");
            }
            if (model.LisDoctorNo != null)
            {
                strSql1.Append("LisDoctorNo,");
                strSql2.Append("" + model.LisDoctorNo + ",");
            }
            if (model.PatState != null)
            {
                strSql1.Append("PatState,");
                strSql2.Append("'" + model.PatState + "',");
            }
            if (model.Mergeno != null)
            {
                strSql1.Append("Mergeno,");
                strSql2.Append("'" + model.Mergeno + "',");
            }
            if (model.HospitalizedTimes != null)
            {
                strSql1.Append("hospitalizedTimes,");
                strSql2.Append("" + model.HospitalizedTimes + ",");
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
            if (model.ZDY11 != null)
            {
                strSql1.Append("ZDY11,");
                strSql2.Append("'" + model.ZDY11 + "',");
            }
            if (model.ZDY12 != null)
            {
                strSql1.Append("ZDY12,");
                strSql2.Append("'" + model.ZDY12 + "',");
            }
            if (model.ZDY13 != null)
            {
                strSql1.Append("ZDY13,");
                strSql2.Append("'" + model.ZDY13 + "',");
            }
            if (model.ZDY14 != null)
            {
                strSql1.Append("ZDY14,");
                strSql2.Append("'" + model.ZDY14 + "',");
            }
            if (model.ZDY15 != null)
            {
                strSql1.Append("ZDY15,");
                strSql2.Append("'" + model.ZDY15 + "',");
            }
            if (model.ZDY16 != null)
            {
                strSql1.Append("ZDY16,");
                strSql2.Append("'" + model.ZDY16 + "',");
            }
            if (model.ZDY17 != null)
            {
                strSql1.Append("ZDY17,");
                strSql2.Append("'" + model.ZDY17 + "',");
            }
            if (model.ZDY18 != null)
            {
                strSql1.Append("ZDY18,");
                strSql2.Append("'" + model.ZDY18 + "',");
            }
            if (model.ZDY19 != null)
            {
                strSql1.Append("ZDY19,");
                strSql2.Append("'" + model.ZDY19 + "',");
            }
            if (model.ZDY20 != null)
            {
                strSql1.Append("ZDY20,");
                strSql2.Append("'" + model.ZDY20 + "',");
            }
            if (model.CollectPart != null)
            {
                strSql1.Append("CollectPart,");
                strSql2.Append("'" + model.CollectPart + "',");
            }
            if (model.Signflag != null)
            {
                strSql1.Append("Signflag,");
                strSql2.Append("" + model.Signflag + ",");
            }
            if (model.IDevelop != null)
            {
                strSql1.Append("iDevelop,");
                strSql2.Append("" + model.IDevelop + ",");
            }
            if (model.IWarningTime != null)
            {
                strSql1.Append("iWarningTime,");
                strSql2.Append("" + model.IWarningTime + ",");
            }
            if (model.FlagDateDelete != null)
            {
                strSql1.Append("FlagDateDelete,");
                strSql2.Append("'" + model.FlagDateDelete + "',");
            }
            if (model.GSampleNoForOrder != null)
            {
                strSql1.Append("GSampleNoForOrder,");
                strSql2.Append("'" + model.GSampleNoForOrder + "',");
            }
            if (model.Reexamined != null)
            {
                strSql1.Append("Reexamined,");
                strSql2.Append("" + model.Reexamined + ",");
            }
            if (model.ReportType != null)
            {
                strSql1.Append("ReportType,");
                strSql2.Append("" + model.ReportType + ",");
            }
            if (model.ReceiveTime != null)
            {
                strSql1.Append("ReceiveTime,");
                strSql2.Append("'" + model.ReceiveTime + "',");
            }
            if (model.ZFDelInfo != null)
            {
                strSql1.Append("ZFDelInfo,");
                strSql2.Append("'" + model.ZFDelInfo + "',");
            }
            if (model.ListPrintCount != null)
            {
                strSql1.Append("ListPrintCount,");
                strSql2.Append("" + model.ListPrintCount + ",");
            }
            if (model.GroupSampleFormPID != null)
            {
                strSql1.Append("GroupSampleFormPID,");
                strSql2.Append("" + model.GroupSampleFormPID + ",");
            }
            if (model.IExamineByHand != null)
            {
                strSql1.Append("iExamineByHand,");
                strSql2.Append("" + model.IExamineByHand + ",");
            }
            if (model.FormComment2 != null)
            {
                strSql1.Append("FormComment2,");
                strSql2.Append("'" + model.FormComment2 + "',");
            }
            if (model.SampleSpecialDesc != null)
            {
                strSql1.Append("SampleSpecialDesc,");
                strSql2.Append("'" + model.SampleSpecialDesc + "',");
            }
            if (model.UnionFrom != null)
            {
                strSql1.Append("UnionFrom,");
                strSql2.Append("'" + model.UnionFrom + "',");
            }
            if (model.FormResultInfo != null)
            {
                strSql1.Append("FormResultInfo,");
                strSql2.Append("" + model.FormResultInfo + ",");
            }
            if (model.DataAddMan != null)
            {
                strSql1.Append("DataAddMan,");
                strSql2.Append("'" + model.DataAddMan + "',");
            }
            if (model.ReceiveMan != null)
            {
                strSql1.Append("ReceiveMan,");
                strSql2.Append("'" + model.ReceiveMan + "',");
            }
            if (model.TestTime != null)
            {
                strSql1.Append("TestTime,");
                strSql2.Append("'" + model.TestTime + "',");
            }
            if (model.TestMethod != null)
            {
                strSql1.Append("TestMethod,");
                strSql2.Append("'" + model.TestMethod + "',");
            }
            if (model.TestPurpose != null)
            {
                strSql1.Append("TestPurpose,");
                strSql2.Append("'" + model.TestPurpose + "',");
            }
            if (model.FinalOperater != null)
            {
                strSql1.Append("FinalOperater,");
                strSql2.Append("" + model.FinalOperater + ",");
            }
            if (model.ReportRemark != null)
            {
                strSql1.Append("ReportRemark,");
                strSql2.Append("'" + model.ReportRemark + "',");
            }
            if (model.IsByHand != null)
            {
                strSql1.Append("isByHand,");
                strSql2.Append("" + (model.IsByHand ? 1 : 0) + ",");
            }
            if (model.IsReceive != null)
            {
                strSql1.Append("isReceive,");
                strSql2.Append("" + (model.IsReceive ? 1 : 0) + ",");
            }
            if (model.SampleType2 != null)
            {
                strSql1.Append("SampleType2,");
                strSql2.Append("'" + model.SampleType2 + "',");
            }
            if (model.BCrisis != null)
            {
                strSql1.Append("bCrisis,");
                strSql2.Append("" + model.BCrisis + ",");
            }
            if (model.SumPrintFlag != null)
            {
                strSql1.Append("SumPrintFlag,");
                strSql2.Append("" + model.SumPrintFlag + ",");
            }
            if (model.ExceptFlag != null)
            {
                strSql1.Append("ExceptFlag,");
                strSql2.Append("" + model.ExceptFlag + ",");
            }
            if (model.AgeDesc != null)
            {
                strSql1.Append("AgeDesc,");
                strSql2.Append("'" + model.AgeDesc + "',");
            }
            if (model.AutoPrintCount != null)
            {
                strSql1.Append("AutoPrintCount,");
                strSql2.Append("" + model.AutoPrintCount + ",");
            }
            if (model.IQSPrintCount != null)
            {
                strSql1.Append("IQSPrintCount,");
                strSql2.Append("" + model.IQSPrintCount + ",");
            }
            if (model.AfterExamineFlag != null)
            {
                strSql1.Append("AfterExamineFlag,");
                strSql2.Append("" + model.AfterExamineFlag + ",");
            }
            if (model.AfterConFirmFlag != null)
            {
                strSql1.Append("AfterConFirmFlag,");
                strSql2.Append("" + model.AfterConFirmFlag + ",");
            }
            if (model.Weight != null)
            {
                strSql1.Append("Weight,");
                strSql2.Append("" + model.Weight + ",");
            }
            if (model.WeightDesc != null)
            {
                strSql1.Append("WeightDesc,");
                strSql2.Append("'" + model.WeightDesc + "',");
            }
            if (model.DispenseTime != null)
            {
                strSql1.Append("DispenseTime,");
                strSql2.Append("'" + model.DispenseTime + "',");
            }
            if (model.DispenseUserNo != null)
            {
                strSql1.Append("DispenseUserNo,");
                strSql2.Append("'" + model.DispenseUserNo + "',");
            }
            if (model.DispenseUserName != null)
            {
                strSql1.Append("DispenseUserName,");
                strSql2.Append("'" + model.DispenseUserName + "',");
            }
            if (model.PatNoF != null)
            {
                strSql1.Append("PatNo_F,");
                strSql2.Append("'" + model.PatNoF + "',");
            }
            if (model.FZDY6 != null)
            {
                strSql1.Append("F_ZDY6,");
                strSql2.Append("'" + model.FZDY6 + "',");
            }
            if (model.FZDY7 != null)
            {
                strSql1.Append("F_ZDY7,");
                strSql2.Append("'" + model.FZDY7 + "',");
            }
            if (model.FZDY8 != null)
            {
                strSql1.Append("F_ZDY8,");
                strSql2.Append("'" + model.FZDY8 + "',");
            }
            if (model.FZDY9 != null)
            {
                strSql1.Append("F_ZDY9,");
                strSql2.Append("'" + model.FZDY9 + "',");
            }
            if (model.FZDY10 != null)
            {
                strSql1.Append("F_ZDY10,");
                strSql2.Append("'" + model.FZDY10 + "',");
            }
            if (model.EAchivPosition != null)
            {
                strSql1.Append("EAchivPosition,");
                strSql2.Append("'" + model.EAchivPosition + "',");
            }
            if (model.EPosition != null)
            {
                strSql1.Append("EPosition,");
                strSql2.Append("'" + model.EPosition + "',");
            }
            if (model.OnlineDate != null)
            {
                strSql1.Append("onlineDate,");
                strSql2.Append("'" + model.OnlineDate + "',");
            }
            if (model.ExamineDocID != null)
            {
                strSql1.Append("ExamineDocID,");
                strSql2.Append("'" + model.ExamineDocID + "',");
            }
            if (model.ExamineDoctor != null)
            {
                strSql1.Append("ExamineDoctor,");
                strSql2.Append("'" + model.ExamineDoctor + "',");
            }
            if (model.ExamineDocDate != null)
            {
                strSql1.Append("ExamineDocDate,");
                strSql2.Append("'" + model.ExamineDocDate + "',");
            }
            if (model.ESend != null)
            {
                strSql1.Append("ESend,");
                strSql2.Append("'" + model.ESend + "',");
            }
            if (model.IPositiveCard != null)
            {
                strSql1.Append("iPositiveCard,");
                strSql2.Append("" + model.IPositiveCard + ",");
            }
            if (model.RedoFlag != null)
            {
                strSql1.Append("RedoFlag,");
                strSql2.Append("" + model.RedoFlag + ",");
            }
            if (model.BAllResultTest != null)
            {
                strSql1.Append("bAllResultTest,");
                strSql2.Append("" + model.BAllResultTest + ",");
            }
            if (model.BZFSysCheck != null)
            {
                strSql1.Append("bZFSysCheck,");
                strSql2.Append("" + model.BZFSysCheck + ",");
            }
            if (model.ZFSysCheckInfo != null)
            {
                strSql1.Append("ZFSysCheckInfo,");
                strSql2.Append("'" + model.ZFSysCheckInfo + "',");
            }
            if (model.CheckInfo != null)
            {
                strSql1.Append("CheckInfo,");
                strSql2.Append("'" + model.CheckInfo + "',");
            }
            if (model.CheckInfoExamine != null)
            {
                strSql1.Append("CheckInfoExamine,");
                strSql2.Append("'" + model.CheckInfoExamine + "',");
            }
            if (model.IConfirmByHand != null)
            {
                strSql1.Append("iConfirmByHand,");
                strSql2.Append("" + model.IConfirmByHand + ",");
            }
            if (model.ReportPlaceTxt != null)
            {
                strSql1.Append("ReportPlaceTxt,");
                strSql2.Append("'" + model.ReportPlaceTxt + "',");
            }
            if (model.LastExamineDate != null)
            {
                strSql1.Append("LastExamineDate,");
                strSql2.Append("'" + model.LastExamineDate + "',");
            }
            if (model.CancelExamineDate != null)
            {
                strSql1.Append("CancelExamineDate,");
                strSql2.Append("'" + model.CancelExamineDate + "',");
            }
            if (model.IsCancelScopeAudited != null)
            {
                strSql1.Append("IsCancelScopeAudited,");
                strSql2.Append("" + model.IsCancelScopeAudited + ",");
            }
            if (model.MicroFlag != null)
            {
                strSql1.Append("MicroFlag,");
                strSql2.Append("" + model.MicroFlag + ",");
            }
            if (model.AntiFlag != null)
            {
                strSql1.Append("AntiFlag,");
                strSql2.Append("" + model.AntiFlag + ",");
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
            if (model.OrderTime != null)
            {
                strSql1.Append("OrderTime,");
                strSql2.Append("'" + model.OrderTime + "',");
            }
            if (model.IDCardNo != null)
            {
                strSql1.Append("IDCardNo,");
                strSql2.Append("'" + model.IDCardNo + "',");
            }
            if (model.SampleQuality != null)
            {
                strSql1.Append("SampleQuality,");
                strSql2.Append("'" + model.SampleQuality + "',");
            }
            if (model.FormMemo2 != null)
            {
                strSql1.Append("FormMemo2,");
                strSql2.Append("'" + model.FormMemo2 + "',");
            }
            if (model.IsCopy != null)
            {
                strSql1.Append("IsCopy,");
                strSql2.Append("" + model.IsCopy + ",");
            }
            strSql.Append("insert into ME_GroupSampleForm(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            bool resultBool = DbHelperSQL.ExecuteSql(DbHelperSQL.ConnectionString,strSql.ToString());
            return resultBool;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddByParameter(MEGroupSampleForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ME_GroupSampleForm(");
            strSql.Append("LabID,GroupSampleFormID,OrderFormID,SampleStatusID,SectionNo,SerialNo,GTestDate,GSampleNo,GTestNo,SampleTypeNo,GSampleInfo,TestComment,DistributeFlag,IsPrint,PrintCount,IsUpload,F_FormMemo,F_ZDY1,F_ZDY2,F_ZDY3,F_ZDY4,F_ZDY5,MainState,IsHasNuclearAdmission,IsOnMachine,IsCancelConfirmedOrAudited,CommState,DataAddTime,DataUpdateTime,IsCopy,DeleteFlag,MigrationFlag,PositiveFlag,ESampleNo,GBarCode,MainTester,MainTesterId,OtherTester,Confirmer,ConfirmerId,ConfirmeDate,Examiner,ExaminerId,ExamineDate,PatNo,CName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,Diag,diagno,ChargeNo,Charge,Collecter,CollectDate,CollectTime,FormMemo,sickorder,chargeflag,jztype,FormComment,incepter,inceptTime,inceptDate,TestTypeNo,CollecterID,OldSerialNo,CountNodesFormSource,clientno,UrgentState,DispenseFlag,jytype,NurseSender,NurseSendTime,NurseSendCarrier,NurseSendNo,ForeignSendFlag,HisDoctorId,HisDoctorPhoneCode,LisDoctorNo,PatState,Mergeno,hospitalizedTimes,ZDY1,ZDY2,ZDY3,ZDY4,ZDY5,ZDY6,ZDY7,ZDY8,ZDY9,ZDY10,ZDY11,ZDY12,ZDY13,ZDY14,ZDY15,ZDY16,ZDY17,ZDY18,ZDY19,ZDY20,CollectPart,Signflag,iDevelop,iWarningTime,FlagDateDelete,GSampleNoForOrder,Reexamined,ReportType,ReceiveTime,ZFDelInfo,ListPrintCount,GroupSampleFormPID,iExamineByHand,FormComment2,SampleSpecialDesc,UnionFrom,FormResultInfo,DataAddMan,ReceiveMan,TestTime,TestMethod,TestPurpose,FinalOperater,ReportRemark,isByHand,isReceive,SampleType2,bCrisis,SumPrintFlag,ExceptFlag,AgeDesc,AutoPrintCount,IQSPrintCount,AfterExamineFlag,AfterConFirmFlag,Weight,WeightDesc,DispenseTime,DispenseUserNo,DispenseUserName,PatNo_F,F_ZDY6,F_ZDY7,F_ZDY8,F_ZDY9,F_ZDY10,EAchivPosition,EPosition,onlineDate,ExamineDocID,ExamineDoctor,ExamineDocDate,ESend,iPositiveCard,RedoFlag,bAllResultTest,bZFSysCheck,ZFSysCheckInfo,CheckInfo,CheckInfoExamine,iConfirmByHand,ReportPlaceTxt,LastExamineDate,CancelExamineDate,IsCancelScopeAudited,MicroFlag,AntiFlag,PrintDateTime,PrintOper,OrderTime,IDCardNo,SampleQuality,FormMemo2)");
            strSql.Append(" values (");
            strSql.Append("@LabID,@GroupSampleFormID,@OrderFormID,@SampleStatusID,@SectionNo,@SerialNo,@GTestDate,@GSampleNo,@GTestNo,@SampleTypeNo,@GSampleInfo,@TestComment,@DistributeFlag,@IsPrint,@PrintCount,@IsUpload,@F_FormMemo,@F_ZDY1,@F_ZDY2,@F_ZDY3,@F_ZDY4,@F_ZDY5,@MainState,@IsHasNuclearAdmission,@IsOnMachine,@IsCancelConfirmedOrAudited,@CommState,@DataAddTime,@DataUpdateTime,@IsCopy,@DeleteFlag,@MigrationFlag,@PositiveFlag,@ESampleNo,@GBarCode,@MainTester,@MainTesterId,@OtherTester,@Confirmer,@ConfirmerId,@ConfirmeDate,@Examiner,@ExaminerId,@ExamineDate,@PatNo,@CName,@GenderNo,@Birthday,@Age,@AgeUnitNo,@FolkNo,@DistrictNo,@WardNo,@Bed,@DeptNo,@Doctor,@Diag,@diagno,@ChargeNo,@Charge,@Collecter,@CollectDate,@CollectTime,@FormMemo,@sickorder,@chargeflag,@jztype,@FormComment,@incepter,@inceptTime,@inceptDate,@TestTypeNo,@CollecterID,@OldSerialNo,@CountNodesFormSource,@clientno,@UrgentState,@DispenseFlag,@jytype,@NurseSender,@NurseSendTime,@NurseSendCarrier,@NurseSendNo,@ForeignSendFlag,@HisDoctorId,@HisDoctorPhoneCode,@LisDoctorNo,@PatState,@Mergeno,@hospitalizedTimes,@ZDY1,@ZDY2,@ZDY3,@ZDY4,@ZDY5,@ZDY6,@ZDY7,@ZDY8,@ZDY9,@ZDY10,@ZDY11,@ZDY12,@ZDY13,@ZDY14,@ZDY15,@ZDY16,@ZDY17,@ZDY18,@ZDY19,@ZDY20,@CollectPart,@Signflag,@iDevelop,@iWarningTime,@FlagDateDelete,@GSampleNoForOrder,@Reexamined,@ReportType,@ReceiveTime,@ZFDelInfo,@ListPrintCount,@GroupSampleFormPID,@iExamineByHand,@FormComment2,@SampleSpecialDesc,@UnionFrom,@FormResultInfo,@DataAddMan,@ReceiveMan,@TestTime,@TestMethod,@TestPurpose,@FinalOperater,@ReportRemark,@isByHand,@isReceive,@SampleType2,@bCrisis,@SumPrintFlag,@ExceptFlag,@AgeDesc,@AutoPrintCount,@IQSPrintCount,@AfterExamineFlag,@AfterConFirmFlag,@Weight,@WeightDesc,@DispenseTime,@DispenseUserNo,@DispenseUserName,@PatNo_F,@F_ZDY6,@F_ZDY7,@F_ZDY8,@F_ZDY9,@F_ZDY10,@EAchivPosition,@EPosition,@onlineDate,@ExamineDocID,@ExamineDoctor,@ExamineDocDate,@ESend,@iPositiveCard,@RedoFlag,@bAllResultTest,@bZFSysCheck,@ZFSysCheckInfo,@CheckInfo,@CheckInfoExamine,@iConfirmByHand,@ReportPlaceTxt,@LastExamineDate,@CancelExamineDate,@IsCancelScopeAudited,@MicroFlag,@AntiFlag,@PrintDateTime,@PrintOper,@OrderTime,@IDCardNo,@SampleQuality,@FormMemo2)");
            SqlParameter[] parameters = {
                    new SqlParameter("@LabID", SqlDbType.BigInt,8),
                    new SqlParameter("@GroupSampleFormID", SqlDbType.BigInt,8),
                    new SqlParameter("@OrderFormID", SqlDbType.BigInt,8),
                    new SqlParameter("@SampleStatusID", SqlDbType.BigInt,8),
                    new SqlParameter("@SectionNo", SqlDbType.Int,4),
                    new SqlParameter("@SerialNo", SqlDbType.VarChar,60),
                    new SqlParameter("@GTestDate", SqlDbType.DateTime),
                    new SqlParameter("@GSampleNo", SqlDbType.VarChar,20),
                    new SqlParameter("@GTestNo", SqlDbType.Int,4),
                    new SqlParameter("@SampleTypeNo", SqlDbType.Int,4),
                    new SqlParameter("@GSampleInfo", SqlDbType.VarChar,50),
                    new SqlParameter("@TestComment", SqlDbType.Text),
                    new SqlParameter("@DistributeFlag", SqlDbType.Int,4),
                    new SqlParameter("@IsPrint", SqlDbType.Int,4),
                    new SqlParameter("@PrintCount", SqlDbType.Int,4),
                    new SqlParameter("@IsUpload", SqlDbType.Int,4),
                    new SqlParameter("@F_FormMemo", SqlDbType.NText),
                    new SqlParameter("@F_ZDY1", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY2", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY3", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY4", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY5", SqlDbType.VarChar,200),
                    new SqlParameter("@MainState", SqlDbType.Int,4),
                    new SqlParameter("@IsHasNuclearAdmission", SqlDbType.Int,4),
                    new SqlParameter("@IsOnMachine", SqlDbType.Int,4),
                    new SqlParameter("@IsCancelConfirmedOrAudited", SqlDbType.Int,4),
                    new SqlParameter("@CommState", SqlDbType.Int,4),
                    new SqlParameter("@DataAddTime", SqlDbType.DateTime),
                    new SqlParameter("@DataUpdateTime", SqlDbType.DateTime),
                    //new SqlParameter("@DataTimeStamp", SqlDbType.Timestamp,8),
                    new SqlParameter("@IsCopy", SqlDbType.Int,4),

                    new SqlParameter("@DeleteFlag", SqlDbType.Bit,1),
                    new SqlParameter("@MigrationFlag", SqlDbType.Bit,1),
                    new SqlParameter("@PositiveFlag", SqlDbType.Int,4),
                    new SqlParameter("@ESampleNo", SqlDbType.VarChar,20),
                    new SqlParameter("@GBarCode", SqlDbType.VarChar,20),
                    new SqlParameter("@MainTester", SqlDbType.VarChar,200),
                    new SqlParameter("@MainTesterId", SqlDbType.BigInt,8),
                    new SqlParameter("@OtherTester", SqlDbType.VarChar,200),
                    new SqlParameter("@Confirmer", SqlDbType.VarChar,200),
                    new SqlParameter("@ConfirmerId", SqlDbType.BigInt,8),
                    new SqlParameter("@ConfirmeDate", SqlDbType.DateTime),
                    new SqlParameter("@Examiner", SqlDbType.VarChar,200),
                    new SqlParameter("@ExaminerId", SqlDbType.BigInt,8),
                    new SqlParameter("@ExamineDate", SqlDbType.DateTime),
                    new SqlParameter("@PatNo", SqlDbType.VarChar,20),
                    new SqlParameter("@CName", SqlDbType.VarChar,40),
                    new SqlParameter("@GenderNo", SqlDbType.Int,4),
                    new SqlParameter("@Birthday", SqlDbType.DateTime),
                    new SqlParameter("@Age", SqlDbType.Float,8),
                    new SqlParameter("@AgeUnitNo", SqlDbType.Int,4),
                    new SqlParameter("@FolkNo", SqlDbType.Int,4),
                    new SqlParameter("@DistrictNo", SqlDbType.Int,4),
                    new SqlParameter("@WardNo", SqlDbType.Int,4),
                    new SqlParameter("@Bed", SqlDbType.VarChar,10),
                    new SqlParameter("@DeptNo", SqlDbType.Int,4),
                    new SqlParameter("@Doctor", SqlDbType.Int,4),
                    new SqlParameter("@Diag", SqlDbType.VarChar,200),
                    new SqlParameter("@diagno", SqlDbType.Int,4),
                    new SqlParameter("@ChargeNo", SqlDbType.Int,4),
                    new SqlParameter("@Charge", SqlDbType.Money,8),
                    new SqlParameter("@Collecter", SqlDbType.VarChar,10),
                    new SqlParameter("@CollectDate", SqlDbType.DateTime),
                    new SqlParameter("@CollectTime", SqlDbType.DateTime),
                    new SqlParameter("@FormMemo", SqlDbType.VarChar,400),
                    new SqlParameter("@sickorder", SqlDbType.VarChar,20),
                    new SqlParameter("@chargeflag", SqlDbType.VarChar,10),
                    new SqlParameter("@jztype", SqlDbType.Int,4),
                    new SqlParameter("@FormComment", SqlDbType.Text),
                    new SqlParameter("@incepter", SqlDbType.VarChar,20),
                    new SqlParameter("@inceptTime", SqlDbType.DateTime),
                    new SqlParameter("@inceptDate", SqlDbType.DateTime),
                    new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
                    new SqlParameter("@CollecterID", SqlDbType.VarChar,20),
                    new SqlParameter("@OldSerialNo", SqlDbType.VarChar,60),
                    new SqlParameter("@CountNodesFormSource", SqlDbType.Char,1),
                    new SqlParameter("@clientno", SqlDbType.Int,4),
                    new SqlParameter("@UrgentState", SqlDbType.VarChar,20),
                    new SqlParameter("@DispenseFlag", SqlDbType.Int,4),
                    new SqlParameter("@jytype", SqlDbType.VarChar,50),
                    new SqlParameter("@NurseSender", SqlDbType.VarChar,20),
                    new SqlParameter("@NurseSendTime", SqlDbType.DateTime),
                    new SqlParameter("@NurseSendCarrier", SqlDbType.VarChar,20),
                    new SqlParameter("@NurseSendNo", SqlDbType.VarChar,20),
                    new SqlParameter("@ForeignSendFlag", SqlDbType.Int,4),
                    new SqlParameter("@HisDoctorId", SqlDbType.VarChar,20),
                    new SqlParameter("@HisDoctorPhoneCode", SqlDbType.VarChar,20),
                    new SqlParameter("@LisDoctorNo", SqlDbType.Int,4),
                    new SqlParameter("@PatState", SqlDbType.VarChar,50),
                    new SqlParameter("@Mergeno", SqlDbType.Char,20),
                    new SqlParameter("@hospitalizedTimes", SqlDbType.Int,4),
                    new SqlParameter("@ZDY1", SqlDbType.VarChar,100),
                    new SqlParameter("@ZDY2", SqlDbType.VarChar,100),
                    new SqlParameter("@ZDY3", SqlDbType.VarChar,100),
                    new SqlParameter("@ZDY4", SqlDbType.VarChar,100),
                    new SqlParameter("@ZDY5", SqlDbType.VarChar,200),
                    new SqlParameter("@ZDY6", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY7", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY8", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY9", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY10", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY11", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY12", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY13", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY14", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY15", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY16", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY17", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY18", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY19", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY20", SqlDbType.VarChar,50),
                    new SqlParameter("@CollectPart", SqlDbType.VarChar,200),
                    new SqlParameter("@Signflag", SqlDbType.Int,4),
                    new SqlParameter("@iDevelop", SqlDbType.Int,4),
                    new SqlParameter("@iWarningTime", SqlDbType.Int,4),
                    new SqlParameter("@FlagDateDelete", SqlDbType.DateTime),
                    new SqlParameter("@GSampleNoForOrder", SqlDbType.VarChar,50),
                    new SqlParameter("@Reexamined", SqlDbType.Int,4),
                    new SqlParameter("@ReportType", SqlDbType.Int,4),
                    new SqlParameter("@ReceiveTime", SqlDbType.DateTime),
                    new SqlParameter("@ZFDelInfo", SqlDbType.VarChar,20),
                    new SqlParameter("@ListPrintCount", SqlDbType.Int,4),
                    new SqlParameter("@GroupSampleFormPID", SqlDbType.BigInt,8),
                    new SqlParameter("@iExamineByHand", SqlDbType.Int,4),
                    new SqlParameter("@FormComment2", SqlDbType.Text),
                    new SqlParameter("@SampleSpecialDesc", SqlDbType.VarChar,50),
                    new SqlParameter("@UnionFrom", SqlDbType.VarChar,50),
                    new SqlParameter("@FormResultInfo", SqlDbType.Image),
                    new SqlParameter("@DataAddMan", SqlDbType.VarChar,30),
                    new SqlParameter("@ReceiveMan", SqlDbType.VarChar,30),
                    new SqlParameter("@TestTime", SqlDbType.DateTime),
                    new SqlParameter("@TestMethod", SqlDbType.VarChar,1000),
                    new SqlParameter("@TestPurpose", SqlDbType.VarChar,1000),
                    new SqlParameter("@FinalOperater", SqlDbType.BigInt,8),
                    new SqlParameter("@ReportRemark", SqlDbType.VarChar,500),
                    new SqlParameter("@isByHand", SqlDbType.Bit,1),
                    new SqlParameter("@isReceive", SqlDbType.Bit,1),
                    new SqlParameter("@SampleType2", SqlDbType.VarChar,100),
                    new SqlParameter("@bCrisis", SqlDbType.Int,4),
                    new SqlParameter("@SumPrintFlag", SqlDbType.Int,4),
                    new SqlParameter("@ExceptFlag", SqlDbType.Int,4),
                    new SqlParameter("@AgeDesc", SqlDbType.VarChar,20),
                    new SqlParameter("@AutoPrintCount", SqlDbType.Int,4),
                    new SqlParameter("@IQSPrintCount", SqlDbType.Int,4),
                    new SqlParameter("@AfterExamineFlag", SqlDbType.Int,4),
                    new SqlParameter("@AfterConFirmFlag", SqlDbType.Int,4),
                    new SqlParameter("@Weight", SqlDbType.Float,8),
                    new SqlParameter("@WeightDesc", SqlDbType.VarChar,20),
                    new SqlParameter("@DispenseTime", SqlDbType.DateTime),
                    new SqlParameter("@DispenseUserNo", SqlDbType.VarChar,30),
                    new SqlParameter("@DispenseUserName", SqlDbType.VarChar,30),
                    new SqlParameter("@PatNo_F", SqlDbType.VarChar,20),
                    new SqlParameter("@F_ZDY6", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY7", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY8", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY9", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY10", SqlDbType.VarChar,200),
                    new SqlParameter("@EAchivPosition", SqlDbType.VarChar,50),
                    new SqlParameter("@EPosition", SqlDbType.VarChar,20),
                    new SqlParameter("@onlineDate", SqlDbType.DateTime),
                    new SqlParameter("@ExamineDocID", SqlDbType.VarChar,30),
                    new SqlParameter("@ExamineDoctor", SqlDbType.VarChar,30),
                    new SqlParameter("@ExamineDocDate", SqlDbType.DateTime),
                    new SqlParameter("@ESend", SqlDbType.VarChar,1000),
                    new SqlParameter("@iPositiveCard", SqlDbType.Int,4),
                    new SqlParameter("@RedoFlag", SqlDbType.Int,4),
                    new SqlParameter("@bAllResultTest", SqlDbType.Int,4),
                    new SqlParameter("@bZFSysCheck", SqlDbType.Int,4),
                    new SqlParameter("@ZFSysCheckInfo", SqlDbType.VarChar,1000),
                    new SqlParameter("@CheckInfo", SqlDbType.VarChar,300),
                    new SqlParameter("@CheckInfoExamine", SqlDbType.VarChar,300),
                    new SqlParameter("@iConfirmByHand", SqlDbType.Int,4),
                    new SqlParameter("@ReportPlaceTxt", SqlDbType.VarChar,200),
                    new SqlParameter("@LastExamineDate", SqlDbType.DateTime),
                    new SqlParameter("@CancelExamineDate", SqlDbType.DateTime),
                    new SqlParameter("@IsCancelScopeAudited", SqlDbType.Int,4),
                    new SqlParameter("@MicroFlag", SqlDbType.Int,4),
                    new SqlParameter("@AntiFlag", SqlDbType.Int,4),
                    new SqlParameter("@PrintDateTime", SqlDbType.DateTime),
                    new SqlParameter("@PrintOper", SqlDbType.VarChar,20),
                    new SqlParameter("@OrderTime", SqlDbType.DateTime),
                    new SqlParameter("@IDCardNo", SqlDbType.VarChar,20),
                    new SqlParameter("@SampleQuality", SqlDbType.VarChar,50),
                    new SqlParameter("@FormMemo2", SqlDbType.Text)
                   };

            #region parameters
            parameters[0].Value = model.LabID;
            parameters[1].Value = model.Id;
            parameters[2].Value = model.OrderFormID;
            parameters[3].Value = model.SampleStatusID;
            parameters[4].Value = model.SectionNo;
            parameters[5].Value = model.SerialNo;
            parameters[6].Value = model.GTestDate;
            parameters[7].Value = model.GSampleNo;
            parameters[8].Value = model.GTestNo;
            parameters[9].Value = model.SampleTypeNo;
            parameters[10].Value = model.GSampleInfo;
            parameters[11].Value = model.TestComment;
            parameters[12].Value = model.DistributeFlag;
            parameters[13].Value = model.IsPrint;
            parameters[14].Value = model.PrintCount;
            parameters[15].Value = model.IsUpload;
            parameters[16].Value = model.FFormMemo;
            parameters[17].Value = model.FZDY1;
            parameters[18].Value = model.FZDY2;
            parameters[19].Value = model.FZDY3;
            parameters[20].Value = model.FZDY4;
            parameters[21].Value = model.FZDY5;
            parameters[22].Value = model.MainState;
            parameters[23].Value = model.IsHasNuclearAdmission;
            parameters[24].Value = model.IsOnMachine;
            parameters[25].Value = model.IsCancelConfirmedOrAudited;
            parameters[26].Value = model.CommState;
            parameters[27].Value = model.DataAddTime;
            parameters[28].Value = model.DataUpdateTime;
            parameters[29].Value = model.IsCopy;

            parameters[30].Value = model.DeleteFlag;
            parameters[31].Value = model.MigrationFlag;
            parameters[32].Value = model.PositiveFlag;
            parameters[33].Value = model.ESampleNo;
            parameters[34].Value = model.GBarCode;
            parameters[35].Value = model.MainTester;
            parameters[36].Value = model.MainTesterId;
            parameters[37].Value = model.OtherTester;
            parameters[38].Value = model.Confirmer;
            parameters[39].Value = model.ConfirmerId;
            parameters[40].Value = model.ConfirmeDate;
            parameters[41].Value = model.Examiner;
            parameters[42].Value = model.ExaminerId;
            parameters[43].Value = model.ExamineDate;
            parameters[44].Value = model.PatNo;
            parameters[45].Value = model.CName;
            parameters[46].Value = model.GenderNo;
            parameters[47].Value = model.Birthday;
            parameters[48].Value = model.Age;
            parameters[49].Value = model.AgeUnitNo;
            parameters[50].Value = model.FolkNo;
            parameters[51].Value = model.DistrictNo;
            parameters[52].Value = model.WardNo;
            parameters[53].Value = model.Bed;
            parameters[54].Value = model.DeptNo;
            parameters[55].Value = model.Doctor;
            parameters[56].Value = model.Diag;
            parameters[57].Value = model.Diagno;
            parameters[58].Value = model.ChargeNo;
            parameters[59].Value = model.Charge;
            parameters[60].Value = model.Collecter;
            parameters[61].Value = model.CollectDate;
            parameters[62].Value = model.CollectTime;
            parameters[63].Value = model.FormMemo;
            parameters[64].Value = model.Sickorder;
            parameters[65].Value = model.Chargeflag;
            parameters[66].Value = model.Jztype;
            parameters[67].Value = model.FormComment;
            parameters[68].Value = model.Incepter;
            parameters[69].Value = model.InceptTime;
            parameters[70].Value = model.InceptDate;
            parameters[71].Value = model.TestTypeNo;
            parameters[72].Value = model.CollecterID;
            parameters[73].Value = model.OldSerialNo;
            parameters[74].Value = model.CountNodesFormSource;
            parameters[75].Value = model.Clientno;
            parameters[76].Value = model.UrgentState;
            parameters[77].Value = model.DispenseFlag;
            parameters[78].Value = model.Jytype;
            parameters[79].Value = model.NurseSender;
            parameters[80].Value = model.NurseSendTime;
            parameters[81].Value = model.NurseSendCarrier;
            parameters[82].Value = model.NurseSendNo;
            parameters[83].Value = model.ForeignSendFlag;
            parameters[84].Value = model.HisDoctorId;
            parameters[85].Value = model.HisDoctorPhoneCode;
            parameters[86].Value = model.LisDoctorNo;
            parameters[87].Value = model.PatState;
            parameters[88].Value = model.Mergeno;
            parameters[89].Value = model.HospitalizedTimes;
            parameters[90].Value = model.ZDY1;
            parameters[91].Value = model.ZDY2;
            parameters[92].Value = model.ZDY3;
            parameters[93].Value = model.ZDY4;
            parameters[94].Value = model.ZDY5;
            parameters[95].Value = model.ZDY6;
            parameters[96].Value = model.ZDY7;
            parameters[97].Value = model.ZDY8;
            parameters[98].Value = model.ZDY9;
            parameters[99].Value = model.ZDY10;
            parameters[100].Value = model.ZDY11;
            parameters[101].Value = model.ZDY12;
            parameters[102].Value = model.ZDY13;
            parameters[103].Value = model.ZDY14;
            parameters[104].Value = model.ZDY15;
            parameters[105].Value = model.ZDY16;
            parameters[106].Value = model.ZDY17;
            parameters[107].Value = model.ZDY18;
            parameters[108].Value = model.ZDY19;
            parameters[109].Value = model.ZDY20;
            parameters[110].Value = model.CollectPart;
            parameters[111].Value = model.Signflag;
            parameters[112].Value = model.IDevelop;
            parameters[113].Value = model.IWarningTime;
            parameters[114].Value = model.FlagDateDelete;
            parameters[115].Value = model.GSampleNoForOrder;
            parameters[116].Value = model.Reexamined;
            parameters[117].Value = model.ReportType;
            parameters[118].Value = model.ReceiveTime;
            parameters[119].Value = model.ZFDelInfo;
            parameters[120].Value = model.ListPrintCount;
            parameters[121].Value = model.GroupSampleFormPID;
            parameters[122].Value = model.IExamineByHand;
            parameters[123].Value = model.FormComment2;
            parameters[124].Value = model.SampleSpecialDesc;
            parameters[125].Value = model.UnionFrom;
            parameters[126].Value = model.FormResultInfo;
            parameters[127].Value = model.DataAddMan;
            parameters[128].Value = model.ReceiveMan;
            parameters[129].Value = model.TestTime;
            parameters[130].Value = model.TestMethod;
            parameters[131].Value = model.TestPurpose;
            parameters[132].Value = model.FinalOperater;
            parameters[133].Value = model.ReportRemark;
            parameters[134].Value = model.IsByHand;
            parameters[135].Value = model.IsReceive;
            parameters[136].Value = model.SampleType2;
            parameters[137].Value = model.BCrisis;
            parameters[138].Value = model.SumPrintFlag;
            parameters[139].Value = model.ExceptFlag;
            parameters[140].Value = model.AgeDesc;
            parameters[141].Value = model.AutoPrintCount;
            parameters[142].Value = model.IQSPrintCount;
            parameters[143].Value = model.AfterExamineFlag;
            parameters[144].Value = model.AfterConFirmFlag;
            parameters[145].Value = model.Weight;
            parameters[146].Value = model.WeightDesc;
            parameters[147].Value = model.DispenseTime;
            parameters[148].Value = model.DispenseUserNo;
            parameters[149].Value = model.DispenseUserName;
            parameters[150].Value = model.PatNoF;
            parameters[151].Value = model.FZDY6;
            parameters[152].Value = model.FZDY7;
            parameters[153].Value = model.FZDY8;
            parameters[154].Value = model.FZDY9;
            parameters[155].Value = model.FZDY10;
            parameters[156].Value = model.EAchivPosition;
            parameters[157].Value = model.EPosition;
            parameters[158].Value = model.OnlineDate;
            parameters[159].Value = model.ExamineDocID;
            parameters[160].Value = model.ExamineDoctor;
            parameters[161].Value = model.ExamineDocDate;
            parameters[162].Value = model.ESend;
            parameters[163].Value = model.IPositiveCard;
            parameters[164].Value = model.RedoFlag;
            parameters[165].Value = model.BAllResultTest;
            parameters[166].Value = model.BZFSysCheck;
            parameters[167].Value = model.ZFSysCheckInfo;
            parameters[168].Value = model.CheckInfo;
            parameters[169].Value = model.CheckInfoExamine;
            parameters[170].Value = model.IConfirmByHand;
            parameters[171].Value = model.ReportPlaceTxt;
            parameters[172].Value = model.LastExamineDate;
            parameters[173].Value = model.CancelExamineDate;
            parameters[174].Value = model.IsCancelScopeAudited;
            parameters[175].Value = model.MicroFlag;
            parameters[176].Value = model.AntiFlag;
            parameters[177].Value = model.PrintDateTime;
            parameters[178].Value = model.PrintOper;
            parameters[179].Value = model.OrderTime;
            parameters[180].Value = model.IDCardNo;
            parameters[181].Value = model.SampleQuality;
            parameters[182].Value = model.FormMemo2;
           
            #endregion

            int rows = 0;
            rows = DbHelperSQL.ExecuteSql(DbHelperSQL.ConnectionString, strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(MEGroupSampleForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ME_GroupSampleForm set ");
            strSql.Append("LabID=@LabID,");
            strSql.Append("OrderFormID=@OrderFormID,");
            strSql.Append("SampleStatusID=@SampleStatusID,");
            strSql.Append("GTestNo=@GTestNo,");
            strSql.Append("SampleTypeNo=@SampleTypeNo,");
            strSql.Append("GSampleInfo=@GSampleInfo,");
            strSql.Append("TestComment=@TestComment,");
            strSql.Append("DistributeFlag=@DistributeFlag,");
            strSql.Append("IsPrint=@IsPrint,");
            strSql.Append("PrintCount=@PrintCount,");
            strSql.Append("IsUpload=@IsUpload,");
            strSql.Append("F_FormMemo=@F_FormMemo,");
            strSql.Append("F_ZDY1=@F_ZDY1,");
            strSql.Append("F_ZDY2=@F_ZDY2,");
            strSql.Append("F_ZDY3=@F_ZDY3,");
            strSql.Append("F_ZDY4=@F_ZDY4,");
            strSql.Append("F_ZDY5=@F_ZDY5,");
            strSql.Append("IsHasNuclearAdmission=@IsHasNuclearAdmission,");
            strSql.Append("IsOnMachine=@IsOnMachine,");
            strSql.Append("IsCancelConfirmedOrAudited=@IsCancelConfirmedOrAudited,");
            strSql.Append("CommState=@CommState,");
            strSql.Append("DataAddTime=@DataAddTime,");
            strSql.Append("DataUpdateTime=@DataUpdateTime,");
            strSql.Append("DeleteFlag=@DeleteFlag,");
            strSql.Append("MigrationFlag=@MigrationFlag,");
            strSql.Append("PositiveFlag=@PositiveFlag,");
            strSql.Append("ESampleNo=@ESampleNo,");
            strSql.Append("GBarCode=@GBarCode,");
            strSql.Append("MainTester=@MainTester,");
            strSql.Append("MainTesterId=@MainTesterId,");
            strSql.Append("OtherTester=@OtherTester,");
            strSql.Append("Confirmer=@Confirmer,");
            strSql.Append("ConfirmerId=@ConfirmerId,");
            strSql.Append("ConfirmeDate=@ConfirmeDate,");
            strSql.Append("Examiner=@Examiner,");
            strSql.Append("ExaminerId=@ExaminerId,");
            strSql.Append("ExamineDate=@ExamineDate,");
            strSql.Append("GenderNo=@GenderNo,");
            strSql.Append("Birthday=@Birthday,");
            strSql.Append("Age=@Age,");
            strSql.Append("AgeUnitNo=@AgeUnitNo,");
            strSql.Append("FolkNo=@FolkNo,");
            strSql.Append("DistrictNo=@DistrictNo,");
            strSql.Append("WardNo=@WardNo,");
            strSql.Append("Bed=@Bed,");
            strSql.Append("Doctor=@Doctor,");
            strSql.Append("Diag=@Diag,");
            strSql.Append("diagno=@diagno,");
            strSql.Append("ChargeNo=@ChargeNo,");
            strSql.Append("Charge=@Charge,");
            strSql.Append("Collecter=@Collecter,");
            strSql.Append("CollectDate=@CollectDate,");
            strSql.Append("CollectTime=@CollectTime,");
            strSql.Append("FormMemo=@FormMemo,");
            strSql.Append("sickorder=@sickorder,");
            strSql.Append("chargeflag=@chargeflag,");
            strSql.Append("FormComment=@FormComment,");
            strSql.Append("incepter=@incepter,");
            strSql.Append("inceptTime=@inceptTime,");
            strSql.Append("inceptDate=@inceptDate,");
            strSql.Append("TestTypeNo=@TestTypeNo,");
            strSql.Append("CollecterID=@CollecterID,");
            strSql.Append("OldSerialNo=@OldSerialNo,");
            strSql.Append("CountNodesFormSource=@CountNodesFormSource,");
            strSql.Append("clientno=@clientno,");
            strSql.Append("UrgentState=@UrgentState,");
            strSql.Append("DispenseFlag=@DispenseFlag,");
            strSql.Append("jytype=@jytype,");
            strSql.Append("NurseSender=@NurseSender,");
            strSql.Append("NurseSendTime=@NurseSendTime,");
            strSql.Append("NurseSendCarrier=@NurseSendCarrier,");
            strSql.Append("NurseSendNo=@NurseSendNo,");
            strSql.Append("ForeignSendFlag=@ForeignSendFlag,");
            strSql.Append("HisDoctorId=@HisDoctorId,");
            strSql.Append("HisDoctorPhoneCode=@HisDoctorPhoneCode,");
            strSql.Append("LisDoctorNo=@LisDoctorNo,");
            strSql.Append("PatState=@PatState,");
            strSql.Append("Mergeno=@Mergeno,");
            strSql.Append("hospitalizedTimes=@hospitalizedTimes,");
            strSql.Append("ZDY3=@ZDY3,");
            strSql.Append("ZDY4=@ZDY4,");
            strSql.Append("ZDY5=@ZDY5,");
            strSql.Append("ZDY6=@ZDY6,");
            strSql.Append("ZDY7=@ZDY7,");
            strSql.Append("ZDY8=@ZDY8,");
            strSql.Append("ZDY9=@ZDY9,");
            strSql.Append("ZDY10=@ZDY10,");
            strSql.Append("ZDY11=@ZDY11,");
            strSql.Append("ZDY12=@ZDY12,");
            strSql.Append("ZDY13=@ZDY13,");
            strSql.Append("ZDY14=@ZDY14,");
            strSql.Append("ZDY15=@ZDY15,");
            strSql.Append("ZDY16=@ZDY16,");
            strSql.Append("ZDY17=@ZDY17,");
            strSql.Append("ZDY18=@ZDY18,");
            strSql.Append("ZDY19=@ZDY19,");
            strSql.Append("ZDY20=@ZDY20,");
            strSql.Append("CollectPart=@CollectPart,");
            strSql.Append("Signflag=@Signflag,");
            strSql.Append("iDevelop=@iDevelop,");
            strSql.Append("iWarningTime=@iWarningTime,");
            strSql.Append("GSampleNoForOrder=@GSampleNoForOrder,");
            strSql.Append("Reexamined=@Reexamined,");
            strSql.Append("ReportType=@ReportType,");
            strSql.Append("ReceiveTime=@ReceiveTime,");
            strSql.Append("ZFDelInfo=@ZFDelInfo,");
            strSql.Append("ListPrintCount=@ListPrintCount,");
            strSql.Append("GroupSampleFormPID=@GroupSampleFormPID,");
            strSql.Append("iExamineByHand=@iExamineByHand,");
            strSql.Append("FormComment2=@FormComment2,");
            strSql.Append("SampleSpecialDesc=@SampleSpecialDesc,");
            strSql.Append("UnionFrom=@UnionFrom,");
            strSql.Append("FormResultInfo=@FormResultInfo,");
            strSql.Append("DataAddMan=@DataAddMan,");
            strSql.Append("ReceiveMan=@ReceiveMan,");
            strSql.Append("TestTime=@TestTime,");
            strSql.Append("TestMethod=@TestMethod,");
            strSql.Append("TestPurpose=@TestPurpose,");
            strSql.Append("FinalOperater=@FinalOperater,");
            strSql.Append("ReportRemark=@ReportRemark,");
            strSql.Append("isByHand=@isByHand,");
            strSql.Append("isReceive=@isReceive,");
            strSql.Append("SampleType2=@SampleType2,");
            strSql.Append("bCrisis=@bCrisis,");
            strSql.Append("SumPrintFlag=@SumPrintFlag,");
            strSql.Append("ExceptFlag=@ExceptFlag,");
            strSql.Append("AgeDesc=@AgeDesc,");
            strSql.Append("AutoPrintCount=@AutoPrintCount,");
            strSql.Append("IQSPrintCount=@IQSPrintCount,");
            strSql.Append("AfterExamineFlag=@AfterExamineFlag,");
            strSql.Append("AfterConFirmFlag=@AfterConFirmFlag,");
            strSql.Append("Weight=@Weight,");
            strSql.Append("WeightDesc=@WeightDesc,");
            strSql.Append("DispenseTime=@DispenseTime,");
            strSql.Append("DispenseUserNo=@DispenseUserNo,");
            strSql.Append("DispenseUserName=@DispenseUserName,");
            strSql.Append("PatNo_F=@PatNo_F,");
            strSql.Append("F_ZDY6=@F_ZDY6,");
            strSql.Append("F_ZDY7=@F_ZDY7,");
            strSql.Append("F_ZDY8=@F_ZDY8,");
            strSql.Append("F_ZDY9=@F_ZDY9,");
            strSql.Append("F_ZDY10=@F_ZDY10,");
            strSql.Append("EAchivPosition=@EAchivPosition,");
            strSql.Append("EPosition=@EPosition,");
            strSql.Append("onlineDate=@onlineDate,");
            strSql.Append("ExamineDocID=@ExamineDocID,");
            strSql.Append("ExamineDoctor=@ExamineDoctor,");
            strSql.Append("ExamineDocDate=@ExamineDocDate,");
            strSql.Append("ESend=@ESend,");
            strSql.Append("iPositiveCard=@iPositiveCard,");
            strSql.Append("RedoFlag=@RedoFlag,");
            strSql.Append("bAllResultTest=@bAllResultTest,");
            strSql.Append("bZFSysCheck=@bZFSysCheck,");
            strSql.Append("ZFSysCheckInfo=@ZFSysCheckInfo,");
            strSql.Append("CheckInfo=@CheckInfo,");
            strSql.Append("CheckInfoExamine=@CheckInfoExamine,");
            strSql.Append("iConfirmByHand=@iConfirmByHand,");
            strSql.Append("ReportPlaceTxt=@ReportPlaceTxt,");
            strSql.Append("LastExamineDate=@LastExamineDate,");
            strSql.Append("CancelExamineDate=@CancelExamineDate,");
            strSql.Append("IsCancelScopeAudited=@IsCancelScopeAudited,");
            strSql.Append("MicroFlag=@MicroFlag,");
            strSql.Append("AntiFlag=@AntiFlag,");
            strSql.Append("PrintDateTime=@PrintDateTime,");
            strSql.Append("PrintOper=@PrintOper,");
            strSql.Append("OrderTime=@OrderTime,");
            strSql.Append("IDCardNo=@IDCardNo,");
            strSql.Append("SampleQuality=@SampleQuality,");
            strSql.Append("FormMemo2=@FormMemo2,");
            strSql.Append("IsCopy=@IsCopy");
            strSql.Append(" where GroupSampleFormID=@GroupSampleFormID and SectionNo=@SectionNo and SerialNo=@SerialNo and GTestDate=@GTestDate and GSampleNo=@GSampleNo and MainState=@MainState and PatNo=@PatNo and CName=@CName and DeptNo=@DeptNo and jztype=@jztype and ZDY1=@ZDY1 and ZDY2=@ZDY2 and FlagDateDelete=@FlagDateDelete ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LabID", SqlDbType.BigInt,8),
                    new SqlParameter("@OrderFormID", SqlDbType.BigInt,8),
                    new SqlParameter("@SampleStatusID", SqlDbType.BigInt,8),
                    new SqlParameter("@GTestNo", SqlDbType.Int,4),
                    new SqlParameter("@SampleTypeNo", SqlDbType.Int,4),
                    new SqlParameter("@GSampleInfo", SqlDbType.VarChar,50),
                    new SqlParameter("@TestComment", SqlDbType.Text),
                    new SqlParameter("@DistributeFlag", SqlDbType.Int,4),
                    new SqlParameter("@IsPrint", SqlDbType.Int,4),
                    new SqlParameter("@PrintCount", SqlDbType.Int,4),
                    new SqlParameter("@IsUpload", SqlDbType.Int,4),
                    new SqlParameter("@F_FormMemo", SqlDbType.NText),
                    new SqlParameter("@F_ZDY1", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY2", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY3", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY4", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY5", SqlDbType.VarChar,200),
                    new SqlParameter("@IsHasNuclearAdmission", SqlDbType.Int,4),
                    new SqlParameter("@IsOnMachine", SqlDbType.Int,4),
                    new SqlParameter("@IsCancelConfirmedOrAudited", SqlDbType.Int,4),
                    new SqlParameter("@CommState", SqlDbType.Int,4),
                    new SqlParameter("@DataAddTime", SqlDbType.DateTime),
                    new SqlParameter("@DataUpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteFlag", SqlDbType.Bit,1),
                    new SqlParameter("@MigrationFlag", SqlDbType.Bit,1),
                    new SqlParameter("@PositiveFlag", SqlDbType.Int,4),
                    new SqlParameter("@ESampleNo", SqlDbType.VarChar,20),
                    new SqlParameter("@GBarCode", SqlDbType.VarChar,20),
                    new SqlParameter("@MainTester", SqlDbType.VarChar,200),
                    new SqlParameter("@MainTesterId", SqlDbType.BigInt,8),
                    new SqlParameter("@OtherTester", SqlDbType.VarChar,200),
                    new SqlParameter("@Confirmer", SqlDbType.VarChar,200),
                    new SqlParameter("@ConfirmerId", SqlDbType.BigInt,8),
                    new SqlParameter("@ConfirmeDate", SqlDbType.DateTime),
                    new SqlParameter("@Examiner", SqlDbType.VarChar,200),
                    new SqlParameter("@ExaminerId", SqlDbType.BigInt,8),
                    new SqlParameter("@ExamineDate", SqlDbType.DateTime),
                    new SqlParameter("@GenderNo", SqlDbType.Int,4),
                    new SqlParameter("@Birthday", SqlDbType.DateTime),
                    new SqlParameter("@Age", SqlDbType.Float,8),
                    new SqlParameter("@AgeUnitNo", SqlDbType.Int,4),
                    new SqlParameter("@FolkNo", SqlDbType.Int,4),
                    new SqlParameter("@DistrictNo", SqlDbType.Int,4),
                    new SqlParameter("@WardNo", SqlDbType.Int,4),
                    new SqlParameter("@Bed", SqlDbType.VarChar,10),
                    new SqlParameter("@Doctor", SqlDbType.Int,4),
                    new SqlParameter("@Diag", SqlDbType.VarChar,200),
                    new SqlParameter("@diagno", SqlDbType.Int,4),
                    new SqlParameter("@ChargeNo", SqlDbType.Int,4),
                    new SqlParameter("@Charge", SqlDbType.Money,8),
                    new SqlParameter("@Collecter", SqlDbType.VarChar,10),
                    new SqlParameter("@CollectDate", SqlDbType.DateTime),
                    new SqlParameter("@CollectTime", SqlDbType.DateTime),
                    new SqlParameter("@FormMemo", SqlDbType.VarChar,400),
                    new SqlParameter("@sickorder", SqlDbType.VarChar,20),
                    new SqlParameter("@chargeflag", SqlDbType.VarChar,10),
                    new SqlParameter("@FormComment", SqlDbType.Text),
                    new SqlParameter("@incepter", SqlDbType.VarChar,20),
                    new SqlParameter("@inceptTime", SqlDbType.DateTime),
                    new SqlParameter("@inceptDate", SqlDbType.DateTime),
                    new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
                    new SqlParameter("@CollecterID", SqlDbType.VarChar,20),
                    new SqlParameter("@OldSerialNo", SqlDbType.VarChar,60),
                    new SqlParameter("@CountNodesFormSource", SqlDbType.Char,1),
                    new SqlParameter("@clientno", SqlDbType.Int,4),
                    new SqlParameter("@UrgentState", SqlDbType.VarChar,20),
                    new SqlParameter("@DispenseFlag", SqlDbType.Int,4),
                    new SqlParameter("@jytype", SqlDbType.VarChar,50),
                    new SqlParameter("@NurseSender", SqlDbType.VarChar,20),
                    new SqlParameter("@NurseSendTime", SqlDbType.DateTime),
                    new SqlParameter("@NurseSendCarrier", SqlDbType.VarChar,20),
                    new SqlParameter("@NurseSendNo", SqlDbType.VarChar,20),
                    new SqlParameter("@ForeignSendFlag", SqlDbType.Int,4),
                    new SqlParameter("@HisDoctorId", SqlDbType.VarChar,20),
                    new SqlParameter("@HisDoctorPhoneCode", SqlDbType.VarChar,20),
                    new SqlParameter("@LisDoctorNo", SqlDbType.Int,4),
                    new SqlParameter("@PatState", SqlDbType.VarChar,50),
                    new SqlParameter("@Mergeno", SqlDbType.Char,20),
                    new SqlParameter("@hospitalizedTimes", SqlDbType.Int,4),
                    new SqlParameter("@ZDY3", SqlDbType.VarChar,100),
                    new SqlParameter("@ZDY4", SqlDbType.VarChar,100),
                    new SqlParameter("@ZDY5", SqlDbType.VarChar,200),
                    new SqlParameter("@ZDY6", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY7", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY8", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY9", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY10", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY11", SqlDbType.VarChar,60),
                    new SqlParameter("@ZDY12", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY13", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY14", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY15", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY16", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY17", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY18", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY19", SqlDbType.VarChar,50),
                    new SqlParameter("@ZDY20", SqlDbType.VarChar,50),
                    new SqlParameter("@CollectPart", SqlDbType.VarChar,200),
                    new SqlParameter("@Signflag", SqlDbType.Int,4),
                    new SqlParameter("@iDevelop", SqlDbType.Int,4),
                    new SqlParameter("@iWarningTime", SqlDbType.Int,4),
                    new SqlParameter("@GSampleNoForOrder", SqlDbType.VarChar,50),
                    new SqlParameter("@Reexamined", SqlDbType.Int,4),
                    new SqlParameter("@ReportType", SqlDbType.Int,4),
                    new SqlParameter("@ReceiveTime", SqlDbType.DateTime),
                    new SqlParameter("@ZFDelInfo", SqlDbType.VarChar,20),
                    new SqlParameter("@ListPrintCount", SqlDbType.Int,4),
                    new SqlParameter("@GroupSampleFormPID", SqlDbType.BigInt,8),
                    new SqlParameter("@iExamineByHand", SqlDbType.Int,4),
                    new SqlParameter("@FormComment2", SqlDbType.Text),
                    new SqlParameter("@SampleSpecialDesc", SqlDbType.VarChar,50),
                    new SqlParameter("@UnionFrom", SqlDbType.VarChar,50),
                    new SqlParameter("@FormResultInfo", SqlDbType.Image),
                    new SqlParameter("@DataAddMan", SqlDbType.VarChar,30),
                    new SqlParameter("@ReceiveMan", SqlDbType.VarChar,30),
                    new SqlParameter("@TestTime", SqlDbType.DateTime),
                    new SqlParameter("@TestMethod", SqlDbType.VarChar,1000),
                    new SqlParameter("@TestPurpose", SqlDbType.VarChar,1000),
                    new SqlParameter("@FinalOperater", SqlDbType.BigInt,8),
                    new SqlParameter("@ReportRemark", SqlDbType.VarChar,500),
                    new SqlParameter("@isByHand", SqlDbType.Bit,1),
                    new SqlParameter("@isReceive", SqlDbType.Bit,1),
                    new SqlParameter("@SampleType2", SqlDbType.VarChar,100),
                    new SqlParameter("@bCrisis", SqlDbType.Int,4),
                    new SqlParameter("@SumPrintFlag", SqlDbType.Int,4),
                    new SqlParameter("@ExceptFlag", SqlDbType.Int,4),
                    new SqlParameter("@AgeDesc", SqlDbType.VarChar,20),
                    new SqlParameter("@AutoPrintCount", SqlDbType.Int,4),
                    new SqlParameter("@IQSPrintCount", SqlDbType.Int,4),
                    new SqlParameter("@AfterExamineFlag", SqlDbType.Int,4),
                    new SqlParameter("@AfterConFirmFlag", SqlDbType.Int,4),
                    new SqlParameter("@Weight", SqlDbType.Float,8),
                    new SqlParameter("@WeightDesc", SqlDbType.VarChar,20),
                    new SqlParameter("@DispenseTime", SqlDbType.DateTime),
                    new SqlParameter("@DispenseUserNo", SqlDbType.VarChar,30),
                    new SqlParameter("@DispenseUserName", SqlDbType.VarChar,30),
                    new SqlParameter("@PatNo_F", SqlDbType.VarChar,20),
                    new SqlParameter("@F_ZDY6", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY7", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY8", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY9", SqlDbType.VarChar,200),
                    new SqlParameter("@F_ZDY10", SqlDbType.VarChar,200),
                    new SqlParameter("@EAchivPosition", SqlDbType.VarChar,50),
                    new SqlParameter("@EPosition", SqlDbType.VarChar,20),
                    new SqlParameter("@onlineDate", SqlDbType.DateTime),
                    new SqlParameter("@ExamineDocID", SqlDbType.VarChar,30),
                    new SqlParameter("@ExamineDoctor", SqlDbType.VarChar,30),
                    new SqlParameter("@ExamineDocDate", SqlDbType.DateTime),
                    new SqlParameter("@ESend", SqlDbType.VarChar,1000),
                    new SqlParameter("@iPositiveCard", SqlDbType.Int,4),
                    new SqlParameter("@RedoFlag", SqlDbType.Int,4),
                    new SqlParameter("@bAllResultTest", SqlDbType.Int,4),
                    new SqlParameter("@bZFSysCheck", SqlDbType.Int,4),
                    new SqlParameter("@ZFSysCheckInfo", SqlDbType.VarChar,1000),
                    new SqlParameter("@CheckInfo", SqlDbType.VarChar,300),
                    new SqlParameter("@CheckInfoExamine", SqlDbType.VarChar,300),
                    new SqlParameter("@iConfirmByHand", SqlDbType.Int,4),
                    new SqlParameter("@ReportPlaceTxt", SqlDbType.VarChar,200),
                    new SqlParameter("@LastExamineDate", SqlDbType.DateTime),
                    new SqlParameter("@CancelExamineDate", SqlDbType.DateTime),
                    new SqlParameter("@IsCancelScopeAudited", SqlDbType.Int,4),
                    new SqlParameter("@MicroFlag", SqlDbType.Int,4),
                    new SqlParameter("@AntiFlag", SqlDbType.Int,4),
                    new SqlParameter("@PrintDateTime", SqlDbType.DateTime),
                    new SqlParameter("@PrintOper", SqlDbType.VarChar,20),
                    new SqlParameter("@OrderTime", SqlDbType.DateTime),
                    new SqlParameter("@IDCardNo", SqlDbType.VarChar,20),
                    new SqlParameter("@SampleQuality", SqlDbType.VarChar,50),
                    new SqlParameter("@FormMemo2", SqlDbType.Text),
                    new SqlParameter("@IsCopy", SqlDbType.Int,4),
                    new SqlParameter("@GroupSampleFormID", SqlDbType.BigInt,8),
                    new SqlParameter("@SectionNo", SqlDbType.Int,4),
                    new SqlParameter("@SerialNo", SqlDbType.VarChar,60),
                    new SqlParameter("@GTestDate", SqlDbType.DateTime),
                    new SqlParameter("@GSampleNo", SqlDbType.VarChar,20),
                    new SqlParameter("@MainState", SqlDbType.Int,4),
                    new SqlParameter("@PatNo", SqlDbType.VarChar,20),
                    new SqlParameter("@CName", SqlDbType.VarChar,40),
                    new SqlParameter("@DeptNo", SqlDbType.Int,4),
                    new SqlParameter("@jztype", SqlDbType.Int,4),
                    new SqlParameter("@ZDY1", SqlDbType.VarChar,100),
                    new SqlParameter("@ZDY2", SqlDbType.VarChar,100),
                    new SqlParameter("@FlagDateDelete", SqlDbType.DateTime)};
            parameters[0].Value = model.LabID;
            parameters[1].Value = model.OrderFormID;
            parameters[2].Value = model.SampleStatusID;
            parameters[3].Value = model.GTestNo;
            parameters[4].Value = model.SampleTypeNo;
            parameters[5].Value = model.GSampleInfo;
            parameters[6].Value = model.TestComment;
            parameters[7].Value = model.DistributeFlag;
            parameters[8].Value = model.IsPrint;
            parameters[9].Value = model.PrintCount;
            parameters[10].Value = model.IsUpload;
            parameters[11].Value = model.FFormMemo;
            parameters[12].Value = model.FZDY1;
            parameters[13].Value = model.FZDY2;
            parameters[14].Value = model.FZDY3;
            parameters[15].Value = model.FZDY4;
            parameters[16].Value = model.FZDY5;
            parameters[17].Value = model.IsHasNuclearAdmission;
            parameters[18].Value = model.IsOnMachine;
            parameters[19].Value = model.IsCancelConfirmedOrAudited;
            parameters[20].Value = model.CommState;
            parameters[21].Value = model.DataAddTime;
            parameters[22].Value = model.DataUpdateTime;
            parameters[23].Value = model.DeleteFlag;
            parameters[24].Value = model.MigrationFlag;
            parameters[25].Value = model.PositiveFlag;
            parameters[26].Value = model.ESampleNo;
            parameters[27].Value = model.GBarCode;
            parameters[28].Value = model.MainTester;
            parameters[29].Value = model.MainTesterId;
            parameters[30].Value = model.OtherTester;
            parameters[31].Value = model.Confirmer;
            parameters[32].Value = model.ConfirmerId;
            parameters[33].Value = model.ConfirmeDate;
            parameters[34].Value = model.Examiner;
            parameters[35].Value = model.ExaminerId;
            parameters[36].Value = model.ExamineDate;
            parameters[37].Value = model.GenderNo;
            parameters[38].Value = model.Birthday;
            parameters[39].Value = model.Age;
            parameters[40].Value = model.AgeUnitNo;
            parameters[41].Value = model.FolkNo;
            parameters[42].Value = model.DistrictNo;
            parameters[43].Value = model.WardNo;
            parameters[44].Value = model.Bed;
            parameters[45].Value = model.Doctor;
            parameters[46].Value = model.Diag;
            parameters[47].Value = model.Diagno;
            parameters[48].Value = model.ChargeNo;
            parameters[49].Value = model.Charge;
            parameters[50].Value = model.Collecter;
            parameters[51].Value = model.CollectDate;
            parameters[52].Value = model.CollectTime;
            parameters[53].Value = model.FormMemo;
            parameters[54].Value = model.Sickorder;
            parameters[55].Value = model.Chargeflag;
            parameters[56].Value = model.FormComment;
            parameters[57].Value = model.Incepter;
            parameters[58].Value = model.InceptTime;
            parameters[59].Value = model.InceptDate;
            parameters[60].Value = model.TestTypeNo;
            parameters[61].Value = model.CollecterID;
            parameters[62].Value = model.OldSerialNo;
            parameters[63].Value = model.CountNodesFormSource;
            parameters[64].Value = model.Clientno;
            parameters[65].Value = model.UrgentState;
            parameters[66].Value = model.DispenseFlag;
            parameters[67].Value = model.Jytype;
            parameters[68].Value = model.NurseSender;
            parameters[69].Value = model.NurseSendTime;
            parameters[70].Value = model.NurseSendCarrier;
            parameters[71].Value = model.NurseSendNo;
            parameters[72].Value = model.ForeignSendFlag;
            parameters[73].Value = model.HisDoctorId;
            parameters[74].Value = model.HisDoctorPhoneCode;
            parameters[75].Value = model.LisDoctorNo;
            parameters[76].Value = model.PatState;
            parameters[77].Value = model.Mergeno;
            parameters[78].Value = model.HospitalizedTimes;
            parameters[79].Value = model.ZDY3;
            parameters[80].Value = model.ZDY4;
            parameters[81].Value = model.ZDY5;
            parameters[82].Value = model.ZDY6;
            parameters[83].Value = model.ZDY7;
            parameters[84].Value = model.ZDY8;
            parameters[85].Value = model.ZDY9;
            parameters[86].Value = model.ZDY10;
            parameters[87].Value = model.ZDY11;
            parameters[88].Value = model.ZDY12;
            parameters[89].Value = model.ZDY13;
            parameters[90].Value = model.ZDY14;
            parameters[91].Value = model.ZDY15;
            parameters[92].Value = model.ZDY16;
            parameters[93].Value = model.ZDY17;
            parameters[94].Value = model.ZDY18;
            parameters[95].Value = model.ZDY19;
            parameters[96].Value = model.ZDY20;
            parameters[97].Value = model.CollectPart;
            parameters[98].Value = model.Signflag;
            parameters[99].Value = model.IDevelop;
            parameters[100].Value = model.IWarningTime;
            parameters[101].Value = model.GSampleNoForOrder;
            parameters[102].Value = model.Reexamined;
            parameters[103].Value = model.ReportType;
            parameters[104].Value = model.ReceiveTime;
            parameters[105].Value = model.ZFDelInfo;
            parameters[106].Value = model.ListPrintCount;
            parameters[107].Value = model.GroupSampleFormPID;
            parameters[108].Value = model.IExamineByHand;
            parameters[109].Value = model.FormComment2;
            parameters[110].Value = model.SampleSpecialDesc;
            parameters[111].Value = model.UnionFrom;
            parameters[112].Value = model.FormResultInfo;
            parameters[113].Value = model.DataAddMan;
            parameters[114].Value = model.ReceiveMan;
            parameters[115].Value = model.TestTime;
            parameters[116].Value = model.TestMethod;
            parameters[117].Value = model.TestPurpose;
            parameters[118].Value = model.FinalOperater;
            parameters[119].Value = model.ReportRemark;
            parameters[120].Value = model.IsByHand;
            parameters[121].Value = model.IsReceive;
            parameters[122].Value = model.SampleType2;
            parameters[123].Value = model.BCrisis;
            parameters[124].Value = model.SumPrintFlag;
            parameters[125].Value = model.ExceptFlag;
            parameters[126].Value = model.AgeDesc;
            parameters[127].Value = model.AutoPrintCount;
            parameters[128].Value = model.IQSPrintCount;
            parameters[129].Value = model.AfterExamineFlag;
            parameters[130].Value = model.AfterConFirmFlag;
            parameters[131].Value = model.Weight;
            parameters[132].Value = model.WeightDesc;
            parameters[133].Value = model.DispenseTime;
            parameters[134].Value = model.DispenseUserNo;
            parameters[135].Value = model.DispenseUserName;
            parameters[136].Value = model.PatNoF;
            parameters[137].Value = model.FZDY6;
            parameters[138].Value = model.FZDY7;
            parameters[139].Value = model.FZDY8;
            parameters[140].Value = model.FZDY9;
            parameters[141].Value = model.FZDY10;
            parameters[142].Value = model.EAchivPosition;
            parameters[143].Value = model.EPosition;
            parameters[144].Value = model.OnlineDate;
            parameters[145].Value = model.ExamineDocID;
            parameters[146].Value = model.ExamineDoctor;
            parameters[147].Value = model.ExamineDocDate;
            parameters[148].Value = model.ESend;
            parameters[149].Value = model.IPositiveCard;
            parameters[150].Value = model.RedoFlag;
            parameters[151].Value = model.BAllResultTest;
            parameters[152].Value = model.BZFSysCheck;
            parameters[153].Value = model.ZFSysCheckInfo;
            parameters[154].Value = model.CheckInfo;
            parameters[155].Value = model.CheckInfoExamine;
            parameters[156].Value = model.IConfirmByHand;
            parameters[157].Value = model.ReportPlaceTxt;
            parameters[158].Value = model.LastExamineDate;
            parameters[159].Value = model.CancelExamineDate;
            parameters[160].Value = model.IsCancelScopeAudited;
            parameters[161].Value = model.MicroFlag;
            parameters[162].Value = model.AntiFlag;
            parameters[163].Value = model.PrintDateTime;
            parameters[164].Value = model.PrintOper;
            parameters[165].Value = model.OrderTime;
            parameters[166].Value = model.IDCardNo;
            parameters[167].Value = model.SampleQuality;
            parameters[168].Value = model.FormMemo2;
            parameters[169].Value = model.IsCopy;
            parameters[170].Value = model.Id;
            parameters[171].Value = model.SectionNo;
            parameters[172].Value = model.SerialNo;
            parameters[173].Value = model.GTestDate;
            parameters[174].Value = model.GSampleNo;
            parameters[175].Value = model.MainState;
            parameters[176].Value = model.PatNo;
            parameters[177].Value = model.CName;
            parameters[178].Value = model.DeptNo;
            parameters[179].Value = model.Jztype;
            parameters[180].Value = model.ZDY1;
            parameters[181].Value = model.ZDY2;
            parameters[182].Value = model.FlagDateDelete;

            int rows = 0;
            //rows =DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion  ExtensionMethod
    }
}

