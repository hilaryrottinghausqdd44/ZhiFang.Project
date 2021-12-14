using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region BHospitalTypeLink

	/// <summary>
	/// BHospitalTypeLink object for NHibernate mapped table 'B_HospitalTypeLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BHospitalTypeLink", ShortCode = "BHospitalTypeLink", Desc = "")]
	public class BHospitalTypeLink : BaseEntityService//BaseEntity
	{
		#region Member Variables
		
        protected long? _hospitalID;
        protected string _hospitalName;
        protected long? _hTypeID;
        protected string _hTypeName;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public BHospitalTypeLink() { }

		public BHospitalTypeLink( long labID, long hospitalID, string hospitalName, long hTypeID, string hTypeName, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._hospitalID = hospitalID;
			this._hospitalName = hospitalName;
			this._hTypeID = hTypeID;
			this._hTypeName = hTypeName;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "HospitalID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? HospitalID
		{
			get { return _hospitalID; }
			set { _hospitalID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string HospitalName
		{
			get { return _hospitalName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for HospitalName", value, value.ToString());
				_hospitalName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "HTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? HTypeID
		{
			get { return _hTypeID; }
			set { _hTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HTypeName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HTypeName
		{
			get { return _hTypeName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HTypeName", value, value.ToString());
				_hTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		
		#endregion
	}
	#endregion
}