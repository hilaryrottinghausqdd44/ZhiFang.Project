using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region BHost

    /// <summary>
    /// BHost object for NHibernate mapped table 'B_Host'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "站点表", ClassCName = "BHost", ShortCode = "BHost", Desc = "站点表")]
    public class BHost : BaseEntity
    {
        #region Member Variables

        protected string _hostName;
        protected string _shortCode;
        protected string _iPAddress;
        protected string _hostDesc;
        protected int _dispOrder;
        protected long? _hostTypeID;
        protected string _hostType;
        protected long? _deptID;
        protected string _pinYinZiTou;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public BHost() { }

        public BHost(long labID, string hostName, string shortCode, string iPAddress, string hostDesc, int dispOrder, long hostTypeID, string hostType, long deptID, string pinYinZiTou, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._hostName = hostName;
            this._shortCode = shortCode;
            this._iPAddress = iPAddress;
            this._hostDesc = hostDesc;
            this._dispOrder = dispOrder;
            this._hostTypeID = hostTypeID;
            this._hostType = hostType;
            this._deptID = deptID;
            this._pinYinZiTou = pinYinZiTou;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "HostName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set { _shortCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IPAddress", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string IPAddress
        {
            get { return _iPAddress; }
            set { _iPAddress = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HostDesc", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string HostDesc
        {
            get { return _hostDesc; }
            set { _hostDesc = value; }
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
        [DataDesc(CName = "", ShortCode = "HostTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? HostTypeID
        {
            get { return _hostTypeID; }
            set { _hostTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HostType", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string HostType
        {
            get { return _hostType; }
            set { _hostType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
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