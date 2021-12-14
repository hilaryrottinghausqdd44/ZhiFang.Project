using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodPatinfo

	/// <summary>
	/// BloodPatinfo object for NHibernate mapped table 'Blood_Patinfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "病人就诊记录信息表", ClassCName = "BloodPatinfo", ShortCode = "BloodPatinfo", Desc = "病人就诊记录信息表")]
	public class BloodPatinfo : BaseEntity
	{
		#region Member Variables
		
        protected string _hisPatID;
        protected int _visitID;
        protected string _hisWardNo;
        protected long? _wardNo;
        protected string _hisDeptNo;
        protected long? _deptNo;
        protected long? _bReqTypeID;
        protected long? _bUseTimeTypeID;
        protected DateTime? _patindate;
        protected string _admID;
        protected string _patNo;
        protected string _bed;
        protected string _wristBandNo;
        protected string _cName;
        protected string _sex;
        protected int _age;
        protected string _ageuNit;
        protected string _ageALL;
        protected DateTime? _birthday;
        protected string _patIdentity;
        protected string _cardId;
        protected double _patHeight;
        protected double _patWeight;
        protected bool _isAgree;
		protected bool _isOrder;
		protected bool _isLabNoC;
		protected string _hisABOCode;
        protected string _hisRhCode;
        protected bool _gravida;
        protected bool _birth;
        protected bool _beforUse;
        protected string _addressType;
        protected string _diag;
        protected double _bodyTPE;
        protected double _bPress;
        protected double _breathe;
        protected double _pulse;
        protected double _heartrate;
        protected double _urine;
        protected bool _isAnesth;
        protected bool _hasAllergy;
        protected string _costtype;
        protected bool _hasTransplant;
        protected DateTime? _transdate;
        protected string _donorABORH;
		#endregion

		#region Constructors

		public BloodPatinfo() { }

		public BloodPatinfo( long labID, string hisPatID, int visitID, string hisWardNo, long wardNo, string hisDeptNo, long deptNo, long bReqTypeID, long bUseTimeTypeID, DateTime patindate, string admID, string patNo, string bed, string wristBandNo, string cName, string sex, int age, string ageuNit, string ageALL, DateTime birthday, string patIdentity, string cardId, double patHeight, double patWeight, bool isAgree, bool isOrder, bool isLabNoC, string hisABOCode, string hisRhCode, bool gravida, bool birth, bool beforUse, string addressType, string diag, double bodyTPE, double bPress, double breathe, double pulse, double heartrate, double urine, bool isAnesth, bool hasAllergy, string costtype, bool hasTransplant, DateTime transdate, string donorABORH, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._hisPatID = hisPatID;
			this._visitID = visitID;
			this._hisWardNo = hisWardNo;
			this._wardNo = wardNo;
			this._hisDeptNo = hisDeptNo;
			this._deptNo = deptNo;
			this._bReqTypeID = bReqTypeID;
			this._bUseTimeTypeID = bUseTimeTypeID;
			this._patindate = patindate;
			this._admID = admID;
			this._patNo = patNo;
			this._bed = bed;
			this._wristBandNo = wristBandNo;
			this._cName = cName;
			this._sex = sex;
			this._age = age;
			this._ageuNit = ageuNit;
			this._ageALL = ageALL;
			this._birthday = birthday;
			this._patIdentity = patIdentity;
			this._cardId = cardId;
			this._patHeight = patHeight;
			this._patWeight = patWeight;
			this._isAgree = isAgree;
			this._isOrder = isOrder;
			this._isLabNoC = isLabNoC;
			this._hisABOCode = hisABOCode;
			this._hisRhCode = hisRhCode;
			this._gravida = gravida;
			this._birth = birth;
			this._beforUse = beforUse;
			this._addressType = addressType;
			this._diag = diag;
			this._bodyTPE = bodyTPE;
			this._bPress = bPress;
			this._breathe = breathe;
			this._pulse = pulse;
			this._heartrate = heartrate;
			this._urine = urine;
			this._isAnesth = isAnesth;
			this._hasAllergy = hasAllergy;
			this._costtype = costtype;
			this._hasTransplant = hasTransplant;
			this._transdate = transdate;
			this._donorABORH = donorABORH;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "His病人记录Id", ShortCode = "HisPatID", Desc = "His病人记录Id", ContextType = SysDic.All, Length = 20)]
        public virtual string HisPatID
		{
			get { return _hisPatID; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisPatID", value, value.ToString());
				_hisPatID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "入院次数", ShortCode = "VisitID", Desc = "入院次数", ContextType = SysDic.All, Length = 4)]
        public virtual int VisitID
		{
			get { return _visitID; }
			set { _visitID = value; }
		}

        [DataMember]
        [DataDesc(CName = "HIS病区编码", ShortCode = "HisWardNo", Desc = "HIS病区编码", ContextType = SysDic.All, Length = 20)]
        public virtual string HisWardNo
		{
			get { return _hisWardNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisWardNo", value, value.ToString());
				_hisWardNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "病区编码", ShortCode = "WardNo", Desc = "病区编码", ContextType = SysDic.All, Length = 8)]
		public virtual long? WardNo
		{
			get { return _wardNo; }
			set { _wardNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "HIS科室编码", ShortCode = "HisDeptNo", Desc = "HIS科室编码", ContextType = SysDic.All, Length = 20)]
        public virtual string HisDeptNo
		{
			get { return _hisDeptNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisDeptNo", value, value.ToString());
				_hisDeptNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "科室编码", ShortCode = "DeptNo", Desc = "科室编码", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptNo
		{
			get { return _deptNo; }
			set { _deptNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请类型编码", ShortCode = "BReqTypeID", Desc = "申请类型编码", ContextType = SysDic.All, Length = 8)]
		public virtual long? BReqTypeID
		{
			get { return _bReqTypeID; }
			set { _bReqTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "就诊时间类型", ShortCode = "BUseTimeTypeID", Desc = "就诊时间类型", ContextType = SysDic.All, Length = 8)]
		public virtual long? BUseTimeTypeID
		{
			get { return _bUseTimeTypeID; }
			set { _bUseTimeTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "患者入院时间", ShortCode = "Patindate", Desc = "患者入院时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Patindate
		{
			get { return _patindate; }
			set { _patindate = value; }
		}

        [DataMember]
        [DataDesc(CName = "就诊号", ShortCode = "AdmID", Desc = "就诊号", ContextType = SysDic.All, Length = 20)]
        public virtual string AdmID
		{
			get { return _admID; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for AdmID", value, value.ToString());
				_admID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "病历号", ShortCode = "PatNo", Desc = "病历号", ContextType = SysDic.All, Length = 20)]
        public virtual string PatNo
		{
			get { return _patNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PatNo", value, value.ToString());
				_patNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "床号", ShortCode = "Bed", Desc = "床号", ContextType = SysDic.All, Length = 20)]
        public virtual string Bed
		{
			get { return _bed; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Bed", value, value.ToString());
				_bed = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "腕带号", ShortCode = "WristBandNo", Desc = "腕带号", ContextType = SysDic.All, Length = 50)]
        public virtual string WristBandNo
		{
			get { return _wristBandNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WristBandNo", value, value.ToString());
				_wristBandNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "姓名", ShortCode = "CName", Desc = "姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "性别", ShortCode = "Sex", Desc = "性别", ContextType = SysDic.All, Length = 10)]
        public virtual string Sex
		{
			get { return _sex; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Sex", value, value.ToString());
				_sex = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "年龄", ShortCode = "Age", Desc = "年龄", ContextType = SysDic.All, Length = 4)]
        public virtual int Age
		{
			get { return _age; }
			set { _age = value; }
		}

        [DataMember]
        [DataDesc(CName = "年龄单位", ShortCode = "AgeuNit", Desc = "年龄单位", ContextType = SysDic.All, Length = 10)]
        public virtual string AgeuNit
		{
			get { return _ageuNit; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for AgeuNit", value, value.ToString());
				_ageuNit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "年龄描述", ShortCode = "AgeALL", Desc = "年龄描述", ContextType = SysDic.All, Length = 20)]
        public virtual string AgeALL
		{
			get { return _ageALL; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for AgeALL", value, value.ToString());
				_ageALL = value;
			}
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
        [DataDesc(CName = "患者身份", ShortCode = "PatIdentity", Desc = "患者身份", ContextType = SysDic.All, Length = 50)]
        public virtual string PatIdentity
		{
			get { return _patIdentity; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PatIdentity", value, value.ToString());
				_patIdentity = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "身份证", ShortCode = "CardId", Desc = "身份证", ContextType = SysDic.All, Length = 20)]
        public virtual string CardId
		{
			get { return _cardId; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CardId", value, value.ToString());
				_cardId = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "身高", ShortCode = "PatHeight", Desc = "身高", ContextType = SysDic.All, Length = 8)]
        public virtual double PatHeight
		{
			get { return _patHeight; }
			set { _patHeight = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "体重", ShortCode = "PatWeight", Desc = "体重", ContextType = SysDic.All, Length = 8)]
        public virtual double PatWeight
		{
			get { return _patWeight; }
			set { _patWeight = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否有知情同意书", ShortCode = "IsAgree", Desc = "是否有知情同意书", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsAgree
		{
			get { return _isAgree; }
			set { _isAgree = value; }
		}
		[DataMember]
		[DataDesc(CName = "是否有开相关医嘱", ShortCode = "IsOrder", Desc = "是否有开相关医嘱", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsOrder
		{
			get { return _isOrder; }
			set { _isOrder = value; }
		}
		[DataMember]
		[DataDesc(CName = "已开相关医嘱后是否有采集", ShortCode = "IsLabNoC", Desc = "已开相关医嘱后是否有采集", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsLabNoC
		{
			get { return _isLabNoC; }
			set { _isLabNoC = value; }
		}

		[DataMember]
        [DataDesc(CName = "HIsABO代码", ShortCode = "HisABOCode", Desc = "HIsABO代码", ContextType = SysDic.All, Length = 20)]
        public virtual string HisABOCode
		{
			get { return _hisABOCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisABOCode", value, value.ToString());
				_hisABOCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "HISRH代码", ShortCode = "HisRhCode", Desc = "HISRH代码", ContextType = SysDic.All, Length = 10)]
        public virtual string HisRhCode
		{
			get { return _hisRhCode; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for HisRhCode", value, value.ToString());
				_hisRhCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "孕史", ShortCode = "Gravida", Desc = "孕史", ContextType = SysDic.All, Length = 1)]
        public virtual bool Gravida
		{
			get { return _gravida; }
			set { _gravida = value; }
		}

        [DataMember]
        [DataDesc(CName = "孕史", ShortCode = "Birth", Desc = "孕史", ContextType = SysDic.All, Length = 1)]
        public virtual bool Birth
		{
			get { return _birth; }
			set { _birth = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血史", ShortCode = "BeforUse", Desc = "用血史", ContextType = SysDic.All, Length = 1)]
        public virtual bool BeforUse
		{
			get { return _beforUse; }
			set { _beforUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "属地", ShortCode = "AddressType", Desc = "属地", ContextType = SysDic.All, Length = 10)]
        public virtual string AddressType
		{
			get { return _addressType; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for AddressType", value, value.ToString());
				_addressType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "诊断", ShortCode = "Diag", Desc = "诊断", ContextType = SysDic.All, Length = 255)]
        public virtual string Diag
		{
			get { return _diag; }
			set
			{
				if ( value != null && value.Length > 255)
					throw new ArgumentOutOfRangeException("Invalid value for Diag", value, value.ToString());
				_diag = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "体温", ShortCode = "BodyTPE", Desc = "体温", ContextType = SysDic.All, Length = 8)]
        public virtual double BodyTPE
		{
			get { return _bodyTPE; }
			set { _bodyTPE = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "血压", ShortCode = "BPress", Desc = "血压", ContextType = SysDic.All, Length = 8)]
        public virtual double BPress
		{
			get { return _bPress; }
			set { _bPress = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "呼吸", ShortCode = "Breathe", Desc = "呼吸", ContextType = SysDic.All, Length = 8)]
        public virtual double Breathe
		{
			get { return _breathe; }
			set { _breathe = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "脉搏", ShortCode = "Pulse", Desc = "脉搏", ContextType = SysDic.All, Length = 8)]
        public virtual double Pulse
		{
			get { return _pulse; }
			set { _pulse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "心率", ShortCode = "Heartrate", Desc = "心率", ContextType = SysDic.All, Length = 8)]
        public virtual double Heartrate
		{
			get { return _heartrate; }
			set { _heartrate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "尿量", ShortCode = "Urine", Desc = "尿量", ContextType = SysDic.All, Length = 8)]
        public virtual double Urine
		{
			get { return _urine; }
			set { _urine = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否在麻醉", ShortCode = "IsAnesth", Desc = "是否在麻醉", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsAnesth
		{
			get { return _isAnesth; }
			set { _isAnesth = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否有过敏史", ShortCode = "HasAllergy", Desc = "是否有过敏史", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasAllergy
		{
			get { return _hasAllergy; }
			set { _hasAllergy = value; }
		}

        [DataMember]
        [DataDesc(CName = "费用类型", ShortCode = "Costtype", Desc = "费用类型", ContextType = SysDic.All, Length = 50)]
        public virtual string Costtype
		{
			get { return _costtype; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Costtype", value, value.ToString());
				_costtype = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "移植史", ShortCode = "HasTransplant", Desc = "移植史", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasTransplant
		{
			get { return _hasTransplant; }
			set { _hasTransplant = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "移植日期", ShortCode = "Transdate", Desc = "移植日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Transdate
		{
			get { return _transdate; }
			set { _transdate = value; }
		}

        [DataMember]
        [DataDesc(CName = "供者血型", ShortCode = "DonorABORH", Desc = "供者血型", ContextType = SysDic.All, Length = 20)]
        public virtual string DonorABORH
		{
			get { return _donorABORH; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for DonorABORH", value, value.ToString());
				_donorABORH = value;
			}
		}

		#endregion
	}
	#endregion
}