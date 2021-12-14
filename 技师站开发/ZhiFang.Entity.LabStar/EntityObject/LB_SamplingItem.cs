using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSamplingItem

    /// <summary>
    /// LBSamplingItem object for NHibernate mapped table 'LB_SamplingItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "采样组项目", ClassCName = "LBSamplingItem", ShortCode = "LBSamplingItem", Desc = "采样组项目")]
    public class LBSamplingItem : BaseEntity
    {
        #region Member Variables

        protected bool _isDefault;
        protected long? _mustItemID;
        protected int _virtualItemNo;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected LBItem _lBItem;
        protected LBSamplingGroup _lBSamplingGroup;


        #endregion

        #region Constructors

        public LBSamplingItem() { }

        public LBSamplingItem(bool isDefault, long? mustItemID, int virtualItemNo, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem lBItem, LBSamplingGroup lBSamplingGroup)
        {
            this._isDefault = isDefault;
            this._mustItemID = mustItemID;
            this._virtualItemNo = virtualItemNo;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBItem = lBItem;
            this._lBSamplingGroup = lBSamplingGroup;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDefault", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MustItem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual long? MustItemID
        {
            get { return _mustItemID; }
            set { _mustItemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "VirtualItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int VirtualItemNo
        {
            get { return _virtualItemNo; }
            set { _virtualItemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBItem
        {
            get { return _lBItem; }
            set { _lBItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样组", ShortCode = "LBSamplingGroup", Desc = "采样组")]
        public virtual LBSamplingGroup LBSamplingGroup
        {
            get { return _lBSamplingGroup; }
            set { _lBSamplingGroup = value; }
        }


        #endregion
    }
    #endregion
}