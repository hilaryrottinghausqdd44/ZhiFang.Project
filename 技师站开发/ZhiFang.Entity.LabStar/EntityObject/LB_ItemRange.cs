using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBItemRange

	/// <summary>
	/// 项目参考范围,LBItemRange object for NHibernate mapped table 'LB_ItemRange'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "项目参考范围", ClassCName = "LBItemRange", ShortCode = "LBItemRange", Desc = "LB_ItemRange项目参考范围")]
	public class LBItemRange : BaseEntity
	{
		#region Member Variables

		protected string _conditionName;
		protected long? _genderID;
		protected double? _lowAge;
		protected double? _highAge;
		protected DateTime? _bCollectTime;
		protected DateTime? _eCollectTime;
		protected bool _isDefault;
		protected string _lValueComp;
		protected double? _lValue;
		protected string _hValueComp;
		protected double? _hValue;
		protected string _refRange;
		protected string _lLValueComp;
		protected double? _lLValue;
		protected string _hHValueComp;
		protected double? _hHValue;
		protected string _diagMethod;
		protected string _unit;
		protected int? _manualSort;
		protected int _dispOrder;
		protected string _redoLValueComp;
		protected double? _redoLValue;
		protected string _redoHValueComp;
		protected double? _redoHValue;
		protected string _invalidLValueComp;
		protected double? _invalidLValue;
		protected string _invalidHValueComp;
		protected double? _invalidHValue;
		protected DateTime? _dataUpdateTime;
		protected long? _deptID;
		protected long? _sectionID;
		protected long? _equipID;
		protected long? _ageUnitID;
		protected long? _sampleTypeID;
		protected bool _bCritical;
		protected long? _diagID;
		protected long? _phyPeriodID;
		protected long? _collectPartID;
		protected LBItem _lBItem;


		#endregion

		#region Constructors

		public LBItemRange() { }

		public LBItemRange(string conditionName, long genderID, double lowAge, double highAge, DateTime bCollectTime, DateTime eCollectTime, bool isDefault, string lValueComp, double lValue, string hValueComp, double hValue, string refRange, string lLValueComp, double lLValue, string hHValueComp, double hHValue, string diagMethod, string unit, int manualSort, int dispOrder, string redoLValueComp, double redoLValue, string redoHValueComp, double redoHValue, string invalidLValueComp, double invalidLValue, string invalidHValueComp, double invalidHValue, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long deptID, long sectionID, long equipID, long ageUnitID, long sampleTypeID, bool bCritical, long diagID, long phyPeriodID, long collectPartID, LBItem lBItem)
		{
			this._conditionName = conditionName;
			this._genderID = genderID;
			this._lowAge = lowAge;
			this._highAge = highAge;
			this._bCollectTime = bCollectTime;
			this._eCollectTime = eCollectTime;
			this._isDefault = isDefault;
			this._lValueComp = lValueComp;
			this._lValue = lValue;
			this._hValueComp = hValueComp;
			this._hValue = hValue;
			this._refRange = refRange;
			this._lLValueComp = lLValueComp;
			this._lLValue = lLValue;
			this._hHValueComp = hHValueComp;
			this._hHValue = hHValue;
			this._diagMethod = diagMethod;
			this._unit = unit;
			this._manualSort = manualSort;
			this._dispOrder = dispOrder;
			this._redoLValueComp = redoLValueComp;
			this._redoLValue = redoLValue;
			this._redoHValueComp = redoHValueComp;
			this._redoHValue = redoHValue;
			this._invalidLValueComp = invalidLValueComp;
			this._invalidLValue = invalidLValue;
			this._invalidHValueComp = invalidHValueComp;
			this._invalidHValue = invalidHValue;
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._deptID = deptID;
			this._sectionID = sectionID;
			this._equipID = equipID;
			this._ageUnitID = ageUnitID;
			this._sampleTypeID = sampleTypeID;
			this._bCritical = bCritical;
			this._diagID = diagID;
			this._phyPeriodID = phyPeriodID;
			this._collectPartID = collectPartID;
			this._lBItem = lBItem;
		}

		#endregion

		#region Public Properties


		/// <summary>
		/// 条件说明
		/// </summary>
		[DataMember]
		[DataDesc(CName = "条件说明", ShortCode = "ConditionName", Desc = "条件说明", ContextType = SysDic.All, Length = 100)]
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
		/// 缺省
		/// </summary>
		[DataMember]
		[DataDesc(CName = "缺省", ShortCode = "IsDefault", Desc = "缺省", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsDefault
		{
			get { return _isDefault; }
			set { _isDefault = value; }
		}

		/// <summary>
		/// 范围低限对比类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "范围低限对比类型", ShortCode = "LValueComp", Desc = "范围低限对比类型", ContextType = SysDic.All, Length = 10)]
		public virtual string LValueComp
		{
			get { return _lValueComp; }
			set { _lValueComp = value; }
		}

		/// <summary>
		/// 范围低限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "范围低限", ShortCode = "LValue", Desc = "范围低限", ContextType = SysDic.All, Length = 8)]
		public virtual double? LValue
		{
			get { return _lValue; }
			set { _lValue = value; }
		}

		/// <summary>
		/// 范围高限对比类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "范围高限对比类型", ShortCode = "HValueComp", Desc = "范围高限对比类型", ContextType = SysDic.All, Length = 10)]
		public virtual string HValueComp
		{
			get { return _hValueComp; }
			set { _hValueComp = value; }
		}

		/// <summary>
		/// 范围高限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "范围高限", ShortCode = "HValue", Desc = "范围高限", ContextType = SysDic.All, Length = 8)]
		public virtual double? HValue
		{
			get { return _hValue; }
			set { _hValue = value; }
		}

		/// <summary>
		/// 参考范围描述
		/// </summary>
		[DataMember]
		[DataDesc(CName = "参考范围描述", ShortCode = "RefRange", Desc = "参考范围描述", ContextType = SysDic.All, Length = 400)]
		public virtual string RefRange
		{
			get { return _refRange; }
			set { _refRange = value; }
		}

		/// <summary>
		/// 异常低对比类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "异常低对比类型", ShortCode = "LLValueComp", Desc = "异常低对比类型", ContextType = SysDic.All, Length = 10)]
		public virtual string LLValueComp
		{
			get { return _lLValueComp; }
			set { _lLValueComp = value; }
		}

		/// <summary>
		/// 异常低限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "异常低限", ShortCode = "LLValue", Desc = "异常低限", ContextType = SysDic.All, Length = 8)]
		public virtual double? LLValue
		{
			get { return _lLValue; }
			set { _lLValue = value; }
		}

		/// <summary>
		/// 异常高限对比类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "异常高限对比类型", ShortCode = "HHValueComp", Desc = "异常高限对比类型", ContextType = SysDic.All, Length = 10)]
		public virtual string HHValueComp
		{
			get { return _hHValueComp; }
			set { _hHValueComp = value; }
		}

		/// <summary>
		/// 异常高限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "异常高限", ShortCode = "HHValue", Desc = "异常高限", ContextType = SysDic.All, Length = 8)]
		public virtual double? HHValue
		{
			get { return _hHValue; }
			set { _hHValue = value; }
		}

		/// <summary>
		/// 默认检验方法
		/// </summary>
		[DataMember]
		[DataDesc(CName = "默认检验方法", ShortCode = "DiagMethod", Desc = "默认检验方法", ContextType = SysDic.All, Length = 50)]
		public virtual string DiagMethod
		{
			get { return _diagMethod; }
			set { _diagMethod = value; }
		}

		/// <summary>
		/// 默认结果单位
		/// </summary>
		[DataMember]
		[DataDesc(CName = "默认结果单位", ShortCode = "Unit", Desc = "默认结果单位", ContextType = SysDic.All, Length = 50)]
		public virtual string Unit
		{
			get { return _unit; }
			set { _unit = value; }
		}

		/// <summary>
		/// 手动排序
		/// </summary>
		[DataMember]
		[DataDesc(CName = "手动排序", ShortCode = "ManualSort", Desc = "手动排序", ContextType = SysDic.All, Length = 4)]
		public virtual int? ManualSort
		{
			get { return _manualSort; }
			set { _manualSort = value; }
		}

		/// <summary>
		/// 判定次序
		/// </summary>
		[DataMember]
		[DataDesc(CName = "判定次序", ShortCode = "DispOrder", Desc = "判定次序", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		/// <summary>
		/// 复检低线对比类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "复检低线对比类型", ShortCode = "RedoLValueComp", Desc = "复检低线对比类型", ContextType = SysDic.All, Length = 10)]
		public virtual string RedoLValueComp
		{
			get { return _redoLValueComp; }
			set { _redoLValueComp = value; }
		}

		/// <summary>
		/// 复检低限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "复检低限", ShortCode = "RedoLValue", Desc = "复检低限", ContextType = SysDic.All, Length = 8)]
		public virtual double? RedoLValue
		{
			get { return _redoLValue; }
			set { _redoLValue = value; }
		}

		/// <summary>
		/// 复检高限对比类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "复检高限对比类型", ShortCode = "RedoHValueComp", Desc = "复检高限对比类型", ContextType = SysDic.All, Length = 10)]
		public virtual string RedoHValueComp
		{
			get { return _redoHValueComp; }
			set { _redoHValueComp = value; }
		}

		/// <summary>
		/// 复检高限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "复检高限", ShortCode = "RedoHValue", Desc = "复检高限", ContextType = SysDic.All, Length = 8)]
		public virtual double? RedoHValue
		{
			get { return _redoHValue; }
			set { _redoHValue = value; }
		}

		/// <summary>
		/// 无效低线对比类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "无效低线对比类型", ShortCode = "InvalidLValueComp", Desc = "无效低线对比类型", ContextType = SysDic.All, Length = 10)]
		public virtual string InvalidLValueComp
		{
			get { return _invalidLValueComp; }
			set { _invalidLValueComp = value; }
		}

		/// <summary>
		/// 无效低限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "无效低限", ShortCode = "InvalidLValue", Desc = "无效低限", ContextType = SysDic.All, Length = 8)]
		public virtual double? InvalidLValue
		{
			get { return _invalidLValue; }
			set { _invalidLValue = value; }
		}

		/// <summary>
		/// 无效高限对比类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "无效高限对比类型", ShortCode = "InvalidHValueComp", Desc = "无效高限对比类型", ContextType = SysDic.All, Length = 10)]
		public virtual string InvalidHValueComp
		{
			get { return _invalidHValueComp; }
			set { _invalidHValueComp = value; }
		}

		/// <summary>
		/// 无效高限
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "无效高限", ShortCode = "InvalidHValue", Desc = "无效高限", ContextType = SysDic.All, Length = 8)]
		public virtual double? InvalidHValue
		{
			get { return _invalidHValue; }
			set { _invalidHValue = value; }
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
		/// 检验小组ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检验小组ID", ShortCode = "SectionID", Desc = "小组ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? SectionID
		{
			get { return _sectionID; }
			set { _sectionID = value; }
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
		/// 报告危急值
		/// </summary>
		[DataMember]
		[DataDesc(CName = "报告危急值", ShortCode = "BCritical", Desc = "报告危急值", ContextType = SysDic.All, Length = 1)]
		public virtual bool BCritical
		{
			get { return _bCritical; }
			set { _bCritical = value; }
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
		/// 检验项目
		/// </summary>
		[DataMember]
		[DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
		public virtual LBItem LBItem
		{
			get { return _lBItem; }
			set { _lBItem = value; }
		}


		#endregion
	}
	#endregion
}