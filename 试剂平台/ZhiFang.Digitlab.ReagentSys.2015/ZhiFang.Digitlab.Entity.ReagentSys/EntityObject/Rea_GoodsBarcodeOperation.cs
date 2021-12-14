using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region ReaGoodsBarcodeOperation

    /// <summary>
    /// ReaGoodsBarcodeOperation object for NHibernate mapped table 'Rea_GoodsBarcodeOperation'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaGoodsBarcodeOperation", ShortCode = "ReaGoodsBarcodeOperation", Desc = "")]
    public class ReaGoodsBarcodeOperation : BaseEntity
    {
        #region Member Variables

        protected string _bDocNo;
        protected long? _bDocID;
        protected long? _bDtlID;
        protected long? _operTypeID;
        protected string _operTypeName;
        protected string _sysPackSerial;
        protected string _otherPackSerial;
        protected string _usePackSerial;
        protected string _lotNo;
        protected string _memo;
        protected int _dispOrder;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;
        protected long? _reaCompanyID;
        protected string _companyName;
        protected long? _goodsID;
        protected string _goodsCName;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected bool _isOutFlag;


        #endregion

        #region Constructors

        public ReaGoodsBarcodeOperation() { }

        public ReaGoodsBarcodeOperation(long labID, string bDocNo, long bDocID, long bDtlID, long operTypeID, string operTypeName, string sysPackSerial, string otherPackSerial, string usePackSerial, string lotNo, string memo, int dispOrder, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long reaCompanyID, string companyName, long goodsID, string goodsCName, long goodsUnitID, string goodsUnit, bool isOutFlag)
        {
            this._labID = labID;
            this._bDocNo = bDocNo;
            this._bDocID = bDocID;
            this._bDtlID = bDtlID;
            this._operTypeID = operTypeID;
            this._operTypeName = operTypeName;
            this._sysPackSerial = sysPackSerial;
            this._otherPackSerial = otherPackSerial;
            this._usePackSerial = usePackSerial;
            this._lotNo = lotNo;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._reaCompanyID = reaCompanyID;
            this._companyName = companyName;
            this._goodsID = goodsID;
            this._goodsCName = goodsCName;
            this._goodsUnitID = goodsUnitID;
            this._goodsUnit = goodsUnit;
            this._isOutFlag = isOutFlag;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "BDocNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string BDocNo
        {
            get { return _bDocNo; }
            set
            {
                _bDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? BDocID
        {
            get { return _bDocID; }
            set { _bDocID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BDtlID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? BDtlID
        {
            get { return _bDtlID; }
            set { _bDtlID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperTypeID
        {
            get { return _operTypeID; }
            set { _operTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OperTypeName
        {
            get { return _operTypeName; }
            set
            {
                _operTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SysPackSerial", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string SysPackSerial
        {
            get { return _sysPackSerial; }
            set
            {
                _sysPackSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OtherPackSerial", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string OtherPackSerial
        {
            get { return _otherPackSerial; }
            set
            {
                _otherPackSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UsePackSerial", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string UsePackSerial
        {
            get { return _usePackSerial; }
            set
            {
                _usePackSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LotNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set
            {
                _lotNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
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
                _createrName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReaCompanyID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompanyID
        {
            get { return _reaCompanyID; }
            set { _reaCompanyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompanyName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsCName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsCName
        {
            get { return _goodsCName; }
            set
            {
                _goodsCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsUnitID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsUnitID
        {
            get { return _goodsUnitID; }
            set { _goodsUnitID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsUnit", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsUnit
        {
            get { return _goodsUnit; }
            set
            {
                _goodsUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsOutFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsOutFlag
        {
            get { return _isOutFlag; }
            set { _isOutFlag = value; }
        }


        #endregion
    }
    #endregion
}