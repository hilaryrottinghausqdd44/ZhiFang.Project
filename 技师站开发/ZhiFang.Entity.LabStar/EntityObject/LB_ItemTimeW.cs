using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBItemTimeW

    /// <summary>
    /// LBItemTimeW object for NHibernate mapped table 'LB_ItemTimeW'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBItemTimeW", ShortCode = "LBItemTimeW", Desc = "")]
    public class LBItemTimeW : BaseEntity
    {
        #region Member Variables

        protected string _timeWType;
        protected long? _itemID;
        protected long? _sampleTypeID;
        protected long? _sickTypeID;
        protected long? _sectionID;
        protected int _timeWValue;
        protected string _timeWDesc;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected long _userID;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBItemTimeW() { }

        public LBItemTimeW(string timeWType, long itemID, long sampleTypeID, long sickTypeID, long sectionID, int timeWValue, string timeWDesc, string comment, bool isUse, int dispOrder, long userID, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._timeWType = timeWType;
            this._itemID = itemID;
            this._sampleTypeID = sampleTypeID;
            this._sickTypeID = sickTypeID;
            this._sectionID = sectionID;
            this._timeWValue = timeWValue;
            this._timeWDesc = timeWDesc;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._userID = userID;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "TimeWType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TimeWType
        {
            get { return _timeWType; }
            set { _timeWType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SampleTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleTypeID
        {
            get { return _sampleTypeID; }
            set { _sampleTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SickTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SickTypeID
        {
            get { return _sickTypeID; }
            set { _sickTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SectionID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SectionID
        {
            get { return _sectionID; }
            set { _sectionID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TimeWValue", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TimeWValue
        {
            get { return _timeWValue; }
            set { _timeWValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TimeWDesc", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TimeWDesc
        {
            get { return _timeWDesc; }
            set { _timeWDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
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
        [DataDesc(CName = "", ShortCode = "UserID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}