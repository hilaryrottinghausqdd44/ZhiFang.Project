using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherAttendanceRegister

	/// <summary>
	/// OtherAttendanceRegister object for NHibernate mapped table 'Other_Attendance_Register'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "考勤-登记", ClassCName = "OtherAttendanceRegister", ShortCode = "OtherAttendanceRegister", Desc = "考勤-登记")]
    public class OtherAttendanceRegister : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _attendanceTypeID;
        protected long? _creatorID;
        protected string _creator;
        protected long? _checkerID;
        protected string _checker;
        protected DateTime? _dataUpdateTime;
        protected string _checkOpinion;
        protected string _comment;
        

		#endregion

		#region Constructors

		public OtherAttendanceRegister() { }

		public OtherAttendanceRegister( long labID, long attendanceTypeID, long creatorID, string creator, DateTime dataAddTime, long checkerID, string checker, DateTime dataUpdateTime, string checkOpinion, string comment, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._attendanceTypeID = attendanceTypeID;
			this._creatorID = creatorID;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._checkerID = checkerID;
			this._checker = checker;
			this._dataUpdateTime = dataUpdateTime;
			this._checkOpinion = checkOpinion;
			this._comment = comment;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "考勤类型ID", ShortCode = "AttendanceTypeID", Desc = "考勤类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AttendanceTypeID
		{
			get { return _attendanceTypeID; }
			set { _attendanceTypeID = value; }
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
        [DataDesc(CName = "登记人", ShortCode = "Creator", Desc = "登记人", ContextType = SysDic.All, Length = 20)]
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

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
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

		#endregion
	}
	#endregion
}