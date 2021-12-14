using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PReceivePlan

	/// <summary>
	/// PReceivePlan object for NHibernate mapped table 'P_ReceivePlan'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "收款计划", ClassCName = "PReceivePlan", ShortCode = "PReceivePlan", Desc = "收款计划")]
	public class PReceivePlan : BaseEntity
	{
		#region Member Variables
		
        protected long? _pPReceivePlanID;
        protected long? _pContractID;
        protected string _pContractName;
        protected long? _pClientID;
        protected string _pClientName;
        protected long? _payOrgID;
        protected string _payOrgName;
        protected long? _compnameID;
        protected string _componeName;
        protected long? _receiveGradationID;
        protected string _receiveGradationName;
        protected DateTime? _receiveDate;
        protected long _status;
        protected double _receivePlanAmount;
        protected double _receiveAmount;
        protected double _unReceiveAmount;
        protected string _receiveDept;
        protected long? _receiveManID;
        protected string _receiveManName;
        protected string _receiveMemo;
        protected DateTime? _expectReceiveDate;
        protected string _accountsYear;
        protected string _yearMonth;
        protected string _editMemoBusiness;
        protected string _businessMemo;
        protected long? _inputerID;
        protected string _inputerName;
        protected long? _reviewManID;
        protected string _reviewMan;
        protected DateTime? _reviewDate;
        protected string _reviewInfo;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected DateTime? _ContractSignDateTime;


        #endregion

        #region Constructors

        public PReceivePlan() { }

		public PReceivePlan( long labID, long pPReceivePlanID, long pContractID, string pContractName, long pClientID, string pClientName, long payOrgID, string payOrgName, long compnameID, string componeName, long receiveGradationID, string receiveGradationName, DateTime receiveDate, long status, double receivePlanAmount, double receiveAmount, double unReceiveAmount, string receiveDept, long receiveManID, string receiveManName, string receiveMemo, DateTime expectReceiveDate, string accountsYear, string yearMonth, string editMemoBusiness, string businessMemo, long inputerID, string inputerName, long reviewManID, string reviewMan, DateTime reviewDate, string reviewInfo, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._pPReceivePlanID = pPReceivePlanID;
			this._pContractID = pContractID;
			this._pContractName = pContractName;
			this._pClientID = pClientID;
			this._pClientName = pClientName;
			this._payOrgID = payOrgID;
			this._payOrgName = payOrgName;
			this._compnameID = compnameID;
			this._componeName = componeName;
			this._receiveGradationID = receiveGradationID;
			this._receiveGradationName = receiveGradationName;
			this._receiveDate = receiveDate;
			this._status = status;
			this._receivePlanAmount = receivePlanAmount;
			this._receiveAmount = receiveAmount;
			this._unReceiveAmount = unReceiveAmount;
			this._receiveDept = receiveDept;
			this._receiveManID = receiveManID;
			this._receiveManName = receiveManName;
			this._receiveMemo = receiveMemo;
			this._expectReceiveDate = expectReceiveDate;
			this._accountsYear = accountsYear;
			this._yearMonth = yearMonth;
			this._editMemoBusiness = editMemoBusiness;
			this._businessMemo = businessMemo;
			this._inputerID = inputerID;
			this._inputerName = inputerName;
			this._reviewManID = reviewManID;
			this._reviewMan = reviewMan;
			this._reviewDate = reviewDate;
			this._reviewInfo = reviewInfo;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "父收款计划ID", ShortCode = "PPReceivePlanID", Desc = "父收款计划ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PPReceivePlanID
		{
			get { return _pPReceivePlanID; }
			set { _pPReceivePlanID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同ID", ShortCode = "PContractID", Desc = "合同ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PContractID
		{
			get { return _pContractID; }
			set { _pContractID = value; }
		}

        [DataMember]
        [DataDesc(CName = "合同名称", ShortCode = "PContractName", Desc = "合同名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PContractName
		{
			get { return _pContractName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PContractName", value, value.ToString());
				_pContractName = value;
			}
		}

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
				if ( value != null && value.Length > 200)
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
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PayOrgName", value, value.ToString());
				_payOrgName = value;
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
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ComponeName", value, value.ToString());
				_componeName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款阶段ID", ShortCode = "ReceiveGradationID", Desc = "收款阶段ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReceiveGradationID
		{
			get { return _receiveGradationID; }
			set { _receiveGradationID = value; }
		}

        [DataMember]
        [DataDesc(CName = "收款阶段名称", ShortCode = "ReceiveGradationName", Desc = "收款阶段名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveGradationName
		{
			get { return _receiveGradationName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveGradationName", value, value.ToString());
				_receiveGradationName = value;
			}
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计划状态 Status：0执行中、1变更申请、2已变更、3已执行", ShortCode = "Status", Desc = "计划状态 Status：0执行中、1变更申请、2已变更、3已执行", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
		{
			get { return _status; }
			set { _status = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款计划金额", ShortCode = "ReceivePlanAmount", Desc = "收款计划金额", ContextType = SysDic.All, Length = 8)]
        public virtual double ReceivePlanAmount
		{
			get { return _receivePlanAmount; }
			set { _receivePlanAmount = value; }
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
        [DataDesc(CName = "应收金额", ShortCode = "UnReceiveAmount", Desc = "应收金额", ContextType = SysDic.All, Length = 8)]
        public virtual double UnReceiveAmount
		{
			get { return _unReceiveAmount; }
			set { _unReceiveAmount = value; }
		}

        [DataMember]
        [DataDesc(CName = "收款单位", ShortCode = "ReceiveDept", Desc = "收款单位", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveDept
		{
			get { return _receiveDept; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveDept", value, value.ToString());
				_receiveDept = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款负责人ID", ShortCode = "ReceiveManID", Desc = "收款负责人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReceiveManID
		{
			get { return _receiveManID; }
			set { _receiveManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "收款负责人", ShortCode = "ReceiveManName", Desc = "收款负责人", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveManName
		{
			get { return _receiveManName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveManName", value, value.ToString());
				_receiveManName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "收款说明", ShortCode = "ReceiveMemo", Desc = "收款说明", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveMemo
		{
			get { return _receiveMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveMemo", value, value.ToString());
				_receiveMemo = value;
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
        [DataDesc(CName = "核算年份", ShortCode = "AccountsYear", Desc = "核算年份", ContextType = SysDic.All, Length = 200)]
        public virtual string AccountsYear
		{
			get { return _accountsYear; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for AccountsYear", value, value.ToString());
				_accountsYear = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "月份", ShortCode = "YearMonth", Desc = "月份", ContextType = SysDic.All, Length = 200)]
        public virtual string YearMonth
		{
			get { return _yearMonth; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for YearMonth", value, value.ToString());
				_yearMonth = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "修改说明（业务）", ShortCode = "EditMemoBusiness", Desc = "修改说明（业务）", ContextType = SysDic.All, Length = 200)]
        public virtual string EditMemoBusiness
		{
			get { return _editMemoBusiness; }
			set
			{
				if ( value != null && value.Length > 200)
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
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for BusinessMemo", value, value.ToString());
				_businessMemo = value;
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
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for InputerName", value, value.ToString());
				_inputerName = value;
			}
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
				if ( value != null && value.Length > 200)
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
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ReviewInfo", value, value.ToString());
				_reviewInfo = value;
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
        [DataDesc(CName = "合同签署时间", ShortCode = "ContractSignDateTime", Desc = "合同签署时间", ContextType = SysDic.All, Length = 1)]
        public virtual DateTime? ContractSignDateTime
        {
            get { return _ContractSignDateTime; }
            set { _ContractSignDateTime = value; }
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