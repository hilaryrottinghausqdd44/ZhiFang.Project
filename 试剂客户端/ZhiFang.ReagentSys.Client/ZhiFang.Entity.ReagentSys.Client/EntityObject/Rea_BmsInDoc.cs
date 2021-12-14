using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsInDoc

    /// <summary>
    /// ReaBmsInDoc object for NHibernate mapped table 'Rea_BmsInDoc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsInDoc", ShortCode = "ReaBmsInDoc", Desc = "")]
    public class ReaBmsInDoc : BaseEntity
    {
        #region Member Variables

        protected string _inDocNo;
        protected long? _companyID;
        protected string _companyName;
        protected string _carrier;
        protected long? _userID;
        protected string _invoiceNo;
        protected int _status;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected double? _totalPrice;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;
        protected long? _inType;
        protected string _inTypeName;
        protected string _statusName;
        protected long? _operateInDocID;
        protected string _operateInDocNo;
        protected long? _checkDocID;
        protected long? _orgID;
        protected int _reconciliationMark;
        protected int _lockMark;
        //protected ReaCenOrg _reaCenOrg;
        //protected IList<ReaBmsInDtl> _reaBmsInDtlList;
        protected string _otherDocNo;
        protected long? _saleDocConfirmID;
        protected string _saleDocNo;
        protected long? _sourceType;
        protected long? _saleDocID;
        protected string _userName;
        protected long? _deptID;
        protected string _deptName;

        protected int _iOFlag;
        protected string _iOMemo;

        #endregion

        #region Constructors

        public ReaBmsInDoc() { }

        public ReaBmsInDoc(long labID, string inDocNo, long companyID, string companyName, string carrier, long userID, string invoiceNo, int status, DateTime operDate, int printTimes, double totalPrice, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long inType, string inTypeName, string statusName, long operateInDocID, string operateInDocNo, long _orgID, int iOFlag, string iOMemo)
        {
            this._labID = labID;
            this._inDocNo = inDocNo;
            this._companyID = companyID;
            this._companyName = companyName;
            this._carrier = carrier;
            this._userID = userID;
            this._invoiceNo = invoiceNo;
            this._status = status;
            this._operDate = operDate;
            this._printTimes = printTimes;
            this._totalPrice = totalPrice;
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
            this._inType = inType;
            this._inTypeName = inTypeName;
            this._statusName = statusName;
            this._operateInDocID = operateInDocID;
            this._operateInDocNo = operateInDocNo;
            this._orgID = _orgID;

            this._iOFlag = iOFlag;
            this._iOMemo = iOMemo;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "操作人员姓名", ShortCode = "UserName", Desc = "操作人员姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "所属科室ID", ShortCode = "DeptID", Desc = "所属科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }
        [DataMember]
        [DataDesc(CName = "所属科室名称", ShortCode = "DeptName", Desc = "所属科室名称", ContextType = SysDic.All, Length = 200)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                _deptName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据来源", ShortCode = "SourceType", Desc = "数据来源", ContextType = SysDic.All, Length = 8)]
        public virtual long? SourceType
        {
            get { return _sourceType; }
            set { _sourceType = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供货单ID", ShortCode = "SaleDocID", Desc = "供货单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SaleDocID
        {
            get { return _saleDocID; }
            set { _saleDocID = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货单号", ShortCode = "SaleDocNo", Desc = "供货单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocNo
        {
            get { return _saleDocNo; }
            set
            {
                _saleDocNo = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供货验收单ID", ShortCode = "SaleDocConfirmID", Desc = "供货验收单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SaleDocConfirmID
        {
            get { return _saleDocConfirmID; }
            set { _saleDocConfirmID = value; }
        }
        [DataMember]
        [DataDesc(CName = "对帐标志", ShortCode = "ReconciliationMark", Desc = "对帐标志", ContextType = SysDic.All, Length = 4)]
        public virtual int ReconciliationMark
        {
            get { return _reconciliationMark; }
            set { _reconciliationMark = value; }
        }
        [DataMember]
        [DataDesc(CName = "对帐锁定标志", ShortCode = "LockMark", Desc = "对帐锁定标志", ContextType = SysDic.All, Length = 4)]
        public virtual int LockMark
        {
            get { return _lockMark; }
            set { _lockMark = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrgID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InDocNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string InDocNo
        {
            get { return _inDocNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for InDocNo", value, value.ToString());
                _inDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CompanyID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompanyName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CompanyName", value, value.ToString());
                _companyName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Carrier", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Carrier
        {
            get { return _carrier; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Carrier", value, value.ToString());
                _carrier = value;
            }
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
        [DataDesc(CName = "", ShortCode = "InvoiceNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string InvoiceNo
        {
            get { return _invoiceNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for InvoiceNo", value, value.ToString());
                _invoiceNo = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperDate
        {
            get { return _operDate; }
            set { _operDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintTimes", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TotalPrice", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InType", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? InType
        {
            get { return _inType; }
            set { _inType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string InTypeName
        {
            get { return _inTypeName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for InTypeName", value, value.ToString());
                _inTypeName = value;
            }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperateInDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateInDocID
        {
            get { return _operateInDocID; }
            set { _operateInDocID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperateInDocNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OperateInDocNo
        {
            get { return _operateInDocNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OperateInDocNo", value, value.ToString());
                _operateInDocNo = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckDocID
        {
            get { return _checkDocID; }
            set { _checkDocID = value; }
        }
        [DataMember]
        [DataDesc(CName = "第三方单号", ShortCode = "OtherDocNo", Desc = "第三方单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OtherDocNo
        {
            get { return _otherDocNo; }
            set
            {
                _otherDocNo = value;
            }
        }
        //[DataMember]
        //[DataDesc(CName = "机构表", ShortCode = "ReaCenOrg", Desc = "机构表")]
        //public virtual ReaCenOrg ReaCenOrg
        //{
        //    get { return _reaCenOrg; }
        //    set { _reaCenOrg = value; }
        //}

        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "ReaBmsInDtlList", Desc = "")]
        //public virtual IList<ReaBmsInDtl> ReaBmsInDtlList
        //{
        //    get
        //    {
        //        if (_reaBmsInDtlList == null)
        //        {
        //            _reaBmsInDtlList = new List<ReaBmsInDtl>();
        //        }
        //        return _reaBmsInDtlList;
        //    }
        //    set { _reaBmsInDtlList = value; }
        //}


        [DataMember]
        [DataDesc(CName = "接口数据标志", ShortCode = "IOFlag", Desc = "接口数据标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "接口结果备注", ShortCode = "IOMemo", Desc = "接口结果备注", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string IOMemo
        {
            get { return _iOMemo; }
            set
            {
                _iOMemo = value;
            }
        }

        #endregion

        #region 自定义属性
        protected long? _reaCompID;
        protected string _reaCompCode;
        protected string _reaCompanyName;

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地供应商ID", ShortCode = "ReaCompID", Desc = "本地供应商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompID
        {
            get { return _reaCompID; }
            set { _reaCompID = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompCode
        {
            get { return _reaCompCode; }
            set
            {
                _reaCompCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompanyName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompanyName
        {
            get { return _reaCompanyName; }
            set
            {
                _reaCompanyName = value;
            }
        }
        #endregion

        #region 自定义属性（入库接口使用）
        [DataMember]
        [DataDesc(CName = "第三方操作人编码", ShortCode = "UserCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string UserCode { get; set; }

        [DataMember]
        [DataDesc(CName = "第三方科室编码", ShortCode = "DeptCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string DeptCode { get; set; }

        #endregion

    }
    #endregion
}