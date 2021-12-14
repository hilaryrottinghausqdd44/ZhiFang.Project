using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "医生开单的特推组套项目（区域项目、实验室项目）", ClassCName = "OSRecommendationItemProductVO", ShortCode = "OSRecommendationItemProductVO", Desc = "医生开单的特推组套项目（区域项目、实验室项目）")]
    public class OSRecommendationItemProductVO
    {
        #region Member Variables
        protected long _id;
        protected long _areaID;
        protected long? _itemID;
        protected string _ItemNo;
        protected string _cName;
        protected string _sName;
        protected string _shortcode;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected int _status;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
        protected long _checkerID;
        protected string _checkerName;
        protected long _approvalID;
        protected string _approvalName;
        protected long _publisherID;
        protected string _publisherName;
        protected long _drafterId;
        protected string _drafterCName;
        protected long _offShelveManID;
        protected string _offShelveManName;
        protected bool _isTop;
        protected DateTime? _drafterDateTime;
        protected DateTime? _checkerDateTime;
        protected DateTime? _startDateTime;
        protected DateTime? _endDateTime;
        protected DateTime? _approvalDateTime;
        protected DateTime? _publisherDateTime;
        protected int _counts;
        protected bool _isDiscuss;
        protected double _marketPrice;
        protected double _greatMasterPrice;
        protected double _discountPrice;
        protected double _discount;
        protected string _image;
        protected double? _BonusPercent;

        #endregion

        #region Constructors

        public OSRecommendationItemProductVO() { }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id
        {
            get
            {
                if (_id <= 0)
                    _id = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong();
                return _id;
            }
            set { _id = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AreaID
        {
            get { return _areaID; }
            set { _areaID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目ID", ShortCode = "ItemID", Desc = "项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目No", ShortCode = "ItemNo", Desc = "项目No", ContextType = SysDic.All, Length = 8)]
        public virtual string ItemNo
        {
            get { return _ItemNo; }
            set { _ItemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
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
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
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
        [DataDesc(CName = "", ShortCode = "Status", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
        {
            get { return _status; }
            set { _status = value; }
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
                _creatorName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人Id", ShortCode = "CheckerID", Desc = "审核人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long CheckerID
        {
            get { return _checkerID; }
            set { _checkerID = value; }
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
        [DataDesc(CName = "审批人Id", ShortCode = "ApprovalID", Desc = "审批人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long ApprovalID
        {
            get { return _approvalID; }
            set { _approvalID = value; }
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
        [DataDesc(CName = "发布人Id", ShortCode = "PublisherID", Desc = "发布人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long PublisherID
        {
            get { return _publisherID; }
            set { _publisherID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "起草人ID", ShortCode = "DrafterId", Desc = "起草人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long DrafterId
        {
            get { return _drafterId; }
            set { _drafterId = value; }
        }

        [DataMember]
        [DataDesc(CName = "起草人姓名", ShortCode = "DrafterCName", Desc = "起草人姓名", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "下架人ID", ShortCode = "OffShelveManID", Desc = "下架人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long OffShelveManID
        {
            get { return _offShelveManID; }
            set { _offShelveManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "下架人名称", ShortCode = "OffShelveManName", Desc = "下架人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string OffShelveManName
        {
            get { return _offShelveManName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OffShelveManName", value, value.ToString());
                _offShelveManName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否置顶", ShortCode = "IsTop", Desc = "是否置顶", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsTop
        {
            get { return _isTop; }
            set { _isTop = value; }
        }

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
        [DataDesc(CName = "开始时间", ShortCode = "StartDateTime", Desc = "开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结束时间", ShortCode = "EndDateTime", Desc = "结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
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
        [DataDesc(CName = "市场价格", ShortCode = "MarketPrice", Desc = "市场价格", ContextType = SysDic.All, Length = 8)]
        public virtual double MarketPrice
        {
            get { return _marketPrice; }
            set { _marketPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "大家价格", ShortCode = "GreatMasterPrice", Desc = "大家价格", ContextType = SysDic.All, Length = 8)]
        public virtual double GreatMasterPrice
        {
            get { return _greatMasterPrice; }
            set { _greatMasterPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "折扣价格", ShortCode = "DiscountPrice", Desc = "折扣价格", ContextType = SysDic.All, Length = 8)]
        public virtual double DiscountPrice
        {
            get { return _discountPrice; }
            set { _discountPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "折扣率", ShortCode = "Discount", Desc = "折扣率", ContextType = SysDic.All, Length = 8)]
        public virtual double Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        [DataMember]
        [DataDesc(CName = "图片", ShortCode = "Image", Desc = "图片", ContextType = SysDic.All, Length = 100)]
        public virtual string Image
        {
            get { return _image; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Image", value, value.ToString());
                _image = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "咨询费比率", ShortCode = "BonusPercent", Desc = "咨询费比率", ContextType = SysDic.All, Length = 50)]
        public virtual Double? BonusPercent
        {
            get { return _BonusPercent; }
            set
            {
                _BonusPercent = value;
            }
        }

        #endregion
    }
}
