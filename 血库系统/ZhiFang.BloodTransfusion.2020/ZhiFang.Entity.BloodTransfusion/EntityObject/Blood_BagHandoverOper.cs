using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagHandoverOper

	/// <summary>
	/// BloodBagHandoverOper object for NHibernate mapped table 'Blood_BagHandoverOper'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "交接记录主单表", ClassCName = "BloodBagHandoverOper", ShortCode = "BloodBagHandoverOper", Desc = "交接记录主单表")]
	public class BloodBagHandoverOper : BaseEntity
	{
		#region Member Variables
		
        protected string _bagOperationNo;
        protected long? _bagOperTypeID;
        protected long? _bagOperResultID;
        protected long? _deptID;
        protected string _deptCName;
        protected long? _bagOperID;
        protected string _bagOper;
        protected DateTime? _bagOperTime;
        protected long? _carrierID;
        protected string _carrier;
        protected bool _visible;
        protected DateTime? _dataUpdateTime;
		protected BloodBOutForm _bloodBOutForm;
		protected BloodBOutItem _bloodBOutItem;
		protected BloodBReqForm _bloodBReqForm;
		protected BloodQtyDtl _bloodQtyDtl;
		protected BloodStyle _bloodStyle;

		#endregion

		#region Constructors

		public BloodBagHandoverOper() { }

		public BloodBagHandoverOper( long labID, string bagOperationNo, long bagOperTypeID, long bagOperResultID, long deptID, string deptCName, long bagOperID, string bagOper, DateTime bagOperTime, long carrierID, string carrier, bool visible, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BloodBOutForm bloodBOutForm, BloodBOutItem bloodBOutItem, BloodBReqForm bloodBReqForm, BloodQtyDtl bloodQtyDtl, BloodStyle bloodStyle )
		{
			this._labID = labID;
			this._bagOperationNo = bagOperationNo;
			this._bagOperTypeID = bagOperTypeID;
			this._bagOperResultID = bagOperResultID;
			this._deptID = deptID;
			this._deptCName = deptCName;
			this._bagOperID = bagOperID;
			this._bagOper = bagOper;
			this._bagOperTime = bagOperTime;
			this._carrierID = carrierID;
			this._carrier = carrier;
			this._visible = visible;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBOutForm = bloodBOutForm;
			this._bloodBOutItem = bloodBOutItem;
			this._bloodBReqForm = bloodBReqForm;
			this._bloodQtyDtl = bloodQtyDtl;
			this._bloodStyle = bloodStyle;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "交接记录单号", ShortCode = "BagOperationNo", Desc = "交接记录单号", ContextType = SysDic.All, Length = 20)]
        public virtual string BagOperationNo
		{
			get { return _bagOperationNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BagOperationNo", value, value.ToString());
				_bagOperationNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作类型ID", ShortCode = "BagOperTypeID", Desc = "操作类型ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BagOperTypeID
		{
			get { return _bagOperTypeID; }
			set { _bagOperTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "交接记录结果ID", ShortCode = "BagOperResultID", Desc = "交接记录结果ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BagOperResultID
		{
			get { return _bagOperResultID; }
			set { _bagOperResultID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "所属科室Id", ShortCode = "DeptID", Desc = "所属科室Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

        [DataMember]
        [DataDesc(CName = "所属科室", ShortCode = "DeptCName", Desc = "所属科室", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptCName
		{
			get { return _deptCName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeptCName", value, value.ToString());
				_deptCName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作者ID", ShortCode = "BagOperID", Desc = "操作者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BagOperID
		{
			get { return _bagOperID; }
			set { _bagOperID = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作人姓名", ShortCode = "BagOper", Desc = "操作人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string BagOper
		{
			get { return _bagOper; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BagOper", value, value.ToString());
				_bagOper = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作时间", ShortCode = "BagOperTime", Desc = "操作时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BagOperTime
		{
			get { return _bagOperTime; }
			set { _bagOperTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "运送人ID", ShortCode = "CarrierID", Desc = "运送人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? CarrierID
		{
			get { return _carrierID; }
			set { _carrierID = value; }
		}

        [DataMember]
        [DataDesc(CName = "运送人姓名", ShortCode = "Carrier", Desc = "运送人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string Carrier
		{
			get { return _carrier; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Carrier", value, value.ToString());
				_carrier = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否启用", ShortCode = "Visible", Desc = "是否启用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据数据修改时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "发血主单表", ShortCode = "BloodBOutForm", Desc = "发血主单表")]
		public virtual BloodBOutForm BloodBOutForm
		{
			get { return _bloodBOutForm; }
			set { _bloodBOutForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "发血明细表", ShortCode = "BloodBOutItem", Desc = "发血明细表")]
		public virtual BloodBOutItem BloodBOutItem
		{
			get { return _bloodBOutItem; }
			set { _bloodBOutItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请主单表", ShortCode = "BloodBReqForm", Desc = "用血申请主单表")]
		public virtual BloodBReqForm BloodBReqForm
		{
			get { return _bloodBReqForm; }
			set { _bloodBReqForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "库存表", ShortCode = "BloodQtyDtl", Desc = "库存表")]
		public virtual BloodQtyDtl BloodQtyDtl
		{
			get { return _bloodQtyDtl; }
			set { _bloodQtyDtl = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

        
		#endregion
	}
	#endregion
}