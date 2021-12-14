using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.Common
{
	#region FFileInteraction

	/// <summary>
	/// FFileInteraction object for NHibernate mapped table 'F_File_Interaction'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "文档交流表（不附带附件）", ClassCName = "FFileInteraction", ShortCode = "FFileInteraction", Desc = "文档交流表（不附带附件）")]
	public class FFileInteraction : BaseEntity
	{
		#region Member Variables
		
        protected string _contents;
        protected string _senderName;
        protected string _receiverName;
        protected bool _hasAttachment;
        protected string _memo;
        protected bool _isUse;
		protected FFile _fFile;
		protected HREmployee _receiver;
		protected long _senderID;

		#endregion

		#region Constructors

		public FFileInteraction() { }

		public FFileInteraction( long labID, string contents, string senderName, string receiverName, bool hasAttachment, string memo, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, FFile fFile, HREmployee receiver, long senderID)
		{
			this._labID = labID;
			this._contents = contents;
			this._senderName = senderName;
			this._receiverName = receiverName;
			this._hasAttachment = hasAttachment;
			this._memo = memo;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._fFile = fFile;
			this._receiver = receiver;
			this._senderID = senderID;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "内容", ShortCode = "Contents", Desc = "内容", ContextType = SysDic.All, Length =12000)]
        public virtual string Contents
		{
			get { return _contents; }
			set
			{
				_contents = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "发件人姓名", ShortCode = "SenderName", Desc = "发件人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string SenderName
		{
			get { return _senderName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SenderName", value, value.ToString());
				_senderName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "收件人姓名", ShortCode = "ReceiverName", Desc = "收件人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceiverName
		{
			get { return _receiverName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiverName", value, value.ToString());
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
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
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

        [DataMember]
        [DataDesc(CName = "文档表", ShortCode = "FFile", Desc = "文档表")]
		public virtual FFile FFile
		{
			get { return _fFile; }
			set { _fFile = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "Receiver", Desc = "员工")]
		public virtual HREmployee Receiver
		{
			get { return _receiver; }
			set { _receiver = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发件人", ShortCode = "SenderID", Desc = "发件人", ContextType = SysDic.All, Length = 8)]
        public virtual long SenderID
        {
            get { return _senderID; }
            set { _senderID = value; }
        }


        #endregion
    }
	#endregion
}