using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBTranRuleType

    /// <summary>
    /// LBTranRuleType object for NHibernate mapped table 'LB_TranRuleType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBTranRuleType", ShortCode = "LBTranRuleType", Desc = "")]
    public class LBTranRuleType : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBTranRuleType() { }

        public LBTranRuleType(string cName, string comment, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._cName = cName;
            this._comment = comment;
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
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
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


        #endregion
    }
    #endregion
}