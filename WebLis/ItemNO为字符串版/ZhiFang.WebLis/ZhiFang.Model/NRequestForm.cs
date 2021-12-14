using System;
using System.Runtime.Serialization;
using ZhiFang.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace ZhiFang.Model
{
    /// <summary>
    /// NRequestForm:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    //[DataContract]
    public partial class NRequestForm
    {
        public NRequestForm()
        { }
        #region Model
        private string _clientno;
        private string _clientname;
        private string _ageunitname;
        private string _gendername;
        private string _deptname;
        private string _districtname;
        private string _wardname;
        private string _folkname;
        private string _clinictypename;
        private string _testtypename;
        private string _cname;
        private string _doctorname;
        private string _collectername;
        private string _sampletypename;
        private string _serialno;
        private int? _receiveflag;
        private int? _statusno;
        private int? _sampletypeno;
        private string _patno;
        private int? _genderno;
        private DateTime? _birthday;
        private decimal? _age;
        private int? _ageunitno;
        private int? _folkno;
        private int? _districtno;
        private int? _wardno;
        private string _bed;
        private int? _deptno;
        private int? _doctor;
        private int? _diagno;
        private string _diag;
        private int? _chargeno;
        private decimal? _charge;
        private string _chargeflag;
        private string _countnodesformsource;
        private int? _ischeckfee;
        private string _operator;
        private DateTime? _operdate;
        private DateTime? _opertime;
        private string _formmemo;
        private string _requestsource;
        private string _sickorder;
        private int? _jztype;
        private string _zdy1;
        private string _zdy2;
        private string _zdy3;
        private string _zdy4;
        private string _zdy5;
        private DateTime? _flagdatedelete;
        private int? _nurseflag;
        private bool? _isbyhand;
        private int? _testtypeno;
        private int? _execdeptno;
        private DateTime? _collectdate;
        private DateTime? _collecttime;
        private string _collecter;
        private string _labcenter;
        private long? _nrequestformno;
        private string _checkno;
        private string _checkname;
        private string _weblissourceorgid;
        private string _weblissourceorgname;
        private string _oldserialno;
        private string _weblissourceorgidlist;
        private string _barcodelist;
        /// <summary>
        /// 录入医疗机构编号
        /// </summary>

        public string ClientNo
        {
            set { _clientno = value; }
            get { return _clientno; }
        }
        /// <summary>
        /// 录入医疗机构名称
        /// </summary>

        public string ClientName
        {
            set { _clientname = value; }
            get { return _clientname; }
        }
        /// <summary>
        /// 年龄单位
        /// </summary>

        public string AgeUnitName
        {
            set { _ageunitname = value; }
            get { return _ageunitname; }
        }
        /// <summary>
        /// 性别
        /// </summary>

        public string GenderName
        {
            set { _gendername = value; }
            get { return _gendername; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>

        public string DeptName
        {
            set { _deptname = value; }
            get { return _deptname; }
        }
        /// <summary>
        /// 病区名称
        /// </summary>

        public string DistrictName
        {
            set { _districtname = value; }
            get { return _districtname; }
        }
        /// <summary>
        /// 病房名称
        /// </summary>

        public string WardName
        {
            set { _wardname = value; }
            get { return _wardname; }
        }
        /// <summary>
        /// 民族
        /// </summary>

        public string FolkName
        {
            set { _folkname = value; }
            get { return _folkname; }
        }
        /// <summary>
        /// 就诊类型名称
        /// </summary>

        public string ClinicTypeName
        {
            set { _clinictypename = value; }
            get { return _clinictypename; }
        }
        /// <summary>
        /// 
        /// </summary>

        public string TestTypeName
        {
            set { _testtypename = value; }
            get { return _testtypename; }
        }
        /// <summary>
        /// 姓名
        /// </summary>

        public string CName
        {
            set { _cname = value; }
            get { return _cname; }
        }
        /// <summary>
        /// 医生
        /// </summary>

        public string DoctorName
        {
            set { _doctorname = value; }
            get { return _doctorname; }
        }
        /// <summary>
        /// 采样人姓名
        /// </summary>

        public string CollecterName
        {
            set { _collectername = value; }
            get { return _collectername; }
        }
        /// <summary>
        /// 
        /// </summary>

        public string SampleTypeName
        {
            set { _sampletypename = value; }
            get { return _sampletypename; }
        }
        /// <summary>
        /// 检验单号
        /// </summary>

        public string SerialNo
        {
            set { _serialno = value; }
            get { return _serialno; }
        }
        /// <summary>
        /// 核收标志
        /// </summary>

        public int? ReceiveFlag
        {
            set { _receiveflag = value; }
            get { return _receiveflag; }
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
        /// 样本类型
        /// </summary>

        public int? SampleTypeNo
        {
            set { _sampletypeno = value; }
            get { return _sampletypeno; }
        }
        /// <summary>
        /// 病历号
        /// </summary>

        public string PatNo
        {
            set { _patno = value; }
            get { return _patno; }
        }
        /// <summary>
        /// 性别编号
        /// </summary>

        public int? GenderNo
        {
            set { _genderno = value; }
            get { return _genderno; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 年龄
        /// </summary>

        public decimal? Age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 年龄单位编号
        /// </summary>

        public int? AgeUnitNo
        {
            set { _ageunitno = value; }
            get { return _ageunitno; }
        }
        /// <summary>
        /// 民族编号
        /// </summary>

        public int? FolkNo
        {
            set { _folkno = value; }
            get { return _folkno; }
        }
        /// <summary>
        /// 病区编号
        /// </summary>

        public int? DistrictNo
        {
            set { _districtno = value; }
            get { return _districtno; }
        }
        /// <summary>
        /// 病房编号
        /// </summary>

        public int? WardNo
        {
            set { _wardno = value; }
            get { return _wardno; }
        }
        /// <summary>
        /// 病床
        /// </summary>

        public string Bed
        {
            set { _bed = value; }
            get { return _bed; }
        }
        /// <summary>
        /// 科室编号
        /// </summary>

        public int? DeptNo
        {
            set { _deptno = value; }
            get { return _deptno; }
        }
        /// <summary>
        /// 医生编号
        /// </summary>

        public int? Doctor
        {
            set { _doctor = value; }
            get { return _doctor; }
        }
        /// <summary>
        /// 诊断编号
        /// </summary>

        public int? DiagNo
        {
            set { _diagno = value; }
            get { return _diagno; }
        }
        /// <summary>
        /// 诊断描述
        /// </summary>

        public string Diag
        {
            set { _diag = value; }
            get { return _diag; }
        }
        /// <summary>
        /// 收费类型
        /// </summary>

        public int? ChargeNo
        {
            set { _chargeno = value; }
            get { return _chargeno; }
        }
        /// <summary>
        /// 费用
        /// </summary>

        public decimal? Charge
        {
            set { _charge = value; }
            get { return _charge; }
        }
        /// <summary>
        /// 
        /// </summary>

        public string Chargeflag
        {
            set { _chargeflag = value; }
            get { return _chargeflag; }
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
        /// 计费状态
        /// </summary>

        public int? IsCheckFee
        {
            set { _ischeckfee = value; }
            get { return _ischeckfee; }
        }
        /// <summary>
        /// 开单者
        /// </summary>

        public string Operator
        {
            set { _operator = value; }
            get { return _operator; }
        }
        /// <summary>
        /// 申请日期
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? OperDate
        {
            set { _operdate = value; }
            get { return _operdate; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? OperTime
        {
            set { _opertime = value; }
            get { return _opertime; }
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

        public string RequestSource
        {
            set { _requestsource = value; }
            get { return _requestsource; }
        }
        /// <summary>
        /// 
        /// </summary>

        public string SickOrder
        {
            set { _sickorder = value; }
            get { return _sickorder; }
        }
        /// <summary>
        /// 
        /// </summary>

        public int? jztype
        {
            set { _jztype = value; }
            get { return _jztype; }
        }
        /// <summary>
        /// 自定义1
        /// </summary>

        public string zdy1
        {
            set { _zdy1 = value; }
            get { return _zdy1; }
        }
        /// <summary>
        /// 自定义2
        /// </summary>
        public string zdy2
        {
            set { _zdy2 = value; }
            get { return _zdy2; }
        }
        /// <summary>
        /// 自定义3
        /// </summary>
        public string zdy3
        {
            set { _zdy3 = value; }
            get { return _zdy3; }
        }
        /// <summary>
        /// 自定义4
        /// </summary>
        public string zdy4
        {
            set { _zdy4 = value; }
            get { return _zdy4; }
        }
        /// <summary>
        /// 自定义5
        /// </summary>
        public string zdy5
        {
            set { _zdy5 = value; }
            get { return _zdy5; }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? FlagDateDelete
        {
            set { _flagdatedelete = value; }
            get { return _flagdatedelete; }
        }
        /// <summary>
        /// 条码处理标记
        /// </summary>

        public int? NurseFlag
        {
            set { _nurseflag = value; }
            get { return _nurseflag; }
        }
        /// <summary>
        /// 
        /// </summary>

        public bool? IsByHand
        {
            set { _isbyhand = value; }
            get { return _isbyhand; }
        }
        /// <summary>
        /// 检测类型
        /// </summary>

        public int? TestTypeNo
        {
            set { _testtypeno = value; }
            get { return _testtypeno; }
        }
        /// <summary>
        /// 
        /// </summary>

        public int? ExecDeptNo
        {
            set { _execdeptno = value; }
            get { return _execdeptno; }
        }
        /// <summary>
        /// 采样日期
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? CollectDate
        {
            set { _collectdate = value; }
            get { return _collectdate; }
        }
        /// <summary>
        /// 采样时间
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? CollectTime
        {
            set { _collecttime = value; }
            get { return _collecttime; }
        }
        /// <summary>
        /// 采样人
        /// </summary>

        public string Collecter
        {
            set { _collecter = value; }
            get { return _collecter; }
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
        /// 申请编号
        /// </summary>
        [JsonConverter(typeof(JsonConvertClass))]
        public long? NRequestFormNo
        {
            set { _nrequestformno = value; }
            get { return _nrequestformno; }
        }
        /// <summary>
        /// 检测单位编号
        /// </summary>

        public string CheckNo
        {
            set { _checkno = value; }
            get { return _checkno; }
        }
        /// <summary>
        /// 检测单位
        /// </summary>

        public string CheckName
        {
            set { _checkname = value; }
            get { return _checkname; }
        }
        /// <summary>
        /// 被录入送检单位编号
        /// </summary>

        public string WebLisSourceOrgID
        {
            set { _weblissourceorgid = value; }
            get { return _weblissourceorgid; }
        }
        /// <summary>
        /// 被录入送检单位名称
        /// </summary>

        public string WebLisSourceOrgName
        {
            set { _weblissourceorgname = value; }
            get { return _weblissourceorgname; }
        }

        public string OldSerialNo
        {
            set { _oldserialno = value; }
            get { return _oldserialno; }
        }
        #endregion Model

        public string Weblisflag { get; set; }

        public string CollecterID { get; set; }

        public string Artificerorder { get; set; }

        public string sickorder { get; set; }

        public string chargeflag { get; set; }

        public string FormComment { get; set; }

        public string nurseflag { get; set; }

        public string diag { get; set; }

        public string CaseNo { get; set; }

        public string refuseopinion { get; set; }

        public string refusereason { get; set; }
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime signintime { get; set; }

        public string signer { get; set; }

        public int signflag { get; set; }

        public int SamplingGroupNo { get; set; }

        public int PrintCount { get; set; }

        public string PrintInfo { get; set; }

        public decimal SampleCap { get; set; }

        public int IsPrep { get; set; }

        public int IsAffirm { get; set; }

        public int IsSampling { get; set; }

        public int IsSend { get; set; }

        public string incepter { get; set; }
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime inceptTime { get; set; }
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime inceptDate { get; set; }

        public bool isByHand { get; set; }

        public int AssignFlag { get; set; }

        public int DispenseFlag { get; set; }

        public string refuseUser { get; set; }
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime refuseTime { get; set; }

        public string jytype { get; set; }

        public string SerialScanTime_old { get; set; }

        public int Dr2Flag { get; set; }

        public string ClientHost { get; set; }

        public int PreNumber { get; set; }

        public string UrgentState { get; set; }

        public string ZDY6 { get; set; }

        public string ZDY7 { get; set; }

        public string ZDY8 { get; set; }

        public string ZDY9 { get; set; }

        public string ZDY10 { get; set; }

        public string phoneCode { get; set; }

        public int IsNode { get; set; }

        public int PhoneNodeCount { get; set; }

        public int AutoNodeCount { get; set; }
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime SerialScanTime { get; set; }

        public int StateFlag { get; set; }
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime AffirmTime { get; set; }

        public int IsNurseDo { get; set; }

        public string NurseSender { get; set; }
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime NurseSendTime { get; set; }

        public string NurseSendCarrier { get; set; }

        public int CollectCount { get; set; }

        public int ForeignSendFlag { get; set; }

        public int HisAffirm { get; set; }

        public byte[] PatPhoto { get; set; }

        public string ChargeOrderNo { get; set; }

        public int ReportFlag { get; set; }
        //[DataMember]
        public string OperDateStart { get; set; }
        //[DataMember]
        public string OperDateEnd { get; set; }

        public string CollectDateStart { get; set; }

        public string CollectDateEnd { get; set; }

        public bool IsOnlyNoBar { get; set; }

        public string ClientList { get; set; }
        public Model.NRequestItem[] NRI { get; set; }
        //
        private string personid;

        public string PersonID
        {
            get { return personid; }
            set { personid = value; }
        }

        private string sampletype;

        public string SampleType
        {
            get { return sampletype; }
            set { sampletype = value; }
        }

        private string telno;

        public string TelNo
        {
            get { return telno; }
            set { telno = value; }
        }

        private string webLisorgid;

        public string WebLisOrgID
        {
            get { return webLisorgid; }
            set { webLisorgid = value; }
        }

        private string webLisorgname;

        public string WebLisOrgName
        {
            get { return webLisorgname; }
            set { webLisorgname = value; }
        }

        private string statusname;

        public string STATUSName
        {
            get { return statusname; }
            set { statusname = value; }
        }

        private string jztypename;

        public string jztypeName
        {
            get { return jztypename; }
            set { jztypename = value; }
        }

        public string BarcodeList
        {
            get { return _barcodelist; }
            set { _barcodelist = value; }
        }
        public string TestAim { get; set; }
       
        #region 区域

        public string AreaNo { get; set; }

        public string AreaName { get; set; }
        #endregion


        #region 十堰大和增加,2014-12-20杨双全
        public int PrintTimes { get; set; }
        public string BarCode { get; set; }
        public string CombiItemName { get; set; }
        public decimal Price { get; set; }
        #endregion
    }

    /// <summary>
    /// NRequestForm:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>

    public partial class NRequestFormResult
    {

        public string BarcodeList { get; set; }

        public string CName { get; set; }

        public string GenderName { get; set; }

        public decimal Age { get; set; }

        public string AgeUnitName { get; set; }

        public string SampleTypeName { get; set; }

        public string DoctorName { get; set; }

        public string OperTime { get; set; }

        public string OperDate { get; set; }

        public string CollectTime { get; set; }

        public string CollectDate { get; set; }

        public string WebLisSourceOrgName { get; set; }

        public string NRequestFormNo { get; set; }

        public string Diag { get; set; }

        public string ItemList { get; set; }

        public string SickTypeName { get; set; }

        public string PatNo { get; set; }

        public string ColorName { get; set; }

        public string DeptName { get; set; }
        public string WebLisFlag { get; set; }
    }

    /// <summary>
    /// NRequestForm:微信采样实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>

    public partial class NRequestFormResultOfConsume
    {
        public string BarcodeList { get; set; }

        public string CName { get; set; }

        public string GenderName { get; set; }

        public decimal Age { get; set; }

        public string AgeUnitName { get; set; }

        public string SampleTypeName { get; set; }

        public string DoctorName { get; set; }

        public string OperTime { get; set; }

        public string OperDate { get; set; }

        public string CollectTime { get; set; }

        public string CollectDate { get; set; }

        public string WebLisSourceOrgName { get; set; }

        public string NRequestFormNo { get; set; }

        public string Diag { get; set; }

        public string ItemList { get; set; }

        public string SickTypeName { get; set; }

        public string PatNo { get; set; }

        public string ColorName { get; set; }

        public string DeptName { get; set; }
        public string WebLisFlag { get; set; }
        public string OldSerialNo { get; set; }
        public string ZDY10 { get; set; }
        
    }

}

