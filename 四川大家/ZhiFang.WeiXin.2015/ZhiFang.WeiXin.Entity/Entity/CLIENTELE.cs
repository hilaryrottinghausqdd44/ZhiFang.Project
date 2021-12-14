using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region CLIENTELE

	/// <summary>
	/// CLIENTELE object for NHibernate mapped table 'CLIENTELE'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "实验室（送检单位、医疗机构、客户）", ClassCName = "CLIENTELE", ShortCode = "CLIENTELE", Desc = "实验室（送检单位、医疗机构、客户）")]
	public class CLIENTELE : BaseEntity
	{
		#region Member Variables
		
        protected string _cNAME;
        protected string _eNAME;
        protected string _sHORTCODE;
        protected double _iSUSE;
        protected string _lINKMAN;
        protected string _pHONENUM1;
        protected string _aDDRESS;
        protected string _mAILNO;
        protected string _eMAIL;
        protected string _pRINCIPAL;
        protected string _pHONENUM2;
        protected double _cLIENTTYPE;
        protected double _bmanno;
        protected string _romark;
        protected double _titletype;
        protected double _uploadtype;
        protected string _tstamp;
        protected double _printtype;
        protected double _inputDataType;
        protected double _reportpagetype;
        protected string _clientarea;
        protected string _clientstyle;
        protected string _faxNo;
        protected double _autoFax;
        protected string _clientReportTitle;
        protected double _isPrintItem;
        protected string _cZDY1;
        protected string _cZDY2;
        protected string _cZDY3;
        protected string _cZDY4;
        protected string _cZDY5;
        protected string _cZDY6;
        protected string _linkManPosition;
        protected string _linkMan1;
        protected string _linkManPosition1;
        protected string _clientcode;
        protected string _cWAccountDate;
        protected string _nFClientType;
        protected string _webLisSourceOrgID;
        protected string _doctor;
        protected string _businessName;
        protected double _labIsReply;
        protected double _labStatusFlag;
        protected double _labUploadFlag;
        protected DateTime? _labUploadDate;
        protected DateTime? _time;
        protected string _groupname;
        protected string _relationName;
        protected int _areaID;
		

		#endregion

		#region Constructors

		public CLIENTELE() { }

		public CLIENTELE( string cNAME, string eNAME, string sHORTCODE, double iSUSE, string lINKMAN, string pHONENUM1, string aDDRESS, string mAILNO, string eMAIL, string pRINCIPAL, string pHONENUM2, double cLIENTTYPE, double bmanno, string romark, double titletype, double uploadtype, string tstamp, double printtype, double inputDataType, double reportpagetype, string clientarea, string clientstyle, string faxNo, double autoFax, string clientReportTitle, double isPrintItem, string cZDY1, string cZDY2, string cZDY3, string cZDY4, string cZDY5, string cZDY6, string linkManPosition, string linkMan1, string linkManPosition1, string clientcode, string cWAccountDate, string nFClientType, string webLisSourceOrgID, string doctor, string businessName, double labIsReply, double labStatusFlag, double labUploadFlag, DateTime labUploadDate, DateTime time, string groupname, string relationName, int areaID, byte[] dTimeStampe )
		{
			this._cNAME = cNAME;
			this._eNAME = eNAME;
			this._sHORTCODE = sHORTCODE;
			this._iSUSE = iSUSE;
			this._lINKMAN = lINKMAN;
			this._pHONENUM1 = pHONENUM1;
			this._aDDRESS = aDDRESS;
			this._mAILNO = mAILNO;
			this._eMAIL = eMAIL;
			this._pRINCIPAL = pRINCIPAL;
			this._pHONENUM2 = pHONENUM2;
			this._cLIENTTYPE = cLIENTTYPE;
			this._bmanno = bmanno;
			this._romark = romark;
			this._titletype = titletype;
			this._uploadtype = uploadtype;
			this._tstamp = tstamp;
			this._printtype = printtype;
			this._inputDataType = inputDataType;
			this._reportpagetype = reportpagetype;
			this._clientarea = clientarea;
			this._clientstyle = clientstyle;
			this._faxNo = faxNo;
			this._autoFax = autoFax;
			this._clientReportTitle = clientReportTitle;
			this._isPrintItem = isPrintItem;
			this._cZDY1 = cZDY1;
			this._cZDY2 = cZDY2;
			this._cZDY3 = cZDY3;
			this._cZDY4 = cZDY4;
			this._cZDY5 = cZDY5;
			this._cZDY6 = cZDY6;
			this._linkManPosition = linkManPosition;
			this._linkMan1 = linkMan1;
			this._linkManPosition1 = linkManPosition1;
			this._clientcode = clientcode;
			this._cWAccountDate = cWAccountDate;
			this._nFClientType = nFClientType;
			this._webLisSourceOrgID = webLisSourceOrgID;
			this._doctor = doctor;
			this._businessName = businessName;
			this._labIsReply = labIsReply;
			this._labStatusFlag = labStatusFlag;
			this._labUploadFlag = labUploadFlag;
			this._labUploadDate = labUploadDate;
			this._time = time;
			this._groupname = groupname;
			this._relationName = relationName;
			this._areaID = areaID;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CNAME", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string CNAME
		{
			get { return _cNAME; }
			set
			{				
				_cNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ENAME", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string ENAME
		{
			get { return _eNAME; }
			set
			{
				_eNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SHORTCODE", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string SHORTCODE
		{
			get { return _sHORTCODE; }
			set
			{
				_sHORTCODE = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ISUSE", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double ISUSE
		{
			get { return _iSUSE; }
			set { _iSUSE = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LINKMAN", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string LINKMAN
		{
			get { return _lINKMAN; }
			set
			{
				_lINKMAN = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PHONENUM1", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string PHONENUM1
		{
			get { return _pHONENUM1; }
			set
			{				
				_pHONENUM1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ADDRESS", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string ADDRESS
		{
			get { return _aDDRESS; }
			set
			{
				_aDDRESS = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MAILNO", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string MAILNO
		{
			get { return _mAILNO; }
			set
			{
				_mAILNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EMAIL", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string EMAIL
		{
			get { return _eMAIL; }
			set
			{
				_eMAIL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PRINCIPAL", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string PRINCIPAL
		{
			get { return _pRINCIPAL; }
			set
			{
				_pRINCIPAL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PHONENUM2", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string PHONENUM2
		{
			get { return _pHONENUM2; }
			set
			{
				_pHONENUM2 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CLIENTTYPE", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double CLIENTTYPE
		{
			get { return _cLIENTTYPE; }
			set { _cLIENTTYPE = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Bmanno", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Bmanno
		{
			get { return _bmanno; }
			set { _bmanno = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Romark", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string Romark
		{
			get { return _romark; }
			set
			{
				_romark = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Titletype", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Titletype
		{
			get { return _titletype; }
			set { _titletype = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Uploadtype", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Uploadtype
		{
			get { return _uploadtype; }
			set { _uploadtype = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Tstamp", Desc = "", ContextType = SysDic.All, Length = 255)]
        public virtual string Tstamp
		{
			get { return _tstamp; }
			set
			{
				if ( value != null && value.Length > 255)
					throw new ArgumentOutOfRangeException("Invalid value for Tstamp", value, value.ToString());
				_tstamp = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Printtype", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Printtype
		{
			get { return _printtype; }
			set { _printtype = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InputDataType", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double InputDataType
		{
			get { return _inputDataType; }
			set { _inputDataType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Reportpagetype", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Reportpagetype
		{
			get { return _reportpagetype; }
			set { _reportpagetype = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Clientarea", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string Clientarea
		{
			get { return _clientarea; }
			set
			{			
				_clientarea = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Clientstyle", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string Clientstyle
		{
			get { return _clientstyle; }
			set
			{
				_clientstyle = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FaxNo", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string FaxNo
		{
			get { return _faxNo; }
			set
			{
				_faxNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AutoFax", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double AutoFax
		{
			get { return _autoFax; }
			set { _autoFax = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ClientReportTitle", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string ClientReportTitle
		{
			get { return _clientReportTitle; }
			set
			{
				_clientReportTitle = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "IsPrintItem", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double IsPrintItem
		{
			get { return _isPrintItem; }
			set { _isPrintItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CZDY1", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string CZDY1
		{
			get { return _cZDY1; }
			set
			{
				_cZDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CZDY2", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string CZDY2
		{
			get { return _cZDY2; }
			set
			{
				_cZDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CZDY3", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string CZDY3
		{
			get { return _cZDY3; }
			set
			{
				_cZDY3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CZDY4", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string CZDY4
		{
			get { return _cZDY4; }
			set
			{
				_cZDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CZDY5", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string CZDY5
		{
			get { return _cZDY5; }
			set
			{
				_cZDY5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CZDY6", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string CZDY6
		{
			get { return _cZDY6; }
			set
			{
				_cZDY6 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkManPosition", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string LinkManPosition
		{
			get { return _linkManPosition; }
			set
			{
				_linkManPosition = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkMan1", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string LinkMan1
		{
			get { return _linkMan1; }
			set
			{
				_linkMan1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkManPosition1", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string LinkManPosition1
		{
			get { return _linkManPosition1; }
			set
			{
				_linkManPosition1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Clientcode", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string Clientcode
		{
			get { return _clientcode; }
			set
			{
				_clientcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CWAccountDate", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string CWAccountDate
		{
			get { return _cWAccountDate; }
			set
			{
				_cWAccountDate = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NFClientType", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string NFClientType
		{
			get { return _nFClientType; }
			set
			{
				_nFClientType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WebLisSourceOrgID", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string WebLisSourceOrgID
		{
			get { return _webLisSourceOrgID; }
			set
			{
				_webLisSourceOrgID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Doctor", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string Doctor
		{
			get { return _doctor; }
			set
			{
				_doctor = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BusinessName", Desc = "", ContextType = SysDic.All, Length = -1)]
        public virtual string BusinessName
		{
			get { return _businessName; }
			set
			{
				_businessName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabIsReply", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LabIsReply
		{
			get { return _labIsReply; }
			set { _labIsReply = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabStatusFlag", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LabStatusFlag
		{
			get { return _labStatusFlag; }
			set { _labStatusFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabUploadFlag", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double LabUploadFlag
		{
			get { return _labUploadFlag; }
			set { _labUploadFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabUploadDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LabUploadDate
		{
			get { return _labUploadDate; }
			set { _labUploadDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Time", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Time
		{
			get { return _time; }
			set { _time = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Groupname", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Groupname
		{
			get { return _groupname; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Groupname", value, value.ToString());
				_groupname = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RelationName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string RelationName
		{
			get { return _relationName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RelationName", value, value.ToString());
				_relationName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AreaID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AreaID
		{
			get { return _areaID; }
			set { _areaID = value; }
		}
        

		
		#endregion
	}
	#endregion
}