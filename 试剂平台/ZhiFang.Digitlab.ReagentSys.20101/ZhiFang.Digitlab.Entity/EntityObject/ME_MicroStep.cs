using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroStep

	/// <summary>
	/// MEMicroStep object for NHibernate mapped table 'ME_MicroStep'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物检验过程", ClassCName = "MEMicroStep", ShortCode = "MEMicroStep", Desc = "微生物检验过程")]
	public class MEMicroStep : BaseEntity
	{
		#region Member Variables
		
        protected string _reportValue;
        protected string _recordItemValue;
        protected string _resultStatus;
        protected int _alarmLevel;
        protected bool _isReport;
        protected string _resultComment;
        protected string _eSampleNo;
        protected string _barcodeNo;
        protected int _reportFlag;
        protected DateTime? _beginTime;
        protected DateTime? _testTime;
        protected bool _testFlag;
        protected DateTime? _alarmTime;
        protected DateTime? _dataUpdateTime;
		protected BMicroTestStep _bMicroTestStep;
		protected MEGroupSampleItem _mEGroupSampleItem;
		protected BMicro _bMicro;
		protected IList<MEMicroStepInfo> _mEMicroStepInfoList; 
		protected IList<MEMicroDSTValue> _mEMicroDSTValueList; 
		protected IList<MEMicroStepBacode> _mEMicroStepBacodeList; 

		#endregion

		#region Constructors

		public MEMicroStep() { }

		public MEMicroStep( long labID, string reportValue, string resultStatus, int alarmLevel, bool isReport, string resultComment, string eSampleNo, string barcodeNo, int reportFlag, DateTime beginTime, DateTime testTime, bool testFlag, DateTime alarmTime, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroTestStep bMicroTestStep, MEGroupSampleItem mEGroupSampleItem, BMicro bMicro )
		{
			this._labID = labID;
			this._reportValue = reportValue;
			this._resultStatus = resultStatus;
			this._alarmLevel = alarmLevel;
			this._isReport = isReport;
			this._resultComment = resultComment;
			this._eSampleNo = eSampleNo;
			this._barcodeNo = barcodeNo;
			this._reportFlag = reportFlag;
			this._beginTime = beginTime;
			this._testTime = testTime;
			this._testFlag = testFlag;
			this._alarmTime = alarmTime;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroTestStep = bMicroTestStep;
			this._mEGroupSampleItem = mEGroupSampleItem;
			this._bMicro = bMicro;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "报告值", ShortCode = "ReportValue", Desc = "报告值", ContextType = SysDic.All, Length = 500)]
        public virtual string ReportValue
		{
			get { return _reportValue; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ReportValue", value, value.ToString());
				_reportValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检验步骤记录项报告值", ShortCode = "RecordItemValue", Desc = "检验步骤记录项报告值", ContextType = SysDic.All, Length = 500)]
        public virtual string RecordItemValue
        {
            get { return _recordItemValue; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for RecordItemValue", value, value.ToString());
                _recordItemValue = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检验结果状态", ShortCode = "ResultStatus", Desc = "检验结果状态", ContextType = SysDic.All, Length = 10)]
        public virtual string ResultStatus
		{
			get { return _resultStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ResultStatus", value, value.ToString());
				_resultStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "结果警示级别", ShortCode = "AlarmLevel", Desc = "结果警示级别", ContextType = SysDic.All, Length = 4)]
        public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否报告", ShortCode = "IsReport", Desc = "是否报告", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReport
		{
			get { return _isReport; }
			set { _isReport = value; }
		}

        [DataMember]
        [DataDesc(CName = "结果说明", ShortCode = "ResultComment", Desc = "结果说明", ContextType = SysDic.All, Length = 16)]
        public virtual string ResultComment
		{
			get { return _resultComment; }
			set
			{
				_resultComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检测编号", ShortCode = "ESampleNo", Desc = "检测编号", ContextType = SysDic.All, Length = 20)]
        public virtual string ESampleNo
		{
			get { return _eSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ESampleNo", value, value.ToString());
				_eSampleNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "BarcodeNo", Desc = "条码号", ContextType = SysDic.All, Length = 20)]
        public virtual string BarcodeNo
		{
			get { return _barcodeNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BarcodeNo", value, value.ToString());
				_barcodeNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否报告状态", ShortCode = "ReportFlag", Desc = "是否报告状态", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportFlag
		{
			get { return _reportFlag; }
			set { _reportFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验开始时间", ShortCode = "BeginTime", Desc = "检验开始时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BeginTime
		{
			get { return _beginTime; }
			set { _beginTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验时间", ShortCode = "TestTime", Desc = "检验时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TestTime
		{
			get { return _testTime; }
			set { _testTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "检验确认状态", ShortCode = "TestFlag", Desc = "检验确认状态", ContextType = SysDic.All, Length = 1)]
        public virtual bool TestFlag
		{
			get { return _testFlag; }
			set { _testFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "提示时间", ShortCode = "AlarmTime", Desc = "提示时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AlarmTime
		{
			get { return _alarmTime; }
			set { _alarmTime = value; }
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
        [DataDesc(CName = "微生物检验步骤", ShortCode = "BMicroTestStep", Desc = "微生物检验步骤")]
		public virtual BMicroTestStep BMicroTestStep
		{
			get { return _bMicroTestStep; }
			set { _bMicroTestStep = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本项目", ShortCode = "MEGroupSampleItem", Desc = "小组样本项目")]
		public virtual MEGroupSampleItem MEGroupSampleItem
		{
			get { return _mEGroupSampleItem; }
			set { _mEGroupSampleItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物", ShortCode = "BMicro", Desc = "微生物")]
		public virtual BMicro BMicro
		{
			get { return _bMicro; }
			set { _bMicro = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物检验详细结果", ShortCode = "MEMicroStepInfoList", Desc = "微生物检验详细结果")]
		public virtual IList<MEMicroStepInfo> MEMicroStepInfoList
		{
			get
			{
				if (_mEMicroStepInfoList==null)
				{
					_mEMicroStepInfoList = new List<MEMicroStepInfo>();
				}
				return _mEMicroStepInfoList;
			}
			set { _mEMicroStepInfoList = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物药敏结果drug sensitive test", ShortCode = "MEMicroDSTValueList", Desc = "微生物药敏结果drug sensitive test")]
		public virtual IList<MEMicroDSTValue> MEMicroDSTValueList
		{
			get
			{
				if (_mEMicroDSTValueList==null)
				{
					_mEMicroDSTValueList = new List<MEMicroDSTValue>();
				}
				return _mEMicroDSTValueList;
			}
			set { _mEMicroDSTValueList = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物检验条码", ShortCode = "MEMicroStepBacodeList", Desc = "微生物检验条码")]
		public virtual IList<MEMicroStepBacode> MEMicroStepBacodeList
		{
			get
			{
				if (_mEMicroStepBacodeList==null)
				{
					_mEMicroStepBacodeList = new List<MEMicroStepBacode>();
				}
				return _mEMicroStepBacodeList;
			}
			set { _mEMicroStepBacodeList = value; }
		}

        
		#endregion
	}
	#endregion
}