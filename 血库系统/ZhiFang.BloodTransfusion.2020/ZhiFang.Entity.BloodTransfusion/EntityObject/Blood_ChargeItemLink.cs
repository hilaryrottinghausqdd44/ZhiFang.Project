using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodChargeItemLink

	/// <summary>
	/// BloodChargeItemLink object for NHibernate mapped table 'Blood_ChargeItemLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "组合项目费用关系表", ClassCName = "BloodChargeItemLink", ShortCode = "BloodChargeItemLink", Desc = "组合项目费用关系表")]
	public class BloodChargeItemLink : BaseEntity
	{
		#region Member Variables
		
        protected double _modulus;
        protected int _dispOrder;
        protected bool _isUse;
		protected BDict _chargeType;
		protected BloodChargeItem _bloodChargeItem;
		
		#endregion

		#region Constructors

		public BloodChargeItemLink() { }

		public BloodChargeItemLink( long labID, double modulus, int dispOrder, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, BDict chargeTypeId, BloodChargeItem bloodChargeItem )
		{
			this._labID = labID;
			this._modulus = modulus;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._chargeType = chargeTypeId;
			this._bloodChargeItem = bloodChargeItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用系数", ShortCode = "Modulus", Desc = "费用系数", ContextType = SysDic.All, Length = 8)]
        public virtual double Modulus
		{
			get { return _modulus; }
			set { _modulus = value; }
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
        [DataDesc(CName = "费用类型", ShortCode = "ChargeType", Desc = "费用类型")]
		public virtual BDict ChargeType
		{
			get { return _chargeType; }
			set { _chargeType = value; }
		}

        [DataMember]
        [DataDesc(CName = "费用项目表", ShortCode = "BloodChargeItem", Desc = "费用项目表")]
		public virtual BloodChargeItem BloodChargeItem
		{
			get { return _bloodChargeItem; }
			set { _bloodChargeItem = value; }
		}

        
		#endregion
	}
	#endregion
}