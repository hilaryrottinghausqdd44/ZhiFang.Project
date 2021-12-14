using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.UiModel
{
    public class TestItemDetail
    {
        public string CName { get; set; }
        public string ItemNo { get; set; }
        public string EName { get; set; }
        public string ColorName { get; set; }
        public string ColorValue { get; set; }
        public string Prices { get; set; }
        public List<SampleTypeDetail> SampleTypeDetail { get; set; }
    }
}
