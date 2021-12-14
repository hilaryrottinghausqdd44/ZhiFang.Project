using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BHospital

	/// <summary>
	/// BHospital object for NHibernate mapped table 'B_Hospital'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "医院字典", ClassCName = "BHospital", ShortCode = "BHospital", Desc = "医院字典")]
	public class BHospital : BaseEntity
	{
		#region Member Variables
		
        protected long _areaID;
        protected long? _iconsID;
        protected long? _hTypeID;
        protected long? _levelID;
        protected string _name;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected string _levelName;
        protected string _hTypeName;
        protected string _postion;
        protected string _provinceName;
        protected string _cityName;
        protected long? _provinceID;
        protected long _cityID;
        protected string _hospitalCode;
		

		#endregion

		#region Constructors

		public BHospital() { }

		public BHospital( long areaID, long iconsID, long hTypeID, long levelID, string name, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, string levelName, string hTypeName, string postion, string provinceName, string cityName, long provinceID, long cityID, string hospitalCode )
		{
			this._areaID = areaID;
			this._iconsID = iconsID;
			this._hTypeID = hTypeID;
			this._levelID = levelID;
			this._name = name;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._levelName = levelName;
			this._hTypeName = hTypeName;
			this._postion = postion;
			this._provinceName = provinceName;
			this._cityName = cityName;
			this._provinceID = provinceID;
			this._cityID = cityID;
			this._hospitalCode = hospitalCode;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AreaID
		{
			get { return _areaID; }
			set { _areaID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "IconsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? IconsID
		{
			get { return _iconsID; }
			set { _iconsID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院类别ID", ShortCode = "HTypeID", Desc = "医院类别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? HTypeID
		{
			get { return _hTypeID; }
			set { _hTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院级别ID", ShortCode = "LevelID", Desc = "医院级别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LevelID
		{
			get { return _levelID; }
			set { _levelID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 100)
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
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 300)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 300)
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
        [DataDesc(CName = "医院级别名称", ShortCode = "LevelName", Desc = "医院级别名称", ContextType = SysDic.All, Length = 30)]
        public virtual string LevelName
		{
			get { return _levelName; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for LevelName", value, value.ToString());
				_levelName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "医院类别名称", ShortCode = "HTypeName", Desc = "医院类别名称", ContextType = SysDic.All, Length = 30)]
        public virtual string HTypeName
		{
			get { return _hTypeName; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for HTypeName", value, value.ToString());
				_hTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "坐标位置", ShortCode = "Postion", Desc = "坐标位置", ContextType = SysDic.All, Length = 30)]
        public virtual string Postion
		{
			get { return _postion; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for Postion", value, value.ToString());
				_postion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "省份名称", ShortCode = "ProvinceName", Desc = "省份名称", ContextType = SysDic.All, Length = 20)]
        public virtual string ProvinceName
		{
			get { return _provinceName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ProvinceName", value, value.ToString());
				_provinceName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "城市名称", ShortCode = "CityName", Desc = "城市名称", ContextType = SysDic.All, Length = 20)]
        public virtual string CityName
		{
			get { return _cityName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CityName", value, value.ToString());
				_cityName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "省份ID", ShortCode = "ProvinceID", Desc = "省份ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProvinceID
		{
			get { return _provinceID; }
			set { _provinceID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "城市ID", ShortCode = "CityID", Desc = "城市ID", ContextType = SysDic.All, Length = 8)]
        public virtual long CityID
		{
			get { return _cityID; }
			set { _cityID = value; }
		}

        [DataMember]
        [DataDesc(CName = "医院编码", ShortCode = "HospitalCode", Desc = "医院编码", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalCode
		{
			get { return _hospitalCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HospitalCode", value, value.ToString());
				_hospitalCode = value;
			}
		}

		
		#endregion
	}
	#endregion
}