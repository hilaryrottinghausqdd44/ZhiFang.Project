using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA
{
	#region ATEmpAttendanceEventParaSettings

	/// <summary>
	/// ATEmpAttendanceEventParaSettings object for NHibernate mapped table 'AT_EmpAttendanceEventParaSettings'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "员工考勤事件参数设置表", ClassCName = "ATEmpAttendanceEventParaSettings", ShortCode = "ATEmpAttendanceEventParaSettings", Desc = "员工考勤事件参数设置表")]
	public class ATEmpAttendanceEventParaSettings : BaseEntity
	{
		#region Member Variables
		
        protected int _aTEventParaSettingsType;
        protected int _empWorkLong;
        protected long? _empID;
        protected string _empName;
        protected DateTime? _signInTime;
        protected DateTime? _signOutTime;
        protected string _aTEventPostion;
        protected string _aTEventPostionName;
        protected int _aTEventPostionRange;
        protected DateTime? _timingOneTime;
        protected DateTime? _timingTwoTime;
        protected DateTime? _timingThreeTime;
        protected DateTime? _timingFourTime;
        protected DateTime? _timingFiveTime;
        protected string _timingOnePostion;
        protected string _timingTwoPostion;
        protected string _timingThreePostion;
        protected string _timingFourPostion;
        protected string _timingFivePostion;
        protected string _timingOnePostionName;
        protected string _timingTwoPostionName;
        protected string _timingThreePostionName;
        protected string _timingFourPostionName;
        protected string _timingFivePostionName;
        protected int _timingPostionRange;
        protected int _timingTimeRange;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected long? _createrID;
        protected string _createrName;
		

		#endregion

		#region Constructors

		public ATEmpAttendanceEventParaSettings() { }

		public ATEmpAttendanceEventParaSettings( long labID, int aTEventParaSettingsType, int empWorkLong, long empID, string empName, DateTime signInTime, DateTime signOutTime, string aTEventPostion, string aTEventPostionName, int aTEventPostionRange, DateTime timingOneTime, DateTime timingTwoTime, DateTime timingThreeTime, DateTime timingFourTime, DateTime timingFiveTime, string timingOnePostion, string timingTwoPostion, string timingThreePostion, string timingFourPostion, string timingFivePostion, string timingOnePostionName, string timingTwoPostionName, string timingThreePostionName, string timingFourPostionName, string timingFivePostionName, int timingPostionRange, int timingTimeRange, string pinYinZiTou, int dispOrder, string comment, bool isUse, long createrID, string createrName, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._aTEventParaSettingsType = aTEventParaSettingsType;
			this._empWorkLong = empWorkLong;
			this._empID = empID;
			this._empName = empName;
			this._signInTime = signInTime;
			this._signOutTime = signOutTime;
			this._aTEventPostion = aTEventPostion;
			this._aTEventPostionName = aTEventPostionName;
			this._aTEventPostionRange = aTEventPostionRange;
			this._timingOneTime = timingOneTime;
			this._timingTwoTime = timingTwoTime;
			this._timingThreeTime = timingThreeTime;
			this._timingFourTime = timingFourTime;
			this._timingFiveTime = timingFiveTime;
			this._timingOnePostion = timingOnePostion;
			this._timingTwoPostion = timingTwoPostion;
			this._timingThreePostion = timingThreePostion;
			this._timingFourPostion = timingFourPostion;
			this._timingFivePostion = timingFivePostion;
			this._timingOnePostionName = timingOnePostionName;
			this._timingTwoPostionName = timingTwoPostionName;
			this._timingThreePostionName = timingThreePostionName;
			this._timingFourPostionName = timingFourPostionName;
			this._timingFivePostionName = timingFivePostionName;
			this._timingPostionRange = timingPostionRange;
			this._timingTimeRange = timingTimeRange;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._createrID = createrID;
			this._createrName = createrName;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "参数设置类型，0：固定时间，1：弹性时间。", ShortCode = "ATEventParaSettingsType", Desc = "参数设置类型，0：固定时间，1：弹性时间。", ContextType = SysDic.All, Length = 4)]
        public virtual int ATEventParaSettingsType
		{
			get { return _aTEventParaSettingsType; }
			set { _aTEventParaSettingsType = value; }
		}

        [DataMember]
        [DataDesc(CName = "工作时长", ShortCode = "EmpWorkLong", Desc = "工作时长", ContextType = SysDic.All, Length = 4)]
        public virtual int EmpWorkLong
		{
			get { return _empWorkLong; }
			set { _empWorkLong = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID", ShortCode = "EmpID", Desc = "员工ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? EmpID
		{
			get { return _empID; }
			set { _empID = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工姓名", ShortCode = "EmpName", Desc = "员工姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string EmpName
		{
			get { return _empName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
				_empName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签到时间", ShortCode = "SignInTime", Desc = "签到时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SignInTime
		{
			get { return _signInTime; }
			set { _signInTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签退时间", ShortCode = "SignOutTime", Desc = "签退时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SignOutTime
		{
			get { return _signOutTime; }
			set { _signOutTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "考勤地点", ShortCode = "ATEventPostion", Desc = "考勤地点", ContextType = SysDic.All, Length = 100)]
        public virtual string ATEventPostion
		{
			get { return _aTEventPostion; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ATEventPostion", value, value.ToString());
				_aTEventPostion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "考勤地点名称", ShortCode = "ATEventPostionName", Desc = "考勤地点名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ATEventPostionName
		{
			get { return _aTEventPostionName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ATEventPostionName", value, value.ToString());
				_aTEventPostionName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "考勤地点范围(米)", ShortCode = "ATEventPostionRange", Desc = "考勤地点范围(米)", ContextType = SysDic.All, Length = 4)]
        public virtual int ATEventPostionRange
		{
			get { return _aTEventPostionRange; }
			set { _aTEventPostionRange = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定时上报1时间", ShortCode = "TimingOneTime", Desc = "定时上报1时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TimingOneTime
		{
			get { return _timingOneTime; }
			set { _timingOneTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定时上报2时间", ShortCode = "TimingTwoTime", Desc = "定时上报2时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TimingTwoTime
		{
			get { return _timingTwoTime; }
			set { _timingTwoTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定时上报3时间", ShortCode = "TimingThreeTime", Desc = "定时上报3时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TimingThreeTime
		{
			get { return _timingThreeTime; }
			set { _timingThreeTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定时上报4时间", ShortCode = "TimingFourTime", Desc = "定时上报4时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TimingFourTime
		{
			get { return _timingFourTime; }
			set { _timingFourTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定时上报5时间", ShortCode = "TimingFiveTime", Desc = "定时上报5时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TimingFiveTime
		{
			get { return _timingFiveTime; }
			set { _timingFiveTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "定时上报1地点", ShortCode = "TimingOnePostion", Desc = "定时上报1地点", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingOnePostion
		{
			get { return _timingOnePostion; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingOnePostion", value, value.ToString());
				_timingOnePostion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报2地点", ShortCode = "TimingTwoPostion", Desc = "定时上报2地点", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingTwoPostion
		{
			get { return _timingTwoPostion; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingTwoPostion", value, value.ToString());
				_timingTwoPostion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报3地点", ShortCode = "TimingThreePostion", Desc = "定时上报3地点", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingThreePostion
		{
			get { return _timingThreePostion; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingThreePostion", value, value.ToString());
				_timingThreePostion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报4地点", ShortCode = "TimingFourPostion", Desc = "定时上报4地点", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingFourPostion
		{
			get { return _timingFourPostion; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingFourPostion", value, value.ToString());
				_timingFourPostion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报5地点", ShortCode = "TimingFivePostion", Desc = "定时上报5地点", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingFivePostion
		{
			get { return _timingFivePostion; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingFivePostion", value, value.ToString());
				_timingFivePostion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报1地点名称", ShortCode = "TimingOnePostionName", Desc = "定时上报1地点名称", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingOnePostionName
		{
			get { return _timingOnePostionName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingOnePostionName", value, value.ToString());
				_timingOnePostionName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报2地点名称", ShortCode = "TimingTwoPostionName", Desc = "定时上报2地点名称", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingTwoPostionName
		{
			get { return _timingTwoPostionName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingTwoPostionName", value, value.ToString());
				_timingTwoPostionName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报3地点名称", ShortCode = "TimingThreePostionName", Desc = "定时上报3地点名称", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingThreePostionName
		{
			get { return _timingThreePostionName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingThreePostionName", value, value.ToString());
				_timingThreePostionName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报4地点名称", ShortCode = "TimingFourPostionName", Desc = "定时上报4地点名称", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingFourPostionName
		{
			get { return _timingFourPostionName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingFourPostionName", value, value.ToString());
				_timingFourPostionName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报5地点名称", ShortCode = "TimingFivePostionName", Desc = "定时上报5地点名称", ContextType = SysDic.All, Length = 100)]
        public virtual string TimingFivePostionName
		{
			get { return _timingFivePostionName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TimingFivePostionName", value, value.ToString());
				_timingFivePostionName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定时上报地点范围(米)", ShortCode = "TimingPostionRange", Desc = "定时上报地点范围(米)", ContextType = SysDic.All, Length = 4)]
        public virtual int TimingPostionRange
		{
			get { return _timingPostionRange; }
			set { _timingPostionRange = value; }
		}

        [DataMember]
        [DataDesc(CName = "上报时间范围(分钟)", ShortCode = "TimingTimeRange", Desc = "上报时间范围(分钟)", ContextType = SysDic.All, Length = 4)]
        public virtual int TimingTimeRange
		{
			get { return _timingTimeRange; }
			set { _timingTimeRange = value; }
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 300)]
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
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
		{
			get { return _createrID; }
			set { _createrID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
		{
			get { return _createrName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
				_createrName = value;
			}
		}

		
		#endregion
	}
	#endregion
}