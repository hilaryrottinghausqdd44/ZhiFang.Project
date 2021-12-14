using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBReqForm

	/// <summary>
	/// BloodBReqForm object for NHibernate mapped table 'Blood_BReqForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "用血申请主单表", ClassCName = "BloodBReqForm", ShortCode = "BloodBReqForm", Desc = "用血申请主单表")]
	public class BloodBReqForm : BaseEntity
	{
		#region Member Variables
		
        protected string _reqFormNo;
        protected string _bloodOrderID;
        protected string _hisOrderID;
        protected string _hisWardNo;
        protected long? _wardNo;
        protected string _wardCName;
        protected string _hisDeptNo;
        protected long? _deptNo;
        protected string _deptCName;
        protected bool _beforUse;
        protected DateTime? _useTime;
        protected long? _assessFormID;
        protected long? _reqBloodABONo;
        protected string _barCode;
        protected string _hisABOCode;
        protected long? _bloodABONo;
        protected string _patABO;
        protected string _hisrhCode;
        protected string _patRh;
        protected string _useWay;
        protected string _evaluation;
        protected double _reqTotal;
        protected string _hisDoctorId;
        protected long? _applyID;
        protected string _applyName;
        protected DateTime? _applyTime;
        protected string _applyMemo;
        protected long? _seniorID;
        protected string _seniorName;
        protected DateTime? _seniorTime;
        protected string _seniorMemo;
        protected long? _directorID;
        protected string _directorName;
        protected DateTime? _directorTime;
        protected string _directorMemo;
        protected long? _medicalID;
        protected string _medicalName;
        protected DateTime? _medicalTime;
        protected string _medicalMemo;
        protected long? _obsoleteID;
        protected string _obsoleteName;
        protected DateTime? _obsoleteTime;
        protected string _obsoleteMemo;
        protected bool _checkCompleteFlag;
        protected DateTime? _checkCompleteTime;
        protected long? _bTransReviewID;
        protected string _bTransReviewName;
        protected DateTime? _bTransReviewTime;
        protected int _bTransReviewFlag;
        protected long? _breqStatusID;
        protected string _breqStatusName;
        protected int _toHisFlag;
        protected string _bLPreEvaluation;
        protected int _printTotal;
        protected string _yizhuCode;
        protected string _memo;
        protected bool _visible;
        protected int _dispOrder;
		protected BDict _bReqType;
		protected BDict _useType;
		protected BloodPatinfo _bloodPatinfo;
		protected BDict _sickType;
		protected BDict _bloodWay;
		protected BDict _bTransReviewMemo;
		protected BDict _discord;
		protected BDict _bReqFormObsolete;
		protected BDict _usePurpose;
		#endregion

		#region Constructors

		public BloodBReqForm() { }

		public BloodBReqForm( long labID, string reqFormNo, string bloodOrderID, string hisOrderID, string hisWardNo, long wardNo, string wardCName, string hisDeptNo, long deptNo, string deptCName, bool beforUse, DateTime useTime, long assessFormID, long reqBloodABONo, string barCode, string hisABOCode, long bloodABONo, string patABO, string hisrhCode, string patRh, string useWay, string evaluation, double reqTotal, string hisDoctorId, long applyID, string applyName, DateTime applyTime, string applyMemo, long seniorID, string seniorName, DateTime seniorTime, string seniorMemo, long directorID, string directorName, DateTime directorTime, string directorMemo, long medicalID, string medicalName, DateTime medicalTime, string medicalMemo, long obsoleteID, string obsoleteName, DateTime obsoleteTime, string obsoleteMemo, bool checkCompleteFlag, DateTime checkCompleteTime, long bTransReviewID, string bTransReviewName, DateTime bTransReviewTime, int bTransReviewFlag, long breqStatusID, string breqStatusName, int toHisFlag, string bLPreEvaluation, int printTotal, string yizhuCode, string memo, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BDict bReqType, BDict useType, BloodPatinfo bloodPatinfo, BDict sickType, BDict bloodWayId, BDict bTransReviewMemoId, BDict discordNo, BDict bReqFormObsolete, BDict usePurposeId )
		{
			this._labID = labID;
			this._reqFormNo = reqFormNo;
			this._bloodOrderID = bloodOrderID;
			this._hisOrderID = hisOrderID;
			this._hisWardNo = hisWardNo;
			this._wardNo = wardNo;
			this._wardCName = wardCName;
			this._hisDeptNo = hisDeptNo;
			this._deptNo = deptNo;
			this._deptCName = deptCName;
			this._beforUse = beforUse;
			this._useTime = useTime;
			this._assessFormID = assessFormID;
			this._reqBloodABONo = reqBloodABONo;
			this._barCode = barCode;
			this._hisABOCode = hisABOCode;
			this._bloodABONo = bloodABONo;
			this._patABO = patABO;
			this._hisrhCode = hisrhCode;
			this._patRh = patRh;
			this._useWay = useWay;
			this._evaluation = evaluation;
			this._reqTotal = reqTotal;
			this._hisDoctorId = hisDoctorId;
			this._applyID = applyID;
			this._applyName = applyName;
			this._applyTime = applyTime;
			this._applyMemo = applyMemo;
			this._seniorID = seniorID;
			this._seniorName = seniorName;
			this._seniorTime = seniorTime;
			this._seniorMemo = seniorMemo;
			this._directorID = directorID;
			this._directorName = directorName;
			this._directorTime = directorTime;
			this._directorMemo = directorMemo;
			this._medicalID = medicalID;
			this._medicalName = medicalName;
			this._medicalTime = medicalTime;
			this._medicalMemo = medicalMemo;
			this._obsoleteID = obsoleteID;
			this._obsoleteName = obsoleteName;
			this._obsoleteTime = obsoleteTime;
			this._obsoleteMemo = obsoleteMemo;
			this._checkCompleteFlag = checkCompleteFlag;
			this._checkCompleteTime = checkCompleteTime;
			this._bTransReviewID = bTransReviewID;
			this._bTransReviewName = bTransReviewName;
			this._bTransReviewTime = bTransReviewTime;
			this._bTransReviewFlag = bTransReviewFlag;
			this._breqStatusID = breqStatusID;
			this._breqStatusName = breqStatusName;
			this._toHisFlag = toHisFlag;
			this._bLPreEvaluation = bLPreEvaluation;
			this._printTotal = printTotal;
			this._yizhuCode = yizhuCode;
			this._memo = memo;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bReqType = bReqType;
			this._useType = useType;
			this._bloodPatinfo = bloodPatinfo;
			this._sickType = sickType;
			this._bloodWay = bloodWayId;
			this._bTransReviewMemo = bTransReviewMemoId;
			this._discord = discordNo;
			this._bReqFormObsolete = bReqFormObsolete;
			this._usePurpose = usePurposeId;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "申请单号", ShortCode = "ReqFormNo", Desc = "申请单号", ContextType = SysDic.All, Length = 20)]
        public virtual string ReqFormNo
		{
			get { return _reqFormNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReqFormNo", value, value.ToString());
				_reqFormNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "BloodOrderID", ShortCode = "BloodOrderID", Desc = "BloodOrderID", ContextType = SysDic.All, Length = 10)]
        public virtual string BloodOrderID
		{
			get { return _bloodOrderID; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for BloodOrderID", value, value.ToString());
				_bloodOrderID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "存储his返回的申请单ID", ShortCode = "HisOrderID", Desc = "存储his返回的申请单ID", ContextType = SysDic.All, Length = 50)]
        public virtual string HisOrderID
		{
			get { return _hisOrderID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderID", value, value.ToString());
				_hisOrderID = value;
			}
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
        [DataDesc(CName = "病区名称", ShortCode = "WardCName", Desc = "病区名称", ContextType = SysDic.All, Length = 50)]
        public virtual string WardCName
		{
			get { return _wardCName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WardCName", value, value.ToString());
				_wardCName = value;
			}
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
        [DataDesc(CName = "科室名称", ShortCode = "DeptCName", Desc = "科室名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptCName
		{
			get { return _deptCName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeptCName", value, value.ToString());
				_deptCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "用血史", ShortCode = "BeforUse", Desc = "用血史", ContextType = SysDic.All, Length = 1)]
        public virtual bool BeforUse
		{
			get { return _beforUse; }
			set { _beforUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预定使用时间", ShortCode = "UseTime", Desc = "预定使用时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? UseTime
		{
			get { return _useTime; }
			set { _useTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "输血前评估号，关联输血前评估主单记录Id", ShortCode = "AssessFormID", Desc = "输血前评估号，关联输血前评估主单记录Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? AssessFormID
		{
			get { return _assessFormID; }
			set { _assessFormID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请血型编码", ShortCode = "ReqBloodABONo", Desc = "申请血型编码", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReqBloodABONo
		{
			get { return _reqBloodABONo; }
			set { _reqBloodABONo = value; }
		}

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "BarCode", Desc = "条码号", ContextType = SysDic.All, Length = 40)]
        public virtual string BarCode
		{
			get { return _barCode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
				_barCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "申请ABO血型", ShortCode = "HisABOCode", Desc = "申请ABO血型", ContextType = SysDic.All, Length = 20)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "患者血型编码", ShortCode = "BloodABONo", Desc = "患者血型编码", ContextType = SysDic.All, Length = 8)]
		public virtual long? BloodABONo
		{
			get { return _bloodABONo; }
			set { _bloodABONo = value; }
		}

        [DataMember]
        [DataDesc(CName = "患者ABO血型", ShortCode = "PatABO", Desc = "患者ABO血型", ContextType = SysDic.All, Length = 10)]
        public virtual string PatABO
		{
			get { return _patABO; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for PatABO", value, value.ToString());
				_patABO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "申请Rh(D)血型", ShortCode = "HisrhCode", Desc = "申请Rh(D)血型", ContextType = SysDic.All, Length = 20)]
        public virtual string HisrhCode
		{
			get { return _hisrhCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisrhCode", value, value.ToString());
				_hisrhCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "患者血型RhD血型", ShortCode = "PatRh", Desc = "患者血型RhD血型", ContextType = SysDic.All, Length = 10)]
        public virtual string PatRh
		{
			get { return _patRh; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for PatRh", value, value.ToString());
				_patRh = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "输血方式", ShortCode = "UseWay", Desc = "输血方式", ContextType = SysDic.All, Length = 20)]
        public virtual string UseWay
		{
			get { return _useWay; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UseWay", value, value.ToString());
				_useWay = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "输血前评估结果", ShortCode = "Evaluation", Desc = "输血前评估结果", ContextType = SysDic.All, Length = 255)]
        public virtual string Evaluation
		{
			get { return _evaluation; }
			set
			{
				if ( value != null && value.Length > 255)
					throw new ArgumentOutOfRangeException("Invalid value for Evaluation", value, value.ToString());
				_evaluation = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "24小时用血申请总量", ShortCode = "ReqTotal", Desc = "24小时用血申请总量", ContextType = SysDic.All, Length = 8)]
        public virtual double ReqTotal
		{
			get { return _reqTotal; }
			set { _reqTotal = value; }
		}

        [DataMember]
        [DataDesc(CName = "His申请医生ID", ShortCode = "HisDoctorId", Desc = "His申请医生ID", ContextType = SysDic.All, Length = 20)]
        public virtual string HisDoctorId
		{
			get { return _hisDoctorId; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisDoctorId", value, value.ToString());
				_hisDoctorId = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请人编码", ShortCode = "ApplyID", Desc = "申请人编码", ContextType = SysDic.All, Length = 8)]
		public virtual long? ApplyID
		{
			get { return _applyID; }
			set { _applyID = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请人", ShortCode = "ApplyName", Desc = "申请人", ContextType = SysDic.All, Length = 20)]
        public virtual string ApplyName
		{
			get { return _applyName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ApplyName", value, value.ToString());
				_applyName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请时间", ShortCode = "ApplyTime", Desc = "申请时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ApplyTime
		{
			get { return _applyTime; }
			set { _applyTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请备注", ShortCode = "ApplyMemo", Desc = "申请备注", ContextType = SysDic.All, Length = 200)]
        public virtual string ApplyMemo
		{
			get { return _applyMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ApplyMemo", value, value.ToString());
				_applyMemo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上级审核者ID", ShortCode = "SeniorID", Desc = "上级审核者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? SeniorID
		{
			get { return _seniorID; }
			set { _seniorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "上级审核者", ShortCode = "SeniorName", Desc = "上级审核者", ContextType = SysDic.All, Length = 20)]
        public virtual string SeniorName
		{
			get { return _seniorName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SeniorName", value, value.ToString());
				_seniorName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上级审核时间", ShortCode = "SeniorTime", Desc = "上级审核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? SeniorTime
		{
			get { return _seniorTime; }
			set { _seniorTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "上级审核者备注", ShortCode = "SeniorMemo", Desc = "上级审核者备注", ContextType = SysDic.All, Length = 200)]
        public virtual string SeniorMemo
		{
			get { return _seniorMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for SeniorMemo", value, value.ToString());
				_seniorMemo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "科主任审核者ID", ShortCode = "DirectorID", Desc = "科主任审核者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DirectorID
		{
			get { return _directorID; }
			set { _directorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "科主任审核者", ShortCode = "DirectorName", Desc = "科主任审核者", ContextType = SysDic.All, Length = 20)]
        public virtual string DirectorName
		{
			get { return _directorName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for DirectorName", value, value.ToString());
				_directorName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "科主任审核时间", ShortCode = "DirectorTime", Desc = "科主任审核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DirectorTime
		{
			get { return _directorTime; }
			set { _directorTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "科主任审核备注", ShortCode = "DirectorMemo", Desc = "科主任审核备注", ContextType = SysDic.All, Length = 200)]
        public virtual string DirectorMemo
		{
			get { return _directorMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for DirectorMemo", value, value.ToString());
				_directorMemo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医务部审核者ID", ShortCode = "MedicalID", Desc = "医务部审核者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? MedicalID
		{
			get { return _medicalID; }
			set { _medicalID = value; }
		}

        [DataMember]
        [DataDesc(CName = "医务部审核者", ShortCode = "MedicalName", Desc = "医务部审核者", ContextType = SysDic.All, Length = 20)]
        public virtual string MedicalName
		{
			get { return _medicalName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for MedicalName", value, value.ToString());
				_medicalName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医务部审核时间", ShortCode = "MedicalTime", Desc = "医务部审核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? MedicalTime
		{
			get { return _medicalTime; }
			set { _medicalTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "医务部审核备注", ShortCode = "MedicalMemo", Desc = "医务部审核备注", ContextType = SysDic.All, Length = 200)]
        public virtual string MedicalMemo
		{
			get { return _medicalMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for MedicalMemo", value, value.ToString());
				_medicalMemo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废人Id", ShortCode = "ObsoleteID", Desc = "作废人Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? ObsoleteID
		{
			get { return _obsoleteID; }
			set { _obsoleteID = value; }
		}

        [DataMember]
        [DataDesc(CName = "作废人", ShortCode = "ObsoleteName", Desc = "作废人", ContextType = SysDic.All, Length = 20)]
        public virtual string ObsoleteName
		{
			get { return _obsoleteName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ObsoleteName", value, value.ToString());
				_obsoleteName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "作废时间", ShortCode = "ObsoleteTime", Desc = "作废时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ObsoleteTime
		{
			get { return _obsoleteTime; }
			set { _obsoleteTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "作废备注", ShortCode = "ObsoleteMemo", Desc = "作废备注", ContextType = SysDic.All, Length = 200)]
        public virtual string ObsoleteMemo
		{
			get { return _obsoleteMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ObsoleteMemo", value, value.ToString());
				_obsoleteMemo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "审核完成标志", ShortCode = "CheckCompleteFlag", Desc = "审核完成标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool CheckCompleteFlag
		{
			get { return _checkCompleteFlag; }
			set { _checkCompleteFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核完成时间", ShortCode = "CheckCompleteTime", Desc = "审核完成时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckCompleteTime
		{
			get { return _checkCompleteTime; }
			set { _checkCompleteTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "输血科审核Id", ShortCode = "BTransReviewID", Desc = "输血科审核Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? BTransReviewID
		{
			get { return _bTransReviewID; }
			set { _bTransReviewID = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血科审核人", ShortCode = "BTransReviewName", Desc = "输血科审核人", ContextType = SysDic.All, Length = 20)]
        public virtual string BTransReviewName
		{
			get { return _bTransReviewName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BTransReviewName", value, value.ToString());
				_bTransReviewName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "输血科审核时间", ShortCode = "BTransReviewTime", Desc = "输血科审核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BTransReviewTime
		{
			get { return _bTransReviewTime; }
			set { _bTransReviewTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血科审核标志", ShortCode = "BTransReviewFlag", Desc = "输血科审核标志", ContextType = SysDic.All, Length = 4)]
        public virtual int BTransReviewFlag
		{
			get { return _bTransReviewFlag; }
			set { _bTransReviewFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请单状态ID", ShortCode = "BreqStatusID", Desc = "申请单状态ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BreqStatusID
		{
			get { return _breqStatusID; }
			set { _breqStatusID = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请单状态名称", ShortCode = "BreqStatusName", Desc = "申请单状态名称", ContextType = SysDic.All, Length = 20)]
        public virtual string BreqStatusName
		{
			get { return _breqStatusName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BreqStatusName", value, value.ToString());
				_breqStatusName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "HIS数据标志", ShortCode = "ToHisFlag", Desc = "HIS数据标志", ContextType = SysDic.All, Length = 4)]
        public virtual int ToHisFlag
		{
			get { return _toHisFlag; }
			set { _toHisFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血前评估", ShortCode = "BLPreEvaluation", Desc = "输血前评估", ContextType = SysDic.All, Length = 5000)]
        public virtual string BLPreEvaluation
		{
			get { return _bLPreEvaluation; }
			set
			{
				if ( value != null && value.Length > 5000)
					throw new ArgumentOutOfRangeException("Invalid value for BLPreEvaluation", value, value.ToString());
				_bLPreEvaluation = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintTotal", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTotal
		{
			get { return _printTotal; }
			set { _printTotal = value; }
		}

        [DataMember]
        [DataDesc(CName = "医嘱码", ShortCode = "YizhuCode", Desc = "医嘱码", ContextType = SysDic.All, Length = 50)]
        public virtual string YizhuCode
		{
			get { return _yizhuCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for YizhuCode", value, value.ToString());
				_yizhuCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "Memo", ShortCode = "Memo", Desc = "Memo", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请类型", ShortCode = "BReqType", Desc = "申请类型")]
		public virtual BDict BReqType
		{
			get { return _bReqType; }
			set { _bReqType = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血类型", ShortCode = "UseType", Desc = "用血类型")]
		public virtual BDict UseType
		{
			get { return _useType; }
			set { _useType = value; }
		}

        [DataMember]
        [DataDesc(CName = "病人就诊记录信息表", ShortCode = "BloodPatinfo", Desc = "病人就诊记录信息表")]
		public virtual BloodPatinfo BloodPatinfo
		{
			get { return _bloodPatinfo; }
			set { _bloodPatinfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "就诊类型", ShortCode = "SickType", Desc = "就诊类型")]
		public virtual BDict SickType
		{
			get { return _sickType; }
			set { _sickType = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血方式", ShortCode = "BloodWay", Desc = "用血方式")]
		public virtual BDict BloodWay
		{
			get { return _bloodWay; }
			set { _bloodWay = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血科审核(不通过)备注", ShortCode = "BTransReviewMemo", Desc = "输血科审核(不通过)备注")]
		public virtual BDict BTransReviewMemo
		{
			get { return _bTransReviewMemo; }
			set { _bTransReviewMemo = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血科审核不通过理由", ShortCode = "Discord", Desc = "输血科审核不通过理由")]
		public virtual BDict Discord
		{
			get { return _discord; }
			set { _discord = value; }
		}

        [DataMember]
        [DataDesc(CName = "医嘱作废原因", ShortCode = "BReqFormObsolete", Desc = "医嘱作废原因")]
		public virtual BDict BReqFormObsolete
		{
			get { return _bReqFormObsolete; }
			set { _bReqFormObsolete = value; }
		}

        [DataMember]
        [DataDesc(CName = "输血目的", ShortCode = "UsePurposeId", Desc = "输血目的")]
		public virtual BDict UsePurpose
		{
			get { return _usePurpose; }
			set { _usePurpose = value; }
		}

		#endregion
	}
	#endregion
}