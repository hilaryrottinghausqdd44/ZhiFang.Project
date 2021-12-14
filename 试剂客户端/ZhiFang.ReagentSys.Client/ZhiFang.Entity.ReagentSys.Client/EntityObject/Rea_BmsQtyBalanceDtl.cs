using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsQtyBalanceDtl

    /// <summary>
    /// ReaBmsQtyBalanceDtl object for NHibernate mapped table 'Rea_BmsQtyBalanceDtl'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsQtyBalanceDtl", ShortCode = "ReaBmsQtyBalanceDtl", Desc = "")]
    public class ReaBmsQtyBalanceDtl : BaseEntity
    {
        #region Member Variables

        protected long? _qtyBalanceDocID;
        protected long _qtyDtlID;
        protected long? _pQtyDtlID;
        protected long? _reaCompanyID;
        protected string _companyName;
        protected long? _goodsID;
        protected long? _orgID;
        protected string _goodsName;
        protected string _lotNo;
        protected long? _storageID;
        protected long? _placeID;
        protected long? _inDtlID;
        protected string _storageName;
        protected string _placeName;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected string _unitMemo;

        protected double? _preGoodsQty;
        protected double? _preSumTotal;
        protected double? _changeGoodsQty;
        protected double? _calcGoodsQty;

        protected double? _goodsQty;
        protected double? _price;
        protected double? _sumTotal;
        protected double? _taxRate;
        protected int _outFlag;
        protected int _sumFlag;
        protected int _iOFlag;
        protected string _goodsSerial;
        protected string _lotSerial;
        protected string _sysLotSerial;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _memo;
        protected int _dispOrder;
        protected bool _visible;
        protected long _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;
        protected string _goodsNo;
        protected long? _compGoodsLinkID;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected DateTime? _invalidWarningDate;
        protected string _reaServerCompCode;
        protected string _registerNo;

        protected long _barCodeType;
        protected string _prodGoodsNo;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        protected string _reaCompCode;
        protected int _goodsSort;
        protected string _LotQRCode;
        protected string _inDocNo;
        protected string _cSQtyDtlNo;
        protected string _cSInDtlNo;
        protected bool? _isNeedPerformanceTest;
        protected long? _verificationStatus;
        protected long? _goodsLotID;
        #endregion

        #region Constructors

        public ReaBmsQtyBalanceDtl() { }

        public ReaBmsQtyBalanceDtl(long labID, long qtyBalanceDocID, long qtyDtlID, long pQtyDtlID, long reaCompanyID, string companyName, long goodsID, long orgID, string goodsName, string lotNo, long storageID, long placeID, long inDtlID, string storageName, string placeName, long goodsUnitID, string goodsUnit, string unitMemo, double goodsQty, double price, double sumTotal, double taxRate, int outFlag, int sumFlag, int iOFlag, string goodsSerial, string lotSerial, string sysLotSerial, string zX1, string zX2, string zX3, string memo, int dispOrder, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string goodsNo, long _compGoodsLinkID, DateTime prodDate, DateTime invalidDate, DateTime invalidWarningDate, string reaServerCompCode, string registerNo)
        {
            this._labID = labID;
            this._qtyBalanceDocID = qtyBalanceDocID;
            this._qtyDtlID = qtyDtlID;
            this._pQtyDtlID = pQtyDtlID;
            this._reaCompanyID = reaCompanyID;
            this._companyName = companyName;
            this._goodsID = goodsID;
            this._orgID = orgID;
            this._goodsName = goodsName;
            this._lotNo = lotNo;
            this._storageID = storageID;
            this._placeID = placeID;
            this._inDtlID = inDtlID;
            this._storageName = storageName;
            this._placeName = placeName;
            this._goodsUnitID = goodsUnitID;
            this._goodsUnit = goodsUnit;
            this._unitMemo = unitMemo;
            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._outFlag = outFlag;
            this._sumFlag = sumFlag;
            this._iOFlag = iOFlag;
            this._goodsSerial = goodsSerial;
            this._lotSerial = lotSerial;
            this._sysLotSerial = sysLotSerial;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._goodsNo = goodsNo;
            this._compGoodsLinkID = _compGoodsLinkID;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._invalidWarningDate = invalidWarningDate;
            this._reaServerCompCode = reaServerCompCode;
            this._registerNo = registerNo;
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
            get { return _LotQRCode; }
            set { _LotQRCode = value; }
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
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 8)]
        public virtual long BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "QtyBalanceDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? QtyBalanceDocID
        {
            get { return _qtyBalanceDocID; }
            set { _qtyBalanceDocID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "QtyDtlID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long QtyDtlID
        {
            get { return _qtyDtlID; }
            set { _qtyDtlID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PQtyDtlID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? PQtyDtlID
        {
            get { return _pQtyDtlID; }
            set { _pQtyDtlID = value; }
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
        [DataDesc(CName = "", ShortCode = "GoodsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrgID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsName
        {
            get { return _goodsName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsName", value, value.ToString());
                _goodsName = value;
            }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InDtlID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? InDtlID
        {
            get { return _inDtlID; }
            set { _inDtlID = value; }
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
        [DataDesc(CName = "", ShortCode = "UnitMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for UnitMemo", value, value.ToString());
                _unitMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreGoodsQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? PreGoodsQty
        {
            get { return _preGoodsQty; }
            set { _preGoodsQty = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreSumTotal", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? PreSumTotal
        {
            get { return _preSumTotal; }
            set { _preSumTotal = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ChangeGoodsQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? ChangeGoodsQty
        {
            get { return _changeGoodsQty; }
            set { _changeGoodsQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CalcGoodsQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? CalcGoodsQty
        {
            get { return _calcGoodsQty; }
            set { _calcGoodsQty = value; }
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
        [DataDesc(CName = "", ShortCode = "OutFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OutFlag
        {
            get { return _outFlag; }
            set { _outFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SumFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SumFlag
        {
            get { return _sumFlag; }
            set { _sumFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IOFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
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
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
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
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CreaterID
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
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsNo
        {
            get { return _goodsNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsNo", value, value.ToString());
                _goodsNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CompGoodsLinkID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID
        {
            get { return _compGoodsLinkID; }
            set { _compGoodsLinkID = value; }
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
        [DataDesc(CName = "", ShortCode = "InvalidWarningDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidWarningDate
        {
            get { return _invalidWarningDate; }
            set { _invalidWarningDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaServerCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerCompCode
        {
            get { return _reaServerCompCode; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaServerCompCode", value, value.ToString());
                _reaServerCompCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RegisterNo", Desc = "", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "产品序号", ShortCode = "GoodsSort", Desc = "产品序号", ContextType = SysDic.All, Length = 4)]
        public virtual int GoodsSort
        {
            get { return _goodsSort; }
            set { _goodsSort = value; }
        }
        [DataMember]
        [DataDesc(CName = "CS升级BS,CS库存ID", ShortCode = "CSQtyDtlNo", Desc = "CS升级BS,CS库存ID", ContextType = SysDic.All, Length = 50)]
        public virtual string CSQtyDtlNo
        {
            get { return _cSQtyDtlNo; }
            set
            {
                _cSQtyDtlNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "CS升级BS,CS入库明细单ID", ShortCode = "CSInDtlNo", Desc = "CS升级BS,CS入库明细单ID", ContextType = SysDic.All, Length = 50)]
        public virtual string CSInDtlNo
        {
            get { return _cSInDtlNo; }
            set
            {
                _cSInDtlNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "入库批次号(入库总单号)", ShortCode = "InDocNo", Desc = "入库批次号(入库总单号)", ContextType = SysDic.All, Length = 20)]
        public virtual string InDocNo
        {
            get { return _inDocNo; }
            set
            {
                _inDocNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "是否需要性能验证", ShortCode = "IsNeedPerformanceTest", Desc = "是否需要性能验证", ContextType = SysDic.All, Length = 1)]
        public virtual bool? IsNeedPerformanceTest
        {
            get { return _isNeedPerformanceTest; }
            set { _isNeedPerformanceTest = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品批号验证状态", ShortCode = "货品批号验证状态", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? VerificationStatus
        {
            get { return _verificationStatus; }
            set { _verificationStatus = value; }
        }

        #endregion
    }
    #endregion
}