using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsCenSaleDtlConfirm

    /// <summary>
    /// BmsCenSaleDtlConfirm object for NHibernate mapped table 'BmsCenSaleDtlConfirm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "供货验收明细单表", ClassCName = "ReaBmsCenSaleDtlConfirm", ShortCode = "BmsCenSaleDtlConfirm", Desc = "供货验收明细单表")]
    public class ReaBmsCenSaleDtlConfirm : BaseEntity
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
        protected double _goodsQty;
        protected double? _price;
        protected double? _sumTotal;
        protected double? _taxRate;
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
        protected double _acceptCount;
        protected double _refuseCount;
        protected string _acceptMemo;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        //protected ReaBmsCenSaleDocConfirm _reaBmsCenSaleDocConfirm;
        protected long? _reaGoodsID;
        protected string _reaGoodsName;
        protected string _goodsNo;
        protected long? _Status;
        protected string _statusName;
        protected int _barCodeMgr;
        protected double _inCount;
        protected long? _saleDtlID;
        protected long? _goodsID;
        protected long? _prodID;
        protected long? _orderDocID;
        protected long? _orderDtlID;
        protected long? _reaCompID;
        protected string _reaCompanyName;
        protected string _reaServerCompCode;
        protected long _barCodeType;
        protected long? _compGoodsLinkID;
        protected long? _labcGoodsLinkID;
        protected long? _saleDocConfirmID;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        //protected string _reaCompCode;
        protected int _goodsSort;
        protected string _lotQRCode;
        protected string _otherDtlNo;
        protected long? _goodsLotID;

        protected string _factoryOutTemperature;
        protected string _arrivalTemperature;
        protected string _appearanceAcceptance;
        #endregion

        #region Constructors

        public ReaBmsCenSaleDtlConfirm() { }

        public ReaBmsCenSaleDtlConfirm(string saleDtlConfirmNo, string saleDocConfirmNo, string prodGoodsNo, string prodOrgName, string goodsName, string goodsUnit, string unitMemo, string storageType, string tempRange, int goodsQty, double price, double sumTotal, double taxRate, string lotNo, DateTime prodDate, DateTime invalidDate, string biddingNo, string approveDocNo, string goodsSerial, DateTime registerInvalidDate, string lotSerial, string SysLotSerial, string registerNo, int acceptCount, int refuseCount, string acceptMemo, int dispOrder, DateTime dataUpdateTime, DateTime dataAddTime, long _saleDocConfirmID)
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
            this._saleDocConfirmID = _saleDocConfirmID;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品批号ID", ShortCode = "GoodsLotID", Desc = "货品批号ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsLotID
        {
            get { return _goodsLotID; }
            set { _goodsLotID = value; }
        }
        [DataMember]
        [DataDesc(CName = "产品序号", ShortCode = "GoodsSort", Desc = "产品序号", ContextType = SysDic.All, Length = 4)]
        public virtual int GoodsSort
        {
            get { return _goodsSort; }
            set { _goodsSort = value; }
        }
        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "ReaCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        //public virtual string ReaCompCode
        //{
        //    get { return _reaCompCode; }
        //    set
        //    {
        //        _reaCompCode = value;
        //    }
        //}

        [DataMember]
        [DataDesc(CName = "二维批条码", ShortCode = "LotQRCode", Desc = "二维批条码", ContextType = SysDic.All, Length = 150)]
        public virtual string LotQRCode
        {
            get { return _lotQRCode; }
            set { _lotQRCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "机构货品编号", ShortCode = "ReaGoodsNo", Desc = "机构货品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo
        {
            get { return _reaGoodsNo; }
            set { _reaGoodsNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商货品编码", ShortCode = "CenOrgGoodsNo", Desc = "供应商货品编码", ContextType = SysDic.All, Length = 100)]
        public virtual string CenOrgGoodsNo
        {
            get { return _cenOrgGoodsNo; }
            set { _cenOrgGoodsNo = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "验收主单ID", ShortCode = "SaleDocConfirmID", Desc = "验收主单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SaleDocConfirmID
        {
            get { return _saleDocConfirmID; }
            set { _saleDocConfirmID = value; }

        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供货方货品机构关系ID", ShortCode = "CompGoodsLinkID", Desc = "供货方货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID
        {
            get { return _compGoodsLinkID; }
            set { _compGoodsLinkID = value; }

        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订货方货品机构关系ID", ShortCode = "LabcGoodsLinkID", Desc = "订货方货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LabcGoodsLinkID
        {
            get { return _labcGoodsLinkID; }
            set { _labcGoodsLinkID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrderDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderDocID
        {
            get { return _orderDocID; }
            set { _orderDocID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrderDtlID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderDtlID
        {
            get { return _orderDtlID; }
            set { _orderDtlID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品ID", ShortCode = "GoodsID", Desc = "货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ProdID", ShortCode = "ProdID", Desc = "ProdID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProdID
        {
            get { return _prodID; }
            set { _prodID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供货明细单ID", ShortCode = "SaleDtlID", Desc = "供货明细单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SaleDtlID
        {
            get { return _saleDtlID; }
            set { _saleDtlID = value; }
        }
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
        [DataDesc(CName = "包装单位", ShortCode = "GoodsUnit", Desc = "包装单位", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "储藏条件", ShortCode = "StorageType", Desc = "储藏条件", ContextType = SysDic.All, Length = 1000)]
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
        public virtual double GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单价", ShortCode = "Price", Desc = "单价", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总计金额", ShortCode = "SumTotal", Desc = "总计金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumTotal
        {
            get { return _sumTotal; }
            set { _sumTotal = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "税率", ShortCode = "TaxRate", Desc = "税率", ContextType = SysDic.All, Length = 8)]
        public virtual double? TaxRate
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
        [DataDesc(CName = "批号条码", ShortCode = "LotSerial", Desc = "批号条码", ContextType = SysDic.All, Length = 150)]
        public virtual string LotSerial
        {
            get { return _lotSerial; }
            set { _lotSerial = value; }
        }

        [DataMember]
        [DataDesc(CName = " 系统内部批号条码", ShortCode = "SysLotSerial", Desc = " 系统内部批号条码", ContextType = SysDic.All, Length = 150)]
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
        public virtual double AcceptCount
        {
            get { return _acceptCount; }
            set { _acceptCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "拒收数量", ShortCode = "RefuseCount", Desc = "拒收数量", ContextType = SysDic.All, Length = 4)]
        public virtual double RefuseCount
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

        //[DataMember]
        //[DataDesc(CName = "供货验收单表", ShortCode = "ReaBmsCenSaleDocConfirm", Desc = "供货验收单表")]
        //public virtual ReaBmsCenSaleDocConfirm ReaBmsCenSaleDocConfirm
        //{
        //    get { return _reaBmsCenSaleDocConfirm; }
        //    set { _reaBmsCenSaleDocConfirm = value; }
        //}

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
        [DataDesc(CName = "产品编号", ShortCode = "GoodsNo", Desc = "产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsNo
        {
            get { return _goodsNo; }
            set { _goodsNo = value; }

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
            get { return _statusName; }
            set
            {
                _statusName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "入库数量", ShortCode = "InCount", Desc = "入库数量", ContextType = SysDic.All, Length = 4)]
        public virtual double InCount
        {
            get { return _inCount; }
            set { _inCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 8)]
        public virtual long BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
        }

        [DataMember]
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeMgr", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeMgr
        {
            get { return _barCodeMgr; }
            set { _barCodeMgr = value; }
        }

        [DataMember]
        [DataDesc(CName = "本地供应商ID", ShortCode = "ReaCompID", Desc = "本地供应商ID")]
        public virtual long? ReaCompID
        {
            get { return _reaCompID; }
            set { _reaCompID = value; }
        }

        [DataMember]
        [DataDesc(CName = "本地供应商名称", ShortCode = "ReaCompanyName", Desc = "本地供应商名称")]
        public virtual string ReaCompanyName
        {
            get { return _reaCompanyName; }
            set { _reaCompanyName = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商机平台构码", ShortCode = "ReaServerCompCode", Desc = "供应商机平台构码")]
        public virtual string ReaServerCompCode
        {
            get { return _reaServerCompCode; }
            set { _reaServerCompCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "第三方明细单号", ShortCode = "BatchSn", Desc = "第三方明细单号", ContextType = SysDic.All, Length = 100)]
        public virtual string OtherDtlNo
        {
            get { return _otherDtlNo; }
            set
            {
                _otherDtlNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "厂家出库温度", ShortCode = "FactoryOutTemperature", Desc = "厂家出库温度", ContextType = SysDic.All, Length = 100)]
        public virtual string FactoryOutTemperature
        {
            get { return _factoryOutTemperature; }
            set { _factoryOutTemperature = value; }
        }
        [DataMember]
        [DataDesc(CName = "到货温度", ShortCode = "ArrivalTemperature", Desc = "到货温度", ContextType = SysDic.All, Length = 100)]
        public virtual string ArrivalTemperature
        {
            get { return _arrivalTemperature; }
            set { _arrivalTemperature = value; }
        }
        [DataMember]
        [DataDesc(CName = "外观验收", ShortCode = "AppearanceAcceptance", Desc = "外观验收", ContextType = SysDic.All, Length = 100)]
        public virtual string AppearanceAcceptance
        {
            get { return _appearanceAcceptance; }
            set { _appearanceAcceptance = value; }
        }

        #endregion

        #region 自定义属性及统计

        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsSName { get; set; }

        protected string _otherSerialNoStr;
        [DataMember]
        [DataDesc(CName = "提取第三方数据的验收明细的盒条码信息(使用分号隔开)", ShortCode = "OtherSerialNoStr", Desc = "提取第三方数据的验收明细的盒条码信息(使用分号隔开)", ContextType = SysDic.All, Length = 8000)]
        public virtual string OtherSerialNoStr
        {
            get { return _otherSerialNoStr; }
            set { _otherSerialNoStr = value; }
        }
        public ReaBmsCenSaleDtlConfirm(long labID, long reaCompID, string reaCompanyName, string reaGoodsNo, long reaGoodsID, string reaGoodsName, string goodsUnit, string unitMemo, double goodsQty, double price, double sumTotal, double taxRate, double acceptCount, double refuseCount, string lotNo, DateTime prodDate, DateTime invalidDate, string biddingNo, string goodsNo, string reaServerCompCode, DateTime registerInvalidDate, long barCodeType, string prodGoodsNo, string cenOrgGoodsNo, string registerNo, int goodsSort, string acceptMemo, DateTime dataAddTime)
        {
            this._labID = labID;
            this._reaCompID = reaCompID;
            this._reaCompanyName = reaCompanyName;
            this._reaGoodsNo = reaGoodsNo;
            this._reaGoodsID = reaGoodsID;
            this._reaGoodsName = reaGoodsName;

            this._goodsUnit = goodsUnit;
            this._unitMemo = unitMemo;
            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;

            this._acceptCount = acceptCount;
            this._refuseCount = refuseCount;
            this._taxRate = taxRate;
            this._lotNo = lotNo;
            this._prodDate = prodDate;

            this._invalidDate = invalidDate;
            this._biddingNo = biddingNo;
            this._goodsNo = goodsNo;
            this._reaServerCompCode = reaServerCompCode;
            this._registerInvalidDate = registerInvalidDate;

            this._barCodeType = barCodeType;
            this._prodGoodsNo = prodGoodsNo;
            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._registerNo = registerNo;
            this._goodsSort = goodsSort;

            this._acceptMemo = acceptMemo;
            this._dataAddTime = dataAddTime;
        }

        public ReaBmsCenSaleDtlConfirm(ReaBmsCenSaleDtlConfirm reabmscensaledtlconfirm, ReaGoods reagoods)
        {
            this._id = reabmscensaledtlconfirm.Id;
            this._labID = reabmscensaledtlconfirm.LabID;
            this._saleDtlConfirmNo = reabmscensaledtlconfirm.SaleDtlConfirmNo;
            this._saleDocConfirmNo = reabmscensaledtlconfirm.SaleDocConfirmNo;
            this._OrderDocNo = reabmscensaledtlconfirm.OrderDocNo;
            this._prodGoodsNo = reabmscensaledtlconfirm.ProdGoodsNo;
            this._goodsName = reabmscensaledtlconfirm.GoodsName;
            this._goodsUnit = reabmscensaledtlconfirm.GoodsUnit;
            this._unitMemo = reabmscensaledtlconfirm.UnitMemo;
            this._storageType = reabmscensaledtlconfirm.StorageType;
            this._tempRange = reabmscensaledtlconfirm.TempRange;
            this._goodsQty = reabmscensaledtlconfirm.GoodsQty;
            this._price = reabmscensaledtlconfirm.Price;
            this._sumTotal = reabmscensaledtlconfirm.SumTotal;
            this._taxRate = reabmscensaledtlconfirm.TaxRate;
            this._lotNo = reabmscensaledtlconfirm.LotNo;
            this._prodDate = reabmscensaledtlconfirm.ProdDate;
            this._invalidDate = reabmscensaledtlconfirm.InvalidDate;
            this._biddingNo = reabmscensaledtlconfirm.BiddingNo;
            this._approveDocNo = reabmscensaledtlconfirm.ApproveDocNo;
            this._goodsSerial = reabmscensaledtlconfirm.GoodsSerial;
            this._registerInvalidDate = reabmscensaledtlconfirm.RegisterInvalidDate;
            this._lotSerial = reabmscensaledtlconfirm.LotSerial;
            this._SysLotSerial = reabmscensaledtlconfirm.SysLotSerial;
            this._registerNo = reabmscensaledtlconfirm.RegisterNo;
            this._acceptCount = reabmscensaledtlconfirm.AcceptCount;
            this._refuseCount = reabmscensaledtlconfirm.RefuseCount;
            this._acceptCount = reabmscensaledtlconfirm.AcceptCount;
            this._dispOrder = reabmscensaledtlconfirm.DispOrder;
            this._dataUpdateTime = reabmscensaledtlconfirm.DataUpdateTime;
            this._dataAddTime = reabmscensaledtlconfirm.DataAddTime;
            this._reaGoodsID = reabmscensaledtlconfirm.ReaGoodsID;
            this._reaGoodsName = reabmscensaledtlconfirm.ReaGoodsName;
            this._goodsNo = reabmscensaledtlconfirm.GoodsNo;
            this._Status = reabmscensaledtlconfirm.Status;
            this._statusName = reabmscensaledtlconfirm.StatusName;
            this._barCodeMgr = reabmscensaledtlconfirm.BarCodeMgr;
            this._inCount = reabmscensaledtlconfirm.InCount;
            this._saleDtlID = reabmscensaledtlconfirm.SaleDtlID;
            this._goodsID = reabmscensaledtlconfirm.GoodsID;
            this._prodID = reabmscensaledtlconfirm.ProdID;
            this._orderDocID = reabmscensaledtlconfirm.OrderDocID;
            this._orderDtlID = reabmscensaledtlconfirm.OrderDtlID;
            this._reaCompID = reabmscensaledtlconfirm.ReaCompID;
            this._reaCompanyName = reabmscensaledtlconfirm.ReaCompanyName;
            this._reaServerCompCode = reabmscensaledtlconfirm.ReaServerCompCode;
            this._barCodeType = reabmscensaledtlconfirm.BarCodeType;
            this._compGoodsLinkID = reabmscensaledtlconfirm.CompGoodsLinkID;
            this._labcGoodsLinkID = reabmscensaledtlconfirm.LabcGoodsLinkID;
            this._saleDocConfirmID = reabmscensaledtlconfirm.SaleDocConfirmID;
            this._reaGoodsNo = reabmscensaledtlconfirm.ReaGoodsNo;
            this._cenOrgGoodsNo = reabmscensaledtlconfirm.CenOrgGoodsNo;
            this._goodsSort = reabmscensaledtlconfirm.GoodsSort;
            this._lotQRCode = reabmscensaledtlconfirm.LotQRCode;
            this._otherDtlNo = reabmscensaledtlconfirm.OtherDtlNo;
            this._goodsLotID = reabmscensaledtlconfirm.GoodsLotID;
            this._factoryOutTemperature = reabmscensaledtlconfirm.FactoryOutTemperature;
            this._arrivalTemperature = reabmscensaledtlconfirm.ArrivalTemperature;
            this._appearanceAcceptance = reabmscensaledtlconfirm.AppearanceAcceptance;

            this._prodOrgName = reagoods.ProdOrgName;
            this.GoodsSName = reagoods.SName;
        }
        #endregion
    }
    #endregion
}