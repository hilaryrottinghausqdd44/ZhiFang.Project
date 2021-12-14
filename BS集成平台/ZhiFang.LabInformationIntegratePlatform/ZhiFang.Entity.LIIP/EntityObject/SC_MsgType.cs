using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region SCMsgType

	/// <summary>
	/// SCMsgType object for NHibernate mapped table 'SC_MsgType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "公共消息类型表", ClassCName = "SCMsgType", ShortCode = "SCMsgType", Desc = "公共消息类型表")]
	public class SCMsgType : BaseEntityService
	{
		#region Member Variables
		
        protected string _cName;
        protected string _code;
        protected string _eName;
        protected string _shortCode;
        protected long? _systemID;
        protected string _systemCName;
        protected string _systemCode;
        protected string _url;
        protected bool _visible;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public SCMsgType() { }

		public SCMsgType( long labID, string cName, string code, string eName, string shortCode, long systemID, string systemCName, string systemCode, string url, bool visible, string memo, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._cName = cName;
			this._code = code;
			this._eName = eName;
			this._shortCode = shortCode;
			this._systemID = systemID;
			this._systemCName = systemCName;
			this._systemCode = systemCode;
			this._url = url;
			this._visible = visible;
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
        [DataDesc(CName = "消息类型名称", ShortCode = "CName", Desc = "消息类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "消息类型代码", ShortCode = "Code", Desc = "消息类型代码", ContextType = SysDic.All, Length = 50)]
        public virtual string Code
		{
			get { return _code; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code", value, value.ToString());
				_code = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "消息类型英文名称", ShortCode = "EName", Desc = "消息类型英文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "消息类型简码", ShortCode = "ShortCode", Desc = "消息类型简码", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
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
        [DataDesc(CName = "展现程序地址", ShortCode = "Url", Desc = "展现程序地址", ContextType = SysDic.All, Length = 1000)]
        public virtual string Url
		{
			get { return _url; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for Url", value, value.ToString());
				_url = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否可用", ShortCode = "Visible", Desc = "是否可用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
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

		
		#endregion
	}
	#endregion
}