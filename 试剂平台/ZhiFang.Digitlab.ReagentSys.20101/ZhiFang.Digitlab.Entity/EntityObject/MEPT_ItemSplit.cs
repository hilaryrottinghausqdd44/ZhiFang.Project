using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTItemSplit

	/// <summary>
	/// MEPTItemSplit object for NHibernate mapped table 'MEPT_ItemSplit'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "按项目拆分", ClassCName = "MEPTItemSplit", ShortCode = "MEPTItemSplit", Desc = "按项目拆分")]
    public class MEPTItemSplit : BaseEntity
	{
		#region Member Variables
		
        
        protected string _newBarCode;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _comment;
        protected DateTime? _dataUpdateTime;
        protected ItemAllItem _itemAllItem;
		protected ItemAllItem _par;
		protected MEPTSamplingGroup _mEPTSamplingGroup;

		#endregion

		#region Constructors

		public MEPTItemSplit() { }

		public MEPTItemSplit( long labID, string newBarCode, int dispOrder, bool isUse, string comment, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, ItemAllItem par, MEPTSamplingGroup mEPTSamplingGroup )
		{
			this._labID = labID;
			this._newBarCode = newBarCode;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._comment = comment;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
			this._par = par;
			this._mEPTSamplingGroup = mEPTSamplingGroup;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "新条码", ShortCode = "NewBarCode", Desc = "新条码", ContextType = SysDic.All, Length = 50)]
        public virtual string NewBarCode
		{
			get { return _newBarCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NewBarCode", value, value.ToString());
				_newBarCode = value;
			}
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "子项目ID", ShortCode = "ItemAllItem", Desc = "子项目ID", ContextType = SysDic.All)]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "原项目ID", ShortCode = "Par", Desc = "原项目ID", ContextType = SysDic.All)]
		public virtual ItemAllItem Par
		{
			get { return _par; }
			set { _par = value; }
		}

        [DataMember]
        [DataDesc(CName = "采样组设置", ShortCode = "MEPTSamplingGroup", Desc = "采样组设置", ContextType = SysDic.All)]
		public virtual MEPTSamplingGroup MEPTSamplingGroup
		{
			get { return _mEPTSamplingGroup; }
			set { _mEPTSamplingGroup = value; }
		}

		#endregion
	}
	#endregion
}