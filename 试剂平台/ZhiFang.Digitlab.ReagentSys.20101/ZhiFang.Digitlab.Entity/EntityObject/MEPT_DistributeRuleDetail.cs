using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTDistributeRuleDetail

	/// <summary>
	/// MEPTDistributeRuleDetail object for NHibernate mapped table 'MEPT_DistributeRuleDetail'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "分发规则明细", ClassCName = "MEPTDistributeRuleDetail", ShortCode = "MEPTDistributeRuleDetail", Desc = "分发规则明细")]
	public class MEPTDistributeRuleDetail : BaseEntity
	{
		#region Member Variables
		
        protected string _maxSampleNo;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected MEPTDistributeGroup _mEPTDistributeGroup;
		protected MEPTDistributeRule _mEPTDistributeRule;

		#endregion

		#region Constructors

		public MEPTDistributeRuleDetail() { }

		public MEPTDistributeRuleDetail( long labID, string maxSampleNo, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEPTDistributeGroup mEPTDistributeGroup, MEPTDistributeRule mEPTDistributeRule )
		{
			this._labID = labID;
			this._maxSampleNo = maxSampleNo;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEPTDistributeGroup = mEPTDistributeGroup;
			this._mEPTDistributeRule = mEPTDistributeRule;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "最大分发样本号", ShortCode = "MaxSampleNo", Desc = "最大分发样本号", ContextType = SysDic.All, Length = 50)]
        public virtual string MaxSampleNo
		{
			get { return _maxSampleNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MaxSampleNo", value, value.ToString());
				_maxSampleNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
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
        [DataDesc(CName = "分发小组", ShortCode = "MEPTDistributeGroup", Desc = "分发小组")]
		public virtual MEPTDistributeGroup MEPTDistributeGroup
		{
			get { return _mEPTDistributeGroup; }
			set { _mEPTDistributeGroup = value; }
		}

        [DataMember]
        [DataDesc(CName = "分发规则", ShortCode = "MEPTDistributeRule", Desc = "分发规则")]
		public virtual MEPTDistributeRule MEPTDistributeRule
		{
			get { return _mEPTDistributeRule; }
			set { _mEPTDistributeRule = value; }
		}

        
		#endregion
	}
	#endregion
}