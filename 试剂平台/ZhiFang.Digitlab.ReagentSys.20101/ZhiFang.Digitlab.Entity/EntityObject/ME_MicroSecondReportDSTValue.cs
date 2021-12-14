using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEMicroSecondReportDSTValue

    /// <summary>
    /// MEMicroSecondReportDSTValue object for NHibernate mapped table 'ME_MicroSecondReportDSTValue'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物二级报告初步药敏结果", ClassCName = "MEMicroSecondReportDSTValue", ShortCode = "MEMicroSecondReportDSTValue", Desc = "微生物二级报告初步药敏结果")]
    public class MEMicroSecondReportDSTValue : BaseEntity
    {
        #region Member Variables

        protected int _dSTType;
        protected string _concentration;
        protected string _reportValue;
        protected string _resultStatus;
        protected double _quanValue;
        protected string _units;
        protected string _refRange;
        protected string _testMethod;
        protected int _alarmLevel;
        protected string _resultComment;
        protected bool _isUse;
        protected int _dispOrder;
        protected string _empName;
        protected DateTime? _dataUpdateTime;
        protected BAnti _bAnti;
        protected HREmployee _hREmployee;
        protected MEGroupSampleForm _mEGroupSampleForm;
        protected MEMicroSecondReportAppraisalValue _mEMicroSecondReportAppraisalValue;
        protected MEMicroThreeReport _mEMicroThreeReport;

        #endregion

        #region Constructors

        public MEMicroSecondReportDSTValue() { }

        public MEMicroSecondReportDSTValue(long labID, int dSTType, string concentration, string reportValue, string resultStatus, double quanValue, string units, string refRange, string testMethod, int alarmLevel, string resultComment, bool isUse, int dispOrder, string empName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BAnti bAnti, HREmployee hREmployee, MEGroupSampleForm mEGroupSampleForm, MEMicroSecondReportAppraisalValue mEMicroSecondReportAppraisalValue, MEMicroThreeReport mEMicroThreeReport)
        {
            this._labID = labID;
            this._dSTType = dSTType;
            this._concentration = concentration;
            this._reportValue = reportValue;
            this._resultStatus = resultStatus;
            this._quanValue = quanValue;
            this._units = units;
            this._refRange = refRange;
            this._testMethod = testMethod;
            this._alarmLevel = alarmLevel;
            this._resultComment = resultComment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._empName = empName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bAnti = bAnti;
            this._hREmployee = hREmployee;
            this._mEGroupSampleForm = mEGroupSampleForm;
            this._mEMicroSecondReportAppraisalValue = mEMicroSecondReportAppraisalValue;
            this._mEMicroThreeReport = mEMicroThreeReport;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "药敏实验类型", ShortCode = "DSTType", Desc = "药敏实验类型", ContextType = SysDic.All, Length = 4)]
        public virtual int DSTType
        {
            get { return _dSTType; }
            set { _dSTType = value; }
        }

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
        [DataDesc(CName = "报告值", ShortCode = "ReportValue", Desc = "报告值", ContextType = SysDic.All, Length = 300)]
        public virtual string ReportValue
        {
            get { return _reportValue; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for ReportValue", value, value.ToString());
                _reportValue = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检验结果状态", ShortCode = "ResultStatus", Desc = "检验结果状态", ContextType = SysDic.All, Length = 10)]
        public virtual string ResultStatus
        {
            get { return _resultStatus; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for ResultStatus", value, value.ToString());
                _resultStatus = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定量结果", ShortCode = "QuanValue", Desc = "定量结果", ContextType = SysDic.All, Length = 8)]
        public virtual double QuanValue
        {
            get { return _quanValue; }
            set { _quanValue = value; }
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
        [DataDesc(CName = "检测方法", ShortCode = "TestMethod", Desc = "检测方法", ContextType = SysDic.All, Length = 50)]
        public virtual string TestMethod
        {
            get { return _testMethod; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TestMethod", value, value.ToString());
                _testMethod = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "结果警示级别", ShortCode = "AlarmLevel", Desc = "结果警示级别", ContextType = SysDic.All, Length = 4)]
        public virtual int AlarmLevel
        {
            get { return _alarmLevel; }
            set { _alarmLevel = value; }
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
        [DataDesc(CName = "药敏结果记录人姓名", ShortCode = "EmpName", Desc = "药敏结果记录人姓名", ContextType = SysDic.All, Length = 60)]
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "抗生素", ShortCode = "BAnti", Desc = "抗生素")]
        public virtual BAnti BAnti
        {
            get { return _bAnti; }
            set { _bAnti = value; }
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
        [DataDesc(CName = "微生物二级报告初步鉴定结果", ShortCode = "MEMicroSecondReportAppraisalValue", Desc = "微生物二级报告初步鉴定结果")]
        public virtual MEMicroSecondReportAppraisalValue MEMicroSecondReportAppraisalValue
        {
            get { return _mEMicroSecondReportAppraisalValue; }
            set { _mEMicroSecondReportAppraisalValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物三级报告主表", ShortCode = "MEMicroThreeReport", Desc = "微生物三级报告主表")]
        public virtual MEMicroThreeReport MEMicroThreeReport
        {
            get { return _mEMicroThreeReport; }
            set { _mEMicroThreeReport = value; }
        }


        #endregion
    }
    #endregion
}