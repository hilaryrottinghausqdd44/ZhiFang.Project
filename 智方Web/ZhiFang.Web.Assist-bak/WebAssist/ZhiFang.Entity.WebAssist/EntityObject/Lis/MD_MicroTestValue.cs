using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region MDMicroTestValue

	/// <summary>
	/// MDMicroTestValue object for NHibernate mapped table 'MD_MicroTestValue'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "MDMicroTestValue", ShortCode = "MDMicroTestValue", Desc = "")]
	public class MDMicroTestValue : BaseEntity
	{
		#region Member Variables
		
        protected long? _microTestDevelopID;
        protected long? _microTestInfoID;
        protected string _fName;
        protected string _testValue;
        protected string _useCode;
        protected bool _bPrint;
        protected int _alarmLevel;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected DateTime? _dataUpdateTime;
        protected DateTime? _gTestDate;
        protected int _dispOrder;
        protected string _groups;

		#endregion

		#region Constructors

		public MDMicroTestValue() { }

		public MDMicroTestValue( long labID, long microTestDevelopID, long microTestInfoID, string fName, string testValue, string useCode, bool bPrint, int alarmLevel, string zX1, string zX2, string zX3, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, DateTime gTestDate, int dispOrder, string groups )
		{
			this._labID = labID;
			this._microTestDevelopID = microTestDevelopID;
			this._microTestInfoID = microTestInfoID;
			this._fName = fName;
			this._testValue = testValue;
			this._useCode = useCode;
			this._bPrint = bPrint;
			this._alarmLevel = alarmLevel;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._gTestDate = gTestDate;
			this._dispOrder = dispOrder;
			this._groups = groups;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MicroTestDevelopID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? MicroTestDevelopID
		{
			get { return _microTestDevelopID; }
			set { _microTestDevelopID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MicroTestInfoID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? MicroTestInfoID
		{
			get { return _microTestInfoID; }
			set { _microTestInfoID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FName", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string FName
		{
			get { return _fName; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for FName", value, value.ToString());
				_fName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestValue", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string TestValue
		{
			get { return _testValue; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for TestValue", value, value.ToString());
				_testValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UseCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
		{
			get { return _useCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
				_useCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPrint", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BPrint
		{
			get { return _bPrint; }
			set { _bPrint = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AlarmLevel", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
				_zX1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
				_zX2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
				_zX3 = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GTestDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GTestDate
		{
			get { return _gTestDate; }
			set { _gTestDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Groups", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Groups
		{
			get { return _groups; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Groups", value, value.ToString());
				_groups = value;
			}
		}

        
		#endregion
	}
	#endregion
}