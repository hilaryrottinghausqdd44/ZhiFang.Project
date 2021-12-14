using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region MEEquipSampleForm

	/// <summary>
	/// MEEquipSampleForm object for NHibernate mapped table 'ME_EquipSampleForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "MEEquipSampleForm", ShortCode = "MEEquipSampleForm", Desc = "")]
	public class MEEquipSampleForm : BaseEntity
	{
		#region Member Variables
		
        protected long _equipSampleFormID;
        protected long? _groupSampleFormID;
        protected int _equipNo;
        protected string _equipModuleCode;
        protected DateTime? _eTestDate;
        protected string _eSampleNo;
        protected string _eRack;
        protected int _ePosition;
        protected int _eFinishCode;
        protected string _eFinishInfo;
        protected string _eResultComment;
        protected DateTime? _dataUpdateTime;
        protected string _eBarCode;
        protected int _reexamined;
        protected int _iExamined;
        protected int _testTypeNo;
        protected string _testType;

		#endregion

		#region Constructors

		public MEEquipSampleForm() { }

		public MEEquipSampleForm( long labID, long equipSampleFormID, long groupSampleFormID, int equipNo, string equipModuleCode, DateTime eTestDate, string eSampleNo, string eRack, int ePosition, int eFinishCode, string eFinishInfo, string eResultComment, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string eBarCode, int reexamined, int iExamined, int testTypeNo, string testType )
		{
			this._labID = labID;
			this._equipSampleFormID = equipSampleFormID;
			this._groupSampleFormID = groupSampleFormID;
			this._equipNo = equipNo;
			this._equipModuleCode = equipModuleCode;
			this._eTestDate = eTestDate;
			this._eSampleNo = eSampleNo;
			this._eRack = eRack;
			this._ePosition = ePosition;
			this._eFinishCode = eFinishCode;
			this._eFinishInfo = eFinishInfo;
			this._eResultComment = eResultComment;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._eBarCode = eBarCode;
			this._reexamined = reexamined;
			this._iExamined = iExamined;
			this._testTypeNo = testTypeNo;
			this._testType = testType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EquipSampleFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long EquipSampleFormID
		{
			get { return _equipSampleFormID; }
			set { _equipSampleFormID = value; }
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
        [DataDesc(CName = "", ShortCode = "EquipNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipNo
		{
			get { return _equipNo; }
			set { _equipNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipModuleCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EquipModuleCode
		{
			get { return _equipModuleCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EquipModuleCode", value, value.ToString());
				_equipModuleCode = value;
			}
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
        [DataDesc(CName = "", ShortCode = "ERack", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ERack
		{
			get { return _eRack; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ERack", value, value.ToString());
				_eRack = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EPosition", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EPosition
		{
			get { return _ePosition; }
			set { _ePosition = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EFinishCode", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EFinishCode
		{
			get { return _eFinishCode; }
			set { _eFinishCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EFinishInfo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EFinishInfo
		{
			get { return _eFinishInfo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EFinishInfo", value, value.ToString());
				_eFinishInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EResultComment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string EResultComment
		{
			get { return _eResultComment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for EResultComment", value, value.ToString());
				_eResultComment = value;
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
        [DataDesc(CName = "", ShortCode = "EBarCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EBarCode
		{
			get { return _eBarCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EBarCode", value, value.ToString());
				_eBarCode = value;
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
        [DataDesc(CName = "", ShortCode = "IExamined", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IExamined
		{
			get { return _iExamined; }
			set { _iExamined = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestTypeNo
		{
			get { return _testTypeNo; }
			set { _testTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestType", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string TestType
		{
			get { return _testType; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for TestType", value, value.ToString());
				_testType = value;
			}
		}

        
		#endregion
	}
	#endregion
}