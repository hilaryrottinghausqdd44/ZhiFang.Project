using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PCustomerServiceOperation

	/// <summary>
	/// PCustomerServiceOperation object for NHibernate mapped table 'P_CustomerServiceOperation'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "服务单处理记录", ClassCName = "PCustomerServiceOperation", ShortCode = "PCustomerServiceOperation", Desc = "服务单处理记录")]
	public class PCustomerServiceOperation : BaseEntity
	{
		#region Member Variables
		
        protected long? _customerServiceID;
        protected string _contents;
        protected long _senderID;
        protected string _senderName;
        protected long? _receiverID;
        protected string _receiverName;
        protected bool _hasAttachment;
        protected string _memo;
        protected bool _isUse;
		

		#endregion

		#region Constructors

		public PCustomerServiceOperation() { }

		public PCustomerServiceOperation( long labID, long customerServiceID, string contents, long senderID, string senderName, long receiverID, string receiverName, bool hasAttachment, string memo, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._customerServiceID = customerServiceID;
			this._contents = contents;
			this._senderID = senderID;
			this._senderName = senderName;
			this._receiverID = receiverID;
			this._receiverName = receiverName;
			this._hasAttachment = hasAttachment;
			this._memo = memo;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务单ID", ShortCode = "CustomerServiceID", Desc = "服务单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CustomerServiceID
		{
			get { return _customerServiceID; }
			set { _customerServiceID = value; }
		}

        [DataMember]
        [DataDesc(CName = "内容", ShortCode = "Contents", Desc = "内容", ContextType = SysDic.All, Length = -1)]
        public virtual string Contents
		{
			get { return _contents; }
			set
			{
				_contents = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发件人", ShortCode = "SenderID", Desc = "发件人", ContextType = SysDic.All, Length = 8)]
        public virtual long SenderID
		{
			get { return _senderID; }
			set { _senderID = value; }
		}

        [DataMember]
        [DataDesc(CName = "发件人姓名", ShortCode = "SenderName", Desc = "发件人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string SenderName
		{
			get { return _senderName; }
			set
			{
				_senderName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收人", ShortCode = "ReceiverID", Desc = "接收人", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReceiverID
		{
			get { return _receiverID; }
			set { _receiverID = value; }
		}

        [DataMember]
        [DataDesc(CName = "收件人姓名", ShortCode = "ReceiverName", Desc = "收件人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceiverName
		{
			get { return _receiverName; }
			set
			{
				_receiverName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否带附件", ShortCode = "HasAttachment", Desc = "是否带附件", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasAttachment
		{
			get { return _hasAttachment; }
			set { _hasAttachment = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		
		#endregion
	}
	#endregion
}