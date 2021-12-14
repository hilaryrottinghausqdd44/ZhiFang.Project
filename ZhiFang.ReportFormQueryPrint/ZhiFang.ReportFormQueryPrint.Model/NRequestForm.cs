using System;
namespace ZhiFang.ReportFormQueryPrint.Model
{
	/// <summary>
	/// NRequestForm:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class NRequestForm
	{
		public NRequestForm()
		{}
		#region Model
		private string _serialno;
		private int? _receiveflag;
		private int? _statusno;
		private int? _sampletypeno;
		private string _patno;
		private string _cname;
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
		private int? _chargeno;
		private decimal? _charge;
		private string _collecterid;
		private string _collecter;
		private DateTime? _collectdate;
		private DateTime? _collecttime;
		private string _operator;
		private DateTime? _operdate;
		private DateTime? _opertime;
		private string _formmemo;
		private string _requestsource;
		private string _artificerorder;
		private string _sickorder;
		private string _chargeflag;
		private int? _jztype;
		private string _zdy1;
		private string _zdy2;
		private string _zdy3;
		private string _zdy4;
		private string _zdy5;
		private DateTime? _flagdatedelete;
		private string _formcomment;
		private string _nurseflag;
		private string _diag;
		private string _caseno;
		private string _refuseopinion;
		private string _refusereason;
		private DateTime? _signintime;
		private string _signer;
		private int? _signflag;
		private int? _samplinggroupno;
		private int? _printcount;
		private string _printinfo;
		private decimal? _samplecap;
		private int? _isprep;
		private int? _isaffirm;
		private int? _issampling;
		private int? _issend;
		private string _incepter;
		private DateTime? _incepttime;
		private DateTime? _inceptdate;
		private bool? _isbyhand;
		private int? _assignflag;
		private string _oldserialno;
		private int? _testtypeno;
		private int? _dispenseflag;
		private string _refuseuser;
		private DateTime? _refusetime;
		private string _jytype;
		private string _serialscantime_old;
		private int? _ischeckfee;
		private int? _dr2flag;
		private int? _execdeptno;
		private string _clienthost;
		private int? _prenumber;
		private string _urgentstate;
		private string _zdy6;
		private string _zdy7;
		private string _zdy8;
		private string _zdy9;
		private string _zdy10;
		private string _phonecode;
		private int? _isnode;
		private int? _phonenodecount;
		private int? _autonodecount;
		private int? _clientno;
		private DateTime? _serialscantime;
		private string _countnodesformsource;
		private int? _stateflag;
		private DateTime? _affirmtime;
		private int? _isnursedo;
		private string _nursesender;
		private DateTime? _nursesendtime;
		private string _nursesendcarrier;
		private int? _collectcount;
		private int? _foreignsendflag;
		private int? _hisaffirm;
		private byte[] _patphoto;
		private string _chargeorderno;
		private int? _reportflag;
		private string _DeptName;
		private string _SickTypeNo;
		private string _SickTypeName;
		private int _ItemNum;

		public int ItemNum
		{
			set { _ItemNum = value; }
			get { return _ItemNum; }
		}
		public string SickTypeNo
		{
			set { _SickTypeNo = value; }
			get { return _SickTypeNo; }
		}
		public string SickTypeName
		{
			set { _SickTypeName = value; }
			get { return _SickTypeName; }
		}
		public string DeptName
		{
			set { _DeptName = value; }
			get { return _DeptName; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string SerialNo
		{
			set{ _serialno=value;}
			get{return _serialno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReceiveFlag
		{
			set{ _receiveflag=value;}
			get{return _receiveflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StatusNo
		{
			set{ _statusno=value;}
			get{return _statusno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SampleTypeNo
		{
			set{ _sampletypeno=value;}
			get{return _sampletypeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PatNo
		{
			set{ _patno=value;}
			get{return _patno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CName
		{
			set{ _cname=value;}
			get{return _cname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GenderNo
		{
			set{ _genderno=value;}
			get{return _genderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Age
		{
			set{ _age=value;}
			get{return _age;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AgeUnitNo
		{
			set{ _ageunitno=value;}
			get{return _ageunitno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FolkNo
		{
			set{ _folkno=value;}
			get{return _folkno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DistrictNo
		{
			set{ _districtno=value;}
			get{return _districtno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? WardNo
		{
			set{ _wardno=value;}
			get{return _wardno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Bed
		{
			set{ _bed=value;}
			get{return _bed;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DeptNo
		{
			set{ _deptno=value;}
			get{return _deptno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Doctor
		{
			set{ _doctor=value;}
			get{return _doctor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DiagNo
		{
			set{ _diagno=value;}
			get{return _diagno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChargeNo
		{
			set{ _chargeno=value;}
			get{return _chargeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Charge
		{
			set{ _charge=value;}
			get{return _charge;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CollecterID
		{
			set{ _collecterid=value;}
			get{return _collecterid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Collecter
		{
			set{ _collecter=value;}
			get{return _collecter;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CollectDate
		{
			set{ _collectdate=value;}
			get{return _collectdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CollectTime
		{
			set{ _collecttime=value;}
			get{return _collecttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Operator
		{
			set{ _operator=value;}
			get{return _operator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? OperDate
		{
			set{ _operdate=value;}
			get{return _operdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? OperTime
		{
			set{ _opertime=value;}
			get{return _opertime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FormMemo
		{
			set{ _formmemo=value;}
			get{return _formmemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RequestSource
		{
			set{ _requestsource=value;}
			get{return _requestsource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Artificerorder
		{
			set{ _artificerorder=value;}
			get{return _artificerorder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sickorder
		{
			set{ _sickorder=value;}
			get{return _sickorder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string chargeflag
		{
			set{ _chargeflag=value;}
			get{return _chargeflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? jztype
		{
			set{ _jztype=value;}
			get{return _jztype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string zdy1
		{
			set{ _zdy1=value;}
			get{return _zdy1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string zdy2
		{
			set{ _zdy2=value;}
			get{return _zdy2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string zdy3
		{
			set{ _zdy3=value;}
			get{return _zdy3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string zdy4
		{
			set{ _zdy4=value;}
			get{return _zdy4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string zdy5
		{
			set{ _zdy5=value;}
			get{return _zdy5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlagDateDelete
		{
			set{ _flagdatedelete=value;}
			get{return _flagdatedelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FormComment
		{
			set{ _formcomment=value;}
			get{return _formcomment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string nurseflag
		{
			set{ _nurseflag=value;}
			get{return _nurseflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string diag
		{
			set{ _diag=value;}
			get{return _diag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CaseNo
		{
			set{ _caseno=value;}
			get{return _caseno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refuseopinion
		{
			set{ _refuseopinion=value;}
			get{return _refuseopinion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refusereason
		{
			set{ _refusereason=value;}
			get{return _refusereason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? signintime
		{
			set{ _signintime=value;}
			get{return _signintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string signer
		{
			set{ _signer=value;}
			get{return _signer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? signflag
		{
			set{ _signflag=value;}
			get{return _signflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SamplingGroupNo
		{
			set{ _samplinggroupno=value;}
			get{return _samplinggroupno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PrintCount
		{
			set{ _printcount=value;}
			get{return _printcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrintInfo
		{
			set{ _printinfo=value;}
			get{return _printinfo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SampleCap
		{
			set{ _samplecap=value;}
			get{return _samplecap;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsPrep
		{
			set{ _isprep=value;}
			get{return _isprep;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsAffirm
		{
			set{ _isaffirm=value;}
			get{return _isaffirm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsSampling
		{
			set{ _issampling=value;}
			get{return _issampling;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsSend
		{
			set{ _issend=value;}
			get{return _issend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string incepter
		{
			set{ _incepter=value;}
			get{return _incepter;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? inceptTime
		{
			set{ _incepttime=value;}
			get{return _incepttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? inceptDate
		{
			set{ _inceptdate=value;}
			get{return _inceptdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool? isByHand
		{
			set{ _isbyhand=value;}
			get{return _isbyhand;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AssignFlag
		{
			set{ _assignflag=value;}
			get{return _assignflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OldSerialNo
		{
			set{ _oldserialno=value;}
			get{return _oldserialno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TestTypeNo
		{
			set{ _testtypeno=value;}
			get{return _testtypeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DispenseFlag
		{
			set{ _dispenseflag=value;}
			get{return _dispenseflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string refuseUser
		{
			set{ _refuseuser=value;}
			get{return _refuseuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? refuseTime
		{
			set{ _refusetime=value;}
			get{return _refusetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string jytype
		{
			set{ _jytype=value;}
			get{return _jytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SerialScanTime_old
		{
			set{ _serialscantime_old=value;}
			get{return _serialscantime_old;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsCheckFee
		{
			set{ _ischeckfee=value;}
			get{return _ischeckfee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Dr2Flag
		{
			set{ _dr2flag=value;}
			get{return _dr2flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ExecDeptNo
		{
			set{ _execdeptno=value;}
			get{return _execdeptno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ClientHost
		{
			set{ _clienthost=value;}
			get{return _clienthost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PreNumber
		{
			set{ _prenumber=value;}
			get{return _prenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UrgentState
		{
			set{ _urgentstate=value;}
			get{return _urgentstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZDY6
		{
			set{ _zdy6=value;}
			get{return _zdy6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZDY7
		{
			set{ _zdy7=value;}
			get{return _zdy7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZDY8
		{
			set{ _zdy8=value;}
			get{return _zdy8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZDY9
		{
			set{ _zdy9=value;}
			get{return _zdy9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZDY10
		{
			set{ _zdy10=value;}
			get{return _zdy10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string phoneCode
		{
			set{ _phonecode=value;}
			get{return _phonecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsNode
		{
			set{ _isnode=value;}
			get{return _isnode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PhoneNodeCount
		{
			set{ _phonenodecount=value;}
			get{return _phonenodecount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AutoNodeCount
		{
			set{ _autonodecount=value;}
			get{return _autonodecount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? clientno
		{
			set{ _clientno=value;}
			get{return _clientno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SerialScanTime
		{
			set{ _serialscantime=value;}
			get{return _serialscantime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CountNodesFormSource
		{
			set{ _countnodesformsource=value;}
			get{return _countnodesformsource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StateFlag
		{
			set{ _stateflag=value;}
			get{return _stateflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AffirmTime
		{
			set{ _affirmtime=value;}
			get{return _affirmtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsNurseDo
		{
			set{ _isnursedo=value;}
			get{return _isnursedo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NurseSender
		{
			set{ _nursesender=value;}
			get{return _nursesender;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? NurseSendTime
		{
			set{ _nursesendtime=value;}
			get{return _nursesendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NurseSendCarrier
		{
			set{ _nursesendcarrier=value;}
			get{return _nursesendcarrier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CollectCount
		{
			set{ _collectcount=value;}
			get{return _collectcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ForeignSendFlag
		{
			set{ _foreignsendflag=value;}
			get{return _foreignsendflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? HisAffirm
		{
			set{ _hisaffirm=value;}
			get{return _hisaffirm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] PatPhoto
		{
			set{ _patphoto=value;}
			get{return _patphoto;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChargeOrderNo
		{
			set{ _chargeorderno=value;}
			get{return _chargeorderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReportFlag
		{
			set{ _reportflag=value;}
			get{return _reportflag;}
		}
		#endregion Model

	}
}

