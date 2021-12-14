using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BCalcFormulaParas

	/// <summary>
	/// BCalcFormulaParas object for NHibernate mapped table 'B_CalcFormulaParas'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "计算公式内参数", ClassCName = "BCalcFormulaParas", ShortCode = "BCalcFormulaParas", Desc = "计算公式内参数")]
	public class BCalcFormulaParas : BaseEntity
	{
		#region Member Variables
		
        protected string _paraCode;
        protected string _paraObjName;
        protected string _paraObjFieldName;
        protected DateTime? _dataUpdateTime;
		protected BCalcFormula _bCalcFormula;

		#endregion

		#region Constructors

		public BCalcFormulaParas() { }

		public BCalcFormulaParas( long labID, string paraCode, string paraObjName, string paraObjFieldName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BCalcFormula bCalcFormula )
		{
			this._labID = labID;
			this._paraCode = paraCode;
			this._paraObjName = paraObjName;
			this._paraObjFieldName = paraObjFieldName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bCalcFormula = bCalcFormula;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "参数标识符", ShortCode = "ParaCode", Desc = "参数标识符", ContextType = SysDic.All, Length = 20)]
        public virtual string ParaCode
		{
			get { return _paraCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ParaCode", value, value.ToString());
				_paraCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "参数对象名", ShortCode = "ParaObjName", Desc = "参数对象名", ContextType = SysDic.All, Length = 50)]
        public virtual string ParaObjName
		{
			get { return _paraObjName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ParaObjName", value, value.ToString());
				_paraObjName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "参数对象属性名", ShortCode = "ParaObjFieldName", Desc = "参数对象属性名", ContextType = SysDic.All, Length = 100)]
        public virtual string ParaObjFieldName
		{
			get { return _paraObjFieldName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ParaObjFieldName", value, value.ToString());
				_paraObjFieldName = value;
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
        [DataDesc(CName = "计算公式", ShortCode = "BCalcFormula", Desc = "计算公式")]
		public virtual BCalcFormula BCalcFormula
		{
			get { return _bCalcFormula; }
			set { _bCalcFormula = value; }
		}

        
		#endregion
	}
	#endregion
}