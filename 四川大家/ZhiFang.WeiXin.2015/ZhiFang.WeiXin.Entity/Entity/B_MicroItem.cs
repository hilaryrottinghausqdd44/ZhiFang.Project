using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BMicroItem

	/// <summary>
	/// BMicroItem object for NHibernate mapped table 'B_MicroItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BMicroItem", ShortCode = "BMicroItem", Desc = "")]
	public class BMicroItem : BaseEntity
	{
		#region Member Variables
		
        protected int _microTypeNo;
        protected string _cName;
        protected string _eName;
        protected string _shortName;
        protected string _shortCode;
        protected int _visible;
        protected int _dispOrder;
        protected string _microDesc;
        protected int _isMicro;

		#endregion

		#region Constructors

		public BMicroItem() { }

		public BMicroItem( int microTypeNo, string cName, string eName, string shortName, string shortCode, int visible, int dispOrder, string microDesc, int isMicro )
		{
			this._microTypeNo = microTypeNo;
			this._cName = cName;
			this._eName = eName;
			this._shortName = shortName;
			this._shortCode = shortCode;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._microDesc = microDesc;
			this._isMicro = isMicro;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "MicroTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MicroTypeNo
		{
			get { return _microTypeNo; }
			set { _microTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortName", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ShortName
		{
			get { return _shortName; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ShortName", value, value.ToString());
				_shortName = value;
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
        [DataDesc(CName = "", ShortCode = "MicroDesc", Desc = "", ContextType = SysDic.All, Length = 250)]
        public virtual string MicroDesc
		{
			get { return _microDesc; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for MicroDesc", value, value.ToString());
				_microDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsMicro", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsMicro
		{
			get { return _isMicro; }
			set { _isMicro = value; }
		}

        
		#endregion
	}
	#endregion
}