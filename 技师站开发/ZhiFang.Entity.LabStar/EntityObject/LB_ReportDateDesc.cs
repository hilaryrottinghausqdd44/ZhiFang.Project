using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBReportDateDesc

    /// <summary>
    /// LBReportDateDesc object for NHibernate mapped table 'LB_ReportDateDesc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBReportDateDesc", ShortCode = "LBReportDateDesc", Desc = "")]
    public class LBReportDateDesc : BaseEntity
    {
        #region Member Variables

        //protected long _reportDateID;
        protected string _reportDateDesc;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected LBReportDate _lBReportDate;

        #endregion

        #region Constructors

        public LBReportDateDesc() { }

        public LBReportDateDesc(LBReportDate LBReportDate, string reportDateDesc, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
           
            this._lBReportDate = LBReportDate;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

/*
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReportDateID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ReportDateID
        {
            get { return _reportDateID; }
            set { _reportDateID = value; }
        }*/

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportDateDesc", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string ReportDateDesc
        {
            get { return _reportDateDesc; }
            set { _reportDateDesc = value; }
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

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBReportDate", Desc = "")]
        public virtual LBReportDate LBReportDate
        {
            get { return _lBReportDate; }
            set { _lBReportDate = value; }
        }

        #endregion
    }
    #endregion
}