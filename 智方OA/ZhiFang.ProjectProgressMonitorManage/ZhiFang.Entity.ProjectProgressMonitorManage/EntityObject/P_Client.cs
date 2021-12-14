using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PClient

    /// <summary>
    /// PClient object for NHibernate mapped table 'P_Client'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户", ClassCName = "PClient", ShortCode = "PClient", Desc = "客户")]
    public class PClient : BaseEntity
    {
        #region Member Variables

        protected string _name;
        protected string _licenceClientName;
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
        protected long? _sCTypeID;
        protected string _SCTypeName;
        protected string _principal;
        protected string _linkMan;
        protected string _phoneNum;
        protected string _phoneNum2;
        protected string _address;
        protected string _mailNo;
        protected string _emall;
        protected string _bman;
        protected long? _ClientAreaID;
        protected string _ClientAreaName;
        protected bool _isUse;
        protected long? _ClientTypeID;
        protected string _ClientTypeName;
        protected long? _HospitalTypeID;
        protected string _HospitalTypeName;
        protected long? _HospitalLevelID;
        protected string _HospitalLevelName;
        protected string _Url;
        protected long? _PSalesManClientLinkID;


        protected IList<PClientLinker> _pClientLinkerList;
        protected IList<PContract> _pContractList;
        protected IList<PTask> _pTaskList;
        private string _VATNumber;
        private string _VATBank;
        private string _VATAccount;

        private string _LicenceCode;
        private string _LicenceKey1;
        private string _LRNo1;
        private string _LicenceKey2;
        private string _LRNo2;

        protected long? _clientNo;
        private bool _isRepeat;
        protected long _clientClassID;
        protected string _clientClassName;
        protected string _equipManager;
        protected string _equipPhone;
        protected string _informationManager;
        protected string _informationPhone;
        protected string _labManager;
        protected string _labPhone;
        protected string _fax;
        protected string _operationMan;
        protected string _operationPhone;
        protected string _hISFactory;
        protected bool _IsContract;
        protected string _ProjectSourceName;
        protected string _AgentName;
        #endregion

        #region Constructors

        public PClient() { }

        public PClient(long labID, string name, string licenceClientName, string sName, string shortcode, string pinYinZiTou, int dispOrder, string comment, long countryID, string countryName, long provinceID, string provinceName, long cityID, string cityName, long sCTypeID, string sCTypeName, string principal, string linkMan, string phoneNum, string phoneNum2, string address, string mailNo, string emall, string bman, long clientAreaID, string clientAreaName, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, long clientTypeID, string clientTypeName, long hospitalLevelID, string hospitalLevelName, string url, long hospitalTypeID, string hospitalTypeName, string vATNumber, string vATBank, string vATAccount, string licenceCode, string licenceKey1, string lRNo1, string licenceKey2, string lRNo2, long? clientNo, bool isRepeat, long clientClassID, string clientClassName, string equipManager, string equipPhone, string informationManager, string informationPhone, string labManager, string labPhone, string fax, string operationMan, string operationPhone, string hISFactory)
        {
            this._labID = labID;
            this._name = name;
            this._licenceClientName = licenceClientName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._dispOrder = dispOrder;
            this._comment = comment;
            this._countryID = countryID;
            this._CountryName = countryName;
            this._provinceID = provinceID;
            this._ProvinceName = provinceName;
            this._cityID = cityID;
            this._CityName = cityName;
            this._sCTypeID = sCTypeID;
            this._SCTypeName = sCTypeName;
            this._principal = principal;
            this._linkMan = linkMan;
            this._phoneNum = phoneNum;
            this._phoneNum2 = phoneNum2;
            this._address = address;
            this._mailNo = mailNo;
            this._emall = emall;
            this._bman = bman;
            this._ClientAreaID = clientAreaID;
            this._ClientAreaName = clientAreaName;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._ClientTypeID = clientTypeID;
            this._ClientTypeName = clientTypeName;
            this._HospitalLevelID = hospitalLevelID;
            this._HospitalLevelName = hospitalLevelName;
            this._Url = url;
            this._HospitalTypeID = hospitalTypeID;
            this._HospitalTypeName = hospitalTypeName;
            this._VATNumber = vATNumber;
            this._VATBank = vATBank;
            this._VATAccount = vATAccount;
            this._LicenceCode = licenceCode;
            this._LicenceKey1 = licenceKey1;
            this._LRNo1 = lRNo1;
            this._LicenceKey2 = licenceKey2;
            this._LRNo2 = lRNo2;
            this._clientNo = clientNo;
            this._isRepeat = isRepeat;
            this._clientClassID = clientClassID;
            this._clientClassName = clientClassName;
            this._equipManager = equipManager;
            this._equipPhone = equipPhone;
            this._informationManager = informationManager;
            this._informationPhone = informationPhone;
            this._labManager = labManager;
            this._labPhone = labPhone;
            this._fax = fax;
            this._operationMan = operationMan;
            this._operationPhone = operationPhone;
            this._hISFactory = hISFactory;
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
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                _name = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "授权客户名称", ShortCode = "LicenceClientName", Desc = "授权客户名称", ContextType = SysDic.All, Length = 500)]
        public virtual string LicenceClientName
        {
            get { return _licenceClientName; }
            set
            {
                _licenceClientName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 500)]
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
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户级别ID", ShortCode = "SCTypeID", Desc = "平台客户级别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SCTypeID
        {
            get { return _sCTypeID; }
            set { _sCTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户级别名称", ShortCode = "SCTypeName", Desc = "客户级别名称", ContextType = SysDic.All, Length = 8)]
        public virtual string SCTypeName
        {
            get { return _SCTypeName; }
            set { _SCTypeName = value; }
        }

        [DataMember]
        [DataDesc(CName = "负责人", ShortCode = "Principal", Desc = "负责人", ContextType = SysDic.All, Length = 40)]
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
        [DataDesc(CName = "联系人", ShortCode = "LinkMan", Desc = "联系人", ContextType = SysDic.All, Length = 40)]
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
        [DataDesc(CName = "联系电话1", ShortCode = "PhoneNum", Desc = "联系电话1", ContextType = SysDic.All, Length = 40)]
        public virtual string PhoneNum
        {
            get { return _phoneNum; }
            set
            {
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
                if (value != null && value.Length > 40)
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
                if (value != null && value.Length > 150)
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
                if (value != null && value.Length > 40)
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
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for Emall", value, value.ToString());
                _emall = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "业务员编码", ShortCode = "Bman", Desc = "业务员编码", ContextType = SysDic.All, Length = 40)]
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
        [DataDesc(CName = "区域ID", ShortCode = "ClientAreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 40)]
        public virtual long? ClientAreaID
        {
            get { return _ClientAreaID; }
            set
            {
                _ClientAreaID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "区域", ShortCode = "ClientArea", Desc = "区域", ContextType = SysDic.All, Length = 40)]
        public virtual string ClientAreaName
        {
            get { return _ClientAreaName; }
            set
            {
                _ClientAreaName = value;
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
        [DataDesc(CName = "客户类别ID", ShortCode = "ClientTypeID", Desc = "客户类别ID", ContextType = SysDic.All, Length = 150)]
        public virtual long? ClientTypeID
        {
            get { return _ClientTypeID; }
            set
            {
                _ClientTypeID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "客户类别名称", ShortCode = "ClientTypeName", Desc = "客户类别名称", ContextType = SysDic.All, Length = 40)]
        public virtual string ClientTypeName
        {
            get { return _ClientTypeName; }
            set
            {
                _ClientTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医院类别ID", ShortCode = "HospitalTypeID", Desc = "医院类别ID", ContextType = SysDic.All, Length = 40)]
        public virtual long? HospitalTypeID
        {
            get { return _HospitalTypeID; }
            set
            {
                _HospitalTypeID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医院类别名称", ShortCode = "HospitalTypeName", Desc = "医院类别名称", ContextType = SysDic.All, Length = 40)]
        public virtual string HospitalTypeName
        {
            get { return _HospitalTypeName; }
            set
            {
                _HospitalTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医院等级ID", ShortCode = "HospitalLevelID", Desc = "医院等级ID", ContextType = SysDic.All, Length = 40)]
        public virtual long? HospitalLevelID
        {
            get { return _HospitalLevelID; }
            set
            {
                _HospitalLevelID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医院等级名称", ShortCode = "HospitalLevelName", Desc = "医院等级名称", ContextType = SysDic.All, Length = 1)]
        public virtual string HospitalLevelName
        {
            get { return _HospitalLevelName; }
            set { _HospitalLevelName = value; }
        }

        [DataMember]
        [DataDesc(CName = "Url", ShortCode = "Url", Desc = "Url", ContextType = SysDic.All, Length = 1)]
        public virtual string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        [DataMember]
        [DataDesc(CName = "联系人", ShortCode = "PClientLinkerList", Desc = "联系人")]
        public virtual IList<PClientLinker> PClientLinkerList
        {
            get
            {
                if (_pClientLinkerList == null)
                {
                    _pClientLinkerList = new List<PClientLinker>();
                }
                return _pClientLinkerList;
            }
            set { _pClientLinkerList = value; }
        }

        [DataMember]
        [DataDesc(CName = "合同", ShortCode = "PContractList", Desc = "合同")]
        public virtual IList<PContract> PContractList
        {
            get
            {
                if (_pContractList == null)
                {
                    _pContractList = new List<PContract>();
                }
                return _pContractList;
            }
            set { _pContractList = value; }
        }

        [DataMember]
        [DataDesc(CName = "任务表", ShortCode = "PTaskList", Desc = "任务表")]
        public virtual IList<PTask> PTaskList
        {
            get
            {
                if (_pTaskList == null)
                {
                    _pTaskList = new List<PTask>();
                }
                return _pTaskList;
            }
            set { _pTaskList = value; }
        }

        [DataMember]
        [DataDesc(CName = "销售人员客户关系ID", ShortCode = "PSalesManClientLinkID", Desc = "销售人员客户关系ID")]
        public virtual long? PSalesManClientLinkID
        {
            get
            {
                return _PSalesManClientLinkID;
            }
            set { _PSalesManClientLinkID = value; }
        }

        [DataMember]
        [DataDesc(CName = "增值税税号", ShortCode = "VATNumber", Desc = "增值税税号")]
        public virtual string VATNumber
        {
            get
            {
                return _VATNumber;
            }
            set { _VATNumber = value; }
        }

        [DataMember]
        [DataDesc(CName = "增值税开户行", ShortCode = "VATBank", Desc = "增值税开户行")]
        public virtual string VATBank
        {
            get
            {
                return _VATBank;
            }
            set { _VATBank = value; }
        }

        [DataMember]
        [DataDesc(CName = "增值税帐号", ShortCode = "VATAccount", Desc = "增值税帐号")]
        public virtual string VATAccount
        {
            get
            {
                return _VATAccount;
            }
            set { _VATAccount = value; }
        }
        [DataMember]
        [DataDesc(CName = "授权编码", ShortCode = "LicenceCode", Desc = "授权编码")]
        public virtual string LicenceCode
        {
            get
            {
                return _LicenceCode;
            }
            set { _LicenceCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "主服务器授权Key", ShortCode = "LicenceKey1", Desc = "主服务器授权Key")]
        public virtual string LicenceKey1
        {
            get
            {
                return _LicenceKey1;
            }
            set { _LicenceKey1 = value; }
        }
        [DataMember]
        [DataDesc(CName = "主服务器授权申请号", ShortCode = "LRNo1", Desc = "主服务器授权申请号")]
        public virtual string LRNo1
        {
            get
            {
                return _LRNo1;
            }
            set { _LRNo1 = value; }
        }
        [DataMember]
        [DataDesc(CName = "备份服务器授权Key", ShortCode = "LicenceKey2", Desc = "备份服务器授权Key")]
        public virtual string LicenceKey2
        {
            get
            {
                return _LicenceKey2;
            }
            set { _LicenceKey2 = value; }
        }
        [DataMember]
        [DataDesc(CName = "备份服务器授权申请号", ShortCode = "LRNo2", Desc = "备份服务器授权申请号")]
        public virtual string LRNo2
        {
            get
            {
                return _LRNo2;
            }
            set { _LRNo2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "客户编码", ShortCode = "ClientNo", Desc = "客户编码")]
        public virtual long? ClientNo
        {
            get
            {
                return _clientNo;
            }
            set { _clientNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "重复标记", ShortCode = "IsRepeat", Desc = "重复标记", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsRepeat
        {
            get { return _isRepeat; }
            set { _isRepeat = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ClientClassID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ClientClassID
        {
            get { return _clientClassID; }
            set { _clientClassID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ClientClassName", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string ClientClassName
        {
            get { return _clientClassName; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for ClientClassName", value, value.ToString());
                _clientClassName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipManager", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipManager
        {
            get { return _equipManager; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EquipManager", value, value.ToString());
                _equipManager = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipPhone", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipPhone
        {
            get { return _equipPhone; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EquipPhone", value, value.ToString());
                _equipPhone = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InformationManager", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string InformationManager
        {
            get { return _informationManager; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for InformationManager", value, value.ToString());
                _informationManager = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InformationPhone", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string InformationPhone
        {
            get { return _informationPhone; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for InformationPhone", value, value.ToString());
                _informationPhone = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabManager", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LabManager
        {
            get { return _labManager; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for LabManager", value, value.ToString());
                _labManager = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabPhone", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LabPhone
        {
            get { return _labPhone; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for LabPhone", value, value.ToString());
                _labPhone = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Fax", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Fax
        {
            get { return _fax; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Fax", value, value.ToString());
                _fax = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperationMan", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OperationMan
        {
            get { return _operationMan; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OperationMan", value, value.ToString());
                _operationMan = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperationPhone", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OperationPhone
        {
            get { return _operationPhone; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OperationPhone", value, value.ToString());
                _operationPhone = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HISFactory", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string HISFactory
        {
            get { return _hISFactory; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for HISFactory", value, value.ToString());
                _hISFactory = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsContract", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual bool IsContract
        {
            get { return _IsContract; }
            set
            {
                _IsContract = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProjectSourceName", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string ProjectSourceName
        {
            get { return _ProjectSourceName; }
            set
            {
                _ProjectSourceName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AgentName", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string AgentName
        {
            get { return _AgentName; }
            set
            {
                _AgentName = value;
            }
        }

        #endregion
    }
    #endregion
}