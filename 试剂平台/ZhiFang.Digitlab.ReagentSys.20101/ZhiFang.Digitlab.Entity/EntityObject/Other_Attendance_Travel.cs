using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherAttendanceTravel

	/// <summary>
	/// OtherAttendanceTravel object for NHibernate mapped table 'Other_Attendance_Travel'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "考勤-出差", ClassCName = "OtherAttendanceTravel", ShortCode = "OtherAttendanceTravel", Desc = "考勤-出差")]
    public class OtherAttendanceTravel : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _creatorID;
        protected string _creator;
        protected long? _travelManID;
        protected string _travelMan;
        protected DateTime? _travelTime;
        protected DateTime? _planBackTime;
        protected DateTime? _backTime;
        protected string _contents;
        protected string _comment;
        protected string _summary;
        protected int _isHasAttachment;
        protected long? _checkerID;
        protected string _checker;
        protected DateTime? _dataUpdateTime;
        protected string _checkOpinion;
        

		#endregion

		#region Constructors

		public OtherAttendanceTravel() { }

		public OtherAttendanceTravel( long labID, long creatorID, string creator, DateTime dataAddTime, long travelManID, string travelMan, DateTime travelTime, DateTime planBackTime, DateTime backTime, string contents, string comment, string summary, int isHasAttachment, long checkerID, string checker, DateTime dataUpdateTime, string checkOpinion, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._creatorID = creatorID;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._travelManID = travelManID;
			this._travelMan = travelMan;
			this._travelTime = travelTime;
			this._planBackTime = planBackTime;
			this._backTime = backTime;
			this._contents = contents;
			this._comment = comment;
			this._summary = summary;
			this._isHasAttachment = isHasAttachment;
			this._checkerID = checkerID;
			this._checker = checker;
			this._dataUpdateTime = dataUpdateTime;
			this._checkOpinion = checkOpinion;
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
        [DataDesc(CName = "创建人", ShortCode = "Creator", Desc = "创建人", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "出差人ID", ShortCode = "TravelManID", Desc = "出差人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TravelManID
		{
			get { return _travelManID; }
			set { _travelManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "出差人", ShortCode = "TravelMan", Desc = "出差人", ContextType = SysDic.All, Length = 20)]
        public virtual string TravelMan
		{
			get { return _travelMan; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for TravelMan", value, value.ToString());
				_travelMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计划出差时间", ShortCode = "TravelTime", Desc = "计划出差时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TravelTime
		{
			get { return _travelTime; }
			set { _travelTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计划回来时间", ShortCode = "PlanBackTime", Desc = "计划回来时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PlanBackTime
		{
			get { return _planBackTime; }
			set { _planBackTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际回来时间", ShortCode = "BackTime", Desc = "实际回来时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BackTime
		{
			get { return _backTime; }
			set { _backTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "事由", ShortCode = "Contents", Desc = "事由", ContextType = SysDic.All, Length = 16)]
        public virtual string Contents
		{
			get { return _contents; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Contents", value, value.ToString());
				_contents = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "说明", ShortCode = "Comment", Desc = "说明", ContextType = SysDic.All, Length = 16)]
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
        [DataDesc(CName = "总结", ShortCode = "Summary", Desc = "总结", ContextType = SysDic.All, Length = 16)]
        public virtual string Summary
		{
			get { return _summary; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Summary", value, value.ToString());
				_summary = value;
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
        [DataDesc(CName = "审核时间", ShortCode = "DataUpdateTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
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