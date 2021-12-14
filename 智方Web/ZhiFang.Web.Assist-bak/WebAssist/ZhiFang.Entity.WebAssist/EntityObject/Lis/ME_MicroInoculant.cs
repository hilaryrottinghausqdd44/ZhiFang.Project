using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region MEMicroInoculant

	/// <summary>
	/// MEMicroInoculant object for NHibernate mapped table 'ME_MicroInoculant'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "MEMicroInoculant", ShortCode = "MEMicroInoculant", Desc = "")]
	public class MEMicroInoculant : BaseEntity
	{
		#region Member Variables
		
        protected string _sampleTypeName;
        protected string _barCode;
        protected string _amount;
        protected string _sampleCharacterName;
        protected long? _cultureMediumID;
        protected long? _microTestDevelopID;
        protected int _inoculantType;
        protected int _sampleTypeNo;
        protected string _cultureMediumName;
        protected string _cultureMediumNo;
        protected string _cultivationType;
        protected string _cultivationConditionName;
        protected double _pointoutTime;
        protected string _bottleSerialNo;
        protected string _cultureMediumEquipResult;
        protected DateTime? _cultureEndDatetime;
        protected string _cultureTotalTime;
        protected string _warnPositiveDatetime;
        protected string _desc;
        protected long? _pMicroInoculantID;
        protected int _empUserNo;
        protected string _empName;
        protected DateTime? _dataUpdateTime;
        protected bool _deleteFlag;
        protected string _cultureDesc;
        protected string _equipOnlineTime;
        protected string _equipOfflineTime;
        protected string _warnPositiveTimelength;
        protected string _bottlePosition;
        protected string _cultureNo;
        protected DateTime? _cultureStartDatetime;
        protected DateTime? _gTestDate;
        protected DateTime? _bottleAddTime;
        protected string _autoAddInfo;

		#endregion

		#region Constructors

		public MEMicroInoculant() { }

		public MEMicroInoculant( long labID, string sampleTypeName, string barCode, string amount, string sampleCharacterName, long cultureMediumID, long microTestDevelopID, int inoculantType, int sampleTypeNo, string cultureMediumName, string cultureMediumNo, string cultivationType, string cultivationConditionName, double pointoutTime, string bottleSerialNo, string cultureMediumEquipResult, DateTime cultureEndDatetime, string cultureTotalTime, string warnPositiveDatetime, string desc, long pMicroInoculantID, int empUserNo, string empName, DateTime dataAddTime, DateTime dataUpdateTime, bool deleteFlag, byte[] dataTimeStamp, string cultureDesc, string equipOnlineTime, string equipOfflineTime, string warnPositiveTimelength, string bottlePosition, string cultureNo, DateTime cultureStartDatetime, DateTime gTestDate, DateTime bottleAddTime, string autoAddInfo )
		{
			this._labID = labID;
			this._sampleTypeName = sampleTypeName;
			this._barCode = barCode;
			this._amount = amount;
			this._sampleCharacterName = sampleCharacterName;
			this._cultureMediumID = cultureMediumID;
			this._microTestDevelopID = microTestDevelopID;
			this._inoculantType = inoculantType;
			this._sampleTypeNo = sampleTypeNo;
			this._cultureMediumName = cultureMediumName;
			this._cultureMediumNo = cultureMediumNo;
			this._cultivationType = cultivationType;
			this._cultivationConditionName = cultivationConditionName;
			this._pointoutTime = pointoutTime;
			this._bottleSerialNo = bottleSerialNo;
			this._cultureMediumEquipResult = cultureMediumEquipResult;
			this._cultureEndDatetime = cultureEndDatetime;
			this._cultureTotalTime = cultureTotalTime;
			this._warnPositiveDatetime = warnPositiveDatetime;
			this._desc = desc;
			this._pMicroInoculantID = pMicroInoculantID;
			this._empUserNo = empUserNo;
			this._empName = empName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._deleteFlag = deleteFlag;
			this._dataTimeStamp = dataTimeStamp;
			this._cultureDesc = cultureDesc;
			this._equipOnlineTime = equipOnlineTime;
			this._equipOfflineTime = equipOfflineTime;
			this._warnPositiveTimelength = warnPositiveTimelength;
			this._bottlePosition = bottlePosition;
			this._cultureNo = cultureNo;
			this._cultureStartDatetime = cultureStartDatetime;
			this._gTestDate = gTestDate;
			this._bottleAddTime = bottleAddTime;
			this._autoAddInfo = autoAddInfo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleTypeName
		{
			get { return _sampleTypeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleTypeName", value, value.ToString());
				_sampleTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string BarCode
		{
			get { return _barCode; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
				_barCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Amount", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Amount
		{
			get { return _amount; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Amount", value, value.ToString());
				_amount = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleCharacterName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SampleCharacterName
		{
			get { return _sampleCharacterName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleCharacterName", value, value.ToString());
				_sampleCharacterName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CultureMediumID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? CultureMediumID
		{
			get { return _cultureMediumID; }
			set { _cultureMediumID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MicroTestDevelopID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? MicroTestDevelopID
		{
			get { return _microTestDevelopID; }
			set { _microTestDevelopID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InoculantType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int InoculantType
		{
			get { return _inoculantType; }
			set { _inoculantType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set { _sampleTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CultureMediumName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CultureMediumName
		{
			get { return _cultureMediumName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CultureMediumName", value, value.ToString());
				_cultureMediumName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CultureMediumNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CultureMediumNo
		{
			get { return _cultureMediumNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CultureMediumNo", value, value.ToString());
				_cultureMediumNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CultivationType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CultivationType
		{
			get { return _cultivationType; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CultivationType", value, value.ToString());
				_cultivationType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CultivationConditionName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CultivationConditionName
		{
			get { return _cultivationConditionName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CultivationConditionName", value, value.ToString());
				_cultivationConditionName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PointoutTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double PointoutTime
		{
			get { return _pointoutTime; }
			set { _pointoutTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BottleSerialNo", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string BottleSerialNo
		{
			get { return _bottleSerialNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for BottleSerialNo", value, value.ToString());
				_bottleSerialNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CultureMediumEquipResult", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string CultureMediumEquipResult
		{
			get { return _cultureMediumEquipResult; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for CultureMediumEquipResult", value, value.ToString());
				_cultureMediumEquipResult = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CultureEndDatetime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CultureEndDatetime
		{
			get { return _cultureEndDatetime; }
			set { _cultureEndDatetime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CultureTotalTime", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CultureTotalTime
		{
			get { return _cultureTotalTime; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CultureTotalTime", value, value.ToString());
				_cultureTotalTime = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WarnPositiveDatetime", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string WarnPositiveDatetime
		{
			get { return _warnPositiveDatetime; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for WarnPositiveDatetime", value, value.ToString());
				_warnPositiveDatetime = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Desc", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Desc
		{
			get { return _desc; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Desc", value, value.ToString());
				_desc = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PMicroInoculantID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? PMicroInoculantID
		{
			get { return _pMicroInoculantID; }
			set { _pMicroInoculantID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EmpUserNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EmpUserNo
		{
			get { return _empUserNo; }
			set { _empUserNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EmpName", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string EmpName
		{
			get { return _empName; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
				_empName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeleteFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool DeleteFlag
		{
			get { return _deleteFlag; }
			set { _deleteFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CultureDesc", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CultureDesc
		{
			get { return _cultureDesc; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CultureDesc", value, value.ToString());
				_cultureDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipOnlineTime", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipOnlineTime
		{
			get { return _equipOnlineTime; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EquipOnlineTime", value, value.ToString());
				_equipOnlineTime = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipOfflineTime", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipOfflineTime
		{
			get { return _equipOfflineTime; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EquipOfflineTime", value, value.ToString());
				_equipOfflineTime = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WarnPositiveTimelength", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string WarnPositiveTimelength
		{
			get { return _warnPositiveTimelength; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WarnPositiveTimelength", value, value.ToString());
				_warnPositiveTimelength = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BottlePosition", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string BottlePosition
		{
			get { return _bottlePosition; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for BottlePosition", value, value.ToString());
				_bottlePosition = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CultureNo", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string CultureNo
		{
			get { return _cultureNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for CultureNo", value, value.ToString());
				_cultureNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CultureStartDatetime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CultureStartDatetime
		{
			get { return _cultureStartDatetime; }
			set { _cultureStartDatetime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GTestDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GTestDate
		{
			get { return _gTestDate; }
			set { _gTestDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BottleAddTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BottleAddTime
		{
			get { return _bottleAddTime; }
			set { _bottleAddTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AutoAddInfo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string AutoAddInfo
		{
			get { return _autoAddInfo; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for AutoAddInfo", value, value.ToString());
				_autoAddInfo = value;
			}
		}

        
		#endregion
	}
	#endregion
}