using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region BHostTypeUser

    /// <summary>
    /// BHostTypeUser object for NHibernate mapped table 'B_HostTypeUser'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "站点类型和人员关系", ClassCName = "BHostTypeUser", ShortCode = "BHostTypeUser", Desc = "站点类型和人员关系")]
    public class BHostTypeUser : BaseEntity
    {
        #region Member Variables

        protected long? _hostTypeID;
        protected long? _empID;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected string _hostTypeName;

        #endregion

        #region Constructors

        public BHostTypeUser() { }

        public BHostTypeUser(long labID, long hostTypeID, long empID, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string hostTypeName)
        {
            this._labID = labID;
            this._hostTypeID = hostTypeID;
            this._empID = empID;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            _hostTypeName = hostTypeName;
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// 临时字段，不作为数据库存储用
        /// </summary>
        [DataMember]
        public virtual string HostTypeName
        {
            get { return _hostTypeName; }
            set { _hostTypeName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "HostTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? HostTypeID
        {
            get { return _hostTypeID; }
            set { _hostTypeID = value; }
        }
   

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EmpID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? EmpID
        {
            get { return _empID; }
            set { _empID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
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