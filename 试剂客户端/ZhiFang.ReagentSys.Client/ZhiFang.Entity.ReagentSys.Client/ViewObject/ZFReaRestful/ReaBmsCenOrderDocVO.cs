using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject
{
    #region ReaBmsCenOrderDoc

    /// <summary>
    /// ReaBmsCenOrderDoc object for NHibernate mapped table 'Rea_BmsCenOrderDoc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsCenOrderDoc", ShortCode = "ReaBmsCenOrderDoc", Desc = "")]
    public class ReaBmsCenOrderDocVO : BaseEntityService
    {
        #region Member Variables

        protected long _labcID;
        protected string _labcName;
        protected string _orderDocNo;
        protected int _stID;
        protected string _stName;
        protected long _compID;
        protected string _companyName;
        protected long _reaCompID;
        protected string _reaCompanyName;
        protected string _reaServerCompCode;
        protected long _cenOrderDocID;
        protected long _userID;
        protected string _userName;
        protected int _urgentFlag;
        protected string _urgentFlagName;
        protected int _status;
        protected string _statusName;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected int _iOFlag;
        protected string _memo;
        protected DateTime? _reqDeliveryTime;
        protected DateTime? _deliveryTime;
        protected string _labMemo;
        protected string _compMemo;
        protected double _totalPrice;
        protected DateTime? _dataUpdateTime;
        protected long _confirmID;
        protected string _confirm;
        protected DateTime? _confirmTime;
        protected long _checkerID;
        protected string _checker;
        protected DateTime? _checkTime;
        protected int _isThirdFlag;
        protected string _sender;
        protected DateTime? _sendTime;
        protected int _deleteFlag;
        protected long _sSCID;
        protected string _checkMemo;
        protected string _deptName;
        protected string _reaServerLabcCode;
        protected bool _isVerifyProdGoodsNo;
        protected string _reaCompCode;
        protected long _labOrderDocID;
        protected long _deptID;
        protected bool _isHasApproval;
        protected long _approvalID;
        protected string _approvalCName;
        protected DateTime? _approvalTime;
        protected string _approvalMemo;
        protected long _payUserId;
        protected string _payUserCName;
        protected long _payStaus;
        protected DateTime? _payTime;
        protected string _payMemo;

        #endregion

        #region Constructors

        public ReaBmsCenOrderDocVO() { }

        public ReaBmsCenOrderDocVO(long labID, long labcID, string labcName, string orderDocNo, int stID, string stName, long compID, string companyName, long reaCompID, string reaCompanyName, string reaServerCompCode, long cenOrderDocID, long userID, string userName, int urgentFlag, string urgentFlagName, int status, string statusName, DateTime operDate, int printTimes, int iOFlag, string memo, DateTime reqDeliveryTime, DateTime deliveryTime, string labMemo, string compMemo, double totalPrice, DateTime dataAddTime, DateTime dataUpdateTime, long confirmID, string confirm, DateTime confirmTime, long checkerID, string checker, DateTime checkTime, int isThirdFlag, string sender, DateTime sendTime, int deleteFlag, long sSCID, string checkMemo, string deptName, byte[] dataTimeStamp, string reaServerLabcCode, bool isVerifyProdGoodsNo, string reaCompCode, long labOrderDocID, long deptID, bool isHasApproval, long approvalID, string approvalCName, DateTime approvalTime, string approvalMemo, long payUserId, string payUserCName, long payStaus, DateTime payTime, string payMemo)
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
        [DataDesc(CName = "", ShortCode = "LabcID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long LabcID
        {
            get { return _labcID; }
            set { _labcID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabcName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LabcName
        {
            get { return _labcName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LabcName", value, value.ToString());
                _labcName = value;
            }
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
        [DataDesc(CName = "", ShortCode = "StID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int StID
        {
            get { return _stID; }
            set { _stID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StName
        {
            get { return _stName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StName", value, value.ToString());
                _stName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CompID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CompID
        {
            get { return _compID; }
            set { _compID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReaCompID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ReaCompID
        {
            get { return _reaCompID; }
            set { _reaCompID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompanyName", Desc = "", ContextType = SysDic.All, Length = 200)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CenOrderDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CenOrderDocID
        {
            get { return _cenOrderDocID; }
            set { _cenOrderDocID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作人员编号", ShortCode = "UserID", Desc = "操作人员编号", ContextType = SysDic.All, Length = 8)]
        public virtual long UserID
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
                _userName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "紧急标志", ShortCode = "UrgentFlag", Desc = "紧急标志", ContextType = SysDic.All, Length = 4)]
        public virtual int UrgentFlag
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
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for UrgentFlagName", value, value.ToString());
                _urgentFlagName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "单据状态", ShortCode = "Status", Desc = "单据状态", ContextType = SysDic.All, Length = 4)]
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
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for StatusName", value, value.ToString());
                _statusName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作日期", ShortCode = "OperDate", Desc = "操作日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperDate
        {
            get { return _operDate; }
            set { _operDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        [DataDesc(CName = "数据上传标志", ShortCode = "IOFlag", Desc = "数据上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "要求送货时间", ShortCode = "ReqDeliveryTime", Desc = "要求送货时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReqDeliveryTime
        {
            get { return _reqDeliveryTime; }
            set { _reqDeliveryTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "真实送货时间", ShortCode = "DeliveryTime", Desc = "真实送货时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DeliveryTime
        {
            get { return _deliveryTime; }
            set { _deliveryTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "订货方备注", ShortCode = "LabMemo", Desc = "订货方备注", ContextType = SysDic.All, Length = 500)]
        public virtual string LabMemo
        {
            get { return _labMemo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for LabMemo", value, value.ToString());
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
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for CompMemo", value, value.ToString());
                _compMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总价", ShortCode = "TotalPrice", Desc = "总价", ContextType = SysDic.Number, Length = 8)]
        public virtual double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ConfirmID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ConfirmID
        {
            get { return _confirmID; }
            set { _confirmID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Confirm", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Confirm
        {
            get { return _confirm; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Confirm", value, value.ToString());
                _confirm = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ConfirmTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ConfirmTime
        {
            get { return _confirmTime; }
            set { _confirmTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckerID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CheckerID
        {
            get { return _checkerID; }
            set { _checkerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Checker", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Checker
        {
            get { return _checker; }
            set
            {
                if (value != null && value.Length > 200)
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
        [DataDesc(CName = "是否写入第三方系统标志", ShortCode = "IsThirdFlag", Desc = "是否写入第三方系统标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IsThirdFlag
        {
            get { return _isThirdFlag; }
            set { _isThirdFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "送货人", ShortCode = "Sender", Desc = "送货人", ContextType = SysDic.All, Length = 50)]
        public virtual string Sender
        {
            get { return _sender; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Sender", value, value.ToString());
                _sender = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送货时间", ShortCode = "SendTime", Desc = "送货时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "删除标记", ShortCode = "DeleteFlag", Desc = "删除标记", ContextType = SysDic.All, Length = 4)]
        public virtual int DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SSCID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long SSCID
        {
            get { return _sSCID; }
            set { _sSCID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckMemo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckMemo
        {
            get { return _checkMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckMemo", value, value.ToString());
                _checkMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "科室名称", ShortCode = "DeptName", Desc = "科室名称", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "", ShortCode = "ReaServerLabcCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerLabcCode
        {
            get { return _reaServerLabcCode; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaServerLabcCode", value, value.ToString());
                _reaServerLabcCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsVerifyProdGoodsNo", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsVerifyProdGoodsNo
        {
            get { return _isVerifyProdGoodsNo; }
            set { _isVerifyProdGoodsNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompCode
        {
            get { return _reaCompCode; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaCompCode", value, value.ToString());
                _reaCompCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabOrderDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long LabOrderDocID
        {
            get { return _labOrderDocID; }
            set { _labOrderDocID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsHasApproval", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsHasApproval
        {
            get { return _isHasApproval; }
            set { _isHasApproval = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ApprovalID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ApprovalID
        {
            get { return _approvalID; }
            set { _approvalID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ApprovalCName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ApprovalCName
        {
            get { return _approvalCName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ApprovalCName", value, value.ToString());
                _approvalCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ApprovalTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApprovalTime
        {
            get { return _approvalTime; }
            set { _approvalTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ApprovalMemo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ApprovalMemo
        {
            get { return _approvalMemo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ApprovalMemo", value, value.ToString());
                _approvalMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PayUserId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long PayUserId
        {
            get { return _payUserId; }
            set { _payUserId = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PayUserCName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PayUserCName
        {
            get { return _payUserCName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PayUserCName", value, value.ToString());
                _payUserCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PayStaus", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long PayStaus
        {
            get { return _payStaus; }
            set { _payStaus = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PayTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PayTime
        {
            get { return _payTime; }
            set { _payTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PayMemo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string PayMemo
        {
            get { return _payMemo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for PayMemo", value, value.ToString());
                _payMemo = value;
            }
        }


        #endregion
    }
    #endregion
}