using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsQtyDtl

    /// <summary>
    /// ReaBmsQtyDtl object for NHibernate mapped table 'Rea_BmsQtyDtl'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "库存表", ClassCName = "ReaBmsQtyDtl", ShortCode = "ReaBmsQtyDtl", Desc = "库存表")]
    public class ReaBmsQtyDtl : BaseEntity
    {
        #region Member Variables

        protected long? _pQtyDtlID;
        protected long? _reaCompanyID;
        protected string _companyName;
        protected long? _goodsID;
        protected string _goodsName;
        protected string _lotNo;
        protected long? _storageID;
        protected long? _placeID;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected double? _goodsQty;
        protected double? _price;
        protected double? _sumTotal;
        protected double? _taxRate;
        protected int _outFlag;
        protected int _sumFlag;
        protected int _iOFlag;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _memo;
        protected int _dispOrder;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;
        protected string _storageName;
        protected string _placeName;
        protected string _goodsSerial;
        protected string _lotSerial;
        protected string _sysLotSerial;
        protected string _lotQRCode;
        protected string _goodsNo;
        protected long? _compGoodsLinkID;
        protected string _reaServerCompCode;
        protected int _barCodeType;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected string _unitMemo;
        protected string _registerNo;
        protected long? _inDtlID;
        protected DateTime? _invalidWarningDate;
        protected int _printCount;
        protected string _prodGoodsNo;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        protected string _reaCompCode;
        protected int _goodsSort;
        protected string _inDocNo;
        protected string _cSQtyDtlNo;
        protected string _cSInDtlNo;
        protected bool? _isNeedPerformanceTest;
        protected long? _verificationStatus;
        protected long? _goodsLotID;
        protected long _qtyDtlMark;
        protected string _transportNo;

        protected bool _isNeedBOpen;
        #endregion

        #region Constructors

        public ReaBmsQtyDtl() { }
        public ReaBmsQtyDtl(long labID, long pQtyDtlID, long reaCompanyID, string companyName, long goodsID, string goodsName, string lotNo, long storageID, long placeID, long goodsUnitID, string goodsUnit, double goodsQty, double price, double sumTotal, double taxRate, int outFlag, int sumFlag, int iOFlag, string zX1, string zX2, string zX3, string memo, int dispOrder, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string storageName, string placeName, string goodsSerial, string lotSerial, string sysLotSerial, string goodsNo, long compGoodsLinkID, string reaServerCompCode, int barCodeType, DateTime prodDate, DateTime invalidDate, string unitMemo, string registerNo, long inDtlID, DateTime invalidWarningDate, int printCount, string prodGoodsNo, string reaGoodsNo, string cenOrgGoodsNo, string lotQRCode, string reaCompCode, int goodsSort, string inDocNo, string cSQtyDtlNo, string cSInDtlNo, bool isNeedPerformanceTest, long verificationStatus, long goodsLotID)
        {
            this._labID = labID;
            this._pQtyDtlID = pQtyDtlID;
            this._reaCompanyID = reaCompanyID;
            this._companyName = companyName;
            this._goodsID = goodsID;
            this._goodsName = goodsName;
            this._lotNo = lotNo;
            this._storageID = storageID;
            this._placeID = placeID;
            this._goodsUnitID = goodsUnitID;
            this._goodsUnit = goodsUnit;
            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._outFlag = outFlag;
            this._sumFlag = sumFlag;
            this._iOFlag = iOFlag;
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
            this._storageName = storageName;
            this._placeName = placeName;
            this._goodsSerial = goodsSerial;
            this._lotSerial = lotSerial;
            this._sysLotSerial = sysLotSerial;
            this._goodsNo = goodsNo;
            this._compGoodsLinkID = compGoodsLinkID;
            this._reaServerCompCode = reaServerCompCode;
            this._barCodeType = barCodeType;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._unitMemo = unitMemo;
            this._registerNo = registerNo;
            this._inDtlID = inDtlID;
            this._inDocNo = inDocNo;
            this._invalidWarningDate = invalidWarningDate;
            this._printCount = printCount;
            this._prodGoodsNo = prodGoodsNo;
            this._reaGoodsNo = reaGoodsNo;
            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._lotQRCode = lotQRCode;
            this._reaCompCode = reaCompCode;
            this._goodsSort = goodsSort;
            this._cSQtyDtlNo = cSQtyDtlNo;
            this._cSInDtlNo = cSInDtlNo;
            this._isNeedPerformanceTest = isNeedPerformanceTest;
            this._verificationStatus = verificationStatus;
            this._goodsLotID = goodsLotID;
        }

        #endregion


        #region Public Properties

        [DataMember]
        [DataDesc(CName = "是否需要开瓶管理", ShortCode = "IsNeedBOpen", Desc = "是否需要开瓶管理", ContextType = SysDic.All, Length = 4)]
        public virtual bool IsNeedBOpen
        {
            get { return _isNeedBOpen; }
            set { _isNeedBOpen = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存标志", ShortCode = "QtyDtlMark", Desc = "库存标志", ContextType = SysDic.All, Length = 8)]
        public virtual long QtyDtlMark
        {
            get { return _qtyDtlMark; }
            set { _qtyDtlMark = value; }
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
        [DataDesc(CName = "", ShortCode = "InDtlID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? InDtlID
        {
            get { return _inDtlID; }
            set { _inDtlID = value; }
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
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "父库存ID", ShortCode = "PQtyDtlID", Desc = "父库存ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PQtyDtlID
        {
            get { return _pQtyDtlID; }
            set { _pQtyDtlID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地供应商", ShortCode = "ReaCompanyID", Desc = "本地供应商", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompanyID
        {
            get { return _reaCompanyID; }
            set { _reaCompanyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "CompanyName", Desc = "供应商名称", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "货品ID", ShortCode = "GoodsID", Desc = "货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "包装单位ID", ShortCode = "GoodsUnitID", Desc = "包装单位ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsUnitID
        {
            get { return _goodsUnitID; }
            set { _goodsUnitID = value; }
        }

        [DataMember]
        [DataDesc(CName = "包装单位", ShortCode = "GoodsUnit", Desc = "包装单位", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "库存数量", ShortCode = "GoodsQty", Desc = "库存数量", ContextType = SysDic.All, Length = 8)]
        public virtual double? GoodsQty
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
        [DataDesc(CName = "库存总计金额", ShortCode = "SumTotal", Desc = "库存总计金额", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "出库标志", ShortCode = "OutFlag", Desc = "出库标志", ContextType = SysDic.All, Length = 4)]
        public virtual int OutFlag
        {
            get { return _outFlag; }
            set { _outFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "统计标志", ShortCode = "SumFlag", Desc = "统计标志", ContextType = SysDic.All, Length = 4)]
        public virtual int SumFlag
        {
            get { return _sumFlag; }
            set { _sumFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "数据上传标志", ShortCode = "IOFlag", Desc = "数据上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "更新时间", ShortCode = "DataUpdateTime", Desc = "更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "存储库房名称", ShortCode = "StorageName", Desc = "存储库房名称", ContextType = SysDic.All, Length = 100)]
        public virtual string StorageName
        {
            get { return _storageName; }
            set
            {

                _storageName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "货位名称", ShortCode = "PlaceName", Desc = "货位名称", ContextType = SysDic.All, Length = 100)]
        public virtual string PlaceName
        {
            get { return _placeName; }
            set
            {

                _placeName = value;
            }
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
        [DataDesc(CName = "批号条码", ShortCode = "LotSerial", Desc = "批号条码", ContextType = SysDic.All, Length = 150)]
        public virtual string LotSerial
        {
            get { return _lotSerial; }
            set
            {
                _lotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = " 系统内部批号条码", ShortCode = "SysLotSerial", Desc = " 系统内部批号条码", ContextType = SysDic.All, Length = 150)]
        public virtual string SysLotSerial
        {
            get { return _sysLotSerial; }
            set
            {
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
        [DataDesc(CName = "批条码打印次数", ShortCode = "PrintCount", Desc = "批条码打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
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

        [DataMember]
        [DataDesc(CName = "货运单号", ShortCode = "TransportNo", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string TransportNo
        {
            get { return _transportNo; }
            set
            {
                _transportNo = value;
            }
        }
        #endregion

        #region 自定义属性
        protected string _eName;
        protected string _sName;
        protected string _prodOrgName;
        protected string _goodsClass;
        protected string _goodsClassType;
        protected double? _goodsQtySum;
        protected double? _stocSurplusTestQty;
        protected double? _reaTestQty;
  
        [DataMember]
        [DataDesc(CName = "(移库/出库)货品扫码时封装条码的相关信息", ShortCode = "JObjectBarCode", Desc = "(移库/出库)货品扫码时封装条码的相关信息", ContextType = SysDic.All, Length = 500)]
        public virtual string JObjectBarCode { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? MonthlyUsage { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? StoreUpper { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? StoreLower { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? ComparisonValue { get; set; }

        [DataMember]
        [DataDesc(CName = "英文名", ShortCode = "EName", Desc = "英文名", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                _eName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 200)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                _sName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "品牌名称", ShortCode = "ProdOrgName", Desc = "品牌名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName
        {
            get { return _prodOrgName; }
            set
            {
                _prodOrgName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "一级分类", ShortCode = "GoodsClass", Desc = "一级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClass
        {
            get { return _goodsClass; }
            set
            {
                _goodsClass = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "二级分类", ShortCode = "GoodsClassType", Desc = "二级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClassType
        {
            get { return _goodsClassType; }
            set
            {
                _goodsClassType = value;
            }
        }
        /// <summary>
        /// 前台显示按合并条件后的库存货品的剩余总库存数
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "剩余总库存", ShortCode = "SumCurrentQty", Desc = "剩余总库存", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumCurrentQty
        {
            get { return _goodsQtySum; }
            set { _goodsQtySum = value; }
        }
        /// <summary>
        /// 机构货品理论测试数
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "机构货品理论测试数", ShortCode = "ReaTestQty", Desc = "机构货品理论测试数", ContextType = SysDic.All, Length = 8)]
        public virtual double? ReaTestQty
        {
            get { return _reaTestQty; }
            set { _reaTestQty = value; }
        }
        /// <summary>
        /// 库存所剩理论测试数
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存所剩理论测试数", ShortCode = "StocSurplusTestQty", Desc = "库存所剩理论测试数", ContextType = SysDic.All, Length = 8)]
        public virtual double? StocSurplusTestQty
        {
            get { return _stocSurplusTestQty; }
            set { _stocSurplusTestQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual ReaGoods ReaGoods { get; set; }

        private void _setReaBmsQtyDtl(ReaBmsQtyDtl reabmsqtydtl)
        {
            this._id = reabmsqtydtl.Id;
            this._qtyDtlMark = reabmsqtydtl.QtyDtlMark;
            this._labID = reabmsqtydtl.LabID;
            this._pQtyDtlID = reabmsqtydtl.PQtyDtlID;
            this._reaCompanyID = reabmsqtydtl.ReaCompanyID;
            this._companyName = reabmsqtydtl.CompanyName;
            this._goodsID = reabmsqtydtl.GoodsID;
            this._goodsName = reabmsqtydtl.GoodsName;
            this._lotNo = reabmsqtydtl.LotNo;
            this._storageID = reabmsqtydtl.StorageID;
            this._placeID = reabmsqtydtl.PlaceID;
            this._goodsUnitID = reabmsqtydtl.GoodsUnitID;
            this._goodsUnit = reabmsqtydtl.GoodsUnit;
            this._goodsQty = reabmsqtydtl.GoodsQty;
            this._price = reabmsqtydtl.Price;
            this._sumTotal = reabmsqtydtl.SumTotal;
            this._taxRate = reabmsqtydtl.TaxRate;
            this._outFlag = reabmsqtydtl.OutFlag;
            this._sumFlag = reabmsqtydtl.SumFlag;
            this._iOFlag = reabmsqtydtl.IOFlag;
            this._zX1 = reabmsqtydtl.ZX1;
            this._zX2 = reabmsqtydtl.ZX2;
            this._zX3 = reabmsqtydtl.ZX3;
            this._memo = reabmsqtydtl.Memo;
            this._dispOrder = reabmsqtydtl.DispOrder;
            this._visible = reabmsqtydtl.Visible;
            this._createrID = reabmsqtydtl.CreaterID;
            this._createrName = reabmsqtydtl.CreaterName;
            this._dataAddTime = reabmsqtydtl.DataAddTime;
            this._dataUpdateTime = reabmsqtydtl.DataUpdateTime;
            this._dataTimeStamp = reabmsqtydtl.DataTimeStamp;
            this._storageName = reabmsqtydtl.StorageName;
            this._placeName = reabmsqtydtl.PlaceName;
            this._goodsSerial = reabmsqtydtl.GoodsSerial;
            this._lotSerial = reabmsqtydtl.LotSerial;
            this._sysLotSerial = reabmsqtydtl.SysLotSerial;
            this._goodsNo = reabmsqtydtl.GoodsNo;
            this._compGoodsLinkID = reabmsqtydtl.CompGoodsLinkID;
            this._reaServerCompCode = reabmsqtydtl.ReaServerCompCode;
            this._barCodeType = reabmsqtydtl.BarCodeType;
            this._prodDate = reabmsqtydtl.ProdDate;
            this._invalidDate = reabmsqtydtl.InvalidDate;
            this._unitMemo = reabmsqtydtl.UnitMemo;
            this._registerNo = reabmsqtydtl.RegisterNo;
            this._inDtlID = reabmsqtydtl.InDtlID;
            this._invalidWarningDate = reabmsqtydtl.InvalidWarningDate;
            this._printCount = reabmsqtydtl.PrintCount;
            this._prodGoodsNo = reabmsqtydtl.ProdGoodsNo;
            this._reaGoodsNo = reabmsqtydtl.ReaGoodsNo;
            this._cenOrgGoodsNo = reabmsqtydtl.CenOrgGoodsNo;
            this._lotQRCode = reabmsqtydtl.LotQRCode;
            this._reaCompCode = reabmsqtydtl.ReaCompCode;
            this._goodsSort = reabmsqtydtl.GoodsSort;
            this._inDocNo = reabmsqtydtl.InDocNo;
            this._cSQtyDtlNo = reabmsqtydtl.CSQtyDtlNo;
            this._cSInDtlNo = reabmsqtydtl.CSInDtlNo;
            this._isNeedPerformanceTest = reabmsqtydtl.IsNeedPerformanceTest;
            this._verificationStatus = reabmsqtydtl.VerificationStatus;
            this._goodsLotID = reabmsqtydtl.GoodsLotID;
            this._transportNo = reabmsqtydtl.TransportNo;
            this._isNeedBOpen = reabmsqtydtl.IsNeedBOpen;
            
        }
        public ReaBmsQtyDtl(ReaBmsQtyDtl reabmsqtydtl)
        {
            _setReaBmsQtyDtl(reabmsqtydtl);
        }
        public ReaBmsQtyDtl(ReaBmsQtyDtl reabmsqtydtl, ReaGoods reagoods)
        {
            _setReaBmsQtyDtl(reabmsqtydtl);
            if (reagoods != null)
            {
                this.ReaGoods = reagoods;
                this.DispOrder = reagoods.DispOrder;

                this._eName = reagoods.EName;
                this._sName = reagoods.SName;
                if (string.IsNullOrEmpty(reagoods.ProdOrgName))
                    this._prodOrgName = "未知";
                else
                    this._prodOrgName = reagoods.ProdOrgName;
                if (string.IsNullOrEmpty(reagoods.GoodsClass))
                    this._goodsClass = "未知";
                else
                    this._goodsClass = reagoods.GoodsClass;
                if (string.IsNullOrEmpty(reagoods.GoodsClassType))
                    this._goodsClassType = "未知";
                else
                    this._goodsClassType = reagoods.GoodsClassType;
                this.MonthlyUsage = reagoods.MonthlyUsage;
                this.StoreUpper = reagoods.StoreUpper;
                this.StoreLower = reagoods.StoreLower;
                //库存所剩理论测试数计算:库存数量乘以基础数据中每包装单位理论测试数
                if (reagoods.TestCount > 0)
                {
                    this.ReaTestQty = reagoods.TestCount;
                    this.StocSurplusTestQty = reabmsqtydtl.GoodsQty * reagoods.TestCount;
                }
            }
        }
        #endregion
    }
    #endregion
}