using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region OtherReceivables

    /// <summary>
    /// OtherReceivables object for NHibernate mapped table 'Other_Receivables'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "收款管理", ClassCName = "OtherReceivables", ShortCode = "OtherReceivables", Desc = "收款管理")]
    public class OtherReceivables : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _clientID;
        protected string _clienName;
        protected long? _creatorID;
        protected string _payee;
        protected DateTime? _receiveTime;
        protected decimal _amount;
        protected string _accountingDepartment;
        protected string _accounting;
        protected long? _inputPeopleID;
        protected string _inputPeople;
        protected DateTime? _inputTime;
        protected string _comment;
        protected DateTime? _dataUpdateTime;
        

		#endregion

		#region Constructors

		public OtherReceivables() { }

		public OtherReceivables( long labID, long clientID, string clienName, long creatorID, string payee, DateTime receiveTime, decimal amount, string accountingDepartment, string accounting, long inputPeopleID, string inputPeople, DateTime inputTime, string comment, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._clientID = clientID;
			this._clienName = clienName;
			this._creatorID = creatorID;
			this._payee = payee;
			this._receiveTime = receiveTime;
			this._amount = amount;
			this._accountingDepartment = accountingDepartment;
			this._accounting = accounting;
			this._inputPeopleID = inputPeopleID;
			this._inputPeople = inputPeople;
			this._inputTime = inputTime;
			this._comment = comment;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "ClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ClientID
		{
			get { return _clientID; }
			set { _clientID = value; }
		}

        [DataMember]
        [DataDesc(CName = "客户名称", ShortCode = "ClienName", Desc = "客户名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ClienName
		{
			get { return _clienName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ClienName", value, value.ToString());
				_clienName = value;
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
        [DataDesc(CName = "收款人", ShortCode = "Payee", Desc = "收款人", ContextType = SysDic.All, Length = 40)]
        public virtual string Payee
		{
			get { return _payee; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Payee", value, value.ToString());
				_payee = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款时间", ShortCode = "ReceiveTime", Desc = "收款时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveTime
		{
			get { return _receiveTime; }
			set { _receiveTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款金额", ShortCode = "Amount", Desc = "收款金额", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Amount
		{
			get { return _amount; }
			set { _amount = value; }
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
        [DataDesc(CName = "核算个人", ShortCode = "Accounting", Desc = "核算个人", ContextType = SysDic.All, Length = 40)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "录入人ID", ShortCode = "InputPeopleID", Desc = "录入人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? InputPeopleID
		{
			get { return _inputPeopleID; }
			set { _inputPeopleID = value; }
		}

        [DataMember]
        [DataDesc(CName = "录入人", ShortCode = "InputPeople", Desc = "录入人", ContextType = SysDic.All, Length = 40)]
        public virtual string InputPeople
		{
			get { return _inputPeople; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for InputPeople", value, value.ToString());
				_inputPeople = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "录入时间", ShortCode = "InputTime", Desc = "录入时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InputTime
		{
			get { return _inputTime; }
			set { _inputTime = value; }
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		#endregion
	}
	#endregion
}