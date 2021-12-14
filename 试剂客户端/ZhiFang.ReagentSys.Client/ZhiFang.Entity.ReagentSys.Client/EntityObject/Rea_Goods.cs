using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaGoods

    /// <summary>
    /// ReaGoods object for NHibernate mapped table 'Rea_Goods'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "平台货品表", ClassCName = "ReaGoods", ShortCode = "ReaGoods", Desc = "平台货品表")]
    public class ReaGoods : BaseEntity
    {
        #region Member Variables

        protected string _goodsSN;
        protected string _cName;
        protected string _sName;
        protected string _eName;
        protected string _shortCode;
        protected string _pinYinZiTou;
        protected string _goodsClass;
        protected string _goodsClassType;
        protected string _prodOrgName;
        protected string _prodEara;
        protected double _price;
        protected string _unitName;
        protected string _unitMemo;
        protected string _storageType;
        protected string _goodsDesc;
        protected string _approveDocNo;
        protected string _standard;
        protected string _registNo;
        protected DateTime? _registDate;
        protected DateTime? _registNoInvalidDate;
        protected string _purpose;
        protected string _constitute;
        protected byte[] _license;
        protected byte[] _goodsPicture;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _equipment;
        protected int _cenOrgConfirm;
        protected int _barCodeMgr;
        protected int _isRegister;
        protected int _isPrintBarCode;
        protected int _dispOrder;
        protected int _visible;
        protected string _goodsMemo;
        protected DateTime? _dataUpdateTime;
        protected long? _prodID;
        protected string _GonvertGroupCode;
        protected string _LinkGroupCode;
        protected Double _GonvertQty;
        protected int _GonvertSort;
        protected string _suitableType;
        protected int _testCount;
        protected int _GoodsSort;
        protected bool _IsMinUnit;
        protected double? _storeUpper;
        protected double? _storeLower;
        protected double? _beforeWarningDay;
        protected string _goodsNo;
        protected string _prodGoodsNo;
        protected string _reaGoodsNo;
        protected string _reaCompanyName;
        protected string _matchCode;
        protected string _deptName;
        protected double? _monthlyUsage;
        protected bool _isNeedPerformanceTest;
        protected long? _goodsUnitID;
        protected string _netGoodsNo;
        protected string _verificationMemo;

        protected bool _isNeedBOpen;
        protected int? _bOpenDays;
        protected bool _isMed;
        protected string _invType;
        protected DateTime? _ts;

        #endregion

        #region Constructors

        public ReaGoods() { }

        public ReaGoods(long labID, string goodsNo, string cName, string sName, string eName, string shortCode, string pinYinZiTou, string goodsClass, string goodsClassType, string prodGoodsNo, string prodOrgName, string prodEara, double price, string unitName, string unitMemo, string storageType, string goodsDesc, string approveDocNo, string standard, string registNo, DateTime registDate, DateTime registNoInvalidDate, string purpose, string constitute, byte[] license, byte[] goodsPicture, string zX1, string zX2, string zX3, string equipment, int cenOrgConfirm, int barCodeMgr, int isRegister, int isPrintBarCode, int dispOrder, int visible, string goodsMemo, DateTime? dataUpdateTime, DateTime dataAddTime, long? prodID, string netGoodsNo, string verificationMemo)
        {
            this._labID = labID;
            this._goodsNo = goodsNo;
            this._cName = cName;
            this._sName = sName;
            this._eName = eName;
            this._shortCode = shortCode;
            this._pinYinZiTou = pinYinZiTou;
            this._goodsClass = goodsClass;
            this._goodsClassType = goodsClassType;
            this._prodGoodsNo = prodGoodsNo;
            this._prodOrgName = prodOrgName;
            this._prodEara = prodEara;
            this._price = price;
            this._unitName = unitName;
            this._unitMemo = unitMemo;
            this._storageType = storageType;
            this._goodsDesc = goodsDesc;
            this._approveDocNo = approveDocNo;
            this._standard = standard;
            this._registNo = registNo;
            this._registDate = registDate;
            this._registNoInvalidDate = registNoInvalidDate;
            this._purpose = purpose;
            this._constitute = constitute;
            this._license = license;
            this._goodsPicture = goodsPicture;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._equipment = equipment;
            this._cenOrgConfirm = cenOrgConfirm;
            this._barCodeMgr = barCodeMgr;
            this._isRegister = isRegister;
            this._isPrintBarCode = isPrintBarCode;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._goodsMemo = goodsMemo;
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
            this._prodID = prodID;
            this._netGoodsNo = netGoodsNo;
            this._verificationMemo = verificationMemo;
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
        [DataDesc(CName = "开瓶后有效天数", ShortCode = "BOpenDays", Desc = "开瓶后有效天数", ContextType = SysDic.All, Length = 4)]
        public virtual int? BOpenDays
        {
            get { return _bOpenDays; }
            set { _bOpenDays = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "理论月用量", ShortCode = "MonthlyUsage", Desc = "理论月用量", ContextType = SysDic.All, Length = 8)]
        public virtual double? MonthlyUsage
        {
            get { return _monthlyUsage; }
            set { _monthlyUsage = value; }
        }
        /// <summary>
        /// 手工录入,提供在部门货品试剂选择时可以进行过滤
        /// </summary>
        [DataMember]
        [DataDesc(CName = "部门名称(手工录入)", ShortCode = "DeptName", Desc = "部门名称(手工录入)", ContextType = SysDic.All, Length = 100)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }
        [DataMember]
        [DataDesc(CName = "物资接口对照码", ShortCode = "MatchCode", Desc = "物资接口对照码", ContextType = SysDic.All, Length = 100)]
        public virtual string MatchCode
        {
            get { return _matchCode; }
            set { _matchCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "ReaCompanyName", Desc = "供应商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompanyName
        {
            get { return _reaCompanyName; }
            set { _reaCompanyName = value; }
        }
        [DataMember]
        [DataDesc(CName = "厂商货品编号", ShortCode = "ProdGoodsNo", Desc = "厂商货品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set { _prodGoodsNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "机构货品(内部)编号", ShortCode = "ReaGoodsNo", Desc = "机构货品(内部)编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo
        {
            get { return _reaGoodsNo; }
            set { _reaGoodsNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "货品平台编号", ShortCode = "GoodsNo", Desc = "货品平台编号", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsNo
        {
            get { return _goodsNo; }
            set { _goodsNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "中文名", ShortCode = "CName", Desc = "中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "中文简称", ShortCode = "SName", Desc = "中文简称", ContextType = SysDic.All, Length = 200)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "英文名", ShortCode = "EName", Desc = "英文名", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
        {
            get { return _eName; }
            set { _eName = value; }
        }

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "ShortCode", Desc = "代码", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set { _shortCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
        }

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

        [DataMember]
        [DataDesc(CName = "生成厂家", ShortCode = "ProdOrgName", Desc = "生成厂家", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName
        {
            get { return _prodOrgName; }
            set { _prodOrgName = value; }
        }

        [DataMember]
        [DataDesc(CName = "产地", ShortCode = "ProdEara", Desc = "产地", ContextType = SysDic.All, Length = 50)]
        public virtual string ProdEara
        {
            get { return _prodEara; }
            set { _prodEara = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出厂单价", ShortCode = "Price", Desc = "出厂单价", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "UnitName", Desc = "单位", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitName
        {
            get { return _unitName; }
            set { _unitName = value; }
        }

        [DataMember]
        [DataDesc(CName = "单位描述（规格）", ShortCode = "UnitMemo", Desc = "单位描述（规格）", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "货品描述", ShortCode = "GoodsDesc", Desc = "货品描述", ContextType = SysDic.All, Length = 500)]
        public virtual string GoodsDesc
        {
            get { return _goodsDesc; }
            set { _goodsDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "批准文号", ShortCode = "ApproveDocNo", Desc = "批准文号", ContextType = SysDic.All, Length = 200)]
        public virtual string ApproveDocNo
        {
            get { return _approveDocNo; }
            set { _approveDocNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "国标", ShortCode = "Standard", Desc = "国标", ContextType = SysDic.All, Length = 200)]
        public virtual string Standard
        {
            get { return _standard; }
            set { _standard = value; }
        }

        [DataMember]
        [DataDesc(CName = "注册号", ShortCode = "RegistNo", Desc = "注册号", ContextType = SysDic.All, Length = 200)]
        public virtual string RegistNo
        {
            get { return _registNo; }
            set { _registNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册日期", ShortCode = "RegistDate", Desc = "注册日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegistDate
        {
            get { return _registDate; }
            set { _registDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册证有效期", ShortCode = "RegistNoInvalidDate", Desc = "注册证有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegistNoInvalidDate
        {
            get { return _registNoInvalidDate; }
            set { _registNoInvalidDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "用途", ShortCode = "Purpose", Desc = "用途", ContextType = SysDic.All, Length = 1000)]
        public virtual string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }

        [DataMember]
        [DataDesc(CName = "结构组成", ShortCode = "Constitute", Desc = "结构组成", ContextType = SysDic.Text, Length = 1000)]
        public virtual string Constitute
        {
            get { return _constitute; }
            set { _constitute = value; }
        }

        [DataMember]
        [DataDesc(CName = "证书内容", ShortCode = "License", Desc = "证书内容", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] License
        {
            get { return _license; }
            set { _license = value; }
        }

        [DataMember]
        [DataDesc(CName = "外观照片", ShortCode = "GoodsPicture", Desc = "外观照片", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] GoodsPicture
        {
            get { return _goodsPicture; }
            set { _goodsPicture = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set { _zX1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set { _zX2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set { _zX3 = value; }
        }

        [DataMember]
        [DataDesc(CName = "支持的实验室仪器型号", ShortCode = "Equipment", Desc = "支持的实验室仪器型号", ContextType = SysDic.All, Length = 100)]
        public virtual string Equipment
        {
            get { return _equipment; }
            set { _equipment = value; }
        }

        [DataMember]
        [DataDesc(CName = "本机构确认", ShortCode = "CenOrgConfirm", Desc = "本机构确认", ContextType = SysDic.All, Length = 4)]
        public virtual int CenOrgConfirm
        {
            get { return _cenOrgConfirm; }
            set { _cenOrgConfirm = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否盒条码", ShortCode = "BarCodeMgr", Desc = "是否盒条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeMgr
        {
            get { return _barCodeMgr; }
            set { _barCodeMgr = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否有注册证", ShortCode = "IsRegister", Desc = "是否有注册证", ContextType = SysDic.All, Length = 4)]
        public virtual int IsRegister
        {
            get { return _isRegister; }
            set { _isRegister = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否打印条码", ShortCode = "IsPrintBarCode", Desc = "是否打印条码", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPrintBarCode
        {
            get { return _isPrintBarCode; }
            set { _isPrintBarCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "GoodsMemo", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string GoodsMemo
        {
            get { return _goodsMemo; }
            set { _goodsMemo = value; }
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
        [DataDesc(CName = "厂商", ShortCode = "ProdID", Desc = "机构表")]
        public virtual long? ProdID
        {
            get { return _prodID; }
            set { _prodID = value; }
        }

        //      [DataMember]
        //      [DataDesc(CName = "订货信息表", ShortCode = "ReaOrderGoodsList", Desc = "订货信息表")]
        //public virtual IList<ReaGoodsOrgLink> ReaOrderGoodsList
        //{
        //	get
        //	{
        //		if (_reaOrderGoodsList==null)
        //		{
        //			_reaOrderGoodsList = new List<ReaGoodsOrgLink>();
        //		}
        //		return _reaOrderGoodsList;
        //	}
        //	set { _reaOrderGoodsList = value; }
        //}        

        [DataMember]
        [DataDesc(CName = "货品相似码", ShortCode = "LinkGroupCode", Desc = "货品相似码", ContextType = SysDic.All, Length = 16)]
        public virtual string LinkGroupCode
        {
            get { return _LinkGroupCode; }
            set { _LinkGroupCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "换算比率", ShortCode = "GonvertQty", Desc = "换算比率", ContextType = SysDic.All, Length = 16)]
        public virtual Double GonvertQty
        {
            get { return _GonvertQty; }
            set { _GonvertQty = value; }
        }

        [DataMember]
        [DataDesc(CName = "适用机型", ShortCode = "SuitableType", Desc = "适用机型", ContextType = SysDic.All, Length = 100)]
        public virtual string SuitableType
        {
            get { return _suitableType; }
            set
            {
                _suitableType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "测试数", ShortCode = "TestCount", Desc = "测试数", ContextType = SysDic.All, Length = 4)]
        public virtual int TestCount
        {
            get { return _testCount; }
            set { _testCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "产品序号", ShortCode = "GoodsSort", Desc = "产品序号", ContextType = SysDic.All, Length = 4)]
        public virtual int GoodsSort
        {
            get { return _GoodsSort; }
            set { _GoodsSort = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "是否最小单位", ShortCode = "IsMinUnit", Desc = "是否最小单位", ContextType = SysDic.All, Length = 4)]
        public virtual bool IsMinUnit
        {
            get { return _IsMinUnit; }
            set { _IsMinUnit = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存上限", ShortCode = "StoreUpper", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? StoreUpper
        {
            get { return _storeUpper; }
            set { _storeUpper = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存下限", ShortCode = "StoreLower", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? StoreLower
        {
            get { return _storeLower; }
            set { _storeLower = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期报警天数", ShortCode = "BeforeWarningDay", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? BeforeWarningDay
        {
            get { return _beforeWarningDay; }
            set { _beforeWarningDay = value; }
        }
        [DataMember]
        [DataDesc(CName = "是否需要性能验证", ShortCode = "IsNeedPerformanceTest", Desc = "是否需要性能验证", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsNeedPerformanceTest
        {
            get { return _isNeedPerformanceTest; }
            set { _isNeedPerformanceTest = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "包装单位ID", ShortCode = "GoodsUnitID", Desc = "包装单位ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsUnitID
        {
            get { return _goodsUnitID; }
            set { _goodsUnitID = value; }
        }

        /// <summary>
        /// 用于存放挂网流水号，或其他一些特殊编码
        /// </summary>
        [DataMember]
        [DataDesc(CName = "挂网流水号", ShortCode = "NetGoodsNo", Desc = "挂网流水号", ContextType = SysDic.All, Length = 100)]
        public virtual string NetGoodsNo
        {
            get { return _netGoodsNo; }
            set { _netGoodsNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "验证说明", ShortCode = "VerificationMemo", Desc = "验证说明", ContextType = SysDic.All, Length = 5000)]
        public virtual string VerificationMemo
        {
            get { return _verificationMemo; }
            set { _verificationMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否医疗器械", ShortCode = "IsMed", Desc = "是否医疗器械", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsMed
        {
            get { return _isMed; }
            set { _isMed = value; }
        }

        [DataMember]
        [DataDesc(CName = "型号", ShortCode = "InvType", Desc = "型号", ContextType = SysDic.All, Length = 1)]
        public virtual string InvType
        {
            get { return _invType; }
            set { _invType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "第三方接口同步货品时间戳", ShortCode = "TS", Desc = "第三方接口同步货品时间戳", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TS
        {
            get { return _ts; }
            set { _ts = value; }
        }

        #endregion

        #region 自定义属性
        [DataMember]
        [DataDesc(CName = "供应商编码", ShortCode = "ReaCompCode", Desc = "供应商编码", ContextType = SysDic.All, Length = 50)]
        public virtual string ReaCompCode { get; set; }

        [DataMember]
        [DataDesc(CName = "供货商货品关系备注", ShortCode = "ZDYMemo", Desc = "同步供货商货品关系接口时使用", ContextType = SysDic.All, Length = 500)]
        public virtual string ZDYMemo { get; set; }

        /// <summary>
        /// 厂商编码，接口同步货品时保存厂商信息（品牌）
        /// </summary>
        public virtual string ProdOrgNo { get; set; }

        /// <summary>
        /// 货品当前库存数量描述(订单管理-订单申请-模板选择，使用)
        /// </summary>
        public virtual string CurrentQty { get; set; }
        /// <summary>
        /// 货品当前库存数量(订单管理-订单申请-模板选择，使用)
        /// </summary>
        public virtual double? GoodsQty { get; set; }

        /// <summary>
        /// 平均使用量（智能采购）
        /// </summary>
        public virtual int AvgUsedQty { get; set; }

        /// <summary>
        /// 建议采购量（智能采购）
        /// </summary>
        public virtual int SuggestPurchaseQty { get; set; }

        #endregion

    }
    #endregion
}