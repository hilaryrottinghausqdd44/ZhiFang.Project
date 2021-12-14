using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region Equipment

	/// <summary>
	/// Equipment object for NHibernate mapped table 'Equipment'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "Equipment", ShortCode = "Equipment", Desc = "")]
	public class Equipment : BaseEntityService
    {
		#region Member Variables
		
        protected string _cName;
        protected string _shortName;
        protected string _shortCode;
        protected int _sectionNo;
        protected string _computer;
        protected string _comProgram;
        protected string _comPort;
        protected string _baudRate;
        protected string _parity;
        protected string _dataBits;
        protected string _stopBits;
        protected int _visible;
        protected int _doubleDir;
        protected string _licenceKey;
        protected string _licenceType;
        protected DateTime? _licenceDate;
        protected string _sQH;
        protected int _sNo;
        protected int _sickType;
        protected int _useImmPlate;
        protected int _immCalc;
        protected string _commPara;
        protected string _reagentPara;
        protected int _equipfunctionTypeNo;
        protected string _equipIndexNo;
        protected string _sampleNoStart;
        protected string _sampleNoEnd;
		

		#endregion

		#region Constructors

		public Equipment() { }

		public Equipment( string cName, string shortName, string shortCode, int sectionNo, string computer, string comProgram, string comPort, string baudRate, string parity, string dataBits, string stopBits, int visible, int doubleDir, string licenceKey, string licenceType, DateTime licenceDate, string sQH, int sNo, int sickType, int useImmPlate, int immCalc, string commPara, string reagentPara, int equipfunctionTypeNo, string equipIndexNo, string sampleNoStart, string sampleNoEnd )
		{
			this._cName = cName;
			this._shortName = shortName;
			this._shortCode = shortCode;
			this._sectionNo = sectionNo;
			this._computer = computer;
			this._comProgram = comProgram;
			this._comPort = comPort;
			this._baudRate = baudRate;
			this._parity = parity;
			this._dataBits = dataBits;
			this._stopBits = stopBits;
			this._visible = visible;
			this._doubleDir = doubleDir;
			this._licenceKey = licenceKey;
			this._licenceType = licenceType;
			this._licenceDate = licenceDate;
			this._sQH = sQH;
			this._sNo = sNo;
			this._sickType = sickType;
			this._useImmPlate = useImmPlate;
			this._immCalc = immCalc;
			this._commPara = commPara;
			this._reagentPara = reagentPara;
			this._equipfunctionTypeNo = equipfunctionTypeNo;
			this._equipIndexNo = equipIndexNo;
			this._sampleNoStart = sampleNoStart;
			this._sampleNoEnd = sampleNoEnd;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ShortName
		{
			get { return _shortName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ShortName", value, value.ToString());
				_shortName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionNo
		{
			get { return _sectionNo; }
			set { _sectionNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Computer", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Computer
		{
			get { return _computer; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Computer", value, value.ToString());
				_computer = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ComProgram", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ComProgram
		{
			get { return _comProgram; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ComProgram", value, value.ToString());
				_comProgram = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ComPort", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ComPort
		{
			get { return _comPort; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ComPort", value, value.ToString());
				_comPort = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BaudRate", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BaudRate
		{
			get { return _baudRate; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for BaudRate", value, value.ToString());
				_baudRate = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Parity", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Parity
		{
			get { return _parity; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Parity", value, value.ToString());
				_parity = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DataBits", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string DataBits
		{
			get { return _dataBits; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for DataBits", value, value.ToString());
				_dataBits = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StopBits", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string StopBits
		{
			get { return _stopBits; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for StopBits", value, value.ToString());
				_stopBits = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoubleDir", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DoubleDir
		{
			get { return _doubleDir; }
			set { _doubleDir = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LicenceKey", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string LicenceKey
		{
			get { return _licenceKey; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for LicenceKey", value, value.ToString());
				_licenceKey = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LicenceType", Desc = "", ContextType = SysDic.All, Length = 25)]
        public virtual string LicenceType
		{
			get { return _licenceType; }
			set
			{
				if ( value != null && value.Length > 25)
					throw new ArgumentOutOfRangeException("Invalid value for LicenceType", value, value.ToString());
				_licenceType = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LicenceDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LicenceDate
		{
			get { return _licenceDate; }
			set { _licenceDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SQH", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual string SQH
		{
			get { return _sQH; }
			set
			{
				if ( value != null && value.Length > 4)
					throw new ArgumentOutOfRangeException("Invalid value for SQH", value, value.ToString());
				_sQH = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SNo
		{
			get { return _sNo; }
			set { _sNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SickType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SickType
		{
			get { return _sickType; }
			set { _sickType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UseImmPlate", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UseImmPlate
		{
			get { return _useImmPlate; }
			set { _useImmPlate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ImmCalc", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ImmCalc
		{
			get { return _immCalc; }
			set { _immCalc = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CommPara", Desc = "", ContextType = SysDic.All, Length = 800)]
        public virtual string CommPara
		{
			get { return _commPara; }
			set
			{
				if ( value != null && value.Length > 800)
					throw new ArgumentOutOfRangeException("Invalid value for CommPara", value, value.ToString());
				_commPara = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReagentPara", Desc = "", ContextType = SysDic.All, Length = 800)]
        public virtual string ReagentPara
		{
			get { return _reagentPara; }
			set
			{
				if ( value != null && value.Length > 800)
					throw new ArgumentOutOfRangeException("Invalid value for ReagentPara", value, value.ToString());
				_reagentPara = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipfunctionTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipfunctionTypeNo
		{
			get { return _equipfunctionTypeNo; }
			set { _equipfunctionTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipIndexNo", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string EquipIndexNo
		{
			get { return _equipIndexNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for EquipIndexNo", value, value.ToString());
				_equipIndexNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleNoStart", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SampleNoStart
		{
			get { return _sampleNoStart; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SampleNoStart", value, value.ToString());
				_sampleNoStart = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleNoEnd", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SampleNoEnd
		{
			get { return _sampleNoEnd; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SampleNoEnd", value, value.ToString());
				_sampleNoEnd = value;
			}
		}

		
		#endregion
	}
	#endregion
}