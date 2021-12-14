using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaBmsOutDtl

	/// <summary>
	/// ReaBmsOutDtl object for NHibernate mapped table 'Rea_BmsOutDtl'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "出库明细表", ClassCName = "ReaBmsOutDtl", ShortCode = "ReaBmsOutDtl", Desc = "出库明细表")]
    public class ReaBmsOutDtl : BaseEntity
    {
        #region Member Variables

        protected long? _outDocID;
        protected long _qtyDtlID;
        protected long? _goodsID;
        protected string _goodsCName;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected double _goodsQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected string _lotNo;
        protected long _storageID;
        protected long _placeID;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;
        protected string _storageName;
        protected string _placeName;
        protected long _companyID;
        protected string _goodsSerial;
        protected string _lotSerial;
        protected string _SysLotSerial;

        #endregion

        #region Constructors

        public ReaBmsOutDtl() { }

        public ReaBmsOutDtl(long labID, long outDocID, long qtyDtlID, long goodsID, string goodsCName, string serialNo, long goodsUnitID, string goodsUnit, double goodsQty, double price, double sumTotal, double taxRate, string lotNo, long storageID, long placeID, string goodsSerial, string packSerial, string lotSerial, string mixSerial, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string storageName, string placeName)
        {
            this._labID = labID;
            this._outDocID = outDocID;
            this._qtyDtlID = qtyDtlID;
            this._goodsID = goodsID;
            this._goodsCName = goodsCName;
            this._goodsUnitID = goodsUnitID;
            this._goodsUnit = goodsUnit;
            this._goodsQty = goodsQty;
            this._price = price;
            this._sumTotal = sumTotal;
            this._taxRate = taxRate;
            this._lotNo = lotNo;
            this._storageID = storageID;
            this._placeID = placeID;
            this._goodsSerial = goodsSerial;
            this._lotSerial = lotSerial;
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
            this._storageName = storageName;
            this._placeName = placeName;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库总单ID", ShortCode = "OutDocID", Desc = "出库总单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OutDocID
        {
            get { return _outDocID; }
            set { _outDocID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存ID", ShortCode = "QtyDtlID", Desc = "库存ID", ContextType = SysDic.All, Length = 8)]
        public virtual long QtyDtlID
        {
            get { return _qtyDtlID; }
            set { _qtyDtlID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品ID", ShortCode = "GoodsID", Desc = "货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID
        {
            get { return _goodsID; }
            set { _goodsID = value; }
        }

        [DataMember]
        [DataDesc(CName = "货品中文名", ShortCode = "GoodsCName", Desc = "货品中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsCName
        {
            get { return _goodsCName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsCName", value, value.ToString());
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
        [DataDesc(CName = "包装单位", ShortCode = "GoodsUnit", Desc = "包装单位", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsUnit
        {
            get { return _goodsUnit; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsUnit", value, value.ToString());
                _goodsUnit = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库数量", ShortCode = "GoodsQty", Desc = "出库数量", ContextType = SysDic.All, Length = 8)]
        public virtual double GoodsQty
        {
            get { return _goodsQty; }
            set { _goodsQty = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单价", ShortCode = "Price", Desc = "单价", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总计金额", ShortCode = "SumTotal", Desc = "总计金额", ContextType = SysDic.All, Length = 8)]
        public virtual double SumTotal
        {
            get { return _sumTotal; }
            set { _sumTotal = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "税率", ShortCode = "TaxRate", Desc = "税率", ContextType = SysDic.All, Length = 8)]
        public virtual double TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }

        [DataMember]
        [DataDesc(CName = "货品批号", ShortCode = "LotNo", Desc = "货品批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set
            {
                _lotNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库房ID", ShortCode = "StorageID", Desc = "库房ID", ContextType = SysDic.All, Length = 8)]
        public virtual long StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货架ID", ShortCode = "PlaceID", Desc = "货架ID", ContextType = SysDic.All, Length = 8)]
        public virtual long PlaceID
        {
            get { return _placeID; }
            set { _placeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "产品条码", ShortCode = "GoodsSerial", Desc = "产品条码", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsSerial
        {
            get { return _goodsSerial; }
            set
            {
                _goodsSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "批号条码", ShortCode = "LotSerial", Desc = "批号条码", ContextType = SysDic.All, Length = 100)]
        public virtual string LotSerial
        {
            get { return _lotSerial; }
            set
            {
                _lotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = " 系统内部批号条码", ShortCode = "SysLotSerial", Desc = " 系统内部批号条码", ContextType = SysDic.All, Length = 100)]
        public virtual string SysLotSerial
        {
            get { return _SysLotSerial; }
            set
            {
                _SysLotSerial = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "更新时间", ShortCode = "DataUpdateTime", Desc = "更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "货位名称", ShortCode = "StorageName", Desc = "货位名称", ContextType = SysDic.All, Length = 100)]
        public virtual string StorageName
        {
            get { return _storageName; }
            set
            {

                _storageName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "存储库房名称", ShortCode = "PlaceName", Desc = "存储库房名", ContextType = SysDic.All, Length = 100)]
        public virtual string PlaceName
        {
            get { return _placeName; }
            set
            {

                _placeName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地供应商", ShortCode = "ReaCompanyID", Desc = "本地供应商", ContextType = SysDic.All, Length = 8)]
        public virtual long ReaCompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }

        #endregion
    }
    #endregion
}