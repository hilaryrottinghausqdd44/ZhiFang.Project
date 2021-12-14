using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBRight

    /// <summary>
    /// LBRight object for NHibernate mapped table 'LB_Right'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBRight", ShortCode = "LBRight", Desc = "")]
    public class LBRight : BaseEntity
    {
        #region Member Variables

        protected long? _empID;
        protected long? _roleID;
        protected string _operator;
        protected long? _operatorID;
        protected DateTime? _dataUpdateTime;
        protected LBSection _lBSection;


        #endregion

        #region Constructors

        public LBRight() { }

        public LBRight(long labID, long empID, long roleID, string _operator, long operatorID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBSection lBSection)
        {
            this._labID = labID;
            this._empID = empID;
            this._roleID = roleID;
            this._operator = _operator;
            this._operatorID = operatorID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBSection = lBSection;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EmpID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? EmpID
        {
            get { return _empID; }
            set { _empID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "RoleID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
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
        [DataDesc(CName = "", ShortCode = "LBSection", Desc = "")]
        public virtual LBSection LBSection
        {
            get { return _lBSection; }
            set { _lBSection = value; }
        }


        #endregion
    }
    #endregion
}