using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBTGetMaxNo

	/// <summary>
	/// LBTGetMaxNo object for NHibernate mapped table 'LB_TGetMaxNo'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "LBTGetMaxNo", ShortCode = "LBTGetMaxNo", Desc = "")]
	public class LBTGetMaxNo : BaseEntity
	{
		#region Member Variables

		protected long _bmsTypeID;
		protected string _bmsType;
		protected DateTime _bmsDate;
		protected string _maxID;
		protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public LBTGetMaxNo() { }

		public LBTGetMaxNo(long bmsTypeID, string bmsType, DateTime bmsDate, string maxID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._bmsTypeID = bmsTypeID;
			this._bmsType = bmsType;
			this._bmsDate = bmsDate;
			this._maxID = maxID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "BmsTypeID", Desc = "业务类型ID", ContextType = SysDic.All, Length = 100)]
		public virtual long BmsTypeID
		{
			get { return _bmsTypeID; }
			set { _bmsTypeID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BmsType", Desc = "业务类型", ContextType = SysDic.All, Length = 100)]
		public virtual string BmsType
		{
			get { return _bmsType; }
			set { _bmsType = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BmsDate", Desc = "业务时间", ContextType = SysDic.All, Length = 16)]
		public virtual DateTime BmsDate
		{
			get { return _bmsDate; }
			set { _bmsDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "MaxID", Desc = "最大号", ContextType = SysDic.All, Length = 16)]
		public virtual string MaxID
		{
			get { return _maxID; }
			set { _maxID = value; }
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