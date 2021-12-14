using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodClass

    /// <summary>
    /// BloodClass object for NHibernate mapped table 'Blood_Class'.
    /// BCNoÎªÖ÷¼ü,Id²»ÓÃ
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodClass", ShortCode = "BloodClass", Desc = "")]
    public class BloodClass : BaseEntityServiceByString
    {
        #region Member Variables

        //protected string _bCNo;
        protected string _cName;
        protected string _shortCode;
        protected string _bCCode;
        protected string _iswarn;
        protected string _islargeUse;
        protected int _dispOrder;
        protected bool _visible;

        protected string _sName;
        protected string _pinYinZiTou;
        #endregion

        #region Constructors

        public BloodClass() { }

        public BloodClass(string bCName, string shortCode, string bCCode, string iswarn, string islargeUse, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible)
        {
            this._cName = bCName;
            this._shortCode = shortCode;
            this._bCCode = bCCode;
            this._iswarn = iswarn;
            this._islargeUse = islargeUse;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "ºº×ÖÆ´Òô×ÖÍ·", ShortCode = "PinYinZiTou", Desc = "ºº×ÖÆ´Òô×ÖÍ·", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "¼ò³Æ", ShortCode = "SName", Desc = "¼ò³Æ", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "BCCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BCCode
        {
            get { return _bCCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BCCode", value, value.ToString());
                _bCCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Iswarn", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Iswarn
        {
            get { return _iswarn; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Iswarn", value, value.ToString());
                _iswarn = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IslargeUse", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string IslargeUse
        {
            get { return _islargeUse; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for IslargeUse", value, value.ToString());
                _islargeUse = value;
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
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }


        #endregion
    }
    #endregion
}