using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region SCIMInfomationContent

	/// <summary>
	/// SCIMInfomationContent object for NHibernate mapped table 'SC_IM_InfomationContent'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "公共即时通讯信息内容表", ClassCName = "SCIMInfomationContent", ShortCode = "SCIMInfomationContent", Desc = "公共即时通讯信息内容表")]
	public class SCIMInfomationContent : BaseEntity
	{
		#region Member Variables
		
        protected string _infomationContent;
        protected long _senderID;
        protected string _senderName;
        protected string _senderNickName;
        protected long _receiverID;
        protected string _receiverName;
        protected string _receiverNickName;
        protected DateTime? _sendDateTime;
        protected DateTime? _receiveDateTime;
        protected long _iMICTypeID;
        protected string _iMICTypeName;
        protected bool _readFlag;
        protected bool _backFlag;
        protected bool _isUse;
		

		#endregion

		#region Constructors

		public SCIMInfomationContent() { }

		public SCIMInfomationContent( long labID, string infomationContent, long senderID, string senderName, string senderNickName, long receiverID, string receiverName, string receiverNickName, DateTime sendDateTime, DateTime receiveDateTime, long iMICTypeID, string iMICTypeName, bool readFlag, bool backFlag, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._infomationContent = infomationContent;
			this._senderID = senderID;
			this._senderName = senderName;
			this._senderNickName = senderNickName;
			this._receiverID = receiverID;
			this._receiverName = receiverName;
			this._receiverNickName = receiverNickName;
			this._sendDateTime = sendDateTime;
			this._receiveDateTime = receiveDateTime;
			this._iMICTypeID = iMICTypeID;
			this._iMICTypeName = iMICTypeName;
			this._readFlag = readFlag;
			this._backFlag = backFlag;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "信息内容", ShortCode = "InfomationContent", Desc = "信息内容", ContextType = SysDic.All, Length = -1)]
        public virtual string InfomationContent
		{
			get { return _infomationContent; }
			set
			{
				_infomationContent = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发送者ID", ShortCode = "SenderID", Desc = "发送者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long SenderID
		{
			get { return _senderID; }
			set { _senderID = value; }
		}

        [DataMember]
        [DataDesc(CName = "发送者名称", ShortCode = "SenderName", Desc = "发送者名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "发送者昵称", ShortCode = "SenderNickName", Desc = "发送者昵称", ContextType = SysDic.All, Length = 50)]
        public virtual string SenderNickName
		{
			get { return _senderNickName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SenderNickName", value, value.ToString());
				_senderNickName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收者ID", ShortCode = "ReceiverID", Desc = "接收者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ReceiverID
		{
			get { return _receiverID; }
			set { _receiverID = value; }
		}

        [DataMember]
        [DataDesc(CName = "接收者名称", ShortCode = "ReceiverName", Desc = "接收者名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "接收者昵称", ShortCode = "ReceiverNickName", Desc = "接收者昵称", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceiverNickName
		{
			get { return _receiverNickName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiverNickName", value, value.ToString());
				_receiverNickName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发送时间", ShortCode = "SendDateTime", Desc = "发送时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SendDateTime
		{
			get { return _sendDateTime; }
			set { _sendDateTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收时间", ShortCode = "ReceiveDateTime", Desc = "接收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveDateTime
		{
			get { return _receiveDateTime; }
			set { _receiveDateTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "信息类型ID", ShortCode = "IMICTypeID", Desc = "信息类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long IMICTypeID
		{
			get { return _iMICTypeID; }
			set { _iMICTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "信息类型名称", ShortCode = "IMICTypeName", Desc = "信息类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string IMICTypeName
		{
			get { return _iMICTypeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for IMICTypeName", value, value.ToString());
				_iMICTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "阅读标志", ShortCode = "ReadFlag", Desc = "阅读标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool ReadFlag
		{
			get { return _readFlag; }
			set { _readFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "撤回标志", ShortCode = "BackFlag", Desc = "撤回标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool BackFlag
		{
			get { return _backFlag; }
			set { _backFlag = value; }
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