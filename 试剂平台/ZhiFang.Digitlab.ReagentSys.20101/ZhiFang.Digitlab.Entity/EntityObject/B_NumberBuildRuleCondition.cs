using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BNumberBuildRuleCondition

	/// <summary>
	/// BNumberBuildRuleCondition object for NHibernate mapped table 'B_NumberBuildRuleCondition'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "号码生成规则条件", ClassCName = "BNumberBuildRuleCondition", ShortCode = "BNumberBuildRuleCondition", Desc = "号码生成规则条件")]
	public class BNumberBuildRuleCondition : BaseEntity
	{
		#region Member Variables
		
        protected string _conditionObject;
        protected long? _conditionObjectID;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BNumberBuildRule _bNumberBuildRule;

		#endregion

		#region Constructors

		public BNumberBuildRuleCondition() { }

		public BNumberBuildRuleCondition( long labID, string conditionObject, long conditionObjectID, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BNumberBuildRule bNumberBuildRule )
		{
			this._labID = labID;
			this._conditionObject = conditionObject;
			this._conditionObjectID = conditionObjectID;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bNumberBuildRule = bNumberBuildRule;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "条件对象", ShortCode = "ConditionObject", Desc = "条件对象", ContextType = SysDic.All, Length = 50)]
        public virtual string ConditionObject
		{
			get { return _conditionObject; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ConditionObject", value, value.ToString());
				_conditionObject = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "条件对象ID", ShortCode = "ConditionObjectID", Desc = "条件对象ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ConditionObjectID
		{
			get { return _conditionObjectID; }
			set { _conditionObjectID = value; }
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

        
		#endregion
	}
	#endregion
}