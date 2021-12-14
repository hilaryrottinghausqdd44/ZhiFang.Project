using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCData

	/// <summary>
	/// QCData object for NHibernate mapped table 'QCData'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "QCData", ShortCode = "QCData", Desc = "")]
	public class QCData : BaseEntity
	{
		#region Member Variables
		
        protected DateTime _receiveDate;
        protected int _qCDataLotNo;
        protected string _receiveTime;
        protected double _userValue;
        protected double _originalValue;
        protected int _ruleIndex;
        protected int _ifUse;
        protected string _operator;
        protected string _loserule;
        protected string _losememo;
        protected string _loseoperator;
        protected int _iloseType;
        protected string _loseType;
        protected string _loseReason;
        protected string _correctMeasure;
        protected double _correctValue;
        protected string _userDesc;
        protected string _originalDesc;
        protected int _plateNo;
        protected int _positionNo;
        protected int _isAnaly;
		protected QCItem _qCItem;

		#endregion

		#region Constructors

		public QCData() { }

		

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReceiveDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime ReceiveDate
		{
			get { return _receiveDate; }
			set { _receiveDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCDataLotNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int QCDataLotNo
		{
			get { return _qCDataLotNo; }
			set { _qCDataLotNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReceiveTime", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ReceiveTime
		{
			get { return _receiveTime; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveTime", value, value.ToString());
				_receiveTime = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "UserValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double UserValue
		{
			get { return _userValue; }
			set { _userValue = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OriginalValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double OriginalValue
		{
			get { return _originalValue; }
			set { _originalValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RuleIndex", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int RuleIndex
		{
			get { return _ruleIndex; }
			set { _ruleIndex = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IfUse", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IfUse
		{
			get { return _ifUse; }
			set { _ifUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Operator", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Operator
		{
			get { return _operator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Operator", value, value.ToString());
				_operator = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Loserule", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Loserule
		{
			get { return _loserule; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Loserule", value, value.ToString());
				_loserule = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Losememo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Losememo
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
        [DataDesc(CName = "", ShortCode = "Loseoperator", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Loseoperator
		{
			get { return _loseoperator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Loseoperator", value, value.ToString());
				_loseoperator = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IloseType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IloseType
		{
			get { return _iloseType; }
			set { _iloseType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LoseType", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string LoseType
		{
			get { return _loseType; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for LoseType", value, value.ToString());
				_loseType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LoseReason", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "CorrectMeasure", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CorrectValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double CorrectValue
		{
			get { return _correctValue; }
			set { _correctValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserDesc", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string UserDesc
		{
			get { return _userDesc; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for UserDesc", value, value.ToString());
				_userDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OriginalDesc", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string OriginalDesc
		{
			get { return _originalDesc; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for OriginalDesc", value, value.ToString());
				_originalDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PlateNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PlateNo
		{
			get { return _plateNo; }
			set { _plateNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PositionNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PositionNo
		{
			get { return _positionNo; }
			set { _positionNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsAnaly", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsAnaly
		{
			get { return _isAnaly; }
			set { _isAnaly = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCItem", Desc = "")]
		public virtual QCItem QCItem
		{
			get { return _qCItem; }
			set { _qCItem = value; }
		}

        
		#endregion
	}
	#endregion
}