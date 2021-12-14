using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSampleDeliveryItem

	/// <summary>
	/// MEPTSampleDeliveryItem object for NHibernate mapped table 'MEPT_SampleDeliveryItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "外送项目表", ClassCName = "MEPTSampleDeliveryItem", ShortCode = "MEPTSampleDeliveryItem", Desc = "外送项目表")]
	public class MEPTSampleDeliveryItem : BaseEntity
	{
		#region Member Variables
		
        protected bool _isUse;
        protected int _reportDays;
        protected DateTime? _dataUpdateTime;
        protected BLaboratory _deliveryLab;
		protected ItemAllItem _itemAllItem;

		#endregion

		#region Constructors

		public MEPTSampleDeliveryItem() { }

		public MEPTSampleDeliveryItem( long labID, bool isUse, int reportDays, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BLaboratory deliveryLab, ItemAllItem itemAllItem )
		{
			this._labID = labID;
			this._isUse = isUse;
			this._reportDays = reportDays;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
            this._deliveryLab = deliveryLab;
			this._itemAllItem = itemAllItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "报告天数", ShortCode = "ReportDays", Desc = "报告天数", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportDays
		{
			get { return _reportDays; }
			set { _reportDays = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "更新时间", ShortCode = "DataUpdateTime", Desc = "更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "实验室字典表", ShortCode = "Delivery", Desc = "实验室字典表")]
        public virtual BLaboratory DeliveryLab
		{
			get { return _deliveryLab; }
            set { _deliveryLab = value; }
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