using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region BmsCenOrderDoc

	/// <summary>
	/// BmsCenOrderDoc object for NHibernate mapped table 'BmsCenOrderDoc'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "平台订货总单表", ClassCName = "BmsCenOrderDoc", ShortCode = "BmsCenOrderDoc", Desc = "")]
	public class BmsCenOrderDoc : BaseEntity
	{
		#region Member Variables
		
        protected string _orderDocNo;
        protected int _stID;
        protected string _stName;
        protected string _companyName;
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
        protected string _labName;                  
        protected string _compMemo;
        protected string _labMemo;
        protected DateTime? _deliveryTime;
        protected DateTime? _reqDeliveryTime;
        protected double _totalPrice;
        protected DateTime? _dataUpdateTime;
        protected long _checkerID;
        protected string _checker;
        protected DateTime? _checkTime;
        protected long _confirmID;
        protected string _confirm;
        protected DateTime? _confirmTime;
        protected string _sender;
        protected DateTime? _sendTime;
        protected int _isThirdFlag;
        protected int _deleteFlag;
		protected CenOrg _lab;
		protected CenOrg _comp;
		protected IList<BmsCenOrderDtl> _bmsCenOrderDtlList; 

		#endregion

		#region Constructors

		public BmsCenOrderDoc() { }

        public BmsCenOrderDoc(string orderDocNo, int stID, string stName, string companyName, long userID, string userName, int urgentFlag, string urgentFlagName, int status, string statusName, DateTime operDate, int printTimes, int iOFlag, string memo, CenOrg lab, CenOrg comp)
		{
			this._orderDocNo = orderDocNo;
			this._stID = stID;
			this._stName = stName;
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
			this._lab = lab;
			this._comp = comp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "平台订货总单号", ShortCode = "OrderDocNo", Desc = "平台订货总单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDocNo
		{
			get { return _orderDocNo; }
			set
			{
				_orderDocNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "保管帐ID", ShortCode = "StID", Desc = "保管帐ID", ContextType = SysDic.All, Length = 4)]
        public virtual int StID
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
        [DataDesc(CName = "数据写入第三方系统标志", ShortCode = "IsThirdFlag", Desc = "数据写入第三方系统标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IsThirdFlag
		{
            get { return _isThirdFlag; }
            set { _isThirdFlag = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总价", ShortCode = "TotalPrice", Desc = "总价", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "审核人员编号", ShortCode = "CheckerID", Desc = "审核人员编号", ContextType = SysDic.All, Length = 8)]
        public virtual long CheckerID
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
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "确认人编号", ShortCode = "ConfirmID", Desc = "确认人编号", ContextType = SysDic.All, Length = 8)]
        public virtual long ConfirmID
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
        [DataDesc(CName = "确认时间", ShortCode = "ConfirmTime", Desc = "确认时间", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "发货时间", ShortCode = "SendTime", Desc = "发货时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
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
        [DataDesc(CName = "", ShortCode = "Lab", Desc = "")]
		public virtual CenOrg Lab
		{
			get { return _lab; }
			set { _lab = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comp", Desc = "")]
		public virtual CenOrg Comp
		{
			get { return _comp; }
			set { _comp = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BmsCenOrderDtlList", Desc = "")]
		public virtual IList<BmsCenOrderDtl> BmsCenOrderDtlList
		{
			get
			{
				if (_bmsCenOrderDtlList==null)
				{
					_bmsCenOrderDtlList = new List<BmsCenOrderDtl>();
				}
				return _bmsCenOrderDtlList;
			}
			set { _bmsCenOrderDtlList = value; }
		}

        
		#endregion
	}
	#endregion
}