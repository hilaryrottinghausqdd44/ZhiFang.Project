using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region RFPReportFormPrintOperation

    /// <summary>
    /// RFPReportFormPrintOperation object for NHibernate mapped table 'RFP_ReportFormPrint_Operation'.
    /// </summary>
    [DataContract]
    public class RFPReportFormPrintOperation
    {
        #region Member Variables
        protected long _labID = 0;
        protected long _RFPOperationID;
        protected long _bobjectID;
        protected DateTime? _receiveDate;
        protected int _sectionNo;
        protected int _testTypeNo;
        protected string _sampleNo;
        protected string _station;
        protected long _empID;
        protected string _empName;
        protected long _deptId;
        protected string _deptName;
        protected long _type;
        protected string _typeName;
        protected string _businessModuleCode;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
        private DateTime? _dataAddTime;
        private byte[] _dataTimeStamp;


        #endregion

        #region Constructors

        public RFPReportFormPrintOperation() { }

        public RFPReportFormPrintOperation(long labID,long RFPOperationID, long bobjectID, DateTime receiveDate, int sectionNo, int testTypeNo, string sampleNo, string station, long empID, string empName, long deptId, string deptName, long type, string typeName, string businessModuleCode, string memo, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._RFPOperationID = RFPOperationID;
            this._bobjectID = bobjectID;
            this._receiveDate = receiveDate;
            this._sectionNo = sectionNo;
            this._testTypeNo = testTypeNo;
            this._sampleNo = sampleNo;
            this._station = station;
            this._empID = empID;
            this._empName = empName;
            this._deptId = deptId;
            this._deptName = deptName;
            this._type = type;
            this._typeName = typeName;
            this._businessModuleCode = businessModuleCode;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._creatorID = creatorID;
            this._creatorName = creatorName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public virtual long LabID
        {
            get { return _labID; }
            set { _labID = value; }
        }

        [DataMember]
        public virtual long RFPOperationID
        {
            get { return _RFPOperationID; }
            set { _RFPOperationID = value; }
        }


        [DataMember]
        public virtual long BobjectID
        {
            get { return _bobjectID; }
            set { _bobjectID = value; }
        }

        [DataMember]
        public virtual DateTime? ReceiveDate
        {
            get { return _receiveDate; }
            set { _receiveDate = value; }
        }

        [DataMember]
        public virtual int SectionNo
        {
            get { return _sectionNo; }
            set { _sectionNo = value; }
        }

        [DataMember]
        public virtual int TestTypeNo
        {
            get { return _testTypeNo; }
            set { _testTypeNo = value; }
        }

        [DataMember]
        public virtual string SampleNo
        {
            get { return _sampleNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SampleNo", value, value.ToString());
                _sampleNo = value;
            }
        }

        [DataMember]
        public virtual string Station
        {
            get { return _station; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Station", value, value.ToString());
                _station = value;
            }
        }

        [DataMember]
        public virtual long EmpID
        {
            get { return _empID; }
            set { _empID = value; }
        }

        [DataMember]
        public virtual string EmpName
        {
            get { return _empName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
                _empName = value;
            }
        }

        [DataMember]
        public virtual long DeptId
        {
            get { return _deptId; }
            set { _deptId = value; }
        }

        [DataMember]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
                _deptName = value;
            }
        }

        [DataMember]
        public virtual long Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [DataMember]
        public virtual string TypeName
        {
            get { return _typeName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
                _typeName = value;
            }
        }

        [DataMember]
        public virtual string BusinessModuleCode
        {
            get { return _businessModuleCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BusinessModuleCode", value, value.ToString());
                _businessModuleCode = value;
            }
        }

        [DataMember]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        public virtual long CreatorID
        {
            get { return _creatorID; }
            set { _creatorID = value; }
        }

        [DataMember]
        public virtual string CreatorName
        {
            get { return _creatorName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
                _creatorName = value;
            }
        }

        [DataMember]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}