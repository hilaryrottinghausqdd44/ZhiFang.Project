using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BMicroItemTestStep

    /// <summary>
    /// BMicroItemTestStep object for NHibernate mapped table 'B_MicroItemTestStep'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "项目微生物检验步骤关系", ClassCName = "BMicroItemTestStep", ShortCode = "BMicroItemTestStep", Desc = "项目微生物检验步骤关系")]
    public class BMicroItemTestStep : BaseEntity
    {
        #region Member Variables

        protected bool _isNextStep;
        protected string _comment;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected bool _isDefault;
        protected BMicroItemTestStepGroup _bMicroItemTestStepGroup;
        protected BMicroTestStep _bMicroTestStep;
        protected BMicroTestStep _next;
        protected IList<EPMicroEquipItemStep> _ePMicroEquipItemStepList;

        #endregion

        #region Constructors

        public BMicroItemTestStep() { }

        public BMicroItemTestStep(long labID, bool isNextStep, string comment, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool isDefault, BMicroItemTestStepGroup bMicroItemTestStepGroup, BMicroTestStep bMicroTestStep, BMicroTestStep next)
        {
            this._labID = labID;
            this._isNextStep = isNextStep;
            this._comment = comment;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._isDefault = isDefault;
            this._bMicroItemTestStepGroup = bMicroItemTestStepGroup;
            this._bMicroTestStep = bMicroTestStep;
            this._next = next;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "自动下一步", ShortCode = "IsNextStep", Desc = "自动下一步", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsNextStep
        {
            get { return _isNextStep; }
            set { _isNextStep = value; }
        }

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
                _comment = value;
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
        [DataDesc(CName = "是否默认", ShortCode = "IsDefault", Desc = "是否默认", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物检验步骤组合，为（检验项目+样本类型）这样的检验对象定制一系列与之对应的检验步骤", ShortCode = "BMicroItemTestStepGroup", Desc = "微生物检验步骤组合，为（检验项目+样本类型）这样的检验对象定制一系列与之对应的检验步骤")]
        public virtual BMicroItemTestStepGroup BMicroItemTestStepGroup
        {
            get { return _bMicroItemTestStepGroup; }
            set { _bMicroItemTestStepGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物检验步骤", ShortCode = "BMicroTestStep", Desc = "微生物检验步骤")]
        public virtual BMicroTestStep BMicroTestStep
        {
            get { return _bMicroTestStep; }
            set { _bMicroTestStep = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物下一检验步骤", ShortCode = "Next", Desc = "微生物下一检验步骤")]
        public virtual BMicroTestStep Next
        {
            get { return _next; }
            set { _next = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物仪器项目默认检验步骤", ShortCode = "EPMicroEquipItemStepList", Desc = "微生物仪器项目默认检验步骤")]
        public virtual IList<EPMicroEquipItemStep> EPMicroEquipItemStepList
        {
            get
            {
                if (_ePMicroEquipItemStepList == null)
                {
                    _ePMicroEquipItemStepList = new List<EPMicroEquipItemStep>();
                }
                return _ePMicroEquipItemStepList;
            }
            set { _ePMicroEquipItemStepList = value; }
        }


        #endregion
    }
    #endregion
}