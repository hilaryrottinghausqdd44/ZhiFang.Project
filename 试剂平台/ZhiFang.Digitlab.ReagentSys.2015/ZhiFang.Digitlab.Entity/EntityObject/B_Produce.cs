using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BProduce

	/// <summary>
	/// BProduce object for NHibernate mapped table 'B_Produce'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "产品", ClassCName = "BProduce", ShortCode = "BProduce", Desc = "产品")]
    public class BProduce : BaseEntity
	{
		#region Member Variables
		
        
        protected string _name;
        protected string _sName;
        protected string _produceFactory;
        protected string _specification;
        protected string _unit;
        protected DateTime? _produceTime;
        protected string _batchCode;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        

		#endregion

		#region Constructors

		public BProduce() { }

		public BProduce( long labID, string name, string sName, string produceFactory, string specification, string unit, DateTime produceTime, string batchCode, string shortcode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._name = name;
			this._sName = sName;
			this._produceFactory = produceFactory;
			this._specification = specification;
			this._unit = unit;
			this._produceTime = produceTime;
			this._batchCode = batchCode;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "生产厂家", ShortCode = "ProduceFactory", Desc = "生产厂家", ContextType = SysDic.All, Length = 200)]
        public virtual string ProduceFactory
		{
			get { return _produceFactory; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ProduceFactory", value, value.ToString());
				_produceFactory = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "规格型号", ShortCode = "Specification", Desc = "规格型号", ContextType = SysDic.All, Length = 100)]
        public virtual string Specification
		{
			get { return _specification; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Specification", value, value.ToString());
				_specification = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "Unit", Desc = "单位", ContextType = SysDic.All, Length = 20)]
        public virtual string Unit
		{
			get { return _unit; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Unit", value, value.ToString());
				_unit = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生产日期", ShortCode = "ProduceTime", Desc = "生产日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ProduceTime
		{
			get { return _produceTime; }
			set { _produceTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "批次编号", ShortCode = "BatchCode", Desc = "批次编号", ContextType = SysDic.All, Length = 40)]
        public virtual string BatchCode
		{
			get { return _batchCode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for BatchCode", value, value.ToString());
				_batchCode = value;
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
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
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

		#endregion
	}
	#endregion
}