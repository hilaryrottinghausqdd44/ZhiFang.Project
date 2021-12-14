using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region NPUser

	/// <summary>
	/// NPUser object for NHibernate mapped table 'NPUser'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "NPUser", ShortCode = "NPUser", Desc = "")]
	public class NPUser : BaseEntityServiceByInt
    {
		#region Member Variables
		
        protected string _cName;
        protected string _shortCode;
        protected string _passWord;
        protected int _deptNo;
        protected int _districtNo;
        protected string _role;
        protected int _visible;
        protected int _dispOrder;
        protected string _refSectionNo;
        protected string _sSectionNo;
        protected string _refSikeTypeNo;
        protected string _sSikeTypeNo;
        protected string _refDeftNo;
        protected string _sDeftNo;
        protected string _refDoctor;
        protected string _sDoctor;
        protected string _refDistrictNo;
        protected string _sDistrictNo;
        protected string _refWardNo;
        protected string _sWardNo;
        protected string _refClientNo;
        protected string _sClientNo;
        protected string _hisOrderCode;
        protected string _telephone;
        protected string _userDataRights;

		#endregion

		#region Constructors

		public NPUser() { }

		public NPUser( string cName, string shortCode, string passWord, int deptNo, int districtNo, string role, int visible, int dispOrder, string refSectionNo, string sSectionNo, string refSikeTypeNo, string sSikeTypeNo, string refDeftNo, string sDeftNo, string refDoctor, string sDoctor, string refDistrictNo, string sDistrictNo, string refWardNo, string sWardNo, string refClientNo, string sClientNo, string hisOrderCode, string telephone, string userDataRights )
		{
			this._cName = cName;
			this._shortCode = shortCode;
			this._passWord = passWord;
			this._deptNo = deptNo;
			this._districtNo = districtNo;
			this._role = role;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._refSectionNo = refSectionNo;
			this._sSectionNo = sSectionNo;
			this._refSikeTypeNo = refSikeTypeNo;
			this._sSikeTypeNo = sSikeTypeNo;
			this._refDeftNo = refDeftNo;
			this._sDeftNo = sDeftNo;
			this._refDoctor = refDoctor;
			this._sDoctor = sDoctor;
			this._refDistrictNo = refDistrictNo;
			this._sDistrictNo = sDistrictNo;
			this._refWardNo = refWardNo;
			this._sWardNo = sWardNo;
			this._refClientNo = refClientNo;
			this._sClientNo = sClientNo;
			this._hisOrderCode = hisOrderCode;
			this._telephone = telephone;
			this._userDataRights = userDataRights;
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
        [DataDesc(CName = "", ShortCode = "PassWord", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PassWord
		{
			get { return _passWord; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PassWord", value, value.ToString());
				_passWord = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DeptNo
		{
			get { return _deptNo; }
			set { _deptNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DistrictNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DistrictNo
		{
			get { return _districtNo; }
			set { _districtNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Role", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Role
		{
			get { return _role; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Role", value, value.ToString());
				_role = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefSectionNo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string RefSectionNo
		{
			get { return _refSectionNo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for RefSectionNo", value, value.ToString());
				_refSectionNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SSectionNo", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string SSectionNo
		{
			get { return _sSectionNo; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for SSectionNo", value, value.ToString());
				_sSectionNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefSikeTypeNo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string RefSikeTypeNo
		{
			get { return _refSikeTypeNo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for RefSikeTypeNo", value, value.ToString());
				_refSikeTypeNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SSikeTypeNo", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string SSikeTypeNo
		{
			get { return _sSikeTypeNo; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for SSikeTypeNo", value, value.ToString());
				_sSikeTypeNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefDeftNo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string RefDeftNo
		{
			get { return _refDeftNo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for RefDeftNo", value, value.ToString());
				_refDeftNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SDeftNo", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string SDeftNo
		{
			get { return _sDeftNo; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for SDeftNo", value, value.ToString());
				_sDeftNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefDoctor", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string RefDoctor
		{
			get { return _refDoctor; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for RefDoctor", value, value.ToString());
				_refDoctor = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SDoctor", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string SDoctor
		{
			get { return _sDoctor; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for SDoctor", value, value.ToString());
				_sDoctor = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefDistrictNo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string RefDistrictNo
		{
			get { return _refDistrictNo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for RefDistrictNo", value, value.ToString());
				_refDistrictNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SDistrictNo", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string SDistrictNo
		{
			get { return _sDistrictNo; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for SDistrictNo", value, value.ToString());
				_sDistrictNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefWardNo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string RefWardNo
		{
			get { return _refWardNo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for RefWardNo", value, value.ToString());
				_refWardNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SWardNo", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string SWardNo
		{
			get { return _sWardNo; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for SWardNo", value, value.ToString());
				_sWardNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefClientNo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string RefClientNo
		{
			get { return _refClientNo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for RefClientNo", value, value.ToString());
				_refClientNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SClientNo", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string SClientNo
		{
			get { return _sClientNo; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for SClientNo", value, value.ToString());
				_sClientNo = value;
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
        [DataDesc(CName = "", ShortCode = "Telephone", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Telephone
		{
			get { return _telephone; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Telephone", value, value.ToString());
				_telephone = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserDataRights", Desc = "", ContextType = SysDic.All, Length = 2000)]
        public virtual string UserDataRights
		{
			get { return _userDataRights; }
			set
			{
				if ( value != null && value.Length > 2000)
					throw new ArgumentOutOfRangeException("Invalid value for UserDataRights", value, value.ToString());
				_userDataRights = value;
			}
		}

        
		#endregion
	}
	#endregion
}