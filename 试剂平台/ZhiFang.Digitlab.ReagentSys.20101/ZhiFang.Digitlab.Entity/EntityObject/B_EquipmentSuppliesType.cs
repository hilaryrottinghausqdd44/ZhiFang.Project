using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BEquipmentSuppliesType

	/// <summary>
	/// BEquipmentSuppliesType object for NHibernate mapped table 'B_EquipmentSuppliesType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "检验设备耗材类型表", ClassCName = "BEquipmentSuppliesType", ShortCode = "BEquipmentSuppliesType", Desc = "检验设备耗材类型表")]
	public class BEquipmentSuppliesType : BaseEntity
	{
		#region Member Variables
		
        protected string _eSTName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _content;
        protected string _comment;
        protected string _eSUnit;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public BEquipmentSuppliesType() { }

		public BEquipmentSuppliesType( long labID, string eSTName, string sName, string shortcode, string pinYinZiTou, string content, string comment, string eSUnit, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._eSTName = eSTName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._content = content;
			this._comment = comment;
			this._eSUnit = eSUnit;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "类型名称", ShortCode = "ESTName", Desc = "类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ESTName
		{
			get { return _eSTName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ESTName", value, value.ToString());
				_eSTName = value;
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
        [DataDesc(CName = "内容", ShortCode = "Content", Desc = "内容", ContextType = SysDic.All, Length = 16)]
        public virtual string Content
		{
			get { return _content; }
			set
			{
                //if ( value != null && value.Length > 16)
                //    throw new ArgumentOutOfRangeException("Invalid value for Content", value, value.ToString());
				_content = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "单位：个、台、支等，由用户自定义", ShortCode = "ESUnit", Desc = "单位：个、台、支等，由用户自定义", ContextType = SysDic.All, Length = 10)]
        public virtual string ESUnit
		{
			get { return _eSUnit; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ESUnit", value, value.ToString());
				_eSUnit = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		
		#endregion
	}
	#endregion
}