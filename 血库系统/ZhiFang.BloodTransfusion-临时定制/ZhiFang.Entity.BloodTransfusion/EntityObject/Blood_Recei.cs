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
    [DataDesc(CName = "", ClassCName = "BloodRecei", ShortCode = "BloodRecei", Desc = "")]
    public class BloodRecei : BaseEntityServiceByString
    {
        #region Member Variables

        protected string _patID;
        protected string _patNo;
        protected string _cName;
        protected string _patsex;
        protected string _patAge;
        protected string _deptNo;
        protected string _bisbarCode;
        protected DateTime? _collectDate;
        protected DateTime? _receidate;
        protected string _sUserID;
        protected string _sampleState;
        protected int _scanflag;
        protected string _deliveryID;
        protected int _deliveryflag;
        protected DateTime? _scandate;
        protected string _standNo;
        protected string _frameNo;
        protected string _bitNo;
        protected string _standOperator;
        protected DateTime? _standOperateTime;
        protected string _standOperatorName;
        protected DateTime? _expirationTime;
        protected string _aboComparedFlag;
        protected DateTime? _lisAboCheckDateTime;
        protected DateTime? _lisReViewAboCheckDateTime;
        protected string _lisAboResult;
        protected string _lisReViewAboResult;
        protected string _bSourceId;
        protected string _aboReportDesc;
        protected string _rhReportDesc;
        protected string _reViewAboReportDesc;
        protected string _reViewRHReportDesc;
        protected int _printflag;
        protected DateTime? _printTime;
        //protected string _refuseNo;
        //protected string _refuseDisposeNo;
        protected string _visitID;
        protected string _reViewAboRhCheckId;
        protected DateTime? _patindate;
        protected int _isCharge;
        protected string _brsampleNo;
        protected DateTime? _brSampledate;
        protected string _reViewReportType;
        //protected string _bindBreqFormID;
        protected DateTime? _bisbarCodeCreateTime;
        protected string _yqCode;
        protected string _pstNo;
        protected string _astNo;
        protected string _antiNo;
        protected string _bPreWayNo;
        protected string _bReqTypeID;
        protected string _doctorCode;
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
        protected string _invalidflag;
        protected string _sex;
        protected string _ageALL;
        protected string _bed;
        protected string _nurseSender;
        protected string _nurseSendTime;
        protected string _inceptID;
        protected string _incepter;
        protected string _inceptTime;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected string _reViewAboReportDescDemo;
        protected string _reViewRhReportDescDemo;
        protected string _iFlag1;
        protected string _iFlag2;
        protected string _iFlag3;
        protected string _iFlag4;
        protected string _iFlag5;
        protected int _dispOrder;
        protected bool _visible;

        protected Bloodrefuse _bloodrefuse;
        protected BloodrefuseDispose _bloodrefuseDispose;
        protected BloodBReqForm _bloodBReqForm;

        #endregion

        #region Constructors

        public BloodRecei() { }

        public BloodRecei(Bloodrefuse bloodrefuse, BloodrefuseDispose bloodrefuseDispose, BloodBReqForm bloodBReqForm, string patID, string patNo, string cName, string patsex, string patAge, string deptNo, string bisbarCode, DateTime collectDate, DateTime receidate, string sUserID, string sampleState, int scanflag, string deliveryID, int deliveryflag, DateTime scandate, string standNo, string frameNo, string bitNo, string standOperator, DateTime standOperateTime, string standOperatorName, DateTime expirationTime, string aboComparedFlag, DateTime lisAboCheckDateTime, DateTime lisReViewAboCheckDateTime, string lisAboResult, string lisReViewAboResult, string bSourceId, string aboReportDesc, string rhReportDesc, string reViewAboReportDesc, string reViewRHReportDesc, int printflag, DateTime printTime, string visitID, string reViewAboRhCheckId, DateTime patindate, int isCharge, string brsampleNo, DateTime brSampledate, string reViewReportType, DateTime bisbarCodeCreateTime, string yqCode, string pstNo, string astNo, string antiNo, string bPreWayNo, string bReqTypeID, string doctorCode, byte[] aboReportDescImage, byte[] rhReportDescImage, string aboReportDescDemoNoList, string aboReportDescDemoNameList, string rhReportDescDemoNoList, string rhReportDescDemoNameList, byte[] srResult1Image, byte[] srResult2Image, string srResult1DemoNoList, string srResult1DemoNameList, string srResult2DemoNoList, string srResult2DemoNameList, string invalidflag, string sex, string ageALL, string bed, string nurseSender, string nurseSendTime, string inceptID, string incepter, string inceptTime, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, string reViewAboReportDescDemo, string reViewRhReportDescDemo, string iFlag1, string iFlag2, string iFlag3, string iFlag4, string iFlag5, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible)
        {
            this._bloodrefuse = bloodrefuse;
            this._bloodrefuseDispose = bloodrefuseDispose;
            this._bloodBReqForm = bloodBReqForm;

            this._patID = patID;
            this._patNo = patNo;
            this._cName = cName;
            this._patsex = patsex;
            this._patAge = patAge;
            this._deptNo = deptNo;
            this._bisbarCode = bisbarCode;
            this._collectDate = collectDate;
            this._receidate = receidate;
            this._sUserID = sUserID;
            this._sampleState = sampleState;
            this._scanflag = scanflag;
            this._deliveryID = deliveryID;
            this._deliveryflag = deliveryflag;
            this._scandate = scandate;
            this._standNo = standNo;
            this._frameNo = frameNo;
            this._bitNo = bitNo;
            this._standOperator = standOperator;
            this._standOperateTime = standOperateTime;
            this._standOperatorName = standOperatorName;
            this._expirationTime = expirationTime;
            this._aboComparedFlag = aboComparedFlag;
            this._lisAboCheckDateTime = lisAboCheckDateTime;
            this._lisReViewAboCheckDateTime = lisReViewAboCheckDateTime;
            this._lisAboResult = lisAboResult;
            this._lisReViewAboResult = lisReViewAboResult;
            this._bSourceId = bSourceId;
            this._aboReportDesc = aboReportDesc;
            this._rhReportDesc = rhReportDesc;
            this._reViewAboReportDesc = reViewAboReportDesc;
            this._reViewRHReportDesc = reViewRHReportDesc;
            this._printflag = printflag;
            this._printTime = printTime;
            this._visitID = visitID;
            this._reViewAboRhCheckId = reViewAboRhCheckId;
            this._patindate = patindate;
            this._isCharge = isCharge;
            this._brsampleNo = brsampleNo;
            this._brSampledate = brSampledate;
            this._reViewReportType = reViewReportType;
            this._bisbarCodeCreateTime = bisbarCodeCreateTime;
            this._yqCode = yqCode;
            this._pstNo = pstNo;
            this._astNo = astNo;
            this._antiNo = antiNo;
            this._bPreWayNo = bPreWayNo;
            this._bReqTypeID = bReqTypeID;
            this._doctorCode = doctorCode;
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
            this._invalidflag = invalidflag;
            this._sex = sex;
            this._ageALL = ageALL;
            this._bed = bed;
            this._nurseSender = nurseSender;
            this._nurseSendTime = nurseSendTime;
            this._inceptID = inceptID;
            this._incepter = incepter;
            this._inceptTime = inceptTime;
            this._zDY1 = zDY1;
            this._zDY2 = zDY2;
            this._zDY3 = zDY3;
            this._zDY4 = zDY4;
            this._zDY5 = zDY5;
            this._reViewAboReportDescDemo = reViewAboReportDescDemo;
            this._reViewRhReportDescDemo = reViewRhReportDescDemo;
            this._iFlag1 = iFlag1;
            this._iFlag2 = iFlag2;
            this._iFlag3 = iFlag3;
            this._iFlag4 = iFlag4;
            this._iFlag5 = iFlag5;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "Bloodrefuse", ShortCode = "Bloodrefuse", Desc = "Bloodrefuse")]
        public virtual Bloodrefuse Bloodrefuse
        {
            get { return _bloodrefuse; }
            set { _bloodrefuse = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodrefuseDispose", ShortCode = "BloodrefuseDispose", Desc = "BloodrefuseDispose")]
        public virtual BloodrefuseDispose BloodrefuseDispose
        {
            get { return _bloodrefuseDispose; }
            set { _bloodrefuseDispose = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodBReqForm", ShortCode = "BloodBReqForm", Desc = "BloodBReqForm")]
        public virtual BloodBReqForm BloodBReqForm
        {
            get { return _bloodBReqForm; }
            set { _bloodBReqForm = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PatID
        {
            get { return _patID; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PatID", value, value.ToString());
                _patID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PatNo
        {
            get { return _patNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PatNo", value, value.ToString());
                _patNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Patsex", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Patsex
        {
            get { return _patsex; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Patsex", value, value.ToString());
                _patsex = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatAge", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PatAge
        {
            get { return _patAge; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PatAge", value, value.ToString());
                _patAge = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DeptNo
        {
            get { return _deptNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptNo", value, value.ToString());
                _deptNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BisbarCode", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CollectDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CollectDate
        {
            get { return _collectDate; }
            set { _collectDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Receidate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Receidate
        {
            get { return _receidate; }
            set { _receidate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SUserID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SUserID
        {
            get { return _sUserID; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SUserID", value, value.ToString());
                _sUserID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleState", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleState
        {
            get { return _sampleState; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SampleState", value, value.ToString());
                _sampleState = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Scanflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Scanflag
        {
            get { return _scanflag; }
            set { _scanflag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeliveryID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DeliveryID
        {
            get { return _deliveryID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DeliveryID", value, value.ToString());
                _deliveryID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Deliveryflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Deliveryflag
        {
            get { return _deliveryflag; }
            set { _deliveryflag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Scandate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Scandate
        {
            get { return _scandate; }
            set { _scandate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandNo", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string StandNo
        {
            get { return _standNo; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for StandNo", value, value.ToString());
                _standNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FrameNo", Desc = "", ContextType = SysDic.All, Length = 10)]
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
        [DataDesc(CName = "", ShortCode = "BitNo", Desc = "", ContextType = SysDic.All, Length = 10)]
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
        [DataDesc(CName = "", ShortCode = "StandOperator", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StandOperator
        {
            get { return _standOperator; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StandOperator", value, value.ToString());
                _standOperator = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StandOperateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StandOperateTime
        {
            get { return _standOperateTime; }
            set { _standOperateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandOperatorName", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "ExpirationTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ExpirationTime
        {
            get { return _expirationTime; }
            set { _expirationTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AboComparedFlag", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "LisAboCheckDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LisAboCheckDateTime
        {
            get { return _lisAboCheckDateTime; }
            set { _lisAboCheckDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LisReViewAboCheckDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LisReViewAboCheckDateTime
        {
            get { return _lisReViewAboCheckDateTime; }
            set { _lisReViewAboCheckDateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisAboResult", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "LisReViewAboResult", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "AboReportDesc", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "RhReportDesc", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "ReViewAboReportDesc", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "ReViewRHReportDesc", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "Printflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Printflag
        {
            get { return _printflag; }
            set { _printflag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PrintTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PrintTime
        {
            get { return _printTime; }
            set { _printTime = value; }
        }


        [DataMember]
        [DataDesc(CName = "", ShortCode = "VisitID", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string VisitID
        {
            get { return _visitID; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for VisitID", value, value.ToString());
                _visitID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReViewAboRhCheckId", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ReViewAboRhCheckId
        {
            get { return _reViewAboRhCheckId; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for ReViewAboRhCheckId", value, value.ToString());
                _reViewAboRhCheckId = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Patindate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Patindate
        {
            get { return _patindate; }
            set { _patindate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCharge", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCharge
        {
            get { return _isCharge; }
            set { _isCharge = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BrsampleNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BrsampleNo
        {
            get { return _brsampleNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BrsampleNo", value, value.ToString());
                _brsampleNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BrSampledate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BrSampledate
        {
            get { return _brSampledate; }
            set { _brSampledate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReViewReportType", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BisbarCodeCreateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BisbarCodeCreateTime
        {
            get { return _bisbarCodeCreateTime; }
            set { _bisbarCodeCreateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "YqCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string YqCode
        {
            get { return _yqCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for YqCode", value, value.ToString());
                _yqCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PstNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PstNo
        {
            get { return _pstNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PstNo", value, value.ToString());
                _pstNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AstNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string AstNo
        {
            get { return _astNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for AstNo", value, value.ToString());
                _astNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AntiNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string AntiNo
        {
            get { return _antiNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for AntiNo", value, value.ToString());
                _antiNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreWayNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreWayNo
        {
            get { return _bPreWayNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreWayNo", value, value.ToString());
                _bPreWayNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BReqTypeID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BReqTypeID
        {
            get { return _bReqTypeID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BReqTypeID", value, value.ToString());
                _bReqTypeID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoctorCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DoctorCode
        {
            get { return _doctorCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DoctorCode", value, value.ToString());
                _doctorCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AboReportDescImage", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] AboReportDescImage
        {
            get { return _aboReportDescImage; }
            set { _aboReportDescImage = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RhReportDescImage", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] RhReportDescImage
        {
            get { return _rhReportDescImage; }
            set { _rhReportDescImage = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AboReportDescDemoNoList", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "AboReportDescDemoNameList", Desc = "", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "", ShortCode = "RhReportDescDemoNoList", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "RhReportDescDemoNameList", Desc = "", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "", ShortCode = "SrResult1Image", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] SrResult1Image
        {
            get { return _srResult1Image; }
            set { _srResult1Image = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SrResult2Image", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] SrResult2Image
        {
            get { return _srResult2Image; }
            set { _srResult2Image = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SrResult1DemoNoList", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "SrResult1DemoNameList", Desc = "", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "", ShortCode = "SrResult2DemoNoList", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "SrResult2DemoNameList", Desc = "", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "", ShortCode = "Invalidflag", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Invalidflag
        {
            get { return _invalidflag; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Invalidflag", value, value.ToString());
                _invalidflag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Sex", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Sex
        {
            get { return _sex; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Sex", value, value.ToString());
                _sex = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AgeALL", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string AgeALL
        {
            get { return _ageALL; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for AgeALL", value, value.ToString());
                _ageALL = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bed", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Bed
        {
            get { return _bed; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Bed", value, value.ToString());
                _bed = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NurseSender", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "NurseSendTime", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string NurseSendTime
        {
            get { return _nurseSendTime; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for NurseSendTime", value, value.ToString());
                _nurseSendTime = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InceptID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string InceptID
        {
            get { return _inceptID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for InceptID", value, value.ToString());
                _inceptID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Incepter", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "InceptTime", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string InceptTime
        {
            get { return _inceptTime; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for InceptTime", value, value.ToString());
                _inceptTime = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY1", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ZDY1
        {
            get { return _zDY1; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
                _zDY1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY2", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ZDY2
        {
            get { return _zDY2; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
                _zDY2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY3", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ZDY3
        {
            get { return _zDY3; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
                _zDY3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY4", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ZDY4
        {
            get { return _zDY4; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
                _zDY4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY5", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ZDY5
        {
            get { return _zDY5; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
                _zDY5 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReViewAboReportDescDemo", Desc = "", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "", ShortCode = "ReViewRhReportDescDemo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ReViewRhReportDescDemo
        {
            get { return _reViewRhReportDescDemo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReViewRhReportDescDemo", value, value.ToString());
                _reViewRhReportDescDemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag1", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag1
        {
            get { return _iFlag1; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag1", value, value.ToString());
                _iFlag1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag2", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag2
        {
            get { return _iFlag2; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag2", value, value.ToString());
                _iFlag2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag3", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag3
        {
            get { return _iFlag3; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag3", value, value.ToString());
                _iFlag3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag4", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag4
        {
            get { return _iFlag4; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag4", value, value.ToString());
                _iFlag4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag5", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag5
        {
            get { return _iFlag5; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag5", value, value.ToString());
                _iFlag5 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }


        #endregion

        #region 自定义属性
        protected string _statusCName;
        [DataMember]
        [DataDesc(CName = "StatusCName", ShortCode = "StatusCName", Desc = "StatusCName", ContextType = SysDic.All)]
        public virtual string StatusCName
        {
            get { return _statusCName; }
            set
            {
                _statusCName = value;
            }
        }
        #endregion

    }
    #endregion
}