using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsCenSaleDtl

    /// <summary>
    /// ReaBmsCenSaleDtl object for NHibernate mapped table 'Rea_BmsCenSaleDtl'.
    /// 供货明细的条码类型从机构货品关系表里取
    /// int?及double?添加上[JsonConverter(typeof(JsonConvertClass))]时,在上传平台进行序列化及反序列化时值为空
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsCenSaleDtl", ShortCode = "ReaBmsCenSaleDtl", Desc = "")]
    public class ReaBmsCenSaleDtl : BaseEntityService
    {
        #region Member Variables

        protected string _saleDtlNo;
        protected long? _saleDocID;
        protected string _saleDocNo;
        protected long? _prodID;
        protected string _prodOrgName;
        protected string _prodGoodsNo;
        protected long? _goodsID;
        protected string _goodsName;
        protected string _goodsUnit;
        protected string _unitMemo;
        protected string _storageType;
        protected string _tempRange;
        protected double? _goodsQty;
        protected double? _price;
        protected double? _sumTotal;
        protected double? _taxRate;
        protected string _lotNo;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected string _biddingNo;
        protected string _approveDocNo;
        protected int _iOFlag;
        protected string _goodsSerial;
        protected string _lotSerial;
        protected string _sysLotSerial;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _shortCode;
        protected long? _reaGoodsID;
        protected string _reaGoodsName;
        protected string _goodsNo;
        protected double? _iOGoodsQty;
        protected double? _dispOrder;
        protected string _registerNo;
        protected DateTime? _registerInvalidDate;
        protected DateTime? _dataUpdateTime;
        protected int _status;
        protected string _statusName;
        protected long? _reaCompID;
        protected string _reaCompanyName;
        protected string _reaServerCompCode;
        protected long _barCodeType;
        protected int _printCount;
        protected long? _compGoodsLinkID;
        protected long? _labcGoodsLinkID;
        protected long _isPrintBarCode;
        protected string _prodOrgNo;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        protected int _goodsSort;
        protected long? _labOrderDtlID;
        protected string _lotQRCode;
        protected string _otherDtlNo;
        protected long? _goodsLotID;
        protected int _deleteFlag;
        protected string _memo;
        #endregion

        #region Constructors

        public ReaBmsCenSaleDtl() { }

        public ReaBmsCenSaleDtl(long labID, string saleDtlNo, long saleDocID, string saleDocNo, long prodID, string prodOrgName, string prodGoodsNo, long goodsID, string goodsName, string goodsUnit, string unitMemo, string storageType, string tempRange, double goodsQty, double price, double sumTotal, double taxRate, string lotNo, DateTime prodDate, DateTime invalidDate, string biddingNo, string approveDocNo, int iOFlag, string goodsSerial, string lotSerial, string sysLotSerial, string zX1, string zX2, string zX3, string shortCode, long reaGoodsID, string reaGoodsName, string goodsNo, long compGoodsLinkID, double iOGoodsQty, double dispOrder, string registerNo, DateTime registerInvalidDate, DateTime dataUpdateTime, DateTime dataAddTime, int status, string statusName, byte[] dataTimeStamp, int deleteFlag, string memo, long reaCompID, string reaCompanyName, string reaServerCompCode, long barCodeType, int printCount, long labcGoodsLinkID, long isPrintBarCode, string prodOrgNo, string reaGoodsNo, string cenOrgGoodsNo, string lotQRCode, int goodsSort, long labOrderDtlID, string otherDtlNo, long goodsLotID)
        {
            this._labID = labID;
            this._saleDtlNo = saleDtlNo;
            this._saleDocID = saleDocID;
            this._saleDocNo = saleDocNo;
            this._prodID = prodID;
            this._prodOrgName = prodOrgName;
            this._prodGoodsNo = prodGoodsNo;
            this._goodsID = goodsID;
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
            this._iOFlag = iOFlag;
            this._goodsSerial = goodsSerial;
            this._lotSerial = lotSerial;
            this._sysLotSerial = sysLotSerial;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._shortCode = shortCode;
            this._reaGoodsID = reaGoodsID;
            this._reaGoodsName = reaGoodsName;
            this._goodsNo = goodsNo;
            this._compGoodsLinkID = compGoodsLinkID;
            this._iOGoodsQty = iOGoodsQty;
            this._dispOrder = dispOrder;
            this._registerNo = registerNo;
            this._registerInvalidDate = registerInvalidDate;
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
            this._status = status;
            this._statusName = statusName;
            this._dataTimeStamp = dataTimeStamp;
            this._deleteFlag = deleteFlag;
            this._memo = memo;
            this._reaCompID = reaCompID;
            this._reaCompanyName = reaCompanyName;
            this._reaServerCompCode = reaServerCompCode;
            this._barCodeType = barCodeType;
            this._printCount = printCount;
            this._labcGoodsLinkID = labcGoodsLinkID;
            this._isPrintBarCode = isPrintBarCode;
            this._prodOrgNo = prodOrgNo;
            this._reaGoodsNo = reaGoodsNo;
            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._lotQRCode = lotQRCode;
            this._goodsSort = goodsSort;
            this._labOrderDtlID = labOrderDtlID;
            this._otherDtlNo = otherDtlNo;
            this._goodsLotID = goodsLotID;
        }


        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "DeleteFlag", ShortCode = "DeleteFlag", Desc = "DeleteFlag", ContextType = SysDic.All, Length = 4)]
        public virtual int DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "Memo", ShortCode = "Memo", Desc = "Memo", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实验室订货明细ID", ShortCode = "LabOrderDtlID", Desc = "实验室订货明细ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LabOrderDtlID
        {
            get { return _labOrderDtlID; }
            set { _labOrderDtlID = value; }
        }
        [DataMember]
        [DataDesc(CName = "产品序号", ShortCode = "GoodsSort", Desc = "产品序号", ContextType = SysDic.All, Length = 4)]
        public virtual int GoodsSort
        {
            get { return _goodsSort; }
            set { _goodsSort = value; }
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
        [DataDesc(CName = "是否打印条码", ShortCode = "IsPrintBarCode", Desc = "是否打印条码", ContextType = SysDic.All, Length = 8)]
        public virtual long IsPrintBarCode
        {
            get { return _isPrintBarCode; }
            set { _isPrintBarCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "厂商编码", ShortCode = "ProdOrgNo", Desc = "厂商编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgNo
        {
            get { return _prodOrgNo; }
            set
            {
                _prodOrgNo = value;
            }
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

        //[DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        //[DataDesc(CName = "PSaleDtlID", ShortCode = "PSaleDtlID", Desc = "PSaleDtlID", ContextType = SysDic.All, Length = 8)]
        //public virtual long? PSaleDtlID
        //{
        //    get { return _pSaleDtlID; }
        //    set { _pSaleDtlID = value; }
        //}

        /// <summary>
        /// 供货明细的条码类型从机构货品关系表里取
        /// </summary>
        [DataMember]
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 8)]
        public virtual long BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SaleDtlNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDtlNo
        {
            get { return _saleDtlNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SaleDtlNo", value, value.ToString());
                _saleDtlNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SaleDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SaleDocID
        {
            get { return _saleDocID; }
            set { _saleDocID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SaleDocNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocNo
        {
            get { return _saleDocNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SaleDocNo", value, value.ToString());
                _saleDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ProdID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProdID
        {
            get { return _prodID; }
            set { _prodID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProdOrgName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName
        {
            get { return _prodOrgName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ProdOrgName", value, value.ToString());
                _prodOrgName = value;
            }
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
        [DataDesc(CName = "", ShortCode = "GoodsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
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
        [DataDesc(CName = "", ShortCode = "UnitMemo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for UnitMemo", value, value.ToString());
                _unitMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StorageType", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string StorageType
        {
            get { return _storageType; }
            set
            {
                _storageType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TempRange", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string TempRange
        {
            get { return _tempRange; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TempRange", value, value.ToString());
                _tempRange = value;
            }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SumTotal", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumTotal
        {
            get { return _sumTotal; }
            set { _sumTotal = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
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
        [DataDesc(CName = "", ShortCode = "LotSerial", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string LotSerial
        {
            get { return _lotSerial; }
            set
            {
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
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReaGoodsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaGoodsID
        {
            get { return _reaGoodsID; }
            set { _reaGoodsID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaGoodsName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsName
        {
            get { return _reaGoodsName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaGoodsName", value, value.ToString());
                _reaGoodsName = value;
            }
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
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "IOGoodsQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? IOGoodsQty
        {
            get { return _iOGoodsQty; }
            set { _iOGoodsQty = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "RegisterInvalidDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegisterInvalidDate
        {
            get { return _registerInvalidDate; }
            set { _registerInvalidDate = value; }
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
        [DataDesc(CName = "", ShortCode = "Status", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string StatusName
        {
            get { return _statusName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for StatusName", value, value.ToString());
                _statusName = value;
            }
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
        [DataDesc(CName = "批条码打印次数", ShortCode = "PrintCount", Desc = "批条码打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
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

        //自定义字段，无数据库对应的属性
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompCode { get; set; }
        #endregion

        #region 自定义属性及统计
        protected string _otherSerialNoStr;

        [DataMember]
        [DataDesc(CName = "供货主单", ShortCode = "ReaBmsCenSaleDoc", Desc = "供货主单")]
        public virtual ReaBmsCenSaleDoc ReaBmsCenSaleDoc { get; set; }

        [DataMember]
        [DataDesc(CName = "提取第三方数据的供货明细盒条码信息(使用分号隔开)", ShortCode = "OtherSerialNoStr", Desc = "提取第三方数据的供货明细盒条码信息(使用分号隔开)", ContextType = SysDic.All, Length = 8000)]
        public virtual string OtherSerialNoStr
        {
            get { return _otherSerialNoStr; }
            set { _otherSerialNoStr = value; }
        }

        public ReaBmsCenSaleDtl(ReaBmsCenSaleDoc reabmscensaledoc, ReaBmsCenSaleDtl reabmscensaledtl)
        {
            this.ReaBmsCenSaleDoc = reabmscensaledoc;
            this._id = reabmscensaledtl.Id;
            this._labID = reabmscensaledtl.LabID;
            this._saleDtlNo = reabmscensaledtl.SaleDtlNo;
            this._saleDocID = reabmscensaledtl.SaleDocID;
            this._saleDocNo = reabmscensaledtl.SaleDocNo;
            this._prodID = reabmscensaledtl.ProdID;
            this._prodOrgName = reabmscensaledtl.ProdOrgName;
            this._prodGoodsNo = reabmscensaledtl.ProdGoodsNo;
            this._goodsID = reabmscensaledtl.GoodsID;
            this._goodsName = reabmscensaledtl.GoodsName;
            this._goodsUnit = reabmscensaledtl.GoodsUnit;
            this._unitMemo = reabmscensaledtl.UnitMemo;
            this._storageType = reabmscensaledtl.StorageType;
            this._tempRange = reabmscensaledtl.TempRange;
            this._goodsQty = reabmscensaledtl.GoodsQty;
            this._price = reabmscensaledtl.Price;
            this._sumTotal = reabmscensaledtl.SumTotal;
            this._taxRate = reabmscensaledtl.TaxRate;
            this._lotNo = reabmscensaledtl.LotNo;
            this._prodDate = reabmscensaledtl.ProdDate;
            this._invalidDate = reabmscensaledtl.InvalidDate;
            this._biddingNo = reabmscensaledtl.BiddingNo;
            this._approveDocNo = reabmscensaledtl.ApproveDocNo;
            this._iOFlag = reabmscensaledtl.IOFlag;
            this._goodsSerial = reabmscensaledtl.GoodsSerial;
            this._lotSerial = reabmscensaledtl.LotSerial;
            this._sysLotSerial = reabmscensaledtl.SysLotSerial;
            this._zX1 = reabmscensaledtl.ZX1;
            this._zX2 = reabmscensaledtl.ZX2;
            this._zX3 = reabmscensaledtl.ZX3;
            this._shortCode = reabmscensaledtl.ShortCode;
            this._reaGoodsID = reabmscensaledtl.ReaGoodsID;
            this._reaGoodsName = reabmscensaledtl.ReaGoodsName;
            this._goodsNo = reabmscensaledtl.GoodsNo;
            this._compGoodsLinkID = reabmscensaledtl.CompGoodsLinkID;
            this._iOGoodsQty = reabmscensaledtl.IOGoodsQty;
            this._dispOrder = reabmscensaledtl.DispOrder;
            this._registerNo = reabmscensaledtl.RegisterNo;
            this._registerInvalidDate = reabmscensaledtl.RegisterInvalidDate;
            this._dataUpdateTime = reabmscensaledtl.DataUpdateTime;
            this._dataAddTime = reabmscensaledtl.DataAddTime;
            this._status = reabmscensaledtl.Status;
            this._statusName = reabmscensaledtl.StatusName;
            this._dataTimeStamp = reabmscensaledtl.DataTimeStamp;

            this._reaCompID = reabmscensaledtl.ReaCompID;
            this._reaCompanyName = reabmscensaledtl.ReaCompanyName;
            this._reaServerCompCode = reabmscensaledtl.ReaServerCompCode;
            this._barCodeType = reabmscensaledtl.BarCodeType;
            this._printCount = reabmscensaledtl.PrintCount;
            this._labcGoodsLinkID = reabmscensaledtl.LabcGoodsLinkID;
            this._isPrintBarCode = reabmscensaledtl.IsPrintBarCode;
            this._prodOrgNo = reabmscensaledtl.ProdOrgNo;
            this._reaGoodsNo = reabmscensaledtl.ReaGoodsNo;
            this._cenOrgGoodsNo = reabmscensaledtl.CenOrgGoodsNo;
            this._lotQRCode = reabmscensaledtl.LotQRCode;
            this._goodsSort = reabmscensaledtl.GoodsSort;
            this._labOrderDtlID = reabmscensaledtl.LabOrderDtlID;
        }
        #endregion
    }
    #endregion
}