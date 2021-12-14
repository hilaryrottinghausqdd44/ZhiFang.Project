using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEImmuneResults

	/// <summary>
	/// MEImmuneResults object for NHibernate mapped table 'ME_ImmuneResults'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "存储酶免板的96个孔的孔号、标本类型、检验项目、检验结果信息", ClassCName = "MEImmuneResults", ShortCode = "MEImmuneResults", Desc = "存储酶免板的96个孔的孔号、标本类型、检验项目、检验结果信息")]
	public class MEImmuneResults : BaseEntity
	{
		#region Member Variables
		
        protected int _positionNo;
        protected string _dispPositionNo;
        protected int _sampleType;
        protected string _gSampleNo;
        protected DateTime? _gSampleDate;
        protected string _originalOD;
        protected double? _oD;
        protected double? _resultValue;
        protected string _resultDesc;
        protected string _resultDisplay;
        protected string _resultPrint;
        protected double? _cutOff;
        protected double? _sCO;
        protected string _reportResult;
        protected int? _showColor;
        protected int? _bZPSampleOrder;
        protected int? _sameBZPSampleOrder;
        protected DateTime? _dataUpdateTime;
        protected EPEquipItem _ePEquipItem;
		protected ItemAllItem _itemAllItem;
		protected MEImmunePlate _mEImmunePlate;
        protected GMGroup _gmGroup;

		#endregion

		#region Constructors

		public MEImmuneResults() { }

        public MEImmuneResults(long labID, int positionNo, string dispPositionNo, int sampleType, string gSampleNo, DateTime gSampleDate, string originalOD, double? oD, double? resultValue, string resultDesc, string resultDisplay, string resultPrint, double? cutOff, double? sCO, string reportResult, int? showColor, int? bZPSampleOrder, int? sameBZPSampleOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, MEImmunePlate mEImmunePlate,  GMGroup _gmGroup)
		{
			this._labID = labID;
			this._positionNo = positionNo;
			this._dispPositionNo = dispPositionNo;
			this._sampleType = sampleType;
			this._gSampleNo = gSampleNo;
			this._gSampleDate = gSampleDate;
			this._originalOD = originalOD;
			this._oD = oD;
			this._resultValue = resultValue;
			this._resultDesc = resultDesc;
			this._resultDisplay = resultDisplay;
			this._resultPrint = resultPrint;
			this._cutOff = cutOff;
			this._sCO = sCO;
			this._reportResult = reportResult;
			this._showColor = showColor;
			this._bZPSampleOrder = bZPSampleOrder;
			this._sameBZPSampleOrder = sameBZPSampleOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
			this._mEImmunePlate = mEImmunePlate;
            this._gmGroup = _gmGroup;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "酶免板孔位ID，其值为1-96", ShortCode = "PositionNo", Desc = "酶免板孔位ID，其值为1-96", ContextType = SysDic.All, Length = 4)]
        public virtual int PositionNo
		{
			get { return _positionNo; }
			set { _positionNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "酶免板孔位显示名称", ShortCode = "DispPositionNo", Desc = "酶免板孔位显示名称", ContextType = SysDic.All, Length = 10)]
        public virtual string DispPositionNo
		{
			get { return _dispPositionNo; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for DispPositionNo", value, value.ToString());
				_dispPositionNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "每个孔所检标本的类型：", ShortCode = "SampleType", Desc = "每个孔所检标本的类型：", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleType
		{
			get { return _sampleType; }
			set { _sampleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "检测标本的样本号：", ShortCode = "GSampleNo", Desc = "检测标本的样本号：", ContextType = SysDic.All, Length = 20)]
        public virtual string GSampleNo
		{
			get { return _gSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for GSampleNo", value, value.ToString());
				_gSampleNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结果对应检验单日期", ShortCode = "GSampleDate", Desc = "结果对应检验单日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GSampleDate
		{
			get { return _gSampleDate; }
			set { _gSampleDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "原始吸光度值", ShortCode = "OriginalOD", Desc = "原始吸光度值", ContextType = SysDic.All, Length = 20)]
        public virtual string OriginalOD
		{
			get { return _originalOD; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for OriginalOD", value, value.ToString());
				_originalOD = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "吸光度值=原始吸光度值-空白对照均值", ShortCode = "OD", Desc = "吸光度值=原始吸光度值-空白对照均值", ContextType = SysDic.All, Length = 8)]
        public virtual double? OD
		{
			get { return _oD; }
			set { _oD = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定量结果值", ShortCode = "ResultValue", Desc = "定量结果值", ContextType = SysDic.All, Length = 8)]
        public virtual double? ResultValue
		{
			get { return _resultValue; }
			set { _resultValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "定性结果报告值", ShortCode = "ResultDesc", Desc = "定性结果报告值", ContextType = SysDic.All, Length = 60)]
        public virtual string ResultDesc
		{
			get { return _resultDesc; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ResultDesc", value, value.ToString());
				_resultDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定性结果显示值", ShortCode = "ResultDisplay", Desc = "定性结果显示值", ContextType = SysDic.All, Length = 60)]
        public virtual string ResultDisplay
		{
			get { return _resultDisplay; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ResultDisplay", value, value.ToString());
				_resultDisplay = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定性结果打印值", ShortCode = "ResultPrint", Desc = "定性结果打印值", ContextType = SysDic.All, Length = 60)]
        public virtual string ResultPrint
		{
			get { return _resultPrint; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for ResultPrint", value, value.ToString());
				_resultPrint = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "cutoff即临界值，是判断检测结果的标准。", ShortCode = "CutOff", Desc = "cutoff即临界值，是判断检测结果的标准。", ContextType = SysDic.All, Length = 8)]
        public virtual double? CutOff
		{
			get { return _cutOff; }
			set { _cutOff = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "S/CO是OD值与CutOff值的比值。一般来说，检测病毒性肝炎血清标志物最普通的方法为酶联免疫吸附试验。大多数医院都采用s/co的方式向临床出具报告，可看作是病毒性肝炎标志物的半定量分析，在一定程度上可以反映病毒标志物的强弱，但由于免疫检测方法学敏感性强，检测试剂品种繁多，变异大，线性范围小，所以应该对不同肝炎病人的血清做进一步的分析，才能对肝炎病毒标志物的浓度进行正确的检测与评估。，s/co值越大，说明血清中HBsAg浓度越高。但由于其方法学特定的局限性，实验室要快速报告结果，大都采用“一步法”，部分厂家的试剂存在质量差异，抗原抗体结合有一定线性区、等价区和抗原过剩区，只有当HBsAg浓度性范围内，s/co值与HBsAg浓度呈正相关，HBsAg浓度超过线性范围时，s/co值不能客观地反映患者体内HBsAg真实水平。因此，临床报告结果时，用s/co值来说明检测指标的强弱存在着一定的局限性，必须严格相应的质控措施，同时，s/co值必须结合样本的稀释度才能客观反映乙肝病毒标志物的真实浓度，为临床分析和诊断病情提供可靠的诊断依据。", ShortCode = "SCO", Desc = "S/CO是OD值与CutOff值的比值。一般来说，检测病毒性肝炎血清标志物最普通的方法为酶联免疫吸附试验。大多数医院都采用s/co的方式向临床出具报告，可看作是病毒性肝炎标志物的半定量分析，在一定程度上可以反映病毒标志物的强弱，但由于免疫检测方法学敏感性强，检测试剂品种繁多，变异大，线性范围小，所以应该对不同肝炎病人的血清做进一步的分析，才能对肝炎病毒标志物的浓度进行正确的检测与评估。，s/co值越大，说明血清中HBsAg浓度越高。但由于其方法学特定的局限性，实验室要快速报告结果，大都采用“一步法”，部分厂家的试剂存在质量差异，抗原抗体结合有一定线性区、等价区和抗原过剩区，只有当HBsAg浓度性范围内，s/co值与HBsAg浓度呈正相关，HBsAg浓度超过线性范围时，s/co值不能客观地反映患者体内HBsAg真实水平。因此，临床报告结果时，用s/co值来说明检测指标的强弱存在着一定的局限性，必须严格相应的质控措施，同时，s/co值必须结合样本的稀释度才能客观反映乙肝病毒标志物的真实浓度，为临床分析和诊断病情提供可靠的诊断依据。", ContextType = SysDic.All, Length = 8)]
        public virtual double? SCO
		{
			get { return _sCO; }
			set { _sCO = value; }
		}

        [DataMember]
        [DataDesc(CName = "报告结果值", ShortCode = "ReportResult", Desc = "报告结果值", ContextType = SysDic.All, Length = 100)]
        public virtual string ReportResult
		{
			get { return _reportResult; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ReportResult", value, value.ToString());
				_reportResult = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "定性结果颜色标识", ShortCode = "ShowColor", Desc = "定性结果颜色标识", ContextType = SysDic.All, Length = 4)]
        public virtual int? ShowColor
		{
			get { return _showColor; }
			set { _showColor = value; }
		}

        [DataMember]
        [DataDesc(CName = "标准品标本次序", ShortCode = "BZPSampleOrder", Desc = "标准品标本次序", ContextType = SysDic.All, Length = 4)]
        public virtual int? BZPSampleOrder
		{
			get { return _bZPSampleOrder; }
			set { _bZPSampleOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "同型标准品标本次序", ShortCode = "SameBZPSampleOrder", Desc = "同型标准品标本次序", ContextType = SysDic.All, Length = 4)]
        public virtual int? SameBZPSampleOrder
		{
			get { return _sameBZPSampleOrder; }
			set { _sameBZPSampleOrder = value; }
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
        [DataDesc(CName = "仪器项目关系", ShortCode = "EPEquipItem", Desc = "仪器项目关系")]
        public virtual EPEquipItem EPEquipItem
        {
            get { return _ePEquipItem; }
            set { _ePEquipItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "存储酶免板的板信息：", ShortCode = "MEImmunePlate", Desc = "存储酶免板的板信息：")]
		public virtual MEImmunePlate MEImmunePlate
		{
			get { return _mEImmunePlate; }
			set { _mEImmunePlate = value; }
		}


        [DataMember]
        [DataDesc(CName = "质控物", ShortCode = "QCMat", Desc = "质控物")]
        public virtual GMGroup GMGroup
		{
            get { return _gmGroup; }
            set { _gmGroup = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作返回标志", ShortCode = "ReturnFlag", Desc = "操作返回标志", ContextType = SysDic.All, Length = 4)]
        public virtual bool ReturnFlag
        {
            get; 
            set;
        }

        [DataMember]
        [DataDesc(CName = "操作返回信息", ShortCode = "ReturnInfo", Desc = "操作返回信息", ContextType = SysDic.All, Length = 1000)]
        public virtual string ReturnInfo
        {
            get;
            set;
        }        
		#endregion
	}
	#endregion
}