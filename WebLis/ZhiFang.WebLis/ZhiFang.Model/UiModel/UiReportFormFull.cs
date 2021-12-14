using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.UiModel
{
    public class UiReportFormFull
    {
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string CLIENTNO { get; set; }
        public string SECTIONNO { get; set; }
        public string CNAME { get; set; }
        public string GENDERNAME { get; set; }
        public string SAMPLENO { get; set; }
        public string PATNO { get; set; }
        public int page { get; set; }
        public int rows { get; set; }
    }
}
