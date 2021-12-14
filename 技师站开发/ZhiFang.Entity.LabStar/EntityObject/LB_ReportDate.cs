using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBReportDate

    /// <summary>
    /// LBReportDate object for NHibernate mapped table 'LB_ReportDate'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "取单时间分类", ClassCName = "LBReportDate", ShortCode = "LBReportDate", Desc = "取单时间分类")]
    public class LBReportDate : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _datememo;
        //protected string _reportDateDesc;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        //protected IList<LBReportDateItem> _lBReportDateItemList;
        //protected IList<LBReportDateRule> _lBReportDateRuleList;


        #endregion

        #region Constructors

        public LBReportDate() { }

        public LBReportDate(string cName, string datememo, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._cName = cName;
            this._datememo = datememo;           
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
        [DataDesc(CName = "", ShortCode = "Datememo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Datememo
        {
            get { return _datememo; }
            set { _datememo = value; }
        }

      /*[DataMember]
        [DataDesc(CName = "", ShortCode = "ReportDateDesc", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string ReportDateDesc
        {
            get { return _reportDateDesc; }
            set { _reportDateDesc = value; }
        }*/

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

        //[DataMember]
        //[DataDesc(CName = "取单分类项目明细", ShortCode = "LBReportDateItemList", Desc = "取单分类项目明细")]
        //public virtual IList<LBReportDateItem> LBReportDateItemList
        //{
        //    get
        //    {
        //        if (_lBReportDateItemList == null)
        //        {
        //            _lBReportDateItemList = new List<LBReportDateItem>();
        //        }
        //        return _lBReportDateItemList;
        //    }
        //    set { _lBReportDateItemList = value; }
        //}

        //[DataMember]
        //[DataDesc(CName = "取单时间段规则", ShortCode = "LBReportDateRuleList", Desc = "取单时间段规则")]
        //public virtual IList<LBReportDateRule> LBReportDateRuleList
        //{
        //    get
        //    {
        //        if (_lBReportDateRuleList == null)
        //        {
        //            _lBReportDateRuleList = new List<LBReportDateRule>();
        //        }
        //        return _lBReportDateRuleList;
        //    }
        //    set { _lBReportDateRuleList = value; }
        //}


        #endregion
    }
    #endregion
}