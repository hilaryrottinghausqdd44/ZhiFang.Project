using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSampleDelivery

	/// <summary>
	/// MEPTSampleDelivery object for NHibernate mapped table 'MEPT_SampleDelivery'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "外送清单表", ClassCName = "MEPTSampleDelivery", ShortCode = "MEPTSampleDelivery", Desc = "外送清单表")]
	public class MEPTSampleDelivery : BaseEntity
	{
		#region Member Variables
		
        protected string _sampleDeliveryNo;
        protected DateTime _sampleDeliveryDate;
        protected string _sampleDeliveryMan;
        protected int _printCount;
        protected DateTime? _printTime;
        protected DateTime? _dataUpdateTime;
		protected BLaboratory _deliverylab;
		protected HRDept _deliverydept;
		protected IList<MEPTSampleDeliveryConditon> _mEPTSampleDeliveryConditonList; 

		#endregion

		#region Constructors

		public MEPTSampleDelivery() { }

		public MEPTSampleDelivery( long labID, string sampleDeliveryNo, DateTime sampleDeliveryDate, string sampleDeliveryMan, int printCount, DateTime printTime, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BLaboratory deliverylab, HRDept deliverydept )
		{
			this._labID = labID;
			this._sampleDeliveryNo = sampleDeliveryNo;
			this._sampleDeliveryDate = sampleDeliveryDate;
			this._sampleDeliveryMan = sampleDeliveryMan;
			this._printCount = printCount;
			this._printTime = printTime;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._deliverylab = deliverylab;
			this._deliverydept = deliverydept;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "外送清单编号", ShortCode = "SampleDeliveryNo", Desc = "外送清单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string SampleDeliveryNo
		{
			get { return _sampleDeliveryNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for SampleDeliveryNo", value, value.ToString());
				_sampleDeliveryNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "外送日期", ShortCode = "SampleDeliveryDate", Desc = "外送日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime SampleDeliveryDate
		{
			get { return _sampleDeliveryDate; }
			set { _sampleDeliveryDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "外送人或交接人", ShortCode = "SampleDeliveryMan", Desc = "外送人或交接人", ContextType = SysDic.All, Length = 30)]
        public virtual string SampleDeliveryMan
		{
			get { return _sampleDeliveryMan; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for SampleDeliveryMan", value, value.ToString());
				_sampleDeliveryMan = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintCount", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
		{
			get { return _printCount; }
			set { _printCount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "打印时间", ShortCode = "PrintTime", Desc = "打印时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PrintTime
		{
			get { return _printTime; }
			set { _printTime = value; }
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
        [DataDesc(CName = "实验室字典表", ShortCode = "Delivery", Desc = "实验室字典表")]
		public virtual BLaboratory DeliveryLab
		{
			get { return _deliverylab; }
			set { _deliverylab = value; }
		}

        [DataMember]
        [DataDesc(CName = "部门", ShortCode = "Delivery", Desc = "部门")]
		public virtual HRDept DeliveryDept
		{
			get { return _deliverydept; }
			set { _deliverydept = value; }
		}

        [DataMember]
        [DataDesc(CName = "外送清单表", ShortCode = "MEPTSampleDeliveryConditonList", Desc = "外送清单表")]
		public virtual IList<MEPTSampleDeliveryConditon> MEPTSampleDeliveryConditonList
		{
			get
			{
				if (_mEPTSampleDeliveryConditonList==null)
				{
					_mEPTSampleDeliveryConditonList = new List<MEPTSampleDeliveryConditon>();
				}
				return _mEPTSampleDeliveryConditonList;
			}
			set { _mEPTSampleDeliveryConditonList = value; }
		}

        
		#endregion
	}
	#endregion
}