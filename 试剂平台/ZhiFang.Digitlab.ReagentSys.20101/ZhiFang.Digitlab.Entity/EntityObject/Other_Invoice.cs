using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherInvoice

	/// <summary>
	/// OtherInvoice object for NHibernate mapped table 'Other_Invoice'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "发票管理", ClassCName = "OtherInvoice", ShortCode = "OtherInvoice", Desc = "发票管理")]
    public class OtherInvoice : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _clientID;
        protected long? _creatorID;
        protected string _clientName;
        protected decimal _money;
        protected string _comment;
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

		public OtherInvoice() { }

		public OtherInvoice( long labID, long clientID, long creatorID, string clientName, decimal money, string comment, string creator, DateTime dataAddTime, string checker, long checkerID, DateTime checkTime, string checkOpinion, long approverID, string approver, DateTime approvalTime, string approvalOpinion, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._clientID = clientID;
			this._creatorID = creatorID;
			this._clientName = clientName;
			this._money = money;
			this._comment = comment;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "ClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ClientID
		{
			get { return _clientID; }
			set { _clientID = value; }
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
        [DataDesc(CName = "客户名称", ShortCode = "ClientName", Desc = "客户名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ClientName
		{
			get { return _clientName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ClientName", value, value.ToString());
				_clientName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开票金额", ShortCode = "Money", Desc = "开票金额", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Money
		{
			get { return _money; }
			set { _money = value; }
		}

        [DataMember]
        [DataDesc(CName = "开票信息（备注）", ShortCode = "Comment", Desc = "开票信息（备注）", ContextType = SysDic.All, Length = 16)]
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