using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSamplingTube

	/// <summary>
	/// MEPTSamplingTube object for NHibernate mapped table 'MEPT_SamplingTube'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "采样管设置", ClassCName = "MEPTSamplingTube", ShortCode = "MEPTSamplingTube", Desc = "采样管设置")]
    public class MEPTSamplingTube : BaseEntity
	{
		#region Member Variables
		
        
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _colorName;
        protected string _colorValue;
        protected decimal _capacity;
        protected string _unit;
        protected decimal _minCapacity;
        protected int _dispOrder;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected MEPTBCharge _mEPTBCharge;
		protected IList<MEPTSamplingGroup> _mEPTSamplingGroupList;

		#endregion

		#region Constructors

		public MEPTSamplingTube() { }

		public MEPTSamplingTube( long labID, string cName, string eName, string sName, string shortcode, string colorName, string colorValue, decimal capacity, string unit, decimal minCapacity, int dispOrder, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEPTBCharge mEPTBCharge )
		{
			this._labID = labID;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._colorName = colorName;
			this._colorValue = colorValue;
			this._capacity = capacity;
			this._unit = unit;
			this._minCapacity = minCapacity;
			this._dispOrder = dispOrder;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEPTBCharge = mEPTBCharge;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "颜色名称", ShortCode = "ColorName", Desc = "颜色名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ColorName
		{
			get { return _colorName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ColorName", value, value.ToString());
				_colorName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "颜色值", ShortCode = "ColorValue", Desc = "颜色值", ContextType = SysDic.All, Length = 4)]
        public virtual string ColorValue
		{
			get { return _colorValue; }
			set { _colorValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "容量", ShortCode = "Capacity", Desc = "容量", ContextType = SysDic.All, Length = 5)]
        public virtual decimal Capacity
		{
			get { return _capacity; }
			set { _capacity = value; }
		}

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "Unit", Desc = "单位", ContextType = SysDic.All, Length = 50)]
        public virtual string Unit
		{
			get { return _unit; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Unit", value, value.ToString());
				_unit = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "最少标本量", ShortCode = "MinCapacity", Desc = "最少标本量", ContextType = SysDic.All, Length = 5)]
        public virtual decimal MinCapacity
		{
			get { return _minCapacity; }
			set { _minCapacity = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头，主要用于录入方便", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头，主要用于录入方便", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "说明", ShortCode = "Comment", Desc = "说明", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
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
        [DataDesc(CName = "费用表", ShortCode = "MEPTBCharge", Desc = "费用表", ContextType = SysDic.All)]
		public virtual MEPTBCharge MEPTBCharge
		{
			get { return _mEPTBCharge; }
			set { _mEPTBCharge = value; }
		}

        [DataMember]
        [DataDesc(CName = "采样组设置", ShortCode = "MEPTSamplingGroupList", Desc = "采样组设置", ContextType = SysDic.All)]
		public virtual IList<MEPTSamplingGroup> MEPTSamplingGroupList
		{
			get
			{
				if (_mEPTSamplingGroupList==null)
				{
					_mEPTSamplingGroupList = new List<MEPTSamplingGroup>();
				}
				return _mEPTSamplingGroupList;
			}
			set { _mEPTSamplingGroupList = value; }
		}

		#endregion
	}
	#endregion
}