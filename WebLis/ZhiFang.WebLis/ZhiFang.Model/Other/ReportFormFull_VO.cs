using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.Other
{
    public class ReportFormFull_VO : ReportFormFull
    {
        public List<ReportItemFull> ReportItemList { get; set; }
        public List<ReportMicroFull> ReportMicroList { get; set; }
        public List<ReportMarrowFull> ReportMarrowList { get; set; }
    }
}
