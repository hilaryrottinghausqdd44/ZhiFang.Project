using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region SCMsgHandle

	/// <summary>
	/// SCMsgHandle object for NHibernate mapped table 'SC_MsgHandle'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "公共消息处理表", ClassCName = "SCMsgHandle", ShortCode = "SCMsgHandle", Desc = "公共消息处理表")]
	public class SCMsgHandle : BaseEntity
	{
		#region Member Variables
		
        protected long? _msgTypeID;
        protected long _msgID;
        protected string _msgTypeCName;
        protected string _msgTypeCode;
        protected long? _systemID;
        protected string _systemCName;
        protected string _systemCode;
        protected long _handleTypeID;
        protected string _handleTypeName;
        protected string _handleSysCode;
        protected string _handleSysName;
        protected DateTime? _handleTime;
        protected long _handlerID;
        protected string _handlerName;
        protected string _handleDesc;
        protected long _handleDeptID;
        protected string _handleDeptName;
        protected string _handleNodeName;
        protected string _handleNodeIPAddress;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public SCMsgHandle() { }

		public SCMsgHandle( long labID, long msgTypeID, long msgID, string msgTypeCName, string msgTypeCode, long systemID, string systemCName, string systemCode, long handleTypeID, string handleTypeName, string handleSysCode, string handleSysName, DateTime? handleTime, long handlerID, string handlerName, string handleDesc, long handleDeptID, string handleDeptName, string handleNodeName, string handleNodeIPAddress, string memo, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._msgTypeID = msgTypeID;
			this._msgID = msgID;
			this._msgTypeCName = msgTypeCName;
			this._msgTypeCode = msgTypeCode;
			this._systemID = systemID;
			this._systemCName = systemCName;
			this._systemCode = systemCode;
			this._handleTypeID = handleTypeID;
			this._handleTypeName = handleTypeName;
			this._handleSysCode = handleSysCode;
			this._handleSysName = handleSysName;
			this._handleTime = handleTime;
			this._handlerID = handlerID;
			this._handlerName = handlerName;
			this._handleDesc = handleDesc;
			this._handleDeptID = handleDeptID;
			this._handleDeptName = handleDeptName;
			this._handleNodeName = handleNodeName;
			this._handleNodeIPAddress = handleNodeIPAddress;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._creatorID = creatorID;
			this._creatorName = creatorName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消息类型ID", ShortCode = "MsgTypeID", Desc = "消息类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? MsgTypeID
		{
			get { return _msgTypeID; }
			set { _msgTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消息ID", ShortCode = "MsgID", Desc = "消息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long MsgID
		{
			get { return _msgID; }
			set { _msgID = value; }
		}

        [DataMember]
        [DataDesc(CName = "消息类型名称", ShortCode = "MsgTypeCName", Desc = "消息类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string MsgTypeCName
		{
			get { return _msgTypeCName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for MsgTypeCName", value, value.ToString());
				_msgTypeCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "消息类型代码", ShortCode = "MsgTypeCode", Desc = "消息类型代码", ContextType = SysDic.All, Length = 50)]
        public virtual string MsgTypeCode
		{
			get { return _msgTypeCode; }
			set { _msgTypeCode = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "所属系统ID", ShortCode = "SystemID", Desc = "所属系统ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SystemID
		{
			get { return _systemID; }
			set { _systemID = value; }
		}

        [DataMember]
        [DataDesc(CName = "所属系统名称", ShortCode = "SystemCName", Desc = "所属系统名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SystemCName
		{
			get { return _systemCName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SystemCName", value, value.ToString());
				_systemCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "所属系统代码", ShortCode = "SystemCode", Desc = "所属系统代码", ContextType = SysDic.All, Length = 50)]
        public virtual string SystemCode
		{
			get { return _systemCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SystemCode", value, value.ToString());
				_systemCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "处理类型ID", ShortCode = "HandleTypeID", Desc = "处理类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HandleTypeID
		{
			get { return _handleTypeID; }
			set { _handleTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "处理类型名称", ShortCode = "HandleTypeName", Desc = "处理类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string HandleTypeName
		{
			get { return _handleTypeName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for HandleTypeName", value, value.ToString());
				_handleTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "处理系统编码", ShortCode = "HandleSysCode", Desc = "处理系统编码", ContextType = SysDic.All, Length = 100)]
        public virtual string HandleSysCode
		{
			get { return _handleSysCode; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for HandleSysCode", value, value.ToString());
				_handleSysCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "处理系统名称", ShortCode = "HandleSysName", Desc = "处理系统名称", ContextType = SysDic.All, Length = 100)]
        public virtual string HandleSysName
		{
			get { return _handleSysName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for HandleSysName", value, value.ToString());
				_handleSysName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "处理时间", ShortCode = "HandleTime", Desc = "处理时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? HandleTime
		{
			get { return _handleTime; }
			set { _handleTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "处理人ID", ShortCode = "HandlerID", Desc = "处理人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HandlerID
		{
			get { return _handlerID; }
			set { _handlerID = value; }
		}

        [DataMember]
        [DataDesc(CName = "处理人姓名", ShortCode = "HandlerName", Desc = "处理人姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string HandlerName
		{
			get { return _handlerName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for HandlerName", value, value.ToString());
				_handlerName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "处理意见", ShortCode = "HandleDesc", Desc = "处理意见", ContextType = SysDic.All, Length = 5000)]
        public virtual string HandleDesc
		{
			get { return _handleDesc; }
			set
			{
				if ( value != null && value.Length > 5000)
					throw new ArgumentOutOfRangeException("Invalid value for HandleDesc", value, value.ToString());
				_handleDesc = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "处理科室ID", ShortCode = "HandleDeptID", Desc = "处理科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HandleDeptID
		{
			get { return _handleDeptID; }
			set { _handleDeptID = value; }
		}

        [DataMember]
        [DataDesc(CName = "处理科室名称", ShortCode = "HandleDeptName", Desc = "处理科室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string HandleDeptName
		{
			get { return _handleDeptName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for HandleDeptName", value, value.ToString());
				_handleDeptName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "处理站点名称", ShortCode = "HandleNodeName", Desc = "处理站点名称", ContextType = SysDic.All, Length = 50)]
        public virtual string HandleNodeName
		{
			get { return _handleNodeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for HandleNodeName", value, value.ToString());
				_handleNodeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "处理站点IP地址", ShortCode = "HandleNodeIPAddress", Desc = "处理站点IP地址", ContextType = SysDic.All, Length = 20)]
        public virtual string HandleNodeIPAddress
		{
			get { return _handleNodeIPAddress; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HandleNodeIPAddress", value, value.ToString());
				_handleNodeIPAddress = value;
			}
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者", ShortCode = "CreatorID", Desc = "创建者", ContextType = SysDic.All, Length = 8)]
        public virtual long CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreatorName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreatorName
		{
			get { return _creatorName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
				_creatorName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        public virtual SCMsg SCMsg { get; set; }


		[DataMember]
		[DataDesc(CName = "处理人密码", ShortCode = "DataUpdateTime", Desc = "处理人密码", ContextType = SysDic.All, Length = 8)]
		public virtual string HandlerPWD { get; set; }


		#endregion
	}
	#endregion
}