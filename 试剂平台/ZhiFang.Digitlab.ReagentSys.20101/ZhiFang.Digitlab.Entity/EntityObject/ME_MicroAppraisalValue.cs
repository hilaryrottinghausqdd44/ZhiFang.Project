using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEMicroAppraisalValue

    /// <summary>
    /// MEMicroAppraisalValue object for NHibernate mapped table 'ME_MicroAppraisalValue'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物鉴定结果", ClassCName = "MEMicroAppraisalValue", ShortCode = "MEMicroAppraisalValue", Desc = "微生物鉴定结果")]
    public class MEMicroAppraisalValue : BaseEntity
    {
        #region Member Variables

        protected string _empName;
        protected string _equipSampleNo;
        protected bool _isReport;
        protected string _resultComment;
        protected DateTime? _dataUpdateTime;
        protected long? _zdy1;
        protected long? _zdy2;
        protected long? _zdy3;
        protected long? _zdy4;
        protected long? _zdy5;
        protected long? _laboratoryEvaluation;
        protected long? _appraisalResult;
        protected bool _isDST;
        protected bool _isReportPublication;
        protected long? _resistancePhenotypeID;
        protected string _resistancePhenotypeName;
        protected string _equipResistancePhenotype;
        protected bool _deleteFlag;
        protected BIdentificationMethod _bIdentificationMethod;
        protected HREmployee _hREmployee;
        protected MEGroupSampleForm _mEGroupSampleForm;
        protected MEMicroCultureValue _mEMicroCultureValue;
        protected MEMicroRetainedBacteria _mEMicroRetainedBacteria;
        protected BMicro _bMicro;
        protected MEGroupSampleItem _mEGroupSampleItem;
        protected IList<MEMicroAppraisalResultReportPublication> _mEMicroAppraisalResultReportPublicationList;
        protected IList<MEMicroAppraisalTest> _mEMicroAppraisalTestList;
        protected IList<MEMicroDSTValue> _mEMicroDSTValueList;
        protected IList<MEMicroRetainedBacteria> _mEMicroRetainedBacteriaList;

        #endregion

        #region Constructors

        public MEMicroAppraisalValue() { }

        public MEMicroAppraisalValue(long labID, string empName, string equipSampleNo, bool isReport, string resultComment, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long zdy1, long zdy2, long zdy3, long zdy4, long zdy5, long laboratoryEvaluation, long appraisalResult, bool isDST, bool isReportPublication, long resistancePhenotypeID, string resistancePhenotypeName, string equipResistancePhenotype, bool deleteFlag, BIdentificationMethod bIdentificationMethod, HREmployee hREmployee, MEGroupSampleForm mEGroupSampleForm, MEMicroCultureValue mEMicroCultureValue, MEMicroRetainedBacteria mEMicroRetainedBacteria, BMicro bMicro, MEGroupSampleItem mEGroupSampleItem)
        {
            this._labID = labID;
            this._empName = empName;
            this._equipSampleNo = equipSampleNo;
            this._isReport = isReport;
            this._resultComment = resultComment;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._zdy1 = zdy1;
            this._zdy2 = zdy2;
            this._zdy3 = zdy3;
            this._zdy4 = zdy4;
            this._zdy5 = zdy5;
            this._laboratoryEvaluation = laboratoryEvaluation;
            this._appraisalResult = appraisalResult;
            this._isDST = isDST;
            this._isReportPublication = isReportPublication;
            this._resistancePhenotypeID = resistancePhenotypeID;
            this._resistancePhenotypeName = resistancePhenotypeName;
            this._equipResistancePhenotype = equipResistancePhenotype;
            this._deleteFlag = deleteFlag;
            this._bIdentificationMethod = bIdentificationMethod;
            this._hREmployee = hREmployee;
            this._mEGroupSampleForm = mEGroupSampleForm;
            this._mEMicroCultureValue = mEMicroCultureValue;
            this._mEMicroRetainedBacteria = mEMicroRetainedBacteria;
            this._bMicro = bMicro;
            this._mEGroupSampleItem = mEGroupSampleItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "鉴定结果记录人姓名", ShortCode = "EmpName", Desc = "鉴定结果记录人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string EmpName
        {
            get { return _empName; }
            set
            {
                _empName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "仪器样本号", ShortCode = "EquipSampleNo", Desc = "仪器样本号", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipSampleNo
        {
            get { return _equipSampleNo; }
            set
            {
                _equipSampleNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否报告", ShortCode = "IsReport", Desc = "是否报告", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReport
        {
            get { return _isReport; }
            set { _isReport = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果说明", ShortCode = "ResultComment", Desc = "结果说明", ContextType = SysDic.All, Length = 8000)]
        public virtual string ResultComment
        {
            get { return _resultComment; }
            set
            {
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Zdy1", ShortCode = "Zdy1", Desc = "Zdy1", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy1
        {
            get { return _zdy1; }
            set { _zdy1 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Zdy2", ShortCode = "Zdy2", Desc = "Zdy2", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy2
        {
            get { return _zdy2; }
            set { _zdy2 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Zdy3", ShortCode = "Zdy3", Desc = "Zdy3", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy3
        {
            get { return _zdy3; }
            set { _zdy3 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Zdy4", ShortCode = "Zdy4", Desc = "Zdy4", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy4
        {
            get { return _zdy4; }
            set { _zdy4 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Zdy5", ShortCode = "Zdy5", Desc = "Zdy5", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy5
        {
            get { return _zdy5; }
            set { _zdy5 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实验室评价", ShortCode = "LaboratoryEvaluation", Desc = "实验室评价", ContextType = SysDic.All, Length = 8)]
        public virtual long? LaboratoryEvaluation
        {
            get { return _laboratoryEvaluation; }
            set { _laboratoryEvaluation = value; }
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
        [DataDesc(CName = "是否药敏", ShortCode = "IsDST", Desc = "是否药敏", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDST
        {
            get { return _isDST; }
            set { _isDST = value; }
        }

        [DataMember]
        [DataDesc(CName = "报告是否已发布", ShortCode = "IsReportPublication", Desc = "报告是否已发布", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReportPublication
        {
            get { return _isReportPublication; }
            set { _isReportPublication = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "耐药表型ID", ShortCode = "ResistancePhenotypeID", Desc = "耐药表型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ResistancePhenotypeID
        {
            get { return _resistancePhenotypeID; }
            set { _resistancePhenotypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "耐药表型名称", ShortCode = "ResistancePhenotypeName", Desc = "耐药表型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ResistancePhenotypeName
        {
            get { return _resistancePhenotypeName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ResistancePhenotypeName", value, value.ToString());
                _resistancePhenotypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "仪器耐药表型", ShortCode = "EquipResistancePhenotype", Desc = "仪器耐药表型", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipResistancePhenotype
        {
            get { return _equipResistancePhenotype; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EquipResistancePhenotype", value, value.ToString());
                _equipResistancePhenotype = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "删除标志", ShortCode = "DeleteFlag", Desc = "删除标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "鉴定方法字典表", ShortCode = "BIdentificationMethod", Desc = "鉴定方法字典表")]
        public virtual BIdentificationMethod BIdentificationMethod
        {
            get { return _bIdentificationMethod; }
            set { _bIdentificationMethod = value; }
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
        [DataDesc(CName = "微生物留菌记录", ShortCode = "MEMicroRetainedBacteria", Desc = "微生物留菌记录")]
        public virtual MEMicroRetainedBacteria MEMicroRetainedBacteria
        {
            get { return _mEMicroRetainedBacteria; }
            set { _mEMicroRetainedBacteria = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物", ShortCode = "BMicro", Desc = "微生物")]
        public virtual BMicro BMicro
        {
            get { return _bMicro; }
            set { _bMicro = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本项目", ShortCode = "MEGroupSampleItem", Desc = "小组样本项目")]
        public virtual MEGroupSampleItem MEGroupSampleItem
        {
            get { return _mEGroupSampleItem; }
            set { _mEGroupSampleItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物报告鉴定结果发布记录表", ShortCode = "MEMicroAppraisalResultReportPublicationList", Desc = "微生物报告鉴定结果发布记录表")]
        public virtual IList<MEMicroAppraisalResultReportPublication> MEMicroAppraisalResultReportPublicationList
        {
            get
            {
                if (_mEMicroAppraisalResultReportPublicationList == null)
                {
                    _mEMicroAppraisalResultReportPublicationList = new List<MEMicroAppraisalResultReportPublication>();
                }
                return _mEMicroAppraisalResultReportPublicationList;
            }
            set { _mEMicroAppraisalResultReportPublicationList = value; }
        }

        [DataMember]
        [DataDesc(CName = "手工鉴定试验结果", ShortCode = "MEMicroAppraisalTestList", Desc = "手工鉴定试验结果")]
        public virtual IList<MEMicroAppraisalTest> MEMicroAppraisalTestList
        {
            get
            {
                if (_mEMicroAppraisalTestList == null)
                {
                    _mEMicroAppraisalTestList = new List<MEMicroAppraisalTest>();
                }
                return _mEMicroAppraisalTestList;
            }
            set { _mEMicroAppraisalTestList = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物药敏结果drug sensitive test", ShortCode = "MEMicroDSTValueList", Desc = "微生物药敏结果drug sensitive test")]
        public virtual IList<MEMicroDSTValue> MEMicroDSTValueList
        {
            get
            {
                if (_mEMicroDSTValueList == null)
                {
                    _mEMicroDSTValueList = new List<MEMicroDSTValue>();
                }
                return _mEMicroDSTValueList;
            }
            set { _mEMicroDSTValueList = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物留菌记录", ShortCode = "MEMicroRetainedBacteriaList", Desc = "微生物留菌记录")]
        public virtual IList<MEMicroRetainedBacteria> MEMicroRetainedBacteriaList
        {
            get
            {
                if (_mEMicroRetainedBacteriaList == null)
                {
                    _mEMicroRetainedBacteriaList = new List<MEMicroRetainedBacteria>();
                }
                return _mEMicroRetainedBacteriaList;
            }
            set { _mEMicroRetainedBacteriaList = value; }
        }


        #endregion
    }
    #endregion
}