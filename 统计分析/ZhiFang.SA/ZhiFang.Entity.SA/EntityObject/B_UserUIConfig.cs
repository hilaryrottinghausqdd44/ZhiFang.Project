using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.SA
{
    #region BUserUIConfig

    /// <summary>
    /// BUserUIConfig object for NHibernate mapped table 'B_UserUIConfig'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BUserUIConfig", ShortCode = "BUserUIConfig", Desc = "")]
    public class BUserUIConfig : BaseEntity
    {
        #region Member Variables

        protected string _userUIKey;
        protected string _userUIName;
        protected long? _templateTypeID;
        protected string _templateTypeCName;
        protected long? _uITypeID;
        protected string _uITypeName;
        protected long? _moduleId;
        protected long? _empID;
        protected bool? _isDefault;
        protected string _comment;
        protected int _dispOrder;
        protected bool? _isUse;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public BUserUIConfig() { }

        public BUserUIConfig(long labID, string userUIKey, string userUIName, long templateTypeID, string templateTypeCName, long uITypeID, string uITypeName, long moduleId, long empID, bool isDefault, string comment, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._userUIKey = userUIKey;
            this._userUIName = userUIName;
            this._templateTypeID = templateTypeID;
            this._templateTypeCName = templateTypeCName;
            this._uITypeID = uITypeID;
            this._uITypeName = uITypeName;
            this._moduleId = moduleId;
            this._empID = empID;
            this._isDefault = isDefault;
            this._comment = comment;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserUIKey", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string UserUIKey
        {
            get { return _userUIKey; }
            set
            {
                _userUIKey = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserUIName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string UserUIName
        {
            get { return _userUIName; }
            set
            {
                _userUIName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TemplateTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? TemplateTypeID
        {
            get { return _templateTypeID; }
            set { _templateTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TemplateTypeCName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string TemplateTypeCName
        {
            get { return _templateTypeCName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TemplateTypeCName", value, value.ToString());
                _templateTypeCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "UITypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? UITypeID
        {
            get { return _uITypeID; }
            set { _uITypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UITypeName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string UITypeName
        {
            get { return _uITypeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for UITypeName", value, value.ToString());
                _uITypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ModuleId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ModuleId
        {
            get { return _moduleId; }
            set { _moduleId = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EmpID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? EmpID
        {
            get { return _empID; }
            set { _empID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDefault", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool? IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.NText, Length =Int32.MaxValue)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
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
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool? IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}