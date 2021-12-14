using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PWorkDayLog

	/// <summary>
	/// PWorkDayLog object for NHibernate mapped table 'P_WorkDayLog'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "工作日志表", ClassCName = "PWorkDayLog", ShortCode = "PWorkDayLog", Desc = "工作日志表")]
	public class PWorkDayLog : PWorkLogBase
    {
        #region Member Variables

        protected bool _hasRisk;
        protected double _workload;
        protected bool _isFinish;
        protected bool _isOver;
        protected bool _IsUse;

        #endregion

        #region Constructors

        public PWorkDayLog() { }


        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "是否存在风险", ShortCode = "HasRisk", Desc = "是否存在风险", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasRisk
        {
            get { return _hasRisk; }
            set { _hasRisk = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "已完成工作量", ShortCode = "Workload", Desc = "已完成工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double Workload
        {
            get { return _workload; }
            set { _workload = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已完成", ShortCode = "IsFinish", Desc = "是否已完成", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsFinish
        {
            get { return _isFinish; }
            set { _isFinish = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否结束", ShortCode = "IsOver", Desc = "是否结束", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsOver
        {
            get { return _isOver; }
            set { _isOver = value; }
        }        

        #endregion
    }
    #endregion

    #region PWorkLogBase

    /// <summary>
    /// PWorkDayLog object for NHibernate mapped table 'P_WorkDayLog'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "工作日志表", ClassCName = "PWorkDayLog", ShortCode = "PWorkDayLog", Desc = "工作日志表")]
    public class PWorkLogBase : BaseEntity
    {
        #region Member Variables

        protected long? _empID;
        protected string _empName;
        protected string _toDayContent;
        protected string _nextDayContent;
        protected DateTime? _dataUpdateTime;
        protected string _dateCode;
        protected WorkLogExportLevel _WorkLogExportLevel;
        protected PTask _pTask;
        //protected IList<PWorkTaskLog> _pWorkTaskLogList;
        protected IList<PProjectAttachment> _pProjectAttachmentList;
        protected IList<long> _CopyForEmpIdList;
        protected IList<string> _CopyForEmpNameList;
        protected long? _LikeCount;
        protected string _Image1;
        protected string _Image2;
        protected string _Image3;
        protected string _Image4;
        protected string _Image5;
        protected bool _IsUse;
        protected bool _Status;

        #endregion

        #region Constructors

        public PWorkLogBase() { }

        public PWorkLogBase(long labID, long empID, string empName, string toDayContent, string nextDayContent, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string dateCode, PTask pTask)
        {
            this._labID = labID;
            this._empID = empID;
            this._empName = empName;
            this._toDayContent = toDayContent;
            this._nextDayContent = nextDayContent;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._dateCode = dateCode;
            this._pTask = pTask;
        }

        #endregion

        #region Public Properties


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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
                _empName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "今日工作内容", ShortCode = "ToDayContent", Desc = "今日工作内容", ContextType = SysDic.All, Length = -1)]
        public virtual string ToDayContent
        {
            get { return _toDayContent; }
            set
            {
                _toDayContent = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "明日工作计划", ShortCode = "NextDayContent", Desc = "明日工作计划", ContextType = SysDic.All, Length = -1)]
        public virtual string NextDayContent
        {
            get { return _nextDayContent; }
            set
            {
                _nextDayContent = value;
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
        [DataDesc(CName = "日期标识", ShortCode = "DateCode", Desc = "日期标识", ContextType = SysDic.All, Length = 100)]
        public virtual string DateCode
        {
            get { return _dateCode; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for DateCode", value, value.ToString());
                _dateCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Image1", ShortCode = "Image1", Desc = "Image1", ContextType = SysDic.All, Length = 100)]
        public virtual string Image1
        {
            get { return _Image1; }
            set
            {
                _Image1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Image2", ShortCode = "Image2", Desc = "Image2", ContextType = SysDic.All, Length = 100)]
        public virtual string Image2
        {
            get { return _Image2; }
            set
            {
                _Image2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Image3", ShortCode = "Image3", Desc = "Image3", ContextType = SysDic.All, Length = 100)]
        public virtual string Image3
        {
            get { return _Image3; }
            set
            {
                _Image3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Image4", ShortCode = "Image4", Desc = "Image4", ContextType = SysDic.All, Length = 100)]
        public virtual string Image4
        {
            get { return _Image4; }
            set
            {
                _Image4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Image5", ShortCode = "Image5", Desc = "Image5", ContextType = SysDic.All, Length = 100)]
        public virtual string Image5
        {
            get { return _Image5; }
            set
            {
                _Image5 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否提交", ShortCode = "IsUse", Desc = "是否提交", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _IsUse; }
            set { _IsUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Status", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        [DataMember]
        [DataDesc(CName = "可见级别", ShortCode = "WorkLogExportLevel", Desc = "可见级别", ContextType = SysDic.All, Length = 100)]
        public virtual WorkLogExportLevel WorkLogExportLevel
        {
            get { return _WorkLogExportLevel; }
            set
            {
                _WorkLogExportLevel = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "点赞计数", ShortCode = "LikeCount", Desc = "点赞计数", ContextType = SysDic.All, Length = 100)]
        public virtual long? LikeCount
        {
            get { return _LikeCount; }
            set
            {
                _LikeCount = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "任务表", ShortCode = "PTask", Desc = "任务表")]
        public virtual PTask PTask
        {
            get { return _pTask; }
            set { _pTask = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "工作任务日志表", ShortCode = "PWorkTaskLogList", Desc = "工作任务日志表")]
        //public virtual IList<PWorkTaskLog> PWorkTaskLogList
        //{
        //    get
        //    {
        //        if (_pWorkTaskLogList == null)
        //        {
        //            _pWorkTaskLogList = new List<PWorkTaskLog>();
        //        }
        //        return _pWorkTaskLogList;
        //    }
        //    set { _pWorkTaskLogList = value; }
        //}


        [DataMember]
        [DataDesc(CName = "项目附件表", ShortCode = "PProjectAttachmentList", Desc = "项目附件表")]
        public virtual IList<PProjectAttachment> PProjectAttachmentList
        {
            get
            {
                if (_pProjectAttachmentList == null)
                {
                    _pProjectAttachmentList = new List<PProjectAttachment>();
                }
                return _pProjectAttachmentList;
            }
            set { _pProjectAttachmentList = value; }
        }

        [DataMember]
        [DataDesc(CName = "抄送员工IdList", ShortCode = "CopyForEmpIdList", Desc = "抄送员工IdList")]
        public virtual IList<long> CopyForEmpIdList
        {
            get
            {
                return _CopyForEmpIdList;
            }
            set { _CopyForEmpIdList = value; }
        }

        [DataMember]
        [DataDesc(CName = "抄送员工NameList", ShortCode = "CopyForEmpNameList", Desc = "抄送员工NameList")]
        public virtual IList<string> CopyForEmpNameList
        {
            get
            {
                return _CopyForEmpNameList;
            }
            set { _CopyForEmpNameList = value; }
        }
        #endregion
    }
    #endregion
}