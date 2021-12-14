using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisOrderItem

    /// <summary>
    /// LisOrderItem object for NHibernate mapped table 'Lis_OrderItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "医嘱项目", ClassCName = "LisOrderItem", ShortCode = "LisOrderItem", Desc = "医嘱项目")]
    public class LisOrderItem : BaseEntity
    {
        #region Member Variables

        protected long? _ordersItemID;
        protected string _orderFormNo;
        protected DateTime? _orderDate;
        protected DateTime _partitionDate;
        protected int _orderItemExecFlag;
        protected long? _itemStatusID;
        protected int _isCancelled;
        protected int _isPriceItem;
        protected int _isCheckFee;
        protected double? _charge;
        protected string _removeHost;
        protected string _remover;
        protected DateTime? _removeTime;
        protected long? _sampleTypeID;
        protected string _collectPart;
        protected string _hisItemNo;
        protected string _hisItemName;
        protected string _hisSampleTypeNo;
        protected string _hisSampleTypeName;
        protected DateTime? _dataUpdateTime;
        protected long? _orderFormID;
        /*protected LisOrderForm _lisOrderForm;*/


        #endregion

        #region Constructors

        public LisOrderItem() { }

        public LisOrderItem(long ordersItemID, string orderFormNo, DateTime orderDate, DateTime partitionDate, int orderItemExecFlag, long itemStatusID, int isCancelled, int isPriceItem, int isCheckFee, double charge, string removeHost, string remover, DateTime removeTime, long sampleTypeID, string collectPart, string hisItemNo, string hisItemName, string hisSampleTypeNo, string hisSampleTypeName, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long OrderFormID)
        {
            this._ordersItemID = ordersItemID;
            this._orderFormNo = orderFormNo;
            this._orderDate = orderDate;
            this._partitionDate = partitionDate;
            this._orderItemExecFlag = orderItemExecFlag;
            this._itemStatusID = itemStatusID;
            this._isCancelled = isCancelled;
            this._isPriceItem = isPriceItem;
            this._isCheckFee = isCheckFee;
            this._charge = charge;
            this._removeHost = removeHost;
            this._remover = remover;
            this._removeTime = removeTime;
            this._sampleTypeID = sampleTypeID;
            this._collectPart = collectPart;
            this._hisItemNo = hisItemNo;
            this._hisItemName = hisItemName;
            this._hisSampleTypeNo = hisSampleTypeNo;
            this._hisSampleTypeName = hisSampleTypeName;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._orderFormID = OrderFormID;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱项目ID", ShortCode = "OrdersItemID", Desc = "医嘱项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrdersItemID
        {
            get { return _ordersItemID; }
            set { _ordersItemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱单号", ShortCode = "OrderFormNo", Desc = "医嘱单号", ContextType = SysDic.All, Length = 100)]
        public virtual string OrderFormNo
        {
            get { return _orderFormNo; }
            set { _orderFormNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱日期", ShortCode = "OrderDate", Desc = "医嘱日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "分区日期", ShortCode = "PartitionDate", Desc = "分区日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime PartitionDate
        {
            get { return _partitionDate; }
            set { _partitionDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱项目执行标志", ShortCode = "OrderItemExecFlag", Desc = "医嘱项目执行标志", ContextType = SysDic.All, Length = 4)]
        public virtual int OrderItemExecFlag
        {
            get { return _orderItemExecFlag; }
            set { _orderItemExecFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱项目状态ID", ShortCode = "ItemStatusID", Desc = "医嘱项目状态ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemStatusID
        {
            get { return _itemStatusID; }
            set { _itemStatusID = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已经作废", ShortCode = "IsCancelled", Desc = "是否已经作废", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCancelled
        {
            get { return _isCancelled; }
            set { _isCancelled = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否增补收费项目", ShortCode = "IsPriceItem", Desc = "是否增补收费项目", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPriceItem
        {
            get { return _isPriceItem; }
            set { _isPriceItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已经收费", ShortCode = "IsCheckFee", Desc = "是否已经收费", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCheckFee
        {
            get { return _isCheckFee; }
            set { _isCheckFee = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用金额", ShortCode = "Charge", Desc = "费用金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

        [DataMember]
        [DataDesc(CName = "退费站点", ShortCode = "RemoveHost", Desc = "退费站点", ContextType = SysDic.All, Length = 100)]
        public virtual string RemoveHost
        {
            get { return _removeHost; }
            set { _removeHost = value; }
        }

        [DataMember]
        [DataDesc(CName = "退费人", ShortCode = "Remover", Desc = "退费人", ContextType = SysDic.All, Length = 100)]
        public virtual string Remover
        {
            get { return _remover; }
            set { _remover = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费时间", ShortCode = "RemoveTime", Desc = "退费时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RemoveTime
        {
            get { return _removeTime; }
            set { _removeTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本类型ID", ShortCode = "SampleTypeID", Desc = "样本类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleTypeID
        {
            get { return _sampleTypeID; }
            set { _sampleTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样部位", ShortCode = "CollectPart", Desc = "采样部位", ContextType = SysDic.All, Length = 100)]
        public virtual string CollectPart
        {
            get { return _collectPart; }
            set { _collectPart = value; }
        }

        [DataMember]
        [DataDesc(CName = "HIS项目编号", ShortCode = "HisItemNo", Desc = "HIS项目编号", ContextType = SysDic.All, Length = 100)]
        public virtual string HisItemNo
        {
            get { return _hisItemNo; }
            set { _hisItemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "HIS项目名称", ShortCode = "HisItemName", Desc = "HIS项目名称", ContextType = SysDic.All, Length = 100)]
        public virtual string HisItemName
        {
            get { return _hisItemName; }
            set { _hisItemName = value; }
        }

        [DataMember]
        [DataDesc(CName = "His样本类型编号", ShortCode = "HisSampleTypeNo", Desc = "His样本类型编号", ContextType = SysDic.All, Length = 100)]
        public virtual string HisSampleTypeNo
        {
            get { return _hisSampleTypeNo; }
            set { _hisSampleTypeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His样本类型", ShortCode = "HisSampleTypeName", Desc = "His样本类型", ContextType = SysDic.All, Length = 100)]
        public virtual string HisSampleTypeName
        {
            get { return _hisSampleTypeName; }
            set { _hisSampleTypeName = value; }
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
        [DataDesc(CName = "医嘱单ID", ShortCode = "OrderFormID", Desc = "医嘱单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderFormID
        {
            get { return _orderFormID; }
            set { _orderFormID = value; }
        }

        /*[DataMember]
        [DataDesc(CName = "医嘱单", ShortCode = "LisOrderForm", Desc = "医嘱单")]
        public virtual LisOrderForm LisOrderForm
        {
            get { return _lisOrderForm; }
            set { _lisOrderForm = value; }
        }*/


        #endregion
    }
    #endregion
}