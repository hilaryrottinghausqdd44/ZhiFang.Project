using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model
{
    //枚举
    //Type 文档类型（1、新闻；2、知识库；3、文档）
    //Status 状态（1、待审核；2、已审核；3、发布）	

    public class N_News
    {

        /// <summary>
        /// LabID
        /// </summary>		
        private long _labid;
        
        public long LabID
        {
            get { return _labid; }
            set { _labid = value; }
        }
        /// <summary>
        /// FileID
        /// </summary>		
        private long _fileid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long FileID
        {
            get { return _fileid; }
            set { _fileid = value; }
        }
        /// <summary>
        /// Title
        /// </summary>		
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// No
        /// </summary>		
        private string _no;
        public string No
        {
            get { return _no; }
            set { _no = value; }
        }
        /// <summary>
        /// Content
        /// </summary>		
        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// Type
        /// </summary>		
        private long _type;
        public long Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// ContentType
        /// </summary>		
        private long _contenttype;
        [JsonConverter(typeof(JsonConvertClass))]
        public long ContentType
        {
            get { return _contenttype; }
            set { _contenttype = value; }
        }
        /// <summary>
        /// ContentType
        /// </summary>		
        private string _contenttypename;
        public string ContentTypeName
        {
            get { return _contenttypename; }
            set { _contenttypename = value; }
        }
        /// <summary>
        /// Status
        /// </summary>		
        private int _status;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// StatusName
        /// </summary>		
        private string _statusname;
        public string StatusName
        {
            get { return _statusname; }
            set { _statusname = value; }
        }
        /// <summary>
        /// Keyword
        /// </summary>		
        private string _keyword;
        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }
        /// <summary>
        /// Summary
        /// </summary>		
        private string _summary;
        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }
        /// <summary>
        /// Source
        /// </summary>		
        private string _source;
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
        /// <summary>
        /// VersionNo
        /// </summary>		
        private string _versionno;
        public string VersionNo
        {
            get { return _versionno; }
            set { _versionno = value; }
        }
        /// <summary>
        /// Pagination
        /// </summary>		
        private int _pagination;
        public int Pagination
        {
            get { return _pagination; }
            set { _pagination = value; }
        }
        /// <summary>
        /// ReviseNo
        /// </summary>		
        private string _reviseno;
        public string ReviseNo
        {
            get { return _reviseno; }
            set { _reviseno = value; }
        }
        /// <summary>
        /// RevisorID
        /// </summary>		
        private long _revisorid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long RevisorID
        {
            get { return _revisorid; }
            set { _revisorid = value; }
        }
        /// <summary>
        /// ReviseReason
        /// </summary>		
        private string _revisereason;
        public string ReviseReason
        {
            get { return _revisereason; }
            set { _revisereason = value; }
        }
        /// <summary>
        /// ReviseContent
        /// </summary>		
        private string _revisecontent;
        public string ReviseContent
        {
            get { return _revisecontent; }
            set { _revisecontent = value; }
        }
        /// <summary>
        /// ReviseTime
        /// </summary>		
        private DateTime? _revisetime;
        public DateTime? ReviseTime
        {
            get { return _revisetime; }
            set { _revisetime = value; }
        }
        /// <summary>
        /// BeginTime
        /// </summary>		
        private DateTime? _begintime;
        public DateTime? BeginTime
        {
            get { return _begintime; }
            set { _begintime = value; }
        }
        /// <summary>
        /// EndTime
        /// </summary>		
        private DateTime? _endtime;
        public DateTime? EndTime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }
        /// <summary>
        /// NextExecutorID
        /// </summary>		
        private long _nextexecutorid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long NextExecutorID
        {
            get { return _nextexecutorid; }
            set { _nextexecutorid = value; }
        }
        /// <summary>
        /// Memo
        /// </summary>		
        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        /// <summary>
        /// DispOrder
        /// </summary>		
        private int _disporder;
        public int DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }
        /// <summary>
        /// IsUse
        /// </summary>		
        private int? _isuse=1;
        public int? IsUse
        {
            get { return _isuse; }
            set { _isuse = value; }
        }
        /// <summary>
        /// CreatorID
        /// </summary>		
        private long _creatorid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long CreatorID
        {
            get { return _creatorid; }
            set { _creatorid = value; }
        }
        /// <summary>
        /// 创建者姓名
        /// </summary>		
        private string _creatorname;
        public string CreatorName
        {
            get { return _creatorname; }
            set { _creatorname = value; }
        }
        /// <summary>
        /// DataAddTime
        /// </summary>		
        private DateTime _dataaddtime;
        public DateTime DataAddTime
        {
            get { return _dataaddtime; }
            set { _dataaddtime = value; }
        }
        /// <summary>
        /// 数据修改时间
        /// </summary>		
        private DateTime? _dataupdatetime;
        public DateTime? DataUpdateTime
        {
            get { return _dataupdatetime; }
            set { _dataupdatetime = value; }
        }
        /// <summary>
        /// 时间戳
        /// </summary>		
        private DateTime _datatimestamp;
        public DateTime DataTimeStamp
        {
            get { return _datatimestamp; }
            set { _datatimestamp = value; }
        }
        /// <summary>
        /// 类型树主键ID
        /// </summary>		
        private long _ftypetreeid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long FTypeTreeId
        {
            get { return _ftypetreeid; }
            set { _ftypetreeid = value; }
        }
        /// <summary>
        /// 原始文档Id
        /// </summary>		
        private long _originalfileid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long OriginalFileID
        {
            get { return _originalfileid; }
            set { _originalfileid = value; }
        }
        /// <summary>
        /// 审核人Id
        /// </summary>		
        private long? _checkerid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long? CheckerId
        {
            get { return _checkerid; }
            set { _checkerid = value; }
        }
        /// <summary>
        /// 审核人名称
        /// </summary>		
        private string _checkername;
        public string CheckerName
        {
            get { return _checkername; }
            set { _checkername = value; }
        }
        /// <summary>
        /// 审批人Id
        /// </summary>		
        private long? _approvalid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long? ApprovalId
        {
            get { return _approvalid; }
            set { _approvalid = value; }
        }
        /// <summary>
        /// 审批人名称
        /// </summary>		
        private string _approvalname;
        public string ApprovalName
        {
            get { return _approvalname; }
            set { _approvalname = value; }
        }
        /// <summary>
        /// 发布人Id
        /// </summary>		
        private long? _publisherid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long? PublisherId
        {
            get { return _publisherid; }
            set { _publisherid = value; }
        }
        /// <summary>
        /// 发布人名称
        /// </summary>		
        private string _publishername;
        public string PublisherName
        {
            get { return _publishername; }
            set { _publishername = value; }
        }
        /// <summary>
        /// 起草人Id
        /// </summary>		
        private long _drafterid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long DrafterId
        {
            get { return _drafterid; }
            set { _drafterid = value; }
        }
        /// <summary>
        /// 起草人名称
        /// </summary>		
        private string _draftercname;
        public string DrafterCName
        {
            get { return _draftercname; }
            set { _draftercname = value; }
        }
        /// <summary>
        /// 是否置顶
        /// </summary>		
        private bool _istop=false;
        public bool IsTop
        {
            get { return _istop; }
            set { _istop = value; }
        }
        /// <summary>
        /// 发布后是否可评论
        /// </summary>		
        private bool _isdiscuss=false;
        public bool IsDiscuss
        {
            get { return _isdiscuss; }
            set { _isdiscuss = value; }
        }
        /// <summary>
        /// 总阅读数
        /// </summary>		
        private int? _counts;
        public int? Counts
        {
            get { return _counts; }
            set { _counts = value; }
        }
        /// <summary>
        /// 起草时间
        /// </summary>		
        private DateTime? _drafterdatetime;
        public DateTime? DrafterDateTime
        {
            get { return _drafterdatetime; }
            set { _drafterdatetime = value; }
        }
        /// <summary>
        /// 审核时间
        /// </summary>		
        private DateTime? _checkerdatetime;
        public DateTime? CheckerDateTime
        {
            get { return _checkerdatetime; }
            set { _checkerdatetime = value; }
        }
        /// <summary>
        /// 审批时间
        /// </summary>		
        private DateTime? _approvaldatetime;
        public DateTime? ApprovalDateTime
        {
            get { return _approvaldatetime; }
            set { _approvaldatetime = value; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>		
        private DateTime? _publisherdatetime;
        public DateTime? PublisherDateTime
        {
            get { return _publisherdatetime; }
            set { _publisherdatetime = value; }
        }
        /// <summary>
        /// IsSyncWeiXin
        /// </summary>		
        private bool _issyncweixin;
        public bool IsSyncWeiXin
        {
            get { return _issyncweixin; }
            set { _issyncweixin = value; }
        }
        /// <summary>
        /// WeiXinContent
        /// </summary>		
        private string _weixincontent;
        public string WeiXinContent
        {
            get { return _weixincontent; }
            set { _weixincontent = value; }
        }
        /// <summary>
        /// ThumbnailsPath
        /// </summary>		
        private string _thumbnailspath;
        public string ThumbnailsPath
        {
            get { return _thumbnailspath; }
            set { _thumbnailspath = value; }
        }
        /// <summary>
        /// ThumbnailsMemo
        /// </summary>		
        private string _thumbnailsmemo;
        public string ThumbnailsMemo
        {
            get { return _thumbnailsmemo; }
            set { _thumbnailsmemo = value; }
        }
        /// <summary>
        /// Media_id
        /// </summary>		
        private string _media_id;
        public string Media_id
        {
            get { return _media_id; }
            set { _media_id = value; }
        }
        /// <summary>
        /// Thumb_media_id
        /// </summary>		
        private string _thumb_media_id;
        public string Thumb_media_id
        {
            get { return _thumb_media_id; }
            set { _thumb_media_id = value; }
        }
        /// <summary>
        /// WeiXinTitle
        /// </summary>		
        private string _weixintitle;
        public string WeiXinTitle
        {
            get { return _weixintitle; }
            set { _weixintitle = value; }
        }
        /// <summary>
        /// WeiXinAuthor
        /// </summary>		
        private string _weixinauthor;
        public string WeiXinAuthor
        {
            get { return _weixinauthor; }
            set { _weixinauthor = value; }
        }
        /// <summary>
        /// WeiXinDigest
        /// </summary>		
        private string _weixindigest;
        public string WeiXinDigest
        {
            get { return _weixindigest; }
            set { _weixindigest = value; }
        }
        /// <summary>
        /// WeiXinUrl
        /// </summary>		
        private string _weixinurl;
        public string WeiXinUrl
        {
            get { return _weixinurl; }
            set { _weixinurl = value; }
        }
        /// <summary>
        /// WeiXinContent_source_url
        /// </summary>		
        private string _weixincontent_source_url;
        public string WeiXinContent_source_url
        {
            get { return _weixincontent_source_url; }
            set { _weixincontent_source_url = value; }
        }

        public int ReadCount { get; set; }

        /// <summary>
        /// StartTime
        /// </summary>		
        private string _StartDateTime;
        public string StartDateTime
        {
            get { return _StartDateTime; }
            set { _StartDateTime = value; }
        }
        /// <summary>
        /// EndTime
        /// </summary>		
        private string _EndDateTime;
        public string EndDateTime
        {
            get { return _EndDateTime; }
            set { _EndDateTime = value; }
        }
    }
}

