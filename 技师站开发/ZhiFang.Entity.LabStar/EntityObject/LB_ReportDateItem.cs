using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBReportDateItem

    /// <summary>
    /// LBReportDateItem object for NHibernate mapped table 'LB_ReportDateItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "取单分类项目明细", ClassCName = "LBReportDateItem", ShortCode = "LBReportDateItem", Desc = "取单分类项目明细")]
    public class LBReportDateItem : BaseEntity
    {
        #region Member Variables

        protected DateTime? _dataUpdateTime;
        protected LBItem _lBItem;
        protected LBReportDate _lBReportDate;


        #endregion

        #region Constructors

        public LBReportDateItem() { }

        public LBReportDateItem(long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem lBItem, LBReportDate lBReportDate)
        {
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBItem = lBItem;
            this._lBReportDate = lBReportDate;
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
        [DataDesc(CName = "取单时间分类", ShortCode = "LBReportDate", Desc = "取单时间分类")]
        public virtual LBReportDate LBReportDate
        {
            get { return _lBReportDate; }
            set { _lBReportDate = value; }
        }


        #endregion
    }
    #endregion
}