using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BMicroItemTestStepGroup

    /// <summary>
    /// BMicroItemTestStepGroup object for NHibernate mapped table 'B_MicroItemTestStepGroup'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物检验步骤组合，为（检验项目+样本类型）这样的检验对象定制一系列与之对应的检验步骤", ClassCName = "BMicroItemTestStepGroup", ShortCode = "BMicroItemTestStepGroup", Desc = "微生物检验步骤组合，为（检验项目+样本类型）这样的检验对象定制一系列与之对应的检验步骤")]
    public class BMicroItemTestStepGroup : BaseEntity
    {
        #region Member Variables

        protected DateTime? _dataUpdateTime;
        protected GMGroup _gMGroup;
        protected GMGroupItem _gMGroupItem;
        protected BSampleType _bSampleType;
        protected ItemAllItem _itemAllItem;
        protected IList<BMicroItemTestStep> _bMicroItemTestStepList;

        #endregion

        #region Constructors

        public BMicroItemTestStepGroup() { }

        public BMicroItemTestStepGroup(long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, GMGroup gMGroup, GMGroupItem gMGroupItem, BSampleType bSampleType, ItemAllItem itemAllItem)
        {
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._gMGroup = gMGroup;
            this._gMGroupItem = gMGroupItem;
            this._bSampleType = bSampleType;
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
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
        public virtual GMGroup GMGroup
        {
            get { return _gMGroup; }
            set { _gMGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组项目", ShortCode = "GMGroupItem", Desc = "小组项目")]
        public virtual GMGroupItem GMGroupItem
        {
            get { return _gMGroupItem; }
            set { _gMGroupItem = value; }
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

        [DataMember]
        [DataDesc(CName = "项目微生物检验步骤关系", ShortCode = "BMicroItemTestStepList", Desc = "项目微生物检验步骤关系")]
        public virtual IList<BMicroItemTestStep> BMicroItemTestStepList
        {
            get
            {
                if (_bMicroItemTestStepList == null)
                {
                    _bMicroItemTestStepList = new List<BMicroItemTestStep>();
                }
                return _bMicroItemTestStepList;
            }
            set { _bMicroItemTestStepList = value; }
        }
        #endregion
    }
    #endregion
}