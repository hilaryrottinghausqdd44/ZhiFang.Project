using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region ReportFormFull
    public class ReportFormFull
    {
        #region Member Variables
        protected long _LabID;
        protected long _reportpublicationid;
        protected string _reportFormID;
        protected DateTime? _receiveDate;
        protected long _sectionNo;
        protected long _testTypeNo;
        protected string _sampleNo;
        protected string _sectionName;
        protected string _testTypeName;
        protected string _sampleTypeNo;
        protected string _sampletypeName;
        protected long _secretType;
        protected string _patientID;
        protected string _patNo;
        protected string _cName;
        protected string _inpatientNo;
        protected string _patCardNo;
        protected long _genderNo;
        protected string _genderName;
        protected double _age;
        protected long _ageUnitNo;
        protected string _ageUnitName;
        protected DateTime? _birthday;
        protected long _districtNo;
        protected string _districtName;
        protected long _wardNo;
        protected string _wardName;
        protected string _bed;
        protected long _deptNo;
        protected string _deptName;
        protected long _doctorID;
        protected string _doctor;
        protected string _serialNo;
        protected string _paritemName;
        protected string _doctorItemName;
        protected string _collecter;
        protected DateTime? _collectDate;
        protected DateTime? _collectTime;
        protected string _incepter;
        protected DateTime? _inceptDate;
        protected DateTime? _inceptTime;
        protected long _mainTesterId;
        protected string _technician;
        protected DateTime? _testDate;
        protected DateTime? _testTime;
        protected string _operator;
        protected DateTime? _operDate;
        protected DateTime? _operTime;
        protected long _examinerId;
        protected string _checker;
        protected DateTime? _checkDate;
        protected DateTime? _checkTime;
        protected string _formComment;
        protected string _formMemo;
        protected long _sickTypeNo;
        protected string _sickTypeName;
        protected long _diagNo;
        protected string _diagName;
        protected long _clientNo;
        protected string _clientName;
        protected string _sender2;
        protected int _printTimes;
        protected int _clientPrint;
        protected string _printOper;
        protected DateTime? _printDateTime;
        protected string _printOper1;
        protected DateTime? _printDateTime1;
        protected string _resultsend;
        protected string _reportsend;
        protected int _activeFlag;
        protected int _allFlag;
        protected string _collectPart;
        protected string _testAim;
        protected string _pageName;
        protected string _pageCount;
        protected DateTime? _receiveTime;
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
        protected DateTime? _dataUpdateTime;
        protected DateTime? _dataMigrationTime;
        protected int _sTestType;
        protected string _formComment2;
        protected int _reportType;
        protected int _sectionResultType;
        protected int _iQSPrintCount;
        protected string _sampleType2;
        protected string _testComment;
        protected string _reportRemark;
        protected string _testMethod;
        protected string _ageDesc;
        protected string _zDY11;
        protected int _sumPrintFlag;
        protected string _departmentName;
        protected string _fZDY1;
        protected string _fZDY2;
        protected string _fZDY3;
        protected string _fZDY4;
        protected string _fZDY5;
        protected double _weight;
        protected string _fFormMemo;
        protected string _equipName;
        protected byte[] _formResultInfo;
        protected string _testPurpose;


        #endregion

        #region Constructors

        public ReportFormFull() { }

        #endregion

        #region Public Properties


        public long LabID
        {
            set { _LabID = value; }
            get { return _LabID; }
        }
        public long ReportPublicationID {
            set { _reportpublicationid = value; }
            get { return _reportpublicationid; }
        }
        public virtual string ReportFormID
		{
			get { return _reportFormID; }
			set
			{
				_reportFormID = value;
			}
		}

      
        public virtual DateTime? ReceiveDate
		{
			get { return _receiveDate; }
			set { _receiveDate = value; }
		}

      
        public virtual long SectionNo
		{
			get { return _sectionNo; }
			set { _sectionNo = value; }
		}

       
        public virtual long TestTypeNo
		{
			get { return _testTypeNo; }
			set { _testTypeNo = value; }
		}

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

     
        public virtual string SectionName
		{
			get { return _sectionName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SectionName", value, value.ToString());
				_sectionName = value;
			}
		}

        
        public virtual string TestTypeName
		{
			get { return _testTypeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TestTypeName", value, value.ToString());
				_testTypeName = value;
			}
		}

       
        public virtual string SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampleTypeNo", value, value.ToString());
				_sampleTypeNo = value;
			}
		}

       
        public virtual string SampletypeName
		{
			get { return _sampletypeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SampletypeName", value, value.ToString());
				_sampletypeName = value;
			}
		}

     
        public virtual long SecretType
		{
			get { return _secretType; }
			set { _secretType = value; }
		}

      
        public virtual string PatientID
		{
			get { return _patientID; }
			set
			{
				
				_patientID = value;
			}
		}

       
        public virtual string PatNo
		{
			get { return _patNo; }
			set
			{
				
				_patNo = value;
			}
		}

      
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				
				_cName = value;
			}
		}

        
        public virtual string InpatientNo
		{
			get { return _inpatientNo; }
			set
			{
				
				_inpatientNo = value;
			}
		}

        
        public virtual string PatCardNo
		{
			get { return _patCardNo; }
			set
			{
				
				_patCardNo = value;
			}
		}

       
        public virtual long GenderNo
		{
			get { return _genderNo; }
			set { _genderNo = value; }
		}

      
        public virtual string GenderName
		{
			get { return _genderName; }
			set
			{
				
				_genderName = value;
			}
		}

       
        public virtual double Age
		{
			get { return _age; }
			set { _age = value; }
		}

        
        public virtual long AgeUnitNo
		{
			get { return _ageUnitNo; }
			set { _ageUnitNo = value; }
		}

      
        public virtual string AgeUnitName
		{
			get { return _ageUnitName; }
			set
			{
				
				_ageUnitName = value;
			}
		}

        
        public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

        
        public virtual long DistrictNo
		{
			get { return _districtNo; }
			set { _districtNo = value; }
		}

        
        public virtual string DistrictName
		{
			get { return _districtName; }
			set
			{
				
				_districtName = value;
			}
		}

       
        public virtual long WardNo
		{
			get { return _wardNo; }
			set { _wardNo = value; }
		}

       
        public virtual string WardName
		{
			get { return _wardName; }
			set
			{
				
				_wardName = value;
			}
		}

       
        public virtual string Bed
		{
			get { return _bed; }
			set
			{
				
				_bed = value;
			}
		}

        
        public virtual long DeptNo
		{
			get { return _deptNo; }
			set { _deptNo = value; }
		}

        public virtual string DeptName
		{
			get { return _deptName; }
			set
			{
				
				_deptName = value;
			}
		}

      
        public virtual long DoctorID
		{
			get { return _doctorID; }
			set { _doctorID = value; }
		}

        public virtual string Doctor
		{
			get { return _doctor; }
			set
			{
				_doctor = value;
			}
		}

        public virtual string SerialNo
		{
			get { return _serialNo; }
			set
			{
				
				_serialNo = value;
			}
		}

     
        public virtual string ParitemName
		{
			get { return _paritemName; }
			set
			{
				
				_paritemName = value;
			}
		}

       
        public virtual string DoctorItemName
		{
			get { return _doctorItemName; }
			set
			{
				
				_doctorItemName = value;
			}
		}

      
        public virtual string Collecter
		{
			get { return _collecter; }
			set
			{
				
				_collecter = value;
			}
		}

        
        public virtual DateTime? CollectDate
		{
			get { return _collectDate; }
			set { _collectDate = value; }
		}

       
        public virtual DateTime? CollectTime
		{
			get { return _collectTime; }
			set { _collectTime = value; }
		}

        public virtual string Incepter
		{
			get { return _incepter; }
			set
			{
				_incepter = value;
			}
		}

        public virtual DateTime? InceptDate
		{
			get { return _inceptDate; }
			set { _inceptDate = value; }
		}
        public virtual DateTime? InceptTime
		{
			get { return _inceptTime; }
			set { _inceptTime = value; }
		}

        public virtual long MainTesterId
		{
			get { return _mainTesterId; }
			set { _mainTesterId = value; }
		}

        public virtual string Technician
		{
			get { return _technician; }
			set
			{
				_technician = value;
			}
		}

        public virtual DateTime? TestDate
		{
			get { return _testDate; }
			set { _testDate = value; }
		}
        public virtual DateTime? TestTime
		{
			get { return _testTime; }
			set { _testTime = value; }
		}

        public virtual string Operator
		{
			get { return _operator; }
			set
			{
				_operator = value;
			}
		}

        public virtual DateTime? OperDate
		{
			get { return _operDate; }
			set { _operDate = value; }
		}

        public virtual DateTime? OperTime
		{
			get { return _operTime; }
			set { _operTime = value; }
		}

        public virtual long ExaminerId
		{
			get { return _examinerId; }
			set { _examinerId = value; }
		}

        public virtual string Checker
		{
			get { return _checker; }
			set
			{
				_checker = value;
			}
		}

        public virtual DateTime? CheckDate
		{
			get { return _checkDate; }
			set { _checkDate = value; }
		}

        public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        public virtual string FormComment
		{
			get { return _formComment; }
			set
			{
				_formComment = value;
			}
		}
        public virtual string FormMemo
		{
			get { return _formMemo; }
			set
			{
				_formMemo = value;
			}
		}

        public virtual long SickTypeNo
		{
			get { return _sickTypeNo; }
			set { _sickTypeNo = value; }
		}

        public virtual string SickTypeName
		{
			get { return _sickTypeName; }
			set
			{
				_sickTypeName = value;
			}
		}

        public virtual long DiagNo
		{
			get { return _diagNo; }
			set { _diagNo = value; }
		}

        public virtual string DiagName
		{
			get { return _diagName; }
			set
			{
				_diagName = value;
			}
		}

        public virtual long ClientNo
		{
			get { return _clientNo; }
			set { _clientNo = value; }
		}

        public virtual string ClientName
		{
			get { return _clientName; }
			set
			{
				_clientName = value;
			}
		}

        public virtual string Sender2
		{
			get { return _sender2; }
			set
			{
				_sender2 = value;
			}
		}

        public virtual int PrintTimes
		{
			get { return _printTimes; }
			set { _printTimes = value; }
		}

        public virtual int ClientPrint
		{
			get { return _clientPrint; }
			set { _clientPrint = value; }
		}

        public virtual string PrintOper
		{
			get { return _printOper; }
			set
			{
				_printOper = value;
			}
		}

        public virtual DateTime? PrintDateTime
		{
			get { return _printDateTime; }
			set { _printDateTime = value; }
		}

        public virtual string PrintOper1
		{
			get { return _printOper1; }
			set
			{
				_printOper1 = value;
			}
		}

        public virtual DateTime? PrintDateTime1
		{
			get { return _printDateTime1; }
			set { _printDateTime1 = value; }
		}

        public virtual string Resultsend
		{
			get { return _resultsend; }
			set
			{
				_resultsend = value;
			}
		}

        public virtual string Reportsend
		{
			get { return _reportsend; }
			set
			{
				_reportsend = value;
			}
		}

        public virtual int ActiveFlag
		{
			get { return _activeFlag; }
			set { _activeFlag = value; }
		}

        public virtual int AllFlag
		{
			get { return _allFlag; }
			set { _allFlag = value; }
		}

        public virtual string CollectPart
		{
			get { return _collectPart; }
			set
			{
				_collectPart = value;
			}
		}

        public virtual string TestAim
		{
			get { return _testAim; }
			set
			{
				_testAim = value;
			}
		}

        public virtual string PageName
		{
			get { return _pageName; }
			set
			{
				_pageName = value;
			}
		}

        public virtual string PageCount
		{
			get { return _pageCount; }
			set
			{
				_pageCount = value;
			}
		}

        
        public virtual DateTime? ReceiveTime
		{
			get { return _receiveTime; }
			set { _receiveTime = value; }
		}

        public virtual string ZDY1
		{
			get { return _zDY1; }
			set
			{
				_zDY1 = value;
			}
		}

        public virtual string ZDY2
		{
			get { return _zDY2; }
			set
			{
				_zDY2 = value;
			}
		}
        public virtual string ZDY3
		{
			get { return _zDY3; }
			set
			{
				_zDY3 = value;
			}
		}

        public virtual string ZDY4
		{
			get { return _zDY4; }
			set
			{
				_zDY4 = value;
			}
		}

        public virtual string ZDY5
		{
			get { return _zDY5; }
			set
			{
				_zDY5 = value;
			}
		}

        public virtual string ZDY6
		{
			get { return _zDY6; }
			set
			{
				_zDY6 = value;
			}
		}

        public virtual string ZDY7
		{
			get { return _zDY7; }
			set
			{
				_zDY7 = value;
			}
		}

        public virtual string ZDY8
		{
			get { return _zDY8; }
			set
			{
				_zDY8 = value;
			}
		}

        public virtual string ZDY9
		{
			get { return _zDY9; }
			set
			{
				_zDY9 = value;
			}
		}

        public virtual string ZDY10
		{
			get { return _zDY10; }
			set
			{
				_zDY10 = value;
			}
		}

        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        public virtual DateTime? DataMigrationTime
		{
			get { return _dataMigrationTime; }
			set { _dataMigrationTime = value; }
		}

        public virtual int STestType
		{
			get { return _sTestType; }
			set { _sTestType = value; }
		}

        public virtual string FormComment2
		{
			get { return _formComment2; }
			set
			{
				_formComment2 = value;
			}
		}

        public virtual int ReportType
		{
			get { return _reportType; }
			set { _reportType = value; }
		}

        public virtual int SectionResultType
		{
			get { return _sectionResultType; }
			set { _sectionResultType = value; }
		}

        public virtual int IQSPrintCount
		{
			get { return _iQSPrintCount; }
			set { _iQSPrintCount = value; }
		}

        public virtual string SampleType2
		{
			get { return _sampleType2; }
			set
			{
				_sampleType2 = value;
			}
		}

        public virtual string TestComment
		{
			get { return _testComment; }
			set
			{
				_testComment = value;
			}
		}

        public virtual string ReportRemark
		{
			get { return _reportRemark; }
			set
			{
				_reportRemark = value;
			}
		}

        public virtual string TestMethod
		{
			get { return _testMethod; }
			set
			{
				_testMethod = value;
			}
		}

        public virtual string AgeDesc
		{
			get { return _ageDesc; }
			set
			{
				_ageDesc = value;
			}
		}

        public virtual string ZDY11
		{
			get { return _zDY11; }
			set
			{
				_zDY11 = value;
			}
		}

        public virtual int SumPrintFlag
		{
			get { return _sumPrintFlag; }
			set { _sumPrintFlag = value; }
		}

        public virtual string DepartmentName
		{
			get { return _departmentName; }
			set
			{
				_departmentName = value;
			}
		}

        public virtual string FZDY1
		{
			get { return _fZDY1; }
			set
			{
				_fZDY1 = value;
			}
		}
        public virtual string FZDY2
		{
			get { return _fZDY2; }
			set
			{
				_fZDY2 = value;
			}
		}

        public virtual string FZDY3
		{
			get { return _fZDY3; }
			set
			{
				_fZDY3 = value;
			}
		}

        public virtual string FZDY4
		{
			get { return _fZDY4; }
			set
			{
				_fZDY4 = value;
			}
		}
        public virtual string FZDY5
		{
			get { return _fZDY5; }
			set
			{
				_fZDY5 = value;
			}
		}

        public virtual double Weight
		{
			get { return _weight; }
			set { _weight = value; }
		}

        public virtual string FFormMemo
		{
			get { return _fFormMemo; }
			set
			{
				_fFormMemo = value;
			}
		}

        public virtual string EquipName
		{
			get { return _equipName; }
			set
			{
				_equipName = value;
			}
		}

        public virtual byte[] FormResultInfo
		{
			get { return _formResultInfo; }
			set { _formResultInfo = value; }
		}
        public virtual string TestPurpose
		{
			get { return _testPurpose; }
			set
			{
				_testPurpose = value;
			}
		}

		
		#endregion
	}
	#endregion
}