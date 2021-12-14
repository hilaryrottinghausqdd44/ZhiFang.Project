using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region ItemRefeRange

    /// <summary>
    /// ItemRefeRange object for NHibernate mapped table 'Item_RefeRange'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ItemRefeRange", ShortCode = "ItemRefeRange", Desc = "")]
    public class ItemRefeRange : BaseEntity
    {
        #region Member Variables

        protected string _conditionName;
        protected int _iIndex;
        protected double? _lowAge;
        protected double? _highAge;
        protected DateTime? _bCollectTime;
        protected DateTime? _eCollectTime;
        protected bool _isDefault;
        protected double? _lowValue;
        protected double? _highValue;
        protected string _refRange;
        protected double? _abLow;
        protected double? _abHigh;
        protected DateTime? _dataUpdateTime;
        protected BAgeUnit _bAgeUnit;
        protected BSampleType _bSampleType;
        protected BSex _bSex;
        protected EPBEquip _ePBEquip;
        protected GMGroup _gMGroup;
        protected HRDept _hRDept;
        protected ItemAllItem _itemAllItem;
        protected int _manualSort;

        #endregion

        #region Constructors

        public ItemRefeRange() { }

        public ItemRefeRange(long labID, string conditionName, int iIndex, double? lowAge, double? highAge, DateTime bCollectTime, DateTime eCollectTime, bool isDefault, double? lowValue, double? highValue, string refRange, double? abLow, double? abHigh, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BAgeUnit bAgeUnit, BSampleType bSampleType, BSex bSex, EPBEquip ePBEquip, GMGroup gMGroup, HRDept hRDept, ItemAllItem itemAllItem)
        {
            this._labID = labID;
            this._conditionName = conditionName;
            this._iIndex = iIndex;
            this._lowAge = lowAge;
            this._highAge = highAge;
            this._bCollectTime = bCollectTime;
            this._eCollectTime = eCollectTime;
            this._isDefault = isDefault;
            this._lowValue = lowValue;
            this._highValue = highValue;
            this._refRange = refRange;
            this._abLow = abLow;
            this._abHigh = abHigh;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bAgeUnit = bAgeUnit;
            this._bSampleType = bSampleType;
            this._bSex = bSex;
            this._ePBEquip = ePBEquip;
            this._gMGroup = gMGroup;
            this._hRDept = hRDept;
            this._itemAllItem = itemAllItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "条件说明", ShortCode = "ConditionName", Desc = "条件说明", ContextType = SysDic.All, Length = 100)]
        public virtual string ConditionName
        {
            get { return _conditionName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ConditionName", value, value.ToString());
                _conditionName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "判定次序", ShortCode = "IIndex", Desc = "判定次序", ContextType = SysDic.All, Length = 4)]
        public virtual int IIndex
        {
            get { return _iIndex; }
            set { _iIndex = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "年龄低限", ShortCode = "LowAge", Desc = "年龄低限", ContextType = SysDic.All, Length = 8)]
        public virtual double? LowAge
        {
            get { return _lowAge; }
            set { _lowAge = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "年龄高限", ShortCode = "HighAge", Desc = "年龄高限", ContextType = SysDic.All, Length = 8)]
        public virtual double? HighAge
        {
            get { return _highAge; }
            set { _highAge = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BCollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BCollectTime
        {
            get { return _bCollectTime; }
            set { _bCollectTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ECollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ECollectTime
        {
            get { return _eCollectTime; }
            set { _eCollectTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "缺省", ShortCode = "IsDefault", Desc = "缺省", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
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
        [DataDesc(CName = "参考范围描述", ShortCode = "RefRange", Desc = "参考范围描述", ContextType = SysDic.All, Length = 400)]
        public virtual string RefRange
        {
            get { return _refRange; }
            set
            {
                _refRange = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "异常低限", ShortCode = "AbLow", Desc = "异常低限", ContextType = SysDic.All, Length = 8)]
        public virtual double? AbLow
        {
            get { return _abLow; }
            set { _abLow = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "异常高限", ShortCode = "AbHigh", Desc = "异常高限", ContextType = SysDic.All, Length = 8)]
        public virtual double? AbHigh
        {
            get { return _abHigh; }
            set { _abHigh = value; }
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
        [DataDesc(CName = "手动排序", ShortCode = "ManualSort", Desc = "手动排序", ContextType = SysDic.All, Length = 4)]
        public virtual int ManualSort
        {
            get { return _manualSort; }
            set { _manualSort = value; }
        }

        [DataMember]
        [DataDesc(CName = "年龄单位", ShortCode = "BAgeUnit", Desc = "年龄单位")]
        public virtual BAgeUnit BAgeUnit
        {
            get { return _bAgeUnit; }
            set { _bAgeUnit = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
        public virtual BSampleType BSampleType
        {
            get { return _bSampleType; }
            set { _bSampleType = value; }
        }

        [DataMember]
        [DataDesc(CName = "性别", ShortCode = "BSex", Desc = "性别")]
        public virtual BSex BSex
        {
            get { return _bSex; }
            set { _bSex = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器表", ShortCode = "EPBEquip", Desc = "仪器表")]
        public virtual EPBEquip EPBEquip
        {
            get { return _ePBEquip; }
            set { _ePBEquip = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
        public virtual GMGroup GMGroup
        {
            get { return _gMGroup; }
            set { _gMGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "部门", ShortCode = "HRDept", Desc = "部门")]
        public virtual HRDept HRDept
        {
            get { return _hRDept; }
            set { _hRDept = value; }
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