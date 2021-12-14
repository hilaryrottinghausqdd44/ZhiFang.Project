using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroStepInfo

	/// <summary>
	/// MEMicroStepInfo object for NHibernate mapped table 'ME_MicroStepInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物检验详细结果", ClassCName = "MEMicroStepInfo", ShortCode = "MEMicroStepInfo", Desc = "微生物检验详细结果")]
	public class MEMicroStepInfo : BaseEntity
	{
		#region Member Variables
		
        protected long? _equipMicroStepValueID;
        protected string _reportValue;
        protected int _alarmLevel;
        protected string _resultLink;
        protected string _resultComment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMicroTestItemInfo _bMicroTestItemInfo;
		protected MEGroupSampleItem _mEGroupSampleItem;

		#endregion

		#region Constructors

		public MEMicroStepInfo() { }

		public MEMicroStepInfo( long labID, long equipMicroStepValueID, string reportValue, int alarmLevel, string resultLink, string resultComment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp,  BMicroTestItemInfo bMicroTestItemInfo, MEGroupSampleItem mEGroupSampleItem )
		{
			this._labID = labID;
			this._equipMicroStepValueID = equipMicroStepValueID;
			this._reportValue = reportValue;
			this._alarmLevel = alarmLevel;
			this._resultLink = resultLink;
			this._resultComment = resultComment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroTestItemInfo = bMicroTestItemInfo;
			this._mEGroupSampleItem = mEGroupSampleItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "微生物检验步骤结果ID", ShortCode = "EquipMicroStepValueID", Desc = "微生物检验步骤结果ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipMicroStepValueID
		{
			get { return _equipMicroStepValueID; }
			set { _equipMicroStepValueID = value; }
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
        [DataDesc(CName = "结果警示级别", ShortCode = "AlarmLevel", Desc = "结果警示级别", ContextType = SysDic.All, Length = 4)]
        public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
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
        [DataDesc(CName = "结果说明", ShortCode = "ResultComment", Desc = "结果说明", ContextType = SysDic.All, Length = 16)]
        public virtual string ResultComment
		{
			get { return _resultComment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for ResultComment", value, value.ToString());
				_resultComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
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
        [DataDesc(CName = "微生物检验记录项字典表", ShortCode = "BMicroTestItemInfo", Desc = "微生物检验记录项字典表")]
		public virtual BMicroTestItemInfo BMicroTestItemInfo
		{
			get { return _bMicroTestItemInfo; }
			set { _bMicroTestItemInfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本项目", ShortCode = "MEGroupSampleItem", Desc = "小组样本项目")]
		public virtual MEGroupSampleItem MEGroupSampleItem
		{
			get { return _mEGroupSampleItem; }
			set { _mEGroupSampleItem = value; }
		}

        
		#endregion
	}
	#endregion
}