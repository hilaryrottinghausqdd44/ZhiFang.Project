using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    #region GKSampleRequestForm

    /// <summary>
    /// GKSampleRequestForm object for NHibernate mapped table 'GK_SampleRequestForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "GKSampleRequestForm", ShortCode = "GKSampleRequestForm", Desc = "")]
    public class GKSampleRequestForm : BaseEntity
    {
        #region Member Variables

        protected string _reqDocNo;
        protected long? _deptId;
        protected long? _monitorType;

        protected string _sampler;
        protected long? _samplerId;
        protected string _deptCName;
        protected DateTime? _sampleDate;
        protected DateTime? _sampleTime;
        protected long? _statusID;
        protected string _cName;
        protected string _barCode;
        protected string _sampleNo;
        protected int _printCount;

        protected bool _resultFlag;
        protected long? _creatorID;
        protected string _creatorName;

        protected bool _isAutoReceive;
        protected bool _receiveFlag;
        protected DateTime? _receiveDate;
        protected long? _receiveId;
        protected string _receiveCName;

        protected long? _testerId;
        protected string _testerName;
        protected DateTime? _testTime;
        protected string _testResult;
        protected string _bacteriaTotal;

        protected long? _evaluatorId;
        protected string _evaluators;
        protected DateTime? _evaluationDate;
        protected bool _evaluatorFlag;

        protected long? _checkId;
        protected string _checkCName;
        protected DateTime? _checkDate;

        protected string _judgment;
        protected bool _archived;
        protected bool _visible;
        protected string _memo;
        protected int _dispOrder;

        protected long? _obsoleteID;
        protected string _obsoleteName;
        protected DateTime? _obsoleteTime;
        protected string _obsoleteMemo;

        protected BDict _sRFormObsolete;
        protected SCRecordType _sCRecordType;

        #endregion

        #region Constructors

        public GKSampleRequestForm() { }

        public GKSampleRequestForm(long labID, string reqDocNo, long deptId, string deptCName, long samplerId, string sampler, DateTime sampleDate, DateTime sampleTime, int statusID, string barCode, int printCount, bool receiveFlag, bool resultFlag, long creatorID, string creatorName, long testerId, string testerName, DateTime testTime, string testResult, long evaluatorId, string evaluators, DateTime evaluationDate, string judgment, bool visible, string memo, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, SCRecordType sCRecordType)
        {
            this._labID = labID;
            this._reqDocNo = reqDocNo;
            this._deptId = deptId;
            this._deptCName = deptCName;
            this._samplerId = samplerId;
            this._sampler = sampler;
            this._sampleDate = sampleDate;
            this._sampleTime = sampleTime;
            this._statusID = statusID;
            this._barCode = barCode;
            this._printCount = printCount;
            this._receiveFlag = receiveFlag;
            this._resultFlag = resultFlag;
            this._creatorID = creatorID;
            this._creatorName = creatorName;
            this._testerId = testerId;
            this._testerName = testerName;
            this._testTime = testTime;
            this._testResult = testResult;
            this._evaluatorId = evaluatorId;
            this._evaluators = evaluators;
            this._evaluationDate = evaluationDate;
            this._judgment = judgment;
            this._visible = visible;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._sCRecordType = sCRecordType;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ReceiveId", ShortCode = "ReceiveId", Desc = "ReceiveId", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReceiveId
        {
            get { return _receiveId; }
            set { _receiveId = value; }
        }

        [DataMember]
        [DataDesc(CName = "ReceiveCName", ShortCode = "ReceiveCName", Desc = "ReceiveCName", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceiveCName
        {
            get { return _receiveCName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiveCName", value, value.ToString());
                _receiveCName = value;
            }
        }

        /// <summary>
        /// “感控监测类型”：1:感控监测;2:科室监测;
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "感控监测类型", ShortCode = "MonitorType", Desc = "感控监测类型", ContextType = SysDic.Number, Length = 8)]
        public virtual long? MonitorType
        {
            get { return _monitorType; }
            set { _monitorType = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ReceiveDate", ShortCode = "ReceiveDate", Desc = "ReceiveDate", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveDate
        {
            get { return _receiveDate; }
            set { _receiveDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "ReqDocNo", ShortCode = "ReqDocNo", Desc = "ReqDocNo", ContextType = SysDic.All, Length = 50)]
        public virtual string ReqDocNo
        {
            get { return _reqDocNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ReqDocNo", value, value.ToString());
                _reqDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "DeptId", ShortCode = "DeptId", Desc = "DeptId", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptId
        {
            get { return _deptId; }
            set { _deptId = value; }
        }
        [DataMember]
        [DataDesc(CName = "DeptCName", ShortCode = "DeptCName", Desc = "DeptCName", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptCName
        {
            get { return _deptCName; }
            set { _deptCName = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "SamplerId", ShortCode = "SamplerId", Desc = "SamplerId", ContextType = SysDic.All, Length = 8)]
        public virtual long? SamplerId
        {
            get { return _samplerId; }
            set { _samplerId = value; }
        }

        [DataMember]
        [DataDesc(CName = "Sampler", ShortCode = "Sampler", Desc = "Sampler", ContextType = SysDic.All, Length = 50)]
        public virtual string Sampler
        {
            get { return _sampler; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Sampler", value, value.ToString());
                _sampler = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "SampleDate", ShortCode = "SampleDate", Desc = "SampleDate", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SampleDate
        {
            get { return _sampleDate; }
            set { _sampleDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "SampleTime", ShortCode = "SampleTime", Desc = "SampleTime", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SampleTime
        {
            get { return _sampleTime; }
            set { _sampleTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "StatusID", ShortCode = "StatusID", Desc = "StatusID", ContextType = SysDic.All, Length = 4)]
        public virtual long? StatusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }
        /// <summary>
        /// 姓名，冗余字段，取监测类型的第一个记录项录入值
        /// </summary>
        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "BarCode", ShortCode = "BarCode", Desc = "BarCode", ContextType = SysDic.All, Length = 50)]
        public virtual string BarCode
        {
            get { return _barCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
                _barCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "SampleNo", ShortCode = "SampleNo", Desc = "SampleNo", ContextType = SysDic.All, Length = 200)]
        public virtual string SampleNo
        {
            get { return _sampleNo; }
            set
            {
                _sampleNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "PrintCount", ShortCode = "PrintCount", Desc = "PrintCount", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
        }
        [DataMember]
        [DataDesc(CName = "IsAutoReceive", ShortCode = "IsAutoReceive", Desc = "IsAutoReceive", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsAutoReceive
        {
            get { return _isAutoReceive; }
            set { _isAutoReceive = value; }
        }
        [DataMember]
        [DataDesc(CName = "ReceiveFlag", ShortCode = "ReceiveFlag", Desc = "ReceiveFlag", ContextType = SysDic.All, Length = 1)]
        public virtual bool ReceiveFlag
        {
            get { return _receiveFlag; }
            set { _receiveFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "ResultFlag", ShortCode = "ResultFlag", Desc = "ResultFlag", ContextType = SysDic.All, Length = 1)]
        public virtual bool ResultFlag
        {
            get { return _resultFlag; }
            set { _resultFlag = value; }
        }
        [DataMember]
        [DataDesc(CName = "EvaluatorFlag", ShortCode = "EvaluatorFlag", Desc = "EvaluatorFlag", ContextType = SysDic.All, Length = 1)]
        public virtual bool EvaluatorFlag
        {
            get { return _evaluatorFlag; }
            set { _evaluatorFlag = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "CreatorID", ShortCode = "CreatorID", Desc = "CreatorID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
        {
            get { return _creatorID; }
            set { _creatorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "CreatorName", ShortCode = "CreatorName", Desc = "CreatorName", ContextType = SysDic.All, Length = 50)]
        public virtual string CreatorName
        {
            get { return _creatorName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
                _creatorName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "TesterId", ShortCode = "TesterId", Desc = "TesterId", ContextType = SysDic.All, Length = 8)]
        public virtual long? TesterId
        {
            get { return _testerId; }
            set { _testerId = value; }
        }

        [DataMember]
        [DataDesc(CName = "TesterName", ShortCode = "TesterName", Desc = "TesterName", ContextType = SysDic.All, Length = 50)]
        public virtual string TesterName
        {
            get { return _testerName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TesterName", value, value.ToString());
                _testerName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "TestTime", ShortCode = "TestTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TestTime
        {
            get { return _testTime; }
            set { _testTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "TestResult", ShortCode = "TestResult", Desc = "TestResult", ContextType = SysDic.All, Length = 500)]
        public virtual string TestResult
        {
            get { return _testResult; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for TestResult", value, value.ToString());
                _testResult = value;
            }
        }
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "BacteriaTotal", ShortCode = "BacteriaTotal", Desc = "BacteriaTotal", ContextType = SysDic.All, Length = 500)]
        public virtual string BacteriaTotal
        {
            get { return _bacteriaTotal; }
            set { _bacteriaTotal = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "CheckId", ShortCode = "CheckId", Desc = "CheckId", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckId
        {
            get { return _checkId; }
            set { _checkId = value; }
        }

        [DataMember]
        [DataDesc(CName = "CheckCName", ShortCode = "CheckCName", Desc = "CheckCName", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckCName
        {
            get { return _checkCName; }
            set
            {
                _checkCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "CheckDate", ShortCode = "CheckDate", Desc = "CheckDate", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckDate
        {
            get { return _checkDate; }
            set { _checkDate = value; }
        }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "EvaluatorId", ShortCode = "EvaluatorId", Desc = "EvaluatorId", ContextType = SysDic.All, Length = 8)]
        public virtual long? EvaluatorId
        {
            get { return _evaluatorId; }
            set { _evaluatorId = value; }
        }

        [DataMember]
        [DataDesc(CName = "Evaluators", ShortCode = "Evaluators", Desc = "Evaluators", ContextType = SysDic.All, Length = 50)]
        public virtual string Evaluators
        {
            get { return _evaluators; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Evaluators", value, value.ToString());
                _evaluators = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "EvaluationDate", ShortCode = "EvaluationDate", Desc = "EvaluationDate", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EvaluationDate
        {
            get { return _evaluationDate; }
            set { _evaluationDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "Judgment", ShortCode = "Judgment", Desc = "Judgment", ContextType = SysDic.All, Length = 50)]
        public virtual string Judgment
        {
            get { return _judgment; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Judgment", value, value.ToString());
                _judgment = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "Visible", ShortCode = "Visible", Desc = "Visible", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        [DataMember]
        [DataDesc(CName = "Archived", ShortCode = "归档标志", Desc = "Archived", ContextType = SysDic.All, Length = 1)]
        public virtual bool Archived
        {
            get { return _archived; }
            set { _archived = value; }
        }

        [DataMember]
        [DataDesc(CName = "Memo", ShortCode = "Memo", Desc = "Memo", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "DispOrder", ShortCode = "DispOrder", Desc = "DispOrder", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废人Id", ShortCode = "ObsoleteID", Desc = "作废人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ObsoleteID
        {
            get { return _obsoleteID; }
            set { _obsoleteID = value; }
        }

        [DataMember]
        [DataDesc(CName = "作废人", ShortCode = "ObsoleteName", Desc = "作废人", ContextType = SysDic.All, Length = 20)]
        public virtual string ObsoleteName
        {
            get { return _obsoleteName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ObsoleteName", value, value.ToString());
                _obsoleteName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废时间", ShortCode = "ObsoleteTime", Desc = "作废时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ObsoleteTime
        {
            get { return _obsoleteTime; }
            set { _obsoleteTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "作废备注", ShortCode = "ObsoleteMemo", Desc = "作废备注", ContextType = SysDic.All, Length = 200)]
        public virtual string ObsoleteMemo
        {
            get { return _obsoleteMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ObsoleteMemo", value, value.ToString());
                _obsoleteMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "作废原因", ShortCode = "SRFormObsolete", Desc = "作废原因")]
        public virtual BDict SRFormObsolete
        {
            get { return _sRFormObsolete; }
            set { _sRFormObsolete = value; }

        }
        [DataMember]
        [DataDesc(CName = "SCRecordType", ShortCode = "SCRecordType", Desc = "SCRecordType")]
        public virtual SCRecordType SCRecordType
        {
            get { return _sCRecordType; }
            set { _sCRecordType = value; }
        }
        #endregion

        #region 自定义属性
        /// <summary>
        /// 记录项明细集合信息(JObject字符串值)
        /// </summary>
        [DataMember]
        [DataDesc(CName = "DtlJArray", ShortCode = "DtlJArray", Desc = "DtlJArray", ContextType = SysDic.All)]
        public virtual string DtlJArray { get; set; }

        [DataMember]
        [DataDesc(CName = "监测类型", ShortCode = "RecordTypeCName", Desc = "监测类型", ContextType = SysDic.All)]
        public virtual string RecordTypeCName { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestItemCName", Desc = "", ContextType = SysDic.All)]
        public virtual string TestItemCName { get; set; }

        /// <summary>
        /// 样品信息1(监测类型的记录项结果值1)
        /// </summary>
        [DataMember]
        [DataDesc(CName = "ItemResult1", ShortCode = "ItemResult1", Desc = "样品信息1", ContextType = SysDic.All)]
        public virtual string ItemResult1 { get; set; }

        /// <summary>
        /// 样品信息2(监测类型的记录项结果值1)
        /// </summary>
        [DataMember]
        [DataDesc(CName = "ItemResult2", ShortCode = "ItemResult2", Desc = "样品信息3", ContextType = SysDic.All)]
        public virtual string ItemResult2 { get; set; }

        /// <summary>
        /// 样品信息3(监测类型的记录项结果值1)
        /// </summary>
        [DataMember]
        [DataDesc(CName = "ItemResult3", ShortCode = "ItemResult3", Desc = "样品信息3", ContextType = SysDic.All)]
        public virtual string ItemResult3 { get; set; }

        /// <summary>
        /// 样品信息4(监测类型的记录项结果值1)
        /// </summary>
        [DataMember]
        [DataDesc(CName = "ItemResult4", ShortCode = "ItemResult4", Desc = "样品信息4", ContextType = SysDic.All)]
        public virtual string ItemResult4 { get; set; }
        /// <summary>
        /// 评估判定
        /// </summary>
        [DataMember]
        [DataDesc(CName = "评估判定", ShortCode = "JudgmentInfo", Desc = "评估判定", ContextType = SysDic.All)]
        public virtual string JudgmentInfo { get; set; }

        #endregion
    }
    #endregion
}