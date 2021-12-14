using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PProjectTaskProgress

    /// <summary>
    /// PProjectTaskProgress object for NHibernate mapped table 'P_ProjectTaskProgress'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = " 项目任务进度表", ClassCName = "PProjectTaskProgress", ShortCode = "PProjectTaskProgress", Desc = " 项目任务进度表")]
    public class PProjectTaskProgress : BaseEntity
    {
        #region Member Variables

        protected double _workload;
        protected DateTime? _registerTime;
        protected DateTime? _executeTime;
        protected string _taskWorkInfo;
        protected string _taskRisk;
        protected string _taskTypeDict;
        protected bool _isUse;
        protected string _memo;
        protected HREmployee _register;
        protected PProject _pProject;
        protected PProjectTask _pProjectTask;

        #endregion

        #region Constructors

        public PProjectTaskProgress() { }

        public PProjectTaskProgress(long labID, double workload, DateTime registerTime, DateTime executeTime, string taskWorkInfo, string taskRisk, string taskTypeDict, bool isUse, string memo, DateTime dataAddTime, byte[] dataTimeStamp, HREmployee register, PProject pProject, PProjectTask pProjectTask)
        {
            this._labID = labID;
            this._workload = workload;
            this._registerTime = registerTime;
            this._executeTime = executeTime;
            this._taskWorkInfo = taskWorkInfo;
            this._taskRisk = taskRisk;
            this._taskTypeDict = taskTypeDict;
            this._isUse = isUse;
            this._memo = memo;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._register = register;
            this._pProject = pProject;
            this._pProjectTask = pProjectTask;
        }

        #endregion

        #region Public Properties


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
        [DataDesc(CName = "实际开始时间", ShortCode = "RegisterTime", Desc = "实际开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegisterTime
        {
            get { return _registerTime; }
            set { _registerTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际结束时间", ShortCode = "ExecuteTime", Desc = "实际结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ExecuteTime
        {
            get { return _executeTime; }
            set { _executeTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "其他信息", ShortCode = "TaskWorkInfo", Desc = "其他信息", ContextType = SysDic.All, Length = -1)]
        public virtual string TaskWorkInfo
        {
            get { return _taskWorkInfo; }
            set { _taskWorkInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "附加信息", ShortCode = "TaskRisk", Desc = "附加信息", ContextType = SysDic.All, Length = -1)]
        public virtual string TaskRisk
        {
            get { return _taskRisk; }
            set { _taskRisk = value; }
        }

        [DataMember]
        [DataDesc(CName = "内容", ShortCode = "TaskTypeDict", Desc = "内容", ContextType = SysDic.All, Length = 500)]
        public virtual string TaskTypeDict
        {
            get { return _taskTypeDict; }
            set { _taskTypeDict = value; }
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
        [DataDesc(CName = "员工", ShortCode = "Register", Desc = "员工")]
        public virtual HREmployee Register
        {
            get { return _register; }
            set { _register = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目表", ShortCode = "PProject", Desc = "项目表")]
        public virtual PProject PProject
        {
            get { return _pProject; }
            set { _pProject = value; }
        }

        [DataMember]
        [DataDesc(CName = " 项目任务表", ShortCode = "PProjectTask", Desc = " 项目任务表")]
        public virtual PProjectTask PProjectTask
        {
            get { return _pProjectTask; }
            set { _pProjectTask = value; }
        }


        #endregion
    }
    #endregion
}