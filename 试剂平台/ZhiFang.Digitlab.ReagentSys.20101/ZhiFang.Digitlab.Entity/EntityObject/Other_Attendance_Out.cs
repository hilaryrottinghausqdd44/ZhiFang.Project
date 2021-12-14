using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherAttendanceOut

	/// <summary>
	/// OtherAttendanceOut object for NHibernate mapped table 'Other_Attendance_Out'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "考勤-外出", ClassCName = "OtherAttendanceOut", ShortCode = "OtherAttendanceOut", Desc = "考勤-外出")]
    public class OtherAttendanceOut : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _creatorID;
        protected string _creator;
        protected long? _outManID;
        protected string _outMan;
        protected DateTime? _outTime;
        protected DateTime? _backTime;
        protected string _contents;
        protected long? _checkerID;
        protected string _checker;
        protected DateTime? _dataUpdateTime;
        protected string _checkOpinion;
        protected string _comment;
        

		#endregion

		#region Constructors

		public OtherAttendanceOut() { }

		public OtherAttendanceOut( long labID, long creatorID, string creator, DateTime dataAddTime, long outManID, string outMan, DateTime outTime, DateTime backTime, string contents, long checkerID, string checker, DateTime dataUpdateTime, string checkOpinion, string comment, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._creatorID = creatorID;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._outManID = outManID;
			this._outMan = outMan;
			this._outTime = outTime;
			this._backTime = backTime;
			this._contents = contents;
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
        [DataDesc(CName = "外出人ID", ShortCode = "OutManID", Desc = "外出人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OutManID
		{
			get { return _outManID; }
			set { _outManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "外出人", ShortCode = "OutMan", Desc = "外出人", ContextType = SysDic.All, Length = 20)]
        public virtual string OutMan
		{
			get { return _outMan; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for OutMan", value, value.ToString());
				_outMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "外出时间", ShortCode = "OutTime", Desc = "外出时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OutTime
		{
			get { return _outTime; }
			set { _outTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "回来时间", ShortCode = "BackTime", Desc = "回来时间", ContextType = SysDic.All, Length = 8)]
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

		#endregion
	}
	#endregion
}