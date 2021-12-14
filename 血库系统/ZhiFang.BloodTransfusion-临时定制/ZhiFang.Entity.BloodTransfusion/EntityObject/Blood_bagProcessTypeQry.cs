using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodbagProcessTypeQry

	/// <summary>
	/// BloodbagProcessTypeQry object for NHibernate mapped table 'blood_bagProcessTypeQry'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodbagProcessTypeQry", ShortCode = "BloodbagProcessTypeQry", Desc = "")]
	public class BloodbagProcessTypeQry : BaseEntity
	{
		#region Member Variables
		
        protected string _pTNo;
        protected string _bloodno;
        protected string _cName;
        protected int _dispOrder;

		#endregion

		#region Constructors

		public BloodbagProcessTypeQry() { }

		public BloodbagProcessTypeQry( long labID, string pTNo, string bloodno, string cName, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._pTNo = pTNo;
			this._bloodno = bloodno;
			this._cName = cName;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "PTNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PTNo
		{
			get { return _pTNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PTNo", value, value.ToString());
				_pTNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Bloodno", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Bloodno
		{
			get { return _bloodno; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Bloodno", value, value.ToString());
				_bloodno = value;
			}
		}

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