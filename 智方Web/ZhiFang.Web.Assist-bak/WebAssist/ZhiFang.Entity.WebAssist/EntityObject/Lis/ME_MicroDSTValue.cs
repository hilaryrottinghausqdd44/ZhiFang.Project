using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region MEMicroDSTValue

	/// <summary>
	/// MEMicroDSTValue object for NHibernate mapped table 'ME_MicroDSTValue'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "MEMicroDSTValue", ShortCode = "MEMicroDSTValue", Desc = "")]
	public class MEMicroDSTValue : BaseEntity
	{
		#region Member Variables
		
        protected string _dSTType;
        protected long? _antiID;
        protected string _concentration;
        protected bool _useSDD;
        protected string _reportGroup;
        protected string _reportValue;
        protected string _resultStatus;
        protected double _quanValue;
        protected string _units;
        protected string _refRange;
        protected string _testMethod;
        protected int _alarmLevel;
        protected string _resultComment;
        protected bool _isUse;
        protected int _dispOrder;
        protected bool _isReportPublication;
        protected int _userNo;
        protected long? _appraisalValueID;
        protected long? _equipMicroAntiResultID;
        protected string _empName;
        protected bool _deleteFlag;
        protected DateTime? _dataUpdateTime;
        protected DateTime? _gTestDate;
        protected string _eReportValue;
        protected int _otherDispOrder;
        protected bool _isPrint;
        protected string _alarmInfo;
        protected long? _preMicroDSTValueID;
        protected string _preReportValue;
        protected string _preResultStatus;
        protected string _preCompStatus;
        protected string _eResultStatus;
        protected bool _bNaturalR;
        protected bool _isGroupAnti;
        protected string _antiNote;

		#endregion

		#region Constructors

		public MEMicroDSTValue() { }

		public MEMicroDSTValue( long labID, string dSTType, long antiID, string concentration, bool useSDD, string reportGroup, string reportValue, string resultStatus, double quanValue, string units, string refRange, string testMethod, int alarmLevel, string resultComment, bool isUse, int dispOrder, bool isReportPublication, int userNo, long appraisalValueID, long equipMicroAntiResultID, string empName, bool deleteFlag, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, DateTime gTestDate, string eReportValue, int otherDispOrder, bool isPrint, string alarmInfo, long preMicroDSTValueID, string preReportValue, string preResultStatus, string preCompStatus, string eResultStatus, bool bNaturalR, bool isGroupAnti, string antiNote )
		{
			this._labID = labID;
			this._dSTType = dSTType;
			this._antiID = antiID;
			this._concentration = concentration;
			this._useSDD = useSDD;
			this._reportGroup = reportGroup;
			this._reportValue = reportValue;
			this._resultStatus = resultStatus;
			this._quanValue = quanValue;
			this._units = units;
			this._refRange = refRange;
			this._testMethod = testMethod;
			this._alarmLevel = alarmLevel;
			this._resultComment = resultComment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._isReportPublication = isReportPublication;
			this._userNo = userNo;
			this._appraisalValueID = appraisalValueID;
			this._equipMicroAntiResultID = equipMicroAntiResultID;
			this._empName = empName;
			this._deleteFlag = deleteFlag;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._gTestDate = gTestDate;
			this._eReportValue = eReportValue;
			this._otherDispOrder = otherDispOrder;
			this._isPrint = isPrint;
			this._alarmInfo = alarmInfo;
			this._preMicroDSTValueID = preMicroDSTValueID;
			this._preReportValue = preReportValue;
			this._preResultStatus = preResultStatus;
			this._preCompStatus = preCompStatus;
			this._eResultStatus = eResultStatus;
			this._bNaturalR = bNaturalR;
			this._isGroupAnti = isGroupAnti;
			this._antiNote = antiNote;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "DSTType", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string DSTType
		{
			get { return _dSTType; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for DSTType", value, value.ToString());
				_dSTType = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AntiID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? AntiID
		{
			get { return _antiID; }
			set { _antiID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Concentration", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Concentration
		{
			get { return _concentration; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Concentration", value, value.ToString());
				_concentration = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UseSDD", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool UseSDD
		{
			get { return _useSDD; }
			set { _useSDD = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportGroup", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ReportGroup
		{
			get { return _reportGroup; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReportGroup", value, value.ToString());
				_reportGroup = value;
			}
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
        [DataDesc(CName = "", ShortCode = "RefRange", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string RefRange
		{
			get { return _refRange; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for RefRange", value, value.ToString());
				_refRange = value;
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
        [DataDesc(CName = "", ShortCode = "ResultComment", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ResultComment
		{
			get { return _resultComment; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ResultComment", value, value.ToString());
				_resultComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsReportPublication", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReportPublication
		{
			get { return _isReportPublication; }
			set { _isReportPublication = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UserNo
		{
			get { return _userNo; }
			set { _userNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AppraisalValueID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? AppraisalValueID
		{
			get { return _appraisalValueID; }
			set { _appraisalValueID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EquipMicroAntiResultID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipMicroAntiResultID
		{
			get { return _equipMicroAntiResultID; }
			set { _equipMicroAntiResultID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EmpName", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string EmpName
		{
			get { return _empName; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
				_empName = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
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
        [DataDesc(CName = "", ShortCode = "OtherDispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OtherDispOrder
		{
			get { return _otherDispOrder; }
			set { _otherDispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsPrint", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPrint
		{
			get { return _isPrint; }
			set { _isPrint = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AlarmInfo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string AlarmInfo
		{
			get { return _alarmInfo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for AlarmInfo", value, value.ToString());
				_alarmInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreMicroDSTValueID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? PreMicroDSTValueID
		{
			get { return _preMicroDSTValueID; }
			set { _preMicroDSTValueID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PreReportValue", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string PreReportValue
		{
			get { return _preReportValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for PreReportValue", value, value.ToString());
				_preReportValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PreResultStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string PreResultStatus
		{
			get { return _preResultStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for PreResultStatus", value, value.ToString());
				_preResultStatus = value;
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
        [DataDesc(CName = "", ShortCode = "EResultStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string EResultStatus
		{
			get { return _eResultStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for EResultStatus", value, value.ToString());
				_eResultStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BNaturalR", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BNaturalR
		{
			get { return _bNaturalR; }
			set { _bNaturalR = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsGroupAnti", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsGroupAnti
		{
			get { return _isGroupAnti; }
			set { _isGroupAnti = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AntiNote", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string AntiNote
		{
			get { return _antiNote; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for AntiNote", value, value.ToString());
				_antiNote = value;
			}
		}

        
		#endregion
	}
	#endregion
}