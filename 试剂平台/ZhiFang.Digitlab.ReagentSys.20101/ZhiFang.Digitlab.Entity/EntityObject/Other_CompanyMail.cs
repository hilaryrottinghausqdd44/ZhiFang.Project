using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherCompanyMail

	/// <summary>
	/// OtherCompanyMail object for NHibernate mapped table 'Other_CompanyMail'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "公司邮件", ClassCName = "OtherCompanyMail", ShortCode = "OtherCompanyMail", Desc = "公司邮件")]
    public class OtherCompanyMail : BaseEntity
	{
		#region Member Variables
		
        
        protected string _title;
        protected string _content;
        protected DateTime? _creatTime;
        protected DateTime? _sendTime;
        protected DateTime? _receiveTime;
        protected string _sender;
        protected string _receiveMan;
        protected string _carbonCopy;
        protected string _blankCarbonCopy;
        protected int _isHasAttachment;
        protected int _isRead;
        protected double _size;
        protected long? _senderID;
        protected DateTime? _dataUpdateTime;
		protected BMailFile _bMailFile;
		protected BImportance _bImportance;
		protected BMailStatus _bMailStatus;
		protected BMailType _bMailType;

		#endregion

		#region Constructors

		public OtherCompanyMail() { }

		public OtherCompanyMail( long labID, string title, string content, DateTime creatTime, DateTime sendTime, DateTime receiveTime, string sender, string receiveMan, string carbonCopy, string blankCarbonCopy, int isHasAttachment, int isRead, double size, byte[] dataTimeStamp, long senderID, BMailFile bMailFile, BImportance bImportance, BMailStatus bMailStatus, BMailType bMailType )
		{
			this._labID = labID;
			this._title = title;
			this._content = content;
			this._creatTime = creatTime;
			this._sendTime = sendTime;
			this._receiveTime = receiveTime;
			this._sender = sender;
			this._receiveMan = receiveMan;
			this._carbonCopy = carbonCopy;
			this._blankCarbonCopy = blankCarbonCopy;
			this._isHasAttachment = isHasAttachment;
			this._isRead = isRead;
			this._size = size;
			this._dataTimeStamp = dataTimeStamp;
			this._sender = sender;
			this._bMailFile = bMailFile;
			this._bImportance = bImportance;
			this._bMailStatus = bMailStatus;
			this._bMailType = bMailType;
            this._senderID = senderID;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "标题", ShortCode = "Title", Desc = "标题", ContextType = SysDic.All, Length = 100)]
        public virtual string Title
		{
			get { return _title; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Title", value, value.ToString());
				_title = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "内容", ShortCode = "Content", Desc = "内容", ContextType = SysDic.All, Length = 16)]
        public virtual string Content
		{
			get { return _content; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Content", value, value.ToString());
				_content = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建时间", ShortCode = "CreatTime", Desc = "创建时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CreatTime
		{
			get { return _creatTime; }
			set { _creatTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发送时间", ShortCode = "SendTime", Desc = "发送时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SendTime
		{
			get { return _sendTime; }
			set { _sendTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收时间", ShortCode = "ReceiveTime", Desc = "接收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveTime
		{
			get { return _receiveTime; }
			set { _receiveTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "发件人", ShortCode = "Sender", Desc = "发件人", ContextType = SysDic.All, Length = 40)]
        public virtual string Sender
		{
			get { return _sender; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Sender", value, value.ToString());
				_sender = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "收件人", ShortCode = "ReceiveMan", Desc = "收件人", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveMan
		{
			get { return _receiveMan; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveMan", value, value.ToString());
				_receiveMan = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "抄送", ShortCode = "CarbonCopy", Desc = "抄送", ContextType = SysDic.All, Length = 400)]
        public virtual string CarbonCopy
		{
			get { return _carbonCopy; }
			set
			{
				if ( value != null && value.Length > 400)
					throw new ArgumentOutOfRangeException("Invalid value for CarbonCopy", value, value.ToString());
				_carbonCopy = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "秘密抄送", ShortCode = "BlankCarbonCopy", Desc = "秘密抄送", ContextType = SysDic.All, Length = 400)]
        public virtual string BlankCarbonCopy
		{
			get { return _blankCarbonCopy; }
			set
			{
				if ( value != null && value.Length > 400)
					throw new ArgumentOutOfRangeException("Invalid value for BlankCarbonCopy", value, value.ToString());
				_blankCarbonCopy = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否有附件", ShortCode = "IsHasAttachment", Desc = "是否有附件", ContextType = SysDic.All, Length = 4)]
        public virtual int IsHasAttachment
		{
			get { return _isHasAttachment; }
			set { _isHasAttachment = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否已读", ShortCode = "IsRead", Desc = "是否已读", ContextType = SysDic.All, Length = 4)]
        public virtual int IsRead
		{
			get { return _isRead; }
			set { _isRead = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "大小", ShortCode = "Size", Desc = "大小", ContextType = SysDic.All, Length = 8)]
        public virtual double Size
		{
			get { return _size; }
			set { _size = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发送人ID", ShortCode = "SenderID", Desc = "发送人ID", ContextType = SysDic.All)]
        public virtual long? SenderID
		{
            get { return _senderID; }
            set { _senderID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "所属文件", ShortCode = "BMailFile", Desc = "所属文件", ContextType = SysDic.All)]
		public virtual BMailFile BMailFile
		{
			get { return _bMailFile; }
			set { _bMailFile = value; }
		}

        [DataMember]
        [DataDesc(CName = "重要性", ShortCode = "BImportance", Desc = "重要性", ContextType = SysDic.All)]
		public virtual BImportance BImportance
		{
			get { return _bImportance; }
			set { _bImportance = value; }
		}

        [DataMember]
        [DataDesc(CName = "邮件状态", ShortCode = "BMailStatus", Desc = "邮件状态", ContextType = SysDic.All)]
		public virtual BMailStatus BMailStatus
		{
			get { return _bMailStatus; }
			set { _bMailStatus = value; }
		}

        [DataMember]
        [DataDesc(CName = "邮件类型", ShortCode = "BMailType", Desc = "邮件类型", ContextType = SysDic.All)]
		public virtual BMailType BMailType
		{
			get { return _bMailType; }
			set { _bMailType = value; }
		}

		#endregion
	}
	#endregion
}