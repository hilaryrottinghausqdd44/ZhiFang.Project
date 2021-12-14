using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroAppraisalTest

	/// <summary>
	/// MEMicroAppraisalTest object for NHibernate mapped table 'ME_MicroAppraisalTest'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "手工鉴定试验结果", ClassCName = "MEMicroAppraisalTest", ShortCode = "MEMicroAppraisalTest", Desc = "手工鉴定试验结果")]
	public class MEMicroAppraisalTest : BaseEntity
	{
		#region Member Variables
		
        protected long? _groupSampleFormID;
        protected string _gSampleNo;
        protected long? _testID;
        protected long? _testValueID;
        protected string _memo;
        protected bool _isReport;
        protected long? _empID;
        protected string _empName;
        protected DateTime? _dataUpdateTime;
		protected MEMicroAppraisalValue _mEMicroAppraisalValue;

		#endregion

		#region Constructors

		public MEMicroAppraisalTest() { }

		public MEMicroAppraisalTest( long labID, long groupSampleFormID, string gSampleNo, long testID, long testValueID, string memo, bool isReport, DateTime dataAddTime, long empID, string empName, DateTime dataUpdateTime, byte[] dataTimeStamp, MEMicroAppraisalValue mEMicroAppraisalValue )
		{
			this._labID = labID;
			this._groupSampleFormID = groupSampleFormID;
			this._gSampleNo = gSampleNo;
			this._testID = testID;
			this._testValueID = testValueID;
			this._memo = memo;
			this._isReport = isReport;
			this._dataAddTime = dataAddTime;
			this._empID = empID;
			this._empName = empName;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEMicroAppraisalValue = mEMicroAppraisalValue;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "小组样本单ID", ShortCode = "GroupSampleFormID", Desc = "小组样本单ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? GroupSampleFormID
		{
			get { return _groupSampleFormID; }
			set { _groupSampleFormID = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组检测编号", ShortCode = "GSampleNo", Desc = "小组检测编号", ContextType = SysDic.All, Length = 20)]
        public virtual string GSampleNo
		{
			get { return _gSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for GSampleNo", value, value.ToString());
				_gSampleNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TestID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? TestID
		{
			get { return _testID; }
			set { _testID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TestValueID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? TestValueID
		{
			get { return _testValueID; }
			set { _testValueID = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否报告", ShortCode = "IsReport", Desc = "是否报告", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReport
		{
			get { return _isReport; }
			set { _isReport = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID", ShortCode = "EmpID", Desc = "员工ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EmpID
		{
			get { return _empID; }
			set { _empID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EmpName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EmpName
		{
			get { return _empName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
				_empName = value;
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
        [DataDesc(CName = "微生物鉴定结果", ShortCode = "MEMicroAppraisalValue", Desc = "微生物鉴定结果")]
		public virtual MEMicroAppraisalValue MEMicroAppraisalValue
		{
			get { return _mEMicroAppraisalValue; }
			set { _mEMicroAppraisalValue = value; }
		}

        
		#endregion
	}
	#endregion
}