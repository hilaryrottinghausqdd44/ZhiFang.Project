using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
	#region ReaBusinessInterfaceLink

	/// <summary>
	/// ReaBusinessInterfaceLink object for NHibernate mapped table 'Rea_BusinessInterfaceLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBusinessInterfaceLink", ShortCode = "ReaBusinessInterfaceLink", Desc = "")]
	public class ReaBusinessInterfaceLink : BaseEntity
	{
		#region Member Variables
		
        protected long _businessId;
        protected string _businessCName;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _visible;
        protected long _businessTypeId;
        protected string _businessType;
        protected long _compID;
        protected string _companyName;
        protected string _reaServerCompCode;
        protected string _reaCompCode;
		

		#endregion

		#region Constructors

		public ReaBusinessInterfaceLink() { }

		public ReaBusinessInterfaceLink( long labID, long businessId, string businessCName, int dispOrder, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp, string zX1, string zX2, string zX3, int visible, long businessTypeId, string businessType, long compID, string companyName, string reaServerCompCode, string reaCompCode )
		{
			this._labID = labID;
			this._businessId = businessId;
			this._businessCName = businessCName;
			this._dispOrder = dispOrder;
			this._dataUpdateTime = dataUpdateTime;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._visible = visible;
			this._businessTypeId = businessTypeId;
			this._businessType = businessType;
			this._compID = compID;
			this._companyName = companyName;
			this._reaServerCompCode = reaServerCompCode;
			this._reaCompCode = reaCompCode;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BusinessId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long BusinessId
		{
			get { return _businessId; }
			set { _businessId = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BusinessCName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string BusinessCName
		{
			get { return _businessCName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for BusinessCName", value, value.ToString());
				_businessCName = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
				_zX1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
				_zX2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
				_zX3 = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BusinessTypeId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long BusinessTypeId
		{
			get { return _businessTypeId; }
			set { _businessTypeId = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BusinessType", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string BusinessType
		{
			get { return _businessType; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for BusinessType", value, value.ToString());
				_businessType = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CompID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CompID
		{
			get { return _compID; }
			set { _compID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompanyName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
		{
			get { return _companyName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for CompanyName", value, value.ToString());
				_companyName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaServerCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerCompCode
		{
			get { return _reaServerCompCode; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReaServerCompCode", value, value.ToString());
				_reaServerCompCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompCode
		{
			get { return _reaCompCode; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReaCompCode", value, value.ToString());
				_reaCompCode = value;
			}
		}

		
		#endregion
	}
	#endregion
}