using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BMicroTestItemUseValue

    /// <summary>
    /// BMicroTestItemUseValue object for NHibernate mapped table 'B_MicroTestItemUseValue'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "微生物检验记录项常用值", ClassCName = "BMicroTestItemUseValue", ShortCode = "BMicroTestItemUseValue", Desc = "微生物检验记录项常用值")]
    public class BMicroTestItemUseValue : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _alarmLevel;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected string _deveCode;
        protected bool _isDefault;
        protected BMicroTestItemInfo _bMicroTestItemInfo;
        protected IList<MEMicroThreeReportDetail> _mEMicroThreeReportDetailList;

        #endregion

        #region Constructors

        public BMicroTestItemUseValue() { }

        public BMicroTestItemUseValue(long labID, string cName, string shortcode, string pinYinZiTou, int alarmLevel, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string deveCode, bool isDefault, BMicroTestItemInfo bMicroTestItemInfo)
        {
            this._labID = labID;
            this._cName = cName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._alarmLevel = alarmLevel;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._deveCode = deveCode;
            this._isDefault = isDefault;
            this._bMicroTestItemInfo = bMicroTestItemInfo;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "结果值", ShortCode = "CName", Desc = "结果值", ContextType = SysDic.All, Length = 300)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
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
        [DataDesc(CName = "结果警示级别", ShortCode = "AlarmLevel", Desc = "结果警示级别", ContextType = SysDic.All, Length = 4)]
        public virtual int AlarmLevel
        {
            get { return _alarmLevel; }
            set { _alarmLevel = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
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
        [DataDesc(CName = "", ShortCode = "DeveCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
        {
            get { return _deveCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
                _deveCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否默认结果", ShortCode = "IsDefault", Desc = "是否默认结果", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物检验记录项字典表", ShortCode = "BMicroTestItemInfo", Desc = "微生物检验记录项字典表")]
        public virtual BMicroTestItemInfo BMicroTestItemInfo
        {
            get { return _bMicroTestItemInfo; }
            set { _bMicroTestItemInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "微生物三级报告细表", ShortCode = "MEMicroThreeReportDetailList", Desc = "微生物三级报告细表")]
        public virtual IList<MEMicroThreeReportDetail> MEMicroThreeReportDetailList
        {
            get
            {
                if (_mEMicroThreeReportDetailList == null)
                {
                    _mEMicroThreeReportDetailList = new List<MEMicroThreeReportDetail>();
                }
                return _mEMicroThreeReportDetailList;
            }
            set { _mEMicroThreeReportDetailList = value; }
        }


        #endregion
    }
    #endregion
}