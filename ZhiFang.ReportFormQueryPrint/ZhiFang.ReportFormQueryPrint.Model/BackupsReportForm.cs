using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// 实体类BackupsReportForm 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class BackupsReportForm
    {
        public BackupsReportForm()
        { }
        #region Model
        private long _LabID;
        private long _ReportPublicationID;
        private DateTime? _receivedate;
        private int? _sectionno;
        private int? _testtypeno;
        private string _sampleno;
        private int? _statusno;
        private int? _sampletypeno; 
        private string _patno;
        private string _cname;
        private int? _genderno;
        private DateTime? _birthday;
        private double _age;
        private int? _ageunitno;
        private int? _folkno;
        private int? _districtno;
        private int? _wardno;
        private string _bed;
        private int? _deptno;
        private string _doctor;
        private int? _chargeno;
        private decimal? _charge;
        private string _collecter;
        private DateTime? _collectdate;
        private DateTime? _collecttime;
        private string _formmemo;
        private string _technician;
        private DateTime? _testdate;
        private DateTime? _testtime;
        private string _operator;
        private DateTime? _operdate;
        private DateTime? _opertime;
        private string _checker;
        private DateTime? _checkdate;
        private DateTime? _checktime;
        private string _serialno;
        private string _barcode;
        private string _requestsource;
        private int? _diagno;
        private int? _printtimes;
        private int? _sicktypeno;
        private string _formcomment;
        private string _zdy1;
        private string _zdy2;
        private string _zdy3;
        private string _zdy4;
        private string _zdy5;
        private DateTime? _inceptdate;
        private DateTime? _incepttime;
        private string _incepter;
        private DateTime? _onlinedate;
        private DateTime? _onlinetime;
        private int? _bmanno;
        private long? _clientno;
        private string _chargeflag;
        private int? _isreceive;
        private string _receiveman;
        private DateTime? _receivetime;
        private string _concessionnum;
        private string _sender2;
        private DateTime? _sendertime2;
        private int? _resultstatus;
        private string _testaim;
        private int? _resultprinttimes;
        private string _paritemname;
        private string _clientprint;
        private string _resultsend;
        private string _reportsend;
        private string _countnodesformsource;
        private int? _commsendflag;
        private string _zdy6;
        private DateTime? _printdatetime;
        private string _printoper;
        private string _formno;
        private int? _formstateno;
        private string _oldserialno;
        private int? _mresulttype;
        private string _diagnose;
        private string _testpurpose;
        private int? _isfree;
        private string _noperator;
        private DateTime? _noperdate;
        private DateTime? _nopertime;
        private string _pathologyno;
        private int? _abnormityflag;
        private DateTime? _hisdatetime;
        private int? _allowprint;
        private string _removefeesreason;
        private string _urgentstate;
        private string _zdy7;
        private string _zdy8;
        private string _zdy9;
        private string _zdy10;
        private string _phonecode;
        private int? _isnode;
        private int? _phonenodecount;
        private int? _autonodecount;
        private string _formdesc;
        private string _equipcommmemo;
        private string _esampleno;
        private string _eposition;
        private int? _isusepg;
        private string _opermemo;
        private string _fromqcl;
        private string _esend;
        private int? _isdel;
        private string _emodule;
        private int? _isredo;
        private string _samplesender;
        private DateTime? _samplesendtime;
        private int? _sendplaceno;
        private int? _sendflag;
        private string _sickorder;
        private int? _sicktype;
        private string _caseno;
        private string _PageName;
        private string _PageCount;
        private DateTime? _DataAddTime;
        private DateTime? _DataUpdateTime;
        private DateTime? _DataMigrationTime;
        private long _MainTesterId;
        private string _PatientID;
        private long _ExaminerId;
        private string _CollectPart;
        private int _ActiveFlag;
        private int? _bRevised;


        /// <summary>
        /// 重申字段
        /// </summary>
        public int? BRevised
        {
            get { return _bRevised; }
            set { _bRevised = value; }

        }

        /// <summary>
        /// 实验室ID
        /// </summary>
        public long LabID
        {
            set { _LabID = value; }
            get { return _LabID; }
        }
        /// <summary>
        /// 报告发布ID
        /// </summary>
        public long ReportPublicationID
        {
            set { _ReportPublicationID = value; }
            get { return _ReportPublicationID; }
        }
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        public int? StatusNo
        {
            set { _statusno = value; }
            get { return _statusno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SampleTypeNo
        {
            set { _sampletypeno = value; }
            get { return _sampletypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PatNo
        {
            set { _patno = value; }
            get { return _patno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CName
        {
            set { _cname = value; }
            get { return _cname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? GenderNo
        {
            set { _genderno = value; }
            get { return _genderno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AgeUnitNo
        {
            set { _ageunitno = value; }
            get { return _ageunitno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FolkNo
        {
            set { _folkno = value; }
            get { return _folkno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DistrictNo
        {
            set { _districtno = value; }
            get { return _districtno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WardNo
        {
            set { _wardno = value; }
            get { return _wardno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Bed
        {
            set { _bed = value; }
            get { return _bed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DeptNo
        {
            set { _deptno = value; }
            get { return _deptno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Doctor
        {
            set { _doctor = value; }
            get { return _doctor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ChargeNo
        {
            set { _chargeno = value; }
            get { return _chargeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Charge
        {
            set { _charge = value; }
            get { return _charge; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Collecter
        {
            set { _collecter = value; }
            get { return _collecter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CollectDate
        {
            set { _collectdate = value; }
            get { return _collectdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CollectTime
        {
            set { _collecttime = value; }
            get { return _collecttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormMemo
        {
            set { _formmemo = value; }
            get { return _formmemo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Technician
        {
            set { _technician = value; }
            get { return _technician; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TestDate
        {
            set { _testdate = value; }
            get { return _testdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TestTime
        {
            set { _testtime = value; }
            get { return _testtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Operator
        {
            set { _operator = value; }
            get { return _operator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OperDate
        {
            set { _operdate = value; }
            get { return _operdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OperTime
        {
            set { _opertime = value; }
            get { return _opertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckTime
        {
            set { _checktime = value; }
            get { return _checktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SerialNo
        {
            set { _serialno = value; }
            get { return _serialno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BarCode
        {
            set { _barcode = value; }
            get { return _barcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequestSource
        {
            set { _requestsource = value; }
            get { return _requestsource; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DiagNo
        {
            set { _diagno = value; }
            get { return _diagno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PrintTimes
        {
            set { _printtimes = value; }
            get { return _printtimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SickTypeNo
        {
            set { _sicktypeno = value; }
            get { return _sicktypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormComment
        {
            set { _formcomment = value; }
            get { return _formcomment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy1
        {
            set { _zdy1 = value; }
            get { return _zdy1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy2
        {
            set { _zdy2 = value; }
            get { return _zdy2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy3
        {
            set { _zdy3 = value; }
            get { return _zdy3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy4
        {
            set { _zdy4 = value; }
            get { return _zdy4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy5
        {
            set { _zdy5 = value; }
            get { return _zdy5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? inceptdate
        {
            set { _inceptdate = value; }
            get { return _inceptdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? incepttime
        {
            set { _incepttime = value; }
            get { return _incepttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string incepter
        {
            set { _incepter = value; }
            get { return _incepter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? onlinedate
        {
            set { _onlinedate = value; }
            get { return _onlinedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? onlinetime
        {
            set { _onlinetime = value; }
            get { return _onlinetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bmanno
        {
            set { _bmanno = value; }
            get { return _bmanno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? clientno
        {
            set { _clientno = value; }
            get { return _clientno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string chargeflag
        {
            set { _chargeflag = value; }
            get { return _chargeflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isReceive
        {
            set { _isreceive = value; }
            get { return _isreceive; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiveMan
        {
            set { _receiveman = value; }
            get { return _receiveman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReceiveTime
        {
            set { _receivetime = value; }
            get { return _receivetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string concessionNum
        {
            set { _concessionnum = value; }
            get { return _concessionnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sender2
        {
            set { _sender2 = value; }
            get { return _sender2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SenderTime2
        {
            set { _sendertime2 = value; }
            get { return _sendertime2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? resultstatus
        {
            set { _resultstatus = value; }
            get { return _resultstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string testaim
        {
            set { _testaim = value; }
            get { return _testaim; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? resultprinttimes
        {
            set { _resultprinttimes = value; }
            get { return _resultprinttimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string paritemname
        {
            set { _paritemname = value; }
            get { return _paritemname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string clientprint
        {
            set { _clientprint = value; }
            get { return _clientprint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string resultsend
        {
            set { _resultsend = value; }
            get { return _resultsend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string reportsend
        {
            set { _reportsend = value; }
            get { return _reportsend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CountNodesFormSource
        {
            set { _countnodesformsource = value; }
            get { return _countnodesformsource; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? commsendflag
        {
            set { _commsendflag = value; }
            get { return _commsendflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PrintDateTime
        {
            set { _printdatetime = value; }
            get { return _printdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrintOper
        {
            set { _printoper = value; }
            get { return _printoper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormNo
        {
            set { _formno = value; }
            get { return _formno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FormStateNo
        {
            set { _formstateno = value; }
            get { return _formstateno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldSerialNo
        {
            set { _oldserialno = value; }
            get { return _oldserialno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? mresulttype
        {
            set { _mresulttype = value; }
            get { return _mresulttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Diagnose
        {
            set { _diagnose = value; }
            get { return _diagnose; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TestPurpose
        {
            set { _testpurpose = value; }
            get { return _testpurpose; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsFree
        {
            set { _isfree = value; }
            get { return _isfree; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NOperator
        {
            set { _noperator = value; }
            get { return _noperator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? NOperDate
        {
            set { _noperdate = value; }
            get { return _noperdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? NOperTime
        {
            set { _nopertime = value; }
            get { return _nopertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PathologyNo
        {
            set { _pathologyno = value; }
            get { return _pathologyno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? abnormityflag
        {
            set { _abnormityflag = value; }
            get { return _abnormityflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? HISDateTime
        {
            set { _hisdatetime = value; }
            get { return _hisdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? allowprint
        {
            set { _allowprint = value; }
            get { return _allowprint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RemoveFeesReason
        {
            set { _removefeesreason = value; }
            get { return _removefeesreason; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UrgentState
        {
            set { _urgentstate = value; }
            get { return _urgentstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZDY6
        {
            set { _zdy6 = value; }
            get { return _zdy6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZDY7
        {
            set { _zdy7 = value; }
            get { return _zdy7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZDY8
        {
            set { _zdy8 = value; }
            get { return _zdy8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZDY9
        {
            set { _zdy9 = value; }
            get { return _zdy9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZDY10
        {
            set { _zdy10 = value; }
            get { return _zdy10; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string phoneCode
        {
            set { _phonecode = value; }
            get { return _phonecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsNode
        {
            set { _isnode = value; }
            get { return _isnode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PhoneNodeCount
        {
            set { _phonenodecount = value; }
            get { return _phonenodecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AutoNodeCount
        {
            set { _autonodecount = value; }
            get { return _autonodecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormDesc
        {
            set { _formdesc = value; }
            get { return _formdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquipCommMemo
        {
            set { _equipcommmemo = value; }
            get { return _equipcommmemo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ESampleNo
        {
            set { _esampleno = value; }
            get { return _esampleno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EPosition
        {
            set { _eposition = value; }
            get { return _eposition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ISUsePG
        {
            set { _isusepg = value; }
            get { return _isusepg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperMemo
        {
            set { _opermemo = value; }
            get { return _opermemo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromQCL
        {
            set { _fromqcl = value; }
            get { return _fromqcl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ESend
        {
            set { _esend = value; }
            get { return _esend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EModule
        {
            set { _emodule = value; }
            get { return _emodule; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsRedo
        {
            set { _isredo = value; }
            get { return _isredo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleSender
        {
            set { _samplesender = value; }
            get { return _samplesender; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SampleSendTime
        {
            set { _samplesendtime = value; }
            get { return _samplesendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SendPlaceNo
        {
            set { _sendplaceno = value; }
            get { return _sendplaceno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SendFlag
        {
            set { _sendflag = value; }
            get { return _sendflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sickorder
        {
            set { _sickorder = value; }
            get { return _sickorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SickType
        {
            set { _sicktype = value; }
            get { return _sicktype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string caseno
        {
            set { _caseno = value; }
            get { return _caseno; }
        }

        public string PageName
        {
            set { _PageName = value; }
            get { return _PageName; }
        }
        public string PageCount
        {
            set { _PageCount = value; }
            get { return _PageCount; }
        }
        public DateTime? DataAddTime
        {
            set { _DataAddTime = value; }
            get { return _DataAddTime; }
        }
        public DateTime? DataUpdateTime
        {
            set { _DataUpdateTime = value; }
            get { return _DataUpdateTime; }
        }
        public DateTime? DataMigrationTime
        {
            set { _DataMigrationTime = value; }
            get { return _DataMigrationTime; }
        }
        public long MainTesterId
        {
            set { _MainTesterId = value; }
            get { return _MainTesterId; }
        }
        public string PatientID
        {
            set { _PatientID = value; }
            get { return _PatientID; }
        }
        public long ExaminerId
        {
            set { _ExaminerId = value; }
            get { return _ExaminerId; }
        }
        public string CollectPart
        {
            set { _CollectPart = value; }
            get { return _CollectPart; }
        }
        public int ActiveFlag
        {
            set { _ActiveFlag = value; }
            get { return _ActiveFlag; }
        }

        #endregion Model

    }
}
