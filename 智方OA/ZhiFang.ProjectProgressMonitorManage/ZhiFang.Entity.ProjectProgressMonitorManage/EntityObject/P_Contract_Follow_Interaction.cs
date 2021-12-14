using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PContractFollowInteraction

    /// <summary>
    /// PInteraction object for NHibernate mapped table 'P_Interaction'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "项目跟踪记录表", ClassCName = "PContractFollowInteraction", ShortCode = "PContractFollowInteraction", Desc = "项目跟踪记录表")]
	public class PContractFollowInteraction : BaseEntity
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
		protected PContractFollow _pContractFollow;
		//protected IList<PProjectAttachment> _pProjectAttachmentList; 

		#endregion

		#region Constructors

		public PContractFollowInteraction() { }

		

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
        [DataDesc(CName = "项目跟踪", ShortCode = "PContractFollow", Desc = "项目跟踪")]
        public virtual PContractFollow PContractFollow
        {
            get { return _pContractFollow; }
            set { _pContractFollow = value; }
        }

        #endregion
    }
	#endregion
}