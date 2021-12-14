using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaGoodsLot

	/// <summary>
	/// ReaGoodsLot object for NHibernate mapped table 'Rea_GoodsLot'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "货品批号表", ClassCName = "ReaGoodsLot", ShortCode = "ReaGoodsLot", Desc = "货品批号表")]
	public class ReaGoodsLot : BaseEntity
	{
		#region Member Variables
		
        protected string _goodsCName;
        protected string _lotNo;
        protected DateTime? _prodDate;
        protected DateTime? _invalidDate;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;
		protected ReaGoods _reaGoods;
        protected long? _FactoryID;
        protected string _FactoryName;

        #endregion

        #region Constructors

        public ReaGoodsLot() { }

		public ReaGoodsLot( long labID, string goodsCName, string lotNo, DateTime prodDate, DateTime invalidDate, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ReaGoods reaGoods )
		{
			this._labID = labID;
			this._goodsCName = goodsCName;
			this._lotNo = lotNo;
			this._prodDate = prodDate;
			this._invalidDate = invalidDate;
			this._dispOrder = dispOrder;
			this._memo = memo;
			this._visible = visible;
			this._createrID = createrID;
			this._createrName = createrName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._reaGoods = reaGoods;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "产品中文名", ShortCode = "GoodsCName", Desc = "产品中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsCName
		{
			get { return _goodsCName; }
			set { _goodsCName = value; }
		}

        [DataMember]
        [DataDesc(CName = "批号", ShortCode = "LotNo", Desc = "批号", ContextType = SysDic.All, Length = 20)]
        public virtual string LotNo
		{
			get { return _lotNo; }
			set { _lotNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生产日期", ShortCode = "ProdDate", Desc = "生产日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ProdDate
		{
			get { return _prodDate; }
			set { _prodDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期", ShortCode = "InvalidDate", Desc = "有效期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InvalidDate
		{
			get { return _invalidDate; }
			set { _invalidDate = value; }
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
        [DataDesc(CName = "平台货品表", ShortCode = "ReaGoods", Desc = "平台货品表")]
		public virtual ReaGoods ReaGoods
		{
			get { return _reaGoods; }
			set { _reaGoods = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "厂家ID", ShortCode = "FactoryID", Desc = "厂家ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? FactoryID
        {
            get { return _FactoryID; }
            set { _FactoryID = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂家名称", ShortCode = "FactoryName", Desc = "厂家名称", ContextType = SysDic.All, Length = 50)]
        public virtual string FactoryName
        {
            get { return _FactoryName; }
            set { _FactoryName = value; }
        }
        #endregion
    }
	#endregion
}