using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherReimbursementExpense

	/// <summary>
	/// OtherReimbursementExpense object for NHibernate mapped table 'Other_ReimbursementExpense'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "费用报销", ClassCName = "OtherReimbursementExpense", ShortCode = "OtherReimbursementExpense", Desc = "费用报销")]
    public class OtherReimbursementExpense : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _reimbursementManID;
        protected string _reimbursementMan;
        protected decimal _amount;
        protected DateTime? _writeTime;
        protected DateTime? _reimbursementTime;
        protected long? _accountingID;
        protected string _accounting;
        protected string _accountingDepartment;
        protected long? _subjectIDA;
        protected long? _subjectIDB;
        protected long? _financialIDA;
        protected long? _financialIDB;
        protected long? _clientID;
        protected string _clientName;
        protected string _expenseItemName;
        protected long? _reimbursementTypeID;
        protected string _reimbursementYears;
        protected long? _reimbursementStateID;
        protected string _comment;
        protected string _checker;
        protected long? _checkerID;
        protected DateTime? _checkTime;
        protected string _checkOpinion;
        protected long? _approverID;
        protected string _approver;
        protected DateTime? _approvalTime;
        protected string _approvalOpinion;
        protected long? _financialCheckerID;
        protected string _financialChecker;
        protected DateTime? _financialCheckTime;
        protected string _financialCheckerOpinion;
        protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public OtherReimbursementExpense() { }

		public OtherReimbursementExpense( long labID, long reimbursementManID, string reimbursementMan, decimal amount, DateTime writeTime, DateTime reimbursementTime, long accountingID, string accounting, string accountingDepartment, long subjectIDA, long subjectIDB, long financialIDA, long financialIDB, long clientID, string clientName, string expenseItemName, long reimbursementTypeID, string reimbursementYears, long reimbursementStateID, string comment, string checker, long checkerID, DateTime checkTime, string checkOpinion, long approverID, string approver, DateTime approvalTime, string approvalOpinion, long financialCheckerID, string financialChecker, DateTime financialCheckTime, string financialCheckerOpinion, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._reimbursementManID = reimbursementManID;
			this._reimbursementMan = reimbursementMan;
			this._amount = amount;
			this._writeTime = writeTime;
			this._reimbursementTime = reimbursementTime;
			this._accountingID = accountingID;
			this._accounting = accounting;
			this._accountingDepartment = accountingDepartment;
			this._subjectIDA = subjectIDA;
			this._subjectIDB = subjectIDB;
			this._financialIDA = financialIDA;
			this._financialIDB = financialIDB;
			this._clientID = clientID;
			this._clientName = clientName;
			this._expenseItemName = expenseItemName;
			this._reimbursementTypeID = reimbursementTypeID;
			this._reimbursementYears = reimbursementYears;
			this._reimbursementStateID = reimbursementStateID;
			this._comment = comment;
			this._checker = checker;
			this._checkerID = checkerID;
			this._checkTime = checkTime;
			this._checkOpinion = checkOpinion;
			this._approverID = approverID;
			this._approver = approver;
			this._approvalTime = approvalTime;
			this._approvalOpinion = approvalOpinion;
			this._financialCheckerID = financialCheckerID;
			this._financialChecker = financialChecker;
			this._financialCheckTime = financialCheckTime;
			this._financialCheckerOpinion = financialCheckerOpinion;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "报销人ID", ShortCode = "ReimbursementManID", Desc = "报销人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReimbursementManID
		{
			get { return _reimbursementManID; }
			set { _reimbursementManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "报销人", ShortCode = "ReimbursementMan", Desc = "报销人", ContextType = SysDic.All, Length = 40)]
        public virtual string ReimbursementMan
		{
			get { return _reimbursementMan; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ReimbursementMan", value, value.ToString());
				_reimbursementMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "报销金额", ShortCode = "Amount", Desc = "报销金额", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "填报时间", ShortCode = "WriteTime", Desc = "填报时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? WriteTime
		{
			get { return _writeTime; }
			set { _writeTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "报销时间", ShortCode = "ReimbursementTime", Desc = "报销时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReimbursementTime
		{
			get { return _reimbursementTime; }
			set { _reimbursementTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合算人ID", ShortCode = "AccountingID", Desc = "合算人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AccountingID
		{
			get { return _accountingID; }
			set { _accountingID = value; }
		}

        [DataMember]
        [DataDesc(CName = "核算人", ShortCode = "Accounting", Desc = "核算人", ContextType = SysDic.All, Length = 40)]
        public virtual string Accounting
		{
			get { return _accounting; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Accounting", value, value.ToString());
				_accounting = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "核算部门", ShortCode = "AccountingDepartment", Desc = "核算部门", ContextType = SysDic.All, Length = 200)]
        public virtual string AccountingDepartment
		{
			get { return _accountingDepartment; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for AccountingDepartment", value, value.ToString());
				_accountingDepartment = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "一级科目", ShortCode = "SubjectIDA", Desc = "一级科目", ContextType = SysDic.All, Length = 8)]
        public virtual long? SubjectIDA
		{
			get { return _subjectIDA; }
			set { _subjectIDA = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "二级科目", ShortCode = "SubjectIDB", Desc = "二级科目", ContextType = SysDic.All, Length = 8)]
        public virtual long? SubjectIDB
		{
			get { return _subjectIDB; }
			set { _subjectIDB = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "财务一级", ShortCode = "FinancialIDA", Desc = "财务一级", ContextType = SysDic.All, Length = 8)]
        public virtual long? FinancialIDA
		{
			get { return _financialIDA; }
			set { _financialIDA = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "财务二级", ShortCode = "FinancialIDB", Desc = "财务二级", ContextType = SysDic.All, Length = 8)]
        public virtual long? FinancialIDB
		{
			get { return _financialIDB; }
			set { _financialIDB = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ClientID", Desc = "", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "项目名称", ShortCode = "ExpenseItemName", Desc = "项目名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ExpenseItemName
		{
			get { return _expenseItemName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ExpenseItemName", value, value.ToString());
				_expenseItemName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目类别", ShortCode = "ReimbursementTypeID", Desc = "项目类别", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReimbursementTypeID
		{
			get { return _reimbursementTypeID; }
			set { _reimbursementTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "核算年份", ShortCode = "ReimbursementYears", Desc = "核算年份", ContextType = SysDic.All, Length = 40)]
        public virtual string ReimbursementYears
		{
			get { return _reimbursementYears; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ReimbursementYears", value, value.ToString());
				_reimbursementYears = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "状态", ShortCode = "ReimbursementStateID", Desc = "状态", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReimbursementStateID
		{
			get { return _reimbursementStateID; }
			set { _reimbursementStateID = value; }
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

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "财务审核人ID", ShortCode = "FinancialCheckerID", Desc = "财务审核人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? FinancialCheckerID
		{
			get { return _financialCheckerID; }
			set { _financialCheckerID = value; }
		}

        [DataMember]
        [DataDesc(CName = "财务审核人", ShortCode = "FinancialChecker", Desc = "财务审核人", ContextType = SysDic.All, Length = 40)]
        public virtual string FinancialChecker
		{
			get { return _financialChecker; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for FinancialChecker", value, value.ToString());
				_financialChecker = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "财务审核时间", ShortCode = "FinancialCheckTime", Desc = "财务审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FinancialCheckTime
		{
			get { return _financialCheckTime; }
			set { _financialCheckTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "财务意见", ShortCode = "FinancialCheckerOpinion", Desc = "财务意见", ContextType = SysDic.All, Length = 16)]
        public virtual string FinancialCheckerOpinion
		{
			get { return _financialCheckerOpinion; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for FinancialCheckerOpinion", value, value.ToString());
				_financialCheckerOpinion = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

		#endregion
	}
	#endregion
}