using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PProject

    /// <summary>
    /// PProject object for NHibernate mapped table 'P_Project'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "项目表", ClassCName = "PProject", ShortCode = "PProject", Desc = "项目表")]
    public class PProject : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected long? _pContractID;
        protected string _pContractName;
        protected long? _pClientID;
        protected string _pClientName;
        protected long? _phaseID;
        protected string _phaseName;
        protected long? _riskLevelID;
        protected string _riskLevelName;
        protected long? _paceID;
        protected string _paceName;
        protected long? _dynamicRiskLevelID;
        protected string _dynamicRiskLevelName;
        protected long? _delayLevelID;
        protected string _delayLevelName;
        protected DateTime? _entryTime;
        protected DateTime? _signDate;
        protected long? _principalID;
        protected string _principal;
        protected long? _projectLeaderID;
        protected string _projectLeader;
        protected long? _projectExecID;
        protected string _projectExec;
        protected long? _phaseManagerID;
        protected string _phaseManager;
        protected DateTime? _estiStartTime;
        protected DateTime? _estiEndTime;
        protected DateTime? _startTime;
        protected DateTime? _endTime;
        protected DateTime? _acceptTime;
        protected double? _estiAllDays;
        protected double? _dynamicRemainingWorkDays;
        protected double? _allDays;
        protected double? _scheduleDelayPercent;
        protected double? _scheduleDelayDays;
        protected double? _moreWorkPercent;
        protected double? _moreWorkDays;
        protected long? _creatEmpIdID;
        protected string _otherMsg;
        protected string _extraMsg;
        protected bool _isUse;
        protected bool _isStandard;
        protected string _memo;
        protected string _contents;
        protected DateTime? _reqEndTime;
        protected double? _estiWorkload;
        protected double? _workload;
        protected long? _teamworkEvalID;
        protected string _teamworkEvalName;
        protected long? _paceEvalID;
        protected string _paceEvalName;
        protected long? _efficiencyEvalID;
        protected string _efficiencyEvalName;
        protected long? _qualityEvalID;
        protected string _qualityEvalName;
        protected long? _totalityEvalID;
        protected string _totalityEvalName;
        protected long? _urgencyID;
        protected string _urgencyName;
        protected long? _typeID;
        protected int _subCount;
        protected long? _interactionCount;
        protected long? _operLogCount;
        protected long? _workLogCount;
        protected long? _projectManagerID;
        protected string _projectManager;
        protected long? _contentID;
        protected string _content;
        protected long? _provinceID;
        protected string _provinceName;
        protected int _dispOrder;
        #endregion

        #region Constructors

        public PProject() { }

        public PProject(long labID, string cName, long? pContractID, string pContractName, long? pClientID, string pClientName, long? phaseID, string phaseName, long? riskLevelID, string riskLevelName, long? paceID, string paceName, long? dynamicRiskLevelID, string dynamicRiskLevelName, long? delayLevelID, string delayLevelName, DateTime entryTime, DateTime signDate, long? principalID, string principal, long? projectLeaderID, string projectLeader, long? projectExecID, string projectExec, long? phaseManagerID, string phaseManager, DateTime estiStartTime, DateTime estiEndTime, DateTime startTime, DateTime endTime, DateTime acceptTime, double? estiAllDays, double? dynamicRemainingWorkDays, double? allDays, double? scheduleDelayPercent, double? scheduleDelayDays, double? moreWorkPercent, double? moreWorkDays, long? creatEmpIdID, DateTime dataAddTime, string otherMsg, string extraMsg, bool isUse, string memo, string contents, byte[] dataTimeStamp, DateTime reqEndTime, double? estiWorkload, double? workload, long? teamworkEvalID, string teamworkEvalName, long? paceEvalID, string paceEvalName, long? efficiencyEvalID, string efficiencyEvalName, long? qualityEvalID, string qualityEvalName, long? totalityEvalID, string totalityEvalName, long? urgencyID, string urgencyName, long? typeID, int subCount, long? interactionCount, long? operLogCount, long? workLogCount, long? projectManagerID, string projectManager, long? contentID, string content, long? provinceID, string provinceName)
        {
            this._labID = labID;
            this._cName = cName;
            this._pContractID = pContractID;
            this._pContractName = pContractName;
            this._pClientID = pClientID;
            this._pClientName = pClientName;
            this._phaseID = phaseID;
            this._phaseName = phaseName;
            this._riskLevelID = riskLevelID;
            this._riskLevelName = riskLevelName;
            this._paceID = paceID;
            this._paceName = paceName;
            this._dynamicRiskLevelID = dynamicRiskLevelID;
            this._dynamicRiskLevelName = dynamicRiskLevelName;
            this._delayLevelID = delayLevelID;
            this._delayLevelName = delayLevelName;
            this._entryTime = entryTime;
            this._signDate = signDate;
            this._principalID = principalID;
            this._principal = principal;
            this._projectLeaderID = projectLeaderID;
            this._projectLeader = projectLeader;
            this._projectExecID = projectExecID;
            this._projectExec = projectExec;
            this._phaseManagerID = phaseManagerID;
            this._phaseManager = phaseManager;
            this._estiStartTime = estiStartTime;
            this._estiEndTime = estiEndTime;
            this._startTime = startTime;
            this._endTime = endTime;
            this._acceptTime = acceptTime;
            this._estiAllDays = estiAllDays;
            this._dynamicRemainingWorkDays = dynamicRemainingWorkDays;
            this._allDays = allDays;
            this._scheduleDelayPercent = scheduleDelayPercent;
            this._scheduleDelayDays = scheduleDelayDays;
            this._moreWorkPercent = moreWorkPercent;
            this._moreWorkDays = moreWorkDays;
            this._creatEmpIdID = creatEmpIdID;
            this._dataAddTime = dataAddTime;
            this._otherMsg = otherMsg;
            this._extraMsg = extraMsg;
            this._isUse = isUse;
            this._memo = memo;
            this._contents = contents;
            this._dataTimeStamp = dataTimeStamp;
            this._reqEndTime = reqEndTime;
            this._estiWorkload = estiWorkload;
            this._workload = workload;
            this._teamworkEvalID = teamworkEvalID;
            this._teamworkEvalName = teamworkEvalName;
            this._paceEvalID = paceEvalID;
            this._paceEvalName = paceEvalName;
            this._efficiencyEvalID = efficiencyEvalID;
            this._efficiencyEvalName = efficiencyEvalName;
            this._qualityEvalID = qualityEvalID;
            this._qualityEvalName = qualityEvalName;
            this._totalityEvalID = totalityEvalID;
            this._totalityEvalName = totalityEvalName;
            this._urgencyID = urgencyID;
            this._urgencyName = urgencyName;
            this._typeID = typeID;
            this._subCount = subCount;
            this._interactionCount = interactionCount;
            this._operLogCount = operLogCount;
            this._workLogCount = workLogCount;
            this._projectManagerID = projectManagerID;
            this._projectManager = projectManager;
            this._contentID = contentID;
            this._content = content;
            this._provinceID = provinceID;
            this._provinceName = provinceName;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "项目名称", ShortCode = "CName", Desc = "项目名称", ContextType = SysDic.All, Length = 100)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同ID", ShortCode = "PContractID", Desc = "合同ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PContractID
        {
            get { return _pContractID; }
            set { _pContractID = value; }
        }

        [DataMember]
        [DataDesc(CName = "合同名称", ShortCode = "PContractName", Desc = "合同名称", ContextType = SysDic.All, Length = 100)]
        public virtual string PContractName
        {
            get { return _pContractName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for PContractName", value, value.ToString());
                _pContractName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "PClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PClientID
        {
            get { return _pClientID; }
            set { _pClientID = value; }
        }

        [DataMember]
        [DataDesc(CName = "客户名称", ShortCode = "PClientName", Desc = "客户名称", ContextType = SysDic.All, Length = 100)]
        public virtual string PClientName
        {
            get { return _pClientName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for PClientName", value, value.ToString());
                _pClientName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目阶段ID", ShortCode = "PhaseID", Desc = "项目阶段ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PhaseID
        {
            get { return _phaseID; }
            set { _phaseID = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目阶段名称", ShortCode = "PhaseName", Desc = "项目阶段名称", ContextType = SysDic.All, Length = 50)]
        public virtual string PhaseName
        {
            get { return _phaseName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PhaseName", value, value.ToString());
                _phaseName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目风险等级ID", ShortCode = "RiskLevelID", Desc = "项目风险等级ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RiskLevelID
        {
            get { return _riskLevelID; }
            set { _riskLevelID = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目风险等级名称", ShortCode = "RiskLevelName", Desc = "项目风险等级名称", ContextType = SysDic.All, Length = 50)]
        public virtual string RiskLevelName
        {
            get { return _riskLevelName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RiskLevelName", value, value.ToString());
                _riskLevelName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目进度ID", ShortCode = "PaceID", Desc = "项目进度ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PaceID
        {
            get { return _paceID; }
            set { _paceID = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目进度名称", ShortCode = "PaceName", Desc = "项目进度名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "动态评估风险等级ID", ShortCode = "DynamicRiskLevelID", Desc = "动态评估风险等级ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DynamicRiskLevelID
        {
            get { return _dynamicRiskLevelID; }
            set { _dynamicRiskLevelID = value; }
        }

        [DataMember]
        [DataDesc(CName = "动态评估风险等级名称", ShortCode = "DynamicRiskLevelName", Desc = "动态评估风险等级名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DynamicRiskLevelName
        {
            get { return _dynamicRiskLevelName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DynamicRiskLevelName", value, value.ToString());
                _dynamicRiskLevelName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "延期程度ID", ShortCode = "DelayLevelID", Desc = "延期程度ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DelayLevelID
        {
            get { return _delayLevelID; }
            set { _delayLevelID = value; }
        }

        [DataMember]
        [DataDesc(CName = "延期程度名称", ShortCode = "DelayLevelName", Desc = "延期程度名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DelayLevelName
        {
            get { return _delayLevelName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DelayLevelName", value, value.ToString());
                _delayLevelName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "进场时间", ShortCode = "EntryTime", Desc = "进场时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EntryTime
        {
            get { return _entryTime; }
            set { _entryTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签署时间", ShortCode = "SignDate", Desc = "签署时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SignDate
        {
            get { return _signDate; }
            set { _signDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "销售负责人ID", ShortCode = "PrincipalID", Desc = "销售负责人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PrincipalID
        {
            get { return _principalID; }
            set { _principalID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Principal", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Principal
        {
            get { return _principal; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Principal", value, value.ToString());
                _principal = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目负责人", ShortCode = "ProjectLeaderID", Desc = "项目负责人", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProjectLeaderID
        {
            get { return _projectLeaderID; }
            set { _projectLeaderID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProjectLeader", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ProjectLeader
        {
            get { return _projectLeader; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ProjectLeader", value, value.ToString());
                _projectLeader = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实施人员ID", ShortCode = "ProjectExecID", Desc = "实施人员ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProjectExecID
        {
            get { return _projectExecID; }
            set { _projectExecID = value; }
        }

        [DataMember]
        [DataDesc(CName = "实施人员", ShortCode = "ProjectExec", Desc = "实施人员", ContextType = SysDic.All, Length = 50)]
        public virtual string ProjectExec
        {
            get { return _projectExec; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ProjectExec", value, value.ToString());
                _projectExec = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "进度管理人员ID", ShortCode = "PhaseManagerID", Desc = "进度管理人员ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PhaseManagerID
        {
            get { return _phaseManagerID; }
            set { _phaseManagerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "进度管理人员", ShortCode = "PhaseManager", Desc = "进度管理人员", ContextType = SysDic.All, Length = 50)]
        public virtual string PhaseManager
        {
            get { return _phaseManager; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PhaseManager", value, value.ToString());
                _phaseManager = value;
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
        [DataDesc(CName = "实际验收时间", ShortCode = "AcceptTime", Desc = "实际验收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AcceptTime
        {
            get { return _acceptTime; }
            set { _acceptTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计划人工作量", ShortCode = "EstiAllDays", Desc = "计划人工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double? EstiAllDays
        {
            get { return _estiAllDays; }
            set { _estiAllDays = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "动态剩余工作量", ShortCode = "DynamicRemainingWorkDays", Desc = "动态剩余工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double? DynamicRemainingWorkDays
        {
            get { return _dynamicRemainingWorkDays; }
            set { _dynamicRemainingWorkDays = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际人工作量", ShortCode = "AllDays", Desc = "实际人工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double? AllDays
        {
            get { return _allDays; }
            set { _allDays = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "进度延期百分比", ShortCode = "ScheduleDelayPercent", Desc = "进度延期百分比", ContextType = SysDic.All, Length = 8)]
        public virtual double? ScheduleDelayPercent
        {
            get { return _scheduleDelayPercent; }
            set { _scheduleDelayPercent = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "进度延期天数", ShortCode = "ScheduleDelayDays", Desc = "进度延期天数", ContextType = SysDic.All, Length = 8)]
        public virtual double? ScheduleDelayDays
        {
            get { return _scheduleDelayDays; }
            set { _scheduleDelayDays = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "工作量超百分比", ShortCode = "MoreWorkPercent", Desc = "工作量超百分比", ContextType = SysDic.All, Length = 8)]
        public virtual double? MoreWorkPercent
        {
            get { return _moreWorkPercent; }
            set { _moreWorkPercent = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "工作量超天数", ShortCode = "MoreWorkDays", Desc = "工作量超天数", ContextType = SysDic.All, Length = 8)]
        public virtual double? MoreWorkDays
        {
            get { return _moreWorkDays; }
            set { _moreWorkDays = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建人", ShortCode = "CreatEmpIdID", Desc = "创建人", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatEmpIdID
        {
            get { return _creatEmpIdID; }
            set { _creatEmpIdID = value; }
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
        [DataDesc(CName = "是否标准项目", ShortCode = "IsStandard", Desc = "是否标准项目", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsStandard
        {
            get { return _isStandard; }
            set { _isStandard = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReqEndTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReqEndTime
        {
            get { return _reqEndTime; }
            set { _reqEndTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预计工作量", ShortCode = "EstiWorkload", Desc = "预计工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double? EstiWorkload
        {
            get { return _estiWorkload; }
            set { _estiWorkload = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际工作量", ShortCode = "Workload", Desc = "实际工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double? Workload
        {
            get { return _workload; }
            set { _workload = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "协作评估", ShortCode = "TeamworkEvalID", Desc = "协作评估", ContextType = SysDic.All, Length = 8)]
        public virtual long? TeamworkEvalID
        {
            get { return _teamworkEvalID; }
            set { _teamworkEvalID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "进度评估", ShortCode = "PaceEvalID", Desc = "进度评估", ContextType = SysDic.All, Length = 8)]
        public virtual long? PaceEvalID
        {
            get { return _paceEvalID; }
            set { _paceEvalID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "效率评估", ShortCode = "EfficiencyEvalID", Desc = "效率评估", ContextType = SysDic.All, Length = 8)]
        public virtual long? EfficiencyEvalID
        {
            get { return _efficiencyEvalID; }
            set { _efficiencyEvalID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质量评估", ShortCode = "QualityEvalID", Desc = "质量评估", ContextType = SysDic.All, Length = 8)]
        public virtual long? QualityEvalID
        {
            get { return _qualityEvalID; }
            set { _qualityEvalID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总体评估", ShortCode = "TotalityEvalID", Desc = "总体评估", ContextType = SysDic.All, Length = 8)]
        public virtual long? TotalityEvalID
        {
            get { return _totalityEvalID; }
            set { _totalityEvalID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "紧急程度", ShortCode = "UrgencyID", Desc = "紧急程度", ContextType = SysDic.All, Length = 8)]
        public virtual long? UrgencyID
        {
            get { return _urgencyID; }
            set { _urgencyID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目类别", ShortCode = "TypeID", Desc = "项目类别", ContextType = SysDic.All, Length = 8)]
        public virtual long? TypeID
        {
            get { return _typeID; }
            set { _typeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "子任务数量", ShortCode = "SubCount", Desc = "子任务数量", ContextType = SysDic.All, Length = 4)]
        public virtual int SubCount
        {
            get { return _subCount; }
            set { _subCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InteractionCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? InteractionCount
        {
            get { return _interactionCount; }
            set { _interactionCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperLogCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperLogCount
        {
            get { return _operLogCount; }
            set { _operLogCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "WorkLogCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? WorkLogCount
        {
            get { return _workLogCount; }
            set { _workLogCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目负责人ID", ShortCode = "ProjectManagerID", Desc = "项目负责人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProjectManagerID
        {
            get { return _projectManagerID; }
            set { _projectManagerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目负责人", ShortCode = "ProjectManager", Desc = "项目负责人", ContextType = SysDic.All, Length = 50)]
        public virtual string ProjectManager
        {
            get { return _projectManager; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ProjectManager", value, value.ToString());
                _projectManager = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同类型ID", ShortCode = "ContentID", Desc = "合同类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ContentID
        {
            get { return _contentID; }
            set { _contentID = value; }
        }

        [DataMember]
        [DataDesc(CName = "合同类型", ShortCode = "Content", Desc = "合同类型", ContextType = SysDic.All, Length = 200)]
        public virtual string Content
        {
            get { return _content; }
            set
            {
                _content = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "省份ID", ShortCode = "ProvinceID", Desc = "省份ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProvinceID
        {
            get { return _provinceID; }
            set { _provinceID = value; }
        }

        [DataMember]
        [DataDesc(CName = "省份", ShortCode = "ProvinceName", Desc = "省份", ContextType = SysDic.All, Length = 200)]
        public virtual string ProvinceName
        {
            get { return _provinceName; }
            set
            {
                _provinceName = value;
            }
        }
        #endregion
    }
    #endregion
}