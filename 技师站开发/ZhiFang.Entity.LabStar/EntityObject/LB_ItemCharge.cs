using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBItemCharge

    /// <summary>
    /// LBItemCharge object for NHibernate mapped table 'LB_ItemCharge'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "LB_ItemCharge", ClassCName = "LBItemCharge", ShortCode = "LBItemCharge", Desc = "LB_ItemCharge")]
    public class LBItemCharge : BaseEntity
    {
        #region Member Variables

        protected long? _chargeTypeID;
        protected long? _sickTypeID;
        protected long? _deptID;
        protected long? _sampleTypeID;
        protected decimal _itemCharge;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected LBItem _lBItem;


        #endregion

        #region Constructors

        public LBItemCharge() { }

        public LBItemCharge(long labID, long chargeTypeID, long sickTypeID, long deptID, long sampleTypeID, decimal itemCharge, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem lBItem)
        {
            this._labID = labID;
            this._chargeTypeID = chargeTypeID;
            this._sickTypeID = sickTypeID;
            this._deptID = deptID;
            this._sampleTypeID = sampleTypeID;
            this._itemCharge = itemCharge;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBItem = lBItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收费类型ID", ShortCode = "ChargeTypeID", Desc = "收费类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ChargeTypeID
        {
            get { return _chargeTypeID; }
            set { _chargeTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "就诊类型ID", ShortCode = "SickTypeID", Desc = "就诊类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SickTypeID
        {
            get { return _sickTypeID; }
            set { _sickTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送检科室ID", ShortCode = "DeptID", Desc = "送检科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本类型", ShortCode = "SampleTypeID", Desc = "样本类型", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleTypeID
        {
            get { return _sampleTypeID; }
            set { _sampleTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目价格", ShortCode = "ItemCharge", Desc = "项目价格", ContextType = SysDic.All, Length = 9)]
        public virtual decimal ItemCharge
        {
            get { return _itemCharge; }
            set { _itemCharge = value; }
        }

        [DataMember]
        [DataDesc(CName = "说明", ShortCode = "Comment", Desc = "说明", ContextType = SysDic.All, Length = 500)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "检验项目", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBItem
        {
            get { return _lBItem; }
            set { _lBItem = value; }
        }


        #endregion
    }
    #endregion
}