using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BImmCalcTemplate

	/// <summary>
	/// BImmCalcTemplate object for NHibernate mapped table 'B_ImmCalcTemplate'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "定性计算公式表：用于存储定性计算公式的具体内容", ClassCName = "BImmCalcTemplate", ShortCode = "BImmCalcTemplate", Desc = "定性计算公式表：用于存储定性计算公式的具体内容")]
	public class BImmCalcTemplate : BaseEntity
	{
		#region Member Variables
		
        protected string _calcFormula;
        protected string _calcFormulaShortName;
        protected string _reMarke;
        protected int _dispOrder;
        protected int _isUsed;
        protected DateTime? _dataUpdateTime;
		protected IList<BImmDxCalcRule> _bImmDxCalcRuleList; 
		protected IList<BImmDxCalcVarReplace> _bImmDxCalcVarReplaceList; 
		protected IList<BImmItemCalcInfo> _bImmItemCalcInfoList; 

		#endregion

		#region Constructors

		public BImmCalcTemplate() { }

		public BImmCalcTemplate( long labID, string calcFormula, string calcFormulaShortName, string reMarke, int dispOrder, int isUsed, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._calcFormula = calcFormula;
			this._calcFormulaShortName = calcFormulaShortName;
			this._reMarke = reMarke;
			this._dispOrder = dispOrder;
			this._isUsed = isUsed;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "定性计算公式", ShortCode = "CalcFormula", Desc = "定性计算公式", ContextType = SysDic.All, Length = 60)]
        public virtual string CalcFormula
		{
			get { return _calcFormula; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for CalcFormula", value, value.ToString());
				_calcFormula = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定性计算公式简称", ShortCode = "CalcFormulaShortName", Desc = "定性计算公式简称", ContextType = SysDic.All, Length = 60)]
        public virtual string CalcFormulaShortName
		{
			get { return _calcFormulaShortName; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for CalcFormulaShortName", value, value.ToString());
				_calcFormulaShortName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "公式描述", ShortCode = "ReMarke", Desc = "公式描述", ContextType = SysDic.All, Length = 200)]
        public virtual string ReMarke
		{
			get { return _reMarke; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReMarke", value, value.ToString());
				_reMarke = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否在用:默认值为1", ShortCode = "IsUsed", Desc = "是否在用:默认值为1", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUsed
		{
			get { return _isUsed; }
			set { _isUsed = value; }
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
        [DataDesc(CName = "定性结果判断规则表：用于存储定性计算判断规则的具体内容。", ShortCode = "BImmDxCalcRuleList", Desc = "定性结果判断规则表：用于存储定性计算判断规则的具体内容。")]
		public virtual IList<BImmDxCalcRule> BImmDxCalcRuleList
		{
			get
			{
				if (_bImmDxCalcRuleList==null)
				{
					_bImmDxCalcRuleList = new List<BImmDxCalcRule>();
				}
				return _bImmDxCalcRuleList;
			}
			set { _bImmDxCalcRuleList = value; }
		}

        [DataMember]
        [DataDesc(CName = "定性计算变量替换表：用于存储定性计算过程中的量值替换.", ShortCode = "BImmDxCalcVarReplaceList", Desc = "定性计算变量替换表：用于存储定性计算过程中的量值替换.")]
		public virtual IList<BImmDxCalcVarReplace> BImmDxCalcVarReplaceList
		{
			get
			{
				if (_bImmDxCalcVarReplaceList==null)
				{
					_bImmDxCalcVarReplaceList = new List<BImmDxCalcVarReplace>();
				}
				return _bImmDxCalcVarReplaceList;
			}
			set { _bImmDxCalcVarReplaceList = value; }
		}

        [DataMember]
        [DataDesc(CName = "酶免项目计算模式表：用于存储酶标仪检验项目的计算模式的相关信息", ShortCode = "BImmItemCalcInfoList", Desc = "酶免项目计算模式表：用于存储酶标仪检验项目的计算模式的相关信息")]
		public virtual IList<BImmItemCalcInfo> BImmItemCalcInfoList
		{
			get
			{
				if (_bImmItemCalcInfoList==null)
				{
					_bImmItemCalcInfoList = new List<BImmItemCalcInfo>();
				}
				return _bImmItemCalcInfoList;
			}
			set { _bImmItemCalcInfoList = value; }
		}

        
		#endregion
	}
	#endregion
}