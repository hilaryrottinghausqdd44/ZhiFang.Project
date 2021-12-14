using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBTcuvete

    /// <summary>
    /// LBTcuvete object for NHibernate mapped table 'LB_Tcuvete'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBTcuvete", ShortCode = "LBTcuvete", Desc = "")]
    public class LBTcuvete : BaseEntity
    {
        #region Member Variables

        protected string _color;
        protected decimal _capacity;
        protected string _synopsis;
        protected string _unit;
        protected string _cName;
        protected string _sName;
        protected string _code;
        protected string _sCode;
        protected decimal _minCapability;
        protected string _colorValue;
        protected bool _isPrep;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBTcuvete() { }

        public LBTcuvete(string color, decimal capacity, string synopsis, string unit, string cName, string sName, string code, string sCode, decimal minCapability, string colorValue, bool isPrep, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._color = color;
            this._capacity = capacity;
            this._synopsis = synopsis;
            this._unit = unit;
            this._cName = cName;
            this._sName = sName;
            this._code = code;
            this._sCode = sCode;
            this._minCapability = minCapability;
            this._colorValue = colorValue;
            this._isPrep = isPrep;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "Color", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Capacity", Desc = "", ContextType = SysDic.All, Length = 9)]
        public virtual decimal Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Synopsis", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Synopsis
        {
            get { return _synopsis; }
            set { _synopsis = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Unit", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MinCapability", Desc = "", ContextType = SysDic.All, Length = 9)]
        public virtual decimal MinCapability
        {
            get { return _minCapability; }
            set { _minCapability = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ColorValue", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ColorValue
        {
            get { return _colorValue; }
            set { _colorValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsPrep", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPrep
        {
            get { return _isPrep; }
            set { _isPrep = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}