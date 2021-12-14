using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroSmearValueDetail

	/// <summary>
	/// MEMicroSmearValueDetail object for NHibernate mapped table 'ME_MicroSmearValueDetail'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物涂片结果明细表", ClassCName = "MEMicroSmearValueDetail", ShortCode = "MEMicroSmearValueDetail", Desc = "微生物涂片结果明细表")]
	public class MEMicroSmearValueDetail : BaseEntity
	{
		#region Member Variables
		
        protected string _empName;
        protected long? _detailID;
        protected string _smearMemo;
        protected bool _isReport=true;
        protected DateTime? _dataUpdateTime;
        protected long? _EmpID;
		protected MEGroupSampleForm _mEGroupSampleForm;
		protected MEGroupSampleItem _mEGroupSampleItem;
		protected MEMicroSmearValue _mEMicroSmearValue;
        private string _DetailName;
        private string _DetailShortCode;

		#endregion

		#region Constructors

		public MEMicroSmearValueDetail() { }

		public MEMicroSmearValueDetail( long labID, string empName, long detailID, string smearMemo, bool isReport, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HREmployee hREmployee, MEGroupSampleForm mEGroupSampleForm, MEGroupSampleItem mEGroupSampleItem, MEMicroSmearValue mEMicroSmearValue )
		{
			this._labID = labID;
			this._empName = empName;
			this._detailID = detailID;
			this._smearMemo = smearMemo;
			this._isReport = isReport;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEGroupSampleForm = mEGroupSampleForm;
			this._mEGroupSampleItem = mEGroupSampleItem;
			this._mEMicroSmearValue = mEMicroSmearValue;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "结果记录人姓名", ShortCode = "EmpName", Desc = "结果记录人姓名", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "明细记录项ID", ShortCode = "DetailID", Desc = "明细记录项ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DetailID
		{
			get { return _detailID; }
			set { _detailID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "明细记录项名称", ShortCode = "DetailName", Desc = "明细记录项名称", ContextType = SysDic.All, Length = 8)]
        public virtual string DetailName
        {
            get { return _DetailName; }
            set { _DetailName = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "明细记录项编码", ShortCode = "DetailShortCode", Desc = "明细记录项编码", ContextType = SysDic.All, Length = 8)]
        public virtual string DetailShortCode
        {
            get { return _DetailShortCode; }
            set { _DetailShortCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "SmearMemo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string SmearMemo
		{
			get { return _smearMemo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for SmearMemo", value, value.ToString());
				_smearMemo = value;
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
        public virtual long? EmpID
		{
            get { return _EmpID; }
            set { _EmpID = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
		public virtual MEGroupSampleForm MEGroupSampleForm
		{
			get { return _mEGroupSampleForm; }
			set { _mEGroupSampleForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本项目", ShortCode = "MEGroupSampleItem", Desc = "小组样本项目")]
		public virtual MEGroupSampleItem MEGroupSampleItem
		{
			get { return _mEGroupSampleItem; }
			set { _mEGroupSampleItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物涂片结果", ShortCode = "MEMicroSmearValue", Desc = "微生物涂片结果")]
		public virtual MEMicroSmearValue MEMicroSmearValue
		{
			get { return _mEMicroSmearValue; }
			set { _mEMicroSmearValue = value; }
		}

        
		#endregion
	}
	#endregion
}