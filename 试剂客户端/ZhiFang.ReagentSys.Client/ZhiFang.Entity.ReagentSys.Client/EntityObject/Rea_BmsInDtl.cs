using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsInDtl

    /// <summary>
    /// ReaBmsInDtl object for NHibernate mapped table 'Rea_BmsInDtl'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsInDtl", ShortCode = "ReaBmsInDtl", Desc = "")]
    public class ReaBmsInDtl : BaseEntity
    {
        #region Member Variables

        protected string _inDtlNo;
        protected string _inDocNo;
        protected string _goodsCName;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected double? _goodsQty;
        protected double? _price;
        protected double? _sumTotal;
        protected double? _taxRate;
        protected string _lotNo;
        protected long? _storageID;
        protected long? _placeID;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;
        protected string _storageName;
        protected string _placeName;
        protected long? _reaCompanyID;
        protected string _companyName;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected DateTime? _registerInvalidDate;
        protected string _biddingNo;
        protected string _approveDocNo;
        protected string _goodsSerial;
        protected string _lotSerial;
        protected string _registerNo;
        protected long? _saleDtlConfirmID;
        protected string _sysLotSerial;

        protected string _goodsNo;
        protected long? _compGoodsLinkID;
        protected string _reaServerCompCode;
        protected long _barCodeType;
        protected string _prodGoodsNo;
        protected long _inDocID;
        //protected ReaBmsInDoc _reaBmsInDoc;
        protected ReaGoods _reaGoods;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        protected string _reaCompCode;
        protected int _goodsSort;
        protected string _lotQRCode;
        protected string _unitMemo;
        protected string _otherDtlNo;
        protected long? _goodsLotID;
        protected long? _saleDtlID;

        protected string _factoryOutTemperature;
        protected string _arrivalTemperature;
        protected string _appearanceAcceptance;
        #endregion

        #region Constructors

        public ReaBmsInDtl() { }

        public ReaBmsInDtl(long labID, string inDtlNo, long inDocID, string inDocNo, string goodsCName, long goodsUnitID, string goodsUnit, double goodsQty, double price, double sumTotal, double taxRate, string lotNo, long storageID, long placeID, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string storageName, string placeName, long reaCompanyID, string companyName, DateTime prodDate, DateTime invalidDate, DateTime registerInvalidDate, string biddingNo, string approveDocNo, string goodsSerial, string lotSerial, string registerNo, long saleDtlConfirmID, string sysLotSerial, string goodsNo, long compGoodsLinkID, string reaServerCompCode, long barCodeType, string prodGoodsNo, string reaGoodsNo, string cenOrgGoodsNo, string lotQRCode, string reaCompCode, int goodsSort, string factoryOutTemperature, string arrivalTemperature, string appearanceAcceptance)
        {
            this._labID = labID;
            this._inDtlNo = inDtlNo;
            this._inDocID = inDocID;
            this._inDocNo = inDocNo;
            this._goodsCName = goodsCName;
            this._goodsUnitID = goodsUnitID;
            this._goodsUnit = goodsUnit;
            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._lotNo = lotNo;
            this._storageID = storageID;
            this._placeID = placeID;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._storageName = storageName;
            this._placeName = placeName;
            this._reaCompanyID = reaCompanyID;
            this._companyName = companyName;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._registerInvalidDate = registerInvalidDate;
            this._biddingNo = biddingNo;
            this._approveDocNo = approveDocNo;
            this._goodsSerial = goodsSerial;
            this._lotSerial = lotSerial;
            this._registerNo = registerNo;
            this._saleDtlConfirmID = saleDtlConfirmID;
            this._sysLotSerial = sysLotSerial;
            this._goodsNo = goodsNo;
            this._compGoodsLinkID = compGoodsLinkID;
            this._reaServerCompCode = reaServerCompCode;
            this._barCodeType = barCodeType;
            this._prodGoodsNo = prodGoodsNo;
            this._reaGoodsNo = reaGoodsNo;
            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._lotQRCode = lotQRCode;
            this._reaCompCode = reaCompCode;
            this._goodsSort = goodsSort;
            this._factoryOutTemperature = factoryOutTemperature;
            this._arrivalTemperature = arrivalTemperature;
            this._appearanceAcceptance = appearanceAcceptance;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供货明细ID", ShortCode = "SaleDtlID", Desc = "供货明细ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SaleDtlID
        {
            get { return _saleDtlID; }
            set { _saleDtlID = value; }
        }

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
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompCode
        {
            get { return _reaCompCode; }
            set
            {
                _reaCompCode = value;
            }
        }

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
        [DataDesc(CName = "", ShortCode = "InDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long InDocID
        {
            get { return _inDocID; }
            set { _inDocID = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProdGoodsNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set
            {
                _prodGoodsNo = value;
            }
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
        [DataDesc(CName = "", ShortCode = "InDtlNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string InDtlNo
        {
            get { return _inDtlNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for InDtlNo", value, value.ToString());
                _inDtlNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InDocNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string InDocNo
        {
            get { return _inDocNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for InDocNo", value, value.ToString());
                _inDocNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsCName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsCName
        {
            get { return _goodsCName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsCName", value, value.ToString());
                _goodsCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsUnitID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsUnitID
        {
            get { return _goodsUnitID; }
            set { _goodsUnitID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsUnit", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsUnit
        {
            get { return _goodsUnit; }
            set
            {
                _goodsUnit = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SumTotal", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumTotal
        {
            get { return _sumTotal; }
            set { _sumTotal = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TaxRate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LotNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LotNo", value, value.ToString());
                _lotNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StorageID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PlaceID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? PlaceID
        {
            get { return _placeID; }
            set { _placeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = int.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CreaterName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
        {
            get { return _createrName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
                _createrName = value;
            }
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
        [DataDesc(CName = "", ShortCode = "StorageName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string StorageName
        {
            get { return _storageName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for StorageName", value, value.ToString());
                _storageName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PlaceName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string PlaceName
        {
            get { return _placeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for PlaceName", value, value.ToString());
                _placeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReaCompanyID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompanyID
        {
            get { return _reaCompanyID; }
            set { _reaCompanyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompanyName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CompanyName", value, value.ToString());
                _companyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ProdDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ProdDate
        {
            get { return _prodDate; }
            set { _prodDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InvalidDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidDate
        {
            get { return _invalidDate; }
            set { _invalidDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "RegisterInvalidDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegisterInvalidDate
        {
            get { return _registerInvalidDate; }
            set { _registerInvalidDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BiddingNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string BiddingNo
        {
            get { return _biddingNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for BiddingNo", value, value.ToString());
                _biddingNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ApproveDocNo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ApproveDocNo
        {
            get { return _approveDocNo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ApproveDocNo", value, value.ToString());
                _approveDocNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsSerial", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsSerial
        {
            get { return _goodsSerial; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsSerial", value, value.ToString());
                _goodsSerial = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "LotSerial", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string LotSerial
        {
            get { return _lotSerial; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for LotSerial", value, value.ToString());
                _lotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "注册号，等同于Rea_Goods表里的RegistNo", ShortCode = "RegisterNo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string RegisterNo
        {
            get { return _registerNo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for RegisterNo", value, value.ToString());
                _registerNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SaleDtlConfirmID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SaleDtlConfirmID
        {
            get { return _saleDtlConfirmID; }
            set { _saleDtlConfirmID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SysLotSerial", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string SysLotSerial
        {
            get { return _sysLotSerial; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for SysLotSerial", value, value.ToString());
                _sysLotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "平台货品平台编码", ShortCode = "GoodsNo", Desc = "平台货品平台编码", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsNo
        {
            get { return _goodsNo; }
            set
            {
                _goodsNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供应商货品关系ID", ShortCode = "CompGoodsLinkID", Desc = "供应商货品关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID
        {
            get { return _compGoodsLinkID; }
            set { _compGoodsLinkID = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商机平台构码", ShortCode = "ReaServerCompCode", Desc = "供应商机平台构码", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaServerCompCode
        {
            get { return _reaServerCompCode; }
            set
            {
                _reaServerCompCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "货品表", ShortCode = "ReaGoods", Desc = "货品表")]
        public virtual ReaGoods ReaGoods
        {
            get { return _reaGoods; }
            set { _reaGoods = value; }
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
        /// 挂网流水号
        /// </summary>
        public virtual string NetGoodsNo { get; set; }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public virtual string ProdOrgName { get; set; }
        /// <summary>
        /// 货品简称
        /// </summary>
        public virtual string GoodSName { get; set; }

        /// <summary>
        /// 第三方的库房编码，入库接口直接写入到入库明细表时使用
        /// </summary>
        public virtual string StorageNo { get; set; }

        protected string _otherSerialNoStr;
        [DataMember]
        [DataDesc(CName = "提取第三方数据的入库明细盒条码信息(使用分号隔开)", ShortCode = "OtherSerialNoStr", Desc = "提取第三方数据的入库明细盒条码信息(使用分号隔开)", ContextType = SysDic.All, Length = 8000)]
        public virtual string OtherSerialNoStr
        {
            get { return _otherSerialNoStr; }
            set { _otherSerialNoStr = value; }
        }
        protected long? _goodsID;
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "GoodsID", ShortCode = "GoodsID", Desc = "GoodsID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "UnitMemo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                _unitMemo = value;
            }
        }
        protected string _purpose;
        [DataMember]
        [DataDesc(CName = "用途", ShortCode = "Purpose", Desc = "用途", ContextType = SysDic.All, Length = 500)]
        public virtual string Purpose
        {
            get { return _purpose; }
            set
            {
                _purpose = value;
            }
        }
        protected string _storageType;
        [DataMember]
        [DataDesc(CName = "储藏条件", ShortCode = "StorageType", Desc = "储藏条件", ContextType = SysDic.All, Length = 500)]
        public virtual string StorageType
        {
            get { return _storageType; }
            set
            {
                _storageType = value;
            }
        }
        protected string _transportNo;
        [DataMember]
        [DataDesc(CName = "货运单号(通过关联取供货验收单里的货运单号)", ShortCode = "TransportNo", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string TransportNo
        {
            get { return _transportNo; }
            set
            {
                _transportNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "订货单号", ShortCode = "OrderDocNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDocNo { get; set; }
        /// <summary>
        /// 发票号，统计-入库明细汇总，使用
        /// </summary>
        public virtual string InvoiceNo { get; set; }

        public ReaBmsInDtl(long labID, long reaCompanyID, string companyName, string reaGoodsNo, string goodsCName, string goodsUnit, double goodsQty, double price, double sumTotal, double taxRate, string lotNo, long storageID, long placeID, string storageName, string placeName, DateTime prodDate, DateTime invalidDate, DateTime registerInvalidDate, string biddingNo, string goodsNo, string reaServerCompCode, long barCodeType, string prodGoodsNo, string cenOrgGoodsNo, string reaCompCode, int goodsSort, long createrID, string createrName, DateTime dataAddTime, string memo, long goodsID, string unitMemo, string factoryOutTemperature, string arrivalTemperature, string appearanceAcceptance)
        {
            this._labID = labID;
            this._reaCompanyID = reaCompanyID;
            this._companyName = companyName;
            this._reaGoodsNo = reaGoodsNo;
            this._goodsCName = goodsCName;
            this._goodsUnit = goodsUnit;

            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._lotNo = lotNo;

            this._storageID = storageID;
            this._placeID = placeID;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;

            this._storageName = storageName;
            this._placeName = placeName;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._registerInvalidDate = registerInvalidDate;

            this._biddingNo = biddingNo;
            this._goodsNo = goodsNo;
            this._reaServerCompCode = reaServerCompCode;
            this._barCodeType = barCodeType;
            this._prodGoodsNo = prodGoodsNo;

            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._reaCompCode = reaCompCode;
            this._goodsSort = goodsSort;
            this._memo = memo;
            this._goodsID = goodsID;

            this._unitMemo = unitMemo;
            this._factoryOutTemperature = factoryOutTemperature;
            this._arrivalTemperature = arrivalTemperature;
            this._appearanceAcceptance = appearanceAcceptance;
        }
        public ReaBmsInDtl(ReaBmsInDtl reabmsindtl, ReaGoods reagoods)
        {
            this._id = reabmsindtl.Id;
            this._labID = reabmsindtl.LabID;
            this._reaCompanyID = reabmsindtl.ReaCompanyID;
            this._companyName = reabmsindtl.CompanyName;
            this._reaGoodsNo = reabmsindtl.ReaGoodsNo;
            this._goodsCName = reabmsindtl.GoodsCName;
            this._goodsUnit = reabmsindtl.GoodsUnit;

            this._goodsQty = reabmsindtl.GoodsQty;
            this._price = reabmsindtl.Price;
            this._sumTotal = reabmsindtl.SumTotal;
            this._taxRate = reabmsindtl.TaxRate;
            this._lotNo = reabmsindtl.LotNo;

            this._storageID = reabmsindtl.StorageID;
            this._placeID = reabmsindtl.PlaceID;
            this._createrID = reabmsindtl.CreaterID;
            this._createrName = reabmsindtl.CreaterName;
            this._dataAddTime = reabmsindtl.DataAddTime;

            this._storageName = reabmsindtl.StorageName;
            this._placeName = reabmsindtl.PlaceName;
            this._prodDate = reabmsindtl.ProdDate;
            this._invalidDate = reabmsindtl.InvalidDate;
            this._registerInvalidDate = reabmsindtl.RegisterInvalidDate;

            this._biddingNo = reabmsindtl.BiddingNo;
            this._goodsNo = reabmsindtl.GoodsNo;
            this._reaServerCompCode = reabmsindtl.ReaServerCompCode;
            this._barCodeType = reabmsindtl.BarCodeType;
            this._prodGoodsNo = reabmsindtl.ProdGoodsNo;

            this._cenOrgGoodsNo = reabmsindtl.CenOrgGoodsNo;
            this._reaCompCode = reabmsindtl.ReaCompCode;
            this._goodsSort = reabmsindtl.GoodsSort;
            this._memo = reabmsindtl.Memo;
            this._factoryOutTemperature = reabmsindtl.FactoryOutTemperature;
            this._arrivalTemperature = reabmsindtl.ArrivalTemperature;
            this._appearanceAcceptance = reabmsindtl.AppearanceAcceptance;

            this._goodsID = reagoods.Id;
            this._unitMemo = reagoods.UnitMemo;
            this._storageType = reagoods.StorageType;
            this._purpose = reagoods.Purpose;
            this.ProdOrgName = reagoods.ProdOrgName;
            this.GoodSName = reagoods.SName;
        }
        public ReaBmsInDtl(ReaBmsInDtl reabmsindtl, ReaGoods reagoods, string transportno, string orderdocno, string invoiceNo)
        {
            this._id = reabmsindtl.Id;
            this._labID = reabmsindtl.LabID;
            this._inDocNo = reabmsindtl.InDocNo;
            this._reaCompanyID = reabmsindtl.ReaCompanyID;
            this._companyName = reabmsindtl.CompanyName;
            this._reaGoodsNo = reabmsindtl.ReaGoodsNo;
            this._goodsCName = reabmsindtl.GoodsCName;
            this._goodsUnit = reabmsindtl.GoodsUnit;

            this._goodsQty = reabmsindtl.GoodsQty;
            this._price = reabmsindtl.Price;
            this._sumTotal = reabmsindtl.SumTotal;
            this._taxRate = reabmsindtl.TaxRate;
            this._lotNo = reabmsindtl.LotNo;

            this._storageID = reabmsindtl.StorageID;
            this._placeID = reabmsindtl.PlaceID;
            this._createrID = reabmsindtl.CreaterID;
            this._createrName = reabmsindtl.CreaterName;
            this._dataAddTime = reabmsindtl.DataAddTime;

            this._storageName = reabmsindtl.StorageName;
            this._placeName = reabmsindtl.PlaceName;
            this._prodDate = reabmsindtl.ProdDate;
            this._invalidDate = reabmsindtl.InvalidDate;
            this._registerInvalidDate = reabmsindtl.RegisterInvalidDate;

            this._biddingNo = reabmsindtl.BiddingNo;
            this._goodsNo = reabmsindtl.GoodsNo;
            this._reaServerCompCode = reabmsindtl.ReaServerCompCode;
            this._barCodeType = reabmsindtl.BarCodeType;
            this._prodGoodsNo = reabmsindtl.ProdGoodsNo;

            this._cenOrgGoodsNo = reabmsindtl.CenOrgGoodsNo;
            this._reaCompCode = reabmsindtl.ReaCompCode;
            this._goodsSort = reabmsindtl.GoodsSort;
            this._memo = reabmsindtl.Memo;
            this._factoryOutTemperature = reabmsindtl.FactoryOutTemperature;
            this._arrivalTemperature = reabmsindtl.ArrivalTemperature;
            this._appearanceAcceptance = reabmsindtl.AppearanceAcceptance;

            this._goodsID = reagoods.Id;
            this._unitMemo = reagoods.UnitMemo;
            this._storageType = reagoods.StorageType;
            this._purpose = reagoods.Purpose;
            this._registerNo = reagoods.RegistNo;//注册号
            this.NetGoodsNo = reagoods.NetGoodsNo;
            this.ProdOrgName = reagoods.ProdOrgName;
            this.GoodSName = reagoods.SName;

            this._transportNo = transportno;
            this.InvoiceNo = invoiceNo;
            this.OrderDocNo = orderdocno;
        }
        #endregion
    }
    #endregion
}