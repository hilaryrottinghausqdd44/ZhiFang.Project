using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherInfoRelease

	/// <summary>
	/// OtherInfoRelease object for NHibernate mapped table 'Other_InfoRelease'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "信息发布", ClassCName = "OtherInfoRelease", ShortCode = "OtherInfoRelease", Desc = "信息发布")]
    public class OtherInfoRelease : BaseEntity
	{
		#region Member Variables
		
		
		protected string _title;
		protected string _infoType;
		protected string _keyWord;
		protected string _tabloid;
		protected string _content;
		protected int _isHasPicture;
		protected int _isHasAttachment;
		protected int _isAllowsReturn;
		protected string _originate;
		protected int _readTimes;
		protected int _returnTimes;
		protected string _creator;
		protected string _checker;
		protected DateTime? _checkTime;
		protected string _releaseMan;
		protected DateTime? _releaseTime;
		protected bool _isUse;
		protected long? _creatorID;
		protected long? _checkerID;
		protected long? _releaseManID;

		#endregion

		#region Constructors

		public OtherInfoRelease() { }

		public OtherInfoRelease( long labID, string title, string infoType, string keyWord, string tabloid, string content, int isHasPicture, int isHasAttachment, int isAllowsReturn, string originate, int readTimes, int returnTimes, string creator, DateTime dataAddTime, string checker, DateTime checkTime, string releaseMan, DateTime releaseTime, bool isUse, byte[] dataTimeStamp, long creatorID, long checkerID, long releaseManID )
		{
			this._labID = labID;
			this._title = title;
			this._infoType = infoType;
			this._keyWord = keyWord;
			this._tabloid = tabloid;
			this._content = content;
			this._isHasPicture = isHasPicture;
			this._isHasAttachment = isHasAttachment;
			this._isAllowsReturn = isAllowsReturn;
			this._originate = originate;
			this._readTimes = readTimes;
			this._returnTimes = returnTimes;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._checker = checker;
			this._checkTime = checkTime;
			this._releaseMan = releaseMan;
			this._releaseTime = releaseTime;
			this._isUse = isUse;
			this._dataTimeStamp = dataTimeStamp;
			this._creatorID = creatorID;
			this._checkerID = checkerID;
			this._releaseManID = releaseManID;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "标题", ShortCode = "Title", Desc = "标题", ContextType = SysDic.NText, Length = 256)]
		public virtual string Title
		{
			get { return _title; }
			set
			{
				if ( value != null && value.Length > 256)
					throw new ArgumentOutOfRangeException("Invalid value for Title", value, value.ToString());
				_title = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "类型", ShortCode = "InfoType", Desc = "类型", ContextType = SysDic.NText, Length = 20)]
		public virtual string InfoType
		{
			get { return _infoType; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for InfoType", value, value.ToString());
				_infoType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "关键字", ShortCode = "KeyWord", Desc = "关键字", ContextType = SysDic.NText, Length = 512)]
		public virtual string KeyWord
		{
			get { return _keyWord; }
			set
			{
				if ( value != null && value.Length > 512)
					throw new ArgumentOutOfRangeException("Invalid value for KeyWord", value, value.ToString());
				_keyWord = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "概要", ShortCode = "Tabloid", Desc = "概要", ContextType = SysDic.NText, Length = 512)]
		public virtual string Tabloid
		{
			get { return _tabloid; }
			set
			{
				if ( value != null && value.Length > 512)
					throw new ArgumentOutOfRangeException("Invalid value for Tabloid", value, value.ToString());
				_tabloid = value;
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
        [DataDesc(CName = "是否有图片", ShortCode = "IsHasPicture", Desc = "是否有图片", ContextType = SysDic.Number, Length = 4)]
		public virtual int IsHasPicture
		{
			get { return _isHasPicture; }
			set { _isHasPicture = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否有附件", ShortCode = "IsHasAttachment", Desc = "是否有附件", ContextType = SysDic.Number, Length = 4)]
		public virtual int IsHasAttachment
		{
			get { return _isHasAttachment; }
			set { _isHasAttachment = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否允许回复", ShortCode = "IsAllowsReturn", Desc = "是否允许回复", ContextType = SysDic.Number, Length = 4)]
		public virtual int IsAllowsReturn
		{
			get { return _isAllowsReturn; }
			set { _isAllowsReturn = value; }
		}

        [DataMember]
        [DataDesc(CName = "信息来源", ShortCode = "Originate", Desc = "信息来源", ContextType = SysDic.NText, Length = 256)]
		public virtual string Originate
		{
			get { return _originate; }
			set
			{
				if ( value != null && value.Length > 256)
					throw new ArgumentOutOfRangeException("Invalid value for Originate", value, value.ToString());
				_originate = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "阅读次数", ShortCode = "ReadTimes", Desc = "阅读次数", ContextType = SysDic.Number, Length = 4)]
		public virtual int ReadTimes
		{
			get { return _readTimes; }
			set { _readTimes = value; }
		}

        [DataMember]
        [DataDesc(CName = "回复数量", ShortCode = "ReturnTimes", Desc = "回复数量", ContextType = SysDic.Number, Length = 4)]
		public virtual int ReturnTimes
		{
			get { return _returnTimes; }
			set { _returnTimes = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.NText, Length = 20)]
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
        [DataDesc(CName = "审核人", ShortCode = "Checker", Desc = "审核人", ContextType = SysDic.NText, Length = 20)]
		public virtual string Checker
		{
			get { return _checker; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
				_checker = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "发布人", ShortCode = "ReleaseMan", Desc = "发布人", ContextType = SysDic.NText, Length = 20)]
		public virtual string ReleaseMan
		{
			get { return _releaseMan; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReleaseMan", value, value.ToString());
				_releaseMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发布时间", ShortCode = "ReleaseTime", Desc = "发布时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? ReleaseTime
		{
			get { return _releaseTime; }
			set { _releaseTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建人ID", ShortCode = "CreatorID", Desc = "创建人ID", ContextType = SysDic.Number, Length = 8)]
		public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人ID", ShortCode = "CheckerID", Desc = "审核人ID", ContextType = SysDic.Number, Length = 8)]
		public virtual long? CheckerID
		{
			get { return _checkerID; }
			set { _checkerID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发布人ID", ShortCode = "ReleaseManID", Desc = "发布人ID", ContextType = SysDic.Number, Length = 8)]
		public virtual long? ReleaseManID
		{
			get { return _releaseManID; }
			set { _releaseManID = value; }
		}

		#endregion
	}
	#endregion
}