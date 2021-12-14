using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagABOCheckLisItem

	/// <summary>
	/// BloodBagABOCheckLisItem object for NHibernate mapped table 'Blood_BagABOCheck_LisItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBagABOCheckLisItem", ShortCode = "BloodBagABOCheckLisItem", Desc = "")]
	public class BloodBagABOCheckLisItem : BaseEntity
	{
		#region Member Variables
		
        protected string _barcode;
        protected string _itemNo;
        protected string _itemName;
        protected string _reportDesc;
        protected string _checkDateTime;
        protected int _dispOrder;
        protected bool _visible;

		#endregion

		#region Constructors

		public BloodBagABOCheckLisItem() { }

		public BloodBagABOCheckLisItem( string barcode, string itemNo, string itemName, string reportDesc, string checkDateTime, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible )
		{
			this._barcode = barcode;
			this._itemNo = itemNo;
			this._itemName = itemName;
			this._reportDesc = reportDesc;
			this._checkDateTime = checkDateTime;
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._visible = visible;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "Barcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Barcode
		{
			get { return _barcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Barcode", value, value.ToString());
				_barcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ItemNo
		{
			get { return _itemNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ItemNo", value, value.ToString());
				_itemNo = value;
			}
		}

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
        [DataDesc(CName = "", ShortCode = "ReportDesc", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ReportDesc
		{
			get { return _reportDesc; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReportDesc", value, value.ToString());
				_reportDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckDateTime", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CheckDateTime
		{
			get { return _checkDateTime; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CheckDateTime", value, value.ToString());
				_checkDateTime = value;
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