using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsTransferDtl

    /// <summary>
    /// ReaBmsTransferDtl object for NHibernate mapped table 'Rea_BmsTransferDtl'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "移库明细表", ClassCName = "ReaBmsTransferDtl", ShortCode = "ReaBmsTransferDtl", Desc = "")]
    public class ReaBmsTransferDtl : BaseEntity
    {
        #region Member Variables

        protected long? _transferDocID;
        protected string _qtyDtlID;
        protected string _sQtyDtlID;
        protected long? _goodsID;
        protected string _goodsCName;
        protected string _serialNo;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected long? _reaCompanyID;
        protected string _reaCompanyName;
        protected double _goodsQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected string _lotNo;
        protected long? _sStorageID;
        protected long? _sPlaceID;
        protected string _sStorageName;
        protected string _sPlaceName;
        protected long? _dStorageID;
        protected long? _dPlaceID;
        protected string _dStorageName;
        protected string _dPlaceName;
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
        protected string _SysLotSerial;
        protected string _GoodsNo;
        protected long? _compGoodsLinkID;
        protected string _ReaServerCompCode;
        protected int _barCodeType;
        protected string _sysLotSerial;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
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
        #endregion

        #region Constructors

        public ReaBmsTransferDtl() { }

        public ReaBmsTransferDtl(long labID, long transferDocID, string qtyDtlID, long goodsID, string goodsCName, string serialNo, long goodsUnitID, string goodsUnit, long reaCompanyID, string reaCompanyName, double goodsQty, double price, double sumTotal, double taxRate, string lotNo, long sStorageID, long sPlaceID, string sStorageName, string sPlaceName, long dStorageID, long dPlaceID, string dStorageName, string dPlaceName, string goodsSerial, string packSerial, string lotSerial, string mixSerial, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._transferDocID = transferDocID;
            this._qtyDtlID = qtyDtlID;
            this._goodsID = goodsID;
            this._goodsCName = goodsCName;
            this._serialNo = serialNo;
            this._goodsUnitID = goodsUnitID;
            this._goodsUnit = goodsUnit;
            this._reaCompanyID = reaCompanyID;
            this._reaCompanyName = reaCompanyName;
            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._lotNo = lotNo;
            this._sStorageID = sStorageID;
            this._sPlaceID = sPlaceID;
            this._sStorageName = sStorageName;
            this._sPlaceName = sPlaceName;
            this._dStorageID = dStorageID;
            this._dPlaceID = dPlaceID;
            this._dStorageName = dStorageName;
            this._dPlaceName = dPlaceName;
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
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "移库总单ID", ShortCode = "TransferDocID", Desc = "移库总单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TransferDocID
        {
            get { return _transferDocID; }
            set { _transferDocID = value; }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "(移库扫码关联的)原库存ID", ShortCode = "SQtyDtlID", Desc = "(移库扫码关联的)原库存ID", ContextType = SysDic.All, Length = 400)]
        public virtual string SQtyDtlID
        {
            get { return _sQtyDtlID; }
            set { _sQtyDtlID = value; }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "移库的库存IDStr(一个移库明细可能对应多个库存记录ID)", ShortCode = "QtyDtlID", Desc = "移库的库存IDStr(一个移库明细可能对应多个库存记录ID)", ContextType = SysDic.All, Length = 400)]
        public virtual string QtyDtlID
        {
            get { return _qtyDtlID; }
            set { _qtyDtlID = value; }
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
                _serialNo = value;
            }
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
        [DataDesc(CName = "本地供应商ID", ShortCode = "ReaCompanyID", Desc = "本地供应商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompanyID
        {
            get { return _reaCompanyID; }
            set { _reaCompanyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "本地供应商名称", ShortCode = "ReaCompanyName", Desc = "本地供应商名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ReaCompanyName
        {
            get { return _reaCompanyName; }
            set
            {
                _reaCompanyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "移库数量", ShortCode = "GoodsQty", Desc = "移库数量", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "源库房ID", ShortCode = "SStorageID", Desc = "源库房ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SStorageID
        {
            get { return _sStorageID; }
            set { _sStorageID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "源货架ID", ShortCode = "SPlaceID", Desc = "源货架ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SPlaceID
        {
            get { return _sPlaceID; }
            set { _sPlaceID = value; }
        }

        [DataMember]
        [DataDesc(CName = "源库房名称", ShortCode = "SStorageName", Desc = "源库房名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SStorageName
        {
            get { return _sStorageName; }
            set
            {
                _sStorageName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "源货架名称", ShortCode = "SPlaceName", Desc = "源货架名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SPlaceName
        {
            get { return _sPlaceName; }
            set
            {
                _sPlaceName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "目的库房ID", ShortCode = "DStorageID", Desc = "目的库房ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DStorageID
        {
            get { return _dStorageID; }
            set { _dStorageID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "目的货架ID", ShortCode = "DPlaceID", Desc = "目的货架ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DPlaceID
        {
            get { return _dPlaceID; }
            set { _dPlaceID = value; }
        }

        [DataMember]
        [DataDesc(CName = "目的库房名称", ShortCode = "DStorageName", Desc = "目的库房名称", ContextType = SysDic.All, Length = 100)]
        public virtual string DStorageName
        {
            get { return _dStorageName; }
            set
            {
                _dStorageName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "目的货架名称", ShortCode = "DPlaceName", Desc = "目的货架名称", ContextType = SysDic.All, Length = 100)]
        public virtual string DPlaceName
        {
            get { return _dPlaceName; }
            set
            {
                _dPlaceName = value;
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
            get { return _SysLotSerial; }
            set
            {
                _SysLotSerial = value;
            }
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
                _createrName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "平台货品平台编码", ShortCode = "GoodsNo", Desc = "平台货品平台编码", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsNo
        {
            get { return _GoodsNo; }
            set
            {
                _GoodsNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品机构关系ID", ShortCode = "CompGoodsLinkID", Desc = "货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID
        {
            get { return _compGoodsLinkID; }
            set { _compGoodsLinkID = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商机平台构码", ShortCode = "ReaServerCompCode", Desc = "供应商机平台构码", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaServerCompCode
        {
            get { return _ReaServerCompCode; }
            set
            {
                _ReaServerCompCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生产日期", ShortCode = "ProdDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ProdDate
        {
            get { return _prodDate; }
            set { _prodDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期", ShortCode = "InvalidDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidDate
        {
            get { return _invalidDate; }
            set { _invalidDate = value; }
        }
        #endregion

        #region 自定义属性
        protected long? _deptID;
        protected string _deptName;
        protected long? _takerID;
        protected string _takerName;
        protected long? _inDtlID;
        protected string _inDocNo;

        /// <summary>
        /// 移库入库使用
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库明细ID", ShortCode = "InDtlID", Desc = "入库明细ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? InDtlID
        {
            get { return _inDtlID; }
            set { _inDtlID = value; }
        }
        /// <summary>
        /// 移库入库使用
        /// </summary>
        [DataMember]
        [DataDesc(CName = "入库主单号", ShortCode = "InDocNo", Desc = "入库主单号", ContextType = SysDic.All, Length = 100)]
        public virtual string InDocNo
        {
            get { return _inDocNo; }
            set
            {
                _inDocNo = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "使用部门ID", ShortCode = "DeptID", Desc = "使用部门ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                _deptName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "领用人ID", ShortCode = "TakerID", Desc = "领用人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TakerID
        {
            get { return _takerID; }
            set { _takerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "领用人", ShortCode = "TakerName", Desc = "领用人", ContextType = SysDic.All, Length = 100)]
        public virtual string TakerName
        {
            get { return _takerName; }
            set
            {
                _takerName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "新增的移库条码扫码操作集合", ShortCode = "ReaBmsTransferDtlLinkList", Desc = "新增的移库条码扫码操作集合")]
        public virtual IList<ReaGoodsBarcodeOperation> ReaBmsTransferDtlLinkList { get; set; }

        public ReaBmsTransferDtl(ReaBmsTransferDoc reabmstransferdoc, ReaBmsTransferDtl reabmstransferdtl)
        {
            this._reqCurrentQty = reabmstransferdtl.ReqCurrentQty;
            this._reqGoodsQty = reabmstransferdtl.ReqGoodsQty;
            this._id = reabmstransferdtl.Id;
            this._labID = reabmstransferdtl.LabID;

            this._deptID = reabmstransferdoc.DeptID;
            this._deptName = reabmstransferdoc.DeptName;
            this._takerID = reabmstransferdoc.TakerID;
            this._takerName = reabmstransferdoc.TakerName;
            this._reaCompanyID = reabmstransferdtl.ReaCompanyID;
            this._reaCompanyName = reabmstransferdtl.ReaCompanyName;

            this._sStorageID = reabmstransferdtl.SStorageID;
            this._sStorageName = reabmstransferdtl.SStorageName;
            this._sPlaceID = reabmstransferdtl.SPlaceID;
            this._sPlaceName = reabmstransferdtl.SPlaceName;
            this._dStorageID = reabmstransferdtl.DStorageID;

            this._dStorageName = reabmstransferdtl.DStorageName;
            this._dPlaceID = reabmstransferdtl.DPlaceID;
            this._dPlaceName = reabmstransferdtl.DPlaceName;
            this._goodsID = reabmstransferdtl.GoodsID;
            this._goodsCName = reabmstransferdtl.GoodsCName;
            this._reaGoodsNo = reabmstransferdtl.ReaGoodsNo;
            this._prodGoodsNo = reabmstransferdtl.ProdGoodsNo;
            this._cenOrgGoodsNo = reabmstransferdtl.CenOrgGoodsNo;
            //this._goodsNo = reabmstransferdtl.GoodsNo;

            this._goodsUnit = reabmstransferdtl.GoodsUnit;
            this._unitMemo = reabmstransferdtl.UnitMemo;
            this._lotNo = reabmstransferdtl.LotNo;
            this._invalidDate = reabmstransferdtl.InvalidDate;
            this._goodsQty = reabmstransferdtl.GoodsQty;

            this._price = reabmstransferdtl.Price;
            this._sumTotal = reabmstransferdtl.SumTotal;
            this._taxRate = reabmstransferdtl.TaxRate;
            this._createrID = reabmstransferdtl.CreaterID;
            this._createrName = reabmstransferdtl.CreaterName;

            this._barCodeType = reabmstransferdtl.BarCodeType;
            this._dataAddTime = reabmstransferdtl.DataAddTime;
            this._prodDate = reabmstransferdtl.ProdDate;
            this._memo = reabmstransferdtl.Memo;
        }
        #endregion
    }
    #endregion
}