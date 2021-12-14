using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaGoodsBarcodeOperation

    /// <summary>
    /// ReaGoodsBarcodeOperation object for NHibernate mapped table 'Rea_GoodsBarcodeOperation'.
    /// int?及double?添加上[JsonConverter(typeof(JsonConvertClass))]时,在上传平台进行序列化及反序列化时值为空
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaGoodsBarcodeOperation", ShortCode = "ReaGoodsBarcodeOperation", Desc = "")]
    public class ReaGoodsBarcodeOperation : BaseEntityService
    {
        #region Member Variables

        protected string _bDocNo;
        protected long? _bDocID;
        protected long? _bDtlID;
        protected long? _qtyDtlID;
        protected long? _operTypeID;
        protected string _operTypeName;
        protected string _sysPackSerial;
        protected string _otherPackSerial;
        protected string _usePackSerial;
        protected string _lotNo;
        protected string _memo;
        protected int _dispOrder;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;
        protected long? _reaCompanyID;
        protected string _companyName;
        protected long? _goodsID;
        protected string _goodsCName;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected bool _isOutFlag;
        protected int _printCount;
        protected double? _goodsQty;
        protected string _unitMemo;
        protected string _prodGoodsNo;
        protected string _goodsNo;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        protected string _reaCompCode;
        protected int _goodsSort;
        protected int _barCodeType;
        protected long? _compGoodsLinkID;
        protected double? _scanCodeQty;
        protected double? _minBarCodeQty;
        protected long? _storageID;
        protected long? _placeID;
        protected string _scanCodeGoodsUnit;
        protected double? _overageQty;
        protected long? _scanCodeGoodsID;
        protected long? _goodsLotID;
        protected string _usePackQRCode;
        protected long? _barcodeCreatType;
        protected string _pUsePackSerial;
        #endregion

        #region Constructors

        public ReaGoodsBarcodeOperation() { }

        public ReaGoodsBarcodeOperation(long labID, string bDocNo, long bDocID, long bDtlID, long operTypeID, string operTypeName, string sysPackSerial, string otherPackSerial, string usePackSerial, string lotNo, string memo, int dispOrder, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long reaCompanyID, string companyName, long goodsID, string goodsCName, long goodsUnitID, string goodsUnit, bool isOutFlag, string pUsePackSerial, long barcodeCreatType, int printCount, string unitMemo, double goodsQty, string prodGoodsNo, string goodsNo, string reaGoodsNo, string cenOrgGoodsNo, string usePackQRCode, string reaCompCode, int goodsSort, long compGoodsLinkID, int barCodeType, long qtyDtlID, long storageID, long placeID, double scanCodeQty, double minBarCodeQty, string scanCodeGoodsUnit, double overageQty, long scanCodeGoodsID, long goodsLotID)
        {
            this._labID = labID;
            this._bDocNo = bDocNo;
            this._bDocID = bDocID;
            this._bDtlID = bDtlID;
            this._operTypeID = operTypeID;
            this._operTypeName = operTypeName;
            this._sysPackSerial = sysPackSerial;
            this._otherPackSerial = otherPackSerial;
            this._usePackSerial = usePackSerial;
            this._lotNo = lotNo;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._reaCompanyID = reaCompanyID;
            this._companyName = companyName;
            this._goodsID = goodsID;
            this._goodsCName = goodsCName;
            this._goodsUnitID = goodsUnitID;
            this._goodsUnit = goodsUnit;
            this._isOutFlag = isOutFlag;
            this._pUsePackSerial = pUsePackSerial;
            this._barcodeCreatType = barcodeCreatType;
            this._printCount = printCount;
            this._unitMemo = unitMemo;
            this._goodsQty = goodsQty;
            this._prodGoodsNo = prodGoodsNo;
            this._goodsNo = goodsNo;
            this._reaGoodsNo = reaGoodsNo;
            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._usePackQRCode = usePackQRCode;
            this._reaCompCode = reaCompCode;
            this._goodsSort = goodsSort;
            this._compGoodsLinkID = compGoodsLinkID;
            this._barCodeType = barCodeType;
            this._qtyDtlID = qtyDtlID;
            this._storageID = storageID;
            this._placeID = placeID;
            this._scanCodeQty = scanCodeQty;
            this._minBarCodeQty = minBarCodeQty;
            this._scanCodeGoodsUnit = scanCodeGoodsUnit;
            this._overageQty = overageQty;
            this._scanCodeGoodsID = scanCodeGoodsID;
            this._goodsLotID = goodsLotID;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供应商货品机构关系ID", ShortCode = "CompGoodsLinkID", Desc = "供应商货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID
        {
            get { return _compGoodsLinkID; }
            set { _compGoodsLinkID = value; }
        }
        [DataMember]
        [DataDesc(CName = "条码类型", ShortCode = "BarCodeType", Desc = "条码类型", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
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
        [DataDesc(CName = "二维盒条码", ShortCode = "UsePackQRCode", Desc = "二维盒条码", ContextType = SysDic.All)]
        public virtual string UsePackQRCode
        {
            get { return _usePackQRCode; }
            set { _usePackQRCode = value; }
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
        [DataDesc(CName = "货品平台编码", ShortCode = "GoodsNo", Desc = "货品平台编码", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsNo
        {
            get { return _goodsNo; }
            set
            {
                _goodsNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "厂商货品编码", ShortCode = "ProdGoodsNo", Desc = "厂商货品编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set
            {
                _prodGoodsNo = value;
            }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总数", ShortCode = "GoodsQty", Desc = "总数", ContextType = SysDic.All, Length = 8)]
        public virtual double? GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "UnitMemo", ShortCode = "UnitMemo", Desc = "UnitMemo", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "条码标志", ShortCode = "BarcodeCreatType", Desc = "条码标志", ContextType = SysDic.All, Length = 8)]
        public virtual long? BarcodeCreatType
        {
            get { return _barcodeCreatType; }
            set
            {
                _barcodeCreatType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "父条码", ShortCode = "PUsePackSerial", Desc = "父条码", ContextType = SysDic.All, Length = 100)]
        public virtual string PUsePackSerial
        {
            get { return _pUsePackSerial; }
            set
            {
                _pUsePackSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "BDocNo", ShortCode = "BDocNo", Desc = "BDocNo", ContextType = SysDic.All, Length = 100)]
        public virtual string BDocNo
        {
            get { return _bDocNo; }
            set
            {
                _bDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "BDocID", ShortCode = "BDocID", Desc = "BDocID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BDocID
        {
            get { return _bDocID; }
            set { _bDocID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "BDtlID", ShortCode = "BDtlID", Desc = "BDtlID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BDtlID
        {
            get { return _bDtlID; }
            set { _bDtlID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "QtyDtlID", ShortCode = "QtyDtlID", Desc = "QtyDtlID", ContextType = SysDic.All, Length = 8)]
        public virtual long? QtyDtlID
        {
            get { return _qtyDtlID; }
            set { _qtyDtlID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperTypeID
        {
            get { return _operTypeID; }
            set { _operTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OperTypeName
        {
            get { return _operTypeName; }
            set
            {
                _operTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "一维盒条码", ShortCode = "SysPackSerial", Desc = "一维盒条码", ContextType = SysDic.All)]
        public virtual string SysPackSerial
        {
            get { return _sysPackSerial; }
            set
            {
                _sysPackSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "OtherPackSerial", ShortCode = "OtherPackSerial", Desc = "OtherPackSerial", ContextType = SysDic.All)]
        public virtual string OtherPackSerial
        {
            get { return _otherPackSerial; }
            set
            {
                _otherPackSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "UsePackSerial", ShortCode = "UsePackSerial", Desc = "UsePackSerial", ContextType = SysDic.All)]
        public virtual string UsePackSerial
        {
            get { return _usePackSerial; }
            set
            {
                _usePackSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "LotNo", ShortCode = "LotNo", Desc = "LotNo", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set
            {
                _lotNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 5000)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "DispOrder", ShortCode = "DispOrder", Desc = "DispOrder", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "Visible", ShortCode = "Visible", Desc = "Visible", ContextType = SysDic.All, Length = 1)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ReaCompanyID", ShortCode = "ReaCompanyID", Desc = "ReaCompanyID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompanyID
        {
            get { return _reaCompanyID; }
            set { _reaCompanyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompanyName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
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
        [DataDesc(CName = "", ShortCode = "GoodsCName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsCName
        {
            get { return _goodsCName; }
            set
            {
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
        [DataDesc(CName = "", ShortCode = "IsOutFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsOutFlag
        {
            get { return _isOutFlag; }
            set { _isOutFlag = value; }
        }
        /// <summary>
        /// 货品条码当次扫码数
        /// 大包装条码在出库可能大于1,最小包装条码等于1;
        /// 出库时可能对大包装单位的条码进行分批多次的扫码出库;
        /// </summary>
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品条码当次扫码数", ShortCode = "ScanCodeQty", Desc = "货品条码当次扫码数", ContextType = SysDic.All, Length = 8)]
        public virtual double? ScanCodeQty
        {
            get { return _scanCodeQty; }
            set { _scanCodeQty = value; }
        }
        /// <summary>
        /// 最小包装单位条码数
        /// 在进行供货生成条码或入库产生库存条码时按货品转换系数进行转换赋值
        /// </summary>
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "最小包装单位条码数", ShortCode = "MinBarCodeQty", Desc = "最小包装单位条码数", ContextType = SysDic.All, Length = 8)]
        public virtual double? MinBarCodeQty
        {
            get { return _minBarCodeQty; }
            set { _minBarCodeQty = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库房ID", ShortCode = "StorageID", Desc = "库房ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货架ID", ShortCode = "PlaceID", Desc = "货架ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PlaceID
        {
            get { return _placeID; }
            set { _placeID = value; }
        }
        [DataMember]
        [DataDesc(CName = "扫码包装单位", ShortCode = "ScanCodeGoodsUnit", Desc = "扫码包装单位", ContextType = SysDic.All, Length = 50)]
        public virtual string ScanCodeGoodsUnit
        {
            get { return _scanCodeGoodsUnit; }
            set
            {
                _scanCodeGoodsUnit = value;
            }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "剩余库存条码数", ShortCode = "OverageQty", Desc = "剩余库存条码数", ContextType = SysDic.All, Length = 8)]
        public virtual double? OverageQty
        {
            get { return _overageQty; }
            set { _overageQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "扫码货品Id", ShortCode = "ScanCodeGoodsID", Desc = "扫码货品Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ScanCodeGoodsID
        {
            get { return _scanCodeGoodsID; }
            set { _scanCodeGoodsID = value; }
        }
        #endregion
    }
    #endregion
}