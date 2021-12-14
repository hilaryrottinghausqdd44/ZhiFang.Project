using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region MEEquipSampleItem

	/// <summary>
	/// MEEquipSampleItem object for NHibernate mapped table 'ME_EquipSampleItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "MEEquipSampleItem", ShortCode = "MEEquipSampleItem", Desc = "")]
	public class MEEquipSampleItem : BaseEntity
	{
		#region Member Variables
		
        protected long _equipResultID;
        protected int _itemNo;
        protected long? _equipSampleFormID;
        protected int _testType;
        protected string _itemChannel;
        protected string _eReportValue;
        protected string _eResultStatus;
        protected string _eResultAlarm;
        protected string _eTestComment;
        protected string _eOriginalValue;
        protected string _eOriginalResultStatus;
        protected string _eOriginalResultAlarm;
        protected string _eOriginalTestComment;
        protected int _eTestState;
        protected int _itemResultFlag;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected DateTime? _eTestDate;
        protected long? _resultID;
        protected int _iExamined;
        protected string _eLValue;
        protected string _eHValue;
        protected double _quanValue1;
        protected double _quanValue2;
        protected double _quanValue3;
        protected double _quanValue4;
        protected string _descValue1;
        protected string _descValue2;
        protected int _iEResultAlarm;

		#endregion

		#region Constructors

		public MEEquipSampleItem() { }

		public MEEquipSampleItem( long labID, long equipResultID, int itemNo, long equipSampleFormID, int testType, string itemChannel, string eReportValue, string eResultStatus, string eResultAlarm, string eTestComment, string eOriginalValue, string eOriginalResultStatus, string eOriginalResultAlarm, string eOriginalTestComment, int eTestState, int itemResultFlag, string zDY1, string zDY2, string zDY3, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, DateTime eTestDate, long resultID, int iExamined, string eLValue, string eHValue, double quanValue1, double quanValue2, double quanValue3, double quanValue4, string descValue1, string descValue2, int iEResultAlarm )
		{
			this._labID = labID;
			this._equipResultID = equipResultID;
			this._itemNo = itemNo;
			this._equipSampleFormID = equipSampleFormID;
			this._testType = testType;
			this._itemChannel = itemChannel;
			this._eReportValue = eReportValue;
			this._eResultStatus = eResultStatus;
			this._eResultAlarm = eResultAlarm;
			this._eTestComment = eTestComment;
			this._eOriginalValue = eOriginalValue;
			this._eOriginalResultStatus = eOriginalResultStatus;
			this._eOriginalResultAlarm = eOriginalResultAlarm;
			this._eOriginalTestComment = eOriginalTestComment;
			this._eTestState = eTestState;
			this._itemResultFlag = itemResultFlag;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._eTestDate = eTestDate;
			this._resultID = resultID;
			this._iExamined = iExamined;
			this._eLValue = eLValue;
			this._eHValue = eHValue;
			this._quanValue1 = quanValue1;
			this._quanValue2 = quanValue2;
			this._quanValue3 = quanValue3;
			this._quanValue4 = quanValue4;
			this._descValue1 = descValue1;
			this._descValue2 = descValue2;
			this._iEResultAlarm = iEResultAlarm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EquipResultID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long EquipResultID
		{
			get { return _equipResultID; }
			set { _equipResultID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemNo
		{
			get { return _itemNo; }
			set { _itemNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EquipSampleFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipSampleFormID
		{
			get { return _equipSampleFormID; }
			set { _equipSampleFormID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestType
		{
			get { return _testType; }
			set { _testType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemChannel", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemChannel
		{
			get { return _itemChannel; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ItemChannel", value, value.ToString());
				_itemChannel = value;
			}
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
        [DataDesc(CName = "", ShortCode = "EResultStatus", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EResultStatus
		{
			get { return _eResultStatus; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EResultStatus", value, value.ToString());
				_eResultStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EResultAlarm", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EResultAlarm
		{
			get { return _eResultAlarm; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EResultAlarm", value, value.ToString());
				_eResultAlarm = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ETestComment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string ETestComment
		{
			get { return _eTestComment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for ETestComment", value, value.ToString());
				_eTestComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EOriginalValue", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string EOriginalValue
		{
			get { return _eOriginalValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalValue", value, value.ToString());
				_eOriginalValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EOriginalResultStatus", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EOriginalResultStatus
		{
			get { return _eOriginalResultStatus; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalResultStatus", value, value.ToString());
				_eOriginalResultStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EOriginalResultAlarm", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EOriginalResultAlarm
		{
			get { return _eOriginalResultAlarm; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalResultAlarm", value, value.ToString());
				_eOriginalResultAlarm = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EOriginalTestComment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string EOriginalTestComment
		{
			get { return _eOriginalTestComment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalTestComment", value, value.ToString());
				_eOriginalTestComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ETestState", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ETestState
		{
			get { return _eTestState; }
			set { _eTestState = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemResultFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemResultFlag
		{
			get { return _itemResultFlag; }
			set { _itemResultFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY1", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY1
		{
			get { return _zDY1; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
				_zDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY2", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY2
		{
			get { return _zDY2; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
				_zDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY3", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY3
		{
			get { return _zDY3; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
				_zDY3 = value;
			}
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ETestDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ETestDate
		{
			get { return _eTestDate; }
			set { _eTestDate = value; }
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
        [DataDesc(CName = "", ShortCode = "IExamined", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IExamined
		{
			get { return _iExamined; }
			set { _iExamined = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "QuanValue1", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double QuanValue1
		{
			get { return _quanValue1; }
			set { _quanValue1 = value; }
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
        [DataDesc(CName = "", ShortCode = "IEResultAlarm", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IEResultAlarm
		{
			get { return _iEResultAlarm; }
			set { _iEResultAlarm = value; }
		}

        
		#endregion
	}
	#endregion
}