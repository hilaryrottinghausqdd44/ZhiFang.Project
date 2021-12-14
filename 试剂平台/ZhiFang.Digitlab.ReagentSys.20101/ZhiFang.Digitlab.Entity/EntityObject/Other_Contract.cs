using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherContract

	/// <summary>
	/// OtherContract object for NHibernate mapped table 'Other_Contract'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "合同管理", ClassCName = "OtherContract", ShortCode = "OtherContract", Desc = "合同管理")]
    public class OtherContract : BaseEntity
	{
		#region Member Variables
		
        
        protected int _orderNo;
        protected string _name;
        protected long? _clientID;
        protected string _clientNane;
        protected string _paymentCompany;
        protected long? _paymentTypeID;
        protected long? _businessTypeID;
        protected long? _itemTypeID;
        protected decimal _amount;
        protected decimal _hardwareAmount;
        protected decimal _purchasesBudget;
        protected decimal _intermediaryCosts;
        protected DateTime? _signingTime;
        protected double _advancePaymentProportion;
        protected double _acceptancePaymentProportion;
        protected double _balanceDueProportion;
        protected double _serviceFeeProportion;
        protected decimal _deposit;
        protected decimal _singleCharges;
        protected decimal _doubleCharges;
        protected DateTime? _endTime;
        protected string _contents;
        protected int _isHasAttachment;
        protected double _receivablesProportion;
        protected long? _executionStateID;
        protected long? _backSectionStateID;
        protected string _signedDepartment;
        protected string _salesMan;
        protected string _comment;
        protected long? _creatorID;
        protected string _creator;
        protected string _checker;
        protected long? _checkerID;
        protected DateTime? _checkTime;
        protected string _checkOpinion;
        protected long? _approverID;
        protected string _approver;
        protected DateTime? _approvalTime;
        protected string _approvalOpinion;
        

		#endregion

		#region Constructors

		public OtherContract() { }

		public OtherContract( long labID, int orderNo, string name, long clientID, string clientNane, string paymentCompany, long paymentTypeID, long businessTypeID, long itemTypeID, decimal amount, decimal hardwareAmount, decimal purchasesBudget, decimal intermediaryCosts, DateTime signingTime, double advancePaymentProportion, double acceptancePaymentProportion, double balanceDueProportion, double serviceFeeProportion, decimal deposit, decimal singleCharges, decimal doubleCharges, DateTime endTime, string contents, int isHasAttachment, double receivablesProportion, long executionStateID, long backSectionStateID, string signedDepartment, string salesMan, string comment, long creatorID, string creator, DateTime dataAddTime, string checker, long checkerID, DateTime checkTime, string checkOpinion, long approverID, string approver, DateTime approvalTime, string approvalOpinion, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._orderNo = orderNo;
			this._name = name;
			this._clientID = clientID;
			this._clientNane = clientNane;
			this._paymentCompany = paymentCompany;
			this._paymentTypeID = paymentTypeID;
			this._businessTypeID = businessTypeID;
			this._itemTypeID = itemTypeID;
			this._amount = amount;
			this._hardwareAmount = hardwareAmount;
			this._purchasesBudget = purchasesBudget;
			this._intermediaryCosts = intermediaryCosts;
			this._signingTime = signingTime;
			this._advancePaymentProportion = advancePaymentProportion;
			this._acceptancePaymentProportion = acceptancePaymentProportion;
			this._balanceDueProportion = balanceDueProportion;
			this._serviceFeeProportion = serviceFeeProportion;
			this._deposit = deposit;
			this._singleCharges = singleCharges;
			this._doubleCharges = doubleCharges;
			this._endTime = endTime;
			this._contents = contents;
			this._isHasAttachment = isHasAttachment;
			this._receivablesProportion = receivablesProportion;
			this._executionStateID = executionStateID;
			this._backSectionStateID = backSectionStateID;
			this._signedDepartment = signedDepartment;
			this._salesMan = salesMan;
			this._comment = comment;
			this._creatorID = creatorID;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._checker = checker;
			this._checkerID = checkerID;
			this._checkTime = checkTime;
			this._checkOpinion = checkOpinion;
			this._approverID = approverID;
			this._approver = approver;
			this._approvalTime = approvalTime;
			this._approvalOpinion = approvalOpinion;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "顺序号", ShortCode = "OrderNo", Desc = "顺序号", ContextType = SysDic.All, Length = 4)]
        public virtual int OrderNo
		{
			get { return _orderNo; }
			set { _orderNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "合同名称", ShortCode = "Name", Desc = "合同名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户编号", ShortCode = "ClientID", Desc = "客户编号", ContextType = SysDic.All, Length = 8)]
        public virtual long? ClientID
		{
			get { return _clientID; }
			set { _clientID = value; }
		}

        [DataMember]
        [DataDesc(CName = "客户名称", ShortCode = "ClientNane", Desc = "客户名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ClientNane
		{
			get { return _clientNane; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ClientNane", value, value.ToString());
				_clientNane = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "付款单位", ShortCode = "PaymentCompany", Desc = "付款单位", ContextType = SysDic.All, Length = 200)]
        public virtual string PaymentCompany
		{
			get { return _paymentCompany; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PaymentCompany", value, value.ToString());
				_paymentCompany = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收支类型", ShortCode = "PaymentTypeID", Desc = "收支类型", ContextType = SysDic.All, Length = 8)]
        public virtual long? PaymentTypeID
		{
			get { return _paymentTypeID; }
			set { _paymentTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务类型", ShortCode = "BusinessTypeID", Desc = "业务类型", ContextType = SysDic.All, Length = 8)]
        public virtual long? BusinessTypeID
		{
			get { return _businessTypeID; }
			set { _businessTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目类别", ShortCode = "ItemTypeID", Desc = "项目类别", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemTypeID
		{
			get { return _itemTypeID; }
			set { _itemTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同金额", ShortCode = "Amount", Desc = "合同金额", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "硬件金额", ShortCode = "HardwareAmount", Desc = "硬件金额", ContextType = SysDic.All, Length = 8)]
        public virtual decimal HardwareAmount
		{
			get { return _hardwareAmount; }
			set { _hardwareAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采购预算", ShortCode = "PurchasesBudget", Desc = "采购预算", ContextType = SysDic.All, Length = 8)]
        public virtual decimal PurchasesBudget
		{
			get { return _purchasesBudget; }
			set { _purchasesBudget = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "中介费用", ShortCode = "IntermediaryCosts", Desc = "中介费用", ContextType = SysDic.All, Length = 8)]
        public virtual decimal IntermediaryCosts
		{
			get { return _intermediaryCosts; }
			set { _intermediaryCosts = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签约时间", ShortCode = "SigningTime", Desc = "签约时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SigningTime
		{
			get { return _signingTime; }
			set { _signingTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预付款比例", ShortCode = "AdvancePaymentProportion", Desc = "预付款比例", ContextType = SysDic.All, Length = 8)]
        public virtual double AdvancePaymentProportion
		{
			get { return _advancePaymentProportion; }
			set { _advancePaymentProportion = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "验收付款比例", ShortCode = "AcceptancePaymentProportion", Desc = "验收付款比例", ContextType = SysDic.All, Length = 8)]
        public virtual double AcceptancePaymentProportion
		{
			get { return _acceptancePaymentProportion; }
			set { _acceptancePaymentProportion = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "尾款比例", ShortCode = "BalanceDueProportion", Desc = "尾款比例", ContextType = SysDic.All, Length = 8)]
        public virtual double BalanceDueProportion
		{
			get { return _balanceDueProportion; }
			set { _balanceDueProportion = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务费比例", ShortCode = "ServiceFeeProportion", Desc = "服务费比例", ContextType = SysDic.All, Length = 8)]
        public virtual double ServiceFeeProportion
		{
			get { return _serviceFeeProportion; }
			set { _serviceFeeProportion = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同保证金", ShortCode = "Deposit", Desc = "合同保证金", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Deposit
		{
			get { return _deposit; }
			set { _deposit = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器单项收费", ShortCode = "SingleCharges", Desc = "仪器单项收费", ContextType = SysDic.All, Length = 8)]
        public virtual decimal SingleCharges
		{
			get { return _singleCharges; }
			set { _singleCharges = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器双向收费", ShortCode = "DoubleCharges", Desc = "仪器双向收费", ContextType = SysDic.All, Length = 8)]
        public virtual decimal DoubleCharges
		{
			get { return _doubleCharges; }
			set { _doubleCharges = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结束时间", ShortCode = "EndTime", Desc = "结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTime
		{
			get { return _endTime; }
			set { _endTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "合同内容", ShortCode = "Contents", Desc = "合同内容", ContextType = SysDic.All, Length = 16)]
        public virtual string Contents
		{
			get { return _contents; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Contents", value, value.ToString());
				_contents = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否有附件", ShortCode = "IsHasAttachment", Desc = "是否有附件", ContextType = SysDic.All, Length = 4)]
        public virtual int IsHasAttachment
		{
			get { return _isHasAttachment; }
			set { _isHasAttachment = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款比例", ShortCode = "ReceivablesProportion", Desc = "收款比例", ContextType = SysDic.All, Length = 8)]
        public virtual double ReceivablesProportion
		{
			get { return _receivablesProportion; }
			set { _receivablesProportion = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "执行状态", ShortCode = "ExecutionStateID", Desc = "执行状态", ContextType = SysDic.All, Length = 8)]
        public virtual long? ExecutionStateID
		{
			get { return _executionStateID; }
			set { _executionStateID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "回款状况", ShortCode = "BackSectionStateID", Desc = "回款状况", ContextType = SysDic.All, Length = 8)]
        public virtual long? BackSectionStateID
		{
			get { return _backSectionStateID; }
			set { _backSectionStateID = value; }
		}

        [DataMember]
        [DataDesc(CName = "签署部门", ShortCode = "SignedDepartment", Desc = "签署部门", ContextType = SysDic.All, Length = 200)]
        public virtual string SignedDepartment
		{
			get { return _signedDepartment; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for SignedDepartment", value, value.ToString());
				_signedDepartment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "销售人员", ShortCode = "SalesMan", Desc = "销售人员", ContextType = SysDic.All, Length = 40)]
        public virtual string SalesMan
		{
			get { return _salesMan; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for SalesMan", value, value.ToString());
				_salesMan = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "CreatorID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请者", ShortCode = "Creator", Desc = "申请者", ContextType = SysDic.All, Length = 20)]
        public virtual string Creator
		{
			get { return _creator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
				_creator = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请时间", ShortCode = "DataAddTime", Desc = "申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataAddTime
		{
			get { return _dataAddTime; }
			set { _dataAddTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "审核人", ShortCode = "Checker", Desc = "审核人", ContextType = SysDic.All, Length = 20)]
        public virtual string Checker
		{
			get { return _checker; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
				_checker = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "CheckerID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckerID
		{
			get { return _checkerID; }
			set { _checkerID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "审核意见", ShortCode = "CheckOpinion", Desc = "审核意见", ContextType = SysDic.All, Length = 16)]
        public virtual string CheckOpinion
		{
			get { return _checkOpinion; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for CheckOpinion", value, value.ToString());
				_checkOpinion = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "ApproverID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApproverID
		{
			get { return _approverID; }
			set { _approverID = value; }
		}

        [DataMember]
        [DataDesc(CName = "审批人", ShortCode = "Approver", Desc = "审批人", ContextType = SysDic.All, Length = 20)]
        public virtual string Approver
		{
			get { return _approver; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Approver", value, value.ToString());
				_approver = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批时间", ShortCode = "ApprovalTime", Desc = "审批时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApprovalTime
		{
			get { return _approvalTime; }
			set { _approvalTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "审批意见", ShortCode = "ApprovalOpinion", Desc = "审批意见", ContextType = SysDic.All, Length = 16)]
        public virtual string ApprovalOpinion
		{
			get { return _approvalOpinion; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for ApprovalOpinion", value, value.ToString());
				_approvalOpinion = value;
			}
		}

		#endregion
	}
	#endregion
}