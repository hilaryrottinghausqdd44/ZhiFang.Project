using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsReqDtl

    /// <summary>
    /// ReaBmsReqDtl object for NHibernate mapped table 'Rea_BmsReqDtl'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "申请明细表", ClassCName = "ReaBmsReqDtl", ShortCode = "ReaBmsReqDtl", Desc = "申请明细表")]
    public class ReaBmsReqDtl : BaseEntity
    {
        #region Member Variables

        protected string _reqDtlNo;
        protected string _reqDocNo;
        protected string _goodsCName;
        protected string _goodsUnit;
        protected double? _goodsQty;
        protected int _orderFlag;
        protected string _orderDtlNo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _orgName;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;
        protected ReaBmsReqDoc _reaBmsReqDoc;
        protected ReaCenOrg _reaCenOrg;
        protected long? _goodsID;
        protected long? _goodsUnitID;
        protected long? _orderDtlID;
        protected long? _compGoodsLinkID;
        protected long? _OrderStatus;
        protected string _OrderCheckMemo;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        protected double? _reqGoodsQty;

        protected long? _prodID;
        protected string _prodOrgName;
        protected string _unitMemo;
        protected DateTime? _arrivalTime;
        protected double? _expectedStock;
        protected double? _currentQty;
        protected double? _monthlyUsage;
        protected double? _lastMonthlyUsage;
        protected double? _price;
        protected double? _sumTotal;
        #endregion

        #region Constructors

        public ReaBmsReqDtl() { }

        public ReaBmsReqDtl(long labID, string reqDtlNo, string reqDocNo, string goodsCName, string goodsUnit, double goodsQty, int orderFlag, string orderDtlNo, string zX1, string zX2, string zX3, string orgName, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ReaBmsReqDoc reaBmsReqDoc, ReaCenOrg reaCenOrg, ReaGoodsUnit reaGoodsUnit, ReaGoods reaGoods)
        {
            this._labID = labID;
            this._reqDtlNo = reqDtlNo;
            this._reqDocNo = reqDocNo;
            this._goodsCName = goodsCName;
            this._goodsUnit = goodsUnit;
            this._goodsQty = goodsQty;
            this._orderFlag = orderFlag;
            this._orderDtlNo = orderDtlNo;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._orgName = orgName;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._reaBmsReqDoc = reaBmsReqDoc;
            this._reaCenOrg = reaCenOrg;
        }

        #endregion
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
        [DataDesc(CName = "到货时间", ShortCode = "ArrivalTime", Desc = "到货时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ArrivalTime
        {
            get { return _arrivalTime; }
            set { _arrivalTime = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预期库存量", ShortCode = "ExpectedStock", Desc = "预期库存量", ContextType = SysDic.All, Length = 8)]
        public virtual double? ExpectedStock
        {
            get { return _expectedStock; }
            set { _expectedStock = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "现有库存量", ShortCode = "CurrentQty", Desc = "现有库存量", ContextType = SysDic.All, Length = 8)]
        public virtual double? CurrentQty
        {
            get { return _currentQty; }
            set { _currentQty = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "理论月用量", ShortCode = "MonthlyUsage", Desc = "理论月用量", ContextType = SysDic.All, Length = 8)]
        public virtual double? MonthlyUsage
        {
            get { return _monthlyUsage; }
            set { _monthlyUsage = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上月月用量", ShortCode = "LastMonthlyUsage", Desc = "上月月用量", ContextType = SysDic.All, Length = 8)]
        public virtual double? LastMonthlyUsage
        {
            get { return _lastMonthlyUsage; }
            set { _lastMonthlyUsage = value; }
        }

        #region Public Properties
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ProdID", ShortCode = "ProdID", Desc = "ProdID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProdID
        {
            get { return _prodID; }
            set { _prodID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请购进数量", ShortCode = "ReqGoodsQty", Desc = "申请购进数量", ContextType = SysDic.All, Length = 8)]
        public virtual double? ReqGoodsQty
        {
            get { return _reqGoodsQty; }
            set { _reqGoodsQty = value; }
        }
        [DataMember]
        [DataDesc(CName = "机构货品(内部)编号", ShortCode = "ReaGoodsNo", Desc = "机构货品(内部)编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo
        {
            get { return _reaGoodsNo; }
            set { _reaGoodsNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商货品编号", ShortCode = "CenOrgGoodsNo", Desc = "供应商货品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string CenOrgGoodsNo
        {
            get { return _cenOrgGoodsNo; }
            set { _cenOrgGoodsNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "申请明细单号", ShortCode = "ReqDtlNo", Desc = "申请明细单号", ContextType = SysDic.All, Length = 20)]
        public virtual string ReqDtlNo
        {
            get { return _reqDtlNo; }
            set { _reqDtlNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "申请总单号", ShortCode = "ReqDocNo", Desc = "申请总单号", ContextType = SysDic.All, Length = 20)]
        public virtual string ReqDocNo
        {
            get { return _reqDocNo; }
            set { _reqDocNo = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批购进数量", ShortCode = "GoodsQty", Desc = "审批购进数量", ContextType = SysDic.All, Length = 8)]
        public virtual double? GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }

        [DataMember]
        [DataDesc(CName = "生成订单标志", ShortCode = "OrderFlag", Desc = "生成订单标志", ContextType = SysDic.All, Length = 4)]
        public virtual int OrderFlag
        {
            get { return _orderFlag; }
            set { _orderFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "订单明细单号", ShortCode = "OrderDtlNo", Desc = "订单明细单号", ContextType = SysDic.All, Length = 20)]
        public virtual string OrderDtlNo
        {
            get { return _orderDtlNo; }
            set { _orderDtlNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set { _zX1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set { _zX2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set { _zX3 = value; }
        }

        [DataMember]
        [DataDesc(CName = "本地供应商名称", ShortCode = "OrgName", Desc = "本地供应商名称", ContextType = SysDic.All, Length = 100)]
        public virtual string OrgName
        {
            get { return _orgName; }
            set { _orgName = value; }
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
            set { _memo = value; }
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
            set { _createrName = value; }
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
        [DataDesc(CName = "申请总单表", ShortCode = "ReaBmsReqDoc", Desc = "申请总单表")]
        public virtual ReaBmsReqDoc ReaBmsReqDoc
        {
            get { return _reaBmsReqDoc; }
            set { _reaBmsReqDoc = value; }
        }

        [DataMember]
        [DataDesc(CName = "货品所属本地供应商", ShortCode = "ReaCenOrg", Desc = "货品所属本地供应商")]
        public virtual ReaCenOrg ReaCenOrg
        {
            get { return _reaCenOrg; }
            set { _reaCenOrg = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地货品ID", ShortCode = "GoodsID", Desc = "本地货品ID", ContextType = SysDic.All, Length = 200)]
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
            set { _goodsCName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品包装单位ID", ShortCode = "GoodsUnitID", Desc = "货品包装单位ID", ContextType = SysDic.All, Length = 8)]
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
            set { _goodsUnit = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "平台订货明细ID", ShortCode = "OrderDtlID", Desc = "平台订货明细ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderDtlID
        {
            get { return _orderDtlID; }
            set { _orderDtlID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订单状态", ShortCode = "OrderStatus", Desc = "订单状态", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderStatus
        {
            get { return _OrderStatus; }
            set { _OrderStatus = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订单审核意见", ShortCode = "OrderCheckMemo", Desc = "订单审核意见", ContextType = SysDic.All, Length = 8)]
        public virtual string OrderCheckMemo
        {
            get { return _OrderCheckMemo; }
            set { _OrderCheckMemo = value; }
        }

        #endregion

        #region 自定义属性
        protected string _reaCompanyName;
        [DataMember]
        [DataDesc(CName = "供货商名称", ShortCode = "ReaCompanyName", Desc = "供货商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompanyName
        {
            get { return _reaCompanyName; }
            set { _reaCompanyName = value; }
        }

        protected string _reaCompCode;
        [DataMember]
        [DataDesc(CName = "供货商机构码", ShortCode = "ReaCompCode", Desc = "供货商机构码", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompCode
        {
            get { return _reaCompCode; }
            set { _reaCompCode = value; }
        }
        
        /// <summary>
        /// 平均使用量（智能采购）
        /// </summary>
        public virtual int AvgUsedQty { get; set; }

        /// <summary>
        /// 建议采购量（智能采购）
        /// </summary>
        public virtual int SuggestPurchaseQty { get; set; }

        /// <summary>
        /// 货品简称
        /// </summary>
        public virtual string GoodsSName { get; set; }

        /// <summary>
        /// 货品英文名称
        /// </summary>
        public virtual string GoodsEName { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual ReaGoods ReaGoods { get; set; }

        public ReaBmsReqDtl(ReaBmsReqDtl reabmsreqdtl, ReaBmsReqDoc reabmsreqdoc, ReaCenOrg reacenorg, ReaGoods reagoods)
        {
            this._id = reabmsreqdtl.Id;
            this._labID = reabmsreqdtl.LabID;
            this._dataAddTime = reabmsreqdtl.DataAddTime;
            this._dataUpdateTime = reabmsreqdtl.DataUpdateTime;
            this._dataTimeStamp = reabmsreqdtl.DataTimeStamp;
            this._price = reabmsreqdtl.Price;
            this._sumTotal = reabmsreqdtl.SumTotal;
            this._arrivalTime = reabmsreqdtl.ArrivalTime;
            this._expectedStock = reabmsreqdtl.ExpectedStock;
            this._currentQty = reabmsreqdtl.CurrentQty;
            this._monthlyUsage = reabmsreqdtl.MonthlyUsage;
            this._lastMonthlyUsage = reabmsreqdtl.LastMonthlyUsage;
            this._unitMemo = reabmsreqdtl.UnitMemo;
            this._prodID = reabmsreqdtl.ProdID;
            this._prodOrgName = reabmsreqdtl.ProdOrgName;
            this._reqGoodsQty = reabmsreqdtl.ReqGoodsQty;
            this._reaGoodsNo = reabmsreqdtl.ReaGoodsNo;
            this._cenOrgGoodsNo = reabmsreqdtl.CenOrgGoodsNo;
            this._reqDtlNo = reabmsreqdtl.ReqDtlNo;
            this._reqDocNo = reabmsreqdtl.ReqDocNo;
            this._goodsQty = reabmsreqdtl.GoodsQty;
            this._orderFlag = reabmsreqdtl.OrderFlag;
            this._orderDtlNo = reabmsreqdtl.OrderDtlNo;
            this._zX1 = reabmsreqdtl.ZX1;
            this._zX2 = reabmsreqdtl.ZX2;
            this._zX3 = reabmsreqdtl.ZX3;
            this._orgName = reabmsreqdtl.OrgName;
            this._dispOrder = reabmsreqdtl.DispOrder;
            this._memo = reabmsreqdtl.Memo;
            this._visible = reabmsreqdtl.Visible;
            this._createrID = reabmsreqdtl.CreaterID;
            this._createrName = reabmsreqdtl.CreaterName;
            this._goodsID = reabmsreqdtl.GoodsID;
            this._goodsCName = reabmsreqdtl.GoodsCName;
            this._goodsUnitID = reabmsreqdtl.GoodsUnitID;
            this._goodsUnit = reabmsreqdtl.GoodsUnit;
            this._orderDtlID = reabmsreqdtl.OrderDtlID;
            this._compGoodsLinkID = reabmsreqdtl.CompGoodsLinkID;
            this._OrderStatus = reabmsreqdtl.OrderStatus;
            this._OrderCheckMemo = reabmsreqdtl.OrderCheckMemo;

            this._reaBmsReqDoc = reabmsreqdoc;
            this._reaCenOrg = reacenorg;

            this.ReaGoods = reagoods;
            this.GoodsSName = reagoods.SName;
            this.GoodsEName = reagoods.EName;
        }

        #endregion
    }
    #endregion
}