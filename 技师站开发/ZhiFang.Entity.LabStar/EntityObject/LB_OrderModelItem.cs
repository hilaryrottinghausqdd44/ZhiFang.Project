using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBOrderModelItem

    /// <summary>
    /// LBOrderModelItem object for NHibernate mapped table 'LB_OrderModelItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "医嘱模板明细", ClassCName = "LBOrderModelItem", ShortCode = "LBOrderModelItem", Desc = "医嘱模板明细")]
    public class LBOrderModelItem : BaseEntity
    {
        #region Member Variables

        protected long _orderModelID;
        protected long _itemID;
        protected string _itemName;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBOrderModelItem() { }

        public LBOrderModelItem(long labID, long orderModelID, long itemID, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._orderModelID = orderModelID;
            this._itemID = itemID;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrderModelID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long OrderModelID
        {
            get { return _orderModelID; }
            set { _orderModelID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "", ShortCode = "ItemName", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        #endregion
    }
    #endregion
}