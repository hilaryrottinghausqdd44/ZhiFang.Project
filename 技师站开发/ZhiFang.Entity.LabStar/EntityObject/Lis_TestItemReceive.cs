using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisTestItemReceive

	/// <summary>
	/// LisTestItemReceive object for NHibernate mapped table 'Lis_TestItemReceive'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "LisTestItemReceive", ShortCode = "LisTestItemReceive", Desc = "")]
	public class LisTestItemReceive : BaseEntity
	{
		#region Member Variables

		protected long? _orderItemID;
		protected long? _barCodeItemID;
		protected long? _ordersItemID;
		protected long? _barCodesItemID;
		protected long? _testItemID;
		protected long? _itemID;
		protected long? _pItemID;
		protected string _barCode;
		protected DateTime? _gTestDate;
		protected bool _isByHand;
		protected DateTime? _reportTime;
		protected DateTime? _dataUpdateTime;
		protected LisTestForm _lisTestForm;


		#endregion

		#region Constructors

		public LisTestItemReceive() { }

		public LisTestItemReceive(long labID, long orderItemID, long barCodeItemID, long ordersItemID, long barCodesItemID, long testItemID, long itemID, long pItemID, string barCode, DateTime gTestDate, bool isByHand, DateTime reportTime, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LisTestForm lisTestForm)
		{
			this._labID = labID;
			this._orderItemID = orderItemID;
			this._barCodeItemID = barCodeItemID;
			this._ordersItemID = ordersItemID;
			this._barCodesItemID = barCodesItemID;
			this._testItemID = testItemID;
			this._itemID = itemID;
			this._pItemID = pItemID;
			this._barCode = barCode;
			this._gTestDate = gTestDate;
			this._isByHand = isByHand;
			this._reportTime = reportTime;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lisTestForm = lisTestForm;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "医嘱单项目ID", ShortCode = "OrderItemID", Desc = "医嘱单项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? OrderItemID
		{
			get { return _orderItemID; }
			set { _orderItemID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样项目ID", ShortCode = "BarCodeItemID", Desc = "采样项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BarCodeItemID
		{
			get { return _barCodeItemID; }
			set { _barCodeItemID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "医嘱项目对应_项目ID", ShortCode = "OrdersItemID", Desc = "医嘱项目对应_项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? OrdersItemID
		{
			get { return _ordersItemID; }
			set { _ordersItemID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样项目对应_项目ID", ShortCode = "BarCodesItemID", Desc = "采样项目对应_项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BarCodesItemID
		{
			get { return _barCodesItemID; }
			set { _barCodesItemID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检验单项目ID", ShortCode = "TestItemID", Desc = "检验单项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? TestItemID
		{
			get { return _testItemID; }
			set { _testItemID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "项目ID", ShortCode = "ItemID", Desc = "项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ItemID
		{
			get { return _itemID; }
			set { _itemID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "组合项目ID", ShortCode = "PItemID", Desc = "组合项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? PItemID
		{
			get { return _pItemID; }
			set { _pItemID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string BarCode
		{
			get { return _barCode; }
			set { _barCode = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检测日期", ShortCode = "GTestDate", Desc = "检测日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GTestDate
		{
			get { return _gTestDate; }
			set { _gTestDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "是否手工", ShortCode = "IsByHand", Desc = "是否手工", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsByHand
		{
			get { return _isByHand; }
			set { _isByHand = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "报告时间", ShortCode = "ReportTime", Desc = "报告时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReportTime
		{
			get { return _reportTime; }
			set { _reportTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "LisTestForm", Desc = "")]
		public virtual LisTestForm LisTestForm
		{
			get { return _lisTestForm; }
			set { _lisTestForm = value; }
		}


		#endregion
	}
	#endregion
}