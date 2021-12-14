using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBEquipResultTH

    /// <summary>
    /// LBEquipResultTH object for NHibernate mapped table 'LB_EquipResultTH'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBEquipResultTH", ShortCode = "LBEquipResultTH", Desc = "")]
    public class LBEquipResultTH : BaseEntity
    {
        #region Member Variables

        protected string _compCode;
        protected bool _isUse;
        protected long? _sampleTypeID;
        protected long? _genderID;
        protected double? _lowAge;
        protected double? _highAge;
        protected DateTime? _bCollectTime;
        protected DateTime? _eCollectTime;
        protected long? _ageUnitID;
        protected string _calcType;
        protected string _sourceValue;
        protected string _reportValue;
        protected string _appValue;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected LBEquip _lBEquip;
        protected LBItem _lBItem;


        #endregion

        #region Constructors

        public LBEquipResultTH() { }

        public LBEquipResultTH(string compCode, bool isUse, long sampleTypeID, long genderID, double lowAge, double highAge, DateTime bCollectTime, DateTime eCollectTime, long ageUnitID, string calcType, string sourceValue, string reportValue, string appValue, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBEquip lBEquip, LBItem lBItem)
        {
            this._compCode = compCode;
            this._isUse = isUse;
            this._sampleTypeID = sampleTypeID;
            this._genderID = genderID;
            this._lowAge = lowAge;
            this._highAge = highAge;
            this._bCollectTime = bCollectTime;
            this._eCollectTime = eCollectTime;
            this._ageUnitID = ageUnitID;
            this._calcType = calcType;
            this._sourceValue = sourceValue;
            this._reportValue = reportValue;
            this._appValue = appValue;
            this._dispOrder = dispOrder;
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
        [DataDesc(CName = "", ShortCode = "CompCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CompCode
        {
            get { return _compCode; }
            set { _compCode = value; }
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
        [DataDesc(CName = "", ShortCode = "SampleTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleTypeID
        {
            get { return _sampleTypeID; }
            set { _sampleTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GenderID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GenderID
        {
            get { return _genderID; }
            set { _genderID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LowAge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? LowAge
        {
            get { return _lowAge; }
            set { _lowAge = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "HighAge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? HighAge
        {
            get { return _highAge; }
            set { _highAge = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BCollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BCollectTime
        {
            get { return _bCollectTime; }
            set { _bCollectTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ECollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ECollectTime
        {
            get { return _eCollectTime; }
            set { _eCollectTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AgeUnitID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? AgeUnitID
        {
            get { return _ageUnitID; }
            set { _ageUnitID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CalcType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CalcType
        {
            get { return _calcType; }
            set { _calcType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SourceValue", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string SourceValue
        {
            get { return _sourceValue; }
            set { _sourceValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportValue", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReportValue
        {
            get { return _reportValue; }
            set { _reportValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AppValue", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string AppValue
        {
            get { return _appValue; }
            set { _appValue = value; }
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