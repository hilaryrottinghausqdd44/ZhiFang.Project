using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region MEGroupSampleForm

	/// <summary>
	/// MEGroupSampleForm object for NHibernate mapped table 'ME_GroupSampleForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "MEGroupSampleForm", ShortCode = "MEGroupSampleForm", Desc = "")]
	public class MEGroupSampleForm : BaseEntity
	{
		#region Member Variables
		
        protected long? _orderFormID;
        protected long? _sampleStatusID;
        protected int _sectionNo;
        protected string _serialNo;
        protected DateTime? _gTestDate;
        protected string _gSampleNo;
        protected int _gTestNo;
        protected int _sampleTypeNo;
        protected string _gSampleInfo;
        protected string _testComment;
        protected int _distributeFlag;
        protected int _isPrint;
        protected int _printCount;
        protected int _isUpload;
        protected string _fFormMemo;
        protected string _fZDY1;
        protected string _fZDY2;
        protected string _fZDY3;
        protected string _fZDY4;
        protected string _fZDY5;
        protected int _mainState;
        protected int _isHasNuclearAdmission;
        protected int _isOnMachine;
        protected int _isCancelConfirmedOrAudited;
        protected int _commState;
        protected DateTime? _dataUpdateTime;
        protected bool _deleteFlag;
        protected bool _migrationFlag;
        protected int _positiveFlag;
        protected string _eSampleNo;
        protected string _gBarCode;
        protected string _mainTester;
        protected long? _mainTesterId;
        protected string _otherTester;
        protected string _confirmer;
        protected long? _confirmerId;
        protected DateTime? _confirmeDate;
        protected string _examiner;
        protected long? _examinerId;
        protected DateTime? _examineDate;
        protected string _patNo;
        protected string _cName;
        protected int _genderNo;
        protected DateTime? _birthday;
        protected double _age;
        protected int _ageUnitNo;
        protected int _folkNo;
        protected int _districtNo;
        protected int _wardNo;
        protected string _bed;
        protected int _deptNo;
        protected int _doctor;
        protected string _diag;
        protected int _diagno;
        protected int _chargeNo;
        protected decimal _charge;
        protected string _collecter;
        protected DateTime? _collectDate;
        protected DateTime? _collectTime;
        protected string _formMemo;
        protected string _sickorder;
        protected string _chargeflag;
        protected int _jztype;
        protected string _formComment;
        protected string _incepter;
        protected DateTime? _inceptTime;
        protected DateTime? _inceptDate;
        protected int _testTypeNo;
        protected string _collecterID;
        protected string _oldSerialNo;
        protected string _countNodesFormSource;
        protected int _clientno;
        protected string _urgentState;
        protected int _dispenseFlag;
        protected string _jytype;
        protected string _nurseSender;
        protected DateTime? _nurseSendTime;
        protected string _nurseSendCarrier;
        protected string _nurseSendNo;
        protected int _foreignSendFlag;
        protected string _hisDoctorId;
        protected string _hisDoctorPhoneCode;
        protected int _lisDoctorNo;
        protected string _patState;
        protected string _mergeno;
        protected int _hospitalizedTimes;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected string _zDY6;
        protected string _zDY7;
        protected string _zDY8;
        protected string _zDY9;
        protected string _zDY10;
        protected string _zDY11;
        protected string _zDY12;
        protected string _zDY13;
        protected string _zDY14;
        protected string _zDY15;
        protected string _zDY16;
        protected string _zDY17;
        protected string _zDY18;
        protected string _zDY19;
        protected string _zDY20;
        protected string _collectPart;
        protected int _signflag;
        protected int _iDevelop;
        protected int _iWarningTime;
        protected DateTime? _flagDateDelete;
        protected string _gSampleNoForOrder;
        protected int _reexamined;
        protected int _reportType;
        protected DateTime? _receiveTime;
        protected string _zFDelInfo;
        protected int _listPrintCount;
        protected long? _groupSampleFormPID;
        protected int _iExamineByHand;
        protected string _formComment2;
        protected string _sampleSpecialDesc;
        protected string _unionFrom;
        protected byte[] _formResultInfo;
        protected string _dataAddMan;
        protected string _receiveMan;
        protected DateTime? _testTime;
        protected string _testMethod;
        protected string _testPurpose;
        protected long? _finalOperater;
        protected string _reportRemark;
        protected bool _isByHand;
        protected bool _isReceive;
        protected string _sampleType2;
        protected int _bCrisis;
        protected int _sumPrintFlag;
        protected int _exceptFlag;
        protected string _ageDesc;
        protected int _autoPrintCount;
        protected int _iQSPrintCount;
        protected int _afterExamineFlag;
        protected int _afterConFirmFlag;
        protected double _weight;
        protected string _weightDesc;
        protected DateTime? _dispenseTime;
        protected string _dispenseUserNo;
        protected string _dispenseUserName;
        protected string _patNoF;
        protected string _fZDY6;
        protected string _fZDY7;
        protected string _fZDY8;
        protected string _fZDY9;
        protected string _fZDY10;
        protected string _eAchivPosition;
        protected string _ePosition;
        protected DateTime? _onlineDate;
        protected string _examineDocID;
        protected string _examineDoctor;
        protected DateTime? _examineDocDate;
        protected string _eSend;
        protected int _iPositiveCard;
        protected int _redoFlag;
        protected int _bAllResultTest;
        protected int _bZFSysCheck;
        protected string _zFSysCheckInfo;
        protected string _checkInfo;
        protected string _checkInfoExamine;
        protected int _iConfirmByHand;
        protected string _reportPlaceTxt;
        protected DateTime? _lastExamineDate;
        protected DateTime? _cancelExamineDate;
        protected int _isCancelScopeAudited;
        protected int _microFlag;
        protected int _antiFlag;
        protected DateTime? _printDateTime;
        protected string _printOper;
        protected DateTime? _orderTime;
        protected string _iDCardNo;
        protected string _sampleQuality;
        protected string _formMemo2;
        protected int _isCopy;

		#endregion

		#region Constructors

		public MEGroupSampleForm() { }

		public MEGroupSampleForm( long labID, long orderFormID, long sampleStatusID, int sectionNo, string serialNo, DateTime gTestDate, string gSampleNo, int gTestNo, int sampleTypeNo, string gSampleInfo, string testComment, int distributeFlag, int isPrint, int printCount, int isUpload, string fFormMemo, string fZDY1, string fZDY2, string fZDY3, string fZDY4, string fZDY5, int mainState, int isHasNuclearAdmission, int isOnMachine, int isCancelConfirmedOrAudited, int commState, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool deleteFlag, bool migrationFlag, int positiveFlag, string eSampleNo, string gBarCode, string mainTester, long mainTesterId, string otherTester, string confirmer, long confirmerId, DateTime confirmeDate, string examiner, long examinerId, DateTime examineDate, string patNo, string cName, int genderNo, DateTime birthday, double age, int ageUnitNo, int folkNo, int districtNo, int wardNo, string bed, int deptNo, int doctor, string diag, int diagno, int chargeNo, decimal charge, string collecter, DateTime collectDate, DateTime collectTime, string formMemo, string sickorder, string chargeflag, int jztype, string formComment, string incepter, DateTime inceptTime, DateTime inceptDate, int testTypeNo, string collecterID, string oldSerialNo, string countNodesFormSource, int clientno, string urgentState, int dispenseFlag, string jytype, string nurseSender, DateTime nurseSendTime, string nurseSendCarrier, string nurseSendNo, int foreignSendFlag, string hisDoctorId, string hisDoctorPhoneCode, int lisDoctorNo, string patState, string mergeno, int hospitalizedTimes, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, string zDY6, string zDY7, string zDY8, string zDY9, string zDY10, string zDY11, string zDY12, string zDY13, string zDY14, string zDY15, string zDY16, string zDY17, string zDY18, string zDY19, string zDY20, string collectPart, int signflag, int iDevelop, int iWarningTime, DateTime flagDateDelete, string gSampleNoForOrder, int reexamined, int reportType, DateTime receiveTime, string zFDelInfo, int listPrintCount, long groupSampleFormPID, int iExamineByHand, string formComment2, string sampleSpecialDesc, string unionFrom, byte[] formResultInfo, string dataAddMan, string receiveMan, DateTime testTime, string testMethod, string testPurpose, long finalOperater, string reportRemark, bool isByHand, bool isReceive, string sampleType2, int bCrisis, int sumPrintFlag, int exceptFlag, string ageDesc, int autoPrintCount, int iQSPrintCount, int afterExamineFlag, int afterConFirmFlag, double weight, string weightDesc, DateTime dispenseTime, string dispenseUserNo, string dispenseUserName, string patNoF, string fZDY6, string fZDY7, string fZDY8, string fZDY9, string fZDY10, string eAchivPosition, string ePosition, DateTime onlineDate, string examineDocID, string examineDoctor, DateTime examineDocDate, string eSend, int iPositiveCard, int redoFlag, int bAllResultTest, int bZFSysCheck, string zFSysCheckInfo, string checkInfo, string checkInfoExamine, int iConfirmByHand, string reportPlaceTxt, DateTime lastExamineDate, DateTime cancelExamineDate, int isCancelScopeAudited, int microFlag, int antiFlag, DateTime printDateTime, string printOper, DateTime orderTime, string iDCardNo, string sampleQuality, string formMemo2, int isCopy )
		{
			this._labID = labID;
			this._orderFormID = orderFormID;
			this._sampleStatusID = sampleStatusID;
			this._sectionNo = sectionNo;
			this._serialNo = serialNo;
			this._gTestDate = gTestDate;
			this._gSampleNo = gSampleNo;
			this._gTestNo = gTestNo;
			this._sampleTypeNo = sampleTypeNo;
			this._gSampleInfo = gSampleInfo;
			this._testComment = testComment;
			this._distributeFlag = distributeFlag;
			this._isPrint = isPrint;
			this._printCount = printCount;
			this._isUpload = isUpload;
			this._fFormMemo = fFormMemo;
			this._fZDY1 = fZDY1;
			this._fZDY2 = fZDY2;
			this._fZDY3 = fZDY3;
			this._fZDY4 = fZDY4;
			this._fZDY5 = fZDY5;
			this._mainState = mainState;
			this._isHasNuclearAdmission = isHasNuclearAdmission;
			this._isOnMachine = isOnMachine;
			this._isCancelConfirmedOrAudited = isCancelConfirmedOrAudited;
			this._commState = commState;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._deleteFlag = deleteFlag;
			this._migrationFlag = migrationFlag;
			this._positiveFlag = positiveFlag;
			this._eSampleNo = eSampleNo;
			this._gBarCode = gBarCode;
			this._mainTester = mainTester;
			this._mainTesterId = mainTesterId;
			this._otherTester = otherTester;
			this._confirmer = confirmer;
			this._confirmerId = confirmerId;
			this._confirmeDate = confirmeDate;
			this._examiner = examiner;
			this._examinerId = examinerId;
			this._examineDate = examineDate;
			this._patNo = patNo;
			this._cName = cName;
			this._genderNo = genderNo;
			this._birthday = birthday;
			this._age = age;
			this._ageUnitNo = ageUnitNo;
			this._folkNo = folkNo;
			this._districtNo = districtNo;
			this._wardNo = wardNo;
			this._bed = bed;
			this._deptNo = deptNo;
			this._doctor = doctor;
			this._diag = diag;
			this._diagno = diagno;
			this._chargeNo = chargeNo;
			this._charge = charge;
			this._collecter = collecter;
			this._collectDate = collectDate;
			this._collectTime = collectTime;
			this._formMemo = formMemo;
			this._sickorder = sickorder;
			this._chargeflag = chargeflag;
			this._jztype = jztype;
			this._formComment = formComment;
			this._incepter = incepter;
			this._inceptTime = inceptTime;
			this._inceptDate = inceptDate;
			this._testTypeNo = testTypeNo;
			this._collecterID = collecterID;
			this._oldSerialNo = oldSerialNo;
			this._countNodesFormSource = countNodesFormSource;
			this._clientno = clientno;
			this._urgentState = urgentState;
			this._dispenseFlag = dispenseFlag;
			this._jytype = jytype;
			this._nurseSender = nurseSender;
			this._nurseSendTime = nurseSendTime;
			this._nurseSendCarrier = nurseSendCarrier;
			this._nurseSendNo = nurseSendNo;
			this._foreignSendFlag = foreignSendFlag;
			this._hisDoctorId = hisDoctorId;
			this._hisDoctorPhoneCode = hisDoctorPhoneCode;
			this._lisDoctorNo = lisDoctorNo;
			this._patState = patState;
			this._mergeno = mergeno;
			this._hospitalizedTimes = hospitalizedTimes;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._zDY4 = zDY4;
			this._zDY5 = zDY5;
			this._zDY6 = zDY6;
			this._zDY7 = zDY7;
			this._zDY8 = zDY8;
			this._zDY9 = zDY9;
			this._zDY10 = zDY10;
			this._zDY11 = zDY11;
			this._zDY12 = zDY12;
			this._zDY13 = zDY13;
			this._zDY14 = zDY14;
			this._zDY15 = zDY15;
			this._zDY16 = zDY16;
			this._zDY17 = zDY17;
			this._zDY18 = zDY18;
			this._zDY19 = zDY19;
			this._zDY20 = zDY20;
			this._collectPart = collectPart;
			this._signflag = signflag;
			this._iDevelop = iDevelop;
			this._iWarningTime = iWarningTime;
			this._flagDateDelete = flagDateDelete;
			this._gSampleNoForOrder = gSampleNoForOrder;
			this._reexamined = reexamined;
			this._reportType = reportType;
			this._receiveTime = receiveTime;
			this._zFDelInfo = zFDelInfo;
			this._listPrintCount = listPrintCount;
			this._groupSampleFormPID = groupSampleFormPID;
			this._iExamineByHand = iExamineByHand;
			this._formComment2 = formComment2;
			this._sampleSpecialDesc = sampleSpecialDesc;
			this._unionFrom = unionFrom;
			this._formResultInfo = formResultInfo;
			this._dataAddMan = dataAddMan;
			this._receiveMan = receiveMan;
			this._testTime = testTime;
			this._testMethod = testMethod;
			this._testPurpose = testPurpose;
			this._finalOperater = finalOperater;
			this._reportRemark = reportRemark;
			this._isByHand = isByHand;
			this._isReceive = isReceive;
			this._sampleType2 = sampleType2;
			this._bCrisis = bCrisis;
			this._sumPrintFlag = sumPrintFlag;
			this._exceptFlag = exceptFlag;
			this._ageDesc = ageDesc;
			this._autoPrintCount = autoPrintCount;
			this._iQSPrintCount = iQSPrintCount;
			this._afterExamineFlag = afterExamineFlag;
			this._afterConFirmFlag = afterConFirmFlag;
			this._weight = weight;
			this._weightDesc = weightDesc;
			this._dispenseTime = dispenseTime;
			this._dispenseUserNo = dispenseUserNo;
			this._dispenseUserName = dispenseUserName;
			this._patNoF = patNoF;
			this._fZDY6 = fZDY6;
			this._fZDY7 = fZDY7;
			this._fZDY8 = fZDY8;
			this._fZDY9 = fZDY9;
			this._fZDY10 = fZDY10;
			this._eAchivPosition = eAchivPosition;
			this._ePosition = ePosition;
			this._onlineDate = onlineDate;
			this._examineDocID = examineDocID;
			this._examineDoctor = examineDoctor;
			this._examineDocDate = examineDocDate;
			this._eSend = eSend;
			this._iPositiveCard = iPositiveCard;
			this._redoFlag = redoFlag;
			this._bAllResultTest = bAllResultTest;
			this._bZFSysCheck = bZFSysCheck;
			this._zFSysCheckInfo = zFSysCheckInfo;
			this._checkInfo = checkInfo;
			this._checkInfoExamine = checkInfoExamine;
			this._iConfirmByHand = iConfirmByHand;
			this._reportPlaceTxt = reportPlaceTxt;
			this._lastExamineDate = lastExamineDate;
			this._cancelExamineDate = cancelExamineDate;
			this._isCancelScopeAudited = isCancelScopeAudited;
			this._microFlag = microFlag;
			this._antiFlag = antiFlag;
			this._printDateTime = printDateTime;
			this._printOper = printOper;
			this._orderTime = orderTime;
			this._iDCardNo = iDCardNo;
			this._sampleQuality = sampleQuality;
			this._formMemo2 = formMemo2;
			this._isCopy = isCopy;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrderFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? OrderFormID
		{
			get { return _orderFormID; }
			set { _orderFormID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SampleStatusID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? SampleStatusID
		{
			get { return _sampleStatusID; }
			set { _sampleStatusID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionNo
		{
			get { return _sectionNo; }
			set { _sectionNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SerialNo", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string SerialNo
		{
			get { return _serialNo; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for SerialNo", value, value.ToString());
				_serialNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GTestDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GTestDate
		{
			get { return _gTestDate; }
			set { _gTestDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GSampleNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string GSampleNo
		{
			get { return _gSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for GSampleNo", value, value.ToString());
				_gSampleNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GTestNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int GTestNo
		{
			get { return _gTestNo; }
			set { _gTestNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set { _sampleTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GSampleInfo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string GSampleInfo
		{
			get { return _gSampleInfo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for GSampleInfo", value, value.ToString());
				_gSampleInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestComment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string TestComment
		{
			get { return _testComment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for TestComment", value, value.ToString());
				_testComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DistributeFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DistributeFlag
		{
			get { return _distributeFlag; }
			set { _distributeFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsPrint", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPrint
		{
			get { return _isPrint; }
			set { _isPrint = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
		{
			get { return _printCount; }
			set { _printCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUpload", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUpload
		{
			get { return _isUpload; }
			set { _isUpload = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FFormMemo", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string FFormMemo
		{
			get { return _fFormMemo; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for FFormMemo", value, value.ToString());
				_fFormMemo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY1", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY1
		{
			get { return _fZDY1; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY1", value, value.ToString());
				_fZDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY2", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY2
		{
			get { return _fZDY2; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY2", value, value.ToString());
				_fZDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY3", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY3
		{
			get { return _fZDY3; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY3", value, value.ToString());
				_fZDY3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY4", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY4
		{
			get { return _fZDY4; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY4", value, value.ToString());
				_fZDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY5", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY5
		{
			get { return _fZDY5; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY5", value, value.ToString());
				_fZDY5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MainState", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MainState
		{
			get { return _mainState; }
			set { _mainState = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsHasNuclearAdmission", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsHasNuclearAdmission
		{
			get { return _isHasNuclearAdmission; }
			set { _isHasNuclearAdmission = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsOnMachine", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsOnMachine
		{
			get { return _isOnMachine; }
			set { _isOnMachine = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCancelConfirmedOrAudited", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCancelConfirmedOrAudited
		{
			get { return _isCancelConfirmedOrAudited; }
			set { _isCancelConfirmedOrAudited = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CommState", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CommState
		{
			get { return _commState; }
			set { _commState = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeleteFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool DeleteFlag
		{
			get { return _deleteFlag; }
			set { _deleteFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MigrationFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool MigrationFlag
		{
			get { return _migrationFlag; }
			set { _migrationFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PositiveFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PositiveFlag
		{
			get { return _positiveFlag; }
			set { _positiveFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ESampleNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ESampleNo
		{
			get { return _eSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ESampleNo", value, value.ToString());
				_eSampleNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GBarCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string GBarCode
		{
			get { return _gBarCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for GBarCode", value, value.ToString());
				_gBarCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MainTester", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string MainTester
		{
			get { return _mainTester; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for MainTester", value, value.ToString());
				_mainTester = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MainTesterId", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? MainTesterId
		{
			get { return _mainTesterId; }
			set { _mainTesterId = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OtherTester", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string OtherTester
		{
			get { return _otherTester; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for OtherTester", value, value.ToString());
				_otherTester = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Confirmer", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Confirmer
		{
			get { return _confirmer; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Confirmer", value, value.ToString());
				_confirmer = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ConfirmerId", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? ConfirmerId
		{
			get { return _confirmerId; }
			set { _confirmerId = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ConfirmeDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ConfirmeDate
		{
			get { return _confirmeDate; }
			set { _confirmeDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Examiner", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Examiner
		{
			get { return _examiner; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Examiner", value, value.ToString());
				_examiner = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ExaminerId", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? ExaminerId
		{
			get { return _examinerId; }
			set { _examinerId = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ExamineDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ExamineDate
		{
			get { return _examineDate; }
			set { _examineDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PatNo
		{
			get { return _patNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PatNo", value, value.ToString());
				_patNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GenderNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int GenderNo
		{
			get { return _genderNo; }
			set { _genderNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Birthday", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Age", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Age
		{
			get { return _age; }
			set { _age = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AgeUnitNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AgeUnitNo
		{
			get { return _ageUnitNo; }
			set { _ageUnitNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FolkNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int FolkNo
		{
			get { return _folkNo; }
			set { _folkNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DistrictNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DistrictNo
		{
			get { return _districtNo; }
			set { _districtNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WardNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int WardNo
		{
			get { return _wardNo; }
			set { _wardNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bed", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Bed
		{
			get { return _bed; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Bed", value, value.ToString());
				_bed = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DeptNo
		{
			get { return _deptNo; }
			set { _deptNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Doctor", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Doctor
		{
			get { return _doctor; }
			set { _doctor = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Diag", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Diag
		{
			get { return _diag; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Diag", value, value.ToString());
				_diag = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Diagno", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Diagno
		{
			get { return _diagno; }
			set { _diagno = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeNo
		{
			get { return _chargeNo; }
			set { _chargeNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Charge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Charge
		{
			get { return _charge; }
			set { _charge = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Collecter", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Collecter
		{
			get { return _collecter; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Collecter", value, value.ToString());
				_collecter = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CollectDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectDate
		{
			get { return _collectDate; }
			set { _collectDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectTime
		{
			get { return _collectTime; }
			set { _collectTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormMemo", Desc = "", ContextType = SysDic.All, Length = 400)]
        public virtual string FormMemo
		{
			get { return _formMemo; }
			set
			{
				if ( value != null && value.Length > 400)
					throw new ArgumentOutOfRangeException("Invalid value for FormMemo", value, value.ToString());
				_formMemo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Sickorder", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Sickorder
		{
			get { return _sickorder; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Sickorder", value, value.ToString());
				_sickorder = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Chargeflag", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Chargeflag
		{
			get { return _chargeflag; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Chargeflag", value, value.ToString());
				_chargeflag = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Jztype", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Jztype
		{
			get { return _jztype; }
			set { _jztype = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormComment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string FormComment
		{
			get { return _formComment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for FormComment", value, value.ToString());
				_formComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Incepter", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Incepter
		{
			get { return _incepter; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Incepter", value, value.ToString());
				_incepter = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InceptTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InceptTime
		{
			get { return _inceptTime; }
			set { _inceptTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InceptDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InceptDate
		{
			get { return _inceptDate; }
			set { _inceptDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestTypeNo
		{
			get { return _testTypeNo; }
			set { _testTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CollecterID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CollecterID
		{
			get { return _collecterID; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CollecterID", value, value.ToString());
				_collecterID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OldSerialNo", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string OldSerialNo
		{
			get { return _oldSerialNo; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for OldSerialNo", value, value.ToString());
				_oldSerialNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CountNodesFormSource", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual string CountNodesFormSource
		{
			get { return _countNodesFormSource; }
			set
			{
				if ( value != null && value.Length > 1)
					throw new ArgumentOutOfRangeException("Invalid value for CountNodesFormSource", value, value.ToString());
				_countNodesFormSource = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Clientno", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Clientno
		{
			get { return _clientno; }
			set { _clientno = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UrgentState", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string UrgentState
		{
			get { return _urgentState; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UrgentState", value, value.ToString());
				_urgentState = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispenseFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispenseFlag
		{
			get { return _dispenseFlag; }
			set { _dispenseFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Jytype", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Jytype
		{
			get { return _jytype; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Jytype", value, value.ToString());
				_jytype = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NurseSender", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string NurseSender
		{
			get { return _nurseSender; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for NurseSender", value, value.ToString());
				_nurseSender = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "NurseSendTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? NurseSendTime
		{
			get { return _nurseSendTime; }
			set { _nurseSendTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NurseSendCarrier", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string NurseSendCarrier
		{
			get { return _nurseSendCarrier; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for NurseSendCarrier", value, value.ToString());
				_nurseSendCarrier = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NurseSendNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string NurseSendNo
		{
			get { return _nurseSendNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for NurseSendNo", value, value.ToString());
				_nurseSendNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ForeignSendFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ForeignSendFlag
		{
			get { return _foreignSendFlag; }
			set { _foreignSendFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDoctorId", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisDoctorId
		{
			get { return _hisDoctorId; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisDoctorId", value, value.ToString());
				_hisDoctorId = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDoctorPhoneCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisDoctorPhoneCode
		{
			get { return _hisDoctorPhoneCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisDoctorPhoneCode", value, value.ToString());
				_hisDoctorPhoneCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisDoctorNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int LisDoctorNo
		{
			get { return _lisDoctorNo; }
			set { _lisDoctorNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatState", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PatState
		{
			get { return _patState; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PatState", value, value.ToString());
				_patState = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Mergeno", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Mergeno
		{
			get { return _mergeno; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Mergeno", value, value.ToString());
				_mergeno = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalizedTimes", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int HospitalizedTimes
		{
			get { return _hospitalizedTimes; }
			set { _hospitalizedTimes = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY1", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZDY1
		{
			get { return _zDY1; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
				_zDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY2", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZDY2
		{
			get { return _zDY2; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
				_zDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY3", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZDY3
		{
			get { return _zDY3; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
				_zDY3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY4", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZDY4
		{
			get { return _zDY4; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
				_zDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY5", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY5
		{
			get { return _zDY5; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
				_zDY5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY6", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string ZDY6
		{
			get { return _zDY6; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY6", value, value.ToString());
				_zDY6 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY7", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string ZDY7
		{
			get { return _zDY7; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY7", value, value.ToString());
				_zDY7 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY8", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string ZDY8
		{
			get { return _zDY8; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY8", value, value.ToString());
				_zDY8 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY9", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string ZDY9
		{
			get { return _zDY9; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY9", value, value.ToString());
				_zDY9 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY10", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string ZDY10
		{
			get { return _zDY10; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY10", value, value.ToString());
				_zDY10 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY11", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string ZDY11
		{
			get { return _zDY11; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY11", value, value.ToString());
				_zDY11 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY12", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY12
		{
			get { return _zDY12; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY12", value, value.ToString());
				_zDY12 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY13", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY13
		{
			get { return _zDY13; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY13", value, value.ToString());
				_zDY13 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY14", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY14
		{
			get { return _zDY14; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY14", value, value.ToString());
				_zDY14 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY15", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY15
		{
			get { return _zDY15; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY15", value, value.ToString());
				_zDY15 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY16", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY16
		{
			get { return _zDY16; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY16", value, value.ToString());
				_zDY16 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY17", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY17
		{
			get { return _zDY17; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY17", value, value.ToString());
				_zDY17 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY18", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY18
		{
			get { return _zDY18; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY18", value, value.ToString());
				_zDY18 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY19", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY19
		{
			get { return _zDY19; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY19", value, value.ToString());
				_zDY19 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY20", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY20
		{
			get { return _zDY20; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY20", value, value.ToString());
				_zDY20 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CollectPart", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CollectPart
		{
			get { return _collectPart; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for CollectPart", value, value.ToString());
				_collectPart = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Signflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Signflag
		{
			get { return _signflag; }
			set { _signflag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IDevelop", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IDevelop
		{
			get { return _iDevelop; }
			set { _iDevelop = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IWarningTime", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IWarningTime
		{
			get { return _iWarningTime; }
			set { _iWarningTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FlagDateDelete", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? FlagDateDelete
		{
			get { return _flagDateDelete; }
			set { _flagDateDelete = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GSampleNoForOrder", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string GSampleNoForOrder
		{
			get { return _gSampleNoForOrder; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for GSampleNoForOrder", value, value.ToString());
				_gSampleNoForOrder = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Reexamined", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Reexamined
		{
			get { return _reexamined; }
			set { _reexamined = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportType
		{
			get { return _reportType; }
			set { _reportType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReceiveTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReceiveTime
		{
			get { return _receiveTime; }
			set { _receiveTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZFDelInfo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ZFDelInfo
		{
			get { return _zFDelInfo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ZFDelInfo", value, value.ToString());
				_zFDelInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ListPrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ListPrintCount
		{
			get { return _listPrintCount; }
			set { _listPrintCount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GroupSampleFormPID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? GroupSampleFormPID
		{
			get { return _groupSampleFormPID; }
			set { _groupSampleFormPID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IExamineByHand", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IExamineByHand
		{
			get { return _iExamineByHand; }
			set { _iExamineByHand = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormComment2", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string FormComment2
		{
			get { return _formComment2; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for FormComment2", value, value.ToString());
				_formComment2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleSpecialDesc", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleSpecialDesc
		{
			get { return _sampleSpecialDesc; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleSpecialDesc", value, value.ToString());
				_sampleSpecialDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UnionFrom", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UnionFrom
		{
			get { return _unionFrom; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UnionFrom", value, value.ToString());
				_unionFrom = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormResultInfo", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] FormResultInfo
		{
			get { return _formResultInfo; }
			set { _formResultInfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DataAddMan", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string DataAddMan
		{
			get { return _dataAddMan; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for DataAddMan", value, value.ToString());
				_dataAddMan = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReceiveMan", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string ReceiveMan
		{
			get { return _receiveMan; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveMan", value, value.ToString());
				_receiveMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TestTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TestTime
		{
			get { return _testTime; }
			set { _testTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestMethod", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string TestMethod
		{
			get { return _testMethod; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for TestMethod", value, value.ToString());
				_testMethod = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestPurpose", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string TestPurpose
		{
			get { return _testPurpose; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for TestPurpose", value, value.ToString());
				_testPurpose = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FinalOperater", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? FinalOperater
		{
			get { return _finalOperater; }
			set { _finalOperater = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportRemark", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ReportRemark
		{
			get { return _reportRemark; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ReportRemark", value, value.ToString());
				_reportRemark = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsByHand", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsByHand
		{
			get { return _isByHand; }
			set { _isByHand = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsReceive", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReceive
		{
			get { return _isReceive; }
			set { _isReceive = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleType2", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string SampleType2
		{
			get { return _sampleType2; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SampleType2", value, value.ToString());
				_sampleType2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BCrisis", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BCrisis
		{
			get { return _bCrisis; }
			set { _bCrisis = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SumPrintFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SumPrintFlag
		{
			get { return _sumPrintFlag; }
			set { _sumPrintFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ExceptFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ExceptFlag
		{
			get { return _exceptFlag; }
			set { _exceptFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AgeDesc", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string AgeDesc
		{
			get { return _ageDesc; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for AgeDesc", value, value.ToString());
				_ageDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AutoPrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AutoPrintCount
		{
			get { return _autoPrintCount; }
			set { _autoPrintCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IQSPrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IQSPrintCount
		{
			get { return _iQSPrintCount; }
			set { _iQSPrintCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AfterExamineFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AfterExamineFlag
		{
			get { return _afterExamineFlag; }
			set { _afterExamineFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AfterConFirmFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AfterConFirmFlag
		{
			get { return _afterConFirmFlag; }
			set { _afterConFirmFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Weight", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Weight
		{
			get { return _weight; }
			set { _weight = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WeightDesc", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string WeightDesc
		{
			get { return _weightDesc; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for WeightDesc", value, value.ToString());
				_weightDesc = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DispenseTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DispenseTime
		{
			get { return _dispenseTime; }
			set { _dispenseTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispenseUserNo", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string DispenseUserNo
		{
			get { return _dispenseUserNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for DispenseUserNo", value, value.ToString());
				_dispenseUserNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispenseUserName", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string DispenseUserName
		{
			get { return _dispenseUserName; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for DispenseUserName", value, value.ToString());
				_dispenseUserName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatNoF", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PatNoF
		{
			get { return _patNoF; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PatNoF", value, value.ToString());
				_patNoF = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY6", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY6
		{
			get { return _fZDY6; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY6", value, value.ToString());
				_fZDY6 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY7", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY7
		{
			get { return _fZDY7; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY7", value, value.ToString());
				_fZDY7 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY8", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY8
		{
			get { return _fZDY8; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY8", value, value.ToString());
				_fZDY8 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY9", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY9
		{
			get { return _fZDY9; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY9", value, value.ToString());
				_fZDY9 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FZDY10", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FZDY10
		{
			get { return _fZDY10; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FZDY10", value, value.ToString());
				_fZDY10 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EAchivPosition", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EAchivPosition
		{
			get { return _eAchivPosition; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EAchivPosition", value, value.ToString());
				_eAchivPosition = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EPosition", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EPosition
		{
			get { return _ePosition; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EPosition", value, value.ToString());
				_ePosition = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OnlineDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? OnlineDate
		{
			get { return _onlineDate; }
			set { _onlineDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ExamineDocID", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string ExamineDocID
		{
			get { return _examineDocID; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for ExamineDocID", value, value.ToString());
				_examineDocID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ExamineDoctor", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string ExamineDoctor
		{
			get { return _examineDoctor; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for ExamineDoctor", value, value.ToString());
				_examineDoctor = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ExamineDocDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ExamineDocDate
		{
			get { return _examineDocDate; }
			set { _examineDocDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ESend", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string ESend
		{
			get { return _eSend; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for ESend", value, value.ToString());
				_eSend = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IPositiveCard", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IPositiveCard
		{
			get { return _iPositiveCard; }
			set { _iPositiveCard = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RedoFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int RedoFlag
		{
			get { return _redoFlag; }
			set { _redoFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BAllResultTest", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BAllResultTest
		{
			get { return _bAllResultTest; }
			set { _bAllResultTest = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BZFSysCheck", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BZFSysCheck
		{
			get { return _bZFSysCheck; }
			set { _bZFSysCheck = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZFSysCheckInfo", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string ZFSysCheckInfo
		{
			get { return _zFSysCheckInfo; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for ZFSysCheckInfo", value, value.ToString());
				_zFSysCheckInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckInfo", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string CheckInfo
		{
			get { return _checkInfo; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for CheckInfo", value, value.ToString());
				_checkInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckInfoExamine", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string CheckInfoExamine
		{
			get { return _checkInfoExamine; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for CheckInfoExamine", value, value.ToString());
				_checkInfoExamine = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IConfirmByHand", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IConfirmByHand
		{
			get { return _iConfirmByHand; }
			set { _iConfirmByHand = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportPlaceTxt", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReportPlaceTxt
		{
			get { return _reportPlaceTxt; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReportPlaceTxt", value, value.ToString());
				_reportPlaceTxt = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LastExamineDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? LastExamineDate
		{
			get { return _lastExamineDate; }
			set { _lastExamineDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CancelExamineDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CancelExamineDate
		{
			get { return _cancelExamineDate; }
			set { _cancelExamineDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCancelScopeAudited", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCancelScopeAudited
		{
			get { return _isCancelScopeAudited; }
			set { _isCancelScopeAudited = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MicroFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MicroFlag
		{
			get { return _microFlag; }
			set { _microFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AntiFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AntiFlag
		{
			get { return _antiFlag; }
			set { _antiFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PrintDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PrintDateTime
		{
			get { return _printDateTime; }
			set { _printDateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintOper", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PrintOper
		{
			get { return _printOper; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PrintOper", value, value.ToString());
				_printOper = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrderTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? OrderTime
		{
			get { return _orderTime; }
			set { _orderTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IDCardNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string IDCardNo
		{
			get { return _iDCardNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for IDCardNo", value, value.ToString());
				_iDCardNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleQuality", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleQuality
		{
			get { return _sampleQuality; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleQuality", value, value.ToString());
				_sampleQuality = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormMemo2", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string FormMemo2
		{
			get { return _formMemo2; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for FormMemo2", value, value.ToString());
				_formMemo2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCopy", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCopy
		{
			get { return _isCopy; }
			set { _isCopy = value; }
		}

        
		#endregion
	}
	#endregion
}