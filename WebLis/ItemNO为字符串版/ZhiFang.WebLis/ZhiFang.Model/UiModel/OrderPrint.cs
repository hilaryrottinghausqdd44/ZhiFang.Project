using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.UiModel
{
    public partial class OrderPrint
    {
        /// <summary>
        /// 
        /// </summary>
        public string OrderNo { get; set; }
        public string SendLabName { get; set; }
        public string OperSartDate { get; set; }
        public string OperEndDate { get; set; }
        public string ReciveCompany { get; set; }
        public List<NRequestFormResult> nrequestFormResultList { get; set; }
        public int? BarcodeSum { get; set; }

    }
}
