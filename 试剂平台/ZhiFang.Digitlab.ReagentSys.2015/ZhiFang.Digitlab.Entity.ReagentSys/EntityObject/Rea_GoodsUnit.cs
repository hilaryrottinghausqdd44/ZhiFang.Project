using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaGoodsUnit

	/// <summary>
	/// ReaGoodsUnit object for NHibernate mapped table 'Rea_GoodsUnit'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "货品包装单位表", ClassCName = "ReaGoodsUnit", ShortCode = "ReaGoodsUnit", Desc = "货品包装单位表")]
	public class ReaGoodsUnit : BaseEntity
	{
		#region Member Variables
		
        protected string _goodsUnit;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public ReaGoodsUnit() { }

		public ReaGoodsUnit( long labID, string goodsUnit, string changeUnit, long changeQty, string goodsCName, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp, ReaGoods reaGoods, ReaGoodsUnit reaGoodsUnit)
		{
			this._labID = labID;
			this._goodsUnit = goodsUnit;
			this._dispOrder = dispOrder;
			this._memo = memo;
			this._visible = visible;
			this._createrID = createrID;
			this._createrName = createrName;
			this._dataUpdateTime = dataUpdateTime;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "包装单位名称", ShortCode = "GoodsUnit", Desc = "包装单位名称", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsUnit
		{
			get { return _goodsUnit; }
			set { _goodsUnit = value; }
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
        
		#endregion
	}
	#endregion
}