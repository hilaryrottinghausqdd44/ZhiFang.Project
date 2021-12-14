using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BSampleTypeAndCultureMedium

    /// <summary>
    /// BSampleTypeAndCultureMedium object for NHibernate mapped table 'B_SampleTypeAndCultureMedium'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "样本类型与培养基关系表", ClassCName = "BSampleTypeAndCultureMedium", ShortCode = "BSampleTypeAndCultureMedium", Desc = "样本类型与培养基关系表")]
    public class BSampleTypeAndCultureMedium : BaseEntity
    {
        #region Member Variables

        protected bool _isDefault;
        protected DateTime? _dataUpdateTime;
        protected int _inoculantType;
        protected BCultureMedium _bCultureMedium;
        protected BSampleType _bSampleType;
        protected ItemAllItem _itemAllItem;

        #endregion

        #region Constructors

        public BSampleTypeAndCultureMedium() { }

        public BSampleTypeAndCultureMedium(long labID, bool isDefault, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, int inoculantType, BCultureMedium bCultureMedium, BSampleType bSampleType, ItemAllItem itemAllItem)
        {
            this._labID = labID;
            this._isDefault = isDefault;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._inoculantType = inoculantType;
            this._bCultureMedium = bCultureMedium;
            this._bSampleType = bSampleType;
            this._itemAllItem = itemAllItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "是否为默认培养基", ShortCode = "IsDefault", Desc = "是否为默认培养基", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
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
        [DataDesc(CName = "接种类型", ShortCode = "InoculantType", Desc = "接种类型", ContextType = SysDic.All, Length = 4)]
        public virtual int InoculantType
        {
            get { return _inoculantType; }
            set { _inoculantType = value; }
        }

        [DataMember]
        [DataDesc(CName = "培养基字典表", ShortCode = "BCultureMedium", Desc = "培养基字典表")]
        public virtual BCultureMedium BCultureMedium
        {
            get { return _bCultureMedium; }
            set { _bCultureMedium = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
        public virtual BSampleType BSampleType
        {
            get { return _bSampleType; }
            set { _bSampleType = value; }
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