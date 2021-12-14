using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroStepBacode

	/// <summary>
	/// MEMicroStepBacode object for NHibernate mapped table 'ME_MicroStepBacode'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物检验条码", ClassCName = "MEMicroStepBacode", ShortCode = "MEMicroStepBacode", Desc = "微生物检验条码")]
	public class MEMicroStepBacode : BaseEntity
	{
		#region Member Variables
		
        protected string _barCode;
        protected DateTime? _dataUpdateTime;
		protected MEMicroStep _mEMicroStep;

		#endregion

		#region Constructors

		public MEMicroStepBacode() { }

		public MEMicroStepBacode( long labID, string barCode, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEMicroStep mEMicroStep )
		{
			this._labID = labID;
			this._barCode = barCode;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEMicroStep = mEMicroStep;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "条码", ShortCode = "BarCode", Desc = "条码", ContextType = SysDic.All, Length = 20)]
        public virtual string BarCode
		{
			get { return _barCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
				_barCode = value;
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
        [DataDesc(CName = "微生物检验过程", ShortCode = "MEMicroStep", Desc = "微生物检验过程")]
		public virtual MEMicroStep MEMicroStep
		{
			get { return _mEMicroStep; }
			set { _mEMicroStep = value; }
		}

        
		#endregion
	}
	#endregion
}