using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSectionHisComp

    /// <summary>
    /// LBSectionHisComp object for NHibernate mapped table 'LB_SectionHisComp'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBSectionHisComp", ShortCode = "LBSectionHisComp", Desc = "")]
    public class LBSectionHisComp : BaseEntity
    {
        #region Member Variables

        protected DateTime? _dataUpdateTime;
        protected LBSection _lBSection;
        protected LBSection _hisComp;


        #endregion

        #region Constructors

        public LBSectionHisComp() { }

        public LBSectionHisComp(long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBSection lBSection, LBSection hisComp)
        {
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBSection = lBSection;
            this._hisComp = hisComp;
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
        [DataDesc(CName = "", ShortCode = "LBSection", Desc = "")]
        public virtual LBSection LBSection
        {
            get { return _lBSection; }
            set { _lBSection = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisComp", Desc = "")]
        public virtual LBSection HisComp
        {
            get { return _hisComp; }
            set { _hisComp = value; }
        }


        #endregion
    }
    #endregion
}