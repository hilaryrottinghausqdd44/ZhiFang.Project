using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region ItemHisComp

    /// <summary>
    /// ItemHisComp object for NHibernate mapped table 'Item_HisComp'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "项目对比", ClassCName = "ItemHisComp", ShortCode = "ItemHisComp", Desc = "项目对比")]
    public class ItemHisComp : BaseEntity
    {
        #region Member Variables
        protected int _iIndex;
        protected double? _lowValue;
        protected double? _highValue;
        protected int _compType;
        protected double? _warningLimit;
        protected double? _outLimit;
        protected DateTime? _dataUpdateTime;
        protected ItemAllItem _itemAllItem;
        #endregion

        #region Constructors

        public ItemHisComp() { }

        public ItemHisComp(long labID, int iIndex, double? lowValue, double? highValue, int compType, double? warningLimit, double? outLimit, DateTime? dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem)
        {
            this._labID = labID;
            this._iIndex = iIndex;
            this._lowValue = lowValue;
            this._highValue = highValue;
            this._compType = compType;
            this._warningLimit = warningLimit;
            this._outLimit = outLimit;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._itemAllItem = itemAllItem;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "判定次序", ShortCode = "IIndex", Desc = "判定次序", ContextType = SysDic.All, Length = 4)]
        public virtual int IIndex
        {
            get { return _iIndex; }
            set { _iIndex = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "范围低限", ShortCode = "LowValue", Desc = "范围低限", ContextType = SysDic.All, Length = 8)]
        public virtual double? LowValue
        {
            get { return _lowValue; }
            set { _lowValue = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "范围高限", ShortCode = "HighValue", Desc = "范围高限", ContextType = SysDic.All, Length = 8)]
        public virtual double? HighValue
        {
            get { return _highValue; }
            set { _highValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "比较类型1值对比2百分比对比", ShortCode = "CompType", Desc = "比较类型1值对比2百分比对比", ContextType = SysDic.All, Length = 4)]
        public virtual int CompType
        {
            get { return _compType; }
            set { _compType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "偏差警告限", ShortCode = "WarningLimit", Desc = "偏差警告限", ContextType = SysDic.All, Length = 8)]
        public virtual double? WarningLimit
        {
            get { return _warningLimit; }
            set { _warningLimit = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "偏差异常限", ShortCode = "OutLimit", Desc = "偏差异常限", ContextType = SysDic.All, Length = 8)]
        public virtual double? OutLimit
        {
            get { return _outLimit; }
            set { _outLimit = value; }
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
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
        public virtual ItemAllItem ItemAllItem
        {
            get { return _itemAllItem; }
            set { _itemAllItem = value; }
        }
        #endregion
    }
    #endregion
}