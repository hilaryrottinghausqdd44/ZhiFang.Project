using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEGroupSampleReCheckItem

	/// <summary>
	/// MEGroupSampleReCheckItem object for NHibernate mapped table 'ME_GroupSampleReCheckItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "复检项目", ClassCName = "MEGroupSampleReCheckItem", ShortCode = "MEGroupSampleReCheckItem", Desc = "复检项目")]
	public class MEGroupSampleReCheckItem : BaseEntity
	{
		#region Member Variables
		
        protected int _testType;
        protected long? _sampleItemID;
        protected long? _parentResultID;
        protected long? _equipResultID;
        protected string _origlValue;
        protected int _valueType;
        protected string _reportValue;
        protected string _resultStatus;
        protected double _quanValue;
        protected double _quanValue2;
        protected double _quanValue3;
        protected string _units;
        protected string _refRange;
        protected string _preValue;
        protected string _preValueComp;
        protected string _preCompStatus;
        protected string _testMethod;
        protected int _alarmLevel;
        protected int _resultLinkType;
        protected string _resultLink;
        protected string _simpleResultLink;
        protected string _resultComment;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected DateTime? _reCheckTime;
        protected bool _useFlag;
		protected ItemAllItem _itemAllItem;
		protected MEGroupSampleReCheckForm _mEGroupSampleReCheckForm;

		#endregion

		#region Constructors

		public MEGroupSampleReCheckItem() { }

		public MEGroupSampleReCheckItem( long labID, int testType, long sampleItemID, long parentResultID, long equipResultID, string origlValue, int valueType, string reportValue, string resultStatus, double quanValue, double quanValue2, double quanValue3, string units, string refRange, string preValue, string preValueComp, string preCompStatus, string testMethod, int alarmLevel, int resultLinkType, string resultLink, string simpleResultLink, string resultComment, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, DateTime reCheckTime, bool useFlag, ItemAllItem itemAllItem, MEGroupSampleReCheckForm mEGroupSampleReCheckForm )
		{
			this._labID = labID;
			this._testType = testType;
			this._sampleItemID = sampleItemID;
			this._parentResultID = parentResultID;
			this._equipResultID = equipResultID;
			this._origlValue = origlValue;
			this._valueType = valueType;
			this._reportValue = reportValue;
			this._resultStatus = resultStatus;
			this._quanValue = quanValue;
			this._quanValue2 = quanValue2;
			this._quanValue3 = quanValue3;
			this._units = units;
			this._refRange = refRange;
			this._preValue = preValue;
			this._preValueComp = preValueComp;
			this._preCompStatus = preCompStatus;
			this._testMethod = testMethod;
			this._alarmLevel = alarmLevel;
			this._resultLinkType = resultLinkType;
			this._resultLink = resultLink;
			this._simpleResultLink = simpleResultLink;
			this._resultComment = resultComment;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._zDY4 = zDY4;
			this._zDY5 = zDY5;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._reCheckTime = reCheckTime;
			this._useFlag = useFlag;
			this._itemAllItem = itemAllItem;
			this._mEGroupSampleReCheckForm = mEGroupSampleReCheckForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "检验结果类型", ShortCode = "TestType", Desc = "检验结果类型", ContextType = SysDic.All, Length = 4)]
        public virtual int TestType
		{
			get { return _testType; }
			set { _testType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本单项目ID", ShortCode = "SampleItemID", Desc = "样本单项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? SampleItemID
		{
			get { return _sampleItemID; }
			set { _sampleItemID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "父结果ID", ShortCode = "ParentResultID", Desc = "父结果ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ParentResultID
		{
			get { return _parentResultID; }
			set { _parentResultID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器样本项目ID", ShortCode = "EquipResultID", Desc = "仪器样本项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipResultID
		{
			get { return _equipResultID; }
			set { _equipResultID = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器原始数值", ShortCode = "OriglValue", Desc = "仪器原始数值", ContextType = SysDic.All, Length = 40)]
        public virtual string OriglValue
		{
			get { return _origlValue; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for OriglValue", value, value.ToString());
				_origlValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "结果类型", ShortCode = "ValueType", Desc = "结果类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ValueType
		{
			get { return _valueType; }
			set { _valueType = value; }
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
        public virtual double QuanValue
		{
			get { return _quanValue; }
			set { _quanValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定量辅助结果2", ShortCode = "QuanValue2", Desc = "定量辅助结果2", ContextType = SysDic.All, Length = 8)]
        public virtual double QuanValue2
		{
			get { return _quanValue2; }
			set { _quanValue2 = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定量辅助结果3", ShortCode = "QuanValue3", Desc = "定量辅助结果3", ContextType = SysDic.All, Length = 8)]
        public virtual double QuanValue3
		{
			get { return _quanValue3; }
			set { _quanValue3 = value; }
		}

        [DataMember]
        [DataDesc(CName = "结果单位", ShortCode = "Units", Desc = "结果单位", ContextType = SysDic.All, Length = 50)]
        public virtual string Units
		{
			get { return _units; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Units", value, value.ToString());
				_units = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "参考范围", ShortCode = "RefRange", Desc = "参考范围", ContextType = SysDic.All, Length = 200)]
        public virtual string RefRange
		{
			get { return _refRange; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for RefRange", value, value.ToString());
				_refRange = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "前报告值", ShortCode = "PreValue", Desc = "前报告值", ContextType = SysDic.All, Length = 300)]
        public virtual string PreValue
		{
			get { return _preValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for PreValue", value, value.ToString());
				_preValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "前值对比", ShortCode = "PreValueComp", Desc = "前值对比", ContextType = SysDic.All, Length = 50)]
        public virtual string PreValueComp
		{
			get { return _preValueComp; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PreValueComp", value, value.ToString());
				_preValueComp = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "前值对比状态", ShortCode = "PreCompStatus", Desc = "前值对比状态", ContextType = SysDic.All, Length = 20)]
        public virtual string PreCompStatus
		{
			get { return _preCompStatus; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PreCompStatus", value, value.ToString());
				_preCompStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检测方法", ShortCode = "TestMethod", Desc = "检测方法", ContextType = SysDic.All, Length = 50)]
        public virtual string TestMethod
		{
			get { return _testMethod; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TestMethod", value, value.ToString());
				_testMethod = value;
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
        [DataDesc(CName = "结果值链接类型", ShortCode = "ResultLinkType", Desc = "结果值链接类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ResultLinkType
		{
			get { return _resultLinkType; }
			set { _resultLinkType = value; }
		}

        [DataMember]
        [DataDesc(CName = "结果值链接", ShortCode = "ResultLink", Desc = "结果值链接", ContextType = SysDic.All, Length = 100)]
        public virtual string ResultLink
		{
			get { return _resultLink; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ResultLink", value, value.ToString());
				_resultLink = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "结果缩略链接", ShortCode = "SimpleResultLink", Desc = "结果缩略链接", ContextType = SysDic.All, Length = 100)]
        public virtual string SimpleResultLink
		{
			get { return _simpleResultLink; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SimpleResultLink", value, value.ToString());
				_simpleResultLink = value;
			}
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
        [DataDesc(CName = "自定义1", ShortCode = "ZDY1", Desc = "自定义1", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY1
		{
			get { return _zDY1; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
				_zDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义2", ShortCode = "ZDY2", Desc = "自定义2", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY2
		{
			get { return _zDY2; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
				_zDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义3", ShortCode = "ZDY3", Desc = "自定义3", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY3
		{
			get { return _zDY3; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
				_zDY3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义4", ShortCode = "ZDY4", Desc = "自定义4", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY4
		{
			get { return _zDY4; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
				_zDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义5", ShortCode = "ZDY5", Desc = "自定义5", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY5
		{
			get { return _zDY5; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
				_zDY5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "复检时间", ShortCode = "ReCheckTime", Desc = "复检时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReCheckTime
		{
			get { return _reCheckTime; }
			set { _reCheckTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UseFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool UseFlag
		{
			get { return _useFlag; }
			set { _useFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "复检记录", ShortCode = "MEGroupSampleReCheckForm", Desc = "复检记录")]
		public virtual MEGroupSampleReCheckForm MEGroupSampleReCheckForm
		{
			get { return _mEGroupSampleReCheckForm; }
			set { _mEGroupSampleReCheckForm = value; }
		}

        
		#endregion
	}
	#endregion
}