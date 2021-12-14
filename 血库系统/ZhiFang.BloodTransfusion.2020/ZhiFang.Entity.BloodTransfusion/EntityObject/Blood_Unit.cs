using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodUnit

    /// <summary>
    /// BloodUnit object for NHibernate mapped table 'Blood_Unit'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "血制品单位", ClassCName = "BloodUnit", ShortCode = "BloodUnit", Desc = "血制品单位")]
    public class BloodUnit : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _pinYinZiTou;
        protected string _shortCode;
        protected string _hisOrderCode;
        protected bool _isUse;
        protected int _dispOrder;
        #endregion

        #region Constructors

        public BloodUnit() { }

        public BloodUnit(long labID, string bloodUnitName, string eName, string sName, string pinYinZiTou, string shortCode, string hisOrderCode, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cName = bloodUnitName;
            this._eName = eName;
            this._sName = sName;
            this._pinYinZiTou = pinYinZiTou;
            this._shortCode = shortCode;
            this._hisOrderCode = hisOrderCode;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number)]
        public override long Id
        {
            get
            {
                if (_id < 0)
                    _id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                return _id;
            }
            set { _id = value; }
        }
        [DataMember]
        [DataDesc(CName = "单位名称", ShortCode = "CName", Desc = "单位名称", ContextType = SysDic.All, Length = 20)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 10)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "His对照码", ShortCode = "HisOrderCode", Desc = "His对照码", ContextType = SysDic.All, Length = 20)]
        public virtual string HisOrderCode
        {
            get { return _hisOrderCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
                _hisOrderCode = value;
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 10)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set
            {
                _dispOrder = value;
            }
        }

        #endregion
    }
    #endregion
}