using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaOrderGoods

    /// <summary>
    ///1.同一供应商的同一货品,启用的只能是一个;
    ///2.同一供应商的不同货品,其供应商货品编码不能相同;
    /// </summary>
    [DataContract]
    [DataDesc(CName = "货品机构关系", ClassCName = "ReaOrderGoods", ShortCode = "ReaOrderGoods", Desc = "货品机构关系")]
	public class ReaGoodsOrgLink : BaseEntity
	{
		#region Member Variables
		
        protected double _price;
        protected string _biddingNo;
        protected string _memo;
        protected int _dispOrder;
        protected int _visible;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected DateTime? _BeginTime;
        protected DateTime? _EndTime;
        protected DateTime? _dataUpdateTime;
		protected ReaCenOrg _cen;
		protected ReaCenOrg _reaCenOrg;
		protected ReaGoods _reaGoods;
        protected int _barCodeType;
        protected int _isPrintBarCode;
        protected string _cenOrgGoodsNo;
        protected string _prodGoodsNo;
        protected long? _labcGoodsLinkID;
        protected long? _compGoodsLinkID;
        #endregion

        #region Constructors

        public ReaGoodsOrgLink() { }

		public ReaGoodsOrgLink( long labID, string cenOrgGoodsNo, double price, string biddingNo, string memo, int dispOrder, int visible, string zX1, string zX2, string zX3, DateTime dataUpdateTime, DateTime dataAddTime, ReaCenOrg cen, ReaCenOrg reaCenOrg, ReaGoods reaGoods )
		{
			this._labID = labID;
			this._cenOrgGoodsNo = cenOrgGoodsNo;
			this._price = price;
			this._biddingNo = biddingNo;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._dataUpdateTime = dataUpdateTime;
			this._dataAddTime = dataAddTime;
			this._cen = cen;
			this._reaCenOrg = reaCenOrg;
			this._reaGoods = reaGoods;
		}

        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "平台供应商货品机构关系ID", ShortCode = "LabOrderDocID", Desc = "平台供应商货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID
        {
            get { return _compGoodsLinkID; }
            set { _compGoodsLinkID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实验室货品机构关系ID", ShortCode = "LabcGoodsLinkID", Desc = "实验室货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LabcGoodsLinkID
        {
            get { return _labcGoodsLinkID; }
            set { _labcGoodsLinkID = value; }
        }
        [DataMember]
        [DataDesc(CName = "供货或订货机构货品编码", ShortCode = "CenOrgGoodsNo", Desc = "供货或订货机构货品编码", ContextType = SysDic.All, Length = 4)]
        public virtual string CenOrgGoodsNo
        {
            get { return _cenOrgGoodsNo; }
            set { _cenOrgGoodsNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "厂商货品编号", ShortCode = "ProdGoodsNo", Desc = "厂商货品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set
            {
                _prodGoodsNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "条码类型", ShortCode = "BarCodeType", Desc = "条码类型", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
        }
        [DataMember]
        [DataDesc(CName = "是否打印条码", ShortCode = "IsPrintBarCode", Desc = "是否打印条码", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPrintBarCode
        {
            get { return _isPrintBarCode; }
            set { _isPrintBarCode = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同价格", ShortCode = "Price", Desc = "合同价格", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [DataDesc(CName = "招标号", ShortCode = "BiddingNo", Desc = "招标号", ContextType = SysDic.All, Length = 100)]
        public virtual string BiddingNo
		{
			get { return _biddingNo; }
			set { _biddingNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Memo
		{
			get { return _memo; }
			set { _memo = value; }
		}

        [DataMember]
        [DataDesc(CName = "优先级", ShortCode = "DispOrder", Desc = "优先级", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set { _zX1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set { _zX2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set { _zX3 = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同开始时间", ShortCode = "BeginTime", Desc = "合同开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同结束时间", ShortCode = "EndTime", Desc = "合同结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTime
        {
			get { return _EndTime; }
			set { _EndTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "供货或订货机构", ShortCode = "Cen", Desc = "供货或订货机构")]
		public virtual ReaCenOrg CenOrg
		{
			get { return _cen; }
			set { _cen = value; }
		}

        [DataMember]
        [DataDesc(CName = "当前机构", ShortCode = "ReaCenOrg", Desc = "当前机构")]
		public virtual ReaCenOrg ReaCenOrg
		{
			get { return _reaCenOrg; }
			set { _reaCenOrg = value; }
		}

        [DataMember]
        [DataDesc(CName = "货品表", ShortCode = "ReaGoods", Desc = "货品表")]
		public virtual ReaGoods ReaGoods
		{
			get { return _reaGoods; }
			set { _reaGoods = value; }
		}

        
		#endregion
	}
	#endregion
}