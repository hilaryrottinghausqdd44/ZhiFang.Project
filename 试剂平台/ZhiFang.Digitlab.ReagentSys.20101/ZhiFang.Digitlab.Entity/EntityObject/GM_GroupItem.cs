using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region GMGroupItem

    /// <summary>
    /// GMGroupItem object for NHibernate mapped table 'GM_GroupItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "小组项目", ClassCName = "GMGroupItem", ShortCode = "GMGroupItem", Desc = "小组项目")]
    public class GMGroupItem : BaseEntity
    {
        #region Member Variables

        protected DateTime? _dataUpdateTime;
        protected string _defaultReportValue;
        protected GMGroup _gMGroup;
        protected ItemAllItem _itemAllItem;

        #endregion

        #region Constructors

        public GMGroupItem() { }

        public GMGroupItem(long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string defaultReportValue, GMGroup gMGroup, ItemAllItem itemAllItem)
        {
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._defaultReportValue = defaultReportValue;
            this._gMGroup = gMGroup;
            this._itemAllItem = itemAllItem;
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
        [DataDesc(CName = "检验项目默认结果值", ShortCode = "DefaultReportValue", Desc = "检验项目默认结果值", ContextType = SysDic.All, Length = 300)]
        public virtual string DefaultReportValue
        {
            get { return _defaultReportValue; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for DefaultReportValue", value, value.ToString());
                _defaultReportValue = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
        public virtual GMGroup GMGroup
        {
            get { return _gMGroup; }
            set { _gMGroup = value; }
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