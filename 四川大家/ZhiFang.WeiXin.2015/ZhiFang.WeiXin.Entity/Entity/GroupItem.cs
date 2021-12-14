using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region GroupItem

	/// <summary>
	/// GroupItem object for NHibernate mapped table 'GroupItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "GroupItem", ShortCode = "GroupItem", Desc = "")]
	public class GroupItem : BaseEntity
	{
		#region Member Variables
		
        protected long _pItemNo;
        protected long _itemNo;
        protected bool _isUse;
		

		#endregion

		#region Constructors

		public GroupItem() { }

		public GroupItem( int pItemNo, int itemNo, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._pItemNo = pItemNo;
			this._itemNo = itemNo;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "PItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual long PItemNo
		{
			get { return _pItemNo; }
			set { _pItemNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual long ItemNo
		{
			get { return _itemNo; }
			set { _itemNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		
		#endregion
	}
	#endregion
}