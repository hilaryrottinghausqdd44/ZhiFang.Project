using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagABOCheck

	/// <summary>
	/// BloodBagABOCheck object for NHibernate mapped table 'Blood_BagABOCheck'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBagABOCheck", ShortCode = "BloodBagABOCheck", Desc = "")]
	public class BloodBagABOCheck : BaseEntityServiceByString
    {
		#region Member Variables
		
        protected string _bloodInId;
        //protected string _bloodAboNo;
        //protected string _reBloodAboNo;
        protected string _reCheckID;
        protected DateTime? _reCheckTime;
        protected int _reCheckFlag;
        protected int _reCheckResult;
        protected DateTime? _scanTime;
        protected string _demo;
        protected string _lisAbo;
        protected string _lisRh;
        protected string _logDemo;
        //protected string _bloodUnitNo;
        protected string _b3code;
        protected string _pcode;
        protected string _bcount;
        protected string _bbagcode;
        protected string _bloodStrongFlag;
        protected string _bloodStrongTime;
        protected string _bloodStrongUserID;
        protected string _bloodStrongID;
        protected string _bloodStrongBloodAboNo;
        protected int _dispOrder;
        protected bool _visible;

		protected Bloodstyle _bloodstyle;
		protected BloodBUnit _bloodBUnit;
		protected BloodABO _bloodABO;
		protected BloodABO _reBloodABO;

		#endregion

		#region Constructors

		public BloodBagABOCheck() { }

		public BloodBagABOCheck(Bloodstyle bloodstyle, BloodBUnit bloodBUnit,BloodABO bloodABO, BloodABO reBloodABO, string bloodInId, string reCheckID, DateTime reCheckTime, int reCheckFlag, int reCheckResult, DateTime scanTime, string demo, string lisAbo, string lisRh, string logDemo,  string b3code, string pcode, string bcount, string bbagcode, string bloodStrongFlag, string bloodStrongTime, string bloodStrongUserID, string bloodStrongID, string bloodStrongBloodAboNo, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible )
		{
			this._bloodstyle = bloodstyle;
			this._bloodBUnit = bloodBUnit;
			this._bloodABO = bloodABO;
			this._reBloodABO = reBloodABO;

			this._bloodInId = bloodInId;
			this._reCheckID = reCheckID;
			this._reCheckTime = reCheckTime;
			this._reCheckFlag = reCheckFlag;
			this._reCheckResult = reCheckResult;
			this._scanTime = scanTime;
			this._demo = demo;
			this._lisAbo = lisAbo;
			this._lisRh = lisRh;
			this._logDemo = logDemo;
			this._b3code = b3code;
			this._pcode = pcode;
			this._bcount = bcount;
			this._bbagcode = bbagcode;
			this._bloodStrongFlag = bloodStrongFlag;
			this._bloodStrongTime = bloodStrongTime;
			this._bloodStrongUserID = bloodStrongUserID;
			this._bloodStrongID = bloodStrongID;
			this._bloodStrongBloodAboNo = bloodStrongBloodAboNo;
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._visible = visible;
		}

		#endregion

		#region Public Properties

		[DataMember]
		[DataDesc(CName = "Bloodstyle", ShortCode = "Bloodstyle", Desc = "Bloodstyle")]
		public virtual Bloodstyle Bloodstyle
		{
			get { return _bloodstyle; }
			set { _bloodstyle = value; }
		}
		[DataMember]
		[DataDesc(CName = "BloodBUnit", ShortCode = "BloodBUnit", Desc = "BloodBUnit")]
		public virtual BloodBUnit BloodBUnit
		{
			get { return _bloodBUnit; }
			set { _bloodBUnit = value; }
		}
		[DataMember]
		[DataDesc(CName = "BloodABO", ShortCode = "BloodABO", Desc = "BloodABO")]
		public virtual BloodABO BloodABO
		{
			get { return _bloodABO; }
			set { _bloodABO = value; }
		}
		[DataMember]
		[DataDesc(CName = "ReBloodABO", ShortCode = "ReBloodABO", Desc = "¸´ºËÑªÐÍ")]
		public virtual BloodABO ReBloodABO
		{
			get { return _reBloodABO; }
			set { _reBloodABO = value; }
		}

		[DataMember]
        [DataDesc(CName = "", ShortCode = "BloodInId", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodInId
		{
			get { return _bloodInId; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BloodInId", value, value.ToString());
				_bloodInId = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReCheckID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ReCheckID
		{
			get { return _reCheckID; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReCheckID", value, value.ToString());
				_reCheckID = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReCheckTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReCheckTime
		{
			get { return _reCheckTime; }
			set { _reCheckTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReCheckFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReCheckFlag
		{
			get { return _reCheckFlag; }
			set { _reCheckFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReCheckResult", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReCheckResult
		{
			get { return _reCheckResult; }
			set { _reCheckResult = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ScanTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ScanTime
		{
			get { return _scanTime; }
			set { _scanTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Demo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string Demo
		{
			get { return _demo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Demo", value, value.ToString());
				_demo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisAbo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisAbo
		{
			get { return _lisAbo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for LisAbo", value, value.ToString());
				_lisAbo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisRh", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisRh
		{
			get { return _lisRh; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for LisRh", value, value.ToString());
				_lisRh = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LogDemo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string LogDemo
		{
			get { return _logDemo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for LogDemo", value, value.ToString());
				_logDemo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "B3code", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string B3code
		{
			get { return _b3code; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for B3code", value, value.ToString());
				_b3code = value;
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
        [DataDesc(CName = "", ShortCode = "Bcount", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Bcount
		{
			get { return _bcount; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Bcount", value, value.ToString());
				_bcount = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bbagcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Bbagcode
		{
			get { return _bbagcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Bbagcode", value, value.ToString());
				_bbagcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodStrongFlag", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodStrongFlag
		{
			get { return _bloodStrongFlag; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BloodStrongFlag", value, value.ToString());
				_bloodStrongFlag = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodStrongTime", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodStrongTime
		{
			get { return _bloodStrongTime; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BloodStrongTime", value, value.ToString());
				_bloodStrongTime = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodStrongUserID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodStrongUserID
		{
			get { return _bloodStrongUserID; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BloodStrongUserID", value, value.ToString());
				_bloodStrongUserID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodStrongID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodStrongID
		{
			get { return _bloodStrongID; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BloodStrongID", value, value.ToString());
				_bloodStrongID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodStrongBloodAboNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodStrongBloodAboNo
		{
			get { return _bloodStrongBloodAboNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BloodStrongBloodAboNo", value, value.ToString());
				_bloodStrongBloodAboNo = value;
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