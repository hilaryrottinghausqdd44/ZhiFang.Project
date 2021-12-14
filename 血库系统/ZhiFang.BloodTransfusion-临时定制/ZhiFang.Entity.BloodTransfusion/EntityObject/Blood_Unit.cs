using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodUnit

    /// <summary>
    /// BloodUnit object for NHibernate mapped table 'Blood_Unit'.
    /// BloodUnitNo×÷Ö÷¼ü
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodUnit", ShortCode = "BloodUnit", Desc = "")]
    public class BloodUnit : BaseEntityServiceByInt
    {
        #region Member Variables

        //protected int? _bloodUnitNo;
        protected string _cName;
        protected string _hisDispCode;
        protected string _shortCode;
        protected string _dispOrder;
        protected bool _visible;
        protected string _eName;
        protected double _bloodScale;
        protected string _bloodScaleUnit;

        protected string _sName;
        protected string _pinYinZiTou;

        #endregion

        #region Constructors

        public BloodUnit() { }

        public BloodUnit(string cName, string hisDispCode, string shortCode, string dispOrder, bool visible, string eName, double bloodScale, string bloodScaleUnit, long labID, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._cName = cName;
            this._hisDispCode = hisDispCode;
            this._shortCode = shortCode;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._eName = eName;
            this._bloodScale = bloodScale;
            this._bloodScaleUnit = bloodScaleUnit;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Ö÷¼üID", ShortCode = "Id", Desc = "Ö÷¼üID", ContextType = SysDic.Number)]
        public override int Id
        {
            get
            {
                if (_id < 0)
                    _id = ZhiFang.Common.Public.GUIDHelp.GetGUIDInt();
                return _id;
            }
            set { _id = value; }
        }
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
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDispCode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string HisDispCode
        {
            get { return _hisDispCode; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for HisDispCode", value, value.ToString());
                _hisDispCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 10)]
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string DispOrder
        {
            get { return _dispOrder; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for DispOrder", value, value.ToString());
                _dispOrder = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BloodScale", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double BloodScale
        {
            get { return _bloodScale; }
            set { _bloodScale = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodScaleUnit", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BloodScaleUnit
        {
            get { return _bloodScaleUnit; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodScaleUnit", value, value.ToString());
                _bloodScaleUnit = value;
            }
        }


        #endregion
    }
    #endregion
}