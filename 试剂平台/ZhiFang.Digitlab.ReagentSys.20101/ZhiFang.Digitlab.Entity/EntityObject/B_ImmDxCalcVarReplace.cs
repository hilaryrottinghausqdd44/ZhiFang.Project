using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BImmDxCalcVarReplace

	/// <summary>
	/// BImmDxCalcVarReplace object for NHibernate mapped table 'B_ImmDxCalcVarReplace'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "定性计算变量替换表：用于存储定性计算过程中的量值替换.", ClassCName = "BImmDxCalcVarReplace", ShortCode = "BImmDxCalcVarReplace", Desc = "定性计算变量替换表：用于存储定性计算过程中的量值替换.")]
	public class BImmDxCalcVarReplace : BaseEntity
	{
		#region Member Variables
		
        protected string _varName;
        protected int _compType;
        protected string _compValue;
        protected string _replaceValue;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BImmCalcTemplate _bImmCalcTemplate;

		#endregion

		#region Constructors

		public BImmDxCalcVarReplace() { }

		public BImmDxCalcVarReplace( long labID, string varName, int compType, string compValue, string replaceValue, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BImmCalcTemplate bImmCalcTemplate )
		{
			this._labID = labID;
			this._varName = varName;
			this._compType = compType;
			this._compValue = compValue;
			this._replaceValue = replaceValue;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bImmCalcTemplate = bImmCalcTemplate;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "变量，即计算公式中的各个变量", ShortCode = "VarName", Desc = "变量，即计算公式中的各个变量", ContextType = SysDic.All, Length = 10)]
        public virtual string VarName
		{
			get { return _varName; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for VarName", value, value.ToString());
				_varName = value;
			}
		}

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
        [DataDesc(CName = "替换值", ShortCode = "ReplaceValue", Desc = "替换值", ContextType = SysDic.All, Length = 20)]
        public virtual string ReplaceValue
		{
			get { return _replaceValue; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReplaceValue", value, value.ToString());
				_replaceValue = value;
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