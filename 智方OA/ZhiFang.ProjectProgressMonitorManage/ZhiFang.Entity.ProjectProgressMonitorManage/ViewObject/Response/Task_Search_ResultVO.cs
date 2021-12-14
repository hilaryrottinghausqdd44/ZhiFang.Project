using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Response
{
    public class Task_Search_ResultVO : Base.BaseEntity
    {
        #region Member Variables

        protected string _projectName;
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
        protected string _executTypeName;
        protected string _executAddr;
        protected string _statusName;
        protected string _paceName;
        protected DateTime? _ReqEndTime;
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
        protected long? _executType;
        protected long? _status;
        protected long? _pace;
        protected long? _teamworkEval;
        protected long? _paceEval;
        protected long? _efficiencyEval;
        protected long? _qualityEval;
        protected long? _totalityEval;
        protected long? _urgency;
        protected long? _pClient;
        protected long? _pProject;
        protected IList<PProjectAttachmentVO> _pProjectAttachmentList;
        protected IList<PTaskCopyForVO> _pTaskCopyForList;
        protected string _IdString;

        protected long? _ApplyID;
        protected string _ApplyName;
        protected DateTime? _ApplyDataTime;

        protected long? _OneAuditID;
        protected string _OneAuditName;
        protected DateTime? _OneAuditDataTime;

        protected long? _TwoAuditID;
        protected string _TwoAuditName;
        protected DateTime? _TwoAuditDataTime;

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "IdString", ShortCode = "IdString", Desc = "IdString", ContextType = SysDic.All, Length = 100)]
        public virtual string IdString
        {
            get { return _IdString; }
            set
            {
                _IdString = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "项目名称", ShortCode = "ProjectName", Desc = "项目名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ProjectName", value, value.ToString());
                _projectName = value;
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
            get { return _ReqEndTime; }
            set { _ReqEndTime = value; }
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
        [DataDesc(CName = "字典表", ShortCode = "Type", Desc = "字典表")]
        public virtual long? TypeID
        {
            get { return _type; }
            set { _type = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "Type", Desc = "字典表")]
        public virtual long? PTypeID
        {
            get { return _ptype; }
            set { _ptype = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "Type", Desc = "字典表")]
        public virtual long? MTypeID
        {
            get { return _mtype; }
            set { _mtype = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "ExecutType", Desc = "字典表")]
        public virtual long? ExecutType
        {
            get { return _executType; }
            set { _executType = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "Status", Desc = "字典表")]
        public virtual long? Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "Pace", Desc = "字典表")]
        public virtual long? Pace
        {
            get { return _pace; }
            set { _pace = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "TeamworkEval", Desc = "字典表")]
        public virtual long? TeamworkEval
        {
            get { return _teamworkEval; }
            set { _teamworkEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "PaceEval", Desc = "字典表")]
        public virtual long? PaceEval
        {
            get { return _paceEval; }
            set { _paceEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "EfficiencyEval", Desc = "字典表")]
        public virtual long? EfficiencyEval
        {
            get { return _efficiencyEval; }
            set { _efficiencyEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "QualityEval", Desc = "字典表")]
        public virtual long? QualityEval
        {
            get { return _qualityEval; }
            set { _qualityEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "TotalityEval", Desc = "字典表")]
        public virtual long? TotalityEval
        {
            get { return _totalityEval; }
            set { _totalityEval = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "Urgency", Desc = "字典表")]
        public virtual long? Urgency
        {
            get { return _urgency; }
            set { _urgency = value; }
        }

        [DataMember]
        [DataDesc(CName = "客户", ShortCode = "PClient", Desc = "客户")]
        public virtual long? PClient
        {
            get { return _pClient; }
            set { _pClient = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目表", ShortCode = "PProject", Desc = "项目表")]
        public virtual long? PProject
        {
            get { return _pProject; }
            set { _pProject = value; }
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
        [DataDesc(CName = "项目附件表", ShortCode = "PProjectAttachmentList", Desc = "项目附件表")]
        public virtual IList<PProjectAttachmentVO> PProjectAttachmentList
        {
            get
            {
                if (_pProjectAttachmentList == null)
                {
                    _pProjectAttachmentList = new List<PProjectAttachmentVO>();
                }
                return _pProjectAttachmentList;
            }
            set { _pProjectAttachmentList = value; }
        }

        [DataMember]
        [DataDesc(CName = "抄送关系表", ShortCode = "PTaskCopyForList", Desc = "抄送关系表")]
        public virtual IList<PTaskCopyForVO> PTaskCopyForList
        {
            get
            {
                if (_pTaskCopyForList == null)
                {
                    _pTaskCopyForList = new List<PTaskCopyForVO>();
                }
                return _pTaskCopyForList;
            }
            set { _pTaskCopyForList = value; }
        }

        #endregion
    }

    public class PProjectAttachmentVO : Base.BaseEntity
    {
        #region Member Variables

        protected string _fileName;
        protected string _fileExt;
        protected long? _fileSize;
        protected string _filePath;
        protected long? _creatorID;
        protected string _creatorName;
        protected bool _isUse;
        protected string _memo;
        protected string _extraMsg;
        protected string _taskName;
        protected string _projectName;
        protected string _workTaskLogName;
        protected PInteraction _pInteraction;
        protected PTask _pTask;
        protected PWorkDayLog _pWorkDayLog;
        protected IList<PWorkMonthLog> _pWorkMonthLogList;
        protected IList<PWorkWeekLog> _pWorkWeekLogList;

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "文件名", ShortCode = "FileName", Desc = "文件名", ContextType = SysDic.All, Length = 100)]
        public virtual string FileName
        {
            get { return _fileName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for FileName", value, value.ToString());
                _fileName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "文件扩展名", ShortCode = "FileExt", Desc = "文件扩展名", ContextType = SysDic.All, Length = 10)]
        public virtual string FileExt
        {
            get { return _fileExt; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for FileExt", value, value.ToString());
                _fileExt = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "文件大小", ShortCode = "FileSize", Desc = "文件大小", ContextType = SysDic.All, Length = 8)]
        public virtual long? FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }

        [DataMember]
        [DataDesc(CName = "文件路径", ShortCode = "FilePath", Desc = "文件路径", ContextType = SysDic.All, Length = 100)]
        public virtual string FilePath
        {
            get { return _filePath; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for FilePath", value, value.ToString());
                _filePath = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者", ShortCode = "CreatorID", Desc = "创建者", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
        {
            get { return _creatorID; }
            set { _creatorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CreatorName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CreatorName
        {
            get { return _creatorName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
                _creatorName = value;
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
        [DataDesc(CName = "", ShortCode = "TaskName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string TaskName
        {
            get { return _taskName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TaskName", value, value.ToString());
                _taskName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProjectName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ProjectName", value, value.ToString());
                _projectName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WorkTaskLogName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string WorkTaskLogName
        {
            get { return _workTaskLogName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for WorkTaskLogName", value, value.ToString());
                _workTaskLogName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "互动记录表", ShortCode = "PInteraction", Desc = "互动记录表")]
        public virtual PInteraction PInteraction
        {
            get { return _pInteraction; }
            set { _pInteraction = value; }
        }

        [DataMember]
        [DataDesc(CName = "任务表", ShortCode = "PTask", Desc = "任务表")]
        public virtual PTask PTask
        {
            get { return _pTask; }
            set { _pTask = value; }
        }

        [DataMember]
        [DataDesc(CName = "工作任务日志表", ShortCode = "PWorkTaskLog", Desc = "工作任务日志表")]
        public virtual PWorkDayLog PWorkTaskLog
        {
            get { return _pWorkDayLog; }
            set { _pWorkDayLog = value; }
        }

        [DataMember]
        [DataDesc(CName = "工作月总结表", ShortCode = "PWorkMonthLogList", Desc = "工作月总结表")]
        public virtual IList<PWorkMonthLog> PWorkMonthLogList
        {
            get
            {
                if (_pWorkMonthLogList == null)
                {
                    _pWorkMonthLogList = new List<PWorkMonthLog>();
                }
                return _pWorkMonthLogList;
            }
            set { _pWorkMonthLogList = value; }
        }

        [DataMember]
        [DataDesc(CName = "工作周计划表", ShortCode = "PWorkWeekLogList", Desc = "工作周计划表")]
        public virtual IList<PWorkWeekLog> PWorkWeekLogList
        {
            get
            {
                if (_pWorkWeekLogList == null)
                {
                    _pWorkWeekLogList = new List<PWorkWeekLog>();
                }
                return _pWorkWeekLogList;
            }
            set { _pWorkWeekLogList = value; }
        }


        #endregion
    }

    public class PTaskCopyForVO : Base.BaseEntity
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

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "TaskName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string TaskName
        {
            get { return _taskName; }
            set
            {
                if (value != null && value.Length > 100)
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
                if (value != null && value.Length > 50)
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
                if (value != null && value.Length > 50)
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
}