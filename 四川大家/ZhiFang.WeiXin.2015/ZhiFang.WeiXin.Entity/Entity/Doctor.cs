using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region Doctor

	/// <summary>
	/// Doctor object for NHibernate mapped table 'Doctor'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "Doctor", ShortCode = "Doctor", Desc = "")]
	public class Doctor : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _shortCode;
        protected string _hisOrderCode;
        protected int _visible;
        //protected byte[] _dTimeStampe;
		

		#endregion

		#region Constructors

		public Doctor() { }

		public Doctor( string cName, string shortCode, string hisOrderCode, int visible, byte[] dTimeStampe, DateTime dataAddTime )
		{
			this._cName = cName;
			this._shortCode = shortCode;
			this._hisOrderCode = hisOrderCode;
			this._visible = visible;
			//this._dTimeStampe = dTimeStampe;
			this._dataAddTime = dataAddTime;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
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
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

  //      [DataMember]
  //      [DataDesc(CName = "", ShortCode = "DTimeStampe", Desc = "", ContextType = SysDic.All, Length = 8)]
  //      public virtual byte[] DTimeStampe
		//{
		//	get { return _dTimeStampe; }
		//	set { _dTimeStampe = value; }
		//}

		
		#endregion
	}
	#endregion
}