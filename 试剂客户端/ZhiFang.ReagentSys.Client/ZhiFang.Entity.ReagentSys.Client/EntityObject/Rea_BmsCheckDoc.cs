using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsCheckDoc

    /// <summary>
    /// ReaBmsCheckDoc object for NHibernate mapped table 'Rea_BmsCheckDoc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsCheckDoc", ShortCode = "ReaBmsCheckDoc", Desc = "")]
    public class ReaBmsCheckDoc : BaseEntity
    {
        #region Member Variables

        protected string _checkDocNo;
        protected long? _reaCompanyID;
        protected string _companyName;
        protected long? _storageID;
        protected long? _placeID;
        protected string _storageName;
        protected string _placeName;
        protected string _reaServerCompCode;
        protected int _status;
        protected string _statusName;
        protected int _isLock;
        protected int _isException;
        protected int _isHandleException;
        protected long? _checkerID;
        protected string _checkerName;
        protected DateTime? _checkDateTime;
        protected long? _examinerID;
        protected string _examinerName;
        protected DateTime? _examinerDateTime;
        protected string _examinerMemo;
        protected int _printTimes;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;
        protected bool _isCompFlag;
        protected long _bmsCheckResult;
        protected string _goodsClass;
        protected string _goodsClassType;

        protected bool _isStorageGoodsLink;
        protected bool _isHasZeroQty;
        #endregion

        #region Constructors

        public ReaBmsCheckDoc() { }

        public ReaBmsCheckDoc(long labID, string checkDocNo, long reaCompanyID, string companyName, long storageID, long placeID, string storageName, string placeName, string reaServerCompCode, int status, string statusName, int isLock, int isException, int isHandleException, long checkerID, string checkerName, DateTime checkDateTime, long examinerID, string examinerName, DateTime examinerDateTime, string examinerMemo, int printTimes, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._checkDocNo = checkDocNo;
            this._reaCompanyID = reaCompanyID;
            this._companyName = companyName;
            this._storageID = storageID;
            this._placeID = placeID;
            this._storageName = storageName;
            this._placeName = placeName;
            this._reaServerCompCode = reaServerCompCode;
            this._status = status;
            this._statusName = statusName;
            this._isLock = isLock;
            this._isException = isException;
            this._isHandleException = isHandleException;
            this._checkerID = checkerID;
            this._checkerName = checkerName;
            this._checkDateTime = checkDateTime;
            this._examinerID = examinerID;
            this._examinerName = examinerName;
            this._examinerDateTime = examinerDateTime;
            this._examinerMemo = examinerMemo;
            this._printTimes = printTimes;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckDocNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CheckDocNo
        {
            get { return _checkDocNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckDocNo", value, value.ToString());
                _checkDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReaCompanyID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompanyID
        {
            get { return _reaCompanyID; }
            set { _reaCompanyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompanyName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CompanyName", value, value.ToString());
                _companyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StorageID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PlaceID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? PlaceID
        {
            get { return _placeID; }
            set { _placeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StorageName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string StorageName
        {
            get { return _storageName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for StorageName", value, value.ToString());
                _storageName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PlaceName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string PlaceName
        {
            get { return _placeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for PlaceName", value, value.ToString());
                _placeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaServerCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerCompCode
        {
            get { return _reaServerCompCode; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaServerCompCode", value, value.ToString());
                _reaServerCompCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Status", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StatusName
        {
            get { return _statusName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StatusName", value, value.ToString());
                _statusName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsLock", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsException", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsException
        {
            get { return _isException; }
            set { _isException = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsHandleException", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsHandleException
        {
            get { return _isHandleException; }
            set { _isHandleException = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckerID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckerID
        {
            get { return _checkerID; }
            set { _checkerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckerName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckerName
        {
            get { return _checkerName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckerName", value, value.ToString());
                _checkerName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckDateTime
        {
            get { return _checkDateTime; }
            set { _checkDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ExaminerID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ExaminerID
        {
            get { return _examinerID; }
            set { _examinerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ExaminerName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ExaminerName
        {
            get { return _examinerName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ExaminerName", value, value.ToString());
                _examinerName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ExaminerDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ExaminerDateTime
        {
            get { return _examinerDateTime; }
            set { _examinerDateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ExaminerMemo", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string ExaminerMemo
        {
            get { return _examinerMemo; }
            set
            {
                _examinerMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintTimes", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CreaterName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
        {
            get { return _createrName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
                _createrName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCompFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsCompFlag
        {
            get { return _isCompFlag; }
            set { _isCompFlag = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BmsCheckResult", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long BmsCheckResult
        {
            get { return _bmsCheckResult; }
            set { _bmsCheckResult = value; }
        }
        [DataMember]
        [DataDesc(CName = "一级分类", ShortCode = "GoodsClass", Desc = "一级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClass
        {
            get { return _goodsClass; }
            set { _goodsClass = value; }
        }

        [DataMember]
        [DataDesc(CName = "二级分类", ShortCode = "GoodsClassType", Desc = "二级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClassType
        {
            get { return _goodsClassType; }
            set { _goodsClassType = value; }
        }

        [DataMember]
        [DataDesc(CName = "仅对选择库房的试剂进行盘库", ShortCode = "IsStorageGoodsLink", Desc = "仅对选择库房的试剂进行盘库", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsStorageGoodsLink
        {
            get { return _isStorageGoodsLink; }
            set { _isStorageGoodsLink = value; }
        }
        [DataMember]
        [DataDesc(CName = "是否包括库存数为零的库存货品", ShortCode = "IsHasZeroQty", Desc = "是否包括库存数为零的库存货品", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsHasZeroQty
        {
            get { return _isHasZeroQty; }
            set { _isHasZeroQty = value; }
        }
        #endregion
    }
    #endregion
}