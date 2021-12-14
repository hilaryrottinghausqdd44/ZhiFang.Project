using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BarCodeForm

	/// <summary>
	/// BarCodeForm object for NHibernate mapped table 'BarCodeForm'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "BarCodeForm", ShortCode = "BarCodeForm", Desc = "")]
	public class BarCodeForm : BaseEntity
	{
		#region Member Variables

		protected string _barCode;
		protected int _samplingGroupNo;
		protected int _isPrep;
		protected int _isSpiltItem;
		protected int _isAffirm;
		protected int _receiveFlag;
		protected double _sampleCap;
		protected string _clientHost;
		protected string _collecter;
		protected long _collecterID;
		protected DateTime? _collectDate;
		protected DateTime? _collectTime;
		protected string _refuseUser;
		protected string _refuseopinion;
		protected string _refusereason;
		protected DateTime? _refuseTime;
		protected int _signflag;
		protected string _incepter;
		protected DateTime? _inceptTime;
		protected DateTime? _inceptDate;
		protected string _receiveMan;
		protected DateTime? _receiveDate;
		protected DateTime? _receiveTime;
		protected string _printInfo;
		protected int _printCount;
		protected int _dr2Flag;
		protected DateTime? _flagDateDelete;
		protected int _dispenseFlag;
		protected string _serialScanTime;
		protected int _barCodeSource;
		protected int _deleteFlag;
		protected int _sendOffFlag;
		protected string _sendOffMan;
		protected string _eMSMan;
		protected DateTime? _sendOffDate;
		protected string _reportSignMan;
		protected DateTime? _reportSignDate;
		protected string _refuseIncepter;
		protected string _refuseIncepterMemo;
		protected int _reportFlag;
		protected string _sendOffMemo;
		protected int _sampleTypeNo;
		protected string _sampleSendNo;
		protected int _webLisFlag;
		protected DateTime? _webLisOpTime;
		protected string _webLiser;
		protected string _webLisDescript;
		protected string _webLisOrgID;
		protected int _webLisIsReply;
		protected string _webLisReplyDate;
		protected string _webLisSourceOrgId;
		protected DateTime? _webLisUploadTime;
		protected int _webLisUploadStatus;
		protected int _webLisUploadTestStatus;
		protected string _webLisUploader;
		protected string _webLisUploadDes;
		protected string _webLisSourceOrgName;
		protected string _clientNo;
		protected string _clientName;
		protected DateTime? _barCodeOpTime;
		protected string _webLisOrgName;
		protected string _color;
		protected string _itemName;
		protected string _itemNo;
		protected string _sampleTypeName;


		#endregion

		#region Constructors

		public BarCodeForm() { }

		public BarCodeForm(string barCode, int samplingGroupNo, int isPrep, int isSpiltItem, int isAffirm, int receiveFlag, double sampleCap, string clientHost, string collecter, long collecterID, DateTime collectDate, DateTime collectTime, string refuseUser, string refuseopinion, string refusereason, DateTime refuseTime, int signflag, string incepter, DateTime inceptTime, DateTime inceptDate, string receiveMan, DateTime receiveDate, DateTime receiveTime, string printInfo, int printCount, int dr2Flag, DateTime flagDateDelete, int dispenseFlag, string serialScanTime, int barCodeSource, int deleteFlag, int sendOffFlag, string sendOffMan, string eMSMan, DateTime sendOffDate, string reportSignMan, DateTime reportSignDate, string refuseIncepter, string refuseIncepterMemo, int reportFlag, string sendOffMemo, int sampleTypeNo, string sampleSendNo, int webLisFlag, DateTime webLisOpTime, string webLiser, string webLisDescript, string webLisOrgID, int webLisIsReply, string webLisReplyDate, string webLisSourceOrgId, DateTime webLisUploadTime, int webLisUploadStatus, int webLisUploadTestStatus, string webLisUploader, string webLisUploadDes, string webLisSourceOrgName, string clientNo, string clientName, DateTime barCodeOpTime, string webLisOrgName, string color, string itemName, string itemNo, string sampleTypeName)
		{
			this._barCode = barCode;
			this._samplingGroupNo = samplingGroupNo;
			this._isPrep = isPrep;
			this._isSpiltItem = isSpiltItem;
			this._isAffirm = isAffirm;
			this._receiveFlag = receiveFlag;
			this._sampleCap = sampleCap;
			this._clientHost = clientHost;
			this._collecter = collecter;
			this._collecterID = collecterID;
			this._collectDate = collectDate;
			this._collectTime = collectTime;
			this._refuseUser = refuseUser;
			this._refuseopinion = refuseopinion;
			this._refusereason = refusereason;
			this._refuseTime = refuseTime;
			this._signflag = signflag;
			this._incepter = incepter;
			this._inceptTime = inceptTime;
			this._inceptDate = inceptDate;
			this._receiveMan = receiveMan;
			this._receiveDate = receiveDate;
			this._receiveTime = receiveTime;
			this._printInfo = printInfo;
			this._printCount = printCount;
			this._dr2Flag = dr2Flag;
			this._flagDateDelete = flagDateDelete;
			this._dispenseFlag = dispenseFlag;
			this._serialScanTime = serialScanTime;
			this._barCodeSource = barCodeSource;
			this._deleteFlag = deleteFlag;
			this._sendOffFlag = sendOffFlag;
			this._sendOffMan = sendOffMan;
			this._eMSMan = eMSMan;
			this._sendOffDate = sendOffDate;
			this._reportSignMan = reportSignMan;
			this._reportSignDate = reportSignDate;
			this._refuseIncepter = refuseIncepter;
			this._refuseIncepterMemo = refuseIncepterMemo;
			this._reportFlag = reportFlag;
			this._sendOffMemo = sendOffMemo;
			this._sampleTypeNo = sampleTypeNo;
			this._sampleSendNo = sampleSendNo;
			this._webLisFlag = webLisFlag;
			this._webLisOpTime = webLisOpTime;
			this._webLiser = webLiser;
			this._webLisDescript = webLisDescript;
			this._webLisOrgID = webLisOrgID;
			this._webLisIsReply = webLisIsReply;
			this._webLisReplyDate = webLisReplyDate;
			this._webLisSourceOrgId = webLisSourceOrgId;
			this._webLisUploadTime = webLisUploadTime;
			this._webLisUploadStatus = webLisUploadStatus;
			this._webLisUploadTestStatus = webLisUploadTestStatus;
			this._webLisUploader = webLisUploader;
			this._webLisUploadDes = webLisUploadDes;
			this._webLisSourceOrgName = webLisSourceOrgName;
			this._clientNo = clientNo;
			this._clientName = clientName;
			this._barCodeOpTime = barCodeOpTime;
			this._webLisOrgName = webLisOrgName;
			this._color = color;
			this._itemName = itemName;
			this._itemNo = itemNo;
			this._sampleTypeName = sampleTypeName;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 30)]
		public virtual string BarCode
		{
			get { return _barCode; }
			set
			{
				if (value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
				_barCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SamplingGroupNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int SamplingGroupNo
		{
			get { return _samplingGroupNo; }
			set { _samplingGroupNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsPrep", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int IsPrep
		{
			get { return _isPrep; }
			set { _isPrep = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsSpiltItem", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int IsSpiltItem
		{
			get { return _isSpiltItem; }
			set { _isSpiltItem = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsAffirm", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int IsAffirm
		{
			get { return _isAffirm; }
			set { _isAffirm = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ReceiveFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ReceiveFlag
		{
			get { return _receiveFlag; }
			set { _receiveFlag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "SampleCap", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual double SampleCap
		{
			get { return _sampleCap; }
			set { _sampleCap = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ClientHost", Desc = "", ContextType = SysDic.All, Length = 60)]
		public virtual string ClientHost
		{
			get { return _clientHost; }
			set
			{
				if (value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ClientHost", value, value.ToString());
				_clientHost = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Collecter", Desc = "", ContextType = SysDic.All, Length = 300)]
		public virtual string Collecter
		{
			get { return _collecter; }
			set
			{
				if (value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Collecter", value, value.ToString());
				_collecter = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CollecterID", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual long CollecterID
		{
			get { return _collecterID; }
			set { _collecterID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "CollectDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectDate
		{
			get { return _collectDate; }
			set { _collectDate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "CollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectTime
		{
			get { return _collectTime; }
			set { _collectTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "RefuseUser", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string RefuseUser
		{
			get { return _refuseUser; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for RefuseUser", value, value.ToString());
				_refuseUser = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Refuseopinion", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string Refuseopinion
		{
			get { return _refuseopinion; }
			set
			{
				if (value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Refuseopinion", value, value.ToString());
				_refuseopinion = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Refusereason", Desc = "", ContextType = SysDic.All, Length = 100)]
		public virtual string Refusereason
		{
			get { return _refusereason; }
			set
			{
				if (value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Refusereason", value, value.ToString());
				_refusereason = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "RefuseTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? RefuseTime
		{
			get { return _refuseTime; }
			set { _refuseTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Signflag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int Signflag
		{
			get { return _signflag; }
			set { _signflag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Incepter", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string Incepter
		{
			get { return _incepter; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Incepter", value, value.ToString());
				_incepter = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "InceptTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InceptTime
		{
			get { return _inceptTime; }
			set { _inceptTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "InceptDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InceptDate
		{
			get { return _inceptDate; }
			set { _inceptDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ReceiveMan", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string ReceiveMan
		{
			get { return _receiveMan; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveMan", value, value.ToString());
				_receiveMan = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ReceiveDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReceiveDate
		{
			get { return _receiveDate; }
			set { _receiveDate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ReceiveTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReceiveTime
		{
			get { return _receiveTime; }
			set { _receiveTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PrintInfo", Desc = "", ContextType = SysDic.All, Length = 600)]
		public virtual string PrintInfo
		{
			get { return _printInfo; }
			set
			{
				if (value != null && value.Length > 600)
					throw new ArgumentOutOfRangeException("Invalid value for PrintInfo", value, value.ToString());
				_printInfo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int PrintCount
		{
			get { return _printCount; }
			set { _printCount = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Dr2Flag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int Dr2Flag
		{
			get { return _dr2Flag; }
			set { _dr2Flag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "FlagDateDelete", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? FlagDateDelete
		{
			get { return _flagDateDelete; }
			set { _flagDateDelete = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DispenseFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DispenseFlag
		{
			get { return _dispenseFlag; }
			set { _dispenseFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SerialScanTime", Desc = "", ContextType = SysDic.All, Length = 30)]
		public virtual string SerialScanTime
		{
			get { return _serialScanTime; }
			set
			{
				if (value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for SerialScanTime", value, value.ToString());
				_serialScanTime = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "BarCodeSource", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int BarCodeSource
		{
			get { return _barCodeSource; }
			set { _barCodeSource = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DeleteFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DeleteFlag
		{
			get { return _deleteFlag; }
			set { _deleteFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SendOffFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int SendOffFlag
		{
			get { return _sendOffFlag; }
			set { _sendOffFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SendOffMan", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string SendOffMan
		{
			get { return _sendOffMan; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SendOffMan", value, value.ToString());
				_sendOffMan = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "EMSMan", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string EMSMan
		{
			get { return _eMSMan; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EMSMan", value, value.ToString());
				_eMSMan = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "SendOffDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? SendOffDate
		{
			get { return _sendOffDate; }
			set { _sendOffDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ReportSignMan", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string ReportSignMan
		{
			get { return _reportSignMan; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReportSignMan", value, value.ToString());
				_reportSignMan = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "ReportSignDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReportSignDate
		{
			get { return _reportSignDate; }
			set { _reportSignDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "RefuseIncepter", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string RefuseIncepter
		{
			get { return _refuseIncepter; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for RefuseIncepter", value, value.ToString());
				_refuseIncepter = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "RefuseIncepterMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
		public virtual string RefuseIncepterMemo
		{
			get { return _refuseIncepterMemo; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for RefuseIncepterMemo", value, value.ToString());
				_refuseIncepterMemo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ReportFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int ReportFlag
		{
			get { return _reportFlag; }
			set { _reportFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SendOffMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
		public virtual string SendOffMemo
		{
			get { return _sendOffMemo; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for SendOffMemo", value, value.ToString());
				_sendOffMemo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set { _sampleTypeNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleSendNo", Desc = "", ContextType = SysDic.All, Length = 30)]
		public virtual string SampleSendNo
		{
			get { return _sampleSendNo; }
			set
			{
				if (value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for SampleSendNo", value, value.ToString());
				_sampleSendNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "外送流程标志", ShortCode = "WebLisFlag", Desc = "外送流程标志", ContextType = SysDic.All, Length = 4)]
		public virtual int WebLisFlag
		{
			get { return _webLisFlag; }
			set { _webLisFlag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "外送流程标志更新时间", ShortCode = "WebLisOpTime", Desc = "外送流程标志更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? WebLisOpTime
		{
			get { return _webLisOpTime; }
			set { _webLisOpTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "外送流程标志更新人", ShortCode = "WebLiser", Desc = "外送流程标志更新人", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLiser
		{
			get { return _webLiser; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLiser", value, value.ToString());
				_webLiser = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "流程操作说明", ShortCode = "WebLisDescript", Desc = "流程操作说明", ContextType = SysDic.All, Length = 500)]
		public virtual string WebLisDescript
		{
			get { return _webLisDescript; }
			set
			{
				if (value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisDescript", value, value.ToString());
				_webLisDescript = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "外送单位编号", ShortCode = "WebLisOrgID", Desc = "外送单位编号", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLisOrgID
		{
			get { return _webLisOrgID; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisOrgID", value, value.ToString());
				_webLisOrgID = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "是否重试", ShortCode = "WebLisIsReply", Desc = "是否重试", ContextType = SysDic.All, Length = 4)]
		public virtual int WebLisIsReply
		{
			get { return _webLisIsReply; }
			set { _webLisIsReply = value; }
		}

		[DataMember]
		[DataDesc(CName = "下次重试执行时间", ShortCode = "WebLisReplyDate", Desc = "下次重试执行时间", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLisReplyDate
		{
			get { return _webLisReplyDate; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisReplyDate", value, value.ToString());
				_webLisReplyDate = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "被录入送检单位编号", ShortCode = "WebLisSourceOrgId", Desc = "被录入送检单位编号", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLisSourceOrgId
		{
			get { return _webLisSourceOrgId; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgId", value, value.ToString());
				_webLisSourceOrgId = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "报告获取时间", ShortCode = "WebLisUploadTime", Desc = "报告获取时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? WebLisUploadTime
		{
			get { return _webLisUploadTime; }
			set { _webLisUploadTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告获取", ShortCode = "WebLisUploadStatus", Desc = "报告获取", ContextType = SysDic.All, Length = 4)]
		public virtual int WebLisUploadStatus
		{
			get { return _webLisUploadStatus; }
			set { _webLisUploadStatus = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告下载当时状态", ShortCode = "WebLisUploadTestStatus", Desc = "报告下载当时状态", ContextType = SysDic.All, Length = 4)]
		public virtual int WebLisUploadTestStatus
		{
			get { return _webLisUploadTestStatus; }
			set { _webLisUploadTestStatus = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告下载人", ShortCode = "WebLisUploader", Desc = "报告下载人", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLisUploader
		{
			get { return _webLisUploader; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisUploader", value, value.ToString());
				_webLisUploader = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "报告下载描述", ShortCode = "WebLisUploadDes", Desc = "报告下载描述", ContextType = SysDic.All, Length = 500)]
		public virtual string WebLisUploadDes
		{
			get { return _webLisUploadDes; }
			set
			{
				if (value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisUploadDes", value, value.ToString());
				_webLisUploadDes = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "被录入送检单位名称", ShortCode = "WebLisSourceOrgName", Desc = "被录入送检单位名称", ContextType = SysDic.All, Length = 300)]
		public virtual string WebLisSourceOrgName
		{
			get { return _webLisSourceOrgName; }
			set
			{
				if (value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgName", value, value.ToString());
				_webLisSourceOrgName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "录入医辽机构编号", ShortCode = "ClientNo", Desc = "录入医辽机构编号", ContextType = SysDic.All, Length = 50)]
		public virtual string ClientNo
		{
			get { return _clientNo; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ClientNo", value, value.ToString());
				_clientNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "录入医疗机构名称", ShortCode = "ClientName", Desc = "录入医疗机构名称", ContextType = SysDic.All, Length = 300)]
		public virtual string ClientName
		{
			get { return _clientName; }
			set
			{
				if (value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ClientName", value, value.ToString());
				_clientName = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "BarCodeOpTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BarCodeOpTime
		{
			get { return _barCodeOpTime; }
			set { _barCodeOpTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "WebLisOrgName", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string WebLisOrgName
		{
			get { return _webLisOrgName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisOrgName", value, value.ToString());
				_webLisOrgName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Color", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string Color
		{
			get { return _color; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Color", value, value.ToString());
				_color = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ItemName", Desc = "", ContextType = SysDic.All, Length = 500)]
		public virtual string ItemName
		{
			get { return _itemName; }
			set
			{
				if (value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ItemName", value, value.ToString());
				_itemName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 500)]
		public virtual string ItemNo
		{
			get { return _itemNo; }
			set
			{
				if (value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ItemNo", value, value.ToString());
				_itemNo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SampleTypeName", Desc = "", ContextType = SysDic.All, Length = 200)]
		public virtual string SampleTypeName
		{
			get { return _sampleTypeName; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for SampleTypeName", value, value.ToString());
				_sampleTypeName = value;
			}
		}


		#endregion
	}
	#endregion
}