using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BSearchAccount

	/// <summary>
	/// BSearchAccount object for NHibernate mapped table 'B_SearchAccount'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "查询子账户", ClassCName = "BSearchAccount", ShortCode = "BSearchAccount", Desc = "查询子账户")]
	public class BSearchAccount : BaseEntity
	{
		#region Member Variables
		
        protected long? _iconsID;
        protected long? _weiXinUserID;
        protected string _name;
        protected long _sexID;
        protected DateTime? _Birthday;
        protected string _mobileCode;
        protected string _iDNumber;
        protected string _mediCare;
        protected string _email;
        protected bool _loginInputPasswordFlag;
        protected string _WeiXinAccount;
        protected int _UnReadCount;
        protected string _RFIndexList="0";
        protected List<BAccountHospitalSearchContext> _listbahsc = new List<BAccountHospitalSearchContext>();

		#endregion

		#region Constructors

		public BSearchAccount() { }

        public BSearchAccount(long iconsID, long weiXinUserID, string name, long sexID, DateTime Birthday, string mobileCode, string iDNumber, string mediCare, string email, bool loginInputPasswordFlag, DateTime dataAddTime, byte[] dataTimeStamp)
		{
			this._iconsID = iconsID;
			this._weiXinUserID = weiXinUserID;
			this._name = name;
			this._sexID = sexID;
			this._Birthday = Birthday;
			this._mobileCode = mobileCode;
			this._iDNumber = iDNumber;
			this._mediCare = mediCare;
			this._email = email;
			this._loginInputPasswordFlag = loginInputPasswordFlag;
            this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "微信关注用户ID", ShortCode = "WeiXinUserID", Desc = "微信关注用户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? WeiXinUserID
		{
			get { return _weiXinUserID; }
			set { _weiXinUserID = value; }
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
        [DataDesc(CName = "", ShortCode = "SexID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long SexID
		{
			get { return _sexID; }
			set { _sexID = value; }
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
        [DataDesc(CName = "", ShortCode = "LoginInputPasswordFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool LoginInputPasswordFlag
		{
			get { return _loginInputPasswordFlag; }
			set { _loginInputPasswordFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "OpenID", ShortCode = "OpenID", Desc = "OpenID", ContextType = SysDic.All, Length = 8)]
        public virtual string WeiXinAccount
        {
            get { return _WeiXinAccount; }
            set { _WeiXinAccount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ListBAccountHospitalSearchContext", ShortCode = "ListBAccountHospitalSearchContext", Desc = "ListBAccountHospitalSearchContext", ContextType = SysDic.All, Length = 8)]
        public virtual List<BAccountHospitalSearchContext> ListBAccountHospitalSearchContext
        {
            get { return _listbahsc; }
            set { _listbahsc = value; }
        }

        [DataMember]
        [DataDesc(CName = "未读统计", ShortCode = "UnReadCount", Desc = "UnReadCount", ContextType = SysDic.All, Length = 8)]
        public virtual int UnReadCount
        {
            get { return _UnReadCount; }
            set { _UnReadCount = value; }
        }
        [DataMember]
        [DataDesc(CName = "RFIndexList", ShortCode = "RFIndexList", Desc = "UnReadCount", ContextType = SysDic.All, Length = 8)]
        public virtual string RFIndexList
        {
            get { return _RFIndexList; }
            set { _RFIndexList = value; }
        }
		#endregion
	}
	#endregion
}