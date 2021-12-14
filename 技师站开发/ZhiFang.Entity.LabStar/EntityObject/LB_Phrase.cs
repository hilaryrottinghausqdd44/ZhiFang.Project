using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBPhrase

    /// <summary>
    /// LBPhrase object for NHibernate mapped table 'LB_Phrase'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBPhrase", ShortCode = "LBPhrase", Desc = "")]
    public class LBPhrase : BaseEntity
    {
        #region Member Variables

        protected string _phraseType;
        protected string _typeName;
        protected string _typeCode;
        protected int _objectType;
        protected long? _objectID;
        protected long? _sampleTypeID;
        protected string _cName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBPhrase() { }

        public LBPhrase(string phraseType, string typeName, string typeCode, int objectType, long objectID, long sampleTypeID, string cName, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._phraseType = phraseType;
            this._typeName = typeName;
            this._typeCode = typeCode;
            this._objectType = objectType;
            this._objectID = objectID;
            this._sampleTypeID = sampleTypeID;
            this._cName = cName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhraseType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PhraseType
        {
            get { return _phraseType; }
            set { _phraseType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ObjectType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ObjectID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ObjectID
        {
            get { return _objectID; }
            set { _objectID = value; }
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
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
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