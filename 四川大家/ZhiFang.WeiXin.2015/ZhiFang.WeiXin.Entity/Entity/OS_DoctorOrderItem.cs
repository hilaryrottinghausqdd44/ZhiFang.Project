using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region OSDoctorOrderItem

	/// <summary>
	/// OSDoctorOrderItem object for NHibernate mapped table 'OS_DoctorOrderItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "医生医嘱项目", ClassCName = "OSDoctorOrderItem", ShortCode = "OSDoctorOrderItem", Desc = "医生医嘱项目")]
	public class OSDoctorOrderItem : BaseEntity
	{
		#region Member Variables
		
        protected long _areaID;
        protected long? _dOFID;
        protected long? _recommendationItemProductID;
        protected long? _itemID;
        protected string _ItemNo;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected double _marketPrice;
        protected double _greatMasterPrice;
        protected double _discountPrice;
        protected double _discount;
        protected double? _ItemBonusPrice;
        protected long _hospitalID;
        protected string _ItemCName;

        #endregion

        #region Constructors

        public OSDoctorOrderItem() { }

		public OSDoctorOrderItem( long areaID, long dOFID, long recommendationItemProductID, long itemID, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, double marketPrice, double greatMasterPrice, double discountPrice, double discount, long hospitalID )
		{
			this._areaID = areaID;
			this._dOFID = dOFID;
			this._recommendationItemProductID = recommendationItemProductID;
			this._itemID = itemID;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._marketPrice = marketPrice;
			this._greatMasterPrice = greatMasterPrice;
			this._discountPrice = discountPrice;
			this._discount = discount;
			this._hospitalID = hospitalID;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AreaID
		{
			get { return _areaID; }
			set { _areaID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DOFID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DOFID
		{
			get { return _dOFID; }
			set { _dOFID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "特推项目产品ID", ShortCode = "RecommendationItemProductID", Desc = "特推项目产品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RecommendationItemProductID
		{
			get { return _recommendationItemProductID; }
			set { _recommendationItemProductID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目ID", ShortCode = "ItemID", Desc = "项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemID
		{
			get { return _itemID; }
			set { _itemID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目No", ShortCode = "ItemNo", Desc = "项目No", ContextType = SysDic.All, Length = 8)]
        public virtual string ItemNo
        {
            get { return _ItemNo; }
            set { _ItemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目中文名称", ShortCode = "ItemCName", Desc = "项目中文名称", ContextType = SysDic.All, Length =50)]
        public virtual string ItemCName
        {
            get { return _ItemCName; }
            set { _ItemCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "市场价格", ShortCode = "MarketPrice", Desc = "市场价格", ContextType = SysDic.All, Length = 8)]
        public virtual double MarketPrice
		{
			get { return _marketPrice; }
			set { _marketPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "大家价格", ShortCode = "GreatMasterPrice", Desc = "大家价格", ContextType = SysDic.All, Length = 8)]
        public virtual double GreatMasterPrice
		{
			get { return _greatMasterPrice; }
			set { _greatMasterPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "折扣价格", ShortCode = "DiscountPrice", Desc = "折扣价格", ContextType = SysDic.All, Length = 8)]
        public virtual double DiscountPrice
		{
			get { return _discountPrice; }
			set { _discountPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目固定咨询费", ShortCode = "ItemBonusPrice", Desc = "项目固定咨询费", ContextType = SysDic.All, Length = 8)]
        public virtual double? ItemBonusPrice
        {
            get { return _ItemBonusPrice; }
            set { _ItemBonusPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "折扣率", ShortCode = "Discount", Desc = "折扣率", ContextType = SysDic.All, Length = 8)]
        public virtual double Discount
		{
			get { return _discount; }
			set { _discount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院ID", ShortCode = "HospitalID", Desc = "医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HospitalID
		{
			get { return _hospitalID; }
			set { _hospitalID = value; }
		}

		
		#endregion
	}
	#endregion
}