using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsQtyBalanceDoc

    /// <summary>
    /// ReaBmsQtyBalanceDoc object for NHibernate mapped table 'Rea_BmsQtyBalanceDoc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsQtyBalanceDoc", ShortCode = "ReaBmsQtyBalanceDoc", Desc = "")]
    public class ReaBmsQtyBalanceDoc : BaseEntity
    {
        #region Member Variables

        protected long _preQtyBalanceDocID;
        protected string _preQtyBalanceDocNo;
        protected DateTime? _preBalanceDateTime;

        protected string _qtyBalanceDocNo;
        protected long _operID;
        protected string _operName;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;


        #endregion

        #region Constructors

        public ReaBmsQtyBalanceDoc() { }

        public ReaBmsQtyBalanceDoc(long labID, string qtyBalanceDocNo, long operID, string operName, DateTime operDate, int printTimes, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._qtyBalanceDocNo = qtyBalanceDocNo;
            this._operID = operID;
            this._operName = operName;
            this._operDate = operDate;
            this._printTimes = printTimes;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreQtyBalanceDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long PreQtyBalanceDocID
        {
            get { return _preQtyBalanceDocID; }
            set { _preQtyBalanceDocID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PreQtyBalanceDocNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PreQtyBalanceDocNo
        {
            get { return _preQtyBalanceDocNo; }
            set
            {
                _preQtyBalanceDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PreBalanceDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PreBalanceDateTime
        {
            get { return _preBalanceDateTime; }
            set { _preBalanceDateTime = value; }
        }


        [DataMember]
        [DataDesc(CName = "", ShortCode = "QtyBalanceDocNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string QtyBalanceDocNo
        {
            get { return _qtyBalanceDocNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for QtyBalanceDocNo", value, value.ToString());
                _qtyBalanceDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long OperID
        {
            get { return _operID; }
            set { _operID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string OperName
        {
            get { return _operName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for OperName", value, value.ToString());
                _operName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperDate
        {
            get { return _operDate; }
            set { _operDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintTimes", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
                _zX3 = value;
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
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CreaterName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
        {
            get { return _createrName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
                _createrName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}