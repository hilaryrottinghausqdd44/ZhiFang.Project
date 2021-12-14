using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBItemCalcFormula

    /// <summary>
    /// LBItemCalcFormula object for NHibernate mapped table 'LB_ItemCalcFormula'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBItemCalcFormula", ShortCode = "LBItemCalcFormula", Desc = "")]
    public class LBItemCalcFormula : BaseEntity
    {
        #region Member Variables

        protected string _formulaCondition;
        protected string _calcFormula;
        protected double _hAge;
        protected double _lAge;
        protected long? _ageUnitID;
        protected long? _genderID;
        protected double _uWeight;
        protected double _lWeight;
        protected long? _sampleTypeID;
        protected long? _sectionID;
        protected bool _isUse;
        protected bool _isDefault;
        protected int _calcType;
        protected bool _isKeepInvalid;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected string _formulaConditionInfo;
        protected string _calcFormulaInfo;
        protected LBItem _lBItem;


        #endregion

        #region Constructors

        public LBItemCalcFormula() { }

        public LBItemCalcFormula(string formulaCondition, string calcFormula, double hAge, double lAge, long ageUnitID, long genderID, double uWeight, double lWeight, long sampleTypeID, long sectionID, bool isUse, bool isDefault, int calcType, bool isKeepInvalid, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string formulaConditionInfo, string calcFormulaInfo, LBItem lBItem)
        {
            this._formulaCondition = formulaCondition;
            this._calcFormula = calcFormula;
            this._hAge = hAge;
            this._lAge = lAge;
            this._ageUnitID = ageUnitID;
            this._genderID = genderID;
            this._uWeight = uWeight;
            this._lWeight = lWeight;
            this._sampleTypeID = sampleTypeID;
            this._sectionID = sectionID;
            this._isUse = isUse;
            this._isDefault = isDefault;
            this._calcType = calcType;
            this._isKeepInvalid = isKeepInvalid;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._formulaConditionInfo = formulaConditionInfo;
            this._calcFormulaInfo = calcFormulaInfo;
            this._lBItem = lBItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormulaCondition", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string FormulaCondition
        {
            get { return _formulaCondition; }
            set { _formulaCondition = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CalcFormula", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string CalcFormula
        {
            get { return _calcFormula; }
            set { _calcFormula = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "HAge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double HAge
        {
            get { return _hAge; }
            set { _hAge = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LAge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LAge
        {
            get { return _lAge; }
            set { _lAge = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GenderID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GenderID
        {
            get { return _genderID; }
            set { _genderID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "UWeight", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double UWeight
        {
            get { return _uWeight; }
            set { _uWeight = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LWeight", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LWeight
        {
            get { return _lWeight; }
            set { _lWeight = value; }
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
        [DataDesc(CName = "", ShortCode = "SectionID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SectionID
        {
            get { return _sectionID; }
            set { _sectionID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDefault", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CalcType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CalcType
        {
            get { return _calcType; }
            set { _calcType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsKeepInvalid", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsKeepInvalid
        {
            get { return _isKeepInvalid; }
            set { _isKeepInvalid = value; }
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
        [DataDesc(CName = "计算条件描述", ShortCode = "FormulaConditionInfo", Desc = "计算条件描述", ContextType = SysDic.All, Length = 300)]
        public virtual string FormulaConditionInfo
        {
            get { return _formulaConditionInfo; }
            set { _formulaConditionInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "计算公式描述", ShortCode = "CalcFormulaInfo", Desc = "计算公式描述", ContextType = SysDic.All, Length = 300)]
        public virtual string CalcFormulaInfo
        {
            get { return _calcFormulaInfo; }
            set { _calcFormulaInfo = value; }
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