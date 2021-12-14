using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
	#region ReaAlertInfoSettings

	/// <summary>
	/// ReaAlertInfoSettings object for NHibernate mapped table 'Rea_AlertInfoSettings'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaAlertInfoSettings", ShortCode = "ReaAlertInfoSettings", Desc = "")]
	public class ReaAlertInfoSettings : BaseEntity
	{
		#region Member Variables
		
        protected int _alertTypeId;
        protected string _alertTypeCName;
        protected string _alertColor;
        protected int _dispOrder;
        protected double _storeUpper;
        protected double _storeLower;
        protected int _visible;
        protected DateTime? _dataUpdateTime;
        protected string _memo;
		

		#endregion

		#region Constructors

		public ReaAlertInfoSettings() { }

		public ReaAlertInfoSettings( long labID, int alertTypeId, string alertTypeCName, string alertColor, int dispOrder, double storeUpper, double storeLower, int visible, DateTime dataUpdateTime, DateTime dataAddTime, string memo, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._alertTypeId = alertTypeId;
			this._alertTypeCName = alertTypeCName;
			this._alertColor = alertColor;
			this._dispOrder = dispOrder;
			this._storeUpper = storeUpper;
			this._storeLower = storeLower;
			this._visible = visible;
			this._dataUpdateTime = dataUpdateTime;
			this._dataAddTime = dataAddTime;
			this._memo = memo;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "AlertTypeId", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AlertTypeId
		{
			get { return _alertTypeId; }
			set { _alertTypeId = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AlertTypeCName", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string AlertTypeCName
		{
			get { return _alertTypeCName; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for AlertTypeCName", value, value.ToString());
				_alertTypeCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AlertColor", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string AlertColor
		{
			get { return _alertColor; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for AlertColor", value, value.ToString());
				_alertColor = value;
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
        [DataDesc(CName = "", ShortCode = "StoreUpper", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double StoreUpper
		{
			get { return _storeUpper; }
			set { _storeUpper = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StoreLower", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double StoreLower
		{
			get { return _storeLower; }
			set { _storeLower = value; }
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
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = int.MaxValue)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

		
		#endregion
	}
	#endregion
}