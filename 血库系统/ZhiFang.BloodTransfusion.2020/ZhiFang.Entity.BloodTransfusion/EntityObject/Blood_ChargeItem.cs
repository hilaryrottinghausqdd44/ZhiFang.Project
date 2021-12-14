using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodChargeItem

	/// <summary>
	/// BloodChargeItem object for NHibernate mapped table 'Blood_ChargeItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "费用项目表", ClassCName = "BloodChargeItem", ShortCode = "BloodChargeItem", Desc = "费用项目表")]
	public class BloodChargeItem : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _sName;
        protected string _shortCode;
        protected string _pinYinZiTou;
        protected bool _isGroup;
        protected double _modulus;
        protected decimal _inPrice;
        protected decimal _outPrice;
        protected string _hisOrderCode;
        protected string _hisChargeItemUnits;
        protected string _hisPriceDemo;
        protected string _chargeItemSpec;
        protected int _dispOrder;
        protected bool _isUse;
		protected BloodChargeItemType _bloodChargeItemType;
		#endregion

		#region Constructors

		public BloodChargeItem() { }

		public BloodChargeItem( long labID, string chargeItemName, string sName, string shortCode, string pinYinZiTou, bool isGroup, double modulus, decimal inPrice, decimal outPrice, string hisChargeItemNo, string hisChargeItemUnits, string hisPriceDemo, string chargeItemSpec, int dispOrder, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, BloodChargeItemType bloodChargeItemType )
		{
			this._labID = labID;
			this._cName = chargeItemName;
			this._sName = sName;
			this._shortCode = shortCode;
			this._pinYinZiTou = pinYinZiTou;
			this._isGroup = isGroup;
			this._modulus = modulus;
			this._inPrice = inPrice;
			this._outPrice = outPrice;
			this._hisOrderCode = hisChargeItemNo;
			this._hisChargeItemUnits = hisChargeItemUnits;
			this._hisPriceDemo = hisPriceDemo;
			this._chargeItemSpec = chargeItemSpec;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodChargeItemType = bloodChargeItemType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "费用名称", ShortCode = "CName", Desc = "费用名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "拼音字头", ShortCode = "PinYinZiTou", Desc = "拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否组合费用", ShortCode = "IsGroup", Desc = "是否组合费用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsGroup
		{
			get { return _isGroup; }
			set { _isGroup = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "系数", ShortCode = "Modulus", Desc = "系数", ContextType = SysDic.All, Length = 8)]
        public virtual double Modulus
		{
			get { return _modulus; }
			set { _modulus = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入价", ShortCode = "InPrice", Desc = "入价", ContextType = SysDic.All, Length = 8)]
        public virtual decimal InPrice
		{
			get { return _inPrice; }
			set { _inPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出价", ShortCode = "OutPrice", Desc = "出价", ContextType = SysDic.All, Length = 8)]
        public virtual decimal OutPrice
		{
			get { return _outPrice; }
			set { _outPrice = value; }
		}

        [DataMember]
        [DataDesc(CName = "HIS费用对照码", ShortCode = "HisOrderCode", Desc = "HIS费用对照码", ContextType = SysDic.All, Length = 20)]
        public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "费用单位", ShortCode = "HisChargeItemUnits", Desc = "费用单位", ContextType = SysDic.All, Length = 20)]
        public virtual string HisChargeItemUnits
		{
			get { return _hisChargeItemUnits; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisChargeItemUnits", value, value.ToString());
				_hisChargeItemUnits = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "HisPriceDemo", Desc = "备注", ContextType = SysDic.All, Length = 20)]
        public virtual string HisPriceDemo
		{
			get { return _hisPriceDemo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisPriceDemo", value, value.ToString());
				_hisPriceDemo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "费用规格", ShortCode = "ChargeItemSpec", Desc = "费用规格", ContextType = SysDic.All, Length = 200)]
        public virtual string ChargeItemSpec
		{
			get { return _chargeItemSpec; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ChargeItemSpec", value, value.ToString());
				_chargeItemSpec = value;
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
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "费用项目类型表 对收费项目进行分类描述", ShortCode = "BloodChargeItemType", Desc = "费用项目类型表 对收费项目进行分类描述")]
		public virtual BloodChargeItemType BloodChargeItemType
		{
			get { return _bloodChargeItemType; }
			set { _bloodChargeItemType = value; }
		}

		#endregion
	}
	#endregion
}