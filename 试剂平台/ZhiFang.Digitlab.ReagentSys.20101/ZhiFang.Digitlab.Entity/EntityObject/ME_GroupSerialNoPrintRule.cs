using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEGroupSerialNoPrintRule

    /// <summary>
    /// MEGroupSerialNoPrintRule object for NHibernate mapped table 'ME_GroupSerialNoPrintRule'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "小组条码打印规则表", ClassCName = "MEGroupSerialNoPrintRule", ShortCode = "MEGroupSerialNoPrintRule", Desc = "小组条码打印规则表")]
    public class MEGroupSerialNoPrintRule : BaseEntity
    {
        #region Member Variables

        protected string _includeItems;
        protected int _printNumber;
        protected bool _isUsed;
        protected DateTime? _dataUpdateTime;
        protected bool _isGroupDefaultPrintRule;
        protected bool _isUsedSPRule;
        protected GMGroup _gMGroup;
        protected IList<MEGroupSerialNoPrintRuleDetail> _mEGroupSerialNoPrintRuleDetailList;

        #endregion

        #region Constructors

        public MEGroupSerialNoPrintRule() { }

        public MEGroupSerialNoPrintRule(long labID, string includeItems, int printNumber, bool isUsed, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool isGroupDefaultPrintRule, bool isUsedSPRule, GMGroup gMGroup)
        {
            this._labID = labID;
            this._includeItems = includeItems;
            this._printNumber = printNumber;
            this._isUsed = isUsed;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._isGroupDefaultPrintRule = isGroupDefaultPrintRule;
            this._isUsedSPRule = isUsedSPRule;
            this._gMGroup = gMGroup;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "包含项目", ShortCode = "IncludeItems", Desc = "包含项目", ContextType = SysDic.All, Length = 200)]
        public virtual string IncludeItems
        {
            get { return _includeItems; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for IncludeItems", value, value.ToString());
                _includeItems = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "条码打印张数", ShortCode = "PrintNumber", Desc = "条码打印张数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintNumber
        {
            get { return _printNumber; }
            set { _printNumber = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUsed", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUsed
        {
            get { return _isUsed; }
            set { _isUsed = value; }
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
        [DataDesc(CName = "是否小组默认打印规则", ShortCode = "IsGroupDefaultPrintRule", Desc = "是否小组默认打印规则", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsGroupDefaultPrintRule
        {
            get { return _isGroupDefaultPrintRule; }
            set { _isGroupDefaultPrintRule = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否调用特殊打印规则", ShortCode = "IsUsedSPRule", Desc = "是否调用特殊打印规则", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUsedSPRule
        {
            get { return _isUsedSPRule; }
            set { _isUsedSPRule = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
        public virtual GMGroup GMGroup
        {
            get { return _gMGroup; }
            set { _gMGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组条码打印规则明细表：为当前规则指定包含哪些项目，当条码对应的检验单中包含指定项目时，执行该规则", ShortCode = "MEGroupSerialNoPrintRuleDetailList", Desc = "小组条码打印规则明细表：为当前规则指定包含哪些项目，当条码对应的检验单中包含指定项目时，执行该规则")]
        public virtual IList<MEGroupSerialNoPrintRuleDetail> MEGroupSerialNoPrintRuleDetailList
        {
            get
            {
                if (_mEGroupSerialNoPrintRuleDetailList == null)
                {
                    _mEGroupSerialNoPrintRuleDetailList = new List<MEGroupSerialNoPrintRuleDetail>();
                }
                return _mEGroupSerialNoPrintRuleDetailList;
            }
            set { _mEGroupSerialNoPrintRuleDetailList = value; }
        }


        #endregion
    }
    #endregion
}