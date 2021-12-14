using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodLargeUseForm

    /// <summary>
    /// BloodLargeUseForm object for NHibernate mapped table 'Blood_LargeUseForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodLargeUseForm", ShortCode = "BloodLargeUseForm", Desc = "")]
    public class BloodLargeUseForm : BaseEntityServiceByString
    {
        #region Member Variables

        protected string _lUFID;
        protected DateTime? _lUFdate;
        protected string _logID;
        protected string _lUFMemo;
        protected int _dispOrder;
        protected bool _visible;
        protected string _breqformIDLast;


        #endregion

        #region Constructors

        public BloodLargeUseForm() { }

        public BloodLargeUseForm(string lUFID, DateTime lUFdate, string logID, string lUFMemo, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible, string breqformIDLast)
        {
            this._lUFID = lUFID;
            this._lUFdate = lUFdate;
            this._logID = logID;
            this._lUFMemo = lUFMemo;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
            this._breqformIDLast = breqformIDLast;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "LUFID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LUFID
        {
            get { return _lUFID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LUFID", value, value.ToString());
                _lUFID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LUFdate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LUFdate
        {
            get { return _lUFdate; }
            set { _lUFdate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LogID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LogID
        {
            get { return _logID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for LogID", value, value.ToString());
                _logID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LUFMemo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LUFMemo
        {
            get { return _lUFMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for LUFMemo", value, value.ToString());
                _lUFMemo = value;
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

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BreqformIDLast", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BreqformIDLast
        {
            get { return _breqformIDLast; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BreqformIDLast", value, value.ToString());
                _breqformIDLast = value;
            }
        }


        #endregion
    }
    #endregion
}