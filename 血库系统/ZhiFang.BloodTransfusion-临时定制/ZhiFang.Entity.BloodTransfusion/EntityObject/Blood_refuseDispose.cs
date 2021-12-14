using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodrefuseDispose

	/// <summary>
	/// BloodrefuseDispose object for NHibernate mapped table 'Blood_refuseDispose'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodrefuseDispose", ShortCode = "BloodrefuseDispose", Desc = "")]
	public class BloodrefuseDispose : BaseEntityServiceByString
    {
		#region Member Variables
		
        protected string _refuseDispose;
        protected int _dispOrder;
        protected string _shortcode;
        protected bool _visible;

		#endregion

		#region Constructors

		public BloodrefuseDispose() { }

		public BloodrefuseDispose( string refuseDispose, int dispOrder, string shortcode, long labID, DateTime dataAddTime, byte[] dataTimeStamp, bool visible )
		{
			this._refuseDispose = refuseDispose;
			this._dispOrder = dispOrder;
			this._shortcode = shortcode;
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._visible = visible;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefuseDispose", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string RefuseDispose
		{
			get { return _refuseDispose; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RefuseDispose", value, value.ToString());
				_refuseDispose = value;
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