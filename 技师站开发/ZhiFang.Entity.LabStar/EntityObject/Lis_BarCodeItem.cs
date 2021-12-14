using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisBarCodeItem

    /// <summary>
    /// LisBarCodeItem object for NHibernate mapped table 'Lis_BarCodeItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "采样样本单项目", ClassCName = "LisBarCodeItem", ShortCode = "LisBarCodeItem", Desc = "采样样本单项目")]
    public class LisBarCodeItem : BaseEntity
    {
        #region Member Variables

        protected DateTime? _partitionDate;
        protected long? _barCodesItemID;
        protected long? _ordersItemID;
        protected int _barCodeItemFlag;
        protected string _reportDateDesc;
        protected int _receiveFlag;
        protected DateTime? _collectTime;
        protected DateTime? _inceptTime;
        protected DateTime? _checkTime;
        protected int _itemDispenseFlag;
        protected int _isItemSplitReceive;
        protected long? _itemStatusID;
        protected DateTime? _dataUpdateTime;
        protected LisBarCodeForm _lisBarCodeForm;
        protected LisOrderItem _lisOrderItem;


        #endregion

        #region Constructors

        public LisBarCodeItem() { }

        public LisBarCodeItem(DateTime partitionDate, long barCodesItemID, long ordersItemID, int barCodeItemFlag, string reportDateDesc, int receiveFlag, DateTime collectTime, DateTime inceptTime, DateTime checkTime, int itemDispenseFlag, int isItemSplitReceive, long itemStatusID, DateTime dataAddTime, DateTime dataUpdateTime, long labID, byte[] dataTimeStamp, LisBarCodeForm lisBarCodeForm, LisOrderItem lisOrderItem)
        {
            this._partitionDate = partitionDate;
            this._barCodesItemID = barCodesItemID;
            this._ordersItemID = ordersItemID;
            this._barCodeItemFlag = barCodeItemFlag;
            this._reportDateDesc = reportDateDesc;
            this._receiveFlag = receiveFlag;
            this._collectTime = collectTime;
            this._inceptTime = inceptTime;
            this._checkTime = checkTime;
            this._itemDispenseFlag = itemDispenseFlag;
            this._isItemSplitReceive = isItemSplitReceive;
            this._itemStatusID = itemStatusID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._labID = labID;
            this._dataTimeStamp = dataTimeStamp;
            this._lisBarCodeForm = lisBarCodeForm;
            this._lisOrderItem = lisOrderItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "分区日期", ShortCode = "PartitionDate", Desc = "分区日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PartitionDate
        {
            get { return _partitionDate; }
            set { _partitionDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采样项目ID", ShortCode = "BarCodesItemID", Desc = "采样项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BarCodesItemID
        {
            get { return _barCodesItemID; }
            set { _barCodesItemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱项目ID", ShortCode = "OrdersItemID", Desc = "医嘱项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrdersItemID
        {
            get { return _ordersItemID; }
            set { _ordersItemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "条码项目标志", ShortCode = "BarCodeItemFlag", Desc = "条码项目标志", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeItemFlag
        {
            get { return _barCodeItemFlag; }
            set { _barCodeItemFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "取单时间描述", ShortCode = "ReportDateDesc", Desc = "取单时间描述", ContextType = SysDic.All, Length = 8)]
        public virtual string ReportDateDesc
        {
            get { return _reportDateDesc; }
            set { _reportDateDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "核收标志", ShortCode = "ReceiveFlag", Desc = "核收标志", ContextType = SysDic.All, Length = 4)]
        public virtual int ReceiveFlag
        {
            get { return _receiveFlag; }
            set { _receiveFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采样时间", ShortCode = "CollectTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CollectTime
        {
            get { return _collectTime; }
            set { _collectTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签收时间", ShortCode = "InceptTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InceptTime
        {
            get { return _inceptTime; }
            set { _inceptTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目分发标志", ShortCode = "ItemDispenseFlag", Desc = "项目分发标志", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemDispenseFlag
        {
            get { return _itemDispenseFlag; }
            set { _itemDispenseFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否组合项目拆分子项核收", ShortCode = "IsItemSplitReceive", Desc = "是否组合项目拆分子项核收", ContextType = SysDic.All, Length = 4)]
        public virtual int IsItemSplitReceive
        {
            get { return _isItemSplitReceive; }
            set { _isItemSplitReceive = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "状态标志", ShortCode = "ItemStatusID", Desc = "状态标志", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemStatusID
        {
            get { return _itemStatusID; }
            set { _itemStatusID = value; }
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
        [DataDesc(CName = "样本单", ShortCode = "LisBarCodeForm", Desc = "样本单")]
        public virtual LisBarCodeForm LisBarCodeForm
        {
            get { return _lisBarCodeForm; }
            set { _lisBarCodeForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱项目", ShortCode = "LisOrderItem", Desc = "医嘱项目")]
        public virtual LisOrderItem LisOrderItem
        {
            get { return _lisOrderItem; }
            set { _lisOrderItem = value; }
        }


        #endregion
    }
    #endregion
}