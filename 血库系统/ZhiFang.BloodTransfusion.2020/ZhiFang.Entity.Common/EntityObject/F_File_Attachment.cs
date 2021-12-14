using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.Common
{
	#region FFileAttachment

	/// <summary>
	/// FFileAttachment object for NHibernate mapped table 'F_File_Attachment'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "文档附件表", ClassCName = "FFileAttachment", ShortCode = "FFileAttachment", Desc = "文档附件表")]
	public class FFileAttachment : BaseEntity
	{
		#region Member Variables
		
        protected string _fileName;
        protected string _fileExt;
        protected long? _fileSize;
        protected string _filePath;

        protected string _newFileName;
        protected string _fileType;

        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
		protected FFile _fFile;
		protected HREmployee _creator;

		#endregion

		#region Constructors

		public FFileAttachment() { }

		public FFileAttachment( long labID, string fileName, string fileExt, string newFileName, string fileType, long fileSize, string filePath, string memo, int dispOrder, bool isUse, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, FFile fFile, HREmployee creator )
		{
			this._labID = labID;
			this._fileName = fileName;
			this._fileExt = fileExt;
			this._fileSize = fileSize;
			this._filePath = filePath;

            this._newFileName = newFileName;
            this._fileType = fileType;

            this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._creatorName = creatorName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._fFile = fFile;
			this._creator = creator;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "文件全名", ShortCode = "FileName", Desc = "文件全名", ContextType = SysDic.All, Length = 500)]
        public virtual string FileName
		{
			get { return _fileName; }
			set
			{
				_fileName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "文件扩展名", ShortCode = "FileExt", Desc = "文件扩展名", ContextType = SysDic.All, Length = 500)]
        public virtual string FileExt
		{
			get { return _fileExt; }
			set
			{
				_fileExt = value;
			}
		}
        [DataMember]
        [DataDesc(CName = "文件名称", ShortCode = "NewFileName", Desc = "文件名称", ContextType = SysDic.All, Length = 500)]
        public virtual string NewFileName
        {
            get { return _newFileName; }
            set
            {
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
        [DataDesc(CName = "文件路径", ShortCode = "FilePath", Desc = "文件路径", ContextType = SysDic.All, Length = 500)]
        public virtual string FilePath
		{
			get { return _filePath; }
			set
			{
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
        [DataDesc(CName = "文档表", ShortCode = "FFile", Desc = "文档表")]
		public virtual FFile FFile
		{
			get { return _fFile; }
			set { _fFile = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "Creator", Desc = "员工")]
		public virtual HREmployee Creator
		{
			get { return _creator; }
			set { _creator = value; }
		}

        
		#endregion
	}
	#endregion
}