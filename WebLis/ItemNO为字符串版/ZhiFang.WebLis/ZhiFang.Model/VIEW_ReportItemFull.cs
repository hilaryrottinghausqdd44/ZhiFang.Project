using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model
{
    [DataContract]
    public class VIEW_ReportItemFull
    {
        /// <summary>
        /// ItemRanNum
        /// </summary>		
        private long? _itemrannum;
        [DataMember]
        public long? ItemRanNum
        {
            get { return _itemrannum; }
            set { _itemrannum = value; }
        }
        /// <summary>
        /// ReportFormID
        /// </summary>		
        private string _reportformid;
        [DataMember]
        public string ReportFormID
        {
            get { return _reportformid; }
            set { _reportformid = value; }
        }
        /// <summary>
        /// ReportItemID
        /// </summary>		
        private int? _reportitemid;
        [DataMember]
        public int? ReportItemID
        {
            get { return _reportitemid; }
            set { _reportitemid = value; }
        }
        /// <summary>
        /// TESTITEMNAME
        /// </summary>		
        private string _testitemname;
        [DataMember]
        public string TESTITEMNAME
        {
            get { return _testitemname; }
            set { _testitemname = value; }
        }
        /// <summary>
        /// TESTITEMSNAME
        /// </summary>		
        private string _testitemsname;
        [DataMember]
        public string TESTITEMSNAME
        {
            get { return _testitemsname; }
            set { _testitemsname = value; }
        }
        /// <summary>
        /// TESTITEMENAME
        /// </summary>		
        private string _testitemename;
        [DataMember]
        public string TESTITEMENAME
        {
            get { return _testitemename; }
            set { _testitemename = value; }
        }
        /// <summary>
        /// RECEIVEDATE
        /// </summary>		
        private DateTime? _receivedate;
        [DataMember]
        public DateTime? RECEIVEDATE
        {
            get { return _receivedate; }
            set { _receivedate = value; }
        }
        /// <summary>
        /// SECTIONNO
        /// </summary>		
        private string _sectionno;
        [DataMember]
        public string SECTIONNO
        {
            get { return _sectionno; }
            set { _sectionno = value; }
        }
        /// <summary>
        /// TESTTYPENO
        /// </summary>		
        private string _testtypeno;
        [DataMember]
        public string TESTTYPENO
        {
            get { return _testtypeno; }
            set { _testtypeno = value; }
        }
        /// <summary>
        /// SAMPLENO
        /// </summary>		
        private string _sampleno;
        [DataMember]
        public string SAMPLENO
        {
            get { return _sampleno; }
            set { _sampleno = value; }
        }
        /// <summary>
        /// PARITEMNO
        /// </summary>		
        private string _paritemno;
        [DataMember]
        public string PARITEMNO
        {
            get { return _paritemno; }
            set { _paritemno = value; }
        }
        /// <summary>
        /// ITEMNO
        /// </summary>		
        private string _itemno;
        [DataMember]
        public string ITEMNO
        {
            get { return _itemno; }
            set { _itemno = value; }
        }
        /// <summary>
        /// ORIGINALVALUE
        /// </summary>		
        private string _originalvalue;
        [DataMember]
        public string ORIGINALVALUE
        {
            get { return _originalvalue; }
            set { _originalvalue = value; }
        }
        /// <summary>
        /// REPORTVALUE
        /// </summary>		
        private string _reportvalue;
        [DataMember]
        public string REPORTVALUE
        {
            get { return _reportvalue; }
            set { _reportvalue = value; }
        }
        /// <summary>
        /// ORIGINALDESC
        /// </summary>		
        private string _originaldesc;
        [DataMember]
        public string ORIGINALDESC
        {
            get { return _originaldesc; }
            set { _originaldesc = value; }
        }
        /// <summary>
        /// REPORTDESC
        /// </summary>		
        private string _reportdesc;
        [DataMember]
        public string REPORTDESC
        {
            get { return _reportdesc; }
            set { _reportdesc = value; }
        }
        /// <summary>
        /// STATUSNO
        /// </summary>		
        private string _statusno;
        [DataMember]
        public string STATUSNO
        {
            get { return _statusno; }
            set { _statusno = value; }
        }
        /// <summary>
        /// EQUIPNO
        /// </summary>		
        private string _equipno;
        [DataMember]
        public string EQUIPNO
        {
            get { return _equipno; }
            set { _equipno = value; }
        }
        /// <summary>
        /// MODIFIED
        /// </summary>		
        private string _modified;
        [DataMember]
        public string MODIFIED
        {
            get { return _modified; }
            set { _modified = value; }
        }
        /// <summary>
        /// REFRANGE
        /// </summary>		
        private string _refrange;
        [DataMember]
        public string REFRANGE
        {
            get { return _refrange; }
            set { _refrange = value; }
        }
        /// <summary>
        /// ITEMDATE
        /// </summary>		
        private DateTime? _itemdate;
        [DataMember]
        public DateTime? ITEMDATE
        {
            get { return _itemdate; }
            set { _itemdate = value; }
        }
        /// <summary>
        /// ITEMTIME
        /// </summary>		
        private DateTime? _itemtime;
        [DataMember]
        public DateTime? ITEMTIME
        {
            get { return _itemtime; }
            set { _itemtime = value; }
        }
        /// <summary>
        /// ISMATCH
        /// </summary>		
        private string _ismatch;
        [DataMember]
        public string ISMATCH
        {
            get { return _ismatch; }
            set { _ismatch = value; }
        }
        /// <summary>
        /// RESULTSTATUS
        /// </summary>		
        private string _resultstatus;
        [DataMember]
        public string RESULTSTATUS
        {
            get { return _resultstatus; }
            set { _resultstatus = value; }
        }
        /// <summary>
        /// TESTITEMDATETIME
        /// </summary>		
        private DateTime? _testitemdatetime;
        [DataMember]
        public DateTime? TESTITEMDATETIME
        {
            get { return _testitemdatetime; }
            set { _testitemdatetime = value; }
        }
        /// <summary>
        /// REPORTVALUEALL
        /// </summary>		
        private string _reportvalueall;
        [DataMember]
        public string REPORTVALUEALL
        {
            get { return _reportvalueall; }
            set { _reportvalueall = value; }
        }
        /// <summary>
        /// PARITEMNAME
        /// </summary>		
        private string _paritemname;
        [DataMember]
        public string PARITEMNAME
        {
            get { return _paritemname; }
            set { _paritemname = value; }
        }
        /// <summary>
        /// PARITEMSNAME
        /// </summary>		
        private string _paritemsname;
        [DataMember]
        public string PARITEMSNAME
        {
            get { return _paritemsname; }
            set { _paritemsname = value; }
        }
        /// <summary>
        /// DISPORDER
        /// </summary>		
        private string _disporder;
        [DataMember]
        public string DISPORDER
        {
            get { return _disporder; }
            set { _disporder = value; }
        }
        /// <summary>
        /// ITEMORDER
        /// </summary>		
        private string _itemorder;
        [DataMember]
        public string ITEMORDER
        {
            get { return _itemorder; }
            set { _itemorder = value; }
        }
        /// <summary>
        /// UNIT
        /// </summary>		
        private string _unit;
        [DataMember]
        public string UNIT
        {
            get { return _unit; }
            set { _unit = value; }
        }
        /// <summary>
        /// SERIALNO
        /// </summary>		
        private string _serialno;
        [DataMember]
        public string SERIALNO
        {
            get { return _serialno; }
            set { _serialno = value; }
        }
        /// <summary>
        /// ZDY1
        /// </summary>		
        private string _zdy1;
        [DataMember]
        public string ZDY1
        {
            get { return _zdy1; }
            set { _zdy1 = value; }
        }
        /// <summary>
        /// ZDY2
        /// </summary>		
        private string _zdy2;
        [DataMember]
        public string ZDY2
        {
            get { return _zdy2; }
            set { _zdy2 = value; }
        }
        /// <summary>
        /// ZDY3
        /// </summary>		
        private string _zdy3;
        [DataMember]
        public string ZDY3
        {
            get { return _zdy3; }
            set { _zdy3 = value; }
        }
        /// <summary>
        /// ZDY4
        /// </summary>		
        private string _zdy4;
        [DataMember]
        public string ZDY4
        {
            get { return _zdy4; }
            set { _zdy4 = value; }
        }
        /// <summary>
        /// ZDY5
        /// </summary>		
        private string _zdy5;
        [DataMember]
        public string ZDY5
        {
            get { return _zdy5; }
            set { _zdy5 = value; }
        }
        /// <summary>
        /// HISORDERNO
        /// </summary>		
        private string _hisorderno;
        [DataMember]
        public string HISORDERNO
        {
            get { return _hisorderno; }
            set { _hisorderno = value; }
        }
        /// <summary>
        /// FORMNO
        /// </summary>		
        private int? _formno;
        [DataMember]
        public int? FORMNO
        {
            get { return _formno; }
            set { _formno = value; }
        }
        /// <summary>
        /// TECHNICIAN
        /// </summary>		
        private string _technician;
        [DataMember]
        public string TECHNICIAN
        {
            get { return _technician; }
            set { _technician = value; }
        }
        /// <summary>
        /// OLDSERIALNO
        /// </summary>		
        private string _oldserialno;
        [DataMember]
        public string OLDSERIALNO
        {
            get { return _oldserialno; }
            set { _oldserialno = value; }
        }
        /// <summary>
        /// PATNO
        /// </summary>		
        private string _patno;
        [DataMember]
        public string PATNO
        {
            get { return _patno; }
            set { _patno = value; }
        }
        /// <summary>
        /// CNAME
        /// </summary>		
        private string _cname;
        [DataMember]
        public string CNAME
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        /// GENDERNAME
        /// </summary>		
        private string _gendername;
        [DataMember]
        public string GENDERNAME
        {
            get { return _gendername; }
            set { _gendername = value; }
        }
        /// <summary>
        /// DEPTNAME
        /// </summary>		
        private string _deptname;
        [DataMember]
        public string DEPTNAME
        {
            get { return _deptname; }
            set { _deptname = value; }
        }
        /// <summary>
        /// SAMPLETYPENAME
        /// </summary>		
        private string _sampletypename;
        [DataMember]
        public string SAMPLETYPENAME
        {
            get { return _sampletypename; }
            set { _sampletypename = value; }
        }
        /// <summary>
        /// AGE
        /// </summary>		
        private string _age;
        [DataMember]
        public string AGE
        {
            get { return _age; }
            set { _age = value; }
        }
        /// <summary>
        /// OPERDATE
        /// </summary>		
        private DateTime? _operdate;
        [DataMember]
        public DateTime? OPERDATE
        {
            get { return _operdate; }
            set { _operdate = value; }
        }
        /// <summary>
        /// OPERTIME
        /// </summary>		
        private DateTime? _opertime;
        [DataMember]
        public DateTime? OPERTIME
        {
            get { return _opertime; }
            set { _opertime = value; }
        }
        /// <summary>
        /// DOCTORNAME
        /// </summary>		
        private string _doctorname;
        [DataMember]
        public string DOCTORNAME
        {
            get { return _doctorname; }
            set { _doctorname = value; }
        }
        /// <summary>
        /// COLLECTER
        /// </summary>		
        private string _collecter;
        [DataMember]
        public string COLLECTER
        {
            get { return _collecter; }
            set { _collecter = value; }
        }
        /// <summary>
        /// COLLECTDATE
        /// </summary>		
        private DateTime? _collectdate;
        [DataMember]
        public DateTime? COLLECTDATE
        {
            get { return _collectdate; }
            set { _collectdate = value; }
        }
        /// <summary>
        /// COLLECTTIME
        /// </summary>		
        private DateTime? _collecttime;
        [DataMember]
        public DateTime? COLLECTTIME
        {
            get { return _collecttime; }
            set { _collecttime = value; }
        }
        /// <summary>
        /// CLIENTNO
        /// </summary>		
        private string _clientno;
        [DataMember]
        public string CLIENTNO
        {
            get { return _clientno; }
            set { _clientno = value; }
        }
        private string clientList;
        [DataMember]
        public string ClientList
        {
            get { return clientList; }
            set { clientList = value; }
        }

        /// <summary>
        /// CLIENTNAME
        /// </summary>		
        private string _clientname;
        [DataMember]
        public string CLIENTNAME
        {
            get { return _clientname; }
            set { _clientname = value; }
        }
        /// <summary>
        /// CheckNo
        /// </summary>		
        private string _checkno;
        [DataMember]
        public string CheckNo
        {
            get { return _checkno; }
            set { _checkno = value; }
        }
        /// <summary>
        /// CheckName
        /// </summary>		
        private string _checkname;
        [DataMember]
        public string CheckName
        {
            get { return _checkname; }
            set { _checkname = value; }
        }
        /// <summary>
        /// ReportValueMSG
        /// </summary>		
        private int? _reportvaluemsg;
        [DataMember]
        public int? ReportValueMSG
        {
            get { return _reportvaluemsg; }
            set { _reportvaluemsg = value; }
        }
        /// <summary>
        /// CHECKDATE
        /// </summary>		
        private DateTime? _checkstartdate;
        [DataMember]
        public DateTime? Checkstartdate
        {
            get { return _checkstartdate; }
            set { _checkstartdate = value; }
        }
        private DateTime? _checkenddate;
        [DataMember]
        public DateTime? Checkenddate
        {
            get { return _checkenddate; }
            set { _checkenddate = value; }
        }
        /// <summary>
        /// CHECKER
        /// </summary>		
        private string _checker;
        [DataMember]
        public string CHECKER
        {
            get { return _checker; }
            set { _checker = value; }
        }
        /// <summary>
        /// NOperDate
        /// </summary>		
        private string _noperdate;
        [DataMember]
        public string NOperDate
        {
            get { return _noperdate; }
            set { _noperdate = value; }
        }
        /// <summary>
        /// NOPERTIME
        /// </summary>		
        private string _nopertime;
        [DataMember]
        public string NOPERTIME
        {
            get { return _nopertime; }
            set { _nopertime = value; }
        }
        /// <summary>
        /// clientzdy3
        /// </summary>		
        private string _clientzdy3;
        [DataMember]
        public string clientzdy3
        {
            get { return _clientzdy3; }
            set { _clientzdy3 = value; }
        }
        /// <summary>
        /// PREC
        /// </summary>		
        private string _prec;
        [DataMember]
        public string PREC
        {
            get { return _prec; }
            set { _prec = value; }
        }
        /// <summary>
        /// itemunit
        /// </summary>		
        private string _itemunit;
        [DataMember]
        public string itemunit
        {
            get { return _itemunit; }
            set { _itemunit = value; }
        }
        /// <summary>
        /// secretgrade
        /// </summary>		
        private int? _secretgrade;
        [DataMember]
        public int? secretgrade
        {
            get { return _secretgrade; }
            set { _secretgrade = value; }
        }
        /// <summary>
        /// itemename
        /// </summary>		
        private string _itemename;
        [DataMember]
        public string itemename
        {
            get { return _itemename; }
            set { _itemename = value; }
        }
        /// <summary>
        /// shortname
        /// </summary>		
        private string _shortname;
        [DataMember]
        public string shortname
        {
            get { return _shortname; }
            set { _shortname = value; }
        }
        /// <summary>
        /// shortcode
        /// </summary>		
        private string _shortcode;
        [DataMember]
        public string shortcode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }
        /// <summary>
        /// cuegrade
        /// </summary>		
        private int? _cuegrade;
        [DataMember]
        public int? cuegrade
        {
            get { return _cuegrade; }
            set { _cuegrade = value; }
        }
        /// <summary>
        /// AGEUNITNAME
        /// </summary>		
        private string _ageunitname;
        [DataMember]
        public string AGEUNITNAME
        {
            get { return _ageunitname; }
            set { _ageunitname = value; }
        }
        /// <summary>
        /// DISTRICTNAME
        /// </summary>		
        private string _districtname;
        [DataMember]
        public string DISTRICTNAME
        {
            get { return _districtname; }
            set { _districtname = value; }
        }
        /// <summary>
        /// WARDNAME
        /// </summary>		
        private string _wardname;
        [DataMember]
        public string WARDNAME
        {
            get { return _wardname; }
            set { _wardname = value; }
        }
        /// <summary>
        /// FOLKNAME
        /// </summary>		
        private string _folkname;
        [DataMember]
        public string FOLKNAME
        {
            get { return _folkname; }
            set { _folkname = value; }
        }
        /// <summary>
        /// SICKTYPENAME
        /// </summary>		
        private string _sicktypename;
        [DataMember]
        public string SICKTYPENAME
        {
            get { return _sicktypename; }
            set { _sicktypename = value; }
        }
        /// <summary>
        /// SECTIONNAME
        /// </summary>		
        private string _sectionname;
        [DataMember]
        public string SECTIONNAME
        {
            get { return _sectionname; }
            set { _sectionname = value; }
        }
        /// <summary>
        /// TESTTYPENAME
        /// </summary>		
        private string _testtypename;
        [DataMember]
        public string TESTTYPENAME
        {
            get { return _testtypename; }
            set { _testtypename = value; }
        }
        /// <summary>
        /// SAMPLETYPENO
        /// </summary>		
        private int? _sampletypeno;
        [DataMember]
        public int? SAMPLETYPENO
        {
            get { return _sampletypeno; }
            set { _sampletypeno = value; }
        }
        /// <summary>
        /// GENDERNO
        /// </summary>		
        private int? _genderno;
        [DataMember]
        public int? GENDERNO
        {
            get { return _genderno; }
            set { _genderno = value; }
        }
        /// <summary>
        /// BIRTHDAY
        /// </summary>		
        private DateTime? _birthday;
        [DataMember]
        public DateTime? BIRTHDAY
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        /// <summary>
        /// AGEUNITNO
        /// </summary>		
        private int? _ageunitno;
        [DataMember]
        public int? AGEUNITNO
        {
            get { return _ageunitno; }
            set { _ageunitno = value; }
        }
        /// <summary>
        /// FOLKNO
        /// </summary>		
        private string _folkno;
        [DataMember]
        public string FOLKNO
        {
            get { return _folkno; }
            set { _folkno = value; }
        }
        /// <summary>
        /// DISTRICTNO
        /// </summary>		
        private string _districtno;
        [DataMember]
        public string DISTRICTNO
        {
            get { return _districtno; }
            set { _districtno = value; }
        }
        /// <summary>
        /// WARDNO
        /// </summary>		
        private string _wardno;
        [DataMember]
        public string WARDNO
        {
            get { return _wardno; }
            set { _wardno = value; }
        }
        /// <summary>
        /// BED
        /// </summary>		
        private string _bed;
        [DataMember]
        public string BED
        {
            get { return _bed; }
            set { _bed = value; }
        }
        /// <summary>
        /// DEPTNO
        /// </summary>		
        private int? _deptno;
        [DataMember]
        public int? DEPTNO
        {
            get { return _deptno; }
            set { _deptno = value; }
        }
        /// <summary>
        /// DOCTOR
        /// </summary>		
        private string _doctor;
        [DataMember]
        public string DOCTOR
        {
            get { return _doctor; }
            set { _doctor = value; }
        }
        /// <summary>
        /// CHARGENO
        /// </summary>		
        private string _chargeno;
        [DataMember]
        public string CHARGENO
        {
            get { return _chargeno; }
            set { _chargeno = value; }
        }
        /// <summary>
        /// CHARGE
        /// </summary>		
        private string _charge;
        [DataMember]
        public string CHARGE
        {
            get { return _charge; }
            set { _charge = value; }
        }
        /// <summary>
        /// FORMMEMO
        /// </summary>		
        private string _formmemo;
        [DataMember]
        public string FORMMEMO
        {
            get { return _formmemo; }
            set { _formmemo = value; }
        }
        /// <summary>
        /// TESTDATE
        /// </summary>		
        private DateTime? _testdate;
        [DataMember]
        public DateTime? TESTDATE
        {
            get { return _testdate; }
            set { _testdate = value; }
        }
        /// <summary>
        /// TESTTIME
        /// </summary>		
        private DateTime? _testtime;
        [DataMember]
        public DateTime? TESTTIME
        {
            get { return _testtime; }
            set { _testtime = value; }
        }
        /// <summary>
        /// OPERATOR
        /// </summary>		
        private string _operator;
        [DataMember]
        public string OPERATOR
        {
            get { return _operator; }
            set { _operator = value; }
        }
        /// <summary>
        /// PRINTTIMES
        /// </summary>		
        private int? _printtimes;
        [DataMember]
        public int? PRINTTIMES
        {
            get { return _printtimes; }
            set { _printtimes = value; }
        }
        /// <summary>
        /// resultfile
        /// </summary>		
        private string _resultfile;
        [DataMember]
        public string resultfile
        {
            get { return _resultfile; }
            set { _resultfile = value; }
        }
        /// <summary>
        /// CHECKTIME
        /// </summary>		
        private DateTime? _checktime;
        [DataMember]
        public DateTime? CHECKTIME
        {
            get { return _checktime; }
            set { _checktime = value; }
        }
        /// <summary>
        /// REQUESTSOURCE
        /// </summary>		
        private string _requestsource;
        [DataMember]
        public string REQUESTSOURCE
        {
            get { return _requestsource; }
            set { _requestsource = value; }
        }
        /// <summary>
        /// DIAGNO
        /// </summary>		
        private string _diagno;
        [DataMember]
        public string DIAGNO
        {
            get { return _diagno; }
            set { _diagno = value; }
        }
        /// <summary>
        /// SICKTYPENO
        /// </summary>		
        private string _sicktypeno;
        [DataMember]
        public string SICKTYPENO
        {
            get { return _sicktypeno; }
            set { _sicktypeno = value; }
        }
        /// <summary>
        /// FORMCOMMENT
        /// </summary>		
        private string _formcomment;
        [DataMember]
        public string FORMCOMMENT
        {
            get { return _formcomment; }
            set { _formcomment = value; }
        }
        /// <summary>
        /// ARTIFICERORDER
        /// </summary>		
        private string _artificerorder;
        [DataMember]
        public string ARTIFICERORDER
        {
            get { return _artificerorder; }
            set { _artificerorder = value; }
        }
        /// <summary>
        /// SICKORDER
        /// </summary>		
        private string _sickorder;
        [DataMember]
        public string SICKORDER
        {
            get { return _sickorder; }
            set { _sickorder = value; }
        }
        /// <summary>
        /// SICKTYPE
        /// </summary>		
        private string _sicktype;
        [DataMember]
        public string SICKTYPE
        {
            get { return _sicktype; }
            set { _sicktype = value; }
        }
        /// <summary>
        /// CHARGEFLAG
        /// </summary>		
        private string _chargeflag;
        [DataMember]
        public string CHARGEFLAG
        {
            get { return _chargeflag; }
            set { _chargeflag = value; }
        }
        /// <summary>
        /// TESTDEST
        /// </summary>		
        private string _testdest;
        [DataMember]
        public string TESTDEST
        {
            get { return _testdest; }
            set { _testdest = value; }
        }
        /// <summary>
        /// SLABLE
        /// </summary>		
        private string _slable;
        [DataMember]
        public string SLABLE
        {
            get { return _slable; }
            set { _slable = value; }
        }
        /// <summary>
        /// INCEPTDATE
        /// </summary>		
        private DateTime? _inceptdate;
        [DataMember]
        public DateTime? INCEPTDATE
        {
            get { return _inceptdate; }
            set { _inceptdate = value; }
        }
        /// <summary>
        /// INCEPTTIME
        /// </summary>		
        private DateTime? _incepttime;
        [DataMember]
        public DateTime? INCEPTTIME
        {
            get { return _incepttime; }
            set { _incepttime = value; }
        }
        /// <summary>
        /// ONLINETIME
        /// </summary>		
        private DateTime? _onlinetime;
        [DataMember]
        public DateTime? ONLINETIME
        {
            get { return _onlinetime; }
            set { _onlinetime = value; }
        }

        /// <summary>
        /// type
        /// </summary>		
        private string _type;
        [DataMember]
        public string TYPE
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}

