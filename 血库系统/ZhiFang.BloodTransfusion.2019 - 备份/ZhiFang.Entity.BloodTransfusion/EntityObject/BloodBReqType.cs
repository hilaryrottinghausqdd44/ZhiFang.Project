using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBReqType

    /// <summary>
    /// BloodBReqType object for NHibernate mapped table 'Blood_BReqType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBReqType", ShortCode = "BloodBReqType", Desc = "")]
    public class BloodBReqType : BaseEntityServiceByInt
    {
        #region Member Variables

        protected int? _bReqTypeID;
        protected string _cName;
        protected string _shortcode;
        protected int _dispOrder;
        protected bool _visible;

        protected string _sName;
        protected string _pinYinZiTou;
        #endregion

        #region Constructors

        public BloodBReqType() { }

        public BloodBReqType(string cname, string shortcode, int dispOrder)
        {
            this._cName = cname;
            this._shortcode = shortcode;
            this._dispOrder = dispOrder;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BReqTypeID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BReqTypeID
        {
            get
            {
                if (!_bReqTypeID.HasValue || _bReqTypeID < 0)
                    _bReqTypeID = ZhiFang.Common.Public.GUIDHelp.GetGUIDInt();
                return _bReqTypeID;
            }
            set { _bReqTypeID = value; }
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