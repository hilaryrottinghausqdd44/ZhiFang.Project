using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEEquipMicroSuscResult

	/// <summary>
	/// MEEquipMicroSuscResult object for NHibernate mapped table 'ME_EquipMicroSuscResult'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物药敏结果", ClassCName = "MEEquipMicroSuscResult", ShortCode = "MEEquipMicroSuscResult", Desc = "微生物药敏结果")]
	public class MEEquipMicroSuscResult : BaseEntity
	{
		#region Member Variables
		
        protected long? _antiID;
        protected int _testType;
        protected string _antiChannel;
        protected string _eReportValue;
        protected string _eResultStatus;
        protected string _eResultAlarm;
        protected string _eTestComment;
        protected string _eOriginalValue;
        protected string _eOriginalResultStatus;
        protected string _eOriginalResultAlarm;
        protected string _eOriginalTestComment;
        protected int _eTestState;
        protected int _itemResultFlag;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected MEEquipMicroStepInfo _mEEquipMicroStepInfo;
		protected MEEquipSampleForm _mEEquipSampleForm;
		protected MEEquipSampleItem _mEEquipSampleItem;
        protected double _eReportDLValue;
        protected double _eOriginalDLValue;

		#endregion

		#region Constructors

		public MEEquipMicroSuscResult() { }

		public MEEquipMicroSuscResult( long labID, long antiID, int testType, string antiChannel, string eReportValue, string eResultStatus, string eResultAlarm, string eTestComment, string eOriginalValue, string eOriginalResultStatus, string eOriginalResultAlarm, string eOriginalTestComment, int eTestState, int itemResultFlag, string zDY1, string zDY2, string zDY3, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEEquipMicroStepInfo mEEquipMicroStepInfo, MEEquipSampleForm mEEquipSampleForm, MEEquipSampleItem mEEquipSampleItem )
		{
			this._labID = labID;
			this._antiID = antiID;
			this._testType = testType;
			this._antiChannel = antiChannel;
			this._eReportValue = eReportValue;
			this._eResultStatus = eResultStatus;
			this._eResultAlarm = eResultAlarm;
			this._eTestComment = eTestComment;
			this._eOriginalValue = eOriginalValue;
			this._eOriginalResultStatus = eOriginalResultStatus;
			this._eOriginalResultAlarm = eOriginalResultAlarm;
			this._eOriginalTestComment = eOriginalTestComment;
			this._eTestState = eTestState;
			this._itemResultFlag = itemResultFlag;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEEquipMicroStepInfo = mEEquipMicroStepInfo;
			this._mEEquipSampleForm = mEEquipSampleForm;
			this._mEEquipSampleItem = mEEquipSampleItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "抗生素ID", ShortCode = "AntiID", Desc = "抗生素ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? AntiID
		{
			get { return _antiID; }
			set { _antiID = value; }
		}

        [DataMember]
        [DataDesc(CName = "检验结果类型", ShortCode = "TestType", Desc = "检验结果类型", ContextType = SysDic.All, Length = 4)]
        public virtual int TestType
		{
			get { return _testType; }
			set { _testType = value; }
		}

        [DataMember]
        [DataDesc(CName = "通道号", ShortCode = "AntiChannel", Desc = "通道号", ContextType = SysDic.All, Length = 50)]
        public virtual string AntiChannel
		{
			get { return _antiChannel; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AntiChannel", value, value.ToString());
				_antiChannel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果", ShortCode = "EReportValue", Desc = "仪器结果", ContextType = SysDic.All, Length = 300)]
        public virtual string EReportValue
		{
			get { return _eReportValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for EReportValue", value, value.ToString());
				_eReportValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器定量结果", ShortCode = "EReportDLValue", Desc = "仪器定量结果", ContextType = SysDic.All, Length = 8)]
        public virtual double EReportDLValue
        {
            get { return _eReportDLValue; }
            set { _eReportDLValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果状态", ShortCode = "EResultStatus", Desc = "仪器结果状态", ContextType = SysDic.All, Length = 20)]
        public virtual string EResultStatus
		{
			get { return _eResultStatus; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EResultStatus", value, value.ToString());
				_eResultStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果警告", ShortCode = "EResultAlarm", Desc = "仪器结果警告", ContextType = SysDic.All, Length = 20)]
        public virtual string EResultAlarm
		{
			get { return _eResultAlarm; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EResultAlarm", value, value.ToString());
				_eResultAlarm = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果备注", ShortCode = "ETestComment", Desc = "仪器结果备注", ContextType = SysDic.All, Length = 16)]
        public virtual string ETestComment
		{
			get { return _eTestComment; }
			set
			{
                //if ( value != null && value.Length > 16)
                //    throw new ArgumentOutOfRangeException("Invalid value for ETestComment", value, value.ToString());
				_eTestComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "通讯原始结果", ShortCode = "EOriginalValue", Desc = "通讯原始结果", ContextType = SysDic.All, Length = 300)]
        public virtual string EOriginalValue
		{
			get { return _eOriginalValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalValue", value, value.ToString());
				_eOriginalValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "通讯原始定量结果", ShortCode = "EOriginalDLValue", Desc = "通讯原始定量结果", ContextType = SysDic.All, Length = 8)]
        public virtual double EOriginalDLValue
        {
            get { return _eOriginalDLValue; }
            set { _eOriginalDLValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "通讯原始结果状态", ShortCode = "EOriginalResultStatus", Desc = "通讯原始结果状态", ContextType = SysDic.All, Length = 20)]
        public virtual string EOriginalResultStatus
		{
			get { return _eOriginalResultStatus; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalResultStatus", value, value.ToString());
				_eOriginalResultStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "通讯原始结果警告", ShortCode = "EOriginalResultAlarm", Desc = "通讯原始结果警告", ContextType = SysDic.All, Length = 20)]
        public virtual string EOriginalResultAlarm
		{
			get { return _eOriginalResultAlarm; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalResultAlarm", value, value.ToString());
				_eOriginalResultAlarm = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果备注", ShortCode = "EOriginalTestComment", Desc = "仪器结果备注", ContextType = SysDic.All, Length = 16)]
        public virtual string EOriginalTestComment
		{
			get { return _eOriginalTestComment; }
			set
			{
                //if ( value != null && value.Length > 16)
                //    throw new ArgumentOutOfRangeException("Invalid value for EOriginalTestComment", value, value.ToString());
				_eOriginalTestComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器检测状态", ShortCode = "ETestState", Desc = "仪器检测状态", ContextType = SysDic.All, Length = 4)]
        public virtual int ETestState
		{
			get { return _eTestState; }
			set { _eTestState = value; }
		}

        [DataMember]
        [DataDesc(CName = "结果导入标志", ShortCode = "ItemResultFlag", Desc = "结果导入标志", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemResultFlag
		{
			get { return _itemResultFlag; }
			set { _itemResultFlag = value; }
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
        [DataDesc(CName = "仪器微生物检验详细结果", ShortCode = "MEEquipMicroStepInfo", Desc = "仪器微生物检验详细结果")]
		public virtual MEEquipMicroStepInfo MEEquipMicroStepInfo
		{
			get { return _mEEquipMicroStepInfo; }
			set { _mEEquipMicroStepInfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器样本单", ShortCode = "MEEquipSampleForm", Desc = "仪器样本单")]
		public virtual MEEquipSampleForm MEEquipSampleForm
		{
			get { return _mEEquipSampleForm; }
			set { _mEEquipSampleForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器样本项目", ShortCode = "MEEquipSampleItem", Desc = "仪器样本项目")]
		public virtual MEEquipSampleItem MEEquipSampleItem
		{
			get { return _mEEquipSampleItem; }
			set { _mEEquipSampleItem = value; }
		}

        
		#endregion
	}
	#endregion
}