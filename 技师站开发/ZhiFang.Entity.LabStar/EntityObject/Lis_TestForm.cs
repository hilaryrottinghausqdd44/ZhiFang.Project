using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisTestForm

	/// <summary>
	/// 检验单,LisTestForm object for NHibernate mapped table 'Lis_TestForm'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "检验单", ClassCName = "LisTestForm", ShortCode = "LisTestForm", Desc = "检验单")]
	public class LisTestForm : BaseEntity
	{
		#region Member Variables

		protected DateTime _gTestDate;
		protected string _gSampleNo;
		protected string _gSampleNoForOrder;
		protected int _mainStatusID;
		protected long? _statusID;
		protected long? _reportStatusID;
		protected int _iSource;
		protected string _gSampleInfo;
		protected string _sampleSpecialDesc;
		protected string _formMemo;
		protected long? _sickTypeID;
		protected long? _gSampleTypeID; 
		protected string _gSampleType;
		protected double? _age;
		protected long? _ageUnitID; 
		protected string _ageDesc;
		protected string _barCode;
		protected int? _testType;
		protected string _patNo;
		protected string _cName;
		protected long? _genderID;
		protected double? _patWeight;
		protected long? _deptID;
		protected long? _districtID;
		protected string _urgentState;
		protected string _testaim;
		protected string _testComment;
		protected string _testInfo;
		protected int _iDevelop;
		protected DateTime? _receiveTime;
		protected DateTime? _onLineTime;
		protected int _iExamine; 
		protected DateTime? _collectTime;
		protected DateTime? _inceptTime;
		protected DateTime? _testTime;
		protected DateTime? _testEndTime;
		protected int _alarmLevel;
		protected string _alarmInfo;
		protected string _zFSysCheckInfo;
		protected string _otherTester;
		protected long? _otherTesterId;
		protected string _confirmer;
		protected long? _confirmerId;
		protected DateTime? _confirmTime;
		protected string _confirmInfo;
		protected string _checker;
		protected long? _checkerID;
		protected DateTime? _checkTime;
		protected string _checkInfo;
		protected string _backUpDesc;
		protected long? _backUpDescID;
		protected double? _charge;
		protected string _removeReason;
		protected string _eSend; 
		protected int _eSendStatus;
		protected int _printCount;
		protected long? _operaterID;
		protected long? _finalOperaterID;
		protected string _mainTester;
		protected long? _mainTesterId;		
		protected long? _equipFormID;		
		protected int _redoStatus;
		protected int _testAllStatus;
		protected int _zFSysCheckStatus;
		protected long? _oldTestFormID;		
		protected int _formInfoStatus;
		protected int _migrationFlag;
		protected DateTime? _dataUpdateTime;
		protected LisBarCodeForm _sampleForm;
		protected LisOrderForm _lisOrderForm; 
		protected LisPatient _lisPatient;
		protected LBSection _lBSection;
		#endregion

		#region Constructors

		public LisTestForm() { }

		public LisTestForm(DateTime gTestDate, string gSampleNo, string gSampleNoForOrder, int mainStatusID, long statusID, long reportStatusID, int iSource, string gSampleInfo, string sampleSpecialDesc, string formMemo, long sickTypeID, long gSampleTypeID, double age, long ageUnitID, string barCode, int testType, string patNo, string cName, long genderID, double patWeight, long deptID, long districtID, string urgentState, string testaim, string testComment, string testInfo, int iDevelop, DateTime receiveTime, DateTime onLineTime, int iExamine, DateTime testTime, DateTime testEndTime, int alarmLevel, string alarmInfo, string zFSysCheckInfo, string otherTester, long otherTesterId, string confirmer, long confirmerId, DateTime confirmTime, string confirmInfo, string checker, long checkerID, DateTime checkTime, string checkInfo, string backUpDesc, long backUpDescID, double charge, string removeReason, string eSend, int printCount, long operaterID, long finalOperaterID, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, DateTime collectTime, DateTime inceptTime, string mainTester, long mainTesterId, string gSampleType, long equipFormID, int eSendStatus, int redoStatus, int testAllStatus, int zFSysCheckStatus, long oldTestFormID, string ageDesc, int formInfoStatus, int migrationFlag, LisPatient lisPatient, LisBarCodeForm sampleForm, LisOrderForm lisOrderForm, LBSection lBSection)
		{
			this._gTestDate = gTestDate;
			this._gSampleNo = gSampleNo;
			this._gSampleNoForOrder = gSampleNoForOrder;
			this._mainStatusID = mainStatusID;
			this._statusID = statusID;
			this._reportStatusID = reportStatusID;
			this._iSource = iSource;
			this._gSampleInfo = gSampleInfo;
			this._sampleSpecialDesc = sampleSpecialDesc;
			this._formMemo = formMemo;
			this._sickTypeID = sickTypeID;
			this._gSampleTypeID = gSampleTypeID;
			this._age = age;
			this._ageUnitID = ageUnitID;
			this._barCode = barCode;
			this._testType = testType;
			this._patNo = patNo;
			this._cName = cName;
			this._genderID = genderID;
			this._patWeight = patWeight;
			this._deptID = deptID;
			this._districtID = districtID;
			this._urgentState = urgentState;
			this._testaim = testaim;
			this._testComment = testComment;
			this._testInfo = testInfo;
			this._iDevelop = iDevelop;
			this._receiveTime = receiveTime;
			this._onLineTime = onLineTime;
			this._iExamine = iExamine;
			this._testTime = testTime;
			this._testEndTime = testEndTime;
			this._alarmLevel = alarmLevel;
			this._alarmInfo = alarmInfo;
			this._zFSysCheckInfo = zFSysCheckInfo;
			this._otherTester = otherTester;
			this._otherTesterId = otherTesterId;
			this._confirmer = confirmer;
			this._confirmerId = confirmerId;
			this._confirmTime = confirmTime;
			this._confirmInfo = confirmInfo;
			this._checker = checker;
			this._checkerID = checkerID;
			this._checkTime = checkTime;
			this._checkInfo = checkInfo;
			this._backUpDesc = backUpDesc;
			this._backUpDescID = backUpDescID;
			this._charge = charge;
			this._removeReason = removeReason;
			this._eSend = eSend;
			this._printCount = printCount;
			this._operaterID = operaterID;
			this._finalOperaterID = finalOperaterID;
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._collectTime = collectTime;
			this._inceptTime = inceptTime;
			this._mainTester = mainTester;
			this._mainTesterId = mainTesterId;
			this._gSampleType = gSampleType;
			this._equipFormID = equipFormID;
			this._eSendStatus = eSendStatus;
			this._redoStatus = redoStatus;
			this._testAllStatus = testAllStatus;
			this._zFSysCheckStatus = zFSysCheckStatus;
			this._oldTestFormID = oldTestFormID;
			this._ageDesc = ageDesc;
			this._formInfoStatus = formInfoStatus;
			this._migrationFlag = migrationFlag;
			this._lisPatient = lisPatient;
			this._sampleForm = sampleForm;
			this._lisOrderForm = lisOrderForm;
			this._lBSection = lBSection;
		}

		#endregion

		#region Public Properties


		/// <summary>
		/// 检测日期
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检测日期", ShortCode = "GTestDate", Desc = "检测日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime GTestDate
		{
			get { return _gTestDate; }
			set { _gTestDate = value; }
		}

		/// <summary>
		/// 小组检测编号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "样本号", ShortCode = "GSampleNo", Desc = "小组检测编号", ContextType = SysDic.All, Length = 20)]
		public virtual string GSampleNo
		{
			get { return _gSampleNo; }
			set { _gSampleNo = value; }
		}

		/// <summary>
		/// 样本号排序字段
		/// </summary>
		[DataMember]
		[DataDesc(CName = "样本号排序字段", ShortCode = "GSampleNoForOrder", Desc = "样本号排序字段", ContextType = SysDic.All, Length = 20)]
		public virtual string GSampleNoForOrder
		{
			get { return _gSampleNoForOrder; }
			set { _gSampleNoForOrder = value; }
		}

		/// <summary>
		/// 检验单主状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验单主状态", ShortCode = "MainStatusID", Desc = "检验单主状态", ContextType = SysDic.All, Length = 4)]
		public virtual int MainStatusID
		{
			get { return _mainStatusID; }
			set { _mainStatusID = value; }
		}

		/// <summary>
		/// 过程状态标志
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "过程状态标志", ShortCode = "StatusID", Desc = "过程状态标志", ContextType = SysDic.All, Length = 8)]
		public virtual long? StatusID
		{
			get { return _statusID; }
			set { _statusID = value; }
		}

		/// <summary>
		/// 结果与报告状态标志
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "结果与报告状态标志", ShortCode = "ReportStatusID", Desc = "结果与报告状态标志", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReportStatusID
		{
			get { return _reportStatusID; }
			set { _reportStatusID = value; }
		}

		/// <summary>
		/// 检验单来源
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验单来源", ShortCode = "ISource", Desc = "检验单来源", ContextType = SysDic.All, Length = 4)]
		public virtual int ISource
		{
			get { return _iSource; }
			set { _iSource = value; }
		}

		/// <summary>
		/// 小组样本描述
		/// </summary>
		[DataMember]
		[DataDesc(CName = "小组样本描述", ShortCode = "GSampleInfo", Desc = "小组样本描述", ContextType = SysDic.All, Length = 200)]
		public virtual string GSampleInfo
		{
			get { return _gSampleInfo; }
			set { _gSampleInfo = value; }
		}

		/// <summary>
		/// 特殊样本标注
		/// </summary>
		[DataMember]
		[DataDesc(CName = "特殊样本标注", ShortCode = "SampleSpecialDesc", Desc = "特殊样本标注", ContextType = SysDic.All, Length = 200)]
		public virtual string SampleSpecialDesc
		{
			get { return _sampleSpecialDesc; }
			set { _sampleSpecialDesc = value; }
		}

		/// <summary>
		/// 检验样本备注
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验样本备注", ShortCode = "FormMemo", Desc = "检验样本备注", ContextType = SysDic.All, Length = 16)]
		public virtual string FormMemo
		{
			get { return _formMemo; }
			set { _formMemo = value; }
		}

		/// <summary>
		/// 就诊类型
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "就诊类型ID", ShortCode = "SickTypeID", Desc = "就诊类型", ContextType = SysDic.All, Length = 8)]
		public virtual long? SickTypeID
		{
			get { return _sickTypeID; }
			set { _sickTypeID = value; }
		}

		/// <summary>
		/// 小组样本类型ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "小组样本类型ID", ShortCode = "GSampleTypeID", Desc = "小组样本类型ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? GSampleTypeID
		{
			get { return _gSampleTypeID; }
			set { _gSampleTypeID = value; }
		}

		/// <summary>
		/// 小组样本类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "小组样本类型", ShortCode = "GSampleType", Desc = "小组样本类型", ContextType = SysDic.All, Length = 50)]
		public virtual string GSampleType
		{
			get { return _gSampleType; }
			set { _gSampleType = value; }
		}

		/// <summary>
		/// 年龄
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄", ShortCode = "Age", Desc = "年龄", ContextType = SysDic.All, Length = 8)]
		public virtual double? Age
		{
			get { return _age; }
			set { _age = value; }
		}

		/// <summary>
		/// 年龄单位
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄单位ID", ShortCode = "AgeUnitID", Desc = "年龄单位", ContextType = SysDic.All, Length = 8)]
		public virtual long? AgeUnitID
		{
			get { return _ageUnitID; }
			set { _ageUnitID = value; }
		}

		/// <summary>
		/// 年龄描述
		/// </summary>
		[DataMember]
		[DataDesc(CName = "年龄描述", ShortCode = "AgeDesc", Desc = "年龄描述", ContextType = SysDic.All, Length = 100)]
		public virtual string AgeDesc
		{
			get { return _ageDesc; }
			set { _ageDesc = value; }
		}

		/// <summary>
		/// 条码号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "条码号", ShortCode = "BarCode", Desc = "条码号", ContextType = SysDic.All, Length = 20)]
		public virtual string BarCode
		{
			get { return _barCode; }
			set { _barCode = value; }
		}

		/// <summary>
		/// 检测类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检测类型", ShortCode = "TestType", Desc = "检测类型", ContextType = SysDic.All, Length = 4)]
		public virtual int? TestType
		{
			get { return _testType; }
			set { _testType = value; }
		}

		/// <summary>
		/// 病历号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "病历号", ShortCode = "PatNo", Desc = "病历号", ContextType = SysDic.All, Length = 20)]
		public virtual string PatNo
		{
			get { return _patNo; }
			set { _patNo = value; }
		}

		/// <summary>
		/// 姓名
		/// </summary>
		[DataMember]
		[DataDesc(CName = "姓名", ShortCode = "CName", Desc = "姓名", ContextType = SysDic.All, Length = 40)]
		public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

		/// <summary>
		/// 性别
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "性别ID", ShortCode = "GenderID", Desc = "性别", ContextType = SysDic.All, Length = 8)]
		public virtual long? GenderID
		{
			get { return _genderID; }
			set { _genderID = value; }
		}

		/// <summary>
		/// 体重
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "体重", ShortCode = "PatWeight", Desc = "体重", ContextType = SysDic.All, Length = 8)]
		public virtual double? PatWeight
		{
			get { return _patWeight; }
			set { _patWeight = value; }
		}

		/// <summary>
		/// 科室
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "科室ID", ShortCode = "DeptID", Desc = "科室", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

		/// <summary>
		/// 病区
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "病区ID", ShortCode = "DistrictID", Desc = "病区", ContextType = SysDic.All, Length = 8)]
		public virtual long? DistrictID
		{
			get { return _districtID; }
			set { _districtID = value; }
		}

		/// <summary>
		/// 加急标识
		/// </summary>
		[DataMember]
		[DataDesc(CName = "加急标识", ShortCode = "UrgentState", Desc = "加急标识", ContextType = SysDic.All, Length = 50)]
		public virtual string UrgentState
		{
			get { return _urgentState; }
			set { _urgentState = value; }
		}

		/// <summary>
		/// 送检目的
		/// </summary>
		[DataMember]
		[DataDesc(CName = "送检目的", ShortCode = "Testaim", Desc = "送检目的", ContextType = SysDic.All, Length = 200)]
		public virtual string Testaim
		{
			get { return _testaim; }
			set { _testaim = value; }
		}

		/// <summary>
		/// 检验备注
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验备注", ShortCode = "TestComment", Desc = "检验备注", ContextType = SysDic.All, Length = 500)]
		public virtual string TestComment
		{
			get { return _testComment; }
			set { _testComment = value; }
		}

		/// <summary>
		/// 检验评语
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验评语", ShortCode = "TestInfo", Desc = "检验评语", ContextType = SysDic.All, Length = 500)]
		public virtual string TestInfo
		{
			get { return _testInfo; }
			set { _testInfo = value; }
		}

		/// <summary>
		/// 检验步骤
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验步骤", ShortCode = "IDevelop", Desc = "检验步骤", ContextType = SysDic.All, Length = 4)]
		public virtual int IDevelop
		{
			get { return _iDevelop; }
			set { _iDevelop = value; }
		}

		/// <summary>
		/// 核收时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "核收时间", ShortCode = "ReceiveTime", Desc = "核收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReceiveTime
		{
			get { return _receiveTime; }
			set { _receiveTime = value; }
		}

		/// <summary>
		/// 上机时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "上机时间", ShortCode = "OnLineTime", Desc = "上机时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? OnLineTime
		{
			get { return _onLineTime; }
			set { _onLineTime = value; }
		}

		/// <summary>
		/// 检查次数
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检查次数", ShortCode = "IExamine", Desc = "检查次数", ContextType = SysDic.All, Length = 4)]
		public virtual int IExamine
		{
			get { return _iExamine; }
			set { _iExamine = value; }
		}

		/// <summary>
		/// 检验时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检验时间", ShortCode = "TestTime", Desc = "检验时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TestTime
		{
			get { return _testTime; }
			set { _testTime = value; }
		}

		/// <summary>
		/// 检验完成时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检验完成时间", ShortCode = "TestEndTime", Desc = "检验完成时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TestEndTime
		{
			get { return _testEndTime; }
			set { _testEndTime = value; }
		}

		/// <summary>
		/// 警示级别
		/// </summary>
		[DataMember]
		[DataDesc(CName = "警示级别", ShortCode = "AlarmLevel", Desc = "警示级别", ContextType = SysDic.All, Length = 4)]
		public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

		/// <summary>
		/// 警示提示
		/// </summary>
		[DataMember]
		[DataDesc(CName = "警示提示", ShortCode = "AlarmInfo", Desc = "警示提示", ContextType = SysDic.All, Length = 50)]
		public virtual string AlarmInfo
		{
			get { return _alarmInfo; }
			set { _alarmInfo = value; }
		}

		/// <summary>
		/// 系统判定不通过内容
		/// </summary>
		[DataMember]
		[DataDesc(CName = "系统判定不通过内容", ShortCode = "ZFSysCheckInfo", Desc = "系统判定不通过内容", ContextType = SysDic.All, Length = 1000)]
		public virtual string ZFSysCheckInfo
		{
			get { return _zFSysCheckInfo; }
			set { _zFSysCheckInfo = value; }
		}

		/// <summary>
		/// 其他检验者
		/// </summary>
		[DataMember]
		[DataDesc(CName = "其他检验者", ShortCode = "OtherTester", Desc = "其他检验者", ContextType = SysDic.All, Length = 50)]
		public virtual string OtherTester
		{
			get { return _otherTester; }
			set { _otherTester = value; }
		}

		/// <summary>
		/// 其他检验者Id
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "其他检验者Id", ShortCode = "OtherTesterId", Desc = "其他检验者Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? OtherTesterId
		{
			get { return _otherTesterId; }
			set { _otherTesterId = value; }
		}

		/// <summary>
		/// 初审（检验确认）者
		/// </summary>
		[DataMember]
		[DataDesc(CName = "初审（检验确认）者", ShortCode = "Confirmer", Desc = "初审（检验确认）者", ContextType = SysDic.All, Length = 50)]
		public virtual string Confirmer
		{
			get { return _confirmer; }
			set { _confirmer = value; }
		}

		/// <summary>
		/// 初审（检验确认）Id
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "初审（检验确认）Id", ShortCode = "ConfirmerId", Desc = "初审（检验确认）Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? ConfirmerId
		{
			get { return _confirmerId; }
			set { _confirmerId = value; }
		}

		/// <summary>
		/// 初审（检验确认）时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "初审（检验确认）时间", ShortCode = "ConfirmTime", Desc = "初审（检验确认）时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ConfirmTime
		{
			get { return _confirmTime; }
			set { _confirmTime = value; }
		}

		/// <summary>
		/// 初审说明
		/// </summary>
		[DataMember]
		[DataDesc(CName = "初审说明", ShortCode = "ConfirmInfo", Desc = "初审说明", ContextType = SysDic.All, Length = 500)]
		public virtual string ConfirmInfo
		{
			get { return _confirmInfo; }
			set { _confirmInfo = value; }
		}

		/// <summary>
		/// 审核者
		/// </summary>
		[DataMember]
		[DataDesc(CName = "审核者", ShortCode = "Checker", Desc = "审核者", ContextType = SysDic.All, Length = 50)]
		public virtual string Checker
		{
			get { return _checker; }
			set { _checker = value; }
		}

		/// <summary>
		/// 审核者Id
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "审核者Id", ShortCode = "CheckerID", Desc = "审核者Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? CheckerID
		{
			get { return _checkerID; }
			set { _checkerID = value; }
		}

		/// <summary>
		/// 审核时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

		/// <summary>
		/// 审核说明
		/// </summary>
		[DataMember]
		[DataDesc(CName = "审核说明", ShortCode = "CheckInfo", Desc = "审核说明", ContextType = SysDic.All, Length = 500)]
		public virtual string CheckInfo
		{
			get { return _checkInfo; }
			set { _checkInfo = value; }
		}

		/// <summary>
		/// 反审定原因
		/// </summary>
		[DataMember]
		[DataDesc(CName = "反审定原因", ShortCode = "BackUpDesc", Desc = "反审定原因", ContextType = SysDic.All, Length = 200)]
		public virtual string BackUpDesc
		{
			get { return _backUpDesc; }
			set { _backUpDesc = value; }
		}

		/// <summary>
		/// 反审定原因ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "反审定原因ID", ShortCode = "BackUpDescID", Desc = "反审定原因ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BackUpDescID
		{
			get { return _backUpDescID; }
			set { _backUpDescID = value; }
		}

		/// <summary>
		/// 收费
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "收费", ShortCode = "Charge", Desc = "收费", ContextType = SysDic.All, Length = 8)]
		public virtual double? Charge
		{
			get { return _charge; }
			set { _charge = value; }
		}

		/// <summary>
		/// 标本退回原因
		/// </summary>
		[DataMember]
		[DataDesc(CName = "标本退回原因", ShortCode = "RemoveReason", Desc = "标本退回原因", ContextType = SysDic.All, Length = 200)]
		public virtual string RemoveReason
		{
			get { return _removeReason; }
			set { _removeReason = value; }
		}

		/// <summary>
		/// 仪器审核状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器审核状态", ShortCode = "ESend", Desc = "仪器审核状态", ContextType = SysDic.All, Length = 100)]
		public virtual string ESend
		{
			get { return _eSend; }
			set { _eSend = value; }
		}

		/// <summary>
		/// 打印次数
		/// </summary>
		[DataMember]
		[DataDesc(CName = "打印次数", ShortCode = "PrintCount", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
		public virtual int PrintCount
		{
			get { return _printCount; }
			set { _printCount = value; }
		}

		/// <summary>
		/// 操作者ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "操作者ID", ShortCode = "OperaterID", Desc = "操作者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? OperaterID
		{
			get { return _operaterID; }
			set { _operaterID = value; }
		}

		/// <summary>
		/// 最终操作者ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "最终操作者ID", ShortCode = "FinalOperaterID", Desc = "最终操作者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? FinalOperaterID
		{
			get { return _finalOperaterID; }
			set { _finalOperaterID = value; }
		}

		/// <summary>
		/// 数据更新时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		/// <summary>
		/// 采样时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "CollectTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectTime
		{
			get { return _collectTime; }
			set { _collectTime = value; }
		}

		/// <summary>
		/// 签收时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "InceptTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InceptTime
		{
			get { return _inceptTime; }
			set { _inceptTime = value; }
		}

		/// <summary>
		/// 主检验者
		/// </summary>
		[DataMember]
		[DataDesc(CName = "主检验者", ShortCode = "MainTester", Desc = "主检验者", ContextType = SysDic.All, Length = 50)]
		public virtual string MainTester
		{
			get { return _mainTester; }
			set { _mainTester = value; }
		}

		/// <summary>
		/// 主检验者Id
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "主检验者Id", ShortCode = "MainTesterId", Desc = "主检验者Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? MainTesterId
		{
			get { return _mainTesterId; }
			set { _mainTesterId = value; }
		}

		/// <summary>
		/// 仪器样本单ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "仪器样本单ID", ShortCode = "EquipFormID", Desc = "仪器样本单ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipFormID
		{
			get { return _equipFormID; }
			set { _equipFormID = value; }
		}

		/// <summary>
		/// 仪器状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器状态", ShortCode = "ESendStatus", Desc = "仪器状态", ContextType = SysDic.All, Length = 4)]
		public virtual int ESendStatus
		{
			get { return _eSendStatus; }
			set { _eSendStatus = value; }
		}

		/// <summary>
		/// 复检状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "复检状态", ShortCode = "RedoStatus", Desc = "复检状态", ContextType = SysDic.All, Length = 4)]
		public virtual int RedoStatus
		{
			get { return _redoStatus; }
			set { _redoStatus = value; }
		}

		/// <summary>
		/// 检验完成状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验完成状态", ShortCode = "TestAllStatus", Desc = "检验完成状态", ContextType = SysDic.All, Length = 4)]
		public virtual int TestAllStatus
		{
			get { return _testAllStatus; }
			set { _testAllStatus = value; }
		}

		/// <summary>
		/// 系统判定状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "系统判定状态", ShortCode = "ZFSysCheckStatus", Desc = "系统判定状态", ContextType = SysDic.All, Length = 4)]
		public virtual int ZFSysCheckStatus
		{
			get { return _zFSysCheckStatus; }
			set { _zFSysCheckStatus = value; }
		}

		/// <summary>
		/// 原样本单ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "原样本单ID", ShortCode = "OldTestFormID", Desc = "原样本单ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? OldTestFormID
		{
			get { return _oldTestFormID; }
			set { _oldTestFormID = value; }
		}

		/// <summary>
		/// 检验单完成状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验单完成状态", ShortCode = "FormInfoStatus", Desc = "检验单完成状态", ContextType = SysDic.All, Length = 4)]
		public virtual int FormInfoStatus
		{
			get { return _formInfoStatus; }
			set { _formInfoStatus = value; }
		}

		/// <summary>
		/// 迁移标记
		/// </summary>
		[DataMember]
		[DataDesc(CName = "迁移标记", ShortCode = "MigrationFlag", Desc = "迁移标记", ContextType = SysDic.All, Length = 4)]
		public virtual int MigrationFlag
		{
			get { return _migrationFlag; }
			set { _migrationFlag = value; }
		}

		/// <summary>
		/// 患者就诊信息
		/// </summary>
		[DataMember]
		[DataDesc(CName = "患者就诊信息", ShortCode = "LisPatient", Desc = "患者就诊信息")]
		public virtual LisPatient LisPatient
		{
			get { return _lisPatient; }
			set { _lisPatient = value; }
		}

		/// <summary>
		/// 样本单
		/// </summary>
		[DataMember]
		[DataDesc(CName = "样本单", ShortCode = "SampleForm", Desc = "")]
		public virtual LisBarCodeForm SampleForm
		{
			get { return _sampleForm; }
			set { _sampleForm = value; }
		}

		/// <summary>
		/// 医嘱单
		/// </summary>
		[DataMember]
		[DataDesc(CName = "医嘱单", ShortCode = "LisOrderForm", Desc = "")]
		public virtual LisOrderForm LisOrderForm
		{
			get { return _lisOrderForm; }
			set { _lisOrderForm = value; }
		}

		/// <summary>
		/// 检验小组
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验小组", ShortCode = "LBSection", Desc = "")]
		public virtual LBSection LBSection
		{
			get { return _lBSection; }
			set { _lBSection = value; }
		}

		/// <summary>
		/// 临时字段-样本单项目列表
		/// </summary>
		[DataMember]
		[DataDesc(CName = "样本单项目列表", ShortCode = "ItemNameList", Desc = "样本单项目列表")]
		public virtual string ItemNameList { get; set; }

		/// <summary>
		/// 临时字段-样本单是否新增
		/// </summary>
		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsAddTestForm", Desc = "样本单是否新增")]
		public virtual bool IsAddTestForm { get; set; }

		#endregion
	}
	#endregion
}