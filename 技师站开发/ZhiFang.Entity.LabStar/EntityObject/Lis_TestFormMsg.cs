using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisTestFormMsg

	/// <summary>
	/// LisTestFormMsg object for NHibernate mapped table 'Lis_TestFormMsg'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "检验单信息，包含危急值，专家规则等", ClassCName = "LisTestFormMsg", ShortCode = "LisTestFormMsg", Desc = "检验单信息，包含危急值，专家规则等")]
	public class LisTestFormMsg : BaseEntity
	{
		#region Member Variables

		protected int _msgType;
		protected string _barCode;
		protected string _patNo;
		protected DateTime? _gTestDate;
		protected string _masterDesc;
		protected string _detailDesc;
		protected long? _readerID;
		protected string _reader;
		protected DateTime? _readTime;
		protected int _reportStatus;
		protected long? _reporterID;
		protected string _reporter;
		protected DateTime? _reportTime;
		protected string _reportInfo;
		protected int _phoneStatus;
		protected long? _phoneCallerID;
		protected string _phoneCaller;
		protected DateTime? _phoneTime;
		protected string _phoneNum;
		protected string _phoneReceiver;
		protected string _phoneDesc;
		protected int _reportEditStatus;
		protected DateTime? _dataUpdateTime;
		protected LisTestForm _lisTestForm;


		#endregion

		#region Constructors

		public LisTestFormMsg() { }

		public LisTestFormMsg(long labID, int msgType, string barCode, string patNo, DateTime gTestDate, string masterDesc, string detailDesc, long readerID, string reader, DateTime readTime, int reportStatus, long reporterID, string reporter, DateTime reportTime, string reportInfo, int phoneStatus, long phoneCallerID, string phoneCaller, DateTime phoneTime, string phoneNum, string phoneReceiver, string phoneDesc, int reportEditStatus, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LisTestForm lisTestForm)
		{
			this._labID = labID;
			this._msgType = msgType;
			this._barCode = barCode;
			this._patNo = patNo;
			this._gTestDate = gTestDate;
			this._masterDesc = masterDesc;
			this._detailDesc = detailDesc;
			this._readerID = readerID;
			this._reader = reader;
			this._readTime = readTime;
			this._reportStatus = reportStatus;
			this._reporterID = reporterID;
			this._reporter = reporter;
			this._reportTime = reportTime;
			this._reportInfo = reportInfo;
			this._phoneStatus = phoneStatus;
			this._phoneCallerID = phoneCallerID;
			this._phoneCaller = phoneCaller;
			this._phoneTime = phoneTime;
			this._phoneNum = phoneNum;
			this._phoneReceiver = phoneReceiver;
			this._phoneDesc = phoneDesc;
			this._reportEditStatus = reportEditStatus;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lisTestForm = lisTestForm;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "信息类型", ShortCode = "MsgType", Desc = "信息类型", ContextType = SysDic.All, Length = 4)]
		public virtual int MsgType
		{
			get { return _msgType; }
			set { _msgType = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string BarCode
		{
			get { return _barCode; }
			set { _barCode = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PatNo", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string PatNo
		{
			get { return _patNo; }
			set { _patNo = value; }
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
		[DataDesc(CName = "临床内容", ShortCode = "MasterDesc", Desc = "临床内容", ContextType = SysDic.All, Length = 2000)]
		public virtual string MasterDesc
		{
			get { return _masterDesc; }
			set { _masterDesc = value; }
		}

		[DataMember]
		[DataDesc(CName = "辅助信息内容", ShortCode = "DetailDesc", Desc = "辅助信息内容", ContextType = SysDic.All, Length = 2000)]
		public virtual string DetailDesc
		{
			get { return _detailDesc; }
			set { _detailDesc = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "阅读人ID", ShortCode = "ReaderID", Desc = "阅读人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReaderID
		{
			get { return _readerID; }
			set { _readerID = value; }
		}

		[DataMember]
		[DataDesc(CName = "阅读人", ShortCode = "Reader", Desc = "阅读人", ContextType = SysDic.All, Length = 50)]
		public virtual string Reader
		{
			get { return _reader; }
			set { _reader = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "阅读时间", ShortCode = "ReadTime", Desc = "阅读时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReadTime
		{
			get { return _readTime; }
			set { _readTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告状态", ShortCode = "ReportStatus", Desc = "报告状态", ContextType = SysDic.All, Length = 4)]
		public virtual int ReportStatus
		{
			get { return _reportStatus; }
			set { _reportStatus = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "报告人ID", ShortCode = "ReporterID", Desc = "报告人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReporterID
		{
			get { return _reporterID; }
			set { _reporterID = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告人", ShortCode = "Reporter", Desc = "报告人", ContextType = SysDic.All, Length = 50)]
		public virtual string Reporter
		{
			get { return _reporter; }
			set { _reporter = value; }
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
		[DataDesc(CName = "报告说明", ShortCode = "ReportInfo", Desc = "报告说明", ContextType = SysDic.All, Length = 200)]
		public virtual string ReportInfo
		{
			get { return _reportInfo; }
			set { _reportInfo = value; }
		}

		[DataMember]
		[DataDesc(CName = "电话通知状态", ShortCode = "PhoneStatus", Desc = "电话通知状态", ContextType = SysDic.All, Length = 4)]
		public virtual int PhoneStatus
		{
			get { return _phoneStatus; }
			set { _phoneStatus = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "电话通知人ID", ShortCode = "PhoneCallerID", Desc = "电话通知人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? PhoneCallerID
		{
			get { return _phoneCallerID; }
			set { _phoneCallerID = value; }
		}

		[DataMember]
		[DataDesc(CName = "电话通知人", ShortCode = "PhoneCaller", Desc = "电话通知人", ContextType = SysDic.All, Length = 50)]
		public virtual string PhoneCaller
		{
			get { return _phoneCaller; }
			set { _phoneCaller = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "电话通知时间", ShortCode = "PhoneTime", Desc = "电话通知时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PhoneTime
		{
			get { return _phoneTime; }
			set { _phoneTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "电话", ShortCode = "PhoneNum", Desc = "电话", ContextType = SysDic.All, Length = 50)]
		public virtual string PhoneNum
		{
			get { return _phoneNum; }
			set { _phoneNum = value; }
		}

		[DataMember]
		[DataDesc(CName = "电话接收人", ShortCode = "PhoneReceiver", Desc = "电话接收人", ContextType = SysDic.All, Length = 50)]
		public virtual string PhoneReceiver
		{
			get { return _phoneReceiver; }
			set { _phoneReceiver = value; }
		}

		[DataMember]
		[DataDesc(CName = "电话通知说明", ShortCode = "PhoneDesc", Desc = "电话通知说明", ContextType = SysDic.All, Length = 200)]
		public virtual string PhoneDesc
		{
			get { return _phoneDesc; }
			set { _phoneDesc = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告修改状态", ShortCode = "ReportEditStatus", Desc = "报告修改状态", ContextType = SysDic.All, Length = 4)]
		public virtual int ReportEditStatus
		{
			get { return _reportEditStatus; }
			set { _reportEditStatus = value; }
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