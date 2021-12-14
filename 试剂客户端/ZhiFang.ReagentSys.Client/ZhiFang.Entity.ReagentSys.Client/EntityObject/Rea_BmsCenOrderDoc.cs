using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaReaBmsCenOrderDoc

    /// <summary>
    /// BmsCenOrderDoc object for NHibernate mapped table 'BmsCenOrderDoc'.
    /// int?及double?添加上[JsonConverter(typeof(JsonConvertClass))]时,在上传平台进行序列化及反序列化时值为空
    /// </summary>
    [DataContract]
    [DataDesc(CName = "订货总单表", ClassCName = "ReaReaBmsCenOrderDoc", ShortCode = "订货总单表", Desc = "")]
    public class ReaBmsCenOrderDoc : BaseEntityService
    {
        #region Member Variables

        protected string _orderDocNo;
        protected int? _stID;
        protected string _stName;
        protected string _companyName;
        protected long? _userID;
        protected string _userName;
        protected int? _urgentFlag;
        protected string _urgentFlagName;
        protected int _status;
        protected string _statusName;
        protected DateTime? _operDate;
        protected int? _printTimes;
        protected int? _iOFlag;
        protected string _memo;
        protected string _labcName;
        protected string _compMemo;
        protected string _labMemo;
        protected DateTime? _deliveryTime;
        protected DateTime? _reqDeliveryTime;
        protected double? _totalPrice;
        protected DateTime? _dataUpdateTime;
        protected long? _checkerID;
        protected string _checker;
        protected DateTime? _checkTime;
        protected long? _confirmID;
        protected string _confirm;
        protected DateTime? _confirmTime;
        protected string _sender;
        protected DateTime? _sendTime;
        protected int? _isThirdFlag;
        protected int? _deleteFlag;
        protected long? _compID;
        protected long? _reaCompID;
        protected string _reaCompanyName;
        protected long? _cenOrderDocID;
        protected string _checkMemo;
        protected long? _deptID;
        protected string _deptName;
        protected long? _labcID;
        protected long? _sSCID;
        protected string _reaServerCompCode;
        protected string _reaServerLabcCode;
        protected bool? _isVerifyProdGoodsNo;
        protected string _reaCompCode;
        protected long? _labOrderDocID;
        protected bool? _isHasApproval;
        protected long? _approvalID;
        protected string _approvalCName;
        protected DateTime? _approvalTime;
        protected string _approvalMemo;
        protected long? _payUserId;
        protected long? _payStaus;
        protected string _payUserCName;
        protected DateTime? _payTime;
        protected string _payMemo;
        protected int _supplyStatus = 1;//默认1，未供货
        #endregion

        #region Constructors

        public ReaBmsCenOrderDoc() { }
        public ReaBmsCenOrderDoc(long labID, long labcID, string labcName, string orderDocNo, int stID, string stName, long compID, string companyName, long reaCompID, string reaCompanyName, string reaServerCompCode, long cenOrderDocID, long userID, string userName, int urgentFlag, string urgentFlagName, int status, string statusName, DateTime operDate, int printTimes, int iOFlag, string memo, DateTime reqDeliveryTime, DateTime deliveryTime, string labMemo, string compMemo, double totalPrice, DateTime dataAddTime, DateTime dataUpdateTime, long confirmID, string confirm, DateTime confirmTime, long checkerID, string checker, DateTime checkTime, int isThirdFlag, string sender, DateTime sendTime, int deleteFlag, long sSCID, string checkMemo, string deptName, byte[] dataTimeStamp, string reaServerLabcCode, bool isVerifyProdGoodsNo, string reaCompCode, long labOrderDocID, long deptID, bool isHasApproval, long approvalID, string approvalCName, DateTime approvalTime, string approvalMemo, long payUserId, string payUserCName, long payStaus, DateTime payTime, string payMemo)
        {
            this._labID = labID;
            this._labcID = labcID;
            this._labcName = labcName;
            this._orderDocNo = orderDocNo;
            this._stID = stID;
            this._stName = stName;
            this._compID = compID;
            this._companyName = companyName;
            this._reaCompID = reaCompID;
            this._reaCompanyName = reaCompanyName;
            this._reaServerCompCode = reaServerCompCode;
            this._cenOrderDocID = cenOrderDocID;
            this._userID = userID;
            this._userName = userName;
            this._urgentFlag = urgentFlag;
            this._urgentFlagName = urgentFlagName;
            this._status = status;
            this._statusName = statusName;
            this._operDate = operDate;
            this._printTimes = printTimes;
            this._iOFlag = iOFlag;
            this._memo = memo;
            this._reqDeliveryTime = reqDeliveryTime;
            this._deliveryTime = deliveryTime;
            this._labMemo = labMemo;
            this._compMemo = compMemo;
            this._totalPrice = totalPrice;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._confirmID = confirmID;
            this._confirm = confirm;
            this._confirmTime = confirmTime;
            this._checkerID = checkerID;
            this._checker = checker;
            this._checkTime = checkTime;
            this._isThirdFlag = isThirdFlag;
            this._sender = sender;
            this._sendTime = sendTime;
            this._deleteFlag = deleteFlag;
            this._sSCID = sSCID;
            this._checkMemo = checkMemo;
            this._deptName = deptName;
            this._dataTimeStamp = dataTimeStamp;
            this._reaServerLabcCode = reaServerLabcCode;
            this._isVerifyProdGoodsNo = isVerifyProdGoodsNo;
            this._reaCompCode = reaCompCode;
            this._labOrderDocID = labOrderDocID;
            this._deptID = deptID;
            this._isHasApproval = isHasApproval;
            this._approvalID = approvalID;
            this._approvalCName = approvalCName;
            this._approvalTime = approvalTime;
            this._approvalMemo = approvalMemo;
            this._payUserId = payUserId;
            this._payUserCName = payUserCName;
            this._payStaus = payStaus;
            this._payTime = payTime;
            this._payMemo = payMemo;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实验室订货总单ID", ShortCode = "LabOrderDocID", Desc = "实验室订货总单ID", ContextType = SysDic.All)]
        public virtual long? LabOrderDocID
        {
            get { return _labOrderDocID; }
            set { _labOrderDocID = value; }
        }
        [DataMember]
        [DataDesc(CName = "ReaCompCode", ShortCode = "ReaCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompCode
        {
            get { return _reaCompCode; }
            set
            {
                _reaCompCode = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "是否强制验证供应商货品编码", ShortCode = "IsVerifyProdGoodsNo", Desc = "是否强制验证供应商货品编码", ContextType = SysDic.All, Length = 1)]
        public virtual bool? IsVerifyProdGoodsNo
        {
            get { return _isVerifyProdGoodsNo; }
            set { _isVerifyProdGoodsNo = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订货方ID", ShortCode = "LabcID", Desc = "订货方ID", ContextType = SysDic.All)]
        public virtual long? LabcID
        {
            get { return _labcID; }
            set { _labcID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "SSCID", ShortCode = "SSCID", Desc = "", ContextType = SysDic.All)]
        public virtual long? SSCID
        {
            get { return _sSCID; }
            set { _sSCID = value; }
        }

        [DataMember]
        [DataDesc(CName = "订货总单号", ShortCode = "OrderDocNo", Desc = "订货总单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDocNo
        {
            get { return _orderDocNo; }
            set
            {
                _orderDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "保管帐ID", ShortCode = "StID", Desc = "保管帐ID", ContextType = SysDic.All)]
        public virtual int? StID
        {
            get { return _stID; }
            set { _stID = value; }
        }

        [DataMember]
        [DataDesc(CName = "保管帐名称", ShortCode = "StName", Desc = "保管帐名称", ContextType = SysDic.All, Length = 50)]
        public virtual string StName
        {
            get { return _stName; }
            set
            {
                _stName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供应商ID", ShortCode = "CompID", Desc = "供应商ID", ContextType = SysDic.All)]
        public virtual long? CompID
        {
            get { return _compID; }
            set { _compID = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "CompanyName", Desc = "供应商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "操作人员编号", ShortCode = "UserID", Desc = "操作人员编号", ContextType = SysDic.All)]
        public virtual long? UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

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
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "紧急标志", ShortCode = "UrgentFlag", Desc = "紧急标志", ContextType = SysDic.All)]
        public virtual int? UrgentFlag
        {
            get { return _urgentFlag; }
            set { _urgentFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "紧急标志描述", ShortCode = "UrgentFlagName", Desc = "紧急标志描述", ContextType = SysDic.All, Length = 10)]
        public virtual string UrgentFlagName
        {
            get { return _urgentFlagName; }
            set
            {
                _urgentFlagName = value;
            }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单据状态", ShortCode = "Status", Desc = "单据状态", ContextType = SysDic.All)]
        public virtual int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "单据状态描述", ShortCode = "StatusName", Desc = "单据状态描述", ContextType = SysDic.All, Length = 10)]
        public virtual string StatusName
        {
            get { return _statusName; }
            set
            {
                _statusName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作日期", ShortCode = "OperDate", Desc = "操作日期", ContextType = SysDic.DateTime)]
        public virtual DateTime? OperDate
        {
            get { return _operDate; }
            set { _operDate = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All)]
        public virtual int? PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据上传标志", ShortCode = "IOFlag", Desc = "数据上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int? IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据写入第三方系统标志", ShortCode = "IsThirdFlag", Desc = "数据写入第三方系统标志", ContextType = SysDic.All, Length = 4)]
        public virtual int? IsThirdFlag
        {
            get { return _isThirdFlag; }
            set { _isThirdFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "订货方名称", ShortCode = "LabcName", Desc = "订货方名称", ContextType = SysDic.All, Length = 100)]
        public virtual string LabcName
        {
            get { return _labcName; }
            set
            {
                _labcName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "订货方备注", ShortCode = "LabMemo", Desc = "订货方备注", ContextType = SysDic.All, Length = 500)]
        public virtual string LabMemo
        {
            get { return _labMemo; }
            set
            {
                _labMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "供货方备注", ShortCode = "CompMemo", Desc = "供货方备注", ContextType = SysDic.All, Length = 500)]
        public virtual string CompMemo
        {
            get { return _compMemo; }
            set
            {
                _compMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "要求送货时间", ShortCode = "ReqDeliveryTime", Desc = "要求送货时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? ReqDeliveryTime
        {
            get { return _reqDeliveryTime; }
            set { _reqDeliveryTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "真实送货时间", ShortCode = "DeliveryTime", Desc = "真实送货时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DeliveryTime
        {
            get { return _deliveryTime; }
            set { _deliveryTime = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总价", ShortCode = "TotalPrice", Desc = "总价", ContextType = SysDic.All)]
        public virtual double? TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人员编号", ShortCode = "CheckerID", Desc = "审核人员编号", ContextType = SysDic.All)]
        public virtual long? CheckerID
        {
            get { return _checkerID; }
            set { _checkerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人员姓名", ShortCode = "Checker", Desc = "审核人员姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string Checker
        {
            get { return _checker; }
            set { _checker = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核意见", ShortCode = "CheckMemo", Desc = "审核意见", ContextType = SysDic.All)]
        public virtual string CheckMemo
        {
            get { return _checkMemo; }
            set { _checkMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "确认人编号", ShortCode = "ConfirmID", Desc = "确认人编号", ContextType = SysDic.All)]
        public virtual long? ConfirmID
        {
            get { return _confirmID; }
            set { _confirmID = value; }
        }

        [DataMember]
        [DataDesc(CName = "确认人", ShortCode = "Confirm", Desc = "确认人", ContextType = SysDic.All, Length = 50)]
        public virtual string Confirm
        {
            get { return _confirm; }
            set { _confirm = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "确认时间", ShortCode = "ConfirmTime", Desc = "确认时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? ConfirmTime
        {
            get { return _confirmTime; }
            set { _confirmTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "发货人", ShortCode = "Sender", Desc = "发货人", ContextType = SysDic.All, Length = 50)]
        public virtual string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发货时间", ShortCode = "SendTime", Desc = "发货时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "删除标记", ShortCode = "DeleteFlag", Desc = "删除标记", ContextType = SysDic.All, Length = 8)]
        public virtual int? DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地供应商ID", ShortCode = "ReaCompID", Desc = "本地供应商ID", ContextType = SysDic.All)]
        public virtual long? ReaCompID
        {
            get { return _reaCompID; }
            set { _reaCompID = value; }
        }

        [DataMember]
        [DataDesc(CName = "本地供应商名称", ShortCode = "ReaCompanyName", Desc = "供应商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompanyName
        {
            get { return _reaCompanyName; }
            set
            {
                _reaCompanyName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "部门ID", ShortCode = "DeptID", Desc = "部门ID", ContextType = SysDic.All)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }
        [DataMember]
        [DataDesc(CName = "科室名称", ShortCode = "DeptName", Desc = "科室名称", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "本地订单的平台主单记录ID", ShortCode = "CenOrderDocID", Desc = "本地订单的平台主单记录ID", ContextType = SysDic.All)]
        public virtual long? CenOrderDocID
        {
            get { return _cenOrderDocID; }
            set { _cenOrderDocID = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商平台构码", ShortCode = "ReaServerCompCode", Desc = "供应商平台构码", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerCompCode
        {
            get { return _reaServerCompCode; }
            set
            {
                _reaServerCompCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "订货方所属机构平台编码", ShortCode = "ReaServerLabcCode", Desc = "订货方所属机构平台编码", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerLabcCode
        {
            get { return _reaServerLabcCode; }
            set
            {
                _reaServerLabcCode = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订单是否需要审批", ShortCode = "IsHasApproval", Desc = "订单是否需要审批", ContextType = SysDic.All)]
        public virtual bool? IsHasApproval
        {
            get { return _isHasApproval; }
            set { _isHasApproval = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批人编码", ShortCode = "ApprovalID", Desc = "审批人编码", ContextType = SysDic.All)]
        public virtual long? ApprovalID
        {
            get { return _approvalID; }
            set { _approvalID = value; }
        }
        [DataMember]
        [DataDesc(CName = "审批人", ShortCode = "ApprovalCName", Desc = "审批人", ContextType = SysDic.All, Length = 200)]
        public virtual string ApprovalCName
        {
            get { return _approvalCName; }
            set
            {
                _approvalCName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批时间", ShortCode = "ApprovalTime", Desc = "审批时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? ApprovalTime
        {
            get { return _approvalTime; }
            set { _approvalTime = value; }
        }
        [DataMember]
        [DataDesc(CName = "审批意见", ShortCode = "ApprovalCName", Desc = "审批意见", ContextType = SysDic.All, Length = 500)]
        public virtual string ApprovalMemo
        {
            get { return _approvalMemo; }
            set
            {
                _approvalMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "付款人ID", ShortCode = "PayUserId", Desc = "付款人ID", ContextType = SysDic.All)]
        public virtual long? PayUserId
        {
            get { return _payUserId; }
            set { _payUserId = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "付款状态", ShortCode = "PayStaus", Desc = "付款状态", ContextType = SysDic.All)]
        public virtual long? PayStaus
        {
            get { return _payStaus; }
            set { _payStaus = value; }
        }
        [DataMember]
        [DataDesc(CName = "付款登记人", ShortCode = "PayUserCName", Desc = "付款登记人", ContextType = SysDic.All, Length = 50)]
        public virtual string PayUserCName
        {
            get { return _payUserCName; }
            set
            {
                _payUserCName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "付款登记日期", ShortCode = "PayTime", Desc = "付款登记日期", ContextType = SysDic.DateTime)]
        public virtual DateTime? PayTime
        {
            get { return _payTime; }
            set { _payTime = value; }
        }
        [DataMember]
        [DataDesc(CName = "付款备注", ShortCode = "PayMemo", Desc = "付款备注", ContextType = SysDic.All, Length = 500)]
        public virtual string PayMemo
        {
            get { return _payMemo; }
            set
            {
                _payMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "供货状态", ShortCode = "SupplyStatus", Desc = "供货状态", ContextType = SysDic.All, Length = 4)]
        public virtual int SupplyStatus
        {
            get { return _supplyStatus; }
            set
            {
                _supplyStatus = value;
            }
        }
        #endregion

        #region 自定义属性
        //实验室的联系信息取部门还是取哪?
        protected string _labCName;
        protected string _labAddress;
        protected string _labContact;
        protected string _labTel;
        protected string _labFox;
        protected string _compBankName;
        protected string _compBankAccount;
        protected string _compAddress;
        protected string _compTel;
        protected string _compTel1;
        protected string _compHotTel;
        protected string _compHotTel1;
        protected string _compFox;
        protected string _compContact;
        protected string _compEmail;

        [DataMember]
        [DataDesc(CName = "实验室名称", ShortCode = "LabCName", Desc = "实验室名称", ContextType = SysDic.All, Length = 200)]
        public virtual string LabCName
        {
            get { return _labCName; }
            set
            {
                _labCName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "实验室联系地址", ShortCode = "LabAddress", Desc = "实验室联系地址", ContextType = SysDic.All, Length = 200)]
        public virtual string LabAddress
        {
            get { return _labAddress; }
            set { _labAddress = value; }
        }
        [DataMember]
        [DataDesc(CName = "实验室联系人", ShortCode = "LabContact", Desc = "实验室联系人", ContextType = SysDic.All, Length = 50)]
        public virtual string LabContact
        {
            get { return _labContact; }
            set { _labContact = value; }
        }
        [DataMember]
        [DataDesc(CName = "实验室联系电话", ShortCode = "LabTel", Desc = "实验室联系电话", ContextType = SysDic.All, Length = 20)]
        public virtual string LabTel
        {
            get { return _labTel; }
            set { _labTel = value; }
        }
        [DataMember]
        [DataDesc(CName = "实验室传真", ShortCode = "LabFox", Desc = "实验室传真", ContextType = SysDic.All, Length = 50)]
        public virtual string LabFox
        {
            get { return _labFox; }
            set { _labFox = value; }
        }

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
        [DataDesc(CName = "供应商联系地址", ShortCode = "CompAddress", Desc = "供应商联系地址", ContextType = SysDic.All, Length = 200)]
        public virtual string CompAddress
        {
            get { return _compAddress; }
            set { _compAddress = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商联系手机", ShortCode = "CompTel", Desc = "供应商联系手机", ContextType = SysDic.All, Length = 20)]
        public virtual string CompTel
        {
            get { return _compTel; }
            set { _compTel = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商联系手机2", ShortCode = "CompTel1", Desc = "供应商联系手机2", ContextType = SysDic.All, Length = 20)]
        public virtual string CompTel1
        {
            get { return _compTel1; }
            set { _compTel1 = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商热线电话", ShortCode = "CompHotTel", Desc = "供应商热线电话", ContextType = SysDic.All, Length = 20)]
        public virtual string CompHotTel
        {
            get { return _compHotTel; }
            set { _compHotTel = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商热线电话2", ShortCode = "CompHotTel1", Desc = "供应商热线电话2", ContextType = SysDic.All)]
        public virtual string CompHotTel1
        {
            get { return _compHotTel1; }
            set { _compHotTel1 = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商传真", ShortCode = "CompFox", Desc = "供应商传真", ContextType = SysDic.All)]
        public virtual string CompFox
        {
            get { return _compFox; }
            set { _compFox = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商联系人", ShortCode = "CompContact", Desc = "供应商联系人", ContextType = SysDic.All)]
        public virtual string CompContact
        {
            get { return _compContact; }
            set { _compContact = value; }
        }
        [DataMember]
        [DataDesc(CName = "供应商联系邮箱", ShortCode = "CompEmail", Desc = "供应商联系邮箱", ContextType = SysDic.All)]
        public virtual string CompEmail
        {
            get { return _compEmail; }
            set { _compEmail = value; }
        }
        #endregion

    }
    #endregion
}