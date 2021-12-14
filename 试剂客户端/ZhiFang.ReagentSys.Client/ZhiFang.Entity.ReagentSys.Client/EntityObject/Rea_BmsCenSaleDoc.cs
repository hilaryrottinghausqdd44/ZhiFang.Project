using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsCenSaleDoc

    /// <summary>
    /// ReaBmsCenSaleDoc object for NHibernate mapped table 'Rea_BmsCenSaleDoc'.
    /// int?及double?添加上[JsonConverter(typeof(JsonConvertClass))]时,在上传平台进行序列化及反序列化时值为空
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsCenSaleDoc", ShortCode = "ReaBmsCenSaleDoc", Desc = "")]
    public class ReaBmsCenSaleDoc : BaseEntityService
    {
        #region Member Variables

        protected long? _labcID;
        protected string _labcName;
        protected string _reaServerLabcCode;
        protected string _saleDocNo;
        protected long? _orderDocID;
        protected long? _orgID;
        protected string _orderDocNo;
        protected long? _compID;
        protected string _companyName;
        protected int _urgentFlag;
        protected string _urgentFlagName;
        protected int _status;
        protected string _statusName;
        protected long? _userID;
        protected string _userName;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected int _iOFlag;
        protected string _memo;
        protected int _source;
        protected DateTime? _accepterTime;
        protected string _invoiceNo;

        protected string _labAddress;
        protected string _labContact;
        protected string _labTel;
        protected string _labHotTel;
        protected string _labFox;
        protected string _labEmail;
        protected string _labWebAddress;
        protected string _compAddress;
        protected string _compContact;
        protected string _compTel;
        protected string _compHotTel;
        protected string _compFox;
        protected string _compEmail;
        protected string _compWebAddress;
        protected string _checker;
        protected DateTime? _checkTime;
        protected string _sender;
        protected string _receiver;
        protected string _invoiceReceiver;
        protected DateTime? _receiveTime;
        protected DateTime? _sendOutTime;
        protected string _otherOrderDocNo;
        protected DateTime? _dataUpdateTime;
        protected long? _reaCompID;
        protected string _reaCompanyName;
        protected string _reaServerCompCode;
        protected long? _cenSaleDocID;
        protected string _deptName;
        protected DateTime? _secAccepterTime;
        protected bool _isSplit;
        protected double? _totalPrice;
        protected long? _checkerID;
        protected int _deleteFlag;
        protected string _reaCompCode;
        protected string _reaLabcCode;
        protected long? _labOrderDocID;
        protected string _otherDocNo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        #endregion

        #region Constructors

        public ReaBmsCenSaleDoc() { }

        public ReaBmsCenSaleDoc(long labID, long labcID, string labcName, string saleDocNo, long orderDocID, long orgID, string orderDocNo, long compID, string companyName, int urgentFlag, string urgentFlagName, int status, string statusName, long userID, string userName, DateTime operDate, int printTimes, int iOFlag, string memo, int source, string invoiceNo, string labAddress, string labContact, string labTel, string labHotTel, string labFox, string labEmail, string labWebAddress, string compAddress, string compContact, string compTel, string compHotTel, string compFox, string compEmail, string compWebAddress, string checker, DateTime checkTime, string sender, string receiver, string invoiceReceiver, DateTime receiveTime, DateTime sendOutTime, string otherOrderDocNo, DateTime dataUpdateTime, DateTime dataAddTime, long reaCompID, string reaCompanyName, string reaServerCompCode, long cenSaleDocID, string deptName, byte[] dataTimeStamp, DateTime secAccepterTime, bool isSplit, double totalPrice, long checkerID, int deleteFlag, string reaServerLabcCode, string reaCompCode, string reaLabcCode, long labOrderDocID, string otherDocNo, string zX1, string zX2, string zX3)
        {
            this._labID = labID;
            this._labcID = labcID;
            this._labcName = labcName;
            this._saleDocNo = saleDocNo;
            this._orderDocID = orderDocID;
            this._orgID = orgID;
            this._orderDocNo = orderDocNo;
            this._compID = compID;
            this._companyName = companyName;
            this._urgentFlag = urgentFlag;
            this._urgentFlagName = urgentFlagName;
            this._status = status;
            this._statusName = statusName;
            this._userID = userID;
            this._userName = userName;
            this._operDate = operDate;
            this._printTimes = printTimes;
            this._iOFlag = iOFlag;
            this._memo = memo;
            this._source = source;
            this._invoiceNo = invoiceNo;
            this._labAddress = labAddress;
            this._labContact = labContact;
            this._labTel = labTel;
            this._labHotTel = labHotTel;
            this._labFox = labFox;
            this._labEmail = labEmail;
            this._labWebAddress = labWebAddress;
            this._compAddress = compAddress;
            this._compContact = compContact;
            this._compTel = compTel;
            this._compHotTel = compHotTel;
            this._compFox = compFox;
            this._compEmail = compEmail;
            this._compWebAddress = compWebAddress;
            this._checker = checker;
            this._checkTime = checkTime;
            this._sender = sender;
            this._receiver = receiver;
            this._invoiceReceiver = invoiceReceiver;
            this._receiveTime = receiveTime;
            this._sendOutTime = sendOutTime;
            this._otherOrderDocNo = otherOrderDocNo;
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
            this._reaCompID = reaCompID;
            this._reaCompanyName = reaCompanyName;
            this._reaServerCompCode = reaServerCompCode;
            this._cenSaleDocID = cenSaleDocID;
            this._deptName = deptName;
            this._dataTimeStamp = dataTimeStamp;
            this._secAccepterTime = secAccepterTime;
            this._isSplit = isSplit;
            this._totalPrice = totalPrice;
            this._checkerID = checkerID;
            this._deleteFlag = deleteFlag;
            this._reaServerLabcCode = reaServerLabcCode;
            this._reaCompCode = reaCompCode;
            this._reaLabcCode = reaLabcCode;
            this._labOrderDocID = labOrderDocID;
            this._otherDocNo = otherDocNo;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
        }

        #endregion

        #region Public Properties
        protected string _compBankName;
        protected string _compBankAccount;
        protected string _compTel1;
        protected string _compHotTel1;

        //订单所属供应商的发票信息
        [DataMember]
        [DataDesc(CName = "供应商开户行", ShortCode = "CompBankName", Desc = "供应商开户行", ContextType = SysDic.All, Length = 200)]
        public virtual string CompBankName
        {
            get { return _compBankName; }
            set { _compBankName = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商开户行银行账号", ShortCode = "CompBankAccount", Desc = "供应商开户行银行账号", ContextType = SysDic.All, Length = 200)]
        public virtual string CompBankAccount
        {
            get { return _compBankAccount; }
            set { _compBankAccount = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商联系手机2", ShortCode = "CompTel1", Desc = "供应商联系手机2", ContextType = SysDic.All, Length = 20)]
        public virtual string CompTel1
        {
            get { return _compTel1; }
            set { _compTel1 = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商热线电话2", ShortCode = "CompHotTel1", Desc = "供应商热线电话2", ContextType = SysDic.All)]
        public virtual string CompHotTel1
        {
            get { return _compHotTel1; }
            set { _compHotTel1 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实验室订货总单ID", ShortCode = "LabOrderDocID", Desc = "实验室订货总单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LabOrderDocID
        {
            get { return _labOrderDocID; }
            set { _labOrderDocID = value; }
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
        [DataDesc(CName = "实验室机构码", ShortCode = "ReaLabcCode", Desc = "实验室机构码", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaLabcCode
        {
            get { return _reaLabcCode; }
            set
            {
                _reaLabcCode = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实验室ID", ShortCode = "LabcID", Desc = "实验室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LabcID
        {
            get { return _labcID; }
            set { _labcID = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室名称", ShortCode = "LabcName", Desc = "实验室", ContextType = SysDic.All, Length = 100)]
        public virtual string LabcName
        {
            get { return _labcName; }
            set
            {
                _labcName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "实验室平台机构码", ShortCode = "ReaServerLabcCode", Desc = "实验室平台机构码", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerLabcCode
        {
            get { return _reaServerLabcCode; }
            set
            {
                _reaServerLabcCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "SaleDocNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocNo
        {
            get { return _saleDocNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SaleDocNo", value, value.ToString());
                _saleDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrderDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderDocID
        {
            get { return _orderDocID; }
            set { _orderDocID = value; }
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
        [DataDesc(CName = "", ShortCode = "OrderDocNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDocNo
        {
            get { return _orderDocNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OrderDocNo", value, value.ToString());
                _orderDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订货方ID", ShortCode = "CompID", Desc = "订货方ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompID
        {
            get { return _compID; }
            set { _compID = value; }
        }

        [DataMember]
        [DataDesc(CName = "订货方名称", ShortCode = "CompanyName", Desc = "订货方名称", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "供货商平台编码", ShortCode = "ReaServerCompCode", Desc = "供货商平台编码", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerCompCode
        {
            get { return _reaServerCompCode; }
            set
            {
                _reaServerCompCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "UrgentFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UrgentFlag
        {
            get { return _urgentFlag; }
            set { _urgentFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UrgentFlagName", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string UrgentFlagName
        {
            get { return _urgentFlagName; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for UrgentFlagName", value, value.ToString());
                _urgentFlagName = value;
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
        [DataDesc(CName = "", ShortCode = "StatusName", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string StatusName
        {
            get { return _statusName; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for StatusName", value, value.ToString());
                _statusName = value;
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
        [DataDesc(CName = "", ShortCode = "UserName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UserName
        {
            get { return _userName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
                _userName = value;
            }
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
        [DataDesc(CName = "", ShortCode = "IOFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Source", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Source
        {
            get { return _source; }
            set { _source = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AccepterTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AccepterTime
        {
            get { return _accepterTime; }
            set { _accepterTime = value; }
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
        [DataDesc(CName = "", ShortCode = "LabAddress", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string LabAddress
        {
            get { return _labAddress; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for LabAddress", value, value.ToString());
                _labAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabContact", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LabContact
        {
            get { return _labContact; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LabContact", value, value.ToString());
                _labContact = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabTel", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string LabTel
        {
            get { return _labTel; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for LabTel", value, value.ToString());
                _labTel = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabHotTel", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string LabHotTel
        {
            get { return _labHotTel; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for LabHotTel", value, value.ToString());
                _labHotTel = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabFox", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string LabFox
        {
            get { return _labFox; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for LabFox", value, value.ToString());
                _labFox = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabEmail", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LabEmail
        {
            get { return _labEmail; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LabEmail", value, value.ToString());
                _labEmail = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabWebAddress", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LabWebAddress
        {
            get { return _labWebAddress; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LabWebAddress", value, value.ToString());
                _labWebAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompAddress", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string CompAddress
        {
            get { return _compAddress; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for CompAddress", value, value.ToString());
                _compAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompContact", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CompContact
        {
            get { return _compContact; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CompContact", value, value.ToString());
                _compContact = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompTel", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string CompTel
        {
            get { return _compTel; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for CompTel", value, value.ToString());
                _compTel = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompHotTel", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string CompHotTel
        {
            get { return _compHotTel; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for CompHotTel", value, value.ToString());
                _compHotTel = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompFox", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string CompFox
        {
            get { return _compFox; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for CompFox", value, value.ToString());
                _compFox = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompEmail", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CompEmail
        {
            get { return _compEmail; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CompEmail", value, value.ToString());
                _compEmail = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompWebAddress", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CompWebAddress
        {
            get { return _compWebAddress; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CompWebAddress", value, value.ToString());
                _compWebAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Checker", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Checker
        {
            get { return _checker; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
                _checker = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Sender", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Sender
        {
            get { return _sender; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Sender", value, value.ToString());
                _sender = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Receiver", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Receiver
        {
            get { return _receiver; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Receiver", value, value.ToString());
                _receiver = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InvoiceReceiver", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string InvoiceReceiver
        {
            get { return _invoiceReceiver; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for InvoiceReceiver", value, value.ToString());
                _invoiceReceiver = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReceiveTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveTime
        {
            get { return _receiveTime; }
            set { _receiveTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SendOutTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SendOutTime
        {
            get { return _sendOutTime; }
            set { _sendOutTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OtherOrderDocNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OtherOrderDocNo
        {
            get { return _otherOrderDocNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OtherOrderDocNo", value, value.ToString());
                _otherOrderDocNo = value;
            }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "冗余,本地供货商ID", ShortCode = "ReaCompID", Desc = "冗余,本地供货商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompID
        {
            get { return _reaCompID; }
            set { _reaCompID = value; }
        }

        [DataMember]
        [DataDesc(CName = "冗余,本地供货商名称", ShortCode = "ReaCompanyName", Desc = "冗余,本地供货商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompanyName
        {
            get { return _reaCompanyName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaCompanyName", value, value.ToString());
                _reaCompanyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CenSaleDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CenSaleDocID
        {
            get { return _cenSaleDocID; }
            set { _cenSaleDocID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptName", Desc = "", ContextType = SysDic.All, Length = 200)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SecAccepterTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SecAccepterTime
        {
            get { return _secAccepterTime; }
            set { _secAccepterTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsSplit", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsSplit
        {
            get { return _isSplit; }
            set { _isSplit = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TotalPrice", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
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
        [DataDesc(CName = "", ShortCode = "DeleteFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
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
        #endregion
    }
    #endregion
}