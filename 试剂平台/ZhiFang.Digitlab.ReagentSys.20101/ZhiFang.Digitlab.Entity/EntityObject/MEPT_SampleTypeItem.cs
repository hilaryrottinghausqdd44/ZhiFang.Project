using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSampleTypeItem

	/// <summary>
	/// MEPTSampleTypeItem object for NHibernate mapped table 'MEPT_SampleTypeItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "样本类型-项目", ClassCName = "MEPTSampleTypeItem", ShortCode = "MEPTSampleTypeItem", Desc = "样本类型-项目")]
	public class MEPTSampleTypeItem : BaseEntity
	{
		#region Member Variables
		
        
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected ItemAllItem _itemAllItem;
		protected BSampleType _bSampleType;

		#endregion

		#region Constructors

		public MEPTSampleTypeItem() { }

		public MEPTSampleTypeItem( long labID, int dispOrder, string comment, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, BSampleType mEBSampleType )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
			this._bSampleType = mEBSampleType;
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
        [DataDesc(CName = "说明", ShortCode = "Comment", Desc = "说明", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
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
        [DataDesc(CName = "项目", ShortCode = "ItemAllItem", Desc = "项目", ContextType = SysDic.All)]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型", ContextType = SysDic.All)]
        public virtual BSampleType BSampleType
		{
			get { return _bSampleType; }
			set { _bSampleType = value; }
		}

		#endregion
	}
	#endregion
}