using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEMicroInoculant

    /// <summary>
    /// MEMicroInoculant object for NHibernate mapped table 'ME_MicroInoculant'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物接种记录", ClassCName = "MEMicroInoculant", ShortCode = "MEMicroInoculant", Desc = "微生物接种记录")]
    public class MEMicroInoculant : BaseEntity
    {
        #region Member Variables

        protected string _groupName;
        protected string _sampleTypeName;
        protected string _barCode;
        protected string _amount;
        protected string _sampleCharacterName;
        protected string _cultureMediumName;
        protected string _cultivationConditionName;
        protected string _empName;
        protected DateTime? _dataUpdateTime;
        protected bool _deleteFlag;
        protected long? _pMicroInoculantID;
        protected string _bottleSerialNo;
        protected string _bSampleTypeSName;
        protected DateTime? _examineDateTime;
        protected string _gSampleNo;
        protected string _cultivationConditionSName;
        protected string _deptName;
        protected string _groupSName;
        protected string _sampleCharacterSName;
        protected string _cultureMediumSName;
        protected string _warnPositiveDatetime;
        protected string _desc;
        protected string _cultureMediumNo;
        protected string _cultureMediumEquipResult;
        protected DateTime? _cultureEndDatetime;
        protected string _cultureTotalTime;
        protected BCultivationCondition _bCultivationCondition;
        protected BCultureMedium _bCultureMedium;
        protected BSampleCharacter _bSampleCharacter;
        protected BSampleType _bSampleType;
        protected GMGroup _gMGroup;
        protected HREmployee _hREmployee;
        protected MEGroupSampleForm _mEGroupSampleForm;
        protected MEPTOrderForm _mEPTOrderForm;
        protected MEPTSampleForm _mEPTSampleForm;
        protected IList<MEMicroCultureLog> _mEMicroCultureLogList;
        protected IList<MEMicroCultureMessage> _mEMicroCultureMessageList;
        protected IList<MEMicroCultureValue> _mEMicroCultureValueList;

        #endregion

        #region Constructors

        public MEMicroInoculant() { }

        public MEMicroInoculant(long labID, string groupName, string sampleTypeName, string barCode, string amount, string sampleCharacterName, string cultureMediumName, string cultivationConditionName, string empName, DateTime dataAddTime, DateTime dataUpdateTime, bool deleteFlag, byte[] dataTimeStamp, long pMicroInoculantID, string bottleSerialNo, string bSampleTypeSName, DateTime examineDateTime, string gSampleNo, string cultivationConditionSName, string deptName, string groupSName, string sampleCharacterSName, string cultureMediumSName, string warnPositiveDatetime, string desc, string cultureMediumNo, string cultureMediumEquipResult, DateTime cultureEndDatetime, string cultureTotalTime, BCultivationCondition bCultivationCondition, BCultureMedium bCultureMedium, BSampleCharacter bSampleCharacter, BSampleType bSampleType, GMGroup gMGroup, HREmployee hREmployee, MEGroupSampleForm mEGroupSampleForm, MEPTOrderForm mEPTOrderForm, MEPTSampleForm mEPTSampleForm)
        {
            this._labID = labID;
            this._groupName = groupName;
            this._sampleTypeName = sampleTypeName;
            this._barCode = barCode;
            this._amount = amount;
            this._sampleCharacterName = sampleCharacterName;
            this._cultureMediumName = cultureMediumName;
            this._cultivationConditionName = cultivationConditionName;
            this._empName = empName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._deleteFlag = deleteFlag;
            this._dataTimeStamp = dataTimeStamp;
            this._pMicroInoculantID = pMicroInoculantID;
            this._bottleSerialNo = bottleSerialNo;
            this._bSampleTypeSName = bSampleTypeSName;
            this._examineDateTime = examineDateTime;
            this._gSampleNo = gSampleNo;
            this._cultivationConditionSName = cultivationConditionSName;
            this._deptName = deptName;
            this._groupSName = groupSName;
            this._sampleCharacterSName = sampleCharacterSName;
            this._cultureMediumSName = cultureMediumSName;
            this._warnPositiveDatetime = warnPositiveDatetime;
            this._desc = desc;
            this._cultureMediumNo = cultureMediumNo;
            this._cultureMediumEquipResult = cultureMediumEquipResult;
            this._cultureEndDatetime = cultureEndDatetime;
            this._cultureTotalTime = cultureTotalTime;
            this._bCultivationCondition = bCultivationCondition;
            this._bCultureMedium = bCultureMedium;
            this._bSampleCharacter = bSampleCharacter;
            this._bSampleType = bSampleType;
            this._gMGroup = gMGroup;
            this._hREmployee = hREmployee;
            this._mEGroupSampleForm = mEGroupSampleForm;
            this._mEPTOrderForm = mEPTOrderForm;
            this._mEPTSampleForm = mEPTSampleForm;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "检验小组名称", ShortCode = "GroupName", Desc = "检验小组名称", ContextType = SysDic.All, Length = 50)]
        public virtual string GroupName
        {
            get { return _groupName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GroupName", value, value.ToString());
                _groupName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "样本类型名称", ShortCode = "SampleTypeName", Desc = "样本类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleTypeName
        {
            get { return _sampleTypeName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SampleTypeName", value, value.ToString());
                _sampleTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "样本条码号", ShortCode = "BarCode", Desc = "样本条码号", ContextType = SysDic.All, Length = 30)]
        public virtual string BarCode
        {
            get { return _barCode; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
                _barCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "样本量", ShortCode = "Amount", Desc = "样本量", ContextType = SysDic.All, Length = 50)]
        public virtual string Amount
        {
            get { return _amount; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Amount", value, value.ToString());
                _amount = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "样本性状名称", ShortCode = "SampleCharacterName", Desc = "样本性状名称", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleCharacterName
        {
            get { return _sampleCharacterName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SampleCharacterName", value, value.ToString());
                _sampleCharacterName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "培养基名称", ShortCode = "CultureMediumName", Desc = "培养基名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CultureMediumName
        {
            get { return _cultureMediumName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CultureMediumName", value, value.ToString());
                _cultureMediumName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "培养条件名称", ShortCode = "CultivationConditionName", Desc = "培养条件名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CultivationConditionName
        {
            get { return _cultivationConditionName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CultivationConditionName", value, value.ToString());
                _cultivationConditionName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "数据录入者姓名", ShortCode = "EmpName", Desc = "数据录入者姓名", ContextType = SysDic.All, Length = 30)]
        public virtual string EmpName
        {
            get { return _empName; }
            set
            {
                if (value != null && value.Length > 30)
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
        [DataDesc(CName = "删除标志", ShortCode = "DeleteFlag", Desc = "删除标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "分纯接种记录Id", ShortCode = "PMicroInoculantID", Desc = "分纯接种记录Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? PMicroInoculantID
        {
            get { return _pMicroInoculantID; }
            set { _pMicroInoculantID = value; }
        }

        [DataMember]
        [DataDesc(CName = "培养瓶条码", ShortCode = "BottleSerialNo", Desc = "培养瓶条码", ContextType = SysDic.All, Length = 50)]
        public virtual string BottleSerialNo
        {
            get { return _bottleSerialNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BottleSerialNo", value, value.ToString());
                _bottleSerialNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "样本类型简称", ShortCode = "BSampleTypeSName", Desc = "样本类型简称", ContextType = SysDic.All, Length = 50)]
        public virtual string BSampleTypeSName
        {
            get { return _bSampleTypeSName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BSampleTypeSName", value, value.ToString());
                _bSampleTypeSName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验单日期", ShortCode = "ExamineDateTime", Desc = "检验单日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ExamineDateTime
        {
            get { return _examineDateTime; }
            set { _examineDateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本号", ShortCode = "GSampleNo", Desc = "小组样本号", ContextType = SysDic.All, Length = 50)]
        public virtual string GSampleNo
        {
            get { return _gSampleNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GSampleNo", value, value.ToString());
                _gSampleNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "培养条件简称", ShortCode = "CultivationConditionSName", Desc = "培养条件简称", ContextType = SysDic.All, Length = 50)]
        public virtual string CultivationConditionSName
        {
            get { return _cultivationConditionSName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CultivationConditionSName", value, value.ToString());
                _cultivationConditionSName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "就诊科室", ShortCode = "DeptName", Desc = "就诊科室", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
                _deptName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检验小组简称", ShortCode = "GroupSName", Desc = "检验小组简称", ContextType = SysDic.All, Length = 50)]
        public virtual string GroupSName
        {
            get { return _groupSName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GroupSName", value, value.ToString());
                _groupSName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "标本性状简称", ShortCode = "SampleCharacterSName", Desc = "标本性状简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleCharacterSName
        {
            get { return _sampleCharacterSName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SampleCharacterSName", value, value.ToString());
                _sampleCharacterSName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "培养基简称", ShortCode = "CultureMediumSName", Desc = "培养基简称", ContextType = SysDic.All, Length = 50)]
        public virtual string CultureMediumSName
        {
            get { return _cultureMediumSName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CultureMediumSName", value, value.ToString());
                _cultureMediumSName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "阳性报警时间", ShortCode = "WarnPositiveDatetime", Desc = "阳性报警时间", ContextType = SysDic.All, Length = 30)]
        public virtual string WarnPositiveDatetime
        {
            get { return _warnPositiveDatetime; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for WarnPositiveDatetime", value, value.ToString());
                _warnPositiveDatetime = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "培养描述", ShortCode = "Desc", Desc = "培养描述", ContextType = SysDic.All, Length = 30)]
        public virtual string Desc
        {
            get { return _desc; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for Desc", value, value.ToString());
                _desc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "培养基编号", ShortCode = "CultureMediumNo", Desc = "培养基编号", ContextType = SysDic.All, Length = 20)]
        public virtual string CultureMediumNo
        {
            get { return _cultureMediumNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CultureMediumNo", value, value.ToString());
                _cultureMediumNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "培养基仪器结果", ShortCode = "CultureMediumEquipResult", Desc = "培养基仪器结果", ContextType = SysDic.All, Length = 30)]
        public virtual string CultureMediumEquipResult
        {
            get { return _cultureMediumEquipResult; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for CultureMediumEquipResult", value, value.ToString());
                _cultureMediumEquipResult = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "培养结束时间", ShortCode = "CultureEndDatetime", Desc = "培养结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CultureEndDatetime
        {
            get { return _cultureEndDatetime; }
            set { _cultureEndDatetime = value; }
        }

        [DataMember]
        [DataDesc(CName = "培养时长", ShortCode = "CultureTotalTime", Desc = "培养时长", ContextType = SysDic.All, Length = 20)]
        public virtual string CultureTotalTime
        {
            get { return _cultureTotalTime; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CultureTotalTime", value, value.ToString());
                _cultureTotalTime = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "培养条件字典表", ShortCode = "BCultivationCondition", Desc = "培养条件字典表")]
        public virtual BCultivationCondition BCultivationCondition
        {
            get { return _bCultivationCondition; }
            set { _bCultivationCondition = value; }
        }

        [DataMember]
        [DataDesc(CName = "培养基字典表", ShortCode = "BCultureMedium", Desc = "培养基字典表")]
        public virtual BCultureMedium BCultureMedium
        {
            get { return _bCultureMedium; }
            set { _bCultureMedium = value; }
        }

        [DataMember]
        [DataDesc(CName = "标本性状字典表", ShortCode = "BSampleCharacter", Desc = "标本性状字典表")]
        public virtual BSampleCharacter BSampleCharacter
        {
            get { return _bSampleCharacter; }
            set { _bSampleCharacter = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
        public virtual BSampleType BSampleType
        {
            get { return _bSampleType; }
            set { _bSampleType = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
        public virtual GMGroup GMGroup
        {
            get { return _gMGroup; }
            set { _gMGroup = value; }
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
        [DataDesc(CName = "医嘱单", ShortCode = "MEPTOrderForm", Desc = "医嘱单")]
        public virtual MEPTOrderForm MEPTOrderForm
        {
            get { return _mEPTOrderForm; }
            set { _mEPTOrderForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本单", ShortCode = "MEPTSampleForm", Desc = "样本单")]
        public virtual MEPTSampleForm MEPTSampleForm
        {
            get { return _mEPTSampleForm; }
            set { _mEPTSampleForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物培养日志", ShortCode = "MEMicroCultureLogList", Desc = "微生物培养日志")]
        public virtual IList<MEMicroCultureLog> MEMicroCultureLogList
        {
            get
            {
                if (_mEMicroCultureLogList == null)
                {
                    _mEMicroCultureLogList = new List<MEMicroCultureLog>();
                }
                return _mEMicroCultureLogList;
            }
            set { _mEMicroCultureLogList = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物培养随笔记录", ShortCode = "MEMicroCultureMessageList", Desc = "微生物培养随笔记录")]
        public virtual IList<MEMicroCultureMessage> MEMicroCultureMessageList
        {
            get
            {
                if (_mEMicroCultureMessageList == null)
                {
                    _mEMicroCultureMessageList = new List<MEMicroCultureMessage>();
                }
                return _mEMicroCultureMessageList;
            }
            set { _mEMicroCultureMessageList = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物培养结果", ShortCode = "MEMicroCultureValueList", Desc = "微生物培养结果")]
        public virtual IList<MEMicroCultureValue> MEMicroCultureValueList
        {
            get
            {
                if (_mEMicroCultureValueList == null)
                {
                    _mEMicroCultureValueList = new List<MEMicroCultureValue>();
                }
                return _mEMicroCultureValueList;
            }
            set { _mEMicroCultureValueList = value; }
        }


        #endregion
    }
    #endregion
}