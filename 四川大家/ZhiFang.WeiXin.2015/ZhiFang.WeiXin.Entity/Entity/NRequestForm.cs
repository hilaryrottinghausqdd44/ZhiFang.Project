using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region NRequestForm

	/// <summary>
	/// NRequestForm object for NHibernate mapped table 'NRequestForm'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "NRequestForm", ShortCode = "NRequestForm", Desc = "")]
	public class NRequestForm : BaseEntity
	{
		#region Member Variables

		protected string _clientNo;
		protected string _clientName;
		protected string _ageUnitName;
		protected string _genderName;
		protected string _deptName;
		protected string _districtName;
		protected string _wardName;
		protected string _folkName;
		protected string _clinicTypeName;
		protected string _testTypeName;
		protected string _cName;
		protected string _doctorName;
		protected string _collecterName;
		protected string _sampleTypeName;
		protected string _serialNo;
		protected int _receiveFlag;
		protected int _statusNo;
		protected int _sampleTypeNo;
		protected string _patNo;
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
		protected string _diag;
		protected int _chargeNo;
		protected decimal _charge;
		protected string _chargeflag;
		protected string _countNodesFormSource;
		protected int _isCheckFee;
		protected string _operator;
		protected DateTime? _operDate;
		protected DateTime? _operTime;
		protected string _formMemo;
		protected string _requestSource;
		protected string _sickOrder;
		protected int _jztype;
		protected string _zdy1;
		protected string _zdy2;
		protected string _zdy3;
		protected string _zdy4;
		protected string _zdy5;
		protected DateTime? _flagDateDelete;
		protected int _nurseFlag;
		protected bool _isByHand;
		protected int _testTypeNo;
		protected int _execDeptNo;
		protected DateTime? _collectDate;
		protected DateTime? _collectTime;
		protected string _collecter;
		protected string _lABCENTER;
		protected string _checkNo;
		protected string _checkName;
		protected string _webLisSourceOrgID;
		protected string _webLisSourceOrgName;
		protected string _lABNREQUESTFORMNO;
		protected string _nFAgeUnitName;
		protected string _nFGenderName;
		protected string _nFDeptName;
		protected string _nFDistrictName;
		protected string _nFWardName;
		protected string _nFFolkName;
		protected string _nFSickTypeName;
		protected string _nFDoctorName;
		protected string _nFClientName;
		protected string _nFClientArea;
		protected string _nFClientStyle;
		protected string _nFClientType;
		protected string _nFbusinessname;
		protected string _nFtesttypename;
		protected string _nFsampletypename;
		protected string _lABCENTERNO;
		protected string _lABCENTERNAME;
		protected string _lABCLIENTNO;
		protected string _lABCLIENTNAME;
		protected string _lABDONO;
		protected string _lABDONAME;
		protected DateTime? _lABUPLOADDATE;
		protected int _areaSendFlag;
		protected DateTime? _areaSendTime;
		protected string _oldSerialNo;
		protected string _barCodeNo;
		protected string _weblisFlag;
		protected DateTime? _flagDateUpload;
		protected int _uploadFlag;
		protected string _loseInfo;
		protected string _ageUnit;
		protected string _personID;
		protected string _sampleType;
		protected string _telNo;
		protected string _webLisOrgID;
		protected string _webLisOrgName;
		protected string _sTATUSName;
		protected string _jztypeName;
		protected int _printTimes;
		protected string _barcode;
		protected string _combiItemName;
		protected double _price;
		protected string _urgentState;
		protected string _zdy9;
		protected string _area;
		protected string _zDY10;
		protected string _zDY6;
		protected string _zDY7;
		protected string _zDY8;


		#endregion

		#region Constructors

		public NRequestForm() { }

		public NRequestForm(string clientNo, string clientName, string ageUnitName, string genderName, string deptName, string districtName, string wardName, string folkName, string clinicTypeName, string testTypeName, string cName, string doctorName, string collecterName, string sampleTypeName, string serialNo, int receiveFlag, int statusNo, int sampleTypeNo, string patNo, int genderNo, DateTime birthday, double age, int ageUnitNo, int folkNo, int districtNo, int wardNo, string bed, int deptNo, int doctor, int diagNo, string diag, int chargeNo, decimal charge, string chargeflag, string countNodesFormSource, int isCheckFee, string Operator, DateTime operDate, DateTime operTime, string formMemo, string requestSource, string sickOrder, int jztype, string zdy1, string zdy2, string zdy3, string zdy4, string zdy5, DateTime flagDateDelete, int nurseFlag, bool isByHand, int testTypeNo, int execDeptNo, DateTime collectDate, DateTime collectTime, string collecter, string lABCENTER, string checkNo, string checkName, string webLisSourceOrgID, string webLisSourceOrgName, string lABNREQUESTFORMNO, string nFAgeUnitName, string nFGenderName, string nFDeptName, string nFDistrictName, string nFWardName, string nFFolkName, string nFSickTypeName, string nFDoctorName, string nFClientName, string nFClientArea, string nFClientStyle, string nFClientType, string nFbusinessname, string nFtesttypename, string nFsampletypename, string lABCENTERNO, string lABCENTERNAME, string lABCLIENTNO, string lABCLIENTNAME, string lABDONO, string lABDONAME, DateTime lABUPLOADDATE, int areaSendFlag, DateTime areaSendTime, string oldSerialNo, string barCodeNo, string weblisFlag, DateTime flagDateUpload, int uploadFlag, string loseInfo, string ageUnit, string personID, string sampleType, string telNo, string webLisOrgID, string webLisOrgName, string sTATUSName, string jztypeName, int printTimes, string barcode, string combiItemName, double price, string urgentState, string zdy9, string area, string zDY10, string zDY6, string zDY7, string zDY8)
		{
			this._clientNo = clientNo;
			this._clientName = clientName;
			this._ageUnitName = ageUnitName;
			this._genderName = genderName;
			this._deptName = deptName;
			this._districtName = districtName;
			this._wardName = wardName;
			this._folkName = folkName;
			this._clinicTypeName = clinicTypeName;
			this._testTypeName = testTypeName;
			this._cName = cName;
			this._doctorName = doctorName;
			this._collecterName = collecterName;
			this._sampleTypeName = sampleTypeName;
			this._serialNo = serialNo;
			this._receiveFlag = receiveFlag;
			this._statusNo = statusNo;
			this._sampleTypeNo = sampleTypeNo;
			this._patNo = patNo;
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
			this._diag = diag;
			this._chargeNo = chargeNo;
			this._charge = charge;
			this._chargeflag = chargeflag;
			this._countNodesFormSource = countNodesFormSource;
			this._isCheckFee = isCheckFee;
			this._operator = Operator;
			this._operDate = operDate;
			this._operTime = operTime;
			this._formMemo = formMemo;
			this._requestSource = requestSource;
			this._sickOrder = sickOrder;
			this._jztype = jztype;
			this._zdy1 = zdy1;
			this._zdy2 = zdy2;
			this._zdy3 = zdy3;
			this._zdy4 = zdy4;
			this._zdy5 = zdy5;
			this._flagDateDelete = flagDateDelete;
			this._nurseFlag = nurseFlag;
			this._isByHand = isByHand;
			this._testTypeNo = testTypeNo;
			this._execDeptNo = execDeptNo;
			this._collectDate = collectDate;
			this._collectTime = collectTime;
			this._collecter = collecter;
			this._lABCENTER = lABCENTER;
			this._checkNo = checkNo;
			this._checkName = checkName;
			this._webLisSourceOrgID = webLisSourceOrgID;
			this._webLisSourceOrgName = webLisSourceOrgName;
			this._lABNREQUESTFORMNO = lABNREQUESTFORMNO;
			this._nFAgeUnitName = nFAgeUnitName;
			this._nFGenderName = nFGenderName;
			this._nFDeptName = nFDeptName;
			this._nFDistrictName = nFDistrictName;
			this._nFWardName = nFWardName;
			this._nFFolkName = nFFolkName;
			this._nFSickTypeName = nFSickTypeName;
			this._nFDoctorName = nFDoctorName;
			this._nFClientName = nFClientName;
			this._nFClientArea = nFClientArea;
			this._nFClientStyle = nFClientStyle;
			this._nFClientType = nFClientType;
			this._nFbusinessname = nFbusinessname;
			this._nFtesttypename = nFtesttypename;
			this._nFsampletypename = nFsampletypename;
			this._lABCENTERNO = lABCENTERNO;
			this._lABCENTERNAME = lABCENTERNAME;
			this._lABCLIENTNO = lABCLIENTNO;
			this._lABCLIENTNAME = lABCLIENTNAME;
			this._lABDONO = lABDONO;
			this._lABDONAME = lABDONAME;
			this._lABUPLOADDATE = lABUPLOADDATE;
			this._areaSendFlag = areaSendFlag;
			this._areaSendTime = areaSendTime;
			this._oldSerialNo = oldSerialNo;
			this._barCodeNo = barCodeNo;
			this._weblisFlag = weblisFlag;
			this._flagDateUpload = flagDateUpload;
			this._uploadFlag = uploadFlag;
			this._loseInfo = loseInfo;
			this._ageUnit = ageUnit;
			this._personID = personID;
			this._sampleType = sampleType;
			this._telNo = telNo;
			this._webLisOrgID = webLisOrgID;
			this._webLisOrgName = webLisOrgName;
			this._sTATUSName = sTATUSName;
			this._jztypeName = jztypeName;
			this._printTimes = printTimes;
			this._barcode = barcode;
			this._combiItemName = combiItemName;
			this._price = price;
			this._urgentState = urgentState;
			this._zdy9 = zdy9;
			this._area = area;
			this._zDY10 = zDY10;
			this._zDY6 = zDY6;
			this._zDY7 = zDY7;
			this._zDY8 = zDY8;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "录入医疗机构编号", ShortCode = "ClientNo", Desc = "录入医疗机构编号", ContextType = SysDic.All, Length = 50)]
		public virtual string ClientNo
		{
			get { return _clientNo; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ClientNo", value, value.ToString());
				_clientNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "录入医疗机构名称", ShortCode = "ClientName", Desc = "录入医疗机构名称", ContextType = SysDic.All, Length = 300)]
		public virtual string ClientName
		{
			get { return _clientName; }
			set
			{
				if (value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ClientName", value, value.ToString());
				_clientName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "年龄单位", ShortCode = "AgeUnitName", Desc = "年龄单位", ContextType = SysDic.All, Length = 50)]
		public virtual string AgeUnitName
		{
			get { return _ageUnitName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AgeUnitName", value, value.ToString());
				_ageUnitName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "性别", ShortCode = "GenderName", Desc = "性别", ContextType = SysDic.All, Length = 50)]
		public virtual string GenderName
		{
			get { return _genderName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for GenderName", value, value.ToString());
				_genderName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "部门名称", ShortCode = "DeptName", Desc = "部门名称", ContextType = SysDic.All, Length = 50)]
		public virtual string DeptName
		{
			get { return _deptName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
				_deptName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "病区名称", ShortCode = "DistrictName", Desc = "病区名称", ContextType = SysDic.All, Length = 50)]
		public virtual string DistrictName
		{
			get { return _districtName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DistrictName", value, value.ToString());
				_districtName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "病房名称", ShortCode = "WardName", Desc = "病房名称", ContextType = SysDic.All, Length = 50)]
		public virtual string WardName
		{
			get { return _wardName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WardName", value, value.ToString());
				_wardName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "民族", ShortCode = "FolkName", Desc = "民族", ContextType = SysDic.All, Length = 50)]
		public virtual string FolkName
		{
			get { return _folkName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for FolkName", value, value.ToString());
				_folkName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "就诊类型名称", ShortCode = "ClinicTypeName", Desc = "就诊类型名称", ContextType = SysDic.All, Length = 50)]
		public virtual string ClinicTypeName
		{
			get { return _clinicTypeName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ClinicTypeName", value, value.ToString());
				_clinicTypeName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "TestTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string TestTypeName
		{
			get { return _testTypeName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TestTypeName", value, value.ToString());
				_testTypeName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "姓名", ShortCode = "CName", Desc = "姓名", ContextType = SysDic.All, Length = 50)]
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
		[DataDesc(CName = "医生", ShortCode = "DoctorName", Desc = "医生", ContextType = SysDic.All, Length = 500)]
		public virtual string DoctorName
		{
			get { return _doctorName; }
			set
			{
				if (value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for DoctorName", value, value.ToString());
				_doctorName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "采样人姓名", ShortCode = "CollecterName", Desc = "采样人姓名", ContextType = SysDic.All, Length = 500)]
		public virtual string CollecterName
		{
			get { return _collecterName; }
			set
			{
				if (value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for CollecterName", value, value.ToString());
				_collecterName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string SampleTypeName
		{
			get { return _sampleTypeName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleTypeName", value, value.ToString());
				_sampleTypeName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "检验单号", ShortCode = "SerialNo", Desc = "检验单号", ContextType = SysDic.All, Length = 100)]
		public virtual string SerialNo
		{
			get { return _serialNo; }
			set
			{
				if (value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SerialNo", value, value.ToString());
				_serialNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "核收标志", ShortCode = "ReceiveFlag", Desc = "核收标志", ContextType = SysDic.All, Length = 4)]
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
		[DataDesc(CName = "样本类型", ShortCode = "SampleTypeNo", Desc = "样本类型", ContextType = SysDic.All, Length = 4)]
		public virtual int SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set { _sampleTypeNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "病历号", ShortCode = "PatNo", Desc = "病历号", ContextType = SysDic.All, Length = 50)]
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
		[DataDesc(CName = "性别编号", ShortCode = "GenderNo", Desc = "性别编号", ContextType = SysDic.All, Length = 4)]
		public virtual int GenderNo
		{
			get { return _genderNo; }
			set { _genderNo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "生日", ShortCode = "Birthday", Desc = "生日", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄", ShortCode = "Age", Desc = "年龄", ContextType = SysDic.All, Length = 8)]
		public virtual double Age
		{
			get { return _age; }
			set { _age = value; }
		}

		[DataMember]
		[DataDesc(CName = "年龄单位编号", ShortCode = "AgeUnitNo", Desc = "年龄单位编号", ContextType = SysDic.All, Length = 4)]
		public virtual int AgeUnitNo
		{
			get { return _ageUnitNo; }
			set { _ageUnitNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "民族编号", ShortCode = "FolkNo", Desc = "民族编号", ContextType = SysDic.All, Length = 4)]
		public virtual int FolkNo
		{
			get { return _folkNo; }
			set { _folkNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "病区编号", ShortCode = "DistrictNo", Desc = "病区编号", ContextType = SysDic.All, Length = 4)]
		public virtual int DistrictNo
		{
			get { return _districtNo; }
			set { _districtNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "病房编号", ShortCode = "WardNo", Desc = "病房编号", ContextType = SysDic.All, Length = 4)]
		public virtual int WardNo
		{
			get { return _wardNo; }
			set { _wardNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "病床", ShortCode = "Bed", Desc = "病床", ContextType = SysDic.All, Length = 50)]
		public virtual string Bed
		{
			get { return _bed; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Bed", value, value.ToString());
				_bed = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "科室编号", ShortCode = "DeptNo", Desc = "科室编号", ContextType = SysDic.All, Length = 4)]
		public virtual int DeptNo
		{
			get { return _deptNo; }
			set { _deptNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "医生编号", ShortCode = "Doctor", Desc = "医生编号", ContextType = SysDic.All, Length = 4)]
		public virtual int Doctor
		{
			get { return _doctor; }
			set { _doctor = value; }
		}

		[DataMember]
		[DataDesc(CName = "诊断编号", ShortCode = "DiagNo", Desc = "诊断编号", ContextType = SysDic.All, Length = 4)]
		public virtual int DiagNo
		{
			get { return _diagNo; }
			set { _diagNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "诊断描述", ShortCode = "Diag", Desc = "诊断描述", ContextType = SysDic.All, Length = 100)]
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
		[DataDesc(CName = "收费类型", ShortCode = "ChargeNo", Desc = "收费类型", ContextType = SysDic.All, Length = 4)]
		public virtual int ChargeNo
		{
			get { return _chargeNo; }
			set { _chargeNo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "费用", ShortCode = "Charge", Desc = "费用", ContextType = SysDic.All, Length = 8)]
		public virtual decimal Charge
		{
			get { return _charge; }
			set { _charge = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Chargeflag", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string Chargeflag
		{
			get { return _chargeflag; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Chargeflag", value, value.ToString());
				_chargeflag = value;
			}
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
		[DataDesc(CName = "计费状态", ShortCode = "IsCheckFee", Desc = "计费状态", ContextType = SysDic.All, Length = 4)]
		public virtual int IsCheckFee
		{
			get { return _isCheckFee; }
			set { _isCheckFee = value; }
		}

		[DataMember]
		[DataDesc(CName = "开单者", ShortCode = "Operator", Desc = "开单者", ContextType = SysDic.All, Length = 50)]
		public virtual string Operator
		{
			get { return _operator; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Operator", value, value.ToString());
				_operator = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "申请日期", ShortCode = "OperDate", Desc = "申请日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? OperDate
		{
			get { return _operDate; }
			set { _operDate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "申请时间", ShortCode = "OperTime", Desc = "申请时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? OperTime
		{
			get { return _operTime; }
			set { _operTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "FormMemo", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string FormMemo
		{
			get { return _formMemo; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for FormMemo", value, value.ToString());
				_formMemo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "RequestSource", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string RequestSource
		{
			get { return _requestSource; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RequestSource", value, value.ToString());
				_requestSource = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SickOrder", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string SickOrder
		{
			get { return _sickOrder; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SickOrder", value, value.ToString());
				_sickOrder = value;
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
		[DataDesc(CName = "自定义1", ShortCode = "Zdy1", Desc = "自定义1", ContextType = SysDic.All, Length = 50)]
		public virtual string Zdy1
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
		[DataDesc(CName = "自定义2", ShortCode = "Zdy2", Desc = "自定义2", ContextType = SysDic.All, Length = 50)]
		public virtual string Zdy2
		{
			get { return _zdy2; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy2", value, value.ToString());
				_zdy2 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "自定义3", ShortCode = "Zdy3", Desc = "自定义3", ContextType = SysDic.All, Length = 50)]
		public virtual string Zdy3
		{
			get { return _zdy3; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy3", value, value.ToString());
				_zdy3 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "自定义4", ShortCode = "Zdy4", Desc = "自定义4", ContextType = SysDic.All, Length = 50)]
		public virtual string Zdy4
		{
			get { return _zdy4; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy4", value, value.ToString());
				_zdy4 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "自定义5", ShortCode = "Zdy5", Desc = "自定义5", ContextType = SysDic.All, Length = 50)]
		public virtual string Zdy5
		{
			get { return _zdy5; }
			set
			{
				if (value != null && value.Length > 50)
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
		[DataDesc(CName = "条码处理标记", ShortCode = "NurseFlag", Desc = "条码处理标记", ContextType = SysDic.All, Length = 4)]
		public virtual int NurseFlag
		{
			get { return _nurseFlag; }
			set { _nurseFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsByHand", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsByHand
		{
			get { return _isByHand; }
			set { _isByHand = value; }
		}

		[DataMember]
		[DataDesc(CName = "检测类型", ShortCode = "TestTypeNo", Desc = "检测类型", ContextType = SysDic.All, Length = 4)]
		public virtual int TestTypeNo
		{
			get { return _testTypeNo; }
			set { _testTypeNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ExecDeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ExecDeptNo
		{
			get { return _execDeptNo; }
			set { _execDeptNo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样日期", ShortCode = "CollectDate", Desc = "采样日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectDate
		{
			get { return _collectDate; }
			set { _collectDate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "CollectTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectTime
		{
			get { return _collectTime; }
			set { _collectTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "采样人", ShortCode = "Collecter", Desc = "采样人", ContextType = SysDic.All, Length = 300)]
		public virtual string Collecter
		{
			get { return _collecter; }
			set
			{
				if (value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Collecter", value, value.ToString());
				_collecter = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "独立实验室名称", ShortCode = "LABCENTER", Desc = "独立实验室名称", ContextType = SysDic.All, Length = 50)]
		public virtual string LABCENTER
		{
			get { return _lABCENTER; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABCENTER", value, value.ToString());
				_lABCENTER = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "检测单位编号", ShortCode = "CheckNo", Desc = "检测单位编号", ContextType = SysDic.All, Length = 50)]
		public virtual string CheckNo
		{
			get { return _checkNo; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CheckNo", value, value.ToString());
				_checkNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "检测单位", ShortCode = "CheckName", Desc = "检测单位", ContextType = SysDic.All, Length = 500)]
		public virtual string CheckName
		{
			get { return _checkName; }
			set
			{
				if (value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for CheckName", value, value.ToString());
				_checkName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "被录入送检单位编号", ShortCode = "WebLisSourceOrgID", Desc = "被录入送检单位编号", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLisSourceOrgID
		{
			get { return _webLisSourceOrgID; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgID", value, value.ToString());
				_webLisSourceOrgID = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "被录入送检单位名称", ShortCode = "WebLisSourceOrgName", Desc = "被录入送检单位名称", ContextType = SysDic.All, Length = 300)]
		public virtual string WebLisSourceOrgName
		{
			get { return _webLisSourceOrgName; }
			set
			{
				if (value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgName", value, value.ToString());
				_webLisSourceOrgName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "编号", ShortCode = "LABNREQUESTFORMNO", Desc = "编号", ContextType = SysDic.All, Length = 50)]
		public virtual string LABNREQUESTFORMNO
		{
			get { return _lABNREQUESTFORMNO; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABNREQUESTFORMNO", value, value.ToString());
				_lABNREQUESTFORMNO = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "年龄单位", ShortCode = "NFAgeUnitName", Desc = "年龄单位", ContextType = SysDic.All, Length = 50)]
		public virtual string NFAgeUnitName
		{
			get { return _nFAgeUnitName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFAgeUnitName", value, value.ToString());
				_nFAgeUnitName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "性别名称", ShortCode = "NFGenderName", Desc = "性别名称", ContextType = SysDic.All, Length = 50)]
		public virtual string NFGenderName
		{
			get { return _nFGenderName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFGenderName", value, value.ToString());
				_nFGenderName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "科室名称", ShortCode = "NFDeptName", Desc = "科室名称", ContextType = SysDic.All, Length = 50)]
		public virtual string NFDeptName
		{
			get { return _nFDeptName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFDeptName", value, value.ToString());
				_nFDeptName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "病区名称", ShortCode = "NFDistrictName", Desc = "病区名称", ContextType = SysDic.All, Length = 50)]
		public virtual string NFDistrictName
		{
			get { return _nFDistrictName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFDistrictName", value, value.ToString());
				_nFDistrictName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "病房名称", ShortCode = "NFWardName", Desc = "病房名称", ContextType = SysDic.All, Length = 50)]
		public virtual string NFWardName
		{
			get { return _nFWardName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFWardName", value, value.ToString());
				_nFWardName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "民族名称", ShortCode = "NFFolkName", Desc = "民族名称", ContextType = SysDic.All, Length = 50)]
		public virtual string NFFolkName
		{
			get { return _nFFolkName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFFolkName", value, value.ToString());
				_nFFolkName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "就诊类型名称", ShortCode = "NFSickTypeName", Desc = "就诊类型名称", ContextType = SysDic.All, Length = 50)]
		public virtual string NFSickTypeName
		{
			get { return _nFSickTypeName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFSickTypeName", value, value.ToString());
				_nFSickTypeName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "医生名称", ShortCode = "NFDoctorName", Desc = "医生名称", ContextType = SysDic.All, Length = 50)]
		public virtual string NFDoctorName
		{
			get { return _nFDoctorName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFDoctorName", value, value.ToString());
				_nFDoctorName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "送检单位名称", ShortCode = "NFClientName", Desc = "送检单位名称", ContextType = SysDic.All, Length = 50)]
		public virtual string NFClientName
		{
			get { return _nFClientName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFClientName", value, value.ToString());
				_nFClientName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "办事处", ShortCode = "NFClientArea", Desc = "办事处", ContextType = SysDic.All, Length = 50)]
		public virtual string NFClientArea
		{
			get { return _nFClientArea; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFClientArea", value, value.ToString());
				_nFClientArea = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "医院级别", ShortCode = "NFClientStyle", Desc = "医院级别", ContextType = SysDic.All, Length = 50)]
		public virtual string NFClientStyle
		{
			get { return _nFClientStyle; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFClientStyle", value, value.ToString());
				_nFClientStyle = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "客户类型", ShortCode = "NFClientType", Desc = "客户类型", ContextType = SysDic.All, Length = 50)]
		public virtual string NFClientType
		{
			get { return _nFClientType; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFClientType", value, value.ToString());
				_nFClientType = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "业务员", ShortCode = "NFbusinessname", Desc = "业务员", ContextType = SysDic.All, Length = 50)]
		public virtual string NFbusinessname
		{
			get { return _nFbusinessname; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFbusinessname", value, value.ToString());
				_nFbusinessname = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "检测类型", ShortCode = "NFtesttypename", Desc = "检测类型", ContextType = SysDic.All, Length = 50)]
		public virtual string NFtesttypename
		{
			get { return _nFtesttypename; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFtesttypename", value, value.ToString());
				_nFtesttypename = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "样本类型", ShortCode = "NFsampletypename", Desc = "样本类型", ContextType = SysDic.All, Length = 50)]
		public virtual string NFsampletypename
		{
			get { return _nFsampletypename; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NFsampletypename", value, value.ToString());
				_nFsampletypename = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "中心编号", ShortCode = "LABCENTERNO", Desc = "中心编号", ContextType = SysDic.All, Length = 50)]
		public virtual string LABCENTERNO
		{
			get { return _lABCENTERNO; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABCENTERNO", value, value.ToString());
				_lABCENTERNO = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "中心名称", ShortCode = "LABCENTERNAME", Desc = "中心名称", ContextType = SysDic.All, Length = 50)]
		public virtual string LABCENTERNAME
		{
			get { return _lABCENTERNAME; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABCENTERNAME", value, value.ToString());
				_lABCENTERNAME = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "客户编号", ShortCode = "LABCLIENTNO", Desc = "客户编号", ContextType = SysDic.All, Length = 50)]
		public virtual string LABCLIENTNO
		{
			get { return _lABCLIENTNO; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABCLIENTNO", value, value.ToString());
				_lABCLIENTNO = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "客户名称", ShortCode = "LABCLIENTNAME", Desc = "客户名称", ContextType = SysDic.All, Length = 50)]
		public virtual string LABCLIENTNAME
		{
			get { return _lABCLIENTNAME; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABCLIENTNAME", value, value.ToString());
				_lABCLIENTNAME = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "检测单位编号", ShortCode = "LABDONO", Desc = "检测单位编号", ContextType = SysDic.All, Length = 50)]
		public virtual string LABDONO
		{
			get { return _lABDONO; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABDONO", value, value.ToString());
				_lABDONO = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "检测单位名称", ShortCode = "LABDONAME", Desc = "检测单位名称", ContextType = SysDic.All, Length = 50)]
		public virtual string LABDONAME
		{
			get { return _lABDONAME; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABDONAME", value, value.ToString());
				_lABDONAME = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "上传时间", ShortCode = "LABUPLOADDATE", Desc = "上传时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? LABUPLOADDATE
		{
			get { return _lABUPLOADDATE; }
			set { _lABUPLOADDATE = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "AreaSendFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int AreaSendFlag
		{
			get { return _areaSendFlag; }
			set { _areaSendFlag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "AreaSendTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AreaSendTime
		{
			get { return _areaSendTime; }
			set { _areaSendTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "OldSerialNo", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string OldSerialNo
		{
			get { return _oldSerialNo; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OldSerialNo", value, value.ToString());
				_oldSerialNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BarCodeNo", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string BarCodeNo
		{
			get { return _barCodeNo; }
			set
			{				
				_barCodeNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "WeblisFlag", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string WeblisFlag
		{
			get { return _weblisFlag; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WeblisFlag", value, value.ToString());
				_weblisFlag = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "FlagDateUpload", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? FlagDateUpload
		{
			get { return _flagDateUpload; }
			set { _flagDateUpload = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "UploadFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int UploadFlag
		{
			get { return _uploadFlag; }
			set { _uploadFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "LoseInfo", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string LoseInfo
		{
			get { return _loseInfo; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LoseInfo", value, value.ToString());
				_loseInfo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "AgeUnit", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string AgeUnit
		{
			get { return _ageUnit; }
			set
			{
				
				_ageUnit = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PersonID", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string PersonID
		{
			get { return _personID; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PersonID", value, value.ToString());
				_personID = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleType", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string SampleType
		{
			get { return _sampleType; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleType", value, value.ToString());
				_sampleType = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "TelNo", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string TelNo
		{
			get { return _telNo; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TelNo", value, value.ToString());
				_telNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "WebLisOrgID", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLisOrgID
		{
			get { return _webLisOrgID; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisOrgID", value, value.ToString());
				_webLisOrgID = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "WebLisOrgName", Desc = "", ContextType = SysDic.All, Length = 300)]
		public virtual string WebLisOrgName
		{
			get { return _webLisOrgName; }
			set
			{
				if (value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisOrgName", value, value.ToString());
				_webLisOrgName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "STATUSName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string STATUSName
		{
			get { return _sTATUSName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for STATUSName", value, value.ToString());
				_sTATUSName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "JztypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string JztypeName
		{
			get { return _jztypeName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for JztypeName", value, value.ToString());
				_jztypeName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PrintTimes", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int PrintTimes
		{
			get { return _printTimes; }
			set { _printTimes = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Barcode", Desc = "", ContextType = SysDic.All, Length = 200)]
		public virtual string Barcode
		{
			get { return _barcode; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Barcode", value, value.ToString());
				_barcode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CombiItemName", Desc = "", ContextType = SysDic.All, Length = 200)]
		public virtual string CombiItemName
		{
			get { return _combiItemName; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for CombiItemName", value, value.ToString());
				_combiItemName = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual double Price
		{
			get { return _price; }
			set { _price = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "UrgentState", Desc = "", ContextType = SysDic.All, Length = 30)]
		public virtual string UrgentState
		{
			get { return _urgentState; }
			set
			{
				if (value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for UrgentState", value, value.ToString());
				_urgentState = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Zdy9", Desc = "", ContextType = SysDic.All, Length = 60)]
		public virtual string Zdy9
		{
			get { return _zdy9; }
			set
			{
				if (value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy9", value, value.ToString());
				_zdy9 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Area", Desc = "", ContextType = SysDic.All, Length = 30)]
		public virtual string Area
		{
			get { return _area; }
			set
			{
				if (value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for Area", value, value.ToString());
				_area = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ZDY10", Desc = "", ContextType = SysDic.All, Length = 60)]
		public virtual string ZDY10
		{
			get { return _zDY10; }
			set
			{		
				_zDY10 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ZDY6", Desc = "", ContextType = SysDic.All, Length = 60)]
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
		[DataDesc(CName = "", ShortCode = "ZDY7", Desc = "", ContextType = SysDic.All, Length = 60)]
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
		[DataDesc(CName = "", ShortCode = "ZDY8", Desc = "", ContextType = SysDic.All, Length = 60)]
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


		#endregion
	}
	#endregion
}