using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PExpenseAccount

	/// <summary>
	/// PExpenseAccount object for NHibernate mapped table 'P_ExpenseAccount'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "报销单管理", ClassCName = "PExpenseAccount", ShortCode = "PExpenseAccount", Desc = "报销单管理")]
	public class PExpenseAccount : BusinessBase
    {
		#region Member Variables
		
        protected string _pExpenseAccountNo;
        protected long? _clientID;
        protected string _clientName;
        protected long? _componeID;
        protected string _componeName;
        protected long? _contractID;
        protected string _contractName;
        protected long? _pExpenseAccounTypeID;
        protected string _pExpenseAccounTypeName;
        protected double _pExpenseAccounAmount;
        protected long? _pExpenseAccounContentTypeID;
        protected string _pExpenseAccounContentTypeName;
        protected string _pExpenseAccounMemo;
        protected long? _deptID;
        protected string _deptName;
        protected long _status;
        protected long? _applyManID;
        protected string _applyMan;
        protected DateTime? _applyDate;
        protected double _dayCount;
        protected double _transport;
        protected double _cityTransport;
        protected double _hotelRates;
        protected double _meals;
        protected double _entertainsCosts;
        protected double _communicationCosts;
        protected double _otherCosts;
        protected string _accountingDate;
        protected long? _reviewManID;
        protected string _reviewMan;
        protected DateTime? _reviewDate;
        protected string _reviewInfo;
        protected long? _accountingDeptID;
        protected string _accountingDeptName;
        protected long? _projectTypeID;
        protected string _projectTypeName;
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
        protected long _oneLevelItemID;
        protected string _oneLevelItemName;
        protected long _twoLevelItemID;
        protected string _twoLevelItemName;
        protected string _voucherNo;
        protected long? _payManID;
        protected string _payManName;
        protected DateTime? _payDate;
        protected string _payDateInfo;
        protected double _cashAmount;
        protected double _transferAmount;
        protected double _loadAmount;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected long? _receiveManID;
        protected string _receiveManName;
        protected string _receiveBankInfo;
        private bool _IsSpecially;


        #endregion

        #region Constructors

        public PExpenseAccount() { }

		public PExpenseAccount( long labID, string pExpenseAccountNo, long clientID, string clientName, long componeID, string componeName, long contractID, string contractName, long pExpenseAccounTypeID, string pExpenseAccounTypeName, double pExpenseAccounAmount, long pExpenseAccounContentTypeID, string pExpenseAccounContentTypeName, string pExpenseAccounMemo, long deptID, string deptName, long status, long applyManID, string applyMan, DateTime applyDate, double dayCount, double transport, double cityTransport, double hotelRates, double meals, double entertainsCosts, double communicationCosts, double otherCosts, string accountingDate, long reviewManID, string reviewMan, DateTime reviewDate, string reviewInfo, long accountingDeptID, string accountingDeptName, long projectTypeID, string projectTypeName, long twoReviewManID, string twoReviewMan, DateTime twoReviewDate, string twoReviewInfo, long threeReviewManID, string threeReviewMan, DateTime threeReviewDate, string threeReviewInfo, long fourReviewManID, string fourReviewMan, DateTime fourReviewDate, string fourReviewInfo, long oneLevelItemID, string oneLevelItemName, long twoLevelItemID, string twoLevelItemName, string voucherNo, long payManID, string payManName, DateTime payDate, string payDateInfo, double cashAmount, double transferAmount, double loadAmount, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, long receiveManID, string receiveManName, string receiveBankInfo )
		{
			this._labID = labID;
			this._pExpenseAccountNo = pExpenseAccountNo;
			this._clientID = clientID;
			this._clientName = clientName;
			this._componeID = componeID;
			this._componeName = componeName;
			this._contractID = contractID;
			this._contractName = contractName;
			this._pExpenseAccounTypeID = pExpenseAccounTypeID;
			this._pExpenseAccounTypeName = pExpenseAccounTypeName;
			this._pExpenseAccounAmount = pExpenseAccounAmount;
			this._pExpenseAccounContentTypeID = pExpenseAccounContentTypeID;
			this._pExpenseAccounContentTypeName = pExpenseAccounContentTypeName;
			this._pExpenseAccounMemo = pExpenseAccounMemo;
			this._deptID = deptID;
			this._deptName = deptName;
			this._status = status;
			this._applyManID = applyManID;
			this._applyMan = applyMan;
			this._applyDate = applyDate;
			this._dayCount = dayCount;
			this._transport = transport;
			this._cityTransport = cityTransport;
			this._hotelRates = hotelRates;
			this._meals = meals;
			this._entertainsCosts = entertainsCosts;
			this._communicationCosts = communicationCosts;
			this._otherCosts = otherCosts;
			this._accountingDate = accountingDate;
			this._reviewManID = reviewManID;
			this._reviewMan = reviewMan;
			this._reviewDate = reviewDate;
			this._reviewInfo = reviewInfo;
			this._accountingDeptID = accountingDeptID;
			this._accountingDeptName = accountingDeptName;
			this._projectTypeID = projectTypeID;
			this._projectTypeName = projectTypeName;
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
			this._oneLevelItemID = oneLevelItemID;
			this._oneLevelItemName = oneLevelItemName;
			this._twoLevelItemID = twoLevelItemID;
			this._twoLevelItemName = twoLevelItemName;
			this._voucherNo = voucherNo;
			this._payManID = payManID;
			this._payManName = payManName;
			this._payDate = payDate;
			this._payDateInfo = payDateInfo;
			this._cashAmount = cashAmount;
			this._transferAmount = transferAmount;
			this._loadAmount = loadAmount;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._receiveManID = receiveManID;
			this._receiveManName = receiveManName;
			this._receiveBankInfo = receiveBankInfo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "报销单号", ShortCode = "PExpenseAccountNo", Desc = "报销单号", ContextType = SysDic.All, Length = 200)]
        public virtual string PExpenseAccountNo
		{
			get { return _pExpenseAccountNo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PExpenseAccountNo", value, value.ToString());
				_pExpenseAccountNo = value;
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
        [DataDesc(CName = "报销单类型ID", ShortCode = "PExpenseAccounTypeID", Desc = "报销单类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PExpenseAccounTypeID
		{
			get { return _pExpenseAccounTypeID; }
			set { _pExpenseAccounTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "报销单类型名称", ShortCode = "PExpenseAccounTypeName", Desc = "报销单类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PExpenseAccounTypeName
		{
			get { return _pExpenseAccounTypeName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PExpenseAccounTypeName", value, value.ToString());
				_pExpenseAccounTypeName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "报销单金额", ShortCode = "PExpenseAccounAmount", Desc = "报销单金额", ContextType = SysDic.All, Length = 8)]
        public virtual double PExpenseAccounAmount
		{
			get { return _pExpenseAccounAmount; }
			set { _pExpenseAccounAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "报销单内容类型ID", ShortCode = "PExpenseAccounContentTypeID", Desc = "报销单内容类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PExpenseAccounContentTypeID
		{
			get { return _pExpenseAccounContentTypeID; }
			set { _pExpenseAccounContentTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "报销单内容类型名称", ShortCode = "PExpenseAccounContentTypeName", Desc = "报销单内容类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PExpenseAccounContentTypeName
		{
			get { return _pExpenseAccounContentTypeName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PExpenseAccounContentTypeName", value, value.ToString());
				_pExpenseAccounContentTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "报销单说明", ShortCode = "PExpenseAccounMemo", Desc = "报销单说明", ContextType = SysDic.All, Length = 200)]
        public virtual string PExpenseAccounMemo
		{
			get { return _pExpenseAccounMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PExpenseAccounMemo", value, value.ToString());
				_pExpenseAccounMemo = value;
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
        [DataDesc(CName = "报销单状态", ShortCode = "Status", Desc = "报销单状态", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "天数", ShortCode = "DayCount", Desc = "天数", ContextType = SysDic.All, Length = 8)]
        public virtual double DayCount
		{
			get { return _dayCount; }
			set { _dayCount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "交通费（跨市）", ShortCode = "Transport", Desc = "交通费（跨市）", ContextType = SysDic.All, Length = 8)]
        public virtual double Transport
		{
			get { return _transport; }
			set { _transport = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "市内车费", ShortCode = "CityTransport", Desc = "市内车费", ContextType = SysDic.All, Length = 8)]
        public virtual double CityTransport
		{
			get { return _cityTransport; }
			set { _cityTransport = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "住宿费", ShortCode = "HotelRates", Desc = "住宿费", ContextType = SysDic.All, Length = 8)]
        public virtual double HotelRates
		{
			get { return _hotelRates; }
			set { _hotelRates = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "餐费补贴", ShortCode = "Meals", Desc = "餐费补贴", ContextType = SysDic.All, Length = 8)]
        public virtual double Meals
		{
			get { return _meals; }
			set { _meals = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "招待费", ShortCode = "EntertainsCosts", Desc = "招待费", ContextType = SysDic.All, Length = 8)]
        public virtual double EntertainsCosts
		{
			get { return _entertainsCosts; }
			set { _entertainsCosts = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "通讯费", ShortCode = "CommunicationCosts", Desc = "通讯费", ContextType = SysDic.All, Length = 8)]
        public virtual double CommunicationCosts
		{
			get { return _communicationCosts; }
			set { _communicationCosts = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "其它费用", ShortCode = "OtherCosts", Desc = "其它费用", ContextType = SysDic.All, Length = 8)]
        public virtual double OtherCosts
		{
			get { return _otherCosts; }
			set { _otherCosts = value; }
		}

        [DataMember]
        [DataDesc(CName = "核算年份", ShortCode = "AccountingDate", Desc = "核算年份", ContextType = SysDic.All, Length = 10)]
        public virtual string AccountingDate
		{
			get { return _accountingDate; }
			set
			{				
				_accountingDate = value;
			}
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
				_reviewInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "核算单位ID", ShortCode = "AccountingDeptID", Desc = "核算单位ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AccountingDeptID
		{
			get { return _accountingDeptID; }
			set { _accountingDeptID = value; }
		}

        [DataMember]
        [DataDesc(CName = "核算单位名称", ShortCode = "AccountingDeptName", Desc = "核算单位名称", ContextType = SysDic.All, Length = 200)]
        public virtual string AccountingDeptName
		{
			get { return _accountingDeptName; }
			set
			{
				_accountingDeptName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目类别ID", ShortCode = "ProjectTypeID", Desc = "项目类别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProjectTypeID
		{
			get { return _projectTypeID; }
			set { _projectTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "项目类别名称", ShortCode = "ProjectTypeName", Desc = "项目类别名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ProjectTypeName
		{
			get { return _projectTypeName; }
			set
			{
				_projectTypeName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "2审核（报销核对）人ID", ShortCode = "TwoReviewManID", Desc = "2审核（报销核对）人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TwoReviewManID
		{
			get { return _twoReviewManID; }
			set { _twoReviewManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "2审核（报销核对）人", ShortCode = "TwoReviewMan", Desc = "2审核（报销核对）人", ContextType = SysDic.All, Length = 200)]
        public virtual string TwoReviewMan
		{
			get { return _twoReviewMan; }
			set
			{
				_twoReviewMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "2审核（报销核对）时间", ShortCode = "TwoReviewDate", Desc = "2审核（报销核对）时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TwoReviewDate
		{
			get { return _twoReviewDate; }
			set { _twoReviewDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "2审核（报销核对）人意见", ShortCode = "TwoReviewInfo", Desc = "2审核（报销核对）人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string TwoReviewInfo
		{
			get { return _twoReviewInfo; }
			set
			{
				_twoReviewInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "3审核（特殊报销单审批）人ID", ShortCode = "ThreeReviewManID", Desc = "3审核（特殊报销单审批）人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ThreeReviewManID
		{
			get { return _threeReviewManID; }
			set { _threeReviewManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "3审核（特殊报销单审批）人", ShortCode = "ThreeReviewMan", Desc = "3审核（特殊报销单审批）人", ContextType = SysDic.All, Length = 200)]
        public virtual string ThreeReviewMan
		{
			get { return _threeReviewMan; }
			set
			{
				_threeReviewMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "3审核（特殊报销单审批）时间", ShortCode = "ThreeReviewDate", Desc = "3审核（特殊报销单审批）时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ThreeReviewDate
		{
			get { return _threeReviewDate; }
			set { _threeReviewDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "3审核（特殊报销单审批）人意见", ShortCode = "ThreeReviewInfo", Desc = "3审核（特殊报销单审批）人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string ThreeReviewInfo
		{
			get { return _threeReviewInfo; }
			set
			{
				_threeReviewInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "4审核（报销复核）人ID", ShortCode = "FourReviewManID", Desc = "4审核（报销复核）人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? FourReviewManID
		{
			get { return _fourReviewManID; }
			set { _fourReviewManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "4审核（报销复核）人", ShortCode = "FourReviewMan", Desc = "4审核（报销复核）人", ContextType = SysDic.All, Length = 200)]
        public virtual string FourReviewMan
		{
			get { return _fourReviewMan; }
			set
			{
				_fourReviewMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "4审核（报销复核）时间", ShortCode = "FourReviewDate", Desc = "4审核（报销复核）时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FourReviewDate
		{
			get { return _fourReviewDate; }
			set { _fourReviewDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "4审核（报销复核）人意见", ShortCode = "FourReviewInfo", Desc = "4审核（报销复核）人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string FourReviewInfo
		{
			get { return _fourReviewInfo; }
			set
			{				
				_fourReviewInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "一级科目ID", ShortCode = "OneLevelItemID", Desc = "一级科目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long OneLevelItemID
		{
			get { return _oneLevelItemID; }
			set { _oneLevelItemID = value; }
		}

        [DataMember]
        [DataDesc(CName = "一级科目", ShortCode = "OneLevelItemName", Desc = "一级科目", ContextType = SysDic.All, Length = 200)]
        public virtual string OneLevelItemName
		{
			get { return _oneLevelItemName; }
			set
			{
				_oneLevelItemName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "二级科目ID", ShortCode = "TwoLevelItemID", Desc = "二级科目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long TwoLevelItemID
		{
			get { return _twoLevelItemID; }
			set { _twoLevelItemID = value; }
		}

        [DataMember]
        [DataDesc(CName = "二级科目", ShortCode = "TwoLevelItemName", Desc = "二级科目", ContextType = SysDic.All, Length = 200)]
        public virtual string TwoLevelItemName
		{
			get { return _twoLevelItemName; }
			set
			{
				_twoLevelItemName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "财务凭证单号", ShortCode = "VoucherNo", Desc = "财务凭证单号", ContextType = SysDic.All, Length = 100)]
        public virtual string VoucherNo
		{
			get { return _voucherNo; }
			set
			{
				_voucherNo = value;
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
				_payDateInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "现金金额", ShortCode = "CashAmount", Desc = "现金金额", ContextType = SysDic.All, Length = 8)]
        public virtual double CashAmount
		{
			get { return _cashAmount; }
			set { _cashAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "转账金额", ShortCode = "TransferAmount", Desc = "转账金额", ContextType = SysDic.All, Length = 8)]
        public virtual double TransferAmount
		{
			get { return _transferAmount; }
			set { _transferAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "借款相抵金额", ShortCode = "LoadAmount", Desc = "借款相抵金额", ContextType = SysDic.All, Length = 8)]
        public virtual double LoadAmount
		{
			get { return _loadAmount; }
			set { _loadAmount = value; }
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
				_receiveManName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "领款人银行备注说明", ShortCode = "ReceiveBankInfo", Desc = "领款人银行备注说明", ContextType = SysDic.All, Length = 500)]
        public virtual string ReceiveBankInfo
		{
			get { return _receiveBankInfo; }
			set
			{
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