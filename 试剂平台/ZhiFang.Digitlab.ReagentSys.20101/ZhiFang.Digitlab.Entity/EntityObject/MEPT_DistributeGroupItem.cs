using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTDistributeGroupItem

	/// <summary>
	/// MEPTDistributeGroupItem object for NHibernate mapped table 'MEPT_DistributeGroupItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "分发小组项目", ClassCName = "MEPTDistributeGroupItem", ShortCode = "MEPTDistributeGroupItem", Desc = "分发小组项目")]
	public class MEPTDistributeGroupItem : BaseEntity
	{
		#region Member Variables
		
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected MEPTDistributeGroup _mEPTDistributeGroup;
		protected ItemAllItem _itemAllItem;

		#endregion

		#region Constructors

		public MEPTDistributeGroupItem() { }

		public MEPTDistributeGroupItem( long labID, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEPTDistributeGroup mEPTDistributeGroup, ItemAllItem itemAllItem )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEPTDistributeGroup = mEPTDistributeGroup;
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
        [DataDesc(CName = "分发小组", ShortCode = "MEPTDistributeGroup", Desc = "分发小组")]
		public virtual MEPTDistributeGroup MEPTDistributeGroup
		{
			get { return _mEPTDistributeGroup; }
			set { _mEPTDistributeGroup = value; }
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