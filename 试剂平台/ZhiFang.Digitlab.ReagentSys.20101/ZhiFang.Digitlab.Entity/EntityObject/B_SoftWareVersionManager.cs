using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BSoftWareVersionManager

	/// <summary>
	/// BSoftWareVersionManager object for NHibernate mapped table 'B_SoftWareVersionManager'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "软件版本管理", ClassCName = "BSoftWareVersionManager", ShortCode = "BSoftWareVersionManager", Desc = "软件版本管理")]
	public class BSoftWareVersionManager : BaseEntity
	{
		#region Member Variables
		
        protected string _name;
        protected string _comment;
        protected string _softWareVersion;
        protected string _softWareName;
        protected string _softWareComment;
        protected bool _isUse;
        protected long? _publishID;
        protected string _publishName;
        protected DateTime? _publishTime;
		protected string _code;

		#endregion

		#region Constructors

		public BSoftWareVersionManager() { }

        public BSoftWareVersionManager(long labID, string name, string comment, string softWareVersion, string softWareName, string softWareComment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, long publishID, string publishName, DateTime publishTime, string Code)
		{
			this._labID = labID;
			this._name = name;
			this._comment = comment;
			this._softWareVersion = softWareVersion;
			this._softWareName = softWareName;
			this._softWareComment = softWareComment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._publishID = publishID;
			this._publishName = publishName;
			this._publishTime = publishTime;
            this._code = Code;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "软件版本名称", ShortCode = "Name", Desc = "软件版本名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "软件版本描述", ShortCode = "Comment", Desc = "软件版本描述", ContextType = SysDic.All, Length = 300)]
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
        [DataDesc(CName = "软件版本号", ShortCode = "SoftWareVersion", Desc = "软件版本号", ContextType = SysDic.All, Length = 20)]
        public virtual string SoftWareVersion
		{
			get { return _softWareVersion; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SoftWareVersion", value, value.ToString());
				_softWareVersion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "软件名称", ShortCode = "SoftWareName", Desc = "软件名称", ContextType = SysDic.All, Length = 40)]
        public virtual string SoftWareName
		{
			get { return _softWareName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for SoftWareName", value, value.ToString());
				_softWareName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "软件描述", ShortCode = "SoftWareComment", Desc = "软件描述", ContextType = SysDic.All, Length = 300)]
        public virtual string SoftWareComment
		{
			get { return _softWareComment; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for SoftWareComment", value, value.ToString());
				_softWareComment = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发布者ID", ShortCode = "PublishID", Desc = "发布者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? PublishID
		{
			get { return _publishID; }
			set { _publishID = value; }
		}

        [DataMember]
        [DataDesc(CName = "发布者姓名", ShortCode = "PublishName", Desc = "发布者姓名", ContextType = SysDic.All, Length = 40)]
        public virtual string PublishName
		{
			get { return _publishName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for PublishName", value, value.ToString());
				_publishName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发布时间", ShortCode = "PublishTime", Desc = "发布时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PublishTime
		{
			get { return _publishTime; }
			set { _publishTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "软件编码", ShortCode = "Code", Desc = "软件字典")]
		public virtual string Code
		{
			get { return _code; }
            set { _code = value; }
		}

        
		#endregion
	}
	#endregion
}