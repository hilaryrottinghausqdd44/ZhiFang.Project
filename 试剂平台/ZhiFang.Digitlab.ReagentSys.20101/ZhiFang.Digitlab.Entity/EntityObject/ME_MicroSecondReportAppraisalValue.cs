using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEMicroSecondReportAppraisalValue

    /// <summary>
    /// MEMicroSecondReportAppraisalValue object for NHibernate mapped table 'ME_MicroSecondReportAppraisalValue'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物二级报告初步鉴定结果", ClassCName = "MEMicroSecondReportAppraisalValue", ShortCode = "MEMicroSecondReportAppraisalValue", Desc = "微生物二级报告初步鉴定结果")]
    public class MEMicroSecondReportAppraisalValue : BaseEntity
    {
        #region Member Variables

        protected string _empName;
        protected long? _appraisalResult;
        protected bool _isReport;
        protected string _resultComment;
        protected DateTime? _dataUpdateTime;
        protected BMicro _bMicro;
        protected HREmployee _hREmployee;
        protected MEGroupSampleForm _mEGroupSampleForm;
        protected MEMicroThreeReport _mEMicroThreeReport;
        protected IList<MEMicroSecondReportDSTValue> _mEMicroSecondReportDSTValueList;

        #endregion

        #region Constructors

        public MEMicroSecondReportAppraisalValue() { }

        public MEMicroSecondReportAppraisalValue(long labID, string empName, long appraisalResult, bool isReport, string resultComment, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicro bMicro, HREmployee hREmployee, MEGroupSampleForm mEGroupSampleForm, MEMicroThreeReport mEMicroThreeReport)
        {
            this._labID = labID;
            this._empName = empName;
            this._appraisalResult = appraisalResult;
            this._isReport = isReport;
            this._resultComment = resultComment;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bMicro = bMicro;
            this._hREmployee = hREmployee;
            this._mEGroupSampleForm = mEGroupSampleForm;
            this._mEMicroThreeReport = mEMicroThreeReport;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "鉴定结果记录人姓名", ShortCode = "EmpName", Desc = "鉴定结果记录人姓名", ContextType = SysDic.All, Length = 60)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "鉴定结果", ShortCode = "AppraisalResult", Desc = "鉴定结果", ContextType = SysDic.All, Length = 8)]
        public virtual long? AppraisalResult
        {
            get { return _appraisalResult; }
            set { _appraisalResult = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否报告", ShortCode = "IsReport", Desc = "是否报告", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReport
        {
            get { return _isReport; }
            set { _isReport = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果说明", ShortCode = "ResultComment", Desc = "结果说明", ContextType = SysDic.All, Length = 16)]
        public virtual string ResultComment
        {
            get { return _resultComment; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for ResultComment", value, value.ToString());
                _resultComment = value;
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
        [DataDesc(CName = "微生物", ShortCode = "BMicro", Desc = "微生物")]
        public virtual BMicro BMicro
        {
            get { return _bMicro; }
            set { _bMicro = value; }
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
        [DataDesc(CName = "微生物三级报告主表", ShortCode = "MEMicroThreeReport", Desc = "微生物三级报告主表")]
        public virtual MEMicroThreeReport MEMicroThreeReport
        {
            get { return _mEMicroThreeReport; }
            set { _mEMicroThreeReport = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物二级报告初步药敏结果", ShortCode = "MEMicroSecondReportDSTValueList", Desc = "微生物二级报告初步药敏结果")]
        public virtual IList<MEMicroSecondReportDSTValue> MEMicroSecondReportDSTValueList
        {
            get
            {
                if (_mEMicroSecondReportDSTValueList == null)
                {
                    _mEMicroSecondReportDSTValueList = new List<MEMicroSecondReportDSTValue>();
                }
                return _mEMicroSecondReportDSTValueList;
            }
            set { _mEMicroSecondReportDSTValueList = value; }
        }


        #endregion
    }
    #endregion
}