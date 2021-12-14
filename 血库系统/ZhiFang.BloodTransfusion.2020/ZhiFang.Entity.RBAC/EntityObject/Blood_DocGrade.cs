using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region BloodDocGrade

    /// <summary>
    /// BloodDocGrade object for NHibernate mapped table 'Blood_DocGrade'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "用血申请审核等级表", ClassCName = "BloodDocGrade", ShortCode = "BloodDocGrade", Desc = "用血申请审核等级表")]
    public class BloodDocGrade : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _sName;
        protected string _pinYinZiTou;
        protected string _shortCode;
        protected double? _bCount;
        protected double? _lowLimit;
        protected double? _upperLimit;
        protected bool _isUse;
        protected int _dispOrder;

        #endregion

        #region Constructors

        public BloodDocGrade() { }

        public BloodDocGrade(long labID, string gradeName, string sName, string pinYinZiTou, string shortCode, double? bCount, double? lowLimit, double? upperLimit, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cName = gradeName;
            this._sName = sName;
            this._pinYinZiTou = pinYinZiTou;
            this._shortCode = shortCode;
            this._bCount = bCount;
            this._lowLimit = lowLimit;
            this._upperLimit = upperLimit;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "用血等级名称", ShortCode = "CName", Desc = "用血等级名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 80)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 80)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "拼音字头", ShortCode = "PinYinZiTou", Desc = "拼音字头", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用血量", ShortCode = "BCount", Desc = "用血量", ContextType = SysDic.All, Length = 9)]
        public virtual double? BCount
        {
            get { return _bCount; }
            set { _bCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用血量下限值", ShortCode = "LowLimit", Desc = "用血量下限值", ContextType = SysDic.All, Length = 8)]
        public virtual double? LowLimit
        {
            get { return _lowLimit; }
            set { _lowLimit = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用血量上限值", ShortCode = "UpperLimit", Desc = "用血量上限值", ContextType = SysDic.All, Length = 8)]
        public virtual double? UpperLimit
        {
            get { return _upperLimit; }
            set { _upperLimit = value; }
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

        #endregion
    }
    #endregion
}