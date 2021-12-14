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
    public class qinhuangdaodiyiPdfXmlVO
    {
        private string _reportid;
        private string _reporttype;
        private string _status;
        private string _type;
        private string _jydate;
        private string _doctor;
        private string _reportData;


        [DataMember]
        public string ReportId
        {
            set { _reportid = value; }
            get { return _reportid; }
        }
        [DataMember]
        public string reportType
        {
            set { _reporttype = value; }
            get { return _reporttype; }
        }
        [DataMember]
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
            [DataMember]
            public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
            [DataMember]
        public string JYDate
        {
            set { _jydate = value; }
            get { return _jydate; }
        }
            [DataMember]
        public string Doctor
        {
            set { _doctor = value; }
            get { return _doctor; }
        }


        [DataMember]
        public string ReportData
        {
            set { _reportData = value; }
            get { return _reportData; }
        }

    }

    [DataContract]
    public class ReportPdfXml
    {

        private List<qinhuangdaodiyiPdfXmlVO> _response;

        [DataMember]
        public List<qinhuangdaodiyiPdfXmlVO> Response
        {
            set { _response = value; }
            get { return _response; }
        }

       

    }
}