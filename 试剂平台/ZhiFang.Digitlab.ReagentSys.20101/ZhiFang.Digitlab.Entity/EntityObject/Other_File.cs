using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherFile

	/// <summary>
	/// OtherFile object for NHibernate mapped table 'Other_File'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "文件管理", ClassCName = "OtherFile", ShortCode = "OtherFile", Desc = "文件管理")]
    public class OtherFile : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _fileTypeID;
        protected long? _fileGroupID;
        protected long? _creatorID;
        protected int _isDirectory;
        protected string _fileName;
        protected long _parFileID;
        protected DateTime? _fileCreatTime;
        protected DateTime? _fileUpdateTime;
        protected double _size;
        protected string _creator;
        protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public OtherFile() { }

		public OtherFile( long labID, long fileTypeID, long fileGroupID, long creatorID, int isDirectory, string fileName, long parFileID, DateTime fileCreatTime, DateTime fileUpdateTime, double size, string creator, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._fileTypeID = fileTypeID;
			this._fileGroupID = fileGroupID;
			this._creatorID = creatorID;
			this._isDirectory = isDirectory;
			this._fileName = fileName;
			this._parFileID = parFileID;
			this._fileCreatTime = fileCreatTime;
			this._fileUpdateTime = fileUpdateTime;
			this._size = size;
			this._creator = creator;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "文件类型ID", ShortCode = "FileTypeID", Desc = "文件类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? FileTypeID
		{
			get { return _fileTypeID; }
			set { _fileTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "文件分类ID", ShortCode = "FileGroupID", Desc = "文件分类ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? FileGroupID
		{
			get { return _fileGroupID; }
			set { _fileGroupID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "CreatorID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否目录", ShortCode = "IsDirectory", Desc = "是否目录", ContextType = SysDic.All, Length = 4)]
        public virtual int IsDirectory
		{
			get { return _isDirectory; }
			set { _isDirectory = value; }
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "FileName", Desc = "名称", ContextType = SysDic.All, Length = 200)]
        public virtual string FileName
		{
			get { return _fileName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for FileName", value, value.ToString());
				_fileName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "父节点ID", ShortCode = "ParFileID", Desc = "父节点ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ParFileID
		{
			get { return _parFileID; }
			set { _parFileID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建时间", ShortCode = "FileCreatTime", Desc = "创建时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FileCreatTime
		{
			get { return _fileCreatTime; }
			set { _fileCreatTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "修改时间", ShortCode = "FileUpdateTime", Desc = "修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FileUpdateTime
		{
			get { return _fileUpdateTime; }
			set { _fileUpdateTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "大小", ShortCode = "Size", Desc = "大小", ContextType = SysDic.All, Length = 8)]
        public virtual double Size
		{
			get { return _size; }
			set { _size = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.All, Length = 20)]
        public virtual string Creator
		{
			get { return _creator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
				_creator = value;
			}
		}


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

		#endregion
	}
	#endregion
}