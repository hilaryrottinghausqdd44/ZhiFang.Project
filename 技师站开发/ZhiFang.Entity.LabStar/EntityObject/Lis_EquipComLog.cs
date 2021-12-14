using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisEquipComLog

	/// <summary>
	/// LisEquipComLog object for NHibernate mapped table 'Lis_EquipComLog'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "仪器通讯数据详细日志", ClassCName = "LisEquipComLog", ShortCode = "LisEquipComLog", Desc = "仪器通讯数据详细日志")]
	public class LisEquipComLog : BaseEntity
	{
		#region Member Variables

		protected long? _comFileID;
		protected int _comLogType;
		protected string _comLogInfo;
		protected DateTime? _comLogTime;
		protected long? _equipID;
		protected string _equipName;
		protected long? _sectionID;
		protected string _sectionName;
		protected string _clientComputer;
		protected string _clientMac;
		protected string _clientIP;
		protected DateTime? _dataUpdateTime;


		#endregion

		#region Constructors

		public LisEquipComLog() { }

		public LisEquipComLog(long labID, long comFileID, int comLogType, string comLogInfo, DateTime comLogTime, long equipID, string equipName, long sectionID, string sectionName, string clientComputer, string clientMac, string clientIP, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._comFileID = comFileID;
			this._comLogType = comLogType;
			this._comLogInfo = comLogInfo;
			this._comLogTime = comLogTime;
			this._equipID = equipID;
			this._equipName = equipName;
			this._sectionID = sectionID;
			this._sectionName = sectionName;
			this._clientComputer = clientComputer;
			this._clientMac = clientMac;
			this._clientIP = clientIP;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ComFileID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? ComFileID
		{
			get { return _comFileID; }
			set { _comFileID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ComLogType", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ComLogType
		{
			get { return _comLogType; }
			set { _comLogType = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ComLogInfo", Desc = "", ContextType = SysDic.All, Length = 16)]
		public virtual string ComLogInfo
		{
			get { return _comLogInfo; }
			set { _comLogInfo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ComLogTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ComLogTime
		{
			get { return _comLogTime; }
			set { _comLogTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "EquipID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipID
		{
			get { return _equipID; }
			set { _equipID = value; }
		}

		[DataMember]
		[DataDesc(CName = "仪器名称", ShortCode = "EquipName", Desc = "仪器名称", ContextType = SysDic.All, Length = 100)]
		public virtual string EquipName
		{
			get { return _equipName; }
			set { _equipName = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "小组ID", ShortCode = "SectionID", Desc = "小组ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? SectionID
		{
			get { return _sectionID; }
			set { _sectionID = value; }
		}

		[DataMember]
		[DataDesc(CName = "小组名称", ShortCode = "SectionName", Desc = "小组名称", ContextType = SysDic.All, Length = 100)]
		public virtual string SectionName
		{
			get { return _sectionName; }
			set { _sectionName = value; }
		}

		[DataMember]
		[DataDesc(CName = "客户端计算机名", ShortCode = "ClientComputer", Desc = "客户端计算机名", ContextType = SysDic.All, Length = 100)]
		public virtual string ClientComputer
		{
			get { return _clientComputer; }
			set { _clientComputer = value; }
		}

		[DataMember]
		[DataDesc(CName = "客户端网卡号", ShortCode = "ClientMac", Desc = "客户端网卡号", ContextType = SysDic.All, Length = 100)]
		public virtual string ClientMac
		{
			get { return _clientMac; }
			set { _clientMac = value; }
		}

		[DataMember]
		[DataDesc(CName = "客户端IP", ShortCode = "ClientIP", Desc = "客户端IP", ContextType = SysDic.All, Length = 100)]
		public virtual string ClientIP
		{
			get { return _clientIP; }
			set { _clientIP = value; }
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