using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSampleIncept

	/// <summary>
	/// MEPTSampleIncept object for NHibernate mapped table 'MEPT_SampleIncept'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "样本签收表", ClassCName = "MEPTSampleIncept", ShortCode = "MEPTSampleIncept", Desc = "样本签收表")]
	public class MEPTSampleIncept : BaseEntity
	{
		#region Member Variables
		
        protected string _sampleInceptNo;
        protected DateTime? _dataUpdateTime;
        protected int _printTimes;
		protected IList<MEPTSampleInceptConditon> _mEPTSampleInceptConditonList;
        protected int[] _bSampleList;

		#endregion

		#region Constructors

		public MEPTSampleIncept() { }

		public MEPTSampleIncept( long labID, string sampleInceptNo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, int printTimes )
		{
			this._labID = labID;
			this._sampleInceptNo = sampleInceptNo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._printTimes = printTimes;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "样本签收编号", ShortCode = "SampleInceptNo", Desc = "样本签收编号", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleInceptNo
		{
			get { return _sampleInceptNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleInceptNo", value, value.ToString());
				_sampleInceptNo = value;
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
        [DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
		{
			get { return _printTimes; }
			set { _printTimes = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本签收关系表", ShortCode = "MEPTSampleInceptConditonList", Desc = "样本签收关系表")]
		public virtual IList<MEPTSampleInceptConditon> MEPTSampleInceptConditonList
		{
			get
			{
				if (_mEPTSampleInceptConditonList==null)
				{
					_mEPTSampleInceptConditonList = new List<MEPTSampleInceptConditon>();
				}
				return _mEPTSampleInceptConditonList;
			}
			set { _mEPTSampleInceptConditonList = value; }
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