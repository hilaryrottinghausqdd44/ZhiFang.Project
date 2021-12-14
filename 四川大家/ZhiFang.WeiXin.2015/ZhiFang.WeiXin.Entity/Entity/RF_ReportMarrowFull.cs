using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region RFReportMarrowFull

	/// <summary>
	/// RFReportMarrowFull object for NHibernate mapped table 'RF_ReportMarrowFull'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "报告单骨髓病例结果", ClassCName = "RFReportMarrowFull", ShortCode = "RFReportMarrowFull", Desc = "报告单骨髓病例结果")]
	public class RFReportMarrowFull : BaseEntity
	{
		#region Member Variables
		
        protected long? _reportFormIndexID;
        protected string _ReportFormID;
        protected int _reportItemID;
        protected int _parItemNo;
        protected int _itemNo;
        protected int _bloodNum;
        protected double _bloodPercent;
        protected int _marrowNum;
        protected double _marrowPercent;
        protected string _bloodDesc;
        protected string _marrowDesc;
        protected int _statusNo;
        protected string _refRange;
        protected int _equipNo;
        protected int _isCale;
        protected int _modified;
        protected DateTime? _itemDate;
        protected DateTime? _itemTime;
        protected int _isMatch;
        protected string _resultStatus;
        protected string _formNo;
        protected int _cItemNo;
        protected string _reportText;
        protected double _orgValue;
        protected string _orgDesc;
        protected int _isPrint;
        protected int _printOrder;
        protected string _itemname;
        protected int _valueTypeNo;
        protected double _reportValue;
        protected string _reportDesc;
        protected byte[] _reportImage;
        protected string _barcode;
        protected string _equipName;
        protected DateTime? _receiveDate;
        protected int _sectionNo;
        protected string _itemCName;
        protected string _itemEName;
        protected string _parItemCName;
        protected string _parItemEName;
        protected int _testTypeNo;
        protected string _sampleNo;
		

		#endregion

		#region Constructors

		public RFReportMarrowFull() { }

        public RFReportMarrowFull(long reportFormIndexID, string ReportFormID, int reportItemID, int parItemNo, int itemNo, int bloodNum, double bloodPercent, int marrowNum, double marrowPercent, string bloodDesc, string marrowDesc, int statusNo, string refRange, int equipNo, int isCale, int modified, DateTime itemDate, DateTime itemTime, int isMatch, string resultStatus, string formNo, int cItemNo, string reportText, double orgValue, string orgDesc, int isPrint, int printOrder, string itemname, int valueTypeNo, double reportValue, string reportDesc, byte[] reportImage, string barcode, string equipName, DateTime receiveDate, int sectionNo, string itemCName, string itemEName, string parItemCName, string parItemEName, int testTypeNo, string sampleNo)
		{
			this._reportFormIndexID = reportFormIndexID;
            this._ReportFormID = ReportFormID;
			this._reportItemID = reportItemID;
			this._parItemNo = parItemNo;
			this._itemNo = itemNo;
			this._bloodNum = bloodNum;
			this._bloodPercent = bloodPercent;
			this._marrowNum = marrowNum;
			this._marrowPercent = marrowPercent;
			this._bloodDesc = bloodDesc;
			this._marrowDesc = marrowDesc;
			this._statusNo = statusNo;
			this._refRange = refRange;
			this._equipNo = equipNo;
			this._isCale = isCale;
			this._modified = modified;
			this._itemDate = itemDate;
			this._itemTime = itemTime;
			this._isMatch = isMatch;
			this._resultStatus = resultStatus;
			this._formNo = formNo;
			this._cItemNo = cItemNo;
			this._reportText = reportText;
			this._orgValue = orgValue;
			this._orgDesc = orgDesc;
			this._isPrint = isPrint;
			this._printOrder = printOrder;
			this._itemname = itemname;
			this._valueTypeNo = valueTypeNo;
			this._reportValue = reportValue;
			this._reportDesc = reportDesc;
			this._reportImage = reportImage;
			this._barcode = barcode;
			this._equipName = equipName;
			this._receiveDate = receiveDate;
			this._sectionNo = sectionNo;
			this._itemCName = itemCName;
			this._itemEName = itemEName;
			this._parItemCName = parItemCName;
			this._parItemEName = parItemEName;
			this._testTypeNo = testTypeNo;
			this._sampleNo = sampleNo;
		}

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
			get { return _ReportFormID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for 报告单ID", value, value.ToString());
				_ReportFormID = value;
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
        [DataDesc(CName = "", ShortCode = "ParItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ParItemNo
		{
			get { return _parItemNo; }
			set { _parItemNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemNo
		{
			get { return _itemNo; }
			set { _itemNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodNum", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BloodNum
		{
			get { return _bloodNum; }
			set { _bloodNum = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BloodPercent", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double BloodPercent
		{
			get { return _bloodPercent; }
			set { _bloodPercent = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MarrowNum", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MarrowNum
		{
			get { return _marrowNum; }
			set { _marrowNum = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MarrowPercent", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double MarrowPercent
		{
			get { return _marrowPercent; }
			set { _marrowPercent = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodDesc", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string BloodDesc
		{
			get { return _bloodDesc; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for BloodDesc", value, value.ToString());
				_bloodDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MarrowDesc", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string MarrowDesc
		{
			get { return _marrowDesc; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for MarrowDesc", value, value.ToString());
				_marrowDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int StatusNo
		{
			get { return _statusNo; }
			set { _statusNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RefRange", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string RefRange
		{
			get { return _refRange; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for RefRange", value, value.ToString());
				_refRange = value;
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
        [DataDesc(CName = "", ShortCode = "IsCale", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCale
		{
			get { return _isCale; }
			set { _isCale = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Modified", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Modified
		{
			get { return _modified; }
			set { _modified = value; }
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
        [DataDesc(CName = "", ShortCode = "IsMatch", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsMatch
		{
			get { return _isMatch; }
			set { _isMatch = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ResultStatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ResultStatus
		{
			get { return _resultStatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ResultStatus", value, value.ToString());
				_resultStatus = value;
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
        [DataDesc(CName = "", ShortCode = "CItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CItemNo
		{
			get { return _cItemNo; }
			set { _cItemNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportText", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string ReportText
		{
			get { return _reportText; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for ReportText", value, value.ToString());
				_reportText = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrgValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double OrgValue
		{
			get { return _orgValue; }
			set { _orgValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrgDesc", Desc = "", ContextType = SysDic.All, Length = 400)]
        public virtual string OrgDesc
		{
			get { return _orgDesc; }
			set
			{
				if ( value != null && value.Length > 400)
					throw new ArgumentOutOfRangeException("Invalid value for OrgDesc", value, value.ToString());
				_orgDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsPrint", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPrint
		{
			get { return _isPrint; }
			set { _isPrint = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintOrder
		{
			get { return _printOrder; }
			set { _printOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Itemname", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Itemname
		{
			get { return _itemname; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Itemname", value, value.ToString());
				_itemname = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ValueTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ValueTypeNo
		{
			get { return _valueTypeNo; }
			set { _valueTypeNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReportValue", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double ReportValue
		{
			get { return _reportValue; }
			set { _reportValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportDesc", Desc = "", ContextType = SysDic.All, Length = 400)]
        public virtual string ReportDesc
		{
			get { return _reportDesc; }
			set
			{
				if ( value != null && value.Length > 400)
					throw new ArgumentOutOfRangeException("Invalid value for ReportDesc", value, value.ToString());
				_reportDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportImage", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] ReportImage
		{
			get { return _reportImage; }
			set { _reportImage = value; }
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
        [DataDesc(CName = "", ShortCode = "ItemCName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ItemCName
		{
			get { return _itemCName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ItemCName", value, value.ToString());
				_itemCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemEName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ItemEName
		{
			get { return _itemEName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ItemEName", value, value.ToString());
				_itemEName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ParItemCName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ParItemCName
		{
			get { return _parItemCName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ParItemCName", value, value.ToString());
				_parItemCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ParItemEName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ParItemEName
		{
			get { return _parItemEName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ParItemEName", value, value.ToString());
				_parItemEName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestTypeNo
		{
			get { return _testTypeNo; }
			set { _testTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual string SampleNo
		{
			get { return _sampleNo; }
			set { _sampleNo = value; }
		}

		
		#endregion
	}
	#endregion
}