using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBTranRule

	/// <summary>
	/// LBTranRule object for NHibernate mapped table 'LB_TranRule'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "LBTranRule", ShortCode = "LBTranRule", Desc = "")]
	public class LBTranRule : BaseEntity
	{
		#region Member Variables

		protected string _cName;
		protected long? _sectionID;
		protected string _sampleNoMin;
		protected string _sampleNoMax;
		protected string _sampleNoRule;
		protected int _sampleNoType;
		protected string _nextSampleNo;
		protected bool _isUseNextNo;
		protected bool _isFollow;
		protected string _urgentState;
		protected DateTime? _useTimeMin;
		protected DateTime? _useTimeMax;
		protected long? _sickTypeID;
		protected long? _testTypeID;
		protected long? _sampleTypeID;
		protected long? _deptID;
		protected long? _samplingGroupID;
		protected long? _clientID;
		protected string _resetType;
		protected DateTime? _resetTime;
		protected string _tranWeek;
		protected string _tranToWeek;
		protected int _testDelayDates;
		protected bool _isAutoPrint;
		protected int _printCount;
		protected bool _isPrintProce;
		protected string _proceModel;
		protected string _dispenseMemo;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected LBTranRuleType _lBTranRuleType;

		#region 临时变量不作为数据库存储使用
		protected string _SickTypeName;
		[DataMember]
		public virtual string SickTypeName { get { return _SickTypeName; } set { _SickTypeName = value; } }
		protected string _SampleTypeName;
		[DataMember]
		public virtual string SampleTypeName { get { return _SampleTypeName; } set { _SampleTypeName = value; } }
		protected string _TestTypeName;
		[DataMember]
		public virtual string TestTypeName { get { return _TestTypeName; } set { _TestTypeName = value; } }
		protected string _DeptName;
		[DataMember]
		public virtual string DeptName { get { return _DeptName; } set { _DeptName = value; } }
		protected string _ClientName;
		[DataMember]
		public virtual string ClientName { get { return _ClientName; } set { _ClientName = value; } }
		protected string _SamplingGroupName;
		[DataMember]
		public virtual string SamplingGroupName { get { return _SamplingGroupName; } set { _SamplingGroupName = value; } }
		#endregion
		#endregion

		#region Constructors

		public LBTranRule() { }

		public LBTranRule(string cName, long sectionID, string sampleNoMin, string sampleNoMax, string sampleNoRule, int sampleNoType, string nextSampleNo, bool isUseNextNo, bool isFollow, string urgentState, DateTime useTimeMin, DateTime useTimeMax, long sickTypeID, long testTypeID, long sampleTypeID, long deptID, long samplingGroupID, long clientID, string resetType, DateTime resetTime, string tranWeek, string tranToWeek, int testDelayDates, bool isAutoPrint, int printCount, bool isPrintProce, string proceModel, string dispenseMemo, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBTranRuleType tranRuleType, string sickTypeName = null, string sampleTypeName = null, string testTypeName = null, string deptName = null, string clientName = null, string samplingGroupName = null)
		{
			this._cName = cName;
			this._sectionID = sectionID;
			this._sampleNoMin = sampleNoMin;
			this._sampleNoMax = sampleNoMax;
			this._sampleNoRule = sampleNoRule;
			this._sampleNoType = sampleNoType;
			this._nextSampleNo = nextSampleNo;
			this._isUseNextNo = isUseNextNo;
			this._isFollow = isFollow;
			this._urgentState = urgentState;
			this._useTimeMin = useTimeMin;
			this._useTimeMax = useTimeMax;
			this._sickTypeID = sickTypeID;
			this._testTypeID = testTypeID;
			this._sampleTypeID = sampleTypeID;
			this._deptID = deptID;
			this._samplingGroupID = samplingGroupID;
			this._clientID = clientID;
			this._resetType = resetType;
			this._resetTime = resetTime;
			this._tranWeek = tranWeek;
			this._tranToWeek = tranToWeek;
			this._testDelayDates = testDelayDates;
			this._isAutoPrint = isAutoPrint;
			this._printCount = printCount;
			this._isPrintProce = isPrintProce;
			this._proceModel = proceModel;
			this._dispenseMemo = dispenseMemo;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lBTranRuleType = tranRuleType;
			_SickTypeName = sickTypeName;
			_SampleTypeName = sampleTypeName;
			_TestTypeName = testTypeName;
			_DeptName = deptName;
			_ClientName = clientName;
			_SamplingGroupName = samplingGroupName;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "SectionID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? SectionID
		{
			get { return _sectionID; }
			set { _sectionID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleNoMin", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string SampleNoMin
		{
			get { return _sampleNoMin; }
			set { _sampleNoMin = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleNoMax", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string SampleNoMax
		{
			get { return _sampleNoMax; }
			set { _sampleNoMax = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleNoRule", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string SampleNoRule
		{
			get { return _sampleNoRule; }
			set { _sampleNoRule = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleNoType", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int SampleNoType
		{
			get { return _sampleNoType; }
			set { _sampleNoType = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "NextSampleNo", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string NextSampleNo
		{
			get { return _nextSampleNo; }
			set { _nextSampleNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsUseNextNo", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUseNextNo
		{
			get { return _isUseNextNo; }
			set { _isUseNextNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsFollow", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsFollow
		{
			get { return _isFollow; }
			set { _isFollow = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "UrgentState", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string UrgentState
		{
			get { return _urgentState; }
			set { _urgentState = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "UseTimeMin", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? UseTimeMin
		{
			get { return _useTimeMin; }
			set { _useTimeMin = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "UseTimeMax", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? UseTimeMax
		{
			get { return _useTimeMax; }
			set { _useTimeMax = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "SickTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? SickTypeID
		{
			get { return _sickTypeID; }
			set { _sickTypeID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "TestTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? TestTypeID
		{
			get { return _testTypeID; }
			set { _testTypeID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "SampleTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? SampleTypeID
		{
			get { return _sampleTypeID; }
			set { _sampleTypeID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "SamplingGroupID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? SamplingGroupID
		{
			get { return _samplingGroupID; }
			set { _samplingGroupID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ClientID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? ClientID
		{
			get { return _clientID; }
			set { _clientID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ResetType", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string ResetType
		{
			get { return _resetType; }
			set { _resetType = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ResetTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ResetTime
		{
			get { return _resetTime; }
			set { _resetTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "TranWeek", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string TranWeek
		{
			get { return _tranWeek; }
			set { _tranWeek = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "TranToWeek", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string TranToWeek
		{
			get { return _tranToWeek; }
			set { _tranToWeek = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "TestDelayDates", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int TestDelayDates
		{
			get { return _testDelayDates; }
			set { _testDelayDates = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsAutoPrint", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsAutoPrint
		{
			get { return _isAutoPrint; }
			set { _isAutoPrint = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int PrintCount
		{
			get { return _printCount; }
			set { _printCount = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsPrintProce", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsPrintProce
		{
			get { return _isPrintProce; }
			set { _isPrintProce = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ProceModel", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string ProceModel
		{
			get { return _proceModel; }
			set { _proceModel = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DispenseMemo", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string DispenseMemo
		{
			get { return _dispenseMemo; }
			set { _dispenseMemo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "TranRuleType", Desc = "")]
		public virtual LBTranRuleType LBTranRuleType
		{
			get { return _lBTranRuleType; }
			set { _lBTranRuleType = value; }
		}


		#endregion
	}
	#endregion
}