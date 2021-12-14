using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BLabTestItem

	/// <summary>
	/// BLabTestItem object for NHibernate mapped table 'B_LabTestItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BLabTestItem", ShortCode = "BLabTestItem", Desc = "")]
	public class BLabTestItem : BaseEntity
	{
		#region Member Variables
		
        protected string _labCode;
        protected string _itemNo;
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
        protected double _price;
        protected double _MarketPrice;
        protected double _GreatMasterPrice;
        protected int _isCombiItem;
        protected string _color;
        protected string _standCode;
        protected string _zFStandCode;
        protected int _useFlag;
        protected int _labSuperGroupNo;
        protected string _Pic;
        protected double? _BonusPercent;
        protected double? _CostPrice;
        protected double? _InspectionPrice;

        #endregion

        #region Constructors

        public BLabTestItem() { }

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabCode", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string LabCode
		{
			get { return _labCode; }
			set
			{
				_labCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ItemNo
		{
			get { return _itemNo; }
			set
			{
				_itemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 4000)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 400)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortName", Desc = "", ContextType = SysDic.All, Length = 400)]
        public virtual string ShortName
		{
			get { return _shortName; }
			set
			{
				_shortName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 400)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DiagMethod", Desc = "", ContextType = SysDic.All, Length = 400)]
        public virtual string DiagMethod
		{
			get { return _diagMethod; }
			set
			{
				_diagMethod = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Unit", Desc = "", ContextType = SysDic.All, Length = 400)]
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
        [DataDesc(CName = "", ShortCode = "OrderNo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string OrderNo
		{
			get { return _orderNo; }
			set
			{
				_orderNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandardCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string StandardCode
		{
			get { return _standardCode; }
			set
			{
				_standardCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemDesc", Desc = "", ContextType = SysDic.All, Length = 4000)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际价格（执行价格）", ShortCode = "Price", Desc = "实际价格（执行价格）", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "市场价格(三甲价格)", ShortCode = "MarketPrice", Desc = "市场价格(三甲价格)", ContextType = SysDic.All, Length = 8)]
        public virtual double MarketPrice
        {
            get { return _MarketPrice; }
            set { _MarketPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "内部价格(三甲价格)", ShortCode = "GreatMasterPrice", Desc = "内部价格(三甲价格)", ContextType = SysDic.All, Length = 8)]
        public virtual double GreatMasterPrice
        {
            get { return _GreatMasterPrice; }
            set { _GreatMasterPrice = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCombiItem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCombiItem
		{
			get { return _isCombiItem; }
			set { _isCombiItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Color", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string Color
		{
			get { return _color; }
			set
			{
				_color = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandCode", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				_standCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZFStandCode", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ZFStandCode
		{
			get { return _zFStandCode; }
			set
			{
				_zFStandCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UseFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UseFlag
		{
			get { return _useFlag; }
			set { _useFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabSuperGroupNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int LabSuperGroupNo
		{
			get { return _labSuperGroupNo; }
			set { _labSuperGroupNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Pic", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string Pic
        {
            get { return _Pic; }
            set
            {
                _Pic = value;
            }
        }
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

        [DataMember]
        [DataDesc(CName = "是否对照", ShortCode = "IsMappingFlag", Desc = "是否对照", ContextType = SysDic.All, Length = 50)]
        public virtual bool IsMappingFlag { get; set; }

        [DataMember]
        [DataDesc(CName = "检验细项", ShortCode = "SubBLabTestItem", Desc = "检验细项", ContextType = SysDic.All, Length = 50)]
        public virtual List<BLabTestItem> SubBLabTestItem { get; set; }
        #endregion
    }
	#endregion
}