using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region BmsCenSaleDoc

	/// <summary>
	/// BmsCenSaleDoc object for NHibernate mapped table 'BmsCenSaleDoc'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "平台供货总单表", ClassCName = "BmsCenSaleDoc", ShortCode = "BmsCenSaleDoc", Desc = "")]
	public class BmsCenSaleDoc : BaseEntity
	{
		#region Member Variables
		
        protected string _saleDocNo;
        protected string _companyName;
        protected long? _userID;
        protected string _userName;
        protected int _urgentFlag;
        protected string _urgentFlagName;
        protected int _status;
        protected string _statusName;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected int _iOFlag;
        protected string _memo;
        protected string _labName;
        protected int _source;
        protected DateTime? _dataUpdateTime;
        protected long? _accepterID;
        protected string _accepterName;
        protected long? _secAccepterID;
        protected string _secAccepterName;
        protected DateTime? _accepterTime;
        protected DateTime? _secAccepterTime;
        protected string _invoiceNo;
        protected string _accepterMemo;
        protected bool _isAccepterError;
        protected int _isSplit;
        protected int? _isAccountInput;
        protected double _totalPrice;
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
        protected long? _checkerID;
        protected string _checker;
        protected DateTime? _checkTime;
        protected string _sender;
        protected string _receiver;
        protected string _invoiceReceiver;
        protected DateTime? _receiveTime;
        protected DateTime? _sendOutTime;
        protected int _deleteFlag;
		protected CenOrg _comp;
		protected CenOrg _lab;
		protected IList<BmsCenSaleDtl> _bmsCenSaleDtlList; 

		#endregion

		#region Constructors

		public BmsCenSaleDoc() { }

        public BmsCenSaleDoc(string saleDocNo, string companyName, long userID, string userName, int urgentFlag, string urgentFlagName, int status, string statusName, DateTime operDate, int printTimes, int iOFlag, string memo, int source, CenOrg comp, CenOrg lab)
		{
			this._saleDocNo = saleDocNo;
			this._companyName = companyName;
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
			this._comp = comp;
			this._lab = lab;
            this._source = source;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "供货单号", ShortCode = "SaleDocNo", Desc = "供货单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocNo
		{
			get { return _saleDocNo; }
			set
			{
				_saleDocNo = value;
			}
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作人员编号", ShortCode = "UserID", Desc = "操作人员编号", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 50)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "实验室名称", ShortCode = "LabName", Desc = "实验室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string LabName
        {
            get { return _labName; }
            set
            {
                _labName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "数据来源", ShortCode = "Source", Desc = "数据来源", ContextType = SysDic.All, Length = 4)]
        public virtual int Source
        {
            get { return _source; }
            set { _source = value; }
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
        [DataDesc(CName = "主验收人ID", ShortCode = "AccepterID", Desc = "主验收人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AccepterID
        {
            get { return _accepterID; }
            set { _accepterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "主验收人", ShortCode = "AccepterName", Desc = "主验收人", ContextType = SysDic.All, Length = 100)]
        public virtual string AccepterName
        {
            get { return _accepterName; }
            set { _accepterName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "次验收人ID", ShortCode = "SecAccepterID", Desc = "次验收人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SecAccepterID
        {
            get { return _secAccepterID; }
            set { _secAccepterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "次验收人", ShortCode = "SecAccepterName", Desc = "次验收人", ContextType = SysDic.All, Length = 100)]
        public virtual string SecAccepterName
        {
            get { return _secAccepterName; }
            set { _secAccepterName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "验收时间", ShortCode = "AccepterTime", Desc = "验收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AccepterTime
        {
            get { return _accepterTime; }
            set { _accepterTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "次验收时间", ShortCode = "AccepterTime", Desc = "次验收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SecAccepterTime
        {
            get { return _secAccepterTime; }
            set { _secAccepterTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "发票号", ShortCode = "InvoiceNo", Desc = "发票号", ContextType = SysDic.All, Length = 100)]
        public virtual string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "验收备注", ShortCode = "AccepterMemo", Desc = "验收备注", ContextType = SysDic.All, Length = 1000)]
        public virtual string AccepterMemo
        {
            get { return _accepterMemo; }
            set { _accepterMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否验收异常", ShortCode = "IsAccepterError", Desc = "是否验收异常", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsAccepterError
        {
            get { return _isAccepterError; }
            set { _isAccepterError = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否拆分", ShortCode = "IsSplit", Desc = "是否拆分", ContextType = SysDic.All, Length = 4)]
        public virtual int IsSplit
        {
            get { return _isSplit; }
            set { _isSplit = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否入账", ShortCode = "IsAccountInput", Desc = "是否入账", ContextType = SysDic.All, Length = 4)]
        public virtual int? IsAccountInput
        {
            get { return _isAccountInput; }
            set { _isAccountInput = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总计金额", ShortCode = "TotalPrice", Desc = "总计金额", ContextType = SysDic.All, Length = 8)]
        public virtual double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室地址", ShortCode = "LabAddress", Desc = "实验室地址", ContextType = SysDic.All, Length = 500)]
        public virtual string LabAddress
        {
            get { return _labAddress; }
            set { _labAddress = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室联系人", ShortCode = "LabContact", Desc = "实验室联系人", ContextType = SysDic.All, Length = 100)]
        public virtual string LabContact
        {
            get { return _labContact; }
            set { _labContact = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室电话", ShortCode = "LabTel", Desc = "实验室电话", ContextType = SysDic.All, Length = 500)]
        public virtual string LabTel
        {
            get { return _labTel; }
            set { _labTel = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室热线电话", ShortCode = "LabHotTel", Desc = "实验室热线电话", ContextType = SysDic.All, Length = 500)]
        public virtual string LabHotTel
        {
            get { return _labHotTel; }
            set { _labHotTel = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室传真", ShortCode = "LabFox", Desc = "实验室传真", ContextType = SysDic.All, Length = 500)]
        public virtual string LabFox
        {
            get { return _labFox; }
            set { _labFox = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室邮箱", ShortCode = "LabEmail", Desc = "实验室邮箱", ContextType = SysDic.All, Length = 50)]
        public virtual string LabEmail
        {
            get { return _labEmail; }
            set { _labEmail = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室网址", ShortCode = "LabWebAddress", Desc = "实验室网址", ContextType = SysDic.All, Length = 100)]
        public virtual string LabWebAddress
        {
            get { return _labWebAddress; }
            set { _labWebAddress = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货商地址", ShortCode = "CompAddress", Desc = "供货商地址", ContextType = SysDic.All, Length = 500)]
        public virtual string CompAddress
        {
            get { return _compAddress; }
            set { _compAddress = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货商联系人", ShortCode = "CompContact", Desc = "供货商联系人", ContextType = SysDic.All, Length = 100)]
        public virtual string CompContact
        {
            get { return _compContact; }
            set { _compContact = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货商电话", ShortCode = "CompTel", Desc = "供货商电话", ContextType = SysDic.All, Length = 500)]
        public virtual string CompTel
        {
            get { return _compTel; }
            set { _compTel = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货商热线电话", ShortCode = "CompHotTel", Desc = "供货商热线电话", ContextType = SysDic.All, Length = 500)]
        public virtual string CompHotTel
        {
            get { return _compHotTel; }
            set { _compHotTel = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货商传真", ShortCode = "CompFox", Desc = "供货商传真", ContextType = SysDic.All, Length = 500)]
        public virtual string CompFox
        {
            get { return _compFox; }
            set { _compFox = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货商邮箱", ShortCode = "CompEmail", Desc = "供货商邮箱", ContextType = SysDic.All, Length = 50)]
        public virtual string CompEmail
        {
            get { return _compEmail; }
            set { _compEmail = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货商网址", ShortCode = "CompWebAddress", Desc = "供货商网址", ContextType = SysDic.All, Length = 100)]
        public virtual string CompWebAddress
        {
            get { return _compWebAddress; }
            set { _compWebAddress = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人ID", ShortCode = "Checker", Desc = "审核人ID", ContextType = SysDic.All, Length = 100)]
        public virtual long? CheckerID
        {
            get { return _checkerID; }
            set { _checkerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人", ShortCode = "Checker", Desc = "审核人", ContextType = SysDic.All, Length = 100)]
        public virtual string Checker
        {
            get { return _checker; }
            set { _checker = value; }
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
        [DataDesc(CName = "送货人", ShortCode = "Sender", Desc = "送货人", ContextType = SysDic.All, Length = 100)]
        public virtual string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        [DataMember]
        [DataDesc(CName = "签收人", ShortCode = "Receiver", Desc = "签收人", ContextType = SysDic.All, Length = 100)]
        public virtual string Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        [DataMember]
        [DataDesc(CName = "发票签收人", ShortCode = "InvoiceReceiver", Desc = "发票签收人", ContextType = SysDic.All, Length = 100)]
        public virtual string InvoiceReceiver
        {
            get { return _invoiceReceiver; }
            set { _invoiceReceiver = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签收时间", ShortCode = "ReceiveTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveTime
        {
            get { return _receiveTime; }
            set { _receiveTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发货日期", ShortCode = "SendOutTime", Desc = "发货日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SendOutTime
        {
            get { return _sendOutTime; }
            set { _sendOutTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "删除标记", ShortCode = "DeleteFlag", Desc = "删除标记", ContextType = SysDic.All, Length = 8)]
        public virtual int DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商", ShortCode = "Comp", Desc = "供应商")]
		public virtual CenOrg Comp
		{
			get { return _comp; }
			set { _comp = value; }
		}

        [DataMember]
        [DataDesc(CName = "实验室", ShortCode = "Lab", Desc = "实验室")]
		public virtual CenOrg Lab
		{
			get { return _lab; }
			set { _lab = value; }
		}

        [DataMember]
        [DataDesc(CName = "平台供货明细表", ShortCode = "BmsCenSaleDtlList", Desc = "平台供货明细表")]
		public virtual IList<BmsCenSaleDtl> BmsCenSaleDtlList
		{
			get
			{
				if (_bmsCenSaleDtlList==null)
				{
					_bmsCenSaleDtlList = new List<BmsCenSaleDtl>();
				}
				return _bmsCenSaleDtlList;
			}
			set { _bmsCenSaleDtlList = value; }
		}

        
		#endregion
	}
	#endregion
}