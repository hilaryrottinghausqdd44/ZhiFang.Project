using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBTranRuleHostSection

	/// <summary>
	/// LBTranRuleHostSection object for NHibernate mapped table 'LB_TranRuleHostSection'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "LBTranRuleHostSection", ShortCode = "LBTranRuleHostSection", Desc = "")]
	public class LBTranRuleHostSection : BaseEntity
	{
		#region Member Variables

		protected long? _hostTypeID;
		protected long? _sectionID;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;

		#endregion

		#region Constructors

		public LBTranRuleHostSection() { }

		public LBTranRuleHostSection(long hostTypeID, long sectionID, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._hostTypeID = hostTypeID;
			this._sectionID = sectionID;
			this._dispOrder = dispOrder;
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "HostTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? HostTypeID
		{
			get { return _hostTypeID; }
			set { _hostTypeID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "SectionID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? SectionID
		{
			get { return _sectionID; }
			set { _sectionID = value; }
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


		#endregion
	}
	#endregion
}