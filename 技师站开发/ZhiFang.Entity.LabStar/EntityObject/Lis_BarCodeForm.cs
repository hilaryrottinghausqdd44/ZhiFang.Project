using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisBarCodeForm

	/// <summary>
	/// LisBarCodeForm object for NHibernate mapped table 'Lis_BarCodeForm'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "样本单", ClassCName = "LisBarCodeForm", ShortCode = "LisBarCodeForm", Desc = "样本单")]
	public class LisBarCodeForm : BaseEntity
	{
		#region Member Variables

		protected DateTime? _partitionDate;
		protected string _barCode;
		protected DateTime? _orderExecTime;
		protected long? _hospitalID;
		protected long? _execDeptID;
		protected long? _destinationID;
		protected int _printTimes;
		protected int _isAffirm;
		protected int _isPrep;
		protected int _barCodeFlag;
		protected long? _barCodeStatusID;
		protected string _barCodeCurrentStatus;
		protected int _isSendPreSystem;
		protected int _receiveFlag;
		protected int _dispenseFlag;
		protected DateTime? _affirmTime;
		protected DateTime? _printTime;
		protected DateTime? _collectTime;
		protected DateTime? _sendTime;
		protected DateTime? _arriveTime;
		protected DateTime? _inceptTime;
		protected DateTime? _groupInceptTime;
		protected DateTime? _rejectTime;
		protected DateTime? _allowTime;
		protected DateTime? _receiveTime;
		protected DateTime? _tranTime;
		protected DateTime? _reportConfirmTime;
		protected DateTime? _reportCheckTime;
		protected DateTime? _reportSendTime;
		protected DateTime? _repotPrintTimeLab;
		protected DateTime? _repotPrintTimeSelf;
		protected DateTime? _repotPrintTimeClinical;
		protected DateTime? _abortTime;
		protected long? _samplingGroupID;
		protected string _color;
		protected string _colorValue;
		protected long? _sampleTypeID;
		protected int _isUrgent;
		protected string _parItemCName;
		protected double _sampleCap;
		protected string _collectPart;
		protected int _collectTimes;
		protected long? _clientID;
		protected string _collectPackNo;
		protected string _autoUnionSNo;
		protected int _isAutoUnion;
		protected double _charge;
		protected int _chargeFlag;
		protected DateTime? _dataUpdateTime;
		protected LisOrderForm _lisOrderForm;
		protected LisPatient _lisPatient;

		#endregion

		#region Constructors

		public LisBarCodeForm() { }

		public LisBarCodeForm(long labID, DateTime partitionDate, string barCode, DateTime orderExecTime, long hospitalID, long execDeptID, long destinationID, int printTimes, int isAffirm, int isPrep, int barCodeFlag, long barCodeStatusID, string barCodeCurrentStatus, int isSendPreSystem, int receiveFlag, int dispenseFlag, DateTime affirmTime, DateTime printTime, DateTime collectTime, DateTime sendTime, DateTime arriveTime, DateTime inceptTime, DateTime groupInceptTime, DateTime rejectTime, DateTime allowTime, DateTime receiveTime, DateTime tranTime, DateTime reportConfirmTime, DateTime reportCheckTime, DateTime reportSendTime, DateTime repotPrintTimeLab, DateTime repotPrintTimeSelf, DateTime repotPrintTimeClinical, DateTime abortTime, long samplingGroupID, string color, string colorValue, long sampleTypeID, int isUrgent, string parItemCName, double sampleCap, string collectPart, int collectTimes, long clientID, string collectPackNo, string autoUnionSNo, int isAutoUnion, double charge, int chargeFlag, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LisOrderForm lisOrderForm, LisPatient lisPatient)
		{
			this._labID = labID;
			this._partitionDate = partitionDate;
			this._barCode = barCode;
			this._orderExecTime = orderExecTime;
			this._hospitalID = hospitalID;
			this._execDeptID = execDeptID;
			this._destinationID = destinationID;
			this._printTimes = printTimes;
			this._isAffirm = isAffirm;
			this._isPrep = isPrep;
			this._barCodeFlag = barCodeFlag;
			this._barCodeStatusID = barCodeStatusID;
			this._barCodeCurrentStatus = barCodeCurrentStatus;
			this._isSendPreSystem = isSendPreSystem;
			this._receiveFlag = receiveFlag;
			this._dispenseFlag = dispenseFlag;
			this._affirmTime = affirmTime;
			this._printTime = printTime;
			this._collectTime = collectTime;
			this._sendTime = sendTime;
			this._arriveTime = arriveTime;
			this._inceptTime = inceptTime;
			this._groupInceptTime = groupInceptTime;
			this._rejectTime = rejectTime;
			this._allowTime = allowTime;
			this._receiveTime = receiveTime;
			this._tranTime = tranTime;
			this._reportConfirmTime = reportConfirmTime;
			this._reportCheckTime = reportCheckTime;
			this._reportSendTime = reportSendTime;
			this._repotPrintTimeLab = repotPrintTimeLab;
			this._repotPrintTimeSelf = repotPrintTimeSelf;
			this._repotPrintTimeClinical = repotPrintTimeClinical;
			this._abortTime = abortTime;
			this._samplingGroupID = samplingGroupID;
			this._color = color;
			this._colorValue = colorValue;
			this._sampleTypeID = sampleTypeID;
			this._isUrgent = isUrgent;
			this._parItemCName = parItemCName;
			this._sampleCap = sampleCap;
			this._collectPart = collectPart;
			this._collectTimes = collectTimes;
			this._clientID = clientID;
			this._collectPackNo = collectPackNo;
			this._autoUnionSNo = autoUnionSNo;
			this._isAutoUnion = isAutoUnion;
			this._charge = charge;
			this._chargeFlag = chargeFlag;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lisOrderForm = lisOrderForm;
			this._lisPatient = lisPatient;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "分区日期", ShortCode = "PartitionDate", Desc = "分区日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PartitionDate
		{
			get { return _partitionDate; }
			set { _partitionDate = value; }
		}

		[DataMember]
		[DataDesc(CName = "条码号", ShortCode = "BarCode", Desc = "条码号", ContextType = SysDic.All, Length = 100)]
		public virtual string BarCode
		{
			get { return _barCode; }
			set { _barCode = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "医嘱执行时间", ShortCode = "OrderExecTime", Desc = "医嘱执行时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? OrderExecTime
		{
			get { return _orderExecTime; }
			set { _orderExecTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "院区ID", ShortCode = "HospitalID", Desc = "院区ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? HospitalID
		{
			get { return _hospitalID; }
			set { _hospitalID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "样本状态ID", ShortCode = "ExecDeptID", Desc = "样本状态ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ExecDeptID
		{
			get { return _execDeptID; }
			set { _execDeptID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "送检目的地ID", ShortCode = "DestinationID", Desc = "送检目的地ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DestinationID
		{
			get { return _destinationID; }
			set { _destinationID = value; }
		}

		[DataMember]
		[DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
		public virtual int PrintTimes
		{
			get { return _printTimes; }
			set { _printTimes = value; }
		}

		[DataMember]
		[DataDesc(CName = "确认标识", ShortCode = "IsAffirm", Desc = "确认标识", ContextType = SysDic.All, Length = 4)]
		public virtual int IsAffirm
		{
			get { return _isAffirm; }
			set { _isAffirm = value; }
		}

		[DataMember]
		[DataDesc(CName = "是否预制条码", ShortCode = "IsPrep", Desc = "是否预制条码", ContextType = SysDic.All, Length = 4)]
		public virtual int IsPrep
		{
			get { return _isPrep; }
			set { _isPrep = value; }
		}

		[DataMember]
		[DataDesc(CName = "条码标志", ShortCode = "BarCodeFlag", Desc = "条码标志", ContextType = SysDic.All, Length = 4)]
		public virtual int BarCodeFlag
		{
			get { return _barCodeFlag; }
			set { _barCodeFlag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "状态ID", ShortCode = "BarCodeStatusID", Desc = "状态ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BarCodeStatusID
		{
			get { return _barCodeStatusID; }
			set { _barCodeStatusID = value; }
		}

		[DataMember]
		[DataDesc(CName = "当前状态", ShortCode = "BarCodeCurrentStatus", Desc = "当前状态", ContextType = SysDic.All, Length = 100)]
		public virtual string BarCodeCurrentStatus
		{
			get { return _barCodeCurrentStatus; }
			set { _barCodeCurrentStatus = value; }
		}

		[DataMember]
		[DataDesc(CName = "发送前处理标识", ShortCode = "IsSendPreSystem", Desc = "发送前处理标识", ContextType = SysDic.All, Length = 4)]
		public virtual int IsSendPreSystem
		{
			get { return _isSendPreSystem; }
			set { _isSendPreSystem = value; }
		}

		[DataMember]
		[DataDesc(CName = "核收标识", ShortCode = "ReceiveFlag", Desc = "核收标识", ContextType = SysDic.All, Length = 4)]
		public virtual int ReceiveFlag
		{
			get { return _receiveFlag; }
			set { _receiveFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "分发标识", ShortCode = "DispenseFlag", Desc = "分发标识", ContextType = SysDic.All, Length = 4)]
		public virtual int DispenseFlag
		{
			get { return _dispenseFlag; }
			set { _dispenseFlag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "AffirmTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AffirmTime
		{
			get { return _affirmTime; }
			set { _affirmTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "PrintTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PrintTime
		{
			get { return _printTime; }
			set { _printTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "CollectTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectTime
		{
			get { return _collectTime; }
			set { _collectTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "SendTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? SendTime
		{
			get { return _sendTime; }
			set { _sendTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "ArriveTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ArriveTime
		{
			get { return _arriveTime; }
			set { _arriveTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "InceptTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InceptTime
		{
			get { return _inceptTime; }
			set { _inceptTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "GroupInceptTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GroupInceptTime
		{
			get { return _groupInceptTime; }
			set { _groupInceptTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "RejectTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? RejectTime
		{
			get { return _rejectTime; }
			set { _rejectTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "AllowTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AllowTime
		{
			get { return _allowTime; }
			set { _allowTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "ReceiveTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReceiveTime
		{
			get { return _receiveTime; }
			set { _receiveTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "TranTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TranTime
		{
			get { return _tranTime; }
			set { _tranTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "ReportConfirmTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReportConfirmTime
		{
			get { return _reportConfirmTime; }
			set { _reportConfirmTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "ReportCheckTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReportCheckTime
		{
			get { return _reportCheckTime; }
			set { _reportCheckTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "ReportSendTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReportSendTime
		{
			get { return _reportSendTime; }
			set { _reportSendTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "RepotPrintTimeLab", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? RepotPrintTimeLab
		{
			get { return _repotPrintTimeLab; }
			set { _repotPrintTimeLab = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "RepotPrintTimeSelf", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? RepotPrintTimeSelf
		{
			get { return _repotPrintTimeSelf; }
			set { _repotPrintTimeSelf = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "RepotPrintTimeClinical", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? RepotPrintTimeClinical
		{
			get { return _repotPrintTimeClinical; }
			set { _repotPrintTimeClinical = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "AbortTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AbortTime
		{
			get { return _abortTime; }
			set { _abortTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样组ID", ShortCode = "SamplingGroupID", Desc = "采样组ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? SamplingGroupID
		{
			get { return _samplingGroupID; }
			set { _samplingGroupID = value; }
		}

		[DataMember]
		[DataDesc(CName = "颜色描述", ShortCode = "Color", Desc = "颜色描述", ContextType = SysDic.All, Length = 100)]
		public virtual string Color
		{
			get { return _color; }
			set { _color = value; }
		}

		[DataMember]
		[DataDesc(CName = "颜色值", ShortCode = "ColorValue", Desc = "颜色值", ContextType = SysDic.All, Length = 50)]
		public virtual string ColorValue
		{
			get { return _colorValue; }
			set { _colorValue = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "样本类型ID", ShortCode = "SampleTypeID", Desc = "样本类型ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? SampleTypeID
		{
			get { return _sampleTypeID; }
			set { _sampleTypeID = value; }
		}

		[DataMember]
		[DataDesc(CName = "是否急查", ShortCode = "IsUrgent", Desc = "是否急查", ContextType = SysDic.All, Length = 4)]
		public virtual int IsUrgent
		{
			get { return _isUrgent; }
			set { _isUrgent = value; }
		}

		[DataMember]
		[DataDesc(CName = "项目名称", ShortCode = "ParItemCName", Desc = "项目名称", ContextType = SysDic.All, Length = 2000)]
		public virtual string ParItemCName
		{
			get { return _parItemCName; }
			set { _parItemCName = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样量", ShortCode = "SampleCap", Desc = "采样量", ContextType = SysDic.All, Length = 8)]
		public virtual double SampleCap
		{
			get { return _sampleCap; }
			set { _sampleCap = value; }
		}

		[DataMember]
		[DataDesc(CName = "采样部位", ShortCode = "CollectPart", Desc = "采样部位", ContextType = SysDic.All, Length = 200)]
		public virtual string CollectPart
		{
			get { return _collectPart; }
			set { _collectPart = value; }
		}

		[DataMember]
		[DataDesc(CName = "采样次数", ShortCode = "CollectTimes", Desc = "采样次数", ContextType = SysDic.All, Length = 4)]
		public virtual int CollectTimes
		{
			get { return _collectTimes; }
			set { _collectTimes = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "客户编号ID", ShortCode = "ClientID", Desc = "客户编号ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ClientID
		{
			get { return _clientID; }
			set { _clientID = value; }
		}

		[DataMember]
		[DataDesc(CName = "打包号", ShortCode = "CollectPackNo", Desc = "打包号", ContextType = SysDic.All, Length = 100)]
		public virtual string CollectPackNo
		{
			get { return _collectPackNo; }
			set { _collectPackNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告合并关键字", ShortCode = "AutoUnionSNo", Desc = "报告合并关键字", ContextType = SysDic.All, Length = 100)]
		public virtual string AutoUnionSNo
		{
			get { return _autoUnionSNo; }
			set { _autoUnionSNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "报告合并标识", ShortCode = "IsAutoUnion", Desc = "报告合并标识", ContextType = SysDic.All, Length = 4)]
		public virtual int IsAutoUnion
		{
			get { return _isAutoUnion; }
			set { _isAutoUnion = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "Charge", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual double Charge
		{
			get { return _charge; }
			set { _charge = value; }
		}

		[DataMember]
		[DataDesc(CName = "0未计费", ShortCode = "ChargeFlag", Desc = "0未计费", ContextType = SysDic.All, Length = 4)]
		public virtual int ChargeFlag
		{
			get { return _chargeFlag; }
			set { _chargeFlag = value; }
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
		[DataDesc(CName = "", ShortCode = "LisOrderForm", Desc = "")]
		public virtual LisOrderForm LisOrderForm
		{
			get { return _lisOrderForm; }
			set { _lisOrderForm = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "LisPatient", Desc = "")]
		public virtual LisPatient LisPatient
		{
			get { return _lisPatient; }
			set { _lisPatient = value; }
		}


		#endregion
	}
	#endregion
}