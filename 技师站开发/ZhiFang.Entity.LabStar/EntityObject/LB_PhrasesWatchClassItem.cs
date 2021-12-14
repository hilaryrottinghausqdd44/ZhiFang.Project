using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBPhrasesWatchClassItem

    /// <summary>
    /// LBPhrasesWatchClassItem object for NHibernate mapped table 'LB_PhrasesWatchClassItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "拒收让步短语类型明细", ClassCName = "LBPhrasesWatchClassItem", ShortCode = "LBPhrasesWatchClassItem", Desc = "拒收让步短语类型明细")]
    public class LBPhrasesWatchClassItem : BaseEntity
    {
        #region Member Variables

        protected DateTime? _dataUpdateTime;
        protected LBPhrasesWatch _lBPhrasesWatch;
        protected LBPhrasesWatchClass _lBPhrasesWatchClass;


        #endregion

        #region Constructors

        public LBPhrasesWatchClassItem() { }

        public LBPhrasesWatchClassItem(long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBPhrasesWatch lBPhrasesWatch, LBPhrasesWatchClass lBPhrasesWatchClass)
        {
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBPhrasesWatch = lBPhrasesWatch;
            this._lBPhrasesWatchClass = lBPhrasesWatchClass;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBPhrasesWatch", Desc = "")]
        public virtual LBPhrasesWatch LBPhrasesWatch
        {
            get { return _lBPhrasesWatch; }
            set { _lBPhrasesWatch = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBPhrasesWatchClass", Desc = "")]
        public virtual LBPhrasesWatchClass LBPhrasesWatchClass
        {
            get { return _lBPhrasesWatchClass; }
            set { _lBPhrasesWatchClass = value; }
        }


        #endregion
    }
    #endregion
}