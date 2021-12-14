using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisQueue

	/// <summary>
	/// LisQueue object for NHibernate mapped table 'Lis_Queue'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "LisQueue", ShortCode = "LisQueue", Desc = "")]
	public class LisQueue : BaseEntity
	{
		#region Member Variables

		protected DateTime? _lineDate;
		protected int _queueNo;
		protected string _queueNoHeader;
		protected string _barCode;
		protected string _patName;
		protected string _hisPatNo;
		protected string _orderFormNo;
		protected int _execFlag;
		protected int _patTypeID;
		protected DateTime? _dataUpdateTime;
		protected long? _barCodeFormID;

		#endregion

		#region Constructors

		public LisQueue() { }

		public LisQueue(DateTime lineDate, int queueNo, string queueNoHeader, string barCode, string patName, string hisPatNo, string orderFormNo, int execFlag, int patTypeID, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long barCodeFormID)
		{
			this._lineDate = lineDate;
			this._queueNo = queueNo;
			this._queueNoHeader = queueNoHeader;
			this._barCode = barCode;
			this._patName = patName;
			this._hisPatNo = hisPatNo;
			this._orderFormNo = orderFormNo;
			this._execFlag = execFlag;
			this._patTypeID = patTypeID;
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._barCodeFormID = barCodeFormID;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "LineDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? LineDate
		{
			get { return _lineDate; }
			set { _lineDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "QueueNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int QueueNo
		{
			get { return _queueNo; }
			set { _queueNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "QueueNoNoHeader", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string QueueNoHeader
		{
			get { return _queueNoHeader; }
			set { _queueNoHeader = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string BarCode
		{
			get { return _barCode; }
			set { _barCode = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PatName", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string PatName
		{
			get { return _patName; }
			set { _patName = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "HisPatNo", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string HisPatNo
		{
			get { return _hisPatNo; }
			set { _hisPatNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "OrderFormNo", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string OrderFormNo
		{
			get { return _orderFormNo; }
			set { _orderFormNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ExecFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ExecFlag
		{
			get { return _execFlag; }
			set { _execFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PatTypeID", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int PatTypeID
		{
			get { return _patTypeID; }
			set { _patTypeID = value; }
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
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "BarCodeFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? BarCodeFormID
		{
			get { return _barCodeFormID; }
			set { _barCodeFormID = value; }
		}


		#endregion
	}
	#endregion
}