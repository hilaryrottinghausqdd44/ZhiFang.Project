using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherKeepURL

	/// <summary>
	/// OtherKeepURL object for NHibernate mapped table 'Other_KeepURL'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "收藏网址", ClassCName = "OtherKeepURL", ShortCode = "OtherKeepURL", Desc = "收藏网址")]
    public class OtherKeepURL : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _uRLGroupID;
        protected long? _uRLTypeID;
        protected string _name;
        protected string _uRL;
        protected string _comment;
        protected long? _creatorID;
        protected string _creator;
        protected DateTime? _dataUpdateTime;
        

		#endregion

		#region Constructors

		public OtherKeepURL() { }

		public OtherKeepURL( long labID, long uRLGroupID, long uRLTypeID, string name, string uRL, string comment, long creatorID, string creator, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._uRLGroupID = uRLGroupID;
			this._uRLTypeID = uRLTypeID;
			this._name = name;
			this._uRL = uRL;
			this._comment = comment;
			this._creatorID = creatorID;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "URL类别ID", ShortCode = "URLGroupID", Desc = "URL类别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? URLGroupID
		{
			get { return _uRLGroupID; }
			set { _uRLGroupID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "URL类型ID", ShortCode = "URLTypeID", Desc = "URL类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? URLTypeID
		{
			get { return _uRLTypeID; }
			set { _uRLTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 200)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "网址", ShortCode = "URL", Desc = "网址", ContextType = SysDic.All, Length = 200)]
        public virtual string URL
		{
			get { return _uRL; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for URL", value, value.ToString());
				_uRL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "CreatorID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.All, Length = 20)]
        public virtual string Creator
		{
			get { return _creator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
				_creator = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		#endregion
	}
	#endregion
}