using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMaxNumberToRule

	/// <summary>
	/// BMaxNumberToRule object for NHibernate mapped table 'B_MaxNumberToRule'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "规则最大号", ClassCName = "BMaxNumberToRule", ShortCode = "BMaxNumberToRule", Desc = "规则最大号")]
	public class BMaxNumberToRule : BaseEntity
	{
		#region Member Variables
		
        protected DateTime? _startDate;
        protected string _startNumber;
        protected string _maxNumber;
        protected string _ruleNumber;
        protected DateTime? _dataUpdateTime;
		protected BNumberBuildRule _bNumberBuildRule;

		#endregion

		#region Constructors

		public BMaxNumberToRule() { }

		public BMaxNumberToRule( long labID, DateTime startDate, string startNumber, string maxNumber, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BNumberBuildRule bNumberBuildRule )
		{
			this._labID = labID;
			this._startDate = startDate;
			this._startNumber = startNumber;
			this._maxNumber = maxNumber;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bNumberBuildRule = bNumberBuildRule;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "起始日期", ShortCode = "StartDate", Desc = "起始日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? StartDate
		{
			get { return _startDate; }
			set { _startDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "起始号", ShortCode = "StartNumber", Desc = "起始号", ContextType = SysDic.All, Length = 100)]
        public virtual string StartNumber
		{
			get { return _startNumber; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for StartNumber", value, value.ToString());
				_startNumber = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "最大号", ShortCode = "MaxNumber", Desc = "最大号", ContextType = SysDic.All, Length = 100)]
        public virtual string MaxNumber
		{
			get { return _maxNumber; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for MaxNumber", value, value.ToString());
				_maxNumber = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "规则号码", ShortCode = "RuleNumber", Desc = "规则号码", ContextType = SysDic.All, Length = 100)]
        public virtual string RuleNumber
        {
            get { return _ruleNumber; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for RuleNumber", value, value.ToString());
                _ruleNumber = value;
            }
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