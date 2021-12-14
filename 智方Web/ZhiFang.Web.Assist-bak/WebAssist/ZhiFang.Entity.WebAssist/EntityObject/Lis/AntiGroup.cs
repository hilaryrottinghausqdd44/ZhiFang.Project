using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region AntiGroup

	/// <summary>
	/// AntiGroup object for NHibernate mapped table 'AntiGroup'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "AntiGroup", ShortCode = "AntiGroup", Desc = "")]
	public class AntiGroup : BaseEntity
	{
		#region Member Variables
		
        protected int _familyNo;
        protected int _useTypeNo;
        protected int _antiNo;
        protected int _rangeNo;
        protected string _antiUnit;
        protected double _floorValue;
        protected double _ceilingValue;
        protected string _range;
        protected int _dispOrder;
        protected int _isUse;
        protected string _susCept;
        protected int _microNo;
        protected string _susDesc;
        protected string _anticode;
        protected string _antigrouptype;
        protected int _oftenUse;

		#endregion

		#region Constructors

		public AntiGroup() { }

		public AntiGroup( int familyNo, int useTypeNo, int antiNo, int rangeNo, string antiUnit, double floorValue, double ceilingValue, string range, int dispOrder, int isUse, string susCept, int microNo, string susDesc, string anticode, string antigrouptype, int oftenUse )
		{
			this._familyNo = familyNo;
			this._useTypeNo = useTypeNo;
			this._antiNo = antiNo;
			this._rangeNo = rangeNo;
			this._antiUnit = antiUnit;
			this._floorValue = floorValue;
			this._ceilingValue = ceilingValue;
			this._range = range;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._susCept = susCept;
			this._microNo = microNo;
			this._susDesc = susDesc;
			this._anticode = anticode;
			this._antigrouptype = antigrouptype;
			this._oftenUse = oftenUse;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "FamilyNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int FamilyNo
		{
			get { return _familyNo; }
			set { _familyNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UseTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UseTypeNo
		{
			get { return _useTypeNo; }
			set { _useTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AntiNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AntiNo
		{
			get { return _antiNo; }
			set { _antiNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RangeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int RangeNo
		{
			get { return _rangeNo; }
			set { _rangeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AntiUnit", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string AntiUnit
		{
			get { return _antiUnit; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for AntiUnit", value, value.ToString());
				_antiUnit = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FloorValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double FloorValue
		{
			get { return _floorValue; }
			set { _floorValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CeilingValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double CeilingValue
		{
			get { return _ceilingValue; }
			set { _ceilingValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Range", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string Range
		{
			get { return _range; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for Range", value, value.ToString());
				_range = value;
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
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SusCept", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string SusCept
		{
			get { return _susCept; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for SusCept", value, value.ToString());
				_susCept = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MicroNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MicroNo
		{
			get { return _microNo; }
			set { _microNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SusDesc", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string SusDesc
		{
			get { return _susDesc; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for SusDesc", value, value.ToString());
				_susDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Anticode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Anticode
		{
			get { return _anticode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Anticode", value, value.ToString());
				_anticode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Antigrouptype", Desc = "", ContextType = SysDic.All, Length = 2)]
        public virtual string Antigrouptype
		{
			get { return _antigrouptype; }
			set
			{
				if ( value != null && value.Length > 2)
					throw new ArgumentOutOfRangeException("Invalid value for Antigrouptype", value, value.ToString());
				_antigrouptype = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OftenUse", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OftenUse
		{
			get { return _oftenUse; }
			set { _oftenUse = value; }
		}

        
		#endregion
	}
	#endregion
}