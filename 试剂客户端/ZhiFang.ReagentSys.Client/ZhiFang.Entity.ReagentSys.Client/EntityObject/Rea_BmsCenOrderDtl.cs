using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region BmsCenOrderDtl

    /// <summary>
    /// BmsCenOrderDtl object for NHibernate mapped table 'BmsCenOrderDtl'.
    /// int?及double?添加上[JsonConverter(typeof(JsonConvertClass))]时,在上传平台进行序列化及反序列化时值为空
    /// </summary>
    [DataContract]
    [DataDesc(CName = "订货明细表", ClassCName = "ReaBmsCenOrderDtl", ShortCode = "ReaBmsCenOrderDtl", Desc = "订货明细表")]
    public class ReaBmsCenOrderDtl : BaseEntityService
    {
        #region Member Variables

        protected string _orderDtlNo;
        protected string _orderDocNo;
        protected string _otherOrderDocNo;
        protected string _prodGoodsNo;
        protected string _prodOrgName;
        protected string _goodsName;
        protected string _goodsUnit;
        protected string _unitMemo;
        protected double? _goodsQty;
        protected double? _currentQty;
        protected int? _iOFlag;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _memo;
        protected string _compMemo;
        protected string _labMemo;
        protected double? _price;
        protected double? _sumTotal;
        protected int _deleteFlag;
        protected DateTime? _dataUpdateTime;

        protected long? _reaGoodsID;
        protected string _reaGoodsName;
        protected string _goodsNo;
        protected long? _goodsID;
        protected long? _prodID;
        protected long? _compGoodsLinkID;
        protected long? _labcGoodsLinkID;
        protected long _orderDocID;
        protected long _barCodeType;
        protected long _isPrintBarCode;
        protected string _prodOrgNo;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        protected int _goodsSort;
        protected long? _labOrderDtlID;
        protected double? _reqGoodsQty;
        protected DateTime? _arrivalTime;
        protected double? _expectedStock;
        protected double? _monthlyUsage;
        protected double? _lastMonthlyUsage;

        protected double _suppliedQty = 0;//已供数量，默认=0
        protected double _unSupplyQty;//未供数量，默认=订货数量
        #endregion

        #region Constructors

        public ReaBmsCenOrderDtl() { }
        public ReaBmsCenOrderDtl(long labID, string orderDtlNo, long orderDocID, string orderDocNo, long goodsID, string prodGoodsNo, long prodID, string prodOrgName, string goodsName, string goodsUnit, string unitMemo, double goodsQty, double currentQty, int iOFlag, string zX1, string zX2, string zX3, double price, long reaGoodsID, string reaGoodsName, string goodsNo, long compGoodsLinkID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, double sumTotal, int deleteFlag, string memo, string labMemo, string compMemo, string otherOrderDocNo, long labcGoodsLinkID, long barCodeType, long isPrintBarCode, string prodOrgNo, string reaGoodsNo, string cenOrgGoodsNo, int goodsSort, long labOrderDtlID, double reqGoodsQty, DateTime arrivalTime, double expectedStock, double monthlyUsage, double lastMonthlyUsage)
        {
            this._labID = labID;
            this._orderDtlNo = orderDtlNo;
            this._orderDocID = orderDocID;
            this._orderDocNo = orderDocNo;
            this._goodsID = goodsID;
            this._prodGoodsNo = prodGoodsNo;
            this._prodID = prodID;
            this._prodOrgName = prodOrgName;
            this._goodsName = goodsName;
            this._goodsUnit = goodsUnit;
            this._unitMemo = unitMemo;
            this._goodsQty = goodsQty;
            this._currentQty = currentQty;
            this._iOFlag = iOFlag;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._price = price;
            this._reaGoodsID = reaGoodsID;
            this._reaGoodsName = reaGoodsName;
            this._goodsNo = goodsNo;
            this._compGoodsLinkID = compGoodsLinkID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._sumTotal = sumTotal;
            this._deleteFlag = deleteFlag;
            this._memo = memo;
            this._labMemo = labMemo;
            this._compMemo = compMemo;
            this._otherOrderDocNo = otherOrderDocNo;
            this._labcGoodsLinkID = labcGoodsLinkID;
            this._barCodeType = barCodeType;
            this._isPrintBarCode = isPrintBarCode;
            this._prodOrgNo = prodOrgNo;
            this._reaGoodsNo = reaGoodsNo;
            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._goodsSort = goodsSort;
            this._labOrderDtlID = labOrderDtlID;
            this._reqGoodsQty = reqGoodsQty;
            this._arrivalTime = arrivalTime;
            this._expectedStock = expectedStock;
            this._monthlyUsage = monthlyUsage;
            this._lastMonthlyUsage = lastMonthlyUsage;
        }


        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "到货时间", ShortCode = "ArrivalTime", Desc = "到货时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ArrivalTime
        {
            get { return _arrivalTime; }
            set { _arrivalTime = value; }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预期库存量", ShortCode = "ExpectedStock", Desc = "预期库存量", ContextType = SysDic.Number, Length = 8)]
        public virtual double? ExpectedStock
        {
            get { return _expectedStock; }
            set { _expectedStock = value; }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "理论月用量", ShortCode = "MonthlyUsage", Desc = "理论月用量", ContextType = SysDic.Number, Length = 8)]
        public virtual double? MonthlyUsage
        {
            get { return _monthlyUsage; }
            set { _monthlyUsage = value; }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上月月用量", ShortCode = "LastMonthlyUsage", Desc = "上月月用量", ContextType = SysDic.Number, Length = 8)]
        public virtual double? LastMonthlyUsage
        {
            get { return _lastMonthlyUsage; }
            set { _lastMonthlyUsage = value; }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请购进数量", ShortCode = "ReqGoodsQty", Desc = "申请购进数量", ContextType = SysDic.Number, Length = 8)]
        public virtual double? ReqGoodsQty
        {
            get { return _reqGoodsQty; }
            set { _reqGoodsQty = value; }
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
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 8)]
        public virtual long BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
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
        [DataDesc(CName = "订单ID", ShortCode = "OrderDocID", Desc = "订单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long OrderDocID
        {
            get { return _orderDocID; }
            set { _orderDocID = value; }
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
        [DataDesc(CName = "ProdID", ShortCode = "ProdID", Desc = "ProdID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProdID
        {
            get { return _prodID; }
            set { _prodID = value; }
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
        [DataDesc(CName = "订货明细单号", ShortCode = "OrderDtlNo", Desc = "订货明细单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDtlNo
        {
            get { return _orderDtlNo; }
            set
            {
                _orderDtlNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "订货总单号", ShortCode = "OrderDocNo", Desc = "订货总单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDocNo
        {
            get { return _orderDocNo; }
            set
            {
                _orderDocNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "第三方订货单号", ShortCode = "OtherOrderDocNo", Desc = "第三方订货单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OtherOrderDocNo
        {
            get { return _otherOrderDocNo; }
            set
            {
                _otherOrderDocNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "厂商产品编号", ShortCode = "ProdGoodsNo", Desc = "厂商产品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set
            {
                _prodGoodsNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "厂商名称", ShortCode = "ProdOrgName", Desc = "厂商名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName
        {
            get { return _prodOrgName; }
            set
            {
                _prodOrgName = value;
            }
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
        [DataDesc(CName = "包装单位描述", ShortCode = "UnitMemo", Desc = "包装单位描述", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                _unitMemo = value;
            }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批订货数量", ShortCode = "GoodsQty", Desc = "审批订货数量", ContextType = SysDic.All, Length = 8)]
        public virtual double? GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "当前库存数量", ShortCode = "CurrentQty", Desc = "当前库存数量", ContextType = SysDic.All, Length = 8)]
        public virtual double? CurrentQty
        {
            get { return _currentQty; }
            set { _currentQty = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据上传标志", ShortCode = "IOFlag", Desc = "数据上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int? IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 50)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "订货方备注", ShortCode = "LabMemo", Desc = "订货方备注", ContextType = SysDic.All, Length = 500)]
        public virtual string LabMemo
        {
            get { return _labMemo; }
            set
            {
                _labMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "供货方备注", ShortCode = "CompMemo", Desc = "供货方备注", ContextType = SysDic.All, Length = 500)]
        public virtual string CompMemo
        {
            get { return _compMemo; }
            set
            {
                _compMemo = value;
            }
        }

        [DataMember]
       // [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单价", ShortCode = "Price", Desc = "单价", ContextType = SysDic.Number)]
        public virtual double? Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总计金额", ShortCode = "SumTotal", Desc = "总计金额", ContextType = SysDic.Number)]
        public virtual double? SumTotal
        {
            get { return _sumTotal; }
            set { _sumTotal = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "删除标记", ShortCode = "DeleteFlag", Desc = "删除标记", ContextType = SysDic.All)]
        public virtual int DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        //预留属性,接口序列化使用，数据库中不存在此字段
        [DataMember]
        [DataDesc(CName = "产品编号、平台货品平台编码", ShortCode = "GoodsNo", Desc = "产品编号、平台货品平台编码", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsNo
        {
            get { return _goodsNo; }
            set { _goodsNo = value; }
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
        [DataDesc(CName = "已供数量", ShortCode = "SuppliedQty", Desc = "已供数量，默认=0", ContextType = SysDic.All, Length = 100)]
        public virtual double SuppliedQty
        {
            get { return _suppliedQty; }
            set
            {
                _suppliedQty = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "未供数量", ShortCode = "UnSupplyQty", Desc = "未供数量，默认=订货数量(审批数量)", ContextType = SysDic.All, Length = 100)]
        public virtual double UnSupplyQty
        {
            get { return _unSupplyQty; }
            set
            {
                _unSupplyQty = value;
            }
        }

        #endregion

        #region 自定义属性

        /// <summary>
        /// 挂网流水号
        /// </summary>
        public virtual string NetGoodsNo { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        public virtual string RegistNo { get; set; }

        /// <summary>
        /// 已入库数
        /// </summary>
        public virtual double InStorageQty { get; set; }
        /// <summary>
        /// 未入库数=订货数(实批数)-已入库数
        /// </summary>
        public virtual double NotInStorageQty { get; set; }

        protected long? _deptID;
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请部门ID", ShortCode = "DeptID", Desc = "申请部门ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }
        protected string _deptName;
        [DataMember]
        [DataDesc(CName = "申请部门", ShortCode = "DeptName", Desc = "申请部门", ContextType = SysDic.All, Length = 100)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                _deptName = value;
            }
        }
        protected long _reaCompID;
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供货方ID", ShortCode = "ReaCompID", Desc = "供货方ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ReaCompID
        {
            get { return _reaCompID; }
            set { _reaCompID = value; }
        }
        protected string _companyName;
        [DataMember]
        [DataDesc(CName = "供货方", ShortCode = "CompanyName", Desc = "供货方", ContextType = SysDic.All, Length = 100)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
            }
        }
        protected long _labcID;
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订货方ID", ShortCode = "LabcID", Desc = "订货方ID", ContextType = SysDic.All, Length = 8)]
        public virtual long LabcID
        {
            get { return _labcID; }
            set { _labcID = value; }
        }
        protected string _labcName;
        [DataMember]
        [DataDesc(CName = "订货方", ShortCode = "LabcName", Desc = "订货方", ContextType = SysDic.All, Length = 100)]
        public virtual string LabcName
        {
            get { return _labcName; }
            set
            {
                _labcName = value;
            }
        }
        protected string _reaServerLabcCode;
        [DataMember]
        [DataDesc(CName = "ReaServerLabcCode", ShortCode = "ReaServerLabcCode", Desc = "ReaServerLabcCode", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaServerLabcCode
        {
            get { return _reaServerLabcCode; }
            set
            {
                _reaServerLabcCode = value;
            }
        }

        protected string _goodEName;
        [DataMember]
        [DataDesc(CName = "货品英文名称", ShortCode = "GoodEName", Desc = "货品英文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodEName
        {
            get { return _goodEName; }
            set
            {
                _goodEName = value;
            }
        }
        protected string _goodSName;
        [DataMember]
        [DataDesc(CName = "货品简称", ShortCode = "GoodSName", Desc = "货品简称", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodSName
        {
            get { return _goodSName; }
            set
            {
                _goodSName = value;
            }
        }
        protected string _goodsClass;
        protected string _goodsClassType;
        [DataMember]
        [DataDesc(CName = "一级分类", ShortCode = "GoodsClass", Desc = "一级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClass
        {
            get { return _goodsClass; }
            set { _goodsClass = value; }
        }

        [DataMember]
        [DataDesc(CName = "二级分类", ShortCode = "GoodsClassType", Desc = "二级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClassType
        {
            get { return _goodsClassType; }
            set { _goodsClassType = value; }
        }

        //订单汇总统计构造函数
<<<<<<< .mine
        public ReaBmsCenOrderDtl(long labID, long deptId, string deptName, long reaCompID, string companyName, long labcID, string labcName, string reaServerLabcCode, long reaGoodsID, string reaGoodsName, string reaGoodsNo, string goodsUnit, string unitMemo, double expectedStock, double monthlyUsage, double lastMonthlyUsage, double reqGoodsQty, double goodsQty, double price, double sumTotal, double currentQty, long prodID, string prodOrgName, long barCodeType, DateTime dataAddTime, DateTime arrivalTime, string memo, long Id, long CompGoodsLinkID,string CenOrgGoodsNo)
||||||| .r2673
        public ReaBmsCenOrderDtl(long labID, long deptId, string deptName, long reaCompID, string companyName, long labcID, string labcName, string reaServerLabcCode, long reaGoodsID, string reaGoodsName, string reaGoodsNo, string goodsUnit, string unitMemo, double expectedStock, double monthlyUsage, double lastMonthlyUsage, double reqGoodsQty, double goodsQty, double price, double sumTotal, double currentQty, long prodID, string prodOrgName, long barCodeType, DateTime dataAddTime, DateTime arrivalTime, string memo, long Id)
=======
        public ReaBmsCenOrderDtl(long labID, long deptId, string deptName, long reaCompID, string companyName, long labcID, string labcName, string reaServerLabcCode, long reaGoodsID, string reaGoodsName, string reaGoodsNo, string goodsUnit, string unitMemo, double expectedStock, double monthlyUsage, double lastMonthlyUsage, double reqGoodsQty, double goodsQty, double price, double sumTotal, double currentQty, long prodID, string prodOrgName, long barCodeType, DateTime dataAddTime, DateTime arrivalTime, string memo, long Id, string orderDocNo, string registNo, string netGoodsNo, string goodSName)
>>>>>>> .r2783
        {
            this._labID = labID;
            this._deptID = deptId;
            this._deptName = deptName;
            this._reaCompID = reaCompID;
            this._companyName = companyName;

            this._labcID = labcID;
            this._labcName = labcName;
            this._reaServerLabcCode = reaServerLabcCode;
            this._reaGoodsID = reaGoodsID;
            this._reaGoodsName = reaGoodsName;
            this._reaGoodsNo = reaGoodsNo;

            this._goodsUnit = goodsUnit;
            this._unitMemo = unitMemo;
            this._expectedStock = expectedStock;
            this._monthlyUsage = monthlyUsage;
            this._lastMonthlyUsage = lastMonthlyUsage;

            this._reqGoodsQty = reqGoodsQty;
            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._currentQty = currentQty;

            this._prodID = prodID;
            this._prodOrgName = prodOrgName;
            this._barCodeType = barCodeType;
            this._dataAddTime = dataAddTime;
            this._arrivalTime = arrivalTime;

            this._memo = memo;
            this._id = Id;
<<<<<<< .mine
            this._compGoodsLinkID = CompGoodsLinkID;
            this._cenOrgGoodsNo = CenOrgGoodsNo;
||||||| .r2673
=======
            this._orderDocNo = orderDocNo;
            this.RegistNo = registNo;
            this.NetGoodsNo = netGoodsNo;
            this.GoodSName = goodSName;
>>>>>>> .r2783
        }
        #endregion

        #region 自定义属性和构造方法

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual ReaGoods ReaGoods { get; set; }

        public ReaBmsCenOrderDtl(ReaBmsCenOrderDtl reaBmsCenOrderDtl, ReaGoods reaGoods)
        {
            this._id = reaBmsCenOrderDtl.Id;
            this._labID = reaBmsCenOrderDtl.LabID;
            this._orderDtlNo = reaBmsCenOrderDtl.OrderDtlNo;
            this._orderDocID = reaBmsCenOrderDtl.OrderDocID;
            this._orderDocNo = reaBmsCenOrderDtl.OrderDocNo;
            this._goodsID = reaBmsCenOrderDtl.GoodsID;
            this._prodGoodsNo = reaBmsCenOrderDtl.ProdGoodsNo;
            this._prodID = reaBmsCenOrderDtl.ProdID;
            this._prodOrgName = reaBmsCenOrderDtl.ProdOrgName;
            this._goodsName = reaBmsCenOrderDtl.GoodSName;
            this._goodsUnit = reaBmsCenOrderDtl.GoodsUnit;
            this._unitMemo = reaBmsCenOrderDtl.UnitMemo;
            this._goodsQty = reaBmsCenOrderDtl.GoodsQty;
            this._currentQty = reaBmsCenOrderDtl.CurrentQty;
            this._iOFlag = reaBmsCenOrderDtl.IOFlag;
            this._zX1 = reaBmsCenOrderDtl.ZX1;
            this._zX2 = reaBmsCenOrderDtl.ZX2;
            this._zX3 = reaBmsCenOrderDtl.ZX3;
            this._price = reaBmsCenOrderDtl.Price;
            this._reaGoodsID = reaBmsCenOrderDtl.ReaGoodsID;
            this._reaGoodsName = reaBmsCenOrderDtl.ReaGoodsName;
            this._goodsNo = reaBmsCenOrderDtl.GoodsNo;
            this._compGoodsLinkID = reaBmsCenOrderDtl.CompGoodsLinkID;
            this._dataAddTime = reaBmsCenOrderDtl.DataAddTime;
            this._dataUpdateTime = reaBmsCenOrderDtl.DataUpdateTime;
            this._dataTimeStamp = reaBmsCenOrderDtl.DataTimeStamp;
            this._sumTotal = reaBmsCenOrderDtl.SumTotal;
            this._deleteFlag = reaBmsCenOrderDtl.DeleteFlag;
            this._memo = reaBmsCenOrderDtl.Memo;
            this._labMemo = reaBmsCenOrderDtl.LabMemo;
            this._compMemo = reaBmsCenOrderDtl.CompMemo;
            this._otherOrderDocNo = reaBmsCenOrderDtl.OtherOrderDocNo;
            this._labcGoodsLinkID = reaBmsCenOrderDtl.LabcGoodsLinkID;
            this._barCodeType = reaBmsCenOrderDtl.BarCodeType;
            this._isPrintBarCode = reaBmsCenOrderDtl.IsPrintBarCode;
            this._prodOrgNo = reaBmsCenOrderDtl.ProdOrgNo;
            this._reaGoodsNo = reaBmsCenOrderDtl.ReaGoodsNo;
            this._cenOrgGoodsNo = reaBmsCenOrderDtl.CenOrgGoodsNo;
            this._goodsSort = reaBmsCenOrderDtl.GoodsSort;
            this._labOrderDtlID = reaBmsCenOrderDtl.LabOrderDtlID;
            this._reqGoodsQty = reaBmsCenOrderDtl.ReqGoodsQty;
            this._arrivalTime = reaBmsCenOrderDtl.ArrivalTime;
            this._expectedStock = reaBmsCenOrderDtl.ExpectedStock;
            this._monthlyUsage = reaBmsCenOrderDtl.MonthlyUsage;
            this._lastMonthlyUsage = reaBmsCenOrderDtl.LastMonthlyUsage;

            this.GoodSName = reaGoods.SName;
            this.GoodEName = reaGoods.EName;
            this.ReaGoods = reaGoods;

            this.InStorageQty = 0;
            this.NotInStorageQty = (reaBmsCenOrderDtl.GoodsQty == null ? 0 : reaBmsCenOrderDtl.GoodsQty.Value);
        }

        #endregion
    }
    #endregion
}