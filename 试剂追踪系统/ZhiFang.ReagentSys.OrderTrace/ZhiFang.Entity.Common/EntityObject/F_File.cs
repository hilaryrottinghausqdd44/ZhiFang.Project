using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Common;

namespace ZhiFang.Entity.Common
{
    #region FFile

    /// <summary>
    /// FFile object for NHibernate mapped table 'F_File'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "文档信息", ClassCName = "FFile", ShortCode = "FFile", Desc = "文档信息")]
    public class FFile : BaseEntity
    {
        #region Member Variables

        protected string _title;
        protected string _no;
        protected string _content;
        protected int _type;
        protected int _status;
        protected string _keyword;
        protected string _summary;
        protected string _source;
        protected string _versionNo;
        protected int _pagination;
        protected string _reviseNo;
        protected string _reviseReason;
        protected string _reviseContent;
        protected DateTime? _reviseTime;
        protected DateTime? _beginTime;
        protected DateTime? _endTime;
        protected string _memo;
        protected int _dispOrder;
        protected int _isUse;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
        protected long? _originalFileID;
        protected long? _checkerId;
        protected string _checkerName;

        protected long? _drafterId;
        protected string _drafterCName;
        protected long? _approvalId;
        protected string _approvalName;
        protected long? _publisherId;
        protected string _publisherName;
        protected bool _isTop;
        protected bool _isDiscuss;
        protected int _counts;
        protected bool? _isSyncWeiXin;
        protected string _weiXinContent;
        protected string _thumbnailsPath;
        protected string _thumbnailsMemo;

        protected string _Media_id;
        protected string _Thumb_media_id;
        protected string _WeiXinTitle;
        protected string _WeiXinAuthor;
        protected string _WeiXinDigest;
        protected string _WeiXinUrl;
        protected string _WeiXinContent_source_url;

        protected BDictTree _bDictTree;
        protected HREmployee _creator;
        protected HREmployee _nextExecutor;
        protected HREmployee _revisor;
        protected BDict _contentType;
        protected IList<FFileAttachment> _fFileAttachmentList;
        protected IList<FFileCopyUser> _fFileCopyUserList;
        protected IList<FFileInteraction> _fFileInteractionList;
        protected IList<FFileOperation> _fFileOperationList;
        protected IList<FFileReadingLog> _fFileReadingLogList;
        protected IList<FFileReadingUser> _fFileReadingUserList;


        #endregion
        #region 添加自定义字段
        protected DateTime? _drafterDateTime;
        protected DateTime? _checkerDateTime;
        protected DateTime? _approvalDateTime;
        protected DateTime? _publisherDateTime;
        #endregion

        #region 添加自定义属性

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "起草时间", ShortCode = "DrafterDateTime", Desc = "起草时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DrafterDateTime
        {
            get { return _drafterDateTime; }
            set { _drafterDateTime = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "CheckerDateTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckerDateTime
        {
            get { return _checkerDateTime; }
            set { _checkerDateTime = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批时间", ShortCode = "ApprovalDateTime", Desc = "审批时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApprovalDateTime
        {
            get { return _approvalDateTime; }
            set { _approvalDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发布时间", ShortCode = "PublisherDateTime", Desc = "发布时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PublisherDateTime
        {
            get { return _publisherDateTime; }
            set { _publisherDateTime = value; }
        }
        #endregion

        #region Constructors

        public FFile() { }

        public FFile(long labID, string title, string no, string content, int type, int status, string keyword, string summary, string source, string versionNo, int pagination, string reviseNo, string reviseReason, string reviseContent, DateTime reviseTime, DateTime beginTime, DateTime endTime, string memo, int dispOrder, int isUse, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long originalFileID, long checkerId, string checkerName, long approvalId, string approvalName, long publisherId, string publisherName, long drafterId, string drafterCName, bool isTop, bool isDiscuss, int counts, DateTime drafterDateTime, DateTime checkerDateTime, DateTime approvalDateTime, DateTime publisherDateTime, bool isSyncWeiXin, string weiXinContent, string thumbnailsPath, string thumbnailsMemo, BDictTree bDictTree, HREmployee creator, HREmployee nextExecutor, HREmployee revisor, BDict contentType)
        {
            this._labID = labID;
            this._title = title;
            this._no = no;
            this._content = content;
            this._type = type;
            this._status = status;
            this._keyword = keyword;
            this._summary = summary;
            this._source = source;
            this._versionNo = versionNo;
            this._pagination = pagination;
            this._reviseNo = reviseNo;
            this._reviseReason = reviseReason;
            this._reviseContent = reviseContent;
            this._reviseTime = reviseTime;
            this._beginTime = beginTime;
            this._endTime = endTime;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._creatorName = creatorName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._originalFileID = originalFileID;
            this._checkerId = checkerId;
            this._checkerName = checkerName;
            this._approvalId = approvalId;
            this._approvalName = approvalName;
            this._publisherId = publisherId;
            this._publisherName = publisherName;
            this._drafterId = drafterId;
            this._drafterCName = drafterCName;
            this._isTop = isTop;
            this._isDiscuss = isDiscuss;
            this._counts = counts;
            this._drafterDateTime = drafterDateTime;
            this._checkerDateTime = checkerDateTime;
            this._approvalDateTime = approvalDateTime;
            this._publisherDateTime = publisherDateTime;
            this._isSyncWeiXin = isSyncWeiXin;
            this._weiXinContent = weiXinContent;
            this._thumbnailsPath = thumbnailsPath;
            this._thumbnailsMemo = thumbnailsMemo;
            this._bDictTree = bDictTree;
            this._creator = creator;
            this._nextExecutor = nextExecutor;
            this._revisor = revisor;
            this._contentType = contentType;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "标题", ShortCode = "Title", Desc = "标题", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "编号", ShortCode = "No", Desc = "编号", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "内容", ShortCode = "Content", Desc = "内容", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Content
        {
            get { return _content; }
            set
            {
                if (value != null && value.Length > 10240000)
                    throw new ArgumentOutOfRangeException("内容长度超过限制,请传附件!", value, value.ToString());
                _content = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Type", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Status", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "关键字", ShortCode = "Keyword", Desc = "", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "文摘", ShortCode = "Summary", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Summary
        {
            get { return _summary; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Summary", value, value.ToString());
                _summary = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "来源", ShortCode = "Source", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Source
        {
            get { return _source; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Source", value, value.ToString());
                _source = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "VersionNo", Desc = "", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "", ShortCode = "Pagination", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Pagination
        {
            get { return _pagination; }
            set { _pagination = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReviseNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ReviseNo
        {
            get { return _reviseNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReviseNo", value, value.ToString());
                _reviseNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReviseReason", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ReviseReason
        {
            get { return _reviseReason; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ReviseReason", value, value.ToString());
                _reviseReason = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReviseContent", Desc = "", ContextType = SysDic.All, Length = 900000)]
        public virtual string ReviseContent
        {
            get { return _reviseContent; }
            set
            {
                _reviseContent = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReviseTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReviseTime
        {
            get { return _reviseTime; }
            set { _reviseTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BeginTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EndTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
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
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否置顶", ShortCode = "IsTop", Desc = "是否置顶", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsTop
        {
            get { return _isTop; }
            set { _isTop = value; }
        }

        [DataMember]
        [DataDesc(CName = "发布后是否可评论", ShortCode = "IsDiscuss", Desc = "发布后是否可评论", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDiscuss
        {
            get { return _isDiscuss; }
            set { _isDiscuss = value; }
        }

        [DataMember]
        [DataDesc(CName = "总阅读数", ShortCode = "Counts", Desc = "总阅读数", ContextType = SysDic.All, Length = 5)]
        public virtual int Counts
        {
            get { return _counts; }
            set { _counts = value; }
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

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "原始文档Id", ShortCode = "OriginalFileID", Desc = "原始文档Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? OriginalFileID
        {
            get { return _originalFileID; }
            set { _originalFileID = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "起草人Id", ShortCode = "DrafterId", Desc = "起草人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? DrafterId
        {
            get { return _drafterId; }
            set { _drafterId = value; }
        }

        [DataMember]
        [DataDesc(CName = "起草人", ShortCode = "DrafterCName", Desc = "起草人", ContextType = SysDic.All, Length = 50)]
        public virtual string DrafterCName
        {
            get { return _drafterCName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DrafterCName", value, value.ToString());
                _drafterCName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人Id", ShortCode = "CheckerId", Desc = "审核人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckerId
        {
            get { return _checkerId; }
            set { _checkerId = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人名称", ShortCode = "CheckerName", Desc = "审核人名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "审批人Id", ShortCode = "ApprovalId", Desc = "审批人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApprovalId
        {
            get { return _approvalId; }
            set { _approvalId = value; }
        }

        [DataMember]
        [DataDesc(CName = "审批人名称", ShortCode = "ApprovalName", Desc = "审批人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ApprovalName
        {
            get { return _approvalName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ApprovalName", value, value.ToString());
                _approvalName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发布人Id", ShortCode = "PublisherId", Desc = "发布人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? PublisherId
        {
            get { return _publisherId; }
            set { _publisherId = value; }
        }

        [DataMember]
        [DataDesc(CName = "发布人名称", ShortCode = "PublisherName", Desc = "发布人名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "是否同步到微信服务器", ShortCode = "IsSyncWeiXin", Desc = "是否同步到微信服务器", ContextType = SysDic.All, Length = 1)]
        public virtual bool? IsSyncWeiXin
        {
            get
            {
                if (!_isSyncWeiXin.HasValue)
                    _isSyncWeiXin = false;
                return _isSyncWeiXin;
            }
            set
            {
                if (value.HasValue)
                {
                    _isSyncWeiXin = value;
                }
                else
                {
                    _isSyncWeiXin = false;
                }
            }
        }
        [DataMember]
        [DataDesc(CName = "微信内容", ShortCode = "WeiXinContent", Desc = "微信内容", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string WeiXinContent
        {
            get { return _weiXinContent; }
            set
            {
                if (value != null && value.Length > 10240000)
                    throw new ArgumentOutOfRangeException("Invalid value for WeiXinContent", value, value.ToString());
                _weiXinContent = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "新闻缩略图上传保存路径", ShortCode = "ThumbnailsPath", Desc = "新闻缩略图上传保存路径", ContextType = SysDic.All, Length = 100)]
        public virtual string ThumbnailsPath
        {
            get { return _thumbnailsPath; }
            set
            {              
                _thumbnailsPath = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "新闻缩略图描述", ShortCode = "ThumbnailsMemo", Desc = "新闻缩略图描述", ContextType = SysDic.All, Length = 500)]
        public virtual string ThumbnailsMemo
        {
            get { return _thumbnailsMemo; }
            set
            {               
                _thumbnailsMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信MEDIAID", ShortCode = "Mediaid", Desc = "微信MEDIAID", ContextType = SysDic.All, Length = 500)]
        public virtual string Mediaid
        {
            get { return _Media_id; }
            set
            {
                _Media_id = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信缩略图Thumbmediaid", ShortCode = "Thumbmediaid", Desc = "微信缩略图Thumbmediaid", ContextType = SysDic.All, Length = 500)]
        public virtual string Thumbmediaid
        {
            get { return _Thumb_media_id; }
            set
            {
                _Thumb_media_id = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信Title", ShortCode = "WeiXinTitle", Desc = "微信Title", ContextType = SysDic.All, Length = 500)]
        public virtual string WeiXinTitle
        {
            get { return _WeiXinTitle; }
            set
            {
                _WeiXinTitle = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信Author", ShortCode = "WeiXinAuthor", Desc = "微信Author", ContextType = SysDic.All, Length = 500)]
        public virtual string WeiXinAuthor
        {
            get { return _WeiXinAuthor; }
            set
            {
                _WeiXinAuthor = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信Digest", ShortCode = "WeiXinDigest", Desc = "微信Digest", ContextType = SysDic.All, Length = 500)]
        public virtual string WeiXinDigest
        {
            get { return _WeiXinDigest; }
            set
            {
                _WeiXinDigest = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信Url", ShortCode = "WeiXinUrl", Desc = "微信Url", ContextType = SysDic.All, Length = 500)]
        public virtual string WeiXinUrl
        {
            get { return _WeiXinUrl; }
            set
            {
                _WeiXinUrl = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信Contentsourceurl", ShortCode = "WeiXinContentsourceurl", Desc = "微信Contentsourceurl", ContextType = SysDic.All, Length = 500)]
        public virtual string WeiXinContentsourceurl
        {
            get { return _WeiXinContent_source_url; }
            set
            {
                _WeiXinContent_source_url = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "类型树", ShortCode = "BDictTree", Desc = "类型树")]
        public virtual BDictTree BDictTree
        {
            get { return _bDictTree; }
            set { _bDictTree = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建人", ShortCode = "Creator", Desc = "创建人")]
        public virtual HREmployee Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        [DataMember]
        [DataDesc(CName = "下一执行者", ShortCode = "NextExecutor", Desc = "下一执行者")]
        public virtual HREmployee NextExecutor
        {
            get { return _nextExecutor; }
            set { _nextExecutor = value; }
        }

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "Revisor", Desc = "员工")]
        public virtual HREmployee Revisor
        {
            get { return _revisor; }
            set { _revisor = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "ContentType", Desc = "字典表")]
        public virtual BDict ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档附件表", ShortCode = "FFileAttachmentList", Desc = "文档附件表")]
        public virtual IList<FFileAttachment> FFileAttachmentList
        {
            get
            {
                if (_fFileAttachmentList == null)
                {
                    _fFileAttachmentList = new List<FFileAttachment>();
                }
                return _fFileAttachmentList;
            }
            set { _fFileAttachmentList = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档抄送对象表", ShortCode = "FFileCopyUserList", Desc = "文档抄送对象表")]
        public virtual IList<FFileCopyUser> FFileCopyUserList
        {
            get
            {
                if (_fFileCopyUserList == null)
                {
                    _fFileCopyUserList = new List<FFileCopyUser>();
                }
                return _fFileCopyUserList;
            }
            set { _fFileCopyUserList = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档交流表（不附带附件）", ShortCode = "FFileInteractionList", Desc = "文档交流表（不附带附件）")]
        public virtual IList<FFileInteraction> FFileInteractionList
        {
            get
            {
                if (_fFileInteractionList == null)
                {
                    _fFileInteractionList = new List<FFileInteraction>();
                }
                return _fFileInteractionList;
            }
            set { _fFileInteractionList = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档操作记录表", ShortCode = "FFileOperationList", Desc = "文档操作记录表")]
        public virtual IList<FFileOperation> FFileOperationList
        {
            get
            {
                if (_fFileOperationList == null)
                {
                    _fFileOperationList = new List<FFileOperation>();
                }
                return _fFileOperationList;
            }
            set { _fFileOperationList = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档阅读记录表", ShortCode = "FFileReadingLogList", Desc = "文档阅读记录表")]
        public virtual IList<FFileReadingLog> FFileReadingLogList
        {
            get
            {
                if (_fFileReadingLogList == null)
                {
                    _fFileReadingLogList = new List<FFileReadingLog>();
                }
                return _fFileReadingLogList;
            }
            set { _fFileReadingLogList = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档阅读对象表", ShortCode = "FFileReadingUserList", Desc = "文档阅读对象表")]
        public virtual IList<FFileReadingUser> FFileReadingUserList
        {
            get
            {
                if (_fFileReadingUserList == null)
                {
                    _fFileReadingUserList = new List<FFileReadingUser>();
                }
                return _fFileReadingUserList;
            }
            set { _fFileReadingUserList = value; }
        }


        #endregion
    }
    #endregion
}