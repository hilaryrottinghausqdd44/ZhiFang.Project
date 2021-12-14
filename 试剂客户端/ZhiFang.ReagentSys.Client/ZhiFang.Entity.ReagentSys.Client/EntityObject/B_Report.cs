using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region BReport

    /// <summary>
    /// BReport object for NHibernate mapped table 'B_Report'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BReport", ShortCode = "BReport", Desc = "")]
    public class BReport : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected long _typeID;
        protected string _typeName;
        protected DateTime? _statisticsBeginDateTime;
        protected DateTime? _statisticsEndDateTime;
        protected long _bobjectID;
        protected string _businessModuleCode;
        protected long _status;
        protected string _filePath;
        protected string _fileExt;
        protected string _contentType;
        protected double _fileSize;
        protected long? _creatorID;
        protected string _creatorName;
        protected long? _checkerID;
        protected string _checkerName;
        protected long? _publisherID;
        protected string _publisherName;
        protected DateTime? _checkerDateTime;
        protected DateTime? _publisherDateTime;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;

       
        #endregion

        #region Constructors

        public BReport() { }

        public BReport(long labID, string cName, long typeID, string typeName, DateTime statisticsBeginDateTime, DateTime statisticsEndDateTime, long bobjectID, string businessModuleCode, long status, string filePath, string fileExt, string contentType, double fileSize, long creatorID, string creatorName, DateTime dataAddTime, long checkerID, string checkerName, long publisherID, string publisherName, DateTime checkerDateTime, DateTime publisherDateTime, string sName, string shortcode, string pinYinZiTou, int dispOrder, string comment, bool isUse, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cName = cName;
            this._typeID = typeID;
            this._typeName = typeName;
            this._statisticsBeginDateTime = statisticsBeginDateTime;
            this._statisticsEndDateTime = statisticsEndDateTime;
            this._bobjectID = bobjectID;
            this._businessModuleCode = businessModuleCode;
            this._status = status;
            this._filePath = filePath;
            this._fileExt = fileExt;
            this._contentType = contentType;
            this._fileSize = fileSize;
            this._creatorID = creatorID;
            this._creatorName = creatorName;
            this._dataAddTime = dataAddTime;
            this._checkerID = checkerID;
            this._checkerName = checkerName;
            this._publisherID = publisherID;
            this._publisherName = publisherName;
            this._checkerDateTime = checkerDateTime;
            this._publisherDateTime = publisherDateTime;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._dispOrder = dispOrder;
            this._comment = comment;
            this._isUse = isUse;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long TypeID
        {
            get { return _typeID; }
            set { _typeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string TypeName
        {
            get { return _typeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
                _typeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StatisticsBeginDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StatisticsBeginDateTime
        {
            get { return _statisticsBeginDateTime; }
            set { _statisticsBeginDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StatisticsEndDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StatisticsEndDateTime
        {
            get { return _statisticsEndDateTime; }
            set { _statisticsEndDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BobjectID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long BobjectID
        {
            get { return _bobjectID; }
            set { _bobjectID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BusinessModuleCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string BusinessModuleCode
        {
            get { return _businessModuleCode; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for BusinessModuleCode", value, value.ToString());
                _businessModuleCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Status", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FilePath", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string FilePath
        {
            get { return _filePath; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for FilePath", value, value.ToString());
                _filePath = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FileExt", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string FileExt
        {
            get { return _fileExt; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for FileExt", value, value.ToString());
                _fileExt = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ContentType", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ContentType
        {
            get { return _contentType; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ContentType", value, value.ToString());
                _contentType = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FileSize", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreatorID", Desc = "", ContextType = SysDic.All, Length = 8)]
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
                _creatorName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckerID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckerID
        {
            get { return _checkerID; }
            set { _checkerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckerName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckerName
        {
            get { return _checkerName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckerName", value, value.ToString());
                _checkerName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PublisherID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? PublisherID
        {
            get { return _publisherID; }
            set { _publisherID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PublisherName", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckerDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckerDateTime
        {
            get { return _checkerDateTime; }
            set { _checkerDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PublisherDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PublisherDateTime
        {
            get { return _publisherDateTime; }
            set { _publisherDateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
                _shortcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
                _pinYinZiTou = value;
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
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
                _comment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        #endregion
    }
    #endregion
}