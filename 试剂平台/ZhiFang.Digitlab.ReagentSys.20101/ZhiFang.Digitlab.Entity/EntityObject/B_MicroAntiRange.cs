using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BMicroAntiRange

    /// <summary>
    /// BMicroAntiRange object for NHibernate mapped table 'B_MicroAntiRange'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "抗生素参考范围", ClassCName = "BMicroAntiRange", ShortCode = "BMicroAntiRange", Desc = "抗生素参考范围")]
    public class BMicroAntiRange : BaseEntity
    {
        #region Member Variables

        protected string _concentration;
        protected int _dSTType;
        protected double _valueH;
        protected double _valueL;
        protected string _units;
        protected string _refRange;
        protected string _reportGroup;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected bool _useSDD;
        protected BAnti _bAnti;
        protected BMicro _bMicro;
        protected BMicroAntiClass _bMicroAntiClass;

        #endregion

        #region Constructors

        public BMicroAntiRange() { }

        public BMicroAntiRange(long labID, string concentration, int dSTType, double valueH, double valueL, string units, string refRange, string reportGroup, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool useSDD, BAnti bAnti, BMicro bMicro, BMicroAntiClass bMicroAntiClass)
        {
            this._labID = labID;
            this._concentration = concentration;
            this._dSTType = dSTType;
            this._valueH = valueH;
            this._valueL = valueL;
            this._units = units;
            this._refRange = refRange;
            this._reportGroup = reportGroup;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._useSDD = useSDD;
            this._bAnti = bAnti;
            this._bMicro = bMicro;
            this._bMicroAntiClass = bMicroAntiClass;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "纸片浓度", ShortCode = "Concentration", Desc = "纸片浓度", ContextType = SysDic.All, Length = 50)]
        public virtual string Concentration
        {
            get { return _concentration; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Concentration", value, value.ToString());
                _concentration = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "药敏试验类型KB，MIC", ShortCode = "DSTType", Desc = "药敏试验类型KB，MIC", ContextType = SysDic.All, Length = 4)]
        public virtual int DSTType
        {
            get { return _dSTType; }
            set { _dSTType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "参考高值", ShortCode = "ValueH", Desc = "参考高值", ContextType = SysDic.All, Length = 8)]
        public virtual double ValueH
        {
            get { return _valueH; }
            set { _valueH = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "参考低值", ShortCode = "ValueL", Desc = "参考低值", ContextType = SysDic.All, Length = 8)]
        public virtual double ValueL
        {
            get { return _valueL; }
            set { _valueL = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果单位", ShortCode = "Units", Desc = "结果单位", ContextType = SysDic.All, Length = 50)]
        public virtual string Units
        {
            get { return _units; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Units", value, value.ToString());
                _units = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "参考范围", ShortCode = "RefRange", Desc = "参考范围", ContextType = SysDic.All, Length = 200)]
        public virtual string RefRange
        {
            get { return _refRange; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for RefRange", value, value.ToString());
                _refRange = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "试验报告组", ShortCode = "ReportGroup", Desc = "试验报告组", ContextType = SysDic.All, Length = 20)]
        public virtual string ReportGroup
        {
            get { return _reportGroup; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReportGroup", value, value.ToString());
                _reportGroup = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "使用SDD替代中介", ShortCode = "UseSDD", Desc = "使用SDD替代中介", ContextType = SysDic.All, Length = 1)]
        public virtual bool UseSDD
        {
            get { return _useSDD; }
            set { _useSDD = value; }
        }

        [DataMember]
        [DataDesc(CName = "抗生素", ShortCode = "BAnti", Desc = "抗生素")]
        public virtual BAnti BAnti
        {
            get { return _bAnti; }
            set { _bAnti = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物", ShortCode = "BMicro", Desc = "微生物")]
        public virtual BMicro BMicro
        {
            get { return _bMicro; }
            set { _bMicro = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物抗生素检测类型", ShortCode = "BMicroAntiClass", Desc = "微生物抗生素检测类型")]
        public virtual BMicroAntiClass BMicroAntiClass
        {
            get { return _bMicroAntiClass; }
            set { _bMicroAntiClass = value; }
        }


        #endregion
    }
    #endregion
}