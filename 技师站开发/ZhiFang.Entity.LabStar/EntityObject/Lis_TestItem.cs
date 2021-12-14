using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LisTestItem

	/// <summary>
	/// 检验结果,LisTestItem object for NHibernate mapped table 'Lis_TestItem'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "检验结果", ClassCName = "LisTestItem", ShortCode = "LisTestItem", Desc = "检验结果")]
	public class LisTestItem : BaseEntity
	{
		#region Member Variables

		protected long? _ordersItemID;
		protected long? _barCodesItemID;
		protected DateTime? _gTestDate;
		protected int _mainStatusID;
		protected long? _statusID;
		protected long? _reportStatusID;
		protected int _iSource;
		protected int _iExamine;
		protected string _origlValue;
		protected int _valueType;
		protected string _reportValue;
		protected string _resultComment;
		protected string _resultStatus;
		protected double? _quanValue;
		protected bool _bAlarmColor;
		protected string _alarmColor;
		protected bool _isReport;
		protected string _eSend;
		protected double? _quanValue2;
		protected double? _quanValue3;
		protected string _testMethod;
		protected string _unit;
		protected string _refRange;
		protected string _eResultStatus;
		protected string _eResultAlarm;
		protected string _redoDesc;
		protected long? _preResultID;
		protected DateTime? _preGTestDate;
		protected string _preValue;
		protected string _preValueComp;
		protected string _preCompStatus;
		protected int _hisResultCount;
		protected long? _preTestItemID2;
		protected DateTime? _preGTestDate2;
		protected string _preValue2;
		protected long? _preTestItemID3;
		protected DateTime? _preGTestDate3;
		protected string _preValue3;
		protected int _alarmLevel;
		protected string _alarmInfo;
		protected long? _operaterID;
		protected int _dispOrder;
		protected DateTime? _testTime;
		protected DateTime? _dataUpdateTime;
		protected long? _equipID;
		protected long? _equipResultID;
		protected string _eReportValue;
		protected string _eTestComment;
		protected string _eReagentInfo;
		protected int _eAlarmState;
		protected int _iDoWith;
		protected int _iResultState;
		protected int _iCommState;
		protected string _resultStatusCode;
		protected int _redoStatus;
		protected string _redoValues;
		protected string _reportInfo;
		protected string _reportInfoPrint;
		protected LisBarCodeItem _lisBarCodeItem;
		protected LisOrderItem _lisOrderItem;
		protected LisTestForm _lisTestForm;
		protected LBItem _lBItem;
		protected LBItem _pLBItem;

		#endregion

		#region Constructors

		public LisTestItem() { }

		public LisTestItem(long ordersItemID, long barCodesItemID, DateTime gTestDate, int mainStatusID, long statusID, long reportStatusID, int iSource, int iExamine, string origlValue, int valueType, string reportValue, string resultComment, string resultStatus, double quanValue, bool bAlarmColor, string alarmColor, bool isReport, string eSend, double quanValue2, double quanValue3, string testMethod, string unit, string refRange, string eResultStatus, string eResultAlarm, string redoDesc, long preResultID, DateTime preGTestDate, string preValue, string preValueComp, string preCompStatus, int hisResultCount, long preTestItemID2, DateTime preGTestDate2, string preValue2, long preTestItemID3, DateTime preGTestDate3, string preValue3, int alarmLevel, string alarmInfo, long operaterID, int dispOrder, long labID, DateTime testTime, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, long equipID, long equipResultID, string eReportValue, string eTestComment, string eReagentInfo, int eAlarmState, int iDoWith, int iResultState, int iCommState, string resultStatusCode, int redoStatus, string redoValues, string reportInfo, string reportInfoPrint, LisBarCodeItem lisBarCodeItem, LisOrderItem lisOrderItem, LisTestForm lisTestForm, LBItem lBItem, LBItem pLBItem)
		{
			this._ordersItemID = ordersItemID;
			this._barCodesItemID = barCodesItemID;
			this._gTestDate = gTestDate;
			this._mainStatusID = mainStatusID;
			this._statusID = statusID;
			this._reportStatusID = reportStatusID;
			this._iSource = iSource;
			this._iExamine = iExamine;
			this._origlValue = origlValue;
			this._valueType = valueType;
			this._reportValue = reportValue;
			this._resultComment = resultComment;
			this._resultStatus = resultStatus;
			this._quanValue = quanValue;
			this._bAlarmColor = bAlarmColor;
			this._alarmColor = alarmColor;
			this._isReport = isReport;
			this._eSend = eSend;
			this._quanValue2 = quanValue2;
			this._quanValue3 = quanValue3;
			this._testMethod = testMethod;
			this._unit = unit;
			this._refRange = refRange;
			this._eResultStatus = eResultStatus;
			this._eResultAlarm = eResultAlarm;
			this._redoDesc = redoDesc;
			this._preResultID = preResultID;
			this._preGTestDate = preGTestDate;
			this._preValue = preValue;
			this._preValueComp = preValueComp;
			this._preCompStatus = preCompStatus;
			this._hisResultCount = hisResultCount;
			this._preTestItemID2 = preTestItemID2;
			this._preGTestDate2 = preGTestDate2;
			this._preValue2 = preValue2;
			this._preTestItemID3 = preTestItemID3;
			this._preGTestDate3 = preGTestDate3;
			this._preValue3 = preValue3;
			this._alarmLevel = alarmLevel;
			this._alarmInfo = alarmInfo;
			this._operaterID = operaterID;
			this._dispOrder = dispOrder;
			this._labID = labID;
			this._testTime = testTime;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._equipID = equipID;
			this._equipResultID = equipResultID;
			this._eReportValue = eReportValue;
			this._eTestComment = eTestComment;
			this._eReagentInfo = eReagentInfo;
			this._eAlarmState = eAlarmState;
			this._iDoWith = iDoWith;
			this._iResultState = iResultState;
			this._iCommState = iCommState;
			this._resultStatusCode = resultStatusCode;
			this._redoStatus = redoStatus;
			this._redoValues = redoValues;
			this._reportInfo = reportInfo;
			this._reportInfoPrint = reportInfoPrint;
			this._lisBarCodeItem = lisBarCodeItem;
			this._lisOrderItem = lisOrderItem;
			this._lisTestForm = lisTestForm;
			this._lBItem = lBItem;
			this._pLBItem = pLBItem;
		}

		#endregion

		#region Public Properties


		/// <summary>
		/// 医嘱项目对应_项目ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "医嘱项目对应_项目ID", ShortCode = "OrdersItemID", Desc = "医嘱项目对应_项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? OrdersItemID
		{
			get { return _ordersItemID; }
			set { _ordersItemID = value; }
		}

		/// <summary>
		/// 采样项目对应_项目ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样项目对应_项目ID", ShortCode = "BarCodesItemID", Desc = "采样项目对应_项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BarCodesItemID
		{
			get { return _barCodesItemID; }
			set { _barCodesItemID = value; }
		}

		/// <summary>
		/// 检测日期
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检测日期", ShortCode = "GTestDate", Desc = "检测日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GTestDate
		{
			get { return _gTestDate; }
			set { _gTestDate = value; }
		}

		/// <summary>
		/// 主状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "主状态", ShortCode = "MainStatusID", Desc = "主状态", ContextType = SysDic.All, Length = 4)]
		public virtual int MainStatusID
		{
			get { return _mainStatusID; }
			set { _mainStatusID = value; }
		}

		/// <summary>
		/// 过程状态标志
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "过程状态标志", ShortCode = "StatusID", Desc = "过程状态标志", ContextType = SysDic.All, Length = 8)]
		public virtual long? StatusID
		{
			get { return _statusID; }
			set { _statusID = value; }
		}

		/// <summary>
		/// 结果与报告状态标志
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "结果与报告状态标志", ShortCode = "ReportStatusID", Desc = "结果与报告状态标志", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReportStatusID
		{
			get { return _reportStatusID; }
			set { _reportStatusID = value; }
		}

		/// <summary>
		/// 项目来源
		/// </summary>
		[DataMember]
		[DataDesc(CName = "项目来源", ShortCode = "ISource", Desc = "项目来源", ContextType = SysDic.All, Length = 4)]
		public virtual int ISource
		{
			get { return _iSource; }
			set { _iSource = value; }
		}

		/// <summary>
		/// 检查次数
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检查次数", ShortCode = "IExamine", Desc = "检查次数", ContextType = SysDic.All, Length = 4)]
		public virtual int IExamine
		{
			get { return _iExamine; }
			set { _iExamine = value; }
		}

		/// <summary>
		/// 仪器原始数值
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器原始数值", ShortCode = "OriglValue", Desc = "仪器原始数值", ContextType = SysDic.All, Length = 300)]
		public virtual string OriglValue
		{
			get { return _origlValue; }
			set { _origlValue = value; }
		}

		/// <summary>
		/// 结果类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "结果类型", ShortCode = "ValueType", Desc = "结果类型", ContextType = SysDic.All, Length = 4)]
		public virtual int ValueType
		{
			get { return _valueType; }
			set { _valueType = value; }
		}

		/// <summary>
		/// 报告值
		/// </summary>
		[DataMember]
		[DataDesc(CName = "报告值", ShortCode = "ReportValue", Desc = "报告值", ContextType = SysDic.All, Length = 300)]
		public virtual string ReportValue
		{
			get { return _reportValue; }
			set { _reportValue = value; }
		}

		/// <summary>
		/// 结果说明
		/// </summary>
		[DataMember]
		[DataDesc(CName = "结果说明", ShortCode = "ResultComment", Desc = "结果说明", ContextType = SysDic.All, Length = 1000)]
		public virtual string ResultComment
		{
			get { return _resultComment; }
			set { _resultComment = value; }
		}

		/// <summary>
		/// 检验结果状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验结果状态", ShortCode = "ResultStatus", Desc = "检验结果状态", ContextType = SysDic.All, Length = 20)]
		public virtual string ResultStatus
		{
			get { return _resultStatus; }
			set { _resultStatus = value; }
		}

		/// <summary>
		/// 定量结果
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "定量结果", ShortCode = "QuanValue", Desc = "定量结果", ContextType = SysDic.All, Length = 8)]
		public virtual double? QuanValue
		{
			get { return _quanValue; }
			set { _quanValue = value; }
		}

		/// <summary>
		/// 采用特殊提示色
		/// </summary>
		[DataMember]
		[DataDesc(CName = "采用特殊提示色", ShortCode = "BAlarmColor", Desc = "采用特殊提示色", ContextType = SysDic.All, Length = 1)]
		public virtual bool BAlarmColor
		{
			get { return _bAlarmColor; }
			set { _bAlarmColor = value; }
		}

		/// <summary>
		/// 结果警示特殊颜色
		/// </summary>
		[DataMember]
		[DataDesc(CName = "结果警示特殊颜色", ShortCode = "AlarmColor", Desc = "结果警示特殊颜色", ContextType = SysDic.All, Length = 50)]
		public virtual string AlarmColor
		{
			get { return _alarmColor; }
			set { _alarmColor = value; }
		}

		/// <summary>
		/// 是否报告
		/// </summary>
		[DataMember]
		[DataDesc(CName = "是否报告", ShortCode = "IsReport", Desc = "是否报告", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsReport
		{
			get { return _isReport; }
			set { _isReport = value; }
		}

		/// <summary>
		/// 仪器审核信息
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器审核信息", ShortCode = "ESend", Desc = "仪器审核信息", ContextType = SysDic.All, Length = 100)]
		public virtual string ESend
		{
			get { return _eSend; }
			set { _eSend = value; }
		}

		/// <summary>
		/// 定量辅助结果2
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "定量辅助结果2", ShortCode = "QuanValue2", Desc = "定量辅助结果2", ContextType = SysDic.All, Length = 8)]
		public virtual double? QuanValue2
		{
			get { return _quanValue2; }
			set { _quanValue2 = value; }
		}

		/// <summary>
		/// 定量辅助结果3
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "定量辅助结果3", ShortCode = "QuanValue3", Desc = "定量辅助结果3", ContextType = SysDic.All, Length = 8)]
		public virtual double? QuanValue3
		{
			get { return _quanValue3; }
			set { _quanValue3 = value; }
		}

		/// <summary>
		/// 检测方法
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检测方法", ShortCode = "TestMethod", Desc = "检测方法", ContextType = SysDic.All, Length = 50)]
		public virtual string TestMethod
		{
			get { return _testMethod; }
			set { _testMethod = value; }
		}

		/// <summary>
		/// 结果单位
		/// </summary>
		[DataMember]
		[DataDesc(CName = "结果单位", ShortCode = "Unit", Desc = "结果单位", ContextType = SysDic.All, Length = 50)]
		public virtual string Unit
		{
			get { return _unit; }
			set { _unit = value; }
		}

		/// <summary>
		/// 参考范围
		/// </summary>
		[DataMember]
		[DataDesc(CName = "参考范围", ShortCode = "RefRange", Desc = "参考范围", ContextType = SysDic.All, Length = 400)]
		public virtual string RefRange
		{
			get { return _refRange; }
			set { _refRange = value; }
		}

		/// <summary>
		/// 仪器结果状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器结果状态", ShortCode = "EResultStatus", Desc = "仪器结果状态", ContextType = SysDic.All, Length = 50)]
		public virtual string EResultStatus
		{
			get { return _eResultStatus; }
			set { _eResultStatus = value; }
		}

		/// <summary>
		/// 仪器结果警告
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器结果警告", ShortCode = "EResultAlarm", Desc = "仪器结果警告", ContextType = SysDic.All, Length = 50)]
		public virtual string EResultAlarm
		{
			get { return _eResultAlarm; }
			set { _eResultAlarm = value; }
		}

		/// <summary>
		/// 复检原因
		/// </summary>
		[DataMember]
		[DataDesc(CName = "复检原因", ShortCode = "RedoDesc", Desc = "复检原因", ContextType = SysDic.All, Length = 200)]
		public virtual string RedoDesc
		{
			get { return _redoDesc; }
			set { _redoDesc = value; }
		}

		/// <summary>
		/// 前值结果ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "前值结果ID", ShortCode = "PreResultID", Desc = "前值结果ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? PreResultID
		{
			get { return _preResultID; }
			set { _preResultID = value; }
		}

		/// <summary>
		/// 前值检验日期
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "前值检验日期", ShortCode = "PreGTestDate", Desc = "前值检验日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PreGTestDate
		{
			get { return _preGTestDate; }
			set { _preGTestDate = value; }
		}

		/// <summary>
		/// 前报告值
		/// </summary>
		[DataMember]
		[DataDesc(CName = "前报告值", ShortCode = "PreValue", Desc = "前报告值", ContextType = SysDic.All, Length = 400)]
		public virtual string PreValue
		{
			get { return _preValue; }
			set { _preValue = value; }
		}

		/// <summary>
		/// 前值对比
		/// </summary>
		[DataMember]
		[DataDesc(CName = "前值对比", ShortCode = "PreValueComp", Desc = "前值对比", ContextType = SysDic.All, Length = 50)]
		public virtual string PreValueComp
		{
			get { return _preValueComp; }
			set { _preValueComp = value; }
		}

		/// <summary>
		/// 前值对比状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "前值对比状态", ShortCode = "PreCompStatus", Desc = "前值对比状态", ContextType = SysDic.All, Length = 50)]
		public virtual string PreCompStatus
		{
			get { return _preCompStatus; }
			set { _preCompStatus = value; }
		}

		/// <summary>
		/// 历史结果数量
		/// </summary>
		[DataMember]
		[DataDesc(CName = "历史结果数量", ShortCode = "HisResultCount", Desc = "历史结果数量", ContextType = SysDic.All, Length = 4)]
		public virtual int HisResultCount
		{
			get { return _hisResultCount; }
			set { _hisResultCount = value; }
		}

		/// <summary>
		/// 前值结果ID2
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "前值结果ID2", ShortCode = "PreTestItemID2", Desc = "前值结果ID2", ContextType = SysDic.All, Length = 8)]
		public virtual long? PreTestItemID2
		{
			get { return _preTestItemID2; }
			set { _preTestItemID2 = value; }
		}

		/// <summary>
		/// 前值检验日期2
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "前值检验日期2", ShortCode = "PreGTestDate2", Desc = "前值检验日期2", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PreGTestDate2
		{
			get { return _preGTestDate2; }
			set { _preGTestDate2 = value; }
		}

		/// <summary>
		/// 前报告值2
		/// </summary>
		[DataMember]
		[DataDesc(CName = "前报告值2", ShortCode = "PreValue2", Desc = "前报告值2", ContextType = SysDic.All, Length = 400)]
		public virtual string PreValue2
		{
			get { return _preValue2; }
			set { _preValue2 = value; }
		}

		/// <summary>
		/// 前值结果ID3
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "前值结果ID3", ShortCode = "PreTestItemID3", Desc = "前值结果ID3", ContextType = SysDic.All, Length = 8)]
		public virtual long? PreTestItemID3
		{
			get { return _preTestItemID3; }
			set { _preTestItemID3 = value; }
		}

		/// <summary>
		/// 前值检验日期3
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "前值检验日期3", ShortCode = "PreGTestDate3", Desc = "前值检验日期3", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PreGTestDate3
		{
			get { return _preGTestDate3; }
			set { _preGTestDate3 = value; }
		}

		/// <summary>
		/// 前报告值3
		/// </summary>
		[DataMember]
		[DataDesc(CName = "前报告值3", ShortCode = "PreValue3", Desc = "前报告值3", ContextType = SysDic.All, Length = 400)]
		public virtual string PreValue3
		{
			get { return _preValue3; }
			set { _preValue3 = value; }
		}

		/// <summary>
		/// 结果警示级别
		/// </summary>
		[DataMember]
		[DataDesc(CName = "结果警示级别", ShortCode = "AlarmLevel", Desc = "结果警示级别", ContextType = SysDic.All, Length = 4)]
		public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

		/// <summary>
		/// 结果警示信息
		/// </summary>
		[DataMember]
		[DataDesc(CName = "结果警示信息", ShortCode = "AlarmInfo", Desc = "结果警示信息", ContextType = SysDic.All, Length = 50)]
		public virtual string AlarmInfo
		{
			get { return _alarmInfo; }
			set { _alarmInfo = value; }
		}

		/// <summary>
		/// 操作者ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "操作者ID", ShortCode = "OperaterID", Desc = "操作者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? OperaterID
		{
			get { return _operaterID; }
			set { _operaterID = value; }
		}

		/// <summary>
		/// 显示次序
		/// </summary>
		[DataMember]
		[DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		/// <summary>
		/// 检验时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "检验时间", ShortCode = "TestTime", Desc = "检验时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TestTime
		{
			get { return _testTime; }
			set { _testTime = value; }
		}

		/// <summary>
		/// 数据更新时间
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		/// <summary>
		/// 仪器ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "仪器ID", ShortCode = "EquipID", Desc = "仪器ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipID
		{
			get { return _equipID; }
			set { _equipID = value; }
		}

		/// <summary>
		/// 仪器样本项目ID
		/// </summary>
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "仪器样本项目ID", ShortCode = "EquipResultID", Desc = "仪器样本项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipResultID
		{
			get { return _equipResultID; }
			set { _equipResultID = value; }
		}

		/// <summary>
		/// 仪器结果
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器结果", ShortCode = "EReportValue", Desc = "仪器结果", ContextType = SysDic.All, Length = 300)]
		public virtual string EReportValue
		{
			get { return _eReportValue; }
			set { _eReportValue = value; }
		}

		/// <summary>
		/// 仪器结果备注
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器结果备注", ShortCode = "ETestComment", Desc = "仪器结果备注", ContextType = SysDic.All, Length = 16)]
		public virtual string ETestComment
		{
			get { return _eTestComment; }
			set { _eTestComment = value; }
		}

		/// <summary>
		/// 仪器试剂信息
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器试剂信息", ShortCode = "EReagentInfo", Desc = "仪器试剂信息", ContextType = SysDic.All, Length = 100)]
		public virtual string EReagentInfo
		{
			get { return _eReagentInfo; }
			set { _eReagentInfo = value; }
		}

		/// <summary>
		/// 仪器结果警示状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "仪器结果警示状态", ShortCode = "EAlarmState", Desc = "仪器结果警示状态", ContextType = SysDic.All, Length = 4)]
		public virtual int EAlarmState
		{
			get { return _eAlarmState; }
			set { _eAlarmState = value; }
		}

		/// <summary>
		/// 处理状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "处理状态", ShortCode = "IDoWith", Desc = "处理状态", ContextType = SysDic.All, Length = 4)]
		public virtual int IDoWith
		{
			get { return _iDoWith; }
			set { _iDoWith = value; }
		}

		/// <summary>
		/// 结果编辑状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "结果编辑状态", ShortCode = "IResultState", Desc = "结果编辑状态", ContextType = SysDic.All, Length = 4)]
		public virtual int IResultState
		{
			get { return _iResultState; }
			set { _iResultState = value; }
		}

		/// <summary>
		/// 通讯状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "通讯状态", ShortCode = "ICommState", Desc = "通讯状态", ContextType = SysDic.All, Length = 4)]
		public virtual int ICommState
		{
			get { return _iCommState; }
			set { _iCommState = value; }
		}

		/// <summary>
		/// 检验结果状态Code
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验结果状态Code", ShortCode = "ResultStatusCode", Desc = "检验结果状态Code", ContextType = SysDic.All, Length = 50)]
		public virtual string ResultStatusCode
		{
			get { return _resultStatusCode; }
			set { _resultStatusCode = value; }
		}

		/// <summary>
		/// 复检状态
		/// </summary>
		[DataMember]
		[DataDesc(CName = "复检状态", ShortCode = "RedoStatus", Desc = "复检状态", ContextType = SysDic.All, Length = 4)]
		public virtual int RedoStatus
		{
			get { return _redoStatus; }
			set { _redoStatus = value; }
		}

		/// <summary>
		/// 复检参考结果
		/// </summary>
		[DataMember]
		[DataDesc(CName = "复检参考结果", ShortCode = "RedoValues", Desc = "复检参考结果", ContextType = SysDic.All, Length = 200)]
		public virtual string RedoValues
		{
			get { return _redoValues; }
			set { _redoValues = value; }
		}

		/// <summary>
		/// 特殊报告值
		/// </summary>
		[DataMember]
		[DataDesc(CName = "特殊报告值", ShortCode = "ReportInfo", Desc = "特殊报告值", ContextType = SysDic.All, Length = -1)]
		public virtual string ReportInfo
		{
			get { return _reportInfo; }
			set { _reportInfo = value; }
		}

		/// <summary>
		/// 特殊报告值打印格式
		/// </summary>
		[DataMember]
		[DataDesc(CName = "特殊报告值打印格式", ShortCode = "ReportInfoPrint", Desc = "特殊报告值打印格式", ContextType = SysDic.All, Length = -1)]
		public virtual string ReportInfoPrint
		{
			get { return _reportInfoPrint; }
			set { _reportInfoPrint = value; }
		}

		/// <summary>
		/// 采样样本单项目
		/// </summary>
		[DataMember]
		[DataDesc(CName = "采样样本单项目", ShortCode = "LisBarCodeItem", Desc = "采样样本单项目")]
		public virtual LisBarCodeItem LisBarCodeItem
		{
			get { return _lisBarCodeItem; }
			set { _lisBarCodeItem = value; }
		}

		/// <summary>
		/// 医嘱项目
		/// </summary>
		[DataMember]
		[DataDesc(CName = "医嘱项目", ShortCode = "LisOrderItem", Desc = "医嘱项目")]
		public virtual LisOrderItem LisOrderItem
		{
			get { return _lisOrderItem; }
			set { _lisOrderItem = value; }
		}

		/// <summary>
		/// 检验单
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验单", ShortCode = "LisTestForm", Desc = "检验单")]
		public virtual LisTestForm LisTestForm
		{
			get { return _lisTestForm; }
			set { _lisTestForm = value; }
		}

		/// <summary>
		/// 检验项目
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验项目", ShortCode = "LBItem", Desc = "检验项目")]
		public virtual LBItem LBItem
		{
			get { return _lBItem; }
			set { _lBItem = value; }
		}

		/// <summary>
		/// 组合项目
		/// </summary>
		[DataMember]
		[DataDesc(CName = "组合项目", ShortCode = "PLBItem", Desc = "组合项目")]
		public virtual LBItem PLBItem
		{
			get { return _pLBItem; }
			set { _pLBItem = value; }
		}


		#endregion
	}
	#endregion
}