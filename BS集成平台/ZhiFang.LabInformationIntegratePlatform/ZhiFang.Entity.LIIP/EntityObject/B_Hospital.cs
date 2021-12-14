using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
    #region BHospital

    /// <summary>
    /// BHospital object for NHibernate mapped table 'B_Hospital'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "医院字典", ClassCName = "BHospital", ShortCode = "BHospital", Desc = "医院字典")]
    public class BHospital : BaseEntityService//BaseEntity
    {
        #region Member Variables

        protected long? _areaID;
        protected string _name;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected string _hospitalCode;
        protected long? _levelID;
        protected long? _hTypeID;
        protected long? _iconsID;
        protected long? _provinceID;
        protected long? _cityID;
        protected long? _CountyID;
        protected string _levelName;
        protected string _hTypeName;
        protected string _postion;
        protected string _provinceName;
        protected string _cityName;
        protected string _CountyName;
        protected bool _isBloodSamplingPoint;
        protected long? _dealerID;
        protected string _linkMan;
        protected string _linkManPosition;
        protected string _phoneNum1;
        protected string _phoneNum2;
        protected string _address;
        protected string _eMAIL;
        protected string _mAILNO;
        protected string _faxNo;
        protected string _autoFax;
        private bool _IsReceiveSamplePoint;
        protected long? _labIdentityTypeID;
        protected long? _branchId;
        protected string _tINumber;
        protected long _businessAnalysisDateType;
        protected string _businessAnalysisDate;
        protected int _billNumber;
        protected bool _isCooperation;
        protected decimal? _personFixedCharges;
        private string _AreaCode;
        private string _AreaName;
        private string _EditerName;
        private long? _EditerId;



        #endregion

        #region Constructors

        public BHospital() { }

        public BHospital(long labID, long areaID, string name, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, DateTime dataUpdateTime, string hospitalCode, long levelID, long hTypeID, long iconsID, long provinceID, long cityID, string levelName, string hTypeName, string postion, string provinceName, string cityName, bool isBloodSamplingPoint, long dealerID, string linkMan, string linkManPosition, string phoneNum1, string phoneNum2, string address, string eMAIL, string mAILNO, string faxNo, string autoFax)
        {
            this._labID = labID;
            this._areaID = areaID;
            this._name = name;
            this._eName = eName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._dataUpdateTime = dataUpdateTime;
            this._hospitalCode = hospitalCode;
            this._levelID = levelID;
            this._hTypeID = hTypeID;
            this._iconsID = iconsID;
            this._provinceID = provinceID;
            this._cityID = cityID;
            this._levelName = levelName;
            this._hTypeName = hTypeName;
            this._postion = postion;
            this._provinceName = provinceName;
            this._cityName = cityName;
            this._isBloodSamplingPoint = isBloodSamplingPoint;
            this._dealerID = dealerID;
            this._linkMan = linkMan;
            this._linkManPosition = linkManPosition;
            this._phoneNum1 = phoneNum1;
            this._phoneNum2 = phoneNum2;
            this._address = address;
            this._eMAIL = eMAIL;
            this._mAILNO = mAILNO;
            this._faxNo = faxNo;
            this._autoFax = autoFax;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AreaID
        {
            get { return _areaID; }
            set { _areaID = value; }
        }

        [DataMember]
        [DataDesc(CName = "区域编码", ShortCode = "AreaCode", Desc = "区域编码", ContextType = SysDic.All, Length = 8)]
        public virtual string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "区域名称", ShortCode = "AreaName", Desc = "区域名称", ContextType = SysDic.All, Length = 8)]
        public virtual string AreaName
        {
            get { return _AreaName; }
            set { _AreaName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 30)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
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
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 300)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
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
        [DataDesc(CName = "", ShortCode = "HospitalCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalCode
        {
            get { return _hospitalCode; }
            set
            {              
                _hospitalCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院级别ID", ShortCode = "LevelID", Desc = "医院级别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? LevelID
        {
            get { return _levelID; }
            set { _levelID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院类别ID", ShortCode = "HTypeID", Desc = "医院类别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? HTypeID
        {
            get { return _hTypeID; }
            set { _hTypeID = value; }
        }

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
        [DataDesc(CName = "区县ID", ShortCode = "CountyID", Desc = "区县ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CountyID
        {
            get { return _CountyID; }
            set { _CountyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "医院级别名称", ShortCode = "LevelName", Desc = "医院级别名称", ContextType = SysDic.All, Length = 20)]
        public virtual string LevelName
        {
            get { return _levelName; }
            set
            {
                _levelName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医院类别名称", ShortCode = "HTypeName", Desc = "医院类别名称", ContextType = SysDic.All, Length = 20)]
        public virtual string HTypeName
        {
            get { return _hTypeName; }
            set
            {
                _hTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "坐标位置", ShortCode = "Postion", Desc = "坐标位置", ContextType = SysDic.All, Length = 20)]
        public virtual string Postion
        {
            get { return _postion; }
            set
            {
                _postion = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "省份名称", ShortCode = "ProvinceName", Desc = "省份名称", ContextType = SysDic.All, Length = 20)]
        public virtual string ProvinceName
        {
            get { return _provinceName; }
            set
            {
                _provinceName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "城市名称", ShortCode = "CityName", Desc = "城市名称", ContextType = SysDic.All, Length = 20)]
        public virtual string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "区县名称", ShortCode = "CountyName", Desc = "区县名称", ContextType = SysDic.All, Length = 20)]
        public virtual string CountyName
        {
            get { return _CountyName; }
            set
            {
                _CountyName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否采血点", ShortCode = "IsBloodSamplingPoint", Desc = "是否采血点", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsBloodSamplingPoint
        {
            get { return _isBloodSamplingPoint; }
            set { _isBloodSamplingPoint = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DealerID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DealerID
        {
            get { return _dealerID; }
            set { _dealerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkMan", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LinkMan
        {
            get { return _linkMan; }
            set
            {
                _linkMan = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkManPosition", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LinkManPosition
        {
            get { return _linkManPosition; }
            set
            {
                _linkManPosition = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhoneNum1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PhoneNum1
        {
            get { return _phoneNum1; }
            set
            {
                _phoneNum1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhoneNum2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PhoneNum2
        {
            get { return _phoneNum2; }
            set
            {
                _phoneNum2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Address", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Address
        {
            get { return _address; }
            set
            {
                _address = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EMAIL", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EMAIL
        {
            get { return _eMAIL; }
            set
            {
                _eMAIL = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MAILNO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string MAILNO
        {
            get { return _mAILNO; }
            set
            {
                _mAILNO = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FaxNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string FaxNo
        {
            get { return _faxNo; }
            set
            {
                _faxNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AutoFax", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AutoFax
        {
            get { return _autoFax; }
            set
            {
                _autoFax = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否接收样本点", ShortCode = "IsReceiveSamplePoint", Desc = "是否接收样本点", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReceiveSamplePoint
        {
            get { return _IsReceiveSamplePoint; }
            set { _IsReceiveSamplePoint = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabIdentityTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? LabIdentityTypeID
        {
            get { return _labIdentityTypeID; }
            set { _labIdentityTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BranchId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TINumber", Desc = "", ContextType = SysDic.All, Length = 80)]
        public virtual string TINumber
        {
            get { return _tINumber; }
            set
            {
                if (value != null && value.Length > 80)
                    throw new ArgumentOutOfRangeException("Invalid value for TINumber", value, value.ToString());
                _tINumber = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BusinessAnalysisDateType", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long BusinessAnalysisDateType
        {
            get { return _businessAnalysisDateType; }
            set { _businessAnalysisDateType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BusinessAnalysisDate", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BusinessAnalysisDate
        {
            get { return _businessAnalysisDate; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BusinessAnalysisDate", value, value.ToString());
                _businessAnalysisDate = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BillNumber", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BillNumber
        {
            get { return _billNumber; }
            set { _billNumber = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCooperation", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsCooperation
        {
            get { return _isCooperation; }
            set { _isCooperation = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "人员固定支出", ShortCode = "PersonFixedCharges", Desc = "", ContextType = SysDic.All, Length = 9)]
        public virtual decimal? PersonFixedCharges
        {
            get { return _personFixedCharges; }
            set { _personFixedCharges = value; }
        }

        [DataMember]
        [DataDesc(CName = "最后一次修改人姓名", ShortCode = "EditerName", Desc = "最后一次修改人姓名", ContextType = SysDic.All, Length = 9)]
        public virtual string EditerName
        {
            get { return _EditerName; }
            set { _EditerName = value; }
        }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "最后一次修改人ID", ShortCode = "EditerId", Desc = "最后一次修改人ID", ContextType = SysDic.All, Length = 9)]
        public virtual long? EditerId 
        {
            get { return _EditerId; }
            set { _EditerId = value; }
        }
        #endregion
    }
    #endregion
}