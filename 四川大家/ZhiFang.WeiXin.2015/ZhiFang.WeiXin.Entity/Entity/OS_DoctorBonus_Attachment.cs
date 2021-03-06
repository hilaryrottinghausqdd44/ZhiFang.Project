using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region OSDoctorBonusAttachment

	/// <summary>
	/// OSDoctorBonusAttachment object for NHibernate mapped table 'OS_DoctorBonus_Attachment'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "医生结算单附件表", ClassCName = "OSDoctorBonusAttachment", ShortCode = "OSDoctorBonusAttachment", Desc = "医生结算单附件表")]
	public class OSDoctorBonusAttachment : BaseEntity
	{
		#region Member Variables
		
        protected long? _bobjectID;
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

		public OSDoctorBonusAttachment() { }

		public OSDoctorBonusAttachment( long bobjectID, string fileName, string fileExt, long fileSize, string filePath, string memo, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string newFileName, string fileType, string businessModuleCode )
		{
			this._bobjectID = bobjectID;
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
        [DataDesc(CName = "文档主键ID", ShortCode = "BobjectID", Desc = "文档主键ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BobjectID
		{
			get { return _bobjectID; }
			set { _bobjectID = value; }
		}

        [DataMember]
        [DataDesc(CName = "文件名", ShortCode = "FileName", Desc = "文件名", ContextType = SysDic.All, Length = 100)]
        public virtual string FileName
		{
			get { return _fileName; }
			set
			{
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
				_businessModuleCode = value;
			}
		}

		
		#endregion
	}
	#endregion
}