using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "医嘱信息", ClassCName = "LisOrderFormPatientOrderIitemVO", ShortCode = "LisOrderFormPatientOrderIitemVO", Desc = "医嘱信息")]
    public class LisOrderFormPatientOrderIitemVO
    {
        #region lisorderform
        protected string _orderFormNo;
        protected long? _orderTypeID;
        protected DateTime? _orderTime;
        protected DateTime? _orderExecTime;
        protected int _orderExecFlag;
        protected long? _hospitalID;
        protected long? _execDeptID;
        protected long? _destinationID;
        protected int _isUrgent;
        protected long? _clientID;
        protected int _isByHand;
        protected int _isAffirm;
        protected string _formMemo;
        protected long? _chargeID;
        protected string _chargeOrderName;
        protected double _charge;
        protected int _isCheckFee;
        protected double _balance;
        protected string _operHost;
        protected long? _operatorID;
        protected string _operatorName;
        protected long? _checkerID;
        protected string _checkerName;
        protected string _parItemCName;
        protected string _photoURL;
        protected string _requestSource;
        protected int _printTimes;
        protected string _hisHospitalNo;
        protected string _hisDeptNo;
        protected string _hisDeptName;
        protected string _hisDoctorNo;
        protected string _hisDoctor;
        protected string _hisDoctorPhoneCode;
        protected long? _patID;

        [DataMember]
        [DataDesc(CName = "医嘱单号", ShortCode = "OrderFormNo", Desc = "医嘱单号", ContextType = SysDic.All, Length = 100)]
        public virtual string OrderFormNo
        {
            get { return _orderFormNo; }
            set { _orderFormNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱类型ID", ShortCode = "OrderTypeID", Desc = "医嘱类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderTypeID
        {
            get { return _orderTypeID; }
            set { _orderTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开单日期", ShortCode = "OrderTime", Desc = "开单日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OrderTime
        {
            get { return _orderTime; }
            set { _orderTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱执行时间", ShortCode = "OrderExecTime", Desc = "医嘱执行时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OrderExecTime
        {
            get { return _orderExecTime; }
            set { _orderExecTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱执行标志", ShortCode = "OrderExecFlag", Desc = "医嘱执行标志", ContextType = SysDic.All, Length = 4)]
        public virtual int OrderExecFlag
        {
            get { return _orderExecFlag; }
            set { _orderExecFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "院区ID", ShortCode = "HospitalID", Desc = "院区ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? HospitalID
        {
            get { return _hospitalID; }
            set { _hospitalID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验执行科室ID", ShortCode = "ExecDeptID", Desc = "检验执行科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ExecDeptID
        {
            get { return _execDeptID; }
            set { _execDeptID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送检目的地ID", ShortCode = "DestinationID", Desc = "送检目的地ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DestinationID
        {
            get { return _destinationID; }
            set { _destinationID = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否急查", ShortCode = "IsUrgent", Desc = "是否急查", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUrgent
        {
            get { return _isUrgent; }
            set { _isUrgent = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "ClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ClientID
        {
            get { return _clientID; }
            set { _clientID = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否手工录入", ShortCode = "IsByHand", Desc = "是否手工录入", ContextType = SysDic.All, Length = 4)]
        public virtual int IsByHand
        {
            get { return _isByHand; }
            set { _isByHand = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已经审核", ShortCode = "IsAffirm", Desc = "是否已经审核", ContextType = SysDic.All, Length = 4)]
        public virtual int IsAffirm
        {
            get { return _isAffirm; }
            set { _isAffirm = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱备注", ShortCode = "FormMemo", Desc = "医嘱备注", ContextType = SysDic.All, Length = 500)]
        public virtual string FormMemo
        {
            get { return _formMemo; }
            set { _formMemo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收费类型ID", ShortCode = "ChargeID", Desc = "收费类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ChargeID
        {
            get { return _chargeID; }
            set { _chargeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "收费类型名称", ShortCode = "ChargeOrderName", Desc = "收费类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ChargeOrderName
        {
            get { return _chargeOrderName; }
            set { _chargeOrderName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用金额", ShortCode = "Charge", Desc = "费用金额", ContextType = SysDic.All, Length = 8)]
        public virtual double Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已经计费", ShortCode = "IsCheckFee", Desc = "是否已经计费", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCheckFee
        {
            get { return _isCheckFee; }
            set { _isCheckFee = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "余额", ShortCode = "Balance", Desc = "余额", ContextType = SysDic.All, Length = 8)]
        public virtual double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        [DataMember]
        [DataDesc(CName = "开单站点", ShortCode = "OperHost", Desc = "开单站点", ContextType = SysDic.All, Length = 200)]
        public virtual string OperHost
        {
            get { return _operHost; }
            set { _operHost = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开单者ID", ShortCode = "OperatorID", Desc = "开单者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "开单者", ShortCode = "OperatorName", Desc = "开单者", ContextType = SysDic.All, Length = 200)]
        public virtual string OperatorName
        {
            get { return _operatorName; }
            set { _operatorName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核者ID", ShortCode = "CheckerID", Desc = "审核者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckerID
        {
            get { return _checkerID; }
            set { _checkerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核者", ShortCode = "CheckerName", Desc = "审核者", ContextType = SysDic.All, Length = 200)]
        public virtual string CheckerName
        {
            get { return _checkerName; }
            set { _checkerName = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目名称", ShortCode = "ParItemCName", Desc = "项目名称", ContextType = SysDic.All, Length = 2000)]
        public virtual string ParItemCName
        {
            get { return _parItemCName; }
            set { _parItemCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "图片路径", ShortCode = "PhotoURL", Desc = "图片路径", ContextType = SysDic.All, Length = 200)]
        public virtual string PhotoURL
        {
            get { return _photoURL; }
            set { _photoURL = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱来源", ShortCode = "RequestSource", Desc = "医嘱来源", ContextType = SysDic.All, Length = 200)]
        public virtual string RequestSource
        {
            get { return _requestSource; }
            set { _requestSource = value; }
        }

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        [DataDesc(CName = "His院区编号", ShortCode = "HisHospitalNo", Desc = "His院区编号", ContextType = SysDic.All, Length = 200)]
        public virtual string HisHospitalNo
        {
            get { return _hisHospitalNo; }
            set { _hisHospitalNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His科室编号", ShortCode = "HisDeptNo", Desc = "His科室编号", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDeptNo
        {
            get { return _hisDeptNo; }
            set { _hisDeptNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His科室", ShortCode = "HisDeptName", Desc = "His科室", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDeptName
        {
            get { return _hisDeptName; }
            set { _hisDeptName = value; }
        }

        [DataMember]
        [DataDesc(CName = "His医生编号", ShortCode = "HisDoctorNo", Desc = "His医生编号", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDoctorNo
        {
            get { return _hisDoctorNo; }
            set { _hisDoctorNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His医生", ShortCode = "HisDoctor", Desc = "His医生", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDoctor
        {
            get { return _hisDoctor; }
            set { _hisDoctor = value; }
        }

        [DataMember]
        [DataDesc(CName = "His医生电话", ShortCode = "HisDoctorPhoneCode", Desc = "His医生电话", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDoctorPhoneCode
        {
            get { return _hisDoctorPhoneCode; }
            set { _hisDoctorPhoneCode = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "患者就诊信息ID", ShortCode = "PatID", Desc = "患者就诊信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PatID
        {
            get { return _patID; }
            set { _patID = value; }
        }
		#endregion

		#region lispatient
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
		protected long? _deptID;
		protected string _deptName;
		protected int _visitTimes;
		protected DateTime? _visitDate;
		protected long? _districtID;
		protected string _districtName;
		protected long? _wardID;
		protected string _wardName;
		protected string _bed;
		protected long _phyPeriodID;
		protected string _phyPeriodName;
		protected long _collectPartID;
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


		[DataMember]
		[DataDesc(CName = "是否确认", ShortCode = "BConfirm", Desc = "是否确认", ContextType = SysDic.All, Length = 1)]
		public virtual bool BConfirm
		{
			get { return _bConfirm; }
			set { _bConfirm = value; }
		}

		[DataMember]
		[DataDesc(CName = "姓名", ShortCode = "CName", Desc = "姓名", ContextType = SysDic.All, Length = 100)]
		public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

		[DataMember]
		[DataDesc(CName = "性别ID", ShortCode = "GenderID", Desc = "性别ID", ContextType = SysDic.All, Length = 4)]
		public virtual int? GenderID
		{
			get { return _genderID; }
			set { _genderID = value; }
		}

		[DataMember]
		[DataDesc(CName = "性别", ShortCode = "GenderName", Desc = "性别", ContextType = SysDic.All, Length = 50)]
		public virtual string GenderName
		{
			get { return _genderName; }
			set { _genderName = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄", ShortCode = "Age", Desc = "年龄", ContextType = SysDic.All, Length = 8)]
		public virtual double? Age
		{
			get { return _age; }
			set { _age = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄单位ID", ShortCode = "AgeUnitID", Desc = "年龄单位ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? AgeUnitID
		{
			get { return _ageUnitID; }
			set { _ageUnitID = value; }
		}

		[DataMember]
		[DataDesc(CName = "年龄单位名称", ShortCode = "AgeUnitName", Desc = "年龄单位名称", ContextType = SysDic.All, Length = 50)]
		public virtual string AgeUnitName
		{
			get { return _ageUnitName; }
			set { _ageUnitName = value; }
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
		[DataDesc(CName = "年龄描述", ShortCode = "AgeDesc", Desc = "年龄描述", ContextType = SysDic.All, Length = 100)]
		public virtual string AgeDesc
		{
			get { return _ageDesc; }
			set { _ageDesc = value; }
		}

		[DataMember]
		[DataDesc(CName = "身高", ShortCode = "PatHeight", Desc = "身高", ContextType = SysDic.All, Length = 4)]
		public virtual int? PatHeight
		{
			get { return _patHeight; }
			set { _patHeight = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "体重", ShortCode = "PatWeight", Desc = "体重", ContextType = SysDic.All, Length = 8)]
		public virtual double? PatWeight
		{
			get { return _patWeight; }
			set { _patWeight = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "民族ID", ShortCode = "FolkID", Desc = "民族ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? FolkID
		{
			get { return _folkID; }
			set { _folkID = value; }
		}

		[DataMember]
		[DataDesc(CName = "民族名称", ShortCode = "FolkName", Desc = "民族名称", ContextType = SysDic.All, Length = 100)]
		public virtual string FolkName
		{
			get { return _folkName; }
			set { _folkName = value; }
		}

		[DataMember]
		[DataDesc(CName = "住址", ShortCode = "PatAddress", Desc = "住址", ContextType = SysDic.All, Length = 200)]
		public virtual string PatAddress
		{
			get { return _patAddress; }
			set { _patAddress = value; }
		}

		[DataMember]
		[DataDesc(CName = "病人相片", ShortCode = "PatPhoto", Desc = "病人相片", ContextType = SysDic.All, Length = -1)]
		public virtual byte[] PatPhoto
		{
			get { return _patPhoto; }
			set { _patPhoto = value; }
		}

		[DataMember]
		[DataDesc(CName = "电话", ShortCode = "PhoneCode", Desc = "电话", ContextType = SysDic.All, Length = 100)]
		public virtual string PhoneCode
		{
			get { return _phoneCode; }
			set { _phoneCode = value; }
		}

		[DataMember]
		[DataDesc(CName = "微信号", ShortCode = "WeChatNo", Desc = "微信号", ContextType = SysDic.All, Length = 100)]
		public virtual string WeChatNo
		{
			get { return _weChatNo; }
			set { _weChatNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "邮箱", ShortCode = "EMailAddress", Desc = "邮箱", ContextType = SysDic.All, Length = 100)]
		public virtual string EMailAddress
		{
			get { return _eMailAddress; }
			set { _eMailAddress = value; }
		}

		[DataMember]
		[DataDesc(CName = "患者类型", ShortCode = "PatType", Desc = "患者类型", ContextType = SysDic.All, Length = 100)]
		public virtual string PatType
		{
			get { return _patType; }
			set { _patType = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "就诊类型", ShortCode = "SickTypeID", Desc = "就诊类型", ContextType = SysDic.All, Length = 8)]
		public virtual long? SickTypeID
		{
			get { return _sickTypeID; }
			set { _sickTypeID = value; }
		}

		[DataMember]
		[DataDesc(CName = "就诊类型名称", ShortCode = "SickType", Desc = "就诊类型名称", ContextType = SysDic.All, Length = 50)]
		public virtual string SickType
		{
			get { return _sickType; }
			set { _sickType = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "诊断ID", ShortCode = "DiagID", Desc = "诊断ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DiagID
		{
			get { return _diagID; }
			set { _diagID = value; }
		}

		[DataMember]
		[DataDesc(CName = "诊断", ShortCode = "DiagName", Desc = "诊断", ContextType = SysDic.All, Length = 2000)]
		public virtual string DiagName
		{
			get { return _diagName; }
			set { _diagName = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "医生ID", ShortCode = "DoctorID", Desc = "医生ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DoctorID
		{
			get { return _doctorID; }
			set { _doctorID = value; }
		}

		[DataMember]
		[DataDesc(CName = "医生", ShortCode = "DoctorName", Desc = "医生", ContextType = SysDic.All, Length = 100)]
		public virtual string DoctorName
		{
			get { return _doctorName; }
			set { _doctorName = value; }
		}

		[DataMember]
		[DataDesc(CName = "医生嘱托", ShortCode = "DoctorTell", Desc = "医生嘱托", ContextType = SysDic.All, Length = 500)]
		public virtual string DoctorTell
		{
			get { return _doctorTell; }
			set { _doctorTell = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "科室ID", ShortCode = "DeptID", Desc = "科室ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

		[DataMember]
		[DataDesc(CName = "科室名称", ShortCode = "DeptName", Desc = "科室名称", ContextType = SysDic.All, Length = 100)]
		public virtual string DeptName
		{
			get { return _deptName; }
			set { _deptName = value; }
		}

		[DataMember]
		[DataDesc(CName = "住院次数", ShortCode = "VisitTimes", Desc = "住院次数", ContextType = SysDic.All, Length = 4)]
		public virtual int VisitTimes
		{
			get { return _visitTimes; }
			set { _visitTimes = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "住院日期", ShortCode = "VisitDate", Desc = "住院日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? VisitDate
		{
			get { return _visitDate; }
			set { _visitDate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "病区ID", ShortCode = "DistrictID", Desc = "病区ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DistrictID
		{
			get { return _districtID; }
			set { _districtID = value; }
		}

		[DataMember]
		[DataDesc(CName = "病区", ShortCode = "DistrictName", Desc = "病区", ContextType = SysDic.All, Length = 100)]
		public virtual string DistrictName
		{
			get { return _districtName; }
			set { _districtName = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "病房ID", ShortCode = "WardID", Desc = "病房ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? WardID
		{
			get { return _wardID; }
			set { _wardID = value; }
		}

		[DataMember]
		[DataDesc(CName = "病房", ShortCode = "WardName", Desc = "病房", ContextType = SysDic.All, Length = 100)]
		public virtual string WardName
		{
			get { return _wardName; }
			set { _wardName = value; }
		}

		[DataMember]
		[DataDesc(CName = "床号", ShortCode = "Bed", Desc = "床号", ContextType = SysDic.All, Length = 100)]
		public virtual string Bed
		{
			get { return _bed; }
			set { _bed = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "生理期ID", ShortCode = "PhyPeriodID", Desc = "生理期ID", ContextType = SysDic.All, Length = 8)]
		public virtual long PhyPeriodID
		{
			get { return _phyPeriodID; }
			set { _phyPeriodID = value; }
		}

		[DataMember]
		[DataDesc(CName = "生理期", ShortCode = "PhyPeriodName", Desc = "生理期", ContextType = SysDic.All, Length = 500)]
		public virtual string PhyPeriodName
		{
			get { return _phyPeriodName; }
			set { _phyPeriodName = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样部位ID", ShortCode = "CollectPartID", Desc = "采样部位ID", ContextType = SysDic.All, Length = 8)]
		public virtual long CollectPartID
		{
			get { return _collectPartID; }
			set { _collectPartID = value; }
		}

		[DataMember]
		[DataDesc(CName = "采样部位", ShortCode = "CollectPartName", Desc = "采样部位", ContextType = SysDic.All, Length = 500)]
		public virtual string CollectPartName
		{
			get { return _collectPartName; }
			set { _collectPartName = value; }
		}

		[DataMember]
		[DataDesc(CName = "临床HIS备注", ShortCode = "HISComment", Desc = "临床HIS备注", ContextType = SysDic.All, Length = 500)]
		public virtual string HISComment
		{
			get { return _hISComment; }
			set { _hISComment = value; }
		}

		[DataMember]
		[DataDesc(CName = "临床病人备注", ShortCode = "PatComment", Desc = "临床病人备注", ContextType = SysDic.All, Length = 500)]
		public virtual string PatComment
		{
			get { return _patComment; }
			set { _patComment = value; }
		}

		[DataMember]
		[DataDesc(CName = "身份证号", ShortCode = "IDCardNo", Desc = "身份证号", ContextType = SysDic.All, Length = 40)]
		public virtual string IDCardNo
		{
			get { return _iDCardNo; }
			set { _iDCardNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "医院病人唯一编号", ShortCode = "HisPatNo", Desc = "医院病人唯一编号", ContextType = SysDic.All, Length = 100)]
		public virtual string HisPatNo
		{
			get { return _hisPatNo; }
			set { _hisPatNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "病历号", ShortCode = "PatNo", Desc = "病历号", ContextType = SysDic.All, Length = 100)]
		public virtual string PatNo
		{
			get { return _patNo; }
			set { _patNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "就诊卡号", ShortCode = "PatCardNo", Desc = "就诊卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string PatCardNo
		{
			get { return _patCardNo; }
			set { _patCardNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "住院号", ShortCode = "InPatNo", Desc = "住院号", ContextType = SysDic.All, Length = 100)]
		public virtual string InPatNo
		{
			get { return _inPatNo; }
			set { _inPatNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "体检号", ShortCode = "ExamNo", Desc = "体检号", ContextType = SysDic.All, Length = 100)]
		public virtual string ExamNo
		{
			get { return _examNo; }
			set { _examNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "医保卡号", ShortCode = "MedicareNo", Desc = "医保卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string MedicareNo
		{
			get { return _medicareNo; }
			set { _medicareNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "银联卡号", ShortCode = "UnionPayNo", Desc = "银联卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string UnionPayNo
		{
			get { return _unionPayNo; }
			set { _unionPayNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "居民健康卡号", ShortCode = "HealthCardNo", Desc = "居民健康卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string HealthCardNo
		{
			get { return _healthCardNo; }
			set { _healthCardNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "一卡通卡号", ShortCode = "PowerCardNo", Desc = "一卡通卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string PowerCardNo
		{
			get { return _powerCardNo; }
			set { _powerCardNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "门诊住院流水号", ShortCode = "InOutSerialNo", Desc = "门诊住院流水号", ContextType = SysDic.All, Length = 100)]
		public virtual string InOutSerialNo
		{
			get { return _inOutSerialNo; }
			set { _inOutSerialNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "收据号（发票号）", ShortCode = "InvoiceNo", Desc = "收据号（发票号）", ContextType = SysDic.All, Length = 100)]
		public virtual string InvoiceNo
		{
			get { return _invoiceNo; }
			set { _invoiceNo = value; }
		}


        #endregion

        #region lisorderitem
        protected long? _ordersItemID;
        protected DateTime? _orderDate;
        protected DateTime _partitionDate;
        protected int _orderItemExecFlag;
        protected long? _itemStatusID;
        protected int _isCancelled;
        protected int _isPriceItem;
        protected string _removeHost;
        protected string _remover;
        protected DateTime? _removeTime;
        protected long? _sampleTypeID;
        protected string _collectPart;
        protected string _hisItemNo;
        protected string _hisItemName;
        protected string _hisSampleTypeNo;
        protected string _hisSampleTypeName;
        protected DateTime? _dataUpdateTime;
        protected long? _orderFormID;

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱项目ID", ShortCode = "OrdersItemID", Desc = "医嘱项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrdersItemID
        {
            get { return _ordersItemID; }
            set { _ordersItemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱日期", ShortCode = "OrderDate", Desc = "医嘱日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "分区日期", ShortCode = "PartitionDate", Desc = "分区日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime PartitionDate
        {
            get { return _partitionDate; }
            set { _partitionDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱项目执行标志", ShortCode = "OrderItemExecFlag", Desc = "医嘱项目执行标志", ContextType = SysDic.All, Length = 4)]
        public virtual int OrderItemExecFlag
        {
            get { return _orderItemExecFlag; }
            set { _orderItemExecFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱项目状态ID", ShortCode = "ItemStatusID", Desc = "医嘱项目状态ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemStatusID
        {
            get { return _itemStatusID; }
            set { _itemStatusID = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已经作废", ShortCode = "IsCancelled", Desc = "是否已经作废", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCancelled
        {
            get { return _isCancelled; }
            set { _isCancelled = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否增补收费项目", ShortCode = "IsPriceItem", Desc = "是否增补收费项目", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPriceItem
        {
            get { return _isPriceItem; }
            set { _isPriceItem = value; }
        }


        [DataMember]
        [DataDesc(CName = "退费站点", ShortCode = "RemoveHost", Desc = "退费站点", ContextType = SysDic.All, Length = 100)]
        public virtual string RemoveHost
        {
            get { return _removeHost; }
            set { _removeHost = value; }
        }

        [DataMember]
        [DataDesc(CName = "退费人", ShortCode = "Remover", Desc = "退费人", ContextType = SysDic.All, Length = 100)]
        public virtual string Remover
        {
            get { return _remover; }
            set { _remover = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费时间", ShortCode = "RemoveTime", Desc = "退费时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RemoveTime
        {
            get { return _removeTime; }
            set { _removeTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本类型ID", ShortCode = "SampleTypeID", Desc = "样本类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleTypeID
        {
            get { return _sampleTypeID; }
            set { _sampleTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样部位", ShortCode = "CollectPart", Desc = "采样部位", ContextType = SysDic.All, Length = 100)]
        public virtual string CollectPart
        {
            get { return _collectPart; }
            set { _collectPart = value; }
        }

        [DataMember]
        [DataDesc(CName = "HIS项目编号", ShortCode = "HisItemNo", Desc = "HIS项目编号", ContextType = SysDic.All, Length = 100)]
        public virtual string HisItemNo
        {
            get { return _hisItemNo; }
            set { _hisItemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "HIS项目名称", ShortCode = "HisItemName", Desc = "HIS项目名称", ContextType = SysDic.All, Length = 100)]
        public virtual string HisItemName
        {
            get { return _hisItemName; }
            set { _hisItemName = value; }
        }

        [DataMember]
        [DataDesc(CName = "His样本类型编号", ShortCode = "HisSampleTypeNo", Desc = "His样本类型编号", ContextType = SysDic.All, Length = 100)]
        public virtual string HisSampleTypeNo
        {
            get { return _hisSampleTypeNo; }
            set { _hisSampleTypeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His样本类型", ShortCode = "HisSampleTypeName", Desc = "His样本类型", ContextType = SysDic.All, Length = 100)]
        public virtual string HisSampleTypeName
        {
            get { return _hisSampleTypeName; }
            set { _hisSampleTypeName = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱单ID", ShortCode = "OrderFormID", Desc = "医嘱单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderFormID
        {
            get { return _orderFormID; }
            set { _orderFormID = value; }
        }

        #endregion

        [DataMember]
        public string BarCodesItemName { get; set; }
        [DataMember]
        public string OrdersItemName { get; set; }
        [DataMember]
        public string BarCode { get; set; }
    }
}
