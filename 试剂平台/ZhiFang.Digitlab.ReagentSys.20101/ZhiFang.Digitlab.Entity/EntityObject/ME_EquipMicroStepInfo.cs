using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEEquipMicroStepInfo

	/// <summary>
    /// 仪器微生物检验详细结果
	/// </summary>
    [DataContract]
    [DataDesc(CName = "仪器微生物检验详细结果", ClassCName = "MEEquipMicroStepInfo", ShortCode = "MEEquipMicroStepInfo", Desc = "仪器微生物检验详细结果")]
	public class MEEquipMicroStepInfo : BaseEntity
	{
		#region Member Variables
		
        protected string _microChannel;
        protected string _reportValue;
        protected string _resultLink;
        protected string _resultComment;
        protected DateTime? _dataUpdateTime;
		protected BMicro _bMicro;
		protected MEEquipSampleForm _mEEquipSampleForm;
		protected MEEquipSampleItem _mEEquipSampleItem;
		protected IList<MEEquipMicroSuscResult> _mEEquipMicroSuscResultList; 

		#endregion

		#region Constructors

		public MEEquipMicroStepInfo() { }

		public MEEquipMicroStepInfo( long labID, string microChannel, string reportValue, string resultLink, string resultComment, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicro bMicro, MEEquipSampleForm mEEquipSampleForm, MEEquipSampleItem mEEquipSampleItem )
		{
			this._labID = labID;
			this._microChannel = microChannel;
			this._reportValue = reportValue;
			this._resultLink = resultLink;
			this._resultComment = resultComment;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicro = bMicro;
			this._mEEquipSampleForm = mEEquipSampleForm;
			this._mEEquipSampleItem = mEEquipSampleItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "微生物通道号", ShortCode = "MicroChannel", Desc = "微生物通道号", ContextType = SysDic.All, Length = 50)]
        public virtual string MicroChannel
		{
			get { return _microChannel; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MicroChannel", value, value.ToString());
				_microChannel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "报告值:当微生物ID为空时，当前记录的报告值为阴性结果描述；不为空时，是细菌结果描述", ShortCode = "ReportValue", Desc = "报告值:当微生物ID为空时，当前记录的报告值为阴性结果描述；不为空时，是细菌结果描述", ContextType = SysDic.All, Length = 300)]
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
                //if ( value != null && value.Length > 16)
                //    throw new ArgumentOutOfRangeException("Invalid value for ResultComment", value, value.ToString());
				_resultComment = value;
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
        [DataDesc(CName = "微生物", ShortCode = "BMicro", Desc = "微生物")]
		public virtual BMicro BMicro
		{
			get { return _bMicro; }
			set { _bMicro = value; }
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

        [DataMember]
        [DataDesc(CName = "微生物药敏结果", ShortCode = "MEEquipMicroSuscResultList", Desc = "微生物药敏结果")]
		public virtual IList<MEEquipMicroSuscResult> MEEquipMicroSuscResultList
		{
			get
			{
				if (_mEEquipMicroSuscResultList==null)
				{
					_mEEquipMicroSuscResultList = new List<MEEquipMicroSuscResult>();
				}
				return _mEEquipMicroSuscResultList;
			}
			set { _mEEquipMicroSuscResultList = value; }
		}

        
		#endregion
	}
	#endregion
}