using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodInCheckedItem

	/// <summary>
	/// BloodInCheckedItem object for NHibernate mapped table 'Blood_In_CheckedItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "盘点明细表", ClassCName = "BloodInCheckedItem", ShortCode = "BloodInCheckedItem", Desc = "盘点明细表")]
	public class BloodInCheckedItem : BaseEntity
	{
		#region Member Variables
		
        protected int _isBloodIn;
        protected string _b3Code;
        protected string _bBagCode;
        protected string _pCode;
        protected DateTime? _invalidDate;
        protected DateTime? _sCanTime;
        protected long? _sCanUserID;
        protected string _sCanUser;
        protected string _aBOType;
        protected string _rhType;
        protected double _bCount;
        protected string _invalidCode;
        protected string _aboRhCode;
        protected int _dispOrder;
        protected bool _visible;
		protected BloodABO _bloodABO;
		protected BloodClass _bloodClass;
		protected BDict _bloodIceBox;
		protected BloodInChecked _bloodInChecked;
		protected BloodQtyDtl _bloodQtyDtl;
		protected BloodStyle _bloodStyle;
		protected BloodUnit _bloodUnit;

		#endregion

		#region Constructors

		public BloodInCheckedItem() { }

		public BloodInCheckedItem( long labID, int isBloodIn, string b3Code, string bBagCode, string pCode, DateTime invalidDate, DateTime sCanTime, long sCanUserID, string sCanUser, string aBOType, string rhType, double bCount, string invalidCode, string aboRhCode, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp, BloodABO aboNo, BloodClass bloodClass, BDict iceboxNo, BloodInChecked bloodInChecked, BloodQtyDtl bloodQtyDtl, BloodStyle bloodStyle, BloodUnit bloodUnit )
		{
			this._labID = labID;
			this._isBloodIn = isBloodIn;
			this._b3Code = b3Code;
			this._bBagCode = bBagCode;
			this._pCode = pCode;
			this._invalidDate = invalidDate;
			this._sCanTime = sCanTime;
			this._sCanUserID = sCanUserID;
			this._sCanUser = sCanUser;
			this._aBOType = aBOType;
			this._rhType = rhType;
			this._bCount = bCount;
			this._invalidCode = invalidCode;
			this._aboRhCode = aboRhCode;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodABO = aboNo;
			this._bloodClass = bloodClass;
			this._bloodIceBox = iceboxNo;
			this._bloodInChecked = bloodInChecked;
			this._bloodQtyDtl = bloodQtyDtl;
			this._bloodStyle = bloodStyle;
			this._bloodUnit = bloodUnit;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "是否系统库存", ShortCode = "IsBloodIn", Desc = "是否系统库存", ContextType = SysDic.All, Length = 4)]
        public virtual int IsBloodIn
		{
			get { return _isBloodIn; }
			set { _isBloodIn = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋唯一号", ShortCode = "B3Code", Desc = "血袋唯一号", ContextType = SysDic.All, Length = 50)]
        public virtual string B3Code
		{
			get { return _b3Code; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for B3Code", value, value.ToString());
				_b3Code = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "血袋号", ShortCode = "BBagCode", Desc = "血袋号", ContextType = SysDic.All, Length = 50)]
        public virtual string BBagCode
		{
			get { return _bBagCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BBagCode", value, value.ToString());
				_bBagCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "产品码", ShortCode = "PCode", Desc = "产品码", ContextType = SysDic.All, Length = 50)]
        public virtual string PCode
		{
			get { return _pCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PCode", value, value.ToString());
				_pCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失效日期", ShortCode = "InvalidDate", Desc = "失效日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InvalidDate
		{
			get { return _invalidDate; }
			set { _invalidDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "扫描时间", ShortCode = "SCanTime", Desc = "扫描时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? SCanTime
		{
			get { return _sCanTime; }
			set { _sCanTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "扫描者", ShortCode = "SCanUserID", Desc = "扫描者", ContextType = SysDic.All, Length = 8)]
		public virtual long? SCanUserID
		{
			get { return _sCanUserID; }
			set { _sCanUserID = value; }
		}

        [DataMember]
        [DataDesc(CName = "扫描者", ShortCode = "SCanUser", Desc = "扫描者", ContextType = SysDic.All, Length = 50)]
        public virtual string SCanUser
		{
			get { return _sCanUser; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SCanUser", value, value.ToString());
				_sCanUser = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "ABO血型", ShortCode = "ABOType", Desc = "ABO血型", ContextType = SysDic.All, Length = 20)]
        public virtual string ABOType
		{
			get { return _aBOType; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ABOType", value, value.ToString());
				_aBOType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "RH血型", ShortCode = "RhType", Desc = "RH血型", ContextType = SysDic.All, Length = 20)]
        public virtual string RhType
		{
			get { return _rhType; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for RhType", value, value.ToString());
				_rhType = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "规格", ShortCode = "BCount", Desc = "规格", ContextType = SysDic.All, Length = 8)]
        public virtual double BCount
		{
			get { return _bCount; }
			set { _bCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "失效日期码", ShortCode = "InvalidCode", Desc = "失效日期码", ContextType = SysDic.All, Length = 20)]
        public virtual string InvalidCode
		{
			get { return _invalidCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for InvalidCode", value, value.ToString());
				_invalidCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "血型码", ShortCode = "AboRhCode", Desc = "血型码", ContextType = SysDic.All, Length = 20)]
        public virtual string AboRhCode
		{
			get { return _aboRhCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for AboRhCode", value, value.ToString());
				_aboRhCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "血型表", ShortCode = "BloodABO", Desc = "血型表")]
		public virtual BloodABO BloodABO
		{
			get { return _bloodABO; }
			set { _bloodABO = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋分类", ShortCode = "BloodClass", Desc = "血袋分类")]
		public virtual BloodClass BloodClass
		{
			get { return _bloodClass; }
			set { _bloodClass = value; }
		}

        [DataMember]
        [DataDesc(CName = "冰箱", ShortCode = "BloodIceBox", Desc = "冰箱")]
		public virtual BDict BloodIceBox
		{
			get { return _bloodIceBox; }
			set { _bloodIceBox = value; }
		}

        [DataMember]
        [DataDesc(CName = "盘点主单表", ShortCode = "BloodInChecked", Desc = "盘点主单表")]
		public virtual BloodInChecked BloodInChecked
		{
			get { return _bloodInChecked; }
			set { _bloodInChecked = value; }
		}

        [DataMember]
        [DataDesc(CName = "库存表", ShortCode = "BloodQtyDtl", Desc = "库存表")]
		public virtual BloodQtyDtl BloodQtyDtl
		{
			get { return _bloodQtyDtl; }
			set { _bloodQtyDtl = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品单位", ShortCode = "BloodUnit", Desc = "血制品单位")]
		public virtual BloodUnit BloodUnit
		{
			get { return _bloodUnit; }
			set { _bloodUnit = value; }
		}

        
		#endregion
	}
	#endregion
}