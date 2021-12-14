using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEMicroCultureValue

    /// <summary>
    /// MEMicroCultureValue object for NHibernate mapped table 'ME_MicroCultureValue'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物培养结果", ClassCName = "MEMicroCultureValue", ShortCode = "MEMicroCultureValue", Desc = "微生物培养结果")]
    public class MEMicroCultureValue : BaseEntity
    {
        #region Member Variables

        protected string _empName;
        protected long? _amountOfBacteria;
        protected long? _colonyMorphology;
        protected long? _hemolysis;
        protected long? _catalyst;
        protected long? _oxidase;
        protected long? _gramStain;
        protected long? _zdy1;
        protected long? _zdy2;
        protected long? _zdy3;
        protected long? _zdy4;
        protected long? _zdy5;
        protected string _smearMemo;
        protected bool _isReport;
        protected DateTime? _dataUpdateTime;
        protected long? _microGroup;
        protected string _warnPositiveDatetime;
        protected bool _deleteFlag;
        protected HREmployee _hREmployee;
        protected MEGroupSampleForm _mEGroupSampleForm;
        protected MEGroupSampleItem _mEGroupSampleItem;
        protected MEMicroInoculant _mEMicroInoculant;
        protected IList<MEMicroAppraisalValue> _mEMicroAppraisalValueList;
        protected IList<MEMicroCultureLog> _mEMicroCultureLogList;
        protected IList<MEMicroDSTValue> _mEMicroDSTValueList;

        #endregion

        #region Constructors

        public MEMicroCultureValue() { }

        public MEMicroCultureValue(long labID, string empName, long amountOfBacteria, long colonyMorphology, long hemolysis, long catalyst, long oxidase, long gramStain, long zdy1, long zdy2, long zdy3, long zdy4, long zdy5, string smearMemo, bool isReport, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long microGroup, string warnPositiveDatetime, bool deleteFlag, HREmployee hREmployee, MEGroupSampleForm mEGroupSampleForm, MEGroupSampleItem mEGroupSampleItem, MEMicroInoculant mEMicroInoculant)
        {
            this._labID = labID;
            this._empName = empName;
            this._amountOfBacteria = amountOfBacteria;
            this._colonyMorphology = colonyMorphology;
            this._hemolysis = hemolysis;
            this._catalyst = catalyst;
            this._oxidase = oxidase;
            this._gramStain = gramStain;
            this._zdy1 = zdy1;
            this._zdy2 = zdy2;
            this._zdy3 = zdy3;
            this._zdy4 = zdy4;
            this._zdy5 = zdy5;
            this._smearMemo = smearMemo;
            this._isReport = isReport;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._microGroup = microGroup;
            this._warnPositiveDatetime = warnPositiveDatetime;
            this._deleteFlag = deleteFlag;
            this._hREmployee = hREmployee;
            this._mEGroupSampleForm = mEGroupSampleForm;
            this._mEGroupSampleItem = mEGroupSampleItem;
            this._mEMicroInoculant = mEMicroInoculant;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "培养结果记录人姓名", ShortCode = "EmpName", Desc = "培养结果记录人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string EmpName
        {
            get { return _empName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
                _empName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "菌量", ShortCode = "AmountOfBacteria", Desc = "菌量", ContextType = SysDic.All, Length = 8)]
        public virtual long? AmountOfBacteria
        {
            get { return _amountOfBacteria; }
            set { _amountOfBacteria = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "菌落形态", ShortCode = "ColonyMorphology", Desc = "菌落形态", ContextType = SysDic.All, Length = 8)]
        public virtual long? ColonyMorphology
        {
            get { return _colonyMorphology; }
            set { _colonyMorphology = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "溶血", ShortCode = "Hemolysis", Desc = "溶血", ContextType = SysDic.All, Length = 8)]
        public virtual long? Hemolysis
        {
            get { return _hemolysis; }
            set { _hemolysis = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "触媒", ShortCode = "Catalyst", Desc = "触媒", ContextType = SysDic.All, Length = 8)]
        public virtual long? Catalyst
        {
            get { return _catalyst; }
            set { _catalyst = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "氧化酶", ShortCode = "Oxidase", Desc = "氧化酶", ContextType = SysDic.All, Length = 8)]
        public virtual long? Oxidase
        {
            get { return _oxidase; }
            set { _oxidase = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "革兰染色", ShortCode = "GramStain", Desc = "革兰染色", ContextType = SysDic.All, Length = 8)]
        public virtual long? GramStain
        {
            get { return _gramStain; }
            set { _gramStain = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "zdy1", ShortCode = "Zdy1", Desc = "zdy1", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy1
        {
            get { return _zdy1; }
            set { _zdy1 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "zdy2", ShortCode = "Zdy2", Desc = "zdy2", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy2
        {
            get { return _zdy2; }
            set { _zdy2 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "zdy3", ShortCode = "Zdy3", Desc = "zdy3", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy3
        {
            get { return _zdy3; }
            set { _zdy3 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "zdy4", ShortCode = "Zdy4", Desc = "zdy4", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy4
        {
            get { return _zdy4; }
            set { _zdy4 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "zdy5", ShortCode = "Zdy5", Desc = "zdy5", ContextType = SysDic.All, Length = 8)]
        public virtual long? Zdy5
        {
            get { return _zdy5; }
            set { _zdy5 = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "SmearMemo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string SmearMemo
        {
            get { return _smearMemo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for SmearMemo", value, value.ToString());
                _smearMemo = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "菌群", ShortCode = "MicroGroup", Desc = "菌群", ContextType = SysDic.All, Length = 8)]
        public virtual long? MicroGroup
        {
            get { return _microGroup; }
            set { _microGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WarnPositiveDatetime", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string WarnPositiveDatetime
        {
            get { return _warnPositiveDatetime; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for WarnPositiveDatetime", value, value.ToString());
                _warnPositiveDatetime = value;
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
        [DataDesc(CName = "小组样本项目", ShortCode = "MEGroupSampleItem", Desc = "小组样本项目")]
        public virtual MEGroupSampleItem MEGroupSampleItem
        {
            get { return _mEGroupSampleItem; }
            set { _mEGroupSampleItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物接种记录", ShortCode = "MEMicroInoculant", Desc = "微生物接种记录")]
        public virtual MEMicroInoculant MEMicroInoculant
        {
            get { return _mEMicroInoculant; }
            set { _mEMicroInoculant = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物鉴定结果", ShortCode = "MEMicroAppraisalValueList", Desc = "微生物鉴定结果")]
        public virtual IList<MEMicroAppraisalValue> MEMicroAppraisalValueList
        {
            get
            {
                if (_mEMicroAppraisalValueList == null)
                {
                    _mEMicroAppraisalValueList = new List<MEMicroAppraisalValue>();
                }
                return _mEMicroAppraisalValueList;
            }
            set { _mEMicroAppraisalValueList = value; }
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


        #endregion
    }
    #endregion
}