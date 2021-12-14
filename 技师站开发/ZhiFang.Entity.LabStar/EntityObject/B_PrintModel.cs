using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region BPrintModel

    /// <summary>
    /// BPrintModel object for NHibernate mapped table 'B_PrintModel'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BPrintModel", ShortCode = "BPrintModel", Desc = "")]
    public class BPrintModel : BaseEntity
    {
        #region Member Variables

        protected string _BusinessTypeName;
        protected string _ModelTypeName;
        protected string _BusinessTypeCode;
        protected string _ModelTypeCode;
        protected string _fileName;
        protected byte[] _fileData;
        protected string _fileComment;
        protected int _dispOrder;
        protected long? _operaterID;
        protected string _operater;
        protected long? _finalOperaterID;
        protected string _finalOperater;
        protected bool _isProtect;
        protected bool _isUse;
        protected DateTime? _fileUploadTime;
        protected string _uploadComputer;
        protected DateTime? _dataUpdateTime;

        #endregion

        #region Constructors

        public BPrintModel() { }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "BusinessTypeName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string BusinessTypeName
        {
            get { return _BusinessTypeName; }
            set { _BusinessTypeName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ModelTypeName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ModelTypeName
        {
            get { return _ModelTypeName; }
            set { _ModelTypeName = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "BusinessTypeCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BusinessTypeCode
        {
            get { return _BusinessTypeCode; }
            set { _BusinessTypeCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ModelTypeCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ModelTypeCode
        {
            get { return _ModelTypeCode; }
            set { _ModelTypeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FileName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FileData", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] FileData
        {
            get { return _fileData; }
            set { _fileData = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FileComment", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string FileComment
        {
            get { return _fileComment; }
            set { _fileComment = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperaterID
        {
            get { return _operaterID; }
            set { _operaterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Operater", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Operater
        {
            get { return _operater; }
            set { _operater = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FinalOperaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? FinalOperaterID
        {
            get { return _finalOperaterID; }
            set { _finalOperaterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FinalOperater", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string FinalOperater
        {
            get { return _finalOperater; }
            set { _finalOperater = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsProtect", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsProtect
        {
            get { return _isProtect; }
            set { _isProtect = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FileUploadTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FileUploadTime
        {
            get { return _fileUploadTime; }
            set { _fileUploadTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UploadComputer", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string UploadComputer
        {
            get { return _uploadComputer; }
            set { _uploadComputer = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}