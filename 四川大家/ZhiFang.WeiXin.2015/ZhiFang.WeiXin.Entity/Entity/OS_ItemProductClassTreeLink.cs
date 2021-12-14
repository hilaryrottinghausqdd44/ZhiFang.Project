using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region OSItemProductClassTreeLink

	/// <summary>
	/// OSItemProductClassTreeLink object for NHibernate mapped table 'OS_ItemProductClassTreeLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "检测项目产品分类树关系", ClassCName = "OSItemProductClassTreeLink", ShortCode = "OSItemProductClassTreeLink", Desc = "检测项目产品分类树关系")]
	public class OSItemProductClassTreeLink : BaseEntity
	{
		#region Member Variables
		
        protected long _areaID;
        protected long _itemProductClassTreeID;
        protected long _itemID;
        protected string _ItemNo;
        protected int _dispOrder;
        protected bool _isUse;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public OSItemProductClassTreeLink() { }

		public OSItemProductClassTreeLink( long areaID, long itemProductClassTreeID, long itemID, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._areaID = areaID;
			this._itemProductClassTreeID = itemProductClassTreeID;
			this._itemID = itemID;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._creatorID = creatorID;
			this._creatorName = creatorName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AreaID
		{
			get { return _areaID; }
			set { _areaID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemProductClassTreeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ItemProductClassTreeID
		{
			get { return _itemProductClassTreeID; }
			set { _itemProductClassTreeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目ID", ShortCode = "ItemID", Desc = "项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ItemID
		{
			get { return _itemID; }
			set { _itemID = value; }
		}

        [DataMember]
        [DataDesc(CName = "项目No", ShortCode = "ItemNo", Desc = "项目No", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemNo
        {
            get { return _ItemNo; }
            set { _ItemNo = value; }
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
        [DataDesc(CName = "创建者", ShortCode = "CreatorID", Desc = "创建者", ContextType = SysDic.All, Length = 8)]
        public virtual long CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreatorName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreatorName
		{
			get { return _creatorName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
				_creatorName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		
		#endregion
	}
	#endregion
}