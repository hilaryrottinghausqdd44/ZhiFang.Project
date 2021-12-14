using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMicroTestItemAndSample

	/// <summary>
	/// BMicroTestItemAndSample object for NHibernate mapped table 'B_MicroTestItemAndSample'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物检验记录项与样本类型、检验项目关系字典表", ClassCName = "BMicroTestItemAndSample", ShortCode = "BMicroTestItemAndSample", Desc = "微生物检验记录项与样本类型、检验项目关系字典表")]
	public class BMicroTestItemAndSample : BaseEntity
	{
		#region Member Variables
		
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMicroTestItemInfo _bMicroTestItemInfo;
		protected BSampleType _bSampleType;
		protected ItemAllItem _itemAllItem;

		#endregion

		#region Constructors

		public BMicroTestItemAndSample() { }

		public BMicroTestItemAndSample( long labID, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroTestItemInfo bMicroTestItemInfo, BSampleType bSampleType, ItemAllItem itemAllItem )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroTestItemInfo = bMicroTestItemInfo;
			this._bSampleType = bSampleType;
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
        [DataDesc(CName = "微生物检验记录项字典表", ShortCode = "BMicroTestItemInfo", Desc = "微生物检验记录项字典表")]
		public virtual BMicroTestItemInfo BMicroTestItemInfo
		{
			get { return _bMicroTestItemInfo; }
			set { _bMicroTestItemInfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
		public virtual BSampleType BSampleType
		{
			get { return _bSampleType; }
			set { _bSampleType = value; }
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