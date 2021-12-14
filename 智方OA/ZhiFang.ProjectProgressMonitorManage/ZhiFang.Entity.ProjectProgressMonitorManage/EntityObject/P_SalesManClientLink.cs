using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PSalesManClientLink

	/// <summary>
	/// PSalesManClientLink object for NHibernate mapped table 'P_SalesManClientLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "销售人员同客户关系表", ClassCName = "PSalesManClientLink", ShortCode = "PSalesManClientLink", Desc = "销售人员同客户关系表")]
	public class PSalesManClientLink : BaseEntity
	{
		#region Member Variables
		
        protected long? _pClientID;
        protected string _pClientName;
        protected long? _salesManID;
        protected string _salesManName;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public PSalesManClientLink() { }

		public PSalesManClientLink( long labID, long pClientID, string pClientName, long salesManID, string salesManName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._pClientID = pClientID;
			this._pClientName = pClientName;
			this._salesManID = salesManID;
			this._salesManName = salesManName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "PClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PClientID
		{
			get { return _pClientID; }
			set { _pClientID = value; }
		}

        [DataMember]
        [DataDesc(CName = "客户名称", ShortCode = "PClientName", Desc = "客户名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PClientName
		{
			get { return _pClientName; }
			set
			{
				_pClientName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "销售人员ID", ShortCode = "SalesManID", Desc = "销售人员ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SalesManID
		{
			get { return _salesManID; }
			set { _salesManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "销售人员姓名", ShortCode = "SalesManName", Desc = "销售人员姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string SalesManName
		{
			get { return _salesManName; }
			set
			{
				_salesManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		
		#endregion
	}
	#endregion
}