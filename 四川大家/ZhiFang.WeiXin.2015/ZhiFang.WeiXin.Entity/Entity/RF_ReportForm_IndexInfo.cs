using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region RFReportFormIndexInfo

	/// <summary>
	/// RFReportFormIndexInfo object for NHibernate mapped table 'RF_ReportForm_IndexInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "报告单索引", ClassCName = "RFReportFormIndexInfo", ShortCode = "RFReportFormIndexInfo", Desc = "报告单索引")]
	public class RFReportFormIndexInfo : BaseEntity
	{
		#region Member Variables
		
        protected string _reportFormID;
        protected string _cLIENTNO;
        protected string _cNAME;
        protected string _aGEUNITNAME;
        protected string _gENDERNAME;
        protected string _dEPTNAME;
        protected string _dOCTORNAME;
        protected string _dISTRICTNAME;
        protected string _wARDNAME;
        protected string _fOLKNAME;
        protected string _sICKTYPENAME;
        protected string _sAMPLETYPENAME;
        protected string _sECTIONNAME;
        protected string _tESTTYPENAME;
        protected DateTime _rECEIVEDATE;
        protected string _sECTIONNO;
        protected string _tESTTYPENO;
        protected string _sAMPLENO;
        protected int _sTATUSNO;
        protected int _sAMPLETYPENO;
        protected string _pATNO;
        protected int _gENDERNO;
        protected DateTime? _bIRTHDAY;
        protected string _aGE;
        protected int _aGEUNITNO;
        protected string _fOLKNO;
        protected string _dISTRICTNO;
        protected string _wARDNO;
        protected string _bED;
        protected int _dEPTNO;
        protected string _dOCTOR;
        protected string _cHARGENO;
        protected string _cHARGE;
        protected string _cOLLECTER;
        protected DateTime? _cOLLECTDATE;
        protected DateTime? _cOLLECTTIME;
        protected string _fORMMEMO;
        protected string _tECHNICIAN;
        protected DateTime? _tESTDATE;
        protected DateTime? _tESTTIME;
        protected string _oPERATOR;
        protected DateTime? _oPERDATE;
        protected DateTime? _oPERTIME;
        protected string _cHECKER;
        protected int _pRINTTIMES;
        protected string _resultfile;
        protected DateTime? _cHECKDATE;
        protected DateTime? _cHECKTIME;
        protected string _sERIALNO;
        protected string _rEQUESTSOURCE;
        protected string _dIAGNO;
        protected string _sICKTYPENO;
        protected string _fORMCOMMENT;
        protected string _aRTIFICERORDER;
        protected string _sICKORDER;
        protected string _sICKTYPE;
        protected string _cHARGEFLAG;
        protected string _tESTDEST;
        protected string _sLABLE;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected string _zDY6;
        protected string _zDY7;
        protected string _zDY8;
        protected string _zDY9;
        protected string _zDY10;
        protected DateTime? _iNCEPTDATE;
        protected DateTime? _iNCEPTTIME;
        protected string _iNCEPTER;
        protected DateTime? _oNLINEDATE;
        protected DateTime? _oNLINETIME;
        protected string _bMANNO;
        protected string _fILETYPE;
        protected string _jPGFILE;
        protected string _pDFFILE;
        protected string _fORMNO;
        protected string _cHILDTABLENAME;
        protected string _pRINTEXEC;
        protected string _lABCENTER;
        protected string _checkName;
        protected string _checkNo;
        protected string _cLIENTNAME;
        protected string _bARCODE;
        protected string _pRINTDATETIME;
        protected string _pRINTTEXEC;
        protected DateTime? _uploadDate;
        protected string _isdown;
        protected string _sECTIONTYPE;
        protected string _sECTIONSHORTNAME;
        protected string _sECTIONSHORTCODE;
        protected string _dIAGNOSE;
        protected string _oldSerialno;
        protected int _areaSendFlag;
        protected DateTime? _areaSendTime;
        protected string _genderEname;
        protected string _sickEname;
        protected string _sampletypeename;
        protected string _folkename;
        protected string _deptename;
        protected string _districtename;
        protected string _ageUnitename;
        protected string _testType;
        protected string _testTypeename;
        protected string _diag;
        protected string _clientstyle;
        protected string _clientReportTitle;
        protected string _aDDRESS;
        protected string _czdy1;
        protected string _czdy2;
        protected string _czdy3;
        protected string _czdy4;
        protected string _czdy5;
        protected string _czdy6;
        protected string _clientzdy3;
        protected byte[] _poperator;
        protected byte[] _pNOperator;
        protected byte[] _pSender2;
        protected DateTime? _nOperDate;
        protected string _nOPERTIME;
        protected string _clientename;
        protected string _clientcode;
        protected string _sectiondesc;
        protected byte[] _ptechnician;
        protected byte[] _pincepter;
        protected DateTime? _receivetime;
        protected string _collectPart;
        protected string _webLisOrgID;
        protected string _webLisSourceOrgID;
        protected string _telNo;
        protected string _resultstatus;
        protected string _webLisOrgName;
        protected string _webLisSourceOrgName;
        protected string _statusType;
        protected string _personID;
		

		#endregion

		#region Constructors

		public RFReportFormIndexInfo() { }

        public RFReportFormIndexInfo(string reportFormID, string cLIENTNO, string cNAME, string aGEUNITNAME, string gENDERNAME, string dEPTNAME, string dOCTORNAME, string dISTRICTNAME, string wARDNAME, string fOLKNAME, string sICKTYPENAME, string sAMPLETYPENAME, string sECTIONNAME, string tESTTYPENAME, DateTime rECEIVEDATE, string sECTIONNO, string tESTTYPENO, string sAMPLENO, int sTATUSNO, int sAMPLETYPENO, string pATNO, int gENDERNO, DateTime bIRTHDAY, string aGE, int aGEUNITNO, string fOLKNO, string dISTRICTNO, string wARDNO, string bED, int dEPTNO, string dOCTOR, string cHARGENO, string cHARGE, string cOLLECTER, DateTime cOLLECTDATE, DateTime cOLLECTTIME, string fORMMEMO, string tECHNICIAN, DateTime tESTDATE, DateTime tESTTIME, string oPERATOR, DateTime oPERDATE, DateTime oPERTIME, string cHECKER, int pRINTTIMES, string resultfile, DateTime cHECKDATE, DateTime cHECKTIME, string sERIALNO, string rEQUESTSOURCE, string dIAGNO, string sICKTYPENO, string fORMCOMMENT, string aRTIFICERORDER, string sICKORDER, string sICKTYPE, string cHARGEFLAG, string tESTDEST, string sLABLE, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, string zDY6, string zDY7, string zDY8, string zDY9, string zDY10, DateTime iNCEPTDATE, DateTime iNCEPTTIME, string iNCEPTER, DateTime oNLINEDATE, DateTime oNLINETIME, string bMANNO, string fILETYPE, string jPGFILE, string pDFFILE, string fORMNO, string cHILDTABLENAME, string pRINTEXEC, string lABCENTER, string checkName, string checkNo, string cLIENTNAME, string bARCODE, string pRINTDATETIME, string pRINTTEXEC, DateTime uploadDate, string isdown, string sECTIONTYPE, string sECTIONSHORTNAME, string sECTIONSHORTCODE, string dIAGNOSE, string oldSerialno, int areaSendFlag, DateTime areaSendTime, string genderEname, string sickEname, string sampletypeename, string folkename, string deptename, string districtename, string ageUnitename, string testType, string testTypeename, string diag, string clientstyle, string clientReportTitle, string aDDRESS, string czdy1, string czdy2, string czdy3, string czdy4, string czdy5, string czdy6, string clientzdy3, byte[] poperator, byte[] pNOperator, byte[] pSender2, DateTime nOperDate, string nOPERTIME, string clientename, string clientcode, string sectiondesc, byte[] ptechnician, byte[] pincepter, DateTime receivetime, string collectPart, string webLisOrgID, string webLisSourceOrgID, string telNo, string resultstatus, string webLisOrgName, string webLisSourceOrgName, string statusType, string personID)
		{
			this._reportFormID = reportFormID;
			this._cLIENTNO = cLIENTNO;
			this._cNAME = cNAME;
			this._aGEUNITNAME = aGEUNITNAME;
			this._gENDERNAME = gENDERNAME;
			this._dEPTNAME = dEPTNAME;
			this._dOCTORNAME = dOCTORNAME;
			this._dISTRICTNAME = dISTRICTNAME;
			this._wARDNAME = wARDNAME;
			this._fOLKNAME = fOLKNAME;
			this._sICKTYPENAME = sICKTYPENAME;
			this._sAMPLETYPENAME = sAMPLETYPENAME;
			this._sECTIONNAME = sECTIONNAME;
			this._tESTTYPENAME = tESTTYPENAME;
			this._rECEIVEDATE = rECEIVEDATE;
			this._sECTIONNO = sECTIONNO;
			this._tESTTYPENO = tESTTYPENO;
			this._sAMPLENO = sAMPLENO;
			this._sTATUSNO = sTATUSNO;
			this._sAMPLETYPENO = sAMPLETYPENO;
			this._pATNO = pATNO;
			this._gENDERNO = gENDERNO;
			this._bIRTHDAY = bIRTHDAY;
			this._aGE = aGE;
			this._aGEUNITNO = aGEUNITNO;
			this._fOLKNO = fOLKNO;
			this._dISTRICTNO = dISTRICTNO;
			this._wARDNO = wARDNO;
			this._bED = bED;
			this._dEPTNO = dEPTNO;
			this._dOCTOR = dOCTOR;
			this._cHARGENO = cHARGENO;
			this._cHARGE = cHARGE;
			this._cOLLECTER = cOLLECTER;
			this._cOLLECTDATE = cOLLECTDATE;
			this._cOLLECTTIME = cOLLECTTIME;
			this._fORMMEMO = fORMMEMO;
			this._tECHNICIAN = tECHNICIAN;
			this._tESTDATE = tESTDATE;
			this._tESTTIME = tESTTIME;
			this._oPERATOR = oPERATOR;
			this._oPERDATE = oPERDATE;
			this._oPERTIME = oPERTIME;
			this._cHECKER = cHECKER;
			this._pRINTTIMES = pRINTTIMES;
			this._resultfile = resultfile;
			this._cHECKDATE = cHECKDATE;
			this._cHECKTIME = cHECKTIME;
			this._sERIALNO = sERIALNO;
			this._rEQUESTSOURCE = rEQUESTSOURCE;
			this._dIAGNO = dIAGNO;
			this._sICKTYPENO = sICKTYPENO;
			this._fORMCOMMENT = fORMCOMMENT;
			this._aRTIFICERORDER = aRTIFICERORDER;
			this._sICKORDER = sICKORDER;
			this._sICKTYPE = sICKTYPE;
			this._cHARGEFLAG = cHARGEFLAG;
			this._tESTDEST = tESTDEST;
			this._sLABLE = sLABLE;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._zDY4 = zDY4;
			this._zDY5 = zDY5;
			this._zDY6 = zDY6;
			this._zDY7 = zDY7;
			this._zDY8 = zDY8;
			this._zDY9 = zDY9;
			this._zDY10 = zDY10;
			this._iNCEPTDATE = iNCEPTDATE;
			this._iNCEPTTIME = iNCEPTTIME;
			this._iNCEPTER = iNCEPTER;
			this._oNLINEDATE = oNLINEDATE;
			this._oNLINETIME = oNLINETIME;
			this._bMANNO = bMANNO;
			this._fILETYPE = fILETYPE;
			this._jPGFILE = jPGFILE;
			this._pDFFILE = pDFFILE;
			this._fORMNO = fORMNO;
			this._cHILDTABLENAME = cHILDTABLENAME;
			this._pRINTEXEC = pRINTEXEC;
			this._lABCENTER = lABCENTER;
			this._checkName = checkName;
			this._checkNo = checkNo;
			this._cLIENTNAME = cLIENTNAME;
			this._bARCODE = bARCODE;
			this._pRINTDATETIME = pRINTDATETIME;
			this._pRINTTEXEC = pRINTTEXEC;
			this._uploadDate = uploadDate;
			this._isdown = isdown;
			this._sECTIONTYPE = sECTIONTYPE;
			this._sECTIONSHORTNAME = sECTIONSHORTNAME;
			this._sECTIONSHORTCODE = sECTIONSHORTCODE;
			this._dIAGNOSE = dIAGNOSE;
			this._oldSerialno = oldSerialno;
			this._areaSendFlag = areaSendFlag;
			this._areaSendTime = areaSendTime;
			this._genderEname = genderEname;
			this._sickEname = sickEname;
			this._sampletypeename = sampletypeename;
			this._folkename = folkename;
			this._deptename = deptename;
			this._districtename = districtename;
			this._ageUnitename = ageUnitename;
			this._testType = testType;
			this._testTypeename = testTypeename;
			this._diag = diag;
			this._clientstyle = clientstyle;
			this._clientReportTitle = clientReportTitle;
			this._aDDRESS = aDDRESS;
			this._czdy1 = czdy1;
			this._czdy2 = czdy2;
			this._czdy3 = czdy3;
			this._czdy4 = czdy4;
			this._czdy5 = czdy5;
			this._czdy6 = czdy6;
			this._clientzdy3 = clientzdy3;
			this._poperator = poperator;
			this._pNOperator = pNOperator;
			this._pSender2 = pSender2;
			this._nOperDate = nOperDate;
			this._nOPERTIME = nOPERTIME;
			this._clientename = clientename;
			this._clientcode = clientcode;
			this._sectiondesc = sectiondesc;
			this._ptechnician = ptechnician;
			this._pincepter = pincepter;
			this._receivetime = receivetime;
			this._collectPart = collectPart;
			this._webLisOrgID = webLisOrgID;
			this._webLisSourceOrgID = webLisSourceOrgID;
			this._telNo = telNo;
			this._resultstatus = resultstatus;
			this._webLisOrgName = webLisOrgName;
			this._webLisSourceOrgName = webLisSourceOrgName;
			this._statusType = statusType;
			this._personID = personID;
		}

		#endregion

		#region Public Properties


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
        [DataDesc(CName = "送检单位", ShortCode = "CLIENTNO", Desc = "送检单位", ContextType = SysDic.All, Length = 50)]
        public virtual string CLIENTNO
		{
			get { return _cLIENTNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CLIENTNO", value, value.ToString());
				_cLIENTNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "姓名", ShortCode = "CNAME", Desc = "姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CNAME
		{
			get { return _cNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CNAME", value, value.ToString());
				_cNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "年龄单位名称", ShortCode = "AGEUNITNAME", Desc = "年龄单位名称", ContextType = SysDic.All, Length = 50)]
        public virtual string AGEUNITNAME
		{
			get { return _aGEUNITNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AGEUNITNAME", value, value.ToString());
				_aGEUNITNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "性别名称", ShortCode = "GENDERNAME", Desc = "性别名称", ContextType = SysDic.All, Length = 50)]
        public virtual string GENDERNAME
		{
			get { return _gENDERNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for GENDERNAME", value, value.ToString());
				_gENDERNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "科室名称", ShortCode = "DEPTNAME", Desc = "科室名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DEPTNAME
		{
			get { return _dEPTNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DEPTNAME", value, value.ToString());
				_dEPTNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "医生姓名", ShortCode = "DOCTORNAME", Desc = "医生姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string DOCTORNAME
		{
			get { return _dOCTORNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DOCTORNAME", value, value.ToString());
				_dOCTORNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "病区名称", ShortCode = "DISTRICTNAME", Desc = "病区名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DISTRICTNAME
		{
			get { return _dISTRICTNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DISTRICTNAME", value, value.ToString());
				_dISTRICTNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "病房名称", ShortCode = "WARDNAME", Desc = "病房名称", ContextType = SysDic.All, Length = 50)]
        public virtual string WARDNAME
		{
			get { return _wARDNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WARDNAME", value, value.ToString());
				_wARDNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "民族名称", ShortCode = "FOLKNAME", Desc = "民族名称", ContextType = SysDic.All, Length = 50)]
        public virtual string FOLKNAME
		{
			get { return _fOLKNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for FOLKNAME", value, value.ToString());
				_fOLKNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "就诊类型名称", ShortCode = "SICKTYPENAME", Desc = "就诊类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string SICKTYPENAME
		{
			get { return _sICKTYPENAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SICKTYPENAME", value, value.ToString());
				_sICKTYPENAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "样本类型名称", ShortCode = "SAMPLETYPENAME", Desc = "样本类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string SAMPLETYPENAME
		{
			get { return _sAMPLETYPENAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SAMPLETYPENAME", value, value.ToString());
				_sAMPLETYPENAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "小组名称", ShortCode = "SECTIONNAME", Desc = "小组名称", ContextType = SysDic.All, Length = 50)]
        public virtual string SECTIONNAME
		{
			get { return _sECTIONNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SECTIONNAME", value, value.ToString());
				_sECTIONNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检验类型名称", ShortCode = "TESTTYPENAME", Desc = "检验类型名称", ContextType = SysDic.All, Length = 40)]
        public virtual string TESTTYPENAME
		{
			get { return _tESTTYPENAME; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for TESTTYPENAME", value, value.ToString());
				_tESTTYPENAME = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器检测日期", ShortCode = "RECEIVEDATE", Desc = "仪器检测日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime RECEIVEDATE
		{
			get { return _rECEIVEDATE; }
			set { _rECEIVEDATE = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组编号", ShortCode = "SECTIONNO", Desc = "小组编号", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "仪器检测类型", ShortCode = "TESTTYPENO", Desc = "仪器检测类型", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "仪器检测样本号", ShortCode = "SAMPLENO", Desc = "仪器检测样本号", ContextType = SysDic.All, Length = 150)]
        public virtual string SAMPLENO
		{
			get { return _sAMPLENO; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for SAMPLENO", value, value.ToString());
				_sAMPLENO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "状态编号", ShortCode = "STATUSNO", Desc = "状态编号", ContextType = SysDic.All, Length = 4)]
        public virtual int STATUSNO
		{
			get { return _sTATUSNO; }
			set { _sTATUSNO = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "SAMPLETYPENO", Desc = "样本类型", ContextType = SysDic.All, Length = 4)]
        public virtual int SAMPLETYPENO
		{
			get { return _sAMPLETYPENO; }
			set { _sAMPLETYPENO = value; }
		}

        [DataMember]
        [DataDesc(CName = "病历号", ShortCode = "PATNO", Desc = "病历号", ContextType = SysDic.All, Length = 50)]
        public virtual string PATNO
		{
			get { return _pATNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PATNO", value, value.ToString());
				_pATNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "性别代码", ShortCode = "GENDERNO", Desc = "性别代码", ContextType = SysDic.All, Length = 4)]
        public virtual int GENDERNO
		{
			get { return _gENDERNO; }
			set { _gENDERNO = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生日", ShortCode = "BIRTHDAY", Desc = "生日", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BIRTHDAY
		{
			get { return _bIRTHDAY; }
			set { _bIRTHDAY = value; }
		}

        [DataMember]
        [DataDesc(CName = "年龄", ShortCode = "AGE", Desc = "年龄", ContextType = SysDic.All, Length = 50)]
        public virtual string AGE
		{
			get { return _aGE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AGE", value, value.ToString());
				_aGE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "年龄单位", ShortCode = "AGEUNITNO", Desc = "年龄单位", ContextType = SysDic.All, Length = 4)]
        public virtual int AGEUNITNO
		{
			get { return _aGEUNITNO; }
			set { _aGEUNITNO = value; }
		}

        [DataMember]
        [DataDesc(CName = "民族代码", ShortCode = "FOLKNO", Desc = "民族代码", ContextType = SysDic.All, Length = 50)]
        public virtual string FOLKNO
		{
			get { return _fOLKNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for FOLKNO", value, value.ToString());
				_fOLKNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "病区代码", ShortCode = "DISTRICTNO", Desc = "病区代码", ContextType = SysDic.All, Length = 50)]
        public virtual string DISTRICTNO
		{
			get { return _dISTRICTNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DISTRICTNO", value, value.ToString());
				_dISTRICTNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "病房代码", ShortCode = "WARDNO", Desc = "病房代码", ContextType = SysDic.All, Length = 50)]
        public virtual string WARDNO
		{
			get { return _wARDNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WARDNO", value, value.ToString());
				_wARDNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "病床", ShortCode = "BED", Desc = "病床", ContextType = SysDic.All, Length = 50)]
        public virtual string BED
		{
			get { return _bED; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BED", value, value.ToString());
				_bED = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "科室代码", ShortCode = "DEPTNO", Desc = "科室代码", ContextType = SysDic.All, Length = 4)]
        public virtual int DEPTNO
		{
			get { return _dEPTNO; }
			set { _dEPTNO = value; }
		}

        [DataMember]
        [DataDesc(CName = "医生名称", ShortCode = "DOCTOR", Desc = "医生名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DOCTOR
		{
			get { return _dOCTOR; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DOCTOR", value, value.ToString());
				_dOCTOR = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "收费类型", ShortCode = "CHARGENO", Desc = "收费类型", ContextType = SysDic.All, Length = 50)]
        public virtual string CHARGENO
		{
			get { return _cHARGENO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CHARGENO", value, value.ToString());
				_cHARGENO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "费用", ShortCode = "CHARGE", Desc = "费用", ContextType = SysDic.All, Length = 50)]
        public virtual string CHARGE
		{
			get { return _cHARGE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CHARGE", value, value.ToString());
				_cHARGE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "采样人", ShortCode = "COLLECTER", Desc = "采样人", ContextType = SysDic.All, Length = 50)]
        public virtual string COLLECTER
		{
			get { return _cOLLECTER; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for COLLECTER", value, value.ToString());
				_cOLLECTER = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采样日期", ShortCode = "COLLECTDATE", Desc = "采样日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? COLLECTDATE
		{
			get { return _cOLLECTDATE; }
			set { _cOLLECTDATE = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采样时间", ShortCode = "COLLECTTIME", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? COLLECTTIME
		{
			get { return _cOLLECTTIME; }
			set { _cOLLECTTIME = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "FORMMEMO", Desc = "备注", ContextType = SysDic.All, Length = 1000)]
        public virtual string FORMMEMO
		{
			get { return _fORMMEMO; }
			set
			{
				if ( value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for FORMMEMO", value, value.ToString());
				_fORMMEMO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检验技师", ShortCode = "TECHNICIAN", Desc = "检验技师", ContextType = SysDic.All, Length = 50)]
        public virtual string TECHNICIAN
		{
			get { return _tECHNICIAN; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TECHNICIAN", value, value.ToString());
				_tECHNICIAN = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检测日期", ShortCode = "TESTDATE", Desc = "检测日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TESTDATE
		{
			get { return _tESTDATE; }
			set { _tESTDATE = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检测时间", ShortCode = "TESTTIME", Desc = "检测时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TESTTIME
		{
			get { return _tESTTIME; }
			set { _tESTTIME = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作者", ShortCode = "OPERATOR", Desc = "操作者", ContextType = SysDic.All, Length = 50)]
        public virtual string OPERATOR
		{
			get { return _oPERATOR; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OPERATOR", value, value.ToString());
				_oPERATOR = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作日期", ShortCode = "OPERDATE", Desc = "操作日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OPERDATE
		{
			get { return _oPERDATE; }
			set { _oPERDATE = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作时间", ShortCode = "OPERTIME", Desc = "操作时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OPERTIME
		{
			get { return _oPERTIME; }
			set { _oPERTIME = value; }
		}

        [DataMember]
        [DataDesc(CName = "审定人", ShortCode = "CHECKER", Desc = "审定人", ContextType = SysDic.All, Length = 50)]
        public virtual string CHECKER
		{
			get { return _cHECKER; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CHECKER", value, value.ToString());
				_cHECKER = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PRINTTIMES", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PRINTTIMES
		{
			get { return _pRINTTIMES; }
			set { _pRINTTIMES = value; }
		}

        [DataMember]
        [DataDesc(CName = "报告单文件", ShortCode = "Resultfile", Desc = "报告单文件", ContextType = SysDic.All, Length = 100)]
        public virtual string Resultfile
		{
			get { return _resultfile; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Resultfile", value, value.ToString());
				_resultfile = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审定日期", ShortCode = "CHECKDATE", Desc = "审定日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CHECKDATE
		{
			get { return _cHECKDATE; }
			set { _cHECKDATE = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审定时间", ShortCode = "CHECKTIME", Desc = "审定时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CHECKTIME
		{
			get { return _cHECKTIME; }
			set { _cHECKTIME = value; }
		}

        [DataMember]
        [DataDesc(CName = "医嘱单号", ShortCode = "SERIALNO", Desc = "医嘱单号", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "来源机器名", ShortCode = "REQUESTSOURCE", Desc = "来源机器名", ContextType = SysDic.All, Length = 50)]
        public virtual string REQUESTSOURCE
		{
			get { return _rEQUESTSOURCE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for REQUESTSOURCE", value, value.ToString());
				_rEQUESTSOURCE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "诊断描述", ShortCode = "DIAGNO", Desc = "诊断描述", ContextType = SysDic.All, Length = 50)]
        public virtual string DIAGNO
		{
			get { return _dIAGNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DIAGNO", value, value.ToString());
				_dIAGNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "就诊类型", ShortCode = "SICKTYPENO", Desc = "就诊类型", ContextType = SysDic.All, Length = 50)]
        public virtual string SICKTYPENO
		{
			get { return _sICKTYPENO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SICKTYPENO", value, value.ToString());
				_sICKTYPENO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检验单评语", ShortCode = "FORMCOMMENT", Desc = "检验单评语", ContextType = SysDic.All, Length = 2000)]
        public virtual string FORMCOMMENT
		{
			get { return _fORMCOMMENT; }
			set
			{
				if ( value != null && value.Length > 2000)
					throw new ArgumentOutOfRangeException("Invalid value for FORMCOMMENT", value, value.ToString());
				_fORMCOMMENT = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ARTIFICERORDER", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ARTIFICERORDER
		{
			get { return _aRTIFICERORDER; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ARTIFICERORDER", value, value.ToString());
				_aRTIFICERORDER = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SICKORDER", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SICKORDER
		{
			get { return _sICKORDER; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SICKORDER", value, value.ToString());
				_sICKORDER = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SICKTYPE", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SICKTYPE
		{
			get { return _sICKTYPE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SICKTYPE", value, value.ToString());
				_sICKTYPE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "收费标记", ShortCode = "CHARGEFLAG", Desc = "收费标记", ContextType = SysDic.All, Length = 50)]
        public virtual string CHARGEFLAG
		{
			get { return _cHARGEFLAG; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CHARGEFLAG", value, value.ToString());
				_cHARGEFLAG = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检验目的", ShortCode = "TESTDEST", Desc = "检验目的", ContextType = SysDic.All, Length = 50)]
        public virtual string TESTDEST
		{
			get { return _tESTDEST; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TESTDEST", value, value.ToString());
				_tESTDEST = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SLABLE", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SLABLE
		{
			get { return _sLABLE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SLABLE", value, value.ToString());
				_sLABLE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义1", ShortCode = "ZDY1", Desc = "自定义1", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "自定义2", ShortCode = "ZDY2", Desc = "自定义2", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "自定义3", ShortCode = "ZDY3", Desc = "自定义3", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "自定义4", ShortCode = "ZDY4", Desc = "自定义4", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "自定义5", ShortCode = "ZDY5", Desc = "自定义5", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签收日期", ShortCode = "INCEPTDATE", Desc = "签收日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? INCEPTDATE
		{
			get { return _iNCEPTDATE; }
			set { _iNCEPTDATE = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签收时间", ShortCode = "INCEPTTIME", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? INCEPTTIME
		{
			get { return _iNCEPTTIME; }
			set { _iNCEPTTIME = value; }
		}

        [DataMember]
        [DataDesc(CName = "签收人", ShortCode = "INCEPTER", Desc = "签收人", ContextType = SysDic.All, Length = 50)]
        public virtual string INCEPTER
		{
			get { return _iNCEPTER; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for INCEPTER", value, value.ToString());
				_iNCEPTER = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上机日期", ShortCode = "ONLINEDATE", Desc = "上机日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ONLINEDATE
		{
			get { return _oNLINEDATE; }
			set { _oNLINEDATE = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上机时间", ShortCode = "ONLINETIME", Desc = "上机时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ONLINETIME
		{
			get { return _oNLINETIME; }
			set { _oNLINETIME = value; }
		}

        [DataMember]
        [DataDesc(CName = "业务员", ShortCode = "BMANNO", Desc = "业务员", ContextType = SysDic.All, Length = 50)]
        public virtual string BMANNO
		{
			get { return _bMANNO; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BMANNO", value, value.ToString());
				_bMANNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "报告文件类型", ShortCode = "FILETYPE", Desc = "报告文件类型", ContextType = SysDic.All, Length = 50)]
        public virtual string FILETYPE
		{
			get { return _fILETYPE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for FILETYPE", value, value.ToString());
				_fILETYPE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "图片报告文件", ShortCode = "JPGFILE", Desc = "图片报告文件", ContextType = SysDic.All, Length = 500)]
        public virtual string JPGFILE
		{
			get { return _jPGFILE; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for JPGFILE", value, value.ToString());
				_jPGFILE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "PDF报告文件", ShortCode = "PDFFILE", Desc = "PDF报告文件", ContextType = SysDic.All, Length = 500)]
        public virtual string PDFFILE
		{
			get { return _pDFFILE; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for PDFFILE", value, value.ToString());
				_pDFFILE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "报告临时编号", ShortCode = "FORMNO", Desc = "报告临时编号", ContextType = SysDic.All, Length = 4)]
        public virtual string FORMNO
		{
			get { return _fORMNO; }
			set { _fORMNO = value; }
		}

        [DataMember]
        [DataDesc(CName = "送检单位名称", ShortCode = "CHILDTABLENAME", Desc = "送检单位名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CHILDTABLENAME
		{
			get { return _cHILDTABLENAME; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CHILDTABLENAME", value, value.ToString());
				_cHILDTABLENAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "打印", ShortCode = "PRINTEXEC", Desc = "打印", ContextType = SysDic.All, Length = 4)]
        public virtual string PRINTEXEC
		{
			get { return _pRINTEXEC; }
			set
			{
				if ( value != null && value.Length > 4)
					throw new ArgumentOutOfRangeException("Invalid value for PRINTEXEC", value, value.ToString());
				_pRINTEXEC = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "独立实验室名称", ShortCode = "LABCENTER", Desc = "独立实验室名称", ContextType = SysDic.All, Length = 50)]
        public virtual string LABCENTER
		{
			get { return _lABCENTER; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LABCENTER", value, value.ToString());
				_lABCENTER = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckName
		{
			get { return _checkName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CheckName", value, value.ToString());
				_checkName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckNo
		{
			get { return _checkNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CheckNo", value, value.ToString());
				_checkNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CLIENTNAME", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CLIENTNAME
		{
			get { return _cLIENTNAME; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CLIENTNAME", value, value.ToString());
				_cLIENTNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BARCODE", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BARCODE
		{
			get { return _bARCODE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BARCODE", value, value.ToString());
				_bARCODE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PRINTDATETIME", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PRINTDATETIME
		{
			get { return _pRINTDATETIME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PRINTDATETIME", value, value.ToString());
				_pRINTDATETIME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PRINTTEXEC", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual string PRINTTEXEC
		{
			get { return _pRINTTEXEC; }
			set
			{
				if ( value != null && value.Length > 4)
					throw new ArgumentOutOfRangeException("Invalid value for PRINTTEXEC", value, value.ToString());
				_pRINTTEXEC = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "UploadDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? UploadDate
		{
			get { return _uploadDate; }
			set { _uploadDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Isdown", Desc = "", ContextType = SysDic.All, Length = 2)]
        public virtual string Isdown
		{
			get { return _isdown; }
			set
			{
				if ( value != null && value.Length > 2)
					throw new ArgumentOutOfRangeException("Invalid value for Isdown", value, value.ToString());
				_isdown = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SECTIONTYPE", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SECTIONTYPE
		{
			get { return _sECTIONTYPE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SECTIONTYPE", value, value.ToString());
				_sECTIONTYPE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SECTIONSHORTNAME", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SECTIONSHORTNAME
		{
			get { return _sECTIONSHORTNAME; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SECTIONSHORTNAME", value, value.ToString());
				_sECTIONSHORTNAME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SECTIONSHORTCODE", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SECTIONSHORTCODE
		{
			get { return _sECTIONSHORTCODE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SECTIONSHORTCODE", value, value.ToString());
				_sECTIONSHORTCODE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DIAGNOSE", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DIAGNOSE
		{
			get { return _dIAGNOSE; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DIAGNOSE", value, value.ToString());
				_dIAGNOSE = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OldSerialno", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OldSerialno
		{
			get { return _oldSerialno; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OldSerialno", value, value.ToString());
				_oldSerialno = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AreaSendFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AreaSendFlag
		{
			get { return _areaSendFlag; }
			set { _areaSendFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AreaSendTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AreaSendTime
		{
			get { return _areaSendTime; }
			set { _areaSendTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GenderEname", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string GenderEname
		{
			get { return _genderEname; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for GenderEname", value, value.ToString());
				_genderEname = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SickEname", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string SickEname
		{
			get { return _sickEname; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for SickEname", value, value.ToString());
				_sickEname = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Sampletypeename", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Sampletypeename
		{
			get { return _sampletypeename; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Sampletypeename", value, value.ToString());
				_sampletypeename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Folkename", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Folkename
		{
			get { return _folkename; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Folkename", value, value.ToString());
				_folkename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Deptename", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Deptename
		{
			get { return _deptename; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Deptename", value, value.ToString());
				_deptename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Districtename", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Districtename
		{
			get { return _districtename; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Districtename", value, value.ToString());
				_districtename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AgeUnitename", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string AgeUnitename
		{
			get { return _ageUnitename; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for AgeUnitename", value, value.ToString());
				_ageUnitename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestType", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string TestType
		{
			get { return _testType; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for TestType", value, value.ToString());
				_testType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestTypeename", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string TestTypeename
		{
			get { return _testTypeename; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for TestTypeename", value, value.ToString());
				_testTypeename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Diag", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Diag
		{
			get { return _diag; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Diag", value, value.ToString());
				_diag = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Clientstyle", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Clientstyle
		{
			get { return _clientstyle; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Clientstyle", value, value.ToString());
				_clientstyle = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ClientReportTitle", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ClientReportTitle
		{
			get { return _clientReportTitle; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ClientReportTitle", value, value.ToString());
				_clientReportTitle = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ADDRESS", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string ADDRESS
		{
			get { return _aDDRESS; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ADDRESS", value, value.ToString());
				_aDDRESS = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Czdy1", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Czdy1
		{
			get { return _czdy1; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Czdy1", value, value.ToString());
				_czdy1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Czdy2", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Czdy2
		{
			get { return _czdy2; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Czdy2", value, value.ToString());
				_czdy2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Czdy3", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Czdy3
		{
			get { return _czdy3; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Czdy3", value, value.ToString());
				_czdy3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Czdy4", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Czdy4
		{
			get { return _czdy4; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Czdy4", value, value.ToString());
				_czdy4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Czdy5", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Czdy5
		{
			get { return _czdy5; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Czdy5", value, value.ToString());
				_czdy5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Czdy6", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Czdy6
		{
			get { return _czdy6; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Czdy6", value, value.ToString());
				_czdy6 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Clientzdy3", Desc = "", ContextType = SysDic.All, Length = 102)]
        public virtual string Clientzdy3
		{
			get { return _clientzdy3; }
			set
			{
				if ( value != null && value.Length > 102)
					throw new ArgumentOutOfRangeException("Invalid value for Clientzdy3", value, value.ToString());
				_clientzdy3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Poperator", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] Poperator
		{
			get { return _poperator; }
			set { _poperator = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PNOperator", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] PNOperator
		{
			get { return _pNOperator; }
			set { _pNOperator = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PSender2", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] PSender2
		{
			get { return _pSender2; }
			set { _pSender2 = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "NOperDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? NOperDate
		{
			get { return _nOperDate; }
			set { _nOperDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NOPERTIME", Desc = "", ContextType = SysDic.All, Length = 102)]
        public virtual string NOPERTIME
		{
			get { return _nOPERTIME; }
			set
			{
				if ( value != null && value.Length > 102)
					throw new ArgumentOutOfRangeException("Invalid value for NOPERTIME", value, value.ToString());
				_nOPERTIME = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Clientename", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Clientename
		{
			get { return _clientename; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Clientename", value, value.ToString());
				_clientename = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Clientcode", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Clientcode
		{
			get { return _clientcode; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Clientcode", value, value.ToString());
				_clientcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Sectiondesc", Desc = "", ContextType = SysDic.All, Length = 250)]
        public virtual string Sectiondesc
		{
			get { return _sectiondesc; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for Sectiondesc", value, value.ToString());
				_sectiondesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Ptechnician", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] Ptechnician
		{
			get { return _ptechnician; }
			set { _ptechnician = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Pincepter", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] Pincepter
		{
			get { return _pincepter; }
			set { _pincepter = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Receivetime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Receivetime
		{
			get { return _receivetime; }
			set { _receivetime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CollectPart", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CollectPart
		{
			get { return _collectPart; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CollectPart", value, value.ToString());
				_collectPart = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WebLisOrgID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string WebLisOrgID
		{
			get { return _webLisOrgID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisOrgID", value, value.ToString());
				_webLisOrgID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WebLisSourceOrgID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string WebLisSourceOrgID
		{
			get { return _webLisSourceOrgID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgID", value, value.ToString());
				_webLisSourceOrgID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TelNo", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string TelNo
		{
			get { return _telNo; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for TelNo", value, value.ToString());
				_telNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Resultstatus", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Resultstatus
		{
			get { return _resultstatus; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Resultstatus", value, value.ToString());
				_resultstatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WebLisOrgName", Desc = "", ContextType = SysDic.All, Length = 150)]
        public virtual string WebLisOrgName
		{
			get { return _webLisOrgName; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisOrgName", value, value.ToString());
				_webLisOrgName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WebLisSourceOrgName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string WebLisSourceOrgName
		{
			get { return _webLisSourceOrgName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WebLisSourceOrgName", value, value.ToString());
				_webLisSourceOrgName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StatusType
		{
			get { return _statusType; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for StatusType", value, value.ToString());
				_statusType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PersonID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PersonID
		{
			get { return _personID; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PersonID", value, value.ToString());
				_personID = value;
			}
		}

		
		#endregion
	}
	#endregion
}