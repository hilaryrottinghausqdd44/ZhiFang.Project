using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PWorkLogSendFor

	/// <summary>
	/// PWorkLogSendFor object for NHibernate mapped table 'P_WorkLogSendFor'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "工作日志发送关系表（日、周、月）", ClassCName = "PWorkLogSendFor", ShortCode = "PWorkLogSendFor", Desc = "工作日志发送关系表（日、周、月）")]
	public class PWorkLogSendFor : BaseEntity
	{
		#region Member Variables
		
        protected string _logType;
        protected long? _logID;
        protected long? _receiveEmpID;
        protected string _receiveEmpName;
        protected long? _publishEmpID;
        protected string _publishEmpName;
        protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public PWorkLogSendFor() { }

		public PWorkLogSendFor( long labID, string logType, long logID, long receiveEmpID, string receiveEmpName, long publishEmpID, string publishEmpName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._logType = logType;
			this._logID = logID;
			this._receiveEmpID = receiveEmpID;
			this._receiveEmpName = receiveEmpName;
			this._publishEmpID = publishEmpID;
			this._publishEmpName = publishEmpName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "日志类型", ShortCode = "LogType", Desc = "日志类型", ContextType = SysDic.All, Length = 10)]
        public virtual string LogType
		{
			get { return _logType; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for LogType", value, value.ToString());
				_logType = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LogID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? LogID
		{
			get { return _logID; }
			set { _logID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收者ID", ShortCode = "ReceiveEmpID", Desc = "接收者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReceiveEmpID
		{
			get { return _receiveEmpID; }
			set { _receiveEmpID = value; }
		}

        [DataMember]
        [DataDesc(CName = "接收者姓名", ShortCode = "ReceiveEmpName", Desc = "接收者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceiveEmpName
		{
			get { return _receiveEmpName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveEmpName", value, value.ToString());
				_receiveEmpName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发送者ID", ShortCode = "PublishEmpID", Desc = "发送者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? PublishEmpID
		{
			get { return _publishEmpID; }
			set { _publishEmpID = value; }
		}

        [DataMember]
        [DataDesc(CName = "发送者姓名", ShortCode = "PublishEmpName", Desc = "发送者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string PublishEmpName
		{
			get { return _publishEmpName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PublishEmpName", value, value.ToString());
				_publishEmpName = value;
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