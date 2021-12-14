using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBOrderModel

    /// <summary>
    /// LBOrderModel object for NHibernate mapped table 'LB_OrderModel'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "医嘱模板", ClassCName = "LBOrderModel", ShortCode = "LBOrderModel", Desc = "医嘱模板")]
    public class LBOrderModel : BaseEntity
    {
        #region Member Variables

        protected long? _pOrderModelID;
        protected long? _orderModelTypeID;
        protected string _orderModelTypeName;
        protected string _cName;
        protected long? _deptID;
        protected long? _userID;
        protected string _sName;
        protected string _sCode;
        protected string _orderModelDesc;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBOrderModel() { }

        public LBOrderModel(long labID,long orderModelTypeID,string orderModelTypeName, long pOrderModelID, string cName, long deptID, long userID, string sName, string sCode, string orderModelDesc, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._orderModelTypeID = orderModelTypeID;
            this._orderModelTypeName = orderModelTypeName;
            this._pOrderModelID = pOrderModelID;
            this._cName = cName;
            this._deptID = deptID;
            this._userID = userID;
            this._sName = sName;
            this._sCode = sCode;
            this._orderModelDesc = orderModelDesc;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "POrderModelID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? POrderModelID
        {
            get { return _pOrderModelID; }
            set { _pOrderModelID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrderModelTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderModelTypeID
        {
            get { return _orderModelTypeID; }
            set { _orderModelTypeID = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrderModelTypeName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string OrderModelTypeName
        {
            get { return _orderModelTypeName; }
            set { _orderModelTypeName = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "UserID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string SCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrderModelDesc", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string OrderModelDesc
        {
            get { return _orderModelDesc; }
            set { _orderModelDesc = value; }
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