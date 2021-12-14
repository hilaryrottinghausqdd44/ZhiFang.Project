using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    #region SickType

    /// <summary>
    /// SickType object for NHibernate mapped table 'SickType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SickType", ShortCode = "SickType", Desc = "")]
    public class SickType : BaseEntityServiceByInt
    {
        #region Member Variables

        protected string _cName;
        protected string _shortCode;
        protected int _dispOrder;
        protected string _hisOrderCode;
        protected string _contractCode;


        #endregion

        #region Constructors

        public SickType() { }

        public SickType(string cName, string shortCode, int dispOrder, string hisOrderCode, string contractCode, long labID, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._cName = cName;
            this._shortCode = shortCode;
            this._dispOrder = dispOrder;
            this._hisOrderCode = hisOrderCode;
            this._contractCode = contractCode;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisOrderCode", Desc = "", ContextType = SysDic.All, Length = 21)]
        public virtual string HisOrderCode
        {
            get { return _hisOrderCode; }
            set
            {
                if (value != null && value.Length > 21)
                    throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
                _hisOrderCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ContractCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ContractCode
        {
            get { return _contractCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ContractCode", value, value.ToString());
                _contractCode = value;
            }
        }


        #endregion
    }
    #endregion
}