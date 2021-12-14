using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSampleSendConditon

	/// <summary>
	/// MEPTSampleSendConditon object for NHibernate mapped table 'MEPT_SampleSendConditon'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "样本送检关系表", ClassCName = "MEPTSampleSendConditon", ShortCode = "MEPTSampleSendConditon", Desc = "样本送检关系表")]
	public class MEPTSampleSendConditon : BaseEntity
	{
		#region Member Variables
		
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected MEPTSampleForm _mEPTSampleForm;
		protected MEPTSampleSend _mEPTSampleSend;

		#endregion

		#region Constructors

		public MEPTSampleSendConditon() { }

		public MEPTSampleSendConditon( long labID, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEPTSampleForm mEPTSampleForm, MEPTSampleSend mEPTSampleSend )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEPTSampleForm = mEPTSampleForm;
			this._mEPTSampleSend = mEPTSampleSend;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "显示次序(送检样本次序)", ShortCode = "DispOrder", Desc = "显示次序(送检样本次序)", ContextType = SysDic.All, Length = 4)]
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
        [DataDesc(CName = "样本单", ShortCode = "MEPTSampleForm", Desc = "样本单")]
		public virtual MEPTSampleForm MEPTSampleForm
		{
			get { return _mEPTSampleForm; }
			set { _mEPTSampleForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本送检表", ShortCode = "MEPTSampleSend", Desc = "样本送检表")]
		public virtual MEPTSampleSend MEPTSampleSend
		{
			get { return _mEPTSampleSend; }
			set { _mEPTSampleSend = value; }
		}

        
		#endregion
	}
	#endregion
}