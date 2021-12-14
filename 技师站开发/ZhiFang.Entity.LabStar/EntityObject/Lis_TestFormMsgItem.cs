using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisTestFormMsgItem

	/// <summary>
	/// LisTestFormMsgItem object for NHibernate mapped table 'Lis_TestFormMsgItem'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "LisTestFormMsgItem", ShortCode = "LisTestFormMsgItem", Desc = "")]
	public class LisTestFormMsgItem : BaseEntity
	{
		#region Member Variables

		protected long? _testFormID;
		protected long? _testItemID;
		protected DateTime? _gTestDate;
		protected long? _itemID;
		protected string _itemName;
		protected string _reportValue;
		protected string _resultStatus;
		protected string _resultStatusCode;
		protected int _alarmLevel;
		protected string _alarmInfo;
		protected DateTime? _dataUpdateTime;
		protected LisTestFormMsg _lisTestFormMsg;		

		#endregion

		#region Constructors

		public LisTestFormMsgItem() { }

		public LisTestFormMsgItem(long labID, long testFormID, long testItemID, DateTime gTestDate, long itemID, string itemName, string reportValue, string resultStatus, string resultStatusCode, int alarmLevel, string alarmInfo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LisTestFormMsg lisTestFormMsg)
		{
			this._labID = labID;
			this._testFormID = testFormID;
			this._testItemID = testItemID;
			this._gTestDate = gTestDate;
			this._itemID = itemID;
			this._itemName = itemName;
			this._reportValue = reportValue;
			this._resultStatus = resultStatus;
			this._resultStatusCode = resultStatusCode;
			this._alarmLevel = alarmLevel;
			this._alarmInfo = alarmInfo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lisTestFormMsg = lisTestFormMsg;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检验单ID", ShortCode = "TestFormID", Desc = "检验单ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? TestFormID
		{
			get { return _testFormID; }
			set { _testFormID = value; }
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
		[DataDesc(CName = "检测日期", ShortCode = "GTestDate", Desc = "检测日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GTestDate
		{
			get { return _gTestDate; }
			set { _gTestDate = value; }
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
		[DataDesc(CName = "报告值", ShortCode = "ReportValue", Desc = "报告值", ContextType = SysDic.All, Length = 300)]
		public virtual string ItemName
		{
			get { return _itemName; }
			set { _itemName = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告值", ShortCode = "ReportValue", Desc = "报告值", ContextType = SysDic.All, Length = 300)]
		public virtual string ReportValue
		{
			get { return _reportValue; }
			set { _reportValue = value; }
		}

		[DataMember]
		[DataDesc(CName = "检验结果状态", ShortCode = "ResultStatus", Desc = "检验结果状态", ContextType = SysDic.All, Length = 20)]
		public virtual string ResultStatus
		{
			get { return _resultStatus; }
			set { _resultStatus = value; }
		}

		/// <summary>
		/// 检验结果状态编码
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验结果状态编码", ShortCode = "ResultStatusCode", Desc = "检验结果状态", ContextType = SysDic.All, Length = 50)]
		public virtual string ResultStatusCode
		{
			get { return _resultStatusCode; }
			set { _resultStatusCode = value; }
		}

		[DataMember]
		[DataDesc(CName = "结果警示级别", ShortCode = "AlarmLevel", Desc = "结果警示级别", ContextType = SysDic.All, Length = 4)]
		public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

		[DataMember]
		[DataDesc(CName = "结果警示信息", ShortCode = "AlarmInfo", Desc = "结果警示信息", ContextType = SysDic.All, Length = 50)]
		public virtual string AlarmInfo
		{
			get { return _alarmInfo; }
			set { _alarmInfo = value; }
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
		[DataDesc(CName = "检验单信息，包含危急值，专家规则等", ShortCode = "LisTestFormMsg", Desc = "检验单信息，包含危急值，专家规则等")]
		public virtual LisTestFormMsg LisTestFormMsg
		{
			get { return _lisTestFormMsg; }
			set { _lisTestFormMsg = value; }
		}


		#endregion
	}
	#endregion
}