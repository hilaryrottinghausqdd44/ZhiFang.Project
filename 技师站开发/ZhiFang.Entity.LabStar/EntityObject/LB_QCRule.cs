using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBQCRule

    /// <summary>
    /// LBQCRule object for NHibernate mapped table 'LB_QCRule'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBQCRule", ShortCode = "LBQCRule", Desc = "")]
    public class LBQCRule : BaseEntity
    {
        #region Member Variables

        protected string _qCRuleName;
        protected string _ruleDesc;
        protected bool _bDefault;
        protected bool _bWestGard;
        protected int _dispOrder;
        protected string _loseType;
        protected bool _bWarnState;
        protected string _operator;
        protected long? _operatorID;
        protected DateTime? _dataUpdateTime;
        protected IList<LBQCItemRule> _lBQCItemRuleList;
        protected IList<LBQCRulesCon> _lBQCRulesConList;


        #endregion

        #region Constructors

        public LBQCRule() { }

        public LBQCRule(long labID, string qCRuleName, string ruleDesc, bool bDefault, bool bWestGard, int dispOrder, string loseType, bool bWarnState, string _operator, long operatorID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._qCRuleName = qCRuleName;
            this._ruleDesc = ruleDesc;
            this._bDefault = bDefault;
            this._bWestGard = bWestGard;
            this._dispOrder = dispOrder;
            this._loseType = loseType;
            this._bWarnState = bWarnState;
            this._operator = _operator;
            this._operatorID = operatorID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCRuleName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string QCRuleName
        {
            get { return _qCRuleName; }
            set { _qCRuleName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RuleDesc", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string RuleDesc
        {
            get { return _ruleDesc; }
            set { _ruleDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BDefault", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BDefault
        {
            get { return _bDefault; }
            set { _bDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BWestGard", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BWestGard
        {
            get { return _bWestGard; }
            set { _bWestGard = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LoseType", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LoseType
        {
            get { return _loseType; }
            set { _loseType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BWarnState", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BWarnState
        {
            get { return _bWarnState; }
            set { _bWarnState = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Operator", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperatorID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
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
        [DataDesc(CName = "", ShortCode = "LBQCItemRuleList", Desc = "")]
        public virtual IList<LBQCItemRule> LBQCItemRuleList
        {
            get
            {
                if (_lBQCItemRuleList == null)
                {
                    _lBQCItemRuleList = new List<LBQCItemRule>();
                }
                return _lBQCItemRuleList;
            }
            set { _lBQCItemRuleList = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBQCRulesConList", Desc = "")]
        public virtual IList<LBQCRulesCon> LBQCRulesConList
        {
            get
            {
                if (_lBQCRulesConList == null)
                {
                    _lBQCRulesConList = new List<LBQCRulesCon>();
                }
                return _lBQCRulesConList;
            }
            set { _lBQCRulesConList = value; }
        }


        #endregion
    }
    #endregion
}