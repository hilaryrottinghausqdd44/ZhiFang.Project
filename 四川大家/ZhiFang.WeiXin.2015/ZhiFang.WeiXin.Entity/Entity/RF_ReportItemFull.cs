using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region RFReportItemFull

	/// <summary>
	/// RFReportItemFull object for NHibernate mapped table 'RF_ReportItemFull'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "报告单项目", ClassCName = "RFReportItemFull", ShortCode = "RFReportItemFull", Desc = "报告单项目")]
	public class RFReportItemFull : BaseEntity
	{
		#region Member Variables
		
        protected long? _reportFormIndexID;
        protected string _reportFormID;
        protected int _reportItemID;
        protected string _tESTITEMNAME;
        protected string _tESTITEMSNAME;
        protected DateTime? _rECEIVEDATE;
        protected string _sECTIONNO;
        protected string _tESTTYPENO;
        protected string _sAMPLENO;
        protected string _pARITEMNO;
        protected string _iTEMNO;
        protected string _oRIGINALVALUE;
        protected string _rEPORTVALUE;
        protected string _oRIGINALDESC;
        protected string _rEPORTDESC;
        protected string _sTATUSNO;
        protected string _eQUIPNO;
        protected string _mODIFIED;
        protected string _rEFRANGE;
        protected DateTime? _iTEMDATE;
        protected DateTime? _iTEMTIME;
        protected string _iSMATCH;
        protected string _rESULTSTATUS;
        protected DateTime? _tESTITEMDATETIME;
        protected string _rEPORTVALUEALL;
        protected string _pARITEMNAME;
        protected string _pARITEMSNAME;
        protected string _dISPORDER;
        protected string _iTEMORDER;
        protected string _uNIT;
        protected string _sERIALNO;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected string _hISORDERNO;
        protected string _fORMNO;
        protected string _tECHNICIAN;
        protected string _oLDSERIALNO;
        protected string _pREC;
        protected string _itemunit;
        protected string _itemename;
        protected int _secretgrade;
        protected string _shortname;
        protected string _shortcode;
        protected int _cuegrade;
        protected string _zDY6;
        protected string _zDY7;
        protected string _zDY8;
        protected string _zDY9;
        protected string _zDY10;
        protected int _curritemredo;
        protected string _barcode;
        protected string _sectionName;
        protected string _equipName;
        protected int _checkType;
        protected string _checkTypeName;
		

		#endregion

		#region Constructors

		public RFReportItemFull() { }

        public RFReportItemFull(long reportFormIndexID, string reportFormID, int reportItemID, string tESTITEMNAME, string tESTITEMSNAME, DateTime rECEIVEDATE, string sECTIONNO, string tESTTYPENO, string sAMPLENO, string pARITEMNO, string iTEMNO, string oRIGINALVALUE, string rEPORTVALUE, string oRIGINALDESC, string rEPORTDESC, string sTATUSNO, string eQUIPNO, string mODIFIED, string rEFRANGE, DateTime iTEMDATE, DateTime iTEMTIME, string iSMATCH, string rESULTSTATUS, DateTime tESTITEMDATETIME, string rEPORTVALUEALL, string pARITEMNAME, string pARITEMSNAME, string dISPORDER, string iTEMORDER, string uNIT, string sERIALNO, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, string hISORDERNO, string fORMNO, string tECHNICIAN, string oLDSERIALNO, string pREC, string itemunit, string itemename, int secretgrade, string shortname, string shortcode, int cuegrade, string zDY6, string zDY7, string zDY8, string zDY9, string zDY10, int curritemredo, string barcode, string sectionName, string equipName, int checkType, string checkTypeName)
		{
			this._reportFormIndexID = reportFormIndexID;
			this._reportFormID = reportFormID;
			this._reportItemID = reportItemID;
			this._tESTITEMNAME = tESTITEMNAME;
			this._tESTITEMSNAME = tESTITEMSNAME;
			this._rECEIVEDATE = rECEIVEDATE;
			this._sECTIONNO = sECTIONNO;
			this._tESTTYPENO = tESTTYPENO;
			this._sAMPLENO = sAMPLENO;
			this._pARITEMNO = pARITEMNO;
			this._iTEMNO = iTEMNO;
			this._oRIGINALVALUE = oRIGINALVALUE;
			this._rEPORTVALUE = rEPORTVALUE;
			this._oRIGINALDESC = oRIGINALDESC;
			this._rEPORTDESC = rEPORTDESC;
			this._sTATUSNO = sTATUSNO;
			this._eQUIPNO = eQUIPNO;
			this._mODIFIED = mODIFIED;
			this._rEFRANGE = rEFRANGE;
			this._iTEMDATE = iTEMDATE;
			this._iTEMTIME = iTEMTIME;
			this._iSMATCH = iSMATCH;
			this._rESULTSTATUS = rESULTSTATUS;
			this._tESTITEMDATETIME = tESTITEMDATETIME;
			this._rEPORTVALUEALL = rEPORTVALUEALL;
			this._pARITEMNAME = pARITEMNAME;
			this._pARITEMSNAME = pARITEMSNAME;
			this._dISPORDER = dISPORDER;
			this._iTEMORDER = iTEMORDER;
			this._uNIT = uNIT;
			this._sERIALNO = sERIALNO;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._zDY4 = zDY4;
			this._zDY5 = zDY5;
			this._hISORDERNO = hISORDERNO;
			this._fORMNO = fORMNO;
			this._tECHNICIAN = tECHNICIAN;
			this._oLDSERIALNO = oLDSERIALNO;
			this._pREC = pREC;
			this._itemunit = itemunit;
			this._itemename = itemename;
			this._secretgrade = secretgrade;
			this._shortname = shortname;
			this._shortcode = shortcode;
			this._cuegrade = cuegrade;
			this._zDY6 = zDY6;
			this._zDY7 = zDY7;
			this._zDY8 = zDY8;
			this._zDY9 = zDY9;
			this._zDY10 = zDY10;
			this._curritemredo = curritemredo;
			this._barcode = barcode;
			this._sectionName = sectionName;
			this._equipName = equipName;
			this._checkType = checkType;
			this._checkTypeName = checkTypeName;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReportFormIndexID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReportFormIndexID
		{
			get { return _reportFormIndexID; }
			set { _reportFormIndexID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportFormID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ReportFormID
		{
			get { return _reportFormID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReportFormID", value, value.ToString());
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
        [DataDesc(CName = "", ShortCode = "TESTITEMNAME", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string TESTITEMNAME
		{
			get { return _tESTITEMNAME; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for TESTITEMNAME", value, value.ToString());
				_tESTITEMNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TESTITEMSNAME", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string TESTITEMSNAME
		{
			get { return _tESTITEMSNAME; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for TESTITEMSNAME", value, value.ToString());
				_tESTITEMSNAME = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "RECEIVEDATE", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RECEIVEDATE
		{
			get { return _rECEIVEDATE; }
			set { _rECEIVEDATE = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SECTIONNO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SECTIONNO
		{
			get { return _sECTIONNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SECTIONNO", value, value.ToString());
				_sECTIONNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TESTTYPENO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TESTTYPENO
		{
			get { return _tESTTYPENO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TESTTYPENO", value, value.ToString());
				_tESTTYPENO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SAMPLENO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SAMPLENO
		{
			get { return _sAMPLENO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SAMPLENO", value, value.ToString());
				_sAMPLENO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PARITEMNO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PARITEMNO
		{
			get { return _pARITEMNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PARITEMNO", value, value.ToString());
				_pARITEMNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ITEMNO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ITEMNO
		{
			get { return _iTEMNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ITEMNO", value, value.ToString());
				_iTEMNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ORIGINALVALUE", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ORIGINALVALUE
		{
			get { return _oRIGINALVALUE; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ORIGINALVALUE", value, value.ToString());
				_oRIGINALVALUE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "REPORTVALUE", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string REPORTVALUE
		{
			get { return _rEPORTVALUE; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for REPORTVALUE", value, value.ToString());
				_rEPORTVALUE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ORIGINALDESC", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ORIGINALDESC
		{
			get { return _oRIGINALDESC; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ORIGINALDESC", value, value.ToString());
				_oRIGINALDESC = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "REPORTDESC", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string REPORTDESC
		{
			get { return _rEPORTDESC; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for REPORTDESC", value, value.ToString());
				_rEPORTDESC = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "STATUSNO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string STATUSNO
		{
			get { return _sTATUSNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for STATUSNO", value, value.ToString());
				_sTATUSNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EQUIPNO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EQUIPNO
		{
			get { return _eQUIPNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EQUIPNO", value, value.ToString());
				_eQUIPNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MODIFIED", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string MODIFIED
		{
			get { return _mODIFIED; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MODIFIED", value, value.ToString());
				_mODIFIED = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "REFRANGE", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string REFRANGE
		{
			get { return _rEFRANGE; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for REFRANGE", value, value.ToString());
				_rEFRANGE = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ITEMDATE", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ITEMDATE
		{
			get { return _iTEMDATE; }
			set { _iTEMDATE = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ITEMTIME", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ITEMTIME
		{
			get { return _iTEMTIME; }
			set { _iTEMTIME = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ISMATCH", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ISMATCH
		{
			get { return _iSMATCH; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ISMATCH", value, value.ToString());
				_iSMATCH = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RESULTSTATUS", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string RESULTSTATUS
		{
			get { return _rESULTSTATUS; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RESULTSTATUS", value, value.ToString());
				_rESULTSTATUS = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TESTITEMDATETIME", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TESTITEMDATETIME
		{
			get { return _tESTITEMDATETIME; }
			set { _tESTITEMDATETIME = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "REPORTVALUEALL", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string REPORTVALUEALL
		{
			get { return _rEPORTVALUEALL; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for REPORTVALUEALL", value, value.ToString());
				_rEPORTVALUEALL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PARITEMNAME", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string PARITEMNAME
		{
			get { return _pARITEMNAME; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for PARITEMNAME", value, value.ToString());
				_pARITEMNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PARITEMSNAME", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string PARITEMSNAME
		{
			get { return _pARITEMSNAME; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for PARITEMSNAME", value, value.ToString());
				_pARITEMSNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DISPORDER", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DISPORDER
		{
			get { return _dISPORDER; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DISPORDER", value, value.ToString());
				_dISPORDER = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ITEMORDER", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ITEMORDER
		{
			get { return _iTEMORDER; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ITEMORDER", value, value.ToString());
				_iTEMORDER = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UNIT", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UNIT
		{
			get { return _uNIT; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UNIT", value, value.ToString());
				_uNIT = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SERIALNO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SERIALNO
		{
			get { return _sERIALNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SERIALNO", value, value.ToString());
				_sERIALNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY1
		{
			get { return _zDY1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
				_zDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY2
		{
			get { return _zDY2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
				_zDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY3
		{
			get { return _zDY3; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
				_zDY3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY4", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY4
		{
			get { return _zDY4; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
				_zDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY5", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY5
		{
			get { return _zDY5; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
				_zDY5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HISORDERNO", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string HISORDERNO
		{
			get { return _hISORDERNO; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for HISORDERNO", value, value.ToString());
				_hISORDERNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FORMNO", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual string FORMNO
		{
			get { return _fORMNO; }
			set { _fORMNO = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TECHNICIAN", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string TECHNICIAN
		{
			get { return _tECHNICIAN; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for TECHNICIAN", value, value.ToString());
				_tECHNICIAN = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OLDSERIALNO", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OLDSERIALNO
		{
			get { return _oLDSERIALNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OLDSERIALNO", value, value.ToString());
				_oLDSERIALNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PREC", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PREC
		{
			get { return _pREC; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PREC", value, value.ToString());
				_pREC = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Itemunit", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Itemunit
		{
			get { return _itemunit; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Itemunit", value, value.ToString());
				_itemunit = value;
			}
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
        [DataDesc(CName = "", ShortCode = "Secretgrade", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Secretgrade
		{
			get { return _secretgrade; }
			set { _secretgrade = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortname", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Shortname
		{
			get { return _shortname; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Shortname", value, value.ToString());
				_shortname = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Cuegrade", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Cuegrade
		{
			get { return _cuegrade; }
			set { _cuegrade = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY6", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY6
		{
			get { return _zDY6; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY6", value, value.ToString());
				_zDY6 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY7", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY7
		{
			get { return _zDY7; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY7", value, value.ToString());
				_zDY7 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY8", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY8
		{
			get { return _zDY8; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY8", value, value.ToString());
				_zDY8 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY9", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY9
		{
			get { return _zDY9; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY9", value, value.ToString());
				_zDY9 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY10", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY10
		{
			get { return _zDY10; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY10", value, value.ToString());
				_zDY10 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Curritemredo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Curritemredo
		{
			get { return _curritemredo; }
			set { _curritemredo = value; }
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
        [DataDesc(CName = "", ShortCode = "SectionName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SectionName
		{
			get { return _sectionName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SectionName", value, value.ToString());
				_sectionName = value;
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
        [DataDesc(CName = "", ShortCode = "CheckType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckType
		{
			get { return _checkType; }
			set { _checkType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckTypeName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CheckTypeName
		{
			get { return _checkTypeName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for CheckTypeName", value, value.ToString());
				_checkTypeName = value;
			}
		}

		
		#endregion
	}
	#endregion
}