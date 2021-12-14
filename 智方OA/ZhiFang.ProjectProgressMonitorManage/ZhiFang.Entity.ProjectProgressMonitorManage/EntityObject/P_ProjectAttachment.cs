using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PProjectAttachment

	/// <summary>
	/// PProjectAttachment object for NHibernate mapped table 'P_ProjectAttachment'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "项目附件表", ClassCName = "PProjectAttachment", ShortCode = "PProjectAttachment", Desc = "项目附件表")]
	public class PProjectAttachment : BaseEntity
	{
		#region Member Variables
		
        protected string _fileName;
        protected string _fileExt;
        protected string _newFileName;
        protected string _fileType;

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
        protected string _attachmentType;
        protected PInteraction _pInteraction;
		protected PTask _pTask;
		//protected PWorkTaskLog _pWorkTaskLog;
        protected PWorkDayLog _pWorkDayLog;
        protected PWorkMonthLog _pWorkMonthLog;
        protected PWorkWeekLog _pWorkWeekLog;
        protected PContract _pContract;

        #endregion

        #region Constructors

        public PProjectAttachment() { }

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "文件全名", ShortCode = "FileName", Desc = "文件全名", ContextType = SysDic.All, Length = 100)]
        public virtual string FileName
		{
			get { return _fileName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for FileName", value, value.ToString());
				_fileName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "文件扩展名", ShortCode = "FileExt", Desc = "文件扩展名", ContextType = SysDic.All, Length = 100)]
        public virtual string FileExt
		{
			get { return _fileExt; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for FileExt", value, value.ToString());
				_fileExt = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "文件名称", ShortCode = "NewFileName", Desc = "文件名称", ContextType = SysDic.All, Length = 100)]
        public virtual string NewFileName
        {
            get { return _newFileName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for NewFileName", value, value.ToString());
                _newFileName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "文件内容类型", ShortCode = "FileType", Desc = "文件内容类型", ContextType = SysDic.All, Length = 100)]
        public virtual string FileType
        {
            get { return _fileType; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for FileType", value, value.ToString());
                _fileType = value;
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
        [DataDesc(CName = "文件路径", ShortCode = "FilePath", Desc = "文件路径", ContextType = SysDic.All, Length = 200)]
        public virtual string FilePath
		{
			get { return _filePath; }
			set
			{
				if ( value != null && value.Length > 200)
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
				if ( value != null && value.Length > 50)
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
				if ( value != null && value.Length > 500)
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
				if ( value != null && value.Length > 100)
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
				if ( value != null && value.Length > 100)
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
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for WorkTaskLogName", value, value.ToString());
				_workTaskLogName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "附件类型", ShortCode = "AttachmentType", Desc = "附件类型", ContextType = SysDic.All, Length = 100)]
        public virtual string AttachmentType
        {
            get { return _attachmentType; }
            set
            {
                _attachmentType = value;
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

  //      [DataMember]
  //      [DataDesc(CName = "工作任务日志表", ShortCode = "PWorkTaskLog", Desc = "工作任务日志表")]
		//public virtual PWorkTaskLog PWorkTaskLog
		//{
		//	get { return _pWorkTaskLog; }
		//	set { _pWorkTaskLog = value; }
		//}

        [DataMember]
        [DataDesc(CName = "工作日志表", ShortCode = "PWorkDayLog", Desc = "工作日志表")]
        public virtual PWorkDayLog PWorkDayLog
        {
            get { return _pWorkDayLog; }
            set { _pWorkDayLog = value; }
        }

        [DataMember]
        [DataDesc(CName = "工作月总结表", ShortCode = "PWorkMonthLog", Desc = "工作月总结表")]
        public virtual PWorkMonthLog PWorkMonthLog
        {
            get { return _pWorkMonthLog; }
            set { _pWorkMonthLog = value; }
        }

        [DataMember]
        [DataDesc(CName = "工作周计划表", ShortCode = "PWorkWeekLog", Desc = "工作周计划表")]
        public virtual PWorkWeekLog PWorkWeekLog
        {
            get { return _pWorkWeekLog; }
            set { _pWorkWeekLog = value; }
        }

        [DataMember]
        [DataDesc(CName = "合同", ShortCode = "PContract", Desc = "合同")]
        public virtual PContract PContract
        {
            get { return _pContract; }
            set { _pContract = value; }
        }

        #endregion
    }
	#endregion
}