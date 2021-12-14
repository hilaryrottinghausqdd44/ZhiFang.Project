using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PCustomerServiceAttachment

	/// <summary>
	/// PCustomerServiceAttachment object for NHibernate mapped table 'P_CustomerServiceAttachment'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "服务单附件", ClassCName = "PCustomerServiceAttachment", ShortCode = "PCustomerServiceAttachment", Desc = "服务单附件")]
	public class PCustomerServiceAttachment : BaseEntity
	{
		#region Member Variables
		
        protected long? _customerServiceID;
        protected string _fileName;
        protected string _fileExt;
        protected long _fileSize;
        protected string _filePath;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
        protected string _newFileName;
        protected string _fileType;
        protected string _businessModuleCode;
		

		#endregion

		#region Constructors

		public PCustomerServiceAttachment() { }

		public PCustomerServiceAttachment( long labID, long customerServiceID, string fileName, string fileExt, long fileSize, string filePath, string memo, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string newFileName, string fileType, string businessModuleCode )
		{
			this._labID = labID;
			this._customerServiceID = customerServiceID;
			this._fileName = fileName;
			this._fileExt = fileExt;
			this._fileSize = fileSize;
			this._filePath = filePath;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._creatorID = creatorID;
			this._creatorName = creatorName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._newFileName = newFileName;
			this._fileType = fileType;
			this._businessModuleCode = businessModuleCode;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务单ID", ShortCode = "CustomerServiceID", Desc = "服务单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CustomerServiceID
		{
			get { return _customerServiceID; }
			set { _customerServiceID = value; }
		}

        [DataMember]
        [DataDesc(CName = "文件名", ShortCode = "FileName", Desc = "文件名", ContextType = SysDic.All, Length = 100)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "文件大小", ShortCode = "FileSize", Desc = "文件大小", ContextType = SysDic.All, Length = 8)]
        public virtual long FileSize
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
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
        [DataDesc(CName = "创建者", ShortCode = "CreatorID", Desc = "创建者", ContextType = SysDic.All, Length = 8)]
        public virtual long CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreatorName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NewFileName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string NewFileName
		{
			get { return _newFileName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for NewFileName", value, value.ToString());
				_newFileName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FileType", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string FileType
		{
			get { return _fileType; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for FileType", value, value.ToString());
				_fileType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "业务模块代码", ShortCode = "BusinessModuleCode", Desc = "业务模块代码", ContextType = SysDic.All, Length = 20)]
        public virtual string BusinessModuleCode
		{
			get { return _businessModuleCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BusinessModuleCode", value, value.ToString());
				_businessModuleCode = value;
			}
		}

		
		#endregion
	}
	#endregion
}