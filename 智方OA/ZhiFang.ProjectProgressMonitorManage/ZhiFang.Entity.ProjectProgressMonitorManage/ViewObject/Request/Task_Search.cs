using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request
{
    public class Task_Search
    {
        protected long _labID;        
        protected string _projectName;
        protected string _cName;
        protected string _typeName;

        protected long? _ApplyID;
        protected string _ApplyName;
        protected DateTime? _ApplyDataTime;

        protected long? _OneAuditID;
        protected string _OneAuditName;
        protected DateTime? _OneAuditDataTime;

        protected long? _TwoAuditID;
        protected string _TwoAuditName;
        protected DateTime? _TwoAuditDataTime;

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
        protected bool? _isUse;
        protected string _memo;
        protected string _contents;
        protected long? _type;
        protected long? _executType;
        protected string _status;
        protected string _nostatus;
        protected long? _pace;
        protected long? _teamworkEval;
        protected long? _paceEval;
        protected long? _efficiencyEval;
        protected long? _qualityEval;
        protected long? _totalityEval;
        protected long? _urgency;
        protected long? _pClient;
        protected long? _pProject;

        private DateTime? _EstiStartTimeB;
        private DateTime? _EstiStartTimeE;
        private DateTime? _EstiEndTimeB;
        private DateTime? _EstiEndTimeE;
        private DateTime? _StartTimeB;
        private DateTime? _StartTimeE;
        private DateTime? _EndTimeB;
        private DateTime? _EndTimeE;
        private DateTime? _DataAddTimeB;
        private DateTime? _DataAddTimeE;
        private string _sort;
        private bool _isPlanish;
        private string _Fields;
        private int _Page;
        private int _Limit;
        private int? _ExportType;

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "平台客户ID", ShortCode = "SYSID", Desc = "平台客户ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long LabID
        {
            get
            {
                return _labID;
            }
            set { _labID = value; }
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
        public virtual bool? IsUse
        {
            get
            {
                if (_isUse.HasValue)
                    return _isUse;
                else
                    return true;
            }
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
        public virtual long? Type
        {
            get { return _type; }
            set { _type = value; }
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
        public virtual string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "NoStatus", Desc = "字典表")]
        public virtual string NoStatus
        {
            get { return _nostatus; }
            set { _nostatus = value; }
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

        #region 查询计划开始时间范围
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询计划开始时间_开始", ShortCode = "EstiStartTimeB", Desc = "查询计划开始时间_开始", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiStartTimeB
        {
            get { return _EstiStartTimeB; }
            set { _EstiStartTimeB = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询计划开始时间_结束", ShortCode = "EstiStartTimeE", Desc = "查询计划开始时间_结束", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiStartTimeE
        {
            get { return _EstiStartTimeE; }
            set { _EstiStartTimeE = value; }
        }
        #endregion

        #region 查询计划结束时间范围
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询计划结束时间_开始", ShortCode = "EstiEndTimeB", Desc = "查询计划结束时间_开始", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiEndTimeB
        {
            get { return _EstiEndTimeB; }
            set { _EstiEndTimeB = value; }
        }
        


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询计划结束时间_结束", ShortCode = "EstiEndTimeE", Desc = "查询计划结束时间_结束", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiEndTimeE
        {
            get { return _EstiEndTimeE; }
            set { _EstiEndTimeE = value; }
        }
        #endregion

        #region 查询实际开始时间范围
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际开始时间_开始", ShortCode = "StartTimeB", Desc = "查询实际开始时间_开始", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StartTimeB
        {
            get { return _StartTimeB; }
            set { _StartTimeB = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际开始时间_结束", ShortCode = "StartTimeE", Desc = "查询实际开始时间_结束", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StartTimeE
        {
            get { return _StartTimeE; }
            set { _StartTimeE = value; }
        }
        #endregion

        #region 查询实际结束时间范围
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际结束时间_开始", ShortCode = "EndTimeB", Desc = "查询实际结束时间_开始", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTimeB
        {
            get { return _EndTimeB; }
            set { _EndTimeB = value; }
        }



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际结束时间_结束", ShortCode = "EndTimeE", Desc = "查询实际结束时间_结束", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTimeE
        {
            get { return _EndTimeE; }
            set { _EndTimeE = value; }
        }
        #endregion

        #region 查询新增开始时间范围
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际开始时间_开始", ShortCode = "StartTimeB", Desc = "查询实际开始时间_开始", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataAddTimeB
        {
            get { return _DataAddTimeB; }
            set { _DataAddTimeB = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际开始时间_结束", ShortCode = "StartTimeE", Desc = "查询实际开始时间_结束", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataAddTimeE
        {
            get { return _DataAddTimeE; }
            set { _DataAddTimeE = value; }
        }
        #endregion

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "排序字段", ShortCode = "Sort", Desc = "排序字段", ContextType = SysDic.All, Length = 8)]
        public virtual string Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "需要序列化字段", ShortCode = "Fields", Desc = "需要序列化字段", ContextType = SysDic.All, Length = 8)]
        public virtual string Fields
        {
            get { return _Fields; }
            set { _Fields = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "页数", ShortCode = "Page", Desc = "页数", ContextType = SysDic.All, Length = 8)]
        public virtual int Page
        {
            get { return _Page; }
            set { _Page = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "页行数", ShortCode = "Limit", Desc = "页行数", ContextType = SysDic.All, Length = 8)]
        public virtual int Limit
        {
            get { return _Limit; }
            set { _Limit = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "浏览查看类型。0：我申请、1：我一审、2：我二审、3：我分配、4：我执行、5：我检查、6：抄送给我", ShortCode = "Limit", Desc = "浏览查看类型。0：我申请、1：我一审、2：我二审、3：我分配、4：我执行、5：我检查、6：抄送给我", ContextType = SysDic.All, Length = 8)]
        public virtual int? ExportType
        {
            get { return _ExportType; }
            set { _ExportType = value; }
        }
    }
}
