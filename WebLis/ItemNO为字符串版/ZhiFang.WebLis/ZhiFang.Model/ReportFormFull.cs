using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Tools;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
    /// <summary>
    /// ReportFormFull:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    //[DataContract]
    public partial class ReportFormFull
    {
        public ReportFormFull()
        { }
        #region Model
        private string _reportformid;
        private string _clientno;
        private string _cname;
        private string _ageunitname;
        private string _gendername;
        private string _deptname;
        private string _doctorname;
        private string _districtname;
        private string _wardname;
        private string _folkname;
        private string _sicktypename;
        private string _sampletypename;
        private string _sectionname;
        private string _testtypename;
        private DateTime? _receivedate = null;
        private string _sectionno;
        private string _testtypeno;
        private string _sampleno;
        private int? _statusno;
        private int? _sampletypeno;
        private string _patno;
        private int? _genderno;
        private DateTime? _birthday;
        private string _age;
        private int? _ageunitno;
        private string _folkno;
        private string _districtno;
        private string _wardno;
        private string _bed;
        private int? _deptno;
        private string _doctor;
        private string _chargeno;
        private string _charge;
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
        private int? _printtimes;
        private string _resultfile;
        private DateTime? _checkdate;
        private DateTime? _checktime;
        private string _serialno;
        private string _requestsource;
        private string _diagno;
        private string _sicktypeno;
        private string _formcomment;
        private string _artificerorder;
        private string _sickorder;
        private string _sicktype;
        private string _chargeflag;
        private string _testdest;
        private string _slable;
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
        private string _bmanno;
        private string _filetype;
        private string _jpgfile;
        private string _pdffile;
        private int? _formno;
        private string _childtablename;
        private string _printexec;
        private string _labcenter;
        private string _printtexec;
        private DateTime? startdate;
        private DateTime? enddate;
        private DateTime? checkstartdate;
        private DateTime? checkenddate;
        private string barcode;
        private string sectiontype;
        private string diagnose;
        private string clientname;
        private string rbacsql;
        private string likesearch;
        private string weblisSourceOrgId;
        private int isdown;
        private int reportsend;
        private int? resultstatus;

        public int? ResultStatus
        {
            get { return resultstatus;}
            set { resultstatus = value; }
        }

        public int ReportSend
        {
            get { return reportsend; }
            set { reportsend = value; }
        }

        public int Isdown
        {
            get { return isdown; }
            set { isdown = value; }
        }

        public string WeblisSourceOrgId
        {
            get { return weblisSourceOrgId; }
            set { weblisSourceOrgId = value; }
        }


        /// <summary>
        /// 序号
        /// </summary>
        public string ReportFormID
        {
            set { _reportformid = value; }
            get { return _reportformid; }
        }
        /// <summary>
        /// 送检单位
        /// </summary>
        public string CLIENTNO
        {
            set { _clientno = value; }
            get { return _clientno; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string CNAME
        {
            set { _cname = value; }
            get { return _cname; }
        }
        /// <summary>
        /// 年龄单位名称
        /// </summary>
        public string AGEUNITNAME
        {
            set { _ageunitname = value; }
            get { return _ageunitname; }
        }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string GENDERNAME
        {
            set { _gendername = value; }
            get { return _gendername; }
        }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DEPTNAME
        {
            set { _deptname = value; }
            get { return _deptname; }
        }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string DOCTORNAME
        {
            set { _doctorname = value; }
            get { return _doctorname; }
        }
        /// <summary>
        /// 病区名称
        /// </summary>
        public string DISTRICTNAME
        {
            set { _districtname = value; }
            get { return _districtname; }
        }
        /// <summary>
        /// 病房名称
        /// </summary>
        public string WARDNAME
        {
            set { _wardname = value; }
            get { return _wardname; }
        }
        /// <summary>
        /// 民族名称
        /// </summary>
        public string FOLKNAME
        {
            set { _folkname = value; }
            get { return _folkname; }
        }
        /// <summary>
        /// 就诊类型名称
        /// </summary>
        public string SICKTYPENAME
        {
            set { _sicktypename = value; }
            get { return _sicktypename; }
        }
        /// <summary>
        /// 样本类型名称
        /// </summary>
        public string SAMPLETYPENAME
        {
            set { _sampletypename = value; }
            get { return _sampletypename; }
        }
        /// <summary>
        /// 小组名称
        /// </summary>
        public string SECTIONNAME
        {
            set { _sectionname = value; }
            get { return _sectionname; }
        }
        /// <summary>
        /// 检验类型名称
        /// </summary>
        public string TESTTYPENAME
        {
            set { _testtypename = value; }
            get { return _testtypename; }
        }
        /// <summary>
        /// 仪器检测日期
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? RECEIVEDATE
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 小组编号
        /// </summary>
        public string SECTIONNO
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 仪器检测类型
        /// </summary>
        public string TESTTYPENO
        {
            set { _testtypeno = value; }
            get { return _testtypeno; }
        }
        /// <summary>
        /// 仪器检测样本号
        /// </summary>
        public string SAMPLENO
        {
            set { _sampleno = value; }
            get { return _sampleno; }
        }
        /// <summary>
        /// 状态编号
        /// </summary>
        public int? STATUSNO
        {
            set { _statusno = value; }
            get { return _statusno; }
        }
        /// <summary>
        /// 样本类型
        /// </summary>
        public int? SAMPLETYPENO
        {
            set { _sampletypeno = value; }
            get { return _sampletypeno; }
        }
        /// <summary>
        /// 病历号
        /// </summary>
        public string PATNO
        {
            set { _patno = value; }
            get { return _patno; }
        }
        /// <summary>
        /// 性别代码
        /// </summary>
        public int? GENDERNO
        {
            set { _genderno = value; }
            get { return _genderno; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BIRTHDAY
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public string AGE
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 年龄单位
        /// </summary>
        public int? AGEUNITNO
        {
            set { _ageunitno = value; }
            get { return _ageunitno; }
        }
        /// <summary>
        /// 民族代码
        /// </summary>
        public string FOLKNO
        {
            set { _folkno = value; }
            get { return _folkno; }
        }
        /// <summary>
        /// 病区代码
        /// </summary>
        public string DISTRICTNO
        {
            set { _districtno = value; }
            get { return _districtno; }
        }
        /// <summary>
        /// 病房代码
        /// </summary>
        public string WARDNO
        {
            set { _wardno = value; }
            get { return _wardno; }
        }
        /// <summary>
        /// 病床
        /// </summary>
        public string BED
        {
            set { _bed = value; }
            get { return _bed; }
        }
        /// <summary>
        /// 科室代码
        /// </summary>
        public int? DEPTNO
        {
            set { _deptno = value; }
            get { return _deptno; }
        }
        /// <summary>
        /// 医生名称
        /// </summary>
        public string DOCTOR
        {
            set { _doctor = value; }
            get { return _doctor; }
        }
        /// <summary>
        /// 收费类型
        /// </summary>
        public string CHARGENO
        {
            set { _chargeno = value; }
            get { return _chargeno; }
        }
        /// <summary>
        /// 费用
        /// </summary>
        public string CHARGE
        {
            set { _charge = value; }
            get { return _charge; }
        }
        /// <summary>
        /// 采样人
        /// </summary>
        public string COLLECTER
        {
            set { _collecter = value; }
            get { return _collecter; }
        }
        /// <summary>
        /// 采样日期
        /// </summary>
        public DateTime? COLLECTDATE
        {
            set { _collectdate = value; }
            get { return _collectdate; }
        }
        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime? COLLECTTIME
        {
            set { _collecttime = value; }
            get { return _collecttime; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string FORMMEMO
        {
            set { _formmemo = value; }
            get { return _formmemo; }
        }
        /// <summary>
        /// 检验技师
        /// </summary>
        public string TECHNICIAN
        {
            set { _technician = value; }
            get { return _technician; }
        }
        /// <summary>
        /// 检测日期
        /// </summary>
        public DateTime? TESTDATE
        {
            set { _testdate = value; }
            get { return _testdate; }
        }
        /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime? TESTTIME
        {
            set { _testtime = value; }
            get { return _testtime; }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string OPERATOR
        {
            set { _operator = value; }
            get { return _operator; }
        }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? OPERDATE
        {
            set { _operdate = value; }
            get { return _operdate; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OPERTIME
        {
            set { _opertime = value; }
            get { return _opertime; }
        }
        /// <summary>
        /// 审定人
        /// </summary>
        public string CHECKER
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 打印次数
        /// </summary>
        public int? PRINTTIMES
        {
            set { _printtimes = value; }
            get { return _printtimes; }
        }
        /// <summary>
        /// 报告单文件
        /// </summary>
        public string resultfile
        {
            set { _resultfile = value; }
            get { return _resultfile; }
        }
        /// <summary>
        /// 审定日期
        /// </summary>
        public DateTime? CHECKDATE
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 审定时间
        /// </summary>
        public DateTime? CHECKTIME
        {
            set { _checktime = value; }
            get { return _checktime; }
        }
        /// <summary>
        /// 医嘱单号
        /// </summary>
        public string SERIALNO
        {
            set { _serialno = value; }
            get { return _serialno; }
        }
        /// <summary>
        /// 来源机器名
        /// </summary>
        public string REQUESTSOURCE
        {
            set { _requestsource = value; }
            get { return _requestsource; }
        }
        /// <summary>
        /// 诊断描述
        /// </summary>
        public string DIAGNO
        {
            set { _diagno = value; }
            get { return _diagno; }
        }
        /// <summary>
        /// 就诊类型
        /// </summary>
        public string SICKTYPENO
        {
            set { _sicktypeno = value; }
            get { return _sicktypeno; }
        }
        /// <summary>
        /// 检验单评语
        /// </summary>
        public string FORMCOMMENT
        {
            set { _formcomment = value; }
            get { return _formcomment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ARTIFICERORDER
        {
            set { _artificerorder = value; }
            get { return _artificerorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SICKORDER
        {
            set { _sickorder = value; }
            get { return _sickorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SICKTYPE
        {
            set { _sicktype = value; }
            get { return _sicktype; }
        }
        /// <summary>
        /// 收费标记
        /// </summary>
        public string CHARGEFLAG
        {
            set { _chargeflag = value; }
            get { return _chargeflag; }
        }
        /// <summary>
        /// 检验目的
        /// </summary>
        public string TESTDEST
        {
            set { _testdest = value; }
            get { return _testdest; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SLABLE
        {
            set { _slable = value; }
            get { return _slable; }
        }
        /// <summary>
        /// 自定义1
        /// </summary>
        public string ZDY1
        {
            set { _zdy1 = value; }
            get { return _zdy1; }
        }
        /// <summary>
        /// 自定义2
        /// </summary>
        public string ZDY2
        {
            set { _zdy2 = value; }
            get { return _zdy2; }
        }
        /// <summary>
        /// 自定义3
        /// </summary>
        public string ZDY3
        {
            set { _zdy3 = value; }
            get { return _zdy3; }
        }
        /// <summary>
        /// 自定义4
        /// </summary>
        public string ZDY4
        {
            set { _zdy4 = value; }
            get { return _zdy4; }
        }
        /// <summary>
        /// 自定义5
        /// </summary>
        public string ZDY5
        {
            set { _zdy5 = value; }
            get { return _zdy5; }
        }
        public string ZDY6 { get; set; }
        public string ZDY7 { get; set; }
        public string ZDY8 { get; set; }
        public string ZDY9 { get; set; }
        public string ZDY10 { get; set; }
        /// <summary>
        /// 签收日期
        /// </summary>
        public DateTime? INCEPTDATE
        {
            set { _inceptdate = value; }
            get { return _inceptdate; }
        }
        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime? INCEPTTIME
        {
            set { _incepttime = value; }
            get { return _incepttime; }
        }
        /// <summary>
        /// 签收人
        /// </summary>
        public string INCEPTER
        {
            set { _incepter = value; }
            get { return _incepter; }
        }
        /// <summary>
        /// 上机日期
        /// </summary>
        public DateTime? ONLINEDATE
        {
            set { _onlinedate = value; }
            get { return _onlinedate; }
        }
        /// <summary>
        /// 上机时间
        /// </summary>
        public DateTime? ONLINETIME
        {
            set { _onlinetime = value; }
            get { return _onlinetime; }
        }
        /// <summary>
        /// 业务员
        /// </summary>
        public string BMANNO
        {
            set { _bmanno = value; }
            get { return _bmanno; }
        }
        /// <summary>
        /// 报告文件类型
        /// </summary>
        public string FILETYPE
        {
            set { _filetype = value; }
            get { return _filetype; }
        }
        /// <summary>
        /// 图片报告文件
        /// </summary>
        public string JPGFILE
        {
            set { _jpgfile = value; }
            get { return _jpgfile; }
        }
        /// <summary>
        /// PDF报告文件
        /// </summary>
        public string PDFFILE
        {
            set { _pdffile = value; }
            get { return _pdffile; }
        }
        /// <summary>
        /// 报告临时编号
        /// </summary>
        public int? FORMNO
        {
            set { _formno = value; }
            get { return _formno; }
        }
        public string FormNo2
        {
            get;
            set;
        }
        /// <summary>
        /// 送检单位名称
        /// </summary>
        public string CHILDTABLENAME
        {
            set { _childtablename = value; }
            get { return _childtablename; }
        }
        /// <summary>
        /// 打印
        /// </summary>
        public string PRINTEXEC
        {
            set { _printexec = value; }
            get { return _printexec; }
        }
        /// <summary>
        /// 独立实验室名称
        /// </summary>
        public string LABCENTER
        {
            set { _labcenter = value; }
            get { return _labcenter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRINTTEXEC
        {
            set { _printtexec = value; }
            get { return _printtexec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LIKESEARCH
        {
            set { likesearch = value; }
            get { return likesearch; }
        }
        //[DataMember]
        public DateTime? Startdate
        {
            set { startdate = value; }
            get { return startdate; }
        }

        //[DataMember]
        public DateTime? Enddate
        {
            set { enddate = value; }
            get { return enddate; }
        }
        //[DataMember]
        public DateTime? collectStartdate { get; set; }
       

        //[DataMember]
        public DateTime? collectEnddate { get; set; }
       
        //[DataMember]
        public DateTime? CheckStartDate
        {
            set { checkstartdate = value; }
            get { return checkstartdate; }
        }

        /// <summary>
        /// 打印时间
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime PRINTDATETIME
        {
            get;
            set;
        }
        //[JsonConverter(typeof(JsonConvertClass))]
        public DateTime? CheckEndDate
        {
            set { checkenddate = value; }
            get { return checkenddate; }
        }

        public string SectionType
        {
            set { sectiontype = value; }
            get { return sectiontype; }
        }
        public string BarCode
        {
            set { barcode = value; }
            get { return barcode; }
        }
        public string DIAGNOSE
        {
            set { diagnose = value; }
            get { return diagnose; }
        }
        public string CLIENTNAME
        {
            set { clientname = value; }
            get { return clientname; }
        }
        public string RBACSQL
        {
            set { rbacsql = value; }
            get { return rbacsql; }
        }
        public string ClientList { get; set; }
        public string WeblisSourceOrgList { get; set; }
        public string PERSONID { get; set; }
        public string clientcode { get; set; }
        public string clientename { get; set; }
        public string serialno { get; set; }
        public DateTime? noperdate { get; set; }
        public DateTime? noperdateStart { get; set; }
        public DateTime? noperdateEnd { get; set; }
        public DateTime? SenderTime2 { get; set; }
        //public decimal Prices { get; set; }
        #endregion Model

    }
}

