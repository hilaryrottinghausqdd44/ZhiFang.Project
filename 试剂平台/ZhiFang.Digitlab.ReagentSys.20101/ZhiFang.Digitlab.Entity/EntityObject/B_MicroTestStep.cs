using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BMicroTestStep

	/// <summary>
	/// BMicroTestStep object for NHibernate mapped table 'B_MicroTestStep'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物检验步骤", ClassCName = "BMicroTestStep", ShortCode = "BMicroTestStep", Desc = "微生物检验步骤")]
	public class BMicroTestStep : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _testUseTime;
        protected int _useTimeUnit;
        protected int _autoResultTime;
        protected int _autoTimeUnit;
        protected string _inputModule;
        protected string _reportModule;
        protected bool _isPrint;
        protected bool _canPrint;
        protected bool _bChildBarcode;
        protected int _importLevel;
        protected int _alarmLevel;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected BMicroStepType _bMicroStepType;
		protected IList<BMicroStepInfo> _bMicroStepInfoList; 
		protected IList<MEMicroStep> _mEMicroStepList; 

		#endregion

		#region Constructors

		public BMicroTestStep() { }

		public BMicroTestStep( long labID, string cName, string useCode, string standCode, string deveCode, string eName, string sName, string shortcode, string pinYinZiTou, int testUseTime, int useTimeUnit, int autoResultTime, int autoTimeUnit, string inputModule, string reportModule, bool isPrint, bool canPrint, bool bChildBarcode, int importLevel, int alarmLevel, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicroStepType bMicroStepType )
		{
			this._labID = labID;
			this._cName = cName;
			this._useCode = useCode;
			this._standCode = standCode;
			this._deveCode = deveCode;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._testUseTime = testUseTime;
			this._useTimeUnit = useTimeUnit;
			this._autoResultTime = autoResultTime;
			this._autoTimeUnit = autoTimeUnit;
			this._inputModule = inputModule;
			this._reportModule = reportModule;
			this._isPrint = isPrint;
			this._canPrint = canPrint;
			this._bChildBarcode = bChildBarcode;
			this._importLevel = importLevel;
			this._alarmLevel = alarmLevel;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicroStepType = bMicroStepType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
		{
			get { return _useCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
				_useCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
				_standCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "开发商标准代码", ShortCode = "DeveCode", Desc = "开发商标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
		{
			get { return _deveCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
				_deveCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "提示时间", ShortCode = "TestUseTime", Desc = "提示时间", ContextType = SysDic.All, Length = 4)]
        public virtual int TestUseTime
		{
			get { return _testUseTime; }
			set { _testUseTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "提示时间单位", ShortCode = "UseTimeUnit", Desc = "提示时间单位", ContextType = SysDic.All, Length = 4)]
        public virtual int UseTimeUnit
		{
			get { return _useTimeUnit; }
			set { _useTimeUnit = value; }
		}

        [DataMember]
        [DataDesc(CName = "自动结果时间", ShortCode = "AutoResultTime", Desc = "自动结果时间", ContextType = SysDic.All, Length = 4)]
        public virtual int AutoResultTime
		{
			get { return _autoResultTime; }
			set { _autoResultTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "自动时间单位", ShortCode = "AutoTimeUnit", Desc = "自动时间单位", ContextType = SysDic.All, Length = 4)]
        public virtual int AutoTimeUnit
		{
			get { return _autoTimeUnit; }
			set { _autoTimeUnit = value; }
		}

        [DataMember]
        [DataDesc(CName = "录入模版", ShortCode = "InputModule", Desc = "录入模版", ContextType = SysDic.All, Length = 200)]
        public virtual string InputModule
		{
			get { return _inputModule; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for InputModule", value, value.ToString());
				_inputModule = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "报告模版", ShortCode = "ReportModule", Desc = "报告模版", ContextType = SysDic.All, Length = 200)]
        public virtual string ReportModule
		{
			get { return _reportModule; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReportModule", value, value.ToString());
				_reportModule = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否报告", ShortCode = "IsPrint", Desc = "是否报告", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPrint
		{
			get { return _isPrint; }
			set { _isPrint = value; }
		}

        [DataMember]
        [DataDesc(CName = "立即报告", ShortCode = "CanPrint", Desc = "立即报告", ContextType = SysDic.All, Length = 1)]
        public virtual bool CanPrint
		{
			get { return _canPrint; }
			set { _canPrint = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否拆子条码", ShortCode = "BChildBarcode", Desc = "是否拆子条码", ContextType = SysDic.All, Length = 1)]
        public virtual bool BChildBarcode
		{
			get { return _bChildBarcode; }
			set { _bChildBarcode = value; }
		}

        [DataMember]
        [DataDesc(CName = "重要性", ShortCode = "ImportLevel", Desc = "重要性", ContextType = SysDic.All, Length = 4)]
        public virtual int ImportLevel
		{
			get { return _importLevel; }
			set { _importLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "默认结果警示级别", ShortCode = "AlarmLevel", Desc = "默认结果警示级别", ContextType = SysDic.All, Length = 4)]
        public virtual int AlarmLevel
		{
			get { return _alarmLevel; }
			set { _alarmLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				_comment = value;
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
        [DataDesc(CName = "微生物检验步骤类型", ShortCode = "BMicroStepType", Desc = "微生物检验步骤类型")]
		public virtual BMicroStepType BMicroStepType
		{
			get { return _bMicroStepType; }
			set { _bMicroStepType = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "微生物检验步骤记录项", ShortCode = "BMicroStepInfoList", Desc = "微生物检验步骤记录项")]
		public virtual IList<BMicroStepInfo> BMicroStepInfoList
		{
			get
			{
				if (_bMicroStepInfoList==null)
				{
					_bMicroStepInfoList = new List<BMicroStepInfo>();
				}
				return _bMicroStepInfoList;
			}
			set { _bMicroStepInfoList = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物检验过程", ShortCode = "MEMicroStepList", Desc = "微生物检验过程")]
		public virtual IList<MEMicroStep> MEMicroStepList
		{
			get
			{
				if (_mEMicroStepList==null)
				{
					_mEMicroStepList = new List<MEMicroStep>();
				}
				return _mEMicroStepList;
			}
			set { _mEMicroStepList = value; }
		}

        
		#endregion
	}
	#endregion
}