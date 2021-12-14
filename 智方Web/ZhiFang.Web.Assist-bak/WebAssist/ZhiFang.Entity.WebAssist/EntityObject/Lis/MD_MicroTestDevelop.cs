using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region MDMicroTestDevelop

	/// <summary>
	/// MDMicroTestDevelop object for NHibernate mapped table 'MD_MicroTestDevelop'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "MDMicroTestDevelop", ShortCode = "MDMicroTestDevelop", Desc = "")]
	public class MDMicroTestDevelop : BaseEntity
	{
		#region Member Variables
		
        protected long? _microTestDevelopPID;
        protected long? _mDevelopID;
        protected long? _groupSampleFormID;
        protected long? _resultID;
        protected bool _bPrint;
        protected int _userNo;
        protected long? _equipMicroStepValueID;
        protected string _operator1;
        protected DateTime? _operatorsTime;
        protected int _userNo2;
        protected string _checker;
        protected DateTime? _checkTime;
        protected int _alarmLevel;
        protected string _reportValue;
        protected string _resultStatus;
        protected double _quanValue;
        protected string _resultDesc;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _equipSampleNo;
        protected DateTime? _dataUpdateTime;
        protected string _lotNo;
        protected int _messageSend;
        protected long? _msgSendID;
        protected DateTime? _gTestDate;

		#endregion

		#region Constructors

		public MDMicroTestDevelop() { }

		public MDMicroTestDevelop( long labID, long microTestDevelopPID, long mDevelopID, long groupSampleFormID, long resultID, bool bPrint, int userNo, long equipMicroStepValueID, string operator1, DateTime operatorsTime, int userNo2, string checker, DateTime checkTime, int alarmLevel, string reportValue, string resultStatus, double quanValue, string resultDesc, string zDY1, string zDY2, string zDY3, string equipSampleNo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string lotNo, int messageSend, long msgSendID, DateTime gTestDate )
		{
			this._labID = labID;
			this._microTestDevelopPID = microTestDevelopPID;
			this._mDevelopID = mDevelopID;
			this._groupSampleFormID = groupSampleFormID;
			this._resultID = resultID;
			this._bPrint = bPrint;
			this._userNo = userNo;
			this._equipMicroStepValueID = equipMicroStepValueID;
			this._operator1 = operator1;
			this._operatorsTime = operatorsTime;
			this._userNo2 = userNo2;
			this._checker = checker;
			this._checkTime = checkTime;
			this._alarmLevel = alarmLevel;
			this._reportValue = reportValue;
			this._resultStatus = resultStatus;
			this._quanValue = quanValue;
			this._resultDesc = resultDesc;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._equipSampleNo = equipSampleNo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lotNo = lotNo;
			this._messageSend = messageSend;
			this._msgSendID = msgSendID;
			this._gTestDate = gTestDate;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MicroTestDevelopPID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? MicroTestDevelopPID
		{
			get { return _microTestDevelopPID; }
			set { _microTestDevelopPID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MDevelopID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? MDevelopID
		{
			get { return _mDevelopID; }
			set { _mDevelopID = value; }
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
        [DataDesc(CName = "", ShortCode = "ResultID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? ResultID
		{
			get { return _resultID; }
			set { _resultID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPrint", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BPrint
		{
			get { return _bPrint; }
			set { _bPrint = value; }
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
        [DataDesc(CName = "", ShortCode = "EquipMicroStepValueID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipMicroStepValueID
		{
			get { return _equipMicroStepValueID; }
			set { _equipMicroStepValueID = value; }
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
        [DataDesc(CName = "", ShortCode = "OperatorsTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? OperatorsTime
		{
			get { return _operatorsTime; }
			set { _operatorsTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserNo2", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UserNo2
		{
			get { return _userNo2; }
			set { _userNo2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Checker", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Checker
		{
			get { return _checker; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
				_checker = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AlarmLevel", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportValue", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ReportValue
		{
			get { return _reportValue; }
			set
			{
				if ( value != null && value.Length > 500)
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
        [DataDesc(CName = "", ShortCode = "ResultDesc", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ResultDesc
		{
			get { return _resultDesc; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ResultDesc", value, value.ToString());
				_resultDesc = value;
			}
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
        [DataDesc(CName = "", ShortCode = "EquipSampleNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EquipSampleNo
		{
			get { return _equipSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EquipSampleNo", value, value.ToString());
				_equipSampleNo = value;
			}
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
        [DataDesc(CName = "", ShortCode = "LotNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LotNo
		{
			get { return _lotNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for LotNo", value, value.ToString());
				_lotNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MessageSend", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MessageSend
		{
			get { return _messageSend; }
			set { _messageSend = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MsgSendID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? MsgSendID
		{
			get { return _msgSendID; }
			set { _msgSendID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GTestDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GTestDate
		{
			get { return _gTestDate; }
			set { _gTestDate = value; }
		}

        
		#endregion
	}
	#endregion
}