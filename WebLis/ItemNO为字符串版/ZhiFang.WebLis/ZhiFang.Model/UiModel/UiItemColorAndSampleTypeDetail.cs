using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.UiModel
{
    public class UiItemColorAndSampleTypeDetail
    {
        public List<UiItemColorSampleTypeNo> UiItemColorSampleTypeNo { get; set; }
    }

    public class UiItemColorSampleTypeNo
    {
        public string ColorId { get; set; }
        public string ColorName { get; set; }
        public List<string> SampleTypeNoList { get; set; }
        public List<string> SampleTypeNameList { get; set; }
    }

}
