using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEGroupSampleForm

    /// <summary>
    /// MEGroupSampleForm object for NHibernate mapped table 'ME_GroupSampleForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "小组样本单", ClassCName = "MEGroupSampleForm", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
    public class MEGroupSampleForm : BaseEntity
    {
        #region Member Variables

        protected string _eSampleNo;
        protected DateTime? _gTestDate;
        protected string _gSampleNo;
        protected int _gTestNo;
        protected long? _gSampleTypeID;
        protected string _gSampleInfo;
        protected string _testComment;
        protected int _distributeFlag;
        protected int _isPrint;
        protected int _printCount;
        protected int _isUpload;
        protected string _formMemo;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected DateTime? _dataUpdateTime;
        protected bool _deleteFlag;

        protected string _gBarCode;
        protected string _mainTester;
        protected string _otherTester;
        protected DateTime? _confirmeDate;
        protected string _examiner;
        protected DateTime? _examineDate;
        protected long? _examinerId;
        protected long? _mainTesterId;
        protected string _confirmer;
        protected long? _confirmerId;
        protected int? _mainState;
        protected int? _isHasNuclearAdmission;
        protected int? _isCancelConfirmedOrAudited;
        protected int? _commState;
        protected bool _positiveFlag;
        protected BSampleStatus _bSampleStatus;
        protected GMGroup _gMGroup;
        protected MEPTOrderForm _mEPTOrderForm;
        protected MEPTSampleForm _mEPTSampleForm;
        protected IList<MEChargeInfo> _mEChargeInfoList;
        protected IList<MEGroupSampleItem> _mEGroupSampleItemList;
        protected IList<MEGroupSampleReportPublication> _mEGroupSampleReportPublicationList;
        protected IList<MEGroupSampleReCheckForm> _mEGroupSampleReCheckFormList;
        protected IList<MEMicroAppraisalValue> _mEMicroAppraisalValueList;
        protected IList<MEMicroCultureLog> _mEMicroCultureLogList;
        protected IList<MEMicroCultureValue> _mEMicroCultureValueList;
        protected IList<MEMicroDSTValue> _mEMicroDSTValueList;
        protected IList<MEMicroInoculant> _mEMicroInoculantList;
        protected IList<MEMicroRetainedBacteria> _mEMicroRetainedBacteriaList;
        protected IList<MEMicroSmearValue> _mEMicroSmearValueList;
        protected IList<MEMicroThreeReport> _mEMicroThreeReportList;
        protected IList<MEMicroThreeReportDetail> _mEMicroThreeReportDetailList;

        #endregion

        #region 自定义
        protected bool _isExistResult;
        protected bool _isBatchAdd;
        #endregion

        #region Constructors

        public MEGroupSampleForm() { }

        public MEGroupSampleForm(long labID, string eSampleNo, DateTime gTestDate, string gSampleNo, int gTestNo, long gSampleTypeID, string gSampleInfo, string testComment, int distributeFlag, int isPrint, int printCount, int isUpload, string formMemo, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool deleteFlag, string gBarCode, string mainTester, string otherTester, DateTime confirmeDate, string examiner, DateTime examineDate, long examinerId, long mainTesterId, string confirmer, long confirmerId, int? mainState, int isHasNuclearAdmission, int isCancelConfirmedOrAudited, int commState, bool positiveFlag, BSampleStatus bSampleStatus, GMGroup gMGroup, MEPTOrderForm mEPTOrderForm, MEPTSampleForm mEPTSampleForm)
        {
            this._labID = labID;
            this._eSampleNo = eSampleNo;
            this._gTestDate = gTestDate;
            this._gSampleNo = gSampleNo;
            this._gTestNo = gTestNo;
            this._gSampleTypeID = gSampleTypeID;
            this._gSampleInfo = gSampleInfo;
            this._testComment = testComment;
            this._distributeFlag = distributeFlag;
            this._isPrint = isPrint;
            this._printCount = printCount;
            this._isUpload = isUpload;
            this._formMemo = formMemo;
            this._zDY1 = zDY1;
            this._zDY2 = zDY2;
            this._zDY3 = zDY3;
            this._zDY4 = zDY4;
            this._zDY5 = zDY5;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._deleteFlag = deleteFlag;
            this._gBarCode = gBarCode;
            this._mainTester = mainTester;
            this._otherTester = otherTester;
            this._confirmeDate = confirmeDate;
            this._examiner = examiner;
            this._examineDate = examineDate;
            this._examinerId = examinerId;
            this._mainTesterId = mainTesterId;
            this._confirmer = confirmer;
            this._confirmerId = confirmerId;
            this._mainState = mainState;
            this._isHasNuclearAdmission = isHasNuclearAdmission;
            this._isCancelConfirmedOrAudited = isCancelConfirmedOrAudited;
            this._commState = commState;
            this._positiveFlag = positiveFlag;
            this._bSampleStatus = bSampleStatus;
            this._gMGroup = gMGroup;
            this._mEPTOrderForm = mEPTOrderForm;
            this._mEPTSampleForm = mEPTSampleForm;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "仪器样本号", ShortCode = "ESampleNo", Desc = "仪器样本号", ContextType = SysDic.All, Length = 20)]
        public virtual string ESampleNo
        {
            get { return _eSampleNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ESampleNo", value, value.ToString());
                _eSampleNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检测日期", ShortCode = "GTestDate", Desc = "检测日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? GTestDate
        {
            get { return _gTestDate; }
            set { _gTestDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组检测编号", ShortCode = "GSampleNo", Desc = "小组检测编号", ContextType = SysDic.All, Length = 20)]
        public virtual string GSampleNo
        {
            get { return _gSampleNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for GSampleNo", value, value.ToString());
                _gSampleNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "日检测样本序号", ShortCode = "GTestNo", Desc = "日检测样本序号", ContextType = SysDic.All, Length = 4)]
        public virtual int GTestNo
        {
            get { return _gTestNo; }
            set { _gTestNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本类型", ShortCode = "GSampleTypeID", Desc = "样本类型", ContextType = SysDic.All, Length = 8)]
        public virtual long? GSampleTypeID
        {
            get { return _gSampleTypeID; }
            set { _gSampleTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本描述", ShortCode = "GSampleInfo", Desc = "小组样本描述", ContextType = SysDic.All, Length = 50)]
        public virtual string GSampleInfo
        {
            get { return _gSampleInfo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GSampleInfo", value, value.ToString());
                _gSampleInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检验备注", ShortCode = "TestComment", Desc = "检验备注", ContextType = SysDic.All, Length = 16)]
        public virtual string TestComment
        {
            get { return _testComment; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for TestComment", value, value.ToString());
                _testComment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "分发标志", ShortCode = "DistributeFlag", Desc = "分发标志", ContextType = SysDic.All, Length = 4)]
        public virtual int DistributeFlag
        {
            get { return _distributeFlag; }
            set { _distributeFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "上传标志", ShortCode = "IsUpload", Desc = "上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUpload
        {
            get { return _isUpload; }
            set { _isUpload = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "FormMemo", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string FormMemo
        {
            get { return _formMemo; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for FormMemo", value, value.ToString());
                _formMemo = value;
            }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "删除标志", ShortCode = "DeleteFlag", Desc = "删除标志", ContextType = SysDic.All, Length = 1)]
        //public virtual bool DeleteFlag
        //{
        //    get { return _deleteFlag; }
        //    set { _deleteFlag = value; }
        //}
        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "GBarCode", Desc = "条码号", ContextType = SysDic.All, Length = 20)]
        public virtual string GBarCode
        {
            get { return _gBarCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for GBarCode", value, value.ToString());
                _gBarCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MainTester", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string MainTester
        {
            get { return _mainTester; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for MainTester", value, value.ToString());
                _mainTester = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OtherTester", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string OtherTester
        {
            get { return _otherTester; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for OtherTester", value, value.ToString());
                _otherTester = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ConfirmeDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ConfirmeDate
        {
            get { return _confirmeDate; }
            set { _confirmeDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Examiner", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Examiner
        {
            get { return _examiner; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Examiner", value, value.ToString());
                _examiner = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ExamineDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ExamineDate
        {
            get { return _examineDate; }
            set { _examineDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ExaminerId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ExaminerId
        {
            get { return _examinerId; }
            set { _examinerId = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MainTesterId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? MainTesterId
        {
            get { return _mainTesterId; }
            set { _mainTesterId = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Confirmer", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Confirmer
        {
            get { return _confirmer; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Confirmer", value, value.ToString());
                _confirmer = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ConfirmerId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ConfirmerId
        {
            get { return _confirmerId; }
            set { _confirmerId = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验单主状态：0检验中，1已报审，2已审核，-1临时冻结，-2作废", ShortCode = "MainState", Desc = "检验单主状态：0检验中，1已报审，2已审核，-1临时冻结，-2作废", ContextType = SysDic.All, Length = 4)]
        public virtual int? MainState
        {
            get { return _mainState; }
            set { _mainState = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已核收：0或null未核收，1已核收", ShortCode = "IsHasNuclearAdmission", Desc = "是否已核收：0或null未核收，1已核收", ContextType = SysDic.All, Length = 4)]
        public virtual int? IsHasNuclearAdmission
        {
            get { return _isHasNuclearAdmission; }
            set { _isHasNuclearAdmission = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否取消报审或反审：0或null为正常状态，1取消报审，2反审", ShortCode = "IsCancelConfirmedOrAudited", Desc = "是否取消报审或反审：0或null为正常状态，1取消报审，2反审", ContextType = SysDic.All, Length = 4)]
        public virtual int? IsCancelConfirmedOrAudited
        {
            get { return _isCancelConfirmedOrAudited; }
            set { _isCancelConfirmedOrAudited = value; }
        }

        [DataMember]
        [DataDesc(CName = "技师站发送项目标记：0或null未发送，1已发送", ShortCode = "CommState", Desc = "技师站发送项目标记：0或null未发送，1已发送", ContextType = SysDic.All, Length = 4)]
        public virtual int? CommState
        {
            get { return _commState; }
            set { _commState = value; }
        }

        [DataMember]
        [DataDesc(CName = "阳性标识", ShortCode = "PositiveFlag", Desc = "阳性标识", ContextType = SysDic.All, Length = 1)]
        public virtual bool PositiveFlag
        {
            get { return _positiveFlag; }
            set { _positiveFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本状态", ShortCode = "BSampleStatus", Desc = "样本状态")]
        public virtual BSampleStatus BSampleStatus
        {
            get { return _bSampleStatus; }
            set { _bSampleStatus = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
        public virtual GMGroup GMGroup
        {
            get { return _gMGroup; }
            set { _gMGroup = value; }
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
        [DataDesc(CName = "计费操作表", ShortCode = "MEChargeInfoList", Desc = "计费操作表")]
        public virtual IList<MEChargeInfo> MEChargeInfoList
        {
            get
            {
                if (_mEChargeInfoList == null)
                {
                    _mEChargeInfoList = new List<MEChargeInfo>();
                }
                return _mEChargeInfoList;
            }
            set { _mEChargeInfoList = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本项目", ShortCode = "MEGroupSampleItemList", Desc = "小组样本项目")]
        public virtual IList<MEGroupSampleItem> MEGroupSampleItemList
        {
            get
            {
                if (_mEGroupSampleItemList == null)
                {
                    _mEGroupSampleItemList = new List<MEGroupSampleItem>();
                }
                return _mEGroupSampleItemList;
            }
            set { _mEGroupSampleItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本单报告发布记录表", ShortCode = "MEGroupSampleReportPublicationList", Desc = "小组样本单报告发布记录表")]
        public virtual IList<MEGroupSampleReportPublication> MEGroupSampleReportPublicationList
        {
            get
            {
                if (_mEGroupSampleReportPublicationList == null)
                {
                    _mEGroupSampleReportPublicationList = new List<MEGroupSampleReportPublication>();
                }
                return _mEGroupSampleReportPublicationList;
            }
            set { _mEGroupSampleReportPublicationList = value; }
        }

        [DataMember]
        [DataDesc(CName = "复检记录", ShortCode = "MEGroupSampleReCheckFormList", Desc = "复检记录")]
        public virtual IList<MEGroupSampleReCheckForm> MEGroupSampleReCheckFormList
        {
            get
            {
                if (_mEGroupSampleReCheckFormList == null)
                {
                    _mEGroupSampleReCheckFormList = new List<MEGroupSampleReCheckForm>();
                }
                return _mEGroupSampleReCheckFormList;
            }
            set { _mEGroupSampleReCheckFormList = value; }
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
        [DataDesc(CName = "微生物接种记录", ShortCode = "MEMicroInoculantList", Desc = "微生物接种记录")]
        public virtual IList<MEMicroInoculant> MEMicroInoculantList
        {
            get
            {
                if (_mEMicroInoculantList == null)
                {
                    _mEMicroInoculantList = new List<MEMicroInoculant>();
                }
                return _mEMicroInoculantList;
            }
            set { _mEMicroInoculantList = value; }
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

        [DataMember]
        [DataDesc(CName = "微生物三级报告主表", ShortCode = "MEMicroThreeReportList", Desc = "微生物三级报告主表")]
        public virtual IList<MEMicroThreeReport> MEMicroThreeReportList
        {
            get
            {
                if (_mEMicroThreeReportList == null)
                {
                    _mEMicroThreeReportList = new List<MEMicroThreeReport>();
                }
                return _mEMicroThreeReportList;
            }
            set { _mEMicroThreeReportList = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物三级报告细表", ShortCode = "MEMicroThreeReportDetailList", Desc = "微生物三级报告细表")]
        public virtual IList<MEMicroThreeReportDetail> MEMicroThreeReportDetailList
        {
            get
            {
                if (_mEMicroThreeReportDetailList == null)
                {
                    _mEMicroThreeReportDetailList = new List<MEMicroThreeReportDetail>();
                }
                return _mEMicroThreeReportDetailList;
            }
            set { _mEMicroThreeReportDetailList = value; }
        }


        #endregion

        #region 自定义属性
        [DataMember]
        [DataDesc(CName = "批量录入时判断是否存在结果", ShortCode = "IsExistResult", Desc = "批量录入时判断是否存在结果", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsExistResult
        {
            get { return _isExistResult; }
            set { _isExistResult = value; }
        }
        [DataMember]
        [DataDesc(CName = "批量录入时判断是否可以新增保存", ShortCode = "IsBatchAdd", Desc = "批量录入时判断是否可以新增保存", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsBatchAdd
        {
            get { return _isBatchAdd; }
            set { _isBatchAdd = value; }
        }
        #endregion
    }
    #endregion
}