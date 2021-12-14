using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBarCodeRules

    /// <summary>
    /// ReaBarCodeRules object for NHibernate mapped table 'Rea_BmsSerial'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaBmsSerial", ShortCode = "ReaBmsSerial", Desc = "")]
	public class ReaBmsSerial : BaseEntity
	{
		#region Member Variables
		
        protected string _bmsType;
        protected DateTime? _dataUpdateTime;
        protected long _curBarCode;
		

		#endregion

		#region Constructors

		public ReaBmsSerial() { }

		public ReaBmsSerial( long labID, string bmsType, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp, long curBarCode )
		{
			this._labID = labID;
			this._bmsType = bmsType;
			this._dataUpdateTime = dataUpdateTime;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._curBarCode = curBarCode;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "BmsType", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string BmsType
		{
			get { return _bmsType; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for BmsType", value, value.ToString());
				_bmsType = value;
			}
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
        [DataDesc(CName = "", ShortCode = "CurBarCode", Desc = "", ContextType = SysDic.All, Length = 6)]
        public virtual long CurBarCode
		{
			get { return _curBarCode; }
			set { _curBarCode = value; }
		}

		
		#endregion
	}
	#endregion
}