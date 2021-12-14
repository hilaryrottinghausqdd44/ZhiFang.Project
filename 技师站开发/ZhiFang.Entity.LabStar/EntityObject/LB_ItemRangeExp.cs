using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBItemRangeExp

	/// <summary>
	/// 项目参考范围扩展,LBItemRangeExp object for NHibernate mapped table 'LB_ItemRangeExp'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "项目参考范围扩展", ClassCName = "LBItemRangeExp", ShortCode = "LBItemRangeExp", Desc = "项目参考范围扩展")]
	public class LBItemRangeExp : BaseEntity
	{
		#region Member Variables

		protected int _judgeType;
		protected string _judgeValue;
		protected string _resultStatus;
		protected string _resultReport;
		protected string _resultComment;
		protected bool _isAddReport;
		protected int _expReport;
		protected string _expComment;
		protected string _alarmColor;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected bool _bAlarmColor;
		protected int? _alarmLevel;
		protected LBItem _lBItem;

		#endregion

		#region Constructors

		public LBItemRangeExp() { }

		public LBItemRangeExp(int judgeType, string judgeValue, string resultStatus, string resultReport, string resultComment, bool isAddReport, int expReport, string expComment, string alarmColor, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool bAlarmColor, int alarmLevel, LBItem lBItem)
		{
			this._judgeType = judgeType;
			this._judgeValue = judgeValue;
			this._resultStatus = resultStatus;
			this._resultReport = resultReport;
			this._resultComment = resultComment;
			this._isAddReport = isAddReport;
			this._expReport = expReport;
			this._expComment = expComment;
			this._alarmColor = alarmColor;
			this._dispOrder = dispOrder;
			this._labID = labID;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bAlarmColor = bAlarmColor;
			this._alarmLevel = alarmLevel;
			this._lBItem = lBItem;
		}

		#endregion

		#region Public Properties


		/// <summary>
		/// 判断类型
		/// </summary>
		[DataMember]
		[DataDesc(CName = "判断类型", ShortCode = "JudgeType", Desc = "判断类型", ContextType = SysDic.All, Length = 4)]
		public virtual int JudgeType
		{
			get { return _judgeType; }
			set { _judgeType = value; }
		}

		/// <summary>
		/// 判定值
		/// </summary>
		[DataMember]
		[DataDesc(CName = "判定值", ShortCode = "JudgeValue", Desc = "判定值", ContextType = SysDic.All, Length = 300)]
		public virtual string JudgeValue
		{
			get { return _judgeValue; }
			set { _judgeValue = value; }
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
		/// 报告值
		/// </summary>
		[DataMember]
		[DataDesc(CName = "报告值", ShortCode = "ResultReport", Desc = "报告值", ContextType = SysDic.All, Length = 500)]
		public virtual string ResultReport
		{
			get { return _resultReport; }
			set { _resultReport = value; }
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
		/// 报告值处理
		/// </summary>
		[DataMember]
		[DataDesc(CName = "报告值处理", ShortCode = "IsAddReport", Desc = "报告值处理", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsAddReport
		{
			get { return _isAddReport; }
			set { _isAddReport = value; }
		}

		/// <summary>
		/// 报告扩展
		/// </summary>
		[DataMember]
		[DataDesc(CName = "报告扩展", ShortCode = "ExpReport", Desc = "报告扩展", ContextType = SysDic.All, Length = 4)]
		public virtual int ExpReport
		{
			get { return _expReport; }
			set { _expReport = value; }
		}

		/// <summary>
		/// 扩展说明
		/// </summary>
		[DataMember]
		[DataDesc(CName = "扩展说明", ShortCode = "ExpComment", Desc = "扩展说明", ContextType = SysDic.All, Length = 500)]
		public virtual string ExpComment
		{
			get { return _expComment; }
			set { _expComment = value; }
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
		/// 判定次序
		/// </summary>
		[DataMember]
		[DataDesc(CName = "判定次序", ShortCode = "DispOrder", Desc = "判定次序", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
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
		/// 结果警示级别
		/// </summary>
		[DataMember]
		[DataDesc(CName = "结果警示级别", ShortCode = "AlarmLevel", Desc = "结果警示级别", ContextType = SysDic.All, Length = 4)]
		public virtual int? AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

		/// <summary>
		/// 检验项目
		/// </summary>
		[DataMember]
		[DataDesc(CName = "检验项目", ShortCode = "LBItem", Desc = "")]
		public virtual LBItem LBItem
		{
			get { return _lBItem; }
			set { _lBItem = value; }
		}


		#endregion
	}
	#endregion
}