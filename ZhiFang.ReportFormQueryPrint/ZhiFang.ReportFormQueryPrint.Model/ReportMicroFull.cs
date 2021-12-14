using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region ReportItemFull

   
    public class ReportMicroFull
    {
        #region Member Variables

        private long _ReportPublicationID;
        private long _OrderNo;
        protected string _reportFormID;
        protected DateTime? _receiveDate;
        protected long _sectionNo;
        protected long _testTypeNo;
        protected string _sampleNo;
        protected long _itemNo;
        protected string _itemCname;
        protected string _itemEname;
        protected long _descNo;
        protected string _descName;
        protected long _microStepID;
        protected string _microStepName;
        protected long _resultID;
        protected string _reportValue;
        protected long _microNo;
        protected string _microName;
        protected string _microEame;
        protected string _microDesc;
        protected string _microResultDesc;
        protected string _itemDesc;
        protected long _antiNo;
        protected string _antiName;
        protected string _antiEName;
        protected string _suscept;
        protected double _susQuan;
        protected string _susDesc;
        protected string _antiUnit;
        protected string _refRange;
        protected string _resultState;
        protected long _equipNo;
        protected string _equipName;
        protected string _antiGroupType;
        protected string _methodName;
        protected string _serumcenTration;
        protected string _emictioncenTration;
        protected int _microdisplayid;
        protected int _antidisplayid;
        protected int _checkType;
        protected DateTime? _dataUpdateTime;
        protected DateTime? _dataMigrationTime;
        protected long _iMID;
        protected string _iMCName;
        protected string _appraisalResultCname;
        protected string _laboratoryEvaluationCname;
        protected string _pYJDF1;
        protected string _pYJDF2;
        protected string _pYJDF3;
        protected string _pYJDF4;
        protected string _pYJDF5;
        protected string _pYJDF6;
        protected string _pYJDF7;
        protected string _pYJDF8;
        protected string _pYJDF9;
        protected string _pYJDF10;
        protected string _pYJDF11;
        protected string _pYJDF12;
        protected string _pYJDF13;
        protected string _pYJDF14;
        protected string _pYJDF15;
        protected string _pYJDF16;
        protected string _pYJDF17;
        protected string _pYJDF18;
        protected string _pYJDF19;
        protected string _pYJDF20;
        protected string _tPF1;
        protected string _tPF2;
        protected string _tPF3;
        protected string _tPF4;
        protected string _tPF5;
        protected string _tPF6;
        protected string _tPF7;
        protected string _tPF8;
        protected string _tPF9;
        protected string _tPF10;
        protected string _resistancePhenotypeName;
        protected string _warnPositiveTimelength;
        protected string _microNumbe;
        protected string _testComment;
        protected string _dSTType;
        protected int _dispOrder;
        protected int _sType;
        protected string _microPositive;
        protected int _parItemNo;
        protected long _microFlag;
        protected string _antiShortCode;
        protected string _diagMethod;
        protected string _itemUnit;
        protected string _itemRefRange;
        protected int _pItemOrder;
        protected string _pAntiGroupType;


        #endregion

        #region Public Properties

        public long ReportPublicationID
        {
            set { _ReportPublicationID = value; }
            get { return _ReportPublicationID; }
        }

        public long OrderNo
        {
            set { _OrderNo = value; }
            get { return _OrderNo; }
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
                _sampleNo = value;
            }
        }

        public virtual long ItemNo
        {
            get { return _itemNo; }
            set { _itemNo = value; }
        }

        public virtual string ItemCname
        {
            get { return _itemCname; }
            set
            {
                _itemCname = value;
            }
        }

        public virtual string ItemEname
        {
            get { return _itemEname; }
            set
            {
                _itemEname = value;
            }
        }

        public virtual long DescNo
        {
            get { return _descNo; }
            set { _descNo = value; }
        }

        public virtual string DescName
        {
            get { return _descName; }
            set
            {
                _descName = value;
            }
        }

        public virtual long MicroStepID
        {
            get { return _microStepID; }
            set { _microStepID = value; }
        }

        public virtual string MicroStepName
        {
            get { return _microStepName; }
            set
            {
                _microStepName = value;
            }
        }

        public virtual long ResultID
        {
            get { return _resultID; }
            set { _resultID = value; }
        }

        public virtual string ReportValue
        {
            get { return _reportValue; }
            set
            {
                _reportValue = value;
            }
        }

        public virtual long MicroNo
        {
            get { return _microNo; }
            set { _microNo = value; }
        }

        public virtual string MicroName
        {
            get { return _microName; }
            set
            {
                _microName = value;
            }
        }

        public virtual string MicroEame
        {
            get { return _microEame; }
            set
            {
                _microEame = value;
            }
        }

        public virtual string MicroDesc
        {
            get { return _microDesc; }
            set
            {
                _microDesc = value;
            }
        }

        public virtual string MicroResultDesc
        {
            get { return _microResultDesc; }
            set
            {
                _microResultDesc = value;
            }
        }

        public virtual string ItemDesc
        {
            get { return _itemDesc; }
            set
            {
                _itemDesc = value;
            }
        }

        public virtual long AntiNo
        {
            get { return _antiNo; }
            set { _antiNo = value; }
        }

        public virtual string AntiName
        {
            get { return _antiName; }
            set
            {
                _antiName = value;
            }
        }

        public virtual string AntiEName
        {
            get { return _antiEName; }
            set
            {
                _antiEName = value;
            }
        }

        public virtual string Suscept
        {
            get { return _suscept; }
            set
            {
                _suscept = value;
            }
        }

        public virtual double SusQuan
        {
            get { return _susQuan; }
            set { _susQuan = value; }
        }

        public virtual string SusDesc
        {
            get { return _susDesc; }
            set
            {
                _susDesc = value;
            }
        }

        public virtual string AntiUnit
        {
            get { return _antiUnit; }
            set
            {
                _antiUnit = value;
            }
        }

        public virtual string RefRange
        {
            get { return _refRange; }
            set
            {
                _refRange = value;
            }
        }

        public virtual string ResultState
        {
            get { return _resultState; }
            set
            {
                _resultState = value;
            }
        }

        public virtual long EquipNo
        {
            get { return _equipNo; }
            set { _equipNo = value; }
        }

        public virtual string EquipName
        {
            get { return _equipName; }
            set
            {
                _equipName = value;
            }
        }

        public virtual string AntiGroupType
        {
            get { return _antiGroupType; }
            set
            {
                _antiGroupType = value;
            }
        }

        public virtual string MethodName
        {
            get { return _methodName; }
            set
            {
                _methodName = value;
            }
        }

        public virtual string SerumcenTration
        {
            get { return _serumcenTration; }
            set
            {
                _serumcenTration = value;
            }
        }

        public virtual string EmictioncenTration
        {
            get { return _emictioncenTration; }
            set
            {
                _emictioncenTration = value;
            }
        }

        public virtual int Microdisplayid
        {
            get { return _microdisplayid; }
            set { _microdisplayid = value; }
        }

        public virtual int Antidisplayid
        {
            get { return _antidisplayid; }
            set { _antidisplayid = value; }
        }

        public virtual int CheckType
        {
            get { return _checkType; }
            set { _checkType = value; }
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

        public virtual long IMID
        {
            get { return _iMID; }
            set { _iMID = value; }
        }

        public virtual string IMCName
        {
            get { return _iMCName; }
            set
            {
                _iMCName = value;
            }
        }

        public virtual string AppraisalResultCname
        {
            get { return _appraisalResultCname; }
            set
            {
                _appraisalResultCname = value;
            }
        }

        public virtual string LaboratoryEvaluationCname
        {
            get { return _laboratoryEvaluationCname; }
            set
            {
                _laboratoryEvaluationCname = value;
            }
        }

        public virtual string PYJDF1
        {
            get { return _pYJDF1; }
            set
            {
                _pYJDF1 = value;
            }
        }

        public virtual string PYJDF2
        {
            get { return _pYJDF2; }
            set
            {
                _pYJDF2 = value;
            }
        }

        public virtual string PYJDF3
        {
            get { return _pYJDF3; }
            set
            {
                _pYJDF3 = value;
            }
        }

        public virtual string PYJDF4
        {
            get { return _pYJDF4; }
            set
            {
                _pYJDF4 = value;
            }
        }

        public virtual string PYJDF5
        {
            get { return _pYJDF5; }
            set
            {
                _pYJDF5 = value;
            }
        }

        public virtual string PYJDF6
        {
            get { return _pYJDF6; }
            set
            {
                _pYJDF6 = value;
            }
        }

        public virtual string PYJDF7
        {
            get { return _pYJDF7; }
            set
            {
                _pYJDF7 = value;
            }
        }

        public virtual string PYJDF8
        {
            get { return _pYJDF8; }
            set
            {
                _pYJDF8 = value;
            }
        }
        public virtual string PYJDF9
        {
            get { return _pYJDF9; }
            set
            {
                _pYJDF9 = value;
            }
        }

        public virtual string PYJDF10
        {
            get { return _pYJDF10; }
            set
            {
                _pYJDF10 = value;
            }
        }

        public virtual string PYJDF11
        {
            get { return _pYJDF11; }
            set
            {
                _pYJDF11 = value;
            }
        }

        public virtual string PYJDF12
        {
            get { return _pYJDF12; }
            set
            {
                _pYJDF12 = value;
            }
        }

        public virtual string PYJDF13
        {
            get { return _pYJDF13; }
            set
            {
                _pYJDF13 = value;
            }
        }

        public virtual string PYJDF14
        {
            get { return _pYJDF14; }
            set
            {
                _pYJDF14 = value;
            }
        }

        public virtual string PYJDF15
        {
            get { return _pYJDF15; }
            set
            {
                _pYJDF15 = value;
            }
        }

        public virtual string PYJDF16
        {
            get { return _pYJDF16; }
            set
            {
                _pYJDF16 = value;
            }
        }

        public virtual string PYJDF17
        {
            get { return _pYJDF17; }
            set
            {
                _pYJDF17 = value;
            }
        }

        public virtual string PYJDF18
        {
            get { return _pYJDF18; }
            set
            {
                _pYJDF18 = value;
            }
        }

        public virtual string PYJDF19
        {
            get { return _pYJDF19; }
            set
            {
                _pYJDF19 = value;
            }
        }

        public virtual string PYJDF20
        {
            get { return _pYJDF20; }
            set
            {
                _pYJDF20 = value;
            }
        }

        public virtual string TPF1
        {
            get { return _tPF1; }
            set
            {
                _tPF1 = value;
            }
        }

        public virtual string TPF2
        {
            get { return _tPF2; }
            set
            {
                _tPF2 = value;
            }
        }
        public virtual string TPF3
        {
            get { return _tPF3; }
            set
            {
                _tPF3 = value;
            }
        }

        public virtual string TPF4
        {
            get { return _tPF4; }
            set
            {
                _tPF4 = value;
            }
        }

        public virtual string TPF5
        {
            get { return _tPF5; }
            set
            {
                _tPF5 = value;
            }
        }

        public virtual string TPF6
        {
            get { return _tPF6; }
            set
            {
                _tPF6 = value;
            }
        }

        public virtual string TPF7
        {
            get { return _tPF7; }
            set
            {
                _tPF7 = value;
            }
        }

        public virtual string TPF8
        {
            get { return _tPF8; }
            set
            {
                _tPF8 = value;
            }
        }

        public virtual string TPF9
        {
            get { return _tPF9; }
            set
            {
                _tPF9 = value;
            }
        }

        public virtual string TPF10
        {
            get { return _tPF10; }
            set
            {
                _tPF10 = value;
            }
        }

        public virtual string ResistancePhenotypeName
        {
            get { return _resistancePhenotypeName; }
            set
            {
                _resistancePhenotypeName = value;
            }
        }

        public virtual string WarnPositiveTimelength
        {
            get { return _warnPositiveTimelength; }
            set
            {
                _warnPositiveTimelength = value;
            }
        }

        public virtual string MicroNumbe
        {
            get { return _microNumbe; }
            set
            {
                _microNumbe = value;
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

        public virtual string DSTType
        {
            get { return _dSTType; }
            set
            {
                _dSTType = value;
            }
        }

        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        public virtual int SType
        {
            get { return _sType; }
            set { _sType = value; }
        }

        public virtual string MicroPositive
        {
            get { return _microPositive; }
            set
            {
                _microPositive = value;
            }
        }

        public virtual int ParItemNo
        {
            get { return _parItemNo; }
            set { _parItemNo = value; }
        }

        public virtual long MicroFlag
        {
            get { return _microFlag; }
            set { _microFlag = value; }
        }

        public virtual string AntiShortCode
        {
            get { return _antiShortCode; }
            set
            {
                _antiShortCode = value;
            }
        }

        public virtual string DiagMethod
        {
            get { return _diagMethod; }
            set
            {
                _diagMethod = value;
            }
        }

        public virtual string ItemUnit
        {
            get { return _itemUnit; }
            set
            {
                _itemUnit = value;
            }
        }

        public virtual string ItemRefRange
        {
            get { return _itemRefRange; }
            set
            {
                _itemRefRange = value;
            }
        }

        public virtual int PItemOrder
        {
            get { return _pItemOrder; }
            set { _pItemOrder = value; }
        }

        public virtual string PAntiGroupType
        {
            get { return _pAntiGroupType; }
            set
            {
                _pAntiGroupType = value;
            }
        }


        #endregion
    }
    #endregion
}