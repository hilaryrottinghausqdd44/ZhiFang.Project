using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMicroBCBottleTypeOfItem

	/// <summary>
	/// BMicroBCBottleTypeOfItem object for NHibernate mapped table 'B_MicroBCBottleTypeOfItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物项目与血培养瓶类型关系表", ClassCName = "BMicroBCBottleTypeOfItem", ShortCode = "BMicroBCBottleTypeOfItem", Desc = "微生物项目与血培养瓶类型关系表")]
	public class BMicroBCBottleTypeOfItem : BaseEntity
	{
		#region Member Variables
		
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMicroBloodCultureBottleType _bMicroBloodCultureBottleType;
		protected ItemAllItem _itemAllItem;

		#endregion

		#region Constructors

		public BMicroBCBottleTypeOfItem() { }

		public BMicroBCBottleTypeOfItem( long labID, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroBloodCultureBottleType bMicroBloodCultureBottleType, ItemAllItem itemAllItem )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroBloodCultureBottleType = bMicroBloodCultureBottleType;
			this._itemAllItem = itemAllItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物血培养瓶类型", ShortCode = "BMicroBloodCultureBottleType", Desc = "微生物血培养瓶类型")]
		public virtual BMicroBloodCultureBottleType BMicroBloodCultureBottleType
		{
			get { return _bMicroBloodCultureBottleType; }
			set { _bMicroBloodCultureBottleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        
		#endregion
	}
	#endregion
}