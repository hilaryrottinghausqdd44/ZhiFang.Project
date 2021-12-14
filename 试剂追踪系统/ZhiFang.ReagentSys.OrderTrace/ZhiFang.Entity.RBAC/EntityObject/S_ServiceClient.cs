using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region SServiceClient

    /// <summary>
    /// SServiceClient只有LabId,并且LabID为主键,没有ID,使用时需要注意
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SServiceClient", ShortCode = "SServiceClient", Desc = "")]
    public class SServiceClient : BaseEntity
    {
        #region Member Variables

        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _comment;
        protected long? _countryID;
        protected long? _provinceID;
        protected long? _cityID;
        protected string _countryName;
        protected string _provinceName;
        protected string _cityName;
        protected long? _sClevelID;
        protected string _principal;
        protected string _linkMan;
        protected string _phoneNum;
        protected string _phoneNum2;
        protected string _address;
        protected string _mailNo;
        protected string _emall;
        protected long _clientType;
        protected string _bman;
        protected string _uploadType;
        protected string _inputDataType;
        protected string _clientArea;
        protected long _clientStyleID;
        protected string _clientStyleName;
        protected string _webLisSourceOrgID;
        protected string _groupName;
        protected bool _isUse;


        #endregion

        #region Constructors

        public SServiceClient() { }

        public SServiceClient(string name, string sName, string shortcode, string pinYinZiTou, int dispOrder, string comment, long countryID, long provinceID, long cityID, string countryName, string provinceName, string cityName, long sClevelID, string principal, string linkMan, string phoneNum, string phoneNum2, string address, string mailNo, string emall, long clientType, string bman, string uploadType, string inputDataType, string clientArea, long clientStyleID, string clientStyleName, string webLisSourceOrgID, string groupName, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp)
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
            this._countryName = countryName;
            this._provinceName = provinceName;
            this._cityName = cityName;
            this._sClevelID = sClevelID;
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
            this._clientStyleID = clientStyleID;
            this._clientStyleName = clientStyleName;
            this._webLisSourceOrgID = webLisSourceOrgID;
            this._groupName = groupName;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "平台客户ID", ShortCode = "SYSID", Desc = "平台客户ID", ContextType = SysDic.Number, Length = 8)]
        public new virtual long LabID
        {
            get
            {
                if (_labID <= 0)
                    _labID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                return _labID;
            }
            set { _labID = value; }
        }

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                _name = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
                _shortcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = -1)]
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
        [DataDesc(CName = "", ShortCode = "CountryName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CountryName
        {
            get { return _countryName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CountryName", value, value.ToString());
                _countryName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProvinceName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ProvinceName
        {
            get { return _provinceName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for ProvinceName", value, value.ToString());
                _provinceName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CityName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CityName
        {
            get { return _cityName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CityName", value, value.ToString());
                _cityName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SClevelID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SClevelID
        {
            get { return _sClevelID; }
            set { _sClevelID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Principal", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Principal
        {
            get { return _principal; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for Principal", value, value.ToString());
                _principal = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkMan", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string LinkMan
        {
            get { return _linkMan; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for LinkMan", value, value.ToString());
                _linkMan = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhoneNum", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string PhoneNum
        {
            get { return _phoneNum; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for PhoneNum", value, value.ToString());
                _phoneNum = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhoneNum2", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string PhoneNum2
        {
            get { return _phoneNum2; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for PhoneNum2", value, value.ToString());
                _phoneNum2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Address", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string Address
        {
            get { return _address; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
                _address = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MailNo", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string MailNo
        {
            get { return _mailNo; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for MailNo", value, value.ToString());
                _mailNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Emall", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Emall
        {
            get { return _emall; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for Emall", value, value.ToString());
                _emall = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ClientType", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ClientType
        {
            get { return _clientType; }
            set { _clientType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bman", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Bman
        {
            get { return _bman; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for Bman", value, value.ToString());
                _bman = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UploadType", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string UploadType
        {
            get { return _uploadType; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for UploadType", value, value.ToString());
                _uploadType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InputDataType", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string InputDataType
        {
            get { return _inputDataType; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for InputDataType", value, value.ToString());
                _inputDataType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ClientArea", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ClientArea
        {
            get { return _clientArea; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for ClientArea", value, value.ToString());
                _clientArea = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ClientStyleID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ClientStyleID
        {
            get { return _clientStyleID; }
            set { _clientStyleID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ClientStyleName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ClientStyleName
        {
            get { return _clientStyleName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for ClientStyleName", value, value.ToString());
                _clientStyleName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WebLisSourceOrgID", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string WebLisSourceOrgID
        {
            get { return _webLisSourceOrgID; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgID", value, value.ToString());
                _webLisSourceOrgID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GroupName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string GroupName
        {
            get { return _groupName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GroupName", value, value.ToString());
                _groupName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }


        #endregion
    }
    #endregion
}