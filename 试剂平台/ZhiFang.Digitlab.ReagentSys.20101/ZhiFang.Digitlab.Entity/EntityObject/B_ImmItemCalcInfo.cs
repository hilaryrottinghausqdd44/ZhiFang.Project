using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BImmItemCalcInfo

	/// <summary>
	/// BImmItemCalcInfo object for NHibernate mapped table 'B_ImmItemCalcInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "酶免项目计算模式表：用于存储酶标仪检验项目的计算模式的相关信息", ClassCName = "BImmItemCalcInfo", ShortCode = "BImmItemCalcInfo", Desc = "酶免项目计算模式表：用于存储酶标仪检验项目的计算模式的相关信息")]
	public class BImmItemCalcInfo : BaseEntity
	{
		#region Member Variables
		
        protected int _calcModel;
        protected int _subtractBlankMean;
        protected int _resultAccuracy;
        protected int _resultAccuracyMode;
        protected int _dXReportResult;
        protected int _dXDisplayResult;
        protected int _dXPrintResult;
        protected int _dXIsHQ;
        protected double _dXHQLowValue;
        protected double _dXHQHighValue;
        protected string _dLDensity;
        protected string _dLFittingInfo;
        protected DateTime? _dataUpdateTime;
		protected BImmCalcTemplate _bImmCalcTemplate;
        protected EPEquipItem _ePEquipItem;

		#endregion

		#region Constructors

		public BImmItemCalcInfo() { }

		public BImmItemCalcInfo( long labID, int calcModel, int dXReportResult, int dXDisplayResult, int dXPrintResult, int dXIsHQ, double dXHQLowValue, double dXHQHighValue, string dLDensity, string dLFittingInfo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BImmCalcTemplate bImmCalcTemplate )
		{
			this._labID = labID;
			this._calcModel = calcModel;
			this._dXReportResult = dXReportResult;
			this._dXDisplayResult = dXDisplayResult;
			this._dXPrintResult = dXPrintResult;
			this._dXIsHQ = dXIsHQ;
			this._dXHQLowValue = dXHQLowValue;
			this._dXHQHighValue = dXHQHighValue;
			this._dLDensity = dLDensity;
			this._dLFittingInfo = dLFittingInfo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bImmCalcTemplate = bImmCalcTemplate;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "计算模式：0-定性；1-定量；2-仅计算OD值", ShortCode = "CalcModel", Desc = "计算模式：0-定性；1-定量；2-仅计算OD值", ContextType = SysDic.All, Length = 4)]
        public virtual int CalcModel
		{
			get { return _calcModel; }
			set { _calcModel = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否减去空白均值：0-不减去；1-减去", ShortCode = "SubtractBlankMean", Desc = "是否减去空白均值：0-不减去；1-减去", ContextType = SysDic.All, Length = 4)]
        public virtual int SubtractBlankMean
        {
            get { return _subtractBlankMean; }
            set { _subtractBlankMean = value; }
        }

        [DataMember]
        [DataDesc(CName = "定量报告结果精度", ShortCode = "ResultAccuracy", Desc = "定量报告结果精度", ContextType = SysDic.All, Length = 4)]
        public virtual int ResultAccuracy
        {
            get { return _resultAccuracy; }
            set { _resultAccuracy = value; }
        }

        [DataMember]
        [DataDesc(CName = "定量报告结果精度保留模式，0或null：四舍五入，为默认模式；1：直接截取", ShortCode = "ResultAccuracyMode", Desc = "定量报告结果精度保留模式，0或null：四舍五入，为默认模式；1：直接截取", ContextType = SysDic.All, Length = 4)]
        public virtual int ResultAccuracyMode
        {
            get { return _resultAccuracyMode; }
            set { _resultAccuracyMode = value; }
        }

        [DataMember]
        [DataDesc(CName = "公式描述", ShortCode = "DxReportResult", Desc = "公式描述", ContextType = SysDic.All, Length = 4)]
        public virtual int DxReportResult
		{
			get { return _dXReportResult; }
			set { _dXReportResult = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DxDisplayResult", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DxDisplayResult
		{
			get { return _dXDisplayResult; }
			set { _dXDisplayResult = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否在用:默认值为1", ShortCode = "DxPrintResult", Desc = "是否在用:默认值为1", ContextType = SysDic.All, Length = 4)]
        public virtual int DxPrintResult
		{
			get { return _dXPrintResult; }
			set { _dXPrintResult = value; }
		}

        [DataMember]
        [DataDesc(CName = "定性计算时是否采用灰区", ShortCode = "DxIsHQ", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DxIsHQ
		{
			get { return _dXIsHQ; }
			set { _dXIsHQ = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定性计算灰区范围低值", ShortCode = "DxHQLowValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double DxHQLowValue
		{
			get { return _dXHQLowValue; }
			set { _dXHQLowValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定性计算灰区范围高值", ShortCode = "DxHQHighValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double DxHQHighValue
		{
			get { return _dXHQHighValue; }
			set { _dXHQHighValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "定量计算标准品信息", ShortCode = "DlDensity", Desc = "", ContextType = SysDic.All, Length = 800)]
        public virtual string DlDensity
		{
			get { return _dLDensity; }
			set
			{
				if ( value != null && value.Length > 800)
                    throw new ArgumentOutOfRangeException("Invalid value for DlDensity", value, value.ToString());
				_dLDensity = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定量计算拟合方式", ShortCode = "DlFittingInfo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DlFittingInfo
		{
			get { return _dLFittingInfo; }
			set
			{
				if ( value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DlFittingInfo", value, value.ToString());
				_dLFittingInfo = value;
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
        [DataDesc(CName = "定性计算公式表：用于存储定性计算公式的具体内容", ShortCode = "BImmCalcTemplate", Desc = "定性计算公式表：用于存储定性计算公式的具体内容")]
		public virtual BImmCalcTemplate BImmCalcTemplate
		{
			get { return _bImmCalcTemplate; }
			set { _bImmCalcTemplate = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器项目关系", ShortCode = "EPEquipItem", Desc = "仪器项目关系")]
        public virtual EPEquipItem EPEquipItem
        {
            get { return _ePEquipItem; }
            set { _ePEquipItem = value; }
        }
        
		#endregion
	}
	#endregion
}