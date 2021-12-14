using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagProcessType

	/// <summary>
	/// BloodBagProcessType object for NHibernate mapped table 'blood_BagProcessType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBagProcessType", ShortCode = "BloodBagProcessType", Desc = "")]
	public class BloodBagProcessType : BaseEntityServiceByString
    {
		#region Member Variables
		
        protected string _cName;
        protected string _shortCode;
        protected string _hisOrderCode;
        protected string _visible;
        protected string _bcno;
        protected string _chargeItemNo;
        protected int _dispOrder;

		#endregion

		#region Constructors

		public BloodBagProcessType() { }

		public BloodBagProcessType( long labID, string cName, string shortCode, string hisOrderCode, string visible, string bcno, string chargeItemNo, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._cName = cName;
			this._shortCode = shortCode;
			this._hisOrderCode = hisOrderCode;
			this._visible = visible;
			this._bcno = bcno;
			this._chargeItemNo = chargeItemNo;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisOrderCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Visible
		{
			get { return _visible; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Visible", value, value.ToString());
				_visible = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bcno", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Bcno
		{
			get { return _bcno; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Bcno", value, value.ToString());
				_bcno = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeItemNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ChargeItemNo
		{
			get { return _chargeItemNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ChargeItemNo", value, value.ToString());
				_chargeItemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        
		#endregion
	}
	#endregion
}