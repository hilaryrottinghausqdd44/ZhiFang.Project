using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherWorkReport

	/// <summary>
	/// OtherWorkReport object for NHibernate mapped table 'Other_WorkReport'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "工作报告", ClassCName = "OtherWorkReport", ShortCode = "OtherWorkReport", Desc = "工作报告")]
    public class OtherWorkReport : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _reportCycleID;
        protected long? _reportTypeID;
        protected long? _reportStatusID;
        protected string _title;
        protected string _content;
        protected string _department;
        protected int _isHasAttachment;
        protected long? _creatorID;
        protected string _creator;
        protected long? _checkerID;
        protected string _checker;
        protected DateTime? _checkTime;
        protected string _checkOpinion;
        

		#endregion

		#region Constructors

		public OtherWorkReport() { }

		public OtherWorkReport( long labID, long reportCycleID, long reportTypeID, long reportStatusID, string title, string content, string department, int isHasAttachment, long creatorID, string creator, DateTime dataAddTime, long checkerID, string checker, DateTime checkTime, string checkOpinion, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._reportCycleID = reportCycleID;
			this._reportTypeID = reportTypeID;
			this._reportStatusID = reportStatusID;
			this._title = title;
			this._content = content;
			this._department = department;
			this._isHasAttachment = isHasAttachment;
			this._creatorID = creatorID;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._checkerID = checkerID;
			this._checker = checker;
			this._checkTime = checkTime;
			this._checkOpinion = checkOpinion;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "报告周期ID", ShortCode = "ReportCycleID", Desc = "报告周期ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReportCycleID
		{
			get { return _reportCycleID; }
			set { _reportCycleID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "报告类型ID", ShortCode = "ReportTypeID", Desc = "报告类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReportTypeID
		{
			get { return _reportTypeID; }
			set { _reportTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "状态ID", ShortCode = "ReportStatusID", Desc = "状态ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReportStatusID
		{
			get { return _reportStatusID; }
			set { _reportStatusID = value; }
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
        [DataDesc(CName = "部门", ShortCode = "Department", Desc = "部门", ContextType = SysDic.All, Length = 200)]
        public virtual string Department
		{
			get { return _department; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Department", value, value.ToString());
				_department = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "CreatorID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "报告人", ShortCode = "Creator", Desc = "报告人", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "员工ID主键", ShortCode = "CheckerID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckerID
		{
			get { return _checkerID; }
			set { _checkerID = value; }
		}

        [DataMember]
        [DataDesc(CName = "审核人", ShortCode = "Checker", Desc = "审核人", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "审核意见", ShortCode = "CheckOpinion", Desc = "审核意见", ContextType = SysDic.All, Length = 16)]
        public virtual string CheckOpinion
		{
			get { return _checkOpinion; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for CheckOpinion", value, value.ToString());
				_checkOpinion = value;
			}
		}

		#endregion
	}
	#endregion
}