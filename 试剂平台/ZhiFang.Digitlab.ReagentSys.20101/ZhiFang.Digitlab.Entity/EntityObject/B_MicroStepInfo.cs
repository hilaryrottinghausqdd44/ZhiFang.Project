using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BMicroStepInfo

    /// <summary>
    /// BMicroStepInfo object for NHibernate mapped table 'B_MicroStepInfo'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物检验步骤记录项", ClassCName = "BMicroStepInfo", ShortCode = "BMicroStepInfo", Desc = "微生物检验步骤记录项")]
    public class BMicroStepInfo : BaseEntity
    {
        #region Member Variables

        protected bool _isDefault;
        protected bool _isPrintTitle;
        protected bool _isPrint;
        protected string _defaultValue;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected BMicroTestItemInfo _bMicroTestItemInfo;
        protected BMicroTestStep _bMicroTestStep;

        #endregion

        #region Constructors

        public BMicroStepInfo() { }

        public BMicroStepInfo(long labID, bool isDefault, bool isPrintTitle, bool isPrint, string defaultValue, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroTestItemInfo bMicroTestItemInfo, BMicroTestStep bMicroTestStep)
        {
            this._labID = labID;
            this._isDefault = isDefault;
            this._isPrintTitle = isPrintTitle;
            this._isPrint = isPrint;
            this._defaultValue = defaultValue;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bMicroTestItemInfo = bMicroTestItemInfo;
            this._bMicroTestStep = bMicroTestStep;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "是否默认", ShortCode = "IsDefault", Desc = "是否默认", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否报告信息名称", ShortCode = "IsPrintTitle", Desc = "是否报告信息名称", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPrintTitle
        {
            get { return _isPrintTitle; }
            set { _isPrintTitle = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否报告", ShortCode = "IsPrint", Desc = "是否报告", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPrint
        {
            get { return _isPrint; }
            set { _isPrint = value; }
        }

        [DataMember]
        [DataDesc(CName = "默认结果", ShortCode = "DefaultValue", Desc = "默认结果", ContextType = SysDic.All, Length = 300)]
        public virtual string DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for DefaultValue", value, value.ToString());
                _defaultValue = value;
            }
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
        [DataDesc(CName = "微生物检验记录项字典表", ShortCode = "BMicroTestItemInfo", Desc = "微生物检验记录项字典表")]
        public virtual BMicroTestItemInfo BMicroTestItemInfo
        {
            get { return _bMicroTestItemInfo; }
            set { _bMicroTestItemInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物检验步骤", ShortCode = "BMicroTestStep", Desc = "微生物检验步骤")]
        public virtual BMicroTestStep BMicroTestStep
        {
            get { return _bMicroTestStep; }
            set { _bMicroTestStep = value; }
        }


        #endregion
    }
    #endregion
}