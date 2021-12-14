using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BDictType

    /// <summary>
    /// BDictType object for NHibernate mapped table 'B_DictType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "字典类型", ClassCName = "BDictType", ShortCode = "BDictType", Desc = "字典类型")]
    public class BDictType : BaseEntity
    {
        #region Member Variables

        protected string _dictTypeCode;
        protected string _cName;
        protected string _sName;
        protected string _shortCode;
        protected string _pinYinZiTou;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected bool _isUse;
        protected int _dispOrder;
        protected string _memo;

        #endregion

        #region Constructors

        public BDictType() { }

        public BDictType(long labID, string dictTypeCode, string cName, string sName, string shortCode, string pinYinZiTou, string useCode, string standCode, string deveCode, bool isUse, int dispOrder, string memo, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._dictTypeCode = dictTypeCode;
            this._cName = cName;
            this._sName = sName;
            this._shortCode = shortCode;
            this._pinYinZiTou = pinYinZiTou;
            this._useCode = useCode;
            this._standCode = standCode;
            this._deveCode = deveCode;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "类型编码", ShortCode = "DictTypeCode", Desc = "类型编码", ContextType = SysDic.All, Length = 100)]
        public virtual string DictTypeCode
        {
            get { return _dictTypeCode; }
            set { _dictTypeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "类型名称", ShortCode = "CName", Desc = "类型名称", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 40)
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
        [DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 40)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
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
        [DataDesc(CName = "业务编码", ShortCode = "UseCode", Desc = "业务编码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
                _useCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
        {
            get { return _standCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
                _standCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "开发商代码", ShortCode = "DeveCode", Desc = "开发商代码", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        #endregion
    }
    #endregion
}