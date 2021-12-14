using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PGMProgram

    /// <summary>
    /// PGMProgram object for NHibernate mapped table 'PGM_Program'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "程序列表", ClassCName = "PGMProgram", ShortCode = "PGMProgram", Desc = "程序列表")]
    public class PGMProgram : BusinessBase
    {
        #region Member Variables

        protected string _title;
        protected string _no;
        protected string _content;
        protected int _status;
        protected string _keyword;
        protected string _versionNo;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected long? _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
       // protected long? _originalFileID;
        protected long? _publisherID;
        protected string _publisherName;
        protected int _counts;
        protected bool _isDiscuss;
        protected DateTime? _publisherDateTime;
        protected long? _size;
        protected string _fileName;
        protected string _fileExt;
        protected string _filePath;
        protected string _newFileName;
        protected string _fileType;
        protected long? _clientID;
        protected string _clientName;
        protected long? _otherFactoryID;
        protected string _otherFactoryName;
        protected string _sQH;
        protected BEquip _bEquip;
        protected BDictTree _pBDictTree;
        protected BDictTree _subBDictTree;
        protected PGMProgram _originalPGMProgram;
        #endregion

        #region Constructors

        public PGMProgram() { }

        public PGMProgram(long labID, string title, string no, string content, int status, string keyword, string versionNo, string memo, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, PGMProgram originalPGMProgram, long publisherID, string publisherName, int counts, bool isDiscuss, DateTime publisherDateTime, long size, string fileName, string fileExt, string filePath, string newFileName, string fileType, long clientID, string clientName, long otherFactoryID, string otherFactoryName,string sQH, BEquip bEquip, BDictTree p, BDictTree sub)
        {
            this._labID = labID;
            this._title = title;
            this._no = no;
            this._content = content;
            this._status = status;
            this._keyword = keyword;
            this._versionNo = versionNo;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._creatorID = creatorID;
            this._creatorName = creatorName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._originalPGMProgram = originalPGMProgram;
            this._publisherID = publisherID;
            this._publisherName = publisherName;
            this._counts = counts;
            this._isDiscuss = isDiscuss;
            this._publisherDateTime = publisherDateTime;
            this._size = size;
            this._fileName = fileName;
            this._fileExt = fileExt;
            this._filePath = filePath;
            this._newFileName = newFileName;
            this._fileType = fileType;
            this._clientID = clientID;
            this._clientName = clientName;
            this._otherFactoryID = otherFactoryID;
            this._otherFactoryName = otherFactoryName;
            this._sQH = sQH;
            this._bEquip = bEquip;
            this._pBDictTree = p;
            this._subBDictTree = sub;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户Id", ShortCode = "ClientID", Desc = "用户Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ClientID
        {
            get { return _clientID; }
            set { _clientID = value; }
        }

        [DataMember]
        [DataDesc(CName = "用户名称", ShortCode = "ClientName", Desc = "用户名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ClientName
        {
            get { return _clientName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ClientName", value, value.ToString());
                _clientName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "厂家Id", ShortCode = "OtherFactoryID", Desc = "厂家Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? OtherFactoryID
        {
            get { return _otherFactoryID; }
            set { _otherFactoryID = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂家", ShortCode = "OtherFactoryName", Desc = "厂家", ContextType = SysDic.All, Length = 200)]
        public virtual string OtherFactoryName
        {
            get { return _otherFactoryName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for OtherFactoryName", value, value.ToString());
                _otherFactoryName = value;
            }
        }


        [DataMember]
        [DataDesc(CName = "程序标题", ShortCode = "Title", Desc = "程序标题", ContextType = SysDic.All, Length = 100)]
        public virtual string Title
        {
            get { return _title; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Title", value, value.ToString());
                _title = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "程序编号", ShortCode = "No", Desc = "程序编号", ContextType = SysDic.All, Length = 100)]
        public virtual string No
        {
            get { return _no; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for No", value, value.ToString());
                _no = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "程序详细内容", ShortCode = "Content", Desc = "程序详细内容", ContextType = SysDic.All, Length =Int32.MaxValue)]
        public virtual string Content
        {
            get { return _content; }
            set
            {
                if (value != null && value.Length >1002400 )
                    throw new ArgumentOutOfRangeException("Invalid value for Content", value, value.ToString());
                _content = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "状态", ShortCode = "Status", Desc = "状态", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "关键字", ShortCode = "Keyword", Desc = "关键字", ContextType = SysDic.All, Length = 100)]
        public virtual string Keyword
        {
            get { return _keyword; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Keyword", value, value.ToString());
                _keyword = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "版本号", ShortCode = "VersionNo", Desc = "版本号", ContextType = SysDic.All, Length = 100)]
        public virtual string VersionNo
        {
            get { return _versionNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for VersionNo", value, value.ToString());
                _versionNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "程序概要说明", ShortCode = "Memo", Desc = "程序概要说明", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length >1002400 )
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
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
        [DataDesc(CName = "创建者ID", ShortCode = "CreatorID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
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
                if (value != null && value.Length > 50)
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

        //[DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        //[DataDesc(CName = "父程序Id", ShortCode = "OriginalFileID", Desc = "父程序Id", ContextType = SysDic.All, Length = 8)]
        //public virtual long? OriginalFileID
        //{
        //    get { return _originalFileID; }
        //    set { _originalFileID = value; }
        //}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "程序发布人Id", ShortCode = "PublisherID", Desc = "程序发布人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? PublisherID
        {
            get { return _publisherID; }
            set { _publisherID = value; }
        }

        [DataMember]
        [DataDesc(CName = "程序发布人名称", ShortCode = "PublisherName", Desc = "程序发布人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string PublisherName
        {
            get { return _publisherName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PublisherName", value, value.ToString());
                _publisherName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "总阅读数", ShortCode = "Counts", Desc = "总阅读数", ContextType = SysDic.All, Length = 4)]
        public virtual int Counts
        {
            get { return _counts; }
            set { _counts = value; }
        }

        [DataMember]
        [DataDesc(CName = "发布后是否可评论", ShortCode = "IsDiscuss", Desc = "发布后是否可评论", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDiscuss
        {
            get { return _isDiscuss; }
            set { _isDiscuss = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发布时间", ShortCode = "PublisherDateTime", Desc = "发布时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PublisherDateTime
        {
            get { return _publisherDateTime; }
            set { _publisherDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "程序大小", ShortCode = "Size", Desc = "程序大小", ContextType = SysDic.All, Length = 8)]
        public virtual long? Size
        {
            get { return _size; }
            set { _size = value; }
        }

        [DataMember]
        [DataDesc(CName = "文件名", ShortCode = "FileName", Desc = "文件名", ContextType = SysDic.All, Length = 500)]
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
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for FileExt", value, value.ToString());
                _fileExt = value;
            }
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
        [DataDesc(CName = "程序文件自定义名称", ShortCode = "NewFileName", Desc = "程序文件自定义名称", ContextType = SysDic.All, Length = 1000)]
        public virtual string NewFileName
        {
            get { return _newFileName; }
            set
            {
                if (value != null && value.Length > 1000)
                    throw new ArgumentOutOfRangeException("Invalid value for NewFileName", value, value.ToString());
                _newFileName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "程序文件内容类型", ShortCode = "FileType", Desc = "程序文件内容类型", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "授权号", ShortCode = "SQH", Desc = "授权号", ContextType = SysDic.All, Length = 100)]
        public virtual string SQH
        {
            get { return _sQH; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for SQH", value, value.ToString());
                _sQH = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "仪器表", ShortCode = "EEquip", Desc = "仪器表")]
        public virtual BEquip BEquip
        {
            get { return _bEquip; }
            set { _bEquip = value; }
        }

        [DataMember]
        [DataDesc(CName = "所属分类类型", ShortCode = "PBDictTree", Desc = "所属分类类型")]
        public virtual BDictTree PBDictTree
        {
            get { return _pBDictTree; }
            set { _pBDictTree = value; }
        }

        [DataMember]
        [DataDesc(CName = "所属分类", ShortCode = "SubBDictTree", Desc = "所属分类(字典树)")]
        public virtual BDictTree SubBDictTree
        {
            get { return _subBDictTree; }
            set { _subBDictTree = value; }
        }

        [DataMember]
        [DataDesc(CName = "程序选择(父程序)", ShortCode = "OriginalPGMProgram", Desc = "程序选择(父程序)")]
        public virtual PGMProgram OriginalPGMProgram
        {
            get { return _originalPGMProgram; }
            set { _originalPGMProgram = value; }
        }

        #endregion
    }
    #endregion
}