using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagProcess

	/// <summary>
	/// BloodBagProcess object for NHibernate mapped table 'Blood_BagProcess'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "血袋加工记录表", ClassCName = "BloodBagProcess", ShortCode = "BloodBagProcess", Desc = "血袋加工记录表")]
	public class BloodBagProcess : BaseEntity
	{
		#region Member Variables
		
        protected int _bPflag;
        protected bool _visible;
        protected int _dispOrder;
		protected BloodBagProcessType _bloodBagProcessType;
		protected BloodBPreForm _bloodBPreForm;
		protected BloodBPreItem _bloodBPreItem;
		protected BloodQtyDtl _bloodQtyDtl;

		#endregion

		#region Constructors

		public BloodBagProcess() { }

		public BloodBagProcess( long labID, int bPflag, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBagProcessType bloodBagProcessType, BloodBPreForm bloodBPreForm, BloodBPreItem bloodBPreItem, BloodQtyDtl bloodQtyDtl )
		{
			this._labID = labID;
			this._bPflag = bPflag;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBagProcessType = bloodBagProcessType;
			this._bloodBPreForm = bloodBPreForm;
			this._bloodBPreItem = bloodBPreItem;
			this._bloodQtyDtl = bloodQtyDtl;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "接口标志", ShortCode = "BPflag", Desc = "接口标志", ContextType = SysDic.All, Length = 4)]
        public virtual int BPflag
		{
			get { return _bPflag; }
			set { _bPflag = value; }
		}

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
        [DataDesc(CName = "加工类型表", ShortCode = "BloodBagProcessType", Desc = "加工类型表")]
		public virtual BloodBagProcessType BloodBagProcessType
		{
			get { return _bloodBagProcessType; }
			set { _bloodBagProcessType = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血主单", ShortCode = "BloodBPreForm", Desc = "配血主单")]
		public virtual BloodBPreForm BloodBPreForm
		{
			get { return _bloodBPreForm; }
			set { _bloodBPreForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血明细表", ShortCode = "BloodBPreItem", Desc = "配血明细表")]
		public virtual BloodBPreItem BloodBPreItem
		{
			get { return _bloodBPreItem; }
			set { _bloodBPreItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "库存表", ShortCode = "BloodQtyDtl", Desc = "库存表")]
		public virtual BloodQtyDtl BloodQtyDtl
		{
			get { return _bloodQtyDtl; }
			set { _bloodQtyDtl = value; }
		}

        
		#endregion
	}
	#endregion
}