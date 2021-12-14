using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherReturn

	/// <summary>
	/// OtherReturn object for NHibernate mapped table 'Other_Return'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "回复", ClassCName = "OtherReturn", ShortCode = "OtherReturn", Desc = "回复")]
    public class OtherReturn : BaseEntity
	{
		#region Member Variables
		
		
		protected long _recordID;
		protected long _parReturnID;
		protected string _title;
		protected string _content;
		protected int _maxNum;
		protected string _creator;
		protected BTDAppComponents _bTDAppComponent;
        protected long? _creatorID;

		#endregion

		#region Constructors

		public OtherReturn() { }

        public OtherReturn(long labID, long recordID, long parReturnID, string title, string content, int maxNum, string creator, DateTime dataAddTime, byte[] dataTimeStamp, BTDAppComponents bTDAppComponent, long creatorID)
		{
			this._labID = labID;
			this._recordID = recordID;
			this._parReturnID = parReturnID;
			this._title = title;
			this._content = content;
			this._maxNum = maxNum;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bTDAppComponent = bTDAppComponent;
            this._creatorID = creatorID;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "记录索引", ShortCode = "RecordID", Desc = "记录索引", ContextType = SysDic.Number, Length = 8)]
		public virtual long RecordID
		{
			get { return _recordID; }
			set { _recordID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "父节点ID", ShortCode = "ParReturnID", Desc = "父节点ID", ContextType = SysDic.Number, Length = 8)]
		public virtual long ParReturnID
		{
			get { return _parReturnID; }
			set { _parReturnID = value; }
		}

        [DataMember]
        [DataDesc(CName = "标题", ShortCode = "Title", Desc = "标题", ContextType = SysDic.NText, Length = 512)]
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
        [DataDesc(CName = "内容", ShortCode = "Content", Desc = "内容", ContextType = SysDic.NText)]
		public virtual string Content
		{
			get { return _content; }
			set
			{
				if ( value != null && value.Length > 10000000)
					throw new ArgumentOutOfRangeException("Invalid value for Content", value, value.ToString());
				_content = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "支持数量", ShortCode = "MaxNum", Desc = "支持数量", ContextType = SysDic.Number, Length = 4)]
		public virtual int MaxNum
		{
			get { return _maxNum; }
			set { _maxNum = value; }
		}

        [DataMember]
        [DataDesc(CName = "发布者", ShortCode = "Creator", Desc = "发布者", ContextType = SysDic.NText, Length = 20)]
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
        [DataDesc(CName = "应用组件", ShortCode = "BTDAppComponent", Desc = "应用组件")]
		public virtual BTDAppComponents BTDAppComponent
		{
			get { return _bTDAppComponent; }
			set { _bTDAppComponent = value; }
		}

        [DataMember]
        [DataDesc(CName = "发布者ID", ShortCode = "CreatorID", Desc = "发布者ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

		#endregion
	}
	#endregion
}