using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region ItemItemCon

	/// <summary>
	/// ItemItemCon object for NHibernate mapped table 'Item_ItemCon'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "项目关系", ClassCName = "", ShortCode = "XMGX", Desc = "项目关系")]
    public class ItemItemCon : BaseEntity
	{
		#region Member Variables
		
		
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected ItemAllItem _itemAllItem;
		protected ItemAllItem _group;

		#endregion

		#region Constructors

		public ItemItemCon() { }

		public ItemItemCon( long labID, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, ItemAllItem group )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
			this._group = group;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "XSCX", Desc = "显示次序", ContextType = SysDic.Number, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "项目", ShortCode = "XM", Desc = "项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "组合项目", ShortCode = "ZHXM", Desc = "组合项目")]
		public virtual ItemAllItem Group
		{
			get { return _group; }
			set { _group = value; }
		}

		#endregion
	}
	#endregion
}