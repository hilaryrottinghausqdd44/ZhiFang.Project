using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodABO

	/// <summary>
	/// BloodABO object for NHibernate mapped table 'Blood_ABO'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodABO", ShortCode = "BloodABO", Desc = "")]
	public class BloodABO : BaseEntityServiceByString
    {
		#region Member Variables
		
        protected string _cName;
        protected string _aBOCode;
        protected string _hisDispCode;
        protected string _aBOType;
        protected string _rHType;
        protected string _shortCode;
        protected int _dispOrder;
        protected string _visible;
        protected string _hisABOCode;
        protected string _hisrhCode;
        protected string _hisABOrhCode;
        protected string _lisABOCode;
        protected string _lisRhCode;
        protected string _lisABORhCode;
        protected string _hIsOrderCOde;
        protected string _demo;
        protected string _rhename;

		#endregion

		#region Constructors

		public BloodABO() { }

		public BloodABO( string bloodABOName, string aBOCode, string hisDispCode, string aBOType, string rHType, string shortCode, int dispOrder, string visible, string hisABOCode, string hisrhCode, string hisABOrhCode, string lisABOCode, string lisRhCode, string lisABORhCode, string hIsOrderCOde, string demo, string rhename, long labID, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._cName = bloodABOName;
			this._aBOCode = aBOCode;
			this._hisDispCode = hisDispCode;
			this._aBOType = aBOType;
			this._rHType = rHType;
			this._shortCode = shortCode;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._hisABOCode = hisABOCode;
			this._hisrhCode = hisrhCode;
			this._hisABOrhCode = hisABOrhCode;
			this._lisABOCode = lisABOCode;
			this._lisRhCode = lisRhCode;
			this._lisABORhCode = lisABORhCode;
			this._hIsOrderCOde = hIsOrderCOde;
			this._demo = demo;
			this._rhename = rhename;
			this._labID = labID;
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
        [DataDesc(CName = "", ShortCode = "ABOCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ABOCode
		{
			get { return _aBOCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ABOCode", value, value.ToString());
				_aBOCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDispCode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string HisDispCode
		{
			get { return _hisDispCode; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for HisDispCode", value, value.ToString());
				_hisDispCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ABOType", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ABOType
		{
			get { return _aBOType; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ABOType", value, value.ToString());
				_aBOType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RHType", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string RHType
		{
			get { return _rHType; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for RHType", value, value.ToString());
				_rHType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
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
        [DataDesc(CName = "", ShortCode = "HisABOCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisABOCode
		{
			get { return _hisABOCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisABOCode", value, value.ToString());
				_hisABOCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisrhCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisrhCode
		{
			get { return _hisrhCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisrhCode", value, value.ToString());
				_hisrhCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisABOrhCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisABOrhCode
		{
			get { return _hisABOrhCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisABOrhCode", value, value.ToString());
				_hisABOrhCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisABOCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisABOCode
		{
			get { return _lisABOCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for LisABOCode", value, value.ToString());
				_lisABOCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisRhCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisRhCode
		{
			get { return _lisRhCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for LisRhCode", value, value.ToString());
				_lisRhCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisABORhCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string LisABORhCode
		{
			get { return _lisABORhCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for LisABORhCode", value, value.ToString());
				_lisABORhCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HIsOrderCOde", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HIsOrderCOde
		{
			get { return _hIsOrderCOde; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HIsOrderCOde", value, value.ToString());
				_hIsOrderCOde = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Demo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Demo
		{
			get { return _demo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Demo", value, value.ToString());
				_demo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Rhename", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Rhename
		{
			get { return _rhename; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Rhename", value, value.ToString());
				_rhename = value;
			}
		}

        
		#endregion
	}
	#endregion
}