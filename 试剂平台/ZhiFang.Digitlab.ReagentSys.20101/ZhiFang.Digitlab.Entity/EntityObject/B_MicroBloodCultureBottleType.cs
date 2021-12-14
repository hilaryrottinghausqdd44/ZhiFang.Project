using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BMicroBloodCultureBottleType

    /// <summary>
    /// BMicroBloodCultureBottleType object for NHibernate mapped table 'B_MicroBloodCultureBottleType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BMicroBloodCultureBottleType", ShortCode = "BMicroBloodCultureBottleType", Desc = "")]
    public class BMicroBloodCultureBottleType : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _logoColor;
        protected byte[] _logoPicture;
        protected int _bottleType;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected BCultureMedium _bCultureMedium;
        protected IList<BMicroBCBottleTypeOfItem> _bMicroBCBottleTypeOfItemList;
        protected IList<BMicroBCBottleTypeOfManufacturer> _bMicroBCBottleTypeOfManufacturerList;
        protected IList<MEMicroBCBottleManageInfo> _mEMicroBCBottleManageInfoList;

        #endregion

        #region Constructors

        public BMicroBloodCultureBottleType() { }

        public BMicroBloodCultureBottleType(long labID, string cName, string eName, string sName, string shortcode, string pinYinZiTou, int logoColor, byte[] logoPicture, int bottleType, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BCultureMedium bCultureMedium)
        {
            this._labID = labID;
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._logoColor = logoColor;
            this._logoPicture = logoPicture;
            this._bottleType = bottleType;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bCultureMedium = bCultureMedium;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 50)
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
        [DataDesc(CName = "", ShortCode = "LogoColor", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int LogoColor
        {
            get { return _logoColor; }
            set { _logoColor = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LogoPicture", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] LogoPicture
        {
            get { return _logoPicture; }
            set { _logoPicture = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BottleType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BottleType
        {
            get { return _bottleType; }
            set { _bottleType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "培养基字典表", ShortCode = "BCultureMedium", Desc = "培养基字典表")]
        public virtual BCultureMedium BCultureMedium
        {
            get { return _bCultureMedium; }
            set { _bCultureMedium = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BMicroBCBottleTypeOfItemList", Desc = "")]
        public virtual IList<BMicroBCBottleTypeOfItem> BMicroBCBottleTypeOfItemList
        {
            get
            {
                if (_bMicroBCBottleTypeOfItemList == null)
                {
                    _bMicroBCBottleTypeOfItemList = new List<BMicroBCBottleTypeOfItem>();
                }
                return _bMicroBCBottleTypeOfItemList;
            }
            set { _bMicroBCBottleTypeOfItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BMicroBCBottleTypeOfManufacturerList", Desc = "")]
        public virtual IList<BMicroBCBottleTypeOfManufacturer> BMicroBCBottleTypeOfManufacturerList
        {
            get
            {
                if (_bMicroBCBottleTypeOfManufacturerList == null)
                {
                    _bMicroBCBottleTypeOfManufacturerList = new List<BMicroBCBottleTypeOfManufacturer>();
                }
                return _bMicroBCBottleTypeOfManufacturerList;
            }
            set { _bMicroBCBottleTypeOfManufacturerList = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MEMicroBCBottleManageInfoList", Desc = "")]
        public virtual IList<MEMicroBCBottleManageInfo> MEMicroBCBottleManageInfoList
        {
            get
            {
                if (_mEMicroBCBottleManageInfoList == null)
                {
                    _mEMicroBCBottleManageInfoList = new List<MEMicroBCBottleManageInfo>();
                }
                return _mEMicroBCBottleManageInfoList;
            }
            set { _mEMicroBCBottleManageInfoList = value; }
        }


        #endregion
    }
    #endregion
}