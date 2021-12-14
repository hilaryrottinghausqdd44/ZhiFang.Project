using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region NRequestForm

    /// <summary>
    /// NRequestForm object for NHibernate mapped table 'NRequestForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "NRequestForm", ShortCode = "NRequestForm", Desc = "")]
    public class NRequestForm : BaseEntityService
    {
        #region Member Variables
        protected string _serialNo;
        protected int _receiveFlag;
        protected int _statusNo;
        protected int _sampleTypeNo;
        protected string _patNo;
        protected string _cName;
        protected int _genderNo;
        protected DateTime? _birthday;
        protected double _age;
        protected int _ageUnitNo;
        protected int _folkNo;
        protected int _districtNo;
        protected int _wardNo;
        protected string _bed;
        protected int _deptNo;
        protected int _doctor;
        protected int _diagNo;
        protected int _chargeNo;
        protected decimal _charge;
        protected string _collecterID;
        protected string _collecter;
        protected DateTime? _collectDate;
        protected DateTime? _collectTime;
        protected string _operator;
        protected DateTime? _operDate;
        protected DateTime? _operTime;
        protected string _formMemo;
        protected string _requestSource;
        protected string _artificerorder;
        protected string _sickorder;
        protected string _chargeflag;
        protected int _jztype;
        protected string _zdy1;
        protected string _zdy2;
        protected string _zdy3;
        protected string _zdy4;
        protected string _zdy5;
        protected DateTime? _flagDateDelete;
        protected string _formComment;
        protected string _nurseflag;
        protected string _diag;
        protected string _caseNo;
        protected string _refuseopinion;
        protected string _refusereason;
        protected DateTime? _signintime;
        protected string _signer;
        protected int _signflag;
        protected int _samplingGroupNo;
        protected int _printCount;
        protected string _printInfo;
        protected double _sampleCap;
        protected int _isPrep;
        protected int _isAffirm;
        protected int _isSampling;
        protected int _isSend;
        protected string _incepter;
        protected DateTime? _inceptTime;
        protected DateTime? _inceptDate;
        protected bool _isByHand;
        protected int _assignFlag;
        protected string _oldSerialNo;
        protected int _testTypeNo;
        protected int _isCheckFee;
        protected string _countNodesFormSource;
        protected int _dr2Flag;
        protected int _execDeptNo;
        protected string _clientHost;
        protected int _preNumber;
        protected int _clientno;
        protected int _stateFlag;
        protected int _dispenseFlag;
        protected string _refuseUser;
        protected DateTime? _refuseTime;
        protected string _jytype;
        protected string _serialScanTime;
        protected DateTime? _affirmTime;
        protected string _urgentState;
        protected string _zDY6;
        protected string _zDY7;
        protected string _zDY8;
        protected string _zDY9;
        protected string _zDY10;
        protected string _phoneCode;
        protected int _isNode;
        protected int _phoneNodeCount;
        protected int _autoNodeCount;
        protected int _isNurseDo;
        protected int _reportFlag;
        protected byte[] _patPhoto;
        protected string _chargeOrderNo;
        protected string _nurseSender;
        protected DateTime? _nurseSendTime;
        protected string _nurseSendCarrier;
        protected int _collectCount;
        protected int _foreignSendFlag;
        protected int _hisAffirm;
        protected string _hisDoctorId;
        protected string _hisDoctorPhoneCode;
        protected int _lisDoctorNo;
        protected string _reportDateDesc;
        protected string _patState;
        protected double _balance;
        protected string _nurseSendNo;
        protected string _collectPack;
        protected string _nurseSendFlag;
        protected int _doInputflag;
        protected string _carrierCode;
        protected string _arriveHost;
        protected DateTime? _arriveTime;
        protected int _isSendMsg;
        protected int _signIntervalTime;
        protected int _signOverTimeFlag;
        protected string _inceptHost;
        protected string _newLisCode;
        protected string _newLisCodeDisp;
        protected string _remover;
        protected DateTime? _removeTime;
        protected string _removeHost;
        protected string _conceder;
        protected DateTime? _concedeTime;
        protected string _concedeHost;
        protected string _concedeInfo;
        protected int _refuseID;
        protected int _inceptID;
        protected string _operCliet;
        protected string _groupSigner;
        protected int _groupSignID;
        protected DateTime? _groupSignTime;
        protected string _groupSignHost;
        protected string _collectPart;
        protected string _refuseComment;
        protected string _mergeno;
        protected string _zdy11;
        protected string _zdy12;
        protected string _zdy13;
        protected string _zdy14;
        protected string _zdy15;
        protected string _zdy16;
        protected string _zdy17;
        protected string _zdy18;
        protected string _zdy19;
        protected string _zdy20;
        protected int _hospitalizedTimes;
        protected string _hisSampleTypeNo;
        protected string _hisDeptNo;
        protected string _hisDoctor;
        protected string _tranSport;
        protected string _collectFlag;


        #endregion

        #region Constructors

        public NRequestForm() { }

        public NRequestForm(int receiveFlag, int statusNo, int sampleTypeNo, string patNo, string cName, int genderNo, DateTime birthday, double age, int ageUnitNo, int folkNo, int districtNo, int wardNo, string bed, int deptNo, int doctor, int diagNo, int chargeNo, decimal charge, string collecterID, string collecter, DateTime collectDate, DateTime collectTime, string _operator, DateTime operDate, DateTime operTime, string formMemo, string requestSource, string artificerorder, string sickorder, string chargeflag, int jztype, string zdy1, string zdy2, string zdy3, string zdy4, string zdy5, DateTime flagDateDelete, string formComment, string nurseflag, string diag, string caseNo, string refuseopinion, string refusereason, DateTime signintime, string signer, int signflag, int samplingGroupNo, int printCount, string printInfo, double sampleCap, int isPrep, int isAffirm, int isSampling, int isSend, string incepter, DateTime inceptTime, DateTime inceptDate, bool isByHand, int assignFlag, string oldSerialNo, int testTypeNo, int isCheckFee, string countNodesFormSource, int dr2Flag, int execDeptNo, string clientHost, int preNumber, int clientno, int stateFlag, int dispenseFlag, string refuseUser, DateTime refuseTime, string jytype, string serialScanTime, DateTime affirmTime, string urgentState, string zDY6, string zDY7, string zDY8, string zDY9, string zDY10, string phoneCode, int isNode, int phoneNodeCount, int autoNodeCount, int isNurseDo, int reportFlag, byte[] patPhoto, string chargeOrderNo, string nurseSender, DateTime nurseSendTime, string nurseSendCarrier, int collectCount, int foreignSendFlag, int hisAffirm, string hisDoctorId, string hisDoctorPhoneCode, int lisDoctorNo, string reportDateDesc, string patState, double balance, string nurseSendNo, string collectPack, string nurseSendFlag, int doInputflag, string carrierCode, string arriveHost, DateTime arriveTime, int isSendMsg, int signIntervalTime, int signOverTimeFlag, string inceptHost, string newLisCode, string newLisCodeDisp, string remover, DateTime removeTime, string removeHost, string conceder, DateTime concedeTime, string concedeHost, string concedeInfo, int refuseID, int inceptID, string operCliet, string groupSigner, int groupSignID, DateTime groupSignTime, string groupSignHost, string collectPart, string refuseComment, string mergeno, string zdy11, string zdy12, string zdy13, string zdy14, string zdy15, string zdy16, string zdy17, string zdy18, string zdy19, string zdy20, int hospitalizedTimes, string hisSampleTypeNo, string hisDeptNo, string hisDoctor, string tranSport, string collectFlag)
        {
            this._receiveFlag = receiveFlag;
            this._statusNo = statusNo;
            this._sampleTypeNo = sampleTypeNo;
            this._patNo = patNo;
            this._cName = cName;
            this._genderNo = genderNo;
            this._birthday = birthday;
            this._age = age;
            this._ageUnitNo = ageUnitNo;
            this._folkNo = folkNo;
            this._districtNo = districtNo;
            this._wardNo = wardNo;
            this._bed = bed;
            this._deptNo = deptNo;
            this._doctor = doctor;
            this._diagNo = diagNo;
            this._chargeNo = chargeNo;
            this._charge = charge;
            this._collecterID = collecterID;
            this._collecter = collecter;
            this._collectDate = collectDate;
            this._collectTime = collectTime;
            this._operator = _operator;
            this._operDate = operDate;
            this._operTime = operTime;
            this._formMemo = formMemo;
            this._requestSource = requestSource;
            this._artificerorder = artificerorder;
            this._sickorder = sickorder;
            this._chargeflag = chargeflag;
            this._jztype = jztype;
            this._zdy1 = zdy1;
            this._zdy2 = zdy2;
            this._zdy3 = zdy3;
            this._zdy4 = zdy4;
            this._zdy5 = zdy5;
            this._flagDateDelete = flagDateDelete;
            this._formComment = formComment;
            this._nurseflag = nurseflag;
            this._diag = diag;
            this._caseNo = caseNo;
            this._refuseopinion = refuseopinion;
            this._refusereason = refusereason;
            this._signintime = signintime;
            this._signer = signer;
            this._signflag = signflag;
            this._samplingGroupNo = samplingGroupNo;
            this._printCount = printCount;
            this._printInfo = printInfo;
            this._sampleCap = sampleCap;
            this._isPrep = isPrep;
            this._isAffirm = isAffirm;
            this._isSampling = isSampling;
            this._isSend = isSend;
            this._incepter = incepter;
            this._inceptTime = inceptTime;
            this._inceptDate = inceptDate;
            this._isByHand = isByHand;
            this._assignFlag = assignFlag;
            this._oldSerialNo = oldSerialNo;
            this._testTypeNo = testTypeNo;
            this._isCheckFee = isCheckFee;
            this._countNodesFormSource = countNodesFormSource;
            this._dr2Flag = dr2Flag;
            this._execDeptNo = execDeptNo;
            this._clientHost = clientHost;
            this._preNumber = preNumber;
            this._clientno = clientno;
            this._stateFlag = stateFlag;
            this._dispenseFlag = dispenseFlag;
            this._refuseUser = refuseUser;
            this._refuseTime = refuseTime;
            this._jytype = jytype;
            this._serialScanTime = serialScanTime;
            this._affirmTime = affirmTime;
            this._urgentState = urgentState;
            this._zDY6 = zDY6;
            this._zDY7 = zDY7;
            this._zDY8 = zDY8;
            this._zDY9 = zDY9;
            this._zDY10 = zDY10;
            this._phoneCode = phoneCode;
            this._isNode = isNode;
            this._phoneNodeCount = phoneNodeCount;
            this._autoNodeCount = autoNodeCount;
            this._isNurseDo = isNurseDo;
            this._reportFlag = reportFlag;
            this._patPhoto = patPhoto;
            this._chargeOrderNo = chargeOrderNo;
            this._nurseSender = nurseSender;
            this._nurseSendTime = nurseSendTime;
            this._nurseSendCarrier = nurseSendCarrier;
            this._collectCount = collectCount;
            this._foreignSendFlag = foreignSendFlag;
            this._hisAffirm = hisAffirm;
            this._hisDoctorId = hisDoctorId;
            this._hisDoctorPhoneCode = hisDoctorPhoneCode;
            this._lisDoctorNo = lisDoctorNo;
            this._reportDateDesc = reportDateDesc;
            this._patState = patState;
            this._balance = balance;
            this._nurseSendNo = nurseSendNo;
            this._collectPack = collectPack;
            this._nurseSendFlag = nurseSendFlag;
            this._doInputflag = doInputflag;
            this._carrierCode = carrierCode;
            this._arriveHost = arriveHost;
            this._arriveTime = arriveTime;
            this._isSendMsg = isSendMsg;
            this._signIntervalTime = signIntervalTime;
            this._signOverTimeFlag = signOverTimeFlag;
            this._inceptHost = inceptHost;
            this._newLisCode = newLisCode;
            this._newLisCodeDisp = newLisCodeDisp;
            this._remover = remover;
            this._removeTime = removeTime;
            this._removeHost = removeHost;
            this._conceder = conceder;
            this._concedeTime = concedeTime;
            this._concedeHost = concedeHost;
            this._concedeInfo = concedeInfo;
            this._refuseID = refuseID;
            this._inceptID = inceptID;
            this._operCliet = operCliet;
            this._groupSigner = groupSigner;
            this._groupSignID = groupSignID;
            this._groupSignTime = groupSignTime;
            this._groupSignHost = groupSignHost;
            this._collectPart = collectPart;
            this._refuseComment = refuseComment;
            this._mergeno = mergeno;
            this._zdy11 = zdy11;
            this._zdy12 = zdy12;
            this._zdy13 = zdy13;
            this._zdy14 = zdy14;
            this._zdy15 = zdy15;
            this._zdy16 = zdy16;
            this._zdy17 = zdy17;
            this._zdy18 = zdy18;
            this._zdy19 = zdy19;
            this._zdy20 = zdy20;
            this._hospitalizedTimes = hospitalizedTimes;
            this._hisSampleTypeNo = hisSampleTypeNo;
            this._hisDeptNo = hisDeptNo;
            this._hisDoctor = hisDoctor;
            this._tranSport = tranSport;
            this._collectFlag = collectFlag;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SerialNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SerialNo
        {
            get { return _serialNo; }
            set
            {
                _serialNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReceiveFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReceiveFlag
        {
            get { return _receiveFlag; }
            set { _receiveFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int StatusNo
        {
            get { return _statusNo; }
            set { _statusNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleTypeNo
        {
            get { return _sampleTypeNo; }
            set { _sampleTypeNo = value; }
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
        [DataDesc(CName = "", ShortCode = "GenderNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int GenderNo
        {
            get { return _genderNo; }
            set { _genderNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Birthday", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Age", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Age
        {
            get { return _age; }
            set { _age = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AgeUnitNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AgeUnitNo
        {
            get { return _ageUnitNo; }
            set { _ageUnitNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FolkNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int FolkNo
        {
            get { return _folkNo; }
            set { _folkNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DistrictNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DistrictNo
        {
            get { return _districtNo; }
            set { _districtNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WardNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int WardNo
        {
            get { return _wardNo; }
            set { _wardNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bed", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Bed
        {
            get { return _bed; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Bed", value, value.ToString());
                _bed = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Doctor", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DiagNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DiagNo
        {
            get { return _diagNo; }
            set { _diagNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeNo
        {
            get { return _chargeNo; }
            set { _chargeNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Charge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CollecterID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CollecterID
        {
            get { return _collecterID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CollecterID", value, value.ToString());
                _collecterID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Collecter", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Collecter
        {
            get { return _collecter; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Collecter", value, value.ToString());
                _collecter = value;
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
        [DataDesc(CName = "", ShortCode = "CollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CollectTime
        {
            get { return _collectTime; }
            set { _collectTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Operator", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Operator
        {
            get { return _operator; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Operator", value, value.ToString());
                _operator = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperDate
        {
            get { return _operDate; }
            set { _operDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperTime
        {
            get { return _operTime; }
            set { _operTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormMemo", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string FormMemo
        {
            get { return _formMemo; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for FormMemo", value, value.ToString());
                _formMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RequestSource", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string RequestSource
        {
            get { return _requestSource; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for RequestSource", value, value.ToString());
                _requestSource = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Artificerorder", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Artificerorder
        {
            get { return _artificerorder; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Artificerorder", value, value.ToString());
                _artificerorder = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Sickorder", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Sickorder
        {
            get { return _sickorder; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Sickorder", value, value.ToString());
                _sickorder = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Chargeflag", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Chargeflag
        {
            get { return _chargeflag; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Chargeflag", value, value.ToString());
                _chargeflag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Jztype", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Jztype
        {
            get { return _jztype; }
            set { _jztype = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy1", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy1
        {
            get { return _zdy1; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy1", value, value.ToString());
                _zdy1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy2", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy2
        {
            get { return _zdy2; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy2", value, value.ToString());
                _zdy2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy3", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy3
        {
            get { return _zdy3; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy3", value, value.ToString());
                _zdy3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy4", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy4
        {
            get { return _zdy4; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy4", value, value.ToString());
                _zdy4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy5", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy5
        {
            get { return _zdy5; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy5", value, value.ToString());
                _zdy5 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FlagDateDelete", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FlagDateDelete
        {
            get { return _flagDateDelete; }
            set { _flagDateDelete = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormComment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string FormComment
        {
            get { return _formComment; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for FormComment", value, value.ToString());
                _formComment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Nurseflag", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Nurseflag
        {
            get { return _nurseflag; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Nurseflag", value, value.ToString());
                _nurseflag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Diag", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Diag
        {
            get { return _diag; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Diag", value, value.ToString());
                _diag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CaseNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CaseNo
        {
            get { return _caseNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CaseNo", value, value.ToString());
                _caseNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Refuseopinion", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Refuseopinion
        {
            get { return _refuseopinion; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Refuseopinion", value, value.ToString());
                _refuseopinion = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Refusereason", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Refusereason
        {
            get { return _refusereason; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Refusereason", value, value.ToString());
                _refusereason = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Signintime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Signintime
        {
            get { return _signintime; }
            set { _signintime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Signer", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Signer
        {
            get { return _signer; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Signer", value, value.ToString());
                _signer = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Signflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Signflag
        {
            get { return _signflag; }
            set { _signflag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SamplingGroupNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SamplingGroupNo
        {
            get { return _samplingGroupNo; }
            set { _samplingGroupNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintInfo", Desc = "", ContextType = SysDic.All, Length = 600)]
        public virtual string PrintInfo
        {
            get { return _printInfo; }
            set
            {
                if (value != null && value.Length > 600)
                    throw new ArgumentOutOfRangeException("Invalid value for PrintInfo", value, value.ToString());
                _printInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SampleCap", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double SampleCap
        {
            get { return _sampleCap; }
            set { _sampleCap = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsPrep", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPrep
        {
            get { return _isPrep; }
            set { _isPrep = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsAffirm", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsAffirm
        {
            get { return _isAffirm; }
            set { _isAffirm = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsSampling", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsSampling
        {
            get { return _isSampling; }
            set { _isSampling = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsSend", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsSend
        {
            get { return _isSend; }
            set { _isSend = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InceptTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InceptTime
        {
            get { return _inceptTime; }
            set { _inceptTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InceptDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InceptDate
        {
            get { return _inceptDate; }
            set { _inceptDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsByHand", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsByHand
        {
            get { return _isByHand; }
            set { _isByHand = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AssignFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AssignFlag
        {
            get { return _assignFlag; }
            set { _assignFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OldSerialNo", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string OldSerialNo
        {
            get { return _oldSerialNo; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for OldSerialNo", value, value.ToString());
                _oldSerialNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestTypeNo
        {
            get { return _testTypeNo; }
            set { _testTypeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCheckFee", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCheckFee
        {
            get { return _isCheckFee; }
            set { _isCheckFee = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CountNodesFormSource", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual string CountNodesFormSource
        {
            get { return _countNodesFormSource; }
            set
            {
                if (value != null && value.Length > 1)
                    throw new ArgumentOutOfRangeException("Invalid value for CountNodesFormSource", value, value.ToString());
                _countNodesFormSource = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Dr2Flag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Dr2Flag
        {
            get { return _dr2Flag; }
            set { _dr2Flag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ExecDeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ExecDeptNo
        {
            get { return _execDeptNo; }
            set { _execDeptNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ClientHost", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string ClientHost
        {
            get { return _clientHost; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for ClientHost", value, value.ToString());
                _clientHost = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PreNumber", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PreNumber
        {
            get { return _preNumber; }
            set { _preNumber = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Clientno", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Clientno
        {
            get { return _clientno; }
            set { _clientno = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StateFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int StateFlag
        {
            get { return _stateFlag; }
            set { _stateFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispenseFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispenseFlag
        {
            get { return _dispenseFlag; }
            set { _dispenseFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefuseUser", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string RefuseUser
        {
            get { return _refuseUser; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for RefuseUser", value, value.ToString());
                _refuseUser = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "RefuseTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RefuseTime
        {
            get { return _refuseTime; }
            set { _refuseTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Jytype", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Jytype
        {
            get { return _jytype; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Jytype", value, value.ToString());
                _jytype = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SerialScanTime", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string SerialScanTime
        {
            get { return _serialScanTime; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for SerialScanTime", value, value.ToString());
                _serialScanTime = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AffirmTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AffirmTime
        {
            get { return _affirmTime; }
            set { _affirmTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UrgentState", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string UrgentState
        {
            get { return _urgentState; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UrgentState", value, value.ToString());
                _urgentState = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY6", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY6
        {
            get { return _zDY6; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY6", value, value.ToString());
                _zDY6 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY7", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY7
        {
            get { return _zDY7; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY7", value, value.ToString());
                _zDY7 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY8", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY8
        {
            get { return _zDY8; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY8", value, value.ToString());
                _zDY8 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY9", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY9
        {
            get { return _zDY9; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY9", value, value.ToString());
                _zDY9 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY10", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY10
        {
            get { return _zDY10; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY10", value, value.ToString());
                _zDY10 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhoneCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PhoneCode
        {
            get { return _phoneCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PhoneCode", value, value.ToString());
                _phoneCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsNode", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsNode
        {
            get { return _isNode; }
            set { _isNode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhoneNodeCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PhoneNodeCount
        {
            get { return _phoneNodeCount; }
            set { _phoneNodeCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AutoNodeCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AutoNodeCount
        {
            get { return _autoNodeCount; }
            set { _autoNodeCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsNurseDo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsNurseDo
        {
            get { return _isNurseDo; }
            set { _isNurseDo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportFlag
        {
            get { return _reportFlag; }
            set { _reportFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatPhoto", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] PatPhoto
        {
            get { return _patPhoto; }
            set { _patPhoto = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeOrderNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ChargeOrderNo
        {
            get { return _chargeOrderNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ChargeOrderNo", value, value.ToString());
                _chargeOrderNo = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "NurseSendTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? NurseSendTime
        {
            get { return _nurseSendTime; }
            set { _nurseSendTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NurseSendCarrier", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string NurseSendCarrier
        {
            get { return _nurseSendCarrier; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for NurseSendCarrier", value, value.ToString());
                _nurseSendCarrier = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CollectCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CollectCount
        {
            get { return _collectCount; }
            set { _collectCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ForeignSendFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ForeignSendFlag
        {
            get { return _foreignSendFlag; }
            set { _foreignSendFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisAffirm", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int HisAffirm
        {
            get { return _hisAffirm; }
            set { _hisAffirm = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDoctorId", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisDoctorId
        {
            get { return _hisDoctorId; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HisDoctorId", value, value.ToString());
                _hisDoctorId = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDoctorPhoneCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisDoctorPhoneCode
        {
            get { return _hisDoctorPhoneCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HisDoctorPhoneCode", value, value.ToString());
                _hisDoctorPhoneCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisDoctorNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int LisDoctorNo
        {
            get { return _lisDoctorNo; }
            set { _lisDoctorNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportDateDesc", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ReportDateDesc
        {
            get { return _reportDateDesc; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ReportDateDesc", value, value.ToString());
                _reportDateDesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatState", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PatState
        {
            get { return _patState; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PatState", value, value.ToString());
                _patState = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Balance", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NurseSendNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string NurseSendNo
        {
            get { return _nurseSendNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for NurseSendNo", value, value.ToString());
                _nurseSendNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CollectPack", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CollectPack
        {
            get { return _collectPack; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CollectPack", value, value.ToString());
                _collectPack = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NurseSendFlag", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string NurseSendFlag
        {
            get { return _nurseSendFlag; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for NurseSendFlag", value, value.ToString());
                _nurseSendFlag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoInputflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DoInputflag
        {
            get { return _doInputflag; }
            set { _doInputflag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CarrierCode", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string CarrierCode
        {
            get { return _carrierCode; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for CarrierCode", value, value.ToString());
                _carrierCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ArriveHost", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ArriveHost
        {
            get { return _arriveHost; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ArriveHost", value, value.ToString());
                _arriveHost = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ArriveTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ArriveTime
        {
            get { return _arriveTime; }
            set { _arriveTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsSendMsg", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsSendMsg
        {
            get { return _isSendMsg; }
            set { _isSendMsg = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SignIntervalTime", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SignIntervalTime
        {
            get { return _signIntervalTime; }
            set { _signIntervalTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SignOverTimeFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SignOverTimeFlag
        {
            get { return _signOverTimeFlag; }
            set { _signOverTimeFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InceptHost", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string InceptHost
        {
            get { return _inceptHost; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for InceptHost", value, value.ToString());
                _inceptHost = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NewLisCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string NewLisCode
        {
            get { return _newLisCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for NewLisCode", value, value.ToString());
                _newLisCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NewLisCodeDisp", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string NewLisCodeDisp
        {
            get { return _newLisCodeDisp; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for NewLisCodeDisp", value, value.ToString());
                _newLisCodeDisp = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Remover", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Remover
        {
            get { return _remover; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Remover", value, value.ToString());
                _remover = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "RemoveTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RemoveTime
        {
            get { return _removeTime; }
            set { _removeTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RemoveHost", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string RemoveHost
        {
            get { return _removeHost; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RemoveHost", value, value.ToString());
                _removeHost = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Conceder", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Conceder
        {
            get { return _conceder; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Conceder", value, value.ToString());
                _conceder = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ConcedeTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ConcedeTime
        {
            get { return _concedeTime; }
            set { _concedeTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ConcedeHost", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ConcedeHost
        {
            get { return _concedeHost; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ConcedeHost", value, value.ToString());
                _concedeHost = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ConcedeInfo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ConcedeInfo
        {
            get { return _concedeInfo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ConcedeInfo", value, value.ToString());
                _concedeInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefuseID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int RefuseID
        {
            get { return _refuseID; }
            set { _refuseID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InceptID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int InceptID
        {
            get { return _inceptID; }
            set { _inceptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperCliet", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OperCliet
        {
            get { return _operCliet; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OperCliet", value, value.ToString());
                _operCliet = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GroupSigner", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string GroupSigner
        {
            get { return _groupSigner; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GroupSigner", value, value.ToString());
                _groupSigner = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GroupSignID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int GroupSignID
        {
            get { return _groupSignID; }
            set { _groupSignID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GroupSignTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? GroupSignTime
        {
            get { return _groupSignTime; }
            set { _groupSignTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GroupSignHost", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string GroupSignHost
        {
            get { return _groupSignHost; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GroupSignHost", value, value.ToString());
                _groupSignHost = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CollectPart", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CollectPart
        {
            get { return _collectPart; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CollectPart", value, value.ToString());
                _collectPart = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefuseComment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string RefuseComment
        {
            get { return _refuseComment; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for RefuseComment", value, value.ToString());
                _refuseComment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Mergeno", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Mergeno
        {
            get { return _mergeno; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Mergeno", value, value.ToString());
                _mergeno = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy11", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy11
        {
            get { return _zdy11; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy11", value, value.ToString());
                _zdy11 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy12", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy12
        {
            get { return _zdy12; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy12", value, value.ToString());
                _zdy12 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy13", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy13
        {
            get { return _zdy13; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy13", value, value.ToString());
                _zdy13 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy14", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy14
        {
            get { return _zdy14; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy14", value, value.ToString());
                _zdy14 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy15", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy15
        {
            get { return _zdy15; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy15", value, value.ToString());
                _zdy15 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy16", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy16
        {
            get { return _zdy16; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy16", value, value.ToString());
                _zdy16 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy17", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy17
        {
            get { return _zdy17; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy17", value, value.ToString());
                _zdy17 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy18", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zdy18
        {
            get { return _zdy18; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy18", value, value.ToString());
                _zdy18 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy19", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zdy19
        {
            get { return _zdy19; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy19", value, value.ToString());
                _zdy19 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy20", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zdy20
        {
            get { return _zdy20; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy20", value, value.ToString());
                _zdy20 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalizedTimes", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int HospitalizedTimes
        {
            get { return _hospitalizedTimes; }
            set { _hospitalizedTimes = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisSampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string HisSampleTypeNo
        {
            get { return _hisSampleTypeNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for HisSampleTypeNo", value, value.ToString());
                _hisSampleTypeNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDeptNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string HisDeptNo
        {
            get { return _hisDeptNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for HisDeptNo", value, value.ToString());
                _hisDeptNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDoctor", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string HisDoctor
        {
            get { return _hisDoctor; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for HisDoctor", value, value.ToString());
                _hisDoctor = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TranSport", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string TranSport
        {
            get { return _tranSport; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for TranSport", value, value.ToString());
                _tranSport = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CollectFlag", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CollectFlag
        {
            get { return _collectFlag; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CollectFlag", value, value.ToString());
                _collectFlag = value;
            }
        }


        #endregion
    }
    #endregion
}