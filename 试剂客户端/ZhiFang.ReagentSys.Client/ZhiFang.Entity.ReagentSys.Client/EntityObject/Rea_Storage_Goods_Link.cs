using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaStorageGoodsLink

    /// <summary>
    /// ReaStorageGoodsLink object for NHibernate mapped table 'Rea_Storage_Goods_Link'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaStorageGoodsLink", ShortCode = "ReaStorageGoodsLink", Desc = "")]
    public class ReaStorageGoodsLink : BaseEntity
    {
        #region Member Variables

        protected long? _operType;
        protected long? _storageID;
        protected string _storageName;
        protected long? _placeID;
        protected string _placeName;
        protected long _goodsID;
        protected int _dispOrder;
        protected bool _visible;
        protected DateTime _dataUpdateTime;


        #endregion

        #region Constructors

        public ReaStorageGoodsLink() { }

        public ReaStorageGoodsLink(long labID, long operType, long storageID, string storageName, long placeID, string placeName, long goodsID, int dispOrder, bool visible, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._operType = operType;
            this._storageID = storageID;
            this._storageName = storageName;
            this._placeID = placeID;
            this._placeName = placeName;
            this._goodsID = goodsID;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperType", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperType
        {
            get { return _operType; }
            set { _operType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StorageID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PlaceID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? PlaceID
        {
            get { return _placeID; }
            set { _placeID = value; }
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
        [DataDesc(CName = "", ShortCode = "GoodsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion

        #region 自定义
        [DataMember]
        [DataDesc(CName = "库房信息", ShortCode = "ReaStorage", Desc = "库房信息")]
        public virtual ReaStorage ReaStorage { get; set; }
        [DataMember]
        [DataDesc(CName = "货架信息", ShortCode = "ReaPlace", Desc = "货架信息")]
        public virtual ReaPlace ReaPlace { get; set; }
        [DataMember]
        [DataDesc(CName = "机构货品", ShortCode = "ReaGoods", Desc = "机构货品")]
        public virtual ReaGoods ReaGoods { get; set; }
        private void _setReaStorageGoodsLink(ReaStorageGoodsLink reastoragegoodslink)
        {
            this._id = reastoragegoodslink.Id;
            this._labID = reastoragegoodslink.LabID;
            this._operType = reastoragegoodslink.OperType;
            this._storageID = reastoragegoodslink.StorageID;

            this._placeID = reastoragegoodslink.PlaceID;
            this._goodsID = reastoragegoodslink.GoodsID;
            this._dispOrder = reastoragegoodslink.DispOrder;
            this._visible = reastoragegoodslink.Visible;
            this._dataAddTime = reastoragegoodslink.DataAddTime;
            this._dataUpdateTime = reastoragegoodslink.DataUpdateTime;
            this._dataTimeStamp = reastoragegoodslink.DataTimeStamp;
        }
        public ReaStorageGoodsLink(ReaStorageGoodsLink reastoragegoodslink, ReaStorage reastorage, ReaPlace reaplace, ReaGoods reagoods)
        {
            _setReaStorageGoodsLink(reastoragegoodslink);
            this.ReaStorage = reastorage;
            this.ReaGoods = reagoods;
            if (reaplace != null)
            {
                this.ReaPlace = reaplace;
            }
        }
        public ReaStorageGoodsLink(ReaStorageGoodsLink reastoragegoodslink, ReaStorage reastorage, ReaGoods reagoods)
        {
            _setReaStorageGoodsLink(reastoragegoodslink);
            this.ReaStorage = reastorage;
            this.ReaGoods = reagoods;
            this.ReaPlace = null;
        }

        #endregion
    }
    #endregion
}