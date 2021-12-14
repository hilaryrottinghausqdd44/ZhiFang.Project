using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BSampleNumberBuildRuleCondition

	/// <summary>
	/// BSampleNumberBuildRuleCondition object for NHibernate mapped table 'B_SampleNumberBuildRuleCondition'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "样本号生成规则条件", ClassCName = "BSampleNumberBuildRuleCondition", ShortCode = "BSampleNumberBuildRuleCondition", Desc = "样本号生成规则条件")]
	public class BSampleNumberBuildRuleCondition : BaseEntity
	{
		#region Member Variables

        protected string _name;
        protected string _itemIDList;
        protected int _ruleLevel;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BNumberBuildRule _bNumberBuildRule;
		protected BSampleType _bSampleType;
		protected BSickType _bSickType;
		protected EPBEquip _ePBEquip;
		protected GMGroup _gMGroup;

		#endregion

		#region Constructors

		public BSampleNumberBuildRuleCondition() { }

		public BSampleNumberBuildRuleCondition( long labID, string itemIDList, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BNumberBuildRule bNumberBuildRule, BSampleType bSampleType, BSickType bSickType, EPBEquip ePBEquip, GMGroup gMGroup )
		{
			this._labID = labID;
			this._itemIDList = itemIDList;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bNumberBuildRule = bNumberBuildRule;
			this._bSampleType = bSampleType;
			this._bSickType = bSickType;
			this._ePBEquip = ePBEquip;
			this._gMGroup = gMGroup;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "条件名称", ShortCode = "Name", Desc = "条件名称", ContextType = SysDic.All, Length = 100)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                _name = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "项目ID列表,逗号分隔", ShortCode = "ItemIDList", Desc = "项目ID列表,逗号分隔", ContextType = SysDic.All, Length = 1000)]
        public virtual string ItemIDList
		{
			get { return _itemIDList; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for ItemIDList", value, value.ToString());
				_itemIDList = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "规则优先级", ShortCode = "RuleLevel", Desc = "规则优先级", ContextType = SysDic.All, Length = 4)]
        public virtual int RuleLevel
        {
            get { return _ruleLevel; }
            set { _ruleLevel = value; }
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
        [DataDesc(CName = "号码生成规则", ShortCode = "BNumberBuildRule", Desc = "号码生成规则")]
		public virtual BNumberBuildRule BNumberBuildRule
		{
			get { return _bNumberBuildRule; }
			set { _bNumberBuildRule = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
		public virtual BSampleType BSampleType
		{
			get { return _bSampleType; }
			set { _bSampleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "就诊类型", ShortCode = "BSickType", Desc = "就诊类型")]
		public virtual BSickType BSickType
		{
			get { return _bSickType; }
			set { _bSickType = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器表", ShortCode = "EPBEquip", Desc = "仪器表")]
		public virtual EPBEquip EPBEquip
		{
			get { return _ePBEquip; }
			set { _ePBEquip = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
		public virtual GMGroup GMGroup
		{
			get { return _gMGroup; }
			set { _gMGroup = value; }
		}

        
		#endregion
	}
	#endregion
}