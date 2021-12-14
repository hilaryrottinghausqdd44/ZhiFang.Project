using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSampleSend

	/// <summary>
	/// MEPTSampleSend object for NHibernate mapped table 'MEPT_SampleSend'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "样本送检表", ClassCName = "MEPTSampleSend", ShortCode = "MEPTSampleSend", Desc = "样本送检表")]
	public class MEPTSampleSend : BaseEntity
	{
		#region Member Variables
		
        protected string _sampleSendNo;
        protected long? _empID;
        protected DateTime? _dataUpdateTime;
        protected int _printTimes;
		protected IList<MEPTSampleSendConditon> _mEPTSampleSendConditonList;
        protected int[] _bSampleList;

		#endregion

		#region Constructors

		public MEPTSampleSend() { }

		public MEPTSampleSend( long labID, string sampleSendNo, long empID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, int printTimes )
		{
			this._labID = labID;
			this._sampleSendNo = sampleSendNo;
			this._empID = empID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._printTimes = printTimes;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "样本送检批次号", ShortCode = "SampleSendNo", Desc = "样本送检批次号", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleSendNo
		{
			get { return _sampleSendNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleSendNo", value, value.ToString());
				_sampleSendNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送检人员ID", ShortCode = "EmpID", Desc = "送检人员ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EmpID
		{
			get { return _empID; }
			set { _empID = value; }
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
        [DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
		{
			get { return _printTimes; }
			set { _printTimes = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本送检关系表", ShortCode = "MEPTSampleSendConditonList", Desc = "样本送检关系表")]
		public virtual IList<MEPTSampleSendConditon> MEPTSampleSendConditonList
		{
			get
			{
				if (_mEPTSampleSendConditonList==null)
				{
					_mEPTSampleSendConditonList = new List<MEPTSampleSendConditon>();
				}
				return _mEPTSampleSendConditonList;
			}
			set { _mEPTSampleSendConditonList = value; }
		}

        public virtual int[] BSampleList
        {
            get { return _bSampleList; }
            set { _bSampleList = value; }
        }
		#endregion
	}
	#endregion
}