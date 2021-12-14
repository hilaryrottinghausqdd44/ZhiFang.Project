using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroThreeReportDetail

	/// <summary>
	/// MEMicroThreeReportDetail object for NHibernate mapped table 'ME_MicroThreeReportDetail'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物三级报告细表", ClassCName = "MEMicroThreeReportDetail", ShortCode = "MEMicroThreeReportDetail", Desc = "微生物三级报告细表")]
	public class MEMicroThreeReportDetail : BaseEntity
	{
		#region Member Variables
		
        protected DateTime? _dataUpdateTime;
		protected BMicroTestItemInfo _bMicroTestItemInfo;
		protected BMicroTestItemUseValue _bMicroTestItemUseValue;
		protected MEGroupSampleForm _mEGroupSampleForm;
		protected MEMicroThreeReport _mEMicroThreeReport;

		#endregion

		#region Constructors

		public MEMicroThreeReportDetail() { }

		public MEMicroThreeReportDetail( long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroTestItemInfo bMicroTestItemInfo, BMicroTestItemUseValue bMicroTestItemUseValue, MEGroupSampleForm mEGroupSampleForm, MEMicroThreeReport mEMicroThreeReport )
		{
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroTestItemInfo = bMicroTestItemInfo;
			this._bMicroTestItemUseValue = bMicroTestItemUseValue;
			this._mEGroupSampleForm = mEGroupSampleForm;
			this._mEMicroThreeReport = mEMicroThreeReport;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物检验记录项字典表", ShortCode = "BMicroTestItemInfo", Desc = "微生物检验记录项字典表")]
		public virtual BMicroTestItemInfo BMicroTestItemInfo
		{
			get { return _bMicroTestItemInfo; }
			set { _bMicroTestItemInfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物检验记录项常用值", ShortCode = "BMicroTestItemUseValue", Desc = "微生物检验记录项常用值")]
		public virtual BMicroTestItemUseValue BMicroTestItemUseValue
		{
			get { return _bMicroTestItemUseValue; }
			set { _bMicroTestItemUseValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
		public virtual MEGroupSampleForm MEGroupSampleForm
		{
			get { return _mEGroupSampleForm; }
			set { _mEGroupSampleForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物三级报告主表", ShortCode = "MEMicroThreeReport", Desc = "微生物三级报告主表")]
		public virtual MEMicroThreeReport MEMicroThreeReport
		{
			get { return _mEMicroThreeReport; }
			set { _mEMicroThreeReport = value; }
		}

        
		#endregion
	}
	#endregion
}