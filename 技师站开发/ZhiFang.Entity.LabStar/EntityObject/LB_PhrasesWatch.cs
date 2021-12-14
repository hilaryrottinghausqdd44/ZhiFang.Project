using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBPhrasesWatch

    /// <summary>
    /// LBPhrasesWatch object for NHibernate mapped table 'LB_PhrasesWatch'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBPhrasesWatch", ShortCode = "LBPhrasesWatch", Desc = "")]
    public class LBPhrasesWatch : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _sName;
        protected string _sCode;
        protected int _phrasesType;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBPhrasesWatch() { }

        public LBPhrasesWatch(string cName, string sName, string sCode, int phrasesType, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._cName = cName;
            this._sName = sName;
            this._sCode = sCode;
            this._phrasesType = phrasesType;
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
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhrasesType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PhrasesType
        {
            get { return _phrasesType; }
            set { _phrasesType = value; }
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