using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBExpertRule

	/// <summary>
	/// 专家规则,LBExpertRule object for NHibernate mapped table 'LB_ExpertRule'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "专家规则", ClassCName = "LBExpertRule", ShortCode = "LBExpertRule", Desc = "专家规则")]
	public class LBExpertRule : BaseEntity
	{
		#region Member Variables

		protected string _cName;
		protected string _gName;
		protected int _resultType;
		protected int _ruleRelation;
		protected string _itemAlarmInfo;
		protected int _alarmLevel;
		protected string _alarmInfo;
		protected string _conditionName;
		protected long? _genderID;
		protected long? _equipID;
		protected long? _deptID;
		protected long? _sampleTypeID;
		protected double? _lowAge;
		protected double? _highAge;
		protected long? _ageUnitID;
		protected DateTime? _bCollectTime;
		protected DateTime? _eCollectTime;
		protected long? _diagID;
		protected long? _phyPeriodID;
		protected long? _collectPartID;
		protected string _comment;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected LBItem _lBItem;
		protected LBSection _lBSection;

		#endregion

		#region Constructors

		public LBExpertRule() { }

		public LBExpertRule(long labID, string cName, string gName, int resultType, int ruleRelation, string itemAlarmInfo, int alarmLevel, string alarmInfo, string conditionName, long genderID, long equipID, long deptID, long sampleTypeID, double lowAge, double highAge, long ageUnitID, DateTime bCollectTime, DateTime eCollectTime, long diagID, long phyPeriodID, long collectPartID, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem lBItem, LBSection lBSection)
		{
			this._labID = labID;
			this._cName = cName;
			this._gName = gName;
			this._resultType = resultType;
			this._ruleRelation = ruleRelation;
			this._itemAlarmInfo = itemAlarmInfo;
			this._alarmLevel = alarmLevel;
			this._alarmInfo = alarmInfo;
			this._conditionName = conditionName;
			this._genderID = genderID;
			this._equipID = equipID;
			this._deptID = deptID;
			this._sampleTypeID = sampleTypeID;
			this._lowAge = lowAge;
			this._highAge = highAge;
			this._ageUnitID = ageUnitID;
			this._bCollectTime = bCollectTime;
			this._eCollectTime = eCollectTime;
			this._diagID = diagID;
			this._phyPeriodID = phyPeriodID;
			this._collectPartID = collectPartID;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lBItem = lBItem;
			this._lBSection = lBSection;
		}

		#endregion

		#region Public Properties


		/// <summary>
		/// 专家规则名称
		/// </summary>
		[DataMember]
		[DataDesc(CName = "专家规则名称", ShortCode = "CName", Desc = "专家规则名称", ContextType = SysDic.All, Length = 400)]
		public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

		/// <summary>
		/// 专家规则组名称
		/// </summary>
		[DataMember]
		[DataDesc(CName = "专家规则组名称", ShortCode = "GName", Desc = "专家规则组名称(组内规则互斥=or关系)", ContextType = SysDic.All, Length = 200)]
		public virtual string GName
		{
			get { return _gName; }
			set { _gName = value; }
		}

		/// <summary>
		/// 0：常规结果（默认） 1：微生物结果
		/// </summary>
		[DataMember]
		[DataDesc(CName = "0：常规结果（默认） 1：微生物结果", ShortCode = "ResultType", Desc = "0：常规结果（默认） 1：微生物结果", ContextType = SysDic.All, Length = 4)]
		public virtual int ResultType
		{
			get { return _resultType; }
			set { _resultType = value; }
		}

		/// <summary>
		/// 0：全部满足(and)
		/// </summary>
		[DataMember]
		[DataDesc(CName = "0：全部满足(and)", ShortCode = "RuleRelation", Desc = "0：全部满足(and)", ContextType = SysDic.All, Length = 4)]
		public virtual int RuleRelation
		{
			get { return _ruleRelation; }
			set { _ruleRelation = value; }
		}

		/// <summary>
		/// 警示提示
		/// </summary>
		[DataMember]
		[DataDesc(CName = "警示提示", ShortCode = "ItemAlarmInfo", Desc = "警示提示", ContextType = SysDic.All, Length = 50)]
		public virtual string ItemAlarmInfo
		{
			get { return _itemAlarmInfo; }
			set { _itemAlarmInfo = value; }
		}

		/// <summary>
		/// 警示级别 枚举：0：正常 1：警示 2：警告 3：严重警告 4：危急 5：错误
		/// </summary>
		[DataMember]
		[DataDesc(CName = "警示级别 枚举：0：正常 1：警示 2：警告 3：严重警告 4：危急 5：错误", ShortCode = "AlarmLevel", Desc = "警示级别 枚举：0：正常 1：警示 2：警告 3：严重警告 4：危急 5：错误", ContextType = SysDic.All, Length = 4)]
		public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

		/// <summary>
		/// 警示提示
		/// </summary>
		[DataMember]
		[DataDesc(CName = "警示提示", ShortCode = "AlarmInfo", Desc = "警示提示", ContextType = SysDic.All, Length = 50)]
		public virtual string AlarmInfo
		{
			get { return _alarmInfo; }
			set { _alarmInfo = value; }
		}

		/// <summary>
		/// 条件说明
		/// </summary>
		[DataMember]
		[DataDesc(CName = "条件说明", ShortCode = "ConditionName", Desc = "条件说明", ContextType = SysDic.All, Length = 200)]
		public virtual string ConditionName
		{
			get { return _conditionName; }
			set { _conditionName = value; }
		}

		/// <summary>
		/// 性别ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "性别ID", ShortCode = "GenderID", Desc = "性别ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? GenderID
		{
			get { return _genderID; }
			set { _genderID = value; }
		}

		/// <summary>
		/// 检验仪器ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检验仪器ID", ShortCode = "EquipID", Desc = "检验仪器ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipID
		{
			get { return _equipID; }
			set { _equipID = value; }
		}

		/// <summary>
		/// 送检科室ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "送检科室ID", ShortCode = "DeptID", Desc = "送检科室ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

		/// <summary>
		/// 样本类型
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "样本类型", ShortCode = "SampleTypeID", Desc = "样本类型", ContextType = SysDic.All, Length = 8)]
		public virtual long? SampleTypeID
		{
			get { return _sampleTypeID; }
			set { _sampleTypeID = value; }
		}

		/// <summary>
		/// 年龄低限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄低限", ShortCode = "LowAge", Desc = "年龄低限", ContextType = SysDic.All, Length = 8)]
		public virtual double? LowAge
		{
			get { return _lowAge; }
			set { _lowAge = value; }
		}

		/// <summary>
		/// 年龄高限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄高限", ShortCode = "HighAge", Desc = "年龄高限", ContextType = SysDic.All, Length = 8)]
		public virtual double? HighAge
		{
			get { return _highAge; }
			set { _highAge = value; }
		}

		/// <summary>
		/// 年龄单位
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "年龄单位", ShortCode = "AgeUnitID", Desc = "年龄单位", ContextType = SysDic.All, Length = 8)]
		public virtual long? AgeUnitID
		{
			get { return _ageUnitID; }
			set { _ageUnitID = value; }
		}

		/// <summary>
		/// 采样开始时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样开始时间", ShortCode = "BCollectTime", Desc = "采样开始时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BCollectTime
		{
			get { return _bCollectTime; }
			set { _bCollectTime = value; }
		}

		/// <summary>
		/// 采样截止时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样截止时间", ShortCode = "ECollectTime", Desc = "采样截止时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ECollectTime
		{
			get { return _eCollectTime; }
			set { _eCollectTime = value; }
		}

		/// <summary>
		/// 临床诊断ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "临床诊断ID", ShortCode = "DiagID", Desc = "临床诊断ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DiagID
		{
			get { return _diagID; }
			set { _diagID = value; }
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
		/// 备注
		/// </summary>
		[DataMember]
		[DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
		public virtual string Comment
		{
			get { return _comment; }
			set { _comment = value; }
		}

		/// <summary>
		/// 是否采用
		/// </summary>
		[DataMember]
		[DataDesc(CName = "是否采用", ShortCode = "IsUse", Desc = "是否采用", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		/// <summary>
		/// 规则次序
		/// </summary>
		[DataMember]
		[DataDesc(CName = "规则次序", ShortCode = "DispOrder", Desc = "规则次序", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
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

		/// <summary>
		/// 检验项目
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验项目", ShortCode = "LBItem", Desc = "检验项目")]
		public virtual LBItem LBItem
		{
			get { return _lBItem; }
			set { _lBItem = value; }
		}

		/// <summary>
		/// 检验小组
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验小组", ShortCode = "LBSection", Desc = "检验小组")]
		public virtual LBSection LBSection
		{
			get { return _lBSection; }
			set { _lBSection = value; }
		}

		/// <summary>
		/// 临时字段---专家规则明细信息
		/// </summary>
		[DataMember]
		[DataDesc(CName = "专家规则明细信息", ShortCode = "LBSection", Desc = "专家规则明细信息")]
		public virtual string ExpertRuleListInfo { get; set; }


		#endregion
	}
	#endregion
}