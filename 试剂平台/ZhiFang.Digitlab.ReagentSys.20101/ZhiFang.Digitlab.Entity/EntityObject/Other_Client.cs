using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherClient

	/// <summary>
	/// OtherClient object for NHibernate mapped table 'Other_Client'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "客户信息表", ClassCName = "OtherClient", ShortCode = "OtherClient", Desc = "客户信息表")]
    public class OtherClient : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _creatorID;
        protected long? _provinceID;
        protected string _name;
        protected string _sName;
        protected string _uRL;
        protected string _phoneNum;
        protected string _address;
        protected string _accountNum;
        protected string _romark;
        protected string _creator;
        protected BClientLevel _bClientLevel;
		protected BClientProperty _bClientProperty;
		protected BClientStyle _bClientStyle;
		protected BClientType _bClientType;

		#endregion

		#region Constructors

		public OtherClient() { }

		public OtherClient( long labID, long creatorID, long provinceID, string name, string sName, string uRL, string phoneNum, string address, string accountNum, string romark, string creator, DateTime dataAddTime, byte[] dataTimeStamp, BClientLevel bClientLevel, BClientProperty bClientProperty, BClientStyle bClientStyle, BClientType bClientType )
		{
			this._labID = labID;
			this._creatorID = creatorID;
			this._provinceID = provinceID;
			this._name = name;
			this._sName = sName;
			this._uRL = uRL;
			this._phoneNum = phoneNum;
			this._address = address;
			this._accountNum = accountNum;
			this._romark = romark;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bClientLevel = bClientLevel;
			this._bClientProperty = bClientProperty;
			this._bClientStyle = bClientStyle;
			this._bClientType = bClientType;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "CreatorID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "省份", ShortCode = "ProvinceID", Desc = "省份", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProvinceID
		{
			get { return _provinceID; }
			set { _provinceID = value; }
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "网址", ShortCode = "URL", Desc = "网址", ContextType = SysDic.All, Length = 100)]
        public virtual string URL
		{
			get { return _uRL; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for URL", value, value.ToString());
				_uRL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "电话", ShortCode = "PhoneNum", Desc = "电话", ContextType = SysDic.All, Length = 40)]
        public virtual string PhoneNum
		{
			get { return _phoneNum; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for PhoneNum", value, value.ToString());
				_phoneNum = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "通讯地址", ShortCode = "Address", Desc = "通讯地址", ContextType = SysDic.All, Length = 200)]
        public virtual string Address
		{
			get { return _address; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
				_address = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "账号", ShortCode = "AccountNum", Desc = "账号", ContextType = SysDic.All, Length = 40)]
        public virtual string AccountNum
		{
			get { return _accountNum; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for AccountNum", value, value.ToString());
				_accountNum = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Romark", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Romark
		{
			get { return _romark; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Romark", value, value.ToString());
				_romark = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.All, Length = 20)]
        public virtual string Creator
		{
			get { return _creator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
				_creator = value;
			}
		}
        
        [DataMember]
        [DataDesc(CName = "客户信息表", ShortCode = "BClientLevel", Desc = "客户信息表", ContextType = SysDic.All)]
		public virtual BClientLevel BClientLevel
		{
			get { return _bClientLevel; }
			set { _bClientLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "客户信息表", ShortCode = "BClientProperty", Desc = "客户信息表", ContextType = SysDic.All)]
		public virtual BClientProperty BClientProperty
		{
			get { return _bClientProperty; }
			set { _bClientProperty = value; }
		}

        [DataMember]
        [DataDesc(CName = "客户信息表", ShortCode = "BClientStyle", Desc = "客户信息表", ContextType = SysDic.All)]
		public virtual BClientStyle BClientStyle
		{
			get { return _bClientStyle; }
			set { _bClientStyle = value; }
		}

        [DataMember]
        [DataDesc(CName = "客户信息表", ShortCode = "BClientType", Desc = "客户信息表", ContextType = SysDic.All)]
		public virtual BClientType BClientType
		{
			get { return _bClientType; }
			set { _bClientType = value; }
		}

		#endregion
	}
	#endregion
}