using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodReceiItem

	/// <summary>
	/// BloodReceiItem object for NHibernate mapped table 'Blood_ReceiItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodReceiItem", ShortCode = "BloodReceiItem", Desc = "")]
	public class BloodReceiItem : BaseEntityServiceByString
    {
		#region Member Variables
		
        protected string _itemName;
        protected int _dispOrder;
        protected bool _visible;

		#endregion

		#region Constructors

		public BloodReceiItem() { }

		public BloodReceiItem( string itemName, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible )
		{
			this._itemName = itemName;
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._visible = visible;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemName
		{
			get { return _itemName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ItemName", value, value.ToString());
				_itemName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        
		#endregion
	}
	#endregion
}