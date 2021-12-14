using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PLoanBill

	/// <summary>
	/// PLoanBill object for NHibernate mapped table 'P_LoanBill'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "借款单管理", ClassCName = "PLoanBill", ShortCode = "PLoanBill", Desc = "借款单管理")]
	public class PLoanBill : BusinessBase
    {
		#region Member Variables
		
        protected string _loanBillNo;
        protected long? _clientID;
        protected string _clientName;
        protected long? _componeID;
        protected string _componeName;
        protected long? _contractID;
        protected string _contractName;
        protected long? _loanBillTypeID;
        protected string _loanBillTypeName;
        protected double _loanBillAmount;
        protected long? _loanBillContentTypeID;
        protected string _loanBillContentTypeName;
        protected string _loanBillMemo;
        protected long? _deptID;
        protected string _deptName;
        protected long _status;
        protected long? _applyManID;
        protected string _applyMan;
        protected DateTime? _applyDate;
        protected long? _reviewManID;
        protected string _reviewMan;
        protected DateTime? _reviewDate;
        protected string _reviewInfo;
        protected long? _twoReviewManID;
        protected string _twoReviewMan;
        protected DateTime? _twoReviewDate;
        protected string _twoReviewInfo;
        protected long? _threeReviewManID;
        protected string _threeReviewMan;
        protected DateTime? _threeReviewDate;
        protected string _threeReviewInfo;
        protected long? _fourReviewManID;
        protected string _fourReviewMan;
        protected DateTime? _fourReviewDate;
        protected string _fourReviewInfo;
        protected long? _payManID;
        protected string _payManName;
        protected DateTime? _payDate;
        protected string _payDateInfo;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected long? _receiveManID;
        protected string _receiveManName;
        protected DateTime? _receiveDate;
        protected string _receiveManInfo;
        protected long? _receiveTypeID;
        protected string _receiveTypeName;
        protected string _receiveBankInfo;
        private bool _IsSpecially;


        #endregion

        #region Constructors

        public PLoanBill() { }

		public PLoanBill( long labID, string loanBillNo, long clientID, string clientName, long componeID, string componeName, long contractID, string contractName, long loanBillTypeID, string loanBillTypeName, double loanBillAmount, long loanBillContentTypeID, string loanBillContentTypeName, string loanBillMemo, long deptID, string deptName, long status, long applyManID, string applyMan, DateTime applyDate, long reviewManID, string reviewMan, DateTime reviewDate, string reviewInfo, long twoReviewManID, string twoReviewMan, DateTime twoReviewDate, string twoReviewInfo, long threeReviewManID, string threeReviewMan, DateTime threeReviewDate, string threeReviewInfo, long fourReviewManID, string fourReviewMan, DateTime fourReviewDate, string fourReviewInfo, long payManID, string payManName, DateTime payDate, string payDateInfo, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, long receiveManID, string receiveManName, DateTime receiveDate, string receiveManInfo, long receiveTypeID, string receiveTypeName, string receiveBankInfo )
		{
			this._labID = labID;
			this._loanBillNo = loanBillNo;
			this._clientID = clientID;
			this._clientName = clientName;
			this._componeID = componeID;
			this._componeName = componeName;
			this._contractID = contractID;
			this._contractName = contractName;
			this._loanBillTypeID = loanBillTypeID;
			this._loanBillTypeName = loanBillTypeName;
			this._loanBillAmount = loanBillAmount;
			this._loanBillContentTypeID = loanBillContentTypeID;
			this._loanBillContentTypeName = loanBillContentTypeName;
			this._loanBillMemo = loanBillMemo;
			this._deptID = deptID;
			this._deptName = deptName;
			this._status = status;
			this._applyManID = applyManID;
			this._applyMan = applyMan;
			this._applyDate = applyDate;
			this._reviewManID = reviewManID;
			this._reviewMan = reviewMan;
			this._reviewDate = reviewDate;
			this._reviewInfo = reviewInfo;
			this._twoReviewManID = twoReviewManID;
			this._twoReviewMan = twoReviewMan;
			this._twoReviewDate = twoReviewDate;
			this._twoReviewInfo = twoReviewInfo;
			this._threeReviewManID = threeReviewManID;
			this._threeReviewMan = threeReviewMan;
			this._threeReviewDate = threeReviewDate;
			this._threeReviewInfo = threeReviewInfo;
			this._fourReviewManID = fourReviewManID;
			this._fourReviewMan = fourReviewMan;
			this._fourReviewDate = fourReviewDate;
			this._fourReviewInfo = fourReviewInfo;
			this._payManID = payManID;
			this._payManName = payManName;
			this._payDate = payDate;
			this._payDateInfo = payDateInfo;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._receiveManID = receiveManID;
			this._receiveManName = receiveManName;
			this._receiveDate = receiveDate;
			this._receiveManInfo = receiveManInfo;
			this._receiveTypeID = receiveTypeID;
			this._receiveTypeName = receiveTypeName;
			this._receiveBankInfo = receiveBankInfo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "借款单号", ShortCode = "LoanBillNo", Desc = "借款单号", ContextType = SysDic.All, Length = 200)]
        public virtual string LoanBillNo
		{
			get { return _loanBillNo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for LoanBillNo", value, value.ToString());
				_loanBillNo = value;
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
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ClientName", value, value.ToString());
				_clientName = value;
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
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ComponeName", value, value.ToString());
				_componeName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同ID", ShortCode = "ContractID", Desc = "合同ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ContractID
		{
			get { return _contractID; }
			set { _contractID = value; }
		}

        [DataMember]
        [DataDesc(CName = "合同名称", ShortCode = "ContractName", Desc = "合同名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ContractName
		{
			get { return _contractName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ContractName", value, value.ToString());
				_contractName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "借款类型ID", ShortCode = "LoanBillTypeID", Desc = "借款类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LoanBillTypeID
		{
			get { return _loanBillTypeID; }
			set { _loanBillTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "借款类型名称", ShortCode = "LoanBillTypeName", Desc = "借款类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string LoanBillTypeName
		{
			get { return _loanBillTypeName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for LoanBillTypeName", value, value.ToString());
				_loanBillTypeName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "借款金额", ShortCode = "LoanBillAmount", Desc = "借款金额", ContextType = SysDic.All, Length = 8)]
        public virtual double LoanBillAmount
		{
			get { return _loanBillAmount; }
			set { _loanBillAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "借款内容类型ID", ShortCode = "LoanBillContentTypeID", Desc = "借款内容类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LoanBillContentTypeID
		{
			get { return _loanBillContentTypeID; }
			set { _loanBillContentTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "借款内容类型名称", ShortCode = "LoanBillContentTypeName", Desc = "借款内容类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string LoanBillContentTypeName
		{
			get { return _loanBillContentTypeName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for LoanBillContentTypeName", value, value.ToString());
				_loanBillContentTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "借款说明", ShortCode = "LoanBillMemo", Desc = "借款说明", ContextType = SysDic.All, Length = 200)]
        public virtual string LoanBillMemo
		{
			get { return _loanBillMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for LoanBillMemo", value, value.ToString());
				_loanBillMemo = value;
			}
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
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
				_deptName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "借款状态", ShortCode = "Status", Desc = "借款状态", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
		{
			get { return _status; }
			set { _status = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请人ID", ShortCode = "ApplyManID", Desc = "申请人ID", ContextType = SysDic.All, Length = 8)]
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
				if ( value != null && value.Length > 200)
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
        [DataDesc(CName = "1审核人ID", ShortCode = "ReviewManID", Desc = "1审核人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReviewManID
		{
			get { return _reviewManID; }
			set { _reviewManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "1审核人", ShortCode = "ReviewMan", Desc = "1审核人", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "1审核时间", ShortCode = "ReviewDate", Desc = "1审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReviewDate
		{
			get { return _reviewDate; }
			set { _reviewDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "1审核人意见", ShortCode = "ReviewInfo", Desc = "1审核人意见", ContextType = SysDic.All, Length = 500)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "2审核（借款核对）人ID", ShortCode = "TwoReviewManID", Desc = "2审核（借款核对）人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TwoReviewManID
		{
			get { return _twoReviewManID; }
			set { _twoReviewManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "2审核（借款核对）人", ShortCode = "TwoReviewMan", Desc = "2审核（借款核对）人", ContextType = SysDic.All, Length = 200)]
        public virtual string TwoReviewMan
		{
			get { return _twoReviewMan; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for TwoReviewMan", value, value.ToString());
				_twoReviewMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "2审核（借款核对）时间", ShortCode = "TwoReviewDate", Desc = "2审核（借款核对）时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TwoReviewDate
		{
			get { return _twoReviewDate; }
			set { _twoReviewDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "2审核（借款核对）人意见", ShortCode = "TwoReviewInfo", Desc = "2审核（借款核对）人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string TwoReviewInfo
		{
			get { return _twoReviewInfo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for TwoReviewInfo", value, value.ToString());
				_twoReviewInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "3审核（特殊审批）人ID", ShortCode = "ThreeReviewManID", Desc = "3审核（特殊审批）人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ThreeReviewManID
		{
			get { return _threeReviewManID; }
			set { _threeReviewManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "3审核（特殊审批）人", ShortCode = "ThreeReviewMan", Desc = "3审核（特殊审批）人", ContextType = SysDic.All, Length = 200)]
        public virtual string ThreeReviewMan
		{
			get { return _threeReviewMan; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ThreeReviewMan", value, value.ToString());
				_threeReviewMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "3审核（特殊审批）时间", ShortCode = "ThreeReviewDate", Desc = "3审核（特殊审批）时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ThreeReviewDate
		{
			get { return _threeReviewDate; }
			set { _threeReviewDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "3审核（特殊审批）人意见", ShortCode = "ThreeReviewInfo", Desc = "3审核（特殊审批）人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string ThreeReviewInfo
		{
			get { return _threeReviewInfo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ThreeReviewInfo", value, value.ToString());
				_threeReviewInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "4审核（借款复核）人ID", ShortCode = "FourReviewManID", Desc = "4审核（借款复核）人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? FourReviewManID
		{
			get { return _fourReviewManID; }
			set { _fourReviewManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "4审核（借款复核）人", ShortCode = "FourReviewMan", Desc = "4审核（借款复核）人", ContextType = SysDic.All, Length = 200)]
        public virtual string FourReviewMan
		{
			get { return _fourReviewMan; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FourReviewMan", value, value.ToString());
				_fourReviewMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "4审核（借款复核）时间", ShortCode = "FourReviewDate", Desc = "4审核（借款复核）时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FourReviewDate
		{
			get { return _fourReviewDate; }
			set { _fourReviewDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "4审核（借款复核）人意见", ShortCode = "FourReviewInfo", Desc = "4审核（借款复核）人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string FourReviewInfo
		{
			get { return _fourReviewInfo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for FourReviewInfo", value, value.ToString());
				_fourReviewInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "打款负责人ID", ShortCode = "PayManID", Desc = "打款负责人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PayManID
		{
			get { return _payManID; }
			set { _payManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "打款负责人", ShortCode = "PayManName", Desc = "打款负责人", ContextType = SysDic.All, Length = 200)]
        public virtual string PayManName
		{
			get { return _payManName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PayManName", value, value.ToString());
				_payManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "打款时间", ShortCode = "PayDate", Desc = "打款时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PayDate
		{
			get { return _payDate; }
			set { _payDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "打款负责人意见", ShortCode = "PayDateInfo", Desc = "打款负责人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string PayDateInfo
		{
			get { return _payDateInfo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for PayDateInfo", value, value.ToString());
				_payDateInfo = value;
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
        [DataDesc(CName = "领款人ID", ShortCode = "ReceiveManID", Desc = "领款人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReceiveManID
		{
			get { return _receiveManID; }
			set { _receiveManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "领款人姓名", ShortCode = "ReceiveManName", Desc = "领款人姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string ReceiveManName
		{
			get { return _receiveManName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveManName", value, value.ToString());
				_receiveManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "领款时间", ShortCode = "ReceiveDate", Desc = "领款时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveDate
		{
			get { return _receiveDate; }
			set { _receiveDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "领款确认意见", ShortCode = "ReceiveManInfo", Desc = "领款确认意见", ContextType = SysDic.All, Length = 500)]
        public virtual string ReceiveManInfo
		{
			get { return _receiveManInfo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveManInfo", value, value.ToString());
				_receiveManInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "领款方式ID", ShortCode = "ReceiveTypeID", Desc = "领款方式ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReceiveTypeID
		{
			get { return _receiveTypeID; }
			set { _receiveTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "领款方式", ShortCode = "ReceiveTypeName", Desc = "领款方式", ContextType = SysDic.All, Length = 100)]
        public virtual string ReceiveTypeName
		{
			get { return _receiveTypeName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveTypeName", value, value.ToString());
				_receiveTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "领款人银行备注说明", ShortCode = "ReceiveBankInfo", Desc = "领款人银行备注说明", ContextType = SysDic.All, Length = 500)]
        public virtual string ReceiveBankInfo
		{
			get { return _receiveBankInfo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveBankInfo", value, value.ToString());
				_receiveBankInfo = value;
			}
		}
        [DataMember]
        [DataDesc(CName = "特殊审批标记", ShortCode = "IsSpecially", Desc = "特殊审批标记", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsSpecially
        {
            get { return _IsSpecially; }
            set { _IsSpecially = value; }
        }

       

        #endregion
    }
	#endregion
}