using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BImmDxCalcRule

	/// <summary>
	/// BImmDxCalcRule object for NHibernate mapped table 'B_ImmDxCalcRule'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "定性结果判断规则表：用于存储定性计算判断规则的具体内容。", ClassCName = "BImmDxCalcRule", ShortCode = "BImmDxCalcRule", Desc = "定性结果判断规则表：用于存储定性计算判断规则的具体内容。")]
	public class BImmDxCalcRule : BaseEntity
	{
		#region Member Variables
		
        protected int _compType;
        protected string _compValue;
        protected string _compResult;
        protected string _shortCompResult;
        protected int _showColor;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BImmCalcTemplate _bImmCalcTemplate;

		#endregion

		#region Constructors

		public BImmDxCalcRule() { }

		public BImmDxCalcRule( int compType, string compValue, string compResult, string shortCompResult, int showColor, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BImmCalcTemplate bImmCalcTemplate )
		{
			this._compType = compType;
			this._compValue = compValue;
			this._compResult = compResult;
			this._shortCompResult = shortCompResult;
			this._showColor = showColor;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bImmCalcTemplate = bImmCalcTemplate;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "比较符", ShortCode = "CompType", Desc = "比较符", ContextType = SysDic.All, Length = 4)]
        public virtual int CompType
		{
			get { return _compType; }
			set { _compType = value; }
		}

        [DataMember]
        [DataDesc(CName = "比较值", ShortCode = "CompValue", Desc = "比较值", ContextType = SysDic.All, Length = 20)]
        public virtual string CompValue
		{
			get { return _compValue; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CompValue", value, value.ToString());
				_compValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定性结论详细描述", ShortCode = "CompResult", Desc = "定性结论详细描述", ContextType = SysDic.All, Length = 20)]
        public virtual string CompResult
		{
			get { return _compResult; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CompResult", value, value.ToString());
				_compResult = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定性结论简要描述", ShortCode = "ShortCompResult", Desc = "定性结论简要描述", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCompResult
		{
			get { return _shortCompResult; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCompResult", value, value.ToString());
				_shortCompResult = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "标识颜色", ShortCode = "ShowColor", Desc = "标识颜色", ContextType = SysDic.All, Length = 4)]
        public virtual int ShowColor
		{
			get { return _showColor; }
			set { _showColor = value; }
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
        [DataDesc(CName = "定性计算公式表：用于存储定性计算公式的具体内容", ShortCode = "BImmCalcTemplate", Desc = "定性计算公式表：用于存储定性计算公式的具体内容")]
		public virtual BImmCalcTemplate BImmCalcTemplate
		{
			get { return _bImmCalcTemplate; }
			set { _bImmCalcTemplate = value; }
		}

        
		#endregion
	}
	#endregion
}