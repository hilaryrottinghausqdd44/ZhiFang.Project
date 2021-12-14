using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BCalcFormula

	/// <summary>
	/// BCalcFormula object for NHibernate mapped table 'B_CalcFormula'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "计算公式", ClassCName = "BCalcFormula", ShortCode = "BCalcFormula", Desc = "计算公式")]
	public class BCalcFormula : BaseEntity
	{
		#region Member Variables

        protected string _calcFormula;
        protected int? _uAge;
        protected int? _lAge;
        protected string _genderList;
        protected int? _uWeight;
        protected int? _lWeight;
        protected string _sampleType;
        protected bool _isUse;
        protected bool _isDefault;
        protected int _priority;
        protected DateTime? _dataUpdateTime;
		protected ItemAllItem _itemAllItem;
		protected IList<BCalcFormulaParas> _bCalcFormulaParasList; 
		protected IList<BCalcItem> _bCalcItemList; 

		#endregion

		#region Constructors

		public BCalcFormula() { }

        public BCalcFormula(long labID, string calcFormula, int? uAge, int? lAge, string genderList, int? uWeight, int? lWeight, string sampleType, bool isUse, bool isDefault, int priority, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem)
		{
			this._labID = labID;
			this._calcFormula = calcFormula;
			this._uAge = uAge;
			this._lAge = lAge;
			this._genderList = genderList;
			this._uWeight = uWeight;
			this._lWeight = lWeight;
			this._sampleType = sampleType;
			this._isUse = isUse;
			this._isDefault = isDefault;
			this._priority = priority;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "计算公式", ShortCode = "CalcFormula", Desc = "计算公式", ContextType = SysDic.All, Length = 8)]
        public virtual string CalcFormula
		{
			get { return _calcFormula; }
			set { _calcFormula = value; }
		}

        [DataMember]
        [DataDesc(CName = "年龄范围上限", ShortCode = "UAge", Desc = "年龄范围上限", ContextType = SysDic.All, Length = 4)]
        public virtual int? UAge
		{
			get { return _uAge; }
			set { _uAge = value; }
		}

        [DataMember]
        [DataDesc(CName = "年龄范围下限", ShortCode = "LAge", Desc = "年龄范围下限", ContextType = SysDic.All, Length = 4)]
        public virtual int? LAge
		{
			get { return _lAge; }
			set { _lAge = value; }
		}

        [DataMember]
        [DataDesc(CName = "性别范围", ShortCode = "GenderList", Desc = "性别范围", ContextType = SysDic.All, Length = 100)]
        public virtual string GenderList
		{
			get { return _genderList; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for GenderList", value, value.ToString());
				_genderList = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "体重上限", ShortCode = "UWeight", Desc = "体重上限", ContextType = SysDic.All, Length = 4)]
        public virtual int? UWeight
		{
			get { return _uWeight; }
			set { _uWeight = value; }
		}

        [DataMember]
        [DataDesc(CName = "体重下线", ShortCode = "LWeight", Desc = "体重下线", ContextType = SysDic.All, Length = 4)]
        public virtual int? LWeight
		{
			get { return _lWeight; }
			set { _lWeight = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型范围", ShortCode = "SampleType", Desc = "样本类型范围", ContextType = SysDic.All, Length = 100)]
        public virtual string SampleType
		{
			get { return _sampleType; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SampleType", value, value.ToString());
				_sampleType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否在用", ShortCode = "IsUse", Desc = "是否在用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否是默认公式", ShortCode = "IsDefault", Desc = "是否是默认公式", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
		{
			get { return _isDefault; }
			set { _isDefault = value; }
		}

        [DataMember]
        [DataDesc(CName = "优先次序", ShortCode = "Priority", Desc = "优先次序", ContextType = SysDic.All, Length = 4)]
        public virtual int Priority
		{
			get { return _priority; }
			set { _priority = value; }
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
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "计算公式内参数", ShortCode = "BCalcFormulaParasList", Desc = "计算公式内参数")]
		public virtual IList<BCalcFormulaParas> BCalcFormulaParasList
		{
			get
			{
				if (_bCalcFormulaParasList==null)
				{
					_bCalcFormulaParasList = new List<BCalcFormulaParas>();
				}
				return _bCalcFormulaParasList;
			}
			set { _bCalcFormulaParasList = value; }
		}

        [DataMember]
        [DataDesc(CName = "计算公式中涉及的项目", ShortCode = "BCalcItemList", Desc = "计算公式中涉及的项目")]
		public virtual IList<BCalcItem> BCalcItemList
		{
			get
			{
				if (_bCalcItemList==null)
				{
					_bCalcItemList = new List<BCalcItem>();
				}
				return _bCalcItemList;
			}
			set { _bCalcItemList = value; }
		}

        
		#endregion
	}
	#endregion
}