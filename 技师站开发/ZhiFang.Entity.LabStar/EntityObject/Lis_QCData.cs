using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisQCData

    /// <summary>
    /// LisQCData object for NHibernate mapped table 'Lis_QCData'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "质控数据", ClassCName = "LisQCData", ShortCode = "LisQCData", Desc = "质控数据")]
    public class LisQCData : BaseEntity
    {
        #region Member Variables

        protected DateTime _receiveTime;
        protected string _reportValue;
        protected double _quanValue;
        protected string _qCValueMemo;
        protected string _eValue;
        protected string _eResultStatus;
        protected string _eResultAlarm;
        protected bool _bUse;
        protected bool _bAnaly;
        protected string _loserule;
        protected string _loseType;
        protected string _loseReason;
        protected string _correctMeasure;
        protected string _correctValue;
        protected string _correctDesc;
        protected string _precaution;
        protected string _loseMemo;
        protected string _clinicalEffects;
        protected string _operateInfo;
        protected string _loseOperator;
        protected string _loseOperatorID;
        protected DateTime? _loseOperateTime;
        protected string _loseExaminer;
        protected long? _loseExaminerID;
        protected DateTime? _loseExamineTime;
        protected int _loseExamineState;
        protected string _examinInfo;
        protected int _mainStatusID;
        protected int _iSource;
        protected long? _statusID;
        protected bool _isUpload;
        protected int _plateNo;
        protected int _positionNo;
        protected long? _operatorID;
        protected string _operator;
        protected int _dispOrder;
        protected DateTime? _testTime;
        protected DateTime? _dataUpdateTime;
        protected LBQCItem _lBQCItem;


        #endregion

        #region Constructors

        public LisQCData() { }

        public LisQCData(long labID, DateTime receiveTime, string reportValue, double quanValue, string qCValueMemo, string eValue, string eResultStatus, string eResultAlarm, bool bUse, bool bAnaly, string loserule, string loseType, string loseReason, string correctMeasure, string correctValue, string correctDesc, string precaution, string loseMemo, string clinicalEffects, string operateInfo, string loseOperator, string loseOperatorID, DateTime loseOperateTime, string loseExaminer, long loseExaminerID, DateTime loseExamineTime, int loseExamineState, string examinInfo, int mainStatusID, int iSource, long statusID, bool isUpload, int plateNo, int positionNo, long operatorID, string _operator, int dispOrder, DateTime testTime, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBQCItem lBQCItem)
        {
            this._labID = labID;
            this._receiveTime = receiveTime;
            this._reportValue = reportValue;
            this._quanValue = quanValue;
            this._qCValueMemo = qCValueMemo;
            this._eValue = eValue;
            this._eResultStatus = eResultStatus;
            this._eResultAlarm = eResultAlarm;
            this._bUse = bUse;
            this._bAnaly = bAnaly;
            this._loserule = loserule;
            this._loseType = loseType;
            this._loseReason = loseReason;
            this._correctMeasure = correctMeasure;
            this._correctValue = correctValue;
            this._correctDesc = correctDesc;
            this._precaution = precaution;
            this._loseMemo = loseMemo;
            this._clinicalEffects = clinicalEffects;
            this._operateInfo = operateInfo;
            this._loseOperator = loseOperator;
            this._loseOperatorID = loseOperatorID;
            this._loseOperateTime = loseOperateTime;
            this._loseExaminer = loseExaminer;
            this._loseExaminerID = loseExaminerID;
            this._loseExamineTime = loseExamineTime;
            this._loseExamineState = loseExamineState;
            this._examinInfo = examinInfo;
            this._mainStatusID = mainStatusID;
            this._iSource = iSource;
            this._statusID = statusID;
            this._isUpload = isUpload;
            this._plateNo = plateNo;
            this._positionNo = positionNo;
            this._operatorID = operatorID;
            this._operator = _operator;
            this._dispOrder = dispOrder;
            this._testTime = testTime;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBQCItem = lBQCItem;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "质控时间", ShortCode = "ReceiveTime", Desc = "质控时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime ReceiveTime
        {
            get { return _receiveTime; }
            set { _receiveTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "报告值", ShortCode = "ReportValue", Desc = "报告值", ContextType = SysDic.All, Length = 100)]
        public virtual string ReportValue
        {
            get { return _reportValue; }
            set { _reportValue = value; }
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
        [DataDesc(CName = "质控结果备注", ShortCode = "QCValueMemo", Desc = "质控结果备注", ContextType = SysDic.All, Length = 100)]
        public virtual string QCValueMemo
        {
            get { return _qCValueMemo; }
            set { _qCValueMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器原始数值", ShortCode = "EValue", Desc = "仪器原始数值", ContextType = SysDic.All, Length = 300)]
        public virtual string EValue
        {
            get { return _eValue; }
            set { _eValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果状态", ShortCode = "EResultStatus", Desc = "仪器结果状态", ContextType = SysDic.All, Length = 50)]
        public virtual string EResultStatus
        {
            get { return _eResultStatus; }
            set { _eResultStatus = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果警告", ShortCode = "EResultAlarm", Desc = "仪器结果警告", ContextType = SysDic.All, Length = 50)]
        public virtual string EResultAlarm
        {
            get { return _eResultAlarm; }
            set { _eResultAlarm = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否采用", ShortCode = "BUse", Desc = "是否采用", ContextType = SysDic.All, Length = 1)]
        public virtual bool BUse
        {
            get { return _bUse; }
            set { _bUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否质控分析", ShortCode = "BAnaly", Desc = "是否质控分析", ContextType = SysDic.All, Length = 1)]
        public virtual bool BAnaly
        {
            get { return _bAnaly; }
            set { _bAnaly = value; }
        }

        [DataMember]
        [DataDesc(CName = "违反规则", ShortCode = "Loserule", Desc = "违反规则", ContextType = SysDic.All, Length = 50)]
        public virtual string Loserule
        {
            get { return _loserule; }
            set { _loserule = value; }
        }

        [DataMember]
        [DataDesc(CName = "失控类型", ShortCode = "LoseType", Desc = "失控类型", ContextType = SysDic.All, Length = 10)]
        public virtual string LoseType
        {
            get { return _loseType; }
            set { _loseType = value; }
        }

        [DataMember]
        [DataDesc(CName = "失控原因", ShortCode = "LoseReason", Desc = "失控原因", ContextType = SysDic.All, Length = 200)]
        public virtual string LoseReason
        {
            get { return _loseReason; }
            set { _loseReason = value; }
        }

        [DataMember]
        [DataDesc(CName = "纠正措施", ShortCode = "CorrectMeasure", Desc = "纠正措施", ContextType = SysDic.All, Length = 200)]
        public virtual string CorrectMeasure
        {
            get { return _correctMeasure; }
            set { _correctMeasure = value; }
        }

        [DataMember]
        [DataDesc(CName = "纠正值", ShortCode = "CorrectValue", Desc = "纠正值", ContextType = SysDic.All, Length = 100)]
        public virtual string CorrectValue
        {
            get { return _correctValue; }
            set { _correctValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "纠正说明", ShortCode = "CorrectDesc", Desc = "纠正说明", ContextType = SysDic.All, Length = 200)]
        public virtual string CorrectDesc
        {
            get { return _correctDesc; }
            set { _correctDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "预防措施", ShortCode = "Precaution", Desc = "预防措施", ContextType = SysDic.All, Length = 200)]
        public virtual string Precaution
        {
            get { return _precaution; }
            set { _precaution = value; }
        }

        [DataMember]
        [DataDesc(CName = "失控备注", ShortCode = "LoseMemo", Desc = "失控备注", ContextType = SysDic.All, Length = 200)]
        public virtual string LoseMemo
        {
            get { return _loseMemo; }
            set { _loseMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "临床影响", ShortCode = "ClinicalEffects", Desc = "临床影响", ContextType = SysDic.All, Length = 200)]
        public virtual string ClinicalEffects
        {
            get { return _clinicalEffects; }
            set { _clinicalEffects = value; }
        }

        [DataMember]
        [DataDesc(CName = "处理意见", ShortCode = "OperateInfo", Desc = "处理意见", ContextType = SysDic.All, Length = 200)]
        public virtual string OperateInfo
        {
            get { return _operateInfo; }
            set { _operateInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "失控处理人", ShortCode = "LoseOperator", Desc = "失控处理人", ContextType = SysDic.All, Length = 50)]
        public virtual string LoseOperator
        {
            get { return _loseOperator; }
            set { _loseOperator = value; }
        }

        [DataMember]
        [DataDesc(CName = "失控处理人ID", ShortCode = "LoseOperatorID", Desc = "失控处理人ID", ContextType = SysDic.All, Length = 50)]
        public virtual string LoseOperatorID
        {
            get { return _loseOperatorID; }
            set { _loseOperatorID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失控处理时间", ShortCode = "LoseOperateTime", Desc = "失控处理时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LoseOperateTime
        {
            get { return _loseOperateTime; }
            set { _loseOperateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "失控审核人", ShortCode = "LoseExaminer", Desc = "失控审核人", ContextType = SysDic.All, Length = 50)]
        public virtual string LoseExaminer
        {
            get { return _loseExaminer; }
            set { _loseExaminer = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失控审核人ID", ShortCode = "LoseExaminerID", Desc = "失控审核人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LoseExaminerID
        {
            get { return _loseExaminerID; }
            set { _loseExaminerID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失控审核时间", ShortCode = "LoseExamineTime", Desc = "失控审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LoseExamineTime
        {
            get { return _loseExamineTime; }
            set { _loseExamineTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "失控处理状态", ShortCode = "LoseExamineState", Desc = "失控处理状态", ContextType = SysDic.All, Length = 4)]
        public virtual int LoseExamineState
        {
            get { return _loseExamineState; }
            set { _loseExamineState = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核意见", ShortCode = "ExaminInfo", Desc = "审核意见", ContextType = SysDic.All, Length = 200)]
        public virtual string ExaminInfo
        {
            get { return _examinInfo; }
            set { _examinInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "主状态", ShortCode = "MainStatusID", Desc = "主状态", ContextType = SysDic.All, Length = 4)]
        public virtual int MainStatusID
        {
            get { return _mainStatusID; }
            set { _mainStatusID = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目来源", ShortCode = "ISource", Desc = "项目来源", ContextType = SysDic.All, Length = 4)]
        public virtual int ISource
        {
            get { return _iSource; }
            set { _iSource = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "过程状态标志", ShortCode = "StatusID", Desc = "过程状态标志", ContextType = SysDic.All, Length = 8)]
        public virtual long? StatusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否提交", ShortCode = "IsUpload", Desc = "是否提交", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUpload
        {
            get { return _isUpload; }
            set { _isUpload = value; }
        }

        [DataMember]
        [DataDesc(CName = "板号", ShortCode = "PlateNo", Desc = "板号", ContextType = SysDic.All, Length = 4)]
        public virtual int PlateNo
        {
            get { return _plateNo; }
            set { _plateNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "位置号", ShortCode = "PositionNo", Desc = "位置号", ContextType = SysDic.All, Length = 4)]
        public virtual int PositionNo
        {
            get { return _positionNo; }
            set { _positionNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作者ID", ShortCode = "OperatorID", Desc = "操作者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作者", ShortCode = "Operator", Desc = "操作者", ContextType = SysDic.All, Length = 20)]
        public virtual string Operator
        {
            get { return _operator; }
            set { _operator = value; }
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
        [DataDesc(CName = "检验时间", ShortCode = "TestTime", Desc = "检验时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TestTime
        {
            get { return _testTime; }
            set { _testTime = value; }
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
        [DataDesc(CName = "", ShortCode = "LBQCItem", Desc = "")]
        public virtual LBQCItem LBQCItem
        {
            get { return _lBQCItem; }
            set { _lBQCItem = value; }
        }


        #endregion
    }
    #endregion
}