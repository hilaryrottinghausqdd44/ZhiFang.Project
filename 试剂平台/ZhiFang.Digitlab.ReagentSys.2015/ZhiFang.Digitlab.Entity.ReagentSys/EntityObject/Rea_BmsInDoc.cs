using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaBmsInDoc

	/// <summary>
	/// ReaBmsInDoc object for NHibernate mapped table 'Rea_BmsInDoc'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "入库总单表", ClassCName = "ReaBmsInDoc", ShortCode = "ReaBmsInDoc", Desc = "入库总单表")]
	public class ReaBmsInDoc : BaseEntity
	{
		#region Member Variables
		
        protected string _inDocNo;
        protected long? _InType;
        protected string _InTypeName;
        protected long? _companyID;
        protected string _companyName;
        protected string _carrier;
        protected long? _userID;
        protected string _invoiceNo;
        protected int _status;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected double _totalPrice;        
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;
		protected ReaCenOrg _reaCenOrg;
		protected IList<ReaBmsInDtl> _reaBmsInDtlList; 

		#endregion

		#region Constructors

		public ReaBmsInDoc() { }

		public ReaBmsInDoc( long labID, string inDocNo, long companyID, string companyName, string carrier, long userID, string invoiceNo, int status, DateTime operDate, int printTimes, double totalPrice, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ReaCenOrg reaCenOrg )
		{
			this._labID = labID;
			this._inDocNo = inDocNo;
			this._companyID = companyID;
			this._companyName = companyName;
			this._carrier = carrier;
			this._userID = userID;
			this._invoiceNo = invoiceNo;
			this._status = status;
			this._operDate = operDate;
			this._printTimes = printTimes;
			this._totalPrice = totalPrice;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._dispOrder = dispOrder;
			this._memo = memo;
			this._visible = visible;
			this._createrID = createrID;
			this._createrName = createrName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._reaCenOrg = reaCenOrg;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "入库总单号", ShortCode = "InDocNo", Desc = "入库总单号", ContextType = SysDic.All, Length = 20)]
        public virtual string InDocNo
		{
			get { return _inDocNo; }
			set { _inDocNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库类型", ShortCode = "InType", Desc = "入库类型", ContextType = SysDic.All, Length = 50)]
        public virtual long? InType
        {
            get { return _InType; }
            set { _InType = value; }
        }

        [DataMember]
        [DataDesc(CName = "入库类型名称", ShortCode = "InTypeName", Desc = "入库类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string InTypeName
        {
            get { return _InTypeName; }
            set { _InTypeName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供应商ID", ShortCode = "CompanyID", Desc = "供应商ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? CompanyID
		{
			get { return _companyID; }
			set { _companyID = value; }
		}

        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "CompanyName", Desc = "供应商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
		{
			get { return _companyName; }
			set { _companyName = value; }
		}

        [DataMember]
        [DataDesc(CName = "送货人", ShortCode = "Carrier", Desc = "送货人", ContextType = SysDic.All, Length = 50)]
        public virtual string Carrier
		{
			get { return _carrier; }
			set { _carrier = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作人员ID", ShortCode = "UserID", Desc = "操作人员ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? UserID
		{
			get { return _userID; }
			set { _userID = value; }
		}

        [DataMember]
        [DataDesc(CName = "发票号", ShortCode = "InvoiceNo", Desc = "发票号", ContextType = SysDic.All, Length = 30)]
        public virtual string InvoiceNo
		{
			get { return _invoiceNo; }
			set { _invoiceNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "单据状态", ShortCode = "Status", Desc = "单据状态", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
		{
			get { return _status; }
			set { _status = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总单金额", ShortCode = "TotalPrice", Desc = "总单金额", ContextType = SysDic.All, Length = 8)]
        public virtual double TotalPrice
		{
			get { return _totalPrice; }
			set { _totalPrice = value; }
		}

        

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set { _zX1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set { _zX2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set { _zX3 = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
		{
			get { return _memo; }
			set { _memo = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? CreaterID
		{
			get { return _createrID; }
			set { _createrID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
		{
			get { return _createrName; }
			set { _createrName = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "机构表", ShortCode = "ReaCenOrg", Desc = "机构表")]
		public virtual ReaCenOrg ReaCenOrg
		{
			get { return _reaCenOrg; }
			set { _reaCenOrg = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库明细表", ShortCode = "ReaBmsInDtlList", Desc = "入库明细表")]
		public virtual IList<ReaBmsInDtl> ReaBmsInDtlList
		{
			get
			{
				if (_reaBmsInDtlList==null)
				{
					_reaBmsInDtlList = new List<ReaBmsInDtl>();
				}
				return _reaBmsInDtlList;
			}
			set { _reaBmsInDtlList = value; }
		}

        
		#endregion
	}
	#endregion
}