using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsCheckDtl

    /// <summary>
    /// ReaBmsCheckDtl object for NHibernate mapped table 'Rea_BmsCheckDtl'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsCheckDtl", ShortCode = "ReaBmsCheckDtl", Desc = "")]
    public class ReaBmsCheckDtl : BaseEntity
    {
        #region Member Variables

        protected long? _checkDocID;
        protected long? _reaCompanyID;
        protected string _companyName;
        protected long? _storageID;
        protected long? _placeID;
        protected string _storageName;
        protected string _placeName;
        protected string _reaServerCompCode;
        protected long? _goodsID;
        protected string _goodsName;
        protected string _lotNo;
        protected string _goodsUnit;
        protected string _unitMemo;
        protected double? _goodsQty;
        protected double? _checkQty;
        protected double? _price;
        protected double? _sumTotal;
        protected int _isException;
        protected int _isHandleException;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected DateTime _dataUpdateTime;
        protected string _sysLotSerial;
        protected string _lotSerial;
        protected string _goodsNo;
        protected long? _compGoodsLinkID;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected string _reaGoodsNo;
        protected string _prodGoodsNo;
        protected string _cenOrgGoodsNo;
        protected string _reaCompCode;
        protected string _lotQRCode;
        protected int _goodsSort;
        protected long? _goodsLotID;
        protected long _barCodeType;
        #endregion

        #region Constructors

        public ReaBmsCheckDtl() { }

        public ReaBmsCheckDtl(long labID, long checkDocID, long reaCompanyID, string companyName, long storageID, long placeID, string storageName, string placeName, string reaServerCompCode, long goodsID, string goodsName, string lotNo, string goodsUnit, string unitMemo, double goodsQty, double checkQty, double price, double sumTotal, int isException, int isHandleException, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string sysLotSerial, string lotSerial, string goodsNo, long compGoodsLinkID, DateTime prodDate, DateTime invalidDate, string reaGoodsNo, string cenOrgGoodsNo, string lotQRCode, string prodGoodsNo, string reaCompCode, int goodsSort, long goodsLotID, long barCodeType)
        {
            this._labID = labID;
            this._checkDocID = checkDocID;
            this._reaCompanyID = reaCompanyID;
            this._companyName = companyName;
            this._storageID = storageID;
            this._placeID = placeID;
            this._storageName = storageName;
            this._placeName = placeName;
            this._reaServerCompCode = reaServerCompCode;
            this._goodsID = goodsID;
            this._goodsName = goodsName;
            this._lotNo = lotNo;
            this._goodsUnit = goodsUnit;
            this._unitMemo = unitMemo;
            this._goodsQty = goodsQty;
            this._checkQty = checkQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._isException = isException;
            this._isHandleException = isHandleException;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._sysLotSerial = sysLotSerial;
            this._lotSerial = lotSerial;
            this._goodsNo = goodsNo;
            this._compGoodsLinkID = compGoodsLinkID;
            this._prodDate = prodDate;
            this._invalidDate = invalidDate;
            this._reaGoodsNo = reaGoodsNo;
            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._lotQRCode = lotQRCode;
            this._prodGoodsNo = prodGoodsNo;
            this._reaCompCode = reaCompCode;
            this._goodsSort = goodsSort;
            this._goodsLotID = goodsLotID;
            this._barCodeType = barCodeType;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BarCodeType", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long BarCodeType
        {
            get { return _barCodeType; }
            set { _barCodeType = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品批号ID", ShortCode = "GoodsLotID", Desc = "货品批号ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsLotID
        {
            get { return _goodsLotID; }
            set { _goodsLotID = value; }
        }
        [DataMember]
        [DataDesc(CName = "产品序号", ShortCode = "GoodsSort", Desc = "产品序号", ContextType = SysDic.All, Length = 4)]
        public virtual int GoodsSort
        {
            get { return _goodsSort; }
            set { _goodsSort = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompCode
        {
            get { return _reaCompCode; }
            set
            {
                _reaCompCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "二维批条码", ShortCode = "LotQRCode", Desc = "二维批条码", ContextType = SysDic.All, Length = 150)]
        public virtual string LotQRCode
        {
            get { return _lotQRCode; }
            set { _lotQRCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "机构货品编号", ShortCode = "ReaGoodsNo", Desc = "机构货品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo
        {
            get { return _reaGoodsNo; }
            set { _reaGoodsNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProdGoodsNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set
            {
                _prodGoodsNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "供应商货品编码", ShortCode = "CenOrgGoodsNo", Desc = "供应商货品编码", ContextType = SysDic.All, Length = 100)]
        public virtual string CenOrgGoodsNo
        {
            get { return _cenOrgGoodsNo; }
            set { _cenOrgGoodsNo = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckDocID
        {
            get { return _checkDocID; }
            set { _checkDocID = value; }
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
        [DataDesc(CName = "", ShortCode = "CompanyName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CompanyName", value, value.ToString());
                _companyName = value;
            }
        }

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
        [DataDesc(CName = "", ShortCode = "ReaServerCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerCompCode
        {
            get { return _reaServerCompCode; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaServerCompCode", value, value.ToString());
                _reaServerCompCode = value;
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
        [DataDesc(CName = "", ShortCode = "GoodsName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsName
        {
            get { return _goodsName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsName", value, value.ToString());
                _goodsName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LotNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LotNo", value, value.ToString());
                _lotNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsUnit", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsUnit
        {
            get { return _goodsUnit; }
            set
            {
                _goodsUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UnitMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for UnitMemo", value, value.ToString());
                _unitMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? CheckQty
        {
            get { return _checkQty; }
            set { _checkQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SumTotal", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumTotal
        {
            get { return _sumTotal; }
            set { _sumTotal = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsException", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsException
        {
            get { return _isException; }
            set { _isException = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsHandleException", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsHandleException
        {
            get { return _isHandleException; }
            set { _isHandleException = value; }
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
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.DateTime)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        [DataMember]
        [DataDesc(CName = "", ShortCode = "SysLotSerial", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string SysLotSerial
        {
            get { return _sysLotSerial; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for SysLotSerial", value, value.ToString());
                _sysLotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LotSerial", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string LotSerial
        {
            get { return _lotSerial; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentOutOfRangeException("Invalid value for LotSerial", value, value.ToString());
                _lotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsNo
        {
            get { return _goodsNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsNo", value, value.ToString());
                _goodsNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供应商货品关系ID", ShortCode = "CompGoodsLinkID", Desc = "供应商货品关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID
        {
            get { return _compGoodsLinkID; }
            set { _compGoodsLinkID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ProdDate", Desc = "", ContextType = SysDic.DateTime)]
        public virtual DateTime? ProdDate
        {
            get { return _prodDate; }
            set { _prodDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InvalidDate", Desc = "", ContextType = SysDic.DateTime)]
        public virtual DateTime? InvalidDate
        {
            get { return _invalidDate; }
            set { _invalidDate = value; }
        }
        #endregion

        #region 自定义属性

        /// <summary>
        /// 厂商名称
        /// </summary>
        public virtual string ProdOrgName { get; set; }
        /// <summary>
        /// 货品简称
        /// </summary>
        public virtual string GoodsSName { get; set; }

        private void _setReaBmsCheckDtl(ReaBmsCheckDtl reabmscheckdtl)
        {
            this._id = reabmscheckdtl.Id;
            this._labID = reabmscheckdtl.LabID;
            this._checkDocID = reabmscheckdtl.CheckDocID;
            this._reaCompanyID = reabmscheckdtl.ReaCompanyID;
            this._companyName = reabmscheckdtl.CompanyName;
            this._storageID = reabmscheckdtl.StorageID;
            this._placeID = reabmscheckdtl.PlaceID;
            this._storageName = reabmscheckdtl.StorageName;
            this._placeName = reabmscheckdtl.PlaceName;
            this._reaServerCompCode = reabmscheckdtl.ReaServerCompCode;
            this._goodsID = reabmscheckdtl.GoodsID;
            this._goodsName = reabmscheckdtl.GoodsName;
            this._lotNo = reabmscheckdtl.LotNo;
            this._goodsUnit = reabmscheckdtl.GoodsUnit;
            this._unitMemo = reabmscheckdtl.UnitMemo;
            this._goodsQty = reabmscheckdtl.GoodsQty;
            this._checkQty = reabmscheckdtl.CheckQty;
            this._price = reabmscheckdtl.Price;
            this._sumTotal = reabmscheckdtl.SumTotal;
            this._isException = reabmscheckdtl.IsException;
            this._isHandleException = reabmscheckdtl.IsHandleException;
            this._zX1 = reabmscheckdtl.ZX1;
            this._zX2 = reabmscheckdtl.ZX2;
            this._zX3 = reabmscheckdtl.ZX3;
            this._dispOrder = reabmscheckdtl.DispOrder;
            this._memo = reabmscheckdtl.Memo;
            this._visible = reabmscheckdtl.Visible;
            this._dataAddTime = reabmscheckdtl.DataAddTime;
            this._dataUpdateTime = reabmscheckdtl.DataUpdateTime;
            this._dataTimeStamp = reabmscheckdtl.DataTimeStamp;
            this._sysLotSerial = reabmscheckdtl.SysLotSerial;
            this._lotSerial = reabmscheckdtl.LotSerial;
            this._goodsNo = reabmscheckdtl.GoodsNo;
            this._compGoodsLinkID = reabmscheckdtl.CompGoodsLinkID;
            this._prodDate = reabmscheckdtl.ProdDate;
            this._invalidDate = reabmscheckdtl.InvalidDate;
            this._reaGoodsNo = reabmscheckdtl.ReaGoodsNo;
            this._cenOrgGoodsNo = reabmscheckdtl.CenOrgGoodsNo;
            this._lotQRCode = reabmscheckdtl.LotQRCode;
            this._prodGoodsNo = reabmscheckdtl.ProdGoodsNo;
            this._reaCompCode = reabmscheckdtl.ReaCompCode;
            this._goodsSort = reabmscheckdtl.GoodsSort;
            this._goodsLotID = reabmscheckdtl.GoodsLotID;
            this._barCodeType = reabmscheckdtl.BarCodeType;
        }
        public ReaBmsCheckDtl(ReaBmsCheckDtl reabmscheckdtl)
        {
            _setReaBmsCheckDtl(reabmscheckdtl);
        }
        public ReaBmsCheckDtl(ReaBmsCheckDtl reabmscheckdtl, ReaGoods reagoods)
        {
            _setReaBmsCheckDtl(reabmscheckdtl);
            this._dispOrder = reagoods.DispOrder;
            this._goodsSort = reagoods.GoodsSort;
            this.ProdOrgName = reagoods.ProdOrgName;
            this.GoodsSName = reagoods.SName;
        }
        public ReaBmsCheckDtl(ReaBmsCheckDoc reabmscheckdoc, ReaBmsCheckDtl reabmscheckdtl, ReaGoods reagoods)
        {
            _setReaBmsCheckDtl(reabmscheckdtl);
            this._dispOrder = reagoods.DispOrder;
            this._goodsSort = reagoods.GoodsSort;
            this.ProdOrgName = reagoods.ProdOrgName;
            this.GoodsSName = reagoods.SName;
        }
        #endregion
    }
    #endregion
}