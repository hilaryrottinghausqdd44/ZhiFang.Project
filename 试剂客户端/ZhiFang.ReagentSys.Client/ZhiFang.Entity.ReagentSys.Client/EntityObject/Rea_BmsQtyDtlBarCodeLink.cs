using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
	#region ReaBmsQtyDtlBarCodeLink

	/// <summary>
	/// ReaBmsQtyDtlBarCodeLink object for NHibernate mapped table 'Rea_BmsQtyDtlBarCodeLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsQtyDtlBarCodeLink", ShortCode = "ReaBmsQtyDtlBarCodeLink", Desc = "")]
	public class ReaBmsQtyDtlBarCodeLink : BaseEntity
	{
		#region Member Variables
		
        protected long _pReaGBOID;
        protected long? _qtyDtlID;
        protected long? _reaGBOID;
        protected long _goodsID;
        protected string _goodsUnit;
        protected double _goodsQty;
        protected double _minBarCodeQty;
		

		#endregion

		#region Constructors

		public ReaBmsQtyDtlBarCodeLink() { }

		public ReaBmsQtyDtlBarCodeLink( long labID, long pReaGBOID, long qtyDtlID, long reaGBOID, long goodsID, string goodsUnit, double goodsQty, double minBarCodeQty, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._pReaGBOID = pReaGBOID;
			this._qtyDtlID = qtyDtlID;
			this._reaGBOID = reaGBOID;
			this._goodsID = goodsID;
			this._goodsUnit = goodsUnit;
			this._goodsQty = goodsQty;
			this._minBarCodeQty = minBarCodeQty;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PReaGBOID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long PReaGBOID
		{
			get { return _pReaGBOID; }
			set { _pReaGBOID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "QtyDtlID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? QtyDtlID
		{
			get { return _qtyDtlID; }
			set { _qtyDtlID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReaGBOID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaGBOID
		{
			get { return _reaGBOID; }
			set { _reaGBOID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GoodsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long GoodsID
		{
			get { return _goodsID; }
			set { _goodsID = value; }
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
        [DataDesc(CName = "", ShortCode = "MinBarCodeQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double MinBarCodeQty
		{
			get { return _minBarCodeQty; }
			set { _minBarCodeQty = value; }
		}

		
		#endregion
	}
	#endregion
}