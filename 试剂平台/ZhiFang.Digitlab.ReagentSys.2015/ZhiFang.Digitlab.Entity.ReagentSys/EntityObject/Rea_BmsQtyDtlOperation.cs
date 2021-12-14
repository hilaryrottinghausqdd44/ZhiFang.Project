using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaBmsQtyDtlOperation

	/// <summary>
	/// ReaBmsQtyDtlOperation object for NHibernate mapped table 'Rea_BmsQtyDtlOperation'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsQtyDtlOperation", ShortCode = "ReaBmsQtyDtlOperation", Desc = "")]
	public class ReaBmsQtyDtlOperation : BaseEntity
	{
		#region Member Variables
		
        protected long _reaCompanyID;
        protected string _companyName;
        protected long? _goodsID;
        protected string _goodsName;
        protected string _lotNo;
        protected long? _storageID;
        protected long? _placeID;
        protected string _storageName;
        protected string _placeName;
        protected long? _goodsUnitID;
        protected long? _orgID;
        protected string _goodsUnit;
        protected double _goodsQty;
        protected double _price;
        protected double _sumTotal;
        protected double _taxRate;
        protected int _outFlag;
        protected int _sumFlag;
        protected int _iOFlag;
        protected string _goodsSerial;
        protected string _lotSerial;
        protected string _sysLotSerial;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _memo;
        protected int _dispOrder;
        protected bool _visible;
        protected long _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;
        protected string _bDocNo;
        protected long _bDocID;
        protected long _bDtlID;
        protected long _operTypeID;
        protected string _operTypeName;
        protected double _changeCount;
		

		#endregion

		#region Constructors

		public ReaBmsQtyDtlOperation() { }

		public ReaBmsQtyDtlOperation( long labID, long reaCompanyID, string companyName, long goodsID, string goodsName, string lotNo, long storageID, long placeID, string storageName, string placeName, long goodsUnitID, long orgID, string goodsUnit, double goodsQty, double price, double sumTotal, double taxRate, int outFlag, int sumFlag, int iOFlag, string goodsSerial, string lotSerial, string sysLotSerial, string zX1, string zX2, string zX3, string memo, int dispOrder, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string bDocNo, long bDocID, long bDtlID, long operTypeID, string operTypeName, double changeCount )
		{
			this._labID = labID;
			this._reaCompanyID = reaCompanyID;
			this._companyName = companyName;
			this._goodsID = goodsID;
			this._goodsName = goodsName;
			this._lotNo = lotNo;
			this._storageID = storageID;
			this._placeID = placeID;
			this._storageName = storageName;
			this._placeName = placeName;
			this._goodsUnitID = goodsUnitID;
			this._orgID = orgID;
			this._goodsUnit = goodsUnit;
			this._goodsQty = goodsQty;
			this._price = price;
			this._sumTotal = sumTotal;
			this._taxRate = taxRate;
			this._outFlag = outFlag;
			this._sumFlag = sumFlag;
			this._iOFlag = iOFlag;
			this._goodsSerial = goodsSerial;
			this._lotSerial = lotSerial;
			this._sysLotSerial = sysLotSerial;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._createrID = createrID;
			this._createrName = createrName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bDocNo = bDocNo;
			this._bDocID = bDocID;
			this._bDtlID = bDtlID;
			this._operTypeID = operTypeID;
			this._operTypeName = operTypeName;
			this._changeCount = changeCount;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReaCompanyID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ReaCompanyID
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
        [DataDesc(CName = "", ShortCode = "GoodsName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsName
		{
			get { return _goodsName; }
			set
			{
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
				_lotNo = value;
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
				_placeName = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrgID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrgID
		{
			get { return _orgID; }
			set { _orgID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double GoodsQty
		{
			get { return _goodsQty; }
			set { _goodsQty = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SumTotal", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double SumTotal
		{
			get { return _sumTotal; }
			set { _sumTotal = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TaxRate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double TaxRate
		{
			get { return _taxRate; }
			set { _taxRate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OutFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OutFlag
		{
			get { return _outFlag; }
			set { _outFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SumFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SumFlag
		{
			get { return _sumFlag; }
			set { _sumFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IOFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
		{
			get { return _iOFlag; }
			set { _iOFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsSerial", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsSerial
		{
			get { return _goodsSerial; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for GoodsSerial", value, value.ToString());
				_goodsSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LotSerial", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LotSerial
		{
			get { return _lotSerial; }
			set
			{
				_lotSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SysLotSerial", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string SysLotSerial
		{
			get { return _sysLotSerial; }
			set
			{
				_sysLotSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set
			{
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
				_zX3 = value;
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
        public virtual long BDocID
		{
			get { return _bDocID; }
			set { _bDocID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BDtlID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long BDtlID
		{
			get { return _bDtlID; }
			set { _bDtlID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long OperTypeID
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ChangeCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double ChangeCount
		{
			get { return _changeCount; }
			set { _changeCount = value; }
		}

		
		#endregion
	}
	#endregion
}