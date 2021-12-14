using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region CUser

    /// <summary>
    /// CUser object for NHibernate mapped table 'CUser'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "CUser", ShortCode = "CUser", Desc = "")]
    public class CUser : BaseEntityService
    {
        #region Member Variables

        protected string _userName;
        protected string _userAddress;
        protected string _userPostcode;
        protected string _userTelephone;
        protected string _userFax;
        protected string _userLinkman;
        protected string _userArea;
        protected string _userCName;
        protected string _userFWNo;
        protected string _pTechSQH;
        protected string _lRNo1;
        protected string _lRNo2;
        protected string _licenceInfo;
        protected string _licenceText;
        protected bool _bHint;
        protected string _lHint;
        protected bool _isMapping;
        protected long? _contrastId;
        protected string _contrastCName;
        protected long? _checkId;
        protected string _checkCName;


        #endregion

        #region Constructors

        public CUser() { }

        public CUser(string userName, string userAddress, string userPostcode, string userTelephone, string userFax, string userLinkman, string userArea, string userCName, string userFWNo, string pTechSQH, string lRNo1, string lRNo2, string licenceInfo, string licenceText, bool bHint, string lHint, bool isMapping, byte[] dataTimeStamp, long? contrastId, string contrastCName, long? checkId, string checkCName)
        {
            this._userName = userName;
            this._userAddress = userAddress;
            this._userPostcode = userPostcode;
            this._userTelephone = userTelephone;
            this._userFax = userFax;
            this._userLinkman = userLinkman;
            this._userArea = userArea;
            this._userCName = userCName;
            this._userFWNo = userFWNo;
            this._pTechSQH = pTechSQH;
            this._lRNo1 = lRNo1;
            this._lRNo2 = lRNo2;
            this._licenceInfo = licenceInfo;
            this._licenceText = licenceText;
            this._bHint = bHint;
            this._lHint = lHint;
            this._isMapping = isMapping;
            this._dataTimeStamp = dataTimeStamp;
            this._contrastId = contrastId;
            this._contrastCName = contrastCName;
            this._checkId = checkId;
            this._checkCName = checkCName;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string UserName
        {
            get { return _userName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
                _userName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserAddress", Desc = "", ContextType = SysDic.All, Length = 250)]
        public virtual string UserAddress
        {
            get { return _userAddress; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("Invalid value for UserAddress", value, value.ToString());
                _userAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserPostcode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string UserPostcode
        {
            get { return _userPostcode; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for UserPostcode", value, value.ToString());
                _userPostcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserTelephone", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string UserTelephone
        {
            get { return _userTelephone; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UserTelephone", value, value.ToString());
                _userTelephone = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserFax", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string UserFax
        {
            get { return _userFax; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UserFax", value, value.ToString());
                _userFax = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserLinkman", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UserLinkman
        {
            get { return _userLinkman; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UserLinkman", value, value.ToString());
                _userLinkman = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserArea", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UserArea
        {
            get { return _userArea; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UserArea", value, value.ToString());
                _userArea = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserCName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UserCName
        {
            get { return _userCName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UserCName", value, value.ToString());
                _userCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserFWNo", Desc = "", ContextType = SysDic.All, Length = 6)]
        public virtual string UserFWNo
        {
            get { return _userFWNo; }
            set
            {
                if (value != null && value.Length > 6)
                    throw new ArgumentOutOfRangeException("Invalid value for UserFWNo", value, value.ToString());
                _userFWNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PTechSQH", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual string PTechSQH
        {
            get { return _pTechSQH; }
            set
            {
                if (value != null && value.Length > 4)
                    throw new ArgumentOutOfRangeException("Invalid value for PTechSQH", value, value.ToString());
                _pTechSQH = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LRNo1", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LRNo1
        {
            get { return _lRNo1; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LRNo1", value, value.ToString());
                _lRNo1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LRNo2", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LRNo2
        {
            get { return _lRNo2; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LRNo2", value, value.ToString());
                _lRNo2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LicenceInfo", Desc = "", ContextType = SysDic.All, Length = 255)]
        public virtual string LicenceInfo
        {
            get { return _licenceInfo; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for LicenceInfo", value, value.ToString());
                _licenceInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LicenceText", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string LicenceText
        {
            get { return _licenceText; }
            set
            {
                if (value != null && value.Length > Int32.MaxValue)
                    throw new ArgumentOutOfRangeException("Invalid value for LicenceText", value, value.ToString());
                _licenceText = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BHint", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool BHint
        {
            get { return _bHint; }
            set { _bHint = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LHint", Desc = "", ContextType = SysDic.All, Length = 255)]
        public virtual string LHint
        {
            get { return _lHint; }
            set
            {
                if (value != null && value.Length > 255)
                    throw new ArgumentOutOfRangeException("Invalid value for LHint", value, value.ToString());
                _lHint = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsMapping", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsMapping
        {
            get { return _isMapping; }
            set { _isMapping = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "对比人Id", ShortCode = "ContrastId", Desc = "对比人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ContrastId
        {
            get { return _contrastId; }
            set { _contrastId = value; }
        }

        [DataMember]
        [DataDesc(CName = "对比人名称", ShortCode = "ContrastCName", Desc = "对比人名称", ContextType = SysDic.All, Length = 60)]
        public virtual string ContrastCName
        {
            get { return _contrastCName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for ContrastCName", value, value.ToString());
                _contrastCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人Id", ShortCode = "CheckId", Desc = "审核人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckId
        {
            get { return _checkId; }
            set { _checkId = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人名称", ShortCode = "CheckCName", Desc = "审核人名称", ContextType = SysDic.All, Length = 60)]
        public virtual string CheckCName
        {
            get { return _checkCName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckCName", value, value.ToString());
                _checkCName = value;
            }
        }


        #endregion
    }
    #endregion
}