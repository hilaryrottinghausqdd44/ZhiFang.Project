using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaBmsInDtl

	/// <summary>
	/// ReaBmsInDtl object for NHibernate mapped table 'Rea_BmsInDtl'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "入库明细表", ClassCName = "ReaBmsInDtl", ShortCode = "ReaBmsInDtl", Desc = "入库明细表")]
	public class ReaBmsInDtl : BaseEntity
	{
		#region Member Variables
		
        protected string _inDtlNo;
        protected string _inDocNo;
        protected string _goodsCName;
        protected string _goodsUnit;
        protected double _goodsQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected string _lotNo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;
		protected ReaBmsInDoc _reaBmsInDoc;
		protected ReaGoods _reaGoods;
        protected long? _goodsUnitID;
        protected long? _storageID;
        protected long? _placeID;
        protected string _storageName;
        protected string _placeName;
        protected long? _ReaCompanyID;
        protected string _CompanyName;
        protected long? _SaleDtlConfirmID;
        protected string _goodsSerial;
        protected string _lotSerial;
        protected string _SysLotSerial;

        #endregion

        #region Constructors

        public ReaBmsInDtl() { }

		public ReaBmsInDtl( long labID, string inDtlNo, string inDocNo, string goodsCName, string goodsUnit, double goodsQty, double price, double sumTotal, double taxRate, string lotNo, string goodsSerial, string packSerial, string lotSerial, string mixSerial, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ReaBmsInDoc reaBmsInDoc, ReaGoods reaGoods)
		{
			this._labID = labID;
			this._inDtlNo = inDtlNo;
			this._inDocNo = inDocNo;
			this._goodsCName = goodsCName;
			this._goodsUnit = goodsUnit;
			this._goodsQty = goodsQty;
			this._price = price;
			this._sumTotal = sumTotal;
			this._taxRate = taxRate;
			this._lotNo = lotNo;
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
			this._reaBmsInDoc = reaBmsInDoc;
			this._reaGoods = reaGoods;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "入库明细单号", ShortCode = "InDtlNo", Desc = "入库明细单号", ContextType = SysDic.All, Length = 20)]
        public virtual string InDtlNo
		{
			get { return _inDtlNo; }
			set { _inDtlNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库总单号", ShortCode = "InDocNo", Desc = "入库总单号", ContextType = SysDic.All, Length = 20)]
        public virtual string InDocNo
		{
			get { return _inDocNo; }
			set { _inDocNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "货品中文名", ShortCode = "GoodsCName", Desc = "货品中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsCName
		{
			get { return _goodsCName; }
			set { _goodsCName = value; }
		}

   
        [DataMember]
        [DataDesc(CName = "包装单位", ShortCode = "GoodsUnit", Desc = "包装单位", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsUnit
		{
			get { return _goodsUnit; }
			set { _goodsUnit = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "购进数量", ShortCode = "GoodsQty", Desc = "购进数量", ContextType = SysDic.All, Length = 8)]
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
			set { _lotNo = value; }
		}        

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set { _zX1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set { _zX2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set { _zX3 = value; }
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
			set { _memo = value; }
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
		public virtual long? CreaterID
		{
			get { return _createrID; }
			set { _createrID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
		{
			get { return _createrName; }
			set { _createrName = value; }
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
        [DataDesc(CName = "入库总单表", ShortCode = "ReaBmsInDoc", Desc = "入库总单表")]
		public virtual ReaBmsInDoc ReaBmsInDoc
		{
			get { return _reaBmsInDoc; }
			set { _reaBmsInDoc = value; }
		}

        [DataMember]
        [DataDesc(CName = "平台货品表", ShortCode = "ReaGoods", Desc = "平台货品表")]
		public virtual ReaGoods ReaGoods
		{
			get { return _reaGoods; }
			set { _reaGoods = value; }
		}

        //      [DataMember]
        //      [DataDesc(CName = "货品包装单位表", ShortCode = "ReaGoodsUnit", Desc = "货品包装单位表")]
        //public virtual ReaGoodsUnit ReaGoodsUnit
        //{
        //	get { return _reaGoodsUnit; }
        //	set { _reaGoodsUnit = value; }
        //}

        //      [DataMember]
        //      [DataDesc(CName = "货位表", ShortCode = "ReaPlace", Desc = "货位表")]
        //public virtual ReaPlace ReaPlace
        //{
        //	get { return _reaPlace; }
        //	set { _reaPlace = value; }
        //}

        //      [DataMember]
        //      [DataDesc(CName = "存储库房表", ShortCode = "ReaStorage", Desc = "存储库房表")]
        //public virtual ReaStorage ReaStorage
        //{
        //	get { return _reaStorage; }
        //	set { _reaStorage = value; }
        //}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品包装单位ID", ShortCode = "GoodsUnitID", Desc = "货品包装单位ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsUnitID
        {
            get { return _goodsUnitID; }
            set { _goodsUnitID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货位ID", ShortCode = "StorageID", Desc = "货位ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "存储库房ID", ShortCode = "PlaceID", Desc = "存储库房ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PlaceID
        {
            get { return _placeID; }
            set { _placeID = value; }
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
        [DataDesc(CName = "本地机构ID", ShortCode = "ReaCompanyID", Desc = "本地机构ID", ContextType = SysDic.All, Length = 100)]
        public virtual long? ReaCompanyID
        {
            get { return _ReaCompanyID; }
            set
            {

                _ReaCompanyID = value;
            }
        }


        [DataMember]
        [DataDesc(CName = "本地机构名称", ShortCode = "CompanyName", Desc = "本地机构名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CompanyName
        {
            get { return _CompanyName; }
            set
            {

                _CompanyName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "验收明细ID", ShortCode = "SaleDtlConfirmID", Desc = "验收明细ID", ContextType = SysDic.All, Length = 100)]
        public virtual long? SaleDtlConfirmID
        {
            get { return _SaleDtlConfirmID; }
            set
            {

                _SaleDtlConfirmID = value;
            }
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
        #endregion
    }
	#endregion
}