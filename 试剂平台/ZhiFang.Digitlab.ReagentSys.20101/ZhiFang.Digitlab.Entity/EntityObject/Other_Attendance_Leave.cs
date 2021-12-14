using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherAttendanceLeave

	/// <summary>
	/// OtherAttendanceLeave object for NHibernate mapped table 'Other_Attendance_Leave'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "考勤-请假", ClassCName = "OtherAttendanceLeave", ShortCode = "OtherAttendanceLeave", Desc = "考勤-请假")]
    public class OtherAttendanceLeave : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _leaveTypeID;
        protected long? _checkStatusID;
        protected long? _creatorID;
        protected string _creator;
        protected long? _leaveManID;
        protected string _leaveMan;
        protected DateTime? _leaveStartTime;
        protected DateTime? _leaveEndTime;
        protected string _contents;
        protected string _comment;
        protected long? _checkerID;
        protected string _checker;
        protected DateTime? _checkTime;
        protected string _checkOpinion;
        protected long? _approverID;
        protected string _approver;
        protected DateTime? _approvalTime;
        protected string _approvalOpinion;
        

		#endregion

		#region Constructors

		public OtherAttendanceLeave() { }

		public OtherAttendanceLeave( long labID, long leaveTypeID, long checkStatusID, long creatorID, string creator, DateTime dataAddTime, long leaveManID, string leaveMan, DateTime leaveStartTime, DateTime leaveEndTime, string contents, string comment, long checkerID, string checker, DateTime checkTime, string checkOpinion, long approverID, string approver, DateTime approvalTime, string approvalOpinion, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._leaveTypeID = leaveTypeID;
			this._checkStatusID = checkStatusID;
			this._creatorID = creatorID;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._leaveManID = leaveManID;
			this._leaveMan = leaveMan;
			this._leaveStartTime = leaveStartTime;
			this._leaveEndTime = leaveEndTime;
			this._contents = contents;
			this._comment = comment;
			this._checkerID = checkerID;
			this._checker = checker;
			this._checkTime = checkTime;
			this._checkOpinion = checkOpinion;
			this._approverID = approverID;
			this._approver = approver;
			this._approvalTime = approvalTime;
			this._approvalOpinion = approvalOpinion;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "请假类型ID", ShortCode = "LeaveTypeID", Desc = "请假类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LeaveTypeID
		{
			get { return _leaveTypeID; }
			set { _leaveTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核状态ID", ShortCode = "CheckStatusID", Desc = "审核状态ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckStatusID
		{
			get { return _checkStatusID; }
			set { _checkStatusID = value; }
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
        [DataDesc(CName = "请假人ID", ShortCode = "LeaveManID", Desc = "请假人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LeaveManID
		{
			get { return _leaveManID; }
			set { _leaveManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "请假人", ShortCode = "LeaveMan", Desc = "请假人", ContextType = SysDic.All, Length = 20)]
        public virtual string LeaveMan
		{
			get { return _leaveMan; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for LeaveMan", value, value.ToString());
				_leaveMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开始时间", ShortCode = "LeaveStartTime", Desc = "开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LeaveStartTime
		{
			get { return _leaveStartTime; }
			set { _leaveStartTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结束时间", ShortCode = "LeaveEndTime", Desc = "结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LeaveEndTime
		{
			get { return _leaveEndTime; }
			set { _leaveEndTime = value; }
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

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "ApproverID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApproverID
		{
			get { return _approverID; }
			set { _approverID = value; }
		}

        [DataMember]
        [DataDesc(CName = "审批人", ShortCode = "Approver", Desc = "审批人", ContextType = SysDic.All, Length = 20)]
        public virtual string Approver
		{
			get { return _approver; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Approver", value, value.ToString());
				_approver = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批时间", ShortCode = "ApprovalTime", Desc = "审批时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApprovalTime
		{
			get { return _approvalTime; }
			set { _approvalTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "审批意见", ShortCode = "ApprovalOpinion", Desc = "审批意见", ContextType = SysDic.All, Length = 16)]
        public virtual string ApprovalOpinion
		{
			get { return _approvalOpinion; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for ApprovalOpinion", value, value.ToString());
				_approvalOpinion = value;
			}
		}

		#endregion
	}
	#endregion
}