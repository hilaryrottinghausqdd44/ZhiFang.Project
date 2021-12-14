using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region OSUserConsumerItem

	/// <summary>
	/// OSUserConsumerItem object for NHibernate mapped table 'OS_UserConsumerItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "用户消费项目", ClassCName = "OSUserConsumerItem", ShortCode = "OSUserConsumerItem", Desc = "用户消费项目")]
	public class OSUserConsumerItem : BaseEntity
	{
		#region Member Variables
		
        protected long _areaID;
        protected long _hospitalID;
        protected long? _oSUserConsumerFormID;
        protected long? _recommendationItemProductID;
        protected long? _uOIID;
        protected long _itemID;
        protected string _ItemNo;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected double _marketPrice;
        protected double _greatMasterPrice;
        protected double _discountPrice;
        protected double _discount;
        protected string _ItemCName;

        #endregion

        #region Constructors

        public OSUserConsumerItem() { }

		public OSUserConsumerItem( long areaID, long hospitalID, long oSUserConsumerFormID, long recommendationItemProductID, long uOIID, long itemID, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, double marketPrice, double greatMasterPrice, double discountPrice, double discount )
		{
			this._areaID = areaID;
			this._hospitalID = hospitalID;
			this._oSUserConsumerFormID = oSUserConsumerFormID;
			this._recommendationItemProductID = recommendationItemProductID;
			this._uOIID = uOIID;
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
        [DataDesc(CName = "医院ID", ShortCode = "HospitalID", Desc = "医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HospitalID
		{
			get { return _hospitalID; }
			set { _hospitalID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OSUserConsumerFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OSUserConsumerFormID
		{
			get { return _oSUserConsumerFormID; }
			set { _oSUserConsumerFormID = value; }
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
        [DataDesc(CName = "用户订单项目ID", ShortCode = "UOIID", Desc = "用户订单项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? UOIID
		{
			get { return _uOIID; }
			set { _uOIID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目ID", ShortCode = "ItemID", Desc = "项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ItemID
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
        [DataDesc(CName = "项目中文名称", ShortCode = "ItemCName", Desc = "项目中文名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "折扣率", ShortCode = "Discount", Desc = "折扣率", ContextType = SysDic.All, Length = 8)]
        public virtual double Discount
		{
			get { return _discount; }
			set { _discount = value; }
		}

		
		#endregion
	}
	#endregion
}