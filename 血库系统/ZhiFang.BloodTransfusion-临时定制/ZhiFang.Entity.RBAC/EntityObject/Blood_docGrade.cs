using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region BlooddocGrade

    /// <summary>
    /// BlooddocGrade object for NHibernate mapped table 'Blood_docGrade'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BlooddocGrade", ShortCode = "BlooddocGrade", Desc = "")]
    public class BlooddocGrade : BaseEntityServiceByString
    {
        #region Member Variables

        protected string _cName;
        protected decimal _bCount;
        protected int _dispOrder;
        protected double? _lowLimit;
        protected double? _upperLimit;
        #endregion

        #region Constructors

        public BlooddocGrade() { }

        public BlooddocGrade(string gradeName, decimal bCount, int dispOrder, long labID, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._cName = gradeName;
            this._bCount = bCount;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用血量审核范围下限值", ShortCode = "LowLimit ", Desc = "用血量审核范围下限值", ContextType = SysDic.All)]
        public virtual double? LowLimit
        {
            get { return _lowLimit; }
            set { _lowLimit = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用血量审核范围上限值", ShortCode = "UpperLimit ", Desc = "用血量审核范围上限值", ContextType = SysDic.All)]
        public virtual double? UpperLimit
        {
            get { return _upperLimit; }
            set { _upperLimit = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GradeName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BCount", Desc = "", ContextType = SysDic.All, Length = 9)]
        public virtual decimal BCount
        {
            get { return _bCount; }
            set { _bCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }


        #endregion
    }
    #endregion
}