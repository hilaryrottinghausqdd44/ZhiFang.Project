using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PTaskTypeEmpLink

    /// <summary>
    /// FFile object for NHibernate mapped table 'F_File'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "任务类型与人员关系", ClassCName = "PTaskTypeEmpLink", ShortCode = "PTaskTypeEmpLink", Desc = "项目类型与人员关系")]
    public class PTaskTypeEmpLink : BaseEntity
    {
        #region Member Variables

        protected long _TaskTypeID;
        protected string _TaskTypeName;
        protected long _EmpID;
        protected string _EmpName;
        protected DateTime? _DataUpdateTime;
        protected bool _TwoAudit;
        protected bool _Publish;

        #endregion

        #region Constructors

        public PTaskTypeEmpLink() { }        

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "任务类型ID", ShortCode = "TaskTypeID", Desc = "任务类型ID", ContextType = SysDic.All, Length = 100)]
        public virtual long TaskTypeID
        {
            get { return _TaskTypeID; }
            set
            {
                _TaskTypeID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "任务类型名称", ShortCode = "TaskTypeName", Desc = "任务类型名称", ContextType = SysDic.All, Length = 8)]
        public virtual string TaskTypeName
        {
            get { return _TaskTypeName; }
            set { _TaskTypeName = value; }
        }

        [DataMember]
        [DataDesc(CName = "员工ID", ShortCode = "EmpID", Desc = "员工ID", ContextType = SysDic.All, Length = 50)]
        public virtual long EmpID
        {
            get { return _EmpID; }
            set
            {
                _EmpID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工姓名", ShortCode = "EmpName", Desc = "员工姓名", ContextType = SysDic.All, Length = 8)]
        public virtual string EmpName
        {
            get { return _EmpName; }
            set { _EmpName = value; }
        }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _DataUpdateTime; }
            set { _DataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "任务类型二审权限", ShortCode = "TwoAudit", Desc = "任务类型二审权限", ContextType = SysDic.All, Length = 100)]
        public virtual bool TwoAudit
        {
            get { return _TwoAudit; }
            set
            {
                _TwoAudit = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "任务类型分配权限", ShortCode = "Publish", Desc = "任务类型分配权限", ContextType = SysDic.All, Length = 100)]
        public virtual bool Publish
        {
            get { return _Publish; }
            set
            {
                _Publish = value;
            }
        }


        #endregion
    }
    #endregion
}