using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBReqForm

    /// <summary>
    /// 医嘱申请不能按就诊类型,申请类型,科室,医生进行联合查询,因为相关数据项都是空的多
    /// BloodBReqForm object for NHibernate mapped table 'Blood_BReqForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBReqForm", ShortCode = "BloodBReqForm", Desc = "")]
    public class BloodBReqForm : BaseEntityServiceByString
    {
        #region Member Variables

        //protected string _bReqFormID;
        protected DateTime? _reqTime;
        protected string _patID;
        protected string _patNo;
        protected string _cName;
        protected string _sex;
        protected int? _age;
        protected string _ageALL;
        protected string _ageUnit;
        protected double? _patHeight;
        protected double? _patWeight;
        protected DateTime? _birthday;
        protected int? _deptNo;
        protected int? _doctorNo;
        protected string _diag;
        protected string _beforUse;
        protected string _gravida;
        protected string _harm;
        protected string _help;
        protected string _drag;
        protected string _addressType;
        protected int? _bPatStatusID;
        protected DateTime? _useTime;
        protected int? _useTypeID;
        protected int _bUseTimeTypeID;
        protected string _usePurpose;
        protected int? _bloodABONo;
        protected int? _reqBloodABONo;
        protected int? _bPresOutFlag;
        protected int? _testFlag;
        protected int? _immFlag;
        protected int? _newbornflag;
        protected string _bed;
        protected string _barCode;
        protected string _sfzNO;
        protected string _reqDoctor;
        protected DateTime? _reqIDTime;
        protected string _patientCount;
        protected string _bloodOrderID;
        protected int? _bReqTypeID;
        protected double? _estCount;
        protected string _usePlaceID;
        protected string _discordNo;
        protected string _zx1;
        protected string _zx2;
        protected string _zx3;
        protected string _memo;
        protected string _birth;
        protected int? _postflag;
        protected DateTime? _toHosdate;
        protected string _wristBandNo;
        protected string _emergentFlag;
        protected string _hisABOCode;
        protected string _hisrhCode;
        protected int? _visitID;
        protected string _costType;
        protected int _bReqFormFlag;
        protected string _bPreMemo;
        protected string _bPreMemoEditID;
        protected DateTime? _bPreMemoTime;
        protected string _bdeptCheckID;
        protected DateTime? _bdeptCheckDate;
        protected string _bPreMemoNo;
        protected int? _reCheckBloodABONo;
        protected string _reChecker;
        protected DateTime? _reCheckDatetime;
        protected DateTime? _patindate;
        protected string _isSame;
        protected string _aBORhDesc;
        protected string _reviewABORhdesc;
        protected string _getbloodHint;
        protected string _evaluation;
        protected string _pataddress;
        protected string _aggluName;
        protected string _aggluMemo;
        protected string _patIdentity;
        protected string _barCodeMemo;
        protected string _transplant;
        protected DateTime? _transdate;
        protected string _donorABOrh;
        protected string _assessFormID;
        protected string _breqMainNo;
        protected string _docphone;
        protected string _yqCode;
        protected string _bprotect;
        protected string _cardio;
        protected string _decom;
        protected string _around;
        protected string _icdcode;
        protected string _lostbcount;
        protected string _lostspeed;
        protected string _bodytpe;
        protected string _bpress;
        protected string _breathe;
        protected string _pulse;
        protected string _heartrate;
        protected string _urine;
        protected string _anesth;
        protected int? _dispOrder;
        protected bool _visible;
        protected string _hisDeptID;
        protected string _hisDoctorId;
        protected bool _checkCompleteFlag;
        protected DateTime? _checkCompleteTime;
        protected long? _applyID;
        protected string _applyName;
        protected DateTime? _applyTime;
        protected string _applyMemo;
        protected long? _seniorID;
        protected string _seniorName;
        protected DateTime? _seniorTime;
        protected string _seniorMemo;
        protected long? _directorID;
        protected string _directorName;
        protected DateTime? _directorTime;
        protected string _directorMemo;
        protected long? _medicalID;
        protected string _medicalName;
        protected DateTime? _medicalTime;
        protected string _medicalMemo;
        protected long? _obsoleteID;
        protected string _obsoleteName;
        protected DateTime? _obsoleteTime;
        protected string _obsoleteMemo;
        protected long? _breqStatusID;
        protected string _breqStatusName;

        protected string _lisALB;
        protected string _lisALT;
        protected string _lisAPTT;
        protected string _lisFbg;
        protected string _lisHBc;
        protected string _lisHBe;
        protected string _lisHBeAg;
        protected string _lisHCT;
        protected string _lisHCV;
        protected string _lisHGB;
        protected string _lisHIV;
        protected string _lisPLT;
        protected string _lisPT;
        protected string _lisRBC;
        protected string _lisRPR;
        protected string _lisTT;
        protected string _lisWBC;
        protected string _lisHBs;
        protected string _lisHBsAg;
        protected string _patABO;
        protected string _patRh;
        protected string _bloodWay;
        protected double? _reqTotal;
        protected int? _toHisFlag;
        protected string _admID;
        protected long? _obsoleteMemoId;
        protected string _organTransplant;
        protected string _marrowTransplantation;
        protected string _wardNo;
        protected string _hisWardNo;
        protected string _bLPreEvaluation;
        protected int _printTotal;
        protected bool _hasAllergy;

        #endregion

        #region Constructors

        public BloodBReqForm() { }

        public BloodBReqForm(DateTime reqTime, string patID, string patNo, string cName, string sex, int age, string ageALL, string ageUnit, double patHeight, double patWeight, DateTime birthday, int deptNo, int doctorNo, string diag, string beforUse, string gravida, string harm, string help, string drag, string addressType, int bPatStatusID, DateTime useTime, int useTypeID, int bUseTimeTypeID, string usePurpose, int bloodABONo, int reqBloodABONo, int bPresOutFlag, int testFlag, int immFlag, int newbornflag, string bed, string barCode, string sfzNO, string reqDoctor, DateTime reqIDTime, string patientCount, string bloodOrderID, int bReqTypeID, double estCount, string usePlaceID, string discordNo, string zx1, string zx2, string zx3, string memo, string birth, int postflag, DateTime toHosdate, string wristBandNo, string emergentFlag, string hisABOCode, string hisrhCode, int visitID, string costType, int bReqFormFlag, string bPreMemo, string bPreMemoEditID, DateTime bPreMemoTime, string bdeptCheckID, DateTime bdeptCheckDate, string bPreMemoNo, int reCheckBloodABONo, string reChecker, DateTime reCheckDatetime, DateTime patindate, string isSame, string aBORhDesc, string reviewABORhdesc, string getbloodHint, string evaluation, string pataddress, string aggluName, string aggluMemo, string patIdentity, string barCodeMemo, string transplant, DateTime transdate, string donorABOrh, string assessFormID, string breqMainNo, string docphone, string yqCode, string bprotect, string cardio, string decom, string around, string icdcode, string lostbcount, string lostspeed, string bodytpe, string bpress, string breathe, string pulse, string heartrate, string urine, string anesth, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible, string hisDeptID, string hisDoctorId, bool checkCompleteFlag, DateTime checkCompleteTime, long applyID, string applyName, DateTime applyTime, string applyMemo, long seniorID, string seniorName, DateTime seniorTime, string seniorMemo, long directorID, string directorName, DateTime directorTime, string directorMemo, long medicalID, string medicalName, DateTime medicalTime, string medicalMemo, long breqStatusID, string breqStatusName, string patABO, string patRh, string bloodWay, double reqTotal, long obsoleteID, string obsoleteName, DateTime obsoleteTime, string obsoleteMemo, int toHisFlag, string admID, long obsoleteMemoId, string organTransplant, string marrowTransplantation, string wardNo, string hisWardNo, string bLPreEvaluation,int printTotal, bool hasAllergy)
        {
            this._organTransplant = organTransplant;
            this._marrowTransplantation = marrowTransplantation;
            this._wardNo = wardNo;
            this._hisWardNo = hisWardNo;
            this._bLPreEvaluation = bLPreEvaluation;

            this._reqTime = reqTime;
            this._patID = patID;
            this._patNo = patNo;
            this._cName = cName;
            this._sex = sex;
            this._age = age;
            this._ageALL = ageALL;
            this._ageUnit = ageUnit;
            this._patHeight = patHeight;
            this._patWeight = patWeight;
            this._birthday = birthday;
            this._deptNo = deptNo;
            this._doctorNo = doctorNo;
            this._diag = diag;
            this._beforUse = beforUse;
            this._gravida = gravida;
            this._harm = harm;
            this._help = help;
            this._drag = drag;
            this._addressType = addressType;
            this._bPatStatusID = bPatStatusID;
            this._useTime = useTime;
            this._useTypeID = useTypeID;
            this._bUseTimeTypeID = bUseTimeTypeID;
            this._usePurpose = usePurpose;
            this._bloodABONo = bloodABONo;
            this._reqBloodABONo = reqBloodABONo;
            this._bPresOutFlag = bPresOutFlag;
            this._testFlag = testFlag;
            this._immFlag = immFlag;
            this._newbornflag = newbornflag;
            this._bed = bed;
            this._barCode = barCode;
            this._sfzNO = sfzNO;
            this._reqDoctor = reqDoctor;
            this._reqIDTime = reqIDTime;
            this._patientCount = patientCount;
            this._bloodOrderID = bloodOrderID;
            this._bReqTypeID = bReqTypeID;
            this._estCount = estCount;
            this._usePlaceID = usePlaceID;
            this._discordNo = discordNo;
            this._zx1 = zx1;
            this._zx2 = zx2;
            this._zx3 = zx3;
            this._memo = memo;
            this._birth = birth;
            this._postflag = postflag;
            this._toHosdate = toHosdate;
            this._wristBandNo = wristBandNo;
            this._emergentFlag = emergentFlag;
            this._hisABOCode = hisABOCode;
            this._hisrhCode = hisrhCode;
            this._visitID = visitID;
            this._costType = costType;
            this._bReqFormFlag = bReqFormFlag;
            this._bPreMemo = bPreMemo;
            this._bPreMemoEditID = bPreMemoEditID;
            this._bPreMemoTime = bPreMemoTime;
            this._bdeptCheckID = bdeptCheckID;
            this._bdeptCheckDate = bdeptCheckDate;
            this._bPreMemoNo = bPreMemoNo;
            this._reCheckBloodABONo = reCheckBloodABONo;
            this._reChecker = reChecker;
            this._reCheckDatetime = reCheckDatetime;
            this._patindate = patindate;
            this._isSame = isSame;
            this._aBORhDesc = aBORhDesc;
            this._reviewABORhdesc = reviewABORhdesc;
            this._getbloodHint = getbloodHint;
            this._evaluation = evaluation;
            this._pataddress = pataddress;
            this._aggluName = aggluName;
            this._aggluMemo = aggluMemo;
            this._patIdentity = patIdentity;
            this._barCodeMemo = barCodeMemo;
            this._transplant = transplant;
            this._transdate = transdate;
            this._donorABOrh = donorABOrh;
            this._assessFormID = assessFormID;
            this._breqMainNo = breqMainNo;
            this._docphone = docphone;
            this._yqCode = yqCode;
            this._bprotect = bprotect;
            this._cardio = cardio;
            this._decom = decom;
            this._around = around;
            this._icdcode = icdcode;
            this._lostbcount = lostbcount;
            this._lostspeed = lostspeed;
            this._bodytpe = bodytpe;
            this._bpress = bpress;
            this._breathe = breathe;
            this._pulse = pulse;
            this._heartrate = heartrate;
            this._urine = urine;
            this._anesth = anesth;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
            this._hisDeptID = hisDeptID;
            this._hisDoctorId = hisDoctorId;
            this._checkCompleteFlag = checkCompleteFlag;
            this._checkCompleteTime = checkCompleteTime;

            this._applyID = applyID;
            this._applyName = applyName;
            this._applyTime = applyTime;
            this._applyMemo = applyMemo;
            this._seniorID = seniorID;
            this._seniorName = seniorName;
            this._seniorTime = seniorTime;
            this._seniorMemo = seniorMemo;
            this._directorID = directorID;
            this._directorName = directorName;
            this._directorTime = directorTime;
            this._directorMemo = directorMemo;
            this._medicalID = medicalID;
            this._medicalName = medicalName;
            this._medicalTime = medicalTime;
            this._medicalMemo = medicalMemo;
            this._breqStatusID = breqStatusID;
            this._breqStatusName = breqStatusName;
            this._patABO = patABO;
            this._patRh = patRh;
            this._bloodWay = bloodWay;
            this._reqTotal = reqTotal;
            this._obsoleteID = obsoleteID;
            this._obsoleteName = obsoleteName;
            this._obsoleteTime = obsoleteTime;
            this._obsoleteMemo = obsoleteMemo;
            this._toHisFlag = toHisFlag;
            this._admID = admID;
            this._obsoleteMemoId = obsoleteMemoId;
            this._printTotal = printTotal;
            this._hasAllergy = hasAllergy;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ReqTime", ShortCode = "ReqTime", Desc = "ReqTime", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReqTime
        {
            get { return _reqTime; }
            set { _reqTime = value; }
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
        [DataDesc(CName = "", ShortCode = "PatNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PatNo
        {
            get { return _patNo; }
            set
            {
                if (value != null && value.Length > 50)
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
        [DataDesc(CName = "", ShortCode = "Sex", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Sex
        {
            get { return _sex; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Sex", value, value.ToString());
                _sex = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Age", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? Age
        {
            get { return _age; }
            set { _age = value; }
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
        [DataDesc(CName = "", ShortCode = "AgeUnit", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string AgeUnit
        {
            get { return _ageUnit; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for AgeUnit", value, value.ToString());
                _ageUnit = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PatHeight", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? PatHeight
        {
            get { return _patHeight; }
            set { _patHeight = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PatWeight", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? PatWeight
        {
            get { return _patWeight; }
            set { _patWeight = value; }
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
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoctorNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? DoctorNo
        {
            get { return _doctorNo; }
            set { _doctorNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Diag", Desc = "", ContextType = SysDic.All, Length = 255)]
        public virtual string Diag
        {
            get { return _diag; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for Diag", value, value.ToString());
                _diag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "BeforUse", ShortCode = "BeforUse", Desc = "输血史(有;无;)", ContextType = SysDic.All, Length = 20)]
        public virtual string BeforUse
        {
            get { return _beforUse; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BeforUse", value, value.ToString());
                _beforUse = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Gravida", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Gravida
        {
            get { return _gravida; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Gravida", value, value.ToString());
                _gravida = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Harm", ShortCode = "Harm", Desc = "输血反应(有;无;)", ContextType = SysDic.All, Length = 20)]
        public virtual string Harm
        {
            get { return _harm; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Harm", value, value.ToString());
                _harm = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Help", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Help
        {
            get { return _help; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Help", value, value.ToString());
                _help = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Drag", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Drag
        {
            get { return _drag; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Drag", value, value.ToString());
                _drag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AddressType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AddressType
        {
            get { return _addressType; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for AddressType", value, value.ToString());
                _addressType = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BPatStatusID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BPatStatusID
        {
            get { return _bPatStatusID; }
            set { _bPatStatusID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "UseTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? UseTime
        {
            get { return _useTime; }
            set { _useTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "UseTypeID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? UseTypeID
        {
            get { return _useTypeID; }
            set { _useTypeID = value; }
        }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "BUseTimeTypeID", ShortCode = "BUseTimeTypeID", Desc = "急查标志(0:否;1:是;)", ContextType = SysDic.All, Length = 4)]
        public virtual int BUseTimeTypeID
        {
            get { return _bUseTimeTypeID; }
            set { _bUseTimeTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "UsePurpose", ShortCode = "UsePurpose", Desc = "UsePurpose", ContextType = SysDic.All, Length = 500)]
        public virtual string UsePurpose
        {
            get { return _usePurpose; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for UsePurpose", value, value.ToString());
                _usePurpose = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BloodABONo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BloodABONo
        {
            get { return _bloodABONo; }
            set { _bloodABONo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReqBloodABONo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? ReqBloodABONo
        {
            get { return _reqBloodABONo; }
            set { _reqBloodABONo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BPresOutFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BPresOutFlag
        {
            get { return _bPresOutFlag; }
            set { _bPresOutFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TestFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? TestFlag
        {
            get { return _testFlag; }
            set { _testFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ImmFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? ImmFlag
        {
            get { return _immFlag; }
            set { _immFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Newbornflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? Newbornflag
        {
            get { return _newbornflag; }
            set { _newbornflag = value; }
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
        [DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string BarCode
        {
            get { return _barCode; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
                _barCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SfzNO", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SfzNO
        {
            get { return _sfzNO; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SfzNO", value, value.ToString());
                _sfzNO = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReqDoctor", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ReqDoctor
        {
            get { return _reqDoctor; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ReqDoctor", value, value.ToString());
                _reqDoctor = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReqIDTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReqIDTime
        {
            get { return _reqIDTime; }
            set { _reqIDTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatientCount", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string PatientCount
        {
            get { return _patientCount; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for PatientCount", value, value.ToString());
                _patientCount = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodOrderID", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BloodOrderID
        {
            get { return _bloodOrderID; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodOrderID", value, value.ToString());
                _bloodOrderID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BReqTypeID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BReqTypeID
        {
            get { return _bReqTypeID; }
            set { _bReqTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EstCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? EstCount
        {
            get { return _estCount; }
            set { _estCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UsePlaceID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string UsePlaceID
        {
            get { return _usePlaceID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UsePlaceID", value, value.ToString());
                _usePlaceID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DiscordNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DiscordNo
        {
            get { return _discordNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DiscordNo", value, value.ToString());
                _discordNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zx1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zx1
        {
            get { return _zx1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zx1", value, value.ToString());
                _zx1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zx2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zx2
        {
            get { return _zx2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zx2", value, value.ToString());
                _zx2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zx3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zx3
        {
            get { return _zx3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zx3", value, value.ToString());
                _zx3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Birth", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Birth
        {
            get { return _birth; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Birth", value, value.ToString());
                _birth = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Postflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? Postflag
        {
            get { return _postflag; }
            set { _postflag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ToHosdate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ToHosdate
        {
            get { return _toHosdate; }
            set { _toHosdate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WristBandNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string WristBandNo
        {
            get { return _wristBandNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for WristBandNo", value, value.ToString());
                _wristBandNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EmergentFlag", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EmergentFlag
        {
            get { return _emergentFlag; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for EmergentFlag", value, value.ToString());
                _emergentFlag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisABOCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisABOCode
        {
            get { return _hisABOCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HisABOCode", value, value.ToString());
                _hisABOCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisrhCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisrhCode
        {
            get { return _hisrhCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HisrhCode", value, value.ToString());
                _hisrhCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "VisitID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? VisitID
        {
            get { return _visitID; }
            set { _visitID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CostType", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CostType
        {
            get { return _costType; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CostType", value, value.ToString());
                _costType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BReqFormFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BReqFormFlag
        {
            get { return _bReqFormFlag; }
            set { _bReqFormFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreMemo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BPreMemo
        {
            get { return _bPreMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreMemo", value, value.ToString());
                _bPreMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreMemoEditID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreMemoEditID
        {
            get { return _bPreMemoEditID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreMemoEditID", value, value.ToString());
                _bPreMemoEditID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BPreMemoTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BPreMemoTime
        {
            get { return _bPreMemoTime; }
            set { _bPreMemoTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BdeptCheckID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BdeptCheckID
        {
            get { return _bdeptCheckID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BdeptCheckID", value, value.ToString());
                _bdeptCheckID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BdeptCheckDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BdeptCheckDate
        {
            get { return _bdeptCheckDate; }
            set { _bdeptCheckDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreMemoNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreMemoNo
        {
            get { return _bPreMemoNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreMemoNo", value, value.ToString());
                _bPreMemoNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReCheckBloodABONo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? ReCheckBloodABONo
        {
            get { return _reCheckBloodABONo; }
            set { _reCheckBloodABONo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReChecker", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ReChecker
        {
            get { return _reChecker; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReChecker", value, value.ToString());
                _reChecker = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReCheckDatetime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReCheckDatetime
        {
            get { return _reCheckDatetime; }
            set { _reCheckDatetime = value; }
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
        [DataDesc(CName = "", ShortCode = "IsSame", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string IsSame
        {
            get { return _isSame; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for IsSame", value, value.ToString());
                _isSame = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ABORhDesc", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ABORhDesc
        {
            get { return _aBORhDesc; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ABORhDesc", value, value.ToString());
                _aBORhDesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReviewABORhdesc", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ReviewABORhdesc
        {
            get { return _reviewABORhdesc; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ReviewABORhdesc", value, value.ToString());
                _reviewABORhdesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GetbloodHint", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string GetbloodHint
        {
            get { return _getbloodHint; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for GetbloodHint", value, value.ToString());
                _getbloodHint = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Evaluation", Desc = "", ContextType = SysDic.All, Length = 255)]
        public virtual string Evaluation
        {
            get { return _evaluation; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for Evaluation", value, value.ToString());
                _evaluation = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Pataddress", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Pataddress
        {
            get { return _pataddress; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Pataddress", value, value.ToString());
                _pataddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AggluName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AggluName
        {
            get { return _aggluName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for AggluName", value, value.ToString());
                _aggluName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AggluMemo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AggluMemo
        {
            get { return _aggluMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for AggluMemo", value, value.ToString());
                _aggluMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatIdentity", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PatIdentity
        {
            get { return _patIdentity; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PatIdentity", value, value.ToString());
                _patIdentity = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BarCodeMemo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BarCodeMemo
        {
            get { return _barCodeMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BarCodeMemo", value, value.ToString());
                _barCodeMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Transplant", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Transplant
        {
            get { return _transplant; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Transplant", value, value.ToString());
                _transplant = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Transdate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Transdate
        {
            get { return _transdate; }
            set { _transdate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DonorABOrh", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string DonorABOrh
        {
            get { return _donorABOrh; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for DonorABOrh", value, value.ToString());
                _donorABOrh = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AssessFormID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AssessFormID
        {
            get { return _assessFormID; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for AssessFormID", value, value.ToString());
                _assessFormID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BreqMainNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BreqMainNo
        {
            get { return _breqMainNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BreqMainNo", value, value.ToString());
                _breqMainNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Docphone", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Docphone
        {
            get { return _docphone; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Docphone", value, value.ToString());
                _docphone = value;
            }
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
        [DataDesc(CName = "", ShortCode = "Bprotect", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Bprotect
        {
            get { return _bprotect; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Bprotect", value, value.ToString());
                _bprotect = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Cardio", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Cardio
        {
            get { return _cardio; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Cardio", value, value.ToString());
                _cardio = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Decom", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Decom
        {
            get { return _decom; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Decom", value, value.ToString());
                _decom = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Around", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Around
        {
            get { return _around; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Around", value, value.ToString());
                _around = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Icdcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Icdcode
        {
            get { return _icdcode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Icdcode", value, value.ToString());
                _icdcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Lostbcount", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Lostbcount
        {
            get { return _lostbcount; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Lostbcount", value, value.ToString());
                _lostbcount = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Lostspeed", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Lostspeed
        {
            get { return _lostspeed; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Lostspeed", value, value.ToString());
                _lostspeed = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bodytpe", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Bodytpe
        {
            get { return _bodytpe; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Bodytpe", value, value.ToString());
                _bodytpe = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bpress", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Bpress
        {
            get { return _bpress; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Bpress", value, value.ToString());
                _bpress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Breathe", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Breathe
        {
            get { return _breathe; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Breathe", value, value.ToString());
                _breathe = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Pulse", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Pulse
        {
            get { return _pulse; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Pulse", value, value.ToString());
                _pulse = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Heartrate", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Heartrate
        {
            get { return _heartrate; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Heartrate", value, value.ToString());
                _heartrate = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Urine", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Urine
        {
            get { return _urine; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Urine", value, value.ToString());
                _urine = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Anesth", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Anesth
        {
            get { return _anesth; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Anesth", value, value.ToString());
                _anesth = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? DispOrder
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

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDeptID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisDeptID
        {
            get { return _hisDeptID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HisDeptID", value, value.ToString());
                _hisDeptID = value;
            }
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
        [DataDesc(CName = "", ShortCode = "CheckCompleteFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool CheckCompleteFlag
        {
            get { return _checkCompleteFlag; }
            set { _checkCompleteFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckCompleteTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckCompleteTime
        {
            get { return _checkCompleteTime; }
            set { _checkCompleteTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "提交申请人Id", ShortCode = "ApplyID", Desc = "提交申请人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApplyID
        {
            get { return _applyID; }
            set { _applyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "提交申请人", ShortCode = "ApplyName", Desc = "提交申请人", ContextType = SysDic.All, Length = 20)]
        public virtual string ApplyName
        {
            get { return _applyName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ApplyName", value, value.ToString());
                _applyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "提交申请时间", ShortCode = "ApplyTime", Desc = "提交申请时间", ContextType = SysDic.DateTime, Length = 8)]
        public virtual DateTime? ApplyTime
        {
            get { return _applyTime; }
            set { _applyTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ApplyMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ApplyMemo
        {
            get { return _applyMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ApplyMemo", value, value.ToString());
                _applyMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SeniorID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SeniorID
        {
            get { return _seniorID; }
            set { _seniorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SeniorName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SeniorName
        {
            get { return _seniorName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SeniorName", value, value.ToString());
                _seniorName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SeniorTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SeniorTime
        {
            get { return _seniorTime; }
            set { _seniorTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SeniorMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string SeniorMemo
        {
            get { return _seniorMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for SeniorMemo", value, value.ToString());
                _seniorMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DirectorID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DirectorID
        {
            get { return _directorID; }
            set { _directorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DirectorName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DirectorName
        {
            get { return _directorName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DirectorName", value, value.ToString());
                _directorName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DirectorTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DirectorTime
        {
            get { return _directorTime; }
            set { _directorTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DirectorMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string DirectorMemo
        {
            get { return _directorMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for DirectorMemo", value, value.ToString());
                _directorMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MedicalID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? MedicalID
        {
            get { return _medicalID; }
            set { _medicalID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MedicalName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string MedicalName
        {
            get { return _medicalName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for MedicalName", value, value.ToString());
                _medicalName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MedicalTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? MedicalTime
        {
            get { return _medicalTime; }
            set { _medicalTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MedicalMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string MedicalMemo
        {
            get { return _medicalMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for MedicalMemo", value, value.ToString());
                _medicalMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ObsoleteID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ObsoleteID
        {
            get { return _obsoleteID; }
            set { _obsoleteID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ObsoleteName", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "ObsoleteTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ObsoleteTime
        {
            get { return _obsoleteTime; }
            set { _obsoleteTime = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废原因Id", ShortCode = "ObsoleteMemoId", Desc = "作废原因Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ObsoleteMemoId
        {
            get { return _obsoleteMemoId; }
            set { _obsoleteMemoId = value; }
        }
        [DataMember]
        [DataDesc(CName = "作废原因", ShortCode = "ObsoleteMemo", Desc = "作废原因", ContextType = SysDic.All, Length = 200)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BreqStatusID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? BreqStatusID
        {
            get { return _breqStatusID; }
            set { _breqStatusID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BreqStatusName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BreqStatusName
        {
            get { return _breqStatusName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BreqStatusName", value, value.ToString());
                _breqStatusName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisALB", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisALB
        {
            get { return _lisALB; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisALB", value, value.ToString());
                _lisALB = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisALT", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisALT
        {
            get { return _lisALT; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisALT", value, value.ToString());
                _lisALT = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisAPTT", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisAPTT
        {
            get { return _lisAPTT; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisAPTT", value, value.ToString());
                _lisAPTT = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisFbg", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisFbg
        {
            get { return _lisFbg; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisFbg", value, value.ToString());
                _lisFbg = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisHBc", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisHBc
        {
            get { return _lisHBc; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisHBc", value, value.ToString());
                _lisHBc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisHBe", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisHBe
        {
            get { return _lisHBe; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisHBe", value, value.ToString());
                _lisHBe = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisHBeAg", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisHBeAg
        {
            get { return _lisHBeAg; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisHBeAg", value, value.ToString());
                _lisHBeAg = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisHCT", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisHCT
        {
            get { return _lisHCT; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisHCT", value, value.ToString());
                _lisHCT = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisHCV", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisHCV
        {
            get { return _lisHCV; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisHCV", value, value.ToString());
                _lisHCV = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisHGB", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisHGB
        {
            get { return _lisHGB; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisHGB", value, value.ToString());
                _lisHGB = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisHIV", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisHIV
        {
            get { return _lisHIV; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisHIV", value, value.ToString());
                _lisHIV = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisPLT", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisPLT
        {
            get { return _lisPLT; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisPLT", value, value.ToString());
                _lisPLT = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisPT", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisPT
        {
            get { return _lisPT; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisPT", value, value.ToString());
                _lisPT = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisRBC", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisRBC
        {
            get { return _lisRBC; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisRBC", value, value.ToString());
                _lisRBC = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisRPR", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisRPR
        {
            get { return _lisRPR; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisRPR", value, value.ToString());
                _lisRPR = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisTT", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisTT
        {
            get { return _lisTT; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisTT", value, value.ToString());
                _lisTT = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisWBC", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisWBC
        {
            get { return _lisWBC; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisWBC", value, value.ToString());
                _lisWBC = value;
            }
        }


        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisHBs", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisHBs
        {
            get { return _lisHBs; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisHBs", value, value.ToString());
                _lisHBs = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisHBsAg", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisHBsAg
        {
            get { return _lisHBsAg; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LisHBsAg", value, value.ToString());
                _lisHBsAg = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatABO", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PatABO
        {
            get { return _patABO; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PatABO", value, value.ToString());
                _patABO = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PatRh", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PatRh
        {
            get { return _patRh; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PatRh", value, value.ToString());
                _patRh = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "BloodWay", ShortCode = "BloodWay", Desc = "BloodWay", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodWay
        {
            get { return _bloodWay; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodWay", value, value.ToString());
                _bloodWay = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "24小时内申请总量", ShortCode = "ReqTotal", Desc = "24小时内申请总量", ContextType = SysDic.All, Length = 8)]
        public virtual double? ReqTotal
        {
            get { return _reqTotal; }
            set { _reqTotal = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "HIS数据标志:0:默认:1:返回HIS成功;2:返回HIS失败;", ShortCode = "ToHisFlag", Desc = "HIS数据标志:0:默认:1:返回HIS成功;2:返回HIS失败;", ContextType = SysDic.All, Length = 4)]
        public virtual int? ToHisFlag
        {
            get { return _toHisFlag; }
            set { _toHisFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "就诊号", ShortCode = "AdmID", Desc = "就诊号", ContextType = SysDic.All, Length = 50)]
        public virtual string AdmID
        {
            get { return _admID; }
            set
            {
                _admID = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "器官移植(是:1;否:0;)", ShortCode = "OrganTransplant", Desc = "器官移植(是:1;否:0;)", ContextType = SysDic.All, Length = 10)]
        public virtual string OrganTransplant
        {
            get { return _organTransplant; }
            set { _organTransplant = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "器官移植(有;无;)", ShortCode = "MarrowTransplantation", Desc = "器官移植(有;无;)", ContextType = SysDic.All, Length = 10)]
        public virtual string MarrowTransplantation
        {
            get { return _marrowTransplantation; }
            set { _marrowTransplantation = value; }
        }
        [DataMember]
        [DataDesc(CName = "病区代码", ShortCode = "WardNo", Desc = "病区代码", ContextType = SysDic.All, Length = 50)]
        public virtual string WardNo
        {
            get { return _wardNo; }
            set
            {
                _wardNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "HIS病区代码", ShortCode = "HisWardNo", Desc = "HIS病区代码", ContextType = SysDic.All, Length = 50)]
        public virtual string HisWardNo
        {
            get { return _hisWardNo; }
            set
            {
                _hisWardNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "输血前评估", ShortCode = "BLPreEvaluation", Desc = "输血前评估", ContextType = SysDic.All, Length = 50)]
        public virtual string BLPreEvaluation
        {
            get { return _bLPreEvaluation; }
            set
            {
                _bLPreEvaluation = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "打印总数", ShortCode = "PrintTotal", Desc = "打印总数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTotal
        {
            get { return _printTotal; }
            set { _printTotal = value; }
        }
        #endregion

        #region 自定义属性
        protected string _bReqTypeCName;
        protected string _useTypeCName;
        protected string _deptCName;
        protected string _doctorCName;
        protected string _bloodListStr;
        protected string _bUseTimeTypeCName;
        [DataMember]
        [DataDesc(CName = "BUseTimeTypeCName", ShortCode = "BUseTimeTypeCName", Desc = "急查标志(0:否;1:是;)", ContextType = SysDic.All, Length = 20)]
        public virtual string BUseTimeTypeCName
        {
            get { return _bUseTimeTypeCName; }
            set { _bUseTimeTypeCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "就诊类型", ShortCode = "BReqTypeCName", Desc = "就诊类型", ContextType = SysDic.All, Length = 50)]
        public virtual string BReqTypeCName
        {
            get { return _bReqTypeCName; }
            set
            {
                _bReqTypeCName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "申请类型", ShortCode = "UseTypeCName", Desc = "申请类型", ContextType = SysDic.All, Length = 50)]
        public virtual string UseTypeCName
        {
            get { return _useTypeCName; }
            set
            {
                _useTypeCName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "申请科室", ShortCode = "DeptCName", Desc = "申请科室", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptCName
        {
            get { return _deptCName; }
            set
            {
                _deptCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "申请医生", ShortCode = "DoctorCName", Desc = "申请医生", ContextType = SysDic.All, Length = 50)]
        public virtual string DoctorCName
        {
            get { return _doctorCName; }
            set
            {
                _doctorCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Frx模板的血制品信息", ShortCode = "BloodListStr", Desc = "Frx模板的血制品信息", ContextType = SysDic.All, Length = 600)]
        public virtual string BloodListStr
        {
            get { return _bloodListStr; }
            set
            {
                _bloodListStr = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "是否有过敏史", ShortCode = "HasAllergy", Desc = "是否有过敏史", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasAllergy
        {
            get { return _hasAllergy; }
            set { _hasAllergy = value; }
        }
        private void _setBloodBReqForm(BloodBReqForm bloodbreqform)
        {
            this._id = bloodbreqform.Id;
            this._reqTime = bloodbreqform.ReqTime;
            this._patID = bloodbreqform.PatID;
            this._patNo = bloodbreqform.PatNo;
            this._cName = bloodbreqform.CName;
            this._sex = bloodbreqform.Sex;
            this._age = bloodbreqform.Age;
            this._ageALL = bloodbreqform.AgeALL;
            this._ageUnit = bloodbreqform.AgeUnit;
            this._patHeight = bloodbreqform.PatHeight;
            this._patWeight = bloodbreqform.PatWeight;
            this._birthday = bloodbreqform.Birthday;
            this._deptNo = bloodbreqform.DeptNo;
            this._doctorNo = bloodbreqform.DoctorNo;
            this._diag = bloodbreqform.Diag;
            this._beforUse = bloodbreqform.BeforUse;
            this._gravida = bloodbreqform.Gravida;
            this._harm = bloodbreqform.Harm;
            this._help = bloodbreqform.Help;
            this._drag = bloodbreqform.Drag;
            this._addressType = bloodbreqform.AddressType;
            this._bPatStatusID = bloodbreqform.BPatStatusID;
            this._useTime = bloodbreqform.UseTime;
            this._useTypeID = bloodbreqform.UseTypeID;
            this._bUseTimeTypeID = bloodbreqform.BUseTimeTypeID;
            this._bUseTimeTypeCName = (this._bUseTimeTypeID == 1 ? "是" : "否");
            this._usePurpose = bloodbreqform.UsePurpose;
            this._bloodABONo = bloodbreqform.BloodABONo;
            this._reqBloodABONo = bloodbreqform.ReqBloodABONo;
            this._bPresOutFlag = bloodbreqform.BPresOutFlag;
            this._testFlag = bloodbreqform.TestFlag;
            this._immFlag = bloodbreqform.ImmFlag;
            this._newbornflag = bloodbreqform.Newbornflag;
            this._bed = bloodbreqform.Bed;
            this._barCode = bloodbreqform.BarCode;
            this._sfzNO = bloodbreqform.SfzNO;
            this._reqDoctor = bloodbreqform.ReqDoctor;
            this._reqIDTime = bloodbreqform.ReqIDTime;
            this._patientCount = bloodbreqform.PatientCount;
            this._bloodOrderID = bloodbreqform.BloodOrderID;
            this._bReqTypeID = bloodbreqform.BReqTypeID;
            this._estCount = bloodbreqform.EstCount;
            this._usePlaceID = bloodbreqform.UsePlaceID;
            this._discordNo = bloodbreqform.DiscordNo;
            this._zx1 = bloodbreqform.Zx1;
            this._zx2 = bloodbreqform.Zx2;
            this._zx3 = bloodbreqform.Zx3;
            this._memo = bloodbreqform.Memo;
            this._birth = bloodbreqform.Birth;
            this._postflag = bloodbreqform.Postflag;
            this._toHosdate = bloodbreqform.ToHosdate;
            this._wristBandNo = bloodbreqform.WristBandNo;
            this._emergentFlag = bloodbreqform.EmergentFlag;
            this._hisABOCode = bloodbreqform.HisABOCode;
            this._hisrhCode = bloodbreqform.HisrhCode;
            this._visitID = bloodbreqform.VisitID;
            this._costType = bloodbreqform.CostType;
            this._bReqFormFlag = bloodbreqform.BReqFormFlag;
            this._bPreMemo = bloodbreqform.BPreMemo;
            this._bPreMemoEditID = bloodbreqform.BPreMemoEditID;
            this._bPreMemoTime = bloodbreqform.BPreMemoTime;
            this._bdeptCheckID = bloodbreqform.BdeptCheckID;
            this._bdeptCheckDate = bloodbreqform.BdeptCheckDate;
            this._bPreMemoNo = bloodbreqform.BPreMemoNo;
            this._reCheckBloodABONo = bloodbreqform.ReCheckBloodABONo;
            this._reChecker = bloodbreqform.ReChecker;
            this._reCheckDatetime = bloodbreqform.ReCheckDatetime;
            this._patindate = bloodbreqform.Patindate;
            this._isSame = bloodbreqform.IsSame;
            this._aBORhDesc = bloodbreqform.ABORhDesc;
            this._reviewABORhdesc = bloodbreqform.ReviewABORhdesc;
            this._getbloodHint = bloodbreqform.GetbloodHint;
            this._evaluation = bloodbreqform.Evaluation;
            this._pataddress = bloodbreqform.Pataddress;
            this._aggluName = bloodbreqform.AggluName;
            this._aggluMemo = bloodbreqform.AggluMemo;
            this._patIdentity = bloodbreqform.PatIdentity;
            this._barCodeMemo = bloodbreqform.BarCodeMemo;
            this._transplant = bloodbreqform.Transplant;
            this._transdate = bloodbreqform.Transdate;
            this._donorABOrh = bloodbreqform.DonorABOrh;
            this._assessFormID = bloodbreqform.AssessFormID;
            this._breqMainNo = bloodbreqform.BreqMainNo;
            this._docphone = bloodbreqform.Docphone;
            this._yqCode = bloodbreqform.YqCode;
            this._bprotect = bloodbreqform.Bprotect;
            this._cardio = bloodbreqform.Cardio;
            this._decom = bloodbreqform.Decom;
            this._around = bloodbreqform.Around;
            this._icdcode = bloodbreqform.Icdcode;
            this._lostbcount = bloodbreqform.Lostbcount;
            this._lostspeed = bloodbreqform.Lostspeed;
            this._bodytpe = bloodbreqform.Bodytpe;
            this._bpress = bloodbreqform.Bpress;
            this._breathe = bloodbreqform.Breathe;
            this._pulse = bloodbreqform.Pulse;
            this._heartrate = bloodbreqform.Heartrate;
            this._urine = bloodbreqform.Urine;
            this._anesth = bloodbreqform.Anesth;
            this._labID = bloodbreqform.LabID;
            this._dispOrder = bloodbreqform.DispOrder;
            this._dataAddTime = bloodbreqform.DataAddTime;
            this._dataTimeStamp = bloodbreqform.DataTimeStamp;
            this._visible = bloodbreqform.Visible;
            this._hisDeptID = bloodbreqform.HisDeptID;
            this._hisDoctorId = bloodbreqform.HisDoctorId;
            this._checkCompleteFlag = bloodbreqform.CheckCompleteFlag;
            this._checkCompleteTime = bloodbreqform.CheckCompleteTime;
            this._applyID = bloodbreqform.ApplyID;
            this._applyName = bloodbreqform.ApplyName;
            this._applyTime = bloodbreqform.ApplyTime;
            this._applyMemo = bloodbreqform.ApplyMemo;
            this._seniorID = bloodbreqform.SeniorID;
            this._seniorName = bloodbreqform.SeniorName;
            this._seniorTime = bloodbreqform.SeniorTime;
            this._seniorMemo = bloodbreqform.SeniorMemo;
            this._directorID = bloodbreqform.DirectorID;
            this._directorName = bloodbreqform.DirectorName;
            this._directorTime = bloodbreqform.DirectorTime;
            this._directorMemo = bloodbreqform.DirectorMemo;
            this._medicalID = bloodbreqform.MedicalID;
            this._medicalName = bloodbreqform.MedicalName;
            this._medicalTime = bloodbreqform.MedicalTime;
            this._medicalMemo = bloodbreqform.MedicalMemo;

            this._obsoleteID = bloodbreqform.ObsoleteID;
            this._obsoleteName = bloodbreqform.ObsoleteName;
            this._obsoleteTime = bloodbreqform.ObsoleteTime;
            this._obsoleteMemo = bloodbreqform.ObsoleteMemo;
            this._obsoleteMemoId = bloodbreqform.ObsoleteMemoId;

            this._breqStatusID = bloodbreqform.BreqStatusID;
            this._breqStatusName = bloodbreqform.BreqStatusName;
            this._lisALB = bloodbreqform.LisALB;
            this._lisALT = bloodbreqform.LisALT;
            this._lisAPTT = bloodbreqform.LisAPTT;
            this._lisFbg = bloodbreqform.LisFbg;
            this._lisHBc = bloodbreqform.LisHBc;
            this._lisHBe = bloodbreqform.LisHBe;
            this._lisHBeAg = bloodbreqform.LisHBeAg;
            this._lisHCT = bloodbreqform.LisHCT;
            this._lisHCV = bloodbreqform.LisHCV;
            this._lisHGB = bloodbreqform.LisHGB;
            this._lisHIV = bloodbreqform.LisHIV;
            this._lisPLT = bloodbreqform.LisPLT;
            this._lisPT = bloodbreqform.LisPT;
            this._lisRBC = bloodbreqform.LisRBC;
            this._lisRPR = bloodbreqform.LisRPR;
            this._lisTT = bloodbreqform.LisTT;
            this._lisWBC = bloodbreqform.LisWBC;
            this._reqTotal = bloodbreqform.ReqTotal;
            this._toHisFlag = bloodbreqform.ToHisFlag;

            this._admID = bloodbreqform.AdmID;
            this._organTransplant = bloodbreqform.OrganTransplant;
            this._marrowTransplantation = bloodbreqform.MarrowTransplantation;
            this._wardNo = bloodbreqform.WardNo;
            this._hisWardNo = bloodbreqform.HisWardNo;
            this._bLPreEvaluation = bloodbreqform.BLPreEvaluation;
            this._printTotal = bloodbreqform.PrintTotal;
            this._hasAllergy = bloodbreqform.HasAllergy;

            this._patABO = bloodbreqform.PatABO;
            this._patRh = bloodbreqform.PatRh;
            this._bloodWay = bloodbreqform.BloodWay;
            this._reqTotal = bloodbreqform.ReqTotal;
            this._obsoleteID = bloodbreqform.ObsoleteID;
            this._obsoleteName = bloodbreqform.ObsoleteName;
            this._obsoleteTime = bloodbreqform.ObsoleteTime;
            this._obsoleteMemo = bloodbreqform.ObsoleteMemo;
            this._toHisFlag = bloodbreqform.ToHisFlag;
            this._admID = bloodbreqform.AdmID;
            this._obsoleteMemoId = bloodbreqform.ObsoleteMemoId;
            this._printTotal = bloodbreqform.PrintTotal;
            this._hasAllergy = bloodbreqform.HasAllergy;
        }
        //public BloodBReqForm(BloodBReqForm bloodbreqform)
        //{
        //    _setBloodBReqForm(bloodbreqform);
        //}
        public BloodBReqForm(BloodBReqForm bloodbreqform, BloodBReqType bloodbreqtype, BloodUseType bloodusetype, Department department, Doctor doctor)
        {
            _setBloodBReqForm(bloodbreqform);

            this._bReqTypeCName = bloodbreqtype.CName;
            this._useTypeCName = bloodusetype.CName;
            this._deptCName = department.CName;
            this._doctorCName = doctor.CName;
        }
        #endregion
    }
    #endregion
}