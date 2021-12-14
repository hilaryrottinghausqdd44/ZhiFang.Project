using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region MEMicroAppraisalValue

	/// <summary>
	/// MEMicroAppraisalValue object for NHibernate mapped table 'ME_MicroAppraisalValue'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "MEMicroAppraisalValue", ShortCode = "MEMicroAppraisalValue", Desc = "")]
	public class MEMicroAppraisalValue : BaseEntity
	{
		#region Member Variables
		
        protected string _iMType;
        protected long? _microTestDevelopID;
        protected long? _microID;
        protected string _resistancePhenotypeName;
        protected string _equipResistancePhenotype;
        protected string _appraisalResult;
        protected string _laboratoryEvaluation;
        protected long? _retainedBacteriaID;
        protected string _zdy1;
        protected string _zdy2;
        protected string _zdy3;
        protected string _zdy4;
        protected string _zdy5;
        protected int _equipNo;
        protected string _equipSampleNo;
        protected string _resultComment;
        protected DateTime? _dataUpdateTime;
        protected long? _iMID;
        protected long? _microAntiClassID;
        protected DateTime? _gTestDate;
        protected string _microPositive;
        protected int _isCopy;

		#endregion

		#region Constructors

		public MEMicroAppraisalValue() { }

		public MEMicroAppraisalValue( long labID, string iMType, long microTestDevelopID, long microID, string resistancePhenotypeName, string equipResistancePhenotype, string appraisalResult, string laboratoryEvaluation, long retainedBacteriaID, string zdy1, string zdy2, string zdy3, string zdy4, string zdy5, int equipNo, string equipSampleNo, string resultComment, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long iMID, long microAntiClassID, DateTime gTestDate, string microPositive, int isCopy )
		{
			this._labID = labID;
			this._iMType = iMType;
			this._microTestDevelopID = microTestDevelopID;
			this._microID = microID;
			this._resistancePhenotypeName = resistancePhenotypeName;
			this._equipResistancePhenotype = equipResistancePhenotype;
			this._appraisalResult = appraisalResult;
			this._laboratoryEvaluation = laboratoryEvaluation;
			this._retainedBacteriaID = retainedBacteriaID;
			this._zdy1 = zdy1;
			this._zdy2 = zdy2;
			this._zdy3 = zdy3;
			this._zdy4 = zdy4;
			this._zdy5 = zdy5;
			this._equipNo = equipNo;
			this._equipSampleNo = equipSampleNo;
			this._resultComment = resultComment;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._iMID = iMID;
			this._microAntiClassID = microAntiClassID;
			this._gTestDate = gTestDate;
			this._microPositive = microPositive;
			this._isCopy = isCopy;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "IMType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string IMType
		{
			get { return _iMType; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for IMType", value, value.ToString());
				_iMType = value;
			}
		}

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
        [DataDesc(CName = "", ShortCode = "MicroID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? MicroID
		{
			get { return _microID; }
			set { _microID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ResistancePhenotypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ResistancePhenotypeName
		{
			get { return _resistancePhenotypeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ResistancePhenotypeName", value, value.ToString());
				_resistancePhenotypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipResistancePhenotype", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipResistancePhenotype
		{
			get { return _equipResistancePhenotype; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EquipResistancePhenotype", value, value.ToString());
				_equipResistancePhenotype = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AppraisalResult", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string AppraisalResult
		{
			get { return _appraisalResult; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for AppraisalResult", value, value.ToString());
				_appraisalResult = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LaboratoryEvaluation", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string LaboratoryEvaluation
		{
			get { return _laboratoryEvaluation; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for LaboratoryEvaluation", value, value.ToString());
				_laboratoryEvaluation = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "RetainedBacteriaID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? RetainedBacteriaID
		{
			get { return _retainedBacteriaID; }
			set { _retainedBacteriaID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy1", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy1
		{
			get { return _zdy1; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy1", value, value.ToString());
				_zdy1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy2", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy2
		{
			get { return _zdy2; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy2", value, value.ToString());
				_zdy2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy3", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy3
		{
			get { return _zdy3; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy3", value, value.ToString());
				_zdy3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy4", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy4
		{
			get { return _zdy4; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy4", value, value.ToString());
				_zdy4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy5", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy5
		{
			get { return _zdy5; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy5", value, value.ToString());
				_zdy5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipNo
		{
			get { return _equipNo; }
			set { _equipNo = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "IMID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? IMID
		{
			get { return _iMID; }
			set { _iMID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MicroAntiClassID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? MicroAntiClassID
		{
			get { return _microAntiClassID; }
			set { _microAntiClassID = value; }
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
        [DataDesc(CName = "", ShortCode = "MicroPositive", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string MicroPositive
		{
			get { return _microPositive; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for MicroPositive", value, value.ToString());
				_microPositive = value;
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