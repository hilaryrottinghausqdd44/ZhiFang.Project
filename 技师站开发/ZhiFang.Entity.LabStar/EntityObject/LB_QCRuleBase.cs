using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBQCRuleBase

    /// <summary>
    /// LBQCRuleBase object for NHibernate mapped table 'LB_QCRuleBase'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBQCRuleBase", ShortCode = "LBQCRuleBase", Desc = "")]
    public class LBQCRuleBase : BaseEntity
    {
        #region Member Variables

        protected int _isDefault;
        protected string _qCRuleBaseName;
        protected string _ruleDesc;
        protected int _countN;
        protected int _countM;
        protected double _multX;
        protected double _multY;
        protected int _ruleType;
        protected string _operator;
        protected long? _operatorID;
        protected DateTime? _dataUpdateTime;
        protected IList<LBQCRulesCon> _lBQCRulesConList;


        #endregion

        #region Constructors

        public LBQCRuleBase() { }

        public LBQCRuleBase(long labID, int isDefault, string qCRuleBaseName, string ruleDesc, int countN, int countM, double multX, double multY, int ruleType, string _operator, long operatorID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._isDefault = isDefault;
            this._qCRuleBaseName = qCRuleBaseName;
            this._ruleDesc = ruleDesc;
            this._countN = countN;
            this._countM = countM;
            this._multX = multX;
            this._multY = multY;
            this._ruleType = ruleType;
            this._operator = _operator;
            this._operatorID = operatorID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDefault", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCRuleBaseName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string QCRuleBaseName
        {
            get { return _qCRuleBaseName; }
            set { _qCRuleBaseName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RuleDesc", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string RuleDesc
        {
            get { return _ruleDesc; }
            set { _ruleDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CountN", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CountN
        {
            get { return _countN; }
            set { _countN = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CountM", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CountM
        {
            get { return _countM; }
            set { _countM = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MultX", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double MultX
        {
            get { return _multX; }
            set { _multX = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MultY", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double MultY
        {
            get { return _multY; }
            set { _multY = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RuleType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int RuleType
        {
            get { return _ruleType; }
            set { _ruleType = value; }
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