using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBQCRulesCon

    /// <summary>
    /// LBQCRulesCon object for NHibernate mapped table 'LB_QCRulesCon'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBQCRulesCon", ShortCode = "LBQCRulesCon", Desc = "")]
    public class LBQCRulesCon : BaseEntity
    {
        #region Member Variables

        protected int _dispOrder;
        protected string _operator;
        protected long? _operatorID;
        protected DateTime? _dataUpdateTime;
        protected LBQCRuleBase _lBQCRuleBase;
        protected LBQCRule _lBQCRule;


        #endregion

        #region Constructors

        public LBQCRulesCon() { }

        public LBQCRulesCon(long labID, int dispOrder, string _operator, long operatorID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBQCRuleBase lBQCRuleBase, LBQCRule lBQCRule)
        {
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._operator = _operator;
            this._operatorID = operatorID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBQCRuleBase = lBQCRuleBase;
            this._lBQCRule = lBQCRule;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "", ShortCode = "LBQCRuleBase", Desc = "")]
        public virtual LBQCRuleBase LBQCRuleBase
        {
            get { return _lBQCRuleBase; }
            set { _lBQCRuleBase = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBQCRule", Desc = "")]
        public virtual LBQCRule LBQCRule
        {
            get { return _lBQCRule; }
            set { _lBQCRule = value; }
        }


        #endregion
    }
    #endregion
}