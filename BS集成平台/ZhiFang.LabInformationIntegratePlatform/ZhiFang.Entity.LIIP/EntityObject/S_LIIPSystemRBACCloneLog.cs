using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region SLIIPSystemRBACCloneLog

	/// <summary>
	/// SLIIPSystemRBACCloneLog object for NHibernate mapped table 'S_LIIPSystemRBACCloneLog'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SLIIPSystemRBACCloneLog", ShortCode = "SLIIPSystemRBACCloneLog", Desc = "")]
	public class SLIIPSystemRBACCloneLog : BaseEntityService
	{
		#region Member Variables
		
        protected long? _systemId;
        protected string _systemName;
        protected string _systemCode;
        protected bool _forwardFlag;
        protected string _dataJson;
        protected string _dataName;
        protected long _operId;
        protected string _operName;
        protected long _dataCount;
        protected string _dataVersion;
        protected string _memo;
		

		#endregion

		#region Constructors

		public SLIIPSystemRBACCloneLog() { }

		public SLIIPSystemRBACCloneLog( long systemId, string systemName, string systemCode, bool forwardFlag, string dataJson, string dataName, long operId, string operName, long dataCount, string dataVersion, string memo, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._systemId = systemId;
			this._systemName = systemName;
			this._systemCode = systemCode;
			this._forwardFlag = forwardFlag;
			this._dataJson = dataJson;
			this._dataName = dataName;
			this._operId = operId;
			this._operName = operName;
			this._dataCount = dataCount;
			this._dataVersion = dataVersion;
			this._memo = memo;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SystemId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SystemId
		{
			get { return _systemId; }
			set { _systemId = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SystemName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string SystemName
		{
			get { return _systemName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SystemName", value, value.ToString());
				_systemName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SystemCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SystemCode
		{
			get { return _systemCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SystemCode", value, value.ToString());
				_systemCode = value;
			}
		}

		/// <summary>
		/// true正向，false逆向
		/// </summary>
		[DataMember]
        [DataDesc(CName = "", ShortCode = "ForwardFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool ForwardFlag
		{
			get { return _forwardFlag; }
			set { _forwardFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DataJson", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string DataJson
		{
			get { return _dataJson; }
			set
			{
				_dataJson = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DataName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DataName
		{
			get { return _dataName; }
			set
			{
				_dataName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long OperId
		{
			get { return _operId; }
			set { _operId = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OperName
		{
			get { return _operName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OperName", value, value.ToString());
				_operName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long DataCount
		{
			get { return _dataCount; }
			set { _dataCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DataVersion", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DataVersion
		{
			get { return _dataVersion; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DataVersion", value, value.ToString());
				_dataVersion = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = -1)]
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