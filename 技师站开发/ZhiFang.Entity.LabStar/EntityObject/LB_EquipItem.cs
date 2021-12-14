using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBEquipItem

    /// <summary>
    /// LBEquipItem object for NHibernate mapped table 'LB_EquipItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBEquipItem", ShortCode = "LBEquipItem", Desc = "")]
    public class LBEquipItem : BaseEntity
    {
        #region Member Variables

        protected int _dispOrder;
        protected int _dispOrderComm;
        protected int _dispOrderQC;
        protected string _compCode;
        protected bool _isReserve;
        protected long? _pItemID;
        protected long? _sectionID;
        protected double _dilutionMultiple;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected LBEquip _lBEquip;
        protected LBItem _lBItem;


        #endregion

        #region Constructors

        public LBEquipItem() { }

        public LBEquipItem(int dispOrder, int dispOrderComm, int dispOrderQC, string compCode, bool isReserve, long pItemID, long sectionID, double dilutionMultiple, bool isUse, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBEquip lBEquip, LBItem lBItem)
        {
            this._dispOrder = dispOrder;
            this._dispOrderComm = dispOrderComm;
            this._dispOrderQC = dispOrderQC;
            this._compCode = compCode;
            this._isReserve = isReserve;
            this._pItemID = pItemID;
            this._sectionID = sectionID;
            this._dilutionMultiple = dilutionMultiple;
            this._isUse = isUse;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBEquip = lBEquip;
            this._lBItem = lBItem;
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
        [DataDesc(CName = "", ShortCode = "DispOrderComm", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrderComm
        {
            get { return _dispOrderComm; }
            set { _dispOrderComm = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrderQC", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrderQC
        {
            get { return _dispOrderQC; }
            set { _dispOrderQC = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CompCode
        {
            get { return _compCode; }
            set { _compCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsReserve", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReserve
        {
            get { return _isReserve; }
            set { _isReserve = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? PItemID
        {
            get { return _pItemID; }
            set { _pItemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SectionID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SectionID
        {
            get { return _sectionID; }
            set { _sectionID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DilutionMultiple", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double DilutionMultiple
        {
            get { return _dilutionMultiple; }
            set { _dilutionMultiple = value; }
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
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
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


        #endregion
    }
    #endregion
}