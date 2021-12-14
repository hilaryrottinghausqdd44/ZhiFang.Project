using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodChargeGroupItem

	/// <summary>
	/// BloodChargeGroupItem object for NHibernate mapped table 'Blood_ChargeGroupItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "收费组合项目关系表", ClassCName = "BloodChargeGroupItem", ShortCode = "BloodChargeGroupItem", Desc = "收费组合项目关系表")]
	public class BloodChargeGroupItem : BaseEntity
	{
		#region Member Variables
		
        protected int _dispOrder;
        protected bool _isUse;
		protected BloodChargeItem _bloodChargeItem;
		protected BloodChargeItem _gBloodChargeItem;

		#endregion

		#region Constructors

		public BloodChargeGroupItem() { }

		public BloodChargeGroupItem( long labID, int dispOrder, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, BloodChargeItem bloodChargeItem, BloodChargeItem g )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodChargeItem = bloodChargeItem;
			this._gBloodChargeItem = g;
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
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "子费用项目", ShortCode = "BloodChargeItem", Desc = "子费用项目")]
		public virtual BloodChargeItem BloodChargeItem
		{
			get { return _bloodChargeItem; }
			set { _bloodChargeItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "组合费用项目", ShortCode = "G", Desc = "组合费用项目")]
		public virtual BloodChargeItem GBloodChargeItem
		{
			get { return _gBloodChargeItem; }
			set { _gBloodChargeItem = value; }
		}
        
		#endregion
	}
	#endregion
}