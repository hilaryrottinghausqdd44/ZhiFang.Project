using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBDicCodeLink

    /// <summary>
    /// LBDicCodeLink object for NHibernate mapped table 'LB_DicCodeLink'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBDicCodeLink", ShortCode = "LBDicCodeLink", Desc = "")]
    public class LBDicCodeLink : BaseEntity
    {
        #region Member Variables

        protected string _dicTypeCode;
        protected string _dicTypeName;
        protected string _linkSystemCode;
        protected string _linkSystemName;
        protected long? _linkSystemID;
        protected string _dicDataID;
        protected string _dicDataCode;
        protected string _dicDataName;
        protected string _linkDicDataCode;
        protected string _linkDicDataName;
        protected long? _transTypeID;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;

        #endregion

        #region Constructors

        public LBDicCodeLink() { }

        public LBDicCodeLink(long labID, string dicTypeCode, string dicTypeName, string linkSystemCode, string linkSystemName, long linkSystemID, string dicDataID, string dicDataCode, string dicDataName, string linkDicDataCode, string linkDicDataName, long transTypeID, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._dicTypeCode = dicTypeCode;
            this._dicTypeName = dicTypeName;
            this._linkSystemCode = linkSystemCode;
            this._linkSystemName = linkSystemName;
            this._linkSystemID = linkSystemID;
            this._dicDataID = dicDataID;
            this._dicDataCode = dicDataCode;
            this._dicDataName = dicDataName;
            this._linkDicDataCode = linkDicDataCode;
            this._linkDicDataName = linkDicDataName;
            this._transTypeID = transTypeID;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "DicTypeCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string DicTypeCode
        {
            get { return _dicTypeCode; }
            set { _dicTypeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DicTypeName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string DicTypeName
        {
            get { return _dicTypeName; }
            set { _dicTypeName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkSystemCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LinkSystemCode
        {
            get { return _linkSystemCode; }
            set { _linkSystemCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkSystemName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LinkSystemName
        {
            get { return _linkSystemName; }
            set { _linkSystemName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LinkSystemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? LinkSystemID
        {
            get { return _linkSystemID; }
            set { _linkSystemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DicDataID", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string DicDataID
        {
            get { return _dicDataID; }
            set { _dicDataID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DicDataCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string DicDataCode
        {
            get { return _dicDataCode; }
            set { _dicDataCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DicDataName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string DicDataName
        {
            get { return _dicDataName; }
            set { _dicDataName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkDicDataCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LinkDicDataCode
        {
            get { return _linkDicDataCode; }
            set { _linkDicDataCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkDicDataName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LinkDicDataName
        {
            get { return _linkDicDataName; }
            set { _linkDicDataName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TransTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? TransTypeID
        {
            get { return _transTypeID; }
            set { _transTypeID = value; }
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