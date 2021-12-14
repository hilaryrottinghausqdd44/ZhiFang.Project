using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBExpertRuleList

	/// <summary>
	/// 专家规则明细,LBExpertRuleList object for NHibernate mapped table 'LB_ExpertRuleList'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "专家规则明细", ClassCName = "LBExpertRuleList", ShortCode = "LBExpertRuleList", Desc = "专家规则明细")]
	public class LBExpertRuleList : BaseEntity
	{
		#region Member Variables

		protected string _ruleName;
		protected string _sysRuleName;
		protected long? _itemID;
		protected string _itemName;
		protected int _valueFieldType;
		protected bool _bRelatedItemValue;
		protected bool _bHisItemValue;
		protected bool _bCalcItemValue;
		protected int _valueType;
		protected int _compType;
		protected string _compValue;
		protected double? _compFValue;
		protected double? _compFValue2;
		protected bool _bValue;
		protected bool _bLastHisComp;
		protected bool _bLimitHisDate;
		protected int? _limitHisDate;
		protected string _calcFormula;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected LBExpertRule _lBExpertRule;

		#endregion

		#region Constructors

		public LBExpertRuleList() { }

		public LBExpertRuleList(long labID, string ruleName, string sysRuleName, long itemID, string itemName, int valueFieldType, bool bRelatedItemValue, bool bHisItemValue, bool bCalcItemValue, int valueType, int compType, string compValue, double compFValue, double compFValue2, bool bValue, bool bLastHisComp, bool bLimitHisDate, int limitHisDate, string calcFormula, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBExpertRule lBExpertRule)
		{
			this._labID = labID;
			this._ruleName = ruleName;
			this._sysRuleName = sysRuleName;
			this._itemID = itemID;
			this._itemName = itemName;
			this._valueFieldType = valueFieldType;
			this._bRelatedItemValue = bRelatedItemValue;
			this._bHisItemValue = bHisItemValue;
			this._bCalcItemValue = bCalcItemValue;
			this._valueType = valueType;
			this._compType = compType;
			this._compValue = compValue;
			this._compFValue = compFValue;
			this._compFValue2 = compFValue2;
			this._bValue = bValue;
			this._bLastHisComp = bLastHisComp;
			this._bLimitHisDate = bLimitHisDate;
			this._limitHisDate = limitHisDate;
			this._calcFormula = calcFormula;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lBExpertRule = lBExpertRule;
		}

		#endregion

		#region Public Properties


		/// <summary>
		/// 规则细则说明
		/// </summary>
		[DataMember]
		[DataDesc(CName = "规则细则说明", ShortCode = "RuleName", Desc = "规则细则说明", ContextType = SysDic.All, Length = 200)]
		public virtual string RuleName
		{
			get { return _ruleName; }
			set { _ruleName = value; }
		}

		/// <summary>
		/// 系统规则细则说明
		/// </summary>
		[DataMember]
		[DataDesc(CName = "系统规则细则说明", ShortCode = "SysRuleName", Desc = "系统规则细则说明", ContextType = SysDic.All, Length = 200)]
		public virtual string SysRuleName
		{
			get { return _sysRuleName; }
			set { _sysRuleName = value; }
		}

		/// <summary>
		/// 项目ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "项目ID", ShortCode = "ItemID", Desc = "项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ItemID
		{
			get { return _itemID; }
			set { _itemID = value; }
		}

		/// <summary>
		/// 项目名称
		/// </summary>
		[DataMember]
		[DataDesc(CName = "项目名称", ShortCode = "ItemName", Desc = "项目名称", ContextType = SysDic.All, Length = 40)]
		public virtual string ItemName
		{
			get { return _itemName; }
			set { _itemName = value; }
		}

		/// <summary>
		/// 枚举，定量结果 报告值 结果状态等
		/// </summary>
		[DataMember]
		[DataDesc(CName = "枚举，定量结果 报告值 结果状态等", ShortCode = "ValueFieldType", Desc = "枚举，定量结果 报告值 结果状态等", ContextType = SysDic.All, Length = 4)]
		public virtual int ValueFieldType
		{
			get { return _valueFieldType; }
			set { _valueFieldType = value; }
		}

		/// <summary>
		/// 相关检验结果
		/// </summary>
		[DataMember]
		[DataDesc(CName = "相关检验结果", ShortCode = "BRelatedItemValue", Desc = "相关检验结果", ContextType = SysDic.All, Length = 1)]
		public virtual bool BRelatedItemValue
		{
			get { return _bRelatedItemValue; }
			set { _bRelatedItemValue = value; }
		}

		/// <summary>
		/// 历史检验结果
		/// </summary>
		[DataMember]
		[DataDesc(CName = "历史检验结果", ShortCode = "BHisItemValue", Desc = "历史检验结果", ContextType = SysDic.All, Length = 1)]
		public virtual bool BHisItemValue
		{
			get { return _bHisItemValue; }
			set { _bHisItemValue = value; }
		}

		/// <summary>
		/// 计算结果
		/// </summary>
		[DataMember]
		[DataDesc(CName = "计算结果", ShortCode = "BCalcItemValue", Desc = "计算结果", ContextType = SysDic.All, Length = 1)]
		public virtual bool BCalcItemValue
		{
			get { return _bCalcItemValue; }
			set { _bCalcItemValue = value; }
		}

		/// <summary>
		/// 枚举，定量 文本 存在
		/// </summary>
		[DataMember]
		[DataDesc(CName = "枚举，定量 文本 存在", ShortCode = "ValueType", Desc = "枚举，定量 文本 存在", ContextType = SysDic.All, Length = 4)]
		public virtual int ValueType
		{
			get { return _valueType; }
			set { _valueType = value; }
		}

		/// <summary>
		/// 枚举，> < = 等
		/// </summary>
		[DataMember]
		[DataDesc(CName = "枚举，> < = 等", ShortCode = "CompType", Desc = "枚举，> < = 等", ContextType = SysDic.All, Length = 4)]
		public virtual int CompType
		{
			get { return _compType; }
			set { _compType = value; }
		}

		/// <summary>
		/// 对比值
		/// </summary>
		[DataMember]
		[DataDesc(CName = "对比值", ShortCode = "CompValue", Desc = "对比值", ContextType = SysDic.All, Length = 100)]
		public virtual string CompValue
		{
			get { return _compValue; }
			set { _compValue = value; }
		}

		/// <summary>
		/// 对比数值
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "对比数值", ShortCode = "CompFValue", Desc = "对比数值", ContextType = SysDic.All, Length = 8)]
		public virtual double? CompFValue
		{
			get { return _compFValue; }
			set { _compFValue = value; }
		}

		/// <summary>
		/// 对比数值2
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "对比数值2", ShortCode = "CompFValue2", Desc = "对比数值2", ContextType = SysDic.All, Length = 8)]
		public virtual double? CompFValue2
		{
			get { return _compFValue2; }
			set { _compFValue2 = value; }
		}

		/// <summary>
		/// 对比内容存在
		/// </summary>
		[DataMember]
		[DataDesc(CName = "对比内容存在", ShortCode = "BValue", Desc = "对比内容存在", ContextType = SysDic.All, Length = 1)]
		public virtual bool BValue
		{
			get { return _bValue; }
			set { _bValue = value; }
		}

		/// <summary>
		/// 仅判断最近一次历史结果
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仅判断最近一次历史结果", ShortCode = "BLastHisComp", Desc = "仅判断最近一次历史结果", ContextType = SysDic.All, Length = 1)]
		public virtual bool BLastHisComp
		{
			get { return _bLastHisComp; }
			set { _bLastHisComp = value; }
		}

		/// <summary>
		/// 限制历史对比日期
		/// </summary>
		[DataMember]
		[DataDesc(CName = "限制历史对比日期", ShortCode = "BLimitHisDate", Desc = "限制历史对比日期", ContextType = SysDic.All, Length = 1)]
		public virtual bool BLimitHisDate
		{
			get { return _bLimitHisDate; }
			set { _bLimitHisDate = value; }
		}

		/// <summary>
		/// 历史对比日期
		/// </summary>
		[DataMember]
		[DataDesc(CName = "历史对比日期", ShortCode = "LimitHisDate", Desc = "历史对比日期", ContextType = SysDic.All, Length = 4)]
		public virtual int? LimitHisDate
		{
			get { return _limitHisDate; }
			set { _limitHisDate = value; }
		}

		/// <summary>
		/// 计算公式 
		/// </summary>
		[DataMember]
		[DataDesc(CName = "计算公式 ", ShortCode = "CalcFormula", Desc = "计算公式 ", ContextType = SysDic.All, Length = 300)]
		public virtual string CalcFormula
		{
			get { return _calcFormula; }
			set { _calcFormula = value; }
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
		/// 
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
		/// 
		/// </summary>
		[DataMember]
		[DataDesc(CName = "专家规则", ShortCode = "LBExpertRule", Desc = "专家规则")]
		public virtual LBExpertRule LBExpertRule
		{
			get { return _lBExpertRule; }
			set { _lBExpertRule = value; }
		}


		#endregion
	}
	#endregion
}