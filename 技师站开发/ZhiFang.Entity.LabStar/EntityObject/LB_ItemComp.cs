using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBItemComp

    /// <summary>
    /// LBItemComp object for NHibernate mapped table 'LB_ItemComp'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBItemComp", ShortCode = "LBItemComp", Desc = "")]
    public class LBItemComp : BaseEntity
    {
        #region Member Variables

        protected string _compType;
        protected string _compType2;
        protected string _lisItemID;
        protected string _sourceID;
        protected string _sourceCode;
        protected string _sourceCode2;
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected long _userID;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBItemComp() { }

        public LBItemComp(string compType, string compType2, string lisItemID, string sourceID, string sourceCode, string sourceCode2, string cName, string eName, string sName, string comment, bool isUse, int dispOrder, long userID, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._compType = compType;
            this._compType2 = compType2;
            this._lisItemID = lisItemID;
            this._sourceID = sourceID;
            this._sourceCode = sourceCode;
            this._sourceCode2 = sourceCode2;
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
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
        [DataDesc(CName = "", ShortCode = "CompType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CompType
        {
            get { return _compType; }
            set { _compType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompType2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CompType2
        {
            get { return _compType2; }
            set { _compType2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisItemID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LisItemID
        {
            get { return _lisItemID; }
            set { _lisItemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SourceID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SourceID
        {
            get { return _sourceID; }
            set { _sourceID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SourceCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SourceCode
        {
            get { return _sourceCode; }
            set { _sourceCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SourceCode2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SourceCode2
        {
            get { return _sourceCode2; }
            set { _sourceCode2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _eName; }
            set { _eName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
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