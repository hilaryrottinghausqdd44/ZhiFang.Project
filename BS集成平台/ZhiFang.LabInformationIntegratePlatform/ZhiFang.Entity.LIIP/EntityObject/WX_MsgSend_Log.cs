using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region WXMsgSendLog

	/// <summary>
	/// WXMsgSendLog object for NHibernate mapped table 'WX_MsgSend_Log'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "WXMsgSendLog", ShortCode = "WXMsgSendLog", Desc = "")]
	public class WXMsgSendLog : BaseEntityService
	{
		#region Member Variables
		
        protected long _bobjectID;
        protected long _type;
        protected string _typeName;
        protected string _businessModuleCode;
        protected long _receiveObjectID;
        protected string _receiveObjectName;
        protected long _receiveWeiXinAccountID;
        protected string _receiveOpenID;
        protected long _senderID;
        protected string _senderName;
        protected long _senderWeiXinAccountID;
        protected string _senderOpenID;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public WXMsgSendLog() { }

		public WXMsgSendLog( long labID, long bobjectID, long type, string typeName, string businessModuleCode, long receiveObjectID, string receiveObjectName, long receiveWeiXinAccountID, string receiveOpenID, long senderID, string senderName, long senderWeiXinAccountID, string senderOpenID, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._bobjectID = bobjectID;
			this._type = type;
			this._typeName = typeName;
			this._businessModuleCode = businessModuleCode;
			this._receiveObjectID = receiveObjectID;
			this._receiveObjectName = receiveObjectName;
			this._receiveWeiXinAccountID = receiveWeiXinAccountID;
			this._receiveOpenID = receiveOpenID;
			this._senderID = senderID;
			this._senderName = senderName;
			this._senderWeiXinAccountID = senderWeiXinAccountID;
			this._senderOpenID = senderOpenID;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BobjectID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long BobjectID
		{
			get { return _bobjectID; }
			set { _bobjectID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Type", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long Type
		{
			get { return _type; }
			set { _type = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string TypeName
		{
			get { return _typeName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
				_typeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BusinessModuleCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BusinessModuleCode
		{
			get { return _businessModuleCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BusinessModuleCode", value, value.ToString());
				_businessModuleCode = value;
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
        [DataDesc(CName = "", ShortCode = "SenderID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long SenderID
		{
			get { return _senderID; }
			set { _senderID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SenderName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SenderName
		{
			get { return _senderName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SenderName", value, value.ToString());
				_senderName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SenderWeiXinAccountID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long SenderWeiXinAccountID
		{
			get { return _senderWeiXinAccountID; }
			set { _senderWeiXinAccountID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SenderOpenID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SenderOpenID
		{
			get { return _senderOpenID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SenderOpenID", value, value.ToString());
				_senderOpenID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
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