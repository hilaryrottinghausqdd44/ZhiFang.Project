using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PProjectDocument

    /// <summary>
    /// PProjectDocument object for NHibernate mapped table 'P_ProjectDocument'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "项目文档表", ClassCName = "PProjectDocument", ShortCode = "PProjectDocument", Desc = "项目文档表")]
    public class PProjectDocument : BaseEntity
    {
        #region Member Variables

        protected long? _projectTaskID;
        protected string _documentName;
        protected string _documentLink;
        protected string _content;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _fileDateTime;
        protected HREmployee _creater;
        protected PProject _pProject;

        #endregion

        #region Constructors

        public PProjectDocument() { }

        public PProjectDocument(long projectTaskID, string documentName, string documentLink, string content, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime fileDateTime, byte[] dataTimeStamp, HREmployee creater, PProject pProject)
        {
            this._projectTaskID = projectTaskID;
            this._documentName = documentName;
            this._documentLink = documentLink;
            this._content = content;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._fileDateTime = fileDateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._creater = creater;
            this._pProject = pProject;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "任务ID", ShortCode = "ProjectTaskID", Desc = "任务ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProjectTaskID
        {
            get { return _projectTaskID; }
            set { _projectTaskID = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档名称", ShortCode = "DocumentName", Desc = "文档名称", ContextType = SysDic.All, Length = 500)]
        public virtual string DocumentName
        {
            get { return _documentName; }
            set { _documentName = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档模板连接", ShortCode = "DocumentLink", Desc = "文档模板连接", ContextType = SysDic.All, Length = 500)]
        public virtual string DocumentLink
        {
            get { return _documentLink; }
            set { _documentLink = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档内容", ShortCode = "Content", Desc = "文档内容", ContextType = SysDic.All, Length = -1)]
        public virtual string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        [DataMember]
        [DataDesc(CName = "次序", ShortCode = "DispOrder", Desc = "次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "归档时间", ShortCode = "FileDateTime", Desc = "归档时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FileDateTime
        {
            get { return _fileDateTime; }
            set { _fileDateTime = value; }
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


        #endregion
    }
    #endregion
}