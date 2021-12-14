using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroSampleDescItemsResults

	/// <summary>
	/// MEMicroSampleDescItemsResults object for NHibernate mapped table 'ME_MicroSampleDescItemsResults'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物标本描述记录项结果表", ClassCName = "MEMicroSampleDescItemsResults", ShortCode = "MEMicroSampleDescItemsResults", Desc = "微生物标本描述记录项结果表")]
	public class MEMicroSampleDescItemsResults : BaseEntity
	{
		#region Member Variables
		
        protected string _reportValue;
        protected string _resultComment;
        protected DateTime? _dataUpdateTime;
		protected BMicroSampleDescItems _bMicroSampleDescItems;
		protected MEGroupSampleForm _mEGroupSampleForm;

		#endregion

		#region Constructors

		public MEMicroSampleDescItemsResults() { }

		public MEMicroSampleDescItemsResults( long labID, string reportValue, string resultComment, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroSampleDescItems bMicroSampleDescItems, MEGroupSampleForm mEGroupSampleForm )
		{
			this._labID = labID;
			this._reportValue = reportValue;
			this._resultComment = resultComment;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroSampleDescItems = bMicroSampleDescItems;
			this._mEGroupSampleForm = mEGroupSampleForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "报告值:当微生物ID为空时，当前记录的报告值为阴性结果描述；不为空时，是细菌结果描述", ShortCode = "ReportValue", Desc = "报告值:当微生物ID为空时，当前记录的报告值为阴性结果描述；不为空时，是细菌结果描述", ContextType = SysDic.All, Length = 300)]
        public virtual string ReportValue
		{
			get { return _reportValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ReportValue", value, value.ToString());
				_reportValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "结果说明", ShortCode = "ResultComment", Desc = "结果说明", ContextType = SysDic.All, Length = 16)]
        public virtual string ResultComment
		{
			get { return _resultComment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for ResultComment", value, value.ToString());
				_resultComment = value;
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
        [DataDesc(CName = "微生物标本描述记录项", ShortCode = "BMicroSampleDescItems", Desc = "微生物标本描述记录项")]
		public virtual BMicroSampleDescItems BMicroSampleDescItems
		{
			get { return _bMicroSampleDescItems; }
			set { _bMicroSampleDescItems = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
		public virtual MEGroupSampleForm MEGroupSampleForm
		{
			get { return _mEGroupSampleForm; }
			set { _mEGroupSampleForm = value; }
		}

        
		#endregion
	}
	#endregion
}