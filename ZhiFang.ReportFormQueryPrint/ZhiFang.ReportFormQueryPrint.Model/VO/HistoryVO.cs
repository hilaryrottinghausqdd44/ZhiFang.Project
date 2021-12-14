using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.ReportFormQueryPrint.Model.VO
{
    public class HistoryVO
    {
        public string ReceiveDate { get; set; }
        public double ReportValue { get; set; }
        public string ReportDesc { get; set; }
        public string CheckDate { get; set; }
        public string CheckTime { get; set; }
        public string ItemNo { get; set; }
        public string ItemCName { get; set; }
        public string ItemEName { get; set; }
        public string ItemSName { get; set; }
        public string RefRange { get; set; }
        public string GTestDate { get; set; }
        public string SName { get; set; }
    }
}