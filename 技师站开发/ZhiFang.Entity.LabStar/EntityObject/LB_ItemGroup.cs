using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBItemGroup

    /// <summary>
    /// LBItemGroup object for NHibernate mapped table 'LB_ItemGroup'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBItemGroup", ShortCode = "LBItemGroup", Desc = "")]
    public class LBItemGroup : BaseEntity
    {
        #region Member Variables

        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected LBItem _lBItem;
        protected LBItem _lBgroup;

        #endregion

        #region Constructors

        public LBItemGroup() { }

        public LBItemGroup(int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem lBItem, LBItem lBGroup)
        {
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBItem = lBItem;
            this._lBgroup = lBGroup;
        }

        #endregion

        #region Public Properties


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
        [DataDesc(CName = "", ShortCode = "Group", Desc = "")]
        public virtual LBItem LBGroup
        {
            get { return _lBgroup; }
            set { _lBgroup = value; }
        }


        #endregion
    }
    #endregion
}