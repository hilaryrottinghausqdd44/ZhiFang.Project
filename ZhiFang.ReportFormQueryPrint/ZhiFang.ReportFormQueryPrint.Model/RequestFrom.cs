using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region RequestForm

    /// <summary>
    /// RequestForm object for NHibernate mapped table 'RequestForm'.
    /// </summary>
    [DataContract]  
    public class RequestForm
    {
        #region Member Variables

        private DateTime? _receivedate;
        private int? _sectionno;
        private int? _testtypeno;
        private string _sampleno;
        protected int? _statusNo;
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
        protected string _doctor;
        protected int _chargeNo;
        protected decimal _charge;
        protected string _collecter;
        protected DateTime? _collectDate;
        protected DateTime? _collectTime;
        protected string _formMemo;
        protected string _technician;
        protected DateTime? _testDate;
        protected DateTime? _testTime;
        protected string _operator;
        protected DateTime? _operDate;
        protected DateTime? _operTime;
        protected string _checker;
        protected DateTime? _checkDate;
        protected DateTime? _checkTime;
        protected string _serialNo;
        protected string _requestSource;
        protected int _diagNo;
        protected int _printTimes;
        protected int _sickTypeNo;
        protected string _formComment;
        protected string _artificerorder;
        protected string _sickorder;
        protected int _sickType;
        protected string _chargeflag;
        protected string _testDest;
        protected string _sLable;
        protected string _zdy1;
        protected string _zdy2;
        protected string _zdy3;
        protected string _zdy4;
        protected string _zdy5;
        protected DateTime? _inceptdate;
        protected DateTime? _incepttime;
        protected string _incepter;
        protected DateTime? _onlinedate;
        protected DateTime? _onlinetime;
        protected int _bmanno;
        protected int _clientno;
        protected int _isReceive;
        protected string _receiveMan;
        protected DateTime? _receiveTime;
        protected string _concessionNum;
        protected int _resultstatus;
        protected string _testaim;
        protected int _resultprinttimes;
        protected string _sender2;
        protected DateTime? _senderTime2;
        protected string _paritemname;
        protected string _clientprint;
        protected string _resultsend;
        protected string _reportsend;
        protected string _countNodesFormSource;
        protected int _commsendflag;
        protected DateTime? _printDateTime;
        protected string _printOper;
        protected int _abnormityflag;
        protected DateTime? _hISDateTime;
        protected int _allowprint;
        protected string _removeFeesReason;
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
        protected string _formDesc;
        protected string _equipCommMemo;
        protected string _eSampleNo;
        protected string _ePosition;
        protected int _iSUsePG;
        protected string _operMemo;
        protected string _fromQCL;
        protected string _eSend;
        protected int _isDel;
        protected string _eModule;
        protected int _isRedo;
        protected string _eAchivPosition;
        protected DateTime? _fenzhuDatetime;
        protected string _eQCLPosition;
        protected int _equipResult;
        protected DateTime? _eResultToHIS;
        protected string _sampleSender;
        protected DateTime? _sampleSendTime;
        protected int _sendPlaceNo;
        protected int _sendFlag;
        protected string _nurseSender;
        protected DateTime? _nurseSendTime;
        protected string _nurseSendCarrier;
        protected string _bGetAllResults;
        protected int _bSendWjz;
        protected string _collecterID;
        protected DateTime? _printDateTime1;
        protected string _printOper1;
        protected string _hisDoctorId;
        protected string _hisDoctorPhoneCode;
        protected int _lisDoctorNo;
        protected int _lischeckerNo;
        protected string _lastOperator;
        protected DateTime? _lastOperDatetime;
        protected int _iReportstatus;
        protected int _autoSended;
        protected string _patState;
        protected int _equipCommSend;
        protected string _equipSampleNo;
        protected byte[] _wordInfo;
        protected string _technician2;
        protected string _formComment2;
        protected string _mergeno;
        protected int _bWarnedAfterTimeout;
        protected int _hospitalizedTimes;
        protected string _backUpDesc;
        protected string _iDNumberpatient;
        protected int _itemreceivedover;
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
        protected int _iAutoUnion;
        protected string _autoUnionSNo;
        protected string _clinicalFeedback;
        protected int _reportExtracted;
        protected string _pageName;
        protected string _pageCount;
        private string _formno;

        #endregion

        #region Public Properties

        public DateTime? ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SectionNo
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TestTypeNo
        {
            set { _testtypeno = value; }
            get { return _testtypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleNo
        {
            set { _sampleno = value; }
            get { return _sampleno; }
        }

        [DataMember]
        public virtual int? StatusNo
        {
            get { return _statusNo; }
            set { _statusNo = value; }
        }

        [DataMember]
        public virtual int SampleTypeNo
        {
            get { return _sampleTypeNo; }
            set { _sampleTypeNo = value; }
        }

        [DataMember]
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
        public virtual int GenderNo
        {
            get { return _genderNo; }
            set { _genderNo = value; }
        }

        [DataMember]
        public virtual DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        [DataMember]
        public virtual double Age
        {
            get { return _age; }
            set { _age = value; }
        }

        [DataMember]
        public virtual int AgeUnitNo
        {
            get { return _ageUnitNo; }
            set { _ageUnitNo = value; }
        }

        [DataMember]
        public virtual int FolkNo
        {
            get { return _folkNo; }
            set { _folkNo = value; }
        }

        [DataMember]
        public virtual int DistrictNo
        {
            get { return _districtNo; }
            set { _districtNo = value; }
        }

        [DataMember]
        public virtual int WardNo
        {
            get { return _wardNo; }
            set { _wardNo = value; }
        }

        [DataMember]
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
        public virtual int DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        [DataMember]
        public virtual string Doctor
        {
            get { return _doctor; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Doctor", value, value.ToString());
                _doctor = value;
            }
        }

        [DataMember]
        public virtual int ChargeNo
        {
            get { return _chargeNo; }
            set { _chargeNo = value; }
        }

        [DataMember]
        public virtual decimal Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

        [DataMember]
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
        public virtual DateTime? CollectDate
        {
            get { return _collectDate; }
            set { _collectDate = value; }
        }

        [DataMember]
        public virtual DateTime? CollectTime
        {
            get { return _collectTime; }
            set { _collectTime = value; }
        }

        [DataMember]
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
        public virtual string Technician
        {
            get { return _technician; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Technician", value, value.ToString());
                _technician = value;
            }
        }

        [DataMember]
        public virtual DateTime? TestDate
        {
            get { return _testDate; }
            set { _testDate = value; }
        }

        [DataMember]
        public virtual DateTime? TestTime
        {
            get { return _testTime; }
            set { _testTime = value; }
        }

        [DataMember]
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
        public virtual DateTime? OperDate
        {
            get { return _operDate; }
            set { _operDate = value; }
        }

        [DataMember]
        public virtual DateTime? OperTime
        {
            get { return _operTime; }
            set { _operTime = value; }
        }

        [DataMember]
        public virtual string Checker
        {
            get { return _checker; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
                _checker = value;
            }
        }

        [DataMember]
        public virtual DateTime? CheckDate
        {
            get { return _checkDate; }
            set { _checkDate = value; }
        }

        [DataMember]
        public virtual DateTime? CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        [DataMember]
        public virtual string SerialNo
        {
            get { return _serialNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SerialNo", value, value.ToString());
                _serialNo = value;
            }
        }

        [DataMember]
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
        public virtual int DiagNo
        {
            get { return _diagNo; }
            set { _diagNo = value; }
        }

        [DataMember]
        public virtual int PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        public virtual int SickTypeNo
        {
            get { return _sickTypeNo; }
            set { _sickTypeNo = value; }
        }

        [DataMember]
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
        public virtual int SickType
        {
            get { return _sickType; }
            set { _sickType = value; }
        }

        [DataMember]
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
        public virtual string TestDest
        {
            get { return _testDest; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for TestDest", value, value.ToString());
                _testDest = value;
            }
        }

        [DataMember]
        public virtual string SLable
        {
            get { return _sLable; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SLable", value, value.ToString());
                _sLable = value;
            }
        }

        [DataMember]
        public virtual string zdy1
        {
            get { return _zdy1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy1", value, value.ToString());
                _zdy1 = value;
            }
        }

        [DataMember]
        public virtual string zdy2
        {
            get { return _zdy2; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy2", value, value.ToString());
                _zdy2 = value;
            }
        }

        [DataMember]
        public virtual string zdy3
        {
            get { return _zdy3; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy3", value, value.ToString());
                _zdy3 = value;
            }
        }

        [DataMember]
        public virtual string zdy4
        {
            get { return _zdy4; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy4", value, value.ToString());
                _zdy4 = value;
            }
        }

        [DataMember]
        public virtual string zdy5
        {
            get { return _zdy5; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy5", value, value.ToString());
                _zdy5 = value;
            }
        }

        [DataMember]
        public virtual DateTime? Inceptdate
        {
            get { return _inceptdate; }
            set { _inceptdate = value; }
        }

        [DataMember]
        public virtual DateTime? Incepttime
        {
            get { return _incepttime; }
            set { _incepttime = value; }
        }

        [DataMember]
        public virtual string Incepter
        {
            get { return _incepter; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Incepter", value, value.ToString());
                _incepter = value;
            }
        }

        [DataMember]
        public virtual DateTime? Onlinedate
        {
            get { return _onlinedate; }
            set { _onlinedate = value; }
        }

        [DataMember]
        public virtual DateTime? Onlinetime
        {
            get { return _onlinetime; }
            set { _onlinetime = value; }
        }

        [DataMember]
        public virtual int Bmanno
        {
            get { return _bmanno; }
            set { _bmanno = value; }
        }

        [DataMember]
        public virtual int Clientno
        {
            get { return _clientno; }
            set { _clientno = value; }
        }

        [DataMember]
        public virtual int IsReceive
        {
            get { return _isReceive; }
            set { _isReceive = value; }
        }

        [DataMember]
        public virtual string ReceiveMan
        {
            get { return _receiveMan; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiveMan", value, value.ToString());
                _receiveMan = value;
            }
        }

        [DataMember]
        public virtual DateTime? ReceiveTime
        {
            get { return _receiveTime; }
            set { _receiveTime = value; }
        }

        [DataMember]
        public virtual string ConcessionNum
        {
            get { return _concessionNum; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ConcessionNum", value, value.ToString());
                _concessionNum = value;
            }
        }

        [DataMember]
        public virtual int Resultstatus
        {
            get { return _resultstatus; }
            set { _resultstatus = value; }
        }

        [DataMember]
        public virtual string Testaim
        {
            get { return _testaim; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Testaim", value, value.ToString());
                _testaim = value;
            }
        }

        [DataMember]
        public virtual int Resultprinttimes
        {
            get { return _resultprinttimes; }
            set { _resultprinttimes = value; }
        }

        [DataMember]
        public virtual string Sender2
        {
            get { return _sender2; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Sender2", value, value.ToString());
                _sender2 = value;
            }
        }

        [DataMember]
        public virtual DateTime? SenderTime2
        {
            get { return _senderTime2; }
            set { _senderTime2 = value; }
        }

        [DataMember]
        public virtual string Paritemname
        {
            get { return _paritemname; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Paritemname", value, value.ToString());
                _paritemname = value;
            }
        }

        [DataMember]
        public virtual string Clientprint
        {
            get { return _clientprint; }
            set
            {
                if (value != null && value.Length > 2)
                    throw new ArgumentOutOfRangeException("Invalid value for Clientprint", value, value.ToString());
                _clientprint = value;
            }
        }

        [DataMember]
        public virtual string Resultsend
        {
            get { return _resultsend; }
            set
            {
                if (value != null && value.Length > 2)
                    throw new ArgumentOutOfRangeException("Invalid value for Resultsend", value, value.ToString());
                _resultsend = value;
            }
        }

        [DataMember]
        public virtual string Reportsend
        {
            get { return _reportsend; }
            set
            {
                if (value != null && value.Length > 2)
                    throw new ArgumentOutOfRangeException("Invalid value for Reportsend", value, value.ToString());
                _reportsend = value;
            }
        }

        [DataMember]
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
        public virtual int Commsendflag
        {
            get { return _commsendflag; }
            set { _commsendflag = value; }
        }

        [DataMember]
        public virtual DateTime? PrintDateTime
        {
            get { return _printDateTime; }
            set { _printDateTime = value; }
        }

        [DataMember]
        public virtual string PrintOper
        {
            get { return _printOper; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PrintOper", value, value.ToString());
                _printOper = value;
            }
        }

        [DataMember]
        public virtual int Abnormityflag
        {
            get { return _abnormityflag; }
            set { _abnormityflag = value; }
        }

        [DataMember]
        public virtual DateTime? HISDateTime
        {
            get { return _hISDateTime; }
            set { _hISDateTime = value; }
        }

        [DataMember]
        public virtual int Allowprint
        {
            get { return _allowprint; }
            set { _allowprint = value; }
        }

        [DataMember]
        public virtual string RemoveFeesReason
        {
            get { return _removeFeesReason; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RemoveFeesReason", value, value.ToString());
                _removeFeesReason = value;
            }
        }

        [DataMember]
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
        public virtual string ZDY6
        {
            get { return _zDY6; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY6", value, value.ToString());
                _zDY6 = value;
            }
        }

        [DataMember]
        public virtual string ZDY7
        {
            get { return _zDY7; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY7", value, value.ToString());
                _zDY7 = value;
            }
        }

        [DataMember]
        public virtual string ZDY8
        {
            get { return _zDY8; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY8", value, value.ToString());
                _zDY8 = value;
            }
        }

        [DataMember]
        public virtual string ZDY9
        {
            get { return _zDY9; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY9", value, value.ToString());
                _zDY9 = value;
            }
        }

        [DataMember]
        public virtual string ZDY10
        {
            get { return _zDY10; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY10", value, value.ToString());
                _zDY10 = value;
            }
        }

        [DataMember]
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
        public virtual int IsNode
        {
            get { return _isNode; }
            set { _isNode = value; }
        }

        [DataMember]
        public virtual int PhoneNodeCount
        {
            get { return _phoneNodeCount; }
            set { _phoneNodeCount = value; }
        }

        [DataMember]
        public virtual int AutoNodeCount
        {
            get { return _autoNodeCount; }
            set { _autoNodeCount = value; }
        }

        [DataMember]
        public virtual string FormDesc
        {
            get { return _formDesc; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for FormDesc", value, value.ToString());
                _formDesc = value;
            }
        }

        [DataMember]
        public virtual string EquipCommMemo
        {
            get { return _equipCommMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EquipCommMemo", value, value.ToString());
                _equipCommMemo = value;
            }
        }

        [DataMember]
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
        public virtual string EPosition
        {
            get { return _ePosition; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for EPosition", value, value.ToString());
                _ePosition = value;
            }
        }

        [DataMember]
        public virtual int ISUsePG
        {
            get { return _iSUsePG; }
            set { _iSUsePG = value; }
        }

        [DataMember]
        public virtual string OperMemo
        {
            get { return _operMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OperMemo", value, value.ToString());
                _operMemo = value;
            }
        }

        [DataMember]
        public virtual string FromQCL
        {
            get { return _fromQCL; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for FromQCL", value, value.ToString());
                _fromQCL = value;
            }
        }

        [DataMember]
        public virtual string ESend
        {
            get { return _eSend; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ESend", value, value.ToString());
                _eSend = value;
            }
        }

        [DataMember]
        public virtual int IsDel
        {
            get { return _isDel; }
            set { _isDel = value; }
        }

        [DataMember]
        public virtual string EModule
        {
            get { return _eModule; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for EModule", value, value.ToString());
                _eModule = value;
            }
        }

        [DataMember]
        public virtual int IsRedo
        {
            get { return _isRedo; }
            set { _isRedo = value; }
        }

        [DataMember]
        public virtual string EAchivPosition
        {
            get { return _eAchivPosition; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EAchivPosition", value, value.ToString());
                _eAchivPosition = value;
            }
        }

        [DataMember]
        public virtual DateTime? FenzhuDatetime
        {
            get { return _fenzhuDatetime; }
            set { _fenzhuDatetime = value; }
        }

        [DataMember]
        public virtual string EQCLPosition
        {
            get { return _eQCLPosition; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for EQCLPosition", value, value.ToString());
                _eQCLPosition = value;
            }
        }

        [DataMember]
        public virtual int EquipResult
        {
            get { return _equipResult; }
            set { _equipResult = value; }
        }

        [DataMember]
        public virtual DateTime? EResultToHIS
        {
            get { return _eResultToHIS; }
            set { _eResultToHIS = value; }
        }

        [DataMember]
        public virtual string SampleSender
        {
            get { return _sampleSender; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SampleSender", value, value.ToString());
                _sampleSender = value;
            }
        }

        [DataMember]
        public virtual DateTime? SampleSendTime
        {
            get { return _sampleSendTime; }
            set { _sampleSendTime = value; }
        }

        [DataMember]
        public virtual int SendPlaceNo
        {
            get { return _sendPlaceNo; }
            set { _sendPlaceNo = value; }
        }

        [DataMember]
        public virtual int SendFlag
        {
            get { return _sendFlag; }
            set { _sendFlag = value; }
        }

        [DataMember]
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
        public virtual DateTime? NurseSendTime
        {
            get { return _nurseSendTime; }
            set { _nurseSendTime = value; }
        }

        [DataMember]
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
        public virtual string BGetAllResults
        {
            get { return _bGetAllResults; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BGetAllResults", value, value.ToString());
                _bGetAllResults = value;
            }
        }

        [DataMember]
        public virtual int BSendWjz
        {
            get { return _bSendWjz; }
            set { _bSendWjz = value; }
        }

        [DataMember]
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
        public virtual DateTime? PrintDateTime1
        {
            get { return _printDateTime1; }
            set { _printDateTime1 = value; }
        }

        [DataMember]
        public virtual string PrintOper1
        {
            get { return _printOper1; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PrintOper1", value, value.ToString());
                _printOper1 = value;
            }
        }

        [DataMember]
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
        public virtual int LisDoctorNo
        {
            get { return _lisDoctorNo; }
            set { _lisDoctorNo = value; }
        }

        [DataMember]
        public virtual int LischeckerNo
        {
            get { return _lischeckerNo; }
            set { _lischeckerNo = value; }
        }

        [DataMember]
        public virtual string LastOperator
        {
            get { return _lastOperator; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for LastOperator", value, value.ToString());
                _lastOperator = value;
            }
        }

        [DataMember]
        public virtual DateTime? LastOperDatetime
        {
            get { return _lastOperDatetime; }
            set { _lastOperDatetime = value; }
        }

        [DataMember]
        public virtual int IReportstatus
        {
            get { return _iReportstatus; }
            set { _iReportstatus = value; }
        }

        [DataMember]
        public virtual int AutoSended
        {
            get { return _autoSended; }
            set { _autoSended = value; }
        }

        [DataMember]
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
        public virtual int EquipCommSend
        {
            get { return _equipCommSend; }
            set { _equipCommSend = value; }
        }

        [DataMember]
        public virtual string EquipSampleNo
        {
            get { return _equipSampleNo; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for EquipSampleNo", value, value.ToString());
                _equipSampleNo = value;
            }
        }

        [DataMember]
        public virtual byte[] WordInfo
        {
            get { return _wordInfo; }
            set { _wordInfo = value; }
        }

        [DataMember]
        public virtual string Technician2
        {
            get { return _technician2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Technician2", value, value.ToString());
                _technician2 = value;
            }
        }

        [DataMember]
        public virtual string FormComment2
        {
            get { return _formComment2; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for FormComment2", value, value.ToString());
                _formComment2 = value;
            }
        }

        [DataMember]
        public virtual string Mergeno
        {
            get { return _mergeno; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for Mergeno", value, value.ToString());
                _mergeno = value;
            }
        }

        [DataMember]
        public virtual int BWarnedAfterTimeout
        {
            get { return _bWarnedAfterTimeout; }
            set { _bWarnedAfterTimeout = value; }
        }

        [DataMember]
        public virtual int HospitalizedTimes
        {
            get { return _hospitalizedTimes; }
            set { _hospitalizedTimes = value; }
        }

        [DataMember]
        public virtual string BackUpDesc
        {
            get { return _backUpDesc; }
            set
            {
                if (value != null && value.Length > 1000)
                    throw new ArgumentOutOfRangeException("Invalid value for BackUpDesc", value, value.ToString());
                _backUpDesc = value;
            }
        }

        [DataMember]
        public virtual string IDNumberpatient
        {
            get { return _iDNumberpatient; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for IDNumberpatient", value, value.ToString());
                _iDNumberpatient = value;
            }
        }

        [DataMember]
        public virtual int Itemreceivedover
        {
            get { return _itemreceivedover; }
            set { _itemreceivedover = value; }
        }

        [DataMember]
        public virtual string zdy11
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
        public virtual string zdy12
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
        public virtual string zdy13
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
        public virtual string zdy14
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
        public virtual string zdy15
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
        public virtual string zdy16
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
        public virtual string zdy17
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
        public virtual string zdy18
        {
            get { return _zdy18; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy18", value, value.ToString());
                _zdy18 = value;
            }
        }

        [DataMember]
        public virtual string zdy19
        {
            get { return _zdy19; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy19", value, value.ToString());
                _zdy19 = value;
            }
        }

        [DataMember]
        public virtual string zdy20
        {
            get { return _zdy20; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy20", value, value.ToString());
                _zdy20 = value;
            }
        }

        [DataMember]
        public virtual int IAutoUnion
        {
            get { return _iAutoUnion; }
            set { _iAutoUnion = value; }
        }

        [DataMember]
        public virtual string AutoUnionSNo
        {
            get { return _autoUnionSNo; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for AutoUnionSNo", value, value.ToString());
                _autoUnionSNo = value;
            }
        }

        [DataMember]
        public virtual string ClinicalFeedback
        {
            get { return _clinicalFeedback; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ClinicalFeedback", value, value.ToString());
                _clinicalFeedback = value;
            }
        }

        [DataMember]
        public virtual int ReportExtracted
        {
            get { return _reportExtracted; }
            set { _reportExtracted = value; }
        }

        [DataMember]
        public virtual string PageName
        {
            get { return _pageName; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for PageName", value, value.ToString());
                _pageName = value;
            }
        }

        [DataMember]
        public virtual string PageCount
        {
            get { return _pageCount; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for PageCount", value, value.ToString());
                _pageCount = value;
            }
        }
        public string FormNo
        {
            set { _formno = value; }
            get { return _formno; }
        }

        #endregion
    }
    #endregion
}