using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PTaskCopyFor

	/// <summary>
	/// PTaskCopyFor object for NHibernate mapped table 'P_TaskCopyFor'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "抄送关系表", ClassCName = "PTaskCopyFor", ShortCode = "PTaskCopyFor", Desc = "抄送关系表")]
	public class PTaskCopyFor : BaseEntity
	{
		#region Member Variables
		
        protected string _taskName;
        protected long? _copyForEmpID;
        protected string _copyForEmpName;
        protected DateTime? _dataUpdateTime;
        protected long? _publishEmpID;
        protected string _publishEmpName;
		protected PTask _pTask;

		#endregion

		#region Constructors

		public PTaskCopyFor() { }

		public PTaskCopyFor( long labID, string taskName, long copyForEmpID, string copyForEmpName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long publishEmpID, string publishEmpName, PTask pTask )
		{
			this._labID = labID;
			this._taskName = taskName;
			this._copyForEmpID = copyForEmpID;
			this._copyForEmpName = copyForEmpName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._publishEmpID = publishEmpID;
			this._publishEmpName = publishEmpName;
			this._pTask = pTask;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "TaskName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string TaskName
		{
			get { return _taskName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TaskName", value, value.ToString());
				_taskName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "抄送者ID", ShortCode = "CopyForEmpID", Desc = "抄送者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? CopyForEmpID
		{
			get { return _copyForEmpID; }
			set { _copyForEmpID = value; }
		}

        [DataMember]
        [DataDesc(CName = "抄送者姓名", ShortCode = "CopyForEmpName", Desc = "抄送者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CopyForEmpName
		{
			get { return _copyForEmpName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CopyForEmpName", value, value.ToString());
				_copyForEmpName = value;
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
        [DataDesc(CName = "任务表", ShortCode = "PTask", Desc = "任务表")]
		public virtual PTask PTask
		{
			get { return _pTask; }
			set { _pTask = value; }
		}

        
		#endregion
	}
	#endregion
}