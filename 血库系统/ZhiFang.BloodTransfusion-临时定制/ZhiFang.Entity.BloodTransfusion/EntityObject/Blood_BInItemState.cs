using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBInItemState

	/// <summary>
	/// BloodBInItemState object for NHibernate mapped table 'Blood_BInItemState'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBInItemState", ShortCode = "BloodBInItemState", Desc = "")]
	public class BloodBInItemState : BaseEntityServiceByString
    {
		#region Member Variables
		
        protected string _bbagCode;
        protected string _pcode;
        protected string _invalidCode;
        protected string _bReaNo;
        protected DateTime? _reaDate;
        protected string _reasUserID;
        protected int _reaState;
        protected int _dispOrder;
        protected bool _visible;

		#endregion

		#region Constructors

		public BloodBInItemState() { }

		public BloodBInItemState( string bbagCode, string pcode, string invalidCode, string bReaNo, DateTime reaDate, string reasUserID, int reaState, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible )
		{
			this._bbagCode = bbagCode;
			this._pcode = pcode;
			this._invalidCode = invalidCode;
			this._bReaNo = bReaNo;
			this._reaDate = reaDate;
			this._reasUserID = reasUserID;
			this._reaState = reaState;
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._visible = visible;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "BbagCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BbagCode
		{
			get { return _bbagCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BbagCode", value, value.ToString());
				_bbagCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Pcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Pcode
		{
			get { return _pcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Pcode", value, value.ToString());
				_pcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InvalidCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string InvalidCode
		{
			get { return _invalidCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for InvalidCode", value, value.ToString());
				_invalidCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BReaNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BReaNo
		{
			get { return _bReaNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BReaNo", value, value.ToString());
				_bReaNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReaDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReaDate
		{
			get { return _reaDate; }
			set { _reaDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReasUserID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ReasUserID
		{
			get { return _reasUserID; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReasUserID", value, value.ToString());
				_reasUserID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaState", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReaState
		{
			get { return _reaState; }
			set { _reaState = value; }
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