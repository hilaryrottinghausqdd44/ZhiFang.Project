using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEImmunePlate

	/// <summary>
	/// MEImmunePlate object for NHibernate mapped table 'ME_ImmunePlate'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "存储酶免板的板信息：", ClassCName = "MEImmunePlate", ShortCode = "MEImmunePlate", Desc = "存储酶免板的板信息：")]
	public class MEImmunePlate : BaseEntity
	{
		#region Member Variables
		
        protected string _plateName;
        protected string _plateComment;
        protected int _plateState;
        protected DateTime? _pStateUpdateTime;
        protected DateTime? _eReceiveTime;
        protected int _eHasReportResult;
        protected int _plateUseKValue;
        protected DateTime? _dataUpdateTime;
		protected EPBEquip _ePBEquip;
		protected MEEquipSampleForm _mEEquipSampleForm;
		protected IList<MEImmuneResults> _mEImmuneResultsList; 

		#endregion

		#region Constructors

		public MEImmunePlate() { }

		public MEImmunePlate( long labID, string plateName, string plateComment, int plateState, DateTime pStateUpdateTime, DateTime eReceiveTime, int eHasReportResult, int plateUseKValue, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EPBEquip ePBEquip, MEEquipSampleForm mEEquipSampleForm )
		{
			this._labID = labID;
			this._plateName = plateName;
			this._plateComment = plateComment;
			this._plateState = plateState;
			this._pStateUpdateTime = pStateUpdateTime;
			this._eReceiveTime = eReceiveTime;
			this._eHasReportResult = eHasReportResult;
			this._plateUseKValue = plateUseKValue;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._ePBEquip = ePBEquip;
			this._mEEquipSampleForm = mEEquipSampleForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "可视板号，用于标识酶免板", ShortCode = "PlateName", Desc = "可视板号，用于标识酶免板", ContextType = SysDic.All, Length = 40)]
        public virtual string PlateName
		{
			get { return _plateName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for PlateName", value, value.ToString());
				_plateName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "酶免板描述", ShortCode = "PlateComment", Desc = "酶免板描述", ContextType = SysDic.All, Length = 60)]
        public virtual string PlateComment
		{
			get { return _plateComment; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for PlateComment", value, value.ToString());
				_plateComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "状态如下：", ShortCode = "PlateState", Desc = "状态如下：", ContextType = SysDic.All, Length = 4)]
        public virtual int PlateState
		{
			get { return _plateState; }
			set { _plateState = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "酶免板状态更新时间", ShortCode = "PStateUpdateTime", Desc = "酶免板状态更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PStateUpdateTime
		{
			get { return _pStateUpdateTime; }
			set { _pStateUpdateTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器数据接收时间", ShortCode = "EReceiveTime", Desc = "仪器数据接收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? EReceiveTime
		{
			get { return _eReceiveTime; }
			set { _eReceiveTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "通讯程序发过来时，是否已有结果", ShortCode = "EHasReportResult", Desc = "通讯程序发过来时，是否已有结果", ContextType = SysDic.All, Length = 4)]
        public virtual int EHasReportResult
		{
			get { return _eHasReportResult; }
			set { _eHasReportResult = value; }
		}

        [DataMember]
        [DataDesc(CName = "用以记录每板计算时，是否减去空白值", ShortCode = "PlateUseKValue", Desc = "用以记录每板计算时，是否减去空白值", ContextType = SysDic.All, Length = 4)]
        public virtual int PlateUseKValue
		{
			get { return _plateUseKValue; }
			set { _plateUseKValue = value; }
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
        [DataDesc(CName = "仪器表", ShortCode = "EPBEquip", Desc = "仪器表")]
		public virtual EPBEquip EPBEquip
		{
			get { return _ePBEquip; }
			set { _ePBEquip = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器样本单", ShortCode = "MEEquipSampleForm", Desc = "仪器样本单")]
		public virtual MEEquipSampleForm MEEquipSampleForm
		{
			get { return _mEEquipSampleForm; }
			set { _mEEquipSampleForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "存储酶免板的96个孔的孔号、标本类型、检验项目、检验结果信息", ShortCode = "MEImmuneResultsList", Desc = "存储酶免板的96个孔的孔号、标本类型、检验项目、检验结果信息")]
		public virtual IList<MEImmuneResults> MEImmuneResultsList
		{
			get
			{
				if (_mEImmuneResultsList==null)
				{
					_mEImmuneResultsList = new List<MEImmuneResults>();
				}
				return _mEImmuneResultsList;
			}
			set { _mEImmuneResultsList = value; }
		}

        
		#endregion
	}
	#endregion
}