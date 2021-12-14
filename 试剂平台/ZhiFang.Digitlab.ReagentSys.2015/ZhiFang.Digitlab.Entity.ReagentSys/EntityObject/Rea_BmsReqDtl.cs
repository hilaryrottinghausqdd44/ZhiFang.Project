using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
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
        protected double _goodsQty;
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
        protected long? _orderGoodsID;
        protected long? _OrderStatus;
        protected string _OrderCheckMemo;


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

        #region Public Properties


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
        [DataDesc(CName = "购进数量", ShortCode = "GoodsQty", Desc = "购进数量", ContextType = SysDic.All, Length = 8)]
        public virtual double GoodsQty
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


        //[DataMember]
        //[DataDesc(CName = "平台货品表", ShortCode = "ReaGoods", Desc = "平台货品表")]
        //public virtual ReaGoods ReaGoods
        //{
        //    get { return _reaGoods; }
        //    set { _reaGoods = value; }
        //}       

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
        [DataDesc(CName = "包装单位", ShortCode = "GoodsUnit", Desc = "包装单位", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "货品机构关系ID", ShortCode = "OrderGoodsID", Desc = "货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderGoodsID
        {
            get { return _orderGoodsID; }
            set { _orderGoodsID = value; }
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
    }
    #endregion
}