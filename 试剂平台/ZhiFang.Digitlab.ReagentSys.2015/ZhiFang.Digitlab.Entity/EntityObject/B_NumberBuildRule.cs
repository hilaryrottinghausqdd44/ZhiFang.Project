using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BNumberBuildRule

	/// <summary>
	/// BNumberBuildRule object for NHibernate mapped table 'B_NumberBuildRule'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "号码生成规则", ClassCName = "BNumberBuildRule", ShortCode = "BNumberBuildRule", Desc = "号码生成规则")]
	public class BNumberBuildRule : BaseEntity
	{
		#region Member Variables
		
        protected int _numberType;
        protected int _ruleLevel;
        protected string _name;
        protected string _sName;
        protected int _isHasCondition;
        protected string _ruleDesc;
        protected string _ruleValue;
        protected string _ruleOrder;
        protected string _example;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected BMaxNumberToRule _bMaxNumberToRule; 
        protected IList<BNumberBuildRuleCondition> _bNumberBuildRuleConditionList; 

		#endregion

		#region Constructors

		public BNumberBuildRule() { }

        public BNumberBuildRule(long labID, int numberType, int ruleLevel, string name, string sName, int isHasCondition, string ruleDesc, string ruleValue, string ruleOrder, string example, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._numberType = numberType;
			this._ruleLevel = ruleLevel;
			this._name = name;
			this._sName = sName;
			this._isHasCondition = isHasCondition;
			this._ruleDesc = ruleDesc;
			this._ruleValue = ruleValue;
            this._ruleOrder = ruleOrder;
			this._example = example;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "号码类型", ShortCode = "NumberType", Desc = "号码类型", ContextType = SysDic.All, Length = 4)]
        public virtual int NumberType
		{
			get { return _numberType; }
			set { _numberType = value; }
		}

        [DataMember]
        [DataDesc(CName = "规则级别", ShortCode = "RuleLevel", Desc = "规则级别", ContextType = SysDic.All, Length = 4)]
        public virtual int RuleLevel
		{
			get { return _ruleLevel; }
			set { _ruleLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否有条件", ShortCode = "IsHasCondition", Desc = "是否有条件", ContextType = SysDic.All, Length = 4)]
        public virtual int IsHasCondition
		{
			get { return _isHasCondition; }
			set { _isHasCondition = value; }
		}

        [DataMember]
        [DataDesc(CName = "规则描述", ShortCode = "RuleDesc", Desc = "规则描述", ContextType = SysDic.All, Length = 1000000)]
        public virtual string RuleDesc
		{
			get { return _ruleDesc; }
			set
			{
                if (value != null && value.Length > 1000000)
					throw new ArgumentOutOfRangeException("Invalid value for RuleDesc", value, value.ToString());
				_ruleDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "规则内容", ShortCode = "RuleValue", Desc = "规则内容", ContextType = SysDic.All, Length = 200)]
        public virtual string RuleValue
		{
			get { return _ruleValue; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for RuleValue", value, value.ToString());
				_ruleValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "规则顺序", ShortCode = "RuleOrder", Desc = "规则顺序", ContextType = SysDic.All, Length = 20)]
        public virtual string RuleOrder
		{
			get { return _ruleOrder; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for RuleValue", value, value.ToString());
                _ruleOrder = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "示例", ShortCode = "Example", Desc = "示例", ContextType = SysDic.All, Length = 100)]
        public virtual string Example
		{
			get { return _example; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Example", value, value.ToString());
				_example = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 8000)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "规则最大号", ShortCode = "BMaxNumberToRuleList", Desc = "规则最大号")]
        public virtual BMaxNumberToRule BMaxNumberToRule
        {
            get { return _bMaxNumberToRule; }
            set { _bMaxNumberToRule = value; }
        }

        [DataMember]
        [DataDesc(CName = "号码生成规则条件", ShortCode = "BNumberBuildRuleConditionList", Desc = "号码生成规则条件")]
        public virtual IList<BNumberBuildRuleCondition> BNumberBuildRuleConditionList
        {
            get
            {
                if (_bNumberBuildRuleConditionList == null)
                {
                    _bNumberBuildRuleConditionList = new List<BNumberBuildRuleCondition>();
                }
                return _bNumberBuildRuleConditionList;
            }
            set { _bNumberBuildRuleConditionList = value; }
        }
		
		#endregion
	}
	#endregion
}