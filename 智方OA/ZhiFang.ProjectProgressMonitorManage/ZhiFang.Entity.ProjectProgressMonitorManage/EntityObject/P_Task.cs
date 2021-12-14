using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PTask

    /// <summary>
    /// PTask object for NHibernate mapped table 'P_Task'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "任务表", ClassCName = "PTask", ShortCode = "PTask", Desc = "任务表")]
    public class PTask : BaseEntity
    {
        #region Member Variables

        protected string _PTaskCName;
        protected string _cName;
        protected string _typeName;
        protected string _ptypeName;
        protected string _mtypeName;
        protected long? _publisherID;
        protected long? _executorID;
        protected long? _checkerID;
        protected string _publisherName;
        protected string _executorName;
        protected string _checkerName;
        protected DateTime? _PublisherDataTime;
        protected DateTime? _CheckerDataTime;
        protected string _executTypeName;
        protected string _executAddr;
        protected string _statusName;
        protected string _paceName;
        protected DateTime? _reqEndTime;
        protected DateTime? _estiStartTime;
        protected DateTime? _estiEndTime;
        protected double _estiWorkload;
        protected DateTime? _startTime;
        protected DateTime? _endTime;
        protected double _workload;
        protected string _teamworkEvalName;
        protected string _paceEvalName;
        protected string _efficiencyEvalName;
        protected string _qualityEvalName;
        protected string _totalityEvalName;
        protected string _urgencyName;
        protected string _otherMsg;
        protected string _extraMsg;
        protected bool _isUse;
        protected string _memo;
        protected string _contents;
        protected long? _type;
        protected long? _ptype;
        protected long? _mtype;
        protected PDict _executType;
        protected PDict _status;
        protected PDict _pace;
        protected PDict _teamworkEval;
        protected PDict _paceEval;
        protected PDict _efficiencyEval;
        protected PDict _qualityEval;
        protected PDict _totalityEval;
        protected PDict _urgency;
        protected PClient _pClient;
        protected long? _pPTaskId;
        protected IList<PInteraction> _pInteractionList;
        protected IList<PProjectAttachment> _pProjectAttachmentList;
        protected IList<PTaskCopyFor> _pTaskCopyForList;
        protected IList<PWorkDayLog> _pWorkDayLogList;
        protected long _InteractionCount;
        protected long _OperLogCount;
        protected long _WorkLogCount;

        protected int _subCount;

        protected long? _ApplyID;
        protected string _ApplyName;
        protected DateTime? _ApplyDataTime;

        protected long? _OneAuditID;
        protected string _OneAuditName;
        protected DateTime? _OneAuditDataTime;

        protected long? _TwoAuditID;
        protected string _TwoAuditName;
        protected DateTime? _TwoAuditDataTime;

        protected long? _pClassID;
        protected string _pClassName;
        #endregion

        #region Constructors

        public PTask() { }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "父任务名称", ShortCode = "PTaskName", Desc = "父任务名称", ContextType = SysDic.All, Length = 100)]
        public virtual string PTaskCName
        {
            get { return _PTaskCName; }
            set
            {
                _PTaskCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "任务名称", ShortCode = "CName", Desc = "任务名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "任务类别名称", ShortCode = "TypeName", Desc = "任务类别名称", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeName
        {
            get { return _typeName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
                _typeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "任务父类别名称", ShortCode = "PTypeName", Desc = "任务父类别名称", ContextType = SysDic.All, Length = 50)]
        public virtual string PTypeName
        {
            get { return _ptypeName; }
            set
            {
                _ptypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "任务主类别名称", ShortCode = "MTypeName", Desc = "任务主类别名称", ContextType = SysDic.All, Length = 50)]
        public virtual string MTypeName
        {
            get { return _mtypeName; }
            set
            {
                _mtypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "分配人", ShortCode = "PublisherID", Desc = "分配人", ContextType = SysDic.All, Length = 8)]
        public virtual long? PublisherID
        {
            get { return _publisherID; }
            set { _publisherID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "执行人", ShortCode = "ExecutorID", Desc = "执行人", ContextType = SysDic.All, Length = 8)]
        public virtual long? ExecutorID
        {
            get { return _executorID; }
            set { _executorID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检查人", ShortCode = "CheckerID", Desc = "检查人", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckerID
        {
            get { return _checkerID; }
            set { _checkerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "分配人姓名", ShortCode = "PublisherName", Desc = "分配人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string PublisherName
        {
            get { return _publisherName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PublisherName", value, value.ToString());
                _publisherName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "执行人姓名", ShortCode = "ExecutorName", Desc = "执行人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ExecutorName
        {
            get { return _executorName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ExecutorName", value, value.ToString());
                _executorName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检查人姓名", ShortCode = "CheckerName", Desc = "检查人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckerName
        {
            get { return _checkerName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckerName", value, value.ToString());
                _checkerName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "分配时间", ShortCode = "PublisherDataTime", Desc = "分配时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PublisherDataTime
        {
            get { return _PublisherDataTime; }
            set { _PublisherDataTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检查时间", ShortCode = "CheckerDataTime", Desc = "检查时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckerDataTime
        {
            get { return _CheckerDataTime; }
            set { _CheckerDataTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ExecutTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ExecutTypeName
        {
            get { return _executTypeName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ExecutTypeName", value, value.ToString());
                _executTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "执行地点", ShortCode = "ExecutAddr", Desc = "执行地点", ContextType = SysDic.All, Length = 100)]
        public virtual string ExecutAddr
        {
            get { return _executAddr; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ExecutAddr", value, value.ToString());
                _executAddr = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StatusName
        {
            get { return _statusName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StatusName", value, value.ToString());
                _statusName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PaceName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PaceName
        {
            get { return _paceName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PaceName", value, value.ToString());
                _paceName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "要求完成时间", ShortCode = "ReqEndTime", Desc = "要求完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReqEndTime
        {
            get { return _reqEndTime; }
            set { _reqEndTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计划开始时间", ShortCode = "EstiStartTime", Desc = "计划开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiStartTime
        {
            get { return _estiStartTime; }
            set { _estiStartTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计划结束时间", ShortCode = "EstiEndTime", Desc = "计划结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiEndTime
        {
            get { return _estiEndTime; }
            set { _estiEndTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预计工作量", ShortCode = "EstiWorkload", Desc = "预计工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double EstiWorkload
        {
            get { return _estiWorkload; }
            set { _estiWorkload = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际开始时间", ShortCode = "StartTime", Desc = "实际开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际结束时间", ShortCode = "EndTime", Desc = "实际结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际工作量", ShortCode = "Workload", Desc = "实际工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double Workload
        {
            get { return _workload; }
            set { _workload = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TeamworkEvalName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TeamworkEvalName
        {
            get { return _teamworkEvalName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TeamworkEvalName", value, value.ToString());
                _teamworkEvalName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PaceEvalName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PaceEvalName
        {
            get { return _paceEvalName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PaceEvalName", value, value.ToString());
                _paceEvalName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EfficiencyEvalName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EfficiencyEvalName
        {
            get { return _efficiencyEvalName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EfficiencyEvalName", value, value.ToString());
                _efficiencyEvalName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QualityEvalName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string QualityEvalName
        {
            get { return _qualityEvalName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for QualityEvalName", value, value.ToString());
                _qualityEvalName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TotalityEvalName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TotalityEvalName
        {
            get { return _totalityEvalName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TotalityEvalName", value, value.ToString());
                _totalityEvalName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UrgencyName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UrgencyName
        {
            get { return _urgencyName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UrgencyName", value, value.ToString());
                _urgencyName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "其他信息", ShortCode = "OtherMsg", Desc = "其他信息", ContextType = SysDic.All, Length = -1)]
        public virtual string OtherMsg
        {
            get { return _otherMsg; }
            set
            {
                _otherMsg = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "附加信息", ShortCode = "ExtraMsg", Desc = "附加信息", ContextType = SysDic.All, Length = -1)]
        public virtual string ExtraMsg
        {
            get { return _extraMsg; }
            set
            {
                _extraMsg = value;
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "内容", ShortCode = "Contents", Desc = "内容", ContextType = SysDic.All, Length = -1)]
        public virtual string Contents
        {
            get { return _contents; }
            set
            {
                _contents = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "任务类别", ShortCode = "TypeID", Desc = "任务类别")]
        public virtual long? TypeID
        {
            get { return _type; }
            set { _type = value; }
        }

        [DataMember]
        [DataDesc(CName = "父类别", ShortCode = "PTypeID", Desc = "父类别")]
        public virtual long? PTypeID
        {
            get { return _ptype; }
            set { _ptype = value; }
        }

        [DataMember]
        [DataDesc(CName = "主类别", ShortCode = "MTypeID", Desc = "主类别")]
        public virtual long? MTypeID
        {
            get { return _mtype; }
            set { _mtype = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "ExecutType", Desc = "字典表")]
        public virtual PDict ExecutType
        {
            get { return _executType; }
            set { _executType = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "Status", Desc = "字典表")]
        public virtual PDict Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "Pace", Desc = "字典表")]
        public virtual PDict Pace
        {
            get { return _pace; }
            set { _pace = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "TeamworkEval", Desc = "字典表")]
        public virtual PDict TeamworkEval
        {
            get { return _teamworkEval; }
            set { _teamworkEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "PaceEval", Desc = "字典表")]
        public virtual PDict PaceEval
        {
            get { return _paceEval; }
            set { _paceEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "EfficiencyEval", Desc = "字典表")]
        public virtual PDict EfficiencyEval
        {
            get { return _efficiencyEval; }
            set { _efficiencyEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "QualityEval", Desc = "字典表")]
        public virtual PDict QualityEval
        {
            get { return _qualityEval; }
            set { _qualityEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "TotalityEval", Desc = "字典表")]
        public virtual PDict TotalityEval
        {
            get { return _totalityEval; }
            set { _totalityEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "Urgency", Desc = "字典表")]
        public virtual PDict Urgency
        {
            get { return _urgency; }
            set { _urgency = value; }
        }

        [DataMember]
        [DataDesc(CName = "客户", ShortCode = "PClient", Desc = "客户")]
        public virtual PClient PClient
        {
            get { return _pClient; }
            set { _pClient = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目表", ShortCode = "PProject", Desc = "项目表")]
        public virtual long? PTaskID
        {
            get { return _pPTaskId; }
            set { _pPTaskId = value; }
        }

        [DataMember]
        [DataDesc(CName = "子任务数量", ShortCode = "SubCount", Desc = "子任务数量")]
        public virtual int SubCount
        {
            get { return _subCount; }
            set { _subCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "互动记录表", ShortCode = "PInteractionList", Desc = "互动记录表")]
        public virtual IList<PInteraction> PInteractionList
        {
            get
            {
                if (_pInteractionList == null)
                {
                    _pInteractionList = new List<PInteraction>();
                }
                return _pInteractionList;
            }
            set { _pInteractionList = value; }
        }

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
        [DataDesc(CName = "抄送关系表", ShortCode = "PTaskCopyForList", Desc = "抄送关系表")]
        public virtual IList<PTaskCopyFor> PTaskCopyForList
        {
            get
            {
                if (_pTaskCopyForList == null)
                {
                    _pTaskCopyForList = new List<PTaskCopyFor>();
                }
                return _pTaskCopyForList;
            }
            set { _pTaskCopyForList = value; }
        }

        [DataMember]
        [DataDesc(CName = "工作日志表", ShortCode = "PWorkDayLogList", Desc = "工作日志表")]
        public virtual IList<PWorkDayLog> PWorkDayLogList
        {
            get
            {
                if (_pWorkDayLogList == null)
                {
                    _pWorkDayLogList = new List<PWorkDayLog>();
                }
                return _pWorkDayLogList;
            }
            set { _pWorkDayLogList = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请人ID", ShortCode = "ApplyID", Desc = "申请人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApplyID
        {
            get { return _ApplyID; }
            set { _ApplyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "申请人姓名", ShortCode = "ApplyName", Desc = "申请人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ApplyName
        {
            get { return _ApplyName; }
            set { _ApplyName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请时间", ShortCode = "ApplyDataTime", Desc = "申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApplyDataTime
        {
            get { return _ApplyDataTime; }
            set { _ApplyDataTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "一审人ID", ShortCode = "OneAuditID", Desc = "一审人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OneAuditID
        {
            get { return _OneAuditID; }
            set { _OneAuditID = value; }
        }

        [DataMember]
        [DataDesc(CName = "一审人姓名", ShortCode = "OneAuditName", Desc = "一审人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string OneAuditName
        {
            get { return _OneAuditName; }
            set { _OneAuditName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "一审时间", ShortCode = "OneAuditDataTime", Desc = "一审时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OneAuditDataTime
        {
            get { return _OneAuditDataTime; }
            set { _OneAuditDataTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "二审人ID", ShortCode = "TwoAuditID", Desc = "二审人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TwoAuditID
        {
            get { return _TwoAuditID; }
            set { _TwoAuditID = value; }
        }

        [DataMember]
        [DataDesc(CName = "二审人姓名", ShortCode = "TwoAuditName", Desc = "二审人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string TwoAuditName
        {
            get { return _TwoAuditName; }
            set { _TwoAuditName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "二审时间", ShortCode = "TwoAuditDataTime", Desc = "二审时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TwoAuditDataTime
        {
            get { return _TwoAuditDataTime; }
            set { _TwoAuditDataTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "交流计数", ShortCode = "InteractionCount", Desc = "交流计数", ContextType = SysDic.All, Length = 8)]
        public virtual long InteractionCount
        {
            get { return _InteractionCount; }
            set { _InteractionCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作日志计数", ShortCode = "OperLogCount", Desc = "操作日志计数", ContextType = SysDic.All, Length = 8)]
        public virtual long OperLogCount
        {
            get { return _OperLogCount; }
            set { _OperLogCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "工作日志计数", ShortCode = "WorkLogCount", Desc = "工作日志计数", ContextType = SysDic.All, Length = 8)]
        public virtual long WorkLogCount
        {
            get { return _WorkLogCount; }
            set { _WorkLogCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "任务分类ID", ShortCode = "PClassID", Desc = "任务分类ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PClassID
        {
            get { return _pClassID; }
            set { _pClassID = value; }
        }
        [DataMember]
        [DataDesc(CName = "任务分类名称", ShortCode = "PClassName", Desc = "任务分类名称", ContextType = SysDic.All, Length = 50)]
        public virtual string PClassName
        {
            get { return _pClassName; }
            set { _pClassName = value; }
        }

        #endregion
    }
    #endregion
}