using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.SA
{
    #region PhrasesWatchClassItem

    /// <summary>
    /// PhrasesWatchClassItem object for NHibernate mapped table 'PhrasesWatchClassItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "PhrasesWatchClassItem", ShortCode = "PhrasesWatchClassItem", Desc = "")]
    public class PhrasesWatchClassItem : BaseEntity
    {
        #region Member Variables

        protected long _phrasesWatchClassID;
        protected long _refuseID;

        #endregion

        #region Constructors
        #region ¶àÖ÷¼ü
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        public PhrasesWatchClassItem() { }

        public PhrasesWatchClassItem(long phrasesWatchClassID, long refuseID)
        {
            this._phrasesWatchClassID = phrasesWatchClassID;
            this._refuseID = refuseID;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "PhrasesWatchClassID", ShortCode = "PhrasesWatchClassID", Desc = "PhrasesWatchClassID", ContextType = SysDic.All, Length = 8)]
        public virtual long PhrasesWatchClassID
        {
            get { return _phrasesWatchClassID; }
            set { _phrasesWatchClassID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "RefuseID", ShortCode = "RefuseID", Desc = "RefuseID", ContextType = SysDic.All, Length = 8)]
        public virtual long RefuseID
        {
            get { return _refuseID; }
            set { _refuseID = value; }
        }
        #endregion
    }
    #endregion
}