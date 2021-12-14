using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
    #region BAccountHospitalSearchContext

    /// <summary>
    /// BAccountHospitalSearchContext object for NHibernate mapped table 'B_AccountHospitalSearchContext'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "账户医院查询条件具体内容", ClassCName = "BAccountHospitalSearchContext", ShortCode = "BAccountHospitalSearchContext", Desc = "账户医院查询条件具体内容")]
    public class BAccountHospitalSearchContext : BaseEntity
    {
        #region Member Variables

        protected string _hospitalCode;
        protected long? _accountID;
        protected long? _HospitalSearchID;
        protected string _searchContext;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected bool _readedFlag;
        protected int _unReadCount;
        protected string _FieldsName;
        protected string _FieldsMeaning;
        protected string _FieldsCode;
        protected string _FieldsValue;
        protected string _WeiXinAccount;
        protected string _Comment;
        protected string _HospitalName;
        protected string _Name;

        #endregion

        #region Constructors

        public BAccountHospitalSearchContext() { }

        public BAccountHospitalSearchContext(string hospitalCode, long accountID, string searchContext, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool readedFlag, int unReadCount)
        {
            this._hospitalCode = hospitalCode;
            this._accountID = accountID;
            this._searchContext = searchContext;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._readedFlag = readedFlag;
            this._unReadCount = unReadCount;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalCode
        {
            get { return _hospitalCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HospitalCode", value, value.ToString());
                _hospitalCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "应用系统账户ID", ShortCode = "AccountID", Desc = "应用系统账户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AccountID
        {
            get { return _accountID; }
            set { _accountID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院查询字段ID", ShortCode = "HospitalSearchID", Desc = "应用系统账户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? HospitalSearchID
        {
            get { return _HospitalSearchID; }
            set { _HospitalSearchID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SearchContext", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string SearchContext
        {
            get { return _searchContext; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for SearchContext", value, value.ToString());
                _searchContext = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReadedFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool ReadedFlag
        {
            get { return _readedFlag; }
            set { _readedFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UnReadCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UnReadCount
        {
            get { return _unReadCount; }
            set { _unReadCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "字段名", ShortCode = "FieldsName", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string FieldsName
        {
            get { return _FieldsName; }
            set
            {
                _FieldsName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "字段含义", ShortCode = "FieldsMeaning", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string FieldsMeaning
        {
            get { return _FieldsMeaning; }
            set
            {
                _FieldsMeaning = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "字段代码", ShortCode = "FieldsCode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string FieldsCode
        {
            get { return _FieldsCode; }
            set
            {
                _FieldsCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "字段值", ShortCode = "FieldsValue", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string FieldsValue
        {
            get { return _FieldsValue; }
            set
            {
                _FieldsValue = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "OpenID", ShortCode = "WeiXinAccount", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string WeiXinAccount
        {
            get { return _WeiXinAccount; }
            set
            {
                _WeiXinAccount = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Comment
        {
            get { return _Comment; }
            set
            {
                _Comment = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "医院名称", ShortCode = "HospitalName", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string HospitalName
        {
            get { return _HospitalName; }
            set
            {
                _HospitalName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "姓名", ShortCode = "姓名", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }
        #endregion
    }
    #endregion
}