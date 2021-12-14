using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCDLoseValue

	/// <summary>
	/// QCDLoseValue object for NHibernate mapped table 'QC_D_LoseValue'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "质控失控处理", ClassCName = "", ShortCode = "ZKSKCL", Desc = "质控失控处理")]
    public class QCDLoseValue : BaseEntity
	{
		#region Member Variables
		
		
		protected string _losememo;
		protected int _loseType;
		protected string _loseReason;
		protected string _correctMeasure;
		protected double _correctValue;
		protected DateTime? _loseExamineTime;
		protected bool _isUse;
		protected DateTime? _dataUpdateTime;
		protected QCDValue _qCDValue;
        protected HREmployee _loseoperator;
        protected HREmployee _loseExaminer;

		#endregion

		#region Constructors

		public QCDLoseValue() { }

		public QCDLoseValue( long labID, string losememo, int loseType, string loseReason, string correctMeasure, double correctValue, DateTime loseExamineTime, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, QCDValue qCDValue )
		{
			this._labID = labID;
			this._losememo = losememo;
			this._loseType = loseType;
			this._loseReason = loseReason;
			this._correctMeasure = correctMeasure;
			this._correctValue = correctValue;
			this._loseExamineTime = loseExamineTime;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._qCDValue = qCDValue;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "失控说明", ShortCode = "SKSM", Desc = "失控说明", ContextType = SysDic.NText, Length = 200)]
		public virtual string LoseMemo
		{
			get { return _losememo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Losememo", value, value.ToString());
				_losememo = value;
			}
		}
        
        [DataMember]
        [DataDesc(CName = "失控类型", ShortCode = "SKLX", Desc = "失控类型", ContextType = SysDic.Number, Length = 4)]
		public virtual int LoseType
		{
			get { return _loseType; }
			set { _loseType = value; }
		}

        [DataMember]
        [DataDesc(CName = "失控原因", ShortCode = "SKYY", Desc = "失控原因", ContextType = SysDic.NText, Length = 50)]
		public virtual string LoseReason
		{
			get { return _loseReason; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LoseReason", value, value.ToString());
				_loseReason = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "纠正措施", ShortCode = "JZCS", Desc = "纠正措施", ContextType = SysDic.NText, Length = 50)]
		public virtual string CorrectMeasure
		{
			get { return _correctMeasure; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CorrectMeasure", value, value.ToString());
				_correctMeasure = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "纠正值", ShortCode = "JZZ", Desc = "纠正值", ContextType = SysDic.Number, Length = 8)]
		public virtual double CorrectValue
		{
			get { return _correctValue; }
			set { _correctValue = value; }
		}
        
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失控审核时间", ShortCode = "SKSHSJ", Desc = "失控审核时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? LoseExamineTime
		{
			get { return _loseExamineTime; }
			set { _loseExamineTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "SFSY", Desc = "是否使用", ContextType = SysDic.All)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "质控数据", ShortCode = "ZKSJ", Desc = "质控数据")]
		public virtual QCDValue QCDValue
		{
			get { return _qCDValue; }
			set { _qCDValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "失控处理人", ShortCode = "SKCLR", Desc = "失控处理人")]
        public virtual HREmployee LoseOperator
        {
            get { return _loseoperator; }
            set { _loseoperator = value; }
        }

        [DataMember]
        [DataDesc(CName = "失控审核人", ShortCode = "SKSHR", Desc = "失控审核人")]
        public virtual HREmployee LoseExaminer
        {
            get { return _loseExaminer; }
            set { _loseExaminer = value; }
        }

		#endregion
	}
	#endregion
}