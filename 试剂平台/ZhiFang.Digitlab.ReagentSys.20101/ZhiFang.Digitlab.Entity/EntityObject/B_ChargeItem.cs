using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BChargeItem

	/// <summary>
	/// BChargeItem object for NHibernate mapped table 'B_ChargeItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "计费项目表：0-开单项目（包含医嘱项目、套餐项目）、1-检验耗材", ClassCName = "BChargeItem", ShortCode = "BChargeItem", Desc = "计费项目表：0-开单项目（包含医嘱项目、套餐项目）、1-检验耗材")]
	public class BChargeItem : BaseEntity
	{
		#region Member Variables
		
        protected int _chargeItemType;
        protected decimal _price;
        protected bool _isUsed;
        protected DateTime? _dataUpdateTime;
		protected ItemAllItem _itemAllItem;
		protected IList<MEChargeInfo> _mEChargeInfoList; 

		#endregion

		#region Constructors

		public BChargeItem() { }

		public BChargeItem( long labID, int chargeItemType, decimal price, bool isUsed, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem )
		{
			this._labID = labID;
			this._chargeItemType = chargeItemType;
			this._price = price;
			this._isUsed = isUsed;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "计费项目类型：0-开单项目；1-检验耗材", ShortCode = "ChargeItemType", Desc = "计费项目类型：0-开单项目；1-检验耗材", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeItemType
		{
			get { return _chargeItemType; }
			set { _chargeItemType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "价格", ShortCode = "Price", Desc = "价格", ContextType = SysDic.All, Length = 9)]
        public virtual decimal Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否在用", ShortCode = "IsUsed", Desc = "是否在用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUsed
		{
			get { return _isUsed; }
			set { _isUsed = value; }
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
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "计费操作表", ShortCode = "MEChargeInfoList", Desc = "计费操作表")]
		public virtual IList<MEChargeInfo> MEChargeInfoList
		{
			get
			{
				if (_mEChargeInfoList==null)
				{
					_mEChargeInfoList = new List<MEChargeInfo>();
				}
				return _mEChargeInfoList;
			}
			set { _mEChargeInfoList = value; }
		}

        
		#endregion
	}
	#endregion
}