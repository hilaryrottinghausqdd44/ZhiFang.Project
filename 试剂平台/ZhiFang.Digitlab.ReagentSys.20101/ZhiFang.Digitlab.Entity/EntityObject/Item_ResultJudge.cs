using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region ItemResultJudge

	/// <summary>
	/// ItemResultJudge object for NHibernate mapped table 'Item_ResultJudge'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ItemResultJudge", ShortCode = "ItemResultJudge", Desc = "")]
	public class ItemResultJudge : BaseEntity
	{
		#region Member Variables

        protected double? _lowAge;
        protected double? _highAge;
        protected DateTime? _bCollectTime;
        protected DateTime? _eCollectTime;
        protected string _conditionName;
        protected int _judgeType;
        protected int? _judgeGroup;
        protected int? _judgeIndex;
        protected double? _lowValue;
        protected double? _highValue;
        protected string _judgeStatus;
        protected string _judgeReport;
        protected string _reportValue;
        protected string _resultStatus;
        protected double? _quanValue;
        protected int? _alarmLevel;
        protected int? _redoLevel;
        protected string _clinicalInfo;
        protected DateTime? _dataUpdateTime;
		protected BAgeUnit _bAgeUnit;
		protected BSampleType _bSampleType;
		protected BSex _bSex;
		protected EPBEquip _ePBEquip;
		protected GMGroup _gMGroup;
		protected HRDept _hRDept;
		protected ItemAllItem _itemAllItem;

		#endregion

		#region Constructors

		public ItemResultJudge() { }

        public ItemResultJudge(long labID, double? lowAge, double? highAge, DateTime bCollectTime, DateTime eCollectTime, string conditionName, int judgeType, int? judgeGroup, int? judgeIndex, double? lowValue, double? highValue, string judgeStatus, string judgeReport, string reportValue, string resultStatus, double? quanValue, int? alarmLevel, int? redoLevel, string clinicalInfo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BAgeUnit bAgeUnit, BSampleType bSampleType, BSex bSex, EPBEquip ePBEquip, GMGroup gMGroup, HRDept hRDept, ItemAllItem itemAllItem)
		{
			this._labID = labID;
			this._lowAge = lowAge;
			this._highAge = highAge;
			this._bCollectTime = bCollectTime;
			this._eCollectTime = eCollectTime;
			this._conditionName = conditionName;
			this._judgeType = judgeType;
			this._judgeGroup = judgeGroup;
			this._judgeIndex = judgeIndex;
			this._lowValue = lowValue;
			this._highValue = highValue;
			this._judgeStatus = judgeStatus;
			this._judgeReport = judgeReport;
			this._reportValue = reportValue;
			this._resultStatus = resultStatus;
			this._quanValue = quanValue;
			this._alarmLevel = alarmLevel;
			this._redoLevel = redoLevel;
			this._clinicalInfo = clinicalInfo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bAgeUnit = bAgeUnit;
			this._bSampleType = bSampleType;
			this._bSex = bSex;
			this._ePBEquip = ePBEquip;
			this._gMGroup = gMGroup;
			this._hRDept = hRDept;
			this._itemAllItem = itemAllItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "年龄低限", ShortCode = "LowAge", Desc = "年龄低限", ContextType = SysDic.All, Length = 8)]
        public virtual double? LowAge
		{
			get { return _lowAge; }
			set { _lowAge = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "年龄高限", ShortCode = "HighAge", Desc = "年龄高限", ContextType = SysDic.All, Length = 8)]
        public virtual double? HighAge
		{
			get { return _highAge; }
			set { _highAge = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BCollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BCollectTime
		{
			get { return _bCollectTime; }
			set { _bCollectTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ECollectTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ECollectTime
		{
			get { return _eCollectTime; }
			set { _eCollectTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "条件说明", ShortCode = "ConditionName", Desc = "条件说明", ContextType = SysDic.All, Length = 100)]
        public virtual string ConditionName
		{
			get { return _conditionName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ConditionName", value, value.ToString());
				_conditionName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "判定类型数值_状态_报告值", ShortCode = "JudgeType", Desc = "判定类型数值_状态_报告值", ContextType = SysDic.All, Length = 4)]
        public virtual int JudgeType
		{
			get { return _judgeType; }
			set { _judgeType = value; }
		}

        [DataMember]
        [DataDesc(CName = "判定组号", ShortCode = "JudgeGroup", Desc = "判定组号", ContextType = SysDic.All, Length = 4)]
        public virtual int? JudgeGroup
		{
			get { return _judgeGroup; }
			set { _judgeGroup = value; }
		}

        [DataMember]
        [DataDesc(CName = "判定序号", ShortCode = "JudgeIndex", Desc = "判定序号", ContextType = SysDic.All, Length = 4)]
        public virtual int? JudgeIndex
		{
			get { return _judgeIndex; }
			set { _judgeIndex = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "范围低限", ShortCode = "LowValue", Desc = "范围低限", ContextType = SysDic.All, Length = 8)]
        public virtual double? LowValue
		{
			get { return _lowValue; }
			set { _lowValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "范围高限", ShortCode = "HighValue", Desc = "范围高限", ContextType = SysDic.All, Length = 8)]
        public virtual double? HighValue
		{
			get { return _highValue; }
			set { _highValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "JudgeStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string JudgeStatus
		{
			get { return _judgeStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for JudgeStatus", value, value.ToString());
				_judgeStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "JudgeReport", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string JudgeReport
		{
			get { return _judgeReport; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for JudgeReport", value, value.ToString());
				_judgeReport = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "报告值", ShortCode = "ReportValue", Desc = "报告值", ContextType = SysDic.All, Length = 300)]
        public virtual string ReportValue
		{
			get { return _reportValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ReportValue", value, value.ToString());
				_reportValue = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定量结果", ShortCode = "QuanValue", Desc = "定量结果", ContextType = SysDic.All, Length = 8)]
        public virtual double? QuanValue
		{
			get { return _quanValue; }
			set { _quanValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "结果警示级别", ShortCode = "AlarmLevel", Desc = "结果警示级别", ContextType = SysDic.All, Length = 4)]
        public virtual int? AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "复检建议", ShortCode = "RedoLevel", Desc = "复检建议", ContextType = SysDic.All, Length = 4)]
        public virtual int? RedoLevel
		{
			get { return _redoLevel; }
			set { _redoLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "临床意义与措施", ShortCode = "ClinicalInfo", Desc = "临床意义与措施", ContextType = SysDic.All, Length = 500)]
        public virtual string ClinicalInfo
		{
			get { return _clinicalInfo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ClinicalInfo", value, value.ToString());
				_clinicalInfo = value;
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
        [DataDesc(CName = "年龄单位", ShortCode = "BAgeUnit", Desc = "年龄单位")]
		public virtual BAgeUnit BAgeUnit
		{
			get { return _bAgeUnit; }
			set { _bAgeUnit = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
		public virtual BSampleType BSampleType
		{
			get { return _bSampleType; }
			set { _bSampleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "性别", ShortCode = "BSex", Desc = "性别")]
		public virtual BSex BSex
		{
			get { return _bSex; }
			set { _bSex = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器表", ShortCode = "EPBEquip", Desc = "仪器表")]
		public virtual EPBEquip EPBEquip
		{
			get { return _ePBEquip; }
			set { _ePBEquip = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroup", Desc = "小组表")]
		public virtual GMGroup GMGroup
		{
			get { return _gMGroup; }
			set { _gMGroup = value; }
		}

        [DataMember]
        [DataDesc(CName = "部门", ShortCode = "HRDept", Desc = "部门")]
		public virtual HRDept HRDept
		{
			get { return _hRDept; }
			set { _hRDept = value; }
		}

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        
		#endregion
	}
	#endregion
}