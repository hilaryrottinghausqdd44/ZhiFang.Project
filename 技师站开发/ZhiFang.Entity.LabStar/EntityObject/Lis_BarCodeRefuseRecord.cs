using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisBarCodeRefuseRecord

	/// <summary>
	/// LisBarCodeRefuseRecord object for NHibernate mapped table 'Lis_BarCodeRefuseRecord'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "LisBarCodeRefuseRecord", ShortCode = "LisBarCodeRefuseRecord", Desc = "")]
	public class LisBarCodeRefuseRecord : BaseEntity
	{
		#region Member Variables

		protected long _phrasesWatchClassItemID;
		protected long? _phrasesWatchClassID;
		protected long? _refuseID;
		protected string _refuseValue;
		protected string _refuseOperate;
		protected string _memo;
		protected long? _operateUserID;
		protected string _operateUser;
		protected string _operateHost;
		protected string _operateAddress;
		protected string _operateHostType;
		protected long? _relationUserID;
		protected string _relationUser;
		protected long? _deptID;
		protected string _dept;
		protected string _deptTelNo;
		protected string _barCode;
		protected int _iOFlag;
		protected long? _iOUserID;
		protected string _iOUserName;
		protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public LisBarCodeRefuseRecord() { }

		public LisBarCodeRefuseRecord(long labID, long phrasesWatchClassItemID, long phrasesWatchClassID, long refuseID, string refuseValue, string refuseOperate, string memo, long operateUserID, string operateUser, string operateHost, string operateAddress, string operateHostType, long relationUserID, string relationUser, long deptID, string dept, string deptTelNo, string barCode, int iOFlag, long iOUserID, string iOUserName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._phrasesWatchClassItemID = phrasesWatchClassItemID;
			this._phrasesWatchClassID = phrasesWatchClassID;
			this._refuseID = refuseID;
			this._refuseValue = refuseValue;
			this._refuseOperate = refuseOperate;
			this._memo = memo;
			this._operateUserID = operateUserID;
			this._operateUser = operateUser;
			this._operateHost = operateHost;
			this._operateAddress = operateAddress;
			this._operateHostType = operateHostType;
			this._relationUserID = relationUserID;
			this._relationUser = relationUser;
			this._deptID = deptID;
			this._dept = dept;
			this._deptTelNo = deptTelNo;
			this._barCode = barCode;
			this._iOFlag = iOFlag;
			this._iOUserID = iOUserID;
			this._iOUserName = iOUserName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "PhrasesWatchClassItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long PhrasesWatchClassItemID
		{
			get { return _phrasesWatchClassItemID; }
			set { _phrasesWatchClassItemID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "PhrasesWatchClassID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? PhrasesWatchClassID
		{
			get { return _phrasesWatchClassID; }
			set { _phrasesWatchClassID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "RefuseID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? RefuseID
		{
			get { return _refuseID; }
			set { _refuseID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "RefuseValue", Desc = "", ContextType = SysDic.All, Length = 200)]
		public virtual string RefuseValue
		{
			get { return _refuseValue; }
			set { _refuseValue = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "RefuseOperate", Desc = "", ContextType = SysDic.All, Length = 200)]
		public virtual string RefuseOperate
		{
			get { return _refuseOperate; }
			set { _refuseOperate = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
		public virtual string Memo
		{
			get { return _memo; }
			set { _memo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "OperateUserID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? OperateUserID
		{
			get { return _operateUserID; }
			set { _operateUserID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "OperateUser", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string OperateUser
		{
			get { return _operateUser; }
			set { _operateUser = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "OperateHost", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string OperateHost
		{
			get { return _operateHost; }
			set { _operateHost = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "OperateAddress", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string OperateAddress
		{
			get { return _operateAddress; }
			set { _operateAddress = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "OperateHostType", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string OperateHostType
		{
			get { return _operateHostType; }
			set { _operateHostType = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "RelationUserID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? RelationUserID
		{
			get { return _relationUserID; }
			set { _relationUserID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "RelationUser", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string RelationUser
		{
			get { return _relationUser; }
			set { _relationUser = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Dept", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string Dept
		{
			get { return _dept; }
			set { _dept = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DeptTelNo", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string DeptTelNo
		{
			get { return _deptTelNo; }
			set { _deptTelNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string BarCode
		{
			get { return _barCode; }
			set { _barCode = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IOFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int IOFlag
		{
			get { return _iOFlag; }
			set { _iOFlag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "IOUserID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? IOUserID
		{
			get { return _iOUserID; }
			set { _iOUserID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IOUserName", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string IOUserName
		{
			get { return _iOUserName; }
			set { _iOUserName = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}


		#endregion
	}
	#endregion
}