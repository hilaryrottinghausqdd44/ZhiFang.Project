using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
    #region BDoctorAccount

    /// <summary>
    /// BDoctorAccount object for NHibernate mapped table 'B_DoctorAccount'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "医生账户信息", ClassCName = "BDoctorAccount", ShortCode = "BDoctorAccount", Desc = "医生账户信息")]
    public class BDoctorAccount : BaseEntity
    {
        #region Member Variables

        protected long _AreaID;
        protected long _hospitalID;
        protected long? _weiXinUserID;
        protected string _hospitalCode;
        protected string _hospitalName;
        protected long? _hospitalDeptID;
        protected string _hospitalDeptName;
        protected string _hWorkNumberID;
        protected string _name;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected long? _professionalAbilityID;
        protected string _professionalAbilityName;
        protected string _professionalAbilityImageUrl;
        protected string _personImageUrl;
        protected long? _bankID;
        protected string _bankAccount;
        protected string _BankAddress;
        protected double? _BonusPercent;
        protected long? _DoctorAccountType;

        #endregion

        #region Constructors

        public BDoctorAccount() { }

        public BDoctorAccount(long hospitalID, long weiXinUserID, string hospitalCode, string hospitalName, long hospitalDeptID, string hospitalDeptName, string hWorkNumberID, string name, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long professionalAbilityID, string professionalAbilityName, string professionalAbilityImageUrl, string personImageUrl, long bankID, string bankAccount, string bankTransFormCode)
        {
            this._hospitalID = hospitalID;
            this._weiXinUserID = weiXinUserID;
            this._hospitalCode = hospitalCode;
            this._hospitalName = hospitalName;
            this._hospitalDeptID = hospitalDeptID;
            this._hospitalDeptName = hospitalDeptName;
            this._hWorkNumberID = hWorkNumberID;
            this._name = name;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._professionalAbilityID = professionalAbilityID;
            this._professionalAbilityName = professionalAbilityName;
            this._professionalAbilityImageUrl = professionalAbilityImageUrl;
            this._personImageUrl = personImageUrl;
            this._bankID = bankID;
            this._bankAccount = bankAccount;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AreaID
        {
            get { return _AreaID; }
            set { _AreaID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院ID", ShortCode = "HospitalID", Desc = "医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HospitalID
        {
            get { return _hospitalID; }
            set { _hospitalID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院编码", ShortCode = "HospitalCode", Desc = "医院编码", ContextType = SysDic.All, Length = 8)]
        public virtual string HospitalCode
        {
            get { return _hospitalCode; }
            set { _hospitalCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "医院名称", ShortCode = "HospitalName", Desc = "医院名称", ContextType = SysDic.All, Length = 100)]
        public virtual string HospitalName
        {
            get { return _hospitalName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for HospitalName", value, value.ToString());
                _hospitalName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院科室ID", ShortCode = "HospitalDeptID", Desc = "医院科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? HospitalDeptID
        {
            get { return _hospitalDeptID; }
            set { _hospitalDeptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "科室名称", ShortCode = "HospitalDeptName", Desc = "科室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string HospitalDeptName
        {
            get { return _hospitalDeptName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for HospitalDeptName", value, value.ToString());
                _hospitalDeptName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医院工号", ShortCode = "HWorkNumberID", Desc = "医院工号", ContextType = SysDic.All, Length = 20)]
        public virtual string HWorkNumberID
        {
            get { return _hWorkNumberID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HWorkNumberID", value, value.ToString());
                _hWorkNumberID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                _name = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "专业级别ID", ShortCode = "ProfessionalAbilityID", Desc = "专业级别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProfessionalAbilityID
        {
            get { return _professionalAbilityID; }
            set { _professionalAbilityID = value; }
        }

        [DataMember]
        [DataDesc(CName = "职称专业级别", ShortCode = "ProfessionalAbilityName", Desc = "职称专业级别", ContextType = SysDic.All, Length = 20)]
        public virtual string ProfessionalAbilityName
        {
            get { return _professionalAbilityName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ProfessionalAbilityName", value, value.ToString());
                _professionalAbilityName = value;
            }
        }


        [DataMember]
        [DataDesc(CName = "职业证书", ShortCode = "ProfessionalAbilityImageUrl", Desc = "职业证书", ContextType = SysDic.All, Length = 500)]
        public virtual string ProfessionalAbilityImageUrl
        {
            get { return _professionalAbilityImageUrl; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ProfessionalAbilityImageUrl", value, value.ToString());
                _professionalAbilityImageUrl = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "个人照片", ShortCode = "PersonImageUrl", Desc = "个人照片", ContextType = SysDic.All, Length = 500)]
        public virtual string PersonImageUrl
        {
            get { return _personImageUrl; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for PersonImageUrl", value, value.ToString());
                _personImageUrl = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "银行种类", ShortCode = "ProfessionalAbilityID", Desc = "银行种类", ContextType = SysDic.All, Length = 8)]
        public virtual long? BankID
        {
            get { return _bankID; }
            set { _bankID = value; }
        }


        [DataMember]
        [DataDesc(CName = "银行帐号", ShortCode = "BankAccount", Desc = "银行帐号", ContextType = SysDic.All, Length = 50)]
        public virtual string BankAccount
        {
            get { return _bankAccount; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BankAccount", value, value.ToString());
                _bankAccount = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "开户行地址", ShortCode = "BankAddress", Desc = "开户行地址", ContextType = SysDic.All, Length = 50)]
        public virtual string BankAddress
        {
            get { return _BankAddress; }
            set
            {
                _BankAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "咨询费比率", ShortCode = "BonusPercent", Desc = "咨询费比率", ContextType = SysDic.All, Length = 50)]
        public virtual Double? BonusPercent
        {
            get { return _BonusPercent; }
            set
            {
                _BonusPercent = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医生账户类型", ShortCode = "DoctorAccountType", Desc = "医生账户类型", ContextType = SysDic.All, Length = 50)]
        public virtual long? DoctorAccountType
        {
            get { return _DoctorAccountType; }
            set
            {
                _DoctorAccountType = value;
            }
        }
        #endregion
    }
    #endregion
}