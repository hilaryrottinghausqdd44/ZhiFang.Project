using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisPatient

	/// <summary>
	/// 患者就诊信息,LisPatient object for NHibernate mapped table 'Lis_Patient'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "患者就诊信息", ClassCName = "LisPatient", ShortCode = "LisPatient", Desc = "患者就诊信息")]
	public class LisPatient : BaseEntity
	{
		#region Member Variables

		protected DateTime? _partitionDate;
		protected bool _bConfirm;
		protected string _cName;
		protected int? _genderID;
		protected string _genderName;
		protected double? _age;
		protected long? _ageUnitID;
		protected string _ageUnitName;
		protected DateTime? _birthday;
		protected string _ageDesc;
		protected int? _patHeight;
		protected double? _patWeight;
		protected long? _folkID;
		protected string _folkName;
		protected string _patAddress;
		protected byte[] _patPhoto;
		protected string _phoneCode;
		protected string _weChatNo;
		protected string _eMailAddress;
		protected string _patType;
		protected long? _sickTypeID;
		protected string _sickType;
		protected long? _diagID;
		protected string _diagName;
		protected long? _doctorID;
		protected string _doctorName;
		protected string _doctorTell;
		protected long? _execDeptID;
		protected long? _deptID;
		protected string _deptName;
		protected int? _visitTimes;
		protected DateTime? _visitDate;
		protected long? _districtID;
		protected string _districtName;
		protected long? _wardID;
		protected string _wardName;
		protected string _bed;
		protected long? _phyPeriodID;
		protected string _phyPeriodName;
		protected long? _collectPartID;
		protected string _collectPartName;
		protected string _hISComment;
		protected string _patComment;
		protected string _iDCardNo;
		protected string _hisPatNo;
		protected string _patNo;
		protected string _patCardNo;
		protected string _inPatNo;
		protected string _examNo;
		protected string _medicareNo;
		protected string _unionPayNo;
		protected string _healthCardNo;
		protected string _powerCardNo;
		protected string _inOutSerialNo;
		protected string _invoiceNo;
		protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public LisPatient() { }

		public LisPatient(long labID, DateTime partitionDate, bool bConfirm, string cName, int genderID, string genderName, double age, long ageUnitID, string ageUnitName, DateTime birthday, string ageDesc, int patHeight, double patWeight, long folkID, string folkName, string patAddress, byte[] patPhoto, string phoneCode, string weChatNo, string eMailAddress, string patType, long sickTypeID, string sickType, long diagID, string diagName, long doctorID, string doctorName, string doctorTell, long execDeptID, long deptID, string deptName, int visitTimes, DateTime visitDate, long districtID, string districtName, long wardID, string wardName, string bed, long phyPeriodID, string phyPeriodName, long collectPartID, string collectPartName, string hISComment, string patComment, string iDCardNo, string hisPatNo, string patNo, string patCardNo, string inPatNo, string examNo, string medicareNo, string unionPayNo, string healthCardNo, string powerCardNo, string inOutSerialNo, string invoiceNo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._partitionDate = partitionDate;
			this._bConfirm = bConfirm;
			this._cName = cName;
			this._genderID = genderID;
			this._genderName = genderName;
			this._age = age;
			this._ageUnitID = ageUnitID;
			this._ageUnitName = ageUnitName;
			this._birthday = birthday;
			this._ageDesc = ageDesc;
			this._patHeight = patHeight;
			this._patWeight = patWeight;
			this._folkID = folkID;
			this._folkName = folkName;
			this._patAddress = patAddress;
			this._patPhoto = patPhoto;
			this._phoneCode = phoneCode;
			this._weChatNo = weChatNo;
			this._eMailAddress = eMailAddress;
			this._patType = patType;
			this._sickTypeID = sickTypeID;
			this._sickType = sickType;
			this._diagID = diagID;
			this._diagName = diagName;
			this._doctorID = doctorID;
			this._doctorName = doctorName;
			this._doctorTell = doctorTell;
			this._execDeptID = execDeptID;
			this._deptID = deptID;
			this._deptName = deptName;
			this._visitTimes = visitTimes;
			this._visitDate = visitDate;
			this._districtID = districtID;
			this._districtName = districtName;
			this._wardID = wardID;
			this._wardName = wardName;
			this._bed = bed;
			this._phyPeriodID = phyPeriodID;
			this._phyPeriodName = phyPeriodName;
			this._collectPartID = collectPartID;
			this._collectPartName = collectPartName;
			this._hISComment = hISComment;
			this._patComment = patComment;
			this._iDCardNo = iDCardNo;
			this._hisPatNo = hisPatNo;
			this._patNo = patNo;
			this._patCardNo = patCardNo;
			this._inPatNo = inPatNo;
			this._examNo = examNo;
			this._medicareNo = medicareNo;
			this._unionPayNo = unionPayNo;
			this._healthCardNo = healthCardNo;
			this._powerCardNo = powerCardNo;
			this._inOutSerialNo = inOutSerialNo;
			this._invoiceNo = invoiceNo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


		/// <summary>
		/// 分区日期
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "分区日期", ShortCode = "PartitionDate", Desc = "分区日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PartitionDate
		{
			get { return _partitionDate; }
			set { _partitionDate = value; }
		}

		/// <summary>
		/// 是否确认
		/// </summary>
		[DataMember]
		[DataDesc(CName = "是否确认", ShortCode = "BConfirm", Desc = "是否确认", ContextType = SysDic.All, Length = 1)]
		public virtual bool BConfirm
		{
			get { return _bConfirm; }
			set { _bConfirm = value; }
		}

		/// <summary>
		/// 姓名
		/// </summary>
		[DataMember]
		[DataDesc(CName = "姓名", ShortCode = "CName", Desc = "姓名", ContextType = SysDic.All, Length = 100)]
		public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

		/// <summary>
		/// 性别ID
		/// </summary>
		[DataMember]
		[DataDesc(CName = "性别ID", ShortCode = "GenderID", Desc = "性别ID", ContextType = SysDic.All, Length = 4)]
		public virtual int? GenderID
		{
			get { return _genderID; }
			set { _genderID = value; }
		}

		/// <summary>
		/// 性别
		/// </summary>
		[DataMember]
		[DataDesc(CName = "性别", ShortCode = "GenderName", Desc = "性别", ContextType = SysDic.All, Length = 50)]
		public virtual string GenderName
		{
			get { return _genderName; }
			set { _genderName = value; }
		}

		/// <summary>
		/// 年龄
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄", ShortCode = "Age", Desc = "年龄", ContextType = SysDic.All, Length = 8)]
		public virtual double? Age
		{
			get { return _age; }
			set { _age = value; }
		}

		/// <summary>
		/// 年龄单位ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄单位ID", ShortCode = "AgeUnitID", Desc = "年龄单位ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? AgeUnitID
		{
			get { return _ageUnitID; }
			set { _ageUnitID = value; }
		}

		/// <summary>
		/// 年龄单位名称
		/// </summary>
		[DataMember]
		[DataDesc(CName = "年龄单位名称", ShortCode = "AgeUnitName", Desc = "年龄单位名称", ContextType = SysDic.All, Length = 50)]
		public virtual string AgeUnitName
		{
			get { return _ageUnitName; }
			set { _ageUnitName = value; }
		}

		/// <summary>
		/// 出生日期
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "出生日期", ShortCode = "Birthday", Desc = "出生日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

		/// <summary>
		/// 年龄描述
		/// </summary>
		[DataMember]
		[DataDesc(CName = "年龄描述", ShortCode = "AgeDesc", Desc = "年龄描述", ContextType = SysDic.All, Length = 100)]
		public virtual string AgeDesc
		{
			get { return _ageDesc; }
			set { _ageDesc = value; }
		}

		/// <summary>
		/// 身高
		/// </summary>
		[DataMember]
		[DataDesc(CName = "身高", ShortCode = "PatHeight", Desc = "身高", ContextType = SysDic.All, Length = 4)]
		public virtual int? PatHeight
		{
			get { return _patHeight; }
			set { _patHeight = value; }
		}

		/// <summary>
		/// 体重
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "体重", ShortCode = "PatWeight", Desc = "体重", ContextType = SysDic.All, Length = 8)]
		public virtual double? PatWeight
		{
			get { return _patWeight; }
			set { _patWeight = value; }
		}

		/// <summary>
		/// 民族ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "民族ID", ShortCode = "FolkID", Desc = "民族ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? FolkID
		{
			get { return _folkID; }
			set { _folkID = value; }
		}

		/// <summary>
		/// 民族名称
		/// </summary>
		[DataMember]
		[DataDesc(CName = "民族名称", ShortCode = "FolkName", Desc = "民族名称", ContextType = SysDic.All, Length = 100)]
		public virtual string FolkName
		{
			get { return _folkName; }
			set { _folkName = value; }
		}

		/// <summary>
		/// 住址
		/// </summary>
		[DataMember]
		[DataDesc(CName = "住址", ShortCode = "PatAddress", Desc = "住址", ContextType = SysDic.All, Length = 200)]
		public virtual string PatAddress
		{
			get { return _patAddress; }
			set { _patAddress = value; }
		}

		/// <summary>
		/// 病人相片
		/// </summary>
		[DataMember]
		[DataDesc(CName = "病人相片", ShortCode = "PatPhoto", Desc = "病人相片", ContextType = SysDic.All, Length = -1)]
		public virtual byte[] PatPhoto
		{
			get { return _patPhoto; }
			set { _patPhoto = value; }
		}

		/// <summary>
		/// 电话
		/// </summary>
		[DataMember]
		[DataDesc(CName = "电话", ShortCode = "PhoneCode", Desc = "电话", ContextType = SysDic.All, Length = 100)]
		public virtual string PhoneCode
		{
			get { return _phoneCode; }
			set { _phoneCode = value; }
		}

		/// <summary>
		/// 微信号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "微信号", ShortCode = "WeChatNo", Desc = "微信号", ContextType = SysDic.All, Length = 100)]
		public virtual string WeChatNo
		{
			get { return _weChatNo; }
			set { _weChatNo = value; }
		}

		/// <summary>
		/// 邮箱
		/// </summary>
		[DataMember]
		[DataDesc(CName = "邮箱", ShortCode = "EMailAddress", Desc = "邮箱", ContextType = SysDic.All, Length = 100)]
		public virtual string EMailAddress
		{
			get { return _eMailAddress; }
			set { _eMailAddress = value; }
		}

		/// <summary>
		/// 患者类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "患者类型", ShortCode = "PatType", Desc = "患者类型", ContextType = SysDic.All, Length = 100)]
		public virtual string PatType
		{
			get { return _patType; }
			set { _patType = value; }
		}

		/// <summary>
		/// 就诊类型
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "就诊类型", ShortCode = "SickTypeID", Desc = "就诊类型", ContextType = SysDic.All, Length = 8)]
		public virtual long? SickTypeID
		{
			get { return _sickTypeID; }
			set { _sickTypeID = value; }
		}

		/// <summary>
		/// 就诊类型名称
		/// </summary>
		[DataMember]
		[DataDesc(CName = "就诊类型名称", ShortCode = "SickType", Desc = "就诊类型名称", ContextType = SysDic.All, Length = 50)]
		public virtual string SickType
		{
			get { return _sickType; }
			set { _sickType = value; }
		}

		/// <summary>
		/// 诊断ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "诊断ID", ShortCode = "DiagID", Desc = "诊断ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DiagID
		{
			get { return _diagID; }
			set { _diagID = value; }
		}

		/// <summary>
		/// 诊断
		/// </summary>
		[DataMember]
		[DataDesc(CName = "诊断", ShortCode = "DiagName", Desc = "诊断", ContextType = SysDic.All, Length = 2000)]
		public virtual string DiagName
		{
			get { return _diagName; }
			set { _diagName = value; }
		}

		/// <summary>
		/// 医生ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "医生ID", ShortCode = "DoctorID", Desc = "医生ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DoctorID
		{
			get { return _doctorID; }
			set { _doctorID = value; }
		}

		/// <summary>
		/// 医生
		/// </summary>
		[DataMember]
		[DataDesc(CName = "医生", ShortCode = "DoctorName", Desc = "医生", ContextType = SysDic.All, Length = 100)]
		public virtual string DoctorName
		{
			get { return _doctorName; }
			set { _doctorName = value; }
		}

		/// <summary>
		/// 医生嘱托
		/// </summary>
		[DataMember]
		[DataDesc(CName = "医生嘱托", ShortCode = "DoctorTell", Desc = "医生嘱托", ContextType = SysDic.All, Length = 500)]
		public virtual string DoctorTell
		{
			get { return _doctorTell; }
			set { _doctorTell = value; }
		}

		/// <summary>
		/// 执行科室ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "执行科室ID", ShortCode = "ExecDeptID", Desc = "执行科室ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ExecDeptID
		{
			get { return _execDeptID; }
			set { _execDeptID = value; }
		}

		/// <summary>
		/// 科室ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "科室ID", ShortCode = "DeptID", Desc = "科室ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

		/// <summary>
		/// 科室名称
		/// </summary>
		[DataMember]
		[DataDesc(CName = "科室名称", ShortCode = "DeptName", Desc = "科室名称", ContextType = SysDic.All, Length = 100)]
		public virtual string DeptName
		{
			get { return _deptName; }
			set { _deptName = value; }
		}

		/// <summary>
		/// 住院次数
		/// </summary>
		[DataMember]
		[DataDesc(CName = "住院次数", ShortCode = "VisitTimes", Desc = "住院次数", ContextType = SysDic.All, Length = 4)]
		public virtual int? VisitTimes
		{
			get { return _visitTimes; }
			set { _visitTimes = value; }
		}

		/// <summary>
		/// 住院日期
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "住院日期", ShortCode = "VisitDate", Desc = "住院日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? VisitDate
		{
			get { return _visitDate; }
			set { _visitDate = value; }
		}

		/// <summary>
		/// 病区ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "病区ID", ShortCode = "DistrictID", Desc = "病区ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DistrictID
		{
			get { return _districtID; }
			set { _districtID = value; }
		}

		/// <summary>
		/// 病区
		/// </summary>
		[DataMember]
		[DataDesc(CName = "病区", ShortCode = "DistrictName", Desc = "病区", ContextType = SysDic.All, Length = 100)]
		public virtual string DistrictName
		{
			get { return _districtName; }
			set { _districtName = value; }
		}

		/// <summary>
		/// 病房ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "病房ID", ShortCode = "WardID", Desc = "病房ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? WardID
		{
			get { return _wardID; }
			set { _wardID = value; }
		}

		/// <summary>
		/// 病房
		/// </summary>
		[DataMember]
		[DataDesc(CName = "病房", ShortCode = "WardName", Desc = "病房", ContextType = SysDic.All, Length = 100)]
		public virtual string WardName
		{
			get { return _wardName; }
			set { _wardName = value; }
		}

		/// <summary>
		/// 床号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "床号", ShortCode = "Bed", Desc = "床号", ContextType = SysDic.All, Length = 100)]
		public virtual string Bed
		{
			get { return _bed; }
			set { _bed = value; }
		}

		/// <summary>
		/// 生理期ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "生理期ID", ShortCode = "PhyPeriodID", Desc = "生理期ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? PhyPeriodID
		{
			get { return _phyPeriodID; }
			set { _phyPeriodID = value; }
		}

		/// <summary>
		/// 生理期
		/// </summary>
		[DataMember]
		[DataDesc(CName = "生理期", ShortCode = "PhyPeriodName", Desc = "生理期", ContextType = SysDic.All, Length = 500)]
		public virtual string PhyPeriodName
		{
			get { return _phyPeriodName; }
			set { _phyPeriodName = value; }
		}

		/// <summary>
		/// 采样部位ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样部位ID", ShortCode = "CollectPartID", Desc = "采样部位ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? CollectPartID
		{
			get { return _collectPartID; }
			set { _collectPartID = value; }
		}

		/// <summary>
		/// 采样部位
		/// </summary>
		[DataMember]
		[DataDesc(CName = "采样部位", ShortCode = "CollectPartName", Desc = "采样部位", ContextType = SysDic.All, Length = 500)]
		public virtual string CollectPartName
		{
			get { return _collectPartName; }
			set { _collectPartName = value; }
		}

		/// <summary>
		/// 临床HIS备注
		/// </summary>
		[DataMember]
		[DataDesc(CName = "临床HIS备注", ShortCode = "HISComment", Desc = "临床HIS备注", ContextType = SysDic.All, Length = 500)]
		public virtual string HISComment
		{
			get { return _hISComment; }
			set { _hISComment = value; }
		}

		/// <summary>
		/// 临床病人备注
		/// </summary>
		[DataMember]
		[DataDesc(CName = "临床病人备注", ShortCode = "PatComment", Desc = "临床病人备注", ContextType = SysDic.All, Length = 500)]
		public virtual string PatComment
		{
			get { return _patComment; }
			set { _patComment = value; }
		}

		/// <summary>
		/// 身份证号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "身份证号", ShortCode = "IDCardNo", Desc = "身份证号", ContextType = SysDic.All, Length = 40)]
		public virtual string IDCardNo
		{
			get { return _iDCardNo; }
			set { _iDCardNo = value; }
		}

		/// <summary>
		/// 医院病人唯一编号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "医院病人唯一编号", ShortCode = "HisPatNo", Desc = "医院病人唯一编号", ContextType = SysDic.All, Length = 100)]
		public virtual string HisPatNo
		{
			get { return _hisPatNo; }
			set { _hisPatNo = value; }
		}

		/// <summary>
		/// 病历号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "病历号", ShortCode = "PatNo", Desc = "病历号", ContextType = SysDic.All, Length = 100)]
		public virtual string PatNo
		{
			get { return _patNo; }
			set { _patNo = value; }
		}

		/// <summary>
		/// 就诊卡号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "就诊卡号", ShortCode = "PatCardNo", Desc = "就诊卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string PatCardNo
		{
			get { return _patCardNo; }
			set { _patCardNo = value; }
		}

		/// <summary>
		/// 住院号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "住院号", ShortCode = "InPatNo", Desc = "住院号", ContextType = SysDic.All, Length = 100)]
		public virtual string InPatNo
		{
			get { return _inPatNo; }
			set { _inPatNo = value; }
		}

		/// <summary>
		/// 体检号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "体检号", ShortCode = "ExamNo", Desc = "体检号", ContextType = SysDic.All, Length = 100)]
		public virtual string ExamNo
		{
			get { return _examNo; }
			set { _examNo = value; }
		}

		/// <summary>
		/// 医保卡号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "医保卡号", ShortCode = "MedicareNo", Desc = "医保卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string MedicareNo
		{
			get { return _medicareNo; }
			set { _medicareNo = value; }
		}

		/// <summary>
		/// 银联卡号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "银联卡号", ShortCode = "UnionPayNo", Desc = "银联卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string UnionPayNo
		{
			get { return _unionPayNo; }
			set { _unionPayNo = value; }
		}

		/// <summary>
		/// 居民健康卡号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "居民健康卡号", ShortCode = "HealthCardNo", Desc = "居民健康卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string HealthCardNo
		{
			get { return _healthCardNo; }
			set { _healthCardNo = value; }
		}

		/// <summary>
		/// 一卡通卡号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "一卡通卡号", ShortCode = "PowerCardNo", Desc = "一卡通卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string PowerCardNo
		{
			get { return _powerCardNo; }
			set { _powerCardNo = value; }
		}

		/// <summary>
		/// 门诊住院流水号
		/// </summary>
		[DataMember]
		[DataDesc(CName = "门诊住院流水号", ShortCode = "InOutSerialNo", Desc = "门诊住院流水号", ContextType = SysDic.All, Length = 100)]
		public virtual string InOutSerialNo
		{
			get { return _inOutSerialNo; }
			set { _inOutSerialNo = value; }
		}

		/// <summary>
		/// 收据号（发票号）
		/// </summary>
		[DataMember]
		[DataDesc(CName = "收据号（发票号）", ShortCode = "InvoiceNo", Desc = "收据号（发票号）", ContextType = SysDic.All, Length = 100)]
		public virtual string InvoiceNo
		{
			get { return _invoiceNo; }
			set { _invoiceNo = value; }
		}

		/// <summary>
		/// 数据更新时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		#endregion
	}
	#endregion
}