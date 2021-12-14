using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodReason

	/// <summary>
	/// BloodReason object for NHibernate mapped table 'Blood_Reason'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodReason", ShortCode = "BloodReason", Desc = "")]
	public class BloodReason : BaseEntityServiceByString
    {
		#region Member Variables
		
        protected string _bReaName;
        protected int _dispOrder;

		#endregion

		#region Constructors

		public BloodReason() { }

		public BloodReason( string bReaName, int dispOrder )
		{
			this._bReaName = bReaName;
			this._dispOrder = dispOrder;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "BReaName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BReaName
		{
			get { return _bReaName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BReaName", value, value.ToString());
				_bReaName = value;
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