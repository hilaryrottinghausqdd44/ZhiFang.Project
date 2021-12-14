using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region WXMessageSendTask

	/// <summary>
	/// WXMessageSendTask object for NHibernate mapped table 'WX_MessageSendTask'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "WXMessageSendTask", ShortCode = "WXMessageSendTask", Desc = "")]
	public class WXMessageSendTask : BaseEntityService
	{
		#region Member Variables
		
        protected long _hospitalID;
        protected long? _pMTID;
        protected string _hospitalCode;
        protected string _hospitalName;
        protected string _taskName;
        protected string _taskCode;
        protected long _taskTypeID;
        protected string _taskTypeName;
        protected long _sendTypeID;
        protected string _sendTypeName;
        protected int _count;
        protected string _context;
        protected string _attachmentName;
        protected string _attachmentURL;
        protected string _attachmentSize;
        protected string _comment;
        protected long _createrID;
        protected string _createrName;
        protected long _receiveObjectID;
        protected string _receiveObjectName;
        protected long _receiveWeiXinAccountID;
        protected string _receiveOpenID;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public WXMessageSendTask() { }

		public WXMessageSendTask( long labID, long hospitalID, long pMTID, string hospitalCode, string hospitalName, string taskName, string taskCode, long taskTypeID, string taskTypeName, long sendTypeID, string sendTypeName, int count, string context, string attachmentName, string attachmentURL, string attachmentSize, string comment, long createrID, string createrName, long receiveObjectID, string receiveObjectName, long receiveWeiXinAccountID, string receiveOpenID, DateTime dataUpdateTime, DateTime dataAddTime )
		{
			this._labID = labID;
			this._hospitalID = hospitalID;
			this._pMTID = pMTID;
			this._hospitalCode = hospitalCode;
			this._hospitalName = hospitalName;
			this._taskName = taskName;
			this._taskCode = taskCode;
			this._taskTypeID = taskTypeID;
			this._taskTypeName = taskTypeName;
			this._sendTypeID = sendTypeID;
			this._sendTypeName = sendTypeName;
			this._count = count;
			this._context = context;
			this._attachmentName = attachmentName;
			this._attachmentURL = attachmentURL;
			this._attachmentSize = attachmentSize;
			this._comment = comment;
			this._createrID = createrID;
			this._createrName = createrName;
			this._receiveObjectID = receiveObjectID;
			this._receiveObjectName = receiveObjectName;
			this._receiveWeiXinAccountID = receiveWeiXinAccountID;
			this._receiveOpenID = receiveOpenID;
			this._dataUpdateTime = dataUpdateTime;
			this._dataAddTime = dataAddTime;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "HospitalID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long HospitalID
		{
			get { return _hospitalID; }
			set { _hospitalID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PMTID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? PMTID
		{
			get { return _pMTID; }
			set { _pMTID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalCode
		{
			get { return _hospitalCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HospitalCode", value, value.ToString());
				_hospitalCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalName
		{
			get { return _hospitalName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HospitalName", value, value.ToString());
				_hospitalName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TaskName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TaskName
		{
			get { return _taskName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TaskName", value, value.ToString());
				_taskName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TaskCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TaskCode
		{
			get { return _taskCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TaskCode", value, value.ToString());
				_taskCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TaskTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long TaskTypeID
		{
			get { return _taskTypeID; }
			set { _taskTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TaskTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TaskTypeName
		{
			get { return _taskTypeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TaskTypeName", value, value.ToString());
				_taskTypeName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SendTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long SendTypeID
		{
			get { return _sendTypeID; }
			set { _sendTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SendTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SendTypeName
		{
			get { return _sendTypeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SendTypeName", value, value.ToString());
				_sendTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Count", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Count
		{
			get { return _count; }
			set { _count = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Context", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string Context
		{
			get { return _context; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Context", value, value.ToString());
				_context = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AttachmentName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AttachmentName
		{
			get { return _attachmentName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AttachmentName", value, value.ToString());
				_attachmentName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AttachmentURL", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string AttachmentURL
		{
			get { return _attachmentURL; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for AttachmentURL", value, value.ToString());
				_attachmentURL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AttachmentSize", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string AttachmentSize
		{
			get { return _attachmentSize; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for AttachmentSize", value, value.ToString());
				_attachmentSize = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CreaterID
		{
			get { return _createrID; }
			set { _createrID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CreaterName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
		{
			get { return _createrName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
				_createrName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReceiveObjectID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ReceiveObjectID
		{
			get { return _receiveObjectID; }
			set { _receiveObjectID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReceiveObjectName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ReceiveObjectName
		{
			get { return _receiveObjectName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveObjectName", value, value.ToString());
				_receiveObjectName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReceiveWeiXinAccountID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ReceiveWeiXinAccountID
		{
			get { return _receiveWeiXinAccountID; }
			set { _receiveWeiXinAccountID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReceiveOpenID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceiveOpenID
		{
			get { return _receiveOpenID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveOpenID", value, value.ToString());
				_receiveOpenID = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		
		#endregion
	}
	#endregion
}