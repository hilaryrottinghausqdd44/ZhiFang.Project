using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsOutDtl

    /// <summary>
    /// ReaBmsOutDtl object for NHibernate mapped table 'Rea_BmsOutDtl'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "出库明细表", ClassCName = "ReaBmsOutDtl", ShortCode = "ReaBmsOutDtl", Desc = "出库明细表")]
    public class ReaBmsOutDtl : BaseEntity
    {
        #region Member Variables

        protected long? _outDocID;
        protected string _qtyDtlID;
        protected long? _goodsID;
        protected string _goodsCName;
        protected string _serialNo;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected double _goodsQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected string _lotNo;
        protected long _storageID;
        protected long? _placeID;
        protected string _goodsSerial;
        protected string _lotSerial;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;
        protected string _storageName;
        protected string _placeName;
        protected long? _reaCompanyID;
        protected string _sysLotSerial;
        protected string _goodsNo;
        protected long? _compGoodsLinkID;
        protected string _reaServerCompCode;
        protected string _companyName;
        protected long _barCodeType;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected long? _testEquipID;
        protected string _testEquipName;
        protected string _prodGoodsNo;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        protected string _unitMemo;
        protected string _reaCompCode;
        protected int _goodsSort;
        protected string _lotQRCode;
        protected double? _reqGoodsQty;
        protected double? _reqCurrentQty;
        protected long? _goodsLotID;
        protected string _transportNo;
        protected string _lastLotNo;
        protected string _lastTransportNo;
        protected bool _isNeedBOpen;
        #endregion

        #region Constructors

        public ReaBmsOutDtl() { }

        public ReaBmsOutDtl(long labID, long outDocID, string qtyDtlID, long goodsID, string goodsCName, string serialNo, long goodsUnitID, string goodsUnit, double goodsQty, double price, double sumTotal, double taxRate, string lotNo, long storageID, long placeID, string goodsSerial, string lotSerial, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string storageName, string placeName, long reaCompanyID, string sysLotSerial, string goodsNo, long compGoodsLinkID, string reaServerCompCode, string companyName, long barCodeType, DateTime prodDate, DateTime invalidDate, long testEquipID, string testEquipName, string prodGoodsNo, string reaGoodsNo, string cenOrgGoodsNo, string lotQRCode, string unitMemo, string reaCompCode, int goodsSort, double reqCurrentQty, double reqGoodsQty, long goodsLotID)
        {
            this._labID = labID;
            this._outDocID = outDocID;
            this._qtyDtlID = qtyDtlID;
            this._goodsID = goodsID;
            this._goodsCName = goodsCName;
            this._serialNo = serialNo;
            this._goodsUnitID = goodsUnitID;
            this._goodsUnit = goodsUnit;
            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._lotNo = lotNo;
            this._storageID = storageID;
            this._placeID = placeID;
            this._goodsSerial = goodsSerial;
            this._lotSerial = lotSerial;
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
            this._sysLotSerial = sysLotSerial;
            this._goodsNo = goodsNo;
            this._compGoodsLinkID = compGoodsLinkID;
            this._reaServerCompCode = reaServerCompCode;
            this._companyName = companyName;
            this._barCodeType = barCodeType;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._testEquipID = testEquipID;
            this._testEquipName = testEquipName;
            this._prodGoodsNo = prodGoodsNo;
            this._reaGoodsNo = reaGoodsNo;
            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._lotQRCode = lotQRCode;
            this._unitMemo = unitMemo;
            this._reaCompCode = reaCompCode;
            this._goodsSort = goodsSort;
            this._reqCurrentQty = reqCurrentQty;
            this._reqGoodsQty = reqGoodsQty;
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
        [DataDesc(CName = "货品批号ID", ShortCode = "GoodsLotID", Desc = "货品批号ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsLotID
        {
            get { return _goodsLotID; }
            set { _goodsLotID = value; }
        }
        [DataMember]

        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请数量", ShortCode = "ReqGoodsQty", Desc = "申请数量", ContextType = SysDic.All, Length = 8)]
        public virtual double? ReqGoodsQty
        {
            get { return _reqGoodsQty; }
            set { _reqGoodsQty = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请时库存数", ShortCode = "ReqCurrentQty", Desc = "申请时库存数", ContextType = SysDic.All, Length = 8)]
        public virtual double? ReqCurrentQty
        {
            get { return _reqCurrentQty; }
            set { _reqCurrentQty = value; }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库关联的原库存ID", ShortCode = "QtyDtlID", Desc = "出库关联的原库存ID", ContextType = SysDic.All, Length = 400)]
        public virtual string QtyDtlID
        {
            get { return _qtyDtlID; }
            set { _qtyDtlID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库总单ID", ShortCode = "OutDocID", Desc = "出库总单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OutDocID
        {
            get { return _outDocID; }
            set { _outDocID = value; }
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
        [DataDesc(CName = "货品中文名", ShortCode = "GoodsCName", Desc = "货品中文名", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "条码号", ShortCode = "SerialNo", Desc = "条码号", ContextType = SysDic.All, Length = 50)]
        public virtual string SerialNo
        {
            get { return _serialNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SerialNo", value, value.ToString());
                _serialNo = value;
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
        [DataDesc(CName = "出库数量", ShortCode = "GoodsQty", Desc = "出库数量", ContextType = SysDic.All, Length = 8)]
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
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LotNo", value, value.ToString());
                _lotNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库房ID", ShortCode = "StorageID", Desc = "库房ID", ContextType = SysDic.All, Length = 8)]
        public virtual long StorageID
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
        [DataDesc(CName = "产品条码", ShortCode = "GoodsSerial", Desc = "产品条码", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "批号条码", ShortCode = "LotSerial", Desc = "批号条码", ContextType = SysDic.All, Length = 150)]
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
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long CreaterID
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
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
        [DataDesc(CName = "", ShortCode = "CompanyName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CompanyName", value, value.ToString());
                _companyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BarCodeType", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
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
        [DataDesc(CName = "", ShortCode = "TestEquipID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? TestEquipID
        {
            get { return _testEquipID; }
            set { _testEquipID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestEquipName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string TestEquipName
        {
            get { return _testEquipName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for TestEquipName", value, value.ToString());
                _testEquipName = value;
            }
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

        [DataMember]
        [DataDesc(CName = "上一次出库的批号", ShortCode = "LastLotNo", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string LastLotNo
        {
            get { return _lastLotNo; }
            set
            {
                _lastLotNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "上一次出库的货运单号", ShortCode = "LastTransportNo", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string LastTransportNo
        {
            get { return _lastTransportNo; }
            set
            {
                _lastTransportNo = value;
            }
        }

        #endregion

        #region 自定义属性

        /// <summary>
        /// 出库单号，模块：统计-出库明细汇总 ，使用
        /// </summary>
        public virtual string OutDocNo { get; set; }

        #region 出库变更台账导出使用，批号是否改变/货运单是否改变

        public virtual string TransportNoIsChange { get; set; }
        public virtual string LotNoIsChange { get; set; }

        #endregion

        [DataMember]
        [DataDesc(CName = "新增货品出库扫码的条码操作集合", ShortCode = "ReaBmsOutDtlLinkList", Desc = "新增货品出库扫码的条码操作集合")]
        public virtual IList<ReaGoodsBarcodeOperation> ReaBmsOutDtlLinkList { get; set; }

        protected long _deptID;
        protected string _deptName;
        protected string _eName;
        protected string _sName;
        protected string _purpose;
        protected string _storageType;
        protected string _prodOrgName;
        protected string _goodsClass;
        protected string _goodsClassType;
        protected bool isAllOut;
        //protected ReaGoods _reaGoods;

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "当前出库货品是否全部出库", ShortCode = "IsAllOut", Desc = "当前出库货品是否全部出库", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsAllOut
        {
            get { return isAllOut; }
            set { isAllOut = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "使用部门ID", ShortCode = "DeptID", Desc = "使用部门ID", ContextType = SysDic.All, Length = 8)]
        public virtual long DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }
        [DataMember]
        [DataDesc(CName = "部门名称", ShortCode = "DeptName", Desc = "部门名称", ContextType = SysDic.All, Length = 100)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
                _deptName = value;
            }
        }
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
        [DataDesc(CName = "用途", ShortCode = "Purpose", Desc = "用途", ContextType = SysDic.All, Length = 500)]
        public virtual string Purpose
        {
            get { return _purpose; }
            set
            {
                _purpose = value;
            }
        }
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

        [DataMember]
        [DataDesc(CName = "单盒测试人次", ShortCode = "TestCount", Desc = "单盒测试人次", ContextType = SysDic.All, Length = 4)]
        public virtual int TestCount { get; set; }

        [DataMember]
        [DataDesc(CName = "注册号", ShortCode = "RegistNo", Desc = "注册号", ContextType = SysDic.All, Length = 200)]
        public virtual string RegistNo { get; set; }

        /// <summary>
        /// 单人价次:单人价次=出库明细的货品单价/单盒测定人次;
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单人价次", ShortCode = "TotalAveragePrice", Desc = "单人价次", ContextType = SysDic.All)]
        public virtual double TotalAveragePrice { get; set; }

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
        /// 用于存放挂网流水号，或其他一些特殊编码
        /// </summary>
        [DataMember]
        [DataDesc(CName = "挂网流水号", ShortCode = "NetGoodsNo", Desc = "挂网流水号", ContextType = SysDic.All, Length = 100)]
        public virtual string NetGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "是否医疗器械", ShortCode = "IsMed", Desc = "是否医疗器械", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsMed { get; set; }

        //[DataMember]
        //[DataDesc(CName = "货品表", ShortCode = "ReaGoods", Desc = "货品表")]
        //public virtual ReaGoods ReaGoods
        //{
        //    get { return _reaGoods; }
        //    set { _reaGoods = value; }
        //}
        #endregion

        #region Constructors
        private void _setReaBmsOutDtl(ReaBmsOutDtl reabmsoutdtl)
        {
            this._id = reabmsoutdtl.Id;
            this._labID = reabmsoutdtl.LabID;
            this._outDocID = reabmsoutdtl.OutDocID;
            this._qtyDtlID = reabmsoutdtl.QtyDtlID;
            this._goodsID = reabmsoutdtl.GoodsID;
            this._goodsCName = reabmsoutdtl.GoodsCName;
            this._serialNo = reabmsoutdtl.SerialNo;
            this._goodsUnitID = reabmsoutdtl.GoodsUnitID;
            this._goodsUnit = reabmsoutdtl.GoodsUnit;
            this._goodsQty = reabmsoutdtl.GoodsQty;
            this._price = reabmsoutdtl.Price;
            this._sumTotal = reabmsoutdtl.SumTotal;
            this._taxRate = reabmsoutdtl.TaxRate;
            this._lotNo = reabmsoutdtl.LotNo;
            this._storageID = reabmsoutdtl.StorageID;
            this._placeID = reabmsoutdtl.PlaceID;
            this._goodsSerial = reabmsoutdtl.GoodsSerial;
            this._lotSerial = reabmsoutdtl.LotSerial;
            this._zX1 = reabmsoutdtl.ZX1;
            this._zX2 = reabmsoutdtl.ZX2;
            this._zX3 = reabmsoutdtl.ZX3;
            this._dispOrder = reabmsoutdtl.DispOrder;
            this._memo = reabmsoutdtl.Memo;
            this._visible = reabmsoutdtl.Visible;
            this._createrID = reabmsoutdtl.CreaterID;
            this._createrName = reabmsoutdtl.CreaterName;
            this._dataAddTime = reabmsoutdtl.DataAddTime;
            this._dataUpdateTime = reabmsoutdtl.DataUpdateTime;
            this._dataTimeStamp = reabmsoutdtl.DataTimeStamp;
            this._storageName = reabmsoutdtl.StorageName;
            this._placeName = reabmsoutdtl.PlaceName;
            this._reaCompanyID = reabmsoutdtl.ReaCompanyID;
            this._sysLotSerial = reabmsoutdtl.SysLotSerial;
            this._goodsNo = reabmsoutdtl.GoodsNo;
            this._compGoodsLinkID = reabmsoutdtl.CompGoodsLinkID;
            this._reaServerCompCode = reabmsoutdtl.ReaServerCompCode;
            this._companyName = reabmsoutdtl.CompanyName;
            this._barCodeType = reabmsoutdtl.BarCodeType;
            this._prodDate = reabmsoutdtl.ProdDate;
            this._invalidDate = reabmsoutdtl.InvalidDate;
            this._testEquipID = reabmsoutdtl.TestEquipID;
            this._testEquipName = reabmsoutdtl.TestEquipName;
            this._prodGoodsNo = reabmsoutdtl.ProdGoodsNo;
            this._reaGoodsNo = reabmsoutdtl.ReaGoodsNo;
            this._cenOrgGoodsNo = reabmsoutdtl.CenOrgGoodsNo;
            this._lotQRCode = reabmsoutdtl.LotQRCode;
            this._unitMemo = reabmsoutdtl.UnitMemo;
            this._reaCompCode = reabmsoutdtl.ReaCompCode;
            this._goodsSort = reabmsoutdtl.GoodsSort;
            this._reqCurrentQty = reabmsoutdtl.ReqCurrentQty;
            this._reqGoodsQty = reabmsoutdtl.ReqGoodsQty;
            this._goodsLotID = reabmsoutdtl.GoodsLotID;

            this._transportNo = reabmsoutdtl.TransportNo;
            this._lastLotNo = reabmsoutdtl.LastLotNo;
            this._lastTransportNo = reabmsoutdtl.LastTransportNo;
            this._isNeedBOpen = reabmsoutdtl.IsNeedBOpen;
            
        }
        public ReaBmsOutDtl(ReaBmsOutDtl reabmsoutdtl)
        {
            _setReaBmsOutDtl(reabmsoutdtl);
        }
        public ReaBmsOutDtl(ReaBmsOutDoc reabmsoutdoc, ReaBmsOutDtl reabmsoutdtl)
        {
            _setReaBmsOutDtl(reabmsoutdtl);
            if (reabmsoutdoc != null)
            {
                if (reabmsoutdoc.DeptID.HasValue)
                    this._deptID = reabmsoutdoc.DeptID.Value;
                this._deptName = reabmsoutdoc.DeptName;
            }
        }
        public ReaBmsOutDtl(ReaBmsOutDoc reabmsoutdoc, ReaBmsOutDtl reabmsoutdtl, ReaGoods reagoods)
        {
            _setReaBmsOutDtl(reabmsoutdtl);
            //this._reaGoods = reagoods;

            if (reabmsoutdoc != null)
            {
                if (reabmsoutdoc.DeptID.HasValue)
                    this._deptID = reabmsoutdoc.DeptID.Value;
                this._deptName = reabmsoutdoc.DeptName;
                this.OutDocNo = reabmsoutdoc.OutDocNo;
            }
            if (reagoods != null)
            {
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
                this._storageType = reagoods.StorageType;
                this._purpose = reagoods.Purpose;

                this.TestCount = reagoods.TestCount;
                this.RegistNo = reagoods.RegistNo;
                this.ProdOrgName = reagoods.ProdOrgName;
                this.NetGoodsNo = reagoods.NetGoodsNo;
                this.IsMed = reagoods.IsMed;
            }
        }

        #endregion
        #endregion
    }
    #endregion
}