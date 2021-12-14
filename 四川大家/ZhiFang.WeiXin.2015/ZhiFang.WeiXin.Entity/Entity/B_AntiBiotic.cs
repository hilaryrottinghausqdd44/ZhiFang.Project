using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BAntiBiotic

	/// <summary>
	/// BAntiBiotic object for NHibernate mapped table 'B_AntiBiotic'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BAntiBiotic", ShortCode = "BAntiBiotic", Desc = "")]
	public class BAntiBiotic : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _shortName;
        protected string _shortCode;
        protected string _antiUnit;
        protected string _antiNote;
        protected int _visible;
        protected int _dispOrder;
        protected int _antiTypeNo;

		#endregion

		#region Constructors

		public BAntiBiotic() { }

		public BAntiBiotic( string cName, string eName, string shortName, string shortCode, string antiUnit, string antiNote, int visible, int dispOrder, int antiTypeNo )
		{
			this._cName = cName;
			this._eName = eName;
			this._shortName = shortName;
			this._shortCode = shortCode;
			this._antiUnit = antiUnit;
			this._antiNote = antiNote;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._antiTypeNo = antiTypeNo;
		}

		#endregion

		#region Public Properties


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
        [DataDesc(CName = "", ShortCode = "AntiUnit", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string AntiUnit
		{
			get { return _antiUnit; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for AntiUnit", value, value.ToString());
				_antiUnit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AntiNote", Desc = "", ContextType = SysDic.All, Length = 250)]
        public virtual string AntiNote
		{
			get { return _antiNote; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for AntiNote", value, value.ToString());
				_antiNote = value;
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
        [DataDesc(CName = "", ShortCode = "AntiTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AntiTypeNo
		{
			get { return _antiTypeNo; }
			set { _antiTypeNo = value; }
		}

        
		#endregion
	}
	#endregion
}