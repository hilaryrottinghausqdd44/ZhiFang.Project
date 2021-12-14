using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherStorageItem

	/// <summary>
	/// OtherStorageItem object for NHibernate mapped table 'Other_StorageItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "入库明细", ClassCName = "OtherStorageItem", ShortCode = "OtherStorageItem", Desc = "入库明细")]
    public class OtherStorageItem : BaseEntity
	{
		#region Member Variables
		
        protected string _productCode;
        protected string _productName;
        protected string _specification;
        protected string _unit;
        protected int _amount;
        protected decimal _price;
        protected decimal _sumPrice;
        protected DateTime? _produceTime;
        protected string _batchCode;
        protected string _comment;
        protected DateTime? _dataUpdateTime;
		protected BProduce _bProduce;
		protected OtherStorageSingle _otherStorageSingle;

		#endregion

		#region Constructors

		public OtherStorageItem() { }

		public OtherStorageItem( string productCode, string productName, string specification, string unit, int amount, decimal price, decimal sumPrice, DateTime produceTime, string batchCode, string comment, BProduce bProduce, OtherStorageSingle otherStorageSingle )
		{
			this._productCode = productCode;
			this._productName = productName;
			this._specification = specification;
			this._unit = unit;
			this._amount = amount;
			this._price = price;
			this._sumPrice = sumPrice;
			this._produceTime = produceTime;
			this._batchCode = batchCode;
			this._comment = comment;
			this._bProduce = bProduce;
			this._otherStorageSingle = otherStorageSingle;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "产品编码", ShortCode = "ProductCode", Desc = "产品编码", ContextType = SysDic.All, Length = 40)]
        public virtual string ProductCode
		{
			get { return _productCode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ProductCode", value, value.ToString());
				_productCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "产品名称", ShortCode = "ProductName", Desc = "产品名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ProductName
		{
			get { return _productName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ProductName", value, value.ToString());
				_productName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "规格型号", ShortCode = "Specification", Desc = "规格型号", ContextType = SysDic.All, Length = 200)]
        public virtual string Specification
		{
			get { return _specification; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Specification", value, value.ToString());
				_specification = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "Unit", Desc = "单位", ContextType = SysDic.All, Length = 40)]
        public virtual string Unit
		{
			get { return _unit; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Unit", value, value.ToString());
				_unit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "数量", ShortCode = "Amount", Desc = "数量", ContextType = SysDic.All, Length = 4)]
        public virtual int Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采购价", ShortCode = "Price", Desc = "采购价", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "金额", ShortCode = "SumPrice", Desc = "金额", ContextType = SysDic.All, Length = 8)]
        public virtual decimal SumPrice
		{
			get { return _sumPrice; }
			set { _sumPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生产日期", ShortCode = "ProduceTime", Desc = "生产日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ProduceTime
		{
			get { return _produceTime; }
			set { _produceTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "批次编号", ShortCode = "BatchCode", Desc = "批次编号", ContextType = SysDic.All, Length = 40)]
        public virtual string BatchCode
		{
			get { return _batchCode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for BatchCode", value, value.ToString());
				_batchCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "产品", ShortCode = "BProduce", Desc = "产品", ContextType = SysDic.All)]
		public virtual BProduce BProduce
		{
			get { return _bProduce; }
			set { _bProduce = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库单", ShortCode = "OtherStorageSingle", Desc = "入库单", ContextType = SysDic.All)]
		public virtual OtherStorageSingle OtherStorageSingle
		{
			get { return _otherStorageSingle; }
			set { _otherStorageSingle = value; }
		}

		#endregion
	}
	#endregion
}