using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;

namespace ZhiFang.IBLL.Common
{
    public interface ILib
    {
       Dictionary<string, ColorSampleType> ItemColor();
       List<Model.SampleType> GetSampleTypeByColorName(string colorName);
    }
}
