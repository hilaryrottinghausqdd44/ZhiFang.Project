using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBQCItemRule

    /// <summary>
    /// LBQCItemRule object for NHibernate mapped table 'LB_QCItemRule'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBQCItemRule", ShortCode = "LBQCItemRule", Desc = "")]
    public class LBQCItemRule : BaseEntity
    {
        #region Member Variables

        protected int _dispOrder;
        protected string _operator;
        protected long? _operatorID;
        protected DateTime? _dataUpdateTime;
        protected LBQCItem _lBQCItem;
        protected LBQCRule _lBQCRule;


        #endregion

        #region Constructors

        public LBQCItemRule() { }

        public LBQCItemRule(long labID, int dispOrder, string _operator, long operatorID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBQCItem lBQCItem, LBQCRule lBQCRule)
        {
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._operator = _operator;
            this._operatorID = operatorID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBQCItem = lBQCItem;
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
        [DataDesc(CName = "", ShortCode = "LBQCItem", Desc = "")]
        public virtual LBQCItem LBQCItem
        {
            get { return _lBQCItem; }
            set { _lBQCItem = value; }
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