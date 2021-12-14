using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroSmearValue

	/// <summary>
	/// MEMicroSmearValue object for NHibernate mapped table 'ME_MicroSmearValue'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物涂片结果", ClassCName = "MEMicroSmearValue", ShortCode = "MEMicroSmearValue", Desc = "微生物涂片结果")]
	public class MEMicroSmearValue : BaseEntity
	{
		#region Member Variables
		
        protected string _empName;
        protected long? _wBC;
        protected long? _eP;
        protected long? _characterEvaluation;
        protected long? _gNB;
        protected long? _gNC;
        protected long? _gPB;
        protected long? _gPC;
        protected long? _yeastLikeFungus;
        protected long? _pseudoHyphae;
        protected long? _fungus;
        protected long? _evaluation;
        protected string _smearMemo;
        protected bool _isReport;
        protected DateTime? _dataUpdateTime;
		protected BStainingMethod _bStainingMethod;
		protected long? _hREmployee;
		protected MEGroupSampleForm _mEGroupSampleForm;
		protected MEGroupSampleItem _mEGroupSampleItem;
        protected IList<MEMicroSmearValueDetail> _mEMicroSmearValueDetailList; 

		#endregion

		#region Constructors

		public MEMicroSmearValue() { }

		public MEMicroSmearValue( long labID, string empName, long wBC, long eP, long characterEvaluation, long gNB, long gNC, long gPB, long gPC, long yeastLikeFungus, long pseudoHyphae, long fungus, long evaluation, string smearMemo, bool isReport, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BStainingMethod bStainingMethod, long hREmployee, MEGroupSampleForm mEGroupSampleForm, MEGroupSampleItem mEGroupSampleItem )
		{
			this._labID = labID;
			this._empName = empName;
			this._wBC = wBC;
			this._eP = eP;
			this._characterEvaluation = characterEvaluation;
			this._gNB = gNB;
			this._gNC = gNC;
			this._gPB = gPB;
			this._gPC = gPC;
			this._yeastLikeFungus = yeastLikeFungus;
			this._pseudoHyphae = pseudoHyphae;
			this._fungus = fungus;
			this._evaluation = evaluation;
			this._smearMemo = smearMemo;
			this._isReport = isReport;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bStainingMethod = bStainingMethod;
			this._hREmployee = hREmployee;
			this._mEGroupSampleForm = mEGroupSampleForm;
			this._mEGroupSampleItem = mEGroupSampleItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "涂片结果记录人姓名", ShortCode = "EmpName", Desc = "涂片结果记录人姓名", ContextType = SysDic.All, Length = 60)]
        public virtual string EmpName
		{
			get { return _empName; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
				_empName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "WBC", ShortCode = "WBC", Desc = "WBC", ContextType = SysDic.All, Length = 8)]
		public virtual long? WBC
		{
			get { return _wBC; }
			set { _wBC = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "EP", ShortCode = "EP", Desc = "EP", ContextType = SysDic.All, Length = 8)]
		public virtual long? EP
		{
			get { return _eP; }
			set { _eP = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "性状评价", ShortCode = "CharacterEvaluation", Desc = "性状评价", ContextType = SysDic.All, Length = 8)]
		public virtual long? CharacterEvaluation
		{
			get { return _characterEvaluation; }
			set { _characterEvaluation = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "GNB", ShortCode = "GNB", Desc = "GNB", ContextType = SysDic.All, Length = 8)]
		public virtual long? GNB
		{
			get { return _gNB; }
			set { _gNB = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "GNC", ShortCode = "GNC", Desc = "GNC", ContextType = SysDic.All, Length = 8)]
		public virtual long? GNC
		{
			get { return _gNC; }
			set { _gNC = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "GPB", ShortCode = "GPB", Desc = "GPB", ContextType = SysDic.All, Length = 8)]
		public virtual long? GPB
		{
			get { return _gPB; }
			set { _gPB = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "GPC", ShortCode = "GPC", Desc = "GPC", ContextType = SysDic.All, Length = 8)]
		public virtual long? GPC
		{
			get { return _gPC; }
			set { _gPC = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "酵母样真菌", ShortCode = "YeastLikeFungus", Desc = "酵母样真菌", ContextType = SysDic.All, Length = 8)]
		public virtual long? YeastLikeFungus
		{
			get { return _yeastLikeFungus; }
			set { _yeastLikeFungus = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "假菌丝", ShortCode = "PseudoHyphae", Desc = "假菌丝", ContextType = SysDic.All, Length = 8)]
		public virtual long? PseudoHyphae
		{
			get { return _pseudoHyphae; }
			set { _pseudoHyphae = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "真菌丝", ShortCode = "Fungus", Desc = "真菌丝", ContextType = SysDic.All, Length = 8)]
		public virtual long? Fungus
		{
			get { return _fungus; }
			set { _fungus = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "评价", ShortCode = "Evaluation", Desc = "评价", ContextType = SysDic.All, Length = 8)]
		public virtual long? Evaluation
		{
			get { return _evaluation; }
			set { _evaluation = value; }
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
        [DataDesc(CName = "染色方法字典表", ShortCode = "BStainingMethod", Desc = "染色方法字典表")]
		public virtual BStainingMethod BStainingMethod
		{
			get { return _bStainingMethod; }
			set { _bStainingMethod = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
        public virtual long? EmpID
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
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
        [DataDesc(CName = "微生物涂片结果明细表", ShortCode = "MEMicroSmearValueDetailList", Desc = "微生物涂片结果明细表")]
        public virtual IList<MEMicroSmearValueDetail> MEMicroSmearValueDetailList
        {
            get
            {
                if (_mEMicroSmearValueDetailList == null)
                {
                    _mEMicroSmearValueDetailList = new List<MEMicroSmearValueDetail>();
                }
                return _mEMicroSmearValueDetailList;
            }
            set { _mEMicroSmearValueDetailList = value; }
        }
        
		#endregion
	}
	#endregion
}