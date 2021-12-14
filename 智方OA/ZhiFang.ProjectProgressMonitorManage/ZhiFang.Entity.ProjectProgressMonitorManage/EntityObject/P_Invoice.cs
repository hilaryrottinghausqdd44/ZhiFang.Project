using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PInvoice

    /// <summary>
    /// PInvoice object for NHibernate mapped table 'P_Invoice'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "PInvoice", ShortCode = "PInvoice", Desc = "")]
    public class PInvoice : BusinessBase
    {
        #region Member Variables

        protected string _invoiceNo;
        protected long? _clientID;
        protected string _clientName;
        protected long? _payOrgID;
        protected string _payOrgName;
        protected long? _componeID;
        protected string _componeName;
        protected long? _invoiceTypeID;
        protected string _invoiceTypeName;
        protected double _invoiceAmount;
        protected long? _invoiceContentTypeID;
        protected string _invoiceContentTypeName;
        protected long? _invoiceContentID;
        protected string _invoiceContentName;
        protected string _invoiceMemo;
        protected string _incomeTypeName;
        protected bool _isReceiveInvoice;
        protected string _isReceiveMoney;
        protected string _receiveInvoiceClientInfo;
        protected string _invoiceInfo;
        protected DateTime? _expectReceiveDate;
        protected long? _deptID;
        protected string _deptName;
        protected string _editMemoBusiness;
        protected long? _applyManID;
        protected string _applyMan;
        protected DateTime? _applyDate;
        protected long? _reviewManID;
        protected string _reviewMan;
        protected DateTime? _reviewDate;
        protected string _reviewInfo;
        protected long? _invoiceManID;
        protected string _invoiceManName;
        protected DateTime? _invoiceDate;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected long? _projectTypeID;
        protected string _projectTypeName;
        protected long? _projectPaceID;
        protected string _projectPaceName;
        protected long _status;
        protected string _receiveInvoiceName;
        protected string _receiveInvoiceAddress;
        protected string _receiveInvoicePhoneNumbers;
        protected string _freightName;
        protected string _freightOddNumbers;
        protected long? _twoReviewManID;
        protected string _twoReviewMan;
        protected DateTime? _twoReviewDate;
        protected string _twoReviewInfo;
        protected string _StatusName;
        protected long _contractID;
        protected string _contractName;
        private string _ClientAddress;
        private string _ClientPhone;
        private string _VATNumber;
        private string _VATBank;
        private string _VATAccount;
        protected double? _hardwareAmount;
        protected double? _softwareAmount;
        protected double? _serverAmount;
        protected double? _hardwareCount;
        protected double? _softwareCount;
        private string _percentAmount;

        protected long? _contrastId;
        protected string _contrastCName;
        protected long? _checkId;
        protected string _checkCName;
        #endregion

        #region Constructors

        public PInvoice() { }

        public PInvoice(long labID, string invoiceNo, long clientID, string clientName, long payOrgID, string payOrgName, long componeID, string componeName, long invoiceTypeID, string invoiceTypeName, double invoiceAmount, long invoiceContentTypeID, string invoiceContentTypeName, string invoiceMemo, string incomeTypeName, bool isReceiveInvoice, string isReceiveMoney, string receiveInvoiceClientInfo, string invoiceInfo, DateTime expectReceiveDate, long deptID, string deptName, string editMemoBusiness, long applyManID, string applyMan, DateTime applyDate, long reviewManID, string reviewMan, DateTime reviewDate, string reviewInfo, long invoiceManID, string invoiceManName, DateTime invoiceDate, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, long projectTypeID, string projectTypeName, long projectPaceID, string projectPaceName, long status, string receiveInvoiceName, string receiveInvoiceAddress, string receiveInvoicePhoneNumbers, string freightName, string freightOddNumbers, long twoReviewManID, string twoReviewMan, DateTime twoReviewDate, string twoReviewInfo, long? contrastId, string contrastCName, long? checkId, string checkCName)
        {
            this._labID = labID;
            this._invoiceNo = invoiceNo;
            this._clientID = clientID;
            this._clientName = clientName;
            this._payOrgID = payOrgID;
            this._payOrgName = payOrgName;
            this._componeID = componeID;
            this._componeName = componeName;
            this._invoiceTypeID = invoiceTypeID;
            this._invoiceTypeName = invoiceTypeName;
            this._invoiceAmount = invoiceAmount;
            this._invoiceContentTypeID = invoiceContentTypeID;
            this._invoiceContentTypeName = invoiceContentTypeName;
            this._invoiceMemo = invoiceMemo;
            this._incomeTypeName = incomeTypeName;
            this._isReceiveInvoice = isReceiveInvoice;
            this._isReceiveMoney = isReceiveMoney;
            this._receiveInvoiceClientInfo = receiveInvoiceClientInfo;
            this._invoiceInfo = invoiceInfo;
            this._expectReceiveDate = expectReceiveDate;
            this._deptID = deptID;
            this._deptName = deptName;
            this._editMemoBusiness = editMemoBusiness;
            this._applyManID = applyManID;
            this._applyMan = applyMan;
            this._applyDate = applyDate;
            this._reviewManID = reviewManID;
            this._reviewMan = reviewMan;
            this._reviewDate = reviewDate;
            this._reviewInfo = reviewInfo;
            this._invoiceManID = invoiceManID;
            this._invoiceManName = invoiceManName;
            this._invoiceDate = invoiceDate;
            this._dispOrder = dispOrder;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._projectTypeID = projectTypeID;
            this._projectTypeName = projectTypeName;
            this._projectPaceID = projectPaceID;
            this._projectPaceName = projectPaceName;
            this._status = status;
            this._receiveInvoiceName = receiveInvoiceName;
            this._receiveInvoiceAddress = receiveInvoiceAddress;
            this._receiveInvoicePhoneNumbers = receiveInvoicePhoneNumbers;
            this._freightName = freightName;
            this._freightOddNumbers = freightOddNumbers;
            this._twoReviewManID = twoReviewManID;
            this._twoReviewMan = twoReviewMan;
            this._twoReviewDate = twoReviewDate;
            this._twoReviewInfo = twoReviewInfo;
            this._contrastId = contrastId;
            this._contrastCName = contrastCName;
            this._checkId = checkId;
            this._checkCName = checkCName;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "发票号", ShortCode = "InvoiceNo", Desc = "发票号", ContextType = SysDic.All, Length = 200)]
        public virtual string InvoiceNo
        {
            get { return _invoiceNo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for InvoiceNo", value, value.ToString());
                _invoiceNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "ClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ClientID
        {
            get { return _clientID; }
            set { _clientID = value; }
        }

        [DataMember]
        [DataDesc(CName = "客户名称", ShortCode = "ClientName", Desc = "客户名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ClientName
        {
            get { return _clientName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ClientName", value, value.ToString());
                _clientName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "付款单位ID", ShortCode = "PayOrgID", Desc = "付款单位ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PayOrgID
        {
            get { return _payOrgID; }
            set { _payOrgID = value; }
        }

        [DataMember]
        [DataDesc(CName = "付款单位", ShortCode = "PayOrgName", Desc = "付款单位", ContextType = SysDic.All, Length = 200)]
        public virtual string PayOrgName
        {
            get { return _payOrgName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for PayOrgName", value, value.ToString());
                _payOrgName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本公司ID", ShortCode = "ComponeID", Desc = "本公司ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ComponeID
        {
            get { return _componeID; }
            set { _componeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "本公司名称", ShortCode = "ComponeName", Desc = "本公司名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ComponeName
        {
            get { return _componeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ComponeName", value, value.ToString());
                _componeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发票类型ID", ShortCode = "InvoiceTypeID", Desc = "发票类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? InvoiceTypeID
        {
            get { return _invoiceTypeID; }
            set { _invoiceTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "发票类型名称", ShortCode = "InvoiceTypeName", Desc = "发票类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string InvoiceTypeName
        {
            get { return _invoiceTypeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for InvoiceTypeName", value, value.ToString());
                _invoiceTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开票金额", ShortCode = "InvoiceAmount", Desc = "开票金额", ContextType = SysDic.All, Length = 8)]
        public virtual double InvoiceAmount
        {
            get { return _invoiceAmount; }
            set { _invoiceAmount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开票内容类型ID", ShortCode = "InvoiceContentTypeID", Desc = "开票内容类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? InvoiceContentTypeID
        {
            get { return _invoiceContentTypeID; }
            set { _invoiceContentTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "开票内容类型名称", ShortCode = "InvoiceContentTypeName", Desc = "开票内容类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string InvoiceContentTypeName
        {
            get { return _invoiceContentTypeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for InvoiceContentTypeName", value, value.ToString());
                _invoiceContentTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开票内容ID", ShortCode = "InvoiceContentID", Desc = "开票内容ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? InvoiceContentID
        {
            get { return _invoiceContentID; }
            set { _invoiceContentID = value; }
        }

        [DataMember]
        [DataDesc(CName = "开票内容名称", ShortCode = "InvoiceContentName", Desc = "开票内容名称", ContextType = SysDic.All, Length = 200)]
        public virtual string InvoiceContentName
        {
            get { return _invoiceContentName; }
            set
            {               
                _invoiceContentName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "其它说明", ShortCode = "InvoiceMemo", Desc = "其它说明", ContextType = SysDic.All, Length = 200)]
        public virtual string InvoiceMemo
        {
            get { return _invoiceMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for InvoiceMemo", value, value.ToString());
                _invoiceMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "收入分类", ShortCode = "IncomeTypeName", Desc = "收入分类", ContextType = SysDic.All, Length = 200)]
        public virtual string IncomeTypeName
        {
            get { return _incomeTypeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for IncomeTypeName", value, value.ToString());
                _incomeTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "票收到否", ShortCode = "IsReceiveInvoice", Desc = "票收到否", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReceiveInvoice
        {
            get { return _isReceiveInvoice; }
            set { _isReceiveInvoice = value; }
        }

        [DataMember]
        [DataDesc(CName = "收款否", ShortCode = "IsReceiveMoney", Desc = "收款否", ContextType = SysDic.All, Length = 200)]
        public virtual string IsReceiveMoney
        {
            get { return _isReceiveMoney; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for IsReceiveMoney", value, value.ToString());
                _isReceiveMoney = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "收票单位信息", ShortCode = "ReceiveInvoiceClientInfo", Desc = "收票单位信息", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveInvoiceClientInfo
        {
            get { return _receiveInvoiceClientInfo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiveInvoiceClientInfo", value, value.ToString());
                _receiveInvoiceClientInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "开票信息", ShortCode = "InvoiceInfo", Desc = "开票信息", ContextType = SysDic.All, Length = 200)]
        public virtual string InvoiceInfo
        {
            get { return _invoiceInfo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for InvoiceInfo", value, value.ToString());
                _invoiceInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预计回款时间", ShortCode = "ExpectReceiveDate", Desc = "预计回款时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ExpectReceiveDate
        {
            get { return _expectReceiveDate; }
            set { _expectReceiveDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "所属部门ID", ShortCode = "DeptID", Desc = "所属部门ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "所属部门名称", ShortCode = "DeptName", Desc = "所属部门名称", ContextType = SysDic.All, Length = 200)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
                _deptName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "修改说明（业务）", ShortCode = "EditMemoBusiness", Desc = "修改说明（业务）", ContextType = SysDic.All, Length = 200)]
        public virtual string EditMemoBusiness
        {
            get { return _editMemoBusiness; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for EditMemoBusiness", value, value.ToString());
                _editMemoBusiness = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ApplyManID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApplyManID
        {
            get { return _applyManID; }
            set { _applyManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "申请人", ShortCode = "ApplyMan", Desc = "申请人", ContextType = SysDic.All, Length = 200)]
        public virtual string ApplyMan
        {
            get { return _applyMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ApplyMan", value, value.ToString());
                _applyMan = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请时间", ShortCode = "ApplyDate", Desc = "申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApplyDate
        {
            get { return _applyDate; }
            set { _applyDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人ID", ShortCode = "ReviewManID", Desc = "审核人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReviewManID
        {
            get { return _reviewManID; }
            set { _reviewManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人", ShortCode = "ReviewMan", Desc = "审核人", ContextType = SysDic.All, Length = 200)]
        public virtual string ReviewMan
        {
            get { return _reviewMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReviewMan", value, value.ToString());
                _reviewMan = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "ReviewDate", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReviewDate
        {
            get { return _reviewDate; }
            set { _reviewDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人意见", ShortCode = "ReviewInfo", Desc = "审核人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string ReviewInfo
        {
            get { return _reviewInfo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ReviewInfo", value, value.ToString());
                _reviewInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InvoiceManID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? InvoiceManID
        {
            get { return _invoiceManID; }
            set { _invoiceManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "开票负责人", ShortCode = "InvoiceManName", Desc = "开票负责人", ContextType = SysDic.All, Length = 200)]
        public virtual string InvoiceManName
        {
            get { return _invoiceManName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for InvoiceManName", value, value.ToString());
                _invoiceManName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开票时间", ShortCode = "InvoiceDate", Desc = "开票时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvoiceDate
        {
            get { return _invoiceDate; }
            set { _invoiceDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目类型ID", ShortCode = "ProjectTypeID", Desc = "项目类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProjectTypeID
        {
            get { return _projectTypeID; }
            set { _projectTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目类型名称", ShortCode = "ProjectTypeName", Desc = "项目类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ProjectTypeName
        {
            get { return _projectTypeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ProjectTypeName", value, value.ToString());
                _projectTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目进度ID", ShortCode = "ProjectPaceID", Desc = "项目进度ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProjectPaceID
        {
            get { return _projectPaceID; }
            set { _projectPaceID = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目进度名称", ShortCode = "ProjectPaceName", Desc = "项目进度名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ProjectPaceName
        {
            get { return _projectPaceName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ProjectPaceName", value, value.ToString());
                _projectPaceName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发票管理", ShortCode = "Status", Desc = "发票管理", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "收票人姓名", ShortCode = "ReceiveInvoiceName", Desc = "收票人姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string ReceiveInvoiceName
        {
            get { return _receiveInvoiceName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiveInvoiceName", value, value.ToString());
                _receiveInvoiceName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "收票人地址", ShortCode = "ReceiveInvoiceAddress", Desc = "收票人地址", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveInvoiceAddress
        {
            get { return _receiveInvoiceAddress; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiveInvoiceAddress", value, value.ToString());
                _receiveInvoiceAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "收票人电话", ShortCode = "ReceiveInvoicePhoneNumbers", Desc = "收票人电话", ContextType = SysDic.All, Length = 20)]
        public virtual string ReceiveInvoicePhoneNumbers
        {
            get { return _receiveInvoicePhoneNumbers; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiveInvoicePhoneNumbers", value, value.ToString());
                _receiveInvoicePhoneNumbers = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "货运公司名称", ShortCode = "FreightName", Desc = "货运公司名称", ContextType = SysDic.All, Length = 200)]
        public virtual string FreightName
        {
            get { return _freightName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for FreightName", value, value.ToString());
                _freightName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "货运单号", ShortCode = "FreightOddNumbers", Desc = "货运单号", ContextType = SysDic.All, Length = 200)]
        public virtual string FreightOddNumbers
        {
            get { return _freightOddNumbers; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for FreightOddNumbers", value, value.ToString());
                _freightOddNumbers = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "2审核人ID", ShortCode = "TwoReviewManID", Desc = "2审核人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TwoReviewManID
        {
            get { return _twoReviewManID; }
            set { _twoReviewManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "2审核人", ShortCode = "TwoReviewMan", Desc = "2审核人", ContextType = SysDic.All, Length = 200)]
        public virtual string TwoReviewMan
        {
            get { return _twoReviewMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for TwoReviewMan", value, value.ToString());
                _twoReviewMan = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "2审核时间", ShortCode = "TwoReviewDate", Desc = "2审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TwoReviewDate
        {
            get { return _twoReviewDate; }
            set { _twoReviewDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "2审核人意见", ShortCode = "TwoReviewInfo", Desc = "2审核人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string TwoReviewInfo
        {
            get { return _twoReviewInfo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for TwoReviewInfo", value, value.ToString());
                _twoReviewInfo = value;
            }
        }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同ID", ShortCode = "ContractID", Desc = "合同ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ContractID
        {
            get { return _contractID; }
            set { _contractID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同名称", ShortCode = "ContractName", Desc = "合同名称", ContextType = SysDic.All, Length = 8)]
        public virtual string ContractName
        {
            get { return _contractName; }
            set { _contractName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户地址", ShortCode = "ClientAddress", Desc = "客户地址", ContextType = SysDic.All, Length = 8)]
        public virtual string ClientAddress
        {
            get { return _ClientAddress; }
            set { _ClientAddress = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户电话", ShortCode = "ClientPhone", Desc = "客户电话", ContextType = SysDic.All, Length = 8)]
        public virtual string ClientPhone
        {
            get { return _ClientPhone; }
            set { _ClientPhone = value; }
        }

        [DataMember]
        [DataDesc(CName = "增值税税号", ShortCode = "VATNumber", Desc = "增值税税号")]
        public virtual string VATNumber
        {
            get
            {
                return _VATNumber;
            }
            set { _VATNumber = value; }
        }

        [DataMember]
        [DataDesc(CName = "增值税开户行", ShortCode = "VATBank", Desc = "增值税开户行")]
        public virtual string VATBank
        {
            get
            {
                return _VATBank;
            }
            set { _VATBank = value; }
        }

        [DataMember]
        [DataDesc(CName = "增值税帐号", ShortCode = "VATAccount", Desc = "增值税帐号")]
        public virtual string VATAccount
        {
            get
            {
                return _VATAccount;
            }
            set { _VATAccount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "硬件金额", ShortCode = "HardwareAmount", Desc = "硬件金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? HardwareAmount
        {
            get { return _hardwareAmount; }
            set { _hardwareAmount = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "软件金额", ShortCode = "SoftwareAmount", Desc = "软件金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? SoftwareAmount
        {
            get { return _softwareAmount; }
            set { _softwareAmount = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务金额", ShortCode = "ServerAmount", Desc = "服务金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? ServerAmount
        {
            get { return _serverAmount; }
            set { _serverAmount = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "软件套数", ShortCode = "SoftwareCount", Desc = "软件套数", ContextType = SysDic.All, Length = 8)]
        public virtual double? SoftwareCount
        {
            get { return _softwareCount; }
            set { _softwareCount = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "硬件数量", ShortCode = "HardwareCount", Desc = "硬件数量", ContextType = SysDic.All, Length = 8)]
        public virtual double? HardwareCount
        {
            get { return _hardwareCount; }
            set { _hardwareCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "本次开票金额占合同额百分比", ShortCode = "PercentAmount", Desc = "本次开票金额占合同额百分比")]
        public virtual string PercentAmount
        {
            get
            {
                return _percentAmount;
            }
            set { _percentAmount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "对比人Id", ShortCode = "ContrastId", Desc = "对比人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ContrastId
        {
            get { return _contrastId; }
            set { _contrastId = value; }
        }

        [DataMember]
        [DataDesc(CName = "对比人名称", ShortCode = "ContrastCName", Desc = "对比人名称", ContextType = SysDic.All, Length = 60)]
        public virtual string ContrastCName
        {
            get { return _contrastCName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for ContrastCName", value, value.ToString());
                _contrastCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人Id", ShortCode = "CheckId", Desc = "审核人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckId
        {
            get { return _checkId; }
            set { _checkId = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人名称", ShortCode = "CheckCName", Desc = "审核人名称", ContextType = SysDic.All, Length = 60)]
        public virtual string CheckCName
        {
            get { return _checkCName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckCName", value, value.ToString());
                _checkCName = value;
            }
        }
        #endregion


    }
    #endregion
}