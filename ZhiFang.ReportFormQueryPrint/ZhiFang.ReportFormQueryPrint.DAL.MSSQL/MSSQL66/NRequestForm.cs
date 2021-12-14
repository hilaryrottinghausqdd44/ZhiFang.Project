using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;

//Please add references
namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// 数据访问类:NRequestForm
	/// </summary>
	public partial class NRequestForm:IDNRequestForm
	{
        DbHelperSQLObj DbHelperSQL = new DbHelperSQLObj(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));
        public NRequestForm()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SerialNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from NRequestForm");
			strSql.Append(" where SerialNo=@SerialNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@SerialNo", SqlDbType.VarChar,40)};
			parameters[0].Value = SerialNo;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.NRequestForm model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into NRequestForm(");
			strSql.Append("SerialNo,ReceiveFlag,StatusNo,SampleTypeNo,PatNo,CName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,DiagNo,ChargeNo,Charge,CollecterID,Collecter,CollectDate,CollectTime,Operator,OperDate,OperTime,FormMemo,RequestSource,Artificerorder,sickorder,chargeflag,jztype,zdy1,zdy2,zdy3,zdy4,zdy5,FlagDateDelete,FormComment,nurseflag,diag,CaseNo,refuseopinion,refusereason,signintime,signer,signflag,SamplingGroupNo,PrintCount,PrintInfo,SampleCap,IsPrep,IsAffirm,IsSampling,IsSend,incepter,inceptTime,inceptDate,isByHand,AssignFlag,OldSerialNo,TestTypeNo,DispenseFlag,refuseUser,refuseTime,jytype,SerialScanTime_old,IsCheckFee,Dr2Flag,ExecDeptNo,ClientHost,PreNumber,UrgentState,ZDY6,ZDY7,ZDY8,ZDY9,ZDY10,phoneCode,IsNode,PhoneNodeCount,AutoNodeCount,clientno,SerialScanTime,CountNodesFormSource,StateFlag,AffirmTime,IsNurseDo,NurseSender,NurseSendTime,NurseSendCarrier,CollectCount,ForeignSendFlag,HisAffirm,PatPhoto,ChargeOrderNo,ReportFlag)");
			strSql.Append(" values (");
			strSql.Append("@SerialNo,@ReceiveFlag,@StatusNo,@SampleTypeNo,@PatNo,@CName,@GenderNo,@Birthday,@Age,@AgeUnitNo,@FolkNo,@DistrictNo,@WardNo,@Bed,@DeptNo,@Doctor,@DiagNo,@ChargeNo,@Charge,@CollecterID,@Collecter,@CollectDate,@CollectTime,@Operator,@OperDate,@OperTime,@FormMemo,@RequestSource,@Artificerorder,@sickorder,@chargeflag,@jztype,@zdy1,@zdy2,@zdy3,@zdy4,@zdy5,@FlagDateDelete,@FormComment,@nurseflag,@diag,@CaseNo,@refuseopinion,@refusereason,@signintime,@signer,@signflag,@SamplingGroupNo,@PrintCount,@PrintInfo,@SampleCap,@IsPrep,@IsAffirm,@IsSampling,@IsSend,@incepter,@inceptTime,@inceptDate,@isByHand,@AssignFlag,@OldSerialNo,@TestTypeNo,@DispenseFlag,@refuseUser,@refuseTime,@jytype,@SerialScanTime_old,@IsCheckFee,@Dr2Flag,@ExecDeptNo,@ClientHost,@PreNumber,@UrgentState,@ZDY6,@ZDY7,@ZDY8,@ZDY9,@ZDY10,@phoneCode,@IsNode,@PhoneNodeCount,@AutoNodeCount,@clientno,@SerialScanTime,@CountNodesFormSource,@StateFlag,@AffirmTime,@IsNurseDo,@NurseSender,@NurseSendTime,@NurseSendCarrier,@CollectCount,@ForeignSendFlag,@HisAffirm,@PatPhoto,@ChargeOrderNo,@ReportFlag)");
			SqlParameter[] parameters = {
					new SqlParameter("@SerialNo", SqlDbType.VarChar,40),
					new SqlParameter("@ReceiveFlag", SqlDbType.Int,4),
					new SqlParameter("@StatusNo", SqlDbType.Int,4),
					new SqlParameter("@SampleTypeNo", SqlDbType.Int,4),
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
					new SqlParameter("@DiagNo", SqlDbType.Int,4),
					new SqlParameter("@ChargeNo", SqlDbType.Int,4),
					new SqlParameter("@Charge", SqlDbType.Money,8),
					new SqlParameter("@CollecterID", SqlDbType.VarChar,20),
					new SqlParameter("@Collecter", SqlDbType.VarChar,10),
					new SqlParameter("@CollectDate", SqlDbType.DateTime),
					new SqlParameter("@CollectTime", SqlDbType.DateTime),
					new SqlParameter("@Operator", SqlDbType.VarChar,10),
					new SqlParameter("@OperDate", SqlDbType.DateTime),
					new SqlParameter("@OperTime", SqlDbType.DateTime),
					new SqlParameter("@FormMemo", SqlDbType.VarChar,40),
					new SqlParameter("@RequestSource", SqlDbType.VarChar,20),
					new SqlParameter("@Artificerorder", SqlDbType.VarChar,20),
					new SqlParameter("@sickorder", SqlDbType.VarChar,20),
					new SqlParameter("@chargeflag", SqlDbType.VarChar,10),
					new SqlParameter("@jztype", SqlDbType.Int,4),
					new SqlParameter("@zdy1", SqlDbType.VarChar,50),
					new SqlParameter("@zdy2", SqlDbType.VarChar,200),
					new SqlParameter("@zdy3", SqlDbType.VarChar,50),
					new SqlParameter("@zdy4", SqlDbType.VarChar,50),
					new SqlParameter("@zdy5", SqlDbType.VarChar,50),
					new SqlParameter("@FlagDateDelete", SqlDbType.DateTime),
					new SqlParameter("@FormComment", SqlDbType.Text),
					new SqlParameter("@nurseflag", SqlDbType.Char,10),
					new SqlParameter("@diag", SqlDbType.VarChar,100),
					new SqlParameter("@CaseNo", SqlDbType.VarChar,20),
					new SqlParameter("@refuseopinion", SqlDbType.VarChar,100),
					new SqlParameter("@refusereason", SqlDbType.VarChar,100),
					new SqlParameter("@signintime", SqlDbType.DateTime),
					new SqlParameter("@signer", SqlDbType.VarChar,20),
					new SqlParameter("@signflag", SqlDbType.Int,4),
					new SqlParameter("@SamplingGroupNo", SqlDbType.Int,4),
					new SqlParameter("@PrintCount", SqlDbType.Int,4),
					new SqlParameter("@PrintInfo", SqlDbType.VarChar,600),
					new SqlParameter("@SampleCap", SqlDbType.Float,8),
					new SqlParameter("@IsPrep", SqlDbType.Int,4),
					new SqlParameter("@IsAffirm", SqlDbType.Int,4),
					new SqlParameter("@IsSampling", SqlDbType.Int,4),
					new SqlParameter("@IsSend", SqlDbType.Int,4),
					new SqlParameter("@incepter", SqlDbType.VarChar,20),
					new SqlParameter("@inceptTime", SqlDbType.DateTime),
					new SqlParameter("@inceptDate", SqlDbType.DateTime),
					new SqlParameter("@isByHand", SqlDbType.Bit,1),
					new SqlParameter("@AssignFlag", SqlDbType.Int,4),
					new SqlParameter("@OldSerialNo", SqlDbType.VarChar,40),
					new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
					new SqlParameter("@DispenseFlag", SqlDbType.Int,4),
					new SqlParameter("@refuseUser", SqlDbType.VarChar,20),
					new SqlParameter("@refuseTime", SqlDbType.DateTime),
					new SqlParameter("@jytype", SqlDbType.VarChar,50),
					new SqlParameter("@SerialScanTime_old", SqlDbType.VarChar,30),
					new SqlParameter("@IsCheckFee", SqlDbType.Int,4),
					new SqlParameter("@Dr2Flag", SqlDbType.Int,4),
					new SqlParameter("@ExecDeptNo", SqlDbType.Int,4),
					new SqlParameter("@ClientHost", SqlDbType.VarChar,60),
					new SqlParameter("@PreNumber", SqlDbType.Int,4),
					new SqlParameter("@UrgentState", SqlDbType.VarChar,20),
					new SqlParameter("@ZDY6", SqlDbType.VarChar,60),
					new SqlParameter("@ZDY7", SqlDbType.VarChar,60),
					new SqlParameter("@ZDY8", SqlDbType.VarChar,60),
					new SqlParameter("@ZDY9", SqlDbType.VarChar,60),
					new SqlParameter("@ZDY10", SqlDbType.VarChar,60),
					new SqlParameter("@phoneCode", SqlDbType.VarChar,20),
					new SqlParameter("@IsNode", SqlDbType.Int,4),
					new SqlParameter("@PhoneNodeCount", SqlDbType.Int,4),
					new SqlParameter("@AutoNodeCount", SqlDbType.Int,4),
					new SqlParameter("@clientno", SqlDbType.Int,4),
					new SqlParameter("@SerialScanTime", SqlDbType.DateTime),
					new SqlParameter("@CountNodesFormSource", SqlDbType.Char,1),
					new SqlParameter("@StateFlag", SqlDbType.Int,4),
					new SqlParameter("@AffirmTime", SqlDbType.DateTime),
					new SqlParameter("@IsNurseDo", SqlDbType.Int,4),
					new SqlParameter("@NurseSender", SqlDbType.VarChar,20),
					new SqlParameter("@NurseSendTime", SqlDbType.DateTime),
					new SqlParameter("@NurseSendCarrier", SqlDbType.VarChar,20),
					new SqlParameter("@CollectCount", SqlDbType.Int,4),
					new SqlParameter("@ForeignSendFlag", SqlDbType.Int,4),
					new SqlParameter("@HisAffirm", SqlDbType.Int,4),
					new SqlParameter("@PatPhoto", SqlDbType.Image),
					new SqlParameter("@ChargeOrderNo", SqlDbType.VarChar,20),
					new SqlParameter("@ReportFlag", SqlDbType.Int,4)};
			parameters[0].Value = model.SerialNo;
			parameters[1].Value = model.ReceiveFlag;
			parameters[2].Value = model.StatusNo;
			parameters[3].Value = model.SampleTypeNo;
			parameters[4].Value = model.PatNo;
			parameters[5].Value = model.CName;
			parameters[6].Value = model.GenderNo;
			parameters[7].Value = model.Birthday;
			parameters[8].Value = model.Age;
			parameters[9].Value = model.AgeUnitNo;
			parameters[10].Value = model.FolkNo;
			parameters[11].Value = model.DistrictNo;
			parameters[12].Value = model.WardNo;
			parameters[13].Value = model.Bed;
			parameters[14].Value = model.DeptNo;
			parameters[15].Value = model.Doctor;
			parameters[16].Value = model.DiagNo;
			parameters[17].Value = model.ChargeNo;
			parameters[18].Value = model.Charge;
			parameters[19].Value = model.CollecterID;
			parameters[20].Value = model.Collecter;
			parameters[21].Value = model.CollectDate;
			parameters[22].Value = model.CollectTime;
			parameters[23].Value = model.Operator;
			parameters[24].Value = model.OperDate;
			parameters[25].Value = model.OperTime;
			parameters[26].Value = model.FormMemo;
			parameters[27].Value = model.RequestSource;
			parameters[28].Value = model.Artificerorder;
			parameters[29].Value = model.sickorder;
			parameters[30].Value = model.chargeflag;
			parameters[31].Value = model.jztype;
			parameters[32].Value = model.zdy1;
			parameters[33].Value = model.zdy2;
			parameters[34].Value = model.zdy3;
			parameters[35].Value = model.zdy4;
			parameters[36].Value = model.zdy5;
			parameters[37].Value = model.FlagDateDelete;
			parameters[38].Value = model.FormComment;
			parameters[39].Value = model.nurseflag;
			parameters[40].Value = model.diag;
			parameters[41].Value = model.CaseNo;
			parameters[42].Value = model.refuseopinion;
			parameters[43].Value = model.refusereason;
			parameters[44].Value = model.signintime;
			parameters[45].Value = model.signer;
			parameters[46].Value = model.signflag;
			parameters[47].Value = model.SamplingGroupNo;
			parameters[48].Value = model.PrintCount;
			parameters[49].Value = model.PrintInfo;
			parameters[50].Value = model.SampleCap;
			parameters[51].Value = model.IsPrep;
			parameters[52].Value = model.IsAffirm;
			parameters[53].Value = model.IsSampling;
			parameters[54].Value = model.IsSend;
			parameters[55].Value = model.incepter;
			parameters[56].Value = model.inceptTime;
			parameters[57].Value = model.inceptDate;
			parameters[58].Value = model.isByHand;
			parameters[59].Value = model.AssignFlag;
			parameters[60].Value = model.OldSerialNo;
			parameters[61].Value = model.TestTypeNo;
			parameters[62].Value = model.DispenseFlag;
			parameters[63].Value = model.refuseUser;
			parameters[64].Value = model.refuseTime;
			parameters[65].Value = model.jytype;
			parameters[66].Value = model.SerialScanTime_old;
			parameters[67].Value = model.IsCheckFee;
			parameters[68].Value = model.Dr2Flag;
			parameters[69].Value = model.ExecDeptNo;
			parameters[70].Value = model.ClientHost;
			parameters[71].Value = model.PreNumber;
			parameters[72].Value = model.UrgentState;
			parameters[73].Value = model.ZDY6;
			parameters[74].Value = model.ZDY7;
			parameters[75].Value = model.ZDY8;
			parameters[76].Value = model.ZDY9;
			parameters[77].Value = model.ZDY10;
			parameters[78].Value = model.phoneCode;
			parameters[79].Value = model.IsNode;
			parameters[80].Value = model.PhoneNodeCount;
			parameters[81].Value = model.AutoNodeCount;
			parameters[82].Value = model.clientno;
			parameters[83].Value = model.SerialScanTime;
			parameters[84].Value = model.CountNodesFormSource;
			parameters[85].Value = model.StateFlag;
			parameters[86].Value = model.AffirmTime;
			parameters[87].Value = model.IsNurseDo;
			parameters[88].Value = model.NurseSender;
			parameters[89].Value = model.NurseSendTime;
			parameters[90].Value = model.NurseSendCarrier;
			parameters[91].Value = model.CollectCount;
			parameters[92].Value = model.ForeignSendFlag;
			parameters[93].Value = model.HisAffirm;
			parameters[94].Value = model.PatPhoto;
			parameters[95].Value = model.ChargeOrderNo;
			parameters[96].Value = model.ReportFlag;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            return rows;
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.NRequestForm model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update NRequestForm set ");
			strSql.Append("ReceiveFlag=@ReceiveFlag,");
			strSql.Append("StatusNo=@StatusNo,");
			strSql.Append("SampleTypeNo=@SampleTypeNo,");
			strSql.Append("PatNo=@PatNo,");
			strSql.Append("CName=@CName,");
			strSql.Append("GenderNo=@GenderNo,");
			strSql.Append("Birthday=@Birthday,");
			strSql.Append("Age=@Age,");
			strSql.Append("AgeUnitNo=@AgeUnitNo,");
			strSql.Append("FolkNo=@FolkNo,");
			strSql.Append("DistrictNo=@DistrictNo,");
			strSql.Append("WardNo=@WardNo,");
			strSql.Append("Bed=@Bed,");
			strSql.Append("DeptNo=@DeptNo,");
			strSql.Append("Doctor=@Doctor,");
			strSql.Append("DiagNo=@DiagNo,");
			strSql.Append("ChargeNo=@ChargeNo,");
			strSql.Append("Charge=@Charge,");
			strSql.Append("CollecterID=@CollecterID,");
			strSql.Append("Collecter=@Collecter,");
			strSql.Append("CollectDate=@CollectDate,");
			strSql.Append("CollectTime=@CollectTime,");
			strSql.Append("Operator=@Operator,");
			strSql.Append("OperDate=@OperDate,");
			strSql.Append("OperTime=@OperTime,");
			strSql.Append("FormMemo=@FormMemo,");
			strSql.Append("RequestSource=@RequestSource,");
			strSql.Append("Artificerorder=@Artificerorder,");
			strSql.Append("sickorder=@sickorder,");
			strSql.Append("chargeflag=@chargeflag,");
			strSql.Append("jztype=@jztype,");
			strSql.Append("zdy1=@zdy1,");
			strSql.Append("zdy2=@zdy2,");
			strSql.Append("zdy3=@zdy3,");
			strSql.Append("zdy4=@zdy4,");
			strSql.Append("zdy5=@zdy5,");
			strSql.Append("FlagDateDelete=@FlagDateDelete,");
			strSql.Append("FormComment=@FormComment,");
			strSql.Append("nurseflag=@nurseflag,");
			strSql.Append("diag=@diag,");
			strSql.Append("CaseNo=@CaseNo,");
			strSql.Append("refuseopinion=@refuseopinion,");
			strSql.Append("refusereason=@refusereason,");
			strSql.Append("signintime=@signintime,");
			strSql.Append("signer=@signer,");
			strSql.Append("signflag=@signflag,");
			strSql.Append("SamplingGroupNo=@SamplingGroupNo,");
			strSql.Append("PrintCount=@PrintCount,");
			strSql.Append("PrintInfo=@PrintInfo,");
			strSql.Append("SampleCap=@SampleCap,");
			strSql.Append("IsPrep=@IsPrep,");
			strSql.Append("IsAffirm=@IsAffirm,");
			strSql.Append("IsSampling=@IsSampling,");
			strSql.Append("IsSend=@IsSend,");
			strSql.Append("incepter=@incepter,");
			strSql.Append("inceptTime=@inceptTime,");
			strSql.Append("inceptDate=@inceptDate,");
			strSql.Append("isByHand=@isByHand,");
			strSql.Append("AssignFlag=@AssignFlag,");
			strSql.Append("OldSerialNo=@OldSerialNo,");
			strSql.Append("TestTypeNo=@TestTypeNo,");
			strSql.Append("DispenseFlag=@DispenseFlag,");
			strSql.Append("refuseUser=@refuseUser,");
			strSql.Append("refuseTime=@refuseTime,");
			strSql.Append("jytype=@jytype,");
			strSql.Append("SerialScanTime_old=@SerialScanTime_old,");
			strSql.Append("IsCheckFee=@IsCheckFee,");
			strSql.Append("Dr2Flag=@Dr2Flag,");
			strSql.Append("ExecDeptNo=@ExecDeptNo,");
			strSql.Append("ClientHost=@ClientHost,");
			strSql.Append("PreNumber=@PreNumber,");
			strSql.Append("UrgentState=@UrgentState,");
			strSql.Append("ZDY6=@ZDY6,");
			strSql.Append("ZDY7=@ZDY7,");
			strSql.Append("ZDY8=@ZDY8,");
			strSql.Append("ZDY9=@ZDY9,");
			strSql.Append("ZDY10=@ZDY10,");
			strSql.Append("phoneCode=@phoneCode,");
			strSql.Append("IsNode=@IsNode,");
			strSql.Append("PhoneNodeCount=@PhoneNodeCount,");
			strSql.Append("AutoNodeCount=@AutoNodeCount,");
			strSql.Append("clientno=@clientno,");
			strSql.Append("SerialScanTime=@SerialScanTime,");
			strSql.Append("CountNodesFormSource=@CountNodesFormSource,");
			strSql.Append("StateFlag=@StateFlag,");
			strSql.Append("AffirmTime=@AffirmTime,");
			strSql.Append("IsNurseDo=@IsNurseDo,");
			strSql.Append("NurseSender=@NurseSender,");
			strSql.Append("NurseSendTime=@NurseSendTime,");
			strSql.Append("NurseSendCarrier=@NurseSendCarrier,");
			strSql.Append("CollectCount=@CollectCount,");
			strSql.Append("ForeignSendFlag=@ForeignSendFlag,");
			strSql.Append("HisAffirm=@HisAffirm,");
			strSql.Append("PatPhoto=@PatPhoto,");
			strSql.Append("ChargeOrderNo=@ChargeOrderNo,");
			strSql.Append("ReportFlag=@ReportFlag");
			strSql.Append(" where  SerialNo=@SerialNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@ReceiveFlag", SqlDbType.Int,4),
					new SqlParameter("@StatusNo", SqlDbType.Int,4),
					new SqlParameter("@SampleTypeNo", SqlDbType.Int,4),
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
					new SqlParameter("@DiagNo", SqlDbType.Int,4),
					new SqlParameter("@ChargeNo", SqlDbType.Int,4),
					new SqlParameter("@Charge", SqlDbType.Money,8),
					new SqlParameter("@CollecterID", SqlDbType.VarChar,20),
					new SqlParameter("@Collecter", SqlDbType.VarChar,10),
					new SqlParameter("@CollectDate", SqlDbType.DateTime),
					new SqlParameter("@CollectTime", SqlDbType.DateTime),
					new SqlParameter("@Operator", SqlDbType.VarChar,10),
					new SqlParameter("@OperDate", SqlDbType.DateTime),
					new SqlParameter("@OperTime", SqlDbType.DateTime),
					new SqlParameter("@FormMemo", SqlDbType.VarChar,40),
					new SqlParameter("@RequestSource", SqlDbType.VarChar,20),
					new SqlParameter("@Artificerorder", SqlDbType.VarChar,20),
					new SqlParameter("@sickorder", SqlDbType.VarChar,20),
					new SqlParameter("@chargeflag", SqlDbType.VarChar,10),
					new SqlParameter("@jztype", SqlDbType.Int,4),
					new SqlParameter("@zdy1", SqlDbType.VarChar,50),
					new SqlParameter("@zdy2", SqlDbType.VarChar,200),
					new SqlParameter("@zdy3", SqlDbType.VarChar,50),
					new SqlParameter("@zdy4", SqlDbType.VarChar,50),
					new SqlParameter("@zdy5", SqlDbType.VarChar,50),
					new SqlParameter("@FlagDateDelete", SqlDbType.DateTime),
					new SqlParameter("@FormComment", SqlDbType.Text),
					new SqlParameter("@nurseflag", SqlDbType.Char,10),
					new SqlParameter("@diag", SqlDbType.VarChar,100),
					new SqlParameter("@CaseNo", SqlDbType.VarChar,20),
					new SqlParameter("@refuseopinion", SqlDbType.VarChar,100),
					new SqlParameter("@refusereason", SqlDbType.VarChar,100),
					new SqlParameter("@signintime", SqlDbType.DateTime),
					new SqlParameter("@signer", SqlDbType.VarChar,20),
					new SqlParameter("@signflag", SqlDbType.Int,4),
					new SqlParameter("@SamplingGroupNo", SqlDbType.Int,4),
					new SqlParameter("@PrintCount", SqlDbType.Int,4),
					new SqlParameter("@PrintInfo", SqlDbType.VarChar,600),
					new SqlParameter("@SampleCap", SqlDbType.Float,8),
					new SqlParameter("@IsPrep", SqlDbType.Int,4),
					new SqlParameter("@IsAffirm", SqlDbType.Int,4),
					new SqlParameter("@IsSampling", SqlDbType.Int,4),
					new SqlParameter("@IsSend", SqlDbType.Int,4),
					new SqlParameter("@incepter", SqlDbType.VarChar,20),
					new SqlParameter("@inceptTime", SqlDbType.DateTime),
					new SqlParameter("@inceptDate", SqlDbType.DateTime),
					new SqlParameter("@isByHand", SqlDbType.Bit,1),
					new SqlParameter("@AssignFlag", SqlDbType.Int,4),
					new SqlParameter("@OldSerialNo", SqlDbType.VarChar,40),
					new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
					new SqlParameter("@DispenseFlag", SqlDbType.Int,4),
					new SqlParameter("@refuseUser", SqlDbType.VarChar,20),
					new SqlParameter("@refuseTime", SqlDbType.DateTime),
					new SqlParameter("@jytype", SqlDbType.VarChar,50),
					new SqlParameter("@SerialScanTime_old", SqlDbType.VarChar,30),
					new SqlParameter("@IsCheckFee", SqlDbType.Int,4),
					new SqlParameter("@Dr2Flag", SqlDbType.Int,4),
					new SqlParameter("@ExecDeptNo", SqlDbType.Int,4),
					new SqlParameter("@ClientHost", SqlDbType.VarChar,60),
					new SqlParameter("@PreNumber", SqlDbType.Int,4),
					new SqlParameter("@UrgentState", SqlDbType.VarChar,20),
					new SqlParameter("@ZDY6", SqlDbType.VarChar,60),
					new SqlParameter("@ZDY7", SqlDbType.VarChar,60),
					new SqlParameter("@ZDY8", SqlDbType.VarChar,60),
					new SqlParameter("@ZDY9", SqlDbType.VarChar,60),
					new SqlParameter("@ZDY10", SqlDbType.VarChar,60),
					new SqlParameter("@phoneCode", SqlDbType.VarChar,20),
					new SqlParameter("@IsNode", SqlDbType.Int,4),
					new SqlParameter("@PhoneNodeCount", SqlDbType.Int,4),
					new SqlParameter("@AutoNodeCount", SqlDbType.Int,4),
					new SqlParameter("@clientno", SqlDbType.Int,4),
					new SqlParameter("@SerialScanTime", SqlDbType.DateTime),
					new SqlParameter("@CountNodesFormSource", SqlDbType.Char,1),
					new SqlParameter("@StateFlag", SqlDbType.Int,4),
					new SqlParameter("@AffirmTime", SqlDbType.DateTime),
					new SqlParameter("@IsNurseDo", SqlDbType.Int,4),
					new SqlParameter("@NurseSender", SqlDbType.VarChar,20),
					new SqlParameter("@NurseSendTime", SqlDbType.DateTime),
					new SqlParameter("@NurseSendCarrier", SqlDbType.VarChar,20),
					new SqlParameter("@CollectCount", SqlDbType.Int,4),
					new SqlParameter("@ForeignSendFlag", SqlDbType.Int,4),
					new SqlParameter("@HisAffirm", SqlDbType.Int,4),
					new SqlParameter("@PatPhoto", SqlDbType.Image),
					new SqlParameter("@ChargeOrderNo", SqlDbType.VarChar,20),
					new SqlParameter("@ReportFlag", SqlDbType.Int,4),
					new SqlParameter("@SerialNo", SqlDbType.VarChar,40)};
			parameters[0].Value = model.ReceiveFlag;
			parameters[1].Value = model.StatusNo;
			parameters[2].Value = model.SampleTypeNo;
			parameters[3].Value = model.PatNo;
			parameters[4].Value = model.CName;
			parameters[5].Value = model.GenderNo;
			parameters[6].Value = model.Birthday;
			parameters[7].Value = model.Age;
			parameters[8].Value = model.AgeUnitNo;
			parameters[9].Value = model.FolkNo;
			parameters[10].Value = model.DistrictNo;
			parameters[11].Value = model.WardNo;
			parameters[12].Value = model.Bed;
			parameters[13].Value = model.DeptNo;
			parameters[14].Value = model.Doctor;
			parameters[15].Value = model.DiagNo;
			parameters[16].Value = model.ChargeNo;
			parameters[17].Value = model.Charge;
			parameters[18].Value = model.CollecterID;
			parameters[19].Value = model.Collecter;
			parameters[20].Value = model.CollectDate;
			parameters[21].Value = model.CollectTime;
			parameters[22].Value = model.Operator;
			parameters[23].Value = model.OperDate;
			parameters[24].Value = model.OperTime;
			parameters[25].Value = model.FormMemo;
			parameters[26].Value = model.RequestSource;
			parameters[27].Value = model.Artificerorder;
			parameters[28].Value = model.sickorder;
			parameters[29].Value = model.chargeflag;
			parameters[30].Value = model.jztype;
			parameters[31].Value = model.zdy1;
			parameters[32].Value = model.zdy2;
			parameters[33].Value = model.zdy3;
			parameters[34].Value = model.zdy4;
			parameters[35].Value = model.zdy5;
			parameters[36].Value = model.FlagDateDelete;
			parameters[37].Value = model.FormComment;
			parameters[38].Value = model.nurseflag;
			parameters[39].Value = model.diag;
			parameters[40].Value = model.CaseNo;
			parameters[41].Value = model.refuseopinion;
			parameters[42].Value = model.refusereason;
			parameters[43].Value = model.signintime;
			parameters[44].Value = model.signer;
			parameters[45].Value = model.signflag;
			parameters[46].Value = model.SamplingGroupNo;
			parameters[47].Value = model.PrintCount;
			parameters[48].Value = model.PrintInfo;
			parameters[49].Value = model.SampleCap;
			parameters[50].Value = model.IsPrep;
			parameters[51].Value = model.IsAffirm;
			parameters[52].Value = model.IsSampling;
			parameters[53].Value = model.IsSend;
			parameters[54].Value = model.incepter;
			parameters[55].Value = model.inceptTime;
			parameters[56].Value = model.inceptDate;
			parameters[57].Value = model.isByHand;
			parameters[58].Value = model.AssignFlag;
			parameters[59].Value = model.OldSerialNo;
			parameters[60].Value = model.TestTypeNo;
			parameters[61].Value = model.DispenseFlag;
			parameters[62].Value = model.refuseUser;
			parameters[63].Value = model.refuseTime;
			parameters[64].Value = model.jytype;
			parameters[65].Value = model.SerialScanTime_old;
			parameters[66].Value = model.IsCheckFee;
			parameters[67].Value = model.Dr2Flag;
			parameters[68].Value = model.ExecDeptNo;
			parameters[69].Value = model.ClientHost;
			parameters[70].Value = model.PreNumber;
			parameters[71].Value = model.UrgentState;
			parameters[72].Value = model.ZDY6;
			parameters[73].Value = model.ZDY7;
			parameters[74].Value = model.ZDY8;
			parameters[75].Value = model.ZDY9;
			parameters[76].Value = model.ZDY10;
			parameters[77].Value = model.phoneCode;
			parameters[78].Value = model.IsNode;
			parameters[79].Value = model.PhoneNodeCount;
			parameters[80].Value = model.AutoNodeCount;
			parameters[81].Value = model.clientno;
			parameters[82].Value = model.SerialScanTime;
			parameters[83].Value = model.CountNodesFormSource;
			parameters[84].Value = model.StateFlag;
			parameters[85].Value = model.AffirmTime;
			parameters[86].Value = model.IsNurseDo;
			parameters[87].Value = model.NurseSender;
			parameters[88].Value = model.NurseSendTime;
			parameters[89].Value = model.NurseSendCarrier;
			parameters[90].Value = model.CollectCount;
			parameters[91].Value = model.ForeignSendFlag;
			parameters[92].Value = model.HisAffirm;
			parameters[93].Value = model.PatPhoto;
			parameters[94].Value = model.ChargeOrderNo;
			parameters[95].Value = model.ReportFlag;
			parameters[96].Value = model.SerialNo;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            return rows;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string SerialNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from NRequestForm ");
			strSql.Append(" where SerialNo=@SerialNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@SerialNo", SqlDbType.VarChar,40)};
			parameters[0].Value = SerialNo;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            return rows;
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string SerialNolist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from NRequestForm ");
			strSql.Append(" where SerialNo in ("+SerialNolist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Model.NRequestForm GetModel(string SerialNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SerialNo,ReceiveFlag,StatusNo,SampleTypeNo,PatNo,CName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,DiagNo,ChargeNo,Charge,CollecterID,Collecter,CollectDate,CollectTime,Operator,OperDate,OperTime,FormMemo,RequestSource,Artificerorder,sickorder,chargeflag,jztype,zdy1,zdy2,zdy3,zdy4,zdy5,FlagDateDelete,FormComment,nurseflag,diag,CaseNo,refuseopinion,refusereason,signintime,signer,signflag,SamplingGroupNo,PrintCount,PrintInfo,SampleCap,IsPrep,IsAffirm,IsSampling,IsSend,incepter,inceptTime,inceptDate,isByHand,AssignFlag,OldSerialNo,TestTypeNo,DispenseFlag,refuseUser,refuseTime,jytype,SerialScanTime_old,IsCheckFee,Dr2Flag,ExecDeptNo,ClientHost,PreNumber,UrgentState,ZDY6,ZDY7,ZDY8,ZDY9,ZDY10,phoneCode,IsNode,PhoneNodeCount,AutoNodeCount,clientno,SerialScanTime,CountNodesFormSource,StateFlag,AffirmTime,IsNurseDo,NurseSender,NurseSendTime,NurseSendCarrier,CollectCount,ForeignSendFlag,HisAffirm,PatPhoto,ChargeOrderNo,ReportFlag from NRequestForm ");
			strSql.Append(" where SerialNo=@SerialNo ");
			SqlParameter[] parameters = {
					new SqlParameter("@SerialNo", SqlDbType.VarChar,40)};
			parameters[0].Value = SerialNo;

			Model.NRequestForm model=new Model.NRequestForm();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SerialNo"]!=null && ds.Tables[0].Rows[0]["SerialNo"].ToString()!="")
				{
					model.SerialNo=ds.Tables[0].Rows[0]["SerialNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReceiveFlag"]!=null && ds.Tables[0].Rows[0]["ReceiveFlag"].ToString()!="")
				{
					model.ReceiveFlag=int.Parse(ds.Tables[0].Rows[0]["ReceiveFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StatusNo"]!=null && ds.Tables[0].Rows[0]["StatusNo"].ToString()!="")
				{
					model.StatusNo=int.Parse(ds.Tables[0].Rows[0]["StatusNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SampleTypeNo"]!=null && ds.Tables[0].Rows[0]["SampleTypeNo"].ToString()!="")
				{
					model.SampleTypeNo=int.Parse(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PatNo"]!=null && ds.Tables[0].Rows[0]["PatNo"].ToString()!="")
				{
					model.PatNo=ds.Tables[0].Rows[0]["PatNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CName"]!=null && ds.Tables[0].Rows[0]["CName"].ToString()!="")
				{
					model.CName=ds.Tables[0].Rows[0]["CName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["GenderNo"]!=null && ds.Tables[0].Rows[0]["GenderNo"].ToString()!="")
				{
					model.GenderNo=int.Parse(ds.Tables[0].Rows[0]["GenderNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Birthday"]!=null && ds.Tables[0].Rows[0]["Birthday"].ToString()!="")
				{
					model.Birthday=DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Age"]!=null && ds.Tables[0].Rows[0]["Age"].ToString()!="")
				{
					model.Age=decimal.Parse(ds.Tables[0].Rows[0]["Age"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AgeUnitNo"]!=null && ds.Tables[0].Rows[0]["AgeUnitNo"].ToString()!="")
				{
					model.AgeUnitNo=int.Parse(ds.Tables[0].Rows[0]["AgeUnitNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FolkNo"]!=null && ds.Tables[0].Rows[0]["FolkNo"].ToString()!="")
				{
					model.FolkNo=int.Parse(ds.Tables[0].Rows[0]["FolkNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DistrictNo"]!=null && ds.Tables[0].Rows[0]["DistrictNo"].ToString()!="")
				{
					model.DistrictNo=int.Parse(ds.Tables[0].Rows[0]["DistrictNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WardNo"]!=null && ds.Tables[0].Rows[0]["WardNo"].ToString()!="")
				{
					model.WardNo=int.Parse(ds.Tables[0].Rows[0]["WardNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Bed"]!=null && ds.Tables[0].Rows[0]["Bed"].ToString()!="")
				{
					model.Bed=ds.Tables[0].Rows[0]["Bed"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DeptNo"]!=null && ds.Tables[0].Rows[0]["DeptNo"].ToString()!="")
				{
					model.DeptNo=int.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Doctor"]!=null && ds.Tables[0].Rows[0]["Doctor"].ToString()!="")
				{
					model.Doctor=int.Parse(ds.Tables[0].Rows[0]["Doctor"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DiagNo"]!=null && ds.Tables[0].Rows[0]["DiagNo"].ToString()!="")
				{
					model.DiagNo=int.Parse(ds.Tables[0].Rows[0]["DiagNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeNo"]!=null && ds.Tables[0].Rows[0]["ChargeNo"].ToString()!="")
				{
					model.ChargeNo=int.Parse(ds.Tables[0].Rows[0]["ChargeNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Charge"]!=null && ds.Tables[0].Rows[0]["Charge"].ToString()!="")
				{
					model.Charge=decimal.Parse(ds.Tables[0].Rows[0]["Charge"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CollecterID"]!=null && ds.Tables[0].Rows[0]["CollecterID"].ToString()!="")
				{
					model.CollecterID=ds.Tables[0].Rows[0]["CollecterID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Collecter"]!=null && ds.Tables[0].Rows[0]["Collecter"].ToString()!="")
				{
					model.Collecter=ds.Tables[0].Rows[0]["Collecter"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CollectDate"]!=null && ds.Tables[0].Rows[0]["CollectDate"].ToString()!="")
				{
					model.CollectDate=DateTime.Parse(ds.Tables[0].Rows[0]["CollectDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CollectTime"]!=null && ds.Tables[0].Rows[0]["CollectTime"].ToString()!="")
				{
					model.CollectTime=DateTime.Parse(ds.Tables[0].Rows[0]["CollectTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Operator"]!=null && ds.Tables[0].Rows[0]["Operator"].ToString()!="")
				{
					model.Operator=ds.Tables[0].Rows[0]["Operator"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperDate"]!=null && ds.Tables[0].Rows[0]["OperDate"].ToString()!="")
				{
					model.OperDate=DateTime.Parse(ds.Tables[0].Rows[0]["OperDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OperTime"]!=null && ds.Tables[0].Rows[0]["OperTime"].ToString()!="")
				{
					model.OperTime=DateTime.Parse(ds.Tables[0].Rows[0]["OperTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FormMemo"]!=null && ds.Tables[0].Rows[0]["FormMemo"].ToString()!="")
				{
					model.FormMemo=ds.Tables[0].Rows[0]["FormMemo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["RequestSource"]!=null && ds.Tables[0].Rows[0]["RequestSource"].ToString()!="")
				{
					model.RequestSource=ds.Tables[0].Rows[0]["RequestSource"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Artificerorder"]!=null && ds.Tables[0].Rows[0]["Artificerorder"].ToString()!="")
				{
					model.Artificerorder=ds.Tables[0].Rows[0]["Artificerorder"].ToString();
				}
				if(ds.Tables[0].Rows[0]["sickorder"]!=null && ds.Tables[0].Rows[0]["sickorder"].ToString()!="")
				{
					model.sickorder=ds.Tables[0].Rows[0]["sickorder"].ToString();
				}
				if(ds.Tables[0].Rows[0]["chargeflag"]!=null && ds.Tables[0].Rows[0]["chargeflag"].ToString()!="")
				{
					model.chargeflag=ds.Tables[0].Rows[0]["chargeflag"].ToString();
				}
				if(ds.Tables[0].Rows[0]["jztype"]!=null && ds.Tables[0].Rows[0]["jztype"].ToString()!="")
				{
					model.jztype=int.Parse(ds.Tables[0].Rows[0]["jztype"].ToString());
				}
				if(ds.Tables[0].Rows[0]["zdy1"]!=null && ds.Tables[0].Rows[0]["zdy1"].ToString()!="")
				{
					model.zdy1=ds.Tables[0].Rows[0]["zdy1"].ToString();
				}
				if(ds.Tables[0].Rows[0]["zdy2"]!=null && ds.Tables[0].Rows[0]["zdy2"].ToString()!="")
				{
					model.zdy2=ds.Tables[0].Rows[0]["zdy2"].ToString();
				}
				if(ds.Tables[0].Rows[0]["zdy3"]!=null && ds.Tables[0].Rows[0]["zdy3"].ToString()!="")
				{
					model.zdy3=ds.Tables[0].Rows[0]["zdy3"].ToString();
				}
				if(ds.Tables[0].Rows[0]["zdy4"]!=null && ds.Tables[0].Rows[0]["zdy4"].ToString()!="")
				{
					model.zdy4=ds.Tables[0].Rows[0]["zdy4"].ToString();
				}
				if(ds.Tables[0].Rows[0]["zdy5"]!=null && ds.Tables[0].Rows[0]["zdy5"].ToString()!="")
				{
					model.zdy5=ds.Tables[0].Rows[0]["zdy5"].ToString();
				}
				if(ds.Tables[0].Rows[0]["FlagDateDelete"]!=null && ds.Tables[0].Rows[0]["FlagDateDelete"].ToString()!="")
				{
					model.FlagDateDelete=DateTime.Parse(ds.Tables[0].Rows[0]["FlagDateDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FormComment"]!=null && ds.Tables[0].Rows[0]["FormComment"].ToString()!="")
				{
					model.FormComment=ds.Tables[0].Rows[0]["FormComment"].ToString();
				}
				if(ds.Tables[0].Rows[0]["nurseflag"]!=null && ds.Tables[0].Rows[0]["nurseflag"].ToString()!="")
				{
					model.nurseflag=ds.Tables[0].Rows[0]["nurseflag"].ToString();
				}
				if(ds.Tables[0].Rows[0]["diag"]!=null && ds.Tables[0].Rows[0]["diag"].ToString()!="")
				{
					model.diag=ds.Tables[0].Rows[0]["diag"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CaseNo"]!=null && ds.Tables[0].Rows[0]["CaseNo"].ToString()!="")
				{
					model.CaseNo=ds.Tables[0].Rows[0]["CaseNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["refuseopinion"]!=null && ds.Tables[0].Rows[0]["refuseopinion"].ToString()!="")
				{
					model.refuseopinion=ds.Tables[0].Rows[0]["refuseopinion"].ToString();
				}
				if(ds.Tables[0].Rows[0]["refusereason"]!=null && ds.Tables[0].Rows[0]["refusereason"].ToString()!="")
				{
					model.refusereason=ds.Tables[0].Rows[0]["refusereason"].ToString();
				}
				if(ds.Tables[0].Rows[0]["signintime"]!=null && ds.Tables[0].Rows[0]["signintime"].ToString()!="")
				{
					model.signintime=DateTime.Parse(ds.Tables[0].Rows[0]["signintime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["signer"]!=null && ds.Tables[0].Rows[0]["signer"].ToString()!="")
				{
					model.signer=ds.Tables[0].Rows[0]["signer"].ToString();
				}
				if(ds.Tables[0].Rows[0]["signflag"]!=null && ds.Tables[0].Rows[0]["signflag"].ToString()!="")
				{
					model.signflag=int.Parse(ds.Tables[0].Rows[0]["signflag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SamplingGroupNo"]!=null && ds.Tables[0].Rows[0]["SamplingGroupNo"].ToString()!="")
				{
					model.SamplingGroupNo=int.Parse(ds.Tables[0].Rows[0]["SamplingGroupNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrintCount"]!=null && ds.Tables[0].Rows[0]["PrintCount"].ToString()!="")
				{
					model.PrintCount=int.Parse(ds.Tables[0].Rows[0]["PrintCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrintInfo"]!=null && ds.Tables[0].Rows[0]["PrintInfo"].ToString()!="")
				{
					model.PrintInfo=ds.Tables[0].Rows[0]["PrintInfo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SampleCap"]!=null && ds.Tables[0].Rows[0]["SampleCap"].ToString()!="")
				{
					model.SampleCap=decimal.Parse(ds.Tables[0].Rows[0]["SampleCap"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsPrep"]!=null && ds.Tables[0].Rows[0]["IsPrep"].ToString()!="")
				{
					model.IsPrep=int.Parse(ds.Tables[0].Rows[0]["IsPrep"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAffirm"]!=null && ds.Tables[0].Rows[0]["IsAffirm"].ToString()!="")
				{
					model.IsAffirm=int.Parse(ds.Tables[0].Rows[0]["IsAffirm"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSampling"]!=null && ds.Tables[0].Rows[0]["IsSampling"].ToString()!="")
				{
					model.IsSampling=int.Parse(ds.Tables[0].Rows[0]["IsSampling"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSend"]!=null && ds.Tables[0].Rows[0]["IsSend"].ToString()!="")
				{
					model.IsSend=int.Parse(ds.Tables[0].Rows[0]["IsSend"].ToString());
				}
				if(ds.Tables[0].Rows[0]["incepter"]!=null && ds.Tables[0].Rows[0]["incepter"].ToString()!="")
				{
					model.incepter=ds.Tables[0].Rows[0]["incepter"].ToString();
				}
				if(ds.Tables[0].Rows[0]["inceptTime"]!=null && ds.Tables[0].Rows[0]["inceptTime"].ToString()!="")
				{
					model.inceptTime=DateTime.Parse(ds.Tables[0].Rows[0]["inceptTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["inceptDate"]!=null && ds.Tables[0].Rows[0]["inceptDate"].ToString()!="")
				{
					model.inceptDate=DateTime.Parse(ds.Tables[0].Rows[0]["inceptDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isByHand"]!=null && ds.Tables[0].Rows[0]["isByHand"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["isByHand"].ToString()=="1")||(ds.Tables[0].Rows[0]["isByHand"].ToString().ToLower()=="true"))
					{
						model.isByHand=true;
					}
					else
					{
						model.isByHand=false;
					}
				}
				if(ds.Tables[0].Rows[0]["AssignFlag"]!=null && ds.Tables[0].Rows[0]["AssignFlag"].ToString()!="")
				{
					model.AssignFlag=int.Parse(ds.Tables[0].Rows[0]["AssignFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OldSerialNo"]!=null && ds.Tables[0].Rows[0]["OldSerialNo"].ToString()!="")
				{
					model.OldSerialNo=ds.Tables[0].Rows[0]["OldSerialNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TestTypeNo"]!=null && ds.Tables[0].Rows[0]["TestTypeNo"].ToString()!="")
				{
					model.TestTypeNo=int.Parse(ds.Tables[0].Rows[0]["TestTypeNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DispenseFlag"]!=null && ds.Tables[0].Rows[0]["DispenseFlag"].ToString()!="")
				{
					model.DispenseFlag=int.Parse(ds.Tables[0].Rows[0]["DispenseFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["refuseUser"]!=null && ds.Tables[0].Rows[0]["refuseUser"].ToString()!="")
				{
					model.refuseUser=ds.Tables[0].Rows[0]["refuseUser"].ToString();
				}
				if(ds.Tables[0].Rows[0]["refuseTime"]!=null && ds.Tables[0].Rows[0]["refuseTime"].ToString()!="")
				{
					model.refuseTime=DateTime.Parse(ds.Tables[0].Rows[0]["refuseTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["jytype"]!=null && ds.Tables[0].Rows[0]["jytype"].ToString()!="")
				{
					model.jytype=ds.Tables[0].Rows[0]["jytype"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SerialScanTime_old"]!=null && ds.Tables[0].Rows[0]["SerialScanTime_old"].ToString()!="")
				{
					model.SerialScanTime_old=ds.Tables[0].Rows[0]["SerialScanTime_old"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsCheckFee"]!=null && ds.Tables[0].Rows[0]["IsCheckFee"].ToString()!="")
				{
					model.IsCheckFee=int.Parse(ds.Tables[0].Rows[0]["IsCheckFee"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Dr2Flag"]!=null && ds.Tables[0].Rows[0]["Dr2Flag"].ToString()!="")
				{
					model.Dr2Flag=int.Parse(ds.Tables[0].Rows[0]["Dr2Flag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ExecDeptNo"]!=null && ds.Tables[0].Rows[0]["ExecDeptNo"].ToString()!="")
				{
					model.ExecDeptNo=int.Parse(ds.Tables[0].Rows[0]["ExecDeptNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ClientHost"]!=null && ds.Tables[0].Rows[0]["ClientHost"].ToString()!="")
				{
					model.ClientHost=ds.Tables[0].Rows[0]["ClientHost"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PreNumber"]!=null && ds.Tables[0].Rows[0]["PreNumber"].ToString()!="")
				{
					model.PreNumber=int.Parse(ds.Tables[0].Rows[0]["PreNumber"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UrgentState"]!=null && ds.Tables[0].Rows[0]["UrgentState"].ToString()!="")
				{
					model.UrgentState=ds.Tables[0].Rows[0]["UrgentState"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ZDY6"]!=null && ds.Tables[0].Rows[0]["ZDY6"].ToString()!="")
				{
					model.ZDY6=ds.Tables[0].Rows[0]["ZDY6"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ZDY7"]!=null && ds.Tables[0].Rows[0]["ZDY7"].ToString()!="")
				{
					model.ZDY7=ds.Tables[0].Rows[0]["ZDY7"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ZDY8"]!=null && ds.Tables[0].Rows[0]["ZDY8"].ToString()!="")
				{
					model.ZDY8=ds.Tables[0].Rows[0]["ZDY8"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ZDY9"]!=null && ds.Tables[0].Rows[0]["ZDY9"].ToString()!="")
				{
					model.ZDY9=ds.Tables[0].Rows[0]["ZDY9"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ZDY10"]!=null && ds.Tables[0].Rows[0]["ZDY10"].ToString()!="")
				{
					model.ZDY10=ds.Tables[0].Rows[0]["ZDY10"].ToString();
				}
				if(ds.Tables[0].Rows[0]["phoneCode"]!=null && ds.Tables[0].Rows[0]["phoneCode"].ToString()!="")
				{
					model.phoneCode=ds.Tables[0].Rows[0]["phoneCode"].ToString();
				}
				if(ds.Tables[0].Rows[0]["IsNode"]!=null && ds.Tables[0].Rows[0]["IsNode"].ToString()!="")
				{
					model.IsNode=int.Parse(ds.Tables[0].Rows[0]["IsNode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PhoneNodeCount"]!=null && ds.Tables[0].Rows[0]["PhoneNodeCount"].ToString()!="")
				{
					model.PhoneNodeCount=int.Parse(ds.Tables[0].Rows[0]["PhoneNodeCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AutoNodeCount"]!=null && ds.Tables[0].Rows[0]["AutoNodeCount"].ToString()!="")
				{
					model.AutoNodeCount=int.Parse(ds.Tables[0].Rows[0]["AutoNodeCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["clientno"]!=null && ds.Tables[0].Rows[0]["clientno"].ToString()!="")
				{
					model.clientno=int.Parse(ds.Tables[0].Rows[0]["clientno"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SerialScanTime"]!=null && ds.Tables[0].Rows[0]["SerialScanTime"].ToString()!="")
				{
					model.SerialScanTime=DateTime.Parse(ds.Tables[0].Rows[0]["SerialScanTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CountNodesFormSource"]!=null && ds.Tables[0].Rows[0]["CountNodesFormSource"].ToString()!="")
				{
					model.CountNodesFormSource=ds.Tables[0].Rows[0]["CountNodesFormSource"].ToString();
				}
				if(ds.Tables[0].Rows[0]["StateFlag"]!=null && ds.Tables[0].Rows[0]["StateFlag"].ToString()!="")
				{
					model.StateFlag=int.Parse(ds.Tables[0].Rows[0]["StateFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AffirmTime"]!=null && ds.Tables[0].Rows[0]["AffirmTime"].ToString()!="")
				{
					model.AffirmTime=DateTime.Parse(ds.Tables[0].Rows[0]["AffirmTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsNurseDo"]!=null && ds.Tables[0].Rows[0]["IsNurseDo"].ToString()!="")
				{
					model.IsNurseDo=int.Parse(ds.Tables[0].Rows[0]["IsNurseDo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NurseSender"]!=null && ds.Tables[0].Rows[0]["NurseSender"].ToString()!="")
				{
					model.NurseSender=ds.Tables[0].Rows[0]["NurseSender"].ToString();
				}
				if(ds.Tables[0].Rows[0]["NurseSendTime"]!=null && ds.Tables[0].Rows[0]["NurseSendTime"].ToString()!="")
				{
					model.NurseSendTime=DateTime.Parse(ds.Tables[0].Rows[0]["NurseSendTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NurseSendCarrier"]!=null && ds.Tables[0].Rows[0]["NurseSendCarrier"].ToString()!="")
				{
					model.NurseSendCarrier=ds.Tables[0].Rows[0]["NurseSendCarrier"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CollectCount"]!=null && ds.Tables[0].Rows[0]["CollectCount"].ToString()!="")
				{
					model.CollectCount=int.Parse(ds.Tables[0].Rows[0]["CollectCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ForeignSendFlag"]!=null && ds.Tables[0].Rows[0]["ForeignSendFlag"].ToString()!="")
				{
					model.ForeignSendFlag=int.Parse(ds.Tables[0].Rows[0]["ForeignSendFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HisAffirm"]!=null && ds.Tables[0].Rows[0]["HisAffirm"].ToString()!="")
				{
					model.HisAffirm=int.Parse(ds.Tables[0].Rows[0]["HisAffirm"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PatPhoto"]!=null && ds.Tables[0].Rows[0]["PatPhoto"].ToString()!="")
				{
					model.PatPhoto=(byte[])ds.Tables[0].Rows[0]["PatPhoto"];
				}
				if(ds.Tables[0].Rows[0]["ChargeOrderNo"]!=null && ds.Tables[0].Rows[0]["ChargeOrderNo"].ToString()!="")
				{
					model.ChargeOrderNo=ds.Tables[0].Rows[0]["ChargeOrderNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReportFlag"]!=null && ds.Tables[0].Rows[0]["ReportFlag"].ToString()!="")
				{
					model.ReportFlag=int.Parse(ds.Tables[0].Rows[0]["ReportFlag"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SerialNo,ReceiveFlag,StatusNo,SampleTypeNo,PatNo,CName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,DiagNo,ChargeNo,Charge,CollecterID,Collecter,CollectDate,CollectTime,Operator,OperDate,OperTime,FormMemo,RequestSource,Artificerorder,sickorder,chargeflag,jztype,zdy1,zdy2,zdy3,zdy4,zdy5,FlagDateDelete,FormComment,nurseflag,diag,CaseNo,refuseopinion,refusereason,signintime,signer,signflag,SamplingGroupNo,PrintCount,PrintInfo,SampleCap,IsPrep,IsAffirm,IsSampling,IsSend,incepter,inceptTime,inceptDate,isByHand,AssignFlag,OldSerialNo,TestTypeNo,DispenseFlag,refuseUser,refuseTime,jytype,SerialScanTime_old,IsCheckFee,Dr2Flag,ExecDeptNo,ClientHost,PreNumber,UrgentState,ZDY6,ZDY7,ZDY8,ZDY9,ZDY10,phoneCode,IsNode,PhoneNodeCount,AutoNodeCount,clientno,SerialScanTime,CountNodesFormSource,StateFlag,AffirmTime,IsNurseDo,NurseSender,NurseSendTime,NurseSendCarrier,CollectCount,ForeignSendFlag,HisAffirm,PatPhoto,ChargeOrderNo,ReportFlag ");
			strSql.Append(" FROM NRequestForm ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" SerialNo,ReceiveFlag,StatusNo,SampleTypeNo,PatNo,CName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,DiagNo,ChargeNo,Charge,CollecterID,Collecter,CollectDate,CollectTime,Operator,OperDate,OperTime,FormMemo,RequestSource,Artificerorder,sickorder,chargeflag,jztype,zdy1,zdy2,zdy3,zdy4,zdy5,FlagDateDelete,FormComment,nurseflag,diag,CaseNo,refuseopinion,refusereason,signintime,signer,signflag,SamplingGroupNo,PrintCount,PrintInfo,SampleCap,IsPrep,IsAffirm,IsSampling,IsSend,incepter,inceptTime,inceptDate,isByHand,AssignFlag,OldSerialNo,TestTypeNo,DispenseFlag,refuseUser,refuseTime,jytype,SerialScanTime_old,IsCheckFee,Dr2Flag,ExecDeptNo,ClientHost,PreNumber,UrgentState,ZDY6,ZDY7,ZDY8,ZDY9,ZDY10,phoneCode,IsNode,PhoneNodeCount,AutoNodeCount,clientno,SerialScanTime,CountNodesFormSource,StateFlag,AffirmTime,IsNurseDo,NurseSender,NurseSendTime,NurseSendCarrier,CollectCount,ForeignSendFlag,HisAffirm,PatPhoto,ChargeOrderNo,ReportFlag ");
			strSql.Append(" FROM NRequestForm ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "NRequestForm";
			parameters[1].Value = "SerialNo";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.NRequestForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from NRequestForm where 1=1 ");
            if(model.SerialNo !=null)
            {
            strSql.Append(" and SerialNo='"+model.SerialNo+"'");
            }
                                          
            if(model.AgeUnitNo !=null)
            {
            strSql.Append(" and AgeUnitNo='"+model.AgeUnitNo+"'");
            }
                                          
            if(model.FolkNo !=null)
            {
            strSql.Append(" and FolkNo='"+model.FolkNo+"'");
            }
                                          
            if(model.DistrictNo !=null)
            {
            strSql.Append(" and DistrictNo='"+model.DistrictNo+"'");
            }
                                          
            if(model.WardNo !=null)
            {
            strSql.Append(" and WardNo='"+model.WardNo+"'");
            }
                                          
            if(model.Bed !=null)
            {
            strSql.Append(" and Bed='"+model.Bed+"'");
            }
                                          
            if(model.DeptNo !=null)
            {
            strSql.Append(" and DeptNo='"+model.DeptNo+"'");
            }
                                          
            if(model.Doctor !=null)
            {
            strSql.Append(" and Doctor='"+model.Doctor+"'");
            }
                                          
            if(model.DiagNo !=null)
            {
            strSql.Append(" and DiagNo='"+model.DiagNo+"'");
            }
                                          
            if(model.ChargeNo !=null)
            {
            strSql.Append(" and ChargeNo='"+model.ChargeNo+"'");
            }
                                          
            if(model.Charge !=null)
            {
            strSql.Append(" and Charge='"+model.Charge+"'");
            }
                                          
            if(model.ReceiveFlag !=null)
            {
            strSql.Append(" and ReceiveFlag='"+model.ReceiveFlag+"'");
            }
                                          
            if(model.CollecterID !=null)
            {
            strSql.Append(" and CollecterID='"+model.CollecterID+"'");
            }
                                          
            if(model.Collecter !=null)
            {
            strSql.Append(" and Collecter='"+model.Collecter+"'");
            }
                                          
            if(model.CollectDate !=null)
            {
            strSql.Append(" and CollectDate='"+model.CollectDate+"'");
            }
                                          
            if(model.CollectTime !=null)
            {
            strSql.Append(" and CollectTime='"+model.CollectTime+"'");
            }
                                          
            if(model.Operator !=null)
            {
            strSql.Append(" and Operator='"+model.Operator+"'");
            }
                                          
            if(model.OperDate !=null)
            {
            strSql.Append(" and OperDate='"+model.OperDate+"'");
            }
                                          
            if(model.OperTime !=null)
            {
            strSql.Append(" and OperTime='"+model.OperTime+"'");
            }
                                          
            if(model.FormMemo !=null)
            {
            strSql.Append(" and FormMemo='"+model.FormMemo+"'");
            }
                                          
            if(model.RequestSource !=null)
            {
            strSql.Append(" and RequestSource='"+model.RequestSource+"'");
            }
                                          
            if(model.Artificerorder !=null)
            {
            strSql.Append(" and Artificerorder='"+model.Artificerorder+"'");
            }
                                          
            if(model.StatusNo !=null)
            {
            strSql.Append(" and StatusNo='"+model.StatusNo+"'");
            }
                                          
            if(model.sickorder !=null)
            {
            strSql.Append(" and sickorder='"+model.sickorder+"'");
            }
                                          
            if(model.chargeflag !=null)
            {
            strSql.Append(" and chargeflag='"+model.chargeflag+"'");
            }
                                          
            if(model.jztype !=null)
            {
            strSql.Append(" and jztype='"+model.jztype+"'");
            }
                                          
            if(model.zdy1 !=null)
            {
            strSql.Append(" and zdy1='"+model.zdy1+"'");
            }
                                          
            if(model.zdy2 !=null)
            {
            strSql.Append(" and zdy2='"+model.zdy2+"'");
            }
                                          
            if(model.zdy3 !=null)
            {
            strSql.Append(" and zdy3='"+model.zdy3+"'");
            }
                                          
            if(model.zdy4 !=null)
            {
            strSql.Append(" and zdy4='"+model.zdy4+"'");
            }
                                          
            if(model.zdy5 !=null)
            {
            strSql.Append(" and zdy5='"+model.zdy5+"'");
            }
                                          
            if(model.FlagDateDelete !=null)
            {
            strSql.Append(" and FlagDateDelete='"+model.FlagDateDelete+"'");
            }
                                          
            if(model.SampleTypeNo !=null)
            {
            strSql.Append(" and SampleTypeNo='"+model.SampleTypeNo+"'");
            }
                                          
            if(model.nurseflag !=null)
            {
            strSql.Append(" and nurseflag='"+model.nurseflag+"'");
            }
                                          
            if(model.diag !=null)
            {
            strSql.Append(" and diag='"+model.diag+"'");
            }
                                          
            if(model.CaseNo !=null)
            {
            strSql.Append(" and CaseNo='"+model.CaseNo+"'");
            }
                                          
            if(model.refuseopinion !=null)
            {
            strSql.Append(" and refuseopinion='"+model.refuseopinion+"'");
            }
                                          
            if(model.refusereason !=null)
            {
            strSql.Append(" and refusereason='"+model.refusereason+"'");
            }
                                          
            if(model.signintime !=null)
            {
            strSql.Append(" and signintime='"+model.signintime+"'");
            }
                                          
            if(model.signer !=null)
            {
            strSql.Append(" and signer='"+model.signer+"'");
            }
                                          
            if(model.signflag !=null)
            {
            strSql.Append(" and signflag='"+model.signflag+"'");
            }
                                          
            if(model.SamplingGroupNo !=null)
            {
            strSql.Append(" and SamplingGroupNo='"+model.SamplingGroupNo+"'");
            }
                                          
            if(model.PrintCount !=null)
            {
            strSql.Append(" and PrintCount='"+model.PrintCount+"'");
            }
                                          
            if(model.PatNo !=null)
            {
            strSql.Append(" and PatNo='"+model.PatNo+"'");
            }
                                          
            if(model.PrintInfo !=null)
            {
            strSql.Append(" and PrintInfo='"+model.PrintInfo+"'");
            }
                                          
            if(model.SampleCap !=null)
            {
            strSql.Append(" and SampleCap='"+model.SampleCap+"'");
            }
                                          
            if(model.IsPrep !=null)
            {
            strSql.Append(" and IsPrep='"+model.IsPrep+"'");
            }
                                          
            if(model.IsAffirm !=null)
            {
            strSql.Append(" and IsAffirm='"+model.IsAffirm+"'");
            }
                                          
            if(model.IsSampling !=null)
            {
            strSql.Append(" and IsSampling='"+model.IsSampling+"'");
            }
                                          
            if(model.IsSend !=null)
            {
            strSql.Append(" and IsSend='"+model.IsSend+"'");
            }
                                          
            if(model.incepter !=null)
            {
            strSql.Append(" and incepter='"+model.incepter+"'");
            }
                                          
            if(model.inceptTime !=null)
            {
            strSql.Append(" and inceptTime='"+model.inceptTime+"'");
            }
                                          
            if(model.inceptDate !=null)
            {
            strSql.Append(" and inceptDate='"+model.inceptDate+"'");
            }
                                          
            if(model.isByHand !=null)
            {
            strSql.Append(" and isByHand='"+model.isByHand+"'");
            }
                                          
            if(model.CName !=null)
            {
            strSql.Append(" and CName='"+model.CName+"'");
            }
                                          
            if(model.AssignFlag !=null)
            {
            strSql.Append(" and AssignFlag='"+model.AssignFlag+"'");
            }
                                          
            if(model.OldSerialNo !=null)
            {
            strSql.Append(" and OldSerialNo='"+model.OldSerialNo+"'");
            }
                                          
            if(model.TestTypeNo !=null)
            {
            strSql.Append(" and TestTypeNo='"+model.TestTypeNo+"'");
            }
                                          
            if(model.DispenseFlag !=null)
            {
            strSql.Append(" and DispenseFlag='"+model.DispenseFlag+"'");
            }
                                          
            if(model.refuseUser !=null)
            {
            strSql.Append(" and refuseUser='"+model.refuseUser+"'");
            }
                                          
            if(model.refuseTime !=null)
            {
            strSql.Append(" and refuseTime='"+model.refuseTime+"'");
            }
                                          
            if(model.jytype !=null)
            {
            strSql.Append(" and jytype='"+model.jytype+"'");
            }
                                          
            if(model.SerialScanTime_old !=null)
            {
            strSql.Append(" and SerialScanTime_old='"+model.SerialScanTime_old+"'");
            }
                                          
            if(model.IsCheckFee !=null)
            {
            strSql.Append(" and IsCheckFee='"+model.IsCheckFee+"'");
            }
                                          
            if(model.Dr2Flag !=null)
            {
            strSql.Append(" and Dr2Flag='"+model.Dr2Flag+"'");
            }
                                          
            if(model.GenderNo !=null)
            {
            strSql.Append(" and GenderNo='"+model.GenderNo+"'");
            }
                                          
            if(model.ExecDeptNo !=null)
            {
            strSql.Append(" and ExecDeptNo='"+model.ExecDeptNo+"'");
            }
                                          
            if(model.ClientHost !=null)
            {
            strSql.Append(" and ClientHost='"+model.ClientHost+"'");
            }
                                          
            if(model.PreNumber !=null)
            {
            strSql.Append(" and PreNumber='"+model.PreNumber+"'");
            }
                                          
            if(model.UrgentState !=null)
            {
            strSql.Append(" and UrgentState='"+model.UrgentState+"'");
            }
                                          
            if(model.ZDY6 !=null)
            {
            strSql.Append(" and ZDY6='"+model.ZDY6+"'");
            }
                                          
            if(model.ZDY7 !=null)
            {
            strSql.Append(" and ZDY7='"+model.ZDY7+"'");
            }
                                          
            if(model.ZDY8 !=null)
            {
            strSql.Append(" and ZDY8='"+model.ZDY8+"'");
            }
                                          
            if(model.ZDY9 !=null)
            {
            strSql.Append(" and ZDY9='"+model.ZDY9+"'");
            }
                                          
            if(model.ZDY10 !=null)
            {
            strSql.Append(" and ZDY10='"+model.ZDY10+"'");
            }
                                          
            if(model.phoneCode !=null)
            {
            strSql.Append(" and phoneCode='"+model.phoneCode+"'");
            }
                                          
            if(model.Birthday !=null)
            {
            strSql.Append(" and Birthday='"+model.Birthday+"'");
            }
                                          
            if(model.IsNode !=null)
            {
            strSql.Append(" and IsNode='"+model.IsNode+"'");
            }
                                          
            if(model.PhoneNodeCount !=null)
            {
            strSql.Append(" and PhoneNodeCount='"+model.PhoneNodeCount+"'");
            }
                                          
            if(model.AutoNodeCount !=null)
            {
            strSql.Append(" and AutoNodeCount='"+model.AutoNodeCount+"'");
            }
                                          
            if(model.clientno !=null)
            {
            strSql.Append(" and clientno='"+model.clientno+"'");
            }
                                          
            if(model.SerialScanTime !=null)
            {
            strSql.Append(" and SerialScanTime='"+model.SerialScanTime+"'");
            }
                                          
            if(model.CountNodesFormSource !=null)
            {
            strSql.Append(" and CountNodesFormSource='"+model.CountNodesFormSource+"'");
            }
                                          
            if(model.StateFlag !=null)
            {
            strSql.Append(" and StateFlag='"+model.StateFlag+"'");
            }
                                          
            if(model.AffirmTime !=null)
            {
            strSql.Append(" and AffirmTime='"+model.AffirmTime+"'");
            }
                                          
            if(model.IsNurseDo !=null)
            {
            strSql.Append(" and IsNurseDo='"+model.IsNurseDo+"'");
            }
                                          
            if(model.Age !=null)
            {
            strSql.Append(" and Age='"+model.Age+"'");
            }
                                          
            if(model.NurseSender !=null)
            {
            strSql.Append(" and NurseSender='"+model.NurseSender+"'");
            }
                                          
            if(model.NurseSendTime !=null)
            {
            strSql.Append(" and NurseSendTime='"+model.NurseSendTime+"'");
            }
                                          
            if(model.NurseSendCarrier !=null)
            {
            strSql.Append(" and NurseSendCarrier='"+model.NurseSendCarrier+"'");
            }
                                          
            if(model.CollectCount !=null)
            {
            strSql.Append(" and CollectCount='"+model.CollectCount+"'");
            }
                                          
            if(model.ForeignSendFlag !=null)
            {
            strSql.Append(" and ForeignSendFlag='"+model.ForeignSendFlag+"'");
            }
                                          
            if(model.HisAffirm !=null)
            {
            strSql.Append(" and HisAffirm='"+model.HisAffirm+"'");
            }
                                          
            if(model.ChargeOrderNo !=null)
            {
            strSql.Append(" and ChargeOrderNo='"+model.ChargeOrderNo+"'");
            }
                                          
            if(model.ReportFlag !=null)
            {
            strSql.Append(" and ReportFlag='"+model.ReportFlag+"'");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


		#endregion

		public int GetCountForm(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) as CountNo ");
			strSql.Append(" FROM NRequestFormQueryDataSource ");
			if (!"".Equals(strWhere) && null != strWhere)
			{
				strSql.Append(" where " + strWhere);
			}
			ZhiFang.Common.Log.Log.Debug(strSql.ToString());
			var counto = DbHelperSQL.GetSingle(strSql.ToString());
			int count = 0;
			if (counto != null)
			{
				count = int.Parse(counto.ToString());
			}
			return count;
		}

		public DataSet GetList_FormFull(string fields, string strWhere,string sort)
		{
			StringBuilder strSql = new StringBuilder();
			
			strSql.Append("select PatNo,CName,Bed,count(1) as ItemNum ");
			
			strSql.Append(" FROM NRequestFormQueryDataSource ");
			if (!"".Equals(strWhere) && null != strWhere)
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" Group By PatNo,CName,Bed");
			if (!"".Equals(sort) && null != sort) {
				strSql.Append(" Order By " + sort);
			}
			ZhiFang.Common.Log.Log.Debug("GetList_FormFull.strSql:" + strSql.ToString());
			return DbHelperSQL.Query(strSql.ToString());
		}
		public DataSet GetList_NReportFormFull(string fields, string strWhere, string sort)
		{
			StringBuilder strSql = new StringBuilder();

			strSql.Append("select * ");

			strSql.Append(" FROM NRequestFormQueryDataSource where ReceiveFlag = 0");
			if (!"".Equals(strWhere) && null != strWhere)
			{
				strSql.Append(" and " + strWhere);
			}
			if (!"".Equals(sort) && null != sort)
			{
				strSql.Append(" Order By " + sort);
			}
			ZhiFang.Common.Log.Log.Debug("GetList_FormFull.strSql:" + strSql.ToString());
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList_FormFull(string fields, string strWhere)
		{
			StringBuilder strSql = new StringBuilder();

			strSql.Append("select * ");
            if (!string.IsNullOrEmpty(fields))
            {
				strSql.Append(","+ fields);
			}
			strSql.Append(" FROM NRequestForm");
			if (!string.IsNullOrEmpty(strWhere))
			{
				strSql.Append(" where " + strWhere);
			}
			
			ZhiFang.Common.Log.Log.Debug("GetList_FormFull.strSql:" + strSql.ToString());
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}

