using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodUseType

    /// <summary>
    /// BloodUseType object for NHibernate mapped table 'Blood_UseType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodUseType", ShortCode = "BloodUseType", Desc = "")]
    public class BloodUseType : BaseEntityServiceByString
    {
        #region Member Variables
       // protected string _usetypeID;
        protected string _cName;
        protected double _beforTime;
        protected string _beforUnit;
        protected int _dispOrder;
        protected string _shortCode;
        protected bool _visible;

        protected string _sName;
        protected string _pinYinZiTou;
        #endregion

        #region Constructors

        public BloodUseType() { }

        public BloodUseType(string usetype, double beforTime, string beforUnit, int dispOrder, string shortCode, long labID, DateTime dataAddTime, byte[] dataTimeStamp, bool visible)
        {
            this._cName = usetype;
            this._beforTime = beforTime;
            this._beforUnit = beforUnit;
            this._dispOrder = dispOrder;
            this._shortCode = shortCode;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
        }

        #endregion

        #region Public Properties
        //[DataMember]
        //[DataDesc(CName = "LUFID", ShortCode = "LUFID", Desc = "CSÖ÷¼ü", ContextType = SysDic.All, Length = 20)]
        //public virtual string UsetypeID
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(_usetypeID))
        //            _usetypeID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString();
        //        return _usetypeID;
        //    }
        //    set
        //    {
        //        if (value != null && value.Length > 20)
        //            throw new ArgumentOutOfRangeException("Invalid value for LUFID", value, value.ToString());
        //        _usetypeID = value;
        //    }
        //}
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
        [DataDesc(CName = "CName", ShortCode = "CName", Desc = "CName", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BeforTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double BeforTime
        {
            get { return _beforTime; }
            set { _beforTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BeforUnit", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BeforUnit
        {
            get { return _beforUnit; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BeforUnit", value, value.ToString());
                _beforUnit = value;
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