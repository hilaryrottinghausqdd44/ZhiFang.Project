using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBTranRuleItem

    /// <summary>
    /// LBTranRuleItem object for NHibernate mapped table 'LB_TranRuleItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "分发规则项目明细", ClassCName = "LBTranRuleItem", ShortCode = "LBTranRuleItem", Desc = "分发规则项目明细")]
    public class LBTranRuleItem : BaseEntity
    {
        #region Member Variables

        protected DateTime? _dataUpdateTime;
        protected LBItem _lBItem;
        protected LBTranRule _lBTranRule;


        #endregion

        #region Constructors

        public LBTranRuleItem() { }

        public LBTranRuleItem(long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem lBItem, LBTranRule lBTranRule)
        {
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBItem = lBItem;
            this._lBTranRule = lBTranRule;
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
        [DataDesc(CName = "分发规则", ShortCode = "LBTranRule", Desc = "分发规则")]
        public virtual LBTranRule LBTranRule
        {
            get { return _lBTranRule; }
            set { _lBTranRule = value; }
        }


        #endregion
    }
    #endregion
}