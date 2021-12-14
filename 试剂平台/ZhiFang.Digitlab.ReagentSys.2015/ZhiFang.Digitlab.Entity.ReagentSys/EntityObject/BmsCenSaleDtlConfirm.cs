using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region BmsCenSaleDtlConfirm

    /// <summary>
    /// BmsCenSaleDtlConfirm object for NHibernate mapped table 'BmsCenSaleDtlConfirm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "供货验收明细单表", ClassCName = "BmsCenSaleDtlConfirm", ShortCode = "BmsCenSaleDtlConfirm", Desc = "供货验收明细单表")]
    public class BmsCenSaleDtlConfirm : BaseEntity
    {
        #region Member Variables

        protected string _saleDtlConfirmNo;
        protected string _saleDocConfirmNo;
        protected string _OrderDocNo;
        protected string _prodGoodsNo;
        protected string _prodOrgName;
        protected string _goodsName;
        protected string _goodsUnit;
        protected string _unitMemo;
        protected string _storageType;
        protected string _tempRange;
        protected int _goodsQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected string _lotNo;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected string _biddingNo;
        protected string _approveDocNo;
        protected string _goodsSerial;
        protected DateTime? _registerInvalidDate;
        protected string _lotSerial;
        protected string _SysLotSerial;
        protected string _registerNo;
        protected int _acceptCount;
        protected int _refuseCount;
        protected string _acceptMemo;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected BmsCenSaleDocConfirm _bmsCenSaleDocConfirm;
        protected BmsCenSaleDtl _bmsCenSaleDtl;
        protected BmsCenOrderDoc _BmsCenOrderDoc;
        protected BmsCenOrderDtl _BmsCenOrderDtl;
        protected CenOrg _prod;
        protected Goods _goods;
        protected long? _reaGoodsID;
        protected string _reaGoodsName;
        protected long? _orderGoodsID;
        protected string _GoodsNo;
        protected long? _Status;
        protected string _StatusName;
        protected int _barCodeMgr;
        protected int _inCount;
        
        #endregion

        #region Constructors

        public BmsCenSaleDtlConfirm() { }

        public BmsCenSaleDtlConfirm(string saleDtlConfirmNo, string saleDocConfirmNo, string prodGoodsNo, string prodOrgName, string goodsName, string goodsUnit, string unitMemo, string storageType, string tempRange, int goodsQty, double price, double sumTotal, double taxRate, string lotNo, DateTime prodDate, DateTime invalidDate, string biddingNo, string approveDocNo, string goodsSerial, DateTime registerInvalidDate, string lotSerial, string SysLotSerial, string registerNo, int acceptCount, int refuseCount, string acceptMemo, int dispOrder, DateTime dataUpdateTime, DateTime dataAddTime, BmsCenSaleDocConfirm bmsCenSaleDocConfirm, BmsCenSaleDtl bmsCenSaleDtl, CenOrg prod, Goods goods)
        {
            this._saleDtlConfirmNo = saleDtlConfirmNo;
            this._saleDocConfirmNo = saleDocConfirmNo;
            this._prodGoodsNo = prodGoodsNo;
            this._prodOrgName = prodOrgName;
            this._goodsName = goodsName;
            this._goodsUnit = goodsUnit;
            this._unitMemo = unitMemo;
            this._storageType = storageType;
            this._tempRange = tempRange;
            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._lotNo = lotNo;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._biddingNo = biddingNo;
            this._approveDocNo = approveDocNo;
            this._goodsSerial = goodsSerial;
            this._registerInvalidDate = registerInvalidDate;
            this._lotSerial = lotSerial;
            this._SysLotSerial = SysLotSerial;
            this._registerNo = registerNo;
            this._acceptCount = acceptCount;
            this._refuseCount = refuseCount;
            this._acceptMemo = acceptMemo;
            this._dispOrder = dispOrder;
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
            this._bmsCenSaleDocConfirm = bmsCenSaleDocConfirm;
            this._bmsCenSaleDtl = bmsCenSaleDtl;
            this._prod = prod;
            this._goods = goods;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "供货验收明细单号", ShortCode = "SaleDtlConfirmNo", Desc = "供货验收明细单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDtlConfirmNo
        {
            get { return _saleDtlConfirmNo; }
            set { _saleDtlConfirmNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货验收单号", ShortCode = "SaleDocConfirmNo", Desc = "供货验收单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocConfirmNo
        {
            get { return _saleDocConfirmNo; }
            set { _saleDocConfirmNo = value; }
        }       

        [DataMember]
        [DataDesc(CName = "订货总单号", ShortCode = "OrderDocNo", Desc = "订货总单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDocNo
        {
            get { return _OrderDocNo; }
            set { _OrderDocNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂商产品编号", ShortCode = "ProdGoodsNo", Desc = "厂商产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set { _prodGoodsNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂商名称", ShortCode = "ProdOrgName", Desc = "厂商名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName
        {
            get { return _prodOrgName; }
            set { _prodOrgName = value; }
        }

        [DataMember]
        [DataDesc(CName = "货品名称", ShortCode = "GoodsName", Desc = "货品名称", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsName
        {
            get { return _goodsName; }
            set { _goodsName = value; }
        }

        [DataMember]
        [DataDesc(CName = "包装单位", ShortCode = "GoodsUnit", Desc = "包装单位", ContextType = SysDic.All, Length = 10)]
        public virtual string GoodsUnit
        {
            get { return _goodsUnit; }
            set { _goodsUnit = value; }
        }

        [DataMember]
        [DataDesc(CName = "包装单位描述（规格）", ShortCode = "UnitMemo", Desc = "包装单位描述（规格）", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set { _unitMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "储藏条件", ShortCode = "StorageType", Desc = "储藏条件", ContextType = SysDic.All, Length = 200)]
        public virtual string StorageType
        {
            get { return _storageType; }
            set { _storageType = value; }
        }

        [DataMember]
        [DataDesc(CName = "温度范围", ShortCode = "TempRange", Desc = "温度范围", ContextType = SysDic.All, Length = 100)]
        public virtual string TempRange
        {
            get { return _tempRange; }
            set { _tempRange = value; }
        }

        [DataMember]
        [DataDesc(CName = "购进数量", ShortCode = "GoodsQty", Desc = "购进数量", ContextType = SysDic.All, Length = 4)]
        public virtual int GoodsQty
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
            set { _lotNo = value; }
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
            set { _biddingNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "批准文号", ShortCode = "ApproveDocNo", Desc = "批准文号", ContextType = SysDic.All, Length = 200)]
        public virtual string ApproveDocNo
        {
            get { return _approveDocNo; }
            set { _approveDocNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "产品条码", ShortCode = "GoodsSerial", Desc = "产品条码", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsSerial
        {
            get { return _goodsSerial; }
            set { _goodsSerial = value; }
        }

        

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册证有效期", ShortCode = "RegisterInvalidDate", Desc = "注册证有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegisterInvalidDate
        {
            get { return _registerInvalidDate; }
            set { _registerInvalidDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "批号条码", ShortCode = "LotSerial", Desc = "批号条码", ContextType = SysDic.All, Length = 100)]
        public virtual string LotSerial
        {
            get { return _lotSerial; }
            set { _lotSerial = value; }
        }

        [DataMember]
        [DataDesc(CName = " 系统内部批号条码", ShortCode = "SysLotSerial", Desc = " 系统内部批号条码", ContextType = SysDic.All, Length = 100)]
        public virtual string SysLotSerial
        {
            get { return _SysLotSerial; }
            set
            {
                _SysLotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "注册证编号", ShortCode = "RegisterNo", Desc = "注册证编号", ContextType = SysDic.All, Length = 200)]
        public virtual string RegisterNo
        {
            get { return _registerNo; }
            set { _registerNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "验收数量", ShortCode = "AcceptCount", Desc = "验收数量", ContextType = SysDic.All, Length = 4)]
        public virtual int AcceptCount
        {
            get { return _acceptCount; }
            set { _acceptCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "拒收数量", ShortCode = "RefuseCount", Desc = "拒收数量", ContextType = SysDic.All, Length = 4)]
        public virtual int RefuseCount
        {
            get { return _refuseCount; }
            set { _refuseCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "验收备注", ShortCode = "AcceptMemo", Desc = "验收备注", ContextType = SysDic.All, Length = 1000)]
        public virtual string AcceptMemo
        {
            get { return _acceptMemo; }
            set { _acceptMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "供货验收单表", ShortCode = "BmsCenSaleDocConfirm", Desc = "供货验收单表")]
        public virtual BmsCenSaleDocConfirm BmsCenSaleDocConfirm
        {
            get { return _bmsCenSaleDocConfirm; }
            set { _bmsCenSaleDocConfirm = value; }
        }

        [DataMember]
        [DataDesc(CName = "平台供货明细表", ShortCode = "BmsCenSaleDtl", Desc = "平台供货明细表")]
        public virtual BmsCenSaleDtl BmsCenSaleDtl
        {
            get { return _bmsCenSaleDtl; }
            set { _bmsCenSaleDtl = value; }
        }

        [DataMember]
        [DataDesc(CName = "定单表", ShortCode = "BmsCenOrderDoc", Desc = "定单表")]
        public virtual BmsCenOrderDoc BmsCenOrderDoc
        {
            get { return _BmsCenOrderDoc; }
            set { _BmsCenOrderDoc = value; }
        }

        [DataMember]
        [DataDesc(CName = "定单明细表", ShortCode = "BmsCenOrderDtl", Desc = "定单明细表")]
        public virtual BmsCenOrderDtl BmsCenOrderDtl
        {
            get { return _BmsCenOrderDtl; }
            set { _BmsCenOrderDtl = value; }
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

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地货品ID", ShortCode = "ReaGoodsID", Desc = "本地货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaGoodsID
        {
            get { return _reaGoodsID; }
            set { _reaGoodsID = value; }
        }

        [DataMember]
        [DataDesc(CName = "本地货品名称", ShortCode = "ReaGoodsName", Desc = "本地货品名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsName
        {
            get { return _reaGoodsName; }
            set
            {
                _reaGoodsName = value;
            }
        }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品机构关系ID", ShortCode = "OrderGoodsID", Desc = "货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderGoodsID
        {
            get { return _orderGoodsID; }
            set { _orderGoodsID = value; }

        }

        [DataMember]
        [DataDesc(CName = "产品编号", ShortCode = "GoodsNo", Desc = "产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsNo
        {
            get { return _GoodsNo; }
            set { _GoodsNo = value; }

        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单据状态", ShortCode = "Status", Desc = "单据状态", ContextType = SysDic.All, Length = 8)]
        public virtual long? Status
        {
            get { return _Status; }
            set { _Status = value; }

        }

        [DataMember]
        [DataDesc(CName = "单据状态描述", ShortCode = "StatusName", Desc = "单据状态描述", ContextType = SysDic.All, Length = 100)]
        public virtual string StatusName
        {
            get { return _StatusName; }
            set
            {
                _StatusName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "入库数量", ShortCode = "InCount", Desc = "入库数量", ContextType = SysDic.All, Length = 4)]
        public virtual int InCount
        {
            get { return _inCount; }
            set { _inCount = value; }
        }
        [DataMember]
        [DataDesc(CName = "是否盒条码", ShortCode = "BarCodeMgr", Desc = "是否盒条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeMgr
        {
            get { return _barCodeMgr; }
            set { _barCodeMgr = value; }
        }
        #endregion
    }
    #endregion
}