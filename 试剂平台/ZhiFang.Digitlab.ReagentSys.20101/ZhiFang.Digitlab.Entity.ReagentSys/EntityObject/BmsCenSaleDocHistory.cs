using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region BmsCenSaleDocHistory

	/// <summary>
	/// BmsCenSaleDocHistory object for NHibernate mapped table 'BmsCenSaleDocHistory'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "平台供货总单历史表", ClassCName = "BmsCenSaleDocHistory", ShortCode = "BmsCenSaleDocHistory", Desc = "平台供货总单历史表")]
	public class BmsCenSaleDocHistory : BaseEntity
	{
		#region Member Variables
		
        protected string _saleDocNo;
        protected string _labName;
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
        protected int _source;
		protected CenOrg _comp;
		protected CenOrg _lab;
		protected IList<BmsCenSaleDtlHistory> _bmsCenSaleDtlHistoryList; 

		#endregion

		#region Constructors

		public BmsCenSaleDocHistory() { }

		public BmsCenSaleDocHistory( string saleDocNo, string labName, string companyName, long userID, string userName, int urgentFlag, string urgentFlagName, int status, string statusName, DateTime operDate, int printTimes, int iOFlag, string memo, int source, CenOrg comp, CenOrg lab )
		{
			this._saleDocNo = saleDocNo;
			this._labName = labName;
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
			this._source = source;
			this._comp = comp;
			this._lab = lab;
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
        [DataDesc(CName = "数据来源:平台(供应商)、平台(实验室)、PC(供应商)、PC(实验室)、手持(供应商)、手持(实验室)", ShortCode = "Source", Desc = "数据来源:平台(供应商)、平台(实验室)、PC(供应商)、PC(实验室)、手持(供应商)、手持(实验室)", ContextType = SysDic.All, Length = 4)]
        public virtual int Source
		{
			get { return _source; }
			set { _source = value; }
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
        [DataDesc(CName = "平台供货明细历史表", ShortCode = "BmsCenSaleDtlHistoryList", Desc = "平台供货明细历史表")]
		public virtual IList<BmsCenSaleDtlHistory> BmsCenSaleDtlHistoryList
		{
			get
			{
				if (_bmsCenSaleDtlHistoryList==null)
				{
					_bmsCenSaleDtlHistoryList = new List<BmsCenSaleDtlHistory>();
				}
				return _bmsCenSaleDtlHistoryList;
			}
			set { _bmsCenSaleDtlHistoryList = value; }
		}

        
		#endregion
	}
	#endregion
}