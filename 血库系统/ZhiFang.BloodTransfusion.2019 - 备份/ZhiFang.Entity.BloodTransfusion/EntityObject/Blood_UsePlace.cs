using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodUsePlace

	/// <summary>
	/// BloodUsePlace object for NHibernate mapped table 'Blood_UsePlace'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodUsePlace", ShortCode = "BloodUsePlace", Desc = "")]
	public class BloodUsePlace : BaseEntityServiceByString
    {
		#region Member Variables
		
        protected string _usePlaceName;
        protected string _shortcode;
        protected int _disporder;
        protected bool _visible;

		#endregion

		#region Constructors

		public BloodUsePlace() { }

		public BloodUsePlace( string usePlaceName, string shortcode, int disporder, long labID, DateTime dataAddTime, byte[] dataTimeStamp, bool visible )
		{
			this._usePlaceName = usePlaceName;
			this._shortcode = shortcode;
			this._disporder = disporder;
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._visible = visible;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "UsePlaceName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UsePlaceName
		{
			get { return _usePlaceName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UsePlaceName", value, value.ToString());
				_usePlaceName = value;
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
        [DataDesc(CName = "", ShortCode = "Disporder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Disporder
		{
			get { return _disporder; }
			set { _disporder = value; }
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