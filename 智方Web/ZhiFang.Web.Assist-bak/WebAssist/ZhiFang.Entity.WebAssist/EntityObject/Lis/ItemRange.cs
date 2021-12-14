using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region ItemRange

	/// <summary>
	/// ItemRange object for NHibernate mapped table 'ItemRange'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ItemRange", ShortCode = "ItemRange", Desc = "")]
	public class ItemRange : BaseEntityService
    {
		#region Member Variables
		
        protected int _sampleTypeNo;
        protected int _genderNo;
        protected int _ageUnitNo;
        protected double _lowAge;
        protected double _highAge;
        protected double _lowValue;
        protected double _highValue;
        protected double _abLow;
        protected double _abHigh;
        protected string _refRange;
        protected int _isDefault;
        protected int _sectionNo;
        protected string _highDesc;
        protected string _lowDesc;
        protected string _naturalDesc;
        protected int _userDesc;
        protected int _equipNo;
        protected string _unit;
        protected string _refDesc;
        protected string _hasResultDesc;
        protected string _highStatus;
        protected string _lowStatus;
        protected string _naturalStatus;
        protected string _hHDesc;
        protected string _hHStatus;
        protected string _lLDesc;
        protected string _lLStatus;
        protected DateTime? _bCollectTime;
        protected DateTime? _eCollectTime;
        protected int _deptNo;
        protected double _redoVLow;
        protected double _redoVHigh;
        protected int _appendUserDesc;
        protected string _abLowStateDesc;
        protected string _abHighStateDesc;
        protected string _lowStateDesc;
        protected string _highStateDesc;
        protected int _paritemNo;
		

		#endregion

		#region Constructors

		public ItemRange() { }

		public ItemRange( int sampleTypeNo, int genderNo, int ageUnitNo, double lowAge, double highAge, double lowValue, double highValue, double abLow, double abHigh, string refRange, int isDefault, int sectionNo, string highDesc, string lowDesc, string naturalDesc, int userDesc, int equipNo, string unit, string refDesc, string hasResultDesc, string highStatus, string lowStatus, string naturalStatus, string hHDesc, string hHStatus, string lLDesc, string lLStatus, DateTime bCollectTime, DateTime eCollectTime, int deptNo, double redoVLow, double redoVHigh, int appendUserDesc, string abLowStateDesc, string abHighStateDesc, string lowStateDesc, string highStateDesc, int paritemNo )
		{
			this._sampleTypeNo = sampleTypeNo;
			this._genderNo = genderNo;
			this._ageUnitNo = ageUnitNo;
			this._lowAge = lowAge;
			this._highAge = highAge;
			this._lowValue = lowValue;
			this._highValue = highValue;
			this._abLow = abLow;
			this._abHigh = abHigh;
			this._refRange = refRange;
			this._isDefault = isDefault;
			this._sectionNo = sectionNo;
			this._highDesc = highDesc;
			this._lowDesc = lowDesc;
			this._naturalDesc = naturalDesc;
			this._userDesc = userDesc;
			this._equipNo = equipNo;
			this._unit = unit;
			this._refDesc = refDesc;
			this._hasResultDesc = hasResultDesc;
			this._highStatus = highStatus;
			this._lowStatus = lowStatus;
			this._naturalStatus = naturalStatus;
			this._hHDesc = hHDesc;
			this._hHStatus = hHStatus;
			this._lLDesc = lLDesc;
			this._lLStatus = lLStatus;
			this._bCollectTime = bCollectTime;
			this._eCollectTime = eCollectTime;
			this._deptNo = deptNo;
			this._redoVLow = redoVLow;
			this._redoVHigh = redoVHigh;
			this._appendUserDesc = appendUserDesc;
			this._abLowStateDesc = abLowStateDesc;
			this._abHighStateDesc = abHighStateDesc;
			this._lowStateDesc = lowStateDesc;
			this._highStateDesc = highStateDesc;
			this._paritemNo = paritemNo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set { _sampleTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GenderNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int GenderNo
		{
			get { return _genderNo; }
			set { _genderNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AgeUnitNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AgeUnitNo
		{
			get { return _ageUnitNo; }
			set { _ageUnitNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LowAge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LowAge
		{
			get { return _lowAge; }
			set { _lowAge = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "HighAge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double HighAge
		{
			get { return _highAge; }
			set { _highAge = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LowValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LowValue
		{
			get { return _lowValue; }
			set { _lowValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "HighValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double HighValue
		{
			get { return _highValue; }
			set { _highValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AbLow", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double AbLow
		{
			get { return _abLow; }
			set { _abLow = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AbHigh", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double AbHigh
		{
			get { return _abHigh; }
			set { _abHigh = value; }
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
        [DataDesc(CName = "", ShortCode = "IsDefault", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsDefault
		{
			get { return _isDefault; }
			set { _isDefault = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionNo
		{
			get { return _sectionNo; }
			set { _sectionNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HighDesc", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string HighDesc
		{
			get { return _highDesc; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for HighDesc", value, value.ToString());
				_highDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LowDesc", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LowDesc
		{
			get { return _lowDesc; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for LowDesc", value, value.ToString());
				_lowDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NaturalDesc", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string NaturalDesc
		{
			get { return _naturalDesc; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for NaturalDesc", value, value.ToString());
				_naturalDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserDesc", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UserDesc
		{
			get { return _userDesc; }
			set { _userDesc = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipNo
		{
			get { return _equipNo; }
			set { _equipNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Unit", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Unit
		{
			get { return _unit; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Unit", value, value.ToString());
				_unit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefDesc", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string RefDesc
		{
			get { return _refDesc; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for RefDesc", value, value.ToString());
				_refDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HasResultDesc", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string HasResultDesc
		{
			get { return _hasResultDesc; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for HasResultDesc", value, value.ToString());
				_hasResultDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HighStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string HighStatus
		{
			get { return _highStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for HighStatus", value, value.ToString());
				_highStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LowStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string LowStatus
		{
			get { return _lowStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for LowStatus", value, value.ToString());
				_lowStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NaturalStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string NaturalStatus
		{
			get { return _naturalStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for NaturalStatus", value, value.ToString());
				_naturalStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HHDesc", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string HHDesc
		{
			get { return _hHDesc; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for HHDesc", value, value.ToString());
				_hHDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HHStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string HHStatus
		{
			get { return _hHStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for HHStatus", value, value.ToString());
				_hHStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LLDesc", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LLDesc
		{
			get { return _lLDesc; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for LLDesc", value, value.ToString());
				_lLDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LLStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string LLStatus
		{
			get { return _lLStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for LLStatus", value, value.ToString());
				_lLStatus = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BCollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BCollectTime
		{
			get { return _bCollectTime; }
			set { _bCollectTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ECollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ECollectTime
		{
			get { return _eCollectTime; }
			set { _eCollectTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DeptNo
		{
			get { return _deptNo; }
			set { _deptNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "RedoVLow", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double RedoVLow
		{
			get { return _redoVLow; }
			set { _redoVLow = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "RedoVHigh", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double RedoVHigh
		{
			get { return _redoVHigh; }
			set { _redoVHigh = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AppendUserDesc", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AppendUserDesc
		{
			get { return _appendUserDesc; }
			set { _appendUserDesc = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AbLowStateDesc", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string AbLowStateDesc
		{
			get { return _abLowStateDesc; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for AbLowStateDesc", value, value.ToString());
				_abLowStateDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AbHighStateDesc", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string AbHighStateDesc
		{
			get { return _abHighStateDesc; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for AbHighStateDesc", value, value.ToString());
				_abHighStateDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LowStateDesc", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string LowStateDesc
		{
			get { return _lowStateDesc; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for LowStateDesc", value, value.ToString());
				_lowStateDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HighStateDesc", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string HighStateDesc
		{
			get { return _highStateDesc; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for HighStateDesc", value, value.ToString());
				_highStateDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ParitemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ParitemNo
		{
			get { return _paritemNo; }
			set { _paritemNo = value; }
		}

		
		#endregion
	}
	#endregion
}