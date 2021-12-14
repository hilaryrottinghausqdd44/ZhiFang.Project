using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBUnit

	/// <summary>
	/// BloodBUnit object for NHibernate mapped table 'Blood_BUnit'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBUnit", ShortCode = "BloodBUnit", Desc = "")]
	public class BloodBUnit : BaseEntityServiceByInt
    {
		#region Member Variables
		
        protected string _bUnitName;
        protected string _eUnitName;
        protected string _shortcode;
        protected int _dispOrder;

		protected bool _visible;
		protected string _sName;
		protected string _pinYinZiTou;
		#endregion

		#region Constructors

		public BloodBUnit() { }

		public BloodBUnit( string bUnitName, string eUnitName, string shortcode, int dispOrder, bool visible )
		{
			this._visible = visible;
			this._bUnitName = bUnitName;
			this._eUnitName = eUnitName;
			this._shortcode = shortcode;
			this._dispOrder = dispOrder;
		}

		#endregion

		#region Public Properties
		[DataMember]
		[DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}
		[DataMember]
		[DataDesc(CName = "ºº×ÖÆ´Òô×ÖÍ·", ShortCode = "PinYinZiTou", Desc = "ºº×ÖÆ´Òô×ÖÍ·", ContextType = SysDic.All, Length = 50)]
		public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}
		[DataMember]
		[DataDesc(CName = "¼ò³Æ", ShortCode = "SName", Desc = "¼ò³Æ", ContextType = SysDic.All, Length = 50)]
		public virtual string SName
		{
			get { return _sName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}
		[DataMember]
        [DataDesc(CName = "", ShortCode = "BUnitName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BUnitName
		{
			get { return _bUnitName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BUnitName", value, value.ToString());
				_bUnitName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EUnitName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EUnitName
		{
			get { return _eUnitName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EUnitName", value, value.ToString());
				_eUnitName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
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