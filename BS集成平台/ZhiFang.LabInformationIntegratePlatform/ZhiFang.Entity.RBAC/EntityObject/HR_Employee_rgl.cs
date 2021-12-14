using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region HREmployee

	/// <summary>
	/// HREmployee object for NHibernate mapped table 'HR_Employee'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "员工", ClassCName = "HREmployee", ShortCode = "HREmployee", Desc = "员工")]
	public class HREmployee_rgl : BaseEntity
	{
		#region Member Variables

		protected long? _positionID;
		protected long? _deptID;
		protected string _useCode;
		protected string _standCode;
		protected string _deveCode;
		protected string _nameL;
		protected string _nameF;
		protected string _cName;
		protected string _eName;
		protected string _sName;
		protected string _shortcode;
		protected string _pinYinZiTou;
		protected string _comment;
		protected bool _isUse;
		protected int _dispOrder;
		protected long? _sexID;
		protected DateTime? _birthday;
		protected string _email;
		protected string _mobileTel;
		protected double _officeTel;
		protected double _extTel;
		protected double _homeTel;
		protected string _tel;
		protected string _address;
		protected long? _cityID;
		protected long? _provinceID;
		protected long? _countryID;
		protected string _zipCode;
		protected long? _maritalStatusID;
		protected long? _educationLevelID;
		protected int _isEnabled;
		protected string _picFile;
		protected string _idNumber;
		protected long? _degreeID;
		protected string _graduateSchool;
		protected long? _professionalAbilityID;
		protected string _eduBackground;
		protected long? _healthStatusID;
		protected long? _politicsStatusID;
		protected string _training;
		protected string _personalHomePage;
		protected string _jobResume;
		protected string _family;
		protected DateTime? _entryTime;
		protected string _continuingEducation;
		protected string _wageChange;
		protected string _laborContract;
		protected long? _nationalityID;
		protected string _professionalQualifications;
		protected string _awardandCertificates;
		protected string _jobDuty;
		protected DateTime? _dataUpdateTime;
		protected string _signatureImage;
		protected long _managerID;
		protected string _managerName;


		#endregion

		#region Constructors

		public HREmployee_rgl() { }

		public HREmployee_rgl(long labID, long positionID, long deptID, string useCode, string standCode, string deveCode, string nameL, string nameF, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, long sexID, DateTime birthday, string email, string mobileTel, double officeTel, double extTel, double homeTel, string tel, string address, long cityID, long provinceID, long countryID, string zipCode, long maritalStatusID, long educationLevelID, int isEnabled, string picFile, string idNumber, long degreeID, string graduateSchool, long professionalAbilityID, string eduBackground, long healthStatusID, long politicsStatusID, string training, string personalHomePage, string jobResume, string family, DateTime entryTime, string continuingEducation, string wageChange, string laborContract, long nationalityID, string professionalQualifications, string awardandCertificates, string jobDuty, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string signatureImage, long managerID, string managerName)
		{
			this._labID = labID;
			this._positionID = positionID;
			this._deptID = deptID;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._nameL = nameL;
			this._nameF = nameF;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._sexID = sexID;
			this._birthday = birthday;
			this._email = email;
			this._mobileTel = mobileTel;
			this._officeTel = officeTel;
			this._extTel = extTel;
			this._homeTel = homeTel;
			this._tel = tel;
			this._address = address;
			this._cityID = cityID;
			this._provinceID = provinceID;
			this._countryID = countryID;
			this._zipCode = zipCode;
			this._maritalStatusID = maritalStatusID;
			this._educationLevelID = educationLevelID;
			this._isEnabled = isEnabled;
			this._picFile = picFile;
			this._idNumber = idNumber;
			this._degreeID = degreeID;
			this._graduateSchool = graduateSchool;
			this._professionalAbilityID = professionalAbilityID;
			this._eduBackground = eduBackground;
			this._healthStatusID = healthStatusID;
			this._politicsStatusID = politicsStatusID;
			this._training = training;
			this._personalHomePage = personalHomePage;
			this._jobResume = jobResume;
			this._family = family;
			this._entryTime = entryTime;
			this._continuingEducation = continuingEducation;
			this._wageChange = wageChange;
			this._laborContract = laborContract;
			this._nationalityID = nationalityID;
			this._professionalQualifications = professionalQualifications;
			this._awardandCertificates = awardandCertificates;
			this._jobDuty = jobDuty;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._signatureImage = signatureImage;
			this._managerID = managerID;
			this._managerName = managerName;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "职位ID", ShortCode = "PositionID", Desc = "职位ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? PositionID
		{
			get { return _positionID; }
			set { _positionID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "部门ID", ShortCode = "DeptID", Desc = "部门ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

		[DataMember]
		[DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
		public virtual string UseCode
		{
			get { return _useCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
				_useCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
		public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
				_standCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "开发商标准代码", ShortCode = "DeveCode", Desc = "开发商标准代码", ContextType = SysDic.All, Length = 50)]
		public virtual string DeveCode
		{
			get { return _deveCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
				_deveCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "姓", ShortCode = "NameL", Desc = "姓", ContextType = SysDic.All, Length = 30)]
		public virtual string NameL
		{
			get { return _nameL; }
			set
			{
				if (value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for NameL", value, value.ToString());
				_nameL = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "名", ShortCode = "NameF", Desc = "名", ContextType = SysDic.All, Length = 30)]
		public virtual string NameF
		{
			get { return _nameF; }
			set
			{
				if (value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for NameF", value, value.ToString());
				_nameF = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
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
		[DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
		public virtual string EName
		{
			get { return _eName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
		public virtual string SName
		{
			get { return _sName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
		public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
		public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
		public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if (value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		[DataMember]
		[DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "性别", ShortCode = "SexID", Desc = "性别", ContextType = SysDic.All, Length = 8)]
		public virtual long? SexID
		{
			get { return _sexID; }
			set { _sexID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "出生日期", ShortCode = "Birthday", Desc = "出生日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

		[DataMember]
		[DataDesc(CName = "Email", ShortCode = "Email", Desc = "Email", ContextType = SysDic.All, Length = 50)]
		public virtual string Email
		{
			get { return _email; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Email", value, value.ToString());
				_email = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "手机号码", ShortCode = "MobileTel", Desc = "手机号码", ContextType = SysDic.All, Length = 50)]
		public virtual string MobileTel
		{
			get { return _mobileTel; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MobileTel", value, value.ToString());
				_mobileTel = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "办公电话", ShortCode = "OfficeTel", Desc = "办公电话", ContextType = SysDic.All, Length = 8)]
		public virtual double OfficeTel
		{
			get { return _officeTel; }
			set { _officeTel = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "分机", ShortCode = "ExtTel", Desc = "分机", ContextType = SysDic.All, Length = 8)]
		public virtual double ExtTel
		{
			get { return _extTel; }
			set { _extTel = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "家庭电话", ShortCode = "HomeTel", Desc = "家庭电话", ContextType = SysDic.All, Length = 8)]
		public virtual double HomeTel
		{
			get { return _homeTel; }
			set { _homeTel = value; }
		}

		[DataMember]
		[DataDesc(CName = "其他电话号码", ShortCode = "Tel", Desc = "其他电话号码", ContextType = SysDic.All, Length = 50)]
		public virtual string Tel
		{
			get { return _tel; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Tel", value, value.ToString());
				_tel = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "联系地址", ShortCode = "Address", Desc = "联系地址", ContextType = SysDic.All, Length = 250)]
		public virtual string Address
		{
			get { return _address; }
			set
			{
				if (value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
				_address = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "城市", ShortCode = "CityID", Desc = "城市", ContextType = SysDic.All, Length = 8)]
		public virtual long? CityID
		{
			get { return _cityID; }
			set { _cityID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "省份", ShortCode = "ProvinceID", Desc = "省份", ContextType = SysDic.All, Length = 8)]
		public virtual long? ProvinceID
		{
			get { return _provinceID; }
			set { _provinceID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "国家", ShortCode = "CountryID", Desc = "国家", ContextType = SysDic.All, Length = 8)]
		public virtual long? CountryID
		{
			get { return _countryID; }
			set { _countryID = value; }
		}

		[DataMember]
		[DataDesc(CName = "邮编", ShortCode = "ZipCode", Desc = "邮编", ContextType = SysDic.All, Length = 50)]
		public virtual string ZipCode
		{
			get { return _zipCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZipCode", value, value.ToString());
				_zipCode = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "婚姻状况", ShortCode = "MaritalStatusID", Desc = "婚姻状况", ContextType = SysDic.All, Length = 8)]
		public virtual long? MaritalStatusID
		{
			get { return _maritalStatusID; }
			set { _maritalStatusID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "学历", ShortCode = "EducationLevelID", Desc = "学历", ContextType = SysDic.All, Length = 8)]
		public virtual long? EducationLevelID
		{
			get { return _educationLevelID; }
			set { _educationLevelID = value; }
		}

		[DataMember]
		[DataDesc(CName = "在职", ShortCode = "IsEnabled", Desc = "在职", ContextType = SysDic.All, Length = 4)]
		public virtual int IsEnabled
		{
			get { return _isEnabled; }
			set { _isEnabled = value; }
		}

		[DataMember]
		[DataDesc(CName = "照片", ShortCode = "PicFile", Desc = "照片", ContextType = SysDic.All, Length = 200)]
		public virtual string PicFile
		{
			get { return _picFile; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PicFile", value, value.ToString());
				_picFile = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "身份证号", ShortCode = "IdNumber", Desc = "身份证号", ContextType = SysDic.All, Length = 50)]
		public virtual string IdNumber
		{
			get { return _idNumber; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for IdNumber", value, value.ToString());
				_idNumber = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "学位", ShortCode = "DegreeID", Desc = "学位", ContextType = SysDic.All, Length = 8)]
		public virtual long? DegreeID
		{
			get { return _degreeID; }
			set { _degreeID = value; }
		}

		[DataMember]
		[DataDesc(CName = "毕业院校", ShortCode = "GraduateSchool", Desc = "毕业院校", ContextType = SysDic.All, Length = 200)]
		public virtual string GraduateSchool
		{
			get { return _graduateSchool; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for GraduateSchool", value, value.ToString());
				_graduateSchool = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "专业能力", ShortCode = "ProfessionalAbilityID", Desc = "专业能力", ContextType = SysDic.All, Length = 8)]
		public virtual long? ProfessionalAbilityID
		{
			get { return _professionalAbilityID; }
			set { _professionalAbilityID = value; }
		}

		[DataMember]
		[DataDesc(CName = "教育背景", ShortCode = "EduBackground", Desc = "教育背景", ContextType = SysDic.All, Length = 200)]
		public virtual string EduBackground
		{
			get { return _eduBackground; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for EduBackground", value, value.ToString());
				_eduBackground = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "健康状况", ShortCode = "HealthStatusID", Desc = "健康状况", ContextType = SysDic.All, Length = 8)]
		public virtual long? HealthStatusID
		{
			get { return _healthStatusID; }
			set { _healthStatusID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "政治面貌", ShortCode = "PoliticsStatusID", Desc = "政治面貌", ContextType = SysDic.All, Length = 8)]
		public virtual long? PoliticsStatusID
		{
			get { return _politicsStatusID; }
			set { _politicsStatusID = value; }
		}

		[DataMember]
		[DataDesc(CName = "培训情况", ShortCode = "Training", Desc = "培训情况", ContextType = SysDic.All, Length = 200)]
		public virtual string Training
		{
			get { return _training; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Training", value, value.ToString());
				_training = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "个人主页", ShortCode = "PersonalHomePage", Desc = "个人主页", ContextType = SysDic.All, Length = 200)]
		public virtual string PersonalHomePage
		{
			get { return _personalHomePage; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PersonalHomePage", value, value.ToString());
				_personalHomePage = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "工作简历", ShortCode = "JobResume", Desc = "工作简历", ContextType = SysDic.All, Length = 200)]
		public virtual string JobResume
		{
			get { return _jobResume; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for JobResume", value, value.ToString());
				_jobResume = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "家庭情况", ShortCode = "Family", Desc = "家庭情况", ContextType = SysDic.All, Length = 200)]
		public virtual string Family
		{
			get { return _family; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Family", value, value.ToString());
				_family = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "入职时间", ShortCode = "EntryTime", Desc = "入职时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? EntryTime
		{
			get { return _entryTime; }
			set { _entryTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "继续教育情况", ShortCode = "ContinuingEducation", Desc = "继续教育情况", ContextType = SysDic.All, Length = 200)]
		public virtual string ContinuingEducation
		{
			get { return _continuingEducation; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ContinuingEducation", value, value.ToString());
				_continuingEducation = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "工资变化", ShortCode = "WageChange", Desc = "工资变化", ContextType = SysDic.All, Length = 200)]
		public virtual string WageChange
		{
			get { return _wageChange; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for WageChange", value, value.ToString());
				_wageChange = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "劳动合同", ShortCode = "LaborContract", Desc = "劳动合同", ContextType = SysDic.All, Length = 200)]
		public virtual string LaborContract
		{
			get { return _laborContract; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for LaborContract", value, value.ToString());
				_laborContract = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "民族", ShortCode = "NationalityID", Desc = "民族", ContextType = SysDic.All, Length = 8)]
		public virtual long? NationalityID
		{
			get { return _nationalityID; }
			set { _nationalityID = value; }
		}

		[DataMember]
		[DataDesc(CName = "专业资格", ShortCode = "ProfessionalQualifications", Desc = "专业资格", ContextType = SysDic.All, Length = 200)]
		public virtual string ProfessionalQualifications
		{
			get { return _professionalQualifications; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ProfessionalQualifications", value, value.ToString());
				_professionalQualifications = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "获奖简历及证书", ShortCode = "AwardandCertificates", Desc = "获奖简历及证书", ContextType = SysDic.All, Length = 200)]
		public virtual string AwardandCertificates
		{
			get { return _awardandCertificates; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for AwardandCertificates", value, value.ToString());
				_awardandCertificates = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "工作职责", ShortCode = "JobDuty", Desc = "工作职责", ContextType = SysDic.All, Length = 200)]
		public virtual string JobDuty
		{
			get { return _jobDuty; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for JobDuty", value, value.ToString());
				_jobDuty = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SignatureImage", Desc = "", ContextType = SysDic.All, Length = 16)]
		public virtual string SignatureImage
		{
			get { return _signatureImage; }
			set
			{
				if (value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for SignatureImage", value, value.ToString());
				_signatureImage = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ManagerID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long ManagerID
		{
			get { return _managerID; }
			set { _managerID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ManagerName", Desc = "", ContextType = SysDic.All, Length = 200)]
		public virtual string ManagerName
		{
			get { return _managerName; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ManagerName", value, value.ToString());
				_managerName = value;
			}
		}


		#endregion
	}
	#endregion
}