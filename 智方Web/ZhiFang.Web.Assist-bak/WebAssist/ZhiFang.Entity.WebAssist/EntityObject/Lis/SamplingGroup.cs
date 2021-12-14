using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region SamplingGroup

	/// <summary>
	/// SamplingGroup object for NHibernate mapped table 'SamplingGroup'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SamplingGroup", ShortCode = "SamplingGroup", Desc = "")]
	public class SamplingGroup : BaseEntity
	{
		#region Member Variables
		
        protected string _samplingGroupName;
        protected int _sampleTypeNo;
        protected int _cubeType;
        protected string _cubeColor;
        protected int _specialtyType;
        protected string _shortname;
        protected string _shortcode;
        protected string _destination;
        protected decimal _capability;
        protected string _unit;
        protected decimal _mincapability;
        protected int _disporder;
        protected string _synopsis;
        protected double _appCap;
        protected string _printerName;
        protected string _shortCode2;
        protected int _printCount;
        protected int _chargeMode;
        protected int _chargeID1;
        protected int _chargeID2;
        protected int _chargeID3;
        protected string _affixTubeFlag;
        protected int _virtualNo;

		#endregion

		#region Constructors

		public SamplingGroup() { }

		public SamplingGroup( string samplingGroupName, int sampleTypeNo, int cubeType, string cubeColor, int specialtyType, string shortname, string shortcode, string destination, decimal capability, string unit, decimal mincapability, int disporder, string synopsis, double appCap, string printerName, string shortCode2, int printCount, int chargeMode, int chargeID1, int chargeID2, int chargeID3, string affixTubeFlag, int virtualNo )
		{
			this._samplingGroupName = samplingGroupName;
			this._sampleTypeNo = sampleTypeNo;
			this._cubeType = cubeType;
			this._cubeColor = cubeColor;
			this._specialtyType = specialtyType;
			this._shortname = shortname;
			this._shortcode = shortcode;
			this._destination = destination;
			this._capability = capability;
			this._unit = unit;
			this._mincapability = mincapability;
			this._disporder = disporder;
			this._synopsis = synopsis;
			this._appCap = appCap;
			this._printerName = printerName;
			this._shortCode2 = shortCode2;
			this._printCount = printCount;
			this._chargeMode = chargeMode;
			this._chargeID1 = chargeID1;
			this._chargeID2 = chargeID2;
			this._chargeID3 = chargeID3;
			this._affixTubeFlag = affixTubeFlag;
			this._virtualNo = virtualNo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "SamplingGroupName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SamplingGroupName
		{
			get { return _samplingGroupName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SamplingGroupName", value, value.ToString());
				_samplingGroupName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set { _sampleTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CubeType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CubeType
		{
			get { return _cubeType; }
			set { _cubeType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CubeColor", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CubeColor
		{
			get { return _cubeColor; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CubeColor", value, value.ToString());
				_cubeColor = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SpecialtyType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SpecialtyType
		{
			get { return _specialtyType; }
			set { _specialtyType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortname", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortname
		{
			get { return _shortname; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortname", value, value.ToString());
				_shortname = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "Destination", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Destination
		{
			get { return _destination; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Destination", value, value.ToString());
				_destination = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Capability", Desc = "", ContextType = SysDic.All, Length = 9)]
        public virtual decimal Capability
		{
			get { return _capability; }
			set { _capability = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Unit", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Unit
		{
			get { return _unit; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Unit", value, value.ToString());
				_unit = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Mincapability", Desc = "", ContextType = SysDic.All, Length = 9)]
        public virtual decimal Mincapability
		{
			get { return _mincapability; }
			set { _mincapability = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Disporder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Disporder
		{
			get { return _disporder; }
			set { _disporder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Synopsis", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Synopsis
		{
			get { return _synopsis; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Synopsis", value, value.ToString());
				_synopsis = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AppCap", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double AppCap
		{
			get { return _appCap; }
			set { _appCap = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrinterName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string PrinterName
		{
			get { return _printerName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for PrinterName", value, value.ToString());
				_printerName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode2", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode2
		{
			get { return _shortCode2; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode2", value, value.ToString());
				_shortCode2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
		{
			get { return _printCount; }
			set { _printCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeMode", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeMode
		{
			get { return _chargeMode; }
			set { _chargeMode = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeID1", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeID1
		{
			get { return _chargeID1; }
			set { _chargeID1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeID2", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeID2
		{
			get { return _chargeID2; }
			set { _chargeID2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeID3", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeID3
		{
			get { return _chargeID3; }
			set { _chargeID3 = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AffixTubeFlag", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string AffixTubeFlag
		{
			get { return _affixTubeFlag; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for AffixTubeFlag", value, value.ToString());
				_affixTubeFlag = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "VirtualNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int VirtualNo
		{
			get { return _virtualNo; }
			set { _virtualNo = value; }
		}

        
		#endregion
	}
	#endregion
}