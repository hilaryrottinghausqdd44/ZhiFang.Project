using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodLargeUseItem

	/// <summary>
	/// BloodLargeUseItem object for NHibernate mapped table 'Blood_LargeUseItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "大量用血申请记录表", ClassCName = "BloodLargeUseItem", ShortCode = "BloodLargeUseItem", Desc = "大量用血申请记录表")]
	public class BloodLargeUseItem : BaseEntity
	{
		#region Member Variables
		
        protected string _lUIMemo;
        protected bool _visible;
        protected int _dispOrder;
		protected BloodBReqForm _lUFBloodBReqForm;
		protected BloodBReqForm _bloodBReqForm;

		#endregion

		#region Constructors

		public BloodLargeUseItem() { }

		public BloodLargeUseItem( long labID, string lUIMemo, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBReqForm lUF, BloodBReqForm bloodBReqForm )
		{
			this._labID = labID;
			this._lUIMemo = lUIMemo;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lUFBloodBReqForm = lUF;
			this._bloodBReqForm = bloodBReqForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "LUIMemo", Desc = "备注", ContextType = SysDic.All, Length = 50)]
        public virtual string LUIMemo
		{
			get { return _lUIMemo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LUIMemo", value, value.ToString());
				_lUIMemo = value;
			}
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
        [DataDesc(CName = "本次大量用血计算的申请", ShortCode = "LUFBloodBReqForm", Desc = "本次大量用血计算的申请")]
		public virtual BloodBReqForm LUFBloodBReqForm
		{
			get { return _lUFBloodBReqForm; }
			set { _lUFBloodBReqForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "参与大量用血计算的申请单", ShortCode = "BloodBReqForm", Desc = "参与大量用血计算的申请单")]
		public virtual BloodBReqForm BloodBReqForm
		{
			get { return _bloodBReqForm; }
			set { _bloodBReqForm = value; }
		}

        
		#endregion
	}
	#endregion
}