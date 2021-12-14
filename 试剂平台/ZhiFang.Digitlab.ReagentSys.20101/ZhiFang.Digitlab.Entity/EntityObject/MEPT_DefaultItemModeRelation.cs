using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTDefaultItemModeRelation

	/// <summary>
	/// MEPTDefaultItemModeRelation object for NHibernate mapped table 'MEPT_DefaultItemModeRelation'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "默认模板项目关系", ClassCName = "MEPTDefaultItemModeRelation", ShortCode = "MEPTDefaultItemModeRelation", Desc = "默认模板项目关系")]
	public class MEPTDefaultItemModeRelation : BaseEntity
	{
		#region Member Variables
		
        
        protected bool _isUse;
        protected ItemAllItem _itemAllItem;
		protected MEPTDefaultItemMode _mEPTDefaultItemMode;

		#endregion

		#region Constructors

		public MEPTDefaultItemModeRelation() { }

		public MEPTDefaultItemModeRelation( long labID, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, MEPTDefaultItemMode mEPTDefaultItemMode )
		{
			this._labID = labID;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
			this._mEPTDefaultItemMode = mEPTDefaultItemMode;
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
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目", ContextType = SysDic.All)]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "默认项目模板", ShortCode = "MEPTDefaultItemMode", Desc = "默认项目模板", ContextType = SysDic.All)]
		public virtual MEPTDefaultItemMode MEPTDefaultItemMode
		{
			get { return _mEPTDefaultItemMode; }
			set { _mEPTDefaultItemMode = value; }
		}

		#endregion
	}
	#endregion
}