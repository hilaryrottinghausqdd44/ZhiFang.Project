using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaBmsTransferDtl

	/// <summary>
	/// ReaBmsTransferDtl object for NHibernate mapped table 'Rea_BmsTransferDtl'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "移库明细表", ClassCName = "ReaBmsTransferDtl", ShortCode = "ReaBmsTransferDtl", Desc = "")]
	public class ReaBmsTransferDtl : BaseEntity
	{
		#region Member Variables
		
        protected long? _transferDocID;
        protected long _qtyDtlID;
        protected long? _goodsID;
        protected string _goodsCName;
        protected string _serialNo;
        protected long? _goodsUnitID;
        protected string _goodsUnit;
        protected long _reaCompanyID;
        protected string _reaCompanyName;
        protected double _goodsQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected string _lotNo;
        protected long? _sStorageID;
        protected long? _sPlaceID;
        protected string _sStorageName;
        protected string _sPlaceName;
        protected long? _dStorageID;
        protected long? _dPlaceID;
        protected string _dStorageName;
        protected string _dPlaceName;
        protected string _goodsSerial;
        protected string _packSerial;
        protected string _lotSerial;
        protected string _mixSerial;
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

		public ReaBmsTransferDtl() { }

		public ReaBmsTransferDtl( long labID, long transferDocID, long qtyDtlID, long goodsID, string goodsCName, string serialNo, long goodsUnitID, string goodsUnit, long reaCompanyID, string reaCompanyName, double goodsQty, double price, double sumTotal, double taxRate, string lotNo, long sStorageID, long sPlaceID, string sStorageName, string sPlaceName, long dStorageID, long dPlaceID, string dStorageName, string dPlaceName, string goodsSerial, string packSerial, string lotSerial, string mixSerial, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._transferDocID = transferDocID;
			this._qtyDtlID = qtyDtlID;
			this._goodsID = goodsID;
			this._goodsCName = goodsCName;
			this._serialNo = serialNo;
			this._goodsUnitID = goodsUnitID;
			this._goodsUnit = goodsUnit;
			this._reaCompanyID = reaCompanyID;
			this._reaCompanyName = reaCompanyName;
			this._goodsQty = goodsQty;
			this._price = price;
			this._sumTotal = sumTotal;
			this._taxRate = taxRate;
			this._lotNo = lotNo;
			this._sStorageID = sStorageID;
			this._sPlaceID = sPlaceID;
			this._sStorageName = sStorageName;
			this._sPlaceName = sPlaceName;
			this._dStorageID = dStorageID;
			this._dPlaceID = dPlaceID;
			this._dStorageName = dStorageName;
			this._dPlaceName = dPlaceName;
			this._goodsSerial = goodsSerial;
			this._packSerial = packSerial;
			this._lotSerial = lotSerial;
			this._mixSerial = mixSerial;
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
        [DataDesc(CName = "移库总单ID", ShortCode = "TransferDocID", Desc = "移库总单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TransferDocID
		{
			get { return _transferDocID; }
			set { _transferDocID = value; }
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
				_goodsCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "SerialNo", Desc = "条码号", ContextType = SysDic.All, Length = 50)]
        public virtual string SerialNo
		{
			get { return _serialNo; }
			set
			{
				_serialNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "包装单位ID", ShortCode = "GoodsUnitID", Desc = "包装单位ID", ContextType = SysDic.All, Length = 8)]
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
				_goodsUnit = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地供应商ID", ShortCode = "ReaCompanyID", Desc = "本地供应商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ReaCompanyID
		{
			get { return _reaCompanyID; }
			set { _reaCompanyID = value; }
		}

        [DataMember]
        [DataDesc(CName = "本地供应商名称", ShortCode = "ReaCompanyName", Desc = "本地供应商名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ReaCompanyName
		{
			get { return _reaCompanyName; }
			set
			{
				_reaCompanyName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "移库库数量", ShortCode = "GoodsQty", Desc = "移库库数量", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "源库房ID", ShortCode = "SStorageID", Desc = "源库房ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SStorageID
		{
			get { return _sStorageID; }
			set { _sStorageID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "源货架ID", ShortCode = "SPlaceID", Desc = "源货架ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SPlaceID
		{
			get { return _sPlaceID; }
			set { _sPlaceID = value; }
		}

        [DataMember]
        [DataDesc(CName = "源库房名称", ShortCode = "SStorageName", Desc = "源库房名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SStorageName
		{
			get { return _sStorageName; }
			set
			{
				_sStorageName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "源货架名称", ShortCode = "SPlaceName", Desc = "源货架名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SPlaceName
		{
			get { return _sPlaceName; }
			set
			{
				_sPlaceName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "目的库房ID", ShortCode = "DStorageID", Desc = "目的库房ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DStorageID
		{
			get { return _dStorageID; }
			set { _dStorageID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "目的货架ID", ShortCode = "DPlaceID", Desc = "目的货架ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DPlaceID
		{
			get { return _dPlaceID; }
			set { _dPlaceID = value; }
		}

        [DataMember]
        [DataDesc(CName = "目的库房名称", ShortCode = "DStorageName", Desc = "目的库房名称", ContextType = SysDic.All, Length = 100)]
        public virtual string DStorageName
		{
			get { return _dStorageName; }
			set
			{
				_dStorageName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "目的货架名称", ShortCode = "DPlaceName", Desc = "目的货架名称", ContextType = SysDic.All, Length = 100)]
        public virtual string DPlaceName
		{
			get { return _dPlaceName; }
			set
			{
				_dPlaceName = value;
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
        [DataDesc(CName = "包装条码", ShortCode = "PackSerial", Desc = "包装条码", ContextType = SysDic.All, Length = 100)]
        public virtual string PackSerial
		{
			get { return _packSerial; }
			set
			{
				_packSerial = value;
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
        [DataDesc(CName = "混合条码", ShortCode = "MixSerial", Desc = "混合条码", ContextType = SysDic.All, Length = 100)]
        public virtual string MixSerial
		{
			get { return _mixSerial; }
			set
			{
				_mixSerial = value;
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		
		#endregion
	}
	#endregion
}