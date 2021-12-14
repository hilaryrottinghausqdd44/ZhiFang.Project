using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region MEGroupSampleItem

	/// <summary>
	/// MEGroupSampleItem object for NHibernate mapped table 'ME_GroupSampleItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "MEGroupSampleItem", ShortCode = "MEGroupSampleItem", Desc = "")]
	public class MEGroupSampleItem : BaseEntity
	{
		#region Member Variables
		
        protected long? _nItemID;
        protected int _testType;
        protected long? _parentResultID;
        protected long? _immResultID;
        protected long? _equipResultID;
        protected long? _pItemNo;
        protected long? _itemNo;
        protected long? _groupSampleFormID;
        protected long? _reCheckFormID;
        protected string _origlValue;
        protected int _valueType;
        protected string _reportValue;
        protected string _resultStatus;
        protected double _quanValue;
        protected double _quanValue2;
        protected double _quanValue3;
        protected string _quanValueComparison;
        protected string _units;
        protected string _refRange;
        protected string _preValue;
        protected string _preValueComp;
        protected string _preCompStatus;
        protected string _testMethod;
        protected int _alarmLevel;
        protected int _resultLinkType;
        protected string _resultLink;
        protected string _simpleResultLink;
        protected string _resultComment;
        protected bool _deleteFlag;
        protected bool _isDuplicate;
        protected int _isHandEditStatus;
        protected int _testFlag;
        protected int _checkFlag;
        protected int _reportFlag;
        protected bool _isExcess;
        protected string _iZDY1;
        protected string _iZDY2;
        protected string _iZDY3;
        protected string _iZDY4;
        protected string _iZDY5;
        protected int _commState;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected int _plateNo;
        protected int _positionNo;
        protected int _equipNo;
        protected int _iDevelop;
        protected int _userNo;
        protected string _operator1;
        protected DateTime? _gTestDate;
        protected string _eReportValue;
        protected string _pReportValue;
        protected string _eLValue;
        protected string _eHValue;
        protected string _conclusion;
        protected string _reportValue2;
        protected string _reportValue3;
        protected long? _preResultID;
        protected DateTime? _preGTestDate;
        protected int _isMatch;
        protected string _alarmInfo;
        protected int _hisResultCount;
        protected DateTime? _resultAddTime;
        protected int _iItemSource;
        protected long? _preResultID2;
        protected DateTime? _preGTestDate2;
        protected string _preValue2;
        protected long? _preResultID3;
        protected DateTime? _preGTestDate3;
        protected string _preValue3;
        protected byte[] _itemResultInfo;
        protected string _itemDiagMethod;
        protected int _iAutoUnion;
        protected string _autoUnionSNo;
        protected int _iUnionCount;
        protected int _iNeedUnionCount;
        protected int _nPItemNo;
        protected string _eSend;
        protected string _dispReportValue;
        protected double _quanValue1;
        protected double _quanValue4;
        protected string _descValue1;
        protected string _descValue2;
        protected int _iPositiveCard;
        protected int _redoFlag;
        protected string _redoReason;
        protected string _eResultStatus;
        protected string _eResultAlarm;
        protected int _iEResultAlarm;
        protected string _reportValue4;
        protected bool _bReportPrint;
        protected string _redoValue;

		#endregion

		#region Constructors

		public MEGroupSampleItem() { }

		public MEGroupSampleItem( long labID, long nItemID, int testType, long parentResultID, long immResultID, long equipResultID, int pItemNo, int itemNo, long groupSampleFormID, long reCheckFormID, string origlValue, int valueType, string reportValue, string resultStatus, double quanValue, double quanValue2, double quanValue3, string quanValueComparison, string units, string refRange, string preValue, string preValueComp, string preCompStatus, string testMethod, int alarmLevel, int resultLinkType, string resultLink, string simpleResultLink, string resultComment, bool deleteFlag, bool isDuplicate, int isHandEditStatus, int testFlag, int checkFlag, int reportFlag, bool isExcess, string iZDY1, string iZDY2, string iZDY3, string iZDY4, string iZDY5, int commState, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, int plateNo, int positionNo, int equipNo, int iDevelop, int userNo, string operator1, DateTime gTestDate, string eReportValue, string pReportValue, string eLValue, string eHValue, string conclusion, string reportValue2, string reportValue3, long preResultID, DateTime preGTestDate, int isMatch, string alarmInfo, int hisResultCount, DateTime resultAddTime, int iItemSource, long preResultID2, DateTime preGTestDate2, string preValue2, long preResultID3, DateTime preGTestDate3, string preValue3, byte[] itemResultInfo, string itemDiagMethod, int iAutoUnion, string autoUnionSNo, int iUnionCount, int iNeedUnionCount, int nPItemNo, string eSend, string dispReportValue, double quanValue1, double quanValue4, string descValue1, string descValue2, int iPositiveCard, int redoFlag, string redoReason, string eResultStatus, string eResultAlarm, int iEResultAlarm, string reportValue4, bool bReportPrint, string redoValue )
		{
			this._labID = labID;
			this._nItemID = nItemID;
			this._testType = testType;
			this._parentResultID = parentResultID;
			this._immResultID = immResultID;
			this._equipResultID = equipResultID;
			this._pItemNo = pItemNo;
			this._itemNo = itemNo;
			this._groupSampleFormID = groupSampleFormID;
			this._reCheckFormID = reCheckFormID;
			this._origlValue = origlValue;
			this._valueType = valueType;
			this._reportValue = reportValue;
			this._resultStatus = resultStatus;
			this._quanValue = quanValue;
			this._quanValue2 = quanValue2;
			this._quanValue3 = quanValue3;
			this._quanValueComparison = quanValueComparison;
			this._units = units;
			this._refRange = refRange;
			this._preValue = preValue;
			this._preValueComp = preValueComp;
			this._preCompStatus = preCompStatus;
			this._testMethod = testMethod;
			this._alarmLevel = alarmLevel;
			this._resultLinkType = resultLinkType;
			this._resultLink = resultLink;
			this._simpleResultLink = simpleResultLink;
			this._resultComment = resultComment;
			this._deleteFlag = deleteFlag;
			this._isDuplicate = isDuplicate;
			this._isHandEditStatus = isHandEditStatus;
			this._testFlag = testFlag;
			this._checkFlag = checkFlag;
			this._reportFlag = reportFlag;
			this._isExcess = isExcess;
			this._iZDY1 = iZDY1;
			this._iZDY2 = iZDY2;
			this._iZDY3 = iZDY3;
			this._iZDY4 = iZDY4;
			this._iZDY5 = iZDY5;
			this._commState = commState;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._zDY4 = zDY4;
			this._zDY5 = zDY5;
			this._plateNo = plateNo;
			this._positionNo = positionNo;
			this._equipNo = equipNo;
			this._iDevelop = iDevelop;
			this._userNo = userNo;
			this._operator1 = operator1;
			this._gTestDate = gTestDate;
			this._eReportValue = eReportValue;
			this._pReportValue = pReportValue;
			this._eLValue = eLValue;
			this._eHValue = eHValue;
			this._conclusion = conclusion;
			this._reportValue2 = reportValue2;
			this._reportValue3 = reportValue3;
			this._preResultID = preResultID;
			this._preGTestDate = preGTestDate;
			this._isMatch = isMatch;
			this._alarmInfo = alarmInfo;
			this._hisResultCount = hisResultCount;
			this._resultAddTime = resultAddTime;
			this._iItemSource = iItemSource;
			this._preResultID2 = preResultID2;
			this._preGTestDate2 = preGTestDate2;
			this._preValue2 = preValue2;
			this._preResultID3 = preResultID3;
			this._preGTestDate3 = preGTestDate3;
			this._preValue3 = preValue3;
			this._itemResultInfo = itemResultInfo;
			this._itemDiagMethod = itemDiagMethod;
			this._iAutoUnion = iAutoUnion;
			this._autoUnionSNo = autoUnionSNo;
			this._iUnionCount = iUnionCount;
			this._iNeedUnionCount = iNeedUnionCount;
			this._nPItemNo = nPItemNo;
			this._eSend = eSend;
			this._dispReportValue = dispReportValue;
			this._quanValue1 = quanValue1;
			this._quanValue4 = quanValue4;
			this._descValue1 = descValue1;
			this._descValue2 = descValue2;
			this._iPositiveCard = iPositiveCard;
			this._redoFlag = redoFlag;
			this._redoReason = redoReason;
			this._eResultStatus = eResultStatus;
			this._eResultAlarm = eResultAlarm;
			this._iEResultAlarm = iEResultAlarm;
			this._reportValue4 = reportValue4;
			this._bReportPrint = bReportPrint;
			this._redoValue = redoValue;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "NItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? NItemID
		{
			get { return _nItemID; }
			set { _nItemID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestType
		{
			get { return _testType; }
			set { _testType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ParentResultID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? ParentResultID
		{
			get { return _parentResultID; }
			set { _parentResultID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ImmResultID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? ImmResultID
		{
			get { return _immResultID; }
			set { _immResultID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EquipResultID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipResultID
		{
			get { return _equipResultID; }
			set { _equipResultID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual long? PItemNo
		{
			get { return _pItemNo; }
			set { _pItemNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual long? ItemNo
		{
			get { return _itemNo; }
			set { _itemNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GroupSampleFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? GroupSampleFormID
		{
			get { return _groupSampleFormID; }
			set { _groupSampleFormID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReCheckFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReCheckFormID
		{
			get { return _reCheckFormID; }
			set { _reCheckFormID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OriglValue", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string OriglValue
		{
			get { return _origlValue; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for OriglValue", value, value.ToString());
				_origlValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ValueType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ValueType
		{
			get { return _valueType; }
			set { _valueType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportValue", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string ReportValue
		{
			get { return _reportValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ReportValue", value, value.ToString());
				_reportValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ResultStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ResultStatus
		{
			get { return _resultStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ResultStatus", value, value.ToString());
				_resultStatus = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "QuanValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double QuanValue
		{
			get { return _quanValue; }
			set { _quanValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "QuanValue2", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double QuanValue2
		{
			get { return _quanValue2; }
			set { _quanValue2 = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "QuanValue3", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double QuanValue3
		{
			get { return _quanValue3; }
			set { _quanValue3 = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QuanValueComparison", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string QuanValueComparison
		{
			get { return _quanValueComparison; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for QuanValueComparison", value, value.ToString());
				_quanValueComparison = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Units", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Units
		{
			get { return _units; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Units", value, value.ToString());
				_units = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefRange", Desc = "", ContextType = SysDic.All, Length = 400)]
        public virtual string RefRange
		{
			get { return _refRange; }
			set
			{
				if ( value != null && value.Length > 400)
					throw new ArgumentOutOfRangeException("Invalid value for RefRange", value, value.ToString());
				_refRange = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PreValue", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string PreValue
		{
			get { return _preValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for PreValue", value, value.ToString());
				_preValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PreValueComp", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PreValueComp
		{
			get { return _preValueComp; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PreValueComp", value, value.ToString());
				_preValueComp = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PreCompStatus", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PreCompStatus
		{
			get { return _preCompStatus; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PreCompStatus", value, value.ToString());
				_preCompStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestMethod", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TestMethod
		{
			get { return _testMethod; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TestMethod", value, value.ToString());
				_testMethod = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AlarmLevel", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ResultLinkType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ResultLinkType
		{
			get { return _resultLinkType; }
			set { _resultLinkType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ResultLink", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ResultLink
		{
			get { return _resultLink; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ResultLink", value, value.ToString());
				_resultLink = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SimpleResultLink", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string SimpleResultLink
		{
			get { return _simpleResultLink; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SimpleResultLink", value, value.ToString());
				_simpleResultLink = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ResultComment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string ResultComment
		{
			get { return _resultComment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for ResultComment", value, value.ToString());
				_resultComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeleteFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool DeleteFlag
		{
			get { return _deleteFlag; }
			set { _deleteFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDuplicate", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDuplicate
		{
			get { return _isDuplicate; }
			set { _isDuplicate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsHandEditStatus", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsHandEditStatus
		{
			get { return _isHandEditStatus; }
			set { _isHandEditStatus = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestFlag
		{
			get { return _testFlag; }
			set { _testFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
		{
			get { return _checkFlag; }
			set { _checkFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportFlag
		{
			get { return _reportFlag; }
			set { _reportFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsExcess", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsExcess
		{
			get { return _isExcess; }
			set { _isExcess = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IZDY1", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string IZDY1
		{
			get { return _iZDY1; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for IZDY1", value, value.ToString());
				_iZDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IZDY2", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string IZDY2
		{
			get { return _iZDY2; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for IZDY2", value, value.ToString());
				_iZDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IZDY3", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string IZDY3
		{
			get { return _iZDY3; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for IZDY3", value, value.ToString());
				_iZDY3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IZDY4", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string IZDY4
		{
			get { return _iZDY4; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for IZDY4", value, value.ToString());
				_iZDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IZDY5", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string IZDY5
		{
			get { return _iZDY5; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for IZDY5", value, value.ToString());
				_iZDY5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CommState", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CommState
		{
			get { return _commState; }
			set { _commState = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
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
        [DataDesc(CName = "", ShortCode = "ZDY1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY1
		{
			get { return _zDY1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
				_zDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY2
		{
			get { return _zDY2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
				_zDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY3
		{
			get { return _zDY3; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
				_zDY3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY4", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY4
		{
			get { return _zDY4; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
				_zDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY5", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY5
		{
			get { return _zDY5; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
				_zDY5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PlateNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PlateNo
		{
			get { return _plateNo; }
			set { _plateNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PositionNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PositionNo
		{
			get { return _positionNo; }
			set { _positionNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipNo
		{
			get { return _equipNo; }
			set { _equipNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IDevelop", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IDevelop
		{
			get { return _iDevelop; }
			set { _iDevelop = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UserNo
		{
			get { return _userNo; }
			set { _userNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Operator", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Operator
		{
			get { return _operator1; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Operator", value, value.ToString());
				_operator1 = value;
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
        [DataDesc(CName = "", ShortCode = "EReportValue", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string EReportValue
		{
			get { return _eReportValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for EReportValue", value, value.ToString());
				_eReportValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PReportValue", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string PReportValue
		{
			get { return _pReportValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for PReportValue", value, value.ToString());
				_pReportValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ELValue", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ELValue
		{
			get { return _eLValue; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ELValue", value, value.ToString());
				_eLValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EHValue", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EHValue
		{
			get { return _eHValue; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EHValue", value, value.ToString());
				_eHValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Conclusion", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string Conclusion
		{
			get { return _conclusion; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Conclusion", value, value.ToString());
				_conclusion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportValue2", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string ReportValue2
		{
			get { return _reportValue2; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ReportValue2", value, value.ToString());
				_reportValue2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportValue3", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string ReportValue3
		{
			get { return _reportValue3; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ReportValue3", value, value.ToString());
				_reportValue3 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreResultID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? PreResultID
		{
			get { return _preResultID; }
			set { _preResultID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreGTestDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PreGTestDate
		{
			get { return _preGTestDate; }
			set { _preGTestDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsMatch", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsMatch
		{
			get { return _isMatch; }
			set { _isMatch = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AlarmInfo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AlarmInfo
		{
			get { return _alarmInfo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AlarmInfo", value, value.ToString());
				_alarmInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisResultCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int HisResultCount
		{
			get { return _hisResultCount; }
			set { _hisResultCount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ResultAddTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ResultAddTime
		{
			get { return _resultAddTime; }
			set { _resultAddTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IItemSource", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IItemSource
		{
			get { return _iItemSource; }
			set { _iItemSource = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreResultID2", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? PreResultID2
		{
			get { return _preResultID2; }
			set { _preResultID2 = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreGTestDate2", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PreGTestDate2
		{
			get { return _preGTestDate2; }
			set { _preGTestDate2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PreValue2", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string PreValue2
		{
			get { return _preValue2; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for PreValue2", value, value.ToString());
				_preValue2 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreResultID3", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? PreResultID3
		{
			get { return _preResultID3; }
			set { _preResultID3 = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreGTestDate3", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PreGTestDate3
		{
			get { return _preGTestDate3; }
			set { _preGTestDate3 = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PreValue3", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string PreValue3
		{
			get { return _preValue3; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for PreValue3", value, value.ToString());
				_preValue3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemResultInfo", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] ItemResultInfo
		{
			get { return _itemResultInfo; }
			set { _itemResultInfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemDiagMethod", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ItemDiagMethod
		{
			get { return _itemDiagMethod; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ItemDiagMethod", value, value.ToString());
				_itemDiagMethod = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IAutoUnion", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IAutoUnion
		{
			get { return _iAutoUnion; }
			set { _iAutoUnion = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AutoUnionSNo", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string AutoUnionSNo
		{
			get { return _autoUnionSNo; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for AutoUnionSNo", value, value.ToString());
				_autoUnionSNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IUnionCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IUnionCount
		{
			get { return _iUnionCount; }
			set { _iUnionCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "INeedUnionCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int INeedUnionCount
		{
			get { return _iNeedUnionCount; }
			set { _iNeedUnionCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NPItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int NPItemNo
		{
			get { return _nPItemNo; }
			set { _nPItemNo = value; }
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
        [DataDesc(CName = "", ShortCode = "DispReportValue", Desc = "", ContextType = SysDic.All, Length = 2000)]
        public virtual string DispReportValue
		{
			get { return _dispReportValue; }
			set
			{
				if ( value != null && value.Length > 2000)
					throw new ArgumentOutOfRangeException("Invalid value for DispReportValue", value, value.ToString());
				_dispReportValue = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "QuanValue1", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double QuanValue1
		{
			get { return _quanValue1; }
			set { _quanValue1 = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "QuanValue4", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double QuanValue4
		{
			get { return _quanValue4; }
			set { _quanValue4 = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DescValue1", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string DescValue1
		{
			get { return _descValue1; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for DescValue1", value, value.ToString());
				_descValue1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DescValue2", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string DescValue2
		{
			get { return _descValue2; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for DescValue2", value, value.ToString());
				_descValue2 = value;
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
        [DataDesc(CName = "", ShortCode = "RedoReason", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string RedoReason
		{
			get { return _redoReason; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for RedoReason", value, value.ToString());
				_redoReason = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EResultStatus", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EResultStatus
		{
			get { return _eResultStatus; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EResultStatus", value, value.ToString());
				_eResultStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EResultAlarm", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EResultAlarm
		{
			get { return _eResultAlarm; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EResultAlarm", value, value.ToString());
				_eResultAlarm = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IEResultAlarm", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IEResultAlarm
		{
			get { return _iEResultAlarm; }
			set { _iEResultAlarm = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportValue4", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string ReportValue4
		{
			get { return _reportValue4; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ReportValue4", value, value.ToString());
				_reportValue4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BReportPrint", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BReportPrint
		{
			get { return _bReportPrint; }
			set { _bReportPrint = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RedoValue", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string RedoValue
		{
			get { return _redoValue; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for RedoValue", value, value.ToString());
				_redoValue = value;
			}
		}

        
		#endregion
	}
	#endregion
}