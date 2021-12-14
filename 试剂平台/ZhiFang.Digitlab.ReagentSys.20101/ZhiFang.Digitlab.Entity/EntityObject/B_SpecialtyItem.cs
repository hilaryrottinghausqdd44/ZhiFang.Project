using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BSpecialtyItem

	/// <summary>
	/// BSpecialtyItem object for NHibernate mapped table 'B_SpecialtyItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "专业项目", ClassCName = "BSpecialtyItem", ShortCode = "BSpecialtyItem", Desc = "专业项目")]
	public class BSpecialtyItem : BaseEntity
	{
		#region Member Variables
		
        protected DateTime? _dataUpdateTime;
		protected ItemAllItem _itemAllItem;
		protected BSpecialty _bSpecialty;

		#endregion

		#region Constructors

		public BSpecialtyItem() { }

		public BSpecialtyItem( long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, BSpecialty bSpecialty )
		{
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
			this._bSpecialty = bSpecialty;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "专业表", ShortCode = "BSpecialty", Desc = "专业表")]
		public virtual BSpecialty BSpecialty
		{
			get { return _bSpecialty; }
			set { _bSpecialty = value; }
		}

        
		#endregion
	}
	#endregion
}