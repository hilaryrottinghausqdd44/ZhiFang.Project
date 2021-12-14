using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region TestItem

	/// <summary>
	/// TestItem object for NHibernate mapped table 'TestItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "TestItem", ShortCode = "TestItem", Desc = "")]
	public class TestItem : BaseEntity
	{
		#region Member Variables
		
        protected long _itemGUID;
        protected string _cName;
        protected string _eName;
        protected string _shortName;
        protected string _shortCode;
        protected string _diagMethod;
        protected string _unit;
        protected int _isCalc;
        protected int _visible;
        protected int _dispOrder;
        protected int _prec;
        protected int _isProfile;
        protected string _orderNo;
        protected string _standardCode;
        protected string _itemDesc;
        protected double _fWorkLoad;
        protected int _secretgrade;
        protected int _cuegrade;
        protected int _isDoctorItem;
        protected int _ischargeItem;
        protected string _tstamp;
        protected int _hisDispOrder;
        protected int _superGroupNo;
        protected double _price;
        protected string _specialType;
        protected string _specialSection;
        protected double _lowprice;
        protected int _specTypeNo;
        protected int _isCombiItem;
        protected int _isHistoryItem;
        protected double _theoryprice;
        protected double _actualprice;
        protected string _itemCode;
        protected string _miniChargeUnit;
        protected double _labIsReply;
        protected double _labStatusFlag;
        protected double _labUploadFlag;
        protected DateTime? _labUploadDate;
        protected string _color;
        //protected byte[] _dTimeStampe;
        protected double? _BonusPercent;
        protected double? _CostPrice;
        protected double? _InspectionPrice;


        #endregion

        #region Constructors

        public TestItem() { }

		public TestItem( long itemGUID, string cName, string eName, string shortName, string shortCode, string diagMethod, string unit, int isCalc, int visible, int dispOrder, int prec, int isProfile, string orderNo, string standardCode, string itemDesc, double fWorkLoad, int secretgrade, int cuegrade, int isDoctorItem, int ischargeItem, string tstamp, int hisDispOrder, int superGroupNo, double price, string specialType, string specialSection, double lowprice, int specTypeNo, int isCombiItem, int isHistoryItem, double theoryprice, double actualprice, string itemCode, string miniChargeUnit, double labIsReply, double labStatusFlag, double labUploadFlag, DateTime labUploadDate, string color, byte[] dTimeStampe )
		{
			this._itemGUID = itemGUID;
			this._cName = cName;
			this._eName = eName;
			this._shortName = shortName;
			this._shortCode = shortCode;
			this._diagMethod = diagMethod;
			this._unit = unit;
			this._isCalc = isCalc;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._prec = prec;
			this._isProfile = isProfile;
			this._orderNo = orderNo;
			this._standardCode = standardCode;
			this._itemDesc = itemDesc;
			this._fWorkLoad = fWorkLoad;
			this._secretgrade = secretgrade;
			this._cuegrade = cuegrade;
			this._isDoctorItem = isDoctorItem;
			this._ischargeItem = ischargeItem;
			this._tstamp = tstamp;
			this._hisDispOrder = hisDispOrder;
			this._superGroupNo = superGroupNo;
			this._price = price;
			this._specialType = specialType;
			this._specialSection = specialSection;
			this._lowprice = lowprice;
			this._specTypeNo = specTypeNo;
			this._isCombiItem = isCombiItem;
			this._isHistoryItem = isHistoryItem;
			this._theoryprice = theoryprice;
			this._actualprice = actualprice;
			this._itemCode = itemCode;
			this._miniChargeUnit = miniChargeUnit;
			this._labIsReply = labIsReply;
			this._labStatusFlag = labStatusFlag;
			this._labUploadFlag = labUploadFlag;
			this._labUploadDate = labUploadDate;
			this._color = color;
			//this._dTimeStampe = dTimeStampe;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemGUID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ItemGUID
		{
			get { return _itemGUID; }
			set { _itemGUID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 255)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ShortName
		{
			get { return _shortName; }
			set
			{
				_shortName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DiagMethod", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string DiagMethod
		{
			get { return _diagMethod; }
			set
			{
				_diagMethod = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Unit", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Unit
		{
			get { return _unit; }
			set
			{
				_unit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCalc", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCalc
		{
			get { return _isCalc; }
			set { _isCalc = value; }
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

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Prec", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Prec
		{
			get { return _prec; }
			set { _prec = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsProfile", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsProfile
		{
			get { return _isProfile; }
			set { _isProfile = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrderNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string OrderNo
		{
			get { return _orderNo; }
			set
			{
				_orderNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandardCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string StandardCode
		{
			get { return _standardCode; }
			set
			{
				_standardCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemDesc", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string ItemDesc
		{
			get { return _itemDesc; }
			set
			{
				_itemDesc = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FWorkLoad", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double FWorkLoad
		{
			get { return _fWorkLoad; }
			set { _fWorkLoad = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Secretgrade", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Secretgrade
		{
			get { return _secretgrade; }
			set { _secretgrade = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Cuegrade", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Cuegrade
		{
			get { return _cuegrade; }
			set { _cuegrade = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDoctorItem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsDoctorItem
		{
			get { return _isDoctorItem; }
			set { _isDoctorItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IschargeItem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IschargeItem
		{
			get { return _ischargeItem; }
			set { _ischargeItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Tstamp", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string Tstamp
		{
			get { return _tstamp; }
			set
			{				
				_tstamp = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int HisDispOrder
		{
			get { return _hisDispOrder; }
			set { _hisDispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SuperGroupNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SuperGroupNo
		{
			get { return _superGroupNo; }
			set { _superGroupNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SpecialType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SpecialType
		{
			get { return _specialType; }
			set
			{
				_specialType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SpecialSection", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SpecialSection
		{
			get { return _specialSection; }
			set
			{
				_specialSection = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Lowprice", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Lowprice
		{
			get { return _lowprice; }
			set { _lowprice = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SpecTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SpecTypeNo
		{
			get { return _specTypeNo; }
			set { _specTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCombiItem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCombiItem
		{
			get { return _isCombiItem; }
			set { _isCombiItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsHistoryItem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsHistoryItem
		{
			get { return _isHistoryItem; }
			set { _isHistoryItem = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Theoryprice", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Theoryprice
		{
			get { return _theoryprice; }
			set { _theoryprice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Actualprice", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Actualprice
		{
			get { return _actualprice; }
			set { _actualprice = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemCode
		{
			get { return _itemCode; }
			set
			{
				_itemCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MiniChargeUnit", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string MiniChargeUnit
		{
			get { return _miniChargeUnit; }
			set
			{			
				_miniChargeUnit = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabIsReply", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LabIsReply
		{
			get { return _labIsReply; }
			set { _labIsReply = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabStatusFlag", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LabStatusFlag
		{
			get { return _labStatusFlag; }
			set { _labStatusFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabUploadFlag", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LabUploadFlag
		{
			get { return _labUploadFlag; }
			set { _labUploadFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabUploadDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LabUploadDate
		{
			get { return _labUploadDate; }
			set { _labUploadDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Color", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string Color
		{
			get { return _color; }
			set
			{
				_color = value;
			}
		}

  //      [DataMember]
  //      [DataDesc(CName = "", ShortCode = "DTimeStampe", Desc = "", ContextType = SysDic.All, Length = 8)]
  //      public virtual byte[] DTimeStampe
		//{
		//	get { return _dTimeStampe; }
		//	set { _dTimeStampe = value; }
		//}

        [DataMember]
        [DataDesc(CName = "咨询费比率", ShortCode = "BonusPercent", Desc = "咨询费比率", ContextType = SysDic.All, Length = 50)]
        public virtual Double? BonusPercent
        {
            get { return _BonusPercent; }
            set
            {
                _BonusPercent = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "成本价格", ShortCode = "CostPrice", Desc = "成本价格", ContextType = SysDic.All, Length = 50)]
        public virtual Double? CostPrice
        {
            get { return _CostPrice; }
            set
            {
                _CostPrice = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检查费", ShortCode = "InspectionPrice", Desc = "检查费", ContextType = SysDic.All, Length = 50)]
        public virtual Double? InspectionPrice
        {
            get { return _InspectionPrice; }
            set
            {
                _InspectionPrice = value;
            }
        }
        #endregion
    }
	#endregion
}