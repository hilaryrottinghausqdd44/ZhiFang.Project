using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region BmsCenOrderDtlHistory

	/// <summary>
	/// BmsCenOrderDtlHistory object for NHibernate mapped table 'BmsCenOrderDtlHistory'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "平台订货明细历史表", ClassCName = "BmsCenOrderDtlHistory", ShortCode = "BmsCenOrderDtlHistory", Desc = "平台订货明细历史表")]
	public class BmsCenOrderDtlHistory : BaseEntity
	{
		#region Member Variables
		
        protected string _orderDtlNo;
        protected string _orderDocNo;
        protected string _prodGoodsNo;
        protected string _prodOrgName;
        protected string _goodsName;
        protected string _goodsUnit;
        protected string _unitMemo;
        protected double _goodsQty;
        protected double _currentQty;
        protected int _iOFlag;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected double _price;
        protected DateTime? _dataUpdateTime;
        protected BmsCenOrderDocHistory _bmsCenOrderDocHistory;
		protected CenOrg _prod;
		protected Goods _goods;

		#endregion

		#region Constructors

		public BmsCenOrderDtlHistory() { }

		public BmsCenOrderDtlHistory( string orderDtlNo, string orderDocNo, string prodGoodsNo, string prodOrgName, string goodsName, string goodsUnit, string unitMemo, double goodsQty, double currentQty, int iOFlag, string zX1, string zX2, string zX3, BmsCenOrderDocHistory bmsCenOrderDocHistory, CenOrg prod, Goods goods )
		{
			this._orderDtlNo = orderDtlNo;
			this._orderDocNo = orderDocNo;
			this._prodGoodsNo = prodGoodsNo;
			this._prodOrgName = prodOrgName;
			this._goodsName = goodsName;
			this._goodsUnit = goodsUnit;
			this._unitMemo = unitMemo;
			this._goodsQty = goodsQty;
			this._currentQty = currentQty;
			this._iOFlag = iOFlag;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._bmsCenOrderDocHistory = bmsCenOrderDocHistory;
			this._prod = prod;
			this._goods = goods;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "平台订货明细单号", ShortCode = "OrderDtlNo", Desc = "平台订货明细单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDtlNo
		{
			get { return _orderDtlNo; }
			set
			{
				_orderDtlNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "平台订货总单号", ShortCode = "OrderDocNo", Desc = "平台订货总单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDocNo
		{
			get { return _orderDocNo; }
			set
			{
				_orderDocNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "厂商产品编号", ShortCode = "ProdGoodsNo", Desc = "厂商产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string ProdGoodsNo
		{
			get { return _prodGoodsNo; }
			set
			{
				_prodGoodsNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "厂商名称", ShortCode = "ProdOrgName", Desc = "厂商名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName
		{
			get { return _prodOrgName; }
			set
			{
				_prodOrgName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "货品名称", ShortCode = "GoodsName", Desc = "货品名称", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsName
		{
			get { return _goodsName; }
			set
			{
				_goodsName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "包装单位", ShortCode = "GoodsUnit", Desc = "包装单位", ContextType = SysDic.All, Length = 10)]
        public virtual string GoodsUnit
		{
			get { return _goodsUnit; }
			set
			{
				_goodsUnit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "包装单位描述", ShortCode = "UnitMemo", Desc = "包装单位描述", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitMemo
		{
			get { return _unitMemo; }
			set
			{
				_unitMemo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订货数量", ShortCode = "GoodsQty", Desc = "订货数量", ContextType = SysDic.All, Length = 8)]
        public virtual double GoodsQty
		{
			get { return _goodsQty; }
			set { _goodsQty = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "当前库存数量", ShortCode = "CurrentQty", Desc = "当前库存数量", ContextType = SysDic.All, Length = 8)]
        public virtual double CurrentQty
		{
			get { return _currentQty; }
			set { _currentQty = value; }
		}

        [DataMember]
        [DataDesc(CName = "数据上传标志", ShortCode = "IOFlag", Desc = "数据上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
		{
			get { return _iOFlag; }
			set { _iOFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set
			{
				_zX1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set
			{
				_zX2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set
			{
				_zX3 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单价", ShortCode = "Price", Desc = "单价", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
        {
            get { return _price; }
            set { _price = value; }
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
        [DataDesc(CName = "平台订货总单历史表", ShortCode = "BmsCenOrderDocHistory", Desc = "平台订货总单历史表")]
		public virtual BmsCenOrderDocHistory BmsCenOrderDocHistory
		{
			get { return _bmsCenOrderDocHistory; }
			set { _bmsCenOrderDocHistory = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Prod", Desc = "")]
		public virtual CenOrg Prod
		{
			get { return _prod; }
			set { _prod = value; }
		}

        [DataMember]
        [DataDesc(CName = "平台产品表", ShortCode = "Goods", Desc = "平台产品表")]
		public virtual Goods Goods
		{
			get { return _goods; }
			set { _goods = value; }
		}

        
		#endregion
	}
	#endregion
}