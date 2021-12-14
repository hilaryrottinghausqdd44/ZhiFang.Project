using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSampleItem

    /// <summary>
    /// LBSampleItem object for NHibernate mapped table 'LB_SampleItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "项目样本类型", ClassCName = "LBSampleItem", ShortCode = "LBSampleItem", Desc = "项目样本类型")]
    public class LBSampleItem : BaseEntity
    {
        #region Member Variables

        protected DateTime? _dataUpdateTime;
        protected LBItem _lBItem;
        protected LBSampleType _lBSampleType;


        #endregion

        #region Constructors

        public LBSampleItem() { }

        public LBSampleItem(DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp, LBItem lBItem, LBSampleType lBSampleType)
        {
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBItem = lBItem;
            this._lBSampleType = lBSampleType;
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
        [DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBItem
        {
            get { return _lBItem; }
            set { _lBItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBSampleType", Desc = "")]
        public virtual LBSampleType LBSampleType
        {
            get { return _lBSampleType; }
            set { _lBSampleType = value; }
        }


        #endregion
    }
    #endregion
}