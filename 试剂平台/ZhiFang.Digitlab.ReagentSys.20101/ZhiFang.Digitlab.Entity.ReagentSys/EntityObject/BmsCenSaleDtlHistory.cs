using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region BmsCenSaleDtlHistory

	/// <summary>
	/// BmsCenSaleDtlHistory object for NHibernate mapped table 'BmsCenSaleDtlHistory'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "平台供货明细历史表", ClassCName = "BmsCenSaleDtlHistory", ShortCode = "BmsCenSaleDtlHistory", Desc = "平台供货明细历史表")]
	public class BmsCenSaleDtlHistory : BaseEntity
	{
		#region Member Variables
		
        protected string _saleDtlNo;
        protected string _saleDocNo;
        protected string _prodGoodsNo;
        protected string _prodOrgName;
        protected string _goodsName;
        protected string _goodsUnit;
        protected string _unitMemo;
        protected double _goodsQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected string _lotNo;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected string _biddingNo;
        protected int _iOFlag;
        protected string _goodsSerial;
        protected string _packSerial;
        protected string _lotSerial;
        protected string _mixSerial;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
		protected BmsCenSaleDocHistory _bmsCenSaleDocHistory;
		protected CenOrg _prod;
		protected Goods _goods;

		#endregion

		#region Constructors

		public BmsCenSaleDtlHistory() { }

		public BmsCenSaleDtlHistory( string saleDtlNo, string saleDocNo, string prodGoodsNo, string prodOrgName, string goodsName, string goodsUnit, string unitMemo, double goodsQty, double price, double sumTotal, double taxRate, string lotNo, DateTime prodDate, DateTime invalidDate, string biddingNo, int iOFlag, string goodsSerial, string packSerial, string lotSerial, string mixSerial, string zX1, string zX2, string zX3, BmsCenSaleDocHistory bmsCenSaleDocHistory, CenOrg prod, Goods goods )
		{
			this._saleDtlNo = saleDtlNo;
			this._saleDocNo = saleDocNo;
			this._prodGoodsNo = prodGoodsNo;
			this._prodOrgName = prodOrgName;
			this._goodsName = goodsName;
			this._goodsUnit = goodsUnit;
			this._unitMemo = unitMemo;
			this._goodsQty = goodsQty;
			this._price = price;
			this._sumTotal = sumTotal;
			this._taxRate = taxRate;
			this._lotNo = lotNo;
			this._prodDate = prodDate;
			this._invalidDate = invalidDate;
			this._biddingNo = biddingNo;
			this._iOFlag = iOFlag;
			this._goodsSerial = goodsSerial;
			this._packSerial = packSerial;
			this._lotSerial = lotSerial;
			this._mixSerial = mixSerial;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._bmsCenSaleDocHistory = bmsCenSaleDocHistory;
			this._prod = prod;
			this._goods = goods;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "供货单号", ShortCode = "SaleDtlNo", Desc = "供货单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDtlNo
		{
			get { return _saleDtlNo; }
			set
			{
				_saleDtlNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "供货单号", ShortCode = "SaleDocNo", Desc = "供货单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocNo
		{
			get { return _saleDocNo; }
			set
			{
				_saleDocNo = value;
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
        [DataDesc(CName = "购进数量", ShortCode = "GoodsQty", Desc = "购进数量", ContextType = SysDic.All, Length = 8)]
        public virtual double GoodsQty
		{
			get { return _goodsQty; }
			set { _goodsQty = value; }
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
        [DataDesc(CName = "总计金额", ShortCode = "SumTotal", Desc = "总计金额", ContextType = SysDic.All, Length = 8)]
        public virtual double SumTotal
		{
			get { return _sumTotal; }
			set { _sumTotal = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "税率", ShortCode = "TaxRate", Desc = "税率", ContextType = SysDic.All, Length = 8)]
        public virtual double TaxRate
		{
			get { return _taxRate; }
			set { _taxRate = value; }
		}

        [DataMember]
        [DataDesc(CName = "货品批号", ShortCode = "LotNo", Desc = "货品批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo
		{
			get { return _lotNo; }
			set
			{
				_lotNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生产日期", ShortCode = "ProdDate", Desc = "生产日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ProdDate
		{
			get { return _prodDate; }
			set { _prodDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期", ShortCode = "InvalidDate", Desc = "有效期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InvalidDate
		{
			get { return _invalidDate; }
			set { _invalidDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "招标号", ShortCode = "BiddingNo", Desc = "招标号", ContextType = SysDic.All, Length = 100)]
        public virtual string BiddingNo
		{
			get { return _biddingNo; }
			set
			{
				_biddingNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "数据上传标志", ShortCode = "IOFlag", Desc = "数据上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
		{
			get { return _iOFlag; }
			set { _iOFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "产品条码", ShortCode = "GoodsSerial", Desc = "产品条码", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsSerial
		{
			get { return _goodsSerial; }
			set
			{
				_goodsSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "包装单位条码", ShortCode = "PackSerial", Desc = "包装单位条码", ContextType = SysDic.All, Length = 100)]
        public virtual string PackSerial
		{
			get { return _packSerial; }
			set
			{
				_packSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "批号条码", ShortCode = "LotSerial", Desc = "批号条码", ContextType = SysDic.All, Length = 100)]
        public virtual string LotSerial
		{
			get { return _lotSerial; }
			set
			{
				_lotSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = " 混合条码", ShortCode = "MixSerial", Desc = " 混合条码", ContextType = SysDic.All, Length = 100)]
        public virtual string MixSerial
		{
			get { return _mixSerial; }
			set
			{
				_mixSerial = value;
			}
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
        [DataDesc(CName = "专项", ShortCode = "ZX3", Desc = "专项", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set
			{
				_zX3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "平台供货总单历史表", ShortCode = "BmsCenSaleDocHistory", Desc = "平台供货总单历史表")]
		public virtual BmsCenSaleDocHistory BmsCenSaleDocHistory
		{
			get { return _bmsCenSaleDocHistory; }
			set { _bmsCenSaleDocHistory = value; }
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