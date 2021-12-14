using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEGroupSerialNoPrintRuleDetail

    /// <summary>
    /// MEGroupSerialNoPrintRuleDetail object for NHibernate mapped table 'ME_GroupSerialNoPrintRuleDetail'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "小组条码打印规则明细表：为当前规则指定包含哪些项目，当条码对应的检验单中包含指定项目时，执行该规则", ClassCName = "MEGroupSerialNoPrintRuleDetail", ShortCode = "MEGroupSerialNoPrintRuleDetail", Desc = "小组条码打印规则明细表：为当前规则指定包含哪些项目，当条码对应的检验单中包含指定项目时，执行该规则")]
    public class MEGroupSerialNoPrintRuleDetail : BaseEntity
    {
        #region Member Variables

        protected DateTime? _dataUpdateTime;
        protected ItemAllItem _itemAllItem;
        protected MEGroupSerialNoPrintRule _mEGroupSerialNoPrintRule;

        #endregion

        #region Constructors

        public MEGroupSerialNoPrintRuleDetail() { }

        public MEGroupSerialNoPrintRuleDetail(long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, MEGroupSerialNoPrintRule mEGroupSerialNoPrintRule)
        {
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._itemAllItem = itemAllItem;
            this._mEGroupSerialNoPrintRule = mEGroupSerialNoPrintRule;
        }

        #endregion

        #region Public Properties


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

        [DataMember]
        [DataDesc(CName = "小组条码打印规则表", ShortCode = "MEGroupSerialNoPrintRule", Desc = "小组条码打印规则表")]
        public virtual MEGroupSerialNoPrintRule MEGroupSerialNoPrintRule
        {
            get { return _mEGroupSerialNoPrintRule; }
            set { _mEGroupSerialNoPrintRule = value; }
        }

        #endregion
    }
    #endregion
}