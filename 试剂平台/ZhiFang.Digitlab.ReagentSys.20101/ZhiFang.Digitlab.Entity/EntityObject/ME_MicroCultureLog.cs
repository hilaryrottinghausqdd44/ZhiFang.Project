using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEMicroCultureLog

    /// <summary>
    /// MEMicroCultureLog object for NHibernate mapped table 'ME_MicroCultureLog'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物培养日志", ClassCName = "MEMicroCultureLog", ShortCode = "MEMicroCultureLog", Desc = "微生物培养日志")]
    public class MEMicroCultureLog : BaseEntity
    {
        #region Member Variables

        protected string _logInfo;
        protected DateTime? _dataUpdateTime;
        protected string _empName;
        protected HREmployee _hREmployee;
        protected MEGroupSampleForm _mEGroupSampleForm;
        protected MEMicroCultureValue _mEMicroCultureValue;
        protected MEMicroInoculant _mEMicroInoculant;

        #endregion

        #region Constructors

        public MEMicroCultureLog() { }

        public MEMicroCultureLog(long labID, string logInfo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string empName, HREmployee hREmployee, MEGroupSampleForm mEGroupSampleForm, MEMicroCultureValue mEMicroCultureValue, MEMicroInoculant mEMicroInoculant)
        {
            this._labID = labID;
            this._logInfo = logInfo;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._empName = empName;
            this._hREmployee = hREmployee;
            this._mEGroupSampleForm = mEGroupSampleForm;
            this._mEMicroCultureValue = mEMicroCultureValue;
            this._mEMicroInoculant = mEMicroInoculant;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "培养日志", ShortCode = "LogInfo", Desc = "培养日志", ContextType = SysDic.All, Length = 500)]
        public virtual string LogInfo
        {
            get { return _logInfo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for LogInfo", value, value.ToString());
                _logInfo = value;
            }
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
        [DataDesc(CName = "记录人姓名", ShortCode = "EmpName", Desc = "记录人姓名", ContextType = SysDic.All, Length = 60)]
        public virtual string EmpName
        {
            get { return _empName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
                _empName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
        public virtual HREmployee HREmployee
        {
            get { return _hREmployee; }
            set { _hREmployee = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
        public virtual MEGroupSampleForm MEGroupSampleForm
        {
            get { return _mEGroupSampleForm; }
            set { _mEGroupSampleForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物培养结果", ShortCode = "MEMicroCultureValue", Desc = "微生物培养结果")]
        public virtual MEMicroCultureValue MEMicroCultureValue
        {
            get { return _mEMicroCultureValue; }
            set { _mEMicroCultureValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物接种记录", ShortCode = "MEMicroInoculant", Desc = "微生物接种记录")]
        public virtual MEMicroInoculant MEMicroInoculant
        {
            get { return _mEMicroInoculant; }
            set { _mEMicroInoculant = value; }
        }


        #endregion
    }
    #endregion
}