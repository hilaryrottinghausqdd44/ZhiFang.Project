using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSamplingItem

	/// <summary>
	/// MEPTSamplingItem object for NHibernate mapped table 'MEPT_SamplingItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "采样项目维护", ClassCName = "MEPTSamplingItem", ShortCode = "MEPTSamplingItem", Desc = "采样项目维护")]
    public class MEPTSamplingItem : BaseEntity
	{
		#region Member Variables
		
        
        protected int _iSDefault;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected MEPTSamplingGroup _mEPTSamplingGroup;
		protected ItemAllItem _itemAllItem;

		#endregion

		#region Constructors

		public MEPTSamplingItem() { }

		public MEPTSamplingItem( long labID, int iSDefault, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEPTSamplingGroup mEPTSamplingGroup, ItemAllItem itemAllItem )
		{
			this._labID = labID;
			this._iSDefault = iSDefault;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEPTSamplingGroup = mEPTSamplingGroup;
			this._itemAllItem = itemAllItem;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "是否默认项目", ShortCode = "IsDefault", Desc = "是否默认项目", ContextType = SysDic.All, Length = 4)]
        public virtual int IsDefault
		{
			get { return _iSDefault; }
			set { _iSDefault = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
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
        [DataDesc(CName = "采样组设置", ShortCode = "MEPTSamplingGroup", Desc = "采样组设置", ContextType = SysDic.All)]
		public virtual MEPTSamplingGroup MEPTSamplingGroup
		{
			get { return _mEPTSamplingGroup; }
			set { _mEPTSamplingGroup = value; }
		}

        [DataMember]
        [DataDesc(CName = "项目", ShortCode = "ItemAllItem", Desc = "项目", ContextType = SysDic.All)]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

		#endregion
	}
	#endregion
}