using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PFinanceReceive

    /// <summary>
    /// PFinanceReceive object for NHibernate mapped table 'P_Finance_Receive'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "PFinanceReceive", ShortCode = "PFinanceReceive", Desc = "")]
    public class PFinanceReceive : BaseEntity
    {
        #region Member Variables

        protected long? _pClientID;
        protected string _pClientName;
        protected long? _payOrgID;
        protected string _payOrgName;
        protected double _receiveAmount;
        protected double _splitAmount;
        protected DateTime? _receiveDate;
        protected string _receiveMemo;
        protected long? _incomeTypeID;
        protected string _incomeTypeName;
        protected long? _incomeClassID;
        protected string _incomeClassName;
        protected string _fundAccount;
        protected long? _inputerID;
        protected string _inputerName;
        protected string _accountsYear;
        protected string _editMemoBusiness;
        protected string _businessMemo;
        protected string _yearMonth;
        protected long? _compnameID;
        protected string _componeName;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected long _status;
        protected long? _reviewManID;
        protected string _reviewMan;
        protected DateTime? _reviewDate;
        protected string _reviewInfo;

        protected long? _contrastId;
        protected string _contrastCName;
        protected long? _checkId;
        protected string _checkCName;
        #endregion

        #region Constructors

        public PFinanceReceive() { }

        public PFinanceReceive(long labID, long pClientID, string pClientName, long payOrgID, string payOrgName, double receiveAmount, double splitAmount, DateTime receiveDate, string receiveMemo, long incomeTypeID, string incomeTypeName, long incomeClassID, string incomeClassName, string fundAccount, long inputerID, string inputerName, string accountsYear, string editMemoBusiness, string businessMemo, string yearMonth, long compnameID, string componeName, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, long status, long reviewManID, string reviewMan, DateTime reviewDate, string reviewInfo, long? contrastId, string contrastCName, long? checkId, string checkCName)
        {
            this._labID = labID;
            this._pClientID = pClientID;
            this._pClientName = pClientName;
            this._payOrgID = payOrgID;
            this._payOrgName = payOrgName;
            this._receiveAmount = receiveAmount;
            this._splitAmount = splitAmount;
            this._receiveDate = receiveDate;
            this._receiveMemo = receiveMemo;
            this._incomeTypeID = incomeTypeID;
            this._incomeTypeName = incomeTypeName;
            this._incomeClassID = incomeClassID;
            this._incomeClassName = incomeClassName;
            this._fundAccount = fundAccount;
            this._inputerID = inputerID;
            this._inputerName = inputerName;
            this._accountsYear = accountsYear;
            this._editMemoBusiness = editMemoBusiness;
            this._businessMemo = businessMemo;
            this._yearMonth = yearMonth;
            this._compnameID = compnameID;
            this._componeName = componeName;
            this._dispOrder = dispOrder;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._status = status;
            this._reviewManID = reviewManID;
            this._reviewMan = reviewMan;
            this._reviewDate = reviewDate;
            this._reviewInfo = reviewInfo;
            this._contrastId = contrastId;
            this._contrastCName = contrastCName;
            this._checkId = checkId;
            this._checkCName = checkCName;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "PClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PClientID
        {
            get { return _pClientID; }
            set { _pClientID = value; }
        }

        [DataMember]
        [DataDesc(CName = "客户名称", ShortCode = "PClientName", Desc = "客户名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PClientName
        {
            get { return _pClientName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for PClientName", value, value.ToString());
                _pClientName = value;
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
        [DataDesc(CName = "付款单位名称", ShortCode = "PayOrgName", Desc = "付款单位名称", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "实际收款金额", ShortCode = "ReceiveAmount", Desc = "实际收款金额", ContextType = SysDic.All, Length = 8)]
        public virtual double ReceiveAmount
        {
            get { return _receiveAmount; }
            set { _receiveAmount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "已分配金额", ShortCode = "SplitAmount", Desc = "已分配金额", ContextType = SysDic.All, Length = 8)]
        public virtual double SplitAmount
        {
            get { return _splitAmount; }
            set { _splitAmount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款日期", ShortCode = "ReceiveDate", Desc = "收款日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveDate
        {
            get { return _receiveDate; }
            set { _receiveDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "收款说明", ShortCode = "ReceiveMemo", Desc = "收款说明", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveMemo
        {
            get { return _receiveMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiveMemo", value, value.ToString());
                _receiveMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收入分类ID", ShortCode = "IncomeTypeID", Desc = "收入分类ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? IncomeTypeID
        {
            get { return _incomeTypeID; }
            set { _incomeTypeID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收入类别ID", ShortCode = "IncomeClassID", Desc = "收入类别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? IncomeClassID
        {
            get { return _incomeClassID; }
            set { _incomeClassID = value; }
        }

        [DataMember]
        [DataDesc(CName = "收入类别", ShortCode = "IncomeClassName", Desc = "收入类别", ContextType = SysDic.All, Length = 200)]
        public virtual string IncomeClassName
        {
            get { return _incomeClassName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for IncomeClassName", value, value.ToString());
                _incomeClassName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "资金帐户", ShortCode = "FundAccount", Desc = "资金帐户", ContextType = SysDic.All, Length = 200)]
        public virtual string FundAccount
        {
            get { return _fundAccount; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for FundAccount", value, value.ToString());
                _fundAccount = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "录入人ID", ShortCode = "InputerID", Desc = "录入人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? InputerID
        {
            get { return _inputerID; }
            set { _inputerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "录入人姓名", ShortCode = "InputerName", Desc = "录入人姓名", ContextType = SysDic.All, Length = 200)]
        public virtual string InputerName
        {
            get { return _inputerName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for InputerName", value, value.ToString());
                _inputerName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "核算年份", ShortCode = "AccountsYear", Desc = "核算年份", ContextType = SysDic.All, Length = 200)]
        public virtual string AccountsYear
        {
            get { return _accountsYear; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for AccountsYear", value, value.ToString());
                _accountsYear = value;
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
        [DataDesc(CName = "商务备注", ShortCode = "BusinessMemo", Desc = "商务备注", ContextType = SysDic.All, Length = 200)]
        public virtual string BusinessMemo
        {
            get { return _businessMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for BusinessMemo", value, value.ToString());
                _businessMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "月份", ShortCode = "YearMonth", Desc = "月份", ContextType = SysDic.All, Length = 200)]
        public virtual string YearMonth
        {
            get { return _yearMonth; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for YearMonth", value, value.ToString());
                _yearMonth = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "执行公司ID", ShortCode = "CompnameID", Desc = "执行公司ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompnameID
        {
            get { return _compnameID; }
            set { _compnameID = value; }
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
        [DataDesc(CName = "状态 Status：0暂存、1已审核", ShortCode = "Status", Desc = "状态 Status：0暂存、1已审核", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
        {
            get { return _status; }
            set { _status = value; }
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

        #region
        /// <summary>
        /// 对此业务实体操作时的描述
        /// </summary>
        [DataMember]
        public virtual string OperationMemo { get; set; }
        #endregion
    }
    #endregion
}