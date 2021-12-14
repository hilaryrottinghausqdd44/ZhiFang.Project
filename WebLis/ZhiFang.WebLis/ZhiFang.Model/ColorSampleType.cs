using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    public class ColorSampleType
    {
        public string ColorName { get; set; }
        public string ColorValue { get; set; }
        public List<ZhiFang.Model.SampleType> SampleType { get; set; }
    }
}
