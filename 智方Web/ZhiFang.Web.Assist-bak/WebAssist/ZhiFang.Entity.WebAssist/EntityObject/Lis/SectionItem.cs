using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region SectionItem

	/// <summary>
	/// SectionItem object for NHibernate mapped table 'SectionItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SectionItem", ShortCode = "SectionItem", Desc = "")]
	public class SectionItem : BaseEntity
	{
		#region Member Variables
		
        protected int _isDefault;
        protected string _defaultValue;
        protected string _warningTime;
        protected int _bWarningTimeOnlyWorkingDay;
        protected int _bResultIsDesc;
        protected int _bKXJSF;
        protected int _kxjsfType;
        protected int _kxjsfDec;
        protected int _kxjsfWhen;
        protected int _bKxjsfDy;
        protected int _kxjsfDyType;
        protected int _bKxjsfXy;
        protected int _kxjsfXyType;
        protected double _kxjsfDyValue;
        protected double _kxjsfXyValue;
        protected int _defaultPItemNo;
        protected int _defaultEquipNo;
        protected int _userNo;
        protected DateTime? _dataUpdateTime;
        protected string _defaultValue2;
        protected bool _isTran;
        protected int _useReagentCount;
        protected string _reagentCode;
        protected int _visible;
        protected int _dispOrder;

		#endregion

		#region Constructors

		public SectionItem() { }

		public SectionItem( int isDefault, string defaultValue, string warningTime, int bWarningTimeOnlyWorkingDay, int bResultIsDesc, int bKXJSF, int kxjsfType, int kxjsfDec, int kxjsfWhen, int bKxjsfDy, int kxjsfDyType, int bKxjsfXy, int kxjsfXyType, double kxjsfDyValue, double kxjsfXyValue, int defaultPItemNo, int defaultEquipNo, int userNo, DateTime dataAddTime, DateTime dataUpdateTime, string defaultValue2, bool isTran, int useReagentCount, string reagentCode, int visible, int dispOrder )
		{
			this._isDefault = isDefault;
			this._defaultValue = defaultValue;
			this._warningTime = warningTime;
			this._bWarningTimeOnlyWorkingDay = bWarningTimeOnlyWorkingDay;
			this._bResultIsDesc = bResultIsDesc;
			this._bKXJSF = bKXJSF;
			this._kxjsfType = kxjsfType;
			this._kxjsfDec = kxjsfDec;
			this._kxjsfWhen = kxjsfWhen;
			this._bKxjsfDy = bKxjsfDy;
			this._kxjsfDyType = kxjsfDyType;
			this._bKxjsfXy = bKxjsfXy;
			this._kxjsfXyType = kxjsfXyType;
			this._kxjsfDyValue = kxjsfDyValue;
			this._kxjsfXyValue = kxjsfXyValue;
			this._defaultPItemNo = defaultPItemNo;
			this._defaultEquipNo = defaultEquipNo;
			this._userNo = userNo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._defaultValue2 = defaultValue2;
			this._isTran = isTran;
			this._useReagentCount = useReagentCount;
			this._reagentCode = reagentCode;
			this._visible = visible;
			this._dispOrder = dispOrder;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDefault", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsDefault
		{
			get { return _isDefault; }
			set { _isDefault = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DefaultValue", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DefaultValue
		{
			get { return _defaultValue; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for DefaultValue", value, value.ToString());
				_defaultValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WarningTime", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string WarningTime
		{
			get { return _warningTime; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for WarningTime", value, value.ToString());
				_warningTime = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BWarningTimeOnlyWorkingDay", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BWarningTimeOnlyWorkingDay
		{
			get { return _bWarningTimeOnlyWorkingDay; }
			set { _bWarningTimeOnlyWorkingDay = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BResultIsDesc", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BResultIsDesc
		{
			get { return _bResultIsDesc; }
			set { _bResultIsDesc = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BKXJSF", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BKXJSF
		{
			get { return _bKXJSF; }
			set { _bKXJSF = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "KxjsfType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int KxjsfType
		{
			get { return _kxjsfType; }
			set { _kxjsfType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "KxjsfDec", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int KxjsfDec
		{
			get { return _kxjsfDec; }
			set { _kxjsfDec = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "KxjsfWhen", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int KxjsfWhen
		{
			get { return _kxjsfWhen; }
			set { _kxjsfWhen = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BKxjsfDy", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BKxjsfDy
		{
			get { return _bKxjsfDy; }
			set { _bKxjsfDy = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "KxjsfDyType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int KxjsfDyType
		{
			get { return _kxjsfDyType; }
			set { _kxjsfDyType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BKxjsfXy", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BKxjsfXy
		{
			get { return _bKxjsfXy; }
			set { _bKxjsfXy = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "KxjsfXyType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int KxjsfXyType
		{
			get { return _kxjsfXyType; }
			set { _kxjsfXyType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "KxjsfDyValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double KxjsfDyValue
		{
			get { return _kxjsfDyValue; }
			set { _kxjsfDyValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "KxjsfXyValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double KxjsfXyValue
		{
			get { return _kxjsfXyValue; }
			set { _kxjsfXyValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DefaultPItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DefaultPItemNo
		{
			get { return _defaultPItemNo; }
			set { _defaultPItemNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DefaultEquipNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DefaultEquipNo
		{
			get { return _defaultEquipNo; }
			set { _defaultEquipNo = value; }
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
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DefaultValue2", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DefaultValue2
		{
			get { return _defaultValue2; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for DefaultValue2", value, value.ToString());
				_defaultValue2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsTran", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsTran
		{
			get { return _isTran; }
			set { _isTran = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UseReagentCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UseReagentCount
		{
			get { return _useReagentCount; }
			set { _useReagentCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReagentCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ReagentCode
		{
			get { return _reagentCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReagentCode", value, value.ToString());
				_reagentCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        
		#endregion
	}
	#endregion
}