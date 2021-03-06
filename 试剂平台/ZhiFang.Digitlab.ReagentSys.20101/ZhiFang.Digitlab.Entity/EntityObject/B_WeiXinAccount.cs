using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BWeiXinAccount

	/// <summary>
	/// BWeiXinAccount object for NHibernate mapped table 'B_WeiXinAccount'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微信账户", ClassCName = "BWeiXinAccount", ShortCode = "BWeiXinAccount", Desc = "微信账户")]
	public class BWeiXinAccount : BaseEntity
	{
		#region Member Variables
		
        protected long? _iconsID;
        protected string _weiXinAccount;
        protected string _userName;
        protected long? _sexID;
        protected string _passWord;
        protected string _name;
        protected DateTime? _brithday;
        protected string _mobileCode;
        protected string _iDNumber;
        protected string _mediCare;
        protected string _email;
        protected long? _countryID;
        protected long? _provinceID;
        protected long? _cityID;
        protected string _countryName;
        protected string _provinceName;
        protected string _cityName;
        protected string _language;
        protected bool _loginInputPasswordFlag;
        protected string _comment;
        protected DateTime? _concernTime;
        protected DateTime? _addTime;
        protected DateTime? _lastLoginTime;
        protected string _lastLoginAddressCoordinate;
        protected string _headImgUrl;
		protected BAccountType _bAccountType;
		protected BWeiXinUserGroup _bWeiXinUserGroup;
		protected IList<BWeiXinEmpLink> _bWeiXinEmpLinkList; 

		#endregion

		#region Constructors

		public BWeiXinAccount() { }

		public BWeiXinAccount( long labID, long iconsID, string weiXinAccount, string userName, long sexID, string passWord, string name, DateTime brithday, string mobileCode, string iDNumber, string mediCare, string email, long countryID, long provinceID, long cityID, string countryName, string provinceName, string cityName, string language, bool loginInputPasswordFlag, string comment, DateTime concernTime, DateTime addTime, byte[] dataTimeStamp, DateTime lastLoginTime, string lastLoginAddressCoordinate, string headImgUrl, BAccountType bAccountType, BWeiXinUserGroup bWeiXinUserGroup )
		{
			this._labID = labID;
			this._iconsID = iconsID;
			this._weiXinAccount = weiXinAccount;
			this._userName = userName;
			this._sexID = sexID;
			this._passWord = passWord;
			this._name = name;
			this._brithday = brithday;
			this._mobileCode = mobileCode;
			this._iDNumber = iDNumber;
			this._mediCare = mediCare;
			this._email = email;
			this._countryID = countryID;
			this._provinceID = provinceID;
			this._cityID = cityID;
			this._countryName = countryName;
			this._provinceName = provinceName;
			this._cityName = cityName;
			this._language = language;
			this._loginInputPasswordFlag = loginInputPasswordFlag;
			this._comment = comment;
			this._concernTime = concernTime;
			this._addTime = addTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lastLoginTime = lastLoginTime;
			this._lastLoginAddressCoordinate = lastLoginAddressCoordinate;
			this._headImgUrl = headImgUrl;
			this._bAccountType = bAccountType;
			this._bWeiXinUserGroup = bWeiXinUserGroup;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "IconsID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? IconsID
		{
			get { return _iconsID; }
			set { _iconsID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WeiXinAccount", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string WeiXinAccount
		{
			get { return _weiXinAccount; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WeiXinAccount", value, value.ToString());
				_weiXinAccount = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string UserName
		{
			get { return _userName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
				_userName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SexID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? SexID
		{
			get { return _sexID; }
			set { _sexID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PassWord", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PassWord
		{
			get { return _passWord; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PassWord", value, value.ToString());
				_passWord = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Brithday", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Brithday
		{
			get { return _brithday; }
			set { _brithday = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MobileCode", Desc = "", ContextType = SysDic.All, Length = 11)]
        public virtual string MobileCode
		{
			get { return _mobileCode; }
			set
			{
				if ( value != null && value.Length > 11)
					throw new ArgumentOutOfRangeException("Invalid value for MobileCode", value, value.ToString());
				_mobileCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IDNumber", Desc = "", ContextType = SysDic.All, Length = 18)]
        public virtual string IDNumber
		{
			get { return _iDNumber; }
			set
			{
				if ( value != null && value.Length > 18)
					throw new ArgumentOutOfRangeException("Invalid value for IDNumber", value, value.ToString());
				_iDNumber = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MediCare", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string MediCare
		{
			get { return _mediCare; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for MediCare", value, value.ToString());
				_mediCare = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Email", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string Email
		{
			get { return _email; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for Email", value, value.ToString());
				_email = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CountryID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? CountryID
		{
			get { return _countryID; }
			set { _countryID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ProvinceID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? ProvinceID
		{
			get { return _provinceID; }
			set { _provinceID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CityID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? CityID
		{
			get { return _cityID; }
			set { _cityID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CountryName", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string CountryName
		{
			get { return _countryName; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for CountryName", value, value.ToString());
				_countryName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProvinceName", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string ProvinceName
		{
			get { return _provinceName; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for ProvinceName", value, value.ToString());
				_provinceName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CityName", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string CityName
		{
			get { return _cityName; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for CityName", value, value.ToString());
				_cityName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Language", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Language
		{
			get { return _language; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Language", value, value.ToString());
				_language = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LoginInputPasswordFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool LoginInputPasswordFlag
		{
			get { return _loginInputPasswordFlag; }
			set { _loginInputPasswordFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 300)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ConcernTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ConcernTime
		{
			get { return _concernTime; }
			set { _concernTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AddTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AddTime
		{
			get { return _addTime; }
			set { _addTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LastLoginTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? LastLoginTime
		{
			get { return _lastLoginTime; }
			set { _lastLoginTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LastLoginAddressCoordinate", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LastLoginAddressCoordinate
		{
			get { return _lastLoginAddressCoordinate; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for LastLoginAddressCoordinate", value, value.ToString());
				_lastLoginAddressCoordinate = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HeadImgUrl", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string HeadImgUrl
		{
			get { return _headImgUrl; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for HeadImgUrl", value, value.ToString());
				_headImgUrl = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "应用系统账户类型", ShortCode = "BAccountType", Desc = "应用系统账户类型")]
		public virtual BAccountType BAccountType
		{
			get { return _bAccountType; }
			set { _bAccountType = value; }
		}

        [DataMember]
        [DataDesc(CName = "微信用户组", ShortCode = "BWeiXinUserGroup", Desc = "微信用户组")]
		public virtual BWeiXinUserGroup BWeiXinUserGroup
		{
			get { return _bWeiXinUserGroup; }
			set { _bWeiXinUserGroup = value; }
		}

        [DataMember]
        [DataDesc(CName = "微信账户绑定员工表", ShortCode = "BWeiXinEmpLinkList", Desc = "微信账户绑定员工表")]
		public virtual IList<BWeiXinEmpLink> BWeiXinEmpLinkList
		{
			get
			{
				if (_bWeiXinEmpLinkList==null)
				{
					_bWeiXinEmpLinkList = new List<BWeiXinEmpLink>();
				}
				return _bWeiXinEmpLinkList;
			}
			set { _bWeiXinEmpLinkList = value; }
		}

        
		#endregion
	}
	#endregion
}