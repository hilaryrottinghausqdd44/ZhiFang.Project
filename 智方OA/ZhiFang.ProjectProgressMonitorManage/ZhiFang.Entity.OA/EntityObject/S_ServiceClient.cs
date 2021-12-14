using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA
{
	#region SServiceClient

	/// <summary>
	/// SServiceClient object for NHibernate mapped table 'S_ServiceClient'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "平台客户", ClassCName = "SServiceClient", ShortCode = "SServiceClient", Desc = "平台客户")]
	public class SServiceClient : BaseEntityService
	{
		#region Member Variables
		
        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _comment;
        protected long? _countryID;
        protected string _CountryName;
        protected long? _provinceID;
        protected string _ProvinceName;
        protected long? _cityID;
        protected string _CityName;
        protected string _principal;
        protected string _linkMan;
        protected string _phoneNum;
        protected string _phoneNum2;
        protected string _address;
        protected string _mailNo;
        protected string _emall;
        protected long? _clientType;
        protected string _bman;
        protected string _uploadType;
        protected string _inputDataType;
        protected string _clientArea;
        protected long? _clientStyle;
        protected string _ClientStyleName;
        protected string _webLisSourceOrgID;
        protected string _groupName;
        protected bool _isUse;
		protected SServiceClientlevel _sServiceClientlevel;

		#endregion

		#region Constructors

		public SServiceClient() { }

		public SServiceClient( string name, string sName, string shortcode, string pinYinZiTou, int dispOrder, string comment, long countryID, long provinceID, long cityID, string principal, string linkMan, string phoneNum, string phoneNum2, string address, string mailNo, string emall, long clientType, string bman, string uploadType, string inputDataType, string clientArea, long clientStyle, string webLisSourceOrgID, string groupName, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, SServiceClientlevel sServiceClientlevel )
		{
			this._name = name;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._countryID = countryID;
			this._provinceID = provinceID;
			this._cityID = cityID;
			this._principal = principal;
			this._linkMan = linkMan;
			this._phoneNum = phoneNum;
			this._phoneNum2 = phoneNum2;
			this._address = address;
			this._mailNo = mailNo;
			this._emall = emall;
			this._clientType = clientType;
			this._bman = bman;
			this._uploadType = uploadType;
			this._inputDataType = inputDataType;
			this._clientArea = clientArea;
			this._clientStyle = clientStyle;
			this._webLisSourceOrgID = webLisSourceOrgID;
			this._groupName = groupName;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._sServiceClientlevel = sServiceClientlevel;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 500)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 500)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 500)
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{				
				_comment = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "国家ID", ShortCode = "CountryID", Desc = "国家ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CountryID
        {
            get { return _countryID; }
            set { _countryID = value; }
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
        public virtual long? CityID
        {
            get { return _cityID; }
            set { _cityID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "国家名称", ShortCode = "CountryName", Desc = "国家名称", ContextType = SysDic.All, Length = 8)]
        public virtual string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "省份名称", ShortCode = "ProvinceName", Desc = "省份名称", ContextType = SysDic.All, Length = 8)]
        public virtual string ProvinceName
        {
            get { return _ProvinceName; }
            set { _ProvinceName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "城市名称", ShortCode = "CityName", Desc = "城市名称", ContextType = SysDic.All, Length = 8)]
        public virtual string CityName
        {
            get { return _CityName; }
            set { _CityName = value; }
        }

        [DataMember]
        [DataDesc(CName = "负责人", ShortCode = "Principal", Desc = "负责人", ContextType = SysDic.All, Length = 40)]
        public virtual string Principal
		{
			get { return _principal; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Principal", value, value.ToString());
				_principal = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "联系人", ShortCode = "LinkMan", Desc = "联系人", ContextType = SysDic.All, Length = 40)]
        public virtual string LinkMan
		{
			get { return _linkMan; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for LinkMan", value, value.ToString());
				_linkMan = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "联系电话1", ShortCode = "PhoneNum", Desc = "联系电话1", ContextType = SysDic.All, Length = 40)]
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
        [DataDesc(CName = "联系电话2", ShortCode = "PhoneNum2", Desc = "联系电话2", ContextType = SysDic.All, Length = 40)]
        public virtual string PhoneNum2
		{
			get { return _phoneNum2; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for PhoneNum2", value, value.ToString());
				_phoneNum2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "地址", ShortCode = "Address", Desc = "地址", ContextType = SysDic.All, Length = 150)]
        public virtual string Address
		{
			get { return _address; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
				_address = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "邮编", ShortCode = "MailNo", Desc = "邮编", ContextType = SysDic.All, Length = 40)]
        public virtual string MailNo
		{
			get { return _mailNo; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for MailNo", value, value.ToString());
				_mailNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "电子邮件", ShortCode = "Emall", Desc = "电子邮件", ContextType = SysDic.All, Length = 40)]
        public virtual string Emall
		{
			get { return _emall; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Emall", value, value.ToString());
				_emall = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实验室类型：", ShortCode = "ClientType", Desc = "实验室类型：", ContextType = SysDic.All, Length = 8)]
		public virtual long? ClientType
		{
			get { return _clientType; }
			set { _clientType = value; }
		}

        [DataMember]
        [DataDesc(CName = "业务员编码", ShortCode = "Bman", Desc = "业务员编码", ContextType = SysDic.All, Length = 40)]
        public virtual string Bman
		{
			get { return _bman; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Bman", value, value.ToString());
				_bman = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "上传类型", ShortCode = "UploadType", Desc = "上传类型", ContextType = SysDic.All, Length = 40)]
        public virtual string UploadType
		{
			get { return _uploadType; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for UploadType", value, value.ToString());
				_uploadType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "数据来源", ShortCode = "InputDataType", Desc = "数据来源", ContextType = SysDic.All, Length = 40)]
        public virtual string InputDataType
		{
			get { return _inputDataType; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for InputDataType", value, value.ToString());
				_inputDataType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "区域", ShortCode = "ClientArea", Desc = "区域", ContextType = SysDic.All, Length = 40)]
        public virtual string ClientArea
		{
			get { return _clientArea; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ClientArea", value, value.ToString());
				_clientArea = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "级别", ShortCode = "ClientStyle", Desc = "级别", ContextType = SysDic.All, Length = 8)]
		public virtual long? ClientStyle
		{
			get { return _clientStyle; }
			set { _clientStyle = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医疗水平级别名称", ShortCode = "ClientStyleName", Desc = "医疗水平级别名称", ContextType = SysDic.All, Length = 8)]
        public virtual string ClientStyleName
        {
            get { return _ClientStyleName; }
            set { _ClientStyleName = value; }
        }        

        [DataMember]
        [DataDesc(CName = "区域医疗机构编码", ShortCode = "WebLisSourceOrgID", Desc = "区域医疗机构编码", ContextType = SysDic.All, Length = 30)]
        public virtual string WebLisSourceOrgID
		{
			get { return _webLisSourceOrgID; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgID", value, value.ToString());
				_webLisSourceOrgID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "办事处", ShortCode = "GroupName", Desc = "办事处", ContextType = SysDic.All, Length = 50)]
        public virtual string GroupName
		{
			get { return _groupName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for GroupName", value, value.ToString());
				_groupName = value;
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
        [DataDesc(CName = "平台客户级别：普通客户、试用客户、付费客户、VIP客户。", ShortCode = "SServiceClientlevel", Desc = "平台客户级别：普通客户、试用客户、付费客户、VIP客户。")]
		public virtual SServiceClientlevel SServiceClientlevel
		{
			get { return _sServiceClientlevel; }
			set { _sServiceClientlevel = value; }
		}

        
		#endregion
	}
	#endregion
}