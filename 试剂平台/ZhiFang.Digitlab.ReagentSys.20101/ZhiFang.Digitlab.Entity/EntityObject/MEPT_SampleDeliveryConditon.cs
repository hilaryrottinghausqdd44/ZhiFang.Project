using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSampleDeliveryConditon

	/// <summary>
	/// MEPTSampleDeliveryConditon object for NHibernate mapped table 'MEPT_SampleDeliveryConditon'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "外送清单表", ClassCName = "MEPTSampleDeliveryConditon", ShortCode = "MEPTSampleDeliveryConditon", Desc = "外送清单表")]
	public class MEPTSampleDeliveryConditon : BaseEntity
	{
		#region Member Variables
		
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected MEPTSampleDelivery _mEPTSampleDelivery;
		protected MEPTSampleForm _mEPTSampleForm;

		#endregion

		#region Constructors

		public MEPTSampleDeliveryConditon() { }

		public MEPTSampleDeliveryConditon( long labID, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEPTSampleDelivery mEPTSampleDelivery, MEPTSampleForm mEPTSampleForm )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEPTSampleDelivery = mEPTSampleDelivery;
			this._mEPTSampleForm = mEPTSampleForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "更新时间", ShortCode = "DataUpdateTime", Desc = "更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "外送清单表", ShortCode = "MEPTSampleDelivery", Desc = "外送清单表")]
		public virtual MEPTSampleDelivery MEPTSampleDelivery
		{
			get { return _mEPTSampleDelivery; }
			set { _mEPTSampleDelivery = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本单", ShortCode = "MEPTSampleForm", Desc = "样本单")]
		public virtual MEPTSampleForm MEPTSampleForm
		{
			get { return _mEPTSampleForm; }
			set { _mEPTSampleForm = value; }
		}

        
		#endregion
	}
	#endregion
}