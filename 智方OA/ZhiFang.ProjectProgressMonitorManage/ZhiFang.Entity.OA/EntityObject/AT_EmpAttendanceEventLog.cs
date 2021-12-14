using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA
{
	#region ATEmpAttendanceEventLog

	/// <summary>
	/// ATEmpAttendanceEventLog object for NHibernate mapped table 'AT_EmpAttendanceEventLog'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "员工考勤事件日志表", ClassCName = "ATEmpAttendanceEventLog", ShortCode = "ATEmpAttendanceEventLog", Desc = "员工考勤事件日志表")]
	public class ATEmpAttendanceEventLog : BaseEntity
	{
		#region Member Variables
		
        protected string _aTEventTypeName;
        protected string _aTEventSubTypeName;
        protected DateTime? _startDateTime;
        protected DateTime? _endDateTime;
        protected string _aTEventDateCode;
        protected string _memo;
        protected long? _approveID;
        protected string _approveName;
        protected DateTime? _approveDateTime;
        protected string _approveMemo;
        protected string _approveStatusName;
        protected string _aTEventLogPostion;
        protected string _aTEventLogPostionName;
        protected bool _IsOffsite;
        protected double _evenLength;
        protected string _eventStatPostion;
        protected string _eventDestinationPostion;
        protected string _transportationName;
        protected long? _applyID;
        protected string _applyName;
		protected ATApproveStatus _aTApproveStatus;
		protected long _aTEventTypeID;
        protected long _aTEventSubTypeID;
        protected ATTransportation _aTTransportation;

		#endregion

		#region Constructors

		public ATEmpAttendanceEventLog() { }

		public ATEmpAttendanceEventLog( long labID, string aTEventTypeName, DateTime startDateTime, DateTime endDateTime, string aTEventDateCode, DateTime dataAddTime, string memo, long approveID, string approveName, DateTime approveDateTime, string approveMemo, string approveStatusName, string aTEventLogPostion, string aTEventLogPostionName, double evenLength, string eventStatPostion, string eventDestinationPostion, string transportationName, long applyID, string applyName, byte[] dataTimeStamp, ATApproveStatus aTApproveStatus, long aTAttendanceEventType, ATTransportation aTTransportation )
		{
			this._labID = labID;
			this._aTEventTypeName = aTEventTypeName;
			this._startDateTime = startDateTime;
			this._endDateTime = endDateTime;
			this._aTEventDateCode = aTEventDateCode;
			this._dataAddTime = dataAddTime;
			this._memo = memo;
			this._approveID = approveID;
			this._approveName = approveName;
			this._approveDateTime = approveDateTime;
			this._approveMemo = approveMemo;
			this._approveStatusName = approveStatusName;
			this._aTEventLogPostion = aTEventLogPostion;
			this._aTEventLogPostionName = aTEventLogPostionName;
			this._evenLength = evenLength;
			this._eventStatPostion = eventStatPostion;
			this._eventDestinationPostion = eventDestinationPostion;
			this._transportationName = transportationName;
			this._applyID = applyID;
			this._applyName = applyName;
			this._dataTimeStamp = dataTimeStamp;
			this._aTApproveStatus = aTApproveStatus;
			this._aTEventTypeID = aTAttendanceEventType;
			this._aTTransportation = aTTransportation;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "事件类型名称", ShortCode = "ATEventTypeName", Desc = "事件类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ATEventTypeName
		{
			get { return _aTEventTypeName; }
			set
			{
				_aTEventTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "事件子类型名称", ShortCode = "ATEventSubTypeName", Desc = "事件子类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ATEventSubTypeName
        {
            get { return _aTEventSubTypeName; }
            set
            {
                _aTEventSubTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开始时间", ShortCode = "StartDateTime", Desc = "开始时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? StartDateTime
		{
			get { return _startDateTime; }
			set { _startDateTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结束时间", ShortCode = "EndDateTime", Desc = "结束时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? EndDateTime
		{
			get { return _endDateTime; }
			set { _endDateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "事件日期编码", ShortCode = "ATEventDateCode", Desc = "事件日期编码", ContextType = SysDic.All, Length = 50)]
        public virtual string ATEventDateCode
		{
			get { return _aTEventDateCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ATEventDateCode", value, value.ToString());
				_aTEventDateCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批人ID", ShortCode = "ApproveID", Desc = "审批人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ApproveID
		{
			get { return _approveID; }
			set { _approveID = value; }
		}

        [DataMember]
        [DataDesc(CName = "审批人姓名", ShortCode = "ApproveName", Desc = "审批人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ApproveName
		{
			get { return _approveName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ApproveName", value, value.ToString());
				_approveName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批时间", ShortCode = "ApproveDateTime", Desc = "审批时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ApproveDateTime
		{
			get { return _approveDateTime; }
			set { _approveDateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "审批备注", ShortCode = "ApproveMemo", Desc = "审批备注", ContextType = SysDic.All, Length = 500)]
        public virtual string ApproveMemo
		{
			get { return _approveMemo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ApproveMemo", value, value.ToString());
				_approveMemo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ApproveStatusName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ApproveStatusName
		{
			get { return _approveStatusName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ApproveStatusName", value, value.ToString());
				_approveStatusName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "事件位置坐标", ShortCode = "ATEventLogPostion", Desc = "事件位置坐标", ContextType = SysDic.All, Length = 50)]
        public virtual string ATEventLogPostion
		{
			get { return _aTEventLogPostion; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ATEventLogPostion", value, value.ToString());
				_aTEventLogPostion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "事件位置名称", ShortCode = "ATEventLogPostionName", Desc = "事件位置名称", ContextType = SysDic.All, Length = 5000)]
        public virtual string ATEventLogPostionName
		{
			get { return _aTEventLogPostionName; }
			set
			{
				if ( value != null && value.Length > 5000)
					throw new ArgumentOutOfRangeException("Invalid value for ATEventLogPostionName", value, value.ToString());
				_aTEventLogPostionName = value;
			}
		}


        [DataMember]
        [DataDesc(CName = "是否脱岗", ShortCode = "IsOffsite", Desc = "是否脱岗", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsOffsite
        {
            get { return _IsOffsite; }
            set
            {
                _IsOffsite = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "事件时长", ShortCode = "EvenLength", Desc = "事件时长", ContextType = SysDic.All, Length = 8)]
        public virtual double EvenLength
		{
			get { return _evenLength; }
			set { _evenLength = value; }
		}

        [DataMember]
        [DataDesc(CName = "始发地点", ShortCode = "EventStatPostion", Desc = "始发地点", ContextType = SysDic.All, Length = 50)]
        public virtual string EventStatPostion
		{
			get { return _eventStatPostion; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EventStatPostion", value, value.ToString());
				_eventStatPostion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "目标地点", ShortCode = "EventDestinationPostion", Desc = "目标地点", ContextType = SysDic.All, Length = 50)]
        public virtual string EventDestinationPostion
		{
			get { return _eventDestinationPostion; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EventDestinationPostion", value, value.ToString());
				_eventDestinationPostion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "交通工具名称", ShortCode = "TransportationName", Desc = "交通工具名称", ContextType = SysDic.All, Length = 50)]
        public virtual string TransportationName
		{
			get { return _transportationName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TransportationName", value, value.ToString());
				_transportationName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请人ID", ShortCode = "ApplyID", Desc = "申请人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ApplyID
		{
			get { return _applyID; }
			set { _applyID = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请人姓名", ShortCode = "ApplyName", Desc = "申请人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ApplyName
		{
			get { return _applyName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ApplyName", value, value.ToString());
				_applyName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "审批状态", ShortCode = "ATApproveStatus", Desc = "审批状态")]
		public virtual ATApproveStatus ATApproveStatus
		{
			get { return _aTApproveStatus; }
			set { _aTApproveStatus = value; }
		}

        [DataMember]
        [DataDesc(CName = "考勤事件类型表", ShortCode = "ATEventTypeID", Desc = "考勤事件类型表")]
		public virtual long ATEventTypeID
        {
			get { return _aTEventTypeID; }
			set { _aTEventTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "考勤事件子类型表", ShortCode = "ATEventSubTypeID", Desc = "考勤事件子类型表")]
        public virtual long ATEventSubTypeID
        {
            get { return _aTEventSubTypeID; }
            set { _aTEventSubTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "交通工具", ShortCode = "ATTransportation", Desc = "交通工具")]
		public virtual ATTransportation ATTransportation
		{
			get { return _aTTransportation; }
			set { _aTTransportation = value; }
		}

        
		#endregion
	}
	#endregion
}