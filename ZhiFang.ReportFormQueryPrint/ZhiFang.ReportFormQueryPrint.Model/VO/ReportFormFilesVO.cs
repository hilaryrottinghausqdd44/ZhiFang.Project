using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ZhiFang.ReportFormQueryPrint.Model.VO
{
    [DataContract]
    public class ReportFormFilesVO
    {
        private string _ReportFormID;
        private string _PDFPath;
        private List<string> _JpgPath;
        private string _PageName;
        private string _PageCount;

        [DataMember]
        public string ReportFormID
        {
            get { return _ReportFormID; }
            set { _ReportFormID = value; }
        }

        [DataMember]
        public string PDFPath
        {
            get { return _PDFPath; }
            set { _PDFPath = value; }
        }

        [DataMember]
        public List<string> JpgPath
        {
            get { return _JpgPath; }
            set { _JpgPath = value; }
        }

        [DataMember]
        public string PageName
        {
            get { return _PageName; }
            set { _PageName = value; }
        }

        [DataMember]
        public string PageCount
        {
            get { return _PageCount; }
            set { _PageCount = value; }
        }
    }
}