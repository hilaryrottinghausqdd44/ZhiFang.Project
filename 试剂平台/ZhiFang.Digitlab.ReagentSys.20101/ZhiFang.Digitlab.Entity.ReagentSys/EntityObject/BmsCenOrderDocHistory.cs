using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region BmsCenOrderDocHistory

	/// <summary>
	/// BmsCenOrderDocHistory object for NHibernate mapped table 'BmsCenOrderDocHistory'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "平台订货总单历史表", ClassCName = "BmsCenOrderDocHistory", ShortCode = "BmsCenOrderDocHistory", Desc = "平台订货总单历史表")]
	public class BmsCenOrderDocHistory : BaseEntity
	{
		#region Member Variables
		
        protected string _orderDocNo;
        protected string _labName;
        protected int _stID;
        protected string _stName;
        protected string _companyName;
        protected int _userID;
        protected string _userName;
        protected int _urgentFlag;
        protected string _urgentFlagName;
        protected int _status;
        protected string _statusName;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected int _iOFlag;
        protected string _memo;
        protected string _compMemo;
        protected string _labMemo;
        protected DateTime? _deliveryTime;
        protected DateTime? _reqDeliveryTime;
        protected double _totalPrice;
		protected CenOrg _comp;
		protected CenOrg _lab;
		protected IList<BmsCenOrderDtlHistory> _bmsCenOrderDtlHistoryList; 

		#endregion

		#region Constructors

		public BmsCenOrderDocHistory() { }

		public BmsCenOrderDocHistory( string orderDocNo, string labName, int stID, string stName, string companyName, int userID, string userName, int urgentFlag, string urgentFlagName, int status, string statusName, DateTime operDate, int printTimes, int iOFlag, string memo, CenOrg comp, CenOrg lab )
		{
			this._orderDocNo = orderDocNo;
			this._labName = labName;
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
			this._comp = comp;
			this._lab = lab;
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
        [DataDesc(CName = "操作人员编号", ShortCode = "UserID", Desc = "操作人员编号", ContextType = SysDic.All, Length = 4)]
        public virtual int UserID
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
        [DataDesc(CName = "", ShortCode = "Comp", Desc = "")]
		public virtual CenOrg Comp
		{
			get { return _comp; }
			set { _comp = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Lab", Desc = "")]
		public virtual CenOrg Lab
		{
			get { return _lab; }
			set { _lab = value; }
		}

        [DataMember]
        [DataDesc(CName = "平台订货明细历史表", ShortCode = "BmsCenOrderDtlHistoryList", Desc = "平台订货明细历史表")]
		public virtual IList<BmsCenOrderDtlHistory> BmsCenOrderDtlHistoryList
		{
			get
			{
				if (_bmsCenOrderDtlHistoryList==null)
				{
					_bmsCenOrderDtlHistoryList = new List<BmsCenOrderDtlHistory>();
				}
				return _bmsCenOrderDtlHistoryList;
			}
			set { _bmsCenOrderDtlHistoryList = value; }
		}

        
		#endregion
	}
	#endregion
}