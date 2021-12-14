using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSectionItem

    /// <summary>
    /// LBSectionItem object for NHibernate mapped table 'LB_SectionItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBSectionItem", ShortCode = "LBSectionItem", Desc = "")]
    public class LBSectionItem : BaseEntity
    {
        #region Member Variables

        protected DateTime? _dataUpdateTime;
        protected bool _isDefult;
        protected string _defultValue;
        protected long? _groupItemID;
        protected long? _equipID;
        protected bool _isUse;
        protected int _dispOrder;
        protected LBSection _lBSection;
        protected LBItem _lBItem;


        #endregion

        #region Constructors

        public LBSectionItem() { }

        public LBSectionItem(DateTime dataAddTime, DateTime dataUpdateTime, bool isDefult, string defultValue, long groupItemID, long equipID, long labID, byte[] dataTimeStamp, bool isUse, int dispOrder, LBSection lBSection, LBItem lBItem)
        {
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._isDefult = isDefult;
            this._defultValue = defultValue;
            this._groupItemID = groupItemID;
            this._equipID = equipID;
            this._labID = labID;
            this._dataTimeStamp = dataTimeStamp;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._lBSection = lBSection;
            this._lBItem = lBItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDefult", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefult
        {
            get { return _isDefult; }
            set { _isDefult = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DefultValue", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DefultValue
        {
            get { return _defultValue; }
            set { _defultValue = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GroupItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GroupItemID
        {
            get { return _groupItemID; }
            set { _groupItemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EquipID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? EquipID
        {
            get { return _equipID; }
            set { _equipID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBSection", Desc = "")]
        public virtual LBSection LBSection
        {
            get { return _lBSection; }
            set { _lBSection = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBItem
        {
            get { return _lBItem; }
            set { _lBItem = value; }
        }


        #endregion
    }
    #endregion
}