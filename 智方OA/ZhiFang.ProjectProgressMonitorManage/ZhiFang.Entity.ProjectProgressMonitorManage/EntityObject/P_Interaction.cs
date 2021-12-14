using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PInteraction

	/// <summary>
	/// PInteraction object for NHibernate mapped table 'P_Interaction'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "互动记录表", ClassCName = "PInteraction", ShortCode = "PInteraction", Desc = "互动记录表")]
	public class PInteraction : BaseEntity
	{
		#region Member Variables
		
        protected long _senderID;
        protected long? _receiverID;
        protected string _contents;
        protected bool _isUse;
        protected string _memo;
        protected bool _hasAttachment;
        protected string _senderName;
        protected string _receiverName;
		protected PTask _pTask;
		protected IList<PProjectAttachment> _pProjectAttachmentList; 

		#endregion

		#region Constructors

		public PInteraction() { }

		public PInteraction( long labID, long senderID, long receiverID, string contents, DateTime dataAddTime, bool isUse, string memo, bool hasAttachment, string senderName, string receiverName, byte[] dataTimeStamp,  PTask pTask )
		{
			this._labID = labID;
			this._senderID = senderID;
			this._receiverID = receiverID;
			this._contents = contents;
			this._dataAddTime = dataAddTime;
			this._isUse = isUse;
			this._memo = memo;
			this._hasAttachment = hasAttachment;
			this._senderName = senderName;
			this._receiverName = receiverName;
			this._dataTimeStamp = dataTimeStamp;
			this._pTask = pTask;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发件人", ShortCode = "SenderID", Desc = "发件人", ContextType = SysDic.All, Length = 8)]
        public virtual long SenderID
		{
			get { return _senderID; }
			set { _senderID = value; }
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
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
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
        [DataDesc(CName = "是否带附件", ShortCode = "HasAttachment", Desc = "是否带附件", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasAttachment
		{
			get { return _hasAttachment; }
			set { _hasAttachment = value; }
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
        [DataDesc(CName = "任务表", ShortCode = "PTask", Desc = "任务表")]
		public virtual PTask PTask
		{
			get { return _pTask; }
			set { _pTask = value; }
		}

        [DataMember]
        [DataDesc(CName = "项目附件表", ShortCode = "PProjectAttachmentList", Desc = "项目附件表")]
		public virtual IList<PProjectAttachment> PProjectAttachmentList
		{
			get
			{
				if (_pProjectAttachmentList==null)
				{
					_pProjectAttachmentList = new List<PProjectAttachment>();
				}
				return _pProjectAttachmentList;
			}
			set { _pProjectAttachmentList = value; }
		}

        
		#endregion
	}
	#endregion
}