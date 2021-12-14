using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodRecei

    /// <summary>
    /// BloodRecei object for NHibernate mapped table 'Blood_Recei'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "送达主单", ClassCName = "BloodRecei", ShortCode = "BloodRecei", Desc = "送达主单")]
    public class BloodRecei : BaseEntity
    {
        #region Member Variables

        protected long? _deptNo;
        protected string _bSourceId;
        protected string _barCode;
        protected long? _doctorCode;
        protected string _bisbarCode;
        protected int _isCharge;
        protected string _brSampleNo;
        protected DateTime? _bisbarCodeCreateTime;
        protected long? _standNo;
        protected string _frameNo;
        protected string _bitNo;
        protected DateTime? _brSampledate;
        protected long? _sampleState;
        protected bool _invalidFlag;
        protected DateTime? _expirationTime;
        protected int _printflag;
        protected DateTime? _printTime;
        protected long? _standOperator;
        protected DateTime? _standOperateTime;
        protected string _standOperatorName;
        protected long? _collectId;
        protected string _collectName;
        protected DateTime? _collectDate;
        protected long? _nurseSenderId;
        protected string _nurseSender;
        protected DateTime? _nurseSendTime;
        protected long? _deliveryID;
        protected string _deliveryrName;
        protected DateTime? _receiDate;
        protected int _deliveryflag;
        protected int _scanflag;
        protected long? _sUserID;
        protected string _sUserName;
        protected long? _inceptID;
        protected string _incepter;
        protected DateTime? _inceptTime;
        protected DateTime? _sCanDate;
        protected string _reViewReportType;
        protected string _aboComparedFlag;
        protected DateTime? _lisAboCheckDateTime;
        protected DateTime? _lisReViewAboCheckDateTime;
        protected string _lisAboResult;
        protected string _lisReViewAboResult;
        protected string _aboReportDesc;
        protected string _rhReportDesc;
        protected long? _reViewAboRhCheckId;
        protected string _reViewAboReportDesc;
        protected string _reViewRHReportDesc;
        protected byte[] _aboReportDescImage;
        protected byte[] _rhReportDescImage;
        protected string _aboReportDescDemoNoList;
        protected string _aboReportDescDemoNameList;
        protected string _rhReportDescDemoNoList;
        protected string _rhReportDescDemoNameList;
        protected byte[] _srResult1Image;
        protected byte[] _srResult2Image;
        protected string _srResult1DemoNoList;
        protected string _srResult1DemoNameList;
        protected string _srResult2DemoNoList;
        protected string _srResult2DemoNameList;
        protected string _reViewAboReportDescDemo;
        protected string _reViewRhReportDescDemo;
        protected bool _visible;
        protected int _dispOrder;
        protected BDict _bloodAntiStereoTypes;
        protected BloodBReqForm _bindBloodBReqForm;
        protected BloodPatinfo _bloodPatinfo;
        protected BDict _bloodAnti;
        protected BDict _bloodBPreWay;
        protected BDict _bReqType;
        protected BDict _bloodPositiveStereoTypes;
        protected BDict _refuse;
        protected BDict _refuseDispose;

        #endregion

        #region Constructors

        public BloodRecei() { }

        public BloodRecei(long labID, long deptNo, string bSourceId, string barCode, long doctorCode, string bisbarCode, int isCharge, string brSampleNo, DateTime bisbarCodeCreateTime, long standNo, string frameNo, string bitNo, DateTime brSampledate, long sampleState, bool invalidFlag, DateTime expirationTime, int printflag, DateTime printTime, long standOperator, DateTime standOperateTime, string standOperatorName, long collectId, string collectName, DateTime collectDate, long nurseSenderId, string nurseSender, DateTime nurseSendTime, long deliveryID, string deliveryrName, DateTime receiDate, int deliveryflag, int scanflag, long sUserID, string sUserName, long inceptID, string incepter, DateTime inceptTime, DateTime sCanDate, string reViewReportType, string aboComparedFlag, DateTime lisAboCheckDateTime, DateTime lisReViewAboCheckDateTime, string lisAboResult, string lisReViewAboResult, string aboReportDesc, string rhReportDesc, long reViewAboRhCheckId, string reViewAboReportDesc, string reViewRHReportDesc, byte[] aboReportDescImage, byte[] rhReportDescImage, string aboReportDescDemoNoList, string aboReportDescDemoNameList, string rhReportDescDemoNoList, string rhReportDescDemoNameList, byte[] srResult1Image, byte[] srResult2Image, string srResult1DemoNoList, string srResult1DemoNameList, string srResult2DemoNoList, string srResult2DemoNameList, string reViewAboReportDescDemo, string reViewRhReportDescDemo, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BDict astNo, BloodBReqForm bind, BloodPatinfo bloodPatinfo, BDict antiNo, BDict bPreWayNo, BDict bReqType, BDict pstNo, BDict refuseNo, BDict refuseDisposeNo)
        {
            this._labID = labID;
            this._deptNo = deptNo;
            this._bSourceId = bSourceId;
            this._barCode = barCode;
            this._doctorCode = doctorCode;
            this._bisbarCode = bisbarCode;
            this._isCharge = isCharge;
            this._brSampleNo = brSampleNo;
            this._bisbarCodeCreateTime = bisbarCodeCreateTime;
            this._standNo = standNo;
            this._frameNo = frameNo;
            this._bitNo = bitNo;
            this._brSampledate = brSampledate;
            this._sampleState = sampleState;
            this._invalidFlag = invalidFlag;
            this._expirationTime = expirationTime;
            this._printflag = printflag;
            this._printTime = printTime;
            this._standOperator = standOperator;
            this._standOperateTime = standOperateTime;
            this._standOperatorName = standOperatorName;
            this._collectId = collectId;
            this._collectName = collectName;
            this._collectDate = collectDate;
            this._nurseSenderId = nurseSenderId;
            this._nurseSender = nurseSender;
            this._nurseSendTime = nurseSendTime;
            this._deliveryID = deliveryID;
            this._deliveryrName = deliveryrName;
            this._receiDate = receiDate;
            this._deliveryflag = deliveryflag;
            this._scanflag = scanflag;
            this._sUserID = sUserID;
            this._sUserName = sUserName;
            this._inceptID = inceptID;
            this._incepter = incepter;
            this._inceptTime = inceptTime;
            this._sCanDate = sCanDate;
            this._reViewReportType = reViewReportType;
            this._aboComparedFlag = aboComparedFlag;
            this._lisAboCheckDateTime = lisAboCheckDateTime;
            this._lisReViewAboCheckDateTime = lisReViewAboCheckDateTime;
            this._lisAboResult = lisAboResult;
            this._lisReViewAboResult = lisReViewAboResult;
            this._aboReportDesc = aboReportDesc;
            this._rhReportDesc = rhReportDesc;
            this._reViewAboRhCheckId = reViewAboRhCheckId;
            this._reViewAboReportDesc = reViewAboReportDesc;
            this._reViewRHReportDesc = reViewRHReportDesc;
            this._aboReportDescImage = aboReportDescImage;
            this._rhReportDescImage = rhReportDescImage;
            this._aboReportDescDemoNoList = aboReportDescDemoNoList;
            this._aboReportDescDemoNameList = aboReportDescDemoNameList;
            this._rhReportDescDemoNoList = rhReportDescDemoNoList;
            this._rhReportDescDemoNameList = rhReportDescDemoNameList;
            this._srResult1Image = srResult1Image;
            this._srResult2Image = srResult2Image;
            this._srResult1DemoNoList = srResult1DemoNoList;
            this._srResult1DemoNameList = srResult1DemoNameList;
            this._srResult2DemoNoList = srResult2DemoNoList;
            this._srResult2DemoNameList = srResult2DemoNameList;
            this._reViewAboReportDescDemo = reViewAboReportDescDemo;
            this._reViewRhReportDescDemo = reViewRhReportDescDemo;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bloodAntiStereoTypes = astNo;
            this._bindBloodBReqForm = bind;
            this._bloodPatinfo = bloodPatinfo;
            this._bloodAnti = antiNo;
            this._bloodBPreWay = bPreWayNo;
            this._bReqType = bReqType;
            this._bloodPositiveStereoTypes = pstNo;
            this._refuse = refuseNo;
            this._refuseDispose = refuseDisposeNo;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请科室编号", ShortCode = "DeptNo", Desc = "申请科室编号", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BSourceId", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BSourceId
        {
            get { return _bSourceId; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BSourceId", value, value.ToString());
                _bSourceId = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "条码号,Lis生成", ShortCode = "BarCode", Desc = "条码号,Lis生成", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请医生代码", ShortCode = "DoctorCode", Desc = "申请医生代码", ContextType = SysDic.All, Length = 8)]
        public virtual long? DoctorCode
        {
            get { return _doctorCode; }
            set { _doctorCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "新条码号，系统内部生成", ShortCode = "BisbarCode", Desc = "新条码号，系统内部生成", ContextType = SysDic.All, Length = 50)]
        public virtual string BisbarCode
        {
            get { return _bisbarCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BisbarCode", value, value.ToString());
                _bisbarCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否收费", ShortCode = "IsCharge", Desc = "是否收费", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCharge
        {
            get { return _isCharge; }
            set { _isCharge = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本编号", ShortCode = "BrSampleNo", Desc = "样本编号", ContextType = SysDic.All, Length = 20)]
        public virtual string BrSampleNo
        {
            get { return _brSampleNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BrSampleNo", value, value.ToString());
                _brSampleNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "条码生成时间", ShortCode = "BisbarCodeCreateTime", Desc = "条码生成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BisbarCodeCreateTime
        {
            get { return _bisbarCodeCreateTime; }
            set { _bisbarCodeCreateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "标本台,取标本台字典编码", ShortCode = "StandNo", Desc = "标本台,取标本台字典编码", ContextType = SysDic.All, Length = 8)]
        public virtual long? StandNo
        {
            get { return _standNo; }
            set { _standNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "架号，系统按业务规则生成", ShortCode = "FrameNo", Desc = "架号，系统按业务规则生成", ContextType = SysDic.All, Length = 10)]
        public virtual string FrameNo
        {
            get { return _frameNo; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for FrameNo", value, value.ToString());
                _frameNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "位号，系统按业务规则生成", ShortCode = "BitNo", Desc = "位号，系统按业务规则生成", ContextType = SysDic.All, Length = 10)]
        public virtual string BitNo
        {
            get { return _bitNo; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BitNo", value, value.ToString());
                _bitNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本编号时间", ShortCode = "BrSampledate", Desc = "样本编号时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BrSampledate
        {
            get { return _brSampledate; }
            set { _brSampledate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "状态", ShortCode = "SampleState", Desc = "状态", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleState
        {
            get { return _sampleState; }
            set { _sampleState = value; }
        }

        [DataMember]
        [DataDesc(CName = "报废标志", ShortCode = "InvalidFlag", Desc = "报废标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool InvalidFlag
        {
            get { return _invalidFlag; }
            set { _invalidFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失效时间", ShortCode = "ExpirationTime", Desc = "失效时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ExpirationTime
        {
            get { return _expirationTime; }
            set { _expirationTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "Printflag", ShortCode = "Printflag", Desc = "Printflag", ContextType = SysDic.All, Length = 4)]
        public virtual int Printflag
        {
            get { return _printflag; }
            set { _printflag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "打印时间", ShortCode = "PrintTime", Desc = "打印时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PrintTime
        {
            get { return _printTime; }
            set { _printTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "标本台操作者ID", ShortCode = "StandOperator", Desc = "标本台操作者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? StandOperator
        {
            get { return _standOperator; }
            set { _standOperator = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "标本台操作时间", ShortCode = "StandOperateTime", Desc = "标本台操作时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StandOperateTime
        {
            get { return _standOperateTime; }
            set { _standOperateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "标本台操作者", ShortCode = "StandOperatorName", Desc = "标本台操作者", ContextType = SysDic.All, Length = 50)]
        public virtual string StandOperatorName
        {
            get { return _standOperatorName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StandOperatorName", value, value.ToString());
                _standOperatorName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采集者Id", ShortCode = "CollectId", Desc = "采集者Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? CollectId
        {
            get { return _collectId; }
            set { _collectId = value; }
        }

        [DataMember]
        [DataDesc(CName = "采集者", ShortCode = "CollectName", Desc = "采集者", ContextType = SysDic.All, Length = 50)]
        public virtual string CollectName
        {
            get { return _collectName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CollectName", value, value.ToString());
                _collectName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采集时间", ShortCode = "CollectDate", Desc = "采集时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CollectDate
        {
            get { return _collectDate; }
            set { _collectDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送检人ID", ShortCode = "NurseSenderId", Desc = "送检人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? NurseSenderId
        {
            get { return _nurseSenderId; }
            set { _nurseSenderId = value; }
        }

        [DataMember]
        [DataDesc(CName = "送检人", ShortCode = "NurseSender", Desc = "送检人", ContextType = SysDic.All, Length = 20)]
        public virtual string NurseSender
        {
            get { return _nurseSender; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for NurseSender", value, value.ToString());
                _nurseSender = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送检时间", ShortCode = "NurseSendTime", Desc = "送检时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? NurseSendTime
        {
            get { return _nurseSendTime; }
            set { _nurseSendTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送达者ID", ShortCode = "DeliveryID", Desc = "送达者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeliveryID
        {
            get { return _deliveryID; }
            set { _deliveryID = value; }
        }

        [DataMember]
        [DataDesc(CName = "送达者", ShortCode = "DeliveryrName", Desc = "送达者", ContextType = SysDic.All, Length = 50)]
        public virtual string DeliveryrName
        {
            get { return _deliveryrName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DeliveryrName", value, value.ToString());
                _deliveryrName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送达时间", ShortCode = "ReceiDate", Desc = "送达时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiDate
        {
            get { return _receiDate; }
            set { _receiDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "送达标志", ShortCode = "Deliveryflag", Desc = "送达标志", ContextType = SysDic.All, Length = 4)]
        public virtual int Deliveryflag
        {
            get { return _deliveryflag; }
            set { _deliveryflag = value; }
        }

        [DataMember]
        [DataDesc(CName = "扫描标志", ShortCode = "Scanflag", Desc = "扫描标志", ContextType = SysDic.All, Length = 4)]
        public virtual int Scanflag
        {
            get { return _scanflag; }
            set { _scanflag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收者ID", ShortCode = "SUserID", Desc = "接收者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SUserID
        {
            get { return _sUserID; }
            set { _sUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "接收者", ShortCode = "SUserName", Desc = "接收者", ContextType = SysDic.All, Length = 50)]
        public virtual string SUserName
        {
            get { return _sUserName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SUserName", value, value.ToString());
                _sUserName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签收人ID", ShortCode = "InceptID", Desc = "签收人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? InceptID
        {
            get { return _inceptID; }
            set { _inceptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "签收人", ShortCode = "Incepter", Desc = "签收人", ContextType = SysDic.All, Length = 20)]
        public virtual string Incepter
        {
            get { return _incepter; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Incepter", value, value.ToString());
                _incepter = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签收时间", ShortCode = "InceptTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InceptTime
        {
            get { return _inceptTime; }
            set { _inceptTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "复核时间", ShortCode = "SCanDate", Desc = "复核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SCanDate
        {
            get { return _sCanDate; }
            set { _sCanDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核类型", ShortCode = "ReViewReportType", Desc = "审核类型", ContextType = SysDic.All, Length = 50)]
        public virtual string ReViewReportType
        {
            get { return _reViewReportType; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ReViewReportType", value, value.ToString());
                _reViewReportType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "血型匹配标志", ShortCode = "AboComparedFlag", Desc = "血型匹配标志", ContextType = SysDic.All, Length = 50)]
        public virtual string AboComparedFlag
        {
            get { return _aboComparedFlag; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for AboComparedFlag", value, value.ToString());
                _aboComparedFlag = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "初检审核时间", ShortCode = "LisAboCheckDateTime", Desc = "初检审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LisAboCheckDateTime
        {
            get { return _lisAboCheckDateTime; }
            set { _lisAboCheckDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "复检审核时间", ShortCode = "LisReViewAboCheckDateTime", Desc = "复检审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LisReViewAboCheckDateTime
        {
            get { return _lisReViewAboCheckDateTime; }
            set { _lisReViewAboCheckDateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "血型初检结果", ShortCode = "LisAboResult", Desc = "血型初检结果", ContextType = SysDic.All, Length = 20)]
        public virtual string LisAboResult
        {
            get { return _lisAboResult; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisAboResult", value, value.ToString());
                _lisAboResult = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "血型复检结果", ShortCode = "LisReViewAboResult", Desc = "血型复检结果", ContextType = SysDic.All, Length = 20)]
        public virtual string LisReViewAboResult
        {
            get { return _lisReViewAboResult; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisReViewAboResult", value, value.ToString());
                _lisReViewAboResult = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "初检血型", ShortCode = "AboReportDesc", Desc = "初检血型", ContextType = SysDic.All, Length = 20)]
        public virtual string AboReportDesc
        {
            get { return _aboReportDesc; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for AboReportDesc", value, value.ToString());
                _aboReportDesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "血型初检Rh(D)", ShortCode = "RhReportDesc", Desc = "血型初检Rh(D)", ContextType = SysDic.All, Length = 20)]
        public virtual string RhReportDesc
        {
            get { return _rhReportDesc; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for RhReportDesc", value, value.ToString());
                _rhReportDesc = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "复核人员编号", ShortCode = "ReViewAboRhCheckId", Desc = "复核人员编号", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReViewAboRhCheckId
        {
            get { return _reViewAboRhCheckId; }
            set { _reViewAboRhCheckId = value; }
        }

        [DataMember]
        [DataDesc(CName = "复检ABO血型", ShortCode = "ReViewAboReportDesc", Desc = "复检ABO血型", ContextType = SysDic.All, Length = 20)]
        public virtual string ReViewAboReportDesc
        {
            get { return _reViewAboReportDesc; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReViewAboReportDesc", value, value.ToString());
                _reViewAboReportDesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "血型复检Rh(D)", ShortCode = "ReViewRHReportDesc", Desc = "血型复检Rh(D)", ContextType = SysDic.All, Length = 20)]
        public virtual string ReViewRHReportDesc
        {
            get { return _reViewRHReportDesc; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReViewRHReportDesc", value, value.ToString());
                _reViewRHReportDesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "初检ABO文本", ShortCode = "AboReportDescImage", Desc = "初检ABO文本", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] AboReportDescImage
        {
            get { return _aboReportDescImage; }
            set { _aboReportDescImage = value; }
        }

        [DataMember]
        [DataDesc(CName = "初检RH文本", ShortCode = "RhReportDescImage", Desc = "初检RH文本", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] RhReportDescImage
        {
            get { return _rhReportDescImage; }
            set { _rhReportDescImage = value; }
        }

        [DataMember]
        [DataDesc(CName = "初检ABO异常血型备注", ShortCode = "AboReportDescDemoNoList", Desc = "初检ABO异常血型备注", ContextType = SysDic.All, Length = 50)]
        public virtual string AboReportDescDemoNoList
        {
            get { return _aboReportDescDemoNoList; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for AboReportDescDemoNoList", value, value.ToString());
                _aboReportDescDemoNoList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "初检ABO异常血型备注名称", ShortCode = "AboReportDescDemoNameList", Desc = "初检ABO异常血型备注名称", ContextType = SysDic.All, Length = 200)]
        public virtual string AboReportDescDemoNameList
        {
            get { return _aboReportDescDemoNameList; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for AboReportDescDemoNameList", value, value.ToString());
                _aboReportDescDemoNameList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "初检RH异常血型备注", ShortCode = "RhReportDescDemoNoList", Desc = "初检RH异常血型备注", ContextType = SysDic.All, Length = 50)]
        public virtual string RhReportDescDemoNoList
        {
            get { return _rhReportDescDemoNoList; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RhReportDescDemoNoList", value, value.ToString());
                _rhReportDescDemoNoList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "初检RH异常血型备注名称", ShortCode = "RhReportDescDemoNameList", Desc = "初检RH异常血型备注名称", ContextType = SysDic.All, Length = 200)]
        public virtual string RhReportDescDemoNameList
        {
            get { return _rhReportDescDemoNameList; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for RhReportDescDemoNameList", value, value.ToString());
                _rhReportDescDemoNameList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "复检ABO文本", ShortCode = "SrResult1Image", Desc = "复检ABO文本", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] SrResult1Image
        {
            get { return _srResult1Image; }
            set { _srResult1Image = value; }
        }

        [DataMember]
        [DataDesc(CName = "复检RH文本", ShortCode = "SrResult2Image", Desc = "复检RH文本", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] SrResult2Image
        {
            get { return _srResult2Image; }
            set { _srResult2Image = value; }
        }

        [DataMember]
        [DataDesc(CName = "复检ABO异常血型备注", ShortCode = "SrResult1DemoNoList", Desc = "复检ABO异常血型备注", ContextType = SysDic.All, Length = 50)]
        public virtual string SrResult1DemoNoList
        {
            get { return _srResult1DemoNoList; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SrResult1DemoNoList", value, value.ToString());
                _srResult1DemoNoList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "复检ABO异常血型备注名称", ShortCode = "SrResult1DemoNameList", Desc = "复检ABO异常血型备注名称", ContextType = SysDic.All, Length = 200)]
        public virtual string SrResult1DemoNameList
        {
            get { return _srResult1DemoNameList; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for SrResult1DemoNameList", value, value.ToString());
                _srResult1DemoNameList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "复检ABO异常血型备注", ShortCode = "SrResult2DemoNoList", Desc = "复检ABO异常血型备注", ContextType = SysDic.All, Length = 50)]
        public virtual string SrResult2DemoNoList
        {
            get { return _srResult2DemoNoList; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SrResult2DemoNoList", value, value.ToString());
                _srResult2DemoNoList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "复检ABO异常血型备注名称", ShortCode = "SrResult2DemoNameList", Desc = "复检ABO异常血型备注名称", ContextType = SysDic.All, Length = 200)]
        public virtual string SrResult2DemoNameList
        {
            get { return _srResult2DemoNameList; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for SrResult2DemoNameList", value, value.ToString());
                _srResult2DemoNameList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "复检血型ABO备注", ShortCode = "ReViewAboReportDescDemo", Desc = "复检血型ABO备注", ContextType = SysDic.All, Length = 100)]
        public virtual string ReViewAboReportDescDemo
        {
            get { return _reViewAboReportDescDemo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReViewAboReportDescDemo", value, value.ToString());
                _reViewAboReportDescDemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "复检血型RH备注", ShortCode = "ReViewRhReportDescDemo", Desc = "复检血型RH备注", ContextType = SysDic.All, Length = 20)]
        public virtual string ReViewRhReportDescDemo
        {
            get { return _reViewRhReportDescDemo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReViewRhReportDescDemo", value, value.ToString());
                _reViewRhReportDescDemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "反定型字典", ShortCode = "BloodAntiStereoTypes", Desc = "反定型字典")]
        public virtual BDict BloodAntiStereoTypes
        {
            get { return _bloodAntiStereoTypes; }
            set { _bloodAntiStereoTypes = value; }
        }

        [DataMember]
        [DataDesc(CName = "绑定的用血申请单", ShortCode = "BindBloodBReqForm", Desc = "绑定的用血申请单")]
        public virtual BloodBReqForm BindBloodBReqForm
        {
            get { return _bindBloodBReqForm; }
            set { _bindBloodBReqForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "病人就诊记录信息表", ShortCode = "BloodPatinfo", Desc = "病人就诊记录信息表")]
        public virtual BloodPatinfo BloodPatinfo
        {
            get { return _bloodPatinfo; }
            set { _bloodPatinfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "抗体筛选字典", ShortCode = "BloodAnti", Desc = "抗体筛选字典")]
        public virtual BDict BloodAnti
        {
            get { return _bloodAnti; }
            set { _bloodAnti = value; }
        }

        [DataMember]
        [DataDesc(CName = "交叉配血方法", ShortCode = "BloodBPreWay", Desc = "交叉配血方法")]
        public virtual BDict BloodBPreWay
        {
            get { return _bloodBPreWay; }
            set { _bloodBPreWay = value; }
        }

        [DataMember]
        [DataDesc(CName = "就诊类型", ShortCode = "BReqType", Desc = "就诊类型")]
        public virtual BDict BReqType
        {
            get { return _bReqType; }
            set { _bReqType = value; }
        }

        [DataMember]
        [DataDesc(CName = "正定型字典信息", ShortCode = "PstNo", Desc = "正定型字典信息")]
        public virtual BDict BloodPositiveStereoTypes
        {
            get { return _bloodPositiveStereoTypes; }
            set { _bloodPositiveStereoTypes = value; }
        }

        [DataMember]
        [DataDesc(CName = "拒收原因", ShortCode = "Refuse", Desc = "拒收原因")]
        public virtual BDict Refuse
        {
            get { return _refuse; }
            set { _refuse = value; }
        }

        [DataMember]
        [DataDesc(CName = "拒收处理", ShortCode = "RefuseDispose", Desc = "拒收处理")]
        public virtual BDict RefuseDispose
        {
            get { return _refuseDispose; }
            set { _refuseDispose = value; }
        }

        #endregion
    }
    #endregion
}