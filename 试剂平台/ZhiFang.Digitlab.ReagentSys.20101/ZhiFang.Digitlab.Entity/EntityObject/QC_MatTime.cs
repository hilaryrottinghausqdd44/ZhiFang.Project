using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCMatTime

	/// <summary>
	/// QCMatTime object for NHibernate mapped table 'QC_MatTime'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "质控物时效", ClassCName = "", ShortCode = "ZKWSX", Desc = "质控物时效")]
    public class QCMatTime : BaseEntity
	{
		#region Member Variables
		
		
		protected DateTime _beginDate;
		protected DateTime? _endDate;
		protected string _lotNo;
		protected DateTime? _canUseBeginDate;
		protected DateTime? _canUseEndDate;
		protected string _canUseDateInfo;
		protected string _qCMatTimeDesc;
        protected string _calibratorName;
        protected string _calibratorLotNo;
        protected string _calibratorF;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected QCMat _qCMat;
		protected IList<QCItemTime> _qCItemTimes;

		#endregion

		#region Constructors

		public QCMatTime() { }

		public QCMatTime( long labID, DateTime beginDate, DateTime endDate, string lotNo, DateTime canUseBeginDate, DateTime canUseEndDate, string canUseDateInfo, string qCMatTimeDesc, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, QCMat qCMat )
		{
			this._labID = labID;
			this._beginDate = beginDate;
			this._endDate = endDate;
			this._lotNo = lotNo;
			this._canUseBeginDate = canUseBeginDate;
			this._canUseEndDate = canUseEndDate;
			this._canUseDateInfo = canUseDateInfo;
			this._qCMatTimeDesc = qCMatTimeDesc;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._qCMat = qCMat;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "启用日期", ShortCode = "QYRQ", Desc = "启用日期", ContextType = SysDic.DateTime)]
		public virtual DateTime BeginDate
		{
			get { return _beginDate; }
			set { _beginDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结束日期", ShortCode = "JSRQ", Desc = "结束日期", ContextType = SysDic.DateTime)]
		public virtual DateTime? EndDate
		{
			get { return _endDate; }
			set { _endDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "批号", ShortCode = "PH", Desc = "批号", ContextType = SysDic.NText, Length = 20)]
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
        [DataDesc(CName = "生产日期", ShortCode = "SCRQ", Desc = "生产日期", ContextType = SysDic.DateTime)]
		public virtual DateTime? CanUseBeginDate
		{
			get { return _canUseBeginDate; }
			set { _canUseBeginDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期", ShortCode = "YXQ", Desc = "有效期", ContextType = SysDic.DateTime)]
		public virtual DateTime? CanUseEndDate
		{
			get { return _canUseEndDate; }
			set { _canUseEndDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "有效期描述", ShortCode = "YXQMS", Desc = "有效期描述", ContextType = SysDic.NText, Length = 50)]
		public virtual string CanUseDateInfo
		{
			get { return _canUseDateInfo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CanUseDateInfo", value, value.ToString());
				_canUseDateInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "质控物时效描述", ShortCode = "ZKWSXMS", Desc = "质控物时效描述", ContextType = SysDic.NText, Length = 200)]
		public virtual string QCMatTimeDesc
		{
			get { return _qCMatTimeDesc; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for QCMatTimeDesc", value, value.ToString());
				_qCMatTimeDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定标物名称", ShortCode = "CalibratorName", Desc = "定标物名称", ContextType = SysDic.NText, Length = 40)]
        public virtual string CalibratorName
        {
            get { return _calibratorName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CalibratorName", value, value.ToString());
                _calibratorName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "定标物批号", ShortCode = "CalibratorLotNo", Desc = "定标物批号", ContextType = SysDic.NText, Length = 40)]
        public virtual string CalibratorLotNo
        {
            get { return _calibratorLotNo; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CalibratorLotNo", value, value.ToString());
                _calibratorLotNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "定标物厂家", ShortCode = "CalibratorF", Desc = "定标物厂家", ContextType = SysDic.NText, Length = 40)]
        public virtual string CalibratorF
        {
            get { return _calibratorF; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CalibratorF", value, value.ToString());
                _calibratorF = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "SFSY", Desc = "是否使用", ContextType = SysDic.All)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "XSCX", Desc = "显示次序", ContextType = SysDic.Number, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "质控物", ShortCode = "ZKW", Desc = "质控物")]
		public virtual QCMat QCMat
		{
			get { return _qCMat; }
			set { _qCMat = value; }
		}

        [DataMember]
        [DataDesc(CName = "质控项目时效列表", ShortCode = "ZKXMSXLB", Desc = "质控项目时效列表", ContextType = SysDic.List)]
		public virtual IList<QCItemTime> QCItemTimeList
		{
			get
			{
				if (_qCItemTimes==null)
				{
                    _qCItemTimes = new List<QCItemTime>();
				}
				return _qCItemTimes;
			}
			set { _qCItemTimes = value; }
		}

		#endregion
	}
	#endregion
}