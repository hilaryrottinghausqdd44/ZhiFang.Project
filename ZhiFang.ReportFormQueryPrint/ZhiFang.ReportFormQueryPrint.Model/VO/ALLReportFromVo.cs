using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    public class ALLReportFromVo
    {
        string _cname; 
        string _sectionName;
        string _patNo;
        string _reportFormID;
        string _itemcname;
        string _itemvalue;
        string _itemno;
        string _itemunit;
        string _reportvalue;
        string _receiveDate;
        string _resultStatus;
        string _checkDate; 
        string _checkTime;
        string _itemename;

        public string CheckDate
        {
            get { return _checkDate; }
            set { _checkDate = value; }
        }
        public string CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        public string ResultStatus
        {
            get { return _resultStatus; }
            set { _resultStatus = value; }
        }
        public string ReceiveDate
        {
            get { return _receiveDate; }
            set { _receiveDate = value; }
        }
        public string ItemNo
        {
            get { return _itemno; }
            set { _itemno = value; }
        }
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        public string SectionName
        {
            get { return _sectionName;  }
            set { _sectionName = value; }
        }
        public string PatNo
        {
            get { return _patNo; }
            set { _patNo = value; }
        }
        public string ReportFormID
        {
            get { return _reportFormID; }
            set { _reportFormID = value; }
        }
        public string ItemCname
        {
            get { return _itemcname; }
            set { _itemcname = value; }
        }
        public string ItemValue
        {
            get { return _itemvalue; }
            set { _itemvalue = value; }
        }
        public string ItemUnit
        {
            get { return _itemunit; }
            set { _itemunit = value; }
        }
        public string ReportValue
        {
            get { return _reportvalue; }
            set { _reportvalue = value; }
        }

        public string ItemEname
        {
            get { return _itemename; }
            set { _itemename = value; }
        }
    }
}
