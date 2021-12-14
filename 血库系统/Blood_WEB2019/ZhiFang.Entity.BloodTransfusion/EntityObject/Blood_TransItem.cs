using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodTransItem

    /// <summary>
    /// BloodTransItem object for NHibernate mapped table 'Blood_TransItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodTransItem", ShortCode = "BloodTransItem", Desc = "")]
    public class BloodTransItem : BaseEntity
    {
        #region Member Variables

        protected int _contentTypeID;
        protected string _transItemResult;
        protected double? _numberItemResult;
        protected bool _visible;
        protected int _dispOrder;
        protected BloodTransForm _bloodTransForm;
        protected BloodTransRecordTypeItem _bloodTransRecordTypeItem;
        protected BloodTransRecordType _bloodTransRecordType;
        #endregion

        #region Constructors

        public BloodTransItem() { }

        public BloodTransItem(long labID, int contentTypeID, string transItemResult, double numberItemResult, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodTransForm bloodTransForm, BloodTransRecordTypeItem bloodTransRecordTypeItem, BloodTransRecordType bloodTransRecordType)
        {
            this._labID = labID;
            this._contentTypeID = contentTypeID;
            this._transItemResult = transItemResult;
            this._numberItemResult = numberItemResult;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bloodTransForm = bloodTransForm;
            this._bloodTransRecordTypeItem = bloodTransRecordTypeItem;
            this._bloodTransRecordType = bloodTransRecordType;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "输血记录项分类", ShortCode = "BloodTransRecordType", Desc = "输血记录项分类")]
        public virtual BloodTransRecordType BloodTransRecordType
        {
            get { return _bloodTransRecordType; }
            set { _bloodTransRecordType = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数字型结果值", ShortCode = "NumberItemResult", Desc = "数字型结果值", ContextType = SysDic.All)]
        public virtual double? NumberItemResult
        {
            get { return _numberItemResult; }
            set { _numberItemResult = value; }
        }

        [DataMember]
        [DataDesc(CName = "内容分类", ShortCode = "ContentTypeID", Desc = "内容分类", ContextType = SysDic.All, Length = 4)]
        public virtual int ContentTypeID
        {
            get { return _contentTypeID; }
            set { _contentTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "记录项结果", ShortCode = "TransItemResult", Desc = "记录项结果", ContextType = SysDic.All, Length = 200)]
        public virtual string TransItemResult
        {
            get { return _transItemResult; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for TransItemResult", value, value.ToString());
                _transItemResult = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "过程记录基本信息", ShortCode = "BloodTransForm", Desc = "过程记录基本信息")]
        public virtual BloodTransForm BloodTransForm
        {
            get { return _bloodTransForm; }
            set { _bloodTransForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "过程记录内容项", ShortCode = "BloodTransRecordTypeItem", Desc = "过程记录内容项")]
        public virtual BloodTransRecordTypeItem BloodTransRecordTypeItem
        {
            get { return _bloodTransRecordTypeItem; }
            set { _bloodTransRecordTypeItem = value; }
        }

        #endregion

        #region 自定义属性

        protected int _batchSign;
        /// <summary>
        /// 批量修改录入的数据标志
        /// 1:表示当前选择的发血血袋对应的记录项完全未作过登记,的结果值全部为空;
        /// 2:表示当前选择的发血血袋对应的记录项的结果值部分相同,部分不相同;
        /// 3:表示当前选择的发血血袋对应的记录项的结果值完全一致;
        /// </summary>
        [DataMember]
        [DataDesc(CName = "批量修改录入的数据标志", ShortCode = "BatchSign", Desc = "批量修改录入的数据标志", ContextType = SysDic.All, Length = 4)]
        public virtual int BatchSign
        {
            get { return _batchSign; }
            set { _batchSign = value; }
        }

        #endregion
    }
    #endregion
}