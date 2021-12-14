using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region OSShoppingCart

	/// <summary>
	/// OSShoppingCart object for NHibernate mapped table 'OS_ShoppingCart'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "购物车", ClassCName = "OSShoppingCart", ShortCode = "OSShoppingCart", Desc = "购物车")]
	public class OSShoppingCart : BaseEntity
	{
		#region Member Variables
		
        protected long? _uOFID;
        protected long _userAccountID;
        protected long _userWeiXinUserID;
        protected string _userName;
        protected string _userOpenID;
        protected long? _oSUserConsumerFormID;
        protected long? _recommendationItemProductID;
        protected long? _itemProductClassTreeLinkID;
        protected long _itemID;
        protected string _itemName;
        protected int _count;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected double _marketPrice;
        protected double _greatMasterPrice;
        protected double _discountPrice;
        protected double _discount;
        protected long _status;
		

		#endregion

		#region Constructors

		public OSShoppingCart() { }

		public OSShoppingCart( long uOFID, long userAccountID, long userWeiXinUserID, string userName, string userOpenID, long oSUserConsumerFormID, long recommendationItemProductID, long itemProductClassTreeLinkID, long itemID, string itemName, int count, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, double marketPrice, double greatMasterPrice, double discountPrice, double discount, long status )
		{
			this._uOFID = uOFID;
			this._userAccountID = userAccountID;
			this._userWeiXinUserID = userWeiXinUserID;
			this._userName = userName;
			this._userOpenID = userOpenID;
			this._oSUserConsumerFormID = oSUserConsumerFormID;
			this._recommendationItemProductID = recommendationItemProductID;
			this._itemProductClassTreeLinkID = itemProductClassTreeLinkID;
			this._itemID = itemID;
			this._itemName = itemName;
			this._count = count;
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
			this._status = status;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户订单ID", ShortCode = "UOFID", Desc = "用户订单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? UOFID
		{
			get { return _uOFID; }
			set { _uOFID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户账户信息ID", ShortCode = "UserAccountID", Desc = "用户账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UserAccountID
		{
			get { return _userAccountID; }
			set { _userAccountID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户微信ID", ShortCode = "UserWeiXinUserID", Desc = "用户微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UserWeiXinUserID
		{
			get { return _userWeiXinUserID; }
			set { _userWeiXinUserID = value; }
		}

        [DataMember]
        [DataDesc(CName = "用户姓名", ShortCode = "UserName", Desc = "用户姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string UserName
		{
			get { return _userName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
				_userName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "用户OpenID", ShortCode = "UserOpenID", Desc = "用户OpenID", ContextType = SysDic.All, Length = 50)]
        public virtual string UserOpenID
		{
			get { return _userOpenID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UserOpenID", value, value.ToString());
				_userOpenID = value;
			}
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
        [DataDesc(CName = "检测项目产品分类关系ID", ShortCode = "ItemProductClassTreeLinkID", Desc = "检测项目产品分类关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemProductClassTreeLinkID
		{
			get { return _itemProductClassTreeLinkID; }
			set { _itemProductClassTreeLinkID = value; }
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
        [DataDesc(CName = "", ShortCode = "ItemName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemName
		{
			get { return _itemName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ItemName", value, value.ToString());
				_itemName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Count", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Count
		{
			get { return _count; }
			set { _count = value; }
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

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订单状态", ShortCode = "Status", Desc = "订单状态", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
		{
			get { return _status; }
			set { _status = value; }
		}

		
		#endregion
	}
	#endregion
}