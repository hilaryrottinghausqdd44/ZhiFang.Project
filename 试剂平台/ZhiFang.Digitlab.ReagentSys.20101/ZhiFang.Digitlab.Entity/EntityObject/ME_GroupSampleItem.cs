using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEGroupSampleItem

    /// <summary>
    /// MEGroupSampleItem object for NHibernate mapped table 'ME_GroupSampleItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "小组样本项目", ClassCName = "MEGroupSampleItem", ShortCode = "MEGroupSampleItem", Desc = "小组样本项目")]
    public class MEGroupSampleItem : BaseEntity
    {
        #region Member Variables

        protected int _testType;
        protected long? _parentResultID;
        protected long? _reCheckFormID;
        protected string _origlValue;
        protected int _valueType;
        protected string _reportValue;
        protected string _resultStatus;
        protected double? _quanValue;
        protected double? _quanValue2;
        protected double? _quanValue3;
        protected string _units;
        protected string _refRange;
        protected string _preValue;
        protected string _preValueComp;
        protected string _preCompStatus;
        protected string _testMethod;
        protected int _alarmLevel;
        protected int _resultLinkType;
        protected string _resultLink;
        protected string _simpleResultLink;
        protected string _resultComment;
        protected bool _deleteFlag;
        protected bool _isDuplicate;
        protected int _isHandEditStatus;
        protected int _testFlag;
        protected int _checkFlag;
        protected int _reportFlag;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected string _quanValueComparison;
        protected bool _isExcess;
        protected int? _commState;
        protected MEEquipSampleItem _mEEquipSampleItem;
        protected MEGroupSampleForm _mEGroupSampleForm;
        protected ItemAllItem _itemAllItem;
        protected ItemAllItem _pItemAllItem;
        protected MEImmuneResults _mEImmuneResults;
        protected MEPTSampleItem _mEPTSampleItem;
        protected IList<MEMicroDSTValue> _mEMicroDSTValueList;
        protected IList<MEMicroSmearValue> _mEMicroSmearValueList;

        #endregion

        #region Constructors

        public MEGroupSampleItem() { }

        public MEGroupSampleItem(long labID, int testType, long parentResultID, long reCheckFormID, string origlValue, int valueType, string reportValue, string resultStatus, double quanValue, double quanValue2, double quanValue3, string units, string refRange, string preValue, string preValueComp, string preCompStatus, string testMethod, int alarmLevel, int resultLinkType, string resultLink, string simpleResultLink, string resultComment, bool deleteFlag, bool isDuplicate, int isHandEditStatus, int testFlag, int checkFlag, int reportFlag, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string quanValueComparison, bool isExcess, int commState, MEEquipSampleItem mEEquipSampleItem, MEGroupSampleForm mEGroupSampleForm, ItemAllItem itemAllItem, ItemAllItem p, MEImmuneResults mEImmuneResults, MEPTSampleItem mEPTSampleItem)
        {
            this._labID = labID;
            this._testType = testType;
            this._parentResultID = parentResultID;
            this._reCheckFormID = reCheckFormID;
            this._origlValue = origlValue;
            this._valueType = valueType;
            this._reportValue = reportValue;
            this._resultStatus = resultStatus;
            this._quanValue = quanValue;
            this._quanValue2 = quanValue2;
            this._quanValue3 = quanValue3;
            this._units = units;
            this._refRange = refRange;
            this._preValue = preValue;
            this._preValueComp = preValueComp;
            this._preCompStatus = preCompStatus;
            this._testMethod = testMethod;
            this._alarmLevel = alarmLevel;
            this._resultLinkType = resultLinkType;
            this._resultLink = resultLink;
            this._simpleResultLink = simpleResultLink;
            this._resultComment = resultComment;
            this._deleteFlag = deleteFlag;
            this._isDuplicate = isDuplicate;
            this._isHandEditStatus = isHandEditStatus;
            this._testFlag = testFlag;
            this._checkFlag = checkFlag;
            this._reportFlag = reportFlag;
            this._zDY1 = zDY1;
            this._zDY2 = zDY2;
            this._zDY3 = zDY3;
            this._zDY4 = zDY4;
            this._zDY5 = zDY5;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._quanValueComparison = quanValueComparison;
            this._isExcess = isExcess;
            this._commState = commState;
            this._mEEquipSampleItem = mEEquipSampleItem;
            this._mEGroupSampleForm = mEGroupSampleForm;
            this._itemAllItem = itemAllItem;
            this._pItemAllItem = p;
            this._mEImmuneResults = mEImmuneResults;
            this._mEPTSampleItem = mEPTSampleItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "检验结果类型", ShortCode = "TestType", Desc = "检验结果类型", ContextType = SysDic.All, Length = 4)]
        public virtual int TestType
        {
            get { return _testType; }
            set { _testType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "父结果ID", ShortCode = "ParentResultID", Desc = "父结果ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ParentResultID
        {
            get { return _parentResultID; }
            set { _parentResultID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReCheckFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReCheckFormID
        {
            get { return _reCheckFormID; }
            set { _reCheckFormID = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器原始数值", ShortCode = "OriglValue", Desc = "仪器原始数值", ContextType = SysDic.All, Length = 40)]
        public virtual string OriglValue
        {
            get { return _origlValue; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for OriglValue", value, value.ToString());
                _origlValue = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "结果类型", ShortCode = "ValueType", Desc = "结果类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ValueType
        {
            get { return _valueType; }
            set { _valueType = value; }
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
        public virtual double? QuanValue
        {
            get { return _quanValue; }
            set { _quanValue = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定量辅助结果2", ShortCode = "QuanValue2", Desc = "定量辅助结果2", ContextType = SysDic.All, Length = 8)]
        public virtual double? QuanValue2
        {
            get { return _quanValue2; }
            set { _quanValue2 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定量辅助结果3", ShortCode = "QuanValue3", Desc = "定量辅助结果3", ContextType = SysDic.All, Length = 8)]
        public virtual double? QuanValue3
        {
            get { return _quanValue3; }
            set { _quanValue3 = value; }
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
        [DataDesc(CName = "参考范围", ShortCode = "RefRange", Desc = "参考范围", ContextType = SysDic.All, Length = 400)]
        public virtual string RefRange
        {
            get { return _refRange; }
            set
            {
                _refRange = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "前报告值", ShortCode = "PreValue", Desc = "前报告值", ContextType = SysDic.All, Length = 300)]
        public virtual string PreValue
        {
            get { return _preValue; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for PreValue", value, value.ToString());
                _preValue = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "前值对比", ShortCode = "PreValueComp", Desc = "前值对比", ContextType = SysDic.All, Length = 50)]
        public virtual string PreValueComp
        {
            get { return _preValueComp; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PreValueComp", value, value.ToString());
                _preValueComp = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "前值对比状态", ShortCode = "PreCompStatus", Desc = "前值对比状态", ContextType = SysDic.All, Length = 20)]
        public virtual string PreCompStatus
        {
            get { return _preCompStatus; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PreCompStatus", value, value.ToString());
                _preCompStatus = value;
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
        [DataDesc(CName = "结果值链接类型", ShortCode = "ResultLinkType", Desc = "结果值链接类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ResultLinkType
        {
            get { return _resultLinkType; }
            set { _resultLinkType = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果值链接", ShortCode = "ResultLink", Desc = "结果值链接", ContextType = SysDic.All, Length = 100)]
        public virtual string ResultLink
        {
            get { return _resultLink; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ResultLink", value, value.ToString());
                _resultLink = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "结果缩略链接", ShortCode = "SimpleResultLink", Desc = "结果缩略链接", ContextType = SysDic.All, Length = 100)]
        public virtual string SimpleResultLink
        {
            get { return _simpleResultLink; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for SimpleResultLink", value, value.ToString());
                _simpleResultLink = value;
            }
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
        [DataDesc(CName = "删除标志", ShortCode = "DeleteFlag", Desc = "删除标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否复制项目", ShortCode = "IsDuplicate", Desc = "是否复制项目", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDuplicate
        {
            get { return _isDuplicate; }
            set { _isDuplicate = value; }
        }

        [DataMember]
        [DataDesc(CName = "人工编辑状态", ShortCode = "IsHandEditStatus", Desc = "人工编辑状态", ContextType = SysDic.All, Length = 4)]
        public virtual int IsHandEditStatus
        {
            get { return _isHandEditStatus; }
            set { _isHandEditStatus = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验确认状态", ShortCode = "TestFlag", Desc = "检验确认状态", ContextType = SysDic.All, Length = 4)]
        public virtual int TestFlag
        {
            get { return _testFlag; }
            set { _testFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核状态", ShortCode = "CheckFlag", Desc = "审核状态", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
        {
            get { return _checkFlag; }
            set { _checkFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否报告状态", ShortCode = "ReportFlag", Desc = "是否报告状态", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportFlag
        {
            get { return _reportFlag; }
            set { _reportFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "自定义1", ShortCode = "ZDY1", Desc = "自定义1", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY1
        {
            get { return _zDY1; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
                _zDY1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义2", ShortCode = "ZDY2", Desc = "自定义2", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY2
        {
            get { return _zDY2; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
                _zDY2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义3", ShortCode = "ZDY3", Desc = "自定义3", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY3
        {
            get { return _zDY3; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
                _zDY3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义4", ShortCode = "ZDY4", Desc = "自定义4", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY4
        {
            get { return _zDY4; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
                _zDY4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义5", ShortCode = "ZDY5", Desc = "自定义5", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY5
        {
            get { return _zDY5; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
                _zDY5 = value;
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
        [DataDesc(CName = "", ShortCode = "QuanValueComparison", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string QuanValueComparison
        {
            get { return _quanValueComparison; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for QuanValueComparison", value, value.ToString());
                _quanValueComparison = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否多做", ShortCode = "IsExcess", Desc = "是否多做", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsExcess
        {
            get { return _isExcess; }
            set { _isExcess = value; }
        }

        [DataMember]
        [DataDesc(CName = "技师站发送项目标记：0或null未发送，1已发送", ShortCode = "CommState", Desc = "技师站发送项目标记：0或null未发送，1已发送", ContextType = SysDic.All, Length = 4)]
        public virtual int? CommState
        {
            get { return _commState; }
            set { _commState = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器样本项目", ShortCode = "MEEquipSampleItem", Desc = "仪器样本项目")]
        public virtual MEEquipSampleItem MEEquipSampleItem
        {
            get { return _mEEquipSampleItem; }
            set { _mEEquipSampleItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
        public virtual MEGroupSampleForm MEGroupSampleForm
        {
            get { return _mEGroupSampleForm; }
            set { _mEGroupSampleForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
        public virtual ItemAllItem ItemAllItem
        {
            get { return _itemAllItem; }
            set { _itemAllItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "P", Desc = "所有项目")]
        public virtual ItemAllItem PItemAllItem
        {
            get { return _pItemAllItem; }
            set { _pItemAllItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "存储酶免板的96个孔的孔号、标本类型、检验项目、检验结果信息", ShortCode = "MEImmuneResults", Desc = "存储酶免板的96个孔的孔号、标本类型、检验项目、检验结果信息")]
        public virtual MEImmuneResults MEImmuneResults
        {
            get { return _mEImmuneResults; }
            set { _mEImmuneResults = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本单项目", ShortCode = "MEPTSampleItem", Desc = "样本单项目")]
        public virtual MEPTSampleItem MEPTSampleItem
        {
            get { return _mEPTSampleItem; }
            set { _mEPTSampleItem = value; }
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
        [DataDesc(CName = "微生物涂片结果", ShortCode = "MEMicroSmearValueList", Desc = "微生物涂片结果")]
        public virtual IList<MEMicroSmearValue> MEMicroSmearValueList
        {
            get
            {
                if (_mEMicroSmearValueList == null)
                {
                    _mEMicroSmearValueList = new List<MEMicroSmearValue>();
                }
                return _mEMicroSmearValueList;
            }
            set { _mEMicroSmearValueList = value; }
        }

        #endregion
    }
    #endregion
}