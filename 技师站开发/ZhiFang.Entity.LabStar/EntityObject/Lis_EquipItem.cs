using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisEquipItem

    /// <summary>
    /// LisEquipItem object for NHibernate mapped table 'Lis_EquipItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "仪器样本项目", ClassCName = "LisEquipItem", ShortCode = "LisEquipItem", Desc = "仪器样本项目")]
    public class LisEquipItem : BaseEntity
    {
        #region Member Variables

        protected long? _testItemID;
        protected DateTime? _eTestDate;
        protected int _iExamine;
        protected bool _bRedo;
        protected int _testType;
        protected string _itemChannel;
        protected string _eReportValue;
        protected string _eResultStatus;
        protected string _eResultAlarm;
        protected string _eTestComment;
        protected string _eSend;
        protected string _eOriginalValue;
        protected string _eOriginalResultStatus;
        protected string _eOriginalResultAlarm;
        protected string _eOriginalSend;
        protected string _eOriginalTestComment;
        protected string _eReagentInfo;
        protected int _eTestState;
        protected int _eAlarmState;
        protected bool _bItemResultFlag;
        protected bool _bResultDowith;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected DateTime? _testTime;
        protected LisEquipForm _lisEquipForm;
        protected LBItem _lBItem;


        #endregion

        #region Constructors

        public LisEquipItem() { }

        public LisEquipItem(long labID, long testItemID, DateTime eTestDate, int iExamine, bool bRedo, int testType, string itemChannel, string eReportValue, string eResultStatus, string eResultAlarm, string eTestComment, string eSend, string eOriginalValue, string eOriginalResultStatus, string eOriginalResultAlarm, string eOriginalSend, string eOriginalTestComment, string eReagentInfo, int eTestState, int eAlarmState, bool bItemResultFlag, bool bResultDowith, string zDY1, string zDY2, string zDY3, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, DateTime testTime, byte[] dataTimeStamp, LisEquipForm lisEquipForm, LBItem lBItem)
        {
            this._labID = labID;
            this._testItemID = testItemID;
            this._eTestDate = eTestDate;
            this._iExamine = iExamine;
            this._bRedo = bRedo;
            this._testType = testType;
            this._itemChannel = itemChannel;
            this._eReportValue = eReportValue;
            this._eResultStatus = eResultStatus;
            this._eResultAlarm = eResultAlarm;
            this._eTestComment = eTestComment;
            this._eSend = eSend;
            this._eOriginalValue = eOriginalValue;
            this._eOriginalResultStatus = eOriginalResultStatus;
            this._eOriginalResultAlarm = eOriginalResultAlarm;
            this._eOriginalSend = eOriginalSend;
            this._eOriginalTestComment = eOriginalTestComment;
            this._eReagentInfo = eReagentInfo;
            this._eTestState = eTestState;
            this._eAlarmState = eAlarmState;
            this._bItemResultFlag = bItemResultFlag;
            this._bResultDowith = bResultDowith;
            this._zDY1 = zDY1;
            this._zDY2 = zDY2;
            this._zDY3 = zDY3;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._testTime = testTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lisEquipForm = lisEquipForm;
            this._lBItem = lBItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "小组项目结果ID", ShortCode = "TestItemID", Desc = "小组项目结果ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TestItemID
        {
            get { return _testItemID; }
            set { _testItemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器检验日期", ShortCode = "ETestDate", Desc = "仪器检验日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ETestDate
        {
            get { return _eTestDate; }
            set { _eTestDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "检查次数", ShortCode = "IExamine", Desc = "检查次数", ContextType = SysDic.All, Length = 4)]
        public virtual int IExamine
        {
            get { return _iExamine; }
            set { _iExamine = value; }
        }

        [DataMember]
        [DataDesc(CName = "复检", ShortCode = "BRedo", Desc = "复检", ContextType = SysDic.All, Length = 1)]
        public virtual bool BRedo
        {
            get { return _bRedo; }
            set { _bRedo = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验结果类型", ShortCode = "TestType", Desc = "检验结果类型", ContextType = SysDic.All, Length = 4)]
        public virtual int TestType
        {
            get { return _testType; }
            set { _testType = value; }
        }

        [DataMember]
        [DataDesc(CName = "通道号", ShortCode = "ItemChannel", Desc = "通道号", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemChannel
        {
            get { return _itemChannel; }
            set { _itemChannel = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果", ShortCode = "EReportValue", Desc = "仪器结果", ContextType = SysDic.All, Length = 300)]
        public virtual string EReportValue
        {
            get { return _eReportValue; }
            set { _eReportValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果状态", ShortCode = "EResultStatus", Desc = "仪器结果状态", ContextType = SysDic.All, Length = 50)]
        public virtual string EResultStatus
        {
            get { return _eResultStatus; }
            set { _eResultStatus = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果警告", ShortCode = "EResultAlarm", Desc = "仪器结果警告", ContextType = SysDic.All, Length = 200)]
        public virtual string EResultAlarm
        {
            get { return _eResultAlarm; }
            set { _eResultAlarm = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果备注", ShortCode = "ETestComment", Desc = "仪器结果备注", ContextType = SysDic.All, Length = 16)]
        public virtual string ETestComment
        {
            get { return _eTestComment; }
            set { _eTestComment = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器审核信息", ShortCode = "ESend", Desc = "仪器审核信息", ContextType = SysDic.All, Length = 100)]
        public virtual string ESend
        {
            get { return _eSend; }
            set { _eSend = value; }
        }

        [DataMember]
        [DataDesc(CName = "通讯原始结果", ShortCode = "EOriginalValue", Desc = "通讯原始结果", ContextType = SysDic.All, Length = 300)]
        public virtual string EOriginalValue
        {
            get { return _eOriginalValue; }
            set { _eOriginalValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "通讯原始结果状态", ShortCode = "EOriginalResultStatus", Desc = "通讯原始结果状态", ContextType = SysDic.All, Length = 50)]
        public virtual string EOriginalResultStatus
        {
            get { return _eOriginalResultStatus; }
            set { _eOriginalResultStatus = value; }
        }

        [DataMember]
        [DataDesc(CName = "通讯原始结果警告", ShortCode = "EOriginalResultAlarm", Desc = "通讯原始结果警告", ContextType = SysDic.All, Length = 200)]
        public virtual string EOriginalResultAlarm
        {
            get { return _eOriginalResultAlarm; }
            set { _eOriginalResultAlarm = value; }
        }

        [DataMember]
        [DataDesc(CName = "通讯原始审核信息", ShortCode = "EOriginalSend", Desc = "通讯原始审核信息", ContextType = SysDic.All, Length = 100)]
        public virtual string EOriginalSend
        {
            get { return _eOriginalSend; }
            set { _eOriginalSend = value; }
        }

        [DataMember]
        [DataDesc(CName = "通讯原始结果备注", ShortCode = "EOriginalTestComment", Desc = "通讯原始结果备注", ContextType = SysDic.All, Length = 16)]
        public virtual string EOriginalTestComment
        {
            get { return _eOriginalTestComment; }
            set { _eOriginalTestComment = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器试剂信息", ShortCode = "EReagentInfo", Desc = "仪器试剂信息", ContextType = SysDic.All, Length = 100)]
        public virtual string EReagentInfo
        {
            get { return _eReagentInfo; }
            set { _eReagentInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器检测状态", ShortCode = "ETestState", Desc = "仪器检测状态", ContextType = SysDic.All, Length = 4)]
        public virtual int ETestState
        {
            get { return _eTestState; }
            set { _eTestState = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果警示状态", ShortCode = "EAlarmState", Desc = "仪器结果警示状态", ContextType = SysDic.All, Length = 4)]
        public virtual int EAlarmState
        {
            get { return _eAlarmState; }
            set { _eAlarmState = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果采用标志", ShortCode = "BItemResultFlag", Desc = "结果采用标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool BItemResultFlag
        {
            get { return _bItemResultFlag; }
            set { _bItemResultFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果处理标志", ShortCode = "BResultDowith", Desc = "结果处理标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool BResultDowith
        {
            get { return _bResultDowith; }
            set { _bResultDowith = value; }
        }

        [DataMember]
        [DataDesc(CName = "自定义1", ShortCode = "ZDY1", Desc = "自定义1", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY1
        {
            get { return _zDY1; }
            set { _zDY1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "自定义2", ShortCode = "ZDY2", Desc = "自定义2", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY2
        {
            get { return _zDY2; }
            set { _zDY2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "自定义3", ShortCode = "ZDY3", Desc = "自定义3", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY3
        {
            get { return _zDY3; }
            set { _zDY3 = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验时间", ShortCode = "TestTime", Desc = "检验时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TestTime
        {
            get { return _testTime; }
            set { _testTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器样本单", ShortCode = "LisEquipForm", Desc = "仪器样本单")]
        public virtual LisEquipForm LisEquipForm
        {
            get { return _lisEquipForm; }
            set { _lisEquipForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
        public virtual LBItem LBItem
        {
            get { return _lBItem; }
            set { _lBItem = value; }
        }


        #endregion
    }
    #endregion
}