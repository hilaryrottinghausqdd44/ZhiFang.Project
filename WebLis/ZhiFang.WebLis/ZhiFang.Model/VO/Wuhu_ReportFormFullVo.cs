using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.VO
{
    public class Wuhu_ReportFormFullVo
    {
        public string SampleCount { get; set; }
        public string SickTypeCount { get; set; }
        public List<Wuhu_SickTypeSample> WHSickTypeSample { get; set; }
        public List<Wuhu_SampleFigure> WHSampleFigure { get; set; }
        public List<Wuhu_SickTypeFigure> WHSickTypeFigure { get; set; }
    }
    public class Wuhu_SickTypeFigure
    {
        public string Times { get; set; }
        public string Count { get; set; }
    }
    public class Wuhu_SampleFigure
    {
        public string Times { get; set; }
        public string Count { get; set; }
    }
    public class Wuhu_SickTypeSample
    {
        public string Name { get; set; }
        public string Count { get; set; }
        public string ALLCount { get; set; }
    }
}
