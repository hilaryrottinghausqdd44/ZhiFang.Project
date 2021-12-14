using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCDStat

	/// <summary>
	/// QCDStat object for NHibernate mapped table 'QC_D_Stat'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "质控统计", ClassCName = "QCDStat", ShortCode = "QCDStat", Desc = "质控统计")]
	public class QCDStat : BaseEntity
	{
		#region Member Variables
		
        protected int? _yearID;
        protected int? _monthID;
        protected DateTime? _beginDate;
        protected DateTime? _endDate;
        protected double? _qCTarget;
        protected double? _qCSD;
        protected double? _qCCV;
        protected double? _tTarget;
        protected double? _tSD;
        protected double? _tCV;
        protected int? _qCCount;
        protected int? _sKCount;
        protected int? _warningCount;
        protected int? _noUseCount;
        protected int? _zKCount;
        protected double? _qCRValue;
        protected double? _qCMValue;
        protected string _qCComment;
        protected int? _sD1Count;
        protected int? _sD2Count;
        protected int? _sD3Count;
        protected string _lotNo;
        protected DateTime? _dataUpdateTime;
		protected HREmployee _hREmployee;
		protected QCItem _qCItem;

		#endregion

		#region Constructors

		public QCDStat() { }

		public QCDStat( long labID, int yearID, int monthID, DateTime beginDate, DateTime endDate, double qCTarget, double qCSD, double qCCV, double tTarget, double tSD, double tCV, int qCCount, int sKCount, int noUseCount, int zKCount, double qCRValue, double qCMValue, string qCComment, int sD1Count, int sD2Count, int sD3Count, string lotNo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HREmployee hREmployee, QCItem qCItem )
		{
			this._labID = labID;
			this._yearID = yearID;
			this._monthID = monthID;
			this._beginDate = beginDate;
			this._endDate = endDate;
			this._qCTarget = qCTarget;
			this._qCSD = qCSD;
			this._qCCV = qCCV;
			this._tTarget = tTarget;
			this._tSD = tSD;
			this._tCV = tCV;
			this._qCCount = qCCount;
			this._sKCount = sKCount;
			this._noUseCount = noUseCount;
			this._zKCount = zKCount;
			this._qCRValue = qCRValue;
			this._qCMValue = qCMValue;
			this._qCComment = qCComment;
			this._sD1Count = sD1Count;
			this._sD2Count = sD2Count;
			this._sD3Count = sD3Count;
			this._lotNo = lotNo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._hREmployee = hREmployee;
			this._qCItem = qCItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "YearID", ShortCode = "YearID", Desc = "YearID", ContextType = SysDic.All, Length = 4)]
        public virtual int? YearID
		{
			get { return _yearID; }
			set { _yearID = value; }
		}

        [DataMember]
        [DataDesc(CName = "MonthID", ShortCode = "MonthID", Desc = "MonthID", ContextType = SysDic.All, Length = 4)]
        public virtual int? MonthID
		{
			get { return _monthID; }
			set { _monthID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开始日期", ShortCode = "BeginDate", Desc = "开始日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BeginDate
		{
			get { return _beginDate; }
			set { _beginDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "截止日期", ShortCode = "EndDate", Desc = "截止日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? EndDate
		{
			get { return _endDate; }
			set { _endDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "靶值", ShortCode = "QCTarget", Desc = "靶值", ContextType = SysDic.All, Length = 8)]
        public virtual double? QCTarget
		{
			get { return _qCTarget; }
			set { _qCTarget = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "标准差", ShortCode = "QCSD", Desc = "标准差", ContextType = SysDic.All, Length = 8)]
        public virtual double? QCSD
		{
			get { return _qCSD; }
			set { _qCSD = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "变异系数", ShortCode = "QCCV", Desc = "变异系数", ContextType = SysDic.All, Length = 8)]
        public virtual double? QCCV
		{
			get { return _qCCV; }
			set { _qCCV = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计算靶值", ShortCode = "TTarget", Desc = "计算靶值", ContextType = SysDic.All, Length = 8)]
        public virtual double? TTarget
		{
			get { return _tTarget; }
			set { _tTarget = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计算标准差", ShortCode = "TSD", Desc = "计算标准差", ContextType = SysDic.All, Length = 8)]
        public virtual double? TSD
		{
			get { return _tSD; }
			set { _tSD = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计算变异系数", ShortCode = "TCV", Desc = "计算变异系数", ContextType = SysDic.All, Length = 8)]
        public virtual double? TCV
		{
			get { return _tCV; }
			set { _tCV = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控总数", ShortCode = "QCCount", Desc = "质控总数", ContextType = SysDic.All, Length = 4)]
        public virtual int? QCCount
		{
			get { return _qCCount; }
			set { _qCCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "失控数", ShortCode = "SKCount", Desc = "失控数", ContextType = SysDic.All, Length = 4)]
        public virtual int? SKCount
		{
			get { return _sKCount; }
			set { _sKCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "警告数", ShortCode = "WarningCount", Desc = "警告数", ContextType = SysDic.All, Length = 4)]
        public virtual int? WarningCount
		{
            get { return _warningCount; }
            set { _warningCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "不使用数", ShortCode = "NoUseCount", Desc = "不使用数", ContextType = SysDic.All, Length = 4)]
        public virtual int? NoUseCount
		{
			get { return _noUseCount; }
			set { _noUseCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "在控数", ShortCode = "ZKCount", Desc = "在控数", ContextType = SysDic.All, Length = 4)]
        public virtual int? ZKCount
		{
			get { return _zKCount; }
			set { _zKCount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "极差", ShortCode = "QCRValue", Desc = "极差", ContextType = SysDic.All, Length = 8)]
        public virtual double? QCRValue
		{
			get { return _qCRValue; }
			set { _qCRValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "中位数", ShortCode = "QCMValue", Desc = "中位数", ContextType = SysDic.All, Length = 8)]
        public virtual double? QCMValue
		{
			get { return _qCMValue; }
			set { _qCMValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "QCComment", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string QCComment
		{
			get { return _qCComment; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for QCComment", value, value.ToString());
				_qCComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "SD1倍数量", ShortCode = "SD1Count", Desc = "SD1倍数量", ContextType = SysDic.All, Length = 4)]
        public virtual int? SD1Count
		{
			get { return _sD1Count; }
			set { _sD1Count = value; }
		}

        [DataMember]
        [DataDesc(CName = "SD2倍数量", ShortCode = "SD2Count", Desc = "SD2倍数量", ContextType = SysDic.All, Length = 4)]
        public virtual int? SD2Count
		{
			get { return _sD2Count; }
			set { _sD2Count = value; }
		}

        [DataMember]
        [DataDesc(CName = "SD3倍数量", ShortCode = "SD3Count", Desc = "SD3倍数量", ContextType = SysDic.All, Length = 4)]
        public virtual int? SD3Count
		{
			get { return _sD3Count; }
			set { _sD3Count = value; }
		}

        [DataMember]
        [DataDesc(CName = "批号", ShortCode = "LotNo", Desc = "批号", ContextType = SysDic.All, Length = 20)]
        public virtual string LotNo
		{
			get { return _lotNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for LotNo", value, value.ToString());
				_lotNo = value;
			}
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
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控项目", ShortCode = "QCItem", Desc = "质控项目")]
		public virtual QCItem QCItem
		{
			get { return _qCItem; }
			set { _qCItem = value; }
		}

        
		#endregion
	}
	#endregion
}