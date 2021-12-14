using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisEquipComFile

	/// <summary>
	/// LisEquipComFile object for NHibernate mapped table 'Lis_EquipComFile'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "仪器通讯数据文件", ClassCName = "LisEquipComFile", ShortCode = "LisEquipComFile", Desc = "仪器通讯数据文件")]
	public class LisEquipComFile : BaseEntity
	{
		#region Member Variables

		protected string _comFileName;
		protected string _comFileComment;
		protected int _comFileResultCount;
		protected string _comFileResultType;
		protected DateTime? _comFileTime;
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

		public LisEquipComFile() { }

		public LisEquipComFile(long labID, string comFileName, string comFileComment, int comFileResultCount, string comFileResultType, DateTime comFileTime, long equipID, string equipName, long sectionID, string sectionName, string clientComputer, string clientMac, string clientIP, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._comFileName = comFileName;
			this._comFileComment = comFileComment;
			this._comFileResultCount = comFileResultCount;
			this._comFileResultType = comFileResultType;
			this._comFileTime = comFileTime;
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
		[DataDesc(CName = "通讯数据文件名称", ShortCode = "ComFileName", Desc = "通讯数据文件名称", ContextType = SysDic.All, Length = 16)]
		public virtual string ComFileName
		{
			get { return _comFileName; }
			set { _comFileName = value; }
		}

		[DataMember]
		[DataDesc(CName = "通讯数据文件内容", ShortCode = "ComFileComment", Desc = "通讯数据文件内容", ContextType = SysDic.All, Length = 16)]
		public virtual string ComFileComment
		{
			get { return _comFileComment; }
			set { _comFileComment = value; }
		}

		[DataMember]
		[DataDesc(CName = "通讯数据文记录数", ShortCode = "ComFileResultCount", Desc = "通讯数据文记录数", ContextType = SysDic.All, Length = 4)]
		public virtual int ComFileResultCount
		{
			get { return _comFileResultCount; }
			set { _comFileResultCount = value; }
		}

		[DataMember]
		[DataDesc(CName = "通讯数据文件类型", ShortCode = "ComFileResultType", Desc = "通讯数据文件类型", ContextType = SysDic.All, Length = 100)]
		public virtual string ComFileResultType
		{
			get { return _comFileResultType; }
			set { _comFileResultType = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "通讯数据文件时间", ShortCode = "ComFileTime", Desc = "通讯数据文件时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ComFileTime
		{
			get { return _comFileTime; }
			set { _comFileTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "仪器ID", ShortCode = "EquipID", Desc = "仪器ID", ContextType = SysDic.All, Length = 8)]
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