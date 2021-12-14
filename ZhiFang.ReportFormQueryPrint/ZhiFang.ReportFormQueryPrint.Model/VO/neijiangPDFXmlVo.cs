using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.Model.VO
{
    [DataContract]
    public class neijiangPDFXmlVo
    {
        private string _reportid;
        private string _patientId;
        private string _patientName;
        private string _age;
        private string _sex;
        private string _bedNo;
        private string _examType;
        private string _checkPart;
        private string _checkDoc;
        private string _examResult;
        private string _sampleType;
        private string _sampleMark;
        private string _receiveTime;
        private string _resultTime;
        private string _printTimes;
        private string _auditTime;
        private string _Status;


        [DataMember]
        public string reportId
        {
            set { _reportid = value; }
            get { return _reportid; }
        }

        [DataMember]
        public string patientId
        {
            set { _patientId = value; }
            get { return _patientId; }
        }
        [DataMember]
        public string patientName
        {
            set { _patientName = value; }
            get { return _patientName; }
        }
        [DataMember]
        public string age
        {
            set { _age = value; }
            get { return _age; }
        }
        [DataMember]
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        [DataMember]
        public string bedNo
        {
            set { _bedNo = value; }
            get { return _bedNo; }
        }
        [DataMember]
        public string examType
        {
            set { _examType = value; }
            get { return _examType; }
        }
        [DataMember]
        public string checkPart
        {
            set { _checkPart = value; }
            get { return _checkPart; }
        }
        [DataMember]
        public string checkDoc
        {
            set { _checkDoc = value; }
            get { return _checkDoc; }
        }
        [DataMember]
        public string examResult
        {
            set { _examResult = value; }
            get { return _examResult; }
        }
        [DataMember]
        public string sampleType
        {
            set { _sampleType = value; }
            get { return _sampleType; }
        }
        [DataMember]
        public string sampleMark
        {
            set { _sampleMark = value; }
            get { return _sampleMark; }
        }
        [DataMember]
        public string receiveTime
        {
            set { _receiveTime = value; }
            get { return _receiveTime; }
        }
        [DataMember]
        public string resultTime
        {
            set { _resultTime = value; }
            get { return _resultTime; }
        }
        [DataMember]
        public string printTimes
        {
            set { _printTimes = value; }
            get { return _printTimes; }
        }
        [DataMember]
        public string auditTime
        {
            set { _auditTime = value; }
            get { return _auditTime; }
        }
        [DataMember]
        public string Status
        {
            set { _Status = value; }
            get { return _Status; }
        }

    }

    public class feixiVo {

        private string _reportformid;
        private string _zdy4;
        private string _cname;
        private string _paritemname;
        private DateTime? _checkdate;
        private DateTime? _checktime;
        private long? _printtimes;
        private string _reportstatus;


        [DataMember]
        public long? PRINTTIMES
        {
            set { _printtimes = value; }
            get { return _printtimes; }
        }
        [DataMember]
        public string ReportFormID
        {
            get { return _reportformid; }
            set { _reportformid = value; }
        }
        [DataMember]
        public string CNAME
        {
            set { _cname = value; }
            get { return _cname; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? CHECKDATE
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? CHECKTIME
        {
            set { _checktime = value; }
            get { return _checktime; }
        }
        [DataMember]
        public string ZDY4
        {
            set { _zdy4 = value; }
            get { return _zdy4; }
        }

        /// <summary>
        /// 报告状态  不属于表字段
        /// </summary>
        public string ReportStatus
        {
            get { return _reportstatus; }
            set { _reportstatus = value; }
        }
        [DataMember]
        public string ParItemName
        {
            get { return _paritemname; }
            set { _paritemname = value; }
        }

    }

    
}