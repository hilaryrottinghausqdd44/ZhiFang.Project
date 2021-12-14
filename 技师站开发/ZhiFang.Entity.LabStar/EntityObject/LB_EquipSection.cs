using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBEquipSection

    /// <summary>
    /// LBEquipSection object for NHibernate mapped table 'LB_EquipSection'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "仪器特殊对应小组", ClassCName = "LBEquipSection", ShortCode = "LBEquipSection", Desc = "仪器特殊对应小组")]
    public class LBEquipSection : BaseEntity
    {
        #region Member Variables

        protected string _compValue1;
        protected string _compValue2;
        protected string _sampleNoCode;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected LBEquip _lBEquip;
        protected LBItem _lBItem;
        protected LBSection _lBSection;


        #endregion

        #region Constructors

        public LBEquipSection() { }

        public LBEquipSection(long labID, string compValue1, string compValue2, string sampleNoCode, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBEquip lBEquip, LBItem lBItem, LBSection lBSection)
        {
            this._labID = labID;
            this._compValue1 = compValue1;
            this._compValue2 = compValue2;
            this._sampleNoCode = sampleNoCode;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBEquip = lBEquip;
            this._lBItem = lBItem;
            this._lBSection = lBSection;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompValue1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CompValue1
        {
            get { return _compValue1; }
            set { _compValue1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompValue2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CompValue2
        {
            get { return _compValue2; }
            set { _compValue2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本号标识", ShortCode = "SampleNoCode", Desc = "样本号标识", ContextType = SysDic.All, Length = 200)]
        public virtual string SampleNoCode
        {
            get { return _sampleNoCode; }
            set { _sampleNoCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
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
        [DataDesc(CName = "", ShortCode = "LBEquip", Desc = "")]
        public virtual LBEquip LBEquip
        {
            get { return _lBEquip; }
            set { _lBEquip = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBItem
        {
            get { return _lBItem; }
            set { _lBItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBSection", Desc = "")]
        public virtual LBSection LBSection
        {
            get { return _lBSection; }
            set { _lBSection = value; }
        }


        #endregion
    }
    #endregion
}