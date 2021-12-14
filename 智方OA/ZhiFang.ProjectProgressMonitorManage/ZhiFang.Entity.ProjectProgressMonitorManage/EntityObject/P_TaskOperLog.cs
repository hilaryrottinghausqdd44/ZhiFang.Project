using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PTaskOperLog

	/// <summary>
	/// PTaskOperLog object for NHibernate mapped table 'P_TaskOperLog'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "任务操作记录表", ClassCName = "PTaskOperLog", ShortCode = "PTaskOperLog", Desc = "任务操作记录表")]
	public class PTaskOperLog : BaseEntity
	{
		#region Member Variables
		
        protected long? _pTaskID;
        protected long _pTaskOperTypeID;
        protected long? _operaterID;
        protected string _operaterName;
        protected DateTime? _operateTime;
        protected string _operateMemo;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
		

		#endregion

		#region Constructors

		public PTaskOperLog() { }

		public PTaskOperLog( long labID, long pTaskID, long pTaskOperTypeID, long operaterID, string operaterName, DateTime operateTime, string operateMemo, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._pTaskID = pTaskID;
			this._pTaskOperTypeID = pTaskOperTypeID;
			this._operaterID = operaterID;
			this._operaterName = operaterName;
			this._operateTime = operateTime;
			this._operateMemo = operateMemo;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._zDY4 = zDY4;
			this._zDY5 = zDY5;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "任务ID", ShortCode = "PTaskID", Desc = "任务ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PTaskID
		{
			get { return _pTaskID; }
			set { _pTaskID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作类型ID", ShortCode = "PTaskOperTypeID", Desc = "操作类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long PTaskOperTypeID
		{
			get { return _pTaskOperTypeID; }
			set { _pTaskOperTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作人ID", ShortCode = "OperaterID", Desc = "操作人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperaterID
		{
			get { return _operaterID; }
			set { _operaterID = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作人", ShortCode = "OperaterName", Desc = "操作人", ContextType = SysDic.All, Length = 30)]
        public virtual string OperaterName
		{
			get { return _operaterName; }
			set
			{
				_operaterName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作时间", ShortCode = "OperateTime", Desc = "操作时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperateTime
		{
			get { return _operateTime; }
			set { _operateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作说明", ShortCode = "OperateMemo", Desc = "操作说明", ContextType = SysDic.All, Length = 16)]
        public virtual string OperateMemo
		{
			get { return _operateMemo; }
			set
			{
				_operateMemo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注1", ShortCode = "ZDY1", Desc = "备注1", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY1
		{
			get { return _zDY1; }
			set
			{
				_zDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注2", ShortCode = "ZDY2", Desc = "备注2", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY2
		{
			get { return _zDY2; }
			set
			{
				_zDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注3", ShortCode = "ZDY3", Desc = "备注3", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY3
		{
			get { return _zDY3; }
			set
			{
				_zDY3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注4", ShortCode = "ZDY4", Desc = "备注4", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY4
		{
			get { return _zDY4; }
			set
			{
				_zDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注5", ShortCode = "ZDY5", Desc = "备注5", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY5
		{
			get { return _zDY5; }
			set
			{
				_zDY5 = value;
			}
		}

		
		#endregion
	}
	#endregion
}