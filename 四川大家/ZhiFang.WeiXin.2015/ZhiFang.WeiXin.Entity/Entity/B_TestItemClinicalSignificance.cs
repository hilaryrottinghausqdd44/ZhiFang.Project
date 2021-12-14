using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BTestItemClinicalSignificance

	/// <summary>
	/// BTestItemClinicalSignificance object for NHibernate mapped table 'B_TestItemClinicalSignificance'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "项目临床意义", ClassCName = "BTestItemClinicalSignificance", ShortCode = "BTestItemClinicalSignificance", Desc = "项目临床意义")]
	public class BTestItemClinicalSignificance : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _highSignificance;
        protected string _lowSignificance;
        protected string _superHighSignificance;
        protected string _superLowSignificance;
        protected string _morphologySignificance;
        protected string _virusesSignificance;
        protected string _baseSignificance;
        protected string _otherSignificance;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected DateTime? _dataUpdateTime;
		protected BSampleType _bSampleType;
		protected BSpecialty _bSpecialty;

		#endregion

		#region Constructors

		public BTestItemClinicalSignificance() { }

		public BTestItemClinicalSignificance( string cName, string highSignificance, string lowSignificance, string superHighSignificance, string superLowSignificance, string morphologySignificance, string virusesSignificance, string baseSignificance, string otherSignificance, string comment, bool isUse, int dispOrder, string useCode, string standCode, string deveCode, string shortcode, string pinYinZiTou, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BSampleType bSampleType, BSpecialty bSpecialty )
		{
			this._cName = cName;
			this._highSignificance = highSignificance;
			this._lowSignificance = lowSignificance;
			this._superHighSignificance = superHighSignificance;
			this._superLowSignificance = superLowSignificance;
			this._morphologySignificance = morphologySignificance;
			this._virusesSignificance = virusesSignificance;
			this._baseSignificance = baseSignificance;
			this._otherSignificance = otherSignificance;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bSampleType = bSampleType;
			this._bSpecialty = bSpecialty;
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
        [DataDesc(CName = "英文名称", ShortCode = "HighSignificance", Desc = "英文名称", ContextType = SysDic.All, Length = 500)]
        public virtual string HighSignificance
		{
			get { return _highSignificance; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for HighSignificance", value, value.ToString());
				_highSignificance = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "LowSignificance", Desc = "简称", ContextType = SysDic.All, Length = 500)]
        public virtual string LowSignificance
		{
			get { return _lowSignificance; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for LowSignificance", value, value.ToString());
				_lowSignificance = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "项目类型", ShortCode = "SuperHighSignificance", Desc = "项目类型", ContextType = SysDic.All, Length = 500)]
        public virtual string SuperHighSignificance
		{
			get { return _superHighSignificance; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for SuperHighSignificance", value, value.ToString());
				_superHighSignificance = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SuperLowSignificance", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string SuperLowSignificance
		{
			get { return _superLowSignificance; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for SuperLowSignificance", value, value.ToString());
				_superLowSignificance = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "结果单位", ShortCode = "MorphologySignificance", Desc = "结果单位", ContextType = SysDic.All, Length = 500)]
        public virtual string MorphologySignificance
		{
			get { return _morphologySignificance; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for MorphologySignificance", value, value.ToString());
				_morphologySignificance = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "默认参考范围", ShortCode = "VirusesSignificance", Desc = "默认参考范围", ContextType = SysDic.All, Length = 500)]
        public virtual string VirusesSignificance
		{
			get { return _virusesSignificance; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for VirusesSignificance", value, value.ToString());
				_virusesSignificance = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "结果类型", ShortCode = "BaseSignificance", Desc = "结果类型", ContextType = SysDic.All, Length = 500)]
        public virtual string BaseSignificance
		{
			get { return _baseSignificance; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for BaseSignificance", value, value.ToString());
				_baseSignificance = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OtherSignificance", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string OtherSignificance
		{
			get { return _otherSignificance; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for OtherSignificance", value, value.ToString());
				_otherSignificance = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{				
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
		{
			get { return _useCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
				_useCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
				_standCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "开发商标准代码", ShortCode = "DeveCode", Desc = "开发商标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
		{
			get { return _deveCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
				_deveCode = value;
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
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
		public virtual BSampleType BSampleType
		{
			get { return _bSampleType; }
			set { _bSampleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "专业表", ShortCode = "BSpecialty", Desc = "专业表")]
		public virtual BSpecialty BSpecialty
		{
			get { return _bSpecialty; }
			set { _bSpecialty = value; }
		}

        
		#endregion
	}
	#endregion
}