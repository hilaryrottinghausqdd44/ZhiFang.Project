using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region RFReportMicroFull

	/// <summary>
	/// RFReportMicroFull object for NHibernate mapped table 'RF_ReportMicroFull'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "报告单微生物结果", ClassCName = "RFReportMicroFull", ShortCode = "RFReportMicroFull", Desc = "报告单微生物结果")]
	public class RFReportMicroFull : BaseEntity
	{
		#region Member Variables
		
        protected long? _reportFormIndexID;
        protected string _reportFormID;
        protected int _reportItemID;
        protected int _resultNo;
        protected int _itemNo;
        protected string _itemName;
        protected int _descNo;
        protected string _descName;
        protected int _microNo;
        protected string _microDesc;
        protected string _microName;
        protected int _antiNo;
        protected string _antiName;
        protected string _suscept;
        protected double _susQuan;
        protected string _refRange;
        protected string _susDesc;
        protected string _antiUnit;
        protected DateTime? _itemDate;
        protected DateTime? _itemTime;
        protected string _itemDesc;
        protected int _equipNo;
        protected int _modified;
        protected int _isMatch;
        protected int _checkType;
        protected string _serialNo;
        protected string _formNo;
        protected int _isReceive;
        protected string _microCountDesc;
        protected int _mresulttType;
        protected string _itemename;
        protected string _microename;
        protected string _antiename;
        protected string _antishortname;
        protected string _antishortcode;
        protected string _barcode;
        protected DateTime? _receiveDate;
        protected int _sectionNo;
        protected int _testTypeNo;
        protected string _sampleNo;
        protected string _equipName;
        protected string _methodName;
		

		#endregion

		#region Constructors

		public RFReportMicroFull() { }
        

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "报告单索引", ShortCode = "ReportFormIndexID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReportFormIndexID
		{
			get { return _reportFormIndexID; }
			set { _reportFormIndexID = value; }
		}

        [DataMember]
        [DataDesc(CName = "报告单ID", ShortCode = "ReportFormID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ReportFormID
		{
			get { return _reportFormID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for 报告单ID", value, value.ToString());
				_reportFormID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportItemID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReportItemID
		{
			get { return _reportItemID; }
			set { _reportItemID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ResultNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ResultNo
		{
			get { return _resultNo; }
			set { _resultNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemNo
		{
			get { return _itemNo; }
			set { _itemNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemName
		{
			get { return _itemName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ItemName", value, value.ToString());
				_itemName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DescNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DescNo
		{
			get { return _descNo; }
			set { _descNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DescName", Desc = "", ContextType = SysDic.All, Length = 250)]
        public virtual string DescName
		{
			get { return _descName; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for DescName", value, value.ToString());
				_descName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MicroNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MicroNo
		{
			get { return _microNo; }
			set { _microNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MicroDesc", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string MicroDesc
		{
			get { return _microDesc; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for MicroDesc", value, value.ToString());
				_microDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MicroName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string MicroName
		{
			get { return _microName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for MicroName", value, value.ToString());
				_microName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AntiNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AntiNo
		{
			get { return _antiNo; }
			set { _antiNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AntiName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string AntiName
		{
			get { return _antiName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for AntiName", value, value.ToString());
				_antiName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Suscept", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Suscept
		{
			get { return _suscept; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Suscept", value, value.ToString());
				_suscept = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SusQuan", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double SusQuan
		{
			get { return _susQuan; }
			set { _susQuan = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefRange", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string RefRange
		{
			get { return _refRange; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for RefRange", value, value.ToString());
				_refRange = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SusDesc", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SusDesc
		{
			get { return _susDesc; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SusDesc", value, value.ToString());
				_susDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AntiUnit", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AntiUnit
		{
			get { return _antiUnit; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AntiUnit", value, value.ToString());
				_antiUnit = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ItemDate
		{
			get { return _itemDate; }
			set { _itemDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ItemTime
		{
			get { return _itemTime; }
			set { _itemTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemDesc", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ItemDesc
		{
			get { return _itemDesc; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ItemDesc", value, value.ToString());
				_itemDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipNo
		{
			get { return _equipNo; }
			set { _equipNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Modified", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Modified
		{
			get { return _modified; }
			set { _modified = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsMatch", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsMatch
		{
			get { return _isMatch; }
			set { _isMatch = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckType
		{
			get { return _checkType; }
			set { _checkType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SerialNo", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string SerialNo
		{
			get { return _serialNo; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for SerialNo", value, value.ToString());
				_serialNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FormNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual string FormNo
		{
			get { return _formNo; }
			set { _formNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsReceive", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsReceive
		{
			get { return _isReceive; }
			set { _isReceive = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MicroCountDesc", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string MicroCountDesc
		{
			get { return _microCountDesc; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for MicroCountDesc", value, value.ToString());
				_microCountDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MresulttType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MresulttType
		{
			get { return _mresulttType; }
			set { _mresulttType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Itemename", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Itemename
		{
			get { return _itemename; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Itemename", value, value.ToString());
				_itemename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Microename", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Microename
		{
			get { return _microename; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Microename", value, value.ToString());
				_microename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Antiename", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Antiename
		{
			get { return _antiename; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Antiename", value, value.ToString());
				_antiename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Antishortname", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Antishortname
		{
			get { return _antishortname; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Antishortname", value, value.ToString());
				_antishortname = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Antishortcode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Antishortcode
		{
			get { return _antishortcode; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Antishortcode", value, value.ToString());
				_antishortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Barcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Barcode
		{
			get { return _barcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Barcode", value, value.ToString());
				_barcode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReceiveDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveDate
		{
			get { return _receiveDate; }
			set { _receiveDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionNo
		{
			get { return _sectionNo; }
			set { _sectionNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestTypeNo
		{
			get { return _testTypeNo; }
			set { _testTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SampleNo
		{
			get { return _sampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SampleNo", value, value.ToString());
				_sampleNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string EquipName
		{
			get { return _equipName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for EquipName", value, value.ToString());
				_equipName = value;
			}
		}
        [DataMember]
        [DataDesc(CName = "", ShortCode = "MethodName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string MethodName
        {
            get { return _methodName; }
            set
            {
                _methodName = value;
            }
        }
		
		#endregion
	}
	#endregion
}