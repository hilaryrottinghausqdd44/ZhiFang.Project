using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PProjectTask

    /// <summary>
    /// PProjectTask object for NHibernate mapped table 'P_ProjectTask'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = " 项目任务表", ClassCName = "PProjectTask", ShortCode = "PProjectTask", Desc = " 项目任务表")]
    public class PProjectTask : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _taskHelp;
        protected long? _pTaskID;
        protected double _standWorkload;
        protected double _estiWorkload;
        protected double _workload;
        protected DateTime? _estiStartTime;
        protected DateTime? _estiEndTime;
        protected DateTime? _startTime;
        protected DateTime? _endTime;
        protected double _remainWorkDays;
        protected double _estiAllDays;
        protected double _allDays;
        protected string _otherMsg;
        protected string _extraMsg;
        protected bool _isStandard;
        protected bool _isUse;
        protected string _memo;
        protected string _contents;
        protected int _dispOrder;
        protected int? _planTheNextFewDays;
        protected int? _planTheEndFewDays;
        protected HREmployee _creater;
        protected PProject _pProject;
        protected IList<PProjectTaskProgress> _pProjectTaskProgressList;

        #endregion

        #region Constructors

        public PProjectTask() { }

        public PProjectTask(long labID, string cName, string taskHelp, long pTaskID, double standWorkload, double estiWorkload, double workload, DateTime estiStartTime, DateTime estiEndTime, DateTime startTime, DateTime endTime, double remainWorkDays, double estiAllDays, double allDays, string otherMsg, string extraMsg, bool isStandard, bool isUse, string memo, string contents, DateTime dataAddTime, byte[] dataTimeStamp, HREmployee creater, PProject pProject)
        {
            this._labID = labID;
            this._cName = cName;
            this._taskHelp = taskHelp;
            this._pTaskID = pTaskID;
            this._standWorkload = standWorkload;
            this._estiWorkload = estiWorkload;
            this._workload = workload;
            this._estiStartTime = estiStartTime;
            this._estiEndTime = estiEndTime;
            this._startTime = startTime;
            this._endTime = endTime;
            this._remainWorkDays = remainWorkDays;
            this._estiAllDays = estiAllDays;
            this._allDays = allDays;
            this._otherMsg = otherMsg;
            this._extraMsg = extraMsg;
            this._isStandard = isStandard;
            this._isUse = isUse;
            this._memo = memo;
            this._contents = contents;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._creater = creater;
            this._pProject = pProject;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "合同名称", ShortCode = "CName", Desc = "合同名称", ContextType = SysDic.All, Length = 500)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目进度名称", ShortCode = "TaskHelp", Desc = "项目进度名称", ContextType = SysDic.All, Length = 500)]
        public virtual string TaskHelp
        {
            get { return _taskHelp; }
            set { _taskHelp = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同ID", ShortCode = "PTaskID", Desc = "合同ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PTaskID
        {
            get { return _pTaskID; }
            set { _pTaskID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预计工作量", ShortCode = "StandWorkload", Desc = "预计工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double StandWorkload
        {
            get { return _standWorkload; }
            set { _standWorkload = value; }
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
        [DataDesc(CName = "实际工作量", ShortCode = "Workload", Desc = "实际工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double Workload
        {
            get { return _workload; }
            set { _workload = value; }
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
        [DataDesc(CName = "动态剩余工作量", ShortCode = "RemainWorkDays", Desc = "动态剩余工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double RemainWorkDays
        {
            get { return _remainWorkDays; }
            set { _remainWorkDays = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计划人工作量", ShortCode = "EstiAllDays", Desc = "计划人工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double EstiAllDays
        {
            get { return _estiAllDays; }
            set { _estiAllDays = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际人工作量", ShortCode = "AllDays", Desc = "实际人工作量", ContextType = SysDic.All, Length = 8)]
        public virtual double AllDays
        {
            get { return _allDays; }
            set { _allDays = value; }
        }

        [DataMember]
        [DataDesc(CName = "其他信息", ShortCode = "OtherMsg", Desc = "其他信息", ContextType = SysDic.All, Length = -1)]
        public virtual string OtherMsg
        {
            get { return _otherMsg; }
            set { _otherMsg = value; }
        }

        [DataMember]
        [DataDesc(CName = "附加信息", ShortCode = "ExtraMsg", Desc = "附加信息", ContextType = SysDic.All, Length = -1)]
        public virtual string ExtraMsg
        {
            get { return _extraMsg; }
            set { _extraMsg = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsStandard", Desc = "", ContextType = SysDic.All, Length = 1)]
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
            set { _memo = value; }
        }

        [DataMember]
        [DataDesc(CName = "内容", ShortCode = "Contents", Desc = "内容", ContextType = SysDic.All, Length = -1)]
        public virtual string Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }
        [DataMember]
        [DataDesc(CName = "标准开始时间", ShortCode = "PlanTheNextFewDays", Desc = "标准开始时间", ContextType = SysDic.All, Length = 4)]
        public virtual int? PlanTheNextFewDays
        {
            get { return _planTheNextFewDays; }
            set { _planTheNextFewDays = value; }
        }
        [DataMember]
        [DataDesc(CName = "标准结束时间", ShortCode = "PlanTheNextFewDays", Desc = "标准结束时间", ContextType = SysDic.All, Length = 4)]
        public virtual int? PlanTheEndFewDays
        {
            get { return _planTheEndFewDays; }
            set { _planTheEndFewDays = value; }
        }
        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "Creat", Desc = "员工")]
        public virtual HREmployee Creater
        {
            get { return _creater; }
            set { _creater = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目表", ShortCode = "PProject", Desc = "项目表")]
        public virtual PProject PProject
        {
            get { return _pProject; }
            set { _pProject = value; }
        }

        [DataMember]
        [DataDesc(CName = " 项目任务进度表", ShortCode = "PProjectTaskProgressList", Desc = " 项目任务进度表")]
        public virtual IList<PProjectTaskProgress> PProjectTaskProgressList
        {
            get
            {
                if (_pProjectTaskProgressList == null)
                {
                    _pProjectTaskProgressList = new List<PProjectTaskProgress>();
                }
                return _pProjectTaskProgressList;
            }
            set { _pProjectTaskProgressList = value; }
        }


        #endregion
    }
    #endregion
}