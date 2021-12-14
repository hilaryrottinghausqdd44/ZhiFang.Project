using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region EAttachment

    /// <summary>
    /// EAttachment object for NHibernate mapped table 'E_Attachment'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "质量记录模板附件表", ClassCName = "EAttachment", ShortCode = "EAttachment", Desc = "质量记录模板附件表")]
    public class EAttachment : BaseEntity
    {
        #region Member Variables

        protected string _templetType;
        protected string _templetTypeCode;
        protected string _fileName;
        protected string _fileExt;
        protected long? _fileSize;
        protected string _filePath;
        protected string _fileNewName;
        protected DateTime? _fileUploadDate;
        protected string _fileType;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
        protected ETemplet _eTemplet;
        protected EEquip _eEquip;
        protected HREmployee _creator;

        #endregion

        #region Constructors

        public EAttachment() { }

        public EAttachment(long labID, string templetType, string templetTypeCode, string fileName, string fileExt, long fileSize, string filePath, string fileNewName, DateTime fileUploadDate, string fileType, string memo, int dispOrder, bool isUse, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ETemplet eTemplet, HREmployee creator)
        {
            this._labID = labID;
            this._templetType = templetType;
            this._templetTypeCode = templetTypeCode;
            this._fileName = fileName;
            this._fileExt = fileExt;
            this._fileSize = fileSize;
            this._filePath = filePath;
            this._fileNewName = fileNewName;
            this._fileUploadDate = fileUploadDate;
            this._fileType = fileType;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._creatorName = creatorName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._eTemplet = eTemplet;
            this._creator = creator;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "模板类型名称", ShortCode = "TempletType", Desc = "模板类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string TempletType
        {
            get { return _templetType; }
            set { _templetType = value; }
        }

        [DataMember]
        [DataDesc(CName = "模板类型编码", ShortCode = "TempletTypeCode", Desc = "模板类型编码", ContextType = SysDic.All, Length = 100)]
        public virtual string TempletTypeCode
        {
            get { return _templetTypeCode; }
            set { _templetTypeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "文件名", ShortCode = "FileName", Desc = "文件名", ContextType = SysDic.All, Length = 100)]
        public virtual string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [DataMember]
        [DataDesc(CName = "文件扩展名", ShortCode = "FileExt", Desc = "文件扩展名", ContextType = SysDic.All, Length = 100)]
        public virtual string FileExt
        {
            get { return _fileExt; }
            set { _fileExt = value; }
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
            set { _filePath = value; }
        }

        [DataMember]
        [DataDesc(CName = "文件自定义名称", ShortCode = "FileNewName", Desc = "文件自定义名称", ContextType = SysDic.All, Length = 100)]
        public virtual string FileNewName
        {
            get { return _fileNewName; }
            set { _fileNewName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "文档上传日期", ShortCode = "FileUploadDate", Desc = "文档上传日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FileUploadDate
        {
            get { return _fileUploadDate; }
            set { _fileUploadDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "文件内容类型", ShortCode = "FileType", Desc = "文件内容类型", ContextType = SysDic.All, Length = 100)]
        public virtual string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set { _memo = value; }
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
            set { _creatorName = value; }
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
        [DataDesc(CName = "仪器模板表", ShortCode = "ETemplet", Desc = "仪器模板表")]
        public virtual ETemplet ETemplet
        {
            get { return _eTemplet; }
            set { _eTemplet = value; }
        }


        [DataMember]
        [DataDesc(CName = "仪器表", ShortCode = "EEquip", Desc = "仪器表")]
        public virtual EEquip EEquip
        {
            get { return _eEquip; }
            set { _eEquip = value; }
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