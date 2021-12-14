using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
	#region ReaBusinessInterface

	/// <summary>
	/// ReaBusinessInterface object for NHibernate mapped table 'Rea_BusinessInterface'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBusinessInterface", ShortCode = "ReaBusinessInterface", Desc = "")]
	public class ReaBusinessInterface : BaseEntity
	{
		#region Member Variables
		
        protected string _interfaceType;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _visible;
        protected string _cName;
        protected string _uRL;
        protected string _appKey;
        protected string _appPassword;
        protected string _reaServerLabcCode;

		#endregion

		#region Constructors

		public ReaBusinessInterface() { }

		public ReaBusinessInterface( long labID, string interfaceType, int dispOrder, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp, string zX1, string zX2, string zX3, int visible, string cName, string uRL, string appKey,string appPassword,  string reaServerLabcCode )
		{
			this._labID = labID;
			this._interfaceType = interfaceType;
			this._dispOrder = dispOrder;
			this._dataUpdateTime = dataUpdateTime;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._visible = visible;
			this._cName = cName;
			this._uRL = uRL;
			this._appKey = appKey;
			this._appPassword = appPassword;
			this._reaServerLabcCode = reaServerLabcCode;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "InterfaceType", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string InterfaceType
		{
			get { return _interfaceType; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for InterfaceType", value, value.ToString());
				_interfaceType = value;
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
        [DataDesc(CName = "", ShortCode = "URL", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string URL
		{
			get { return _uRL; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for URL", value, value.ToString());
				_uRL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AppKey", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string AppKey
		{
			get { return _appKey; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for AppKey", value, value.ToString());
				_appKey = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AppPassword", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string AppPassword
		{
			get { return _appPassword; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for AppPassword", value, value.ToString());
				_appPassword = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaServerLabcCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerLabcCode
		{
			get { return _reaServerLabcCode; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReaServerLabcCode", value, value.ToString());
				_reaServerLabcCode = value;
			}
		}
		
		#endregion
	}
	#endregion
}