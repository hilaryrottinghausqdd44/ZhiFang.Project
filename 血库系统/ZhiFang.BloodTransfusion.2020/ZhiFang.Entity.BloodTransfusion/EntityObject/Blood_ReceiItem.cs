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
    [DataDesc(CName = "送达明细", ClassCName = "BloodReceiItem", ShortCode = "BloodReceiItem", Desc = "送达明细")]
	public class BloodReceiItem : BaseEntity
	{
		#region Member Variables
		
        protected bool _visible;
        protected int _dispOrder;
		protected BloodBTestItem _bloodBTestItem;
		protected BloodRecei _bloodRecei;

		#endregion

		#region Constructors

		public BloodReceiItem() { }

		public BloodReceiItem( long labID, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBTestItem bloodBTestItem, BloodRecei bloodRecei )
		{
			this._labID = labID;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBTestItem = bloodBTestItem;
			this._bloodRecei = bloodRecei;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "检验项目表 ", ShortCode = "BloodBTestItem", Desc = "检验项目表 ")]
		public virtual BloodBTestItem BloodBTestItem
		{
			get { return _bloodBTestItem; }
			set { _bloodBTestItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "送达主单", ShortCode = "BloodRecei", Desc = "送达主单")]
		public virtual BloodRecei BloodRecei
		{
			get { return _bloodRecei; }
			set { _bloodRecei = value; }
		}

        
		#endregion
	}
	#endregion
}