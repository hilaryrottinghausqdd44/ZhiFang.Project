using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBReportDateRule

    /// <summary>
    /// LBReportDateRule object for NHibernate mapped table 'LB_ReportDateRule'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "取单时间段规则", ClassCName = "LBReportDateRule", ShortCode = "LBReportDateRule", Desc = "取单时间段规则")]
    public class LBReportDateRule : BaseEntity
    {
        #region Member Variables

        protected long _reportDateDescID;
        protected int _beginWeekDay;
        protected int _endWeekDay;
        protected DateTime? _beginTime;
        protected DateTime? _endTime;
        protected string _getValue;
        protected DateTime? _dataUpdateTime;
        //protected LBReportDate _lBReportDate;


        #endregion

        #region Constructors

        public LBReportDateRule() { }

        public LBReportDateRule(long reportDateDescID,int beginWeekDay, int endWeekDay, DateTime beginTime, DateTime endTime, string getValue, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._reportDateDescID = reportDateDescID;
            this._beginWeekDay = beginWeekDay;
            this._endWeekDay = endWeekDay;
            this._beginTime = beginTime;
            this._endTime = endTime;
            this._getValue = getValue;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            //this._lBReportDate = lBReportDate;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Id", Desc = "", ContextType = SysDic.Number, Length = 8)]
        public virtual long ReportDateDescID
        {
            get { return _reportDateDescID; }
            set { _reportDateDescID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BeginWeekDay", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BeginWeekDay
        {
            get { return _beginWeekDay; }
            set { _beginWeekDay = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EndWeekDay", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EndWeekDay
        {
            get { return _endWeekDay; }
            set { _endWeekDay = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BeginTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EndTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GetValue", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string GetValue
        {
            get { return _getValue; }
            set { _getValue = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }
        /*
        [DataMember]
        [DataDesc(CName = "取单时间分类", ShortCode = "LBReportDate", Desc = "取单时间分类")]
        public virtual LBReportDate LBReportDate
        {
            get { return _lBReportDate; }
            set { _lBReportDate = value; }
        }*/


        #endregion
    }
    #endregion
}