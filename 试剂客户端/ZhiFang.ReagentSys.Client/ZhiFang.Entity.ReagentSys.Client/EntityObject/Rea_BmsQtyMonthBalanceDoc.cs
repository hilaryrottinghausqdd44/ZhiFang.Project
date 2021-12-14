using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsQtyMonthBalanceDoc

    /// <summary>
    /// ReaBmsQtyMonthBalanceDoc object for NHibernate mapped table 'Rea_BmsQtyMonthBalanceDoc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsQtyMonthBalanceDoc", ShortCode = "ReaBmsQtyMonthBalanceDoc", Desc = "")]
    public class ReaBmsQtyMonthBalanceDoc : BaseEntity
    {
        #region Member Variables

        protected long? _storageID;
        protected long? _placeID;
        protected string _storageName;
        protected string _placeName;
        protected string _qtyMonthBalanceDocNo;
        protected string _round;
        protected DateTime? _startDate;
        protected DateTime? _endDate;
        protected long? _typeID;
        protected string _typeName;
        protected long? _statisticalTypeID;
        protected string _statisticalTypeName;
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

        protected long? _qtyBalanceDocID;
        protected string _qtyBalanceDocNo;
        protected string _goodsClass;
        protected string _goodsClassType;
        #endregion

        #region Constructors

        public ReaBmsQtyMonthBalanceDoc() { }

        public ReaBmsQtyMonthBalanceDoc(long labID, long storageID, long placeID, string storageName, string placeName, string qtyMonthBalanceDocNo, string round, DateTime startDate, DateTime endDate, long typeID, string typeName, long statisticalTypeID, string statisticalTypeName, long operID, string operName, DateTime operDate, int printTimes, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._storageID = storageID;
            this._placeID = placeID;
            this._storageName = storageName;
            this._placeName = placeName;
            this._qtyMonthBalanceDocNo = qtyMonthBalanceDocNo;
            this._round = round;
            this._startDate = startDate;
            this._endDate = endDate;
            this._typeID = typeID;
            this._typeName = typeName;
            this._statisticalTypeID = statisticalTypeID;
            this._statisticalTypeName = statisticalTypeName;
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
        [DataDesc(CName = "所属机构名称(自定义)", ShortCode = "LabCName", Desc = "所属机构名称(自定义)", ContextType = SysDic.All, Length =60)]
        public virtual string LabCName { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存结转单ID", ShortCode = "QtyBalanceDocID", Desc = "库存结转单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? QtyBalanceDocID { get; set; }
        [DataMember]
        [DataDesc(CName = "库存结转单号", ShortCode = "QtyBalanceDocNo", Desc = "库存结转单号", ContextType = SysDic.All, Length = 20)]
        public virtual string QtyBalanceDocNo { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StorageID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PlaceID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? PlaceID
        {
            get { return _placeID; }
            set { _placeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StorageName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string StorageName
        {
            get { return _storageName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for StorageName", value, value.ToString());
                _storageName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PlaceName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string PlaceName
        {
            get { return _placeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for PlaceName", value, value.ToString());
                _placeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QtyMonthBalanceDocNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string QtyMonthBalanceDocNo
        {
            get { return _qtyMonthBalanceDocNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for QtyMonthBalanceDocNo", value, value.ToString());
                _qtyMonthBalanceDocNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Round", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Round
        {
            get { return _round; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Round", value, value.ToString());
                _round = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StartDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EndDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? TypeID
        {
            get { return _typeID; }
            set { _typeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeName
        {
            get { return _typeName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
                _typeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StatisticalTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? StatisticalTypeID
        {
            get { return _statisticalTypeID; }
            set { _statisticalTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatisticalTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StatisticalTypeName
        {
            get { return _statisticalTypeName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StatisticalTypeName", value, value.ToString());
                _statisticalTypeName = value;
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
        [DataMember]
        [DataDesc(CName = "一级分类", ShortCode = "GoodsClass", Desc = "一级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClass
        {
            get { return _goodsClass; }
            set { _goodsClass = value; }
        }

        [DataMember]
        [DataDesc(CName = "二级分类", ShortCode = "GoodsClassType", Desc = "二级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClassType
        {
            get { return _goodsClassType; }
            set { _goodsClassType = value; }
        }

        #endregion
    }
    #endregion
}