using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaCenOrg

    /// <summary>
    /// ReaCenOrg object for NHibernate mapped table 'Rea_CenOrg'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "机构表", ClassCName = "ReaCenOrg", ShortCode = "ReaCenOrg", Desc = "机构表")]
    public class ReaCenOrg : BaseEntity
    {
        #region Member Variables

        protected int? _orgNo;
        protected int? _orgType;
        protected long? _pOrgID;
        protected int? _pOrgNo;
        protected long? _platformOrgID;
        protected int? _platformOrgNo;
        protected long? _pPlatformOrgID;
        protected int? _pPlatformOrgNo;
        protected string _cName;
        protected string _eName;
        protected string _serverIP;
        protected string _serverPort;
        protected string _shortCode;
        protected string _address;
        protected string _contact;
        protected string _tel;
        protected string _tel1;
        protected string _hotTel;
        protected string _hotTel1;
        protected string _fox;
        protected string _email;
        protected string _webAddress;
        protected string _memo;
        protected int _dispOrder;
        protected int _visible;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected DateTime? _dataUpdateTime;
        protected string _openingBank;
        protected string _taxNumber;
        protected string _bankName;
        protected string _bankAccount;
        protected string _identificationCode;
        protected string _matchCode;
        protected int _nextBillType;
        #endregion

        #region Constructors

        public ReaCenOrg() { }

        public ReaCenOrg(long labID, int orgNo, int orgType, long pOrgID, int pOrgNo, long platformOrgID, int platformOrgNo, long pPlatformOrgID, int pPlatformOrgNo, string cName, string eName, string serverIP, string serverPort, string shortCode, string address, string contact, string tel, string tel1, string hotTel, string hotTel1, string fox, string email, string webAddress, string memo, int dispOrder, int visible, string zX1, string zX2, string zX3, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._orgNo = orgNo;
            this._orgType = orgType;
            this._pOrgID = pOrgID;
            this._pOrgNo = pOrgNo;
            this._platformOrgID = platformOrgID;
            this._platformOrgNo = platformOrgNo;
            this._pPlatformOrgID = pPlatformOrgID;
            this._pPlatformOrgNo = pPlatformOrgNo;
            this._cName = cName;
            this._eName = eName;
            this._serverIP = serverIP;
            this._serverPort = serverPort;
            this._shortCode = shortCode;
            this._address = address;
            this._contact = contact;
            this._tel = tel;
            this._tel1 = tel1;
            this._hotTel = hotTel;
            this._hotTel1 = hotTel1;
            this._fox = fox;
            this._email = email;
            this._webAddress = webAddress;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "开户名称", ShortCode = "OpeningBank", Desc = "开户名称", ContextType = SysDic.All, Length = 100)]
        public virtual string OpeningBank
        {
            get { return _openingBank; }
            set
            {
                _openingBank = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "税号", ShortCode = "TaxNumber", Desc = "税号", ContextType = SysDic.All, Length = 100)]
        public virtual string TaxNumber
        {
            get { return _taxNumber; }
            set
            {
                _taxNumber = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "开户银行", ShortCode = "BankName", Desc = "开户银行", ContextType = SysDic.All, Length = 100)]
        public virtual string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "银行帐号", ShortCode = "BankAccount", Desc = "银行帐号", ContextType = SysDic.All, Length = 100)]
        public virtual string BankAccount
        {
            get { return _bankAccount; }
            set
            {
                _bankAccount = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "开户许可证", ShortCode = "IdentificationCode", Desc = "开户许可证", ContextType = SysDic.All, Length = 200)]
        public virtual string IdentificationCode
        {
            get { return _identificationCode; }
            set
            {
                _identificationCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "机构编号", ShortCode = "OrgNo", Desc = "机构编号", ContextType = SysDic.All, Length = 4)]
        public virtual int? OrgNo
        {
            get { return _orgNo; }
            set { _orgNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "机构类型", ShortCode = "OrgType", Desc = "机构类型", ContextType = SysDic.All, Length = 4)]
        public virtual int? OrgType
        {
            get { return _orgType; }
            set { _orgType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "POrgID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? POrgID
        {
            get { return _pOrgID; }
            set { _pOrgID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "POrgNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? POrgNo
        {
            get { return _pOrgNo; }
            set { _pOrgNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "平台机构ID", ShortCode = "PlatformOrgID", Desc = "平台机构ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PlatformOrgID
        {
            get { return _platformOrgID; }
            set { _platformOrgID = value; }
        }

        [DataMember]
        [DataDesc(CName = "平台机构编码", ShortCode = "PlatformOrgNo", Desc = "平台机构编码", ContextType = SysDic.All, Length = 4)]
        public virtual int? PlatformOrgNo
        {
            get { return _platformOrgNo; }
            set { _platformOrgNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PPlatformOrgID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? PPlatformOrgID
        {
            get { return _pPlatformOrgID; }
            set { _pPlatformOrgID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PPlatformOrgNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? PPlatformOrgNo
        {
            get { return _pPlatformOrgNo; }
            set { _pPlatformOrgNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "中文名", ShortCode = "CName", Desc = "中文名", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "英文名", ShortCode = "EName", Desc = "英文名", ContextType = SysDic.All, Length = 100)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "服务器IP", ShortCode = "ServerIP", Desc = "服务器IP", ContextType = SysDic.All, Length = 100)]
        public virtual string ServerIP
        {
            get { return _serverIP; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ServerIP", value, value.ToString());
                _serverIP = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "服务器端口", ShortCode = "ServerPort", Desc = "服务器端口", ContextType = SysDic.All, Length = 10)]
        public virtual string ServerPort
        {
            get { return _serverPort; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for ServerPort", value, value.ToString());
                _serverPort = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "ShortCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "机构地址", ShortCode = "Address", Desc = "机构地址", ContextType = SysDic.All, Length = 100)]
        public virtual string Address
        {
            get { return _address; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
                _address = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "联系人", ShortCode = "Contact", Desc = "联系人", ContextType = SysDic.All, Length = 100)]
        public virtual string Contact
        {
            get { return _contact; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Contact", value, value.ToString());
                _contact = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "电话", ShortCode = "Tel", Desc = "电话", ContextType = SysDic.All, Length = 500)]
        public virtual string Tel
        {
            get { return _tel; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Tel", value, value.ToString());
                _tel = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "电话1", ShortCode = "Tel1", Desc = "电话1", ContextType = SysDic.All, Length = 500)]
        public virtual string Tel1
        {
            get { return _tel1; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Tel1", value, value.ToString());
                _tel1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "热线电话", ShortCode = "HotTel", Desc = "热线电话", ContextType = SysDic.All, Length = 500)]
        public virtual string HotTel
        {
            get { return _hotTel; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for HotTel", value, value.ToString());
                _hotTel = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "热线电话1", ShortCode = "HotTel1", Desc = "热线电话1", ContextType = SysDic.All, Length = 500)]
        public virtual string HotTel1
        {
            get { return _hotTel1; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for HotTel1", value, value.ToString());
                _hotTel1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "传真", ShortCode = "Fox", Desc = "传真", ContextType = SysDic.All, Length = 500)]
        public virtual string Fox
        {
            get { return _fox; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Fox", value, value.ToString());
                _fox = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "邮箱", ShortCode = "Email", Desc = "邮箱", ContextType = SysDic.All, Length = 50)]
        public virtual string Email
        {
            get { return _email; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Email", value, value.ToString());
                _email = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "网址", ShortCode = "WebAddress", Desc = "网址", ContextType = SysDic.All, Length = 100)]
        public virtual string WebAddress
        {
            get { return _webAddress; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for WebAddress", value, value.ToString());
                _webAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 100)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
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
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
                _zX3 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "物资接口对照码", ShortCode = "MatchCode", Desc = "物资接口对照码", ContextType = SysDic.All, Length = 100)]
        public virtual string MatchCode
        {
            get { return _matchCode; }
            set { _matchCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "单据类型", ShortCode = "NextBillType", Desc = "1销售/2共建/3调拨", ContextType = SysDic.All, Length = 100)]
        public virtual int NextBillType
        {
            get { return _nextBillType; }
            set { _nextBillType = value; }
        }
        #endregion
    }
    #endregion
}