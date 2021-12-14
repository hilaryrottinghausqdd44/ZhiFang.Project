using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BCalcItem

	/// <summary>
	/// BCalcItem object for NHibernate mapped table 'B_CalcItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "计算公式中涉及的项目", ClassCName = "BCalcItem", ShortCode = "BCalcItem", Desc = "计算公式中涉及的项目")]
	public class BCalcItem : BaseEntity
	{
		#region Member Variables
		
        protected DateTime? _dataUpdateTime;
		protected BCalcFormula _bCalcFormula;
		protected ItemAllItem _itemAllItem;

		#endregion

		#region Constructors

		public BCalcItem() { }

		public BCalcItem( long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BCalcFormula bCalcFormula, ItemAllItem itemAllItem )
		{
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bCalcFormula = bCalcFormula;
			this._itemAllItem = itemAllItem;
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
        [DataDesc(CName = "计算公式", ShortCode = "BCalcFormula", Desc = "计算公式")]
		public virtual BCalcFormula BCalcFormula
		{
			get { return _bCalcFormula; }
			set { _bCalcFormula = value; }
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