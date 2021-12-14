using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
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
		
        protected long? _weiXinUserGroupID;
        protected long? _iconsID;
        protected long? _accountTypeID;
        protected string _weiXinAccount;
        protected string _userName;
        protected long _sexID;
        protected string _passWord;
        protected string _name;
        protected DateTime? _Birthday;
        protected string _mobileCode;
        protected string _iDNumber;
        protected string _mediCare;
        protected string _email;
        protected long _countryID;
        protected long _provinceID;
        protected long _cityID;
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
        protected string _headimgurl;
        protected DateTime? _ReadAgreement;

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "WeiXinUserGroupID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? WeiXinUserGroupID
		{
			get { return _weiXinUserGroupID; }
			set { _weiXinUserGroupID = value; }
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
        [DataDesc(CName = "", ShortCode = "AccountTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? AccountTypeID
		{
			get { return _accountTypeID; }
			set { _accountTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WeiXinAccount", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string WeiXinAccount
		{
			get { return _weiXinAccount; }
			set
			{
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
        public virtual long SexID
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
        [DataDesc(CName = "", ShortCode = "Birthday", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Birthday
		{
			get { return _Birthday; }
			set { _Birthday = value; }
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
        public virtual long CountryID
		{
			get { return _countryID; }
			set { _countryID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ProvinceID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ProvinceID
		{
			get { return _provinceID; }
			set { _provinceID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CityID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CityID
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
        [DataDesc(CName = "", ShortCode = "HeadImgUrl", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual string HeadImgUrl
        {
            get { return _headimgurl; }
            set { _headimgurl = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReadAgreement", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual DateTime? ReadAgreement
        {
            get { return _ReadAgreement; }
            set
            {
                _ReadAgreement = value;
            }
        }
        #endregion
    }
	#endregion
}