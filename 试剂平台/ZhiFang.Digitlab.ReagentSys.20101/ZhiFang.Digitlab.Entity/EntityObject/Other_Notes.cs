using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherNotes

	/// <summary>
	/// OtherNotes object for NHibernate mapped table 'Other_Notes'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "便签", ClassCName = "OtherNotes", ShortCode = "OtherNotes", Desc = "便签")]
    public class OtherNotes : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _creatorID;
        protected string _title;
        protected string _content;
        protected int _isPublic;
        protected string _creator;
        protected DateTime? _dataUpdateTime;
        

		#endregion

		#region Constructors

		public OtherNotes() { }

		public OtherNotes( long labID, long creatorID, string title, string content, int isPublic, string creator, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._creatorID = creatorID;
			this._title = title;
			this._content = content;
			this._isPublic = isPublic;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "CreatorID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "标题", ShortCode = "Title", Desc = "标题", ContextType = SysDic.All, Length = 512)]
        public virtual string Title
		{
			get { return _title; }
			set
			{
				if ( value != null && value.Length > 512)
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
        [DataDesc(CName = "是否公开", ShortCode = "IsPublic", Desc = "是否公开", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPublic
		{
			get { return _isPublic; }
			set { _isPublic = value; }
		}

        [DataMember]
        [DataDesc(CName = "登录者", ShortCode = "Creator", Desc = "登录者", ContextType = SysDic.All, Length = 20)]
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